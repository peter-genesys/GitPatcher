Friend Class WF_Apex

    Public Shared Sub ReimportApp(ByVal iSchema As String, ByVal iAppId As String)

        'Confirm reimport of app
        Dim result As Integer = MessageBox.Show("Please confirm Re-Install of cleaned version of the App " & iAppId & " into " & Globals.getDB & Environment.NewLine &
                                                "Any changes made to files, during the commit or revert stages, will be reloaded into the DB.",
                                                "Confirm Re-Install of Clean App", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
        If result = DialogResult.Cancel Then
            Exit Sub
        End If

        'Format as script
        Dim masterList As String = "SET SERVEROUTPUT ON" &
             Environment.NewLine & "WHENEVER OSERROR EXIT FAILURE ROLLBACK" &
             Environment.NewLine & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK" &
             Environment.NewLine & "DEFINE database = '" & Globals.getDATASOURCE & "'" &
             Environment.NewLine & "@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql" &
             Environment.NewLine & "CONNECT &&" & iSchema & "_user/&&" & iSchema & "_password@&&database" &
             Environment.NewLine & "execute arm_installer.set_security_for_apex_import(-" &
             Environment.NewLine & "     i_schema => '&&" & iSchema & "_USER'-" &
             Environment.NewLine & "   , i_workspace => '&&" & iAppId & "_WORKSPACE');" &
             Environment.NewLine & "cd " & iSchema & "\" & iAppId &
             Environment.NewLine & "@install.sql"

        ApexAppInstaller.RunMasterScript(masterList)
    End Sub


    Public Shared Sub ApexSplitExportCommit(ByVal iSchema As String, ByVal iAppId As String, Optional ByVal iReimport As Boolean = False) 'Current

        Dim connection As String = Globals.currentConnection
        Dim username As String = iSchema

        Dim fapp_id As String = iAppId
        Dim apex_dir As String = Globals.RootApexDir

        Dim ExportProgress As ProgressDialogue = New ProgressDialogue("Export APEX application " & fapp_id & " from DB " & Globals.currentTNS & " " & connection,
            "Exporting APEX application " & fapp_id & " from parsing schema " & username & " in DB " & Globals.currentTNS & Environment.NewLine &
            "This writes individual apex files to the GIT Repo checkout, and then prompt to add and commit the changes." & Environment.NewLine &
            Environment.NewLine &
            "Consider which branch you are exporting to." & Environment.NewLine &
            "To commit any existing changes, close this workflow and perform a GIT COMMIT.")

        ExportProgress.MdiParent = GitPatcher
        'EXPORT-APP
        ExportProgress.addStep("Export Apex App " & fapp_id & ", split into components")
        'ADD-NEW-FILES  
        'DEPRECATED. Now handled automatically in COMMIT-CHANGES
        ExportProgress.addStep("Add new files to GIT repository", True, "Add new files", False)
        'COMMIT-CHANGES
        ExportProgress.addStep("Commit new and changed files to GIT repository", True,
                               "New files are automatically added before the commit dialogue is shown." & Chr(10) &
                               "Please inspect every changed file and commit only intended changes." & Chr(10) &
                               "NB APEX makes changes too.  You will see no audit user and time for these changes." & Chr(10) &
                               "Eg Changes are often made to parameters within the scripts." & Chr(10) &
                               "   You SHOULD commit these changes too.")

        'REVERT-CHANGES
        ExportProgress.addStep("Revert invalid changes from your checkout")
        'REIMPORT-CLEAN-APP
        ExportProgress.addStep("Reimport the clean version", False, "If changes have been reverted, then it may be useful to reimport the entire clean app into the DB.", iReimport)
        'COMMIT-UNCLEAN-APP
        ExportProgress.addStep("Commit unclean version", False, "As a temporary measure, for release via SVN, you may optionally choose to commit the unclean full export too.")
        ExportProgress.Show()

        Do Until ExportProgress.isStarted
            Common.Wait(1000)
        Loop

        Logger.Dbg("Apex app_id " & fapp_id, "Check app id")

        Dim app_id As String = fapp_id.Split("f")(1)

        Dim parsingSchemaDir As String = apex_dir & iSchema
        Dim appDir As String = parsingSchemaDir & "\" & fapp_id

        Dim fapp_sql As String = fapp_id & ".sql"
        Dim message As String = Nothing


        Try 'Finish workflow if an error occurs

            'EXPORT-APP
            If ExportProgress.toDoNextStep() Then

                'Delete the appDir prior to the export.
                FileIO.deleteFolderIfExists(appDir)

                'Use Host class to execute with a master script.
                Host.RunMasterScript("prompt Exporting Apex App " & app_id &
                    Environment.NewLine & "@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql" &
                    Environment.NewLine & "CONNECT &&" & iSchema & "_user/&&" & iSchema & "_password@" & Globals.getDATASOURCE &
                    Environment.NewLine & "Apex export -applicationid " & app_id & " -skipExportDate -split" &
                    Environment.NewLine & "exit;" _
                  , parsingSchemaDir)

                Dim AppFilePath As String = parsingSchemaDir & "\" & fapp_id & ".sql"
                Dim uncleanAppFilename As String = fapp_id & ".unclean.sql"

                FileIO.deleteFileIfExists(parsingSchemaDir & "\" & uncleanAppFilename)

                FileIO.RenameFile(AppFilePath, uncleanAppFilename)

            End If

            'ADD-NEW-FILES
            If ExportProgress.toDoNextStep() Then
                If GitOp.IsDirty() Then
                    Logger.Dbg("User chose to add And the checkout Is also dirty")
                    'Add new files to GIT repository 
                    Tortoise.Add(appDir, True)
                End If
            End If

            'COMMIT-CHANGES
            If ExportProgress.toDoNextStep() Then

                'Use GitBash to silently add files prior to calling commit dialog.
                Try
                    GitBash.Add(Globals.getRepoPath, appDir & "\*", True)
                Catch ex As Exception
                    MsgBox(ex.Message)
                    MsgBox("Unable to Add Files with GitBash. Check GitBash configuration.")
                    'If GitBash.Push fails just let the process continue.
                    'User will add files via the commit dialog
                End Try

                'User chose to commit, but don't bother unless the checkout has staged changes (added, modified or deleted files)
                'Committing changed files to GIT"
                If GitOp.ChangedFiles() > 0 Then
                    Logger.Dbg("User chose to commit and the checkout has staged changes")

                    'Find the application name in the init.sql file.
                    Dim lAppIdAndName As String = Common.cleanString(FileIO.getTextBetween(appDir & "\application\init.sql", "prompt APPLICATION ", "--"))

                    'Commit valid changes to GIT repository  
                    Tortoise.Commit(appDir, "Apex App " & lAppIdAndName & " (" & Globals.currentTNS & ") " & vbLf & vbLf & "GitPatcher Split-Export from " & Globals.currentTNS, True)

                End If

            End If

            'REVERT-CHANGES
            Dim raisedRevertDialog As Boolean = False
            'User chose to revert, but don't bother unless the checkout has staged changes (added, modified or deleted files)
            If ExportProgress.toDoNextStep() Then
                If GitOp.ChangedFiles() > 0 Then
                    Logger.Dbg("User chose to revert and the checkout has staged changes")
                    raisedRevertDialog = True
                    'Revert invalid changes from your checkout
                    Tortoise.Revert(appDir)
                End If
            End If

            'REIMPORT-CLEAN-APP
            If ExportProgress.toDoNextStep() Then
                ReimportApp(iSchema, iAppId)
            ElseIf Not ExportProgress.IsDisposed Then 'ignore if form has been closed.
                If raisedRevertDialog Then
                    'User chose NOT to REIMPORT, but the revert dialog was used, so it is likely that they reverted some changes, so start the import.  
                    ReimportApp(iSchema, iAppId)
                End If

            End If

            'COMMIT-UNCLEAN-APP
            If ExportProgress.toDoNextStep() Then

                Dim uncleanAppFilePath As String = parsingSchemaDir & "\" & fapp_id & ".unclean.sql"
                Dim fullAppFilename As String = fapp_id & ".full.sql"

                FileIO.deleteFileIfExists(parsingSchemaDir & "\" & fullAppFilename)

                FileIO.RenameFile(uncleanAppFilePath, fullAppFilename)

                'Find the application name in the init.sql file.
                Dim lAppIdAndName As String = Common.cleanString(FileIO.getTextBetween(appDir & "\application\init.sql", "prompt APPLICATION ", "--"))

                'Commit valid changes to GIT repository  
                Tortoise.Commit(parsingSchemaDir, "Apex App " & lAppIdAndName & " (" & Globals.currentTNS & ") " & vbLf & vbLf & "GitPatcher Full-Unclean-Export from " & Globals.currentTNS, True)

            End If

            ExportProgress.toDoNextStep()

        Catch ex As Exception
            MsgBox(ex.Message)
            ExportProgress.setToCompleted()
            ExportProgress.Close()
        End Try




    End Sub



    Public Shared Sub FindApps(ByRef foundApps As Collection)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundApps.Clear()

        If IO.Directory.Exists(Globals.RootApexDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootApexDir, "install.sql", foundApps, Globals.RootApexDir)

        End If

        If foundApps.Count = 0 Then
            Throw New Exception("No Apps found")
        End If

        Cursor.Current = cursorRevert

    End Sub

    Public Shared Sub ChooseApp(ByRef iSchema As String, ByRef iAppId As String)
        Dim AvailableApps As Collection = New Collection()
        FindApps(AvailableApps) 'look for all apps
        Dim appChoice As String = Nothing

        appChoice = ChoiceDialog.Ask("Please choose an Apex App", AvailableApps, "", "Choose App", False, False)

        iSchema = Common.getFirstSegment(appChoice, "\")
        iAppId = Common.getLastSegment(appChoice, "\")

    End Sub



    Public Shared Sub ApexRestoreSinglePage() 'Current

        Dim currentBranch As String = GitOp.CurrentBranch()
        Dim lSchema As String = Nothing
        Dim lAppId As String = Nothing
        Dim applicationDir As String = Nothing
        Dim pagesDir As String = Nothing
        Dim page_file As String = Nothing


        Dim RestoreProgress As ProgressDialogue = New ProgressDialogue("Import 1 APEX page into " & Globals.getDB & " DB.",
        "Import 1 APEX page into " & Globals.getDB & " DB." & Environment.NewLine &
        "This will overwrite only 1 page of the APEX application." & Environment.NewLine & Environment.NewLine &
        "To backup the current state of the Apex App consider :" & Environment.NewLine &
        " + Exporting the Apex App " & Environment.NewLine &
        " + Snapshoting the VM")


        RestoreProgress.MdiParent = GitPatcher
        'CHOOSE-APP
        RestoreProgress.addStep("Choose the Apex App", True, "Choose the Apex App that will have a page restored.")
        'EXPORT-APP
        RestoreProgress.addStep("Export the Apex App", False, "Export the Apex App to the current branch.")
        'SWITCH-CHECKOUT
        RestoreProgress.addStep("Switch the checkout", False, "Switch to a branch, tag or commit that has the correct version of the Apex Page." &
                                                                Chr(10) & "Otherwise use the version currently in the checkout.")

        'CHOOSE-PAGE
        RestoreProgress.addStep("Choose the Page", True, "Choose the Apex Page from list of pages")
        'CREATE-SNAPSHOT
        RestoreProgress.addStep("Create a pre-page-restore VM snapshot", Globals.getDB = "VM", "Use this restore point to to undo this page restore.")
        'IMPORT-PAGE
        RestoreProgress.addStep("Import the Page", True, "Import the page to current DB.")
        'RETURN-TO-BRANCH
        RestoreProgress.addStep("Return to branch: " & currentBranch, True, "Return to the original branch.")
        RestoreProgress.Show()


        Do Until RestoreProgress.isStarted
            Common.Wait(1000)
        Loop


        Try 'Finish workflow if an error occurs

            'CHOOSE-APP
            If RestoreProgress.toDoNextStep Then
                ChooseApp(lSchema, lAppId)
                applicationDir = Globals.RootApexDir & lSchema & "\" & lAppId & "\application\"
                pagesDir = applicationDir & "pages\"
            End If

            'EXPORT-APP
            If RestoreProgress.toDoNextStep Then
                WF_Apex.ApexSplitExportCommit(lSchema, lAppId, True)
            End If

            'SWITCH-CHECKOUT
            If RestoreProgress.toDoNextStep Then

                'USE TORTOISE TO PICK A COMMIT
                Tortoise.Switch(Globals.getRepoPath)

            End If

            'CHOOSE-PAGE
            If RestoreProgress.toDoNextStep Then


                Dim pages As Collection = New Collection

                pages = FileIO.FileList(pagesDir, "page_?????.sql", pagesDir)

                page_file = ChoiceDialog.Ask("Please choose a page to be installed", pages, "", "Choose Page")

            End If

            'CREATE-SNAPSHOT
            If RestoreProgress.toDoNextStep() Then
                'Snapshot VM
                If My.Settings.VBoxName = "No VM" Then
                    MsgBox("Create a pre-page-restore VM snapshot.  " & Chr(10) & "Restore for undo of this page restore.", MsgBoxStyle.Exclamation, "Snapshot VM")
                Else
                    Dim lpage As String = Common.dropLastSegment(page_file, ".")
                    WF_virtual_box.takeSnapshot(lAppId & "-pre-restore-" & lpage)
                End If

            End If


            'IMPORT-PAGE
            If RestoreProgress.toDoNextStep() Then

                'write a lauch page
                Apex.Install1Page(page_file, applicationDir, lSchema)
            End If

            'RETURN-TO-BRANCH
            If RestoreProgress.toDoNextStep Then
                'Return to branch
                GitOp.SwitchBranch(currentBranch)
            End If

            RestoreProgress.toDoNextStep()

        Catch ex As Exception
            MsgBox(ex.Message)
            RestoreProgress.setToCompleted()
            RestoreProgress.Close()
        End Try




    End Sub




    Public Shared Sub confirmApp() 'Deprecated


        Dim appChoice As String = Nothing

        appChoice = ChoiceDialog.Ask("Please verify current Apex App", Globals.getAppCollection(), Globals.getAppName(), "Choose App", False, True)


        AppSettings.retrieveApp(appChoice, Main.RepoComboBox.Text)


    End Sub



    Public Shared Sub ApexExportCommit() 'Deprecated, keep code examples

        'This routine uses a hostout to oracle.apex.APEXExport And oracle.apex.APEXExportSplitter
        'This function has now been built into SQLcl.

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
            Common.Wait(1000)
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




    Public Shared Sub ApexImportFromTag() 'Deprecated, keep code examples

        confirmApp()

        Dim l_skip_reports_DBs As String = "DEV"
        Dim fapp_id As String = Globals.currentApex

        Dim currentBranch As String = GitOp.CurrentBranch()
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
            Common.Wait(1000)
        Loop

        If ImportProgress.toDoNextStep Then
            'Choose a tag to import from
            Dim tagnames As Collection = New Collection
            tagnames.Add("HEAD")
            tagnames = GitOp.getTagNameList(tagnames, Globals.currentBranch)
            tagnames = GitOp.getTagNameList(tagnames, Globals.currentAppCode)


            Dim tagApexVersion As String = Nothing
            tagApexVersion = ChoiceDialog.Ask("Please choose a tag for apex install", tagnames, "HEAD", "Choose tag")

            'Checkout the tag
            GitOp.SwitchTagName(tagApexVersion)

            If ImportProgress.toDoNextStep Then
                'If tag not like Globals.currentAppCode relabel apex

                If Not tagApexVersion.Contains(Globals.currentAppCode) Then

                    Dim l_label As String = Nothing
                    'Host.check_StdOut("""" & My.Settings.GITpath & """ describe --tags", l_label, Globals.currentRepo, True)
                    'alternative method
                    'l_label = Host.getOutput("""" & My.Settings.GITpath & """ describe --tags", Globals.currentRepo) 

                    l_label = "GIT Tag: " & GitOp.describeTags(Globals.currentBranch())

                    ImportProgress.updateStepDescription(1, "Relabel apex with " & l_label)

                    Apex.modCreateApplicationSQL(l_label, "")
                End If
            End If
        Else
            ImportProgress.forceSkipNextStep()
        End If

        If ImportProgress.toDoNextStep Then
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
                                           , Main.get_connect_string(Globals.currentParsingSchema, Globals.currentTNS, Globals.getDATASOURCE))

        End If

        If ImportProgress.toDoNextStep Then
            'Restore apex settings in checkout
            Apex.restoreCreateApplicationSQL()
            Apex.restoreInstallSQL()
        End If


        If ImportProgress.toDoNextStep Then
            'Return to branch
            GitOp.SwitchBranch(currentBranch)

        End If

        ImportProgress.toDoNextStep()


    End Sub


    Shared Sub ApexImport1PageFromTag() 'Deprecated, keep code examples

        confirmApp()

        Dim applicationDir As String = Globals.RootApexDir & Globals.currentApex & "\application\"
        Dim pagesDir As String = applicationDir & "pages\"


        Dim fapp_id As String = Globals.currentApex

        Dim currentBranch As String = GitOp.CurrentBranch()

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
            Common.Wait(1000)
        Loop

        Try


            If ImportProgress.toDoNextStep Then
                'Choose a tag to import from
                Dim tagnames As Collection = New Collection
                tagnames.Add("HEAD")
                tagnames = GitOp.getTagNameList(tagnames, Globals.currentBranch)
                tagnames = GitOp.getTagNameList(tagnames, Globals.currentAppCode)


                Dim tagApexVersion As String = Nothing
                tagApexVersion = ChoiceDialog.Ask("Please choose a tag for apex install", tagnames, "HEAD", "Choose tag")

                'Checkout the tag
                GitOp.SwitchTagName(tagApexVersion)

            End If

            If ImportProgress.toDoNextStep Then


                Dim pages As Collection = New Collection

                'C:\Dev\apex_apps\apex\f101\application\pages

                pages = FileIO.FileList(pagesDir, "page_?????.sql", pagesDir)

                Dim page_file As String = Nothing
                page_file = ChoiceDialog.Ask("Please choose a page to be installed", pages, "", "Choose Page")

                ImportProgress.updateStepDescription(1, "Import Apex Page Filename: " & page_file)

                'write a lauch page
                Apex.Install1Page(page_file, "dummy", "dummy")

            End If

            If ImportProgress.toDoNextStep Then
                'Return to branch
                GitOp.SwitchBranch(currentBranch)
            End If

            ImportProgress.toDoNextStep()

        Catch ex As Exception
            MsgBox(ex.Message)
            ImportProgress.setToCompleted()
            ImportProgress.Close()
        End Try


    End Sub

End Class
