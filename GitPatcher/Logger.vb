﻿Public Class Logger

    Private Shared Function DbgOn() As Boolean
        Return GitPatcher.LoggingToolStripMenuItem.Checked
    End Function

    Shared Sub Debug(ByVal iString As String, Optional ByVal iTitle As String = "Debug")
        If Not String.IsNullOrEmpty(iString) Then
            'Debug.WriteLine(iString)
            Console.WriteLine(iString)  'Write logging to console - which may be redirected to the LogViewer.

            If Logger.DbgOn() Then 'Tools/Logging ticked - currently set to invisible - not used.
                MsgBox(iString, MsgBoxStyle.Information, iTitle)
            End If
        End If
    End Sub

    Shared Sub Note(ByVal iName As String, ByVal iValue As String, Optional ByVal iTitle As String = "Note")
        Debug(iName & ":" & iValue, iTitle)
    End Sub

    Shared Sub ShowError(ByVal iString As String, Optional ByVal iTitle As String = "Error")
        Debug(iString, iTitle)
    End Sub


End Class
