Public Class GitBash
    ' Public Shared Sub Commit(ByVal i_WorkingDir As String, ByVal i_logmsg As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.Commit(i_WorkingDir, i_logmsg)
    ' End Sub
    '
    ' Public Shared Sub Log(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.ShowLog(i_WorkingDir)
    ' End Sub
    '
    ' Public Shared Sub Add(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.Add(i_WorkingDir)
    ' End Sub
    '
    Public Shared Sub Pull(ByVal i_WorkingDir As String, ByVal iRemote As String, ByVal iBranch As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.Pull(iRemote, iBranch)
    End Sub
    Public Shared Sub Push(ByVal i_WorkingDir As String, ByVal iRemote As String, ByVal iBranch As String, Optional ByVal iTags As Boolean = False, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.Push(iRemote, iBranch)
    End Sub
    '
    ' Public Shared Sub Push(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.Push(i_WorkingDir)
    ' End Sub
    '
    ' Public Shared Sub Merge(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.Merge(i_WorkingDir)
    ' End Sub
    '
    Public Shared Sub Switch(ByVal i_WorkingDir As String, ByVal i_newBranch As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.Switch(i_newBranch)
    End Sub
 
    Public Shared Sub createBranch(ByVal i_WorkingDir As String, ByVal i_newBranch As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.createBranch(i_newBranch)
    End Sub
    '
    ' Public Shared Sub Revert(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.Revert(i_WorkingDir)
    ' End Sub
    '
    Public Shared Sub Tag(ByVal i_WorkingDir As String, ByVal iTag As String, ByVal iTagMessage As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.Tag(iTag, iTagMessage)
    End Sub

End Class
