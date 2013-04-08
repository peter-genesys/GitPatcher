Public Class Main

    Public Sub New()
        InitializeComponent()
        'RepoComboBox.DataBindings.Add("Text", My.Settings, "Repos")

        For Each repo In My.Settings.Repos
            repo = Trim(repo)
            If (repo.Length > 0) Then
                RepoComboBox.Items.Add(repo)
            End If
            If My.Settings.CurrentRepo = repo Then
                RepoComboBox.SelectedIndex = RepoComboBox.Items.Count - 1
            End If
        Next
        ' If RepoComboBox.Items.Count > 0 And RepoComboBox.SelectedIndex Then
        '     RepoComboBox.SelectedIndex = 0
        ' End If


    End Sub


    Private Sub RepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RepoComboBox.SelectedIndexChanged


        CurrentBranchTextBox.Text = GitSharpFascade.currentBranch(RepoComboBox.SelectedItem)
        RootPatchDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.PatchDirOffset & "\"

        My.Settings.CurrentRepo = RepoComboBox.SelectedItem

        My.Settings.Save()
    End Sub

    Private Sub PatchFromTagsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PatchFromTagsToolStripMenuItem.Click
        Dim newchildform As New PatchFromTags
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub
End Class
