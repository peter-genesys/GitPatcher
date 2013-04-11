Public Class PatchRunner

    Shared Function get_last_split(ByVal ipath As String, ByVal idelim As String) As String
        Dim Path() As String = ipath.Split(idelim)
        Dim SplitCount = Path.Length
        Dim l_last As String = ipath.Split(idelim)(SplitCount - 1)

        Return l_last
    End Function

    Private Sub FindPatches()
        AvailablePatchesListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            For Each foldername As String In IO.Directory.GetDirectories(Main.RootPatchDirTextBox.Text)
                AvailablePatchesListBox.Items.Add(get_last_split(foldername, "\"))
            Next

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

    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click

        'Format as script
        Dim masterList As String = Nothing
 
        For i As Integer = 0 To MasterScriptListBox.Items.Count - 1

            masterList = masterList & Chr(10) & MasterScriptListBox.Items(i).ToString()

        Next
 
        Dim masterScriptName As String = Main.RootPatchDirTextBox.Text & "temp_master_script.sql"
 
        FileIO.writeFile(masterScriptName, masterList, True)
 
        Host.executeSQLscriptInteractive(masterScriptName, Main.RootPatchDirTextBox.Text)

        FileIO.deleteFileIfExists(masterScriptName)
 
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