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
        CreateDBHotFixPatchToolStripMenuItem1.Text = "Create DB Hotfix Patch for Branch: " & HotFixToolStripComboBox.SelectedItem
        MultiDBHotFixPatchToolStripMenuItem.Text = "Multi DB Hotfix Patch: " & HotFixToolStripComboBox.SelectedItem & " and Downwards"
    End Sub

    Public Sub loadHotFixBranches()
        HotFixToolStripComboBox.Items.Clear()
        For Each branch In My.Settings.HotFixBranches.Split(Chr(10))
            branch = Trim(branch)
            branch = branch.Replace(Chr(13), "")
            If (branch.Length > 0) Then
                HotFixToolStripComboBox.Items.Add(branch)
            End If
            'If Globals.currentRepo = branch Then
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

        Globals.setRepo(RepoComboBox.SelectedItem)
        BranchPathTextBox.Text = GitSharpFascade.currentBranch(Globals.currentRepo)
        CurrentBranchTextBox.Text = Globals.currentBranch

 
        RootPatchDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.PatchDirOffset & "\"
        RootApexDirTextBox.Text = RepoComboBox.SelectedItem & My.Settings.ApexDirOffset & "\"

        My.Settings.CurrentRepo = RepoComboBox.SelectedItem

        My.Settings.Save()



    End Sub

    Private Sub PatchFromTagsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PatchFromTags.createPatchProcess("feature", "develop")
    End Sub
 
    Private Sub DBListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DBListComboBox.SelectedIndexChanged
        My.Settings.CurrentDB = DBListComboBox.SelectedItem
        My.Settings.Save()


        Globals.setDB(DBListComboBox.SelectedItem)

        CurrentConnectionTextBox.Text = Globals.deriveConnection()

    End Sub

    Private Sub ApplicationListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ApplicationListComboBox.SelectedIndexChanged
        My.Settings.CurrentApp = ApplicationListComboBox.SelectedItem
        My.Settings.Save()
        Globals.setApplication(ApplicationListComboBox.SelectedItem)

        'repo = Trim(repo)
        'repo = repo.Replace(Chr(13), "")

        'Patch Set
        AppCodeTextBox.Text = Trim(My.Settings.PatchSetList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)).Replace(Chr(13), "")


        ApexAppTextBox.Text = Trim(My.Settings.AppList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)).Replace(Chr(13), "")

        My.Settings.CurrentApex = ApexAppTextBox.Text
        Globals.setApex(ApexAppTextBox.Text)
        ParsingSchemaTextBox.Text = My.Settings.ParsingSchemaList.Split(Chr(10))(ApplicationListComboBox.SelectedIndex)
        Globals.setParsingSchema(ParsingSchemaTextBox.Text)

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
        Common.checkBranch(iBranchType)
        Dim currentBranch As String = GitSharpFascade.currentBranch(Globals.currentRepo)

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
            'Push to origin/develop 
            GitBash.Push(Globals.currentRepo, "origin", iBranchTo)

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
            Dim branchName As String = InputBox("Enter the Jira Id.", "Jira Id for new " & iBranchType & " Branch")
            Dim newBranch As String = iBranchType & "/" & Me.AppCodeTextBox.Text & "/" & branchName

            If Not String.IsNullOrEmpty(branchName) Then

                newFeature.updateTitle("Create new " & iBranchType & " branch:  " & branchName)
                newFeature.updateStepDescription(2, "Create and switch to " & iBranchType & " branch: " & newBranch)

                GitBash.createBranch(Globals.currentRepo, newBranch)

            End If
 
            newFeature.toDoNextStep()

        End If

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

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 4")

        End If

        testWorkflow.toDoNextStep()
        'testWorkflow.toDoNextStep()






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
        rebasing.addStep("Export Apex to branch: " & currentBranchLong, True, "Using the Apex Export workflow")
        rebasing.addStep("Use QCGU to generate changed domain data: " & currentBranchLong, True, "Think hard!  Did i change domain config?  If so, i should logon to QCGU and generate that data out. Then commit it too.")
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch")
        rebasing.addStep("Pull from Origin")
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & ".99A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & CurrentBranchTextBox.Text & ".99A")
        rebasing.addStep("Return to branch: " & currentBranchLong)
        rebasing.addStep("Rebase Branch: " & currentBranchLong & " From Upstream:" & iRebaseBranchOn, True, "Please select the Upstream Branch:" & iRebaseBranchOn & " from the Tortoise Rebase Dialogue")
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & ".99B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.")
        rebasing.addStep("Use PatchRunner to run Unapplied Patches", True, "Before running patches, consider reverting to a VM snapshot prior to the development of your current work, or swapping to a unit test VM.")
        'rebasing.addStep("Review tags on the branch" )
        rebasing.addStep("Import Apex from HEAD of branch: " & currentBranchLong, True, "Using the Apex Import workflow")

        rebasing.Show()



        Do Until rebasing.isStarted
            Common.wait(1000)
        Loop

        If rebasing.toDoNextStep() Then
            'Export Apex to branch
            Apex.ApexExportCommit()

        End If

        If rebasing.toDoNextStep() Then
            'QCGU
            MsgBox("Please launch QCGU and generate Domain data", MsgBoxStyle.Exclamation, "QCGU")

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
            rebasing.updateStepDescription(3, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
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
            rebasing.updateStepDescription(6, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
            GitBash.TagSimple(Globals.currentRepo, l_tagB)

        End If

        If rebasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied Patches
            Dim newchildform As New PatchRunner(True, False, False)
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If
        'If rebasing.toDoNextStep() Then
        '    'Review tags on the branch
        '    Tortoise.Log(Globals.currentRepo)
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
            Dim newchildform As New PatchRunner(False, True, False)
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If

        If releasing.toDoNextStep() Then
            'Import Apex 
            Apex.ApexImportFromTag()

        End If

        If releasing.toDoNextStep() Then
            'Revert current DB  
            Globals.setDB(lcurrentDB.ToUpper)

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

    Private Sub NewHotfixToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewHotFixToolStripMenuItem1.Click
        createNewBranch("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub MergeAndPushHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushHotfixToolStripMenuItem.Click
        mergeAndPushBranch("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub RebaseHotFixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseHotFixToolStripMenuItem.Click
        rebaseBranch("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub CreateDBHotFixPatchToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CreateDBHotFixPatchToolStripMenuItem1.Click
        PatchFromTags.createPatchProcess("hotfix", HotFixToolStripComboBox.SelectedItem)
    End Sub

    Private Sub CreateDBFeaturePatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBFeaturePatchToolStripMenuItem.Click
        PatchFromTags.createPatchProcess("feature", "develop")
    End Sub

    Private Sub TestCreatePatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestCreatePatchSetToolStripMenuItem.Click
        'Create, edit And test collection
        Dim Wizard As New CreatePatchCollection("prism-2.17.04", "patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
        Wizard.ShowDialog() 'WAITING HERE!!
    End Sub

    Private Sub MultiDBHotFixPatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiDBHotFixPatchToolStripMenuItem.Click
        Common.checkBranch("hotfix")
        Dim patchThisBranch As Boolean = False
        Dim multiHotFix As ProgressDialogue = New ProgressDialogue("Multi HotFix Patch for " & HotFixToolStripComboBox.SelectedItem & " Downwards")

        multiHotFix.MdiParent = GitPatcher

        For i = 0 To HotFixToolStripComboBox.Items.Count - 1

            If HotFixToolStripComboBox.Items(i) = HotFixToolStripComboBox.SelectedItem Then
                patchThisBranch = True
            End If

            multiHotFix.addStep("Create a HotFix Patch for branch : " & HotFixToolStripComboBox.Items(i), patchThisBranch)

        Next
 
        multiHotFix.Show()

        Do Until multiHotFix.isStarted
            Common.wait(1000)
        Loop

        For i = 0 To HotFixToolStripComboBox.Items.Count - 1

            If multiHotFix.toDoNextStep() Then
                PatchFromTags.createPatchProcess("hotfix", HotFixToolStripComboBox.Items(i))
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
End Class
