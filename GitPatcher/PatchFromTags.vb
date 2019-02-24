﻿
Public Class PatchFromTags

    Dim gBranchType As String
    Dim gDBtarget As String
    Dim gRebaseBranchOn As String
    Dim gtag_base As String

    Public Sub New(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String, itag_base As String)
        InitializeComponent()

        FindTagsButton.Text = "Find Tags like " & Globals.currentBranch & "."

        Findtags()

        gBranchType = iBranchType
        gDBtarget = iDBtarget
        gRebaseBranchOn = iRebaseBranchOn
        gtag_base = itag_base

        PatchTabControl.TabPages.Remove(TabPageSHA1)

        HideChangesTab()
        HideTabs()

        ExecuteButton.Text = "Execute Patch on " & Globals.currentTNS

    End Sub

    Private Sub UseTags()
        PatchTabControl.TabPages.Insert(0, TabPageTags)
        PatchTabControl.TabPages.Remove(TabPageSHA1)
        HideTabs()
    End Sub

    Private Sub UseSHA1()
        PatchTabControl.TabPages.Insert(0, TabPageSHA1)
        PatchTabControl.TabPages.Remove(TabPageTags)
        HideTabs()
    End Sub

    Private Sub HideChangesTab()
        PatchTabControl.TabPages.Remove(TabPageChanges)
    End Sub

    Private Sub ShowChangesTab()
        PatchTabControl.TabPages.Remove(TabPageChanges)
        PatchTabControl.TabPages.Insert(1, TabPageChanges)
    End Sub

    Private Sub HideTabs()

        PatchTabControl.TabPages.Remove(TabPageExtras)
        PatchTabControl.TabPages.Remove(TabPagePreReqsA)
        PatchTabControl.TabPages.Remove(TabPagePreReqsB)
        PatchTabControl.TabPages.Remove(TabPagePatchDefn)
        FindButton.Visible = False
        CopyChangesButton.Visible = False

    End Sub
    Private Sub ShowTabs()

        PatchTabControl.TabPages.Insert(2, TabPageExtras)
        PatchTabControl.TabPages.Insert(3, TabPagePreReqsA)
        PatchTabControl.TabPages.Insert(4, TabPagePreReqsB)
        PatchTabControl.TabPages.Insert(5, TabPagePatchDefn)
    End Sub


    Private Sub Findtags()

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim tag_no_padding As Integer = 2

        TagsCheckedListBox.Items.Clear()
        For Each tagname In GitOp.getTagList()
            If Common.getFirstSegment(tagname, ".") = Globals.currentBranch Then
                'This is a tag worth listing
                Dim ticked As Boolean = (gtag_base = Common.getLastSegment(tagname, ".").Substring(0, tag_no_padding)) 'This is a tag worth ticking
                TagsCheckedListBox.Items.Add(tagname, ticked)

            End If
        Next

        Cursor.Current = cursorRevert

    End Sub

    Private Sub deriveSchemas()
        'NB This routine also causes the Globals.commit1 and Globals.commit2 to be calculated, for reuse in the Wizard.

        If String.IsNullOrEmpty(SchemaComboBox.Text) Then
            SchemaComboBox.Items.Clear()

            Try

                If PatchTabControl.TabPages.Contains(TabPageTags) Then
                    'Tags
                    Logger.Dbg("Load schemas from Tags")

                    GitOp.setCommitsFromTags(Tag1TextBox.Text, Tag2TextBox.Text)

                Else
                    Logger.Dbg("Load schemas from SHA1s")

                    GitOp.setCommitsFromSHA(SHA1TextBox1.Text, SHA1TextBox2.Text)


                End If

                For Each schema In GitOp.getSchemaList(Globals.DBRepoPathMask)
                    SchemaComboBox.Items.Add(schema)
                Next

                SchemaCountTextBox.Text = SchemaComboBox.Items.Count

                'If exactly one schema found then select it
                'otherwise force user to choose one.
                If SchemaComboBox.Items.Count = 0 Then
                    MsgBox("No schemas found.  Please check location of tags or SHA-1 (esp SHA-1 order)")

                ElseIf SchemaComboBox.Items.Count = 1 Then
                    SchemaComboBox.SelectedIndex = 0
                Else
                    MsgBox("There are changes across " & SchemaComboBox.Items.Count.ToString & " schemas.")
                    Logger.Dbg("Multiple schemas")
                End If
            Catch ex As Exception
                MsgBox("Unable to find Changes" & vbCrLf & ex.Message)
            End Try


        End If
    End Sub



    Private Sub FindChanges()
        Try
            If SchemaComboBox.Text = "" Then
                Throw (New Halt("Schema not selected"))
            End If

            TreeViewChanges.PathSeparator = "/"
            TreeViewChanges.Nodes.Clear()

            For Each change In GitOp.getChanges(Globals.DBRepoPathMask & SchemaComboBox.Text, False)

                'find or create each node for item
                TreeViewChanges.AddNode(change, "/", True)

            Next

            TreeViewChanges.ExpandAll()

            HideTabs()
            ShowTabs()
            ResetForNewPatch()

        Catch schema_not_selected As Halt
            MsgBox("Please select a schema")
        End Try
    End Sub


    Private Sub FindButton_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindChanges()
    End Sub


    Private Sub exportExtraFiles(ByRef extrasCollection As Collection, ByRef filenames As Collection, ByVal patch_dir As String)

        Dim Filename As String = Nothing
        If extrasCollection.Count > 0 Then
            For Each FilePath In extrasCollection
                'The following filter does not work, and does not appear to be needed.
                'If InStr(FilePath, Globals.DBRepoPathMask) = 0 Then
                'Screened out repo files

                Try
                    FileIO.CopyFileToDir(FilePath, patch_dir)
                    filenames.Add(Filename)
                Catch ex As Exception
                    MsgBox("Warning: File " & FilePath & " could not be exported, but will be in the install file.  It may be a folder.  Deselect, then recreate Patch.")

                End Try

            Next
        End If
    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click

        'Create Patch Dir
        Try
            FileIO.confirmDeleteFolder(PatchDirTextBox.Text)
        Catch cancelled_delete As Halt
            MsgBox("Warning: Now overwriting existing patch")
        End Try

        Dim l_create_folder As String = Nothing
        For Each folder In PatchDirTextBox.Text.Split("\")
            If String.IsNullOrEmpty(l_create_folder) Then
                l_create_folder = folder
            Else
                l_create_folder = l_create_folder & "\" & folder
            End If

            'l_create_folder = l_create_folder & "\" & folder
            FileIO.createFolderIfNotExists(l_create_folder)
        Next

        FileIO.createFolderIfNotExists(PatchDirTextBox.Text)

        'Get a list of selected changed files (filepaths) 
        Dim changesFiles As Collection = New Collection
        'Repo changes
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        TreeViewChanges.ReadCheckedLeafNodes(changesFiles)

        'NO LONGER USED
        'Get a list of patchable files (filepaths) from the TreeViewPatchOrder to send to exportTagChanges and exportExtraFiles
        'TreeViewPatchOrder.ReadTags(patchableFiles, False, True, True, False)

        Dim filenames As Collection = Nothing
        filenames = GitOp.exportChanges(Globals.DBRepoPathMask & SchemaComboBox.Text, changesFiles, PatchDirTextBox.Text)


        'Additional file exports 
        Dim ExtraFiles As Collection = New Collection
        'Extra files
        'Retrieve checked node items from the TreeViewFiles as a collection of files.
        TreeViewFiles.ReadCheckedLeafNodes(ExtraFiles)


        exportExtraFiles(ExtraFiles, filenames, PatchDirTextBox.Text)

        Dim PreReqPatchesA As Collection = New Collection
        'Retrieve checked node items from the PreReqPatchesTreeView as a collection of patches.
        PreReqPatchesTreeViewA.ReadCheckedLeafNodes(PreReqPatchesA)

        Dim PreReqPatchesB As Collection = New Collection
        'Retrieve checked node items from the PreReqPatchesTreeView as a collection of patches.
        PreReqPatchesTreeViewB.ReadCheckedLeafNodes(PreReqPatchesB)

        Try
            Dim filelist As Collection = New Collection
            'Ok - no longer need the filenames list created by exportTagChanges and exportExtraFiles
            'Instead we will rederive this list from TreeViewPatchOrder
            TreeViewPatchOrder.ReadTags(filelist, False, False, False, False)


            Dim checkedFilelist As Collection = New Collection
            'List of files that have been ticked.
            TreeViewPatchOrder.ReadTags(checkedFilelist, False, True, False, True)


            'Write the install script - using ARM
            writeInstallScript(PatchNameTextBox.Text,
                               Common.getFirstSegment(Globals.currentLongBranch, "/"),
                               SchemaComboBox.Text,
                               Globals.currentLongBranch,
                               Tag1TextBox.Text,
                               Tag2TextBox.Text,
                               SupIdTextBox.Text,
                               PatchDescTextBox.Text,
                               NoteTextBox.Text,
                               True,
                               RerunCheckBox.Checked,
                               filelist,
                               checkedFilelist,
                               PreReqPatchesA,
                               PreReqPatchesB,
                               PatchDirTextBox.Text,
                               PatchPathTextBox.Text,
                               TrackPromoCheckBox.Checked,
                               AlternateSchemasCheckBox.Checked
                                )
            'Write the install script lite - without ARM
            writeInstallScript(PatchNameTextBox.Text,
                               Common.getFirstSegment(Globals.currentLongBranch, "/"),
                               SchemaComboBox.Text,
                               Globals.currentLongBranch,
                               Tag1TextBox.Text,
                               Tag2TextBox.Text,
                               SupIdTextBox.Text,
                               PatchDescTextBox.Text,
                               NoteTextBox.Text,
                               False,
                               RerunCheckBox.Checked,
                               filelist,
                               checkedFilelist,
                               PreReqPatchesA,
                               PreReqPatchesB,
                               PatchDirTextBox.Text,
                               PatchPathTextBox.Text,
                               TrackPromoCheckBox.Checked,
                               AlternateSchemasCheckBox.Checked)


            Host.RunExplorer(PatchDirTextBox.Text)
        Catch ex As ArgumentException
            'MsgBox(ex.ToString)
            MsgBox("There are duplicated filenames in the patch.  You may have selected an Extra File that is already in the Patch.")

        End Try


    End Sub




    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim CheckedChanges As Collection = New Collection

        'Retrieve checked node items from the TreeViewChanges as a collection of changes.
        TreeViewChanges.ReadCheckedLeafNodes(CheckedChanges)

        MsgBox(GitOp.viewChanges(Globals.getRepoPath, "database/" & SchemaComboBox.Text, CheckedChanges))
    End Sub




    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        derivePatchDir()
        'PatchDirTextBox.Text = Globals.RootPatchDir & Replace(PatchNameTextBox.Text, "/", "\") & "\"
    End Sub



    Private Sub writeInstallScript(ByVal patch_name As String,
                                  ByVal patch_type As String,
                                  ByVal db_schema As String,
                                  ByVal branch_path As String,
                                  ByVal tag1_name As String,
                                  ByVal tag2_name As String,
                                  ByVal suffix As String,
                                  ByVal patch_desc As String,
                                  ByVal note As String,
                                  ByVal use_arm As Boolean,
                                  ByVal rerunnable As Boolean,
                                  ByRef targetFiles As Collection,
                                  ByRef ignoreErrorFiles As Collection,
                                  ByRef prereqs_last_patch As Collection,
                                  ByRef prereqs_best_order As Collection,
                                  ByVal patchDir As String,
                                  ByVal groupPath As String,
                                  ByVal track_promotion As Boolean,
                                  ByVal alt_schema As Boolean)

        Dim l_app_code As String = Globals.getAppCode().ToUpper()

        Dim l_file_extension As String = Nothing
        Dim l_install_file_line As String = Nothing

        Dim l_all_programs As String = Nothing


        Dim l_show_error As String = "Show error;"

        Dim rerunnable_yn As String = "N"
        If rerunnable Then
            rerunnable_yn = "Y"
        End If


        Dim track_promotion_yn As String = "N"
        If track_promotion Then
            track_promotion_yn = "Y"
        End If


        Dim alt_schema_yn As String = "N"
        If alt_schema Then
            alt_schema_yn = "Y"
        End If


        Dim l_patch_started As String = Nothing

        Dim l_install_list As String = Nothing
        Dim l_post_install_list As String = Nothing

        Dim Category As String = Nothing



        If targetFiles.Count > 0 Then

            Dim l_log_filename As String = patch_name & ".log"
            Dim l_master_filename As String = Nothing

            If use_arm Then
                l_master_filename = "install.sql"
            Else
                l_master_filename = "install_lite.sql"
            End If



            Dim l_master_file As New System.IO.StreamWriter(patchDir & "\" & l_master_filename)

            l_master_file.WriteLine("PROMPT LOG TO " & l_log_filename)
            l_master_file.WriteLine("PROMPT .")
            l_master_file.WriteLine("SET AUTOCOMMIT OFF")
            l_master_file.WriteLine("SET AUTOPRINT ON")
            l_master_file.WriteLine("SET ECHO ON")
            l_master_file.WriteLine("SET FEEDBACK ON")
            l_master_file.WriteLine("SET PAUSE OFF")
            l_master_file.WriteLine("SET SERVEROUTPUT ON SIZE 1000000")
            l_master_file.WriteLine("SET TERMOUT ON")
            l_master_file.WriteLine("SET TRIMOUT ON")
            l_master_file.WriteLine("SET VERIFY ON")
            l_master_file.WriteLine("SET trims on pagesize 3000")
            l_master_file.WriteLine("SET auto off")
            l_master_file.WriteLine("SET verify off echo off define on")
            l_master_file.WriteLine("WHENEVER OSERROR EXIT FAILURE ROLLBACK")
            l_master_file.WriteLine("WHENEVER SQLERROR EXIT FAILURE ROLLBACK")
            l_master_file.WriteLine("")

            l_master_file.WriteLine("define patch_name = '" & patch_name & "'")
            l_master_file.WriteLine("define patch_desc = '" & patch_desc.Replace("'", "''") & "'")
            l_master_file.WriteLine("define patch_path = '" & branch_path & "/" & patch_name & "/" & "'")

            l_master_file.WriteLine("SPOOL " & l_log_filename)

            'Allow user to choose an alternate schema name at patch execution
            Dim l_schema As String = db_schema
            If alt_schema Then
                l_schema = "&&" & db_schema & "_user"
            End If

            'Force use of SYSDBA role
            Dim l_role As String = ""
            If SYSDBACheckBox.Checked Then
                l_role = " as sysdba"
            End If

            l_master_file.WriteLine("CONNECT " & l_schema & "/&&" & db_schema & "_password@&&database" & l_role)


            l_master_file.WriteLine("set serveroutput on;")

            'Files have already been sorted into Categories, we only need to list the categories and spit out the files under each.
            For Each l_filename In targetFiles


                'Sort the files by files extention into lists.

                If Not l_filename.contains(".") Then
                    'No file extension so assume this is a Category heading.
                    Category = l_filename.ToUpper
                    l_install_file_line = Chr(10) & "PROMPT " & Category


                Else

                    l_file_extension = l_filename.Split(".")(1)

                    'This is a releaseable file, so put an entry in the script.
                    If ignoreErrorFiles.Contains(l_filename) Then
                        l_install_file_line = Chr(10) & "WHENEVER SQLERROR CONTINUE" &
                                              Chr(10) & "PROMPT " & l_filename & " " &
                                              Chr(10) & "@&&patch_path." & l_filename & ";" &
                                              Chr(10) & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK"

                    Else
                        l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " &
                                            Chr(10) & "@&&patch_path." & l_filename & ";"

                    End If

                    'Add Show Error after these file types.
                    If "tps,tpb,pks,pls,pkb,plb,fnc,prc,vw,trg,dblink,mv".Contains(l_file_extension) Then
                        l_install_file_line = l_install_file_line & Chr(10) & l_show_error
                    End If


                    If String.IsNullOrEmpty(l_all_programs) Then
                        l_all_programs = l_filename
                    Else
                        l_all_programs = l_all_programs & "' -" & Chr(10) & "||'," & l_filename
                    End If


                End If

                If Category = "POST COMPLETION" Then
                    l_post_install_list = l_post_install_list & Chr(10) & l_install_file_line
                ElseIf Category = "DO NOT EXECUTE" Then
                    'Do nothing
                Else
                    'Add this file to the normal list
                    l_install_list = l_install_list & Chr(10) & l_install_file_line
                End If

            Next



            If use_arm Then

                l_patch_started =
                    "execute &&APEXRM_user..arm_installer.patch_started( -" _
        & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
        & Chr(10) & " ,i_patch_type         => '" & patch_type & "' -" _
        & Chr(10) & " ,i_db_schema          => '" & l_schema & "' -" _
        & Chr(10) & " ,i_app_code           => '" & l_app_code & "' -" _
        & Chr(10) & " ,i_branch_name        => '" & branch_path & "' -" _
        & Chr(10) & " ,i_tag_from           => '" & tag1_name & "' -" _
        & Chr(10) & " ,i_tag_to             => '" & tag2_name & "' -" _
        & Chr(10) & " ,i_suffix             => '" & suffix & "' -" _
        & Chr(10) & " ,i_patch_desc         => '" & patch_desc.Replace("'", "''") & "' -" _
        & Chr(10) & " ,i_patch_components   => '" & l_all_programs & "' -" _
        & Chr(10) & " ,i_patch_create_date  => '" & DateString & "' -" _
        & Chr(10) & " ,i_patch_created_by   => '" & Environment.UserName & "' -" _
        & Chr(10) & " ,i_note               => '" & note.Replace("'", "''") & "' -" _
        & Chr(10) & " ,i_rerunnable_yn      => '" & rerunnable_yn & "' -" _
        & Chr(10) & " ,i_tracking_yn        => '" & track_promotion_yn & "' -" _
        & Chr(10) & " ,i_alt_schema_yn      => '" & alt_schema_yn & "' -" _
        & Chr(10) & " ,i_retired_yn         => 'N' -" _
        & Chr(10) & " ,i_remove_prereqs     => 'N' );" _
        & Chr(10)


                l_master_file.WriteLine(l_patch_started)


                Dim l_prereq_short_name As String = Nothing
                For Each l_prereq_patch In prereqs_last_patch
                    l_prereq_short_name = Common.getLastSegment(l_prereq_patch, "\")
                    If l_prereq_short_name = PatchNameTextBox.Text Then
                        MsgBox("A Patch may NOT have itself as a prerequisite, skipping Prerequisite Patch: " & l_prereq_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
                    Else
                        l_master_file.WriteLine("PROMPT")
                        l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_short_name)
                        l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.add_prereq_last_patch( -")
                        l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                        l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")
                    End If

                Next

                For Each l_prereq_patch In prereqs_best_order
                    l_prereq_short_name = Common.getLastSegment(l_prereq_patch, "\")
                    If l_prereq_short_name = PatchNameTextBox.Text Then
                        MsgBox("A Patch may NOT have itself as a prerequisite, skipping Prerequisite Patch: " & l_prereq_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
                    Else
                        l_master_file.WriteLine("PROMPT")
                        l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_short_name)
                        l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.add_prereq_best_order( -")
                        l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                        l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")
                    End If

                Next

                l_prereq_short_name = My.Settings.MinPatch
                l_master_file.WriteLine("PROMPT Ensure ARM is late enough for this patch")
                l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.add_prereq_best_order( -")
                l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")

            End If

            l_master_file.WriteLine("select user||'@'||global_name Connection from global_name;")

            'Write the list of files to execute.
            l_master_file.WriteLine(l_install_list)

            l_master_file.WriteLine(Chr(10) & "COMMIT;")

            If use_arm Then

                l_master_file.WriteLine("PROMPT Compiling objects in schema " & l_schema)
                l_master_file.WriteLine("execute &&APEXRM_user..arm_invoker.compile_post_patch;")

                If db_schema = "APEXRM" Then
                    l_master_file.WriteLine("--APEXRM patches are likely to loose the session state of arm_installer, so complete using the patch_name parm.")
                    l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.patch_completed(i_patch_name  => '" & patch_name & "');")
                Else
                    l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.patch_completed;")
                End If





            End If

            l_master_file.WriteLine("COMMIT;")

            l_master_file.WriteLine("PROMPT ")
            l_master_file.WriteLine("PROMPT " & l_master_filename & " - COMPLETED.")

            l_master_file.WriteLine("spool off;")

            'Now we want to do the Post Completion node.
            l_master_file.WriteLine(l_post_install_list)

            l_master_file.WriteLine(Chr(10) & "COMMIT;")

            l_master_file.Close()

            'Convert the file to unix
            FileIO.FileDOStoUNIX(patchDir & "\" & l_master_filename)

        End If


    End Sub

    Private Sub CopySelectedChanges()


        Dim ChosenChanges As Collection = New Collection
        'Repo changes
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        TreeViewChanges.ReadCheckedLeafNodes(ChosenChanges)

        'Extra files
        'Retrieve checked node items from the TreeViewFiles as a collection of files.
        TreeViewFiles.ReadCheckedLeafNodes(ChosenChanges)

        If ChosenChanges.Count = 0 Then
            MsgBox("No files selected.")
        Else

            'Copy to the new Patchable Tree
            TreeViewPatchOrder.PathSeparator = "#"
            TreeViewPatchOrder.Nodes.Clear()

            'Prepopulate Tree with default category nodes.
            'This should become a method on TreeViewDraggableNodes2Levels

            TreeViewPatchOrder.AddCategory("Initialise")
            TreeViewPatchOrder.AddCategory("Users")
            TreeViewPatchOrder.AddCategory("Tables")
            TreeViewPatchOrder.AddCategory("Sequences")
            TreeViewPatchOrder.AddCategory("Type Specs")
            TreeViewPatchOrder.AddCategory("Roles")
            TreeViewPatchOrder.AddCategory("Database Links")
            TreeViewPatchOrder.AddCategory("Functions")
            TreeViewPatchOrder.AddCategory("Procedures")
            TreeViewPatchOrder.AddCategory("Package Specs")
            TreeViewPatchOrder.AddCategory("Views")
            TreeViewPatchOrder.AddCategory("Materialised Views")
            TreeViewPatchOrder.AddCategory("Grants")
            TreeViewPatchOrder.AddCategory("Synonyms")
            TreeViewPatchOrder.AddCategory("Type Bodies")
            TreeViewPatchOrder.AddCategory("Package Bodies")
            TreeViewPatchOrder.AddCategory("Triggers")
            TreeViewPatchOrder.AddCategory("Indexes")
            TreeViewPatchOrder.AddCategory("Primary Keys")
            TreeViewPatchOrder.AddCategory("Unique Keys")
            TreeViewPatchOrder.AddCategory("Non-Unique Keys")
            TreeViewPatchOrder.AddCategory("Data")
            TreeViewPatchOrder.AddCategory("Foreign Keys")
            TreeViewPatchOrder.AddCategory("Constraints")
            TreeViewPatchOrder.AddCategory("Configuration")
            TreeViewPatchOrder.AddCategory("Jobs")
            TreeViewPatchOrder.AddCategory("Miscellaneous")
            TreeViewPatchOrder.AddCategory("Finalise")
            TreeViewPatchOrder.AddCategory("Post Completion")
            TreeViewPatchOrder.AddCategory("Do Not Execute")

            For Each change In ChosenChanges

                Dim l_category As String = Nothing
                Dim l_file_extension As String = Common.getLastSegment(change, ".")
                Dim l_label As String
                Select Case l_file_extension
                    Case "user"
                        l_category = "Users"
                    Case "tab"
                        l_category = "Tables"
                    Case "seq"
                        l_category = "Sequences"
                    Case "tps"
                        l_category = "Type Specs"
                    Case "grt"
                        l_category = "Grants"
                    Case "pks", "pls", "aps", "sqs"
                        l_category = "Package Specs"
                    Case "fnc"
                        l_category = "Functions"
                    Case "prc"
                        l_category = "Procedures"
                    Case "vw"
                        l_category = "Views"
                    Case "syn"
                        l_category = "Synonyms"
                    Case "tpb"
                        l_category = "Type Bodies"
                    Case "pkb", "plb", "apb", "sqb"
                        l_category = "Package Bodies"
                    Case "trg", "plt"
                        l_category = "Triggers"
                    Case "pk"
                        l_category = "Primary Keys"
                    Case "uk"
                        l_category = "Unique Keys"
                    Case "nk"
                        l_category = "Non-Unique Keys"
                    Case "idx"
                        l_category = "Indexes"
                    Case "fk"
                        l_category = "Foreign Keys"
                    Case "con"
                        l_category = "Constraints"
                    Case "sdl"
                        l_category = "Loader Scripts"
                    Case "rg", "rol"
                        l_category = "Roles"
                    Case "job"
                        l_category = "Jobs"
                    Case "sdl"
                        l_category = "Data"
                    Case "dblink"
                        l_category = "Database Links"
                    Case "mv"
                        l_category = "Materialised Views"
                    Case "sql"
                        l_category = "Miscellaneous"
                    Case "begin", "init"
                        l_category = "Initialise"
                    Case "end", "final"
                        l_category = "Finalise"
                    Case "post"
                        l_category = "Post Completion"

                    Case Else
                        l_category = "Do Not Execute"
                End Select

                Dim pathSeparator As String = Nothing
                If change.contains(":") Then
                    'Windows path
                    pathSeparator = "\"
                Else
                    'Repo path
                    pathSeparator = "/"
                End If

                l_label = Common.getLastSegment(change, pathSeparator)
                TreeViewPatchOrder.AddFileToCategory(l_category, l_label, change)
            Next

            TreeViewPatchOrder.RemoveChildlessLevel1Nodes()

            'Set tree to expanded.
            TreeViewPatchOrder.ExpandAll()

            TreeViewPatchOrder.PrependCategory("Initialise")
            TreeViewPatchOrder.AddCategory("Finalise")
            TreeViewPatchOrder.AddCategory("Post Completion")
            TreeViewPatchOrder.AddCategory("Do Not Execute")


        End If

    End Sub





    Private Sub PatchTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchTabControl.SelectedIndexChanged

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageTags" Then
            If TagsCheckedListBox.Items.Count = 0 Then
                Findtags()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageChanges" Then
            deriveTags()
            deriveSchemas()

            'If TreeViewChanges.Nodes.Count = 0 Then
            'FindChanges()
            'End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageExtras" Then
            If TreeViewFiles.Nodes.Count = 0 Then
                FindExtras()
            End If
        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePreReqsA" Then



            If PreReqPatchesTreeViewA.Nodes.Count = 0 Then
                LastPatches()
            End If


        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePreReqsB" Then



            If PreReqPatchesTreeViewB.Nodes.Count = 0 Then
                RestrictPreReqToBranchCheckBox.Checked = True
                FindPreReqs()
            End If


        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePatchDefn" Then
            'Copy Patchable items to the next list.

            'PatchPathTextBox.Text = Replace(Globals.currentLongBranch, "/", "\") & "\" & Globals.currentAppCode & "\"

            'PatchPathTextBox.Text = Common.getFirstSegment(Globals.currentLongBranch, "/") & "\" & Globals.currentAppCode & "\" & Globals.currentBranch & "\"

            PatchPathTextBox.Text = Replace(Globals.currentLongBranch, "/", "\") & "\"

            If gBranchType = "hotfix" Then
                'SupIdTextBox.Text = gDBtarget
                NoteTextBox.Text = "Hotfix built for " & gDBtarget
            End If

            SYSDBACheckBox.Checked = (SchemaComboBox.SelectedItem.ToString = "SYS")
            AlternateSchemasCheckBox.Checked = False

            derivePatchName()

            derivePatchDir()

            UseARMCheckBox.Checked = Globals.getUseARM

            RerunCheckBox.Checked = True

            TrackPromoCheckBox.Checked = True

            If TreeViewPatchOrder.Nodes.Count = 0 Then
                CopySelectedChanges()
            End If

            'Show/hide buttons
            PatchButton.Visible = Not String.IsNullOrEmpty(PatchNameTextBox.Text)
            ExecuteButton.Visible = Not String.IsNullOrEmpty(PatchNameTextBox.Text)
            CommitButton.Visible = Not String.IsNullOrEmpty(PatchNameTextBox.Text)



        End If



    End Sub

    Private Sub PopDesc(target As Windows.Forms.Control, targetControlName As String)

        Dim l_old_text = target.Text
        Try
            Dim log As Collection = New Collection
            'log = GitSharpFascade.TagLog(Globals.getRepoPath, Tag1TextBox.Text, Tag2TextBox.Text)
            log = GitOp.Log()

            target.Text = ChoiceDialog.Ask("You may choose a log message for the " & targetControlName, log, "", "Choose log message", False)

        Catch noneChosen As Halt
            target.Text = l_old_text
        End Try

    End Sub



    Private Sub derivePatchName()

        If PatchTabControl.TabPages.Contains(TabPageTags) Then
            If Not String.IsNullOrEmpty(Tag1TextBox.Text) And Not String.IsNullOrEmpty(Tag2TextBox.Text) And SchemaComboBox.Items.Count > 0 Then

                'PatchNameTextBox.Text = Globals.currentBranch & "_" & Common.dropFirstSegment(Tag1TextBox.Text, ".") & "_" & Common.dropFirstSegment(Tag2TextBox.Text, ".") & "_" & SchemaComboBox.SelectedItem.ToString
                PatchNameTextBox.Text = Tag2TextBox.Text.TrimEnd("B") & "." & SchemaComboBox.SelectedItem.ToString

                If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then
                    PatchNameTextBox.Text = PatchNameTextBox.Text & "." & SupIdTextBox.Text

                End If
            Else
                MsgBox("Please select two tags, then review changes ensuring you select a schema, to allow derivation of PatchName")

            End If
        Else
            'SHA-1
            If SHA1TextBox1.Text <> "" And SHA1TextBox2.Text <> "" And SchemaComboBox.Items.Count > 0 Then

                PatchNameTextBox.Text = Globals.currentBranch & "." & SchemaComboBox.SelectedItem.ToString

                If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then
                    PatchNameTextBox.Text = PatchNameTextBox.Text & "." & SupIdTextBox.Text

                End If
            Else
                MsgBox("Please select two SHAs, then review changes ensuring you select a schema, to allow derivation of PatchName")

            End If

        End If


    End Sub

    Private Sub derivePatchDir()
        PatchDirTextBox.Text = Common.dos_path_trailing_slash(Globals.RootPatchDir & PatchPathTextBox.Text & PatchNameTextBox.Text)
    End Sub

    Private Sub SupIdTextBox_TextChanged(sender As Object, e As EventArgs) Handles SupIdTextBox.TextChanged
        derivePatchName()
        derivePatchDir()
    End Sub


    ' Private Sub FindPatches(ByRef foundPatches As CheckedListBox, ByVal restrictToBranch As Boolean)
    '     foundPatches.Items.Clear()
    '     If IO.Directory.Exists(Globals.RootPatchDir) Then
    '
    '         FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", foundPatches, Globals.RootPatchDir)
    '
    '         If restrictToBranch Then
    '             For i As Integer = foundPatches.Items.Count - 1 To 0 Step -1
    '                 If Not foundPatches.Items(i).contains(Globals.currentBranch) Then
    '                     'This patch is not from this branch and will be removed from the list
    '                     foundPatches.Items.RemoveAt(i)
    '                 End If
    '             Next
    '         End If
    '
    '
    '     End If
    '
    ' End Sub


    Shared Sub FindPatches(ByRef foundPatches As TreeViewEnhanced.TreeViewEnhanced, ByVal restrictToBranch As Boolean, Optional ByVal patchType As String = "ALL")

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim searchPath As String = Nothing
        If patchType <> "ALL" Then
            searchPath = patchType & "/"
        End If


        Dim lfoundPatches As Collection = New Collection

        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir & searchPath, "install.sql", lfoundPatches, Globals.RootPatchDir)

            If restrictToBranch Then

                For i As Integer = lfoundPatches.Count To 1 Step -1
                    If Not lfoundPatches(i).contains(Globals.currentBranch) Then
                        'This patch is not from this branch and will be removed from the list
                        lfoundPatches.Remove(i)

                    End If
                Next

            End If


        End If


        foundPatches.populateTreeFromCollection(lfoundPatches)

        If restrictToBranch Then
            foundPatches.ExpandAll()
        End If

        Cursor.Current = cursorRevert

    End Sub


    Private Sub FindPreReqs()
        FindPatches(PreReqPatchesTreeViewB, RestrictPreReqToBranchCheckBox.Checked)

    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub



    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecuteButton.Click
        'Use patch runner to execute with a master script.
        Dim l_master_filename As String = Nothing

        If UseARMCheckBox.Checked Then
            l_master_filename = "install.sql"
        Else
            l_master_filename = "install_lite.sql"
        End If

        PatchRunner.RunMasterScript("DEFINE database = '" & Globals.currentTNS & "'" & Chr(10) & "@" & PatchPathTextBox.Text & PatchNameTextBox.Text & "/" & l_master_filename)

    End Sub

    Private Sub CopyChangesButton_Click(sender As Object, e As EventArgs) Handles CopyChangesButton.Click
        CopySelectedChanges()
    End Sub

    Private Sub FindTagsButton_Click(sender As Object, e As EventArgs) Handles FindTagsButton.Click
        Findtags()
    End Sub

    Private Sub deriveTags()
        'MsgBox("TagsCheckedListBox.SelectedIndexChanged")

        If TagsCheckedListBox.CheckedItems.Count > 0 Then
            Tag1TextBox.Text = TagsCheckedListBox.CheckedItems.Item(0)
        Else
            Tag1TextBox.Text = ""
        End If
        If TagsCheckedListBox.CheckedItems.Count > 1 Then
            Tag2TextBox.Text = TagsCheckedListBox.CheckedItems.Item(1)
        Else
            Tag2TextBox.Text = ""
        End If

    End Sub

    Private Sub CommitButton_Click(sender As Object, e As EventArgs) Handles CommitButton.Click

        Dim lUntracked As String = Nothing
        If Not Me.TrackPromoCheckBox.Checked Then
            lUntracked = "UNTRACKED "
        End If

        Tortoise.Commit(PatchDirTextBox.Text, lUntracked & "NEW Patch: " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, True)

        'Mail.SendNotification(lUntracked & "NEW Patch: " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, "Patch created.", PatchDirTextBox.Text & "install.sql," & Globals.RootPatchDir & PatchNameTextBox.Text & ".log")

        'user
        'branch
        'tags
        'desc
        'note


    End Sub


    Public Shared Sub createPatchProcess(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)

        Common.checkBranch(iBranchType)
        Dim l_tag_base As String = Nothing

        Dim currentBranch As String = Globals.currentLongBranch()
        Dim createPatchProgress As ProgressDialogue = New ProgressDialogue("Create " & iBranchType & " Patch")
        createPatchProgress.MdiParent = GitPatcher
        createPatchProgress.addStep("Export Apex to branch: " & currentBranch, False, "Using the Apex Export workflow")
        createPatchProgress.addStep("Use QCGU to generate changed domain data: " & currentBranch, False, "Think hard!  Did i change any domains, tables, security or menus?  " _
                                 & "If so, i should logon to QCGU and generate that data out. " _
                                 & "Then commit it too." _
                                 & "Regenerate: Menu (new pages, menu changes), Security (new pages, security changes), Tapis (table or view column changes), Domains (new or changed tables or views, new domains or domain ussage changed)")
        createPatchProgress.addStep("Rebase branch: " & currentBranch & " on branch: " & iRebaseBranchOn, True, "Using the Rebase workflow")
        createPatchProgress.addStep("Review tags on Branch: " & currentBranch)
        createPatchProgress.addStep("Create edit, test", True, "Now is a great time to smoke test my work before i commit the patch.")
        createPatchProgress.addStep("Commit to Branch: " & currentBranch)
        createPatchProgress.addStep("Switch to " & iRebaseBranchOn & " branch")
        createPatchProgress.addStep("Merge from Branch: " & currentBranch, True, "Please select the Branch:" & currentBranch & " from the Tortoise Merge Dialogue")
        createPatchProgress.addStep("Push to Origin", True, "If at this stage there is an error because your " & iRebaseBranchOn & " branch is out of date, then you must restart the process to ensure you are patching the lastest merged files.")
        createPatchProgress.addStep("Synch to Verify Push", True, "Should say '0 commits ahead orgin/" & iRebaseBranchOn & "'.  " _
                                 & "If NOT, then the push FAILED. Your " & iRebaseBranchOn & " branch is now out of date, so is your rebase from it, and any patches COULD BE stale. " _
                                 & "In this situation, it is safest to restart the Create Patch process to ensure you are patching the lastest merged files. ")
        createPatchProgress.addStep("Release to " & iDBtarget, True)
        createPatchProgress.addStep("Return to Branch: " & currentBranch)
        createPatchProgress.addStep("Snapshot VM", True, "Create a snapshot of your current VM state, to use as your next restore point.  I label mine with the patch_name of the last applied patch.")
        createPatchProgress.Show()


        Do Until createPatchProgress.isStarted
            Common.wait(1000)
        Loop

        If createPatchProgress.toDoNextStep() Then
            'Export Apex to branch
            Apex.ApexExportCommit()

        End If

        If createPatchProgress.toDoNextStep() Then
            'QCGU
            MsgBox("Please launch QCGU and generate Domain data", MsgBoxStyle.Exclamation, "QCGU")

        End If

        If createPatchProgress.toDoNextStep() Then
            'Rebase branch
            l_tag_base = Main.rebaseBranch(iBranchType, iDBtarget, iRebaseBranchOn)

        End If


        If createPatchProgress.toDoNextStep() Then
            'Review tags on the branch
            Tortoise.Log(Globals.getRepoPath)
        End If


        If createPatchProgress.toDoNextStep() Then

            Dim Wizard As New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn, l_tag_base)
            'newchildform.MdiParent = GitPatcher
            Wizard.ShowDialog() 'NEED TO WAIT HERE!!


        End If


        If createPatchProgress.toDoNextStep() Then
            MsgBox("Now is a great time to smoke test my work before i commit the patch.", MsgBoxStyle.Information, "Smoke Test")

            'Committing changed files to GIT"
            Tortoise.Commit(Globals.getRepoPath, "Commit any patches you've not yet committed", True)
        End If


        If createPatchProgress.toDoNextStep() Then
            'switch
            GitOp.switchBranch(iRebaseBranchOn)


        End If

        If createPatchProgress.toDoNextStep() Then
            'Merge from Feature branch
            Tortoise.Merge(Globals.getRepoPath)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Push to origin/develop 
            GitOp.pushBranch(iRebaseBranchOn)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Synch command to verfiy that Push was successful.
            Tortoise.Sync(Globals.getRepoPath)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Release to DB Target
            Main.releaseTo(iDBtarget, iBranchType)
        End If

        If createPatchProgress.toDoNextStep() Then
            'GitSharpFascade.switchBranch(Globals.currentRepo, currentBranch)
            GitOp.switchBranch(currentBranch)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Snapshot VM
            MsgBox("Create a snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")

        End If

        'Finish
        createPatchProgress.toDoNextStep()


    End Sub

    Private Sub PopulateTreeView(ByVal dir As String, ByVal parentNode As TreeNode)
        Dim folder As String = String.Empty
        Try

            Dim files() As String = System.IO.Directory.GetFiles(dir) ', strPattern)
            Dim fileNode As TreeNode = Nothing
            For Each file In files
                fileNode = New TreeNode(Common.getLastSegment(file, "\")) 'translate to relative URL
                parentNode.Nodes.Add(fileNode)
            Next

            Dim folders() As String = IO.Directory.GetDirectories(dir)
            If folders.Length <> 0 Then
                Dim childNode As TreeNode = Nothing
                For Each folder In folders
                    childNode = New TreeNode(Common.getLastSegment(folder, "\")) 'translate to relative URL
                    parentNode.Nodes.Add(childNode)
                    PopulateTreeView(folder, childNode)
                Next
            End If
        Catch ex As UnauthorizedAccessException
            parentNode.Nodes.Add(folder & ": Access Denied")
        Catch ex As System.IO.DirectoryNotFoundException
            parentNode.Nodes.Add(dir & ": Path Not Found")
        End Try
    End Sub

    Private Sub FindExtras()
        TreeViewFiles.PathSeparator = "\"
        TreeViewFiles.Nodes.Clear()

        Dim extrasDirCol As Collection = extrasDirCollection()

        For Each relDir In extrasDirCol
            Dim aRootDir As String = Globals.getRepoPath() & relDir
            Dim aRootNode As TreeNode = New TreeNode(aRootDir)
            TreeViewFiles.Nodes.Add(aRootNode)
            PopulateTreeView(aRootDir, aRootNode)
        Next
    End Sub



    Private Sub ButtonFindFiles_Click(sender As Object, e As EventArgs) Handles ButtonFindFiles.Click
        FindExtras()
    End Sub

    Private Sub LastPatches()
        Dim ChosenChanges As Collection = New Collection
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        TreeViewChanges.ReadCheckedLeafNodes(ChosenChanges)

        'Requery ALL patches
        FindPatches(PreReqPatchesTreeViewA, False) 'Do not restrict to branch

        For Each change In ChosenChanges
            Dim patch_component As String = Common.getLastSegment(change.ToString(), "/")
            Dim LastPatch As String = PatchRunner.FindLastPatch(patch_component)
            If String.IsNullOrEmpty(LastPatch) Then
                Logger.Dbg("No previous patch for Change: " & patch_component)
            Else
                Logger.Dbg("Change: " & patch_component & " LastPatch: " & LastPatch)
                Dim l_found As Boolean = False
                PreReqPatchesTreeViewA.TickNode(LastPatch, l_found)

                If Not l_found Then
                    MsgBox("Unable to find patch: " & LastPatch & " for Change: " & patch_component)
                End If

            End If



        Next

        'Set Prereq tree to Collapsed view.
        PreReqPatchesTreeViewA.showCheckedNodes()
    End Sub



    Private Sub ButtonLastPatch_Click(sender As Object, e As EventArgs) Handles ButtonLastPatch.Click

        LastPatches()

    End Sub


    Private Sub ButtonPopDesc_Click(sender As Object, e As EventArgs) Handles ButtonPopDesc.Click
        PopDesc(PatchDescTextBox, "Patch Description")
    End Sub

    Private Sub ButtonPopNotes_Click(sender As Object, e As EventArgs) Handles ButtonPopNotes.Click
        PopDesc(NoteTextBox, "Notes")
    End Sub

    Private Sub TagsCheckedListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TagsCheckedListBox.SelectedIndexChanged
        'If tags are changed then we will clear the selected changes and the schema list.
        SchemaCountTextBox.Text = "0"
        SchemaComboBox.Text = ""
        TreeViewChanges.Nodes.Clear()
        HideTabs()
        If TagsCheckedListBox.CheckedItems.Count > 1 Then
            ShowChangesTab()
        Else
            HideChangesTab()
        End If


    End Sub

    Private Sub ResetForNewPatch()

        TreeViewFiles.Nodes.Clear()
        PreReqPatchesTreeViewA.Nodes.Clear()
        TreeViewPatchOrder.Nodes.Clear()
        SupIdTextBox.Text = ""
        PatchDescTextBox.Text = ""
        NoteTextBox.Text = ""
    End Sub




    Private Sub SchemaComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SchemaComboBox.SelectedIndexChanged
        'FindButton.Visible = True
        FindChanges()
    End Sub

    Private Sub TreeViewChanges_Click(sender As Object, e As EventArgs) Handles TreeViewChanges.Click
        TreeViewPatchOrder.Nodes.Clear()
    End Sub

    Private Sub TreeViewFile_Click(sender As Object, e As EventArgs) Handles TreeViewFiles.Click
        TreeViewPatchOrder.Nodes.Clear()
    End Sub



    Shared Sub exportPatch(ByVal patchpath As String,
                           ByVal patchExportDir As String)


        Dim l_path As String = Common.dos_path(patchpath)
        Dim l_patchExportDir As String = Common.dos_path(patchExportDir)


        Dim l_source_path As String = Common.dos_path(Globals.RootPatchDir & l_path)
        Dim l_target_path As String = Common.dos_path(l_patchExportDir & "\" & l_path)


        FileIO.createFolderIfNotExists(l_target_path)

        Dim objfso = CreateObject("Scripting.FileSystemObject")
        Dim objFolder As Object
        Dim objFile As Object

        objFolder = objfso.GetFolder(l_source_path)
        For Each objFile In objFolder.Files
            FileIO.CopyFile(l_source_path & "\" & objFile.Name, l_target_path & "\" & objFile.Name)
            'Info("using reference file " & objFile.Name)
        Next

        'Copy README.txt
        Try
            FileIO.CopyFile(Globals.RootPatchDir & "README.txt", l_patchExportDir & "\README.txt")
        Catch ex As Exception
            MsgBox("No README.txt found, to copy to the patchset.")

        End Try


        Dim l_master_lite_filename As String = "install_patch_lite.sql"
        Dim l_master_lite_file As New System.IO.StreamWriter(l_patchExportDir & "\" & l_master_lite_filename)

        l_master_lite_file.WriteLine(Common.unix_path("@" & l_path & "\" & "install_lite.sql"))

        l_master_lite_file.Close()

        Dim l_master_filename As String = "install_patch.sql"
        Dim l_master_file As New System.IO.StreamWriter(l_patchExportDir & "\" & l_master_filename)

        l_master_file.WriteLine(Common.unix_path("@" & l_path & "\" & "install.sql"))

        l_master_file.Close()



    End Sub

    Shared Sub doExportPatch(iPatchPath As String, iPatchName As String)

        Dim l_repo_patch_dir As String = Common.dos_path_trailing_slash(Globals.PatchExportDir & Globals.getRepoName)
        Logger.Note("l_repo_patch_dir", l_repo_patch_dir)
        Dim l_repo_patch_export_dir As String = l_repo_patch_dir & iPatchName
        Logger.Note("l_repo_patch_export_dir", l_repo_patch_export_dir)

        'Remove previous patch 
        Try
            FileIO.confirmDeleteFolder(l_repo_patch_export_dir)
        Catch cancelled_delete As Halt
            MsgBox("Warning: Now overwriting previously exported patch")
        End Try


        FileIO.createFolderIfNotExists(l_repo_patch_export_dir)

        'Export each patch, and create the patch_install.sql
        exportPatch(iPatchPath & iPatchName, _
                    l_repo_patch_export_dir)


        zip.zip_dir(l_repo_patch_dir & iPatchName & ".zip",
                    l_repo_patch_export_dir)

        Host.RunExplorer(l_repo_patch_export_dir)

    End Sub


    Private Sub ExportPatchButton_Click(sender As Object, e As EventArgs) Handles ExportPatchButton.Click

        doExportPatch(PatchPathTextBox.Text, PatchNameTextBox.Text)
 
    End Sub

    Private Sub UseSHA1Button_Click(sender As Object, e As EventArgs) Handles UseSHA1Button.Click
        UseSHA1()
    End Sub

    Private Sub UseTagsButton_Click(sender As Object, e As EventArgs) Handles UseTagsButton.Click
        UseTags()
    End Sub

    Private Sub FindsSHA1Button_Click(sender As Object, e As EventArgs) Handles FindsSHA1Button.Click
        Tortoise.Log(Globals.getRepoPath, False) 'Do not wait.
    End Sub

    Private Sub showHideChangesTab()
        SchemaCountTextBox.Text = "0"
        SchemaComboBox.Text = ""
        TreeViewChanges.Nodes.Clear()
        HideTabs()
        If SHA1TextBox1.Text <> "" And SHA1TextBox2.Text <> "" Then
            ShowChangesTab()
        Else
            HideChangesTab()
        End If
    End Sub



    Private Sub SHA1TextBox1_TextChanged(sender As Object, e As EventArgs) Handles SHA1TextBox1.TextChanged
        showHideChangesTab()
    End Sub

    Private Sub SHA1TextBox2_TextChanged(sender As Object, e As EventArgs) Handles SHA1TextBox2.TextChanged
        showHideChangesTab()
    End Sub
End Class