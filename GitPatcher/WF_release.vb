Friend Class WF_release
    Shared Sub releaseTo(iTargetDB As String, Optional ByVal iBranchType As String = "")

        Dim InstallStatus As String = Nothing
        If iTargetDB = "DEV" Then
            InstallStatus = "Uninstalled"
        Else
            InstallStatus = "Unapplied"
        End If



        Dim lcurrentDB As String = Globals.getDB()

            Dim currentBranch As String = GitOp.CurrentBranch()

            Dim releaseFromBranch As String = Globals.deriveHotfixBranch(iTargetDB)

            Dim releasing As ProgressDialogue = New ProgressDialogue("Release to " & iTargetDB)

            releasing.MdiParent = GitPatcher
            releasing.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)
        releasing.addStep("Switch to " & releaseFromBranch & " branch", currentBranch <> releaseFromBranch)
        releasing.addStep("Pull from Origin to " & releaseFromBranch & " branch", True)

        releasing.addStep("Choose a tag to release from and checkout the tag", False)

        'REVERT-VM
        releasing.addStep("Restore to a clean VM snapshot", True, "GitPatcher will restore your VM to a clean VM snapshot." &
                         Environment.NewLine & "Before releasing patches to the VM, restore to a clean VM snapshot.", iTargetDB = "VM")
        'PATCH-RUNNER

        releasing.addStep("Use PatchRunner to run " & InstallStatus & " Patches", True, "")

        releasing.addStep("Import any queued apps: " & releaseFromBranch, True, "Any Apex Apps that were included in a patch, must be reinstalled now. ")

            'releasing.addStep("Import Apex", True, "Using the Apex2Git") '"Using the Apex Import workflow")
            releasing.addStep("Smoke Test", True, "Perform a quick test to verify the patched system is working in " & iTargetDB)

            releasing.addStep("Clean VM Snapshot", True, "Create a clean snapshot of your current VM state, to use as your next restore point.", iTargetDB = "VM")


            releasing.addStep("Reset current DB to : " & lcurrentDB, lcurrentDB <> iTargetDB)
            releasing.Show()



            Do Until releasing.isStarted
                Common.wait(1000)
            Loop

            If releasing.toDoNextStep() Then
                'Change current DB to release DB
                Globals.setDB(iTargetDB.ToUpper)
                OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

            End If

            If releasing.toDoNextStep() Then
                'Switch to develop branch
                GitOp.SwitchBranch(releaseFromBranch)

            End If
            If releasing.toDoNextStep() Then
                'Pull from origin/develop
                GitOp.pullBranch(releaseFromBranch)

            End If

            If releasing.toDoNextStep() Then
                'Choose a tag to import from
                Dim tagnames As Collection = New Collection
                tagnames.Add("HEAD")
                tagnames = GitOp.getTagNameList(tagnames, Globals.currentBranch)
                tagnames = GitOp.getTagNameList(tagnames, Globals.getAppCode)


                Dim PatchTag As String = Nothing
                PatchTag = ChoiceDialog.Ask("Please choose a tag for patch installs", tagnames, "HEAD", "Choose tag")

                GitOp.SwitchTagName(PatchTag)

            End If

            'REVERT-VM
            If releasing.toDoNextStep() Then
                'Revert VM
                If My.Settings.VBoxName = "No VM" Then
                MsgBox("Please create a snapshot of your current VM state, and then restore to a clean VM snapshot.", MsgBoxStyle.Exclamation, "Revert VM")
            Else
                    WF_virtual_box.revertVM("Reverting", True, "clean")
                End If


            End If



            If releasing.toDoNextStep() Then
            'Use PatchRunner to run  Uninstalled/Unapplied Patches
            Dim GitPatcherChild As PatchRunner = New PatchRunner(InstallStatus, iBranchType)

        End If

            If releasing.toDoNextStep() Then
                'Install queued Apex Apps.
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller("Queued")

        End If

        If releasing.toDoNextStep() Then
                'Smoke Test 
                MsgBox("Perform a quick test to verify the patched system is working in " & iTargetDB, MsgBoxStyle.Information, "Smoke Test")

            End If

            If releasing.toDoNextStep() Then
                'Snapshot VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Create a clean snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")
                Else
                    WF_virtual_box.takeSnapshot(PatchRunner.GetlastSuccessfulPatch & "-clean")
                End If



            End If


            If releasing.toDoNextStep() Then
                'Reset current DB  
                Globals.setDB(lcurrentDB.ToUpper)
                OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

            End If

            'Finish
            releasing.toDoNextStep()
    End Sub



    Shared Sub createReleaseProcess(ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String, iTargetDB As String)

        Dim lcurrentDB As String = Globals.getDB

        'TODO add code to read tags on the release branch to determine last semantic release id

        Dim l_app_version = InputBox("Please confirm new semantic release id for " & Globals.currentAppCode & "", "New " & Globals.getAppName & " Version", "1.0.0")
        If String.IsNullOrEmpty(l_app_version) Then
            MsgBox("User Cancelled Operation")
            Return
        End If


        l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version

        Dim newBranch As String = "release"

        If Globals.getAppInFeature() = "Y" Then
            newBranch = newBranch & "/" & Globals.currentAppCode
        End If

        newBranch = newBranch & "/" & l_app_version

        Dim currentBranch As String = GitOp.CurrentBranch()

        Dim createPatchSetProgress As ProgressDialogue = New ProgressDialogue("Create Patch Release", "Create formal release of patches.") '("Create DB " & iCreatePatchType)
        createPatchSetProgress.MdiParent = GitPatcher

        'SET DB TARGET
        createPatchSetProgress.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        'SWITCH TO RELEASE BASE
        'I could list the potential release branches in the notes.
        createPatchSetProgress.addStep("Switch to last release branch", True,
                                       "TGIT - Let user choose any branch. Or could create a dialog with a list of release/ branches.")
        'PULL RELEASE BASE
        createPatchSetProgress.addStep("Pull the release branch", True,
                                       "LGIT - automatic.")
        'CREATE NEW RELEASE
        createPatchSetProgress.addStep("Create and Switch to new release Branch: " & newBranch, True,
                                       "LGIT - automatic.")
        'Could also use the TGIT Create Branch dialog - but this is more like a power user interface.

        'MERGE PATCHES FROM MASTER
        createPatchSetProgress.addStep("Merge some commit from the master branch history", True,
                                       "TGIT - Let user choose any commit, but should be a commit from the master branch, and usually a merge commit of a feature branch.")

        'CREATE RELEASE PATCH
        createPatchSetProgress.addStep("Create, edit and test " & iCreatePatchType)

        'PUSH NEW RELEASE
        createPatchSetProgress.addStep("Push to origin/" & newBranch)

        'RELEASE UAT
        createPatchSetProgress.addStep("Release to UAT", False)

        'RELEASE PROD
        createPatchSetProgress.addStep("Release to PROD", False)

        'RESET CURRENT DB
        createPatchSetProgress.addStep("Reset current DB to : " & lcurrentDB, True)


        'createPatchSetProgress.addStep("Bump Apex version to " & l_app_version)
        'createPatchSetProgress.addStep("Commit Apex version " & l_app_version)
        'createPatchSetProgress.addStep("Tag this release as " & l_app_version)


        'createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to develop", False)
        'createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to test", True)
        'createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to uat", False)
        'createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to master", False)

        'createPatchSetProgress.addStep("Release to ISDEVL", False)
        'createPatchSetProgress.addStep("Release to ISTEST", True)

        'createPatchSetProgress.addStep("Export Patchset")
        'Import

        createPatchSetProgress.Show()

        Do Until createPatchSetProgress.isStarted
            Common.wait(1000)
        Loop


        'SET DB TARGET
        If createPatchSetProgress.toDoNextStep() Then
            'Change current DB to release DB
            Globals.setDB(iTargetDB.ToUpper)

        End If


        'SWITCH TO RELEASE BASE
        If createPatchSetProgress.toDoNextStep() Then
            'Switch to last release branch
            Tortoise.Switch(Globals.getRepoPath)
        End If


        'PULL RELEASE BASE
        If createPatchSetProgress.toDoNextStep() Then
            'Pull 
            GitOp.pullBranch(Globals.currentLongBranch()) 'LGIT does not use the branch param. BGIT does.
        End If

        'CREATE NEW RELEASE
        If createPatchSetProgress.toDoNextStep() Then
            'Create and Switch to new release branch
            GitOp.createAndSwitchBranch(newBranch)

        End If

        'MERGE PATCHES FROM MASTER
        If createPatchSetProgress.toDoNextStep() Then
            'Merge from master branch
            Tortoise.Merge(Globals.getRepoPath)
        End If

        'CREATE RELEASE PATCH
        If createPatchSetProgress.toDoNextStep() Then

            'Create, edit And test collection
            Dim Wizard As New CreateRelease(l_app_version, iCreatePatchType, iFindPatchTypes, iFindPatchFilters, iPrereqPatchTypes, iSupPatchTypes)
            Wizard.ShowDialog() 'WAITING HERE!!


        End If


        'PUSH NEW RELEASE
        If createPatchSetProgress.toDoNextStep() Then
            'Push release to origin with tags
            GitOp.pushBranch(newBranch)

        End If

        'RELEASE UAT
        If createPatchSetProgress.toDoNextStep() Then
            'Release to UAT
            WF_release.releaseTo("UAT")
        End If

        'RELEASE PROD
        If createPatchSetProgress.toDoNextStep() Then
            'Release to PROD
            WF_release.releaseTo("PROD")
        End If



        'RESET CURRENT DB
        If createPatchSetProgress.toDoNextStep() Then
            'Revert current DB  
            Globals.setDB(lcurrentDB.ToUpper)

        End If

        'Done
        createPatchSetProgress.toDoNextStep()


        'If createPatchSetProgress.toDoNextStep() Then
        '    'Bump Apex version 
        '    CreateRelease.bumpApexVersion(l_app_version)

        'End If
        'If createPatchSetProgress.toDoNextStep() Then
        '    'Commit Apex version 
        '    Tortoise.Commit(Globals.getRepoPath, "Bump Apex " & Globals.currentApex & " to " & l_app_version)


        'End If
        'If createPatchSetProgress.toDoNextStep() Then
        '    'Tag this commit
        '    GitBash.TagSimple(Globals.getRepoPath, l_app_version)

        'End If


        'If createPatchSetProgress.toDoNextStep() Then
        '    'Merge Patchset to develop 
        '    WF_mergeAndPush.mergeAndPushBranch("patchset", "develop")

        'End If

        'If createPatchSetProgress.toDoNextStep() Then
        '    'Merge Patchset to test 
        '    WF_mergeAndPush.mergeAndPushBranch("patchset", "test")

        'End If

        'If createPatchSetProgress.toDoNextStep() Then
        '    'Merge Patchset to uat 
        '    WF_mergeAndPush.mergeAndPushBranch("patchset", "uat")

        'End If

        'If createPatchSetProgress.toDoNextStep() Then
        '    'Merge Patchset to master 
        '    WF_mergeAndPush.mergeAndPushBranch("patchset", "master")

        'End If

        'If createPatchSetProgress.toDoNextStep() Then
        '    'Release to ISDEVL
        '    WF_release.releaseTo("DEV")
        'End If

        'If createPatchSetProgress.toDoNextStep() Then
        '    'Release to ISTEST
        '    WF_release.releaseTo("TEST")
        'End If



    End Sub


End Class
