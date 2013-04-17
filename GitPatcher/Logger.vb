Public Class Logger

    Private Shared Function DbgOn() As Boolean
        Return GitPatcher.LoggingToolStripMenuItem.Checked
    End Function


    Shared Sub Dbg(ByVal iString As String, Optional ByVal iTitle As String = "Debug")
        If Not String.IsNullOrEmpty(iString) Then
            Debug.WriteLine(iString)
            If Logger.DbgOn() Then
                MsgBox(iString, iTitle)
            End If
        End If
    End Sub

    Shared Sub Note(ByVal iName As String, ByVal iValue As String, Optional ByVal iTitle As String = "Debug")
        Dbg(iName & ":" & iValue, iTitle)
    End Sub

End Class
