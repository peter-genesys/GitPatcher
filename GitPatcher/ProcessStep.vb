Public Class ProcessStep
    Private StepDescription As String
    Private StepNotes As String
    Private StepStatus As String

    Sub setDescription(description As String)
        StepDescription = description
    End Sub

    Sub setNotes(notes As String)
        StepNotes = notes
    End Sub


    Sub setStatus(status As String)
        StepStatus = status
    End Sub

    Public Sub New(ByVal description As String, Optional ByVal notes As String = "")
        setDescription(description)
        setNotes(notes)
    End Sub

    Function description() As String
        Return StepDescription
    End Function

    Function notes() As String
        Return StepNotes
    End Function

    Function status() As String
        Return StepStatus
    End Function
 
End Class
