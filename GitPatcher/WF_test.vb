Friend Class WF_test

    Shared Sub test()
        Dim testWorkflow As ProgressDialogue = New ProgressDialogue("test variable workflow")
        testWorkflow.MdiParent = GitPatcher
        testWorkflow.addStep("Choose a tag to import from", True, "test notes", True)
        testWorkflow.addStep("Checkout the tag", False, "test notes", False)
        testWorkflow.addStep("If tag not like ", True, "test notes", True)
        testWorkflow.addStep("Import Apex", False, "test notes", False)
        testWorkflow.addStep("Return to branch:", False, "test notes", True)


        testWorkflow.Show()



        Do Until testWorkflow.isStarted
            Common.wait(1000)
        Loop

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 0")
            testWorkflow.updateStepDescription(2, "Middle one updated and unchecked")
            testWorkflow.updateCheckListChecked(2, False)

        End If
        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 1")

        End If

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 2")

        End If
        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 3")

        End If

        If testWorkflow.toDoNextStep(False, True) Then
            MsgBox("doing 4")

        End If

        'Finish
        testWorkflow.toDoNextStep()  'USE THIS METHOD

        'testWorkflow.stopAndClose() 'DOES NOT WORK.
    End Sub

End Class
