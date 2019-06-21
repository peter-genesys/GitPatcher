Public Class WH_versions

    'CreateVersionBranch

    Shared Sub newMajorMinorVersionRelease(ByVal iVersionType As String, iTargetDB As String)

        Dim currentBranch As String = GitOp.CurrentBranch()
        Dim lcurrentDB As String = Globals.getDB
        Dim l_app_version As String = Nothing
        Dim newBranch As String = "release"


        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version


        Dim MajorMinorVersion As ProgressDialogue = New ProgressDialogue("Create " & iVersionType & " Version Release", "Create formal set of patches for a " & iVersionType & " Version Release")
        MajorMinorVersion.MdiParent = GitPatcher

        '-------------------
        'PREPARE BRANCH
        '-------------------


        'SWITCH-TO-MASTER
        MajorMinorVersion.addStep("Switch to master branch", currentBranch <> "master")

        'PULL-MASTER
        MajorMinorVersion.addStep("Pull master branch", True, "Ensure master branch is upto date.")

        'SWITCH-TO-LAST-RELEASE
        MajorMinorVersion.addStep("Switch to last release branch", True,
                                  "Please choose the latest release branch")
        'TGIT - Let user choose any branch. Or could create a dialog with a list of release/ branches.

        'PULL-LAST-RELEASE
        MajorMinorVersion.addStep("Pull the last release branch", True, "Ensure last release branch is upto date.")
        'LGIT - automatic

        'DERIVE-NEXT-MAJOR/MINOR-VERSION 
        MajorMinorVersion.addStep("Derive or Ask for a version number", True, "Derive or Ask for a version number")
        'LGIT - automatic."

        'BRANCH-TO-NEW-RELEASE
        MajorMinorVersion.addStep("Create and Switch to new release Branch: " & newBranch, True, "")
        'LGIT - automatic.

        'MERGE(NOFF)-FROM-MASTER-CHOOSE-SHA
        MajorMinorVersion.addStep("Merge some commit from the master branch history", True,
                                  "Please choose the HEAD or some other commit on the master branch that includes all patches to be included in the version release.")
        'TGIT - Let user choose any commit, but should be a commit from the master branch, and usually a merge commit of a feature branch.

        'PUSH-NEW-RELEASE
        MajorMinorVersion.addStep("Push the new release Branch", True, "Ensure new release Branch exists, before building patch, so that other developers can see.")


        '-------------------
        'CREATE PATCH 
        '-------------------

        'SET DB TARGET
        MajorMinorVersion.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        'CREATE RELEASE PATCH WIZARD
        MajorMinorVersion.addStep("Build " & iVersionType & " Version Release", True, "Select Patches" &
                                  Chr(10) & "Build " & iVersionType & " Version Release" &
                                  Chr(10) & "Test against " & iTargetDB &
                                  Chr(10) & "Commit to GIT")


        '-------------------
        'MERGE RELEASE BRANCH INTO MASTER
        '-------------------

        ''SWITCH-TO-RELEASES
        ''PULL
        ''MERGE(FF)-RELEASE/REL-X.X
        ''PUSH

        'SWITCH-TO-MASTER
        MajorMinorVersion.addStep("Switch to master branch", True)

        'PULL
        MajorMinorVersion.addStep("Pull master branch", True, "Ensure master branch is upto date.")

        'MERGE(NOFF)-NEW-RELEASE
        MajorMinorVersion.addStep("Merge new release", True, "Merge new Release to master, no-fastforward.")

        'PUSH
        MajorMinorVersion.addStep("Push master branch", True, "Push the master branch.  This effectively publishes the Release to master branch.")

        'SYNCH
        MajorMinorVersion.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/master")


        MajorMinorVersion.Show()

        Do Until MajorMinorVersion.isStarted
            Common.Wait(1000)
        Loop
        Try

            '-------------------
            'PREPARE BRANCH
            '-------------------

            'SWITCH-TO-MASTER
            If MajorMinorVersion.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch("master")
            End If

            'PULL-MASTER
            If MajorMinorVersion.toDoNextStep() Then
                'Pull from origin/master
                GitOp.pullBranch("master")
            End If

            'SWITCH-TO-LAST-RELEASE
            If MajorMinorVersion.toDoNextStep() Then
                'Switch to last release branch
                Tortoise.Switch(Globals.getRepoPath)
            End If

            'PULL-LAST-RELEASE
            If MajorMinorVersion.toDoNextStep() Then
                'Pull 
                GitOp.pullBranch("") 'LGIT does not use the branch param. BGIT does.
            End If

            'DERIVE-NEXT-MAJOR/MINOR-VERSION 
            If MajorMinorVersion.toDoNextStep() Then
                l_app_version = InputBox("Please confirm new semantic release id for " & Globals.currentAppCode & "", "New " & Globals.getAppName & " Version", Globals.currentAppCode & "-1.0.0")
                If String.IsNullOrEmpty(l_app_version) Then
                    Throw New System.Exception("User Cancelled Operation")
                End If

                If Globals.getAppInFeature() = "Y" Then
                    newBranch = newBranch & "/" & Globals.currentAppCode
                End If

                newBranch = newBranch & "/" & l_app_version

            End If

            'BRANCH-TO-NEW-RELEASE
            If MajorMinorVersion.toDoNextStep() Then
                'Create and Switch to new release branch
                GitOp.createAndSwitchBranch(newBranch)
            End If

            'MERGE(NOFF)-FROM-MASTER-CHOOSE-SHA
            If MajorMinorVersion.toDoNextStep() Then
                'Merge from master branch
                Tortoise.Merge(Globals.getRepoPath)
            End If

            'PUSH-NEW-RELEASE
            If MajorMinorVersion.toDoNextStep() Then
                'Push release to origin with tags
                GitOp.pushBranch(newBranch)
            End If

            '-------------------
            'CREATE PATCH 
            '-------------------

            'SET DB TARGET
            If MajorMinorVersion.toDoNextStep() Then
                'Change current DB to release DB
                Globals.setDB(iTargetDB.ToUpper)

            End If

            'CREATE RELEASE PATCH WIZARD
            If MajorMinorVersion.toDoNextStep() Then
                'Create, edit And test collection
                Dim Wizard As New CreateRelease(l_app_version, "release", "feature", "feature", "release,feature,version,hotfix,ALL", "")

            End If

            'SWITCH-TO-MASTER
            If MajorMinorVersion.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch("master")
            End If

            'PULL
            If MajorMinorVersion.toDoNextStep() Then
                'Pull from origin/master
                GitOp.pullBranch("master")
            End If

            'MERGE(NOFF)-NEW-RELEASE
            If MajorMinorVersion.toDoNextStep() Then
                'Merge from new release branch
                GitOp.Merge(newBranch)
            End If

            'PUSH MASTER
            If MajorMinorVersion.toDoNextStep() Then
                GitOp.pushBranch("master")
            End If

            'SYNCH MASTER
            If MajorMinorVersion.toDoNextStep() Then
                Tortoise.Sync(Globals.getRepoPath)
            End If

            'Done
            MajorMinorVersion.toDoNextStep()



        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            MajorMinorVersion.setToCompleted()
            MajorMinorVersion.Close()
        End Try






    End Sub

    Shared Sub newPatchVersionRelease()



    End Sub

    Shared Sub newFullVersionRelease()



    End Sub


    Shared Sub newVersionRelease(ByVal iVersionType As String, iTargetDB As String)
        Logger.Note("iVersionType", iVersionType)
        If iVersionType = "Major" Or iVersionType = "Minor" Then
            newMajorMinorVersionRelease(iVersionType, iTargetDB)
        ElseIf iVersionType = "Patch" Then
            newPatchVersionRelease()
        ElseIf iVersionType = "Full" Then
            newFullVersionRelease()
        Else
            Throw New Exception("Unknown Version Type: " & iVersionType)
        End If


    End Sub



    Shared Sub createReleaseBranch(ByVal iVersionType As String, ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String, iTargetDB As String)


        'STEPS

        'CHOOSE VERSION TYPE - MAJOR, MINOR, PATCH





        'PATCH VERSION RELEASE
        '-------------------

        'SWITCH-TO-RELEASES

        'PULL

        'CHOOSE MAJOR / MINOR RELEASE RELEASE/REL-X.X

        'SWITCH TO RELEASE/REL-X.X

        'PULL

        'MERGE(FF)-RELEASES

        'CREATE PATCH-VERSION RELEASE PATCH 
        'Installs all hotfixes since last release
        'Run release against VM  - to verify install


        'RELEASE-TO-PAT
        'Run release against PAT - deploy - all hotfixes must be rerunnable! Because they will have already been installed in PAT.

        'SWITCH-TO-RELEASES

        'PULL

        'MERGE(FF)-RELEASE/REL-X.X

        'SWITCH-TO-MASTER

        'PULL

        'MERGE(NOFF)-RELEASES

























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

        Dim currentBranch As String = GitOp.CurrentBranch()

        Dim createPatchSetProgress As ProgressDialogue = New ProgressDialogue("Create Patch Release", "Create formal release of patches.") '("Create DB " & iCreatePatchType)
        createPatchSetProgress.MdiParent = GitPatcher

        'SET DB TARGET
        createPatchSetProgress.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        'SWITCH TO MASTER
        createPatchSetProgress.addStep("Switch to master branch", True, currentBranch <> "master")

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
            Common.Wait(1000)
        Loop


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

            'Create, edit And test collection
            Dim Wizard As New CreateRelease(l_app_version, iCreatePatchType, iFindPatchTypes, iFindPatchFilters, iPrereqPatchTypes, iSupPatchTypes)

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
