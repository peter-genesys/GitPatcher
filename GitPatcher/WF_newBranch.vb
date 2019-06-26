Friend Class WF_newBranch
    Shared Sub createNewBranch(iBranchType As String, iBranchFrom As String)

        Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch", "Create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & (Globals.deriveFeatureCode & "/").TrimStart("/") & "ISSUE_ID.")
        newFeature.MdiParent = GitPatcher
        'SWITCH-TO-MASTER
        newFeature.addStep("Switch to " & iBranchFrom & " branch", Globals.currentBranch <> iBranchFrom, "Switch automatically to " & iBranchFrom, iBranchFrom = "master")
        'SWITCH-TO-CURRENT-RELEASE   'TGIT - Let user choose any branch. Or could create a dialog with a list of release/ branches.
        newFeature.addStep("Switch to current " & iBranchFrom & " branch", Globals.currentBranch <> iBranchFrom,
                           "Switch manually to " & iBranchFrom &
                           "Please choose the latest " & Globals.currentAppCode & " release branch", iBranchType = "hotfix")
        'PULL-CURRENT-BRANCH - LGIT - automatic
        newFeature.addStep("Pull " & iBranchFrom & " branch from Origin", True, "Ensure" & iBranchFrom & " branch is upto date.")
        'SUB-FLOW:RESYNCH-VM
        newFeature.addStep("Release patches to VM")
        'CREATE-BRANCH
        newFeature.addStep("Create and switch to " & iBranchType & " branch")


        newFeature.Show()

        Do Until newFeature.isStarted
            Common.wait(1000)
        Loop

        'SWITCH-TO-MASTER
        If newFeature.toDoNextStep() Then
            'Switch to iBranchFrom branch
            GitOp.SwitchBranch(iBranchFrom)
        End If

        'SUB-FLOW:FIND-PATCH-VERSION-RELEASE
        If newFeature.toDoNextStep() Then
            'Find existing or create a new patch verion release branch
            WH_versions.findPatchVersion("patch", "VM", False)
            'Release to VM
            WF_release.releaseTo("VM", GitOp.CurrentBranch(), iBranchType, False)

        End If

        ''SWITCH-TO-CURRENT-RELEASE   
        'If newFeature.toDoNextStep() Then
        '    'Switch to current release branch
        '    Tortoise.Switch(Globals.getRepoPath)

        '    Dim currentReleaseBranch As String = GitOp.CurrentBranch()

        '    Dim lrelease As String = Common.getFirstSegment(currentReleaseBranch, "/")
        '    If lrelease <> "release" Then
        '        Throw New System.Exception("Current branch is NOT a release branch: " & currentReleaseBranch & "  Aborting Operation.")
        '    End If

        '    'This validates against the current app.  Alternatively i could use this to change the current app.
        '    Dim lAppCode As String = Common.getNthSegment(currentReleaseBranch, "/", 2)
        '    If lAppCode <> Globals.currentAppCode Then
        '        Throw New System.Exception("Current release branch is NOT for the current app " & Globals.currentAppCode & ": " & currentReleaseBranch & "  Aborting Operation.")
        '    End If

        'End If

        'PULL-CURRENT-BRANCH - LGIT - automatic
        If newFeature.toDoNextStep() Then
            'Pull 
            GitOp.pullBranch("") 'LGIT does not use the branch param. BGIT does.
        End If

        'SUB-FLOW:RESYNCH-VM
        If newFeature.toDoNextStep() Then
            'Release to VM
            WF_release.releaseTo("VM", GitOp.CurrentBranch(), iBranchType, False)

        End If
        'CREATE-BRANCH

        If newFeature.toDoNextStep() Then
            'Create and Switch to new branch
            Dim branchName As String = InputBox("Enter the Issue Id.", "Issue Id for new " & iBranchType & " Branch", Globals.getJira).ToUpper 'Ensure UPPERCASE
            If iBranchType = "hotfix" Then
                branchName = branchName & "-HF" 'hotfix branches will have the suffix HF added to the JiraId
            End If

            Dim newBranch As String = iBranchType

            'Derive the feature code from app and org codes.
            Dim featureCode As String = Globals.deriveFeatureCode
            If Not String.IsNullOrEmpty(featureCode) Then
                'Confirm the feature code if not null
                featureCode = InputBox("Feature Code", "Confirm Feature Code", featureCode)
            End If


            If String.IsNullOrEmpty(featureCode) Then
                'No feature code needed.
                newBranch = newBranch & "/" & branchName
            Else
                'Use the feature code if not null
                newBranch = newBranch & "/" & featureCode & "/" & branchName
            End If


            If Not String.IsNullOrEmpty(branchName) Then

                newFeature.updateTitle("Create new " & iBranchType & " branch:  " & branchName)
                newFeature.updateStepDescription(2, "Create and switch to " & iBranchType & " branch: " & newBranch)

                GitOp.createAndSwitchBranch(newBranch)

            End If



        End If

        'Done
        newFeature.toDoNextStep()


    End Sub
End Class
