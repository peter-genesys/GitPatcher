Public Class ProgressDialogue
    Private waitTime As Integer = 200
    Private storedProcessSteps As ProcessStep()
    Private nextStep As Integer = 0
    Private activeStep As Integer = -1
    Private started As Boolean = False
    Private percentComplete As Integer = 0

    ' Public Class SkipStepException : Inherits ApplicationException
    '     Public Sub New(ByVal message As String)
    '         MyBase.New(message)
    '     End Sub
    ' End Class
    '
    ' Public Class EndWorkflowException : Inherits ApplicationException
    '     Public Sub New(ByVal message As String)
    '         MyBase.New(message)
    '     End Sub
    ' End Class

    Public Sub New(ByVal progressTitle As String, Optional ByVal processMessage As String = "")
        Me.Location = New Point(0, 0)
        InitializeComponent()
        Me.Text = progressTitle
        Me.NotesTextBox.Text = processMessage & Environment.NewLine & Environment.NewLine & _
                               "Please review steps in this workflow, deselecting any NOT required." & Environment.NewLine & _
                               Environment.NewLine & _
                               "Press Start to begin."
        CheckAllCheckBox.Checked = True

    End Sub

    Private Function calcPercentComplete(theStep) As Integer
        If nextStep = 0 Then
            percentComplete = 0
        Else
            percentComplete = (theStep) / nextStep * 100
        End If

        Return percentComplete
    End Function


    Public Sub updateTitle(ByVal progressTitle As String)
        Me.Text = progressTitle
    End Sub

    Public Function isStarted() As Boolean
        Return started
    End Function


    Public Sub addStep(ByVal description As String, Optional ByVal checked As Boolean = True, Optional ByVal notes As String = "", Optional ByVal enabled As Boolean = True, Optional ByVal code As String = "")
        'ProgressCheckedListBox.Items will contain items 0-x 
        'Corresponding to the enabled steps in storedProcessSteps

        Dim index As Integer = -1
        If enabled Then
            'This step is enabled so add it to the checkboxlist
            ProgressCheckedListBox.Items.Add(description)
            index = ProgressCheckedListBox.Items.Count - 1
            ProgressCheckedListBox.SetItemChecked(index, checked) 'set checked on latest item.
        End If

        'either way add it to the list of steps.  If enabled will also have an index > -1
        ReDim Preserve storedProcessSteps(nextStep)
        Dim aStep As ProcessStep = New ProcessStep(description, notes, enabled, index)
        storedProcessSteps(nextStep) = aStep
        nextStep = nextStep + 1

    End Sub

    Public Sub updateCheckListChecked(ByVal stepNo As Integer, ByVal checked As Boolean)

        If storedProcessSteps(stepNo).enabled() Then
            'look up the index from the stored steps
            Dim index As Integer = storedProcessSteps(stepNo).index
            Me.ProgressCheckedListBox.SetItemChecked(index, checked)
        End If

    End Sub

    Private Sub updateCheckListdescription(ByVal stepNo As Integer, ByVal description As String)
        'Update the description ,if the step is enabled.
        If storedProcessSteps(stepNo).enabled Then
            'look up the index from the stored steps
            Dim index As Integer = storedProcessSteps(stepNo).index
            Me.ProgressCheckedListBox.Items(index) = description
        End If

    End Sub

    Private Sub updateCheckListSelected(ByVal stepNo As Integer, Optional ByVal selected As Boolean = True)
        'Set the step to selected, if the step is enabled.
        If storedProcessSteps(stepNo).enabled Then
            'look up the index from the stored steps
            Dim index As Integer = storedProcessSteps(stepNo).index
            ProgressCheckedListBox.SetSelected(index, selected)
        End If

    End Sub



    Private Sub updateStepStatus(ByVal stepNo As Integer, ByVal status As String)
        Dim lstatus As String = status

        If stepNo > -1 And stepNo <= storedProcessSteps.GetUpperBound(0) Then

            If status = "Doing" Then
                ProgressBar.Value = calcPercentComplete(stepNo)
            Else
                ProgressBar.Value = calcPercentComplete(stepNo + 1)
            End If
 
            If String.IsNullOrEmpty(storedProcessSteps(stepNo).status) And status = "Done" Then
                lstatus = "Skipped"
            End If

            If storedProcessSteps(stepNo).status <> "Skipped" Then
                storedProcessSteps(stepNo).setStatus(lstatus)
                updateCheckListdescription(stepNo, storedProcessSteps(stepNo).description & " - " & storedProcessSteps(stepNo).status)
            End If

            pauseToRefreshProgressBar()

        End If

    End Sub

    Public Sub updateStepDescription(ByVal stepNo As Integer, ByVal description As String)

        storedProcessSteps(stepNo).setDescription(description)
        updateCheckListdescription(stepNo, storedProcessSteps(stepNo).description & " - " & storedProcessSteps(stepNo).status)

    End Sub

    ' Loops for a specificied period of time (milliseconds)
    Private Sub pauseToRefreshProgressBar()
        If percentComplete = 100 Then
            StartButton.Text = "Done"
            Common.wait(waitTime)
            Me.Close()
        Else
            Common.wait(waitTime)
        End If


    End Sub

 

    Public Function toDoStep(gotoStep As Integer) As Boolean

        'Dim last_active_step As Integer = activeStep
        'updateStep(activeStep, "Done")

        'Progress to the new step
        activeStep = gotoStep

        'Conclude preceding step
        'For i As Integer = last_active_step To activeStep - 1
        'updateStepStatus(i, "Done")
        'Next

        'If gotoStep <= storedProcessSteps.GetUpperBound(0) - 1 Then
        Try 'activeStep >= 0 And activeStep <= storedProcessSteps.GetUpperBound(0) 
            If storedProcessSteps(activeStep).enabled Then

                If ProgressCheckedListBox.CheckedIndices.Contains(storedProcessSteps(activeStep).index) Then
                    updateCheckListSelected(activeStep)
                    updateStepStatus(activeStep, "Doing")
                    Return True
                Else
                    updateStepStatus(activeStep, "Skipped")
                    Return False
                End If
            Else
                'Me.Close()
                Return False

            End If
        Catch ex As System.IndexOutOfRangeException
            Logger.Dbg("Steps completed.")
            Logger.Note("activeStep", activeStep)
            Return False

        End Try


    End Function

    Public Function toDoNextStep(Optional ByVal forceSkip As Boolean = False, Optional ByVal forceDo As Boolean = False) As Boolean

        'skip this step if the form is already closed.
        If Me.IsDisposed Then
            Return False
        End If

        'Find the next enabled step
        Dim l_gotoStep As Integer = activeStep 'Start with current step

        ''Keep incrementing the step until we find an enabled step, or run out of steps.
        'Try
        '    Do
        '        'Set this step to Done (whether or not it is enabled)
        '        updateStepStatus(l_gotoStep, "Done")
        '        'Progress to next step
        '        l_gotoStep = l_gotoStep + 1
        '        'Terminates if last step or enabled step
        '    Loop Until l_gotoStep >= storedProcessSteps.GetUpperBound(0) Or storedProcessSteps(l_gotoStep).enabled
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    MsgBox("Attempt to address a workflow step, outside the the upperbound of the those stored.")
        'End Try


        'Process just one step in the list.
        'Set this step to Done (whether or not it is enabled)
        updateStepStatus(l_gotoStep, "Done")
        'Progress to next step
        l_gotoStep = l_gotoStep + 1


        'If forceSkip Or Me.IsDisposed Then 'attempted to use a flag for all instead of unticking all.
        If forceSkip Then
            updateCheckListChecked(l_gotoStep, False)
        End If
        If forceDo Then
            updateCheckListChecked(l_gotoStep, True)
        End If

        Return toDoStep(l_gotoStep)

    End Function


    Public Sub forceSkipNextStep()
        'unckeck the step and skip over it.
        Dim lSkipped As Boolean = toDoNextStep(True)

    End Sub

    Public Function forceDoNextStep()
        'check the next step
        Return toDoNextStep(False, True)

    End Function

    Private Sub ProgressDialogue_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If percentComplete < 100 Then
            'If activeStep <= ProgressCheckedListBox.Items.Count - 1 Then
            If MessageBox.Show("Skip remaining items?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                'Untick all items
                'For i As Integer = 0 To ProgressCheckedListBox.Items.Count - 1
                'ProgressCheckedListBox.SetItemChecked(i, False)
                'Next
                waitTime = 0 'Run thru steps without any delay since the dialogue will be closed.
                started = True 'Have to start the dialogue so that processes that are waiting will continue, and then skip all steps.
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        If started Then
            Me.Close()
        Else
            started = True
            StartButton.Text = "Cancel"
        End If

    End Sub

    Private Sub CheckAllCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAllCheckBox.CheckedChanged
        'Loop thru items.
        For i As Integer = 0 To ProgressCheckedListBox.Items.Count - 1
            ProgressCheckedListBox.SetItemChecked(i, CheckAllCheckBox.Checked)

        Next
    End Sub

    Function getProcessStep(ByRef iCheckedListBoxIndex As Integer) As Integer

        For i As Integer = 0 To storedProcessSteps.Count - 1
            If storedProcessSteps(i).index = iCheckedListBoxIndex Then
                Return i
            End If
        Next

    End Function


    Private Sub ProgressCheckedListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProgressCheckedListBox.SelectedIndexChanged
        If ProgressCheckedListBox.SelectedIndex > -1 Then
            NotesTextBox.Text = storedProcessSteps(getProcessStep(ProgressCheckedListBox.SelectedIndex)).notes
        End If

    End Sub

    Public Sub stopAndClose()
        Me.toDoStep(nextStep)
    End Sub


    Public Sub setToCompleted()
        Me.percentComplete = 100
    End Sub

End Class