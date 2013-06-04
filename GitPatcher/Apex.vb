Public Class Apex
 
    Public Shared Sub relabelApex(ByVal i_label As String)


        'Relabel Apex 
        '  open script create_application.sql
        '  read input line at a time until line starting "  p_flow_version=> "
        '  replace this line with " p_flow_version=> " & new_version & " " & today
        '  write rest of file and close it.

        Dim l_create_application_new As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql"
        Dim l_create_application_old As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql.old"

        FileIO.deleteFileIfExists(l_create_application_old)
        My.Computer.FileSystem.RenameFile(l_create_application_new, "create_application.sql.old")


        Dim l_old_file As New System.IO.StreamReader(l_create_application_old)
        Dim l_new_file As New System.IO.StreamWriter(l_create_application_new)
        Dim l_line As String = Nothing
        Dim l_build_status As String = Nothing
 
        If Main.CurrentConnectionTextBox.Text.Contains("isdevl") Then
            l_build_status = "RUN_ONLY"
        Else
            l_build_status = "RUN_AND_BUILD"
        End If
 

        Do
            'For each line
            If l_old_file.EndOfStream Then Exit Do

            l_line = l_old_file.ReadLine()
            If l_line.Contains("p_flow_version") Then
                l_line = "  p_flow_version=> '" & i_label & "',"
            End If

            If l_line.Contains("p_build_status") Then
                l_line = "  p_build_status=> '" & l_build_status & "',"
            End If

            l_new_file.WriteLine(l_line)

        Loop

        l_old_file.Close()
        l_new_file.Close()

        FileIO.deleteFileIfExists(l_create_application_old)

    End Sub


    Public Shared Sub ApexExportCommit(connection, username, password, fapp_id, apex_dir)
 
        Dim ExportProgress As ProgressDialogue = New ProgressDialogue("Apex Export  " & fapp_id)
        ExportProgress.MdiParent = GitPatcher
        ExportProgress.addStep("Export Apex as a single file", 20)
        ExportProgress.addStep("Splitting into components", 40)
        ExportProgress.addStep("Add new files to GIT repository", 60)
        ExportProgress.addStep("Commit valid changes to GIT repository", 80)
        ExportProgress.addStep("Revert invalid changes from your checkout", 100)
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
        Tortoise.Add(apex_dir & fapp_id, True)

        'PROGRESS 75
        ExportProgress.setStep(3)

        'Committing changed files to GIT"
        Tortoise.Commit(apex_dir & fapp_id, "App " & fapp_id & " exported and split - IF YOU DIDNT CHANGE IT PLEASE DONT COMMIT IT", True)

        ExportProgress.setStep(4)

        Tortoise.Revert(apex_dir & fapp_id)

        'PROGRESS 100
        ExportProgress.done()

    End Sub

    Public Shared Sub ApexImportFromTag(fapp_id)

        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim ImportProgress As ProgressDialogue = New ProgressDialogue("Apex Import " & fapp_id)
        ImportProgress.MdiParent = GitPatcher
        ImportProgress.addStep("Choose a tag to import from", 20, False)
        ImportProgress.addStep("Checkout the tag", 40)
        ImportProgress.addStep("If tag not like " & Main.AppCodeTextBox.Text & " relabel apex", 50)
        ImportProgress.addStep("Import Apex", 60)
        ImportProgress.addStep("Return to branch: " & currentBranch, 100)
        ImportProgress.Show()

        If ImportProgress.toDoStep(0) Then

            Dim tagnames As Collection = New Collection
            tagnames.Add("HEAD")
            tagnames = GitSharpFascade.getTagList(My.Settings.CurrentRepo, tagnames, Main.CurrentBranchTextBox.Text)
            tagnames = GitSharpFascade.getTagList(My.Settings.CurrentRepo, tagnames, Main.AppCodeTextBox.Text)


            Dim tagApexVersion As String = Nothing
            tagApexVersion = ChoiceDialog.Ask("Please choose a tag for apex install", tagnames, "HEAD", "Choose tag")

            If ImportProgress.toDoStep(1) Then
 
                GitBash.Switch(My.Settings.CurrentRepo, tagApexVersion)
                If ImportProgress.toDoStep(2) Then

                    'If tag not like Main.AppCodeTextBox.Text relabel apex

                    If Not tagApexVersion.Contains(Main.AppCodeTextBox.Text) Then

                        Dim l_label As String = Nothing
                        'Host.check_StdOut("""" & My.Settings.GITpath & """ describe --tags", l_label, My.Settings.CurrentRepo, True)
                        'alternative method
                        'l_label = Host.getOutput("""" & My.Settings.GITpath & """ describe --tags", My.Settings.CurrentRepo) 

                        l_label = GitBash.describeTags(My.Settings.CurrentRepo)

                        relabelApex("GIT Tag: " & l_label)
                    End If
                End If
            End If
        End If


        If ImportProgress.toDoStep(3) Then

            Host.executeSQLscriptInteractive("install.sql" _
                                           , Main.RootApexDirTextBox.Text & My.Settings.CurrentApex _
                                           , Main.get_connect_string(Main.ParsingSchemaTextBox.Text, My.Settings.CurrentDB))

        End If
        If ImportProgress.toDoStep(4) Then
            GitBash.Switch(My.Settings.CurrentRepo, currentBranch)
        End If

        ImportProgress.toDoNextStep()

    End Sub


End Class