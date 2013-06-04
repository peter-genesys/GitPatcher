Public Class ProcessStep
    Private StepDescription As String
    Private StepPercentComplete As Integer
    Private StepStatus As String

    Sub setDescription(description As String)
        StepDescription = description
    End Sub

    Sub setPercentComplete(percentComplete As Integer)
        StepPercentComplete = percentComplete
    End Sub

    Sub setStatus(status As String)
        StepStatus = status
    End Sub

    Public Sub New(ByVal description As String, ByVal percentComplete As Integer, Optional ByVal status As String = "")
        setDescription(description)
        setPercentComplete(percentComplete)
        setStatus(status)
    End Sub

    Function description() As String
        Return StepDescription
    End Function

    Function percentComplete() As String
        Return StepPercentComplete
    End Function

    Function status() As String
        Return StepStatus
    End Function
 
End Class
