Imports Oracle.ManagedDataAccess.Client ' VB.NET


Public Class PatchRunner

    Private waiting As Boolean

    Dim hotFixTargetDBFilter As String = Nothing

    Public Sub New(Optional ByVal iInstallStatus As String = "All", Optional ByVal iBranchType As String = "")
        InitializeComponent()

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

        doSearch()


        Me.MdiParent = GitPatcher
        Me.Show()
        Wait()

    End Sub

    Public Sub Wait()
        'This wait routine will halt the caller until the form is closed.
        waiting = True
        Do Until Not waiting
            Common.wait(1000)
        Loop
    End Sub

    Private Sub PatchRunner_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        waiting = False
    End Sub


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




    Public Shared Function ReorderByDependancy(ByRef givenPatches As Collection) As Collection

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim orderedPatches As Collection = New Collection

        Dim conn As New OracleConnection(Globals.getARMconnection)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim patchMatch As Boolean = False

        'This time loop through unapplied patches first and show in list if available in dir.
        Try

            conn.Open()

            sql = "select patch_name from ARM_UNAPPLIED_V"


            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                Dim l_patch_name As String = dr.Item("patch_name")

                'For Each givenPatch In givenPatches
                '    If givenPatch.Contains(l_patch_name) Then
                '        orderedPatches.Add(givenPatch)
                '    End If
                'Next

                For i As Integer = givenPatches.Count To 1 Step -1

                    If Common.getLastSegment(givenPatches(i), "\") = l_patch_name Then
                        orderedPatches.Add(givenPatches(i))
                        givenPatches.Remove(i)
                    End If

                Next

            End While
            dr.Close()

            conn.Close()
            conn.Dispose()

            If givenPatches.Count > 0 Then
                Dim l_unordered_patches As String = Nothing
                For Each givenPatch In givenPatches

                    orderedPatches.Add(givenPatch)
                    l_unordered_patches = l_unordered_patches & Chr(10) & givenPatch

                Next

                MsgBox("WARNING: Could not determine install order for these patches on " & Globals.getDB & " . Added to the end of the release." _
                       & Chr(10) & "They may not yet have been applied to the reference database, or perhaps already applied to the target database." _
                       & Chr(10) & l_unordered_patches)
            End If


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try

        Cursor.Current = cursorRevert

        Return orderedPatches

    End Function

 



    Public Shared Sub FindPatches(ByRef foundPatches As Collection, ByVal iHideInstalled As Boolean)

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

                    'Check whether the patch has been successfully installed.
                    patchMatch = False
                    Dim lPatchName As String = Common.getLastSegment(foundPatches(i), "\")

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

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundPatches.Clear()
        Dim availablePatches As Collection = New Collection
        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", availablePatches, Globals.RootPatchDir)

        End If

        Dim conn As New OracleConnection(Globals.getARMconnection)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim patchMatch As Boolean = False

        Dim MissingPatchList As Collection = New Collection

        'This time loop through unapplied patches first and show in list if available in dir.
        Try

            conn.Open()

            sql = "select patch_name from ARM_UNAPPLIED_V"
            'sql = "select 'test1' patch_name from dual union all select 'test2' patch_name from dual"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                Dim l_patch_name As String = dr.Item("patch_name")
                Dim l_patch_found As Boolean = False

                For i As Integer = 1 To availablePatches.Count

                    If Common.getLastSegment(availablePatches(i), "\") = l_patch_name Then
                        foundPatches.Add(availablePatches(i))
                        l_patch_found = True
                    End If

                Next

                If Not l_patch_found Then
                    'MsgBox("WARNING: Unapplied patch " & l_patch_name & " is not present in the local checkout.")
                    MissingPatchList.Add(l_patch_name)
                End If

            End While

            dr.Close()

            conn.Close()
            conn.Dispose()


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try

        If MissingPatchList.Count > 0 Then

            MsgBox("Repo: " & Globals.getRepoName & "     Branch: " & Globals.currentBranch & Chr(10) & Chr(10) _
                 & Common.CollectionToText(MissingPatchList), MsgBoxStyle.Information, "These Unapplied Patches are not present")

        End If

        If foundPatches.Count = 0 Then
            MsgBox("No patches were found, that matched the search criteria.", MsgBoxStyle.Information, "No Patches Found")
        End If


        Cursor.Current = cursorRevert

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



    Private Sub doSearch()
        Logger.Dbg("Searching")

        Dim AvailablePatches As Collection = New Collection

        If ComboBoxPatchesFilter.SelectedItem = "Unapplied" Then
            FindUnappliedPatches(AvailablePatches)
        ElseIf ComboBoxPatchesFilter.SelectedItem = "Uninstalled" Then
            FindPatches(AvailablePatches, ComboBoxPatchesFilter.SelectedItem = "Uninstalled")
        ElseIf ComboBoxPatchesFilter.SelectedItem = "All" Then
            FindPatches(AvailablePatches, False) 'Find patches without doing any db search.
        Else
            MsgBox("Choose type of patch to search for.", MsgBoxStyle.Exclamation, "Choose Search criteria")
        End If

        Logger.Dbg("Filtering")
        filterPatchType(AvailablePatches)

        Logger.Dbg("Populate Tree")

        'Populate the treeview, tick unapplied by default
        AvailablePatchesTreeView.populateTreeFromCollection(AvailablePatches, ComboBoxPatchesFilter.SelectedItem = "Unapplied")


    End Sub




    Private Sub SearchPatchesButton_Click(sender As Object, e As EventArgs) Handles SearchPatchesButton.Click
        doSearch()

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

    End Sub


    Private Sub PopMasterScriptListBox()

        MasterScriptListBox.Items.Clear()

        MasterScriptListBox.Items.Add("DEFINE database = '" & Globals.getDATASOURCE & "'")
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
            If TreeViewPatchOrder.Nodes.Count = 0 Then
                CopySelectedChanges()
            End If
        ElseIf (PatchRunnerTabControl.SelectedTab.Name.ToString) = "RunTabPage" Then
            PopMasterScriptListBox()

        ElseIf (PatchRunnerTabControl.SelectedTab.Name.ToString) = "ExportTabPage" Then
            PopPatchListBox()

        End If

    End Sub




    '
    '   Public Shared Function FindLastPatch(ByVal patch_component As String) As String
    '

    '
    '       'Simple but relies on TNSNAMES File
    '       Dim conn As New OracleConnection(Globals.getARMconnection)
    '       'Dim sql As String = Nothing
    '       Dim cmd As New OracleCommand
    '       'Dim dr As OracleDataReader
    '
    '       Dim patch_name As String = Nothing
    '
    '       'This time loop through unapplied patches first and show in list if available in dir.
    '       Try
    '
    '           conn.Open()
    '           cmd.CommandText = "arm_installer.get_last_patch"
    '           cmd.CommandType = CommandType.StoredProcedure
    '           cmd.Parameters.Add("i_patch_component", OracleDbType.Varchar2, 50)
    '           cmd.Parameters.Item("i_patch_component").Value = patch_component
    '           cmd.Parameters.Add("patch_name", OracleDbType.Varchar2, 100)
    '           cmd.Parameters.Item("patch_name").Direction = ParameterDirection.ReturnValue
    '           cmd.Connection = conn
    '
    '           Dim obj As Object = cmd.ExecuteScalar()
    '
    '
    '           patch_name = CType(cmd.Parameters.Item("patch_name").Value, String)
    '
    '           conn.Close()
    '           conn.Dispose()
    '
    '
    '       Catch ex As Exception ' catches any error
    '           MessageBox.Show(ex.Message.ToString())
    '       Finally
    '           ' In a real application, put cleanup code here.
    '
    '       End Try
    '
    '       Return patch_name
    '
    '
    'End Function



    Public Shared Function FindLastPatch(ByVal patch_component As String) As String

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
            MsgBox("No patches selected.")
        Else
            Dim ReorderedChanges As Collection = New Collection


            If ComboBoxPatchesFilter.SelectedItem = "Unapplied" Then

                ReorderedChanges = PatchRunner.ReorderByDependancy(ChosenChanges)
            Else
                ReorderedChanges = ChosenChanges
                MsgBox("WARNING: Unordered patches.  Dependancy order is only used when filter is 'Unapplied'")
            End If



            Dim l_label As String
            For Each change In ReorderedChanges
                Dim pathSeparator As String = "\"

                l_label = Common.getLastSegment(change, pathSeparator)
                TreeViewPatchOrder.AddFileToCategory(l_patches_category, l_label, change)
            Next


            TreeViewPatchOrder.ExpandAll()
        End If



    End Sub


    Private Sub CopyChangesButton_Click(sender As Object, e As EventArgs) Handles CopyChangesButton.Click
        CopySelectedChanges()
    End Sub

  
    Private Sub ExportPatchesButton_Click(sender As Object, e As EventArgs) Handles ExportPatchesButton.Click
        exportPatchList()
    End Sub
End Class