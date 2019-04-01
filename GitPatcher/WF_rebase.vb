Imports LibGit2Sharp

Friend Class WF_rebase
    Shared Function rebaseBranch(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String) As String

        Dim tag_num_padding As Integer = 2
        Common.checkBranch(iBranchType)

        Dim l_tag_prefix As String = Nothing
        If iBranchType = "hotfix" Then
            l_tag_prefix = iDBtarget.Substring(0, 1)
        End If


        Dim currentBranchLong As String = GitOp.CurrentBranch()
        Dim currentBranchShort As String = Globals.currentBranch

        Dim rebasing As ProgressDialogue = New ProgressDialogue("Rebase branch " & currentBranchLong)

        Dim l_max_tag As Integer = 0


        For Each thisTag As Tag In GitOp.getTagList(Globals.currentBranch)
            Try
                Dim tag_no As String = Common.getLastSegment(thisTag.FriendlyName, ".").Substring(0, tag_num_padding)
                Try
                    If tag_no > l_max_tag Then
                        l_max_tag = tag_no
                    End If
                Catch
                End Try
            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox("Problem with formatting of tagname: " & thisTag.FriendlyName & "  This tag may need to be deleted.")
            End Try

        Next


            Dim l_tag_base As String = l_max_tag + 1
        l_tag_base = l_tag_prefix & l_tag_base.PadLeft(tag_num_padding, "0")

        rebasing.MdiParent = GitPatcher
        rebasing.addStep("Commit to Branch: " & currentBranchLong, False, "Commit or Revert to ensure the current branch [" & currentBranchShort & "] contains no staged changes.")
        rebasing.addStep("Stash Save: " & currentBranchLong, False, "Stash Save to ensure the current branch [" & currentBranchShort & "] contains no staged changes.")
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", True, "If you get an error concerning uncommitted changes.  Please resolve the changes and then RESTART this process to ensure the switch to " & iRebaseBranchOn & " branch is successful.")
        rebasing.addStep("Pull from Origin")
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & "." & l_tag_base & "A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & currentBranchShort & ".99A")
        rebasing.addStep("Return to branch: " & currentBranchLong, True)
        rebasing.addStep("Rebase Branch: " & currentBranchLong & " From Upstream:" & iRebaseBranchOn, True, "Please select the Upstream Branch:" & iRebaseBranchOn & " from the Tortoise Rebase Dialogue")
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & "." & l_tag_base & "B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.")
        rebasing.addStep("Revert your VM", True, "Before running patches, consider reverting to a VM snapshot prior to the development of your current work, or swapping to a unit test VM.")
        rebasing.addStep("Use PatchRunner to run Unapplied Patches", True)

        rebasing.addStep("Import any queued apps: " & currentBranch(), True, "Any Apex Apps that were included in a patch, must be reinstalled now. ")

        'rebasing.addStep("Import Apex from HEAD of branch: " & currentBranchLong, True, "Using the Apex2Git.  Please inspect new commits on the master branch to determine which apps to import.")
        'rebasing.addStep("Import Apex from HEAD of branch: " & currentBranchLong, True, "Using the Apex Import workflow")
        rebasing.addStep("Post-Rebase Snapshot", True, "Before creating new patches, snapshot the VM again.  Use this snapshot as a quick restore to point restest patches that have failed, on first execution.")

        rebasing.Show()

        Do Until rebasing.isStarted
            Common.wait(1000)
        Loop


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

        If rebasing.toDoNextStep() Then
            'User chooses to StashSave, but don't bother unless the checkout is also dirty (meaning there is at least 1 staged or unstaged change)
            If GitOp.IsDirty() Then
                Logger.Dbg("User chose to commit and the checkout is also dirty")

                'StashSave changes
                'MsgBox("Checkout is dirty, files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout is dirty")
                Tortoise.StashSave(Globals.getRepoPath, "Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.", True)

            End If
        Else
            'User chooses to NOT to StashSave, but commit anyway if there is at least 1 staged change
            'Committing changed files to GIT"
            If GitOp.ChangedFiles() > 0 Then
                Logger.Dbg("User chose NOT to commit but the checkout has staged changes")

                MsgBox("Files have been changed. Please stash, commit or revert changes before proceding", MsgBoxStyle.Exclamation, "Checkout has changes")
                Tortoise.StashSave(Globals.getRepoPath, "Stash Save to ensure the current branch [" & currentBranchShort & "] contains no staged changes.", True)

            End If

        End If


        If rebasing.toDoNextStep() Then
            'Switch to develop branch
            'GitBash.Switch(Globals.getRepoPath, iRebaseBranchOn)
            GitOp.SwitchBranch(iRebaseBranchOn)
        End If
        If rebasing.toDoNextStep() Then
            'Pull from origin/develop
            GitOp.pullCurrentBranch()
        End If

        If rebasing.toDoNextStep() Then
            'Tag the develop head
            l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please enter 2 digit numeric tag for next patch.", "Create Tag for next patch", l_tag_base)
            Dim l_tagA As String = currentBranchShort & "." & l_tag_base & "A"
            rebasing.updateStepDescription(4, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
            GitOp.createTagHead(l_tagA)

        End If


        If rebasing.toDoNextStep() Then
            'Return to branch
            GitOp.SwitchBranch(currentBranchLong)
        End If

        If rebasing.toDoNextStep() Then
            'Rebase branch
            Tortoise.Rebase(Globals.getRepoPath)
        End If

        If rebasing.toDoNextStep() Then
            'Tag Branch
            Dim l_tagB As String = currentBranchShort & "." & l_tag_base & "B"
            rebasing.updateStepDescription(7, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
            GitOp.createTagHead(l_tagB)

        End If

        If rebasing.toDoNextStep() Then
            'Revert VM
            MsgBox("Please create a snapshot of your current VM state, and then revert to a state prior the work about to be patched.", MsgBoxStyle.Exclamation, "Revert VM")

        End If


        If rebasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied Patches
            Dim newchildform As New PatchRunner("Unapplied")
            'newchildform.MdiParent = GitPatcher - cannot be attached to a parent when using newchildform.ShowDialog() 'ShowDialog - means wait.
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If

        If rebasing.toDoNextStep() Then
            'Install queued Apex Apps.
            Dim newchildform As New ApexAppInstaller("Queued")
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'ShowDialog - means wait.
        End If

        'If rebasing.toDoNextStep() Then
        '    'Import Apex from HEAD of branch
        '    MsgBox("Using the Apex2Git.  Please inspect new commits on the master branch to determine which apps to import.", MsgBoxStyle.Exclamation, "Import Apps into VM")

        '    'WF_Apex.ApexImportFromTag()

        'End If

        If rebasing.toDoNextStep() Then
            'Post-Rebase Snapshot 
            MsgBox("Before creating new patches, snapshot the VM again.", MsgBoxStyle.Exclamation, "Post-Rebase Snapshot")

        End If

        'Finish
        rebasing.toDoNextStep()

        Return l_tag_base
    End Function
End Class
