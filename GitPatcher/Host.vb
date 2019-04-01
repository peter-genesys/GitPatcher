Public Class Host

 

    Public Shared Sub run_shell(ByVal i_filename, ByVal i_path, ByVal i_arguments, ByVal i_WorkingDirectory)
        Dim envSetup As ProcessStartInfo = New ProcessStartInfo
        Dim path As String = envSetup.EnvironmentVariables.Item("PATH")
        envSetup.EnvironmentVariables("PATH") = i_path & ";" & path
        envSetup.FileName = i_filename
        envSetup.Arguments = i_arguments
        envSetup.WorkingDirectory = i_WorkingDirectory
        envSetup.UseShellExecute = False

        ' Launch the tool.
        Dim myProcess As Process = New Process
        myProcess.StartInfo = envSetup
        myProcess.Start()
    End Sub


    Public Shared Sub runInteractive(ByVal command As String, ByRef message As String, ByVal workingDir As String, Optional ByVal wait As Boolean = True)

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
        If wait Then
            myProcess.WaitForExit()
        End If

        'Logger.Dbg("runInteractive:" & Chr(10) & message)
        'Logger.Dbg("Error (if any):" & Chr(10) & myProcess.StandardError.ReadToEnd())

    End Sub


    Public Shared Sub check_StdOut(ByVal command As String, ByRef message As String, ByVal workingDir As String, Optional ByVal oneline As Boolean = False)

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

        If oneline Then
            message = myProcess.StandardOutput.ReadLine()
        Else
            message = myProcess.StandardOutput.ReadToEnd()
        End If
 
        myProcess.WaitForExit()

        Logger.Dbg("check_StdOut:" & Chr(10) & message)
        Logger.Dbg("Error (if any):" & Chr(10) & myProcess.StandardError.ReadToEnd())

    End Sub

 
    Public Shared Function getOutput(ByVal command As String, ByVal workingDir As String) As String

        Dim l_output As String = Nothing

        Dim tempFilename As String = "c:\temp\gitpatcherOutput.txt"

        Logger.Dbg(command)

        Dim starter As New ProcessStartInfo("c:\windows\system32\cmd", "/k " & command & " > " & tempFilename)

        If workingDir = "" Then
            workingDir = "c:\windows\system32"
        End If


        With starter
            .WorkingDirectory = workingDir
            .RedirectStandardOutput = False
            .RedirectStandardError = True
            .UseShellExecute = False

        End With

        FileIO.deleteFileIfExists(tempFilename)

        Dim myProcess As Process = Process.Start(starter)

        'Message = myProcess.StandardOutput.ReadToEnd()

        myProcess.WaitForExit()

        l_output = FileIO.readFileLine1(tempFilename)

        'Logger.Dbg("check_StdOut:" & Chr(10) & Message)
        Logger.Dbg("Error (if any):" & Chr(10) & myProcess.StandardError.ReadToEnd())

        Return l_output

    End Function




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

    Public Shared Sub executeSQLscriptInteractive(ByVal scriptFilename As String, ByVal scriptDir As String, Optional ByVal connection As String = "", Optional ByVal wait As Boolean = True)

        Dim l_message As String = Nothing
        If String.IsNullOrEmpty(connection) Then
            runInteractive(My.Settings.SQLpath & " /nolog @" & scriptFilename, l_message, scriptDir, wait)

        Else
            runInteractive(My.Settings.SQLpath & " " & connection & " @" & scriptFilename, l_message, scriptDir, wait)
        End If

    End Sub

    Public Shared Sub executeDynamicSQLScript(ByVal masterList As String, ByVal scriptDir As String, Optional ByVal wait As Boolean = True)
        'This is NON-INTERACTIVE
        Dim l_message As String = Nothing

        runInteractive(My.Settings.SQLpath & " /nolog <<EOF" & masterList & Chr(10) & "exit" & Chr(10) & "EOF", l_message, scriptDir, wait)
    End Sub


    Public Shared Sub executeSQLplus(ByVal scriptDir As String, Optional ByVal connection As String = "", Optional ByVal wait As Boolean = True)

        Dim l_message As String = Nothing
        If String.IsNullOrEmpty(connection) Then
            runInteractive(My.Settings.SQLpath & " /nolog", l_message, scriptDir, wait)

        Else
            runInteractive(My.Settings.SQLpath & " " & connection, l_message, scriptDir, wait)
        End If

    End Sub


    Public Shared Sub RunMasterScript(scriptData As String, ByVal scriptDir As String)

        'scriptDir is also used as workingDir
        Dim masterScriptName As String = Common.dos_path_trailing_slash(scriptDir) & "temp_master_script.sql"

        FileIO.writeFile(masterScriptName, scriptData, True)

        Host.executeSQLscriptInteractive(masterScriptName, scriptDir)

        FileIO.deleteFileIfExists(masterScriptName)

    End Sub




End Class
