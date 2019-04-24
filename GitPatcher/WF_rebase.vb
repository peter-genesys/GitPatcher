﻿Imports LibGit2Sharp

Friend Class WF_rebase
    Shared Function rebaseBranch(ByVal iBranchType As String _
                               , ByVal iDBtarget As String _
                               , ByVal iRebaseBranchOn As String _
                               , Optional ByVal iPatching As Boolean = True _
                               , Optional ByVal iAppChanges As Boolean = True _
                               , Optional ByVal iDBChanges As Boolean = True) As String

        Dim tag_num_padding As Integer = 2
        Common.checkBranch(iBranchType)

        Dim l_tag_prefix As String = Nothing
        If iBranchType = "hotfix" Then
            l_tag_prefix = iDBtarget.Substring(0, 1)
        End If


        Dim currentBranchLong As String = GitOp.CurrentBranch()
        Dim currentBranchShort As String = Globals.currentBranch
        Dim callStashPop As Boolean = False

        Dim title As String
        If iPatching Then
            title = "Slave Rebase - DB and Apex"
        ElseIf iAppChanges And iDBChanges Then
            title = "Standalone Rebase - DB and Apex"
        ElseIf iDBChanges Then
            title = "Standalone Rebase - DB changes only"
        ElseIf iAppChanges Then
            title = "Standalone Rebase - Apex changes only"
        End If


        Dim rebasing As ProgressDialogue = New ProgressDialogue(title & " - branch " & currentBranchLong)

        Dim l_max_tag As Integer = 0


        For Each thisTag As Tag In GitOp.getTagList(Globals.currentBranch)
            Try
                Dim tag_no As String = Common.getLastSegment(thisTag.FriendlyName, ".").Substring(0, tag_num_padding)

                If tag_no > l_max_tag Then
                    l_max_tag = tag_no
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox("Problem with formatting of tagname: " & thisTag.FriendlyName & "  This tag may need to be deleted.")
            End Try

        Next


        Dim l_tag_base As String = l_max_tag + 1
        l_tag_base = l_tag_prefix & l_tag_base.PadLeft(tag_num_padding, "0")

        rebasing.MdiParent = GitPatcher
        'EXPORT-APPS-MINE
        rebasing.addStep("Export Apex Apps to " & iBranchType & " branch: " & currentBranch(), True, "Using ApexAppExporter, export from the VM any apps you have changed.", iAppChanges) ' or the Apex Export workflow")
        'SMARTGEN
        rebasing.addStep("Use SmartGen to spool changed config data: " & currentBranch(), True,
                                    "Did I change any config data?  " &
                                    "Do I need to spool any table changes or generate related objects?  " &
                                    "If so, logon to SmartGen, generate and/or spool code. " &
                                    "Use db-spooler to spool the objects to the local filesystem. " &
                                    "Then commit it too.", iAppChanges Or iDBChanges)
        'COMMIT
        rebasing.addStep("Commit to Branch: " & currentBranchLong, False, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", iAppChanges Or iDBChanges)
        'STASH-SAVE
        rebasing.addStep("Stash Save: " & currentBranchLong, False, "Stash Save to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", iAppChanges Or iDBChanges)
        'SWITCH-MASTER
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", True, "If you get an error concerning uncommitted changes.  Please resolve the changes and then RESTART this process to ensure the switch to " & iRebaseBranchOn & " branch is successful.", iAppChanges Or iDBChanges)
        'PULL-MASTER
        rebasing.addStep("Pull from Origin", True, "Pull from the master branch.", iAppChanges Or iDBChanges)
        'TAG-A-MASTER
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & "." & l_tag_base & "A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & currentBranchShort & ".99A", iPatching)
        'RETURN-FEATURE
        rebasing.addStep("Return to branch: " & currentBranchLong, True, "Return to the feature branch", iAppChanges Or iDBChanges)
        'REBASE-FEATURE
        rebasing.addStep("Rebase Branch: " & currentBranchLong & " From Upstream:" & iRebaseBranchOn, True, "Please select the Upstream Branch:" & iRebaseBranchOn & " from the Tortoise Rebase Dialogue", iAppChanges Or iDBChanges)
        'TAG-B-FEATURE
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & "." & l_tag_base & "B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.", iPatching)
        'REVERT
        rebasing.addStep("Revert your VM", True, "Before running patches, consider reverting to a VM snapshot prior to the development of your current work, or swapping to a unit test VM.", iDBChanges)
        'PATCH-RUNNER
        rebasing.addStep("Use PatchRunner to run Unapplied Patches", True, iAppChanges Or iDBChanges)
        'IMPORT-APPS-QUEUED
        rebasing.addStep("Import any queued apps: " & currentBranch(), True, "Any Apex Apps that were included in a patch, must be reinstalled now. ", iAppChanges Or iDBChanges)
        'IMPORT-APPS-MINE
        rebasing.addStep("Re-Import my changed apps: " & currentBranch(), True, "Any Apex Apps that were changed and exported by me, must be reinstalled now, since the VM has been reverted. ", Not iPatching And iAppChanges)
        'STASH-POP
        rebasing.addStep("Stash Pop: " & currentBranchLong, False, "Stash Pop if a Stash Save was used previously, and especially, if we are not also making a patch.", Not iPatching)
        'SNAPSHOT
        rebasing.addStep("Post-Rebase Snapshot", True, "Before creating new patches, snapshot the VM again.  Use this snapshot as a quick restore to point restest patches that have failed, on first execution.", iAppChanges Or iDBChanges)

        rebasing.Show()

        Do Until rebasing.isStarted
            Common.wait(1000)
        Loop


        'EXPORT-APPS-MINE
        If rebasing.toDoNextStep() Then
            'Export Apex to branch

            'Start the ApexAppExporter and wait until it closes.
            Dim GitPatcherChild As ApexAppExporter = New ApexAppExporter

        End If

        'STASH-SAVE
        If rebasing.toDoNextStep() Then
            'SMARTGEN
            MsgBox("Please logon to SmartGen and generate/spool objects", MsgBoxStyle.Exclamation, "SmartGen")

        End If

        'COMMIT
        If rebasing.toDoNextStep() Then
            'User chooses to commit, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
            If GitOp.IsDirty() Then
                Logger.Dbg("User chose to commit and the checkout is also dirty")

                'Committing changed files to GIT
                'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")
                Tortoise.Commit(Globals.getRepoPath, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no uncommitted changes.", True)

            End If
        Else
            'User chooses to NOT to commit, but commit anyway if there is at least 1 staged change
            'Committing changed files to GIT"
            If GitOp.ChangedFiles() > 0 Then
                Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                MsgBox("Files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                Tortoise.Commit(Globals.getRepoPath, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no uncommitted changes.", True)

            End If


        End If

        'STASH-SAVE
        If rebasing.toDoNextStep() Then
            'User chooses to StashSave, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
            If GitOp.IsDirty() Then
                Logger.Dbg("User chose to commit and the checkout is also dirty")

                'StashSave changes
                'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")
                Tortoise.StashSave(Globals.getRepoPath, "Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.", True)

                callStashPop = True

            End If
        Else
            'User chooses to NOT to StashSave, but commit anyway if there is at least 1 staged change
            'Committing changed files to GIT"
            If GitOp.ChangedFiles() > 0 Then
                Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                MsgBox("Files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                Tortoise.StashSave(Globals.getRepoPath, "Stash Save to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", True)

                callStashPop = True

            End If

        End If

        'SWITCH-MASTER
        If rebasing.toDoNextStep() Then
            'Switch to develop branch
            'GitBash.Switch(Globals.getRepoPath, iRebaseBranchOn)
            GitOp.SwitchBranch(iRebaseBranchOn)
        End If

        'PULL-MASTER
        If rebasing.toDoNextStep() Then
            'Pull from origin/develop
            GitOp.pullCurrentBranch()
        End If

        'TAG-A-MASTER
        If rebasing.toDoNextStep() Then
            'Tag the head
            l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please enter 2 digit numeric tag for next patch.", "Create Tag for next patch", l_tag_base)
            Dim l_tagA As String = currentBranchShort & "." & l_tag_base & "A"
            rebasing.updateStepDescription(4, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
            GitOp.createTagHead(l_tagA)

        End If

        'RETURN-FEATURE
        If rebasing.toDoNextStep() Then
            'Return to branch
            GitOp.SwitchBranch(currentBranchLong)
        End If

        'REBASE-FEATURE
        If rebasing.toDoNextStep() Then
            'Rebase branch
            Tortoise.Rebase(Globals.getRepoPath)
        End If

        'TAG-B-FEATURE
        If rebasing.toDoNextStep() Then
            'Tag Branch
            Dim l_tagB As String = currentBranchShort & "." & l_tag_base & "B"
            rebasing.updateStepDescription(7, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
            GitOp.createTagHead(l_tagB)

        End If

        'REVERT-VM
        If rebasing.toDoNextStep() Then
            'Revert VM
            MsgBox("Please create a snapshot of your current VM state, and then revert to a state prior the work about to be patched.", MsgBoxStyle.Exclamation, "Revert VM")

        End If

        'PATCHRUNNER-UNAPPLIED
        If rebasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied Patches
            Dim GitPatcherChild As PatchRunner = New PatchRunner("Unapplied")

        End If

        'IMPORT-APPS-QUEUED
        If rebasing.toDoNextStep() Then
            'Install queued Apex Apps.
            'Start the ApexAppInstaller and wait until it closes.
            Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller("Queued")

        End If

        'IMPORT-APPS-MINE
        If rebasing.toDoNextStep() Then
            'Install my Apex Apps.
            'Start the ApexAppInstaller and wait until it closes.
            Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller("All")

        End If

        'STASH-POP
        If rebasing.toDoNextStep(Not callStashPop, callStashPop) Then
            'Do we need to confirm this step?
            Tortoise.StashPop(Globals.getRepoPath, True)

        End If

        'SNAPSHOT-POST-REBASE
        If rebasing.toDoNextStep() Then
            'Post-Rebase Snapshot 
            MsgBox("Before creating new patches, snapshot the VM again.", MsgBoxStyle.Exclamation, "Post-Rebase Snapshot")

        End If

        'Finish
        rebasing.toDoNextStep()

        Return l_tag_base
    End Function
End Class
