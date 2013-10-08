Imports Oracle.DataAccess.Client ' VB.NET

Public Class PatchRunner

    Dim hotFixTargetDBFilter As String = Nothing

    Public Sub New(ByVal iUnapplied As Boolean, ByVal iUninstalled As Boolean, ByVal iAll As Boolean, Optional ByVal iBranchType As String = "")
        InitializeComponent()
        RadioButtonUnapplied.Checked = iUnapplied
        RadioButtonUninstalled.Checked = iUninstalled
        RadioButtonAll.Checked = iAll
        PatchFilterGroupBox.Text = Globals.currentTNS & " Search Criteria"


        hotFixTargetDBFilter = Globals.currentDB()
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



    Public Shared Sub FindPatches(ByRef foundPatches As ListBox, ByVal iHideInstalled As Boolean)

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


        foundPatches.Items.Clear()
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
                For i As Integer = foundPatches.Items.Count - 1 To 0 Step -1

                    'Check whether the patch has been successfully installed.
                    patchMatch = False
                    Dim lPatchName As String = Common.getLastSegment(foundPatches.Items(i), "\")
    
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
                        foundPatches.Items.RemoveAt(i)

                    End If


                Next


                conn.Close()
                conn.Dispose()


            Catch ex As Exception ' catches any error
                MessageBox.Show(ex.Message.ToString())
            Finally
                ' In a real application, put cleanup code here.

            End Try

        End If

        If foundPatches.Items.Count = 0 Then
            MsgBox("No patches matched the search criteria.", MsgBoxStyle.Information, "No patches found")
        End If


    End Sub


    Public Shared Sub FindUnappliedPatches(ByRef foundPatches As ListBox)


        foundPatches.Items.Clear()
        Dim availableList As ListBox = New ListBox
        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", availableList, Globals.RootPatchDir)

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

                For i As Integer = 0 To availableList.Items.Count - 1

                    If availableList.Items(i).ToString().Contains(l_patch_name) Then
                        foundPatches.Items.Add(availableList.Items(i))
                        l_patch_found = True
                    End If

                Next

                If Not l_patch_found Then
                    MsgBox("WARNING: Unapplied patch " & l_patch_name & " is not present in the local checkout.")
                End If

            End While

            conn.Close()
            conn.Dispose()


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try

        If foundPatches.Items.Count = 0 Then
            MsgBox("No patches matched the search criteria.", MsgBoxStyle.Information, "No patches found")
        End If


    End Sub


    Private Sub filterPatchType(ByRef foundPatches As ListBox)

 
        Dim searchTerm As String = "all"
        If Not RadioButtonAll2.Checked And foundPatches.Items.Count > 0 Then


            If RadioButtonFeature.Checked Then
                searchTerm = "feature"
            ElseIf RadioButtonHotfix.Checked Then
                searchTerm = "hotfix"
            ElseIf RadioButtonPatchSet.Checked Then
                searchTerm = "patchset"
            End If

            For i As Integer = foundPatches.Items.Count - 1 To 0 Step -1
                If Not foundPatches.Items(i).contains(searchTerm) Then
                    'This patch does not match the filter and will be removed from the list
                    foundPatches.Items.RemoveAt(i)

                ElseIf foundPatches.Items(i).contains("hotfix") And Not foundPatches.Items(i).contains("_" & hotFixTargetDBFilter) Then
                    'Filter out hotfixes that do not match the current database
                    foundPatches.Items.RemoveAt(i)
                End If


            Next
 
            If foundPatches.Items.Count = 0 Then
                MsgBox("No patches matched the Filter: " & searchTerm, MsgBoxStyle.Information, "No patches found")
            End If


        End If
        Globals.setPatchRunnerFilter(searchTerm)


    End Sub

 


    'Sub populateTreeFromListbox(ByRef patchesTreeView As TreeView, ByRef patchesListBox As ListBox)
    '
    '    patchesTreeView.PathSeparator = "\"
    '    patchesTreeView.Nodes.Clear()
    '
    '    'copy each item from listbox
    '    For i As Integer = 0 To AvailablePatchesListBox.Items.Count - 1
    '
    '        'find or create each node for item
    '
    '        Dim aItem As String = AvailablePatchesListBox.Items(i).ToString()
    '        GPTrees.AddNode(patchesTreeView.Nodes, aItem, aItem)
    '
    '
    '    Next
    '
    'End Sub


    Private Sub doSearch()
        Logger.Dbg("Searching")

        If RadioButtonUnapplied.Checked Then
            FindUnappliedPatches(AvailablePatchesListBox)
        ElseIf RadioButtonUninstalled.Checked Or RadioButtonAll.Checked Then
            FindPatches(AvailablePatchesListBox, RadioButtonUninstalled.Checked)
        Else
            MsgBox("Choose type of patch to search for.", MsgBoxStyle.Exclamation, "Choose Search criteria")
        End If

        Logger.Dbg("Filtering")
        filterPatchType(AvailablePatchesListBox)

        Logger.Dbg("Populate Tree")
        GPTrees.populateTreeFromListbox(AvailablePatchesTreeView, AvailablePatchesListBox)
        'populateTreeFromListbox(AvailablePatchesTreeView.TopNode, AvailablePatchesListBox)


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
  
        Dim chosenPatches As Collection = New Collection
 
        'Retrieve checked node items from the available patches as a collection of patches.
        GPTrees.ReadCheckedNodes(AvailablePatchesTreeView.TopNode, chosenPatches, True)
 
        For Each lpatch In chosenPatches
            MasterScriptListBox.Items.Add("@" & lpatch & "\install.sql")
        Next

 

    End Sub


    Private Sub PatchRunnerTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchRunnerTabControl.SelectedIndexChanged


        If (PatchRunnerTabControl.SelectedTab.Name.ToString) = "RunTabPage" Then
            PopMasterScriptListBox()

        End If

    End Sub
 

    ' NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event. 
    ' After a tree node's Checked property is changed, all its child nodes are updated to the same value. 
    Shared Sub node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles AvailablePatchesTreeView.AfterCheck
 
        GPTrees.CheckChildNodes(e.Node, e.Node.Checked)

    End Sub

 

    Private Sub ButtonTreeChange_Click(sender As Object, e As EventArgs) Handles ButtonTreeChange.Click
        'Impliments a 3 position button Expand, Contract, Collapse.
        GPTrees.treeChange_Click(sender, e, AvailablePatchesTreeView)

    End Sub

 

 
End Class