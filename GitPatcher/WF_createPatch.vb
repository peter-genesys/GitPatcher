Friend Class WF_createPatch
    Shared Sub createPatchProcess(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)

        Common.checkBranch(iBranchType)
        Dim l_tag_base As String = Nothing

        Dim currentBranch As String = Globals.currentLongBranch()
        Dim createPatchProgress As ProgressDialogue = New ProgressDialogue("Create " & iBranchType & " Patch")
        createPatchProgress.MdiParent = GitPatcher
        createPatchProgress.addStep("Export Apex Apps to " & iBranchType & " branch: " & currentBranch, False, "Using Apex2Git or the Apex Export workflow")
        createPatchProgress.addStep("Use SmartGen to spool changed config data: " & currentBranch, False,
                                    "Did I change any config data?  " &
                                    "Do I need to spool any table changes or generate related objects?  " &
                                    "If so, logon to SmartGen, generate and/or spool code. " &
                                    "Use db-spooler to spool the objects to the local filesystem. " &
                                    "Then commit it too.")
        ' "Regenerate: Menu (new pages, menu changes), Security (new pages, security changes), Tapis (table or view column changes), Domains (new or changed tables or views, new domains or domain ussage changed)")
        createPatchProgress.addStep("Rebase branch: " & currentBranch & " on branch: " & iRebaseBranchOn, True, "Using the Rebase workflow")
        createPatchProgress.addStep("Review tags on Branch: " & currentBranch)
        createPatchProgress.addStep("Create edit, test", True, "Now is a great time to smoke test my work before i commit the patch.")
        createPatchProgress.addStep("Commit to Branch: " & currentBranch)
        createPatchProgress.addStep("Switch to " & iRebaseBranchOn & " branch")
        createPatchProgress.addStep("Merge from Branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        createPatchProgress.addStep("Push to Origin", True, "If at this stage there is an error because your " & iRebaseBranchOn & " branch is out of date, then you must restart the process to ensure you are patching the lastest merged files.")
        createPatchProgress.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/" & iRebaseBranchOn & "'.  " _
                                 & "If NOT, then the push FAILED. Your " & iRebaseBranchOn & " branch is now out of date, so is your rebase from it, and any patches COULD BE stale. " _
                                 & "In this situation, it is safest to restart the Create Patch process to ensure you are patching the lastest merged files. ")
        createPatchProgress.addStep("Release to " & iDBtarget, True)
        createPatchProgress.addStep("Return to Branch: " & currentBranch)
        createPatchProgress.addStep("Snapshot VM", True, "Create a snapshot of your current VM state, to use as your next restore point.  I label mine with the patch_name of the last applied patch.")
        createPatchProgress.Show()


        Do Until createPatchProgress.isStarted
            Common.wait(1000)
        Loop

        If createPatchProgress.toDoNextStep() Then
            'Export Apex to branch
            'WF_Apex.ApexExportCommit()
            MsgBox("Please start Apex2Gen from the tools dir, to export the Apex Apps", MsgBoxStyle.Exclamation, "Apex2Gen")

        End If

        If createPatchProgress.toDoNextStep() Then
            'SMARTGEN
            MsgBox("Please logon to SmartGen and generate/spool objects", MsgBoxStyle.Exclamation, "SmartGen")

        End If

        If createPatchProgress.toDoNextStep() Then
            'Rebase branch
            l_tag_base = WF_rebase.rebaseBranch(iBranchType, iDBtarget, iRebaseBranchOn)

        End If


        If createPatchProgress.toDoNextStep() Then
            'Review tags on the branch
            Tortoise.Log(Globals.getRepoPath)
        End If


        If createPatchProgress.toDoNextStep() Then

            Dim Wizard As New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn, l_tag_base)
            'newchildform.MdiParent = GitPatcher
            Wizard.ShowDialog() 'NEED TO WAIT HERE!!


        End If


        If createPatchProgress.toDoNextStep() Then
            'MsgBox("Now is a great time to smoke test my work before i commit the patch.", MsgBoxStyle.Information, "Smoke Test")

            'Committing changed files to GIT"
            Tortoise.Commit(Globals.getRepoPath, "Commit any patches, or changes made while patching, that you've not yet committed", True)
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
            MsgBox("Create a snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")

        End If

        'Finish
        createPatchProgress.toDoNextStep()


    End Sub
End Class
