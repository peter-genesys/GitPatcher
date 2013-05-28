Public Class ProgressDialogue

    Private storedProcessSteps As ProcessStep()
    Private nextStep As Integer = 0
    Private activeStep As Integer = 0

 
    Public Sub New(ByVal progressTitle As String)
        Me.Location = New Point(0, 0)
        InitializeComponent()
        Me.Text = progressTitle

    End Sub

    Public Sub addStep(ByVal description As String, ByVal percentComplete As Integer)
        Dim aStep As ProcessStep = New ProcessStep(description, percentComplete)

        ReDim Preserve storedProcessSteps(nextStep)
        storedProcessSteps(nextStep) = aStep
        Me.ProgressCheckedListBox.Items.Add(aStep.description)
        nextStep = nextStep + 1

    End Sub

    Public Sub updateStep(ByVal stepNo As Integer, ByVal description As String, ByVal percentComplete As Integer, ByVal success As Boolean, ByVal fail As Boolean, ByVal skip As Boolean)
        Dim aStep As ProcessStep = New ProcessStep(description, percentComplete)

        storedProcessSteps(stepNo) = aStep
        If success Then
            Me.ProgressCheckedListBox.Items(stepNo) = description & " - Success"
        ElseIf fail Then
            Me.ProgressCheckedListBox.Items(stepNo) = description & " - Failed"
        ElseIf skip Then
            Me.ProgressCheckedListBox.Items(stepNo) = description & " - Skipped"
        End If


    End Sub


    Private Shared Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub


    ' Loops for a specificied period of time (milliseconds)
    Private Shared Sub pauseToRefreshProgressBar()
        wait(1000)
    End Sub


    Public Sub setStep(gotoStep As Integer)

        For i As Integer = 0 To storedProcessSteps.GetUpperBound(0) - 1
            If i < gotoStep Then
                ProgressCheckedListBox.SetItemChecked(i, True)
                ProgressBar.Value = storedProcessSteps(i).percentComplete
            End If
            'Trying to put DONE on the end of a completed step.
            'If i = currentStep - 1 Then
            '    ProgressCheckedListBox.Items(i).Equals = ProgressCheckedListBox.Items(i).text & " - DONE"
            'End If
            If i = gotoStep Then
                ProgressCheckedListBox.SetSelected(i, True)
            End If

        Next

        activeStep = gotoStep

        pauseToRefreshProgressBar()

    End Sub

    Public Sub goNextStep()

        setStep(activeStep + 1)

    End Sub

    Public Sub done(Optional percentComplete As Integer = 100, Optional doneMsg As String = "Done")
        Me.Text = Me.Text & " - " & doneMsg
        'Same as setStep but ticks the current step too
        For i As Integer = 0 To ProgressCheckedListBox.Items.Count - 1
            ProgressCheckedListBox.SetItemChecked(i, True)
        Next
        ProgressBar.Value = percentComplete
        pauseToRefreshProgressBar()

    End Sub


End Class