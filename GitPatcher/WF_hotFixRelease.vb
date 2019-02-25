Friend Class WF_hotFixRelease
    Shared Sub hotFixRelease(hotFixItems As ToolStripComboBox)
        Common.checkBranch("hotfix")
        Dim patchThisDB As Boolean = False
        Dim multiHotFix As ProgressDialogue = New ProgressDialogue("Multi HotFix Patch for " & hotFixItems.SelectedItem & " Downwards")

        multiHotFix.MdiParent = GitPatcher

        For i = 0 To hotFixItems.Items.Count - 1

            If hotFixItems.Items(i) = hotFixItems.SelectedItem Then
                patchThisDB = True
            End If

            multiHotFix.addStep("Create a HotFix Patch for DB : " & hotFixItems.Items(i), patchThisDB)

        Next

        multiHotFix.Show()

        Do Until multiHotFix.isStarted
            Common.wait(1000)
        Loop

        For i = 0 To hotFixItems.Items.Count - 1

            If multiHotFix.toDoNextStep() Then
                WF_createPatch.createPatchProcess("hotfix", hotFixItems.Items(i), Globals.deriveHotfixBranch(hotFixItems.Items(i)))
            End If

        Next


        'Finish
        multiHotFix.toDoNextStep()
    End Sub
End Class
