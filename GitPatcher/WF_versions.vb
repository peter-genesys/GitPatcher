﻿Public Class WF_versions



    Shared Function isReleaseBuilt(iReleaseBranch, iAppVersion) As Boolean
        Return FileIO.fileExists(Globals.RootPatchDir & iReleaseBranch & "\" & iAppVersion & "\install.sql")
    End Function

    Shared Sub checkAppVersion(ByVal appVersion As String, ByVal appcode As String, ByRef MajorId As Integer, ByRef MinorId As Integer, ByRef PatchId As Integer)

        Logger.Note("checkAppVersion(appVersion,appcode)", appVersion & "," & appcode)

        Dim lNumericOnly As String = Replace(appVersion, appcode & "-", "")
        Dim lMessage As String = "Unable to determine <COMP> from Release Id: " & appVersion & " Format should be APPCODE-MAJ.MIN.PATCH"

        Try
            MajorId = Common.getNthSegment(lNumericOnly, ".", 1)
        Catch ex As Exception
            MsgBox(ex.Message)
            Throw New System.Exception(Replace(lMessage, "<COMP>", "MajorId"))
        End Try

        Try
            MinorId = Common.getNthSegment(lNumericOnly, ".", 2)

        Catch ex As Exception
            MsgBox(ex.Message)
            Throw New System.Exception(Replace(lMessage, "<COMP>", "MinorId"))
        End Try

        Try
            PatchId = Common.getNthSegment(lNumericOnly, ".", 3)

        Catch ex As Exception
            MsgBox(ex.Message)
            Throw New System.Exception(Replace(lMessage, "<COMP>", "PatchId"))
        End Try

        Logger.Note("MajorId", MajorId)
        Logger.Note("MinorId", MinorId)
        Logger.Note("PatchId", PatchId)

    End Sub

    Shared Sub checkReleaseAppVersion(ByVal ReleaseBranch As String, ByVal appcode As String, ByRef MajorId As Integer, ByRef MinorId As Integer, ByRef PatchId As Integer, ByRef AppVersion As String)

        Logger.Note("checkReleaseAppVersion(appVersion,appcode)", ReleaseBranch & "," & appcode)

        AppVersion = Common.getLastSegment(ReleaseBranch, "/")

        checkAppVersion(AppVersion, appcode, MajorId, MinorId, PatchId)

    End Sub


    Shared Function GetNextAppVersion(ByVal lastReleaseBranch As String, ByVal appcode As String, ByVal versionType As String) As String


        Dim lMajorId As Integer
        Dim lMinorId As Integer
        Dim lPatchId As Integer
        Dim lAppVersion As String = Nothing

        checkReleaseAppVersion(lastReleaseBranch, appcode, lMajorId, lMinorId, lPatchId, lAppVersion)

        'Increment the version
        If versionType = "Major" Then
            lMajorId = lMajorId + 1
            lMinorId = 0
            lPatchId = 0
        ElseIf versionType = "Minor" Then
            lMinorId = lMinorId + 1
            lPatchId = 0
        ElseIf versionType = "Patch" Then
            lPatchId = lPatchId + 1
        Else
            Throw New System.Exception("Unexpected Version Type : " & versionType)
        End If

        'Confirm new version
        Dim lNewAppVersion As String = Nothing
        lNewAppVersion = InputBox("Please confirm new semantic release id for " & appcode & "", "New " & Globals.getAppName & " Version", appcode & "-" & lMajorId & "." & lMinorId & "." & lPatchId)
        If String.IsNullOrEmpty(lNewAppVersion) Then
            Throw New System.Exception("User Cancelled Operation")
        End If

        Dim lConfirmAppCode As String = Common.getFirstSegment(lNewAppVersion, "-")

        If lConfirmAppCode <> appcode Then
            Throw New System.Exception("Current branch is NOT a release branch for " & lConfirmAppCode & "  Aborting Operation.")
        End If

        Dim lConfirmMajorId As Integer
        Dim lConfirmMinorId As Integer
        Dim lConfirmPatchId As Integer

        checkAppVersion(lNewAppVersion, appcode, lConfirmMajorId, lConfirmMinorId, lConfirmPatchId)

        If lConfirmMajorId > lMajorId Then
            MsgBox("Major Id " & lConfirmMajorId & " is greater than expected.", MsgBoxStyle.Exclamation, "Unexpected Major Id")
        End If
        If lConfirmMajorId < lMajorId Then
            MsgBox("Major Id " & lConfirmMajorId & " is less than expected.", MsgBoxStyle.Exclamation, "Unexpected Major Id")
        End If
        If lConfirmMinorId > lMinorId Then
            MsgBox("Minor Id " & lConfirmMinorId & " is greater than expected.", MsgBoxStyle.Exclamation, "Unexpected Minor Id")
        End If
        If lConfirmMinorId < lMinorId Then
            MsgBox("Minor Id " & lConfirmMinorId & " is less than expected.", MsgBoxStyle.Exclamation, "Unexpected Minor Id")
        End If
        If lConfirmPatchId > lPatchId Then
            MsgBox("Patch Id " & lConfirmPatchId & " is greater than expected.", MsgBoxStyle.Exclamation, "Unexpected Patch Id")
        End If
        If lConfirmPatchId < lPatchId Then
            MsgBox("Patch Id " & lConfirmPatchId & " is less than expected.", MsgBoxStyle.Exclamation, "Unexpected Patch Id")
        End If

        If versionType = "Major" Then
            If lConfirmMinorId > 0 Then
                Throw New System.Exception("A Major Version Release Id must have 0 as the Minor Version Id.  Aborting Operation.")
            End If
            If lConfirmPatchId > 0 Then
                Throw New System.Exception("A Major Version Release Id must have 0 as the Patch Version Id.  Aborting Operation.")
            End If

        ElseIf versionType = "Minor" Then
            If lConfirmMajorId <> lMajorId Then
                Throw New System.Exception("A Minor Version Release Id must have the same Major Version Id as last Release Id.  Aborting Operation.")
            End If
            If lConfirmPatchId > 0 Then
                Throw New System.Exception("A Minor Version Release Id must have 0 as the Patch Version Id.  Aborting Operation.")
            End If

        ElseIf versionType = "Patch" Then
            If lConfirmMinorId <> lMinorId Then
                Throw New System.Exception("A Patch Version Release Id must have the same Minor Version Id as last Release Id.  Aborting Operation.")
            End If

        Else
            Throw New System.Exception("Unexpected Version Type : " & versionType)
        End If

        Return lNewAppVersion

    End Function




    Shared Sub mergeBranchAndPush(ByVal iVersionType As String, ByVal baseBranch As String, ByVal mergeBranch As String, ByVal iFastForward As Boolean)


        Dim currentBranch As String = GitOp.CurrentFriendlyBranch()
        'Dim lcurrentDB As String = Globals.getDB
        'Dim l_app_version As String = Nothing
        'Dim newBranch As String = "release"


        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version


        Dim mergeBranchProgess As ProgressDialogue = New ProgressDialogue("Merge " & iVersionType & " Version Release to " & baseBranch, "Merge " & iVersionType & " Version Release to " & baseBranch)
        mergeBranchProgess.MdiParent = GitPatcher

        'SWITCH-TO-BASE-BRANCH
        mergeBranchProgess.addStep("Switch to " & baseBranch & " branch", currentBranch <> baseBranch)

        'PULL-BASE-BRANCH
        mergeBranchProgess.addStep("Pull " & baseBranch & " branch", True, "Ensure " & baseBranch & " branch is upto date.")

        'MERGE-A-BRANCH-FF
        mergeBranchProgess.addStep("Merge " & mergeBranch & " branch - FastForward-Only", True, "Merge " & mergeBranch & " branch, to " & baseBranch & " branch, fastforward-only.  Simply moves the " & baseBranch & " pointer to the head of the " & mergeBranch, iFastForward)

        'MERGE-A-BRANCH-NOFF
        mergeBranchProgess.addStep("Merge " & mergeBranch & " branch - No-FastForward", True, "Merge " & mergeBranch & " branch, to " & baseBranch & " branch, no-fastforward.  Always create a new commit on " & baseBranch & ".", Not iFastForward)

        'PUSH-BASE-BRANCH
        mergeBranchProgess.addStep("Push " & baseBranch & " branch", True,
                                   "Push the " & baseBranch & " branch.  This effectively publishes " & mergeBranch & " branch to " & baseBranch & " branch." & Chr(10) &
                                   "If the push is not successful, the TortoiseGIT Synchronisation dialog will be raised.")

        mergeBranchProgess.Show()

        Do Until mergeBranchProgess.isStarted
            Common.Wait(1000)
        Loop
        Try


            'SWITCH-TO-BASE-BRANCH
            If mergeBranchProgess.toDoNextStep() Then
                'Switch to baseBranch branch
                GitOp.SwitchBranch(baseBranch)
            End If

            'PULL-BASE-BRANCH
            If mergeBranchProgess.toDoNextStep() Then
                'Pull from origin/baseBranch
                GitOp.pullBranch(baseBranch)
            End If

            'MERGE-A-BRANCH-FF
            If mergeBranchProgess.toDoNextStep() Then
                'Merge from new release branch
                GitOp.Merge(mergeBranch, iFastForward)
            End If

            'MERGE-A-BRANCH-NOFF
            If mergeBranchProgess.toDoNextStep() Then
                'Merge from new release branch
                GitOp.Merge(mergeBranch)
            End If

            'PUSH-BASE-BRANCH
            If mergeBranchProgess.toDoNextStep() Then
                GitOp.pushBranch(baseBranch)
            End If

            'Done
            mergeBranchProgess.toDoNextStep()



        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            mergeBranchProgess.setToCompleted()
            mergeBranchProgess.Close()
        End Try



    End Sub




    Shared Sub createVersionRelease(ByVal iVersionType As String, ByVal iTargetDB As String)

        Logger.Debug("createVersionRelease")
        Logger.Note("iVersionType", iVersionType)
        Logger.Note("iTargetDB", iTargetDB)

        Dim currentBranch As String = Globals.currentLongBranch()
        Dim lcurrentDB As String = Globals.getDB
        Dim lAppVersion As String = Nothing
        Dim newReleaseBranch As String = "release"


        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version


        Dim VersionRelease As ProgressDialogue = New ProgressDialogue("Create " & iVersionType & " Version Release", "Create formal set of patches for a " & iVersionType & " Version Release")
        VersionRelease.MdiParent = GitPatcher

        '-------------------
        'PREPARE BRANCH - 7 steps
        '-------------------


        'SET DB TARGET
        VersionRelease.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        'IDENTIFY-LAST-RELEASE-BRANCH
        VersionRelease.addStep("Identify the Last Release branch", True, "Identify the release branch.", iVersionType <> "Patch")

        'PULL-LAST-RELEASE-BRANCH
        'VersionRelease.addStep("Pull the Last Release branch", False, "Switch to the Last Release branch and Pull", iVersionType <> "Patch")

        'SUBFLOW:RELEASE-LAST-VERSION
        VersionRelease.addStep("Release versions from last Release branch", True, "Release versions from last Release branch", iVersionType <> "Patch")

        'PULL-MASTER-BRANCH
        VersionRelease.addStep("Pull the master branch", True, "Switch to the master branch and Pull", iVersionType <> "Patch")

        'SWITCH-TO-MASTER-RELEASE-CANDIDATE
        VersionRelease.addStep("Switch to the master release candidate", True,
                                  "Choose a candidate commit on the master branch to create the new release from." &
                                  "Patches and Apex Apps will be released as per this commit.", iVersionType <> "Patch")

        'BRANCH-TO-NEW-RELEASE    'LGIT - automatic.
        VersionRelease.addStep("Branch to new release Branch", True,
                                  "Branch from the candidate commit to the new release.", iVersionType <> "Patch")

        'PUSH-NEW-RELEASE
        VersionRelease.addStep("Push the new release Branch", True, "Ensure new release Branch exists, before building patch, so that other developers can see.", iVersionType <> "Patch")


        '-------------------
        'PATCH-VERSION-RELEASE  - 2 steps
        '-------------------
        'CHOOSE-RELEASE-BRANCH
        VersionRelease.addStep("Choose Patch Version Release Branch", True, "Choose the Patch Version Release to be built.", iVersionType = "Patch")

        'PULL-PATCH-VERSION-RELEASE
        'VersionRelease.addStep("Pull the Patch Version Release branch", True, "Switch to the Patch Version Release branch and Pull", iVersionType = "Patch")

        'SUBFLOW:RELEASE-NEW-VERSION
        VersionRelease.addStep("Release versions from new Release branch", True, "Release versions from new Release branch", iVersionType = "Patch")

        '-------------------
        'CREATE PATCH  - 2 steps
        '-------------------

        'CREATE RELEASE PATCH WIZARD
        VersionRelease.addStep("Build " & iVersionType & " Version Release", True, "Select Patches" &
                                  Chr(10) & "Build " & iVersionType & " Version Release" &
                                  Chr(10) & "Test against " & iTargetDB &
                                  Chr(10) & "Commit to GIT")

        'PUSH-NEW-RELEASE
        VersionRelease.addStep("Push the new release Branch", True, "Ensure other developers can see the new release patch.")



        '-------------------
        'MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
        '-------------------
        VersionRelease.addStep("Merge new release branch to master", True, "Merge new release to master branch, no-fastforward-only")

        'CLEAN-VM-SNAPSHOT
        VersionRelease.addStep("Clean VM Snapshot", True, "Create a clean snapshot of your current VM state, to use as your next restore point.", iTargetDB = "VM")

        VersionRelease.Show()

        Do Until VersionRelease.isStarted
            Common.Wait(1000)
        Loop
        Try

            '-------------------
            'PREPARE BRANCH - 7 steps
            '-------------------

            Dim lastReleaseBranch As String = Nothing
            Dim lAppCode As String = Globals.currentAppCode

            'SET-DB-TARGET
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("SET-DB-TARGET")
                'Change current DB to release DB
                Globals.setDB(iTargetDB.ToUpper)

            End If


            'IDENTIFY-LAST-RELEASE-BRANCH
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("IDENTIFY-LAST-RELEASE-BRANCH")

                Dim appCodeList As Collection = GitOp.getReleaseAppCodeList()

                lAppCode = ChoiceDialog.Ask("Please choose the app to be released (from previously released apps)", appCodeList, lAppCode, "Identify the app", True, False, False)

                Dim releaseBranches As Collection = GitOp.getReleaseList(lAppCode)

                lastReleaseBranch = ChoiceDialog.Ask("Please identify the last release branch for the app: " & lAppCode, releaseBranches, "", "Identify last release branch", False, False, False)

                lAppVersion = GetNextAppVersion(lastReleaseBranch, lAppCode, iVersionType)

                'Currently assuming all release branches include an appCode as 2nd element.
                newReleaseBranch = newReleaseBranch & "/" & lAppCode & "/" & lAppVersion

                'If Globals.getAppInRelease() = "Y" Then
                '    newReleaseBranch = newReleaseBranch & "/" & lAppCode
                'End If
                'newReleaseBranch = newReleaseBranch & "/" & lAppVersion

            End If


            ''PULL-LAST-RELEASE-BRANCH
            'If VersionRelease.toDoNextStep() Then
            '    Logger.Debug("PULL-LAST-RELEASE-BRANCH")
            '    'Switch to master branch
            '    GitOp.SwitchBranch(lastReleaseBranch)
            '    'Pull from origin/master
            '    GitOp.pullBranch(lastReleaseBranch)
            'End If


            'SUBFLOW:RELEASE-LAST-VERSION
            If VersionRelease.toDoNextStep() Then
                WF_release.releaseTo(iTargetDB, lastReleaseBranch, "release", True)
            End If



            'PULL-MASTER-BRANCH
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("PULL-MASTER-BRANCH")
                'Switch to master branch
                GitOp.SwitchBranch("master")
                'Pull from origin/master
                GitOp.pullBranch("master")
            End If


            'SWITCH-TO-MASTER-RELEASE-CANDIDATE
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("SWITCH-TO-MASTER-RELEASE-CANDIDATE")
                'Switch to candidate commit
                Tortoise.Switch(Globals.getRepoPath)
            End If

            'BRANCH-TO-NEW-RELEASE
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("BRANCH-TO-NEW-RELEASE")
                'Create and Switch to new release branch
                GitOp.createAndSwitchBranch(newReleaseBranch)
            End If



            'PUSH-NEW-RELEASE
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("PUSH-NEW-RELEASE")
                'Push release to origin with tags
                'GitOp.pushBranch(newReleaseBranch)
                GitOp.pushCurrentBranch()
            End If


            '-------------------
            'PATCH-VERSION-RELEASE  - 2 steps
            '-------------------

            'CHOOSE-PATCH-VERSION-RELEASE
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("CHOOSE-PATCH-VERSION-RELEASE")

                Dim appCodeList As Collection = GitOp.getReleaseAppCodeList()

                lAppCode = ChoiceDialog.Ask("Please choose the app to be released.", appCodeList, lAppCode, "Identify the app", True, False, False)

                Dim releaseBranches As Collection = GitOp.getPatchVersionReleaseList(lAppCode, True)

                newReleaseBranch = ChoiceDialog.Ask("Please identify the Patch Version Release to be built.",
                                                  releaseBranches, "", "Choose existing Patch Version Release branch", False, False, False, releaseBranches.Count - 1)

            End If

            ''PULL-PATCH-VERSION-RELEASE
            'If VersionRelease.toDoNextStep() Then
            '    Logger.Debug("PULL-PATCH-VERSION-RELEASE")
            '    'Switch to release branch
            '    GitOp.SwitchBranch(newReleaseBranch)
            '    'Pull from origin/release
            '    GitOp.pullBranch(newReleaseBranch)
            'End If

            'SUBFLOW:RELEASE-NEW-VERSION
            If VersionRelease.toDoNextStep() Then
                WF_release.releaseTo(iTargetDB, newReleaseBranch, "release", True)
            End If





            '-------------------
            'CREATE PATCH  - 2 steps
            '-------------------




            'In case we are starting part way through the workflow.
            If String.IsNullOrEmpty(lAppVersion) Then
                'Derive the app version from the current branch
                Logger.Debug("Derive the newReleaseBranch, appCode, and appVersion from the current branch")

                newReleaseBranch = GitOp.CurrentFriendlyBranch()
                Logger.Note("newReleaseBranch", newReleaseBranch)
                Dim lrelease As String = Common.getFirstSegment(newReleaseBranch, "/")
                If lrelease <> "release" Then
                    Throw New System.Exception("Current branch is NOT a release branch: " & newReleaseBranch & "  Aborting Operation.")
                End If
                lAppCode = Common.getNthSegment(newReleaseBranch, "/", 2)
                Logger.Note("lAppCode", lAppCode)
                lAppVersion = Common.getLastSegment(newReleaseBranch, "/")
                Logger.Note("l_app_version", lAppVersion)
            End If


            'CREATE-RELEASE-PATCH-WIZARD
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("CREATE-RELEASE-PATCH-WIZARD")
                'Create, edit And test collection
                If iVersionType = "Patch" Then
                    Dim Wizard As New CreateRelease(lAppCode, lAppVersion, iVersionType, "release", "hotfix", "hotfix", "release, feature, version, hotfix, ALL")
                Else
                    Dim Wizard As New CreateRelease(lAppCode, lAppVersion, iVersionType, "release", "feature", "feature", "release, feature, version, hotfix, ALL")
                End If

            End If

            'PUSH-NEW-RELEASE
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("PUSH-NEW-RELEASE")
                'Push release to origin with tags
                GitOp.pushCurrentBranch()
            End If


            '-------------------
            'MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
            '-------------------

            'MERGE-RELEASE
            If VersionRelease.toDoNextStep() Then
                Logger.Debug("MERGE-RELEASE")
                mergeBranchAndPush(iVersionType, "master", newReleaseBranch, False)
            End If


            'CLEAN-VM-SNAPSHOT
            If VersionRelease.toDoNextStep() Then
                'Snapshot VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Create a clean snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")
                Else
                    WF_virtual_box.takeSnapshot(lAppVersion & "-clean")
                End If

            End If


            'Done
            VersionRelease.toDoNextStep()



        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            VersionRelease.setToCompleted()
            VersionRelease.Close()
        End Try






    End Sub





    Shared Sub findPatchVersionRelease(ByVal iVersionType As String, ByVal iTargetDB As String, Optional ByVal throwError As Boolean = False)

        Dim currentBranch As String = GitOp.CurrentFriendlyBranch()
        Dim lcurrentDB As String = Globals.getDB
        Dim lAppVersion As String = Nothing
        Dim newReleaseBranch As String = "CANCEL"


        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version


        Dim PatchVersion As ProgressDialogue = New ProgressDialogue("Create " & iVersionType & " Version Release", "Create formal set of patches for a " & iVersionType & " Version Release")
        PatchVersion.MdiParent = GitPatcher

        '-------------------
        'PREPARE BRANCH - 7 steps
        '-------------------

        'IDENTIFY-LAST-RELEASE-BRANCH
        PatchVersion.addStep("Identify the current Release branch", True, "Identify the release branch.", True)

        'PULL-LAST-RELEASE-BRANCH
        PatchVersion.addStep("Pull the current Release branch", True, "Switch to the current Release branch and Pull", True)

        ''PULL-RELEASES-BRANCH
        'PatchVersion.addStep("Pull the releases branch", True, "Switch to the releases branch and Pull", useReleasesBranch) 'NEVER Not used currently.

        ''PULL-MASTER-BRANCH
        'PatchVersion.addStep("Pull the master branch", False, "Switch to the master branch and Pull", False)   'NEVER FALSE for PATCH (if i combine with routine with newMajorMinorVersionRelease)

        ''SWITCH-TO-MASTER-RELEASE-CANDIDATE
        'PatchVersion.addStep("Switch to the master release candidate", True,
        '                     "Choose a candidate commit on the master branch to create the new release from." &
        '                     "Patches and Apex Apps will be released as per this commit.", False)         'NEVER FALSE for PATCH

        'Determine whether a new patch release is required or we already have one.



        'BRANCH-TO-NEW-RELEASE    'LGIT - automatic.                                                          'PATCH will branch from the last release branch.
        PatchVersion.addStep("Branch to new Patch Version Release Branch", True,
                             "If required, will Branch from the current Release branch to the new Patch Version Release Branch.", True)

        'PUSH-NEW-RELEASE
        PatchVersion.addStep("Push the new release Branch", True, "If created, push new release Branch, so that other developers can see it.")


        '-------------------
        'CREATE PATCH  - 3 steps
        '-------------------

        ''SET DB TARGET
        'PatchVersion.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        ''CREATE RELEASE PATCH WIZARD
        'PatchVersion.addStep("Build " & iVersionType & " Version Release", True, "Select Patches" &
        '                          Chr(10) & "Build " & iVersionType & " Version Release" &
        '                          Chr(10) & "Test against " & iTargetDB &
        '                          Chr(10) & "Commit to GIT")

        'PUSH-NEW-RELEASE
        ' PatchVersion.addStep("Push the new release Branch", True, "Ensure other developers can see the new release patch.")


        ''-------------------
        ''MERGE NEW-RELEASE BRANCH INTO RELEASES - sub-workflow
        ''-------------------
        'PatchVersion.addStep("Fast-forward releases branch to head of new release branch", True, "Merge new release to releases branch, fastforward-only", useReleasesBranch)

        ''-------------------
        ''MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
        ''-------------------
        'PatchVersion.addStep("Merge new release branch to master", True, "Merge new release to master branch, no-fastforward-only")

        PatchVersion.Show()

        Do Until PatchVersion.isStarted
            Common.Wait(1000)
        Loop
        Try

            '-------------------
            'PREPARE BRANCH - 7 steps
            '-------------------

            Dim lastReleaseBranch As String = Nothing
            Dim lAppCode As String = Globals.currentAppCode
            Dim createNewReleaseBranch As Boolean = False

            'IDENTIFY-LAST-RELEASE-BRANCH
            If PatchVersion.toDoNextStep() Then

                Dim appCodeList As Collection = GitOp.getReleaseAppCodeList()

                lAppCode = ChoiceDialog.Ask("Please choose the app (from previously released apps)", appCodeList, lAppCode, "Identify the app", True, False, False)

                Dim releaseBranches As Collection = GitOp.getReleaseList(lAppCode)

                lastReleaseBranch = ChoiceDialog.Ask("Please identify the current release branch for the app: " & lAppCode, releaseBranches, "", "Identify current release branch", False, False, False, releaseBranches.Count - 1)

                'Determine whether to start a new release branch

                'Is this a patch release branch 
                '  If yes - has the release been patched.  
                '    If yes - branch to another patch release
                '    If no  - use this one.
                '  If no  - branch to another patch release

                Dim lMajorId As Integer
                Dim lMinorId As Integer
                Dim lPatchId As Integer
                'Dim lAppVersion As String = Nothing
                checkReleaseAppVersion(lastReleaseBranch, lAppCode, lMajorId, lMinorId, lPatchId, lAppVersion)


                If lPatchId > 0 Then
                    'Patch version release branch

                    'Has the patch version release been created.
                    'Test patch dir exists in current checkout.

                    If isReleaseBuilt(lastReleaseBranch, lAppVersion) Then

                        createNewReleaseBranch = True
                        MsgBox("A CLOSED Patch Version Release Branch was selected." & Chr(10) &
                               "A new Patch Version Release branch will be branched from here.")
                    Else
                        MsgBox("An OPEN Patch Version Release Branch was selected." & Chr(10) &
                               "The hotfix branch will be based on this branch.")

                    End If

                ElseIf lMinorId > 0 Then
                        'Minor version release branch
                        createNewReleaseBranch = True
                        MsgBox("A Minor Version Release Branch was selected." & Chr(10) &
                           "A new Patch Version Release branch will be branched from here.")
                    ElseIf lMajorId > 0 Then
                        'Major version release branch
                        createNewReleaseBranch = True
                    MsgBox("A Major Version Release Branch was selected." & Chr(10) &
                           "A new Patch Version Release branch will be branched from here.")
                End If

            End If





            'PULL-LAST-RELEASE-BRANCH
            If PatchVersion.toDoNextStep() Then
                'Switch to branch
                GitOp.SwitchBranch(lastReleaseBranch)
                'Pull from origin
                GitOp.pullBranch(lastReleaseBranch)
            End If


            ''PULL-RELEASES-BRANCH
            'If PatchVersion.toDoNextStep() Then
            '    'Switch to master branch
            '    GitOp.SwitchBranch("releases")
            '    'Pull from origin/master
            '    GitOp.pullBranch("releases")
            'End If

            ''PULL-MASTER-BRANCH
            'If PatchVersion.toDoNextStep() Then
            '    'Switch to master branch
            '    GitOp.SwitchBranch("master")
            '    'Pull from origin/master
            '    GitOp.pullBranch("master")
            'End If


            ''SWITCH-TO-MASTER-RELEASE-CANDIDATE
            'If PatchVersion.toDoNextStep() Then
            '    'Switch to candidate commit
            '    Tortoise.Switch(Globals.getRepoPath)
            'End If



            'BRANCH-NEW-RELEASE
            If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then

                lAppVersion = GetNextAppVersion(lastReleaseBranch, lAppCode, iVersionType)

                'Currently assuming all release branches include an appCode as 2nd element.
                newReleaseBranch = "release/" & lAppCode & "/" & lAppVersion

                'Create and Switch to new release branch
                GitOp.createAndSwitchBranch(newReleaseBranch)
            End If


            'PUSH-NEW-RELEASE
            If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then
                'Push release to origin with tags and set upstream
                GitOp.pushCurrentBranch("origin", True)
            End If


            '-------------------
            'CREATE PATCH  - 3 steps
            '-------------------

            'SET DB TARGET
            'If PatchVersion.toDoNextStep() Then
            '    'Change current DB to release DB
            '    Globals.setDB(iTargetDB.ToUpper)

            'End If


            ''In case we are starting part way through the workflow.
            'If String.IsNullOrEmpty(lAppVersion) Then
            '    'Derive the app version from the current branch
            '    Logger.Dbg("Derive the newReleaseBranch, appCode, and appVersion from the current branch")
            '    newReleaseBranch = GitOp.CurrentBranch()
            '    Logger.Note("newReleaseBranch", newReleaseBranch)
            '    Dim lrelease As String = Common.getFirstSegment(newReleaseBranch, "/")
            '    If lrelease <> "release" Then
            '        Throw New System.Exception("Current branch is NOT a release branch: " & newReleaseBranch & "  Aborting Operation.")
            '    End If
            '    lAppCode = Common.getNthSegment(newReleaseBranch, "/", 2)
            '    Logger.Note("lAppCode", lAppCode)
            '    lAppVersion = Common.getLastSegment(newReleaseBranch, "/")
            '    Logger.Note("l_app_version", lAppVersion)
            'End If


            ''CREATE RELEASE PATCH WIZARD
            'If PatchVersion.toDoNextStep() Then

            '    'Create, edit And test collection
            '    Dim Wizard As New CreateRelease(lAppCode, lAppVersion, iVersionType, "release", "feature", "feature", "release, feature, version, hotfix, ALL")

            'End If

            ''PUSH-NEW-RELEASE
            'If PatchVersion.toDoNextStep() Then
            '    'Push release to origin with tags
            '    GitOp.pushCurrentBranch()
            'End If


            ''-------------------
            ''MERGE NEW-RELEASE BRANCH INTO RELEASES - sub-workflow
            ''-------------------
            'If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then
            '    mergeBranchAndPush(iVersionType, "releases", newReleaseBranch, True)
            'End If

            ''-------------------
            ''MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
            ''-------------------

            ''MERGE-RELEASE
            'If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then
            '    mergeBranchAndPush(iVersionType, "master", newReleaseBranch, False)
            'End If

            'Done
            PatchVersion.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            PatchVersion.setToCompleted()
            PatchVersion.Close()

            If throwError Then
                Throw ex
            End If

        End Try


    End Sub






    Shared Sub createHotFixBranch(ByVal iVersionType As String, ByVal iTargetDB As String, ByVal useReleasesBranch As String)

        Dim currentBranch As String = GitOp.CurrentFriendlyBranch()
        Dim lcurrentDB As String = Globals.getDB
        Dim lAppVersion As String = Nothing
        Dim newReleaseBranch As String = "release"


        Dim PatchVersion As ProgressDialogue = New ProgressDialogue("Create " & iVersionType & " Version Release", "Create formal set of patches for a " & iVersionType & " Version Release")
        PatchVersion.MdiParent = GitPatcher

        '-------------------
        'PREPARE BRANCH - 7 steps
        '-------------------

        'IDENTIFY-LAST-RELEASE-BRANCH
        PatchVersion.addStep("Identify the current Release branch", True, "Identify the release branch.", True)

        'PULL-LAST-RELEASE-BRANCH
        PatchVersion.addStep("Pull the current Release branch", False, "Switch to the current Release branch and Pull", True)

        'PULL-RELEASES-BRANCH
        PatchVersion.addStep("Pull the releases branch", True, "Switch to the releases branch and Pull", useReleasesBranch) 'Not used currently.

        'PULL-MASTER-BRANCH
        PatchVersion.addStep("Pull the master branch", False, "Switch to the master branch and Pull", False)   'FALSE for PATCH (if i combine with routine with newMajorMinorVersionRelease)

        'SWITCH-TO-MASTER-RELEASE-CANDIDATE
        PatchVersion.addStep("Switch to the master release candidate", True,
                             "Choose a candidate commit on the master branch to create the new release from." &
                             "Patches and Apex Apps will be released as per this commit.", False)         'FALSE for PATCH

        'Determine whether a new patch release is required or we already have one.



        'BRANCH-TO-NEW-RELEASE    'LGIT - automatic.                                                          'PATCH will branch from the last release branch.
        PatchVersion.addStep("Branch to new release Branch", True,
                             "Branch from the current Release branch to the new release.", True)

        'PUSH-NEW-RELEASE
        PatchVersion.addStep("Push the new release Branch", True, "Ensure new release Branch exists, before building patch, so that other developers can see.")


        '-------------------
        'CREATE PATCH  - 3 steps
        '-------------------

        ''SET DB TARGET
        'PatchVersion.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        ''CREATE RELEASE PATCH WIZARD
        'PatchVersion.addStep("Build " & iVersionType & " Version Release", True, "Select Patches" &
        '                          Chr(10) & "Build " & iVersionType & " Version Release" &
        '                          Chr(10) & "Test against " & iTargetDB &
        '                          Chr(10) & "Commit to GIT")

        'PUSH-NEW-RELEASE
        ' PatchVersion.addStep("Push the new release Branch", True, "Ensure other developers can see the new release patch.")


        '-------------------
        'MERGE NEW-RELEASE BRANCH INTO RELEASES - sub-workflow
        '-------------------
        PatchVersion.addStep("Fast-forward releases branch to head of new release branch", True, "Merge new release to releases branch, fastforward-only", useReleasesBranch)

        '-------------------
        'MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
        '-------------------
        PatchVersion.addStep("Merge new release branch to master", True, "Merge new release to master branch, no-fastforward-only")

        PatchVersion.Show()

        Do Until PatchVersion.isStarted
            Common.Wait(1000)
        Loop
        Try

            '-------------------
            'PREPARE BRANCH - 7 steps
            '-------------------

            Dim lastReleaseBranch As String = Nothing
            Dim lAppCode As String = Globals.currentAppCode

            'IDENTIFY-LAST-RELEASE-BRANCH
            If PatchVersion.toDoNextStep() Then

                Dim appCodeList As Collection = GitOp.getReleaseAppCodeList()

                lAppCode = ChoiceDialog.Ask("Please choose the app (from previously released apps)", appCodeList, lAppCode, "Identify the app", True, False, False)

                Dim releaseBranches As Collection = GitOp.getReleaseList(lAppCode)

                lastReleaseBranch = ChoiceDialog.Ask("Please identify the current release branch for the app: " & lAppCode, releaseBranches, "", "Identify current release branch", False, False, False)

            End If

            'PULL-LAST-RELEASE-BRANCH
            If PatchVersion.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch(lastReleaseBranch)
                'Pull from origin/master
                GitOp.pullBranch(lastReleaseBranch)
            End If


            'PULL-RELEASES-BRANCH
            If PatchVersion.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch("releases")
                'Pull from origin/master
                GitOp.pullBranch("releases")
            End If

            'PULL-MASTER-BRANCH
            If PatchVersion.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch("master")
                'Pull from origin/master
                GitOp.pullBranch("master")
            End If


            'SWITCH-TO-MASTER-RELEASE-CANDIDATE
            If PatchVersion.toDoNextStep() Then
                'Switch to candidate commit
                Tortoise.Switch(Globals.getRepoPath)
            End If


            'BRANCH-TO-NEW-RELEASE
            Dim createNewReleaseBranch As Boolean = False

            If PatchVersion.toDoNextStep() Then

                'Determine whether to start a new release branch

                'Is this a patch release branch 
                '  If yes - has the release been patched.  
                '    If yes - branch to another patch release
                '    If no  - use this one.
                '  If no  - branch to another patch release

                Dim PatchId As Integer
                Try
                    PatchId = Common.getLastSegment(lastReleaseBranch, ".")
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Throw New System.Exception("Unable to read patch version id")
                End Try

                If PatchId > 0 Then
                    'Patch version release branch

                    'Has the patch version release been created.
                    'Test patch dir exists in current checkout.
                    lAppVersion = Common.getLastSegment(lastReleaseBranch, "/")
                    If FileIO.fileExists(Globals.RootPatchDir & "/" & lastReleaseBranch & "/" & lAppVersion & "/install.sql") Then
                        createNewReleaseBranch = True
                    Else
                        createNewReleaseBranch = False
                    End If

                Else
                    'Minor or Major version release branch
                    createNewReleaseBranch = True
                End If



                If createNewReleaseBranch Then

                    lAppVersion = GetNextAppVersion(lastReleaseBranch, lAppCode, iVersionType)

                    'Currently assuming all release branches include an appCode as 2nd element.
                    newReleaseBranch = newReleaseBranch & "/" & lAppCode & "/" & lAppVersion

                    'Create and Switch to new release branch
                    GitOp.createAndSwitchBranch(newReleaseBranch)

                End If

            End If


            'PUSH-NEW-RELEASE
            If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then
                'Push release to origin with tags
                GitOp.pushCurrentBranch()
            End If


            '-------------------
            'CREATE PATCH  - 3 steps
            '-------------------

            'SET DB TARGET
            'If PatchVersion.toDoNextStep() Then
            '    'Change current DB to release DB
            '    Globals.setDB(iTargetDB.ToUpper)

            'End If


            ''In case we are starting part way through the workflow.
            'If String.IsNullOrEmpty(lAppVersion) Then
            '    'Derive the app version from the current branch
            '    Logger.Dbg("Derive the newReleaseBranch, appCode, and appVersion from the current branch")
            '    newReleaseBranch = GitOp.CurrentBranch()
            '    Logger.Note("newReleaseBranch", newReleaseBranch)
            '    Dim lrelease As String = Common.getFirstSegment(newReleaseBranch, "/")
            '    If lrelease <> "release" Then
            '        Throw New System.Exception("Current branch is NOT a release branch: " & newReleaseBranch & "  Aborting Operation.")
            '    End If
            '    lAppCode = Common.getNthSegment(newReleaseBranch, "/", 2)
            '    Logger.Note("lAppCode", lAppCode)
            '    lAppVersion = Common.getLastSegment(newReleaseBranch, "/")
            '    Logger.Note("l_app_version", lAppVersion)
            'End If


            ''CREATE RELEASE PATCH WIZARD
            'If PatchVersion.toDoNextStep() Then

            '    'Create, edit And test collection
            '    Dim Wizard As New CreateRelease(lAppCode, lAppVersion, iVersionType, "release", "feature", "feature", "release, feature, version, hotfix, ALL")

            'End If

            ''PUSH-NEW-RELEASE
            'If PatchVersion.toDoNextStep() Then
            '    'Push release to origin with tags
            '    GitOp.pushCurrentBranch()
            'End If


            '-------------------
            'MERGE NEW-RELEASE BRANCH INTO RELEASES - sub-workflow
            '-------------------
            If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then
                mergeBranchAndPush(iVersionType, "releases", newReleaseBranch, True)
            End If

            '-------------------
            'MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
            '-------------------

            'MERGE-RELEASE
            If PatchVersion.toDoNextStep(Not createNewReleaseBranch, createNewReleaseBranch) Then
                mergeBranchAndPush(iVersionType, "master", newReleaseBranch, False)
            End If


            'Done
            PatchVersion.toDoNextStep()



        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            PatchVersion.setToCompleted()
            PatchVersion.Close()
        End Try






    End Sub





    Shared Sub newPatchVersionReleaseOLD(ByVal iVersionType As String, iTargetDB As String)


        Dim currentBranch As String = GitOp.CurrentFriendlyBranch()
        Dim lcurrentDB As String = Globals.getDB
        Dim l_app_version As String = Nothing
        Dim newReleaseBranch As String = "release"


        'l_app_version = Globals.currentAppCode & "-" & l_app_version

        'Dim newBranch As String = "release/" & iCreatePatchType & "/" & Globals.currentAppCode & "/" & l_app_version


        Dim PatchVersion As ProgressDialogue = New ProgressDialogue("Create " & iVersionType & " Version Release", "Create formal Set Of patches For a " & iVersionType & " Version Release")
        PatchVersion.MdiParent = GitPatcher

        '-------------------
        'PREPARE BRANCH
        '-------------------


        'SWITCH-TO-MASTER
        'PatchVersion.addStep("Switch To master branch", currentBranch <> "master")

        'PULL-MASTER
        'PatchVersion.addStep("Pull master branch", True, "Ensure master branch Is upto Date.")

        'SWITCH-TO-LAST-RELEASE
        PatchVersion.addStep("Switch To current release branch", True,
                                  "Please choose the current release branch")
        'TGIT - Let user choose any branch. Or could create a dialog with a list of release/ branches.

        'PULL-LAST-RELEASE
        PatchVersion.addStep("Pull the current release branch", True, "Ensure current release branch Is upto Date.")
        'LGIT - automatic

        'DERIVE-NEXT-PATCH-VERSION 
        PatchVersion.addStep("Derive Or Ask For a version number", True, "Derive Or Ask For a version number")
        'LGIT - automatic."

        'BRANCH-TO-NEW-RELEASE
        'PatchVersion.addStep("Create and Switch to new release Branch: " & newBranch, True, "")
        'LGIT - automatic.

        'MERGE(NOFF)-FROM-MASTER-CHOOSE-SHA
        'PatchVersion.addStep("Merge some commit from the master branch history", True,
        '                         "Please choose the HEAD or some other commit on the master branch that includes all patches to be included in the version release.")
        'TGIT - Let user choose any commit, but should be a commit from the master branch, and usually a merge commit of a feature branch.

        'PUSH-NEW-RELEASE
        'PatchVersion.addStep("Push the new release Branch", True, "Ensure new release Branch exists, before building patch, so that other developers can see.")


        '-------------------
        'CREATE PATCH 
        '-------------------

        'SET DB TARGET
        PatchVersion.addStep("Change current DB to : " & iTargetDB, lcurrentDB <> iTargetDB)

        'CREATE RELEASE PATCH WIZARD
        PatchVersion.addStep("Build " & iVersionType & " Version Release", True, "Select Patches" &
                                  Chr(10) & "Build " & iVersionType & " Version Release" &
                                  Chr(10) & "Test against " & iTargetDB &
                                  Chr(10) & "Commit to GIT")


        '-------------------
        'MERGE RELEASE BRANCH INTO MASTER
        '-------------------


        'MERGE-RELEASE
        PatchVersion.addStep("Merge new release", True, "Merge new Release to master.")

        PatchVersion.Show()

        Do Until PatchVersion.isStarted
            Common.Wait(1000)
        Loop
        Try

            '-------------------
            'PREPARE BRANCH
            '-------------------

            'SWITCH-TO-MASTER
            If PatchVersion.toDoNextStep() Then
                'Switch to master branch
                GitOp.SwitchBranch("master")
            End If

            'PULL-MASTER
            If PatchVersion.toDoNextStep() Then
                'Pull from origin/master
                GitOp.pullBranch("master")
            End If

            'SWITCH-TO-LAST-RELEASE
            If PatchVersion.toDoNextStep() Then
                'Switch to last release branch
                Tortoise.Switch(Globals.getRepoPath)
            End If

            'PULL-LAST-RELEASE
            If PatchVersion.toDoNextStep() Then
                'Pull 
                GitOp.pullBranch("") 'LGIT does not use the branch param. BGIT does.
            End If

            'DERIVE-NEXT-MAJOR/MINOR-VERSION 
            If PatchVersion.toDoNextStep() Then
                l_app_version = InputBox("Please confirm new semantic release id for " & Globals.currentAppCode & "", "New " & Globals.getAppName & " Version", Globals.currentAppCode & "-1.0.0")
                If String.IsNullOrEmpty(l_app_version) Then
                    Throw New System.Exception("User Cancelled Operation")
                End If

                If Globals.getAppInFeature() = "Y" Then
                    newReleaseBranch = newReleaseBranch & "/" & Globals.currentAppCode
                End If

                newReleaseBranch = newReleaseBranch & "/" & l_app_version

            End If

            'BRANCH-TO-NEW-RELEASE
            If PatchVersion.toDoNextStep() Then
                'Create and Switch to new release branch
                GitOp.createAndSwitchBranch(newReleaseBranch)
            End If

            'MERGE(NOFF)-FROM-MASTER-CHOOSE-SHA
            If PatchVersion.toDoNextStep() Then
                'Merge from master branch
                Tortoise.Merge(Globals.getRepoPath)
            End If

            'PUSH-NEW-RELEASE
            If PatchVersion.toDoNextStep() Then
                'Push release to origin with tags
                GitOp.pushBranch(newReleaseBranch)
            End If

            '-------------------
            'CREATE PATCH 
            '-------------------

            'SET DB TARGET
            If PatchVersion.toDoNextStep() Then
                'Change current DB to release DB
                Globals.setDB(iTargetDB.ToUpper)

            End If


            Dim l_tag_seq As Integer


            'CREATE RELEASE PATCH WIZARD
            If PatchVersion.toDoNextStep() Then
                'Create, edit And test collection
                Dim Wizard As New CreateRelease("QHIDS", l_app_version, "Patch", "release", "hotfix", "hotfix", "release,feature,version,hotfix,ALL")

            End If


            '-------------------
            'MERGE NEW-RELEASE BRANCH INTO RELEASES - sub-workflow
            '-------------------
            If PatchVersion.toDoNextStep() Then
                mergeBranchAndPush(iVersionType, "releases", newReleaseBranch, True)
            End If

            '-------------------
            'MERGE NEW-RELEASE BRANCH INTO INTO MASTER - sub-workflow
            '-------------------

            'MERGE-RELEASE
            If PatchVersion.toDoNextStep() Then
                mergeBranchAndPush(iVersionType, "master", newReleaseBranch, False)
            End If


            'Done
            PatchVersion.toDoNextStep()



        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            PatchVersion.setToCompleted()
            PatchVersion.Close()
        End Try


    End Sub

    Shared Sub newFullVersionRelease(ByVal iVersionType As String, iTargetDB As String)
        'This will be a series of workflows that build the version branch and patches and then creates a release.


        'switch master
        'pull
        'switch to commit on master
        'create a version branch

        'createVersionPatch(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)

        'create a version patch
        'merge version to master 
        '  (mergeReleaseToMaster(iVersionType, newBranch)
        'create a major/minor release



    End Sub


    Shared Sub newVersionRelease(ByVal iVersionType As String, iTargetDB As String)
        Logger.Debug("newVersionRelease")
        Logger.Note("iVersionType", iVersionType)
        Logger.Note("iTargetDB", iTargetDB)


        If iVersionType = "Major" Or iVersionType = "Minor" Then
            createVersionRelease(iVersionType, iTargetDB)
        ElseIf iVersionType = "Patch" Then
            createVersionRelease(iVersionType, iTargetDB)
            'newPatchVersionRelease(iVersionType, iTargetDB)
        ElseIf iVersionType = "Full" Then
            newFullVersionRelease(iVersionType, iTargetDB)
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

        Dim currentBranch As String = GitOp.CurrentFriendlyBranch()

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
            'WF_release.releaseTo("UAT")
        End If

        'RELEASE PROD
        If createPatchSetProgress.toDoNextStep() Then
            'Release to PROD
            'WF_release.releaseTo("PROD")
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