Friend Class WF_createPatch
    Shared Sub createPatchProcess(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)

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
        createPatchProgress.addStep("Rebase branch: " & currentBranch & " on branch: " & iRebaseBranchOn, True, "Using the Rebase workflow")
        createPatchProgress.addStep("Review tags on Branch: " & currentBranch)
        createPatchProgress.addStep("Create edit, test", True, "Now is a great time to smoke test my work before i commit the patch.")
        createPatchProgress.addStep("Commit to Branch: " & currentBranch, False)

        createPatchProgress.addStep("Import any queued apps: " & currentBranch, True, "Any Apex Apps that were included in a patch, must be reinstalled now. ")

        createPatchProgress.addStep("Switch to " & iRebaseBranchOn & " branch", True)
        createPatchProgress.addStep("Merge from Branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        createPatchProgress.addStep("Push to Origin", True, "If at this stage there is an error because your " & iRebaseBranchOn & " branch is out of date, then you must restart the process to ensure you are patching the lastest merged files.")
        createPatchProgress.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/" & iRebaseBranchOn & "'.  " _
                                 & "If NOT, then the push FAILED. Your " & iRebaseBranchOn & " branch is now out of date, so is your rebase from it, and any patches COULD BE stale. " _
                                 & "In this situation, it is safest to restart the Create Patch process to ensure you are patching the lastest merged files. ")
        createPatchProgress.addStep("Release to " & iDBtarget, True)
        createPatchProgress.addStep("Return to Branch: " & currentBranch, True)
        createPatchProgress.addStep("Clean VM Snapshot", True, "Create a clean snapshot of your current VM state, to use as your next restore point.")
        createPatchProgress.Show()



        Do Until createPatchProgress.isStarted
            Common.wait(1000)
        Loop

        Try

            If createPatchProgress.toDoNextStep() Then
                'Rebase branch
                l_tag_base = WF_rebase.rebaseBranch(iBranchType, iDBtarget, iRebaseBranchOn, True, True, True)
                If String.IsNullOrEmpty(l_tag_base) Then
                    Throw New Exception("Invalid tag - cancelling patch.")
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

            If createPatchProgress.toDoNextStep() Then
                'Run your app changes
                'Start the ApexAppInstaller and wait until it closes.
                Dim GitPatcherChild As ApexAppInstaller = New ApexAppInstaller("Queued")

                'Dim newchildform As New ApexAppInstaller("Queued")
                'newchildform.MdiParent = GitPatcher
                'newchildform.ShowDialog() 'ShowDialog - means wait.
            End If


            If createPatchProgress.toDoNextStep() Then
                'switch
                GitOp.SwitchBranch(iRebaseBranchOn)

            End If

            If createPatchProgress.toDoNextStep() Then
                'Merge from Feature branch

                'Tortoise.Merge(Globals.getRepoPath)
                GitOp.Merge(currentBranch)

            End If

            If createPatchProgress.toDoNextStep() Then
                'Push to origin/develop 
                GitOp.pushBranch(iRebaseBranchOn)
            End If

            If createPatchProgress.toDoNextStep() Then
                'Synch command to verfiy that Push was successful.
                Tortoise.Sync(Globals.getRepoPath)
            End If

            If createPatchProgress.toDoNextStep() Then
                'Release to DB Target
                WF_release.releaseTo(iDBtarget, iBranchType)
            End If

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
