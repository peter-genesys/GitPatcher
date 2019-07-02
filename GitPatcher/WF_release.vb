Friend Class WF_release
    Shared Sub releaseTo(ByVal iTargetDB As String, ByVal ireleaseFromBranch As String, ByVal iBranchType As String, ByVal iPull As Boolean, Optional ByVal iInstallStatus As String = "Unapplied")


        Dim lcurrentDB As String = Globals.getDB()

        'Dim releaseFromBranch As String = Globals.deriveHotfixBranch(iTargetDB)

        Dim releasing As ProgressDialogue = New ProgressDialogue("Release to " & iTargetDB)

        releasing.MdiParent = GitPatcher

        'CHANGE-DB-TARGET
        releasing.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)
        'SWITCH-BRANCH
        releasing.addStep("Switch to " & ireleaseFromBranch & " branch", Globals.currentLongBranch() <> ireleaseFromBranch)
        'PULL-BRANCH
        releasing.addStep("Pull from Origin to " & ireleaseFromBranch & " branch", iPull)
        'CHECK-OUT-TAG
        releasing.addStep("Choose a tag to release from and checkout the tag", False)

        'RESTORE-VM
        releasing.addStep("Restore to a clean VM snapshot", True, "GitPatcher will restore your VM to a clean VM snapshot." &
                         Environment.NewLine & "Before releasing patches to the VM, restore to a clean VM snapshot.", iTargetDB = "VM")
        'PATCH-RUNNER
        releasing.addStep("Use PatchRunner to run " & iInstallStatus & " " & iBranchType & " Patches", True, "")

        'IMPORT-APPS
        releasing.addStep("Import any queued apps from branch " & ireleaseFromBranch, True, "Any Apex Apps that were included in a patch, must be reinstalled now. ")

        'SMOKE-TEST
        releasing.addStep("Smoke Test", True, "Perform a quick test to verify the patched system is working in " & iTargetDB)

        'CLEAN-VM-SNAPSHOT
        releasing.addStep("Clean VM Snapshot", True, "Create a clean snapshot of your current VM state, to use as your next restore point.", iTargetDB = "VM")

        'RESET-DB-TARGET
        releasing.addStep("Reset current DB to : " & lcurrentDB, lcurrentDB <> iTargetDB)
        releasing.Show()



        Do Until releasing.isStarted
            Common.Wait(1000)
        Loop
        Try
            'CHANGE-DB-TARGET
            If releasing.toDoNextStep() Then
                'Change current DB to release DB
                Globals.setDB(iTargetDB.ToUpper)
                'OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

            End If

            'SWITCH-BRANCH
            If releasing.toDoNextStep() Then
                If ireleaseFromBranch = "release" Then
                    Dim releaseBranches As Collection = GitOp.getReleaseList(Globals.currentAppCode)

                    ireleaseFromBranch = ChoiceDialog.Ask("Identify the release branch that contains the required releases." & Environment.NewLine &
                                                          "Please choose the latest Major, Minor or Patch Version Release branch.",
                                                          releaseBranches, "", "Choose Release branch", False, False, False, releaseBranches.Count - 1)

                End If

                'Switch to develop branch
                GitOp.SwitchBranch(ireleaseFromBranch)

            End If

            'PULL-BRANCH
            If releasing.toDoNextStep() Then
                'Pull from origin/develop
                GitOp.pullBranch(ireleaseFromBranch)

            End If

            'CHECK-OUT-TAG
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

            'RESTORE-VM
            If releasing.toDoNextStep() Then
                'Revert VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Please create a snapshot of your current VM state, and then restore to a clean VM snapshot.", MsgBoxStyle.Exclamation, "Revert VM")
                Else
                    WF_virtual_box.revertVM("Reverting", True, "clean")
                End If


            End If


            'PATCH-RUNNER
            Dim l_no_patches As Boolean = True
            If releasing.toDoNextStep() Then
                'Use PatchRunner to run  Uninstalled/Unapplied Patches

                Dim GitPatcherChild As PatchRunner = New PatchRunner(l_no_patches, iInstallStatus, iBranchType, False)

            End If

            'IMPORT-APPS
            Dim l_no_queued_apps As Boolean = True
            If releasing.toDoNextStep() Then
                'Install queued Apex Apps.
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")

            End If

            'SMOKE-TEST
            If releasing.toDoNextStep() Then
                'Smoke Test 
                MsgBox("Perform a quick test to verify the patched system is working in " & iTargetDB, MsgBoxStyle.Information, "Smoke Test")

            End If

            'CLEAN-VM-SNAPSHOT
            If releasing.toDoNextStep() Then
                'Snapshot VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Create a clean snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")
                Else
                    WF_virtual_box.takeSnapshot(PatchRunner.GetlastSuccessfulPatch & "-clean")
                End If

            End If

            'RESET-DB-TARGET
            If releasing.toDoNextStep() Then
                'Reset current DB  
                Globals.setDB(lcurrentDB.ToUpper)
                'OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

            End If

            'Done
            releasing.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            releasing.setToCompleted()
            releasing.Close()
        End Try

    End Sub



    Shared Sub createReleaseProcess(ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String, iTargetDB As String)

        Dim lcurrentDB As String = Globals.getDB

        'TODO add code to read tags or releases ? on the release branch to determine last semantic release id

        Dim l_app_version = InputBox("Please confirm new semantic release id for " & Globals.currentAppCode & "", "New " & Globals.getAppName & " Version", Globals.currentAppCode & "-1.0.0")
        If String.IsNullOrEmpty(l_app_version) Then
            MsgBox("User Cancelled Operation")
            Return
        End If


        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version

        Dim newBranch As String = "release"

        If Globals.getAppInFeature() = "Y" Then
            newBranch = newBranch & "/" & Globals.currentAppCode
        End If

        newBranch = newBranch & "/" & l_app_version

        Dim currentBranch As String = GitOp.CurrentFriendlyBranch()

        Dim createPatchSetProgress As ProgressDialogue = New ProgressDialogue("Create Patch Release", "Create formal release of patches.") '("Create DB " & iCreatePatchType)
        createPatchSetProgress.MdiParent = GitPatcher

        'SET DB TARGET
        createPatchSetProgress.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        'SWITCH TO MASTER
        createPatchSetProgress.addStep("Switch to master branch", currentBranch <> "master")

        'PULL MASTER
        createPatchSetProgress.addStep("Pull master branch", True, "Ensure master branch is upto date.")

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

        Try

            'SET DB TARGET
            If createPatchSetProgress.toDoNextStep() Then
                'Change current DB to release DB
                Globals.setDB(iTargetDB.ToUpper)

            End If

            'SWITCH TO MASTER
            If createPatchSetProgress.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch("master")

            End If

            'PULL MASTER
            If createPatchSetProgress.toDoNextStep() Then
                'Pull from origin/master
                GitOp.pullBranch("master")

            End If

            'SWITCH TO RELEASE BASE
            If createPatchSetProgress.toDoNextStep() Then
                'Switch to last release branch
                Tortoise.Switch(Globals.getRepoPath)
            End If


            'PULL RELEASE BASE
            If createPatchSetProgress.toDoNextStep() Then
                'Pull 
                GitOp.pullBranch("") 'LGIT does not use the branch param. BGIT does.
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
                Dim l_tag_seq As Integer
                'Create, edit And test collection
                Dim Wizard As New CreateRelease("QHIDS", l_app_version, "Minor", iCreatePatchType, iFindPatchTypes, iFindPatchFilters, iPrereqPatchTypes)

            End If


            'PUSH NEW RELEASE
            If createPatchSetProgress.toDoNextStep() Then
                'Push release to origin with tags
                GitOp.pushBranch(newBranch)

            End If

            'RELEASE UAT
            If createPatchSetProgress.toDoNextStep() Then
                'Release to UAT
                WF_release.releaseTo("UAT", "release", "", True, "Uninstalled")
            End If

            'RELEASE PROD
            If createPatchSetProgress.toDoNextStep() Then
                'Release to PROD
                WF_release.releaseTo("PROD", "release", "", True)
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
        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            createPatchSetProgress.setToCompleted()
            createPatchSetProgress.Close()
        End Try

    End Sub


End Class
