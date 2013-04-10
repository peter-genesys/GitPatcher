Public Class Host


    Public Shared Sub runInteractive(ByVal command, ByRef message, ByVal workingDir)

        Logger.Dbg(command)

        Dim starter As New ProcessStartInfo("c:\windows\system32\cmd", "/k " & command)

        If workingDir = "" Then
            workingDir = "c:\windows\system32"
        End If


        With starter
            .WorkingDirectory = workingDir
            .RedirectStandardOutput = False
            .RedirectStandardError = False
            .UseShellExecute = True

        End With

        Dim myProcess As Process = Process.Start(starter)

        'message = myProcess.StandardOutput.ReadToEnd()

        myProcess.WaitForExit()

        'Logger.Dbg("runInteractive:" & Chr(10) & message)
        'Logger.Dbg("Error (if any):" & Chr(10) & myProcess.StandardError.ReadToEnd())

    End Sub


    Public Shared Sub check_StdOut(ByVal command, ByRef message, ByVal workingDir)

        Logger.Dbg(command)

        Dim starter As New ProcessStartInfo("c:\windows\system32\cmd", "/k " & command)

        If workingDir = "" Then
            workingDir = "c:\windows\system32"
        End If


        With starter
            .WorkingDirectory = workingDir
            .RedirectStandardOutput = True
            .RedirectStandardError = True
            .UseShellExecute = False

        End With

        Dim myProcess As Process = Process.Start(starter)

        message = myProcess.StandardOutput.ReadToEnd()

        myProcess.WaitForExit()

        Logger.Dbg("check_StdOut:" & Chr(10) & message)
        Logger.Dbg("Error (if any):" & Chr(10) & myProcess.StandardError.ReadToEnd())

    End Sub

    Public Shared Sub check_StdErr(ByVal command, ByRef message, ByVal workingDir)

        Logger.Dbg(command)

        Dim starter As New ProcessStartInfo("c:\windows\system32\cmd", "/k " & command)

        If workingDir = "" Then
            workingDir = "c:\windows\system32"
        End If


        With starter
            .WorkingDirectory = workingDir
            .RedirectStandardError = True
            .UseShellExecute = False

        End With

        Dim myProcess As Process = Process.Start(starter)

        message = myProcess.StandardError.ReadToEnd()

        myProcess.WaitForExit()

        Logger.Dbg("check_StdErr:" & Chr(10) & message)

    End Sub

    Shared Sub RunExplorer(ByVal root_path As String)

        Dim envSetup As ProcessStartInfo = New ProcessStartInfo

        envSetup.FileName = "explorer.exe"
        envSetup.Arguments = "/e,/root," & root_path
        envSetup.WorkingDirectory = ""
        envSetup.UseShellExecute = False

        ' Launch the tool.
        Dim shelly As Process = New Process
        shelly.StartInfo = envSetup
        shelly.Start()

    End Sub


    Public Shared Sub run_command(ByVal command)

        Dim l_message As String = Nothing

        check_StdOut(command, l_message, "")

    End Sub


    Public Shared Sub run_command_in_dir(ByVal command, ByRef message, ByVal workingDir)

        Dim l_message As String = Nothing

        check_StdOut(command, l_message, workingDir)

        message = l_message

    End Sub

    Public Shared Sub run_command_in_dir_get_output(ByVal command As String, ByVal workingDir As String, Optional ByVal i_output_file As String = "", Optional ByVal show_output As Boolean = True)

        Dim l_message As String = Nothing
        Dim l_title As String = "COPIED TO CLIPBOARD"

        check_StdOut(command, l_message, workingDir)

        Logger.Dbg("command: " & command)
        Logger.Dbg("l_message: " & l_message)


        Clipboard.Clear()
        Clipboard.SetText(l_message)

        If i_output_file <> "" Then
            Logger.Dbg("Writing output to: " & i_output_file)
            Dim l_output_file As New System.IO.StreamWriter(i_output_file)

            l_output_file.Write(l_message)
            l_output_file.Close()

            l_title = l_title & " and to FILE " & i_output_file

        End If

        If show_output Then
            MsgBox(l_message, MsgBoxStyle.OkOnly, l_title)
        End If

    End Sub

    Public Shared Sub executeSQLscript(ByVal scriptFilename As String, ByVal scriptDir As String)
        run_command_in_dir_get_output(My.Settings.SQLpath & " /nolog @" & scriptFilename, scriptDir)
    End Sub

    Public Shared Sub executeSQLscriptInteractive(ByVal scriptFilename As String, ByVal scriptDir As String)

        Dim l_message As String = Nothing
 
        runInteractive(My.Settings.SQLpath & " /nolog @" & scriptFilename, l_message, scriptDir)
    End Sub

    Public Shared Sub executeSQLdynamicScriptInteractive(ByVal masterList As String, ByVal scriptDir As String)

        Dim l_message As String = Nothing

        runInteractive(My.Settings.SQLpath & " /nolog <<EOF" & masterList & Chr(10) & "exit" & Chr(10) & "EOF", l_message, scriptDir)
    End Sub


End Class
