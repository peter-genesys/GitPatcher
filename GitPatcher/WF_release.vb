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
        Dim l_app_version = InputBox("Please enter Patchset Code for " & Globals.currentAppCode & "", "New " & Globals.getAppName & " Version")

        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version

        Dim newBranch As String = "release"

        If Globals.getAppInFeature() = "Y" Then
            newBranch = newBranch & "/" & Globals.currentAppCode
        End If

        newBranch = newBranch & "/" & l_app_version

        Dim currentBranch As String = GitOp.CurrentBranch()

        Dim createPatchSetProgress As ProgressDialogue = New ProgressDialogue("Create DB " & iCreatePatchType)
        createPatchSetProgress.MdiParent = GitPatcher

        createPatchSetProgress.addStep("Switch to develop branch")
        createPatchSetProgress.addStep("Pull from origin/develop")
        createPatchSetProgress.addStep("Create and Switch to release Branch: " & newBranch)

        createPatchSetProgress.addStep("Change current DB to : " & iTargetDB)

        createPatchSetProgress.addStep("Create, edit and test " & iCreatePatchType)
        createPatchSetProgress.addStep("Bump Apex version to " & l_app_version)
        createPatchSetProgress.addStep("Commit Apex version " & l_app_version)
        createPatchSetProgress.addStep("Tag this release as " & l_app_version)
        createPatchSetProgress.addStep("Push to origin/" & newBranch)

        createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to develop", False)
        createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to test", True)
        createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to uat", False)
        createPatchSetProgress.addStep("Merge Patchset " & l_app_version & " to master", False)

        createPatchSetProgress.addStep("Release to ISDEVL", False)
        createPatchSetProgress.addStep("Release to ISTEST", True)
        createPatchSetProgress.addStep("Release to ISUAT", False)
        createPatchSetProgress.addStep("Release to ISPROD", False)
        createPatchSetProgress.addStep("Revert current DB to : " & lcurrentDB)
        'createPatchSetProgress.addStep("Export Patchset")
        'Import

        createPatchSetProgress.Show()

        Do Until createPatchSetProgress.isStarted
            Common.wait(1000)
        Loop

        If createPatchSetProgress.toDoNextStep() Then
            'Switch to develop branch
            GitOp.SwitchBranch("develop")


        End If
        If createPatchSetProgress.toDoNextStep() Then
            'Pull from origin/develop
            GitOp.pullBranch("develop")
  

        End If
        If createPatchSetProgress.toDoNextStep() Then
            'Create and Switch to new collection branch
            GitOp.createAndSwitchBranch(newBranch)



        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Change current DB to release DB
            Globals.setDB(iTargetDB.ToUpper)

        End If

        If createPatchSetProgress.toDoNextStep() Then

            'Create, edit And test collection
            Dim Wizard As New CreateRelease(l_app_version, iCreatePatchType, iFindPatchTypes, iFindPatchFilters, iPrereqPatchTypes, iSupPatchTypes)
            Wizard.ShowDialog() 'WAITING HERE!!


        End If
        If createPatchSetProgress.toDoNextStep() Then
            'Bump Apex version 
            CreateRelease.bumpApexVersion(l_app_version)

        End If
        If createPatchSetProgress.toDoNextStep() Then
            'Commit Apex version 
            Tortoise.Commit(Globals.getRepoPath, "Bump Apex " & Globals.currentApex & " to " & l_app_version)


        End If
        If createPatchSetProgress.toDoNextStep() Then
            'Tag this commit
            GitBash.TagSimple(Globals.getRepoPath, l_app_version)

        End If
        If createPatchSetProgress.toDoNextStep() Then
            'Push release to origin with tags
            GitOp.pushBranch(newBranch) 'previous call to GitBash.Push sent tags and it waited (synchronously)
            'GitBash.Push(Globals.getRepoPath, "origin", newBranch, True)

        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Merge Patchset to develop 
            WF_mergeAndPush.mergeAndPushBranch("patchset", "develop")

        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Merge Patchset to test 
            WF_mergeAndPush.mergeAndPushBranch("patchset", "test")

        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Merge Patchset to uat 
            WF_mergeAndPush.mergeAndPushBranch("patchset", "uat")

        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Merge Patchset to master 
            WF_mergeAndPush.mergeAndPushBranch("patchset", "master")

        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Release to ISDEVL
            WF_release.releaseTo("DEV")
        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Release to ISTEST
            WF_release.releaseTo("TEST")
        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Release to ISTEST
            WF_release.releaseTo("UAT")
        End If

        If createPatchSetProgress.toDoNextStep() Then
            'Release to ISTEST
            WF_release.releaseTo("PROD")
        End If


        If createPatchSetProgress.toDoNextStep() Then
            'Revert current DB  
            Globals.setDB(lcurrentDB.ToUpper)

        End If

        'Done
        createPatchSetProgress.toDoNextStep()

    End Sub


End Class
