Public Class TortoiseFacade

    ' This class is designed to shield the Tortoise logic in other classes from the SharpSVN implemenation classes.
    Private tortoiseSetup As ProcessStartInfo = Nothing
    Private tortoise As Process = Nothing
    Private tortoiseWait As Boolean = Nothing

    Public Sub New(Optional ByVal i_wait As Boolean = True)
        tortoise = New Process
        tortoiseSetup = New ProcessStartInfo
        tortoiseSetup.FileName = "TortoiseGitProc.exe"
        tortoiseSetup.UseShellExecute = False
        tortoiseWait = i_wait
    End Sub

    Public Property Wait As Boolean
        Get
            Return tortoiseWait
        End Get
        Set(ByVal i_wait As Boolean)
            tortoiseWait = i_wait
        End Set
    End Property


    Private Sub execute()
        tortoise.StartInfo = tortoiseSetup
        tortoise.Start()
        If (tortoiseWait) Then
            tortoise.WaitForExit()
        End If
    End Sub


    ' Add files to GIT with tortoiseGit
    Public Sub Add(ByVal i_path)
        tortoiseSetup.Arguments = "/command:add /path:""" & i_path & """ /closeonend:1"
        execute()
    End Sub


    ' Comit files to GIT with tortoiseGit
    Public Sub Commit(ByVal i_path, ByVal i_logmsg)
        tortoiseSetup.Arguments = "/command:commit /path:""" & i_path & """ /logmsg:""" & i_logmsg & """ /closeonend:1"
        execute()
    End Sub

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


