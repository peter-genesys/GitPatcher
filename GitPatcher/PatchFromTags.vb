Public Class PatchFromTags

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'GitSharpFascade.getTagDiff(Main.RepoComboBox.SelectedItem, Tag1TextBox.Text, Tag2TextBox.Text)
        GitSharpFascade.getTagDiff(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text)

    End Sub
End Class