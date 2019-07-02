Friend Class WF_newBranch

    Shared Sub createNewBranch(iBranchType As String, iBranchFrom As String, Optional ByRef ipull As Boolean = True, Optional ByRef iBranchName As String = "")

        Dim newFeature As ProgressDialogue = New ProgressDialogue("Create new " & iBranchType & " branch", "Create a new " & iBranchType & " Branch with the standardised naming " & iBranchType & "/" & (Globals.deriveFeatureCode & "/").TrimStart("/") & "ISSUE_ID.")
        newFeature.MdiParent = GitPatcher
        'SWITCH-TO-MASTER
        newFeature.addStep("Switch to " & iBranchFrom & " branch", Globals.currentLongBranch <> iBranchFrom, "Switch automatically to " & iBranchFrom, True)
        ''SUB-FLOW:FIND-PATCH-VERSION-RELEASE
        'newFeature.addStep("Switch to release branch", Globals.currentBranch <> iBranchFrom, "Find existing, or create a new Patch Version release branch", iBranchFrom = "release")
        'PULL-CURRENT-BRANCH - LGIT - automatic
        newFeature.addStep("Pull " & iBranchFrom & " branch from Origin", ipull, "Ensure" & iBranchFrom & " branch is upto date.", True)
        'SUB-FLOW:RESYNCH-VM
        newFeature.addStep("Release patches to VM")
        'CREATE-BRANCH
        newFeature.addStep("Create and switch to " & iBranchType & " branch")


        newFeature.Show()

        Do Until newFeature.isStarted
            Common.Wait(1000)
        Loop

        Try

            'SWITCH-TO-MASTER
            If newFeature.toDoNextStep() Then
                'Switch to iBranchFrom branch
                GitOp.SwitchBranch(iBranchFrom)
            End If

            ''SUB-FLOW:FIND-PATCH-VERSION-RELEASE
            'If newFeature.toDoNextStep() Then
            '    'Find existing or create a new patch version release branch
            '    WF_versions.findPatchVersionRelease("patch", "VM", False)
            '    'Release to VM
            '    WF_release.releaseTo("VM", GitOp.CurrentBranch(), iBranchType, False)

            'End If

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
                WF_release.releaseTo("VM", Globals.currentLongBranch, iBranchType, False)

            End If


            'CREATE-BRANCH

            If newFeature.toDoNextStep() Then
                'Create and Switch to new branch
                Dim branchName As String = Nothing
                If String.IsNullOrEmpty(iBranchName) Then
                    branchName = InputBox("Enter the Jira Number for the new " & iBranchType, "Jira Number for new " & iBranchType & " Branch", Globals.getJira).ToUpper() 'Ensure UPPERCASE
                Else
                    'branchName is passed when rebasing a hotfix as a new feature
                    branchName = iBranchName
                End If

                If Not String.IsNullOrEmpty(branchName) Then

                    If iBranchType = "hotfix" And Not branchName Like "*-HF" Then
                        'Add -HF suffix
                        'branchName = Replace(branchName, "-HF", "") & "-HF" 'hotfix branches will have the suffix HF added to the JiraId
                        branchName = branchName & "-HF"
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

                        newFeature.updateTitle("Create new " & iBranchType & " branch:  " & branchName)
                        newFeature.updateStepDescription(1, "Create and switch to " & iBranchType & " branch: " & newBranch)

                        GitOp.createAndSwitchBranch(newBranch)

                    End If



                End If

                'Done
                newFeature.toDoNextStep()

        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            newFeature.setToCompleted()
            newFeature.Close()
        End Try


    End Sub

    Shared Sub createHotFixBranch()

        Dim umbrella As ProgressDialogue = New ProgressDialogue("Find Patch Version Release and Create Hotfix branch")
        Dim branchType As String = "hotfix"
        Dim baseBranch As String = Nothing

        umbrella.MdiParent = GitPatcher
        'SUB-FLOW:FIND-PATCH-VERSION-RELEASE
        umbrella.addStep("Find or Create Patch Version Release branch", True, "Find an existing Patch Version Release branch, or create one", True)

        'SUB-FLOW:CREATE-BRANCH
        umbrella.addStep("Create and switch to " & branchType & " branch")

        umbrella.Show()

        Do Until umbrella.isStarted
            Common.Wait(1000)
        Loop

        Try

            'SUB-FLOW:FIND-PATCH-VERSION-RELEASE
            If umbrella.toDoNextStep() Then
                'Find existing or create a new patch version release branch
                WF_versions.findPatchVersionRelease("patch", "VM", True)
            End If

            ''SUB-FLOW:RELEASE-TO-VM
            'If umbrella.toDoNextStep() Then
            '    'Release to VM
            '    WF_release.releaseTo("VM", GitOp.CurrentBranch(), branchType, False)

            'End If

            'CREATE-BRANCH

            If umbrella.toDoNextStep() Then
                'Create and Switch to new branch
                baseBranch = Globals.currentLongBranch
                createNewBranch(branchType, baseBranch, False)

            End If

            'Done
            umbrella.toDoNextStep()
        Catch ex As Exception
            'Finish workflow if an error occurs
            MsgBox(ex.Message)
            umbrella.setToCompleted()
            umbrella.Close()
        End Try

    End Sub


End Class
