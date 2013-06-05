Public Class Apex
 
    Public Shared Sub modCreateApplication(ByVal i_label As String, ByVal i_buildStatus As String)


        'Relabel Apex 
        '  open script create_application.sql
        '  read input line at a time until line starting "  p_flow_version=> "
        '  replace this line with " p_flow_version=> " & new_version & " " & today
        '  write rest of file and close it.

        Dim l_create_application_new As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql"
        Dim l_create_application_old As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql.old"
        Dim l_create_application_orig As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql.orig"

        FileIO.deleteFileIfExists(l_create_application_old)
        If Not FileIO.fileExists(l_create_application_orig) Then
            'Backup the create_application.sql file
            My.Computer.FileSystem.CopyFile(l_create_application_new, l_create_application_orig)

        End If
        My.Computer.FileSystem.RenameFile(l_create_application_new, "create_application.sql.old")

        Dim l_old_file As New System.IO.StreamReader(l_create_application_old)
        Dim l_new_file As New System.IO.StreamWriter(l_create_application_new)
        Dim l_line As String = Nothing

        Do
            'For each line
            If l_old_file.EndOfStream Then Exit Do

            l_line = l_old_file.ReadLine()
            If l_line.Contains("p_flow_version") And Not String.IsNullOrEmpty(i_label) Then
                l_line = "  p_flow_version=> '" & i_label & "',"
            End If

            If l_line.Contains("p_build_status") And Not String.IsNullOrEmpty(i_buildStatus) Then
                l_line = "  p_build_status=> '" & i_buildStatus & "',"
            End If

            l_new_file.WriteLine(l_line)

        Loop

        l_old_file.Close()
        l_new_file.Close()


        FileIO.deleteFileIfExists(l_create_application_old)


    End Sub

 
    Public Shared Sub restoreCreateApplication()
 
        Dim l_create_application_new As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql"
        Dim l_create_application_orig As String = Main.RootApexDirTextBox.Text & Main.ApexAppTextBox.Text & "\application\create_application.sql.orig"
 
        If FileIO.fileExists(l_create_application_orig) Then
            'Restore Backup of create_application.sql file
            FileIO.deleteFileIfExists(l_create_application_new)
            My.Computer.FileSystem.RenameFile(l_create_application_orig, "create_application.sql")
        End If

    End Sub

    Public Shared Sub ApexExportCommit()

 
            Dim connection As String = Main.CurrentConnectionTextBox.Text
            Dim username As String = Main.ParsingSchemaTextBox.Text

            Dim fapp_id As String = My.Settings.CurrentApex
            Dim apex_dir As String = Main.RootApexDirTextBox.Text

        Dim ExportProgress As ProgressDialogue = New ProgressDialogue("Export APEX application " & fapp_id & " from DB " & My.Settings.CurrentDB, _
            "Exporting APEX application " & My.Settings.CurrentApex & " from parsing schema " & Main.ParsingSchemaTextBox.Text & " in DB " & My.Settings.CurrentDB & Environment.NewLine & _
            "This writes individual apex files to the GIT Repo checkout, and then prompt to add and commit the changes." & Environment.NewLine & _
            Environment.NewLine & _
            "Consider which branch you are exporting to." & Environment.NewLine & _
            "To commit any existing changes, close this workflow and perform a GIT COMMIT.")
 
            ExportProgress.MdiParent = GitPatcher
            ExportProgress.addStep("Export Apex as a single file", 20)
            ExportProgress.addStep("Splitting into components", 40)
            ExportProgress.addStep("Add new files to GIT repository", 60)
            ExportProgress.addStep("Commit valid changes to GIT repository", 80)
            ExportProgress.addStep("Revert invalid changes from your checkout", 100)
            ExportProgress.Show()

            Do Until ExportProgress.isStarted
                Common.wait(1000)
            Loop

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
        If ExportProgress.toDoNextStep() Then

            Dim password = Main.get_password(Main.ParsingSchemaTextBox.Text, My.Settings.CurrentDB)
            'Export Apex as a single file
            'NB Not exporting application comments
            'Host.runInteractive("java oracle.apex.APEXExport -db " & connection & " -user " & username & " -password " & password & " -applicationid " & app_id & " -expPubReports -skipExportDate" _
            '                  , message, apex_dir)
            Host.check_StdErr("java oracle.apex.APEXExport -db " & connection & " -user " & username & " -password " & password & " -applicationid " & app_id & " -expPubReports -skipExportDate" _
                      , message, apex_dir)
            Logger.Dbg(message, "Apex Export Error")

            'write-host "Remove the application directory apex_dir\fapp_id" 


            'Remove-Item -Recurse -Force -ErrorAction 0 @("apex_dir\fapp_id")
            FileIO.deleteFolderIfExists(apex_dir & fapp_id)

        End If


            If ExportProgress.toDoNextStep() Then
                'Splitting into components 

                'write-host "Splitting $APP_SQL into its composite files"
                'java oracle.apex.APEXExportSplitter $APP_SQL 
                Host.check_StdErr("java oracle.apex.APEXExportSplitter " & fapp_sql, message, apex_dir)

                Logger.Dbg(message, "Apex Export Splitter Error")

            End If

            If ExportProgress.toDoNextStep() Then
                'Add new files to GIT repository 
                Tortoise.Add(apex_dir & fapp_id, True)

            End If

            If ExportProgress.toDoNextStep() Then
                'Commit valid changes to GIT repository  
                Tortoise.Commit(apex_dir & fapp_id, "App " & fapp_id & " exported and split - IF YOU DIDNT CHANGE IT PLEASE DONT COMMIT IT", True)

            End If

            If ExportProgress.toDoNextStep() Then
                'Revert invalid changes from your checkout
                Tortoise.Revert(apex_dir & fapp_id)
            End If

            ExportProgress.toDoNextStep()
      

    End Sub

    Public Shared Sub ApexImportFromTag()

   
            Dim fapp_id As String = My.Settings.CurrentApex

            Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)
            Dim runOnlyDBs As String = "isdevl,istest,isuat,isprod"

            Dim ImportProgress As ProgressDialogue = New ProgressDialogue ("Import APEX application " & fapp_id & " into DB " & My.Settings.CurrentDB, _
            "Importing APEX application " & My.Settings.CurrentApex & " into parsing schema " & Main.ParsingSchemaTextBox.Text & " in DB " & My.Settings.CurrentDB & Environment.NewLine & _
            "This will overwrite the existing APEX application." & Environment.NewLine & _
            Environment.NewLine & _
            "Consider creating a VM snapshot as a restore point." & Environment.NewLine & _
            "To save any existing changes, close this workflow and perform an APEX EXPORT.")
 

            ImportProgress.MdiParent = GitPatcher
            ImportProgress.addStep("Choose a tag to import from", 20)
            ImportProgress.addStep("Checkout the tag", 40)
            ImportProgress.addStep("If tag not like " & Main.AppCodeTextBox.Text & " relabel apex", 50)
            ImportProgress.addStep("If db in " & runOnlyDBs & " set apex to RUN_ONLY", 60)
            ImportProgress.addStep("Import Apex", 70)
            ImportProgress.addStep("Restore apex settings in checkout", 80)
            ImportProgress.addStep("Return to branch: " & currentBranch, 100)
            ImportProgress.Show()

            Do Until ImportProgress.isStarted
                Common.wait(1000)
            Loop

            If ImportProgress.toDoStep(0) Then
            'Choose a tag to import from
                Dim tagnames As Collection = New Collection
                tagnames.Add("HEAD")
                tagnames = GitSharpFascade.getTagList(My.Settings.CurrentRepo, tagnames, Main.CurrentBranchTextBox.Text)
                tagnames = GitSharpFascade.getTagList(My.Settings.CurrentRepo, tagnames, Main.AppCodeTextBox.Text)


                Dim tagApexVersion As String = Nothing
                tagApexVersion = ChoiceDialog.Ask("Please choose a tag for apex install", tagnames, "HEAD", "Choose tag")

                If ImportProgress.toDoStep(1) Then
                'Checkout the tag
                    GitBash.Switch(My.Settings.CurrentRepo, tagApexVersion)
                    If ImportProgress.toDoStep(2) Then
                    'If tag not like Main.AppCodeTextBox.Text relabel apex

                        If Not tagApexVersion.Contains(Main.AppCodeTextBox.Text) Then

                            Dim l_label As String = Nothing
                            'Host.check_StdOut("""" & My.Settings.GITpath & """ describe --tags", l_label, My.Settings.CurrentRepo, True)
                            'alternative method
                            'l_label = Host.getOutput("""" & My.Settings.GITpath & """ describe --tags", My.Settings.CurrentRepo) 

                            l_label = "GIT Tag: " & GitBash.describeTags(My.Settings.CurrentRepo)
                            ImportProgress.updateStepDescription(2, "Relabel apex with " & l_label)

                            modCreateApplication(l_label, "")
                        End If
                    End If
                End If
            End If

            If ImportProgress.toDoStep(3) Then
            'set apex to RUN MODE
                Dim l_build_status As String = Nothing

                If runOnlyDBs.Contains(Main.DBListComboBox().SelectedItem.ToString.ToLower) Then
                    l_build_status = "RUN_ONLY"
                Else
                    l_build_status = "RUN_AND_BUILD"
                End If

                ImportProgress.updateStepDescription(3, "Set apex to " & l_build_status)

                modCreateApplication("", l_build_status)

            End If


            If ImportProgress.toDoStep(4) Then
            'Import Apex
                Host.executeSQLscriptInteractive("install.sql" _
                                               , Main.RootApexDirTextBox.Text & My.Settings.CurrentApex _
                                               , Main.get_connect_string(Main.ParsingSchemaTextBox.Text, My.Settings.CurrentDB))

            End If

            If ImportProgress.toDoStep(5) Then
                'Restore apex settings in checkout
                Apex.restoreCreateApplication()
            End If


        If ImportProgress.toDoStep(6) Then
            'Return to branch
            GitBash.Switch(My.Settings.CurrentRepo, currentBranch)
        End If

            ImportProgress.toDoNextStep()


    End Sub


End Class