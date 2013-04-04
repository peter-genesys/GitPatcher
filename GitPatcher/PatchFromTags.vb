Public Class PatchFromTags

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        'GitSharpFascade.getTagDiff(Main.RepoComboBox.SelectedItem, Tag1TextBox.Text, Tag2TextBox.Text)
        'GitSharpFascade.getTagDiff(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text)
        ChangesCheckedListBox.Items.Clear()
        For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database")
            ChangesCheckedListBox.Items.Add(change)
            ChangesCheckedListBox.SetItemChecked(ChangesCheckedListBox.Items.Count - 1, True)
        Next

    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click


        For i As Integer = 0 To ChangesCheckedListBox.CheckedItems.Count - 1
            Console.WriteLine(ChangesCheckedListBox.CheckedItems.Item(i).ToString())
        Next
 
    End Sub
End Class