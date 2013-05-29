Public Class Config

    Public Sub New()
        InitializeComponent()

        'Repos
        RepoListTextBox.DataBindings.Add("Text", My.Settings, "RepoList")

        'Databases
        DBListTextBox.DataBindings.Add("Text", My.Settings, "DBList")
        ConnectionTextBox.DataBindings.Add("Text", My.Settings, "ConnectionList")

        'Paths
        PatchOffsetTextBox.DataBindings.Add("Text", My.Settings, "PatchDirOffset")
        ApexOffsetTextBox.DataBindings.Add("Text", My.Settings, "ApexDirOffset")
        OJDBCjarFileTextBox.DataBindings.Add("Text", My.Settings, "JDBCjar")
        SQLpathTextBox.DataBindings.Add("Text", My.Settings, "SQLpath")
        GitExeTextBox.DataBindings.Add("Text", My.Settings, "GITpath")

        'Apps
        ApplicationsTextBox.DataBindings.Add("Text", My.Settings, "ApplicationsList")
        PatchSchemasTextBox.DataBindings.Add("Text", My.Settings, "PatchSchemaList")
        PatchSetTextBox.DataBindings.Add("Text", My.Settings, "PatchSetList")
        AppListTextBox.DataBindings.Add("Text", My.Settings, "AppList")
        ParsingSchemaTextbox.DataBindings.Add("Text", My.Settings, "ParsingSchemaList")


        'Mail
        SMTPhostTextBox.DataBindings.Add("Text", My.Settings, "SMTPhost")
        SMTPportTextBox.DataBindings.Add("Text", My.Settings, "SMTPport")
        RecipientDomainTextBox.DataBindings.Add("Text", My.Settings, "RecipientDomain")
        RecipientTextBox.DataBindings.Add("Text", My.Settings, "RecipientList")



    End Sub


    Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs) Handles RepoListTextBox.TextChanged
        Main.loadRepos()
    End Sub

    Private Sub TestMailButton_Click(sender As Object, e As EventArgs) Handles TestMailButton.Click
        Mail.SendNotification("Test Email", "Just testing my config for email from GitPatcher")
    End Sub
End Class