Public Class ProgressDialogue

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


    Public Sub addStep(ByVal description As String, Optional ByVal checked As Boolean = True, Optional ByVal notes As String = "")

        Dim aStep As ProcessStep = New ProcessStep(description, notes)

        ReDim Preserve storedProcessSteps(nextStep)
        storedProcessSteps(nextStep) = aStep
        Me.ProgressCheckedListBox.Items.Add(aStep.description)
        ProgressCheckedListBox.SetItemChecked(nextStep, checked)
        'ProgressCheckedListBox.SetSelected(0, True) 'Select first item - nah this hides the initial Notes.

        nextStep = nextStep + 1

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
                Me.ProgressCheckedListBox.Items(stepNo) = storedProcessSteps(stepNo).description & " - " & storedProcessSteps(stepNo).status

            End If

            pauseToRefreshProgressBar()

        End If

    End Sub

    Public Sub updateStepDescription(ByVal stepNo As Integer, ByVal description As String)

        storedProcessSteps(stepNo).setDescription(description)
        Me.ProgressCheckedListBox.Items(stepNo) = storedProcessSteps(stepNo).description & " - " & storedProcessSteps(stepNo).status

    End Sub

    'Public Sub updateStepPercentComplete(ByVal stepNo As Integer, percentComplete As Integer)
    '
    '    storedProcessSteps(stepNo).setPercentComplete(percentComplete)
    '
    'End Sub

 
    ' Loops for a specificied period of time (milliseconds)
    Private Sub pauseToRefreshProgressBar()
        If percentComplete = 100 Then
            StartButton.Text = "Done"
            Common.wait(1000)
            Me.Close()
        Else
            Common.wait(1000)
        End If


    End Sub


    '  Public Sub setStep(gotoStep As Integer)
    '      If Not ProgressCheckedListBox.CheckedIndices.Contains(gotoStep) Then
    '          activeStep = gotoStep
    '          updateStep(activeStep, "Skipped")
    '          'Throw (New SkipStepException("Step " & activeStep & " Skipped"))
    '      End If
    '
    '      For i As Integer = 0 To storedProcessSteps.GetUpperBound(0) - 1
    '          If i < gotoStep Then
    '              'ProgressCheckedListBox.SetItemChecked(i, True)
    '              ProgressBar.Value = storedProcessSteps(i).percentComplete
    '          End If
    '          'Trying to put DONE on the end of a completed step.
    '          'If i = currentStep - 1 Then
    '          '    ProgressCheckedListBox.Items(i).Equals = ProgressCheckedListBox.Items(i).text & " - DONE"
    '          'End If
    '          If i = gotoStep Then
    '              ProgressCheckedListBox.SetSelected(i, True)
    '              updateStep(activeStep, "Doing")
    '          End If
    '
    '      Next
    '
    '      activeStep = gotoStep
    '
    '      pauseToRefreshProgressBar()
    '
    '  End Sub
    '
    '  Public Sub goNextStep()
    '
    '      setStep(activeStep + 1)
    '
    '  End Sub

    Public Function toDoStep(gotoStep As Integer) As Boolean

        Dim last_active_step As Integer = activeStep
        'updateStep(activeStep, "Done")
        activeStep = gotoStep

        'Conclude preceding step
        For i As Integer = last_active_step To activeStep - 1
            updateStepStatus(i, "Done")
        Next
 
        'If gotoStep <= storedProcessSteps.GetUpperBound(0) - 1 Then
        If activeStep <= storedProcessSteps.GetUpperBound(0) Then
            'activeStep = gotoStep
            If ProgressCheckedListBox.CheckedIndices.Contains(gotoStep) Then
                ProgressCheckedListBox.SetSelected(activeStep, True)
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

    End Function

    Public Function toDoNextStep() As Boolean
        Dim l_gotoStep As Integer = activeStep + 1
        Return toDoStep(l_gotoStep)
    End Function


    '  Public Sub done(Optional percentComplete As Integer = 100, Optional doneMsg As String = "Done")
    '      Me.Text = Me.Text & " - " & doneMsg
    '      'Same as setStep but ticks the current step too
    '      'For i As Integer = 0 To ProgressCheckedListBox.Items.Count - 1
    '      '    ProgressCheckedListBox.SetItemChecked(i, True)
    '      'Next
    '      ProgressBar.Value = percentComplete
    '      pauseToRefreshProgressBar()
    '
    '  End Sub


    Private Sub ProgressDialogue_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If activeStep <= ProgressCheckedListBox.Items.Count - 1 Then
            If MessageBox.Show("Skip remaining items?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                For i As Integer = 0 To ProgressCheckedListBox.Items.Count - 1
                    ProgressCheckedListBox.SetItemChecked(i, False)
                Next
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

    Private Sub ProgressCheckedListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProgressCheckedListBox.SelectedIndexChanged
        If ProgressCheckedListBox.SelectedIndex > -1 Then
            NotesTextBox.Text = storedProcessSteps(ProgressCheckedListBox.SelectedIndex).notes
        End If

    End Sub
End Class