Public Class ProgressDialogue

    Private storedProcessSteps As ProcessStep()
    Private lastStep As Integer = 0

    Public Sub popSteps(ByVal processSteps As ProcessStep())
        storedProcessSteps = processSteps
        Me.ProgressCheckedListBox.Items.Clear()
        lastStep = 0
        For i As Integer = 0 To storedProcessSteps.GetUpperBound(0) - 1
            Me.ProgressCheckedListBox.Items.Add(storedProcessSteps(i).description)
            lastStep = lastStep + 1
        Next

    End Sub


    Public Sub New(ByVal progressTitle As String, ByVal processSteps As ProcessStep())
        Me.Location = New Point(0, 0)
        InitializeComponent()
        Me.Text = progressTitle

        popSteps(processSteps)

    End Sub

    Public Sub New(ByVal progressTitle As String)
        Me.Location = New Point(0, 0)
        InitializeComponent()
        Me.Text = progressTitle

    End Sub

    Public Sub addStep(ByVal description As String, ByVal percentComplete As Integer)
        Dim aStep As ProcessStep = New ProcessStep(description, percentComplete)

        ReDim Preserve storedProcessSteps(lastStep)
        'storedProcessSteps(storedProcessSteps.GetUpperBound(0)) = aStep
        storedProcessSteps(lastStep) = aStep
        Me.ProgressCheckedListBox.Items.Add(aStep.description)
        lastStep = lastStep + 1

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
        wait(2000)
    End Sub


    Public Sub setStep(currentStep As Integer)

        For i As Integer = 0 To storedProcessSteps.GetUpperBound(0) - 1
            If i < currentStep Then
                ProgressCheckedListBox.SetItemChecked(i, True)
            End If
            If i = currentStep Then
                ProgressCheckedListBox.SetSelected(i, True)
                ProgressBar.Value = storedProcessSteps(i).percentComplete
            End If

        Next
        pauseToRefreshProgressBar()

    End Sub

    Public Sub doneStep(currentStep As Integer)
        'Same as setStep but ticks the current step too
        For i As Integer = 0 To storedProcessSteps.GetUpperBound(0) - 1
            If i <= currentStep Then
                ProgressCheckedListBox.SetItemChecked(i, True)
            End If
            If i = currentStep Then
                ProgressCheckedListBox.SetSelected(i, True)
                ProgressBar.Value = storedProcessSteps(i).percentComplete
            End If

        Next
        pauseToRefreshProgressBar()

    End Sub


End Class