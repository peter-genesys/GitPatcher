Public Class Apex



    Shared Sub TortoiseAdd(ByVal i_WorkingDir As String, Optional ByVal i_wait As Boolean = True)
        Dim Tortoise As New TortoiseFacade(i_wait)
        Tortoise.Add(i_WorkingDir)
    End Sub

 
    Shared Sub TortoiseCommit(ByVal i_WorkingDir As String, ByVal i_logmsg As String, Optional ByVal i_wait As Boolean = True)
        Dim Tortoise As New TortoiseFacade(i_wait)
        Tortoise.Commit(i_WorkingDir, i_logmsg)
    End Sub

 

    Public Shared Sub ApexExportCommit(connection, username, password, fapp_id, apex_dir)
 
        Dim ExportProgress As ProgressDialogue = New ProgressDialogue("Apex Export  " & fapp_id)
        ExportProgress.MdiParent = GitPatcher
        ExportProgress.addStep("Export Apex as a single file", 25)
        ExportProgress.addStep("Splitting into components", 50)
        ExportProgress.addStep("Add new files to GIT repository", 75)
        ExportProgress.addStep("Commit changes to GIT repository", 100)
        ExportProgress.Show()
 
        ''write-host "APEX file export and commit - uses oracle.apex.APEXExport.class and java oracle.apex.APEXExportSplitter.class"
        ''Does this need to perform a pull from the master ??
        ''TortoiseGitProc.exe /command:"pull" /path:"apex_dir" | Out-Null
        ''add ojdbc5.jar to the CLASSPATH, in this case its on the checkout path

        Dim classpath As String = Environment.GetEnvironmentVariable("CLASSPATH")
        'EG \oracle\jdbc\lib\ojdbc5.jar
        If Not classpath.Contains(apex_dir & My.Settings.JDBCjar) Then
            classpath = classpath & ";" & apex_dir & My.Settings.JDBCjar
            Environment.SetEnvironmentVariable("CLASSPATH", classpath)
        End If

        Dim app_id As String = fapp_id.Split("f")(1)
        Dim fapp_sql As String = fapp_id & ".sql"
        Dim message As String = Nothing

        'PROGRESS 0
        ExportProgress.setStep(0)

        'NB Not exporting application comments
        'Host.runInteractive("java oracle.apex.APEXExport -db " & connection & " -user " & username & " -password " & password & " -applicationid " & app_id & " -expPubReports -skipExportDate" _
        '                  , message, apex_dir)
        Host.check_StdErr("java oracle.apex.APEXExport -db " & connection & " -user " & username & " -password " & password & " -applicationid " & app_id & " -expPubReports -skipExportDate" _
                  , message, apex_dir)
        Logger.Dbg(message, "Apex Export Error")

        'write-host "Remove the application directory apex_dir\fapp_id" 


        'Remove-Item -Recurse -Force -ErrorAction 0 @("apex_dir\fapp_id")
        FileIO.deleteFolderIfExists(apex_dir & fapp_id)

        'PROGRESS 25
        ExportProgress.setStep(1)

        '
        'write-host "Splitting $APP_SQL into its composite files"
        'java oracle.apex.APEXExportSplitter $APP_SQL 
        Host.check_StdErr("java oracle.apex.APEXExportSplitter " & fapp_sql, message, apex_dir)

        Logger.Dbg(message, "Apex Export Splitter Error")

        'PROGRESS 50
        ExportProgress.setStep(2)

        'Adding new files to GIT"
        TortoiseAdd(apex_dir & fapp_id, True)

        'PROGRESS 75
        ExportProgress.setStep(3)

        'Committing changed files to GIT"
        TortoiseCommit(apex_dir & fapp_id, "App " & fapp_id & " exported and split - IF YOU DIDNT CHANGE IT PLEASE DONT COMMIT IT", True)

        'PROGRESS 100
        ExportProgress.done()

    End Sub

 


End Class