Public Class Main

    Public Sub New()
        InitializeComponent()
        loadRepos()
        loadDBs()
        loadApexApps()
        loadHotFixDBs()
        MinPatchTextBox.Text = My.Settings.MinPatch
 
    End Sub


    Private Sub HotFixToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles HotFixToolStripComboBox.SelectedIndexChanged
        NewHotFixToolStripMenuItem1.Text = "New Hotfix for DB: " & HotFixToolStripComboBox.SelectedItem
        RebaseHotFixToolStripMenuItem.Text = "Rebase Hotfix on Branch: " & Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem)
        MergeAndPushHotfixToolStripMenuItem.Text = "Merge Hotfix to Branch: " & Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem) & ", then Push"
        CreateDBHotFixPatchToolStripMenuItem1.Text = "Create DB Hotfix Patch for DB: " & HotFixToolStripComboBox.SelectedItem
        MultiDBHotFixPatchToolStripMenuItem.Text = "Multi DB Hotfix Patch: " & HotFixToolStripComboBox.SelectedItem & " and Downwards"
    End Sub

    Public Sub loadHotFixDBs()
        HotFixToolStripComboBox.Items.Clear()
        For Each DB In My.Settings.DBList.Split(Chr(10))
            DB = Trim(DB)
            DB = DB.Replace(Chr(13), "")
            If Globals.deriveHotfixBranch(DB).Length Then
                'DB has an assoc hotfix branch so list it.
                HotFixToolStripComboBox.Items.Add(DB)
            End If
  
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

        Globals.setRepo(RepoComboBox.SelectedItem)
        BranchPathTextBox.Text = Globals.currentLongBranch()
        CurrentBranchTextBox.Text = Globals.currentBranch
        RootPatchDirTextBox.Text = Globals.RootPatchDir
        RootApexDirTextBox.Text = Globals.RootApexDir
 

    End Sub

    'Private Sub PatchFromTagsToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    PatchFromTags.createPatchProcess("feature", "DEV", Globals.deriveHotfixBranch("DEV"))
    'End Sub
 
    Private Sub DBListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DBListComboBox.SelectedIndexChanged

        Globals.setDB(DBListComboBox.SelectedItem)
        CurrentConnectionTextBox.Text = Globals.currentConnection()

    End Sub

    Private Sub ApplicationListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ApplicationListComboBox.SelectedIndexChanged

        Globals.setApplication(ApplicationListComboBox.SelectedItem, ApplicationListComboBox.SelectedIndex)
 
        'Patch Set
        AppCodeTextBox.Text = Globals.currentAppCode
        ApexAppTextBox.Text = Globals.currentApex
        ParsingSchemaTextBox.Text = Globals.currentParsingSchema


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

    Public Sub mergeAndPushBranch(iBranchType As String, iBranchTo As String)
        Common.checkBranch(iBranchType)
        Dim currentBranch As String = GitSharpFascade.currentBranch(Globals.currentRepo)

        Dim mergeAndPush As ProgressDialogue = New ProgressDialogue("Merge and Push branch:  " & currentBranch)
        mergeAndPush.MdiParent = GitPatcher
        mergeAndPush.addStep("Switch to " & iBranchTo & " branch")
        mergeAndPush.addStep("Pull from Origin")
        mergeAndPush.addStep("Merge from branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        mergeAndPush.addStep("Commit - incase of merge conflict")
        mergeAndPush.addStep("Push to Origin")
        mergeAndPush.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/" & iBranchTo & "'.  " _
                                 & "If NOT, then the push FAILED. Your " & iBranchTo & " branch is now out of date, so your merged files could be stale. " _
                                 & "In this situation, it is safest to perform a Rebase and then restart the Merge process to ensure you are pushing the lastest merged files. ")
        mergeAndPush.addStep("Return to branch: " & currentBranch)

        mergeAndPush.Show()

        Do Until mergeAndPush.isStarted
            Common.wait(1000)
        Loop

        If mergeAndPush.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(Globals.currentRepo, iBranchTo)

        End If

        If mergeAndPush.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(Globals.currentRepo, "origin", iBranchTo)

        End If

        If mergeAndPush.toDoNextStep() Then
            'Merge from Feature branch
            'TortoiseMerge(Globals.currentRepo, currentBranch)
            Tortoise.Merge(Globals.currentRepo)
        End If

        If mergeAndPush.toDoNextStep() Then
            'Commit - incase of merge conflict
            Tortoise.Commit(Globals.currentRepo, "Merged " & currentBranch & " [CANCEL IF NO MERGE CONFLICTS]")


        End If

        If mergeAndPush.toDoNextStep() Then
            'Push to origin/develop 
            GitBash.Push(Globals.currentRepo, "origin", iBranchTo)

        End If


        If mergeAndPush.toDoNextStep() Then
            'Synch command to verfiy that Push was successful.
            Tortoise.Sync(Globals.currentRepo)
        End If

        If mergeAndPush.toDoNextStep() Then
            'Return to branch
            'GitSharpFascade.switchBranch(Globals.currentRepo, currentBranch)
            GitBash.Switch(Globals.currentRepo, currentBranch)
        End If

        mergeAndPush.toDoNextStep()

    End Sub



    Private Sub MergeAndPushFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushFeatureToolStripMenuItem.Click

        mergeAndPushBranch("feature", "develop")

    End Sub

    Private Sub createNewBranch(iBranchType As String, iBranchFrom As String)

        Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch", "Create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & Me.AppCodeTextBox.Text & "/JIRA.")
        newFeature.MdiParent = GitPatcher
        newFeature.addStep("Switch to " & iBranchFrom & " branch")
        newFeature.addStep("Pull from Origin")
        newFeature.addStep("Create and switch to " & iBranchType & " branch")
        'newFeature.addStep("Create intial Tag: " & branchName & ".00" )



        newFeature.Show()

        Do Until newFeature.isStarted
            Common.wait(1000)
        Loop

        If newFeature.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(Globals.currentRepo, iBranchFrom)

        End If

        If newFeature.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(Globals.currentRepo, "origin", iBranchFrom)

        End If

        If newFeature.toDoNextStep() Then
            'Create and Switch to new branch
            Dim branchName As String = InputBox("Enter the Jira Id.", "Jira Id for new " & iBranchType & " Branch", Globals.currentJiraProject & "-")
            Dim newBranch As String = iBranchType & "/" & Me.AppCodeTextBox.Text & "/" & branchName

            If Not String.IsNullOrEmpty(branchName) Then

                newFeature.updateTitle("Create new " & iBranchType & " branch:  " & branchName)
                newFeature.updateStepDescription(2, "Create and switch to " & iBranchType & " branch: " & newBranch)

                GitBash.createBranch(Globals.currentRepo, newBranch)

            End If
 
            newFeature.toDoNextStep()

        End If

        'Close and Open Main window to refresh it.
        Me.Close()
        GitPatcher.newMainWindow()

    End Sub

    Private Sub NewFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFeatureToolStripMenuItem.Click
        createNewBranch("feature", "develop")
    End Sub

    Private Sub CreateDBPatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBPatchSetToolStripMenuItem.Click
        CreatePatchCollection.createCollectionProcess("patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL", "ISTEST")
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
        CreatePatchCollection.createCollectionProcess("minor", "patchset", Me.AppCodeTextBox.Text, "minor,patchset,feature,hotfix,ALL", "minor,patchset,feature,hotfix,ALL", "ISTEST")
    End Sub



    Private Sub TagtestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        GitBash.TagSimple(Globals.currentRepo, "DEMOTAG")
    End Sub

    Private Sub ShowindexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowindexToolStripMenuItem.Click
        GitSharpFascade.getIndexedChanges(Globals.currentRepo)
    End Sub

    Private Sub TestworkflowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestworkflowToolStripMenuItem.Click

        Dim testWorkflow As ProgressDialogue = New ProgressDialogue("test variable workflow")
        testWorkflow.MdiParent = GitPatcher
        testWorkflow.addStep("Choose a tag to import from")
        testWorkflow.addStep("Checkout the tag", False)
        testWorkflow.addStep("If tag not like ")
        testWorkflow.addStep("Import Apex", False)
        testWorkflow.addStep("Return to branch:")


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

        ' If testWorkflow.toDoNextStep() Then
        '     MsgBox("doing 4")
        '
        ' End If
        '

        'testWorkflow.toDoNextStep()

        testWorkflow.stopAndClose()
 

    End Sub

    Private Sub TestrevertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestrevertToolStripMenuItem.Click
        'GitSharpFascade.revertItem(Globals.currentRepo, "apex/f101/application/create_application.sql")
        Apex.restoreCreateApplicationSQL()
    End Sub

    Public Sub rebaseBranch(iBranchType As String, iRebaseBranchOn As String)

        Common.checkBranch(iBranchType)

        Dim currentBranchLong As String = GitSharpFascade.currentBranch(Globals.currentRepo)
        Dim currentBranchShort As String = Globals.currentBranch

        Dim rebasing As ProgressDialogue = New ProgressDialogue("Rebase branch " & currentBranchLong)

        Dim l_tag_base As String = Nothing

        rebasing.MdiParent = GitPatcher
        rebasing.addStep("Commit to Branch: " & currentBranchLong, True, "Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.")
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", True, "If you get an error concerning uncommitted changes.  Please resolve the changes and then RESTART this process to ensure the switch to " & iRebaseBranchOn & " branch is successful.")
        rebasing.addStep("Pull from Origin")
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & ".99A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & CurrentBranchTextBox.Text & ".99A")
        rebasing.addStep("Return to branch: " & currentBranchLong)
        rebasing.addStep("Rebase Branch: " & currentBranchLong & " From Upstream:" & iRebaseBranchOn, True, "Please select the Upstream Branch:" & iRebaseBranchOn & " from the Tortoise Rebase Dialogue")
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & ".99B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.")
        rebasing.addStep("Revert your VM", True, "Before running patches, consider reverting to a VM snapshot prior to the development of your current work, or swapping to a unit test VM.")
        rebasing.addStep("Use PatchRunner to run Unapplied Patches", True)
        rebasing.addStep("Import Apex from HEAD of branch: " & currentBranchLong, True, "Using the Apex Import workflow")
        rebasing.addStep("Post-Rebase Snapshot", True, "Before creating new patches, snapshot the VM again.  Use this snapshot as a quick restore to point restest patches that have failed, on first execution.")

        rebasing.Show()
 
        Do Until rebasing.isStarted
            Common.wait(1000)
        Loop


        If rebasing.toDoNextStep() Then
            'Committing changed files to GIT"
            Tortoise.Commit(Globals.currentRepo, "CANCEL IF NOT NEEDED: Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.", True)
        End If
 
        If rebasing.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(Globals.currentRepo, iRebaseBranchOn)
        End If
        If rebasing.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(Globals.currentRepo, "origin", iRebaseBranchOn)
        End If

        If rebasing.toDoNextStep() Then
            'Tag the develop head
            l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please enter 2 digit numeric tag for next patch.", "Create Tag for next patch")
            Dim l_tagA As String = currentBranchShort & "." & l_tag_base & "A"
            rebasing.updateStepDescription(2, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
            GitBash.TagSimple(Globals.currentRepo, l_tagA)

        End If


        If rebasing.toDoNextStep() Then
            'Return to branch
            GitBash.Switch(Globals.currentRepo, currentBranchLong)
        End If

        If rebasing.toDoNextStep() Then
            'Rebase branch
            Tortoise.Rebase(Globals.currentRepo)
        End If

        If rebasing.toDoNextStep() Then
            'Tag Branch
            Dim l_tagB As String = currentBranchShort & "." & l_tag_base & "B"
            rebasing.updateStepDescription(5, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
            GitBash.TagSimple(Globals.currentRepo, l_tagB)

        End If

        If rebasing.toDoNextStep() Then
            'Revert VM
            MsgBox("Please create a snapshot of your current VM state, and then revert to a state prior the work about to be patched.", MsgBoxStyle.Exclamation, "Revert VM")

        End If


        If rebasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied Patches
            Dim newchildform As New PatchRunner(True, False, False)
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If

        If rebasing.toDoNextStep() Then
            'Import Apex from HEAD of branch
            Apex.ApexImportFromTag()

        End If

        If rebasing.toDoNextStep() Then
            'Post-Rebase Snapshot 
            MsgBox("Before creating new patches, snapshot the VM again.", MsgBoxStyle.Exclamation, "Post-Rebase Snapshot")

        End If

        'Finish
        rebasing.toDoNextStep()
    End Sub



    Private Sub RebaseFeatureHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseFeatureHotfixToolStripMenuItem.Click
        rebaseBranch("feature", "develop")
    End Sub


    Public Sub releaseTo(iTargetDB As String, Optional ByVal iBranchType As String = "")

        Dim lcurrentDB As String = Globals.currentDB()

        Dim currentBranch As String = GitSharpFascade.currentBranch(Globals.currentRepo)

        Dim releaseFromBranch As String = Globals.deriveHotfixBranch(iTargetDB)

        Dim releasing As ProgressDialogue = New ProgressDialogue("Release to " & iTargetDB)

        releasing.MdiParent = GitPatcher
        releasing.addStep("Change current DB to : " & iTargetDB)
        releasing.addStep("Switch to " & releaseFromBranch & " branch", False)
        releasing.addStep("Pull from Origin to " & releaseFromBranch & " branch", False)

        releasing.addStep("Choose a tag to release from and checkout the tag", False)

        releasing.addStep("Use PatchRunner to run Uninstalled Patches", True, "")
        releasing.addStep("Import Apex", True, "Using the Apex Import workflow")
        releasing.addStep("Smoke Test", True, "Perform a quick test to verify the patched system is working in " & iTargetDB)
        releasing.addStep("Revert current DB to : " & lcurrentDB)
        releasing.Show()



        Do Until releasing.isStarted
            Common.wait(1000)
        Loop

        If releasing.toDoNextStep() Then
            'Change current DB to release DB
            Globals.setDB(iTargetDB.ToUpper)

        End If

        If releasing.toDoNextStep() Then
            'Switch to develop branch
            GitBash.Switch(Globals.currentRepo, releaseFromBranch)
        End If
        If releasing.toDoNextStep() Then
            'Pull from origin/develop
            GitBash.Pull(Globals.currentRepo, "origin", releaseFromBranch)
        End If

        If releasing.toDoNextStep() Then
            'Choose a tag to import from
            Dim tagnames As Collection = New Collection
            tagnames.Add("HEAD")
            tagnames = GitSharpFascade.getTagList(Globals.currentRepo, tagnames, Globals.currentBranch)
            tagnames = GitSharpFascade.getTagList(Globals.currentRepo, tagnames, AppCodeTextBox.Text)


            Dim PatchTag As String = Nothing
            PatchTag = ChoiceDialog.Ask("Please choose a tag for patch installs", tagnames, "HEAD", "Choose tag")

            'Checkout the tag
            GitBash.Switch(Globals.currentRepo, PatchTag)

        End If


        If releasing.toDoNextStep() Then
            'Use PatchRunner to run  Uninstalled Patches
            Dim newchildform As New PatchRunner(False, True, False, iBranchType)
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If

        If releasing.toDoNextStep() Then
            'Import Apex 
            Apex.ApexImportFromTag()

        End If

        If releasing.toDoNextStep() Then
            'Smoke Test 
            MsgBox("Perform a quick test to verify the patched system is working in " & iTargetDB, MsgBoxStyle.Information, "Smoke Test")

        End If


        If releasing.toDoNextStep() Then
            'Revert current DB  
            Globals.setDB(lcurrentDB.ToUpper)

        End If

        'Finish
        releasing.toDoNextStep()
    End Sub



    Private Sub ReleaseToISDEVLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISDEVLToolStripMenuItem.Click
        releaseTo("DEV")
    End Sub

    Private Sub ReleaseToISTESTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISTESTToolStripMenuItem.Click
        releaseTo("TEST")
    End Sub

    Private Sub ReleaseToISUATToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISUATToolStripMenuItem.Click
        releaseTo("UAT")
    End Sub

    Private Sub ReleaseToISPRODToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISPRODToolStripMenuItem.Click
        releaseTo("PROD")
    End Sub

    Private Sub NewHotfixToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewHotFixToolStripMenuItem1.Click
        createNewBranch("hotfix", Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub MergeAndPushHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushHotfixToolStripMenuItem.Click
        mergeAndPushBranch("hotfix", Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub RebaseHotFixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseHotFixToolStripMenuItem.Click
        rebaseBranch("hotfix", Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub CreateDBHotFixPatchToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CreateDBHotFixPatchToolStripMenuItem1.Click
        PatchFromTags.createPatchProcess("hotfix", HotFixToolStripComboBox.SelectedItem, Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub CreateDBFeaturePatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBFeaturePatchToolStripMenuItem.Click
        PatchFromTags.createPatchProcess("feature", "DEV", Globals.deriveHotfixBranch("DEV"))
    End Sub

    Private Sub TestCreatePatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestCreatePatchSetToolStripMenuItem.Click
        'Create, edit And test collection
        Dim Wizard As New CreatePatchCollection("prism-2.17.04", "patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
        Wizard.ShowDialog() 'WAITING HERE!!
    End Sub

    Private Sub MultiDBHotFixPatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiDBHotFixPatchToolStripMenuItem.Click
        Common.checkBranch("hotfix")
        Dim patchThisDB As Boolean = False
        Dim multiHotFix As ProgressDialogue = New ProgressDialogue("Multi HotFix Patch for " & HotFixToolStripComboBox.SelectedItem & " Downwards")

        multiHotFix.MdiParent = GitPatcher

        For i = 0 To HotFixToolStripComboBox.Items.Count - 1

            If HotFixToolStripComboBox.Items(i) = HotFixToolStripComboBox.SelectedItem Then
                patchThisDB = True
            End If

            multiHotFix.addStep("Create a HotFix Patch for DB : " & HotFixToolStripComboBox.Items(i), patchThisDB)

        Next
 
        multiHotFix.Show()

        Do Until multiHotFix.isStarted
            Common.wait(1000)
        Loop

        For i = 0 To HotFixToolStripComboBox.Items.Count - 1

            If multiHotFix.toDoNextStep() Then
                PatchFromTags.createPatchProcess("hotfix", HotFixToolStripComboBox.Items(i), Globals.deriveHotfixBranch(HotFixToolStripComboBox.Items(i)))
            End If
 
        Next

 
        'Finish
        multiHotFix.toDoNextStep()
 
    End Sub

 
    Private Sub UnappliedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnappliedToolStripMenuItem.Click
        Dim newchildform As New PatchRunner(True, False, False)
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub UninstalledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UninstalledToolStripMenuItem.Click
        Dim newchildform As New PatchRunner(False, True, False)
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub AllPatchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllPatchesToolStripMenuItem.Click
        Dim newchildform As New PatchRunner(False, False, True)
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub Import1PageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Import1PageToolStripMenuItem.Click
        Apex.ApexImport1PageFromTag()
    End Sub

    Private Sub GITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GITToolStripMenuItem.Click

    End Sub
End Class
