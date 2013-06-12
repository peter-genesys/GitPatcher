Public Class Main

    Public Sub New()
        InitializeComponent()
        loadRepos()
        loadDBs()
        loadApexApps()
        loadHotFixBranches()
        MinPatchTextBox.Text = My.Settings.MinPatch
    End Sub


    Private Sub HotFixToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles HotFixToolStripComboBox.SelectedIndexChanged

        NewHotFixToolStripMenuItem1.Text = "New Hotfix from Branch: " & HotFixToolStripComboBox.SelectedItem
        RebaseHotFixToolStripMenuItem.Text = "Rebase Hotfix on Branch: " & HotFixToolStripComboBox.SelectedItem
        MergeAndPushHotfixToolStripMenuItem.Text = "Merge Hotfix to Branch: " & HotFixToolStripComboBox.SelectedItem & ", then Push"
        CreateDBHotFixPatchToolStripMenuItem.Text = "Create DB Hotfix Patch for Branch: " & HotFixToolStripComboBox.SelectedItem
        CreateDBHotFixPatchToolStripMenuItem1.Text = "Create DB Hotfix Patch for Branch: " & HotFixToolStripComboBox.SelectedItem
    End Sub

    Public Sub loadHotFixBranches()
        HotFixToolStripComboBox.Items.Clear()
        For Each branch In My.Settings.HotFixBranches.Split(Chr(10))
            branch = Trim(branch)
            branch = branch.Replace(Chr(13), "")
            If (branch.Length > 0) Then
                HotFixToolStripComboBox.Items.Add(branch)
            End If
            'If My.Settings.CurrentRepo = branch Then
            '    RepoComboBox.SelectedIndex = RepoComboBox.Items.Count - 1
            'End If
        Next
        HotFixToolStripComboBox.SelectedIndex = 0

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
        PatchFromTags.createPatchProcess("feature", "develop")
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

 
        Apex.ApexImportFromTag()

      
    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click

   
            Apex.ApexExportCommit()
  
    End Sub

    Private Sub mergeAndPushBranch(iBranchType As String, iBranchTo As String)

        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim mergeAndPush As ProgressDialogue = New ProgressDialogue("Merge and Push branch:  " & currentBranch)
        mergeAndPush.MdiParent = GitPatcher
        mergeAndPush.addStep("Switch to " & iBranchTo & " branch", 20)
        mergeAndPush.addStep("Pull from Origin", 40)
        mergeAndPush.addStep("Merge from branch: " & currentBranch, 60)
        mergeAndPush.addStep("Push to Origin", 80)
        mergeAndPush.addStep("Return to branch: " & currentBranch, 100)

        mergeAndPush.Show()

        Do Until mergeAndPush.isStarted
            Common.wait(1000)
        Loop

        If mergeAndPush.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(My.Settings.CurrentRepo, iBranchTo)

        End If

        If mergeAndPush.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(My.Settings.CurrentRepo, "origin", iBranchTo)

        End If

        If mergeAndPush.toDoNextStep() Then
            'Merge from Feature branch
            'TortoiseMerge(My.Settings.CurrentRepo, currentBranch)
            Tortoise.Merge(My.Settings.CurrentRepo)
        End If

        If mergeAndPush.toDoNextStep() Then
            'Push to origin/develop 
            GitBash.Push(My.Settings.CurrentRepo, "origin", iBranchTo)

        End If

        If mergeAndPush.toDoNextStep() Then
            'Return to branch
            'GitSharpFascade.switchBranch(My.Settings.CurrentRepo, currentBranch)
            GitBash.Switch(My.Settings.CurrentRepo, currentBranch)
        End If

        mergeAndPush.toDoNextStep()

    End Sub



    Private Sub MergeAndPushFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushFeatureToolStripMenuItem.Click

        mergeAndPushBranch("feature", "develop")
 
    End Sub

    Private Sub createNewBranch(iBranchType As String, iBranchFrom As String)

        Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch", "Create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & Me.AppCodeTextBox.Text & "/JIRA.")
        newFeature.MdiParent = GitPatcher
        newFeature.addStep("Switch to " & iBranchFrom & " branch", 33)
        newFeature.addStep("Pull from Origin", 60)
        newFeature.addStep("Create and switch to " & iBranchType & " branch", 100)
        'newFeature.addStep("Create intial Tag: " & branchName & ".00", 100)

 

            newFeature.Show()

            Do Until newFeature.isStarted
                Common.wait(1000)
            Loop

            If newFeature.toDoNextStep() Then
                'Switch to develop branch
            GitBash.Switch(My.Settings.CurrentRepo, iBranchFrom)

            End If

            If newFeature.toDoNextStep() Then
                'Pull from origin/develop
            GitBash.Pull(My.Settings.CurrentRepo, "origin", iBranchFrom)

            End If

            If newFeature.toDoNextStep() Then
                'Create and Switch to new branch
                Dim branchName As String = InputBox("Enter the Jira Id.", "Jira Id for new " & iBranchType & " Branch")
                Dim newBranch As String = iBranchType & "/" & Me.AppCodeTextBox.Text & "/" & branchName

                If Not String.IsNullOrEmpty(branchName) Then

                    newFeature.updateTitle("Create new " & iBranchType & " branch:  " & branchName)
                    newFeature.updateStepDescription(2, "Create and switch to " & iBranchType & " branch: " & newBranch)
 
                    GitBash.createBranch(My.Settings.CurrentRepo, newBranch)

                End If

                'If newFeature.toDoNextStep() Then
                '    'Create the initial tag
                '    GitBash.TagSimple(My.Settings.CurrentRepo, branchName & ".00")
                '    'GitBash.TagAnnotated(My.Settings.CurrentRepo, branchName & ".00", "Initial tag on new " & Me.ApplicationListComboBox.SelectedItem & " " & iBranchType & " " & branchName)
                '
                'End If

                newFeature.toDoNextStep()
 
        End If
         
    End Sub

    Private Sub NewFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFeatureToolStripMenuItem.Click
        createNewBranch("feature", "develop")
    End Sub

    Private Sub CreateDBPatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBPatchSetToolStripMenuItem.Click
        CreatePatchCollection.createCollectionProcess("patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
    End Sub

    'Private Sub DBPatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Dim Wizard As New CreatePatchCollection("", "patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
    '    Wizard.ShowDialog()
    'End Sub

    'Private Sub DBPatchSetAllTypesToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    Dim Wizard As New CreatePatchCollection("", "patchset", "", Me.PatchSchemasTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
    '    Wizard.ShowDialog()
    'End Sub

    Private Sub CreateDBMinorReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBMinorReleaseToolStripMenuItem.Click
        CreatePatchCollection.createCollectionProcess("minor", "patchset", Me.AppCodeTextBox.Text, "minor,patchset,feature,hotfix,ALL", "minor,patchset,feature,hotfix,ALL")
    End Sub

 

    Private Sub TagtestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        GitBash.TagSimple(My.Settings.CurrentRepo, "DEMOTAG")
    End Sub

    Private Sub ShowindexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowindexToolStripMenuItem.Click
        GitSharpFascade.getIndexedChanges(My.Settings.CurrentRepo)
    End Sub
 
    Private Sub TestworkflowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestworkflowToolStripMenuItem.Click

        Dim testWorkflow As ProgressDialogue = New ProgressDialogue("test variable workflow")
        testWorkflow.MdiParent = GitPatcher
        testWorkflow.addStep("Choose a tag to import from", 20)
        testWorkflow.addStep("Checkout the tag", 40, False)
        testWorkflow.addStep("If tag not like ", 60)
        testWorkflow.addStep("Import Apex", 80, False)
        testWorkflow.addStep("Return to branch:", 100)


        testWorkflow.Show()

        Do Until testWorkflow.isStarted
            Common.wait(1000)
        Loop

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 0")

        End If
        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 1")

        End If

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 2")

        End If
        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 3")

        End If

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 4")

        End If

        testWorkflow.toDoNextStep()
        'testWorkflow.toDoNextStep()






    End Sub

    Private Sub TestrevertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestrevertToolStripMenuItem.Click
        'GitSharpFascade.revertItem(My.Settings.CurrentRepo, "apex/f101/application/create_application.sql")
        Apex.restoreCreateApplicationSQL()
    End Sub

    Public Sub rebaseBranch(iBranchType As String, iRebaseBranchOn As String)
        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim rebasing As ProgressDialogue = New ProgressDialogue("Rebase branch " & currentBranch)

        Dim l_tag_base As String = Nothing

        rebasing.MdiParent = GitPatcher
        rebasing.addStep("Export Apex to branch: " & currentBranch, 10, True, "Using the Apex Export workflow")
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", 20)
        rebasing.addStep("Pull from Origin", 30)
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & CurrentBranchTextBox.Text & ".99A", 40, True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & CurrentBranchTextBox.Text & ".99A")
        rebasing.addStep("Return to branch: " & currentBranch, 50)
        rebasing.addStep("Rebase Branch: " & currentBranch, 60)
        rebasing.addStep("Tag Branch: " & currentBranch & " HEAD with " & CurrentBranchTextBox.Text & ".99B", 70, True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & CurrentBranchTextBox.Text & ".99B.")
        rebasing.addStep("Use PatchRunner to run Unapplied/Uninstalled Patches", 80, True, "Before running patches, consider reverting to a VM snapshot prior to the development of your current work, or swapping to a unit test VM.")
        'rebasing.addStep("Review tags on the branch", 90)
        rebasing.addStep("Import Apex from HEAD of branch: " & currentBranch, 100, True, "Using the Apex Import workflow")

        rebasing.Show()



        Do Until rebasing.isStarted
            Common.wait(1000)
        Loop

        If rebasing.toDoNextStep() Then
            'Export Apex to branch
            Apex.ApexExportCommit()

        End If

        If rebasing.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(My.Settings.CurrentRepo, iRebaseBranchOn)
        End If
        If rebasing.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(My.Settings.CurrentRepo, "origin", iRebaseBranchOn)
        End If

        If rebasing.toDoNextStep() Then
            'Tag the develop head
            l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please enter 2 digit numeric tag for next patch.", "Create Tag for next patch")
            Dim l_tagA As String = CurrentBranchTextBox.Text & "." & l_tag_base & "A"
            rebasing.updateStepDescription(3, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
            GitBash.TagSimple(My.Settings.CurrentRepo, l_tagA)

        End If


        If rebasing.toDoNextStep() Then
            'Return to branch
            GitBash.Switch(My.Settings.CurrentRepo, currentBranch)
        End If

        If rebasing.toDoNextStep() Then
            'Rebase branch
            Tortoise.Rebase(My.Settings.CurrentRepo)
        End If

        If rebasing.toDoNextStep() Then
            'Tag Branch
            Dim l_tagB As String = CurrentBranchTextBox.Text & "." & l_tag_base & "B"
            rebasing.updateStepDescription(6, "Tag Branch: " & currentBranch & " HEAD with " & l_tagB)
            GitBash.TagSimple(My.Settings.CurrentRepo, l_tagB)

        End If

        If rebasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied/Uninstalled Patches
            Dim newchildform As New PatchRunner
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If
        'If rebasing.toDoNextStep() Then
        '    'Review tags on the branch
        '    Tortoise.Log(My.Settings.CurrentRepo)
        'End If

        If rebasing.toDoNextStep() Then
            'Import Apex from HEAD of branch
            Apex.ApexImportFromTag()

        End If

        'Finish
        rebasing.toDoNextStep()
    End Sub



    Private Sub RebaseFeatureHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseFeatureHotfixToolStripMenuItem.Click
        rebaseBranch("feature", "develop")
    End Sub


    Public Sub releaseTo(iTargetDB As String)

        Dim lcurrentDB As String = DBListComboBox.SelectedItem
        Dim lTargetDB As String = iTargetDB.ToLower
        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim releasing As ProgressDialogue = New ProgressDialogue("Release to " & iTargetDB)
 
        releasing.MdiParent = GitPatcher
        releasing.addStep("Change current DB to : " & lTargetDB, 10)
        releasing.addStep("Switch to develop branch", 20, False)
        releasing.addStep("Pull from Origin", 30, False)

        releasing.addStep("Choose a tag to release from and checkout the tag", 40, False)
 
        releasing.addStep("Use PatchRunner to run Uninstalled Patches", 40, True, "")
        releasing.addStep("Import Apex", 50, True, "Using the Apex Import workflow")
        releasing.addStep("Revert current DB to : " & lcurrentDB, 60)
        releasing.Show()



        Do Until releasing.isStarted
            Common.wait(1000)
        Loop

        If releasing.toDoNextStep() Then
            'Change current DB to release DB
            DBListComboBox.SelectedItem = lTargetDB

        End If

        If releasing.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(My.Settings.CurrentRepo, "develop")
        End If
        If releasing.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(My.Settings.CurrentRepo, "origin", "develop")
        End If

        If releasing.toDoNextStep() Then
            'Choose a tag to import from
            Dim tagnames As Collection = New Collection
            tagnames.Add("HEAD")
            tagnames = GitSharpFascade.getTagList(My.Settings.CurrentRepo, tagnames, CurrentBranchTextBox.Text)
            tagnames = GitSharpFascade.getTagList(My.Settings.CurrentRepo, tagnames, AppCodeTextBox.Text)


            Dim PatchTag As String = Nothing
            PatchTag = ChoiceDialog.Ask("Please choose a tag for patch installs", tagnames, "HEAD", "Choose tag")

            'Checkout the tag
            GitBash.Switch(My.Settings.CurrentRepo, PatchTag)

        End If


        If releasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied/Uninstalled Patches
            Dim newchildform As New PatchRunner
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If

        If releasing.toDoNextStep() Then
            'Import Apex 
            Apex.ApexImportFromTag()

        End If

        If releasing.toDoNextStep() Then
            'Revert current DB  
            DBListComboBox.SelectedItem = lcurrentDB

        End If

        'Finish
        releasing.toDoNextStep()
    End Sub



    Private Sub ReleaseToISDEVLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISDEVLToolStripMenuItem.Click
        releaseTo("ISDEVL")
    End Sub

    Private Sub ReleaseToISTESTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISTESTToolStripMenuItem.Click
        releaseTo("ISTEST")
    End Sub

    Private Sub ReleaseToISUATToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISUATToolStripMenuItem.Click
        releaseTo("ISUAT")
    End Sub

    Private Sub ReleaseToISPRODToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISPRODToolStripMenuItem.Click
        releaseTo("ISPROD")
    End Sub

    Private Sub NewHotfixToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        createNewBranch("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub MergeAndPushHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushHotfixToolStripMenuItem.Click
        mergeAndPushBranch("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub RebaseHotFixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseHotFixToolStripMenuItem.Click
        rebaseBranch("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub CreateDBHotFixPatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBHotFixPatchToolStripMenuItem.Click
        PatchFromTags.createPatchProcess("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub CreateDBHotFixPatchToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CreateDBHotFixPatchToolStripMenuItem1.Click
        PatchFromTags.createPatchProcess("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub CreateDBFeaturePatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBFeaturePatchToolStripMenuItem.Click
        PatchFromTags.createPatchProcess("feature", "develop")
    End Sub
End Class
