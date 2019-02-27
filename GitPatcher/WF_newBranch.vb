Friend Class WF_newBranch
    Shared Sub createNewBranch(iBranchType As String, iBranchFrom As String)

        Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch", "Create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & Globals.deriveFeatureCode() & "ISSUE_ID.")
        newFeature.MdiParent = GitPatcher
        newFeature.addStep("Switch to " & iBranchFrom & " branch")
        newFeature.addStep("Pull from Origin")
        newFeature.addStep("Create and switch to " & iBranchType & " branch")


        newFeature.Show()

        Do Until newFeature.isStarted
            Common.wait(1000)
        Loop

        If newFeature.toDoNextStep() Then
            'Switch to develop branch
            GitOp.SwitchBranch(iBranchFrom)
   

        End If

        If newFeature.toDoNextStep() Then
            'Pull from origin/develop
            GitOp.pullBranch(iBranchFrom)

        End If

        If newFeature.toDoNextStep() Then
            'Create and Switch to new branch
            Dim branchName As String = InputBox("Enter the Issue Id.", "Issue Id for new " & iBranchType & " Branch", Globals.getJira)
            Dim featureCode As String = InputBox("Feature Code", "Confirm Feature Code", Globals.deriveFeatureCode)
            Dim newBranch As String = iBranchType
            If Globals.getAppInFeature() = "Y" Then
                newBranch = newBranch & "/" & Globals.currentAppCode
            End If
            newBranch = newBranch & "/" & featureCode & branchName



            If Not String.IsNullOrEmpty(branchName) Then

                newFeature.updateTitle("Create new " & iBranchType & " branch:  " & branchName)
                newFeature.updateStepDescription(2, "Create and switch to " & iBranchType & " branch: " & newBranch)

                GitOp.createAndSwitchBranch(newBranch)

            End If

            newFeature.toDoNextStep()

        End If


    End Sub
End Class
