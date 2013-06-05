Public Class ProcessStep
    Private StepDescription As String
    Private StepNotes As String
    Private StepPercentComplete As Integer
    Private StepStatus As String

    Sub setDescription(description As String)
        StepDescription = description
    End Sub

    Sub setNotes(notes As String)
        StepNotes = notes
    End Sub

    Sub setPercentComplete(percentComplete As Integer)
        StepPercentComplete = percentComplete
    End Sub

    Sub setStatus(status As String)
        StepStatus = status
    End Sub

    Public Sub New(ByVal description As String, ByVal percentComplete As Integer, Optional ByVal notes As String = "")
        setDescription(description)
        setPercentComplete(percentComplete)
        setNotes(notes)
    End Sub

    Function description() As String
        Return StepDescription
    End Function

    Function notes() As String
        Return StepNotes
    End Function

    Function percentComplete() As String
        Return StepPercentComplete
    End Function

    Function status() As String
        Return StepStatus
    End Function
 
End Class
