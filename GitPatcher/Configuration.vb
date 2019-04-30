Public Class Configuration

    Public Sub New()
        InitializeComponent()

 
        'Paths
        'PatchOffsetTextBox.DataBindings.Add("Text", My.Settings, "PatchDirOffset")
        'ApexOffsetTextBox.DataBindings.Add("Text", My.Settings, "ApexDirOffset")
        'DBOffsetTextBox.DataBindings.Add("Text", My.Settings, "DBDirOffset")



        'Extras
        'ExtrasDirListTextBox.DataBindings.Add("Text", My.Settings, "ExtrasDirList")


        'OJDBCjarFileTextBox.DataBindings.Add("Text", My.Settings, "JDBCjar")
        SQLpathTextBox.DataBindings.Add("Text", My.Settings, "SQLpath")
        GitExeTextBox.DataBindings.Add("Text", My.Settings, "GITpath")
        XMLRepoFilePathTextBox.DataBindings.Add("Text", My.Settings, "XMLRepoFilePath")
        RunConfigDirTextBox.DataBindings.Add("Text", My.Settings, "RunConfigDir")
        GPScriptsDirTextBox.DataBindings.Add("Text", My.Settings, "GPScriptsDir")


        'PatchExportPathTextBox.DataBindings.Add("Text", My.Settings, "PatchExportPath")


        'Apps
        'ApplicationsTextBox.DataBindings.Add("Text", My.Settings, "ApplicationsList")   'Descriptions for Applications Eg Prism
        'AppCodeTextBox.DataBindings.Add("Text", My.Settings, "AppCodeList")             'Codes for Applications        Eg prism
        'AppListTextBox.DataBindings.Add("Text", My.Settings, "AppList")                 'Apex ids
        'JiraProjectTextBox.DataBindings.Add("Text", My.Settings, "JiraProject") 'Default Jira Project of Apex Application
        'ParsingSchemaTextbox.DataBindings.Add("Text", My.Settings, "ParsingSchemaList") 'Parsing schema of Apex Application


        'Mail
        SMTPhostTextBox.DataBindings.Add("Text", My.Settings, "SMTPhost")
        SMTPportTextBox.DataBindings.Add("Text", My.Settings, "SMTPport")
        RecipientDomainTextBox.DataBindings.Add("Text", My.Settings, "RecipientDomain")
        RecipientTextBox.DataBindings.Add("Text", My.Settings, "RecipientList")



    End Sub


    'Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Main.loadRepos()
    'End Sub

    Private Sub TestMailButton_Click(sender As Object, e As EventArgs) Handles TestMailButton.Click
        Mail.SendNotification("Test Email", "Just testing my config for email from GitPatcher")
    End Sub

    'Private Sub DBOffsetTextBox_TextChanged(sender As Object, e As EventArgs)
    '    DBOffsetTextBox.Text = Replace(DBOffsetTextBox.Text, "\", "/")
    'End Sub

    Private Sub XMLButton_Click(sender As Object, e As EventArgs) Handles XMLButton.Click

        If Not FileIO.fileExists(Globals.XMLRepoFilePath) Then
            FileIO.writeFile(Globals.XMLRepoFilePath,"<?xml version=""1.0"" encoding=""utf-8""?><repos></repos>" )
        End If
 
        'MsgBox(RepoComboBox.SelectedItem)
        Dim theRepoSettings As RepoSettings = New RepoSettings()
        'theDatabaseSettings.MdiParent = Me
        theRepoSettings.Show()
    End Sub
End Class