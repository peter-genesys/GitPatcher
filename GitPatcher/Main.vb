Public Class Main

    Public Sub New()
        InitializeComponent()
        loadRepos()
        loadDBs()
        loadApexApps()
        MinPatchTextBox.Text = My.Settings.MinPatch
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
 

        'Now stored as a settings list instead.
        ApplicationListComboBox.Items.Clear()
        For Each App In My.Settings.ApplicationsList.Split(Chr(10))
            App = Trim(App)
            App = App.Replace(Chr(13), "")
            If (App.Length > 0) Then
                ApplicationListComboBox.Items.Add(App)
            End If
            If My.Settings.CurrentApp = App Then
                ApplicationListComboBox.SelectedIndex = ApplicationListComboBox.Items.Count - 1
            End If
        Next


    End Sub


    Private Sub RepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RepoComboBox.SelectedIndexChanged

        BranchPathTextBox.Text = GitSharpFascade.currentBranch(RepoComboBox.SelectedItem)

        CurrentBranchTextBox.Text = Common.getLastSegment(BranchPathTextBox.Text, "/")
 

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

    Private Sub ApplicationListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ApplicationListComboBox.SelectedIndexChanged
        My.Settings.CurrentApp = ApplicationListComboBox.SelectedItem
        My.Settings.Save()

        'Patch Schemas
        PatchSchemasTextBox.Text = Trim(My.Settings.PatchSchemaList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)).Replace(Chr(13), "")


        'repo = Trim(repo)
        'repo = repo.Replace(Chr(13), "")

        'Patch Set
        AppCodeTextBox.Text = Trim(My.Settings.PatchSetList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)).Replace(Chr(13), "")


        ApexAppTextBox.Text = Trim(My.Settings.AppList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)).Replace(Chr(13), "")
 
        My.Settings.CurrentApex = ApexAppTextBox.Text
        ParsingSchemaTextBox.Text = My.Settings.ParsingSchemaList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)

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
 
            Apex.ApexImportFromTag(My.Settings.CurrentApex)

        End If

    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click

        If MsgBox("Exporting APEX application " & My.Settings.CurrentApex & " from parsing schema " & ParsingSchemaTextBox.Text & " in DB " & My.Settings.CurrentDB & _
                  Chr(10) & "This writes individual apex files to the GIT Repo checkout, and then prompt to add and commit the changes." & Chr(10) & _
                  Chr(10) & "Consider which branch you are exporting to." & _
                  Chr(10) & "To commit any existing changes, CANCEL this operation and perform a GIT COMMIT.", MsgBoxStyle.OkCancel, "Export APEX application " & My.Settings.CurrentApex) = MsgBoxResult.Ok Then



            Dim password = Main.get_password(ParsingSchemaTextBox.Text, My.Settings.CurrentDB)

            Apex.ApexExportCommit(CurrentConnectionTextBox.Text, ParsingSchemaTextBox.Text, password, My.Settings.CurrentApex, RootApexDirTextBox.Text)
 

        End If
    End Sub


    Private Sub MergeAndPushFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushFeatureToolStripMenuItem.Click

        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim mergeAndPush As ProgressDialogue = New ProgressDialogue("Merge and Push branch:  " & currentBranch)
        mergeAndPush.MdiParent = GitPatcher
        mergeAndPush.addStep("Switch to develop branch", 20)
        mergeAndPush.addStep("Pull from Origin", 40)
        mergeAndPush.addStep("Merge from branch: " & currentBranch, 60)
        mergeAndPush.addStep("Push to Origin", 80)
        mergeAndPush.addStep("Return to branch: " & currentBranch, 100)

        mergeAndPush.Show()

        'Switch to develop branch
        GitBash.Switch(My.Settings.CurrentRepo, "develop")
        mergeAndPush.goNextStep()

        'Pull from origin/develop
        GitBash.Pull(My.Settings.CurrentRepo, "origin", "develop")
        mergeAndPush.goNextStep()

        'Merge from Feature branch
        'TortoiseMerge(My.Settings.CurrentRepo, currentBranch)
        Tortoise.Merge(My.Settings.CurrentRepo)

        mergeAndPush.goNextStep()

        'Push to origin/develop 
        GitBash.Push(My.Settings.CurrentRepo, "origin", "develop")
        mergeAndPush.goNextStep()

        'Return to branch
        'GitSharpFascade.switchBranch(My.Settings.CurrentRepo, currentBranch)
        GitBash.Switch(My.Settings.CurrentRepo, currentBranch)

        'Done
        mergeAndPush.done()

    End Sub

    Private Sub createNewBranch(iBranchType As String)

        If MsgBox("Would you like to create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & Me.AppCodeTextBox.Text & "/JIRA?", MsgBoxStyle.OkCancel, "Create a new " & iBranchType & " Branch") = MsgBoxResult.Ok Then

            Dim branchName As String = InputBox("Enter the Jira Id.", "Jira Id for new " & iBranchType & " Branch")
            Dim newBranch As String = iBranchType & "/" & Me.AppCodeTextBox.Text & "/" & branchName

            If Not String.IsNullOrEmpty(branchName) Then

                Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch:  " & branchName)
                newFeature.MdiParent = GitPatcher
                newFeature.addStep("Switch to develop branch", 25)
                newFeature.addStep("Pull from Origin", 50)
                newFeature.addStep("Create and switch to branch: " & newBranch, 75)
                newFeature.addStep("Create intial Tag: " & branchName & ".00", 100)

                newFeature.Show()

                'Switch to develop branch
                GitBash.Switch(My.Settings.CurrentRepo, "develop")
                newFeature.goNextStep()

                'Pull from origin/develop
                GitBash.Pull(My.Settings.CurrentRepo, "origin", "develop")
                newFeature.goNextStep()

                'Create and Switch to new branch
                GitBash.createBranch(My.Settings.CurrentRepo, newBranch)
                newFeature.goNextStep()

                'Create the initial tag
                GitBash.TagSimple(My.Settings.CurrentRepo, branchName & ".00")
                'GitBash.TagAnnotated(My.Settings.CurrentRepo, branchName & ".00", "Initial tag on new " & Me.ApplicationListComboBox.SelectedItem & " " & iBranchType & " " & branchName)
                newFeature.goNextStep()

                'Done
                newFeature.done()



            End If

        End If
    End Sub

    Private Sub NewFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFeatureToolStripMenuItem.Click
        createNewBranch("feature")
    End Sub

    Private Sub CreateDBPatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBPatchSetToolStripMenuItem.Click
        CreatePatchCollection.createCollectionProcess("patchset", "feature,hotfix", Me.PatchSchemasTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
    End Sub

    Private Sub DBPatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim Wizard As New CreatePatchCollection("", "patchset", "feature,hotfix", Me.PatchSchemasTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
        Wizard.ShowDialog()
    End Sub

    Private Sub DBPatchSetAllTypesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim Wizard As New CreatePatchCollection("", "patchset", "", Me.PatchSchemasTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
        Wizard.ShowDialog()
    End Sub

    Private Sub CreateDBMinorReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBMinorReleaseToolStripMenuItem.Click
        CreatePatchCollection.createCollectionProcess("minor", "patchset", Me.PatchSchemasTextBox.Text, "minor,patchset,feature,hotfix,ALL", "minor,patchset,feature,hotfix,ALL")
    End Sub

    Private Sub NewHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewHotfixToolStripMenuItem.Click
        createNewBranch("hotfix")
    End Sub

    Private Sub TagtestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        GitBash.TagSimple(My.Settings.CurrentRepo, "DEMOTAG")
    End Sub
End Class
