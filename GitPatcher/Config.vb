Public Class Config

    Public Sub New()
        InitializeComponent()
        RepoListTextBox.DataBindings.Add("Text", My.Settings, "RepoList")
        PatchOffsetTextBox.DataBindings.Add("Text", My.Settings, "PatchDirOffset")
        ApexOffsetTextBox.DataBindings.Add("Text", My.Settings, "ApexDirOffset")
        SQLpathTextBox.DataBindings.Add("Text", My.Settings, "SQLpath")
        DBListTextBox.DataBindings.Add("Text", My.Settings, "DBList")


    End Sub

 
    Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs) Handles RepoListTextBox.TextChanged
        Main.loadRepos()
    End Sub
End Class