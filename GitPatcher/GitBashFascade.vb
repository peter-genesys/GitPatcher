Imports System.IO

Public Class GitBashFascade

    'Used by the class GitBash.  Abstracts implementation of GitBash operations.
    'This class executes GitBash via command line.
    Private GitBashSetup As ProcessStartInfo = Nothing
    Private GitBash As Process = Nothing
    Private BashWait As Boolean = Nothing

    Public Sub New(ByVal i_GitDir As String, Optional ByVal i_wait As Boolean = True)
        GitBash = New Process
        GitBashSetup = New ProcessStartInfo
        GitBashSetup.FileName = My.Settings.GITpath
        GitBashSetup.UseShellExecute = False
        GitBashSetup.WorkingDirectory = i_GitDir
        GitBashSetup.RedirectStandardError = True
        BashWait = i_wait
    End Sub

    Public Property Wait As Boolean
        Get
            Return BashWait
        End Get
        Set(ByVal i_wait As Boolean)
            BashWait = i_wait
        End Set
    End Property

    Private Function executeSuccess() As Boolean
        Logger.Dbg("GitBashFascade.execute_success")
        GitBash.StartInfo = GitBashSetup
        GitBash.Start()

        Dim stdErrMessage As String = GitBash.StandardError.ReadToEnd()

        If (BashWait) Then
            GitBash.WaitForExit()
        End If
        Dim ExitCode As Integer = GitBash.ExitCode
        Logger.Note("ExitCode", ExitCode)
        Dim Success As Boolean = (ExitCode = 0)

        If Not Success Then
            MsgBox(stdErrMessage)
        End If

        Return Success
    End Function


    Private Sub execute()
        Logger.Dbg("GitBashFascade.execute")
        Dim success As Boolean = executeSuccess()
    End Sub

    ' Add files to GIT with GitBash
    Public Sub Add(ByVal i_files)
        GitBashSetup.Arguments = "add " & i_files 'git add files
        execute()
    End Sub
    '
    '
    '   ' Commit files to GIT with tortoiseGit
    '   Public Sub Commit(ByVal i_path, ByVal i_logmsg)
    '       GitBashSetup.Arguments = "/command:commit /path:""" & i_path & """ /logmsg:""" & i_logmsg & """ /closeonend:1"
    '       execute()
    '   End Sub
    '
    '
    '   ' Show log of current branch with tortoiseGit
    '   Public Sub ShowLog(ByVal i_path)
    '       GitBashSetup.Arguments = "/command:log /path:""" & i_path & """ /closeonend:1"
    '       execute()
    '   End Sub
    '
    Public Sub Pull(ByVal iRemote As String, ByVal iBranch As String)
        GitBashSetup.Arguments = "pull " & iRemote & " " & iBranch ' 'git pull origin develop
        execute()
    End Sub

    Public Function PushSuccess(ByVal iRemote As String, ByVal iBranch As String, Optional ByVal iTags As Boolean = False)
        Logger.Dbg("GitBashFascade.PushSuccess( " & iRemote & "," & iBranch & "," & iTags.ToString & ")")
        If iTags Then
            GitBashSetup.Arguments = "push " & iRemote & " " & iBranch & " --tags " 'git push origin master --tags
        Else
            GitBashSetup.Arguments = "push " & iRemote & " " & iBranch ' 'git push origin master
        End If

        Return executeSuccess()

    End Function


    Public Sub Push(ByVal iRemote As String, ByVal iBranch As String, Optional ByVal iTags As Boolean = False)
        Logger.Dbg("GitBashFascade.Push( " & iRemote & "," & iBranch & "," & iTags.ToString & ")")
        Dim success As Boolean = PushSuccess(iRemote, iBranch, iTags)

    End Sub


    '
    '   Public Sub Push(ByVal i_path)
    '       GitBashSetup.Arguments = "/command:push /path:""" & i_path & """ /closeonend:1"
    '       execute()
    '   End Sub
    '
    '   Public Sub Merge(ByVal i_path)
    '       'tortoiseSetup.Arguments = "/command:merge /path:""" & i_path & """ /branch:""" & i_merge_branch & """ /closeonend:1"
    '       GitBashSetup.Arguments = "/command:merge /path:""" & i_path & """ /closeonend:1"
    '       execute()
    '   End Sub
    '
    Public Sub Switch(ByVal iBranch As String)
        GitBashSetup.Arguments = "checkout " & iBranch  'git checkout iss53
        execute()
    End Sub

    Public Sub createBranch(ByVal iBranch As String)
        GitBashSetup.Arguments = "checkout -b " & iBranch  'git checkout -b iss53
        execute()
    End Sub
    '
    '   Public Sub Revert(ByVal i_path)
    '       GitBashSetup.Arguments = "/command:revert /path:""" & i_path & """ /closeonend:1"
    '       execute()
    '   End Sub
    '
    Public Sub TagSimple(ByVal iTag As String)
        GitBashSetup.Arguments = "tag " & iTag & " --force" 'git tag v1.4
        execute()
    End Sub

    'DOES NOT WORK FOR SOME REASON - TOO MANY PARAMS returned when using logging.
    'Public Sub TagAnnotated(ByVal iTag As String, ByVal iTagMessage As String)
    '    GitBashSetup.Arguments = "tag -a " & iTag & " -m '" & iTagMessage & "'"  'git tag -a v1.4 -m 'my version 1.4'
    '    execute()
    'End Sub


    ' Start Tortoise Repo Browser
    'Public Sub Repo(ByVal i_URL)
    '    tortoiseSetup.Arguments = "/command:repobrowser /path:""" & i_URL & """ /closeonend:1"
    '    execute()
    'End Sub

    ' Start Tortoise Update
    'Public Sub Update(ByVal i_WorkingDir)
    '    tortoiseSetup.Arguments = "/command:update /path:""" & i_WorkingDir & """ /closeonend:1"
    '    execute()
    'End Sub



    'TortoiseGitProc.exe /command:"commit" /path:"apex_dir" /logmsg:"""App app_id has been exported and split""" /closeonend:1  | Out-Null



End Class


