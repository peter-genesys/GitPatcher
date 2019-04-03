Public Class ProcessStep
    Private StepDescription As String
    Private StepNotes As String
    Private StepStatus As String
    Private StepEnabled As Boolean
    Private StepCode As String
    Private StepIndex As String 'index into the checkbox list

    Sub setDescription(description As String)
        StepDescription = description
    End Sub

    Sub setNotes(notes As String)
        StepNotes = notes
    End Sub

    Sub setStatus(status As String)
        StepStatus = status
    End Sub

    Sub setEnabled(enabled As Boolean)
        StepEnabled = enabled
    End Sub

    Sub setCode(code As String)
        StepCode = code
    End Sub

    Sub setIndex(Index As Integer)
        StepIndex = Index
    End Sub

    Public Sub New(ByVal description As String, Optional ByVal notes As String = "", Optional enabled As Boolean = True, Optional index As Integer = -1, Optional code As String = "")
        setDescription(description)
        setNotes(notes)
        setEnabled(enabled)
        setCode(code)
        setIndex(index)
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

    Function enabled() As Boolean
        Return StepEnabled
    End Function

    Function code() As String
        Return StepCode
    End Function

    Function index() As Integer
        Return StepIndex
    End Function

End Class
