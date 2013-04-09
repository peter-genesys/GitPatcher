Public Class Logger

    Private Shared Function DbgOn() As Boolean
        Return GitPatcher.LoggingToolStripMenuItem.Checked
    End Function


    Shared Sub Dbg(ByVal iString As String)
        Debug.WriteLine(iString)
        If Logger.DbgOn() Then
            MsgBox(iString)
        End If
    End Sub

    Shared Sub Note(ByVal iName As String, ByVal iValue As String)
        Dbg(iName & ":" & iValue)
    End Sub

End Class
