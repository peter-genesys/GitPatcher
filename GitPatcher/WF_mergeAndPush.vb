Friend Class WF_mergeAndPush
    Shared Sub mergeAndPushBranch(iBranchType As String, iBranchTo As String)
        'THIS ONE
        Common.checkBranch(iBranchType)
        Dim currentBranch As String = GitOp.CurrentBranch()

        Dim mergeAndPush As ProgressDialogue = New ProgressDialogue("Merge and Push branch:  " & currentBranch)
        mergeAndPush.MdiParent = GitPatcher
        mergeAndPush.addStep("Switch to " & iBranchTo & " branch")
        mergeAndPush.addStep("Pull from Origin")
        mergeAndPush.addStep("Merge from branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        mergeAndPush.addStep("Commit - incase of merge conflict")
        mergeAndPush.addStep("Push to Origin")
        mergeAndPush.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/" & iBranchTo & "'.  " _
                                 & "If NOT, then the push FAILED. Your " & iBranchTo & " branch is now out of date, so your merged files could be stale. " _
                                 & "In this situation, it is safest to perform a Rebase and then restart the Merge process to ensure you are pushing the lastest merged files. ")
        mergeAndPush.addStep("Return to branch: " & currentBranch)

        mergeAndPush.Show()

        Do Until mergeAndPush.isStarted
            Common.wait(1000)
        Loop

        If mergeAndPush.toDoNextStep() Then
            'Switch to develop branch
            GitOp.SwitchBranch(iBranchTo)


        End If

        If mergeAndPush.toDoNextStep() Then
            'Pull from origin/develop
            'GitBash.Pull(Globals.currentRepo, "origin", iBranchTo)
            Tortoise.Pull(Globals.getRepoPath)

        End If

        If mergeAndPush.toDoNextStep() Then
            'Merge from Feature branch
            'TortoiseMerge(Globals.currentRepo, currentBranch)
            Tortoise.Merge(Globals.getRepoPath)
        End If

        If mergeAndPush.toDoNextStep() Then
            'Commit - incase of merge conflict
            Tortoise.Commit(Globals.getRepoPath, "Merged " & currentBranch & " [CANCEL IF NO MERGE CONFLICTS]")


        End If

        If mergeAndPush.toDoNextStep() Then
            'Push to origin/develop 
            GitOp.pushBranch(iBranchTo)


        End If


        If mergeAndPush.toDoNextStep() Then
            'Synch command to verfiy that Push was successful.
            Tortoise.Sync(Globals.getRepoPath)
        End If

        If mergeAndPush.toDoNextStep() Then
            'Return to branch
            'GitSharpFascade.switchBranch(Globals.currentRepo, currentBranch)
            GitOp.SwitchBranch(currentBranch)

        End If

        mergeAndPush.toDoNextStep()

    End Sub
End Class
