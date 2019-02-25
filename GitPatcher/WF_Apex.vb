Friend Class WF_Apex


    Public Shared Sub confirmApp()


        Dim appChoice As String = Nothing

        appChoice = ChoiceDialog.Ask("Please verify current Apex App", Globals.getAppCollection(), Globals.getAppName(), "Choose App", False, True)


        AppSettings.retrieveApp(appChoice, Main.RepoComboBox.Text)


    End Sub






    Public Shared Sub ApexExportCommit()

        confirmApp()

        Dim connection As String = Globals.currentConnection
        Dim username As String = Globals.currentParsingSchema

        Dim fapp_id As String = Globals.currentApex
        Dim apex_dir As String = Globals.RootApexDir

        Dim ExportProgress As ProgressDialogue = New ProgressDialogue("Export APEX application " & fapp_id & " from DB " & Globals.currentTNS & " " & connection,
            "Exporting APEX application " & Globals.currentApex & " from parsing schema " & Globals.currentParsingSchema & " in DB " & Globals.currentTNS & Environment.NewLine &
            "This writes individual apex files to the GIT Repo checkout, and then prompt to add and commit the changes." & Environment.NewLine &
            Environment.NewLine &
            "Consider which branch you are exporting to." & Environment.NewLine &
            "To commit any existing changes, close this workflow and perform a GIT COMMIT.")

        ExportProgress.MdiParent = GitPatcher
        ExportProgress.addStep("Export Apex as a single file")
        ExportProgress.addStep("Splitting into components")
        ExportProgress.addStep("Add new files to GIT repository")
        ExportProgress.addStep("Commit valid changes to GIT repository")
        ExportProgress.addStep("Revert invalid changes from your checkout")
        ExportProgress.Show()

        Do Until ExportProgress.isStarted
            Common.wait(1000)
        Loop

        ''write-host "APEX file export and commit - uses oracle.apex.APEXExport.class and java oracle.apex.APEXExportSplitter.class"
        ''Does this need to perform a pull from the master ??
        ''TortoiseGitProc.exe /command:"pull" /path:"apex_dir" | Out-Null
        ''add ojdbc5.jar to the CLASSPATH, in this case its on the checkout path

        Dim classpath As String = Environment.GetEnvironmentVariable("CLASSPATH")
        If String.IsNullOrEmpty(classpath) Then
            MsgBox("Environment variable CLASSPATH does not exist - please create and repeat.")
        End If
        'EG \oracle\jdbc\lib\ojdbc5.jar
        'If Not classpath.Contains(apex_dir & My.Settings.JDBCjar) Then
        '    classpath = classpath & ";" & apex_dir & My.Settings.JDBCjar
        '    Environment.SetEnvironmentVariable("CLASSPATH", classpath)
        'End If

        If Not classpath.Contains(Globals.getODBCjavaAbsPath()) Then
            classpath = classpath & ";" & Globals.getODBCjavaAbsPath()
            Environment.SetEnvironmentVariable("CLASSPATH", classpath)
        End If


        Logger.Dbg("Apex app_id " + fapp_id, "Check app id")

        Dim app_id As String = fapp_id.Split("f")(1)
        Dim fapp_sql As String = fapp_id & ".sql"
        Dim message As String = Nothing

        'PROGRESS 0
        If ExportProgress.toDoNextStep() Then

            Dim password = Main.get_password(Globals.currentParsingSchema, Globals.currentTNS)
            'Export Apex as a single file
            'NB Not exporting application comments
            'Host.runInteractive("java oracle.apex.APEXExport -db " & connection & " -user " & username & " -password " & password & " -applicationid " & app_id & " -expPubReports -skipExportDate" _
            '                  , message, apex_dir)
            Host.check_StdErr("java oracle.apex.APEXExport -db " & connection & " -user " & username & " -password " & password & " -applicationid " & app_id & " -expPubReports -skipExportDate" _
                      , message, apex_dir)


            Logger.Dbg(message, "Apex Export Error")

            'write-host "Remove the application directory apex_dir\fapp_id" 


        End If


        If ExportProgress.toDoNextStep() Then

            'Remove-Item -Recurse -Force -ErrorAction 0 @("apex_dir\fapp_id")
            FileIO.deleteFolderIfExists(apex_dir & fapp_id)

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

        confirmApp()

        Dim l_skip_reports_DBs As String = "DEV"
        Dim fapp_id As String = Globals.currentApex

        Dim currentBranch As String = GitOp.currentBranch()
        Dim runOnlyDBs As String = "DEV,TEST,UAT,PROD"

        Dim ImportProgress As ProgressDialogue = New ProgressDialogue("Import APEX application " & fapp_id & " into DB " & Globals.currentTNS,
        "Importing APEX application " & Globals.currentApex & " into parsing schema " & Globals.currentParsingSchema & " in DB " & Globals.currentTNS & Environment.NewLine &
        "This will overwrite the existing APEX application." & Environment.NewLine &
        Environment.NewLine &
        "Consider creating a VM snapshot as a restore point." & Environment.NewLine &
        "To save any existing changes, close this workflow and perform an APEX EXPORT.")


        ImportProgress.MdiParent = GitPatcher
        ImportProgress.addStep("Choose a tag to install apex from and checkout the tag", False)
        ImportProgress.addStep("If tag not like " & Globals.currentAppCode & " relabel apex", False)
        ImportProgress.addStep("If db in " & runOnlyDBs & " set apex to RUN_ONLY", False)
        ImportProgress.addStep("If db in " & l_skip_reports_DBs & " set install.sql to Skip reports queries and layouts", False, "This is intended to speed up imports into " & l_skip_reports_DBs & ", where they otherwise take upto 30mins")
        ImportProgress.addStep("Import Apex", True)
        ImportProgress.addStep("Restore apex settings in checkout", False, "Revert changes made in steps 3, 4 and 5.  These were only required for the import.  They do not need to be committed.")
        ImportProgress.addStep("Return to branch: " & currentBranch, False)
        ImportProgress.Show()

        Do Until ImportProgress.isStarted
            Common.wait(1000)
        Loop

        If ImportProgress.toDoStep(0) Then
            'Choose a tag to import from
            Dim tagnames As Collection = New Collection
            tagnames.Add("HEAD")
            tagnames = GitOp.getTagList(tagnames, Globals.currentBranch)
            tagnames = GitOp.getTagList(tagnames, Globals.currentAppCode)


            Dim tagApexVersion As String = Nothing
            tagApexVersion = ChoiceDialog.Ask("Please choose a tag for apex install", tagnames, "HEAD", "Choose tag")


            'Checkout the tag
            GitOp.switchBranch(tagApexVersion)
            If ImportProgress.toDoNextStep Then
                'If tag not like Globals.currentAppCode relabel apex

                If Not tagApexVersion.Contains(Globals.currentAppCode) Then

                    Dim l_label As String = Nothing
                    'Host.check_StdOut("""" & My.Settings.GITpath & """ describe --tags", l_label, Globals.currentRepo, True)
                    'alternative method
                    'l_label = Host.getOutput("""" & My.Settings.GITpath & """ describe --tags", Globals.currentRepo) 

                    l_label = "GIT Tag: " & GitBash.describeTags(Globals.getRepoPath)
                    ImportProgress.updateStepDescription(1, "Relabel apex with " & l_label)

                    Apex.modCreateApplicationSQL(l_label, "")
                End If
            End If

        End If

        If ImportProgress.toDoStep(2) Then
            'set apex to RUN MODE
            Dim l_build_status As String = Nothing

            If runOnlyDBs.Contains(Globals.getDB) Then
                l_build_status = "RUN_ONLY"
            Else
                l_build_status = "RUN_AND_BUILD"
            End If

            ImportProgress.updateStepDescription(2, "Set apex to " & l_build_status)

            Apex.modCreateApplicationSQL("", l_build_status)

        End If


        If ImportProgress.toDoNextStep Then
            'Skip reports queries and layouts

            If l_skip_reports_DBs.Contains(Globals.getDB) Then
                Apex.modInstallSQL()
                ImportProgress.updateStepDescription(3, "Import will SKIP reports queries and layouts")
            Else
                ImportProgress.updateStepDescription(3, "Import to INCLUDE reports queries and layouts")
            End If

        End If


        If ImportProgress.toDoNextStep Then
            'Import Apex
            Host.executeSQLscriptInteractive("install.sql" _
                                           , Globals.RootApexDir & Globals.currentApex _
                                           , Main.get_connect_string(Globals.currentParsingSchema, Globals.currentTNS))

        End If

        If ImportProgress.toDoNextStep Then
            'Restore apex settings in checkout
            Apex.restoreCreateApplicationSQL()
            Apex.restoreInstallSQL()
        End If


        If ImportProgress.toDoNextStep Then
            'Return to branch
            GitBash.Switch(Globals.getRepoPath, currentBranch)
        End If

        ImportProgress.toDoNextStep()


    End Sub


    Shared Sub ApexImport1PageFromTag()

        confirmApp()

        Dim applicationDir As String = Globals.RootApexDir & Globals.currentApex & "\application\"
        Dim pagesDir As String = applicationDir & "pages\"


        Dim fapp_id As String = Globals.currentApex

        Dim currentBranch As String = GitOp.currentBranch()

        Dim ImportProgress As ProgressDialogue = New ProgressDialogue("Import 1 APEX page " & fapp_id & " into DB " & Globals.currentTNS,
        "Importing 1 APEX page of Application " & Globals.currentApex & " into parsing schema " & Globals.currentParsingSchema & " in DB " & Globals.currentTNS & Environment.NewLine &
        "This will overwrite only 1 page in APEX application." & Environment.NewLine &
        Environment.NewLine &
        "Consider creating a VM snapshot as a restore point." & Environment.NewLine &
        "To save any existing changes, close this workflow and perform an APEX EXPORT.")


        ImportProgress.MdiParent = GitPatcher
        ImportProgress.addStep("Choose a tag to install apex from and checkout the tag", False)
        ImportProgress.addStep("Import Apex Page", True, "Choose Apex Page from list of pages, to apply to current DB.")
        ImportProgress.addStep("Return to branch: " & currentBranch, False)
        ImportProgress.Show()

        Do Until ImportProgress.isStarted
            Common.wait(1000)
        Loop

        Try


            If ImportProgress.toDoStep(0) Then
                'Choose a tag to import from
                Dim tagnames As Collection = New Collection
                tagnames.Add("HEAD")
                tagnames = GitOp.getTagList(tagnames, Globals.currentBranch)
                tagnames = GitOp.getTagList(tagnames, Globals.currentAppCode)


                Dim tagApexVersion As String = Nothing
                tagApexVersion = ChoiceDialog.Ask("Please choose a tag for apex install", tagnames, "HEAD", "Choose tag")

            End If

            If ImportProgress.toDoNextStep Then


                Dim pages As Collection = New Collection

                'C:\Dev\apex_apps\apex\f101\application\pages

                pages = FileIO.FileList(pagesDir, "page_*.sql", pagesDir)

                Dim page_file As String = Nothing
                page_file = ChoiceDialog.Ask("Please choose a page to be installed", pages, "", "Choose Page")

                ImportProgress.updateStepDescription(1, "Import Apex Page Filename: " & page_file)

                'write a lauch page
                Apex.Install1Page(page_file)

            End If

            If ImportProgress.toDoNextStep Then
                'Return to branch
                GitOp.switchBranch(currentBranch)
            End If

            ImportProgress.toDoNextStep()


        Catch page_not_selected As Halt
            MsgBox("No page selected")
            ImportProgress.stopAndClose()
        End Try


    End Sub

End Class
