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
        client.Pull(iRemote, Common.unix_path(iBranch))
    End Sub
    Public Shared Sub Push(ByVal i_WorkingDir As String, ByVal iRemote As String, ByVal iBranch As String, Optional ByVal iTags As Boolean = False, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.Push(iRemote, Common.unix_path(iBranch))
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
        client.Switch(Common.unix_path(i_newBranch))
    End Sub
 
    Public Shared Sub createBranch(ByVal i_WorkingDir As String, ByVal i_newBranch As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.createBranch(Common.unix_path(i_newBranch))
    End Sub
    '
    ' Public Shared Sub Revert(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
    '     Dim client As New TortoiseFascade(i_wait)
    '     client.Revert(i_WorkingDir)
    ' End Sub
    '


    Public Shared Function callGit(ByVal i_WorkingDir As String, ByVal icommand As String)
        Dim lResult As String = Nothing
        Host.check_StdOut("""" & My.Settings.GITpath & """ " & icommand, lResult, i_WorkingDir, True)

        Return lResult
    End Function

    Public Shared Function describe(ByVal i_WorkingDir As String)

        Return callGit(i_WorkingDir, " describe")

    End Function

    Public Shared Function describeTags(ByVal i_WorkingDir As String)

        Return callGit(i_WorkingDir, " describe --tags")

    End Function

    Public Shared Sub TagSimple(ByVal i_WorkingDir As String, ByVal iTag As String, Optional ByVal i_wait As Boolean = True)
        Dim client As New GitBashFascade(i_WorkingDir, i_wait)
        client.TagSimple(iTag)

        'WORKS
        'callGit(i_WorkingDir, "tag " & iTag)

    End Sub

    'DOES NOT WORK
    'Public Shared Sub TagAnnotated(ByVal i_WorkingDir As String, ByVal iTag As String, ByVal iTagMessage As String, Optional ByVal i_wait As Boolean = True)
    '    Dim client As New GitBashFascade(i_WorkingDir, i_wait)
    '    client.TagAnnotated(iTag, iTagMessage)
    '
    '    'DOES NOT WORK
    '    'callGit(i_WorkingDir, "tag -a " & iTag & " -m '" & iTagMessage & "'") 'may push this down to gitbashfascade.
    '
    'End Sub


End Class
