Imports Oracle.DataAccess.Client ' VB.NET


Public Class PatchRunner

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

        doSearch()


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

        'Simple but replies on TNSNAMES File
        Dim oradb As String = "Data Source=" & Globals.currentTNS & ";User Id=patch_admin;Password=patch_admin;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim patchMatch As Boolean = False

        'This time loop through unapplied patches first and show in list if available in dir.
        Try

            conn.Open()

            sql = "select patch_name from patches_unapplied_v"


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

                    If givenPatches(i).ToString().Contains(l_patch_name) Then
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

        'Simple but replies on TNSNAMES File
        Dim oradb As String = "Data Source=" & Globals.currentTNS & ";User Id=patch_admin;Password=patch_admin;"

        'Harder to get working but no need for TNSNAMES File
        'Dim oradb As String = "Data Source=(DESCRIPTION=" _
        '   + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" & Common.getHost & ")(PORT=" & Common.getPort & ")))" _
        '   + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" & Common.getSid & ")));" _
        '   + "User Id=patch_admin;Password=patch_admin;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader


        foundPatches.Clear()
        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", foundPatches, Globals.RootPatchDir)

        End If

        If iHideInstalled Then

            'http://www.oracle.com/technetwork/articles/dotnet/cook-dotnet-101788.html

            'Now remove patches that have already been applied to the database.

            Dim patchMatch As Boolean = False


            Try

                conn.Open()

                'process in reverse order, because removing items from the list, changes the indexes.  reverse order will not be affected.
                For i As Integer = foundPatches.Count To 1 Step -1

                    'Check whether the patch has been successfully installed.
                    patchMatch = False
                    Dim lPatchName As String = Common.getLastSegment(foundPatches(i), "\")

                    sql = "select max(patch_name) patch_name from patches where patch_name = '" & lPatchName & "' and success_yn = 'Y' "

                    'sql = "select max(patch_name) patch_name from ( " _
                    '    & "select patch_name from patches " _
                    '    & "where patch_name = '" & lPatchName & "' " _
                    '    & "and success_yn = 'Y' " _
                    '    & "UNION " _
                    '    & "select ps.supersedes_patch " _
                    '    & "from patch_supersedes ps, " _
                    '    & "patches p " _
                    '    & "where p.patch_name = ps.patch_name " _
                    '    & "and p.success_yn = 'Y' " _
                    '    & "and  ps.supersedes_patch = '" & lPatchName & "') "



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

        'Simple but replies on TNSNAMES File
        Dim oradb As String = "Data Source=" & Globals.currentTNS & ";User Id=patch_admin;Password=patch_admin;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim patchMatch As Boolean = False

        'This time loop through unapplied patches first and show in list if available in dir.
        Try

            conn.Open()

            sql = "select patch_name from patches_unapplied_v"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                Dim l_patch_name As String = dr.Item("patch_name")
                Dim l_patch_found As Boolean = False

                For i As Integer = 1 To availablePatches.Count

                    If availablePatches(i).ToString().Contains(l_patch_name) Then
                        foundPatches.Add(availablePatches(i))
                        l_patch_found = True
                    End If

                Next

                If Not l_patch_found Then
                    MsgBox("WARNING: Unapplied patch " & l_patch_name & " is not present in the local checkout.")
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

        If foundPatches.Count = 0 Then
            MsgBox("No patches matched the search criteria.", MsgBoxStyle.Information, "No patches found")
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
        ElseIf ComboBoxPatchesFilter.SelectedItem = "Uninstalled" Or ComboBoxPatchesFilter.SelectedItem = "All" Then
            FindPatches(AvailablePatches, ComboBoxPatchesFilter.SelectedItem = "Uninstalled")
        Else
            MsgBox("Choose type of patch to search for.", MsgBoxStyle.Exclamation, "Choose Search criteria")
        End If

        Logger.Dbg("Filtering")
        filterPatchType(AvailablePatches)

        Logger.Dbg("Populate Tree")
 
        AvailablePatchesTreeView.populateTreeFromCollection(AvailablePatches)
  

    End Sub




    Private Sub SearchPatchesButton_Click(sender As Object, e As EventArgs) Handles SearchPatchesButton.Click
        doSearch()

    End Sub




    Public Shared Sub RunMasterScript(scriptData As String)

        Dim masterScriptName As String = Globals.RootPatchDir & "temp_master_script.sql"

        FileIO.writeFile(masterScriptName, scriptData, True)

        Host.executeSQLscriptInteractive(masterScriptName, Globals.RootPatchDir)

        FileIO.deleteFileIfExists(masterScriptName)

    End Sub


    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click

        'Format as script
        Dim masterList As String = Nothing

        For i As Integer = 0 To MasterScriptListBox.Items.Count - 1

            masterList = masterList & Chr(10) & MasterScriptListBox.Items(i).ToString()

        Next

        RunMasterScript(masterList)


    End Sub


    Private Sub PopMasterScriptListBox()

        MasterScriptListBox.Items.Clear()

        MasterScriptListBox.Items.Add("DEFINE database = '" & Globals.currentTNS & "'")

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
    '       Dim oradb As String = "Data Source=" & Globals.currentTNS & ";User Id=patch_admin;Password=patch_admin;"
    '
    '       Dim conn As New OracleConnection(oradb)
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
    '           cmd.CommandText = "patch_installer.get_last_patch"
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

        'Simple but replies on TNSNAMES File
        Dim oradb As String = "Data Source=" & Globals.currentTNS & ";User Id=patch_admin;Password=patch_admin;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim l_patch_name As String = Nothing


        Try

            conn.Open()

            sql = "select patch_installer.get_last_patch('" & patch_component & "') patch_name from dual"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            dr.Read()


            If Not IsDBNull(dr.Item("patch_name")) Then
                l_patch_name = dr.Item("patch_name")
            End If

            dr.Close()
            conn.Close()
            conn.Dispose()



        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
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