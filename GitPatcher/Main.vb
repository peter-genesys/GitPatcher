Public Class Main
    Private NO_VM As String = "No VM"

    Public Sub displayVBoxName()

        'If VBoxNameToolStripMenuItem.Text = NO_VM Then
        '    VBoxNameToolStripMenuItem.Font.Strikeout
        'End If

        If String.IsNullOrEmpty(My.Settings.VBoxDir) Then
            VBoxNameToolStripMenuItem.Text = "Virtual Box path not set"
            StartVMToolStripMenuItem.Visible = False
            SaveVMToolStripMenuItem.Visible = False
            ChooseVMToolStripMenuItem.Visible = False
            RestoreStateVMToolStripMenuItem.Visible = False
        Else

            If My.Settings.VBoxName <> NO_VM Then

                'Is the VM runnning
                Dim RunningVMsList As Collection = New Collection
                WF_virtual_box.getVMs(RunningVMsList, "runningvms")

                Dim isVMrunning As Boolean = RunningVMsList.Contains(My.Settings.VBoxName)

                If isVMrunning Then
                    VBoxNameToolStripMenuItem.Text = My.Settings.VBoxName & " (running)"
                    StartVMToolStripMenuItem.Visible = False
                    SaveVMToolStripMenuItem.Visible = True
                Else

                    StartVMToolStripMenuItem.Text = "Start VM (" & My.Settings.startvmType & ")"

                    'Does VM exist
                    Dim VMsList As Collection = New Collection
                    WF_virtual_box.getVMs(VMsList, "vms")

                    Dim VMexists As Boolean = VMsList.Contains(My.Settings.VBoxName)

                    If Not VMexists Then
                        'VM no longer exists, so set VBoxName to NO VM
                        My.Settings.VBoxName = NO_VM
                    End If

                    VBoxNameToolStripMenuItem.Text = My.Settings.VBoxName
                    StartVMToolStripMenuItem.Visible = True
                    SaveVMToolStripMenuItem.Visible = False
                End If

            End If


            If My.Settings.VBoxName = NO_VM Then


                StartVMToolStripMenuItem.Visible = False
                SaveVMToolStripMenuItem.Visible = False

                RestoreStateVMToolStripMenuItem.Visible = False
                VBoxNameToolStripMenuItem.Checked = False
                VBoxNameToolStripMenuItem.Enabled = False

            Else
                RestoreStateVMToolStripMenuItem.Visible = True
                VBoxNameToolStripMenuItem.Checked = True
                VBoxNameToolStripMenuItem.Enabled = True

            End If

        End If
    End Sub


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

        loadHotFixComboBoxDBs()
        MinPatchTextBox.Text = My.Settings.MinPatch
        TextBoxReleaseId.Text = My.Settings.ReleaseId

        displayVBoxName()

        GitPatcher.MainToolStripMenu.Visible = False
        GitPatcher.ConfigToolStripMenu.Visible = False

    End Sub


    Private Sub Main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GitPatcher.MainToolStripMenu.Visible = True
        GitPatcher.ConfigToolStripMenu.Visible = True
    End Sub


    Private Sub HotFixToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles HotFixToolStripComboBox.SelectedIndexChanged
        NewHotFixToolStripMenuItem1.Text = "New Hotfix for DB: " & HotFixToolStripComboBox.SelectedItem
        RebaseHotFixToolStripMenuItem.Text = "Rebase Hotfix on Branch: " & Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem)
        MergeAndPushHotfixToolStripMenuItem.Text = "Merge Hotfix to Branch: " & Globals.deriveHotfixBranch(HotFixToolStripComboBox.SelectedItem) & ", then Push"
        CreateDBHotFixPatchToolStripMenuItem1.Text = "Create DB Hotfix Patch for DB: " & HotFixToolStripComboBox.SelectedItem
        MultiDBHotFixPatchToolStripMenuItem.Text = "Multi DB Hotfix Patch: " & HotFixToolStripComboBox.SelectedItem & " and Downwards"
    End Sub

    Public Sub loadHotFixComboBoxDBs()
        Logger.Dbg("Main.loadHotFixComboBoxDBs")
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

    Private Sub SetFeatureMenuItems()

        Dim showMenuItems As Boolean = Globals.currentLongBranch.Contains("feature")

        CreateDBFeaturePatchToolStripMenuItem.Visible = showMenuItems
        FeaturePatchToolStripMenuItem.Visible = showMenuItems
        RebaseFeatureFullToolStripMenuItem.Visible = showMenuItems
        MergeAndPushFeatureToolStripMenuItem.Visible = showMenuItems


        VersionPatchToolStripMenuItem.Visible = Globals.currentLongBranch.Contains("version")

    End Sub



    Private Sub showRepoSettings()
        Logger.Dbg("Main.showRepoSettings")
        RepoSettings.checkRepo(Globals.getRepoName)
        RepoPathTextBox.Text = Globals.getRepoPath()

        BranchPathTextBox.Text = Globals.currentLongBranch()
        CurrentBranchTextBox.Text = Globals.currentBranch
        RootPatchDirTextBox.Text = Globals.RootPatchDir
        RootApexDirTextBox.Text = Globals.RootApexDir

        SetFeatureMenuItems()
        SetMergeRebaseButtons()

        loadOrgs()
        loadApexApps()

    End Sub



    Public Sub loadRepos()

        Logger.Dbg("Main.loadRepos")
        RepoSettings.readGitRepos(RepoComboBox, My.Settings.CurrentRepo)
        'showRepoSettings()

    End Sub


    Private Sub showOrgSettings()

        OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

        OrgCodeTextBox.Text = Globals.getOrgCode()
        OrgInFeatureCheckBox.Checked = Globals.getOrgInFeature = "Y"
        HotFixTextBox.Text = Globals.deriveHotfixBranch
        TNSTextBox.Text = Globals.getTNS()
        CONNECTTextBox.Text = Globals.getCONNECT()

    End Sub


    Public Sub loadOrgs()

        OrgSettings.readOrgs(OrgComboBox, Globals.getOrgName(), Globals.getRepoName)
        showOrgSettings()


        'OrgSettings.readOrgs(OrgComboBox, OrgComboBox.Text, RepoComboBox.Text)


        ''VERSION 1
        ''Derive promolevels and display best promo-level (last chosen, or lowest available.)
        'If String.IsNullOrEmpty(Globals.getDB) Then
        'Globals.setDB("VM")
        'End If
        'Dim promoList As Collection = New Collection()
        ''promoList = Globals.getPromoList()
        'promoList = OrgSettings.retrieveOrgPromos(OrgComboBox.Text, "PROD|UAT|TEST|DEV|VM", Globals.getRepoName())
        'Dim promoFound As Boolean = False
        'Dim lowestPromo As String = Nothing
        'DBComboBox.Items.Clear()
        'For Each promo In promoList
        '    'For Each promo In Globals.getPromoList(OrgComboBox.Text)
        '    'For Each promo In Globals.getPromoDict()
        '    DBComboBox.Items.Add(promo)
        '    lowestPromo = promo
        '    If promo = Globals.getDB Then
        '        promoFound = True
        '    End If
        'Next
        'If Not promoFound Then
        '    Globals.setDB(lowestPromo)
        'End If
        'DBComboBox.Text = Globals.getDB


        'OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)
        'showOrgSettings()

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
        'raise an exception if password is null.  Trap it downstream in the calling routines.
        If String.IsNullOrEmpty(password) Then
            Throw New System.Exception("User Cancelled Operation")
        End If

        Return password
    End Function


    Shared Function get_connect_string(ByVal schema As String, ByVal database As String, ByVal datasource As String) As String

        Return connect_string(schema, get_password(schema, database), datasource)

    End Function


    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click


        WF_Apex.ApexImportFromTag()


    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click


        WF_Apex.ApexExportCommit()

    End Sub


    Private Sub MergeAndPushFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MergeAndPushFeatureToolStripMenuItem.Click

        WF_mergeAndPush.mergeAndPushBranch("feature", Globals.deriveHotfixBranch("DEV"))

    End Sub



    Private Sub NewFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewFeatureToolStripMenuItem.Click

        'Call worksflow
        WF_newBranch.createNewBranch("feature", Globals.deriveHotfixBranch("DEV"))

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
        GitOp.createTagHead("DEMOTAG")
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

    'Private Sub RebaseFeatureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebaseFeatureToolStripMenuItem.Click
    '    WF_rebase.rebaseBranch("feature", "DEV", Globals.deriveHotfixBranch("DEV"), False, True, True)
    'End Sub
    Private Sub ReleaseToISDEVLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToDEVMenuItem.Click
        WF_release.releaseTo("DEV")
    End Sub

    Private Sub ReleaseToISTESTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToTESTMenuItem.Click
        WF_release.releaseTo("TEST")
    End Sub

    Private Sub ReleaseToISUATToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToUATMenuItem.Click
        WF_release.releaseTo("UAT")
    End Sub

    Private Sub ReleaseToISPRODToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToPRODMenuItem.Click
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

        'Pull
        GitOp.pullWhenMasterBranch()
        Dim l_no_unapplied_patches As Boolean = True
        Dim GitPatcherChild As PatchRunner = New PatchRunner(l_no_unapplied_patches, "Unapplied")
        If Not l_no_unapplied_patches Then
            Dim l_no_queued_apps As Boolean = True
            Dim GitPatcherChild2 As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")
        End If


    End Sub

    Private Sub UninstalledToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UninstalledToolStripMenuItem.Click

        'Pull
        GitOp.pullWhenMasterBranch()
        Dim l_no_uninstalled_patches As Boolean = True
        Dim GitPatcherChild As PatchRunner = New PatchRunner(l_no_uninstalled_patches, "Uninstalled")
        If Not l_no_uninstalled_patches Then
            Dim l_no_queued_apps As Boolean = True
            Dim GitPatcherChild2 As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")
        End If

    End Sub

    Private Sub AllPatchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllPatchesToolStripMenuItem.Click

        'Pull
        GitOp.pullWhenMasterBranch()

        Dim l_no_patches As Boolean = True
        Dim GitPatcherChild As PatchRunner = New PatchRunner(l_no_patches, "All")
        If Not l_no_patches Then
            Dim l_no_queued_apps As Boolean = True
            Dim GitPatcherChild2 As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")
        End If


    End Sub

    Private Sub GITToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PATCHToolStripMenuItem.Click

    End Sub

    Private Sub CreateDBMajorReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBMajorReleaseToolStripMenuItem.Click
        WF_release.createReleaseProcess("major", "minor", Me.AppCodeTextBox.Text, "major,minor,patchset,feature,hotfix,ALL", "major,minor,patchset,feature,hotfix,ALL", "TEST")
    End Sub


    Private Sub SwitchButton_Click(sender As Object, e As EventArgs) Handles SwitchButton.Click
        Tortoise.Switch(Globals.getRepoPath)
        BranchPathTextBox.Text = Globals.currentLongBranch()
        CurrentBranchTextBox.Text = Globals.currentBranch
        SetFeatureMenuItems()
        SetMergeRebaseButtons()
    End Sub

    Private Sub RebaseButton_Click(sender As Object, e As EventArgs) Handles RebaseButton.Click
        Tortoise.Rebase(Globals.getRepoPath)
    End Sub

    Private Sub MergeButton_Click(sender As Object, e As EventArgs) Handles MergeButton.Click
        Tortoise.Merge(Globals.getRepoPath)
    End Sub

    Private Sub CreateDBReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateDBReleaseToolStripMenuItem.Click
        WF_release.createReleaseProcess("release", "feature,hotfix,version", Me.AppCodeTextBox.Text, "release,feature,hotfix,version,ALL", "release,feature,hotfix,ALL", "TEST")
    End Sub

    Private Sub OrgComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OrgComboBox.SelectedIndexChanged
        'setOrgCode(OrgComboBox.SelectedItem)
        Globals.setOrgName(OrgComboBox.SelectedItem)

        'Reset the DB
        If Not String.IsNullOrEmpty(OrgComboBox.SelectedItem) Then

            'VERSION 1
            'Derive promolevels and display best promo-level (last chosen, or lowest available.)
            If String.IsNullOrEmpty(Globals.getDB) Then
                Globals.setDB("VM")
            End If
            'Dim promoList As Collection = New Collection()
            'promoList = Globals.getPromoList()
            ' promoList = OrgSettings.retrieveOrgPromos(OrgComboBox.Text, "PROD|UAT|TEST|DEV|VM", Globals.getRepoName())
            Dim promoFound As Boolean = False
            Dim lowestPromo As String = Nothing
            DBComboBox.Items.Clear()
            'For Each promo In promoList
            'For Each promo In Globals.getPromoList(OrgComboBox.Text)
            For Each promo In Globals.getPromoList
                DBComboBox.Items.Add(promo)
                lowestPromo = promo
                If Globals.getDB = promo Then
                    promoFound = True
                End If
            Next
            If Not promoFound Then
                Globals.setDB(lowestPromo)
            End If
            DBComboBox.Text = Globals.getDB

            ReleaseToVMMenuItem.Visible = Globals.getPromoList.Contains("VM")
            ReleaseToDEVMenuItem.Visible = Globals.getPromoList.Contains("DEV")
            ReleaseToTESTMenuItem.Visible = Globals.getPromoList.Contains("TEST")
            ReleaseToUATMenuItem.Visible = Globals.getPromoList.Contains("UAT")
            ReleaseToPRODMenuItem.Visible = Globals.getPromoList.Contains("PROD")

        End If

        'showOrgSettings()
    End Sub

    Private Sub MainActivated(sender As Object, e As EventArgs) Handles Me.Activated
        'Refresh repo info
        showRepoSettings()
    End Sub

    Private Sub QueuedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueuedToolStripMenuItem.Click

        'Pull
        GitOp.pullWhenMasterBranch()
        Dim l_no_queued_apps As Boolean = True
        Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")

    End Sub

    Private Sub AllApexAppsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllApexAppsToolStripMenuItem.Click

        'Pull
        GitOp.pullWhenMasterBranch()
        Dim l_no_apps As Boolean = True
        Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_apps, "All")

    End Sub

    Private Sub ApexAppExporterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApexAppExporterToolStripMenuItem.Click

        Dim GitPatcherChild As ApexAppExporter = New ApexAppExporter(True)

    End Sub

    Private Sub DBChangesOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DBChangesOnlyToolStripMenuItem.Click

        WF_rebase.rebaseBranch("feature", "DEV", Globals.deriveHotfixBranch("DEV"), False, False, True)

    End Sub

    Private Sub ApexChangesOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApexChangesOnlyToolStripMenuItem.Click
        WF_rebase.rebaseBranch("feature", "DEV", Globals.deriveHotfixBranch("DEV"), False, True, False)
    End Sub

    Private Sub ChooseVMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChooseVMToolStripMenuItem.Click
        Dim vmChoice As String = Nothing

        Dim vmList As Collection = New Collection
        vmList.Add(NO_VM, NO_VM) 'Initialise the list
        WF_virtual_box.getVMs(vmList, "vms")


        vmChoice = ChoiceDialog.Ask("Please choose a new Virtual Machine from those available." &
                                    Environment.NewLine &
                                    Environment.NewLine & "GitPatcher will create and restore snaphots on the chosen Virtual Machine." &
                                    Environment.NewLine & "The current Virtual Machine is " & My.Settings.VBoxName, vmList, My.Settings.VBoxName, "Choose Virtual Machine", False, True)
        My.Settings.VBoxName = vmChoice

        VBoxNameToolStripMenuItem.Text = My.Settings.VBoxName

        displayVBoxName()


    End Sub

    Private Sub VBoxNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VBoxNameToolStripMenuItem.Click
        If VBoxNameToolStripMenuItem.Checked Then
            My.Settings.VBoxName = NO_VM

            displayVBoxName()

        End If
    End Sub

    Private Sub RestoreStateVMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreStateVMToolStripMenuItem.Click
        WF_virtual_box.revertVM("Reverting", True, "desired")
    End Sub

    Private Sub SaveVMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveVMToolStripMenuItem.Click
        WF_virtual_box.controlVM("savestate")
        displayVBoxName()
    End Sub

    Private Sub StartVMNormalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartVMNormalToolStripMenuItem.Click
        My.Settings.startvmType = "gui"
        WF_virtual_box.startVM(My.Settings.startvmType)
        displayVBoxName()
    End Sub

    Private Sub StartVMHeadleassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartVMHeadleassToolStripMenuItem.Click
        My.Settings.startvmType = "headless"
        WF_virtual_box.startVM(My.Settings.startvmType)
        displayVBoxName()
    End Sub

    Private Sub StartVMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartVMToolStripMenuItem.Click
        WF_virtual_box.startVM(My.Settings.startvmType)
        displayVBoxName()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ReleaseToVMMenuItem.Click
        WF_release.releaseTo("VM")
    End Sub

    Private Sub ExportDataMenuItem_Click(sender As Object, e As EventArgs)
        WF_rebase.exportData()
    End Sub

    Private Sub FullRebaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullRebaseToolStripMenuItem.Click
        WF_rebase.rebaseBranch("feature", "DEV", Globals.deriveHotfixBranch("DEV"), False, True, True)
    End Sub

    Private Sub SinglePageImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SinglePageImportToolStripMenuItem.Click

        WF_Apex.ApexRestoreSinglePage()
    End Sub

    Private Sub FeaturePatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FeaturePatchToolStripMenuItem.Click
        WF_createPatch.createPatchProcess("feature", "DEV", Globals.deriveHotfixBranch("DEV"))
    End Sub

    Private Sub VersionPatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionPatchToolStripMenuItem.Click
        WF_createPatch.createVersionPatch("version", "DEV", Globals.deriveHotfixBranch("DEV"))
    End Sub

    Private Sub LogViewerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogViewerToolStripMenuItem.Click
        LogViewer.Show()
    End Sub

    Private Sub TestSQLclToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestSQLclToolStripMenuItem.Click
        Try
            Host.executeSQLplus(Globals.getRepoPath, Main.get_connect_string(Globals.getSchema, Globals.getTNS, Globals.getDATASOURCE), False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExportDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportDataToolStripMenuItem.Click
        WF_rebase.exportData()
    End Sub
End Class
