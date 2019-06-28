Friend Class WF_createPatch
    Shared Sub createPatchProcess(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String,
                                  Optional ByVal iRebaseHotfix As Boolean = False,
                                  Optional ByVal iRebaseFeature As Boolean = False,
                                  Optional ByVal iFeatureTipSHA As String = "",
                                  Optional ByVal ipreMasterSHA As String = "",
                                  Optional ByVal ipostMasterSHA As String = "")

        Dim rebaseBranchOn As String = iRebaseBranchOn

        Common.checkBranch(iBranchType)

        Dim l_tag_base As String = Nothing

        Dim shortBranch As String = Globals.currentBranch()
        Dim shortBranchUpper As String = shortBranch.ToUpper

        If Not shortBranch.Equals(shortBranchUpper) Then
            MsgBox("Please rename " & iBranchType & " " & shortBranch & " in uppercase " & shortBranchUpper, MsgBoxStyle.Exclamation, "Non-standard feature name")
            Exit Sub
        End If


        Dim currentBranch As String = Globals.currentLongBranch()

        Dim title As String = "Create " & iBranchType & " Patch"
        If iRebaseHotfix Then
            title = "Rebased Hotfix as Feature Patch"
        End If
        If iRebaseFeature Then
            title = "Rebasing and Updating Feature Patch"
        End If


        Dim createPatchProgress As ProgressDialogue = New ProgressDialogue(title)
        createPatchProgress.MdiParent = GitPatcher

        ' "Regenerate: Menu (new pages, menu changes), Security (new pages, security changes), Tapis (table or view column changes), Domains (new or changed tables or views, new domains or domain ussage changed)")
        'CHOOSE-RELEASE-BRANCH
        createPatchProgress.addStep("Choose Release Branch", True, "Choose the Patch Version Release branch that this hotfix was branched from.", iBranchType = "hotfix")

        'REBASE-FEATURE
        createPatchProgress.addStep("Rebase branch: " & currentBranch & " on branch: " & iRebaseBranchOn, True, "Using the Rebase workflow", Not iRebaseHotfix)
        'REVIEW-TAGS
        createPatchProgress.addStep("Review tags on Branch: " & currentBranch, True, "Check that the tags have been assigned to the correct commits.", Not iRebaseFeature And Not iRebaseHotfix)
        'CREATE-PATCH
        createPatchProgress.addStep("Create edit, test", True, "Now is a great time to smoke test my work before i commit the patch.", Not iRebaseFeature And Not iRebaseHotfix)
        'UPDATE-FEATURE-PATCH
        createPatchProgress.addStep("Update my " & iBranchType & " patches", True,
                                    "If there are changed files in common between master and feature branches, then these files must be updated in the feature patches.", iRebaseFeature)
        'UPDATE-REBASED-HOTFIX-PATCH
        createPatchProgress.addStep("Update my rebased hotfix patches", True,
                                    "If there are changed files in common between master and hotfix branches, then these files must be updated in the rebased hotfix patches.", iRebaseHotfix)

        'EXECUTE-PATCHES
        createPatchProgress.addStep("Execute my patches", True, "My patches need to be executed, changed or not.", iRebaseFeature Or iRebaseHotfix)
        'EXTRA-COMMIT
        createPatchProgress.addStep("Commit to Branch: " & currentBranch, False)
        'IMPORT-QUEUED-APPS
        createPatchProgress.addStep("Import any queued apps: " & currentBranch, True, "Any Apex Apps that were included in a patch, must be reinstalled now. ")
        'SWITCH-TO-MASTER
        createPatchProgress.addStep("Switch to " & iRebaseBranchOn & " branch", True)

        'PULL-MASTER-AGAIN
        createPatchProgress.addStep("Pull " & iRebaseBranchOn & " branch", True, "Double-check that the " & iRebaseBranchOn & " is still on the latest commit.")

        'MERGE-FEATURE
        createPatchProgress.addStep("Merge from Branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        'PUSH-MASTER
        createPatchProgress.addStep("Push to Origin", True,
                                    "If the push is not successful, the TortoiseGIT Synchronisation dialog will be raised." & Chr(10) &
                                    "Should say '0 commits ahead orgin/" & iRebaseBranchOn & "'." & Chr(10) &
                                    "If NOT, then your " & iRebaseBranchOn & " branch may now out of date, and also your rebase from it.  So any patches you created COULD BE stale. " &
                                    "In this situation, it is safest to restart the Create Patch process to ensure you are patching the lastest merged files. ")

        'RELEASE-TO-TARGET
        createPatchProgress.addStep("Release to " & iDBtarget, True)
        'RETURN-FEATURE
        createPatchProgress.addStep("Return to Branch: " & currentBranch, True)
        'SNAPSHOT-CLEAN
        createPatchProgress.addStep("Clean VM Snapshot", True, "Create a clean snapshot of your current VM state, to use as your next restore point.")


        'HOTFIX ONLY
        '-----------

        'SUB-FLOW: CREATE-FEATURE-BRANCH-FOR-HOTFIX
        createPatchProgress.addStep("Create a new feature branch to rebase " & shortBranch, True, "Create a new feature branch to rebase the hotfix on master.", iBranchType = "hotfix")

        'MERGE-BASE-RELEASE-TO-FEATURE
        createPatchProgress.addStep("Merge " & iRebaseBranchOn & " to new feature", True, "Merge " & iRebaseBranchOn & " to the new feature.  This merges in the new hotfix patches.", iBranchType = "hotfix")

        'COPY-HOTFIX-PATCHES
        createPatchProgress.addStep("Copy and rebase " & shortBranch & " patches to new feature patches", True,
        "Loop thru patches and copy them" & Chr(10) &
        " + Copy the patch folders And files" & Chr(10) &
        " + Update scripts with the latest versions" & Chr(10) &
        " + Modify install.sql And install_lite.sql with New patch_name And patch_patch.",
         iBranchType = "hotfix")

        'May actually be able to use STEP UPDATE-PATCH to + Update scripts with the latest versions

        'SUB-FLOW: CREATE-PATCH-START-AT-EXECUTE-PATCHES
        createPatchProgress.addStep("Execute Patches, Merge to master, Release to DEV ", True, "Start the Create Feature Patch workflow midway to complete the rebased hotfix patch as a feature patch.", iBranchType = "hotfix")




        'Need to create a new feature 
        'Eg if we just did hotfix/QHIDS-1234-HF then need to now create feature/QHIDS-1234-RB

        'Need to use the Create New Branch workflow
        ' passing - new feature name feature/QHIDS-1234-RB
        '         - name of the Patch Version Release to be merged in.  (or may come back here to do that?)
        ' must also change VM's to the DEV VM
        ' shutdown the HOTFIX VM and start the DEV VM - need to remember 2 VM's now.
        ' then need to identify and copy each patch, and 
        ' update the install script And files.






        createPatchProgress.Show()



        Do Until createPatchProgress.isStarted
            Common.Wait(1000)
        Loop

        Try

            'CHOOSE-RELEASE-BRANCH
            If createPatchProgress.toDoNextStep() Then
                'Choose the release branch that this hotfix is currently based on

                'Switch to current release branch
                ' Tortoise.Switch(Globals.getRepoPath)
                Dim releaseBranches As Collection = GitOp.getPatchVersionReleaseList(Globals.currentAppCode)

                rebaseBranchOn = ChoiceDialog.Ask("This hotfix was branched from a Patch Version Release branch." & Chr(10) &
                                                  "Please identify the original Patch Version Release branch on which to rebase this hotfix",
                                                  releaseBranches, "", "Choose original Patch Version Release branch", False, False, False)

                'Dim currentReleaseBranch As String = GitOp.CurrentBranch()

                'Dim lrelease As String = Common.getFirstSegment(currentReleaseBranch, "/")
                'If lrelease <> "release" Then
                '    Throw New System.Exception("Current branch is NOT a release branch: " & currentReleaseBranch & "  Aborting Operation.")
                'End If

                ''This validates against the current app.  Alternatively i could use this to change the current app.
                'Dim lAppCode As String = Common.getNthSegment(currentReleaseBranch, "/", 2)
                'If lAppCode <> Globals.currentAppCode Then
                '    Throw New System.Exception("Current release branch is NOT for the current app " & Globals.currentAppCode & ": " & currentReleaseBranch & "  Aborting Operation.")
                'End If

                'rebaseBranchOn = currentReleaseBranch

            End If

            'REBASE-FEATURE
            If createPatchProgress.toDoNextStep() Then
                'Rebase branch
                l_tag_base = WF_rebase.rebaseBranch(iBranchType, iDBtarget, rebaseBranchOn, True, True, True, iRebaseFeature)
                If String.IsNullOrEmpty(l_tag_base) Then
                    Throw New Exception("Invalid tag - cancelling patch.")
                End If
                If l_tag_base = "CANCEL" Then
                    Throw New Exception("Rebase cancelled - cancelling patch.")
                End If


            End If

            'REVIEW-TAGS
            If createPatchProgress.toDoNextStep() Then
                'Review tags on the branch
                Tortoise.Log(Globals.getRepoPath)
            End If

            'CREATE-PATCH
            If createPatchProgress.toDoNextStep() Then

                'Start the PatchFromTags and wait until it closes.
                Dim GitPatcherChild As PatchFromTags = New PatchFromTags(iBranchType, iDBtarget, rebaseBranchOn, l_tag_base)

                'Dim Wizard As New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn, l_tag_base)
                'newchildform.MdiParent = GitPatcher
                'Wizard.ShowDialog() 'NEED TO WAIT HERE!!


            End If


            'UPDATE-FEATURE-PATCH
            'If createPatchProgress.toDoNextStep(False, ipreMasterSHA <> ipostMasterSHA) Then
            If createPatchProgress.toDoNextStep() Then

                'If no database files shared in the 2 sets of changes, then we are able to do a simple rebase.
                'But if database files have been changed in common, then those in our patches must be updated from the source again.

                'More changes have been pulled.
                'Get a list of database changes between preMasterSHA and postMasterSHA

                GitOp.setCommitsFromSHA(ipreMasterSHA, ipostMasterSHA)
                Logger.Note("DBRepoPathMask", Globals.DBRepoPathMask)
                Dim masterDBChanges As Collection = GitOp.getChanges(Globals.DBRepoPathMask, False, True, False)
                'Common.MsgBoxCollection(masterDBChanges, "masterDBChanges")


                'Compare this to the list of changes patched in the feature since the previous rebase.
                GitOp.setCommitsFromSHA(ipreMasterSHA, iFeatureTipSHA)

                Dim featurePatchChanges As Collection = GitOp.getChanges(Globals.getPatchRelPath, False, True, False)
                'Common.MsgBoxCollection(featurePatchChanges, "featurePatchChanges")

                'Find common changed files on feature and master branches
                Dim commonChanges As Collection = New Collection()
                For Each change In featurePatchChanges
                    Dim filename As String = Common.getLastSegment(change, "/")
                    If masterDBChanges.Contains(filename) Then
                        'Copy master merged file into patch
                        Logger.Dbg("Update patched file " & change & " from merged database file " & masterDBChanges(filename))
                        FileIO.CopyFile(Globals.getRepoPath & masterDBChanges(filename), Globals.getRepoPath & change)
                        commonChanges.Add(change, filename)
                    End If
                Next
                Common.MsgBoxCollection(commonChanges, "Updated patch files", "The following patched files have been updated with rebased versions." & Chr(10))

            End If

            'UPDATE-REBASED-HOTFIX-PATCH
            If createPatchProgress.toDoNextStep() Then

                'If no database files shared in the 2 sets of changes, then we only need to update the install.sql and install_lite.sql
                'But if database files have been changed in common, then those in our patches must be updated from the source again.

                'More changes have been pulled.
                'Get a list of database changes between preMasterSHA and postMasterSHA

                GitOp.setCommitsFromSHA(ipreMasterSHA, ipostMasterSHA)
                Logger.Note("DBRepoPathMask", Globals.DBRepoPathMask)
                Dim masterDBChanges As Collection = GitOp.getChanges(Globals.DBRepoPathMask, False, True, False)
                'Common.MsgBoxCollection(masterDBChanges, "masterDBChanges")


                'Compare this to the list of changes patched in the feature since the previous rebase.
                GitOp.setCommitsFromSHA(ipreMasterSHA, iFeatureTipSHA)

                Dim featurePatchChanges As Collection = GitOp.getChanges(Globals.getPatchRelPath, False, True, False)
                'Common.MsgBoxCollection(featurePatchChanges, "featurePatchChanges")

                'Find common changed files on feature and master branches
                Dim commonChanges As Collection = New Collection()
                For Each change In featurePatchChanges
                    Dim filename As String = Common.getLastSegment(change, "/")
                    If masterDBChanges.Contains(filename) Then
                        'Copy master merged file into patch
                        Logger.Dbg("Update patched file " & change & " from merged database file " & masterDBChanges(filename))
                        FileIO.CopyFile(Globals.getRepoPath & masterDBChanges(filename), Globals.getRepoPath & change)
                        commonChanges.Add(change, filename)
                    End If
                Next
                Common.MsgBoxCollection(commonChanges, "Updated patch files", "The following patched files have been updated with rebased versions." & Chr(10))

            End If







            'EXECUTE-PATCHES - patch runner uninstalled.

            If createPatchProgress.toDoNextStep() Then
                Dim l_no_uninstalled_patches As Boolean = True
                Dim GitPatcherChild As PatchRunner = New PatchRunner(l_no_uninstalled_patches, "Uninstalled")
            End If

            'EXTRA-COMMIT
            Dim lCommitComment As String = Nothing
            If iRebaseFeature Then
                lCommitComment = "Rebase Feature Patches: " & shortBranch & "." & l_tag_base
            ElseIf iRebaseHotfix Then
                lCommitComment = "Rebase Hotfix Patches: " & shortBranch & "." & l_tag_base & " - Step 2. Update object and install files."
            End If

            If createPatchProgress.toDoNextStep() Then
                'User chooses to commit, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
                If GitOp.IsDirty() Then
                    Logger.Dbg("User chose to commit and the checkout is also dirty")

                    'Committing changed files to GIT
                    'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")

                    Tortoise.Commit(Globals.getRepoPath, lCommitComment, True)

                End If
            ElseIf Not createPatchProgress.IsDisposed Then 'ignore if form has been closed.
                'User chooses to NOT to commit, but commit anyway if there is at least 1 staged change
                'Committing changed files to GIT"
                If GitOp.ChangedFiles() > 0 Then
                    Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                    MsgBox("Files have been changed. Please commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                    Tortoise.Commit(Globals.getRepoPath, lCommitComment, True)

                End If

            End If

            'IMPORT-QUEUED-APPS
            Dim l_no_queued_apps As Boolean = True
            If createPatchProgress.toDoNextStep() Then
                'Run your app changes
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")

            End If

            Dim FeatureTipSHA As String = Nothing

            'Remember Tip of feature 
            FeatureTipSHA = GitOp.getTipSHA()
            Logger.Note("FeatureTipSHA", FeatureTipSHA)

            'SWITCH-TO-BASE
            If createPatchProgress.toDoNextStep() Then
                'switch
                GitOp.SwitchBranch(rebaseBranchOn)

            End If

            'PULL-BASE-AGAIN
            Dim preMasterSHA As String = "XX"
            Dim postMasterSHA As String = "XX"
            If createPatchProgress.toDoNextStep() Then
                'Need to determine whether master has moved on.

                'Remember Tip of master before pull 
                preMasterSHA = GitOp.getTipSHA()
                Logger.Note("preMasterSHA", preMasterSHA)

                'Pull from origin/develop
                GitOp.pullCurrentBranch()

                'Remember Tip of master after pull 
                postMasterSHA = GitOp.getTipSHA()
                Logger.Note("postMasterSHA", postMasterSHA)

                'If SHAs different then need a rebase - of some sort.
                If preMasterSHA <> postMasterSHA Then

                    MsgBox("A Pull on " & rebaseBranchOn & " has retrieved extra commits. " & Environment.NewLine &
                           "The " & iBranchType & " must be rebased again, before it can be pushed." & Environment.NewLine & Environment.NewLine &
                           "A new workflow will now open to handle the rebase. " & Environment.NewLine &
                           "This process will compare files modified in the " & rebaseBranchOn & " and " & iBranchType & " branches, and " & Environment.NewLine &
                           "automatically update patched files with New versions as needed.")

                    createPatchProgress.setToCompleted()
                    createPatchProgress.Close()

                    'switch back to the feature
                    GitOp.SwitchBranch(currentBranch)
                    'restart the createPatchProcess in rebase feature mode.
                    WF_createPatch.createPatchProcess(iBranchType, iDBtarget, rebaseBranchOn, False, True, FeatureTipSHA, preMasterSHA, postMasterSHA)

                End If

            End If


            'MERGE-FEATURE
            If createPatchProgress.toDoNextStep() Then
                'Merge from Feature branch

                'Tortoise.Merge(Globals.getRepoPath)
                GitOp.Merge(currentBranch)

            End If

            'PUSH-MASTER
            If createPatchProgress.toDoNextStep() Then
                'Push to origin/develop 
                GitOp.pushBranch(rebaseBranchOn)
            End If

            'RELEASE-TO-TARGET
            If createPatchProgress.toDoNextStep() Then
                'Release to DB Target
                WF_release.releaseTo(iDBtarget, rebaseBranchOn, iBranchType, False)
            End If

            'RETURN-FEATURE
            If createPatchProgress.toDoNextStep() Then
                'GitSharpFascade.switchBranch(Globals.currentRepo, currentBranch)
                GitOp.SwitchBranch(currentBranch)
            End If

            'SNAPSHOT-CLEAN
            If createPatchProgress.toDoNextStep() Then
                'Snapshot VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Create a clean snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")
                Else
                    WF_virtual_box.takeSnapshot(shortBranch & "." & l_tag_base & "-clean")
                End If

            End If

            'HOTFIX ONLY
            '-----------

            'SUB-FLOW:CREATE-FEATURE-BRANCH-FOR-HOTFIX

            Dim RebasedHotfixFeatureBranch As String = Replace(Globals.currentBranch, "-HF", "-RB")

            If createPatchProgress.toDoNextStep() Then
                'Create and Switch to new branch

                '@TODO Need to check that target Is VM_DEV And / Or that the VM has changed to VM_DEV


                WF_newBranch.createNewBranch("feature", "master", True, RebasedHotfixFeatureBranch)

            End If

            'MERGE-BASE-RELEASE-TO-FEATURE
            If createPatchProgress.toDoNextStep() Then
                'Merge from base branch
                GitOp.Merge(iRebaseBranchOn)

            End If


            'In case we parachute in at this point.
            RebasedHotfixFeatureBranch = Globals.currentBranch
            Dim originalHotfixBranch = Replace(RebasedHotfixFeatureBranch, "-RB", "-HF")
            'COPY-HOTFIX-PATCHES
            'May actually be able to use STEP UPDATE-PATCH


            If createPatchProgress.toDoNextStep() Then
                'Loop thru patches and copy them
                ' + Copy the patch folders And files
                ' + Update scripts with the latest versions
                ' + Modify install.sql And install_lite.sql with New patch_name And patch_patch.


                'Create dir feature/JIRA-123-HF - if not exists
                FileIO.createFolderIfNotExists(Globals.RootPatchDir & "feature\" & RebasedHotfixFeatureBranch & "." & l_tag_base)

                'Get a list of Hotfix patches
                'hotfix/JIRA-123-HF/JIRA-123-HF.01.QHOWN
                'hotfix/JIRA-123-HF/JIRA-123-HF.01.QHOWN

                'Create a set of Rebased Hotfix patches
                'feature/JIRA-123-RB/JIRA-123-RB.01.QHOWN
                'feature/JIRA-123-RB/JIRA-123-RB.01.QHOWN
                Dim HotFixPatchList As Collection = FileIO.FolderList(Globals.RootPatchDir & "hotfix\" & originalHotfixBranch, "*" & originalHotfixBranch & "." & l_tag_base & "*", "", True)
                'Dim HotFixPatchList As Collection = FileIO.FolderList(Globals.RootPatchDir & "hotfix\" & originalHotfixBranch, "*", "", True)
                For Each HotFixPatch In HotFixPatchList
                    'Copy the patch to the new location
                    Dim RebasePatch As String = Replace(HotFixPatch, "hotfix", "feature")
                    RebasePatch = Replace(RebasePatch, "-HF", "-RB")

                    FileIO.CopyDir(HotFixPatch, RebasePatch) 'Copy dir and contents
                Next

                'Add new folder and files.

                'Use GitBash to silently add files prior committing
                Try
                    GitBash.Add(Globals.getRepoPath, Globals.getPatchRelPath & "feature\" & RebasedHotfixFeatureBranch & "\" & RebasedHotfixFeatureBranch & "." & l_tag_base & "*", True)
                    'GitBash.Add(Globals.getRepoPath, Globals.RootPatchDir & "feature\" & RebasedHotfixFeatureBranch & "\" & RebasedHotfixFeatureBranch & "." & l_tag_base & "*", True)
                Catch ex As Exception
                    MsgBox(ex.Message)
                    MsgBox("Unable to Add Files with GitBash. Check GitBash configuration.")
                    'If GitBash.Push fails just let the process continue.
                    'User will add files via the commit dialog
                End Try


                'Commit step 1. copy patch folders and files
                Tortoise.Commit(Globals.getRepoPath, "Rebase Hotfix Patches: " & RebasedHotfixFeatureBranch & "." & l_tag_base & " - Step 1. Copy hotfix patch folders and files.", True)

                'Modify the installs in each patch.

                '@TODO hopefully will be able to pass SHA's back into this routine to get the updates done simply.

            End If

            '@TODO NB what happens to the merge from releae branch if there Is an extra rebase?


            'SUB-FLOW: CREATE-PATCH-START-AT-EXECUTE-PATCHES
            If createPatchProgress.toDoNextStep() Then
                'restart the createPatchProcess in rebase hotfix mode.
                WF_createPatch.createPatchProcess("feature", "DEV", "master", True)
            End If

            'Finish
            createPatchProgress.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            createPatchProgress.setToCompleted()
            createPatchProgress.Close()
        End Try


    End Sub


    'Use a separate routine, though may be able to consolidate later.
    Shared Sub createVersionPatch(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)

        Common.checkBranch(iBranchType)
        Dim l_tag_base As String = Nothing

        Dim shortBranch As String = Globals.currentBranch()
        Dim shortBranchUpper As String = shortBranch.ToUpper

        If Not shortBranch.Equals(shortBranchUpper) Then
            MsgBox("Please rename " & iBranchType & " " & shortBranch & " in uppercase " & shortBranchUpper, MsgBoxStyle.Exclamation, "Non-standard feature name")
            Exit Sub
        End If


        Dim currentBranch As String = Globals.currentLongBranch()

        Dim createPatchProgress As ProgressDialogue = New ProgressDialogue("Create " & iBranchType & " Patch")
        createPatchProgress.MdiParent = GitPatcher

        ' "Regenerate: Menu (new pages, menu changes), Security (new pages, security changes), Tapis (table or view column changes), Domains (new or changed tables or views, new domains or domain ussage changed)")
        createPatchProgress.addStep("Tag branch: " & currentBranch & " on branch: " & iRebaseBranchOn, True, "Using the tag workflow")
        createPatchProgress.addStep("Review tags on Branch: " & currentBranch)
        createPatchProgress.addStep("Create edit, test", True, "Now is a great time to smoke test my work before i commit the patch.")
        createPatchProgress.addStep("Commit to Branch: " & currentBranch, False)
        createPatchProgress.addStep("Import any queued apps: " & currentBranch, True, "Any Apex Apps that were included in a patch, must be reinstalled now. ")

        'createPatchProgress.addStep("Switch to " & iRebaseBranchOn & " branch", True)
        'createPatchProgress.addStep("Merge from Branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        createPatchProgress.addStep("Push to Origin", True, "If at this stage there is an error because your " & iRebaseBranchOn & " branch is out of date, then you must restart the process to ensure you are patching the lastest merged files.")
        createPatchProgress.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/" & iRebaseBranchOn & "'.  " _
                                 & "If NOT, then the push FAILED. Your " & iRebaseBranchOn & " branch is now out of date, so is your rebase from it, and any patches COULD BE stale. " _
                                 & "In this situation, it is safest to restart the Create Patch process to ensure you are patching the lastest merged files. ")
        'createPatchProgress.addStep("Release to " & iDBtarget, True)
        createPatchProgress.addStep("Return to Branch: " & currentBranch, True)
        createPatchProgress.addStep("Clean VM Snapshot", True, "Create a clean snapshot of your current VM state, to use as your next restore point.")
        createPatchProgress.Show()



        Do Until createPatchProgress.isStarted
            Common.Wait(1000)
        Loop

        Try

            If createPatchProgress.toDoNextStep() Then
                'Rebase branch
                l_tag_base = WF_rebase.tagBranch(iBranchType, iDBtarget, iRebaseBranchOn)
                If String.IsNullOrEmpty(l_tag_base) Then
                    Throw New Exception("Invalid tag - cancelling patch.")
                End If
                If l_tag_base = "CANCEL" Then
                    Throw New Exception("Rebase cancelled - cancelling patch.")
                End If


            End If


            If createPatchProgress.toDoNextStep() Then
                'Review tags on the branch
                Tortoise.Log(Globals.getRepoPath)
            End If


            If createPatchProgress.toDoNextStep() Then

                'Start the PatchFromTags and wait until it closes.
                Dim GitPatcherChild As PatchFromTags = New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn, l_tag_base)

                'Dim Wizard As New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn, l_tag_base)
                'newchildform.MdiParent = GitPatcher
                'Wizard.ShowDialog() 'NEED TO WAIT HERE!!


            End If


            If createPatchProgress.toDoNextStep() Then
                'User chooses to commit, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
                If GitOp.IsDirty() Then
                    Logger.Dbg("User chose to commit and the checkout is also dirty")

                    'Committing changed files to GIT
                    'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")
                    Tortoise.Commit(Globals.getRepoPath, "Commit any patches, or changes made while patching, that you've not yet committed", True)
                    'Tortoise.Commit(Globals.getRepoPath, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no uncommitted changes.", True)

                End If
            ElseIf Not createPatchProgress.IsDisposed Then 'ignore if form has been closed.
                'User chooses to NOT to commit, but commit anyway if there is at least 1 staged change
                'Committing changed files to GIT"
                If GitOp.ChangedFiles() > 0 Then
                    Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                    MsgBox("Files have been changed. Please commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                    Tortoise.Commit(Globals.getRepoPath, "Commit any patches, or changes made while patching, that you've not yet committed", True)
                    'MsgBox("Files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                    'Tortoise.Commit(Globals.getRepoPath, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no uncommitted changes.", True)

                End If

            End If

            Dim l_no_queued_apps As Boolean = True
            If createPatchProgress.toDoNextStep() Then
                'Run your app changes
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller(l_no_queued_apps, "Queued")

                'Dim newchildform As New ApexAppInstaller("Queued")
                'newchildform.MdiParent = GitPatcher
                'newchildform.ShowDialog() 'ShowDialog - means wait.
            End If


            'If createPatchProgress.toDoNextStep() Then
            '    'switch
            '    GitOp.SwitchBranch(iRebaseBranchOn)

            'End If

            'If createPatchProgress.toDoNextStep() Then
            '    'Merge from Feature branch

            '    'Tortoise.Merge(Globals.getRepoPath)
            '    GitOp.Merge(currentBranch)

            'End If

            If createPatchProgress.toDoNextStep() Then
                'Push to origin/develop 
                GitOp.pushBranch(iRebaseBranchOn)
            End If

            If createPatchProgress.toDoNextStep() Then
                'Synch command to verfiy that Push was successful.
                Tortoise.Sync(Globals.getRepoPath)
            End If

            'If createPatchProgress.toDoNextStep() Then
            '    'Release to DB Target
            '    WF_release.releaseTo(iDBtarget, iBranchType)
            'End If

            If createPatchProgress.toDoNextStep() Then
                'GitSharpFascade.switchBranch(Globals.currentRepo, currentBranch)
                GitOp.SwitchBranch(currentBranch)
            End If

            If createPatchProgress.toDoNextStep() Then
                'Snapshot VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Create a clean snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")
                Else
                    WF_virtual_box.takeSnapshot(shortBranch & "." & l_tag_base & "-clean")
                End If



            End If

            'Finish
            createPatchProgress.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            createPatchProgress.setToCompleted()
            createPatchProgress.Close()
        End Try


    End Sub


End Class
