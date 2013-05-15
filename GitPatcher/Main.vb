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

        BranchPathTextBox.Text = GitSharpFascade.currentBranch(RepoComboBox.SelectedItem)

        CurrentBranchTextBox.Text = PatchFromTags.get_last_split(BranchPathTextBox.Text, "/")
 
        'Dim l_CurrentBranch As String = GitSharpFascade.currentBranch(RepoComboBox.SelectedItem)
        'Dim l_group As String = Nothing
        'CurrentBranchTextBox.Text = Nothing
        'For Each folder In l_CurrentBranch.Split("/")
        '    If String.IsNullOrEmpty(l_group) Then
        '        l_group = CurrentBranchTextBox.Text
        '    Else
        '        l_group = l_group & "/" & CurrentBranchTextBox.Text
        '    End If
        '
        '    CurrentBranchTextBox.Text = folder
        'Next

        'If String.IsNullOrEmpty(l_group) Then
        '    BranchPathTextBox.Text = Nothing
        'Else
        '    BranchPathTextBox.Text = l_group & "/"
        'End If
 
        'CurrentBranchTextBox.Text = GitSharpFascade.currentBranch(RepoComboBox.SelectedItem)
        RootPatchDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.PatchDirOffset & "\"
        RootApexDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.ApexDirOffset & "\"

        My.Settings.CurrentRepo = RepoComboBox.SelectedItem

        My.Settings.Save()
    End Sub

    Private Sub PatchFromTagsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PatchFromTagsToolStripMenuItem.Click
        PatchFromTags.createPatchProcess()
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

 
    Private Sub MergeAndPushFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushFeatureToolStripMenuItem.Click
 
        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim mergeAndPush As ProgressDialogue = New ProgressDialogue("Merge and Push branch:  " & currentBranch)
        mergeAndPush.MdiParent = GitPatcher
        mergeAndPush.addStep("Switch to Master branch", 20)
        mergeAndPush.addStep("Pull from Origin", 40)
        mergeAndPush.addStep("Merge from branch: " & currentBranch, 60)
        mergeAndPush.addStep("Push to Origin", 80)
        mergeAndPush.addStep("Return to branch: " & currentBranch, 100)

        mergeAndPush.Show()

        mergeAndPush.setStep(0)
 

        'switch
        'GitSharpFascade.switchBranch(My.Settings.CurrentRepo, "master")
        Tortoise.Switch(My.Settings.CurrentRepo)

        mergeAndPush.setStep(1)
        'Pull from Origin 
        Tortoise.Pull(My.Settings.CurrentRepo)

        mergeAndPush.setStep(2)

        'Merge from Feature branch
        'TortoiseMerge(My.Settings.CurrentRepo, currentBranch)
        Tortoise.Merge(My.Settings.CurrentRepo)

        mergeAndPush.setStep(3)

        'Push to Origin 
        Tortoise.Push(My.Settings.CurrentRepo)

        mergeAndPush.setStep(4)

        'GitSharpFascade.switchBranch(My.Settings.CurrentRepo, currentBranch)
        Tortoise.Switch(My.Settings.CurrentRepo)

        'Done
        mergeAndPush.done()

    End Sub

    Private Sub NewFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFeatureToolStripMenuItem.Click
        If MsgBox("Would you like to create a new Feature Branch with the standardised naming feature/JIRA?", MsgBoxStyle.OkCancel, "Create a new Feature Branch") = MsgBoxResult.Ok Then

            Dim featureName As String = InputBox("Enter the Jira Id.", "Jira Id for new Feature Branch")

            If Not String.IsNullOrEmpty(featureName) Then
 
                Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new Feature branch:  " & featureName)
                newFeature.MdiParent = GitPatcher
                newFeature.addStep("Switch to Master branch", 33)
                newFeature.addStep("Pull from Origin", 66)
                newFeature.addStep("Create and switch to Feature branch: " & featureName, 100)
  
                newFeature.Show()

                newFeature.setStep(0)

                'switch to master - manual
                Tortoise.Switch(My.Settings.CurrentRepo)

                newFeature.setStep(1)
                'Pull from Origin 
                Tortoise.Pull(My.Settings.CurrentRepo)

                newFeature.setStep(2)

                'Create Feature branch
                GitSharpFascade.createBranch(My.Settings.CurrentRepo, "feature/" & featureName)
 
                'Done
                newFeature.done()

 

            End If

        End If
    End Sub
End Class
