Public Class Main

    Public Sub New()
        InitializeComponent()
        loadRepos()
        loadDBs()
        loadApexApps()
    End Sub

    Public Sub loadRepos()
        RepoComboBox.Items.Clear()
        For Each repo In My.Settings.RepoList.Split(Chr(10))
            repo = Trim(repo)
            repo = repo.Replace(Chr(13), "")
            If (repo.Length > 0) Then
                RepoComboBox.Items.Add(repo)
            End If
            If My.Settings.CurrentRepo = repo Then
                RepoComboBox.SelectedIndex = RepoComboBox.Items.Count - 1
            End If
        Next
    End Sub


    Public Sub loadDBs()
        DBListComboBox.Items.Clear()
        For Each DB In My.Settings.DBList.Split(Chr(10))
            DB = Trim(DB)
            DB = DB.Replace(Chr(13), "")
            If (DB.Length > 0) Then
                DBListComboBox.Items.Add(DB)
            End If
            If My.Settings.CurrentDB = DB Then
                DBListComboBox.SelectedIndex = DBListComboBox.Items.Count - 1
            End If
        Next
    End Sub


    Public Sub loadApexApps()
        'ApexListComboBox.Items.Clear()
        'Original method was to look up dirs
        'If IO.Directory.Exists(RootApexDirTextBox.Text) Then
        '
        '    For Each foldername As String In IO.Directory.GetDirectories(RootApexDirTextBox.Text)
        '        Dim apexApp As String = PatchRunner.get_last_split(foldername, "\")
        '        ApexListComboBox.Items.Add(apexApp)
        '
        '        If My.Settings.CurrentApex = apexApp Then
        '            ApexListComboBox.SelectedIndex = ApexListComboBox.Items.Count - 1
        '        End If
        '    Next
        '
        'End If

        'Now stored as a settings list instead.
        ApexListComboBox.Items.Clear()
        For Each apexApp In My.Settings.AppList.Split(Chr(10))
            apexApp = Trim(apexApp)
            apexApp = apexApp.Replace(Chr(13), "")
            If (apexApp.Length > 0) Then
                ApexListComboBox.Items.Add(apexApp)
            End If
            If My.Settings.CurrentApex = apexApp Then
                ApexListComboBox.SelectedIndex = ApexListComboBox.Items.Count - 1
            End If
        Next
 

    End Sub


    Private Sub RepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RepoComboBox.SelectedIndexChanged

        CurrentBranchTextBox.Text = GitSharpFascade.currentBranch(RepoComboBox.SelectedItem)
        RootPatchDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.PatchDirOffset & "\"
        RootApexDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.ApexDirOffset & "\"

        My.Settings.CurrentRepo = RepoComboBox.SelectedItem

        My.Settings.Save()
    End Sub

    Private Sub PatchFromTagsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PatchFromTagsToolStripMenuItem.Click
        Dim newchildform As New PatchFromTags
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub PatchRunnerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PatchRunnerToolStripMenuItem.Click
        Dim newchildform As New PatchRunner
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub DBListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DBListComboBox.SelectedIndexChanged
        My.Settings.CurrentDB = DBListComboBox.SelectedItem
        My.Settings.Save()

        CurrentConnectionTextBox.Text = My.Settings.ConnectionList.Split(Chr(10))(DBListComboBox.SelectedIndex)

    End Sub

    Private Sub ApexListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ApexListComboBox.SelectedIndexChanged
        My.Settings.CurrentApex = ApexListComboBox.SelectedItem
        My.Settings.Save()

        ParsingSchemaTextBox.Text = My.Settings.ParsingSchemaList.Split(Chr(10))(ApexListComboBox.SelectedIndex)

    End Sub

    Shared Function connect_string(ByVal schema As String, ByVal password As String, ByVal database As String) As String

        Return schema & "/" & password & "@" & database

    End Function

    Shared Function get_password(ByVal schema As String, ByVal database As String) As String
        Dim password As String = InputBox("Schema: " & schema & Chr(10) & "Database: " & database & Chr(10) & Chr(10) & "Enter password", "Password")
        Return password
    End Function


    Shared Function get_connect_string(ByVal schema As String, ByVal database As String) As String

        Return connect_string(schema, get_password(schema, database), database)

    End Function


    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click


        If MsgBox("Importing APEX application " & My.Settings.CurrentApex & " into parsing schema " & ParsingSchemaTextBox.Text & " in DB " & My.Settings.CurrentDB & _
                  Chr(10) & "This will overwrite the existing APEX application." & Chr(10) & _
                  Chr(10) & "Consider creating a VM snapshot as a restore point." & _
                  Chr(10) & "To save any existing changes, CANCEL this operation and perform an EXPORT.", MsgBoxStyle.OkCancel, "Import APEX application " & My.Settings.CurrentApex) = MsgBoxResult.Ok Then
 
            Host.executeSQLscriptInteractive("install.sql" _
                                           , RootApexDirTextBox.Text & My.Settings.CurrentApex _
                                           ,get_connect_string(ParsingSchemaTextBox.Text, My.Settings.CurrentDB) )
 
        End If

    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click

        If MsgBox("Exporting APEX application " & My.Settings.CurrentApex & " from parsing schema " & ParsingSchemaTextBox.Text & " in DB " & My.Settings.CurrentDB & _
                  Chr(10) & "This writes individual apex files to the GIT Repo checkout, and then prompt to add and commit the changes." & Chr(10) & _
                  Chr(10) & "Consider which branch you are exporting to." & _
                  Chr(10) & "To commit any existing changes, CANCEL this operation and perform a GIT COMMIT.", MsgBoxStyle.OkCancel, "Export APEX application " & My.Settings.CurrentApex) = MsgBoxResult.Ok Then

  

            Dim password = Main.get_password(ParsingSchemaTextBox.Text, My.Settings.CurrentDB)

            Apex.ApexExportCommit(CurrentConnectionTextBox.Text, ParsingSchemaTextBox.Text, password, My.Settings.CurrentApex, RootApexDirTextBox.Text)
            'Apex.progress_test(CurrentConnectionTextBox.Text, ParsingSchemaTextBox.Text, password, My.Settings.CurrentApex, RootApexDirTextBox.Text)
            'ApexExport.demo_progress_bar()
 
        End If
    End Sub
End Class
