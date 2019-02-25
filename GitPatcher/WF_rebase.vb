﻿Friend Class WF_rebase
    Shared Function rebaseBranch(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String) As String

        Dim tag_no_padding As Integer = 2
        Common.checkBranch(iBranchType)

        Dim l_tag_prefix As String = Nothing
        If iBranchType = "hotfix" Then
            l_tag_prefix = iDBtarget.Substring(0, 1)
        End If


        Dim currentBranchLong As String = GitOp.currentBranch()
        Dim currentBranchShort As String = Globals.currentBranch

        Dim rebasing As ProgressDialogue = New ProgressDialogue("Rebase branch " & currentBranchLong)

        Dim l_max_tag As Integer = 0

        Dim tagnames As Collection = New Collection
        tagnames = GitOp.getTagList(tagnames, Globals.currentBranch)
        For Each tagname In tagnames
            Dim tag_no As String = Common.getLastSegment(tagname.ToString, ".").Substring(0, tag_no_padding)
            Try
                If tag_no > l_max_tag Then
                    l_max_tag = tag_no
                End If
            Catch
            End Try

        Next
        Dim l_tag_base As String = l_max_tag + 1
        l_tag_base = l_tag_prefix & l_tag_base.PadLeft(tag_no_padding, "0")

        rebasing.MdiParent = GitPatcher
        rebasing.addStep("Commit to Branch: " & currentBranchLong, True, "Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.")
        rebasing.addStep("Switch to " & iRebaseBranchOn & " branch", True, "If you get an error concerning uncommitted changes.  Please resolve the changes and then RESTART this process to ensure the switch to " & iRebaseBranchOn & " branch is successful.")
        rebasing.addStep("Pull from Origin")
        rebasing.addStep("Tag " & iRebaseBranchOn & " HEAD with " & currentBranchShort & "." & l_tag_base & "A", True, "Will Tag the " & iRebaseBranchOn & " head commit for patch comparisons. Asks for the tag value in format 99, but creates tag " & currentBranchShort & ".99A")
        rebasing.addStep("Return to branch: " & currentBranchLong)
        rebasing.addStep("Rebase Branch: " & currentBranchLong & " From Upstream:" & iRebaseBranchOn, True, "Please select the Upstream Branch:" & iRebaseBranchOn & " from the Tortoise Rebase Dialogue")
        rebasing.addStep("Tag Branch: " & currentBranchLong & " HEAD with " & currentBranchShort & "." & l_tag_base & "B", True, "Will Tag the " & iBranchType & " head commit for patch comparisons. Creates tag " & currentBranchShort & ".99B.")
        rebasing.addStep("Revert your VM", True, "Before running patches, consider reverting to a VM snapshot prior to the development of your current work, or swapping to a unit test VM.")
        rebasing.addStep("Use PatchRunner to run Unapplied Patches", True)
        rebasing.addStep("Import Apex from HEAD of branch: " & currentBranchLong, True, "Using the Apex Import workflow")
        rebasing.addStep("Post-Rebase Snapshot", True, "Before creating new patches, snapshot the VM again.  Use this snapshot as a quick restore to point restest patches that have failed, on first execution.")

        rebasing.Show()

        Do Until rebasing.isStarted
            Common.wait(1000)
        Loop


        If rebasing.toDoNextStep() Then
            'Committing changed files to GIT"
            Tortoise.Commit(Globals.getRepoPath, "CANCEL IF NOT NEEDED: Ensure the current branch [" & currentBranchShort & "] is free of uncommitted changes.", True)
        End If

        If rebasing.toDoNextStep() Then
            'Switch to develop branch
            'GitBash.Switch(Globals.getRepoPath, iRebaseBranchOn)
            GitOp.switchBranch(iRebaseBranchOn)
        End If
        If rebasing.toDoNextStep() Then
            'Pull from origin/develop
            GitOp.pullCurrentBranch()
        End If

        If rebasing.toDoNextStep() Then
            'Tag the develop head
            l_tag_base = InputBox("Tagging current HEAD of " & iRebaseBranchOn & ".  Please enter 2 digit numeric tag for next patch.", "Create Tag for next patch", l_tag_base)
            Dim l_tagA As String = currentBranchShort & "." & l_tag_base & "A"
            rebasing.updateStepDescription(3, "Tag " & iRebaseBranchOn & " HEAD with " & l_tagA)
            GitOp.createTag(l_tagA)

        End If


        If rebasing.toDoNextStep() Then
            'Return to branch
            GitOp.switchBranch(currentBranchLong)
        End If

        If rebasing.toDoNextStep() Then
            'Rebase branch
            Tortoise.Rebase(Globals.getRepoPath)
        End If

        If rebasing.toDoNextStep() Then
            'Tag Branch
            Dim l_tagB As String = currentBranchShort & "." & l_tag_base & "B"
            rebasing.updateStepDescription(6, "Tag Branch: " & currentBranchLong & " HEAD with " & l_tagB)
            GitOp.createTag(l_tagB)

        End If

        If rebasing.toDoNextStep() Then
            'Revert VM
            MsgBox("Please create a snapshot of your current VM state, and then revert to a state prior the work about to be patched.", MsgBoxStyle.Exclamation, "Revert VM")

        End If


        If rebasing.toDoNextStep() Then
            'Use PatchRunner to run Unapplied Patches
            Dim newchildform As New PatchRunner("Unapplied")
            'newchildform.MdiParent = GitPatcher
            newchildform.ShowDialog() 'NEED TO WAIT HERE!!

        End If

        If rebasing.toDoNextStep() Then
            'Import Apex from HEAD of branch
            WF_Apex.ApexImportFromTag()

        End If

        If rebasing.toDoNextStep() Then
            'Post-Rebase Snapshot 
            MsgBox("Before creating new patches, snapshot the VM again.", MsgBoxStyle.Exclamation, "Post-Rebase Snapshot")

        End If

        'Finish
        rebasing.toDoNextStep()

        Return l_tag_base
    End Function
End Class
