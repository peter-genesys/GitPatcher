Friend Class WF_test

    Shared Sub test()
        Dim testWorkflow As ProgressDialogue = New ProgressDialogue("test variable workflow")
        testWorkflow.MdiParent = GitPatcher
        testWorkflow.addStep("Choose a tag to import from")
        testWorkflow.addStep("Checkout the tag", False)
        testWorkflow.addStep("If tag not like ")
        testWorkflow.addStep("Import Apex", False)
        testWorkflow.addStep("Return to branch:")


        testWorkflow.Show()

        Do Until testWorkflow.isStarted
            Common.wait(1000)
        Loop

        If testWorkflow.toDoNextStep() Then
            MsgBox("doing 0")

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

        ' If testWorkflow.toDoNextStep() Then
        '     MsgBox("doing 4")
        '
        ' End If
        '

        'testWorkflow.toDoNextStep()

        testWorkflow.stopAndClose()
    End Sub

End Class
