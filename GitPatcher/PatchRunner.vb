Imports Oracle.ManagedDataAccess.Client ' VB.NET


Public Class PatchRunner

    Private waiting As Boolean

    'Dim unappliedPatches As Collection = New Collection()
    Dim AvailablePatches As Collection = New Collection

    Dim hotFixTargetDBFilter As String = Nothing

    Public Sub New(ByRef foundNone As Boolean, Optional ByVal iInstallStatus As String = "All", Optional ByVal iBranchType As String = "")
        InitializeComponent()
        foundNone = False

        'Other legal values Unapplied and Uninstalled
        ComboBoxPatchesFilter.SelectedItem = iInstallStatus

        Me.Text = "PatchRunner - Running patches on " & Globals.currentTNS

        'PatchFilterGroupBox.Text = Globals.currentTNS & " Search Criteria"


        hotFixTargetDBFilter = Globals.getDB()
        If hotFixTargetDBFilter = "VM" Then
            hotFixTargetDBFilter = "DEV"
        End If

        RadioButtonHotfix.Text = "Hotfixes for " & hotFixTargetDBFilter

        If iBranchType = "" Then
            iBranchType = Globals.getPatchRunnerFilter
        End If

        Logger.Note("iBranchType", iBranchType)
        Select Case iBranchType
            Case "feature"
                RadioButtonFeature.Checked = True
            Case "hotfix"
                RadioButtonHotfix.Checked = True
            Case "patchset"
                RadioButtonPatchSet.Checked = True
            Case "all"
                RadioButtonAll2.Checked = True
            Case Else
                RadioButtonAll2.Checked = True
        End Select

        UsePatchAdminCheckBox.Checked = Globals.getUseARM

        'Hide Order and Run tabs
        'PatchRunnerTabControl.TabPages.Remove(OrderTabPage)
        PatchRunnerTabControl.TabPages.Remove(RunTabPage)

        'No longer supporting Patch Exports
        PatchRunnerTabControl.TabPages.Remove(ExportTabPage)



        Me.MdiParent = GitPatcher
        If doSearch() > 0 Then
            Me.Show()
            Wait()
        Else
            foundNone = True
            Me.Close()
        End If


    End Sub

    Private Sub Wait()
        'Wait until the form is closed.
        waiting = True
        Do Until Not waiting
            Common.Wait()
        Loop
    End Sub

    Private Sub PatchRunner_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        waiting = False
    End Sub



    Shared Function GetlastSuccessfulPatch() As String
        Dim lastSuccessfulPatch As String = "Patched"

        lastSuccessfulPatch = OracleSQL.QueryToString("select patch_name from arm_patch where success_yn = 'Y' order by log_datetime desc", "patch_name")

        Logger.Dbg(lastSuccessfulPatch)

        Return lastSuccessfulPatch

    End Function



    Shared Function GetUnappliedAppCode(ByVal iPatchName As String) As String
    'get the app_code from the relevant Unapplied/Unpromoted view
        Logger.Dbg("GetUnappliedAppCode(" & iPatchName & ")")

        Dim lAppCode As String = Nothing

        If Globals.getDB = "DEV" Then

            'Change to VM
            Globals.setDB("VM")

            'Get AppCode from unpromoted patches
            lAppCode = OracleSQL.QueryToString("select app_code from ARM_PROMOTED_V where patch_name = '" & iPatchName & "'", "app_code")

            'Change back to DEV
            Globals.setDB("DEV")

        Else
            'Get AppCode from unapplied patches from the target DB
            lAppCode = OracleSQL.QueryToString("select app_code from ARM_UNAPPLIED_V where patch_name = '" & iPatchName & "'", "app_code")
        End If

        Logger.Dbg("lAppCode=" & lAppCode)
        Return lAppCode

    End Function



    Private Sub RecursiveSearch(ByVal strPath As String, ByVal strPattern As String, ByRef lstTarget As ListBox)

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)
        Dim clsFile As FileInfoEx = Nothing

        'Add the files
        For Each strFile As String In strFiles
            clsFile = New FileInfoEx(strFile)
            lstTarget.Items.Add(clsFile)
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearch(strFolder, strPattern, lstTarget)
        Next

    End Sub




    Public Shared Function ReorderByDependancy(ByRef chosenPatches As Collection, ByRef availablePatches As Collection) As Collection


        Dim orderedPatches As Collection = New Collection

        'loop through availablePatches and add to the orderedPatches if present in the chosenPatches set.
        For Each availablePatch In availablePatches

            If chosenPatches.Contains(availablePatch) Then
                orderedPatches.Add(availablePatch, availablePatch)  'Add patches that are found
                chosenPatches.Remove(availablePatch)                'Remove patches that are found

            End If

        Next

        If chosenPatches.Count > 0 Then
            'but there should never be any
            Common.MsgBoxCollection(chosenPatches, "Unorderable Patches!",
                      "Could not determine install order for these patches on " & Globals.getDB & " . Added to the end of the release." & Chr(10) &
                      "They may not yet have been applied to the reference database, or perhaps already applied to the target database.")

        End If

        For Each unorderPatch In chosenPatches

            orderedPatches.Add(unorderPatch, unorderPatch)  'Add patches that are found
            chosenPatches.Remove(unorderPatch)              'Remove patches that are found

        Next


        Return orderedPatches

    End Function





    Public Shared Sub FindPatches(ByRef foundPatches As Collection, ByVal iHideInstalled As Boolean)
        'OLDER STYLE

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundPatches.Clear()
        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", foundPatches, Globals.RootPatchDir)

        End If

        If iHideInstalled Then

            'http://www.oracle.com/technetwork/articles/dotnet/cook-dotnet-101788.html

            'Now remove patches that have already been applied to the database.

            Dim patchMatch As Boolean = False

            Dim conn As New OracleConnection(Globals.getARMconnection)
            Dim sql As String = Nothing
            Dim cmd As OracleCommand
            Dim dr As OracleDataReader


            Try

                conn.Open()

                'process in reverse order, because removing items from the list, changes the indexes.  reverse order will not be affected.
                For i As Integer = foundPatches.Count To 1 Step -1

                    'Check whether the patch has been successfully installed. (convert to uppercase)
                    patchMatch = False
                    Dim lPatchName As String = Common.getLastSegment(foundPatches(i).ToUpper(), "\")

                    sql = "select max(patch_name) patch_name from ARM_INSTALLED_PATCH_V where patch_name = '" & lPatchName & "'"

                    cmd = New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    dr = cmd.ExecuteReader()
                    dr.Read()

                    If Not IsDBNull(dr.Item("patch_name")) Then
                        'patch matches the search
                        patchMatch = True
                    End If

                    If patchMatch Then
                        'patch is to be filtered from the list.
                        foundPatches.Remove(i)

                    End If
                    dr.Close()

                Next


                conn.Close()
                conn.Dispose()


            Catch ex As Exception ' catches any error
                MessageBox.Show(ex.Message.ToString())
            Finally
                ' In a real application, put cleanup code here.

            End Try

        End If

        If foundPatches.Count = 0 Then
            MsgBox("No patches matched the search criteria.", MsgBoxStyle.Information, "No patches found")
        End If


        Cursor.Current = cursorRevert


    End Sub


    Public Shared Sub FindUnappliedPatches(ByRef foundPatches As Collection)
        'THIS IS THE NEWER STYLE

        Dim unappliedPatches As Collection = New Collection()

        If Globals.getDB = "DEV" Then

            'Get patch list from the VM
            'Change to VM to query the Unpromoted view of the VM db

            'Change to VM
            Globals.setDB("VM")
            'OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

            'Get a list of unpromoted patches
            unappliedPatches = OracleSQL.GetUnpromotedPatches()

            'Change back to DEV
            Globals.setDB("DEV")
            'OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

        Else
            'Get a list of unapplied patches from the target DB
            unappliedPatches = OracleSQL.GetUnappliedPatches()
        End If

        'Get a list of patches in the repository checkout
        Dim repoPatches As Collection = FileIO.FindRepoPatches()

        Dim MissingPatchList As Collection = New Collection

        foundPatches.Clear()

        'This time loop through unapplied patches first and show in list if available in dir.
        For Each unappliedPatch In unappliedPatches

            Dim l_patch_name As String = unappliedPatch.ToString
            Dim l_patch_found As Boolean = False

            For i As Integer = 1 To repoPatches.Count

                If Common.getLastSegment(repoPatches(i), "\").ToUpper() = l_patch_name Then 'convert to uppercase
                    foundPatches.Add(repoPatches(i), repoPatches(i))
                    l_patch_found = True
                End If

            Next

            If Not l_patch_found Then
                Dim l_app_code As String = GetUnappliedAppCode(l_patch_name)
                MissingPatchList.Add("App Code: " & l_app_code & "  Patch: " & l_patch_name, l_patch_name)
            End If

        Next

        If MissingPatchList.Count > 0 Then
            Common.MsgBoxCollection(MissingPatchList _
                  , "More Patches to be applied" _
                  , "Current Repo: " & Globals.getRepoName & "    Current Branch: " & Globals.currentBranch & Environment.NewLine & Environment.NewLine &
                    "These Unapplied Patches are ALSO ready to be applied." & Environment.NewLine &
                    "They are not present in the current branch." & Environment.NewLine & Environment.NewLine &
                    "These patches may be from another repo, or another branch, or may indeed be missing from this repo-branch." & Environment.NewLine &
                    "You do not have to install these patches first, but please consider installing them afterwards." & Environment.NewLine & Environment.NewLine &
                    "If the patches should be in this repo, they may have been installed in the reference database, " & Environment.NewLine &
                    "but either not committed to the feature branch, merged to the master branch, or pushed to the origin repo." & Environment.NewLine
                   )

        End If

        'If foundPatches.Count = 0 Then
        ' MsgBox("No patches were found, that matched the search criteria.", MsgBoxStyle.Information, "No Patches Found")
        ' End If


    End Sub



    Private Sub filterPatchType(ByRef foundPatches As Collection)


        Dim searchTerm As String = "all"
        If Not RadioButtonAll2.Checked And foundPatches.Count > 0 Then


            If RadioButtonFeature.Checked Then
                searchTerm = "feature"
            ElseIf RadioButtonHotfix.Checked Then
                searchTerm = "hotfix"
            ElseIf RadioButtonPatchSet.Checked Then
                searchTerm = "patchset"
            End If

            For i As Integer = foundPatches.Count To 1 Step -1
                If Not foundPatches(i).contains(searchTerm) Then
                    'This patch does not match the filter and will be removed from the list
                    foundPatches.Remove(i)

                ElseIf foundPatches(i).contains("hotfix") And Not foundPatches(i).contains("_" & hotFixTargetDBFilter) Then
                    'Filter out hotfixes that do not match the current database
                    foundPatches.Remove(i)
                End If


            Next

            If foundPatches.Count = 0 Then
                MsgBox("No patches matched the Filter: " & searchTerm, MsgBoxStyle.Information, "No patches found")
            End If


        End If
        Globals.setPatchRunnerFilter(searchTerm)


    End Sub



    Private Function doSearch() As Integer
        Logger.Dbg("Searching")

        'Dim AvailablePatches As Collection = New Collection

        'AvailablePatches is a private class variable 

        AvailablePatchesTreeView.Nodes.Clear()
        'PatchRunnerTabControl.TabPages.Remove(OrderTabPage)
        PatchRunnerTabControl.TabPages.Remove(RunTabPage)

        If ComboBoxPatchesFilter.SelectedItem = "Unapplied" Then
            FindUnappliedPatches(AvailablePatches) 'AvailablePatches will be ordered
        ElseIf ComboBoxPatchesFilter.SelectedItem = "Uninstalled" Then
            FindPatches(AvailablePatches, ComboBoxPatchesFilter.SelectedItem = "Uninstalled")
        ElseIf ComboBoxPatchesFilter.SelectedItem = "All" Then
            FindPatches(AvailablePatches, False) 'Find patches without doing any db search.
        Else
            MsgBox("Choose type of patch to search for.", MsgBoxStyle.Exclamation, "Choose Search criteria")
        End If

        If AvailablePatches.Count = 0 Then
            MsgBox("No " & ComboBoxPatchesFilter.SelectedItem & " patches were found.", MsgBoxStyle.Information, "No Patches Found")
        Else
            Logger.Dbg("Filtering")
            filterPatchType(AvailablePatches)
            If AvailablePatches.Count > 0 Then

                Logger.Dbg("Populate Tree")

                'Populate the treeview, tick unapplied by default
                'Once in the tree view patches are no longer ordered, because they are grouped in paths.
                AvailablePatchesTreeView.populateTreeFromCollection(AvailablePatches _
                    , ComboBoxPatchesFilter.SelectedItem = "Unapplied")

                'Show Order tab, Keep Run tab hidden
                'PatchRunnerTabControl.TabPages.Insert(1, OrderTabPage)


            End If

        End If

        Return AvailablePatches.Count

    End Function




    Private Sub SearchPatchesButton_Click(sender As Object, e As EventArgs) Handles SearchPatchesButton.Click
        doSearch()
    End Sub


    Public Shared Sub FindNewLogFiles(ByRef foundLogs As Collection)
        'OLDER STYLE
        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundLogs.Clear()

        Dim availableLogs As Collection = New Collection

        availableLogs = FileIO.FileList(Globals.RootPatchDir, "*.log", Globals.RootPatchDir, True)

        Dim conn As New OracleConnection(Globals.getARMconnection)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim missingLogs As Collection = New Collection

        'Loop through arm_log records that were recently logged by the current OS_USER
        Try

            conn.Open()

            sql = "select patch_name, log_filename from ARM_LOG_V where ready_to_load = 'Y' and logged_by_me = 'Y'"


            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                Dim l_patch_name As String = dr.Item("patch_name")
                Dim l_log_filename As String = dr.Item("log_filename")
                Dim l_log_found As Boolean = False

                If Not availableLogs.Contains(l_log_filename) Then
                    'MsgBox("WARNING: New Log File " & l_log_filename & " is not present in the log dir " & Globals.RootPatchDir)
                    missingLogs.Add(l_log_filename, l_log_filename)
                End If

                'Include all log files, even if missing.
                foundLogs.Add(l_patch_name, l_patch_name)

            End While

            dr.Close()

            conn.Close()
            conn.Dispose()


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try

        If missingLogs.Count > 0 Then

            MsgBox("Log Dir: " & Globals.RootPatchDir & Chr(10) & Chr(10) _
                 & Common.CollectionToText(missingLogs), MsgBoxStyle.Information, "These Log Files are missing and will not be loaded.")

        End If

        'If foundLogs.Count = 0 Then
        '    MsgBox("Log files are already loaded.", MsgBoxStyle.Information, "No Logs to be loaded.")
        'Else
        '    MsgBox("Log Dir: " & Globals.RootPatchDir & Chr(10) & Chr(10) _
        '        & Common.CollectionToText(foundLogs), MsgBoxStyle.Information, "These Log Files are to be loaded.")
        'End If

        Cursor.Current = cursorRevert

    End Sub

    Shared Sub LoadNewLogFiles()
        'Find any log files that have not been loaded.
        Dim newLogs As Collection = New Collection
        FindNewLogFiles(newLogs)

        If newLogs.Count > 0 Then

            'Format as script
            Dim masterLoadLogs As String = Nothing

            masterLoadLogs = "DEFINE database = '" & Globals.getDATASOURCE & "'" &
                             Environment.NewLine &
                             "DEFINE load_log_file = '" & Globals.getGPScriptsDir & "loadlogfile.js " & Globals.getOrgCode & " " & Globals.getDB & "'" &
                             Environment.NewLine &
                             "@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql" &
                             Environment.NewLine &
                             "CONNECT " & Globals.getARMuser & "/" & Globals.getARMpword & "@&&database"

            For i As Integer = 1 To newLogs.Count

                masterLoadLogs = masterLoadLogs & Environment.NewLine & "script &&load_log_file " & newLogs(i)

            Next

            masterLoadLogs = masterLoadLogs & Environment.NewLine & "commit;"
            masterLoadLogs = masterLoadLogs & Environment.NewLine & "exit;"

            Logger.Dbg(masterLoadLogs, "MasterLoadLogs script")

            'Use Host class to execute with a master script.
            Host.RunMasterScript(masterLoadLogs, Globals.RootPatchDir)

        End If
    End Sub


    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click


        'Confirm run against non-VM target
        If Globals.getDB <> "VM" Then
            Dim result As Integer = MessageBox.Show("Confirm target is " & Globals.getDB &
      Chr(10) & "The Patches will be installed in " & Globals.getDB & ".", "Confirm Target", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        'Format as script
        Dim masterList As String = Nothing

        For i As Integer = 0 To MasterScriptListBox.Items.Count - 1

            masterList = masterList & Chr(10) & MasterScriptListBox.Items(i).ToString()

        Next

        'Use Host class to execute with a master script.
        Host.RunMasterScript(masterList, Globals.RootPatchDir)

        LoadNewLogFiles()

    End Sub


    Private Sub PopMasterScriptListBox()

        MasterScriptListBox.Items.Clear()

        MasterScriptListBox.Items.Add("DEFINE database = '" & Globals.getDATASOURCE & "'")
        MasterScriptListBox.Items.Add("DEFINE load_log_file = '" & Globals.getGPScriptsDir & "loadlogfile.js " & Globals.getOrgCode & " " & Globals.getDB & "'")
        MasterScriptListBox.Items.Add("@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql")

        Dim l_master_filename As String = Nothing

        If UsePatchAdminCheckBox.Checked Then
            l_master_filename = "install.sql"
        Else
            l_master_filename = "install_lite.sql"
        End If


        Dim ReorderedChanges As Collection = New Collection
        TreeViewPatchOrder.ReadTags(ReorderedChanges, False, True, True, False)

        If ReorderedChanges.Count = 0 Then
            MsgBox("No patches in ordered list.")
        Else

            For Each lpatch In ReorderedChanges
                MasterScriptListBox.Items.Add("@" & lpatch & "\" & l_master_filename)
            Next
        End If


    End Sub


    Private Sub PopPatchListBox()

        PatchListBox.Items.Clear()
 
        Dim ReorderedChanges As Collection = New Collection
        TreeViewPatchOrder.ReadTags(ReorderedChanges, False, True, True, False)

        If ReorderedChanges.Count = 0 Then
            MsgBox("No patches in ordered list.")
        Else

            For Each lpatch In ReorderedChanges
                PatchListBox.Items.Add(lpatch)
            Next
        End If


    End Sub

    Private Sub exportPatchList()

  
        Dim ReorderedChanges As Collection = New Collection
        TreeViewPatchOrder.ReadTags(ReorderedChanges, False, True, True, False)
        Dim lpatchpath As String = Nothing
        Dim lpatchname As String = Nothing
 
        If ReorderedChanges.Count = 0 Then
            MsgBox("No patches in ordered list.")
        Else

            For Each lpatch In ReorderedChanges

                Logger.Note("lpatch", lpatch)
                lpatchpath = Common.dropLastSegment(lpatch, "\") & "\"
                Logger.Note("lpatchpath", lpatchpath)
                lpatchname = Common.getLastSegment(lpatch, "\")
                Logger.Note("lpatchname", lpatchname)

                PatchFromTags.doExportPatch(lpatchpath, lpatchname)

            Next
        End If


    End Sub







    Private Sub PatchRunnerTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchRunnerTabControl.SelectedIndexChanged


        If (PatchRunnerTabControl.SelectedTab.Name.ToString) = "OrderTabPage" Then
            'If TreeViewPatchOrder.Nodes.Count = 0 Then
            CopySelectedChanges()
            'End If
        ElseIf (PatchRunnerTabControl.SelectedTab.Name.ToString) = "RunTabPage" Then
            PopMasterScriptListBox()

        ElseIf (PatchRunnerTabControl.SelectedTab.Name.ToString) = "ExportTabPage" Then
            PopPatchListBox()

        End If

    End Sub




    Public Shared Function FindLastPatch(ByVal patch_component As String) As String
        'OLDER STYLE
        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor


        Dim conn As New OracleConnection(Globals.getARMconnection)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim l_patch_name As String = Nothing


        Try

            conn.Open()

            sql = "select arm_installer.get_last_patch('" & patch_component & "') patch_name from dual"

        cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            'cmd.CommandTimeout = 10 'It does timeout, but takes about 15secs regardless of whether this line is here or not.
            dr.Read()


            If Not IsDBNull(dr.Item("patch_name")) Then
                l_patch_name = dr.Item("patch_name")
            End If

            dr.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
            Return "TIMEOUT" 'propagate as error code.
        Finally
            ' In a real application, put cleanup code here.

        End Try

        Cursor.Current = cursorRevert

        Return l_patch_name


    End Function


    Private Sub CopySelectedChanges()

        'Copy to the new Patchable Tree
        TreeViewPatchOrder.PathSeparator = "#"
        TreeViewPatchOrder.Nodes.Clear()


        'Prepopulate Tree with default category nodes.
        Dim l_patches_category As String = "Patches"
        TreeViewPatchOrder.AddCategory(l_patches_category)

        Dim ChosenChanges As Collection = New Collection
        'Repo changes
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        AvailablePatchesTreeView.ReadCheckedLeafNodes(ChosenChanges)

        If ChosenChanges.Count = 0 Then
            'Show Run tab hidden
            PatchRunnerTabControl.TabPages.Remove(RunTabPage)

            MsgBox("No patches selected.")

        Else
            Dim ReorderedChanges As Collection = New Collection


            If ComboBoxPatchesFilter.SelectedItem = "Unapplied" Then

                ReorderedChanges = PatchRunner.ReorderByDependancy(ChosenChanges, AvailablePatches)
            Else
                ReorderedChanges = ChosenChanges
                If ChosenChanges.Count > 1 Then
                    MsgBox("WARNING: Unordered patches.  Dependancy order can only be determined when using the 'Unapplied' filter. " & Chr(10) &
                           "Please drag and drop to re-order the patches.")
                End If
            End If



            Dim l_label As String
            For Each change In ReorderedChanges
                Dim pathSeparator As String = "\"

                l_label = Common.getLastSegment(change, pathSeparator)
                TreeViewPatchOrder.AddFileToCategory(l_patches_category, l_label, change)
            Next


            TreeViewPatchOrder.ExpandAll()
            'Show Run tab 
            PatchRunnerTabControl.TabPages.Remove(RunTabPage)
            PatchRunnerTabControl.TabPages.Insert(2, RunTabPage)

        End If





    End Sub


    Private Sub CopyChangesButton_Click(sender As Object, e As EventArgs) Handles CopyChangesButton.Click
        CopySelectedChanges()
    End Sub

  
    Private Sub ExportPatchesButton_Click(sender As Object, e As EventArgs) Handles ExportPatchesButton.Click
        exportPatchList()
    End Sub

    Private Sub ComboBoxPatchesFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPatchesFilter.SelectedIndexChanged
        'Hide Order and Run tabs
        'PatchRunnerTabControl.TabPages.Remove(OrderTabPage)
        PatchRunnerTabControl.TabPages.Remove(RunTabPage)
    End Sub
End Class