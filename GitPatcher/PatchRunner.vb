Public Class PatchRunner

    Shared Function get_last_split(ByVal ipath As String, ByVal idelim As String) As String
        Dim Path() As String = ipath.Split(idelim)
        Dim SplitCount = Path.Length
        Dim l_last As String = ipath.Split(idelim)(SplitCount - 1)

        Return l_last
    End Function

    Shared Function get_first_split(ByVal ipath As String, ByVal idelim As String) As String

        Dim l_first As String = ipath.Split(idelim)(0)

        Return l_first
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

    Public Sub RecursiveSearchContainingFolder(ByVal strPath As String, ByVal strPattern As String, ByRef lstTarget As ListBox, ByVal removePath As String)

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)
        Dim clsFile As FileInfoEx = Nothing

        'Add the files
        For Each strFile As String In strFiles
            clsFile = New FileInfoEx(strFile)
            lstTarget.Items.Add(strPath.Substring(removePath.Length))
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearchContainingFolder(strFolder, strPattern, lstTarget, removePath)
        Next

    End Sub


    Private Sub FindPatches()
        AvailablePatchesListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text, "install.sql", AvailablePatchesListBox, Main.RootPatchDirTextBox.Text)
 
            'For Each foldername As String In IO.Directory.GetDirectories(Main.RootPatchDirTextBox.Text)
            '    AvailablePatchesListBox.Items.Add(get_last_split(foldername, "\"))
            'Next

        End If
    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles SearchPatchesButton.Click
        FindPatches()
    End Sub

    Private Sub AvailablePatchesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AvailablePatchesListBox.DoubleClick
        If Not ChosenPatchesListBox.Items.Contains(AvailablePatchesListBox.SelectedItem) Then
            ChosenPatchesListBox.Items.Add(AvailablePatchesListBox.SelectedItem)
        End If

    End Sub

    Private Sub ChosenPatchesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChosenPatchesListBox.DoubleClick

        If ChosenPatchesListBox.Items.Count > 0 Then
            ChosenPatchesListBox.Items.RemoveAt(ChosenPatchesListBox.SelectedIndex)
        End If

    End Sub

    Public Shared Sub RunMasterScript(scriptData As String)

        Dim masterScriptName As String = Main.RootPatchDirTextBox.Text & "temp_master_script.sql"

        FileIO.writeFile(masterScriptName, scriptData, True)

        Host.executeSQLscriptInteractive(masterScriptName, Main.RootPatchDirTextBox.Text)

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

        MasterScriptListBox.Items.Add("DEFINE database = '" & My.Settings.CurrentDB & "'")

        For i As Integer = 0 To ChosenPatchesListBox.Items.Count - 1

            MasterScriptListBox.Items.Add("@" & ChosenPatchesListBox.Items(i).ToString() & "\install.sql")

        Next


    End Sub


    Private Sub PatchRunnerTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchRunnerTabControl.SelectedIndexChanged


        If (PatchRunnerTabControl.SelectedTab.Name.ToString) = "RunTabPage" Then
            PopMasterScriptListBox()

        End If

    End Sub

End Class