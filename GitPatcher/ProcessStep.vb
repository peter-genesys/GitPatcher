Public Class ProcessStep
    Private StepDescription As String
    Private StepPercentComplete As Integer

    Public Sub New(ByVal description As String, ByVal percentComplete As Integer)
        StepDescription = description
        StepPercentComplete = percentComplete
    End Sub

    Function description() As String
        Return StepDescription
    End Function

    Function percentComplete() As String
        Return StepPercentComplete
    End Function

End Class
