Friend Class WF_newBranch
    Shared Sub createNewBranch(iBranchType As String, iBranchFrom As String)

        Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch", "Create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & (Globals.deriveFeatureCode & "/").TrimStart("/") & "ISSUE_ID.")
        newFeature.MdiParent = GitPatcher
        newFeature.addStep("Switch to " & iBranchFrom & " branch", Globals.currentBranch <> iBranchFrom)
        'newFeature.addStep("Pull from Origin")
        newFeature.addStep("Release patches to VM")
        newFeature.addStep("Create and switch to " & iBranchType & " branch")


        newFeature.Show()

        Do Until newFeature.isStarted
            Common.wait(1000)
        Loop

        If newFeature.toDoNextStep() Then
            'Switch to develop branch
            GitOp.SwitchBranch(iBranchFrom)
   

        End If

        'If newFeature.toDoNextStep() Then
        '    'Pull from origin/develop
        '    GitOp.pullBranch(iBranchFrom)
        'End If

        If newFeature.toDoNextStep() Then
            'Release to VM
            WF_release.releaseTo("VM")

        End If

        If newFeature.toDoNextStep() Then
            'Create and Switch to new branch
            Dim branchName As String = InputBox("Enter the Issue Id.", "Issue Id for new " & iBranchType & " Branch", Globals.getJira).ToUpper 'Ensure UPPERCASE
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

                newFeature.toDoNextStep()

            End If


    End Sub
End Class
