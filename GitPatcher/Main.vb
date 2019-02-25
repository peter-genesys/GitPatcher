Public Class Main

    Public Sub New()

        InitializeComponent()

        Dim DB_count As Integer = -1
        For Each DB In DBComboBox.Items
            DB_count = DB_count + 1
            If DB = Globals.getDB Then
                DBComboBox.SelectedIndex = DB_count
            End If

        Next


        'SelectedIndex = 4 'Default to VM

        loadRepos()
        'loadDBs()
        'loadApexApps()
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
        Logger.Dbg("Main.loadHotFixDBs")
        HotFixToolStripComboBox.Items.Clear()

        HotFixToolStripComboBox.Items.Add("PROD")
        HotFixToolStripComboBox.Items.Add("UAT")
        HotFixToolStripComboBox.Items.Add("TEST")
        HotFixToolStripComboBox.Items.Add("DEV")

        HotFixToolStripComboBox.SelectedIndex = 0

    End Sub


    Private Sub SetMergeRebaseButtons()

        Select Case Globals.currentBranch
            Case "develop", "test", "uat", "master"
                MergeButton.Show()
                RebaseButton.Hide()

            Case Else
                MergeButton.Hide()
                RebaseButton.Show()

        End Select

    End Sub




    Private Sub showRepoSettings()
        Logger.Dbg("Main.showRepoSettings")
        RepoSettings.checkRepo(RepoComboBox.Text)
        RepoPathTextBox.Text = Globals.getRepoPath()

        BranchPathTextBox.Text = Globals.currentLongBranch()
        CurrentBranchTextBox.Text = Globals.currentBranch
        RootPatchDirTextBox.Text = Globals.RootPatchDir
        RootApexDirTextBox.Text = Globals.RootApexDir

        SetMergeRebaseButtons()

        loadOrgs()
        loadApexApps()

    End Sub



    Public Sub loadRepos()

        Logger.Dbg("Main.loadRepos")
        RepoSettings.readGitRepos(RepoComboBox, My.Settings.CurrentRepo)
        showRepoSettings()

    End Sub


    Private Sub showOrgSettings()


        OrgSettings.retrieveOrg(OrgComboBox.Text, DBComboBox.Text, RepoComboBox.Text)

        OrgCodeTextBox.Text = Globals.getOrgCode()
        OrgInFeatureCheckBox.Checked = Globals.getOrgInFeature = "Y"
        HotFixTextBox.Text = Globals.deriveHotfixBranch
        TNSTextBox.Text = Globals.getTNS()
        CONNECTTextBox.Text = Globals.getCONNECT()

    End Sub


    Public Sub loadOrgs()

        OrgSettings.readOrgs(OrgComboBox, OrgComboBox.Text, RepoComboBox.Text)
 
        If String.IsNullOrEmpty(Globals.getDB) Then
            Globals.setDB("VM")
        End If
        showOrgSettings()

    End Sub


    Private Sub showAppSettings()

        AppSettings.retrieveApp(ApplicationListComboBox.Text, RepoComboBox.Text)
        Logger.Note("AppName", Globals.getAppName())
        AppCodeTextBox.Text = Globals.getAppCode()
        AppInFeatureCheckBox.Checked = Globals.getAppInFeature = "Y"
        AppIdTextBox.Text = Globals.getAppId()
        JiraTextBox.Text = Globals.getJira()
        SchemaTextBox.Text = Globals.getSchema()


    End Sub


    Public Sub loadApexApps()

        AppSettings.readApps(ApplicationListComboBox, ApplicationListComboBox.Text, RepoComboBox.Text)
        showAppSettings()



    End Sub


    Private Sub RepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RepoComboBox.SelectedIndexChanged

        Globals.setRepoName(RepoComboBox.SelectedItem)
        showRepoSettings()

    End Sub

    'Private Sub PatchFromTagsToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    PatchFromTags.createPatchProcess("feature", "DEV", Globals.deriveHotfixBranch("DEV"))
    'End Sub

    Private Sub DBListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DBComboBox.SelectedIndexChanged

        Globals.setDB(DBComboBox.SelectedItem)
        If Not String.IsNullOrEmpty(RepoComboBox.Text) Then
            showOrgSettings()
        End If

    End Sub

    Private Sub ApplicationListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ApplicationListComboBox.SelectedIndexChanged

        Globals.setAppName(ApplicationListComboBox.Text)
        showAppSettings()

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


        WF_Apex.ApexImportFromTag()


    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click


        WF_Apex.ApexExportCommit()

    End Sub


    Private Sub MergeAndPushFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushFeatureToolStripMenuItem.Click

        WF_mergeAndPush.mergeAndPushBranch("feature", "develop")

    End Sub



    Private Sub NewFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFeatureToolStripMenuItem.Click

        'Call worksflow
        WF_newBranch.createNewBranch("feature", "develop")

        'Close and Open Main window to refresh it.
        Me.Close()
        GitPatcher.newMainWindow()

    End Sub

    Private Sub CreateDBPatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBPatchSetToolStripMenuItem.Click
        WF_release.createReleaseProcess("patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL", "TEST")
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
        WF_release.createReleaseProcess("minor", "patchset", Me.AppCodeTextBox.Text, "minor,patchset,feature,hotfix,ALL", "minor,patchset,feature,hotfix,ALL", "TEST")
    End Sub



    Private Sub TagtestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        GitOp.createTag("DEMOTAG")
    End Sub

    Private Sub ShowindexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowindexToolStripMenuItem.Click
        GitOp.getIndexedChanges()
    End Sub

    Private Sub TestworkflowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestworkflowToolStripMenuItem.Click

        WF_test.test()

    End Sub

    Private Sub TestrevertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestrevertToolStripMenuItem.Click
        'GitSharpFascade.revertItem(Globals.currentRepo, "apex/f101/application/create_application.sql")
        Apex.restoreCreateApplicationSQL()
    End Sub



    Private Sub RebaseFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseFeatureToolStripMenuItem.Click
        WF_rebase.rebaseBranch("feature", "DEV", "develop")
    End Sub

    Private Sub ReleaseToISDEVLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISDEVLToolStripMenuItem.Click
        WF_release.releaseTo("DEV")
    End Sub

    Private Sub ReleaseToISTESTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISTESTToolStripMenuItem.Click
        WF_release.releaseTo("TEST")
    End Sub

    Private Sub ReleaseToISUATToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISUATToolStripMenuItem.Click
        WF_release.releaseTo("UAT")
    End Sub

    Private Sub ReleaseToISPRODToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToISPRODToolStripMenuItem.Click
        WF_release.releaseTo("PROD")
    End Sub

    Private Sub NewHotfixToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewHotFixToolStripMenuItem1.Click
        WF_newBranch.createNewBranch("hotfix", Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub MergeAndPushHotfixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushHotfixToolStripMenuItem.Click
        WF_mergeAndPush.mergeAndPushBranch("hotfix", Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub RebaseHotFixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseHotFixToolStripMenuItem.Click
        WF_rebase.rebaseBranch("hotfix", HotFixToolStripComboBox.SelectedItem, Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub CreateDBHotFixPatchToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CreateDBHotFixPatchToolStripMenuItem1.Click
        WF_createPatch.createPatchProcess("hotfix", HotFixToolStripComboBox.SelectedItem, Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem))
    End Sub

    Private Sub CreateDBFeaturePatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBFeaturePatchToolStripMenuItem.Click
        WF_createPatch.createPatchProcess("feature", "DEV", Globals.deriveHotfixBranch("DEV"))
    End Sub

    Private Sub TestCreatePatchSetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestCreatePatchSetToolStripMenuItem.Click
        'Create, edit And test collection
        Dim Wizard As New CreateRelease("prism-2.17.04", "patchset", "feature,hotfix", Me.AppCodeTextBox.Text, "patchset,feature,hotfix,ALL", "patchset,feature,hotfix,ALL")
        Wizard.ShowDialog() 'WAITING HERE!!
    End Sub

    Private Sub MultiDBHotFixPatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiDBHotFixPatchToolStripMenuItem.Click
        WF_hotFixRelease.hotFixRelease(HotFixToolStripComboBox)
    End Sub

    Private Sub UnappliedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnappliedToolStripMenuItem.Click
        Dim newchildform As New PatchRunner("Unapplied")
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub UninstalledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UninstalledToolStripMenuItem.Click
        Dim newchildform As New PatchRunner("Uninstalled")
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub AllPatchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllPatchesToolStripMenuItem.Click
        Dim newchildform As New PatchRunner("All")
        newchildform.MdiParent = GitPatcher
        newchildform.Show()
    End Sub

    Private Sub Import1PageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Import1PageToolStripMenuItem.Click
        WF_Apex.ApexImport1PageFromTag()
    End Sub

    Private Sub GITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GITToolStripMenuItem.Click

    End Sub

    Private Sub CreateDBMajorReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBMajorReleaseToolStripMenuItem.Click
        WF_release.createReleaseProcess("major", "minor", Me.AppCodeTextBox.Text, "major,minor,patchset,feature,hotfix,ALL", "major,minor,patchset,feature,hotfix,ALL", "TEST")
    End Sub


    Private Sub SwitchButton_Click(sender As Object, e As EventArgs) Handles SwitchButton.Click
        Tortoise.Switch(Globals.getRepoPath)
        BranchPathTextBox.Text = Globals.currentLongBranch()
        CurrentBranchTextBox.Text = Globals.currentBranch
        SetMergeRebaseButtons()
    End Sub

    Private Sub RebaseButton_Click(sender As Object, e As EventArgs) Handles RebaseButton.Click
        Tortoise.Rebase(Globals.getRepoPath)
    End Sub

    Private Sub MergeButton_Click(sender As Object, e As EventArgs) Handles MergeButton.Click
        Tortoise.Merge(Globals.getRepoPath)
    End Sub
End Class
