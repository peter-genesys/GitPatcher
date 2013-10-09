
Public Class PatchFromTags

    Dim gBranchType As String
    Dim gDBtarget As String
    Dim gRebaseBranchOn As String


    Public Sub New(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)
        InitializeComponent()

        FindTagsButton.Text = "Find Tags like " & Globals.currentBranch & ".XX"

        Findtags()

        gBranchType = iBranchType
        gDBtarget = iDBtarget
        gRebaseBranchOn = iRebaseBranchOn

        'NOT CURRENTLY USING THE TabPageSuperBy TABPAGE
        PatchTabControl.TabPages.Remove(TabPageSuperBy)
        'If gBranchType <> "hotfix" Then
        '  PatchTabControl.TabPages.Remove(TabPageSuperBy)
        'End If

 
    End Sub



    Private Sub Findtags()
        TagsCheckedListBox.Items.Clear()
        For Each tagname In GitSharpFascade.getTagList(Globals.currentRepo)
            If Common.getFirstSegment(tagname, ".") = Globals.currentBranch Then
                TagsCheckedListBox.Items.Add(tagname)
            End If
        Next
    End Sub

    Private Sub FindChanges()
        Try
            If SchemaComboBox.Text = "" Then
                Throw (New Halt("Schema not selected"))
            End If

            TreeViewChanges.PathSeparator = "/"
            TreeViewChanges.Nodes.Clear()


            For Each change In GitSharpFascade.getTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, False)

                'find or create each node for item
                GPTrees.AddNode(TreeViewChanges.Nodes, change, change, "/", True)
 
            Next
            ButtonTreeChangeChanges.Text = "Expand"
            GPTrees.treeChange_Click(ButtonTreeChangeChanges, TreeViewChanges)
 
        Catch schema_not_selected As Halt
            MsgBox("Please select a schema")
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindChanges()


    End Sub


    Private Sub exportExtraFiles(ByRef extrasCollection As Collection, ByRef filenames As Collection, ByVal patch_dir As String)

        Dim Filename As String = Nothing
        If extrasCollection.Count > 0 Then
            For Each FilePath In extrasCollection

                Filename = Common.getLastSegment(FilePath, "\")

                My.Computer.FileSystem.CopyFile(FilePath, patch_dir & "\" & Filename, True)

                filenames.Add(Filename)

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


        Dim filenames As Collection = Nothing

        filenames = GitSharpFascade.exportTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, PatchableCheckedListBox.Items, PatchDirTextBox.Text)

        'Additional file exports 

        Dim ExtraFiles As Collection = New Collection
        'Retrieve checked node items from the TreeViewFiles as a collection of files.
        GPTrees.ReadCheckedNodes(TreeViewFiles.TopNode, ExtraFiles, True)

        exportExtraFiles(ExtraFiles, filenames, PatchDirTextBox.Text)

        'Add to filenames too


        Dim PreReqPatches As Collection = New Collection

        'Retrieve checked node items from the PreReqPatchesTreeView as a collection of patches.
        GPTrees.ReadCheckedNodes(PreReqPatchesTreeView.TopNode, PreReqPatches, True)


        Dim SuperPatches As Collection = New Collection

        'Retrieve checked node items from the SuperPatchesTreeView as a collection of patches.
        GPTrees.ReadCheckedNodes(SuperPatchesTreeView.TopNode, SuperPatches, True)

        Dim SuperByPatches As Collection = New Collection


        'Retrieve checked node items from the SuperByPatchesTreeView as a collection of patches.
        GPTrees.ReadCheckedNodes(SuperByPatchesTreeView.TopNode, SuperByPatches, True)



        'Write the install script
        writeInstallScript(PatchNameTextBox.Text, _
                           Common.getFirstSegment(Globals.currentLongBranch, "/"), _
                           SchemaComboBox.Text, _
                           Globals.currentLongBranch, _
                           Tag1TextBox.Text, _
                           Tag2TextBox.Text, _
                           SupIdTextBox.Text, _
                           PatchDescTextBox.Text, _
                           NoteTextBox.Text, _
                           UsePatchAdminCheckBox.Checked, _
                           RerunCheckBox.Checked, _
                           filenames, _
                           PatchableCheckedListBox.CheckedItems, _
                           PreReqPatches, _
                           SuperPatches, _
                           SuperByPatches, _
                           PatchDirTextBox.Text, _
                           PatchPathTextBox.Text, _
                           TrackPromoCheckBox.Checked)

        Host.RunExplorer(PatchDirTextBox.Text)



    End Sub

    Private Sub deriveSchemas()
        SchemaComboBox.Items.Clear()
        SchemaComboBox.Text = ""
        For Each schema In GitSharpFascade.getSchemaList(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database")
            SchemaComboBox.Items.Add(schema)
        Next

        SchemaCountTextBox.Text = SchemaComboBox.Items.Count

        'If exactly one schema found then select it
        'otherwise force user to choose one.
        If SchemaComboBox.Items.Count = 1 Then
            SchemaComboBox.SelectedIndex = 0
        End If
    End Sub

    '??NOT BEING USED
    'Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAllCheckBox.CheckedChanged
    '    'Loop thru items.
    '    For i As Integer = 0 To ChangesCheckedListBox.Items.Count - 1
    '        ChangesCheckedListBox.SetItemChecked(i, CheckAllCheckBox.Checked)
    '
    '    Next
    'End Sub

    Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged

        deriveSchemas()

    End Sub

    Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
        deriveSchemas()
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim CheckedChanges As Collection = New Collection

        'Retrieve checked node items from the TreeViewChanges as a collection of changes.
        GPTrees.ReadCheckedNodes(TreeViewChanges.TopNode, CheckedChanges, True)
 

        MsgBox(GitSharpFascade.viewTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, CheckedChanges))
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveButton.Click

 
        GPTrees.RemoveNodes(TreeViewChanges.Nodes, True)
 
    End Sub


    Private Sub ButtonCropTo_Click(sender As Object, e As EventArgs) Handles ButtonCropTo.Click
        GPTrees.RemoveNodes(TreeViewChanges.Nodes, False)
    End Sub



    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        PatchDirTextBox.Text = Globals.RootPatchDir & Replace(PatchNameTextBox.Text, "/", "\") & "\"
    End Sub



    Private Sub writeInstallScript(ByVal patch_name As String, _
                                  ByVal patch_type As String, _
                                  ByVal db_schema As String, _
                                  ByVal branch_path As String, _
                                  ByVal tag1_name As String, _
                                  ByVal tag2_name As String, _
                                  ByVal supplementary As String, _
                                  ByVal patch_desc As String, _
                                  ByVal note As String, _
                                  ByVal use_patch_admin As Boolean, _
                                  ByVal rerunnable As Boolean, _
                                  ByRef targetFiles As Collection, _
                                  ByRef ignoreErrorFiles As CheckedListBox.CheckedItemCollection, _
                                  ByRef prereq_patches As Collection, _
                                  ByRef supersedes_patches As Collection, _
                                  ByRef superseded_by_patches As Collection, _
                                  ByVal patchDir As String, _
                                  ByVal groupPath As String, _
                                  ByVal track_promotion As Boolean)


        Dim l_db_objects_users As String = Nothing 'user
        Dim l_db_objects_tables As String = Nothing 'tab
        Dim l_db_objects_sequences As String = Nothing 'seq
        Dim l_db_objects_type_specs As String = Nothing 'tps
        Dim l_db_objects_type_bodies As String = Nothing 'tpb
        Dim l_db_objects_grants As String = Nothing 'grt
        Dim l_db_objects_data As String = Nothing 'sql

        Dim l_db_objects_package_specs As String = Nothing 'pks,pls
        Dim l_db_objects_functions As String = Nothing 'fnc
        Dim l_db_objects_procedures As String = Nothing 'prc

        Dim l_db_objects_views As String = Nothing 'vw
        Dim l_db_objects_synonyms As String = Nothing 'syn
        Dim l_db_objects_package_bodies As String = Nothing 'pkb,plb
        Dim l_db_objects_triggers As String = Nothing 'trg

        Dim l_db_objects_primary_keys As String = Nothing 'pk
        Dim l_db_objects_unique_keys As String = Nothing 'uk
        Dim l_db_objects_non_unique_keys As String = Nothing 'nk
        Dim l_db_objects_indexes As String = Nothing 'idx
        Dim l_db_objects_foreign_keys As String = Nothing 'fk
        Dim l_db_objects_constraints As String = Nothing 'con
        Dim l_db_objects_configuration As String = Nothing 'sdl
        Dim l_db_objects_roles As String = Nothing 'rg, rol
        Dim l_db_objects_jobs As String = Nothing 'job
        Dim l_db_objects_dblinks As String = Nothing 'dblink
        Dim l_db_objects_mviews As String = Nothing 'mv


        Dim l_ignored As String = Nothing 'ctl
        Dim l_db_objects_misc As String = Nothing 'everything else


        Dim l_file_extension As String = Nothing
        Dim l_install_file_line As String = Nothing

        Dim l_all_programs As String = Nothing


        Dim l_show_error As String = Chr(10) & "Show error;"

        Dim l_patchable_count As Integer = 0

        Dim rerunnable_yn As String = "N"
        If rerunnable Then
            rerunnable_yn = "Y"
        End If


        Dim track_promotion_yn As String = "N"
        If track_promotion Then
            track_promotion_yn = "Y"
        End If



        Dim l_patch_started As String = Nothing


        For Each l_path In targetFiles

            Dim l_filename As String = Common.getLastSegment(l_path, "/")

            'Sort the files by files extention into lists.

            l_file_extension = l_filename.Split(".")(1)

            If ignoreErrorFiles.Contains(l_path) Then
                l_install_file_line = Chr(10) & "WHENEVER SQLERROR CONTINUE" & _
                                      Chr(10) & "PROMPT " & l_filename & " " & _
                                      Chr(10) & "@" & groupPath & patch_name & "\" & l_filename & ";" & _
                                      Chr(10) & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK"

            Else
                l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " & _
                                      Chr(10) & "@" & groupPath & patch_name & "\" & l_filename & ";"

            End If

            Try

                Select Case l_file_extension
                    Case "user"
                        l_db_objects_users = l_db_objects_users & l_install_file_line
                    Case "tab"
                        l_db_objects_tables = l_db_objects_tables & l_install_file_line
                    Case "seq"
                        l_db_objects_sequences = l_db_objects_sequences & l_install_file_line
                    Case "tps"
                        l_db_objects_type_specs = l_db_objects_type_specs & l_install_file_line & l_show_error
                    Case "grt"
                        l_db_objects_grants = l_db_objects_grants & l_install_file_line
                    Case "sql"
                        l_db_objects_data = l_db_objects_data & l_install_file_line
                    Case "pks", "pls"
                        l_db_objects_package_specs = l_db_objects_package_specs & l_install_file_line & l_show_error
                    Case "fnc"
                        l_db_objects_functions = l_db_objects_functions & l_install_file_line & l_show_error
                    Case "prc"
                        l_db_objects_procedures = l_db_objects_procedures & l_install_file_line & l_show_error
                    Case "vw"
                        l_db_objects_views = l_db_objects_views & l_install_file_line & l_show_error
                    Case "syn"
                        l_db_objects_synonyms = l_db_objects_synonyms & l_install_file_line
                    Case "tpb"
                        l_db_objects_type_bodies = l_db_objects_type_bodies & l_install_file_line & l_show_error
                    Case "pkb", "plb"
                        l_db_objects_package_bodies = l_db_objects_package_bodies & l_install_file_line & l_show_error
                    Case "trg"
                        l_db_objects_triggers = l_db_objects_triggers & l_install_file_line & l_show_error
                    Case "pk"
                        l_db_objects_primary_keys = l_db_objects_primary_keys & l_install_file_line
                    Case "uk"
                        l_db_objects_unique_keys = l_db_objects_unique_keys & l_install_file_line
                    Case "nk"
                        l_db_objects_non_unique_keys = l_db_objects_non_unique_keys & l_install_file_line
                    Case "idx"
                        l_db_objects_indexes = l_db_objects_indexes & l_install_file_line
                    Case "fk"
                        l_db_objects_foreign_keys = l_db_objects_foreign_keys & l_install_file_line
                    Case "con"
                        l_db_objects_constraints = l_db_objects_constraints & l_install_file_line
                    Case "sdl"
                        l_db_objects_configuration = l_db_objects_configuration & l_install_file_line
                    Case "rg", "rol"
                        l_db_objects_roles = l_db_objects_roles & l_install_file_line
                    Case "job"
                        l_db_objects_jobs = l_db_objects_jobs & l_install_file_line
                    Case "dblink"
                        l_db_objects_dblinks = l_db_objects_dblinks & l_install_file_line & l_show_error
                    Case "mv"
                        l_db_objects_mviews = l_db_objects_mviews & l_install_file_line & l_show_error
                    Case "ctl", "xml"
                        l_ignored = l_ignored & l_install_file_line
                        'System.IO.File.Delete(PatchDirTextBox.Text & l_filename)
                        Throw (New Halt("Skip this ignorable object"))
                    Case Else
                        l_db_objects_misc = l_db_objects_misc & l_install_file_line
                End Select

                If String.IsNullOrEmpty(l_all_programs) Then
                    l_all_programs = l_filename
                Else
                    l_all_programs = l_all_programs & "' -" & Chr(10) & "||'," & l_filename
                End If


            Catch SkipTestObject As Halt
                ' Logger.Dbg("Skip object " & l_item.path)
                ' l_all_programs = l_all_programs & "XX " & l_program_name & "NOT PATCHED" & Chr(10)
            End Try


        Next

        If targetFiles.Count > 0 Then

            Dim l_log_filename As String = patch_name & ".log"
            Dim l_master_filename As String = "install.sql"
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

            l_master_file.WriteLine("SPOOL " & l_log_filename)

            If db_schema = "SYS" Then
                l_master_file.WriteLine("CONNECT APEX_SYS/&&APEX_SYS_password@&&database as sysdba")
            Else
                l_master_file.WriteLine("CONNECT " & db_schema & "/&&" & db_schema & "_password@&&database")
            End If


            l_master_file.WriteLine("set serveroutput on;")



            If use_patch_admin Then

                l_patch_started = _
                    "execute patch_admin.patch_installer.patch_started( -" _
        & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
        & Chr(10) & " ,i_patch_type         => '" & patch_type & "' -" _
        & Chr(10) & " ,i_db_schema          => '" & db_schema & "' -" _
        & Chr(10) & " ,i_branch_name        => '" & branch_path & "' -" _
        & Chr(10) & " ,i_tag_from           => '" & tag1_name & "' -" _
        & Chr(10) & " ,i_tag_to             => '" & tag2_name & "' -" _
        & Chr(10) & " ,i_supplementary      => '" & supplementary & "' -" _
        & Chr(10) & " ,i_patch_desc         => '" & patch_desc.Replace("'", "''") & "' -" _
        & Chr(10) & " ,i_patch_componants   => '" & l_all_programs & "' -" _
        & Chr(10) & " ,i_patch_create_date  => '" & DateString & "' -" _
        & Chr(10) & " ,i_patch_created_by   => '" & Environment.UserName & "' -" _
        & Chr(10) & " ,i_note               => '" & note.Replace("'", "''") & "' -" _
        & Chr(10) & " ,i_rerunnable_yn      => '" & rerunnable_yn & "' -" _
        & Chr(10) & " ,i_remove_prereqs     => 'N' -" _
        & Chr(10) & " ,i_remove_sups        => 'N' -" _
        & Chr(10) & " ,i_track_promotion    => '" & track_promotion_yn & "'); " _
        & Chr(10)


                l_master_file.WriteLine(l_patch_started)


                Dim l_prereq_short_name As String = Nothing
                For Each l_prereq_patch In prereq_patches
                    l_prereq_short_name = Common.getLastSegment(l_prereq_patch, "\")
                    If l_prereq_short_name = PatchNameTextBox.Text Then
                        MsgBox("A Patch may NOT have itself as a prerequisite, skipping Prerequisite Patch: " & l_prereq_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
                    Else
                        l_master_file.WriteLine("PROMPT")
                        l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_short_name)
                        l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_prereq( -")
                        l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                        l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")
                    End If

                Next

                l_prereq_short_name = My.Settings.MinPatch
                l_master_file.WriteLine("PROMPT Ensure Patch Admin is late enough for this patch")
                l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_prereq( -")
                l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")

            End If

            l_master_file.WriteLine("select user||'@'||global_name Connection from global_name;")
            'Write the list of files to execute.


            If Not String.IsNullOrEmpty(l_db_objects_users) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing USERS" & l_db_objects_users)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_tables) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing TABLES" & l_db_objects_tables)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_sequences) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing SEQUENCES" & l_db_objects_sequences)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_type_specs) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing TYPE SPECS" & l_db_objects_type_specs)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_roles) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing ROLES" & l_db_objects_roles)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_dblinks) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing DB_LINKS" & l_db_objects_dblinks)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_functions) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing FUNCTIONS" & l_db_objects_functions)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_procedures) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing PROCEDURES" & l_db_objects_procedures)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_package_specs) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing PACKAGE SPECS" & l_db_objects_package_specs)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_views) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing VIEWS" & l_db_objects_views)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_mviews) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing MATERIALISED VIEWS" & l_db_objects_mviews)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_grants) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing GRANTS" & l_db_objects_grants)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_synonyms) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing SYNONYMS" & l_db_objects_synonyms)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_type_bodies) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing TYPE BODIES" & l_db_objects_type_bodies)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_package_bodies) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing PACKAGE BODIES" & l_db_objects_package_bodies)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_triggers) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing TRIGGERS" & l_db_objects_triggers)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_indexes) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing INDEXES" & l_db_objects_indexes)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_primary_keys) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing PRIMARY KEYS" & l_db_objects_primary_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_unique_keys) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing UNIQUE KEYS" & l_db_objects_unique_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_non_unique_keys) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing NON-UNIQUE KEYS" & l_db_objects_non_unique_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_data) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing DATA" & l_db_objects_data)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_foreign_keys) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing FOREIGN KEYS" & l_db_objects_foreign_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_constraints) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing CONSTRAINTS" & l_db_objects_constraints)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_configuration) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing CONFIGURATION" & l_db_objects_configuration)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_jobs) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing JOBS" & l_db_objects_jobs)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_misc) Then
                l_master_file.WriteLine(Chr(10) & "Prompt installing MISCELLANIOUS" & l_db_objects_misc)
            End If


            l_master_file.WriteLine(Chr(10) & "COMMIT;")

            If use_patch_admin Then

                l_master_file.WriteLine("PROMPT Compiling objects in schema " & db_schema)
                l_master_file.WriteLine("execute patch_admin.patch_invoker.compile_post_patch;")

                If db_schema = "PATCH_ADMIN" Then
                    l_master_file.WriteLine("--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.")
                    l_master_file.WriteLine("execute patch_admin.patch_installer.patch_completed(i_patch_name  => '" & patch_name & "');")
                Else
                    l_master_file.WriteLine("execute patch_admin.patch_installer.patch_completed;")
                End If

                Dim l_sup_short_name As String = Nothing
                For Each l_sup_patch In supersedes_patches
                    l_sup_short_name = Common.getLastSegment(l_sup_patch, "\")
                    If l_sup_short_name = PatchNameTextBox.Text Then
                        MsgBox("A Patch may NOT supersede itself, skipping Supersedes Patch: " & l_sup_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
                    Else
                        l_master_file.WriteLine("PROMPT")
                        l_master_file.WriteLine("PROMPT Supersedes patch " & l_sup_short_name)
                        l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_supersedes( -")
                        l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                        l_master_file.WriteLine(",i_supersedes_patch  => '" & l_sup_short_name & "' );")
                    End If
                Next

                For Each l_sup_patch In superseded_by_patches
                    l_sup_short_name = Common.getLastSegment(l_sup_patch, "\")
                    If l_sup_short_name = PatchNameTextBox.Text Then
                        MsgBox("A Patch may NOT be superseded by itself, skipping Superseded By Patch: " & l_sup_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
                    Else

                        l_master_file.WriteLine("PROMPT")
                        l_master_file.WriteLine("PROMPT Superseded by patch " & l_sup_short_name)
                        l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_supersedes( -")
                        l_master_file.WriteLine("i_patch_name     => '" & l_sup_short_name & "' -")
                        l_master_file.WriteLine(",i_supersedes_patch  => '" & patch_name & "' );")
                    End If
                Next

            End If

            l_master_file.WriteLine("COMMIT;")

            l_master_file.WriteLine("PROMPT ")
            l_master_file.WriteLine("PROMPT " & l_master_filename & " - COMPLETED.")

            l_master_file.WriteLine("spool off;")


            l_master_file.Close()


        End If


    End Sub

    Private Sub CopySelectedChanges()
        'Copy Selected Changes to the next list box.
        PatchableCheckedListBox.Items.Clear()

        Dim ChosenChanges As Collection = New Collection
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        GPTrees.ReadCheckedNodes(TreeViewChanges.TopNode, ChosenChanges, True)

        For Each change In ChosenChanges

            PatchableCheckedListBox.Items.Add(change.ToString)

        Next


    End Sub


    Private Sub PatchTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchTabControl.SelectedIndexChanged

        'MessageBox.Show("you selected the fifth tab:  " & PatchTabControl.SelectedTab.Name.ToString)


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageTags" Then
            If TagsCheckedListBox.Items.Count = 0 Then
                Findtags()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageChanges" Then
            deriveTags()

            If TreeViewChanges.Nodes.Count = 0 Then
                FindChanges()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePreReqs" Then



            If PreReqPatchesTreeView.Nodes.Count = 0 Then
                RestrictPreReqToBranchCheckBox.Checked = True
                FindPreReqs()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageSuper" Then



            If SuperPatchesTreeView.Nodes.Count = 0 Then
                RestrictSupToBranchCheckBox.Checked = True
                FindSuper()
            End If


        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageSuperBy" Then



            If SuperByPatchesTreeView.Nodes.Count = 0 Then
                RestrictSupByToBranchCheckBox.Checked = True
                FindSuperBy()
            End If


        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePatchDefn" Then
            'Copy Patchable items to the next list.

            PatchPathTextBox.Text = Replace(Globals.currentLongBranch, "/", "\") & "\" & Globals.currentAppCode & "\"

            PatchPathTextBox.Text = Common.getFirstSegment(Globals.currentLongBranch, "/") & "\" & Globals.currentAppCode & "\" & Globals.currentBranch & "\"

            If gBranchType = "hotfix" Then
                SupIdTextBox.Text = gDBtarget
            End If


            derivePatchName()

            derivePatchDir()

            UsePatchAdminCheckBox.Checked = True

            RerunCheckBox.Checked = True

            TrackPromoCheckBox.Checked = True

            If PatchableCheckedListBox.Items.Count = 0 Then
                CopySelectedChanges()
            End If

            'Show/hide buttons
            PatchButton.Visible = Not String.IsNullOrEmpty(PatchNameTextBox.Text)
            ExecuteButton.Visible = Not String.IsNullOrEmpty(PatchNameTextBox.Text)
            CommitButton.Visible = Not String.IsNullOrEmpty(PatchNameTextBox.Text)


        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageExecute" Then

            ExecuteButton.Text = "Execute Patch on " & Globals.currentTNS

        End If

    End Sub

    Private Sub derivePatchName()

        If Not String.IsNullOrEmpty(Tag1TextBox.Text) And Not String.IsNullOrEmpty(Tag2TextBox.Text) Then

            PatchNameTextBox.Text = Globals.currentBranch & "_" & Common.dropFirstSegment(Tag1TextBox.Text, ".") & "_" & Common.dropFirstSegment(Tag2TextBox.Text, ".") & "_" & SchemaComboBox.SelectedItem.ToString

            If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then
                PatchNameTextBox.Text = PatchNameTextBox.Text & "_" & SupIdTextBox.Text

            End If
        Else
            MsgBox("Please select two tags, and review changes, to allow derivation of PatchName")

        End If


    End Sub

    Private Sub derivePatchDir()
        PatchDirTextBox.Text = Globals.RootPatchDir & PatchPathTextBox.Text & PatchNameTextBox.Text & "\"
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


    Private Sub FindPatches(ByRef foundPatches As TreeView, ByVal restrictToBranch As Boolean, ByRef sender As Object)


        Dim lfoundPatches As Collection = New Collection

        sender.text = "Expand"

        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", lfoundPatches, Globals.RootPatchDir)

            If restrictToBranch Then
                'For i As Integer = lfoundPatches.Count - 1 To 0 Step -1
                For i As Integer = lfoundPatches.Count To 1 Step -1
                    If Not lfoundPatches(i).contains(Globals.currentBranch) Then
                        'This patch is not from this branch and will be removed from the list
                        lfoundPatches.Remove(i)

                    End If
                Next

            End If


        End If


        GPTrees.populateTreeFromCollection(foundPatches, lfoundPatches)

        If restrictToBranch Then
            GPTrees.treeChange_Click(sender, foundPatches)
        End If

    End Sub


    Private Sub FindPreReqs()
        FindPatches(PreReqPatchesTreeView, RestrictPreReqToBranchCheckBox.Checked, ButtonTreeChangePrereq)

    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub

    Private Sub FindSuper()
        FindPatches(SuperPatchesTreeView, RestrictSupToBranchCheckBox.Checked, ButtonTreeChangeSuper)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles SupButton.Click
        FindSuper()
    End Sub

    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecuteButton.Click
        'Host.executeSQLscriptInteractive(PatchNameTextBox.Text & "\install.sql", Globals.RootPatchDir)
        'Use patch runner to execute with a master script.
        PatchRunner.RunMasterScript("DEFINE database = '" & Globals.currentTNS & "'" & Chr(10) & "@" & PatchPathTextBox.Text & PatchNameTextBox.Text & "\install.sql")

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

        Mail.SendNotification(lUntracked & "NEW Patch: " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, "Patch created.", PatchDirTextBox.Text & "install.sql," & Globals.RootPatchDir & PatchNameTextBox.Text & ".log")

        'user
        'branch
        'tags
        'desc
        'note


    End Sub


    Public Shared Sub createPatchProcess(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String)

        Common.checkBranch(iBranchType)


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
            Main.rebaseBranch(iBranchType, iRebaseBranchOn)

        End If


        If createPatchProgress.toDoNextStep() Then
            'Review tags on the branch
            Tortoise.Log(Globals.currentRepo)
        End If


        If createPatchProgress.toDoNextStep() Then

            Dim Wizard As New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn)
            'newchildform.MdiParent = GitPatcher
            Wizard.ShowDialog() 'NEED TO WAIT HERE!!


        End If


        If createPatchProgress.toDoNextStep() Then
            MsgBox("Now is a great time to smoke test my work before i commit the patch.", MsgBoxStyle.Information, "Smoke Test")

            'Committing changed files to GIT"
            Tortoise.Commit(Globals.currentRepo, "Commit any patches you've not yet committed", True)
        End If


        If createPatchProgress.toDoNextStep() Then
            'switch
            'GitSharpFascade.switchBranch(Globals.currentRepo, "master")
            'Tortoise.Switch(Globals.currentRepo)
            'Switch to develop branch
            GitBash.Switch(Globals.currentRepo, iRebaseBranchOn)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Merge from Feature branch
            Tortoise.Merge(Globals.currentRepo)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Push to origin/develop 
            GitBash.Push(Globals.currentRepo, "origin", iRebaseBranchOn)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Synch command to verfiy that Push was successful.
            Tortoise.Sync(Globals.currentRepo)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Release to DB Target
            Main.releaseTo(iDBtarget, iBranchType)
        End If

        If createPatchProgress.toDoNextStep() Then
            'GitSharpFascade.switchBranch(Globals.currentRepo, currentBranch)
            GitBash.Switch(Globals.currentRepo, currentBranch)
        End If

        If createPatchProgress.toDoNextStep() Then
            'Snapshot VM
            MsgBox("Create a snapshot of your current VM state, to use as your next restore point.", MsgBoxStyle.Exclamation, "Snapshot VM")

        End If

        'Finish
        createPatchProgress.toDoNextStep()


    End Sub

    Private Sub FindSuperBy()
        FindPatches(SuperByPatchesTreeView, RestrictSupByToBranchCheckBox.Checked, ButtonTreeChangeSuperBy)
    End Sub


    Private Sub SupByButton_Click(sender As Object, e As EventArgs) Handles SupByButton.Click
        FindSuperBy()
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
        End Try
    End Sub


    Private Sub ButtonFindFiles_Click(sender As Object, e As EventArgs) Handles ButtonFindFiles.Click


        TreeViewFiles.PathSeparator = "\"
        TreeViewFiles.Nodes.Clear()

        Dim extrasDirCol As Collection = extrasDirCollection()

        For Each relDir In extrasDirCol
            Dim aRootDir As String = Globals.currentRepo() & relDir
            Dim aRootNode As TreeNode = New TreeNode(aRootDir)
            TreeViewFiles.Nodes.Add(aRootNode)
            PopulateTreeView(aRootDir, aRootNode)
        Next

    End Sub

    ' Private Sub TreeViewFiles_DoubleClick(sender As Object, e As MouseEventArgs) Handles TreeViewFiles.DoubleClick
    '
    '     'Ignore Doubleclick on a folder
    '     If TreeViewFiles.SelectedNode.Nodes.Count = 0 Then
    '
    '         Dim aItem As String = TreeViewFiles.SelectedNode.FullPath
    '
    '         If ExtrasListBox.Items.Contains(aItem) Then
    '             MsgBox(aItem & " has ALREADY been added to Extras.", MsgBoxStyle.Exclamation)
    '         Else
    '             ExtrasListBox.Items.Add(aItem)
    '             MsgBox("Added " & aItem & " to Extras.")
    '         End If
    '
    '
    '     End If
    '
    ' End Sub


    'Private Sub ExtrasListBox_DoubleClick(sender As Object, e As EventArgs)
    '    If ExtrasListBox.Items.Count > 0 Then
    '        MsgBox("Removing " & ExtrasListBox.SelectedItem & " from Extras.")
    '        ExtrasListBox.Items.RemoveAt(ExtrasListBox.SelectedIndex)
    '    End If
    'End Sub

    Private Sub ButtonTreeChange_Click(sender As Object, e As EventArgs) Handles ButtonTreeChangePrereq.Click
        'Impliments a 3 position button Expand, Contract, Collapse.
        GPTrees.treeChange_Click(sender, PreReqPatchesTreeView)
    End Sub

    Private Sub ButtonTreeChangeSuper_Click(sender As Object, e As EventArgs) Handles ButtonTreeChangeSuper.Click
        'Impliments a 3 position button Expand, Contract, Collapse.
        GPTrees.treeChange_Click(sender, SuperPatchesTreeView)
    End Sub

    Private Sub ButtonTreeChangeSuperBy_Click(sender As Object, e As EventArgs) Handles ButtonTreeChangeSuperBy.Click
        'Impliments a 3 position button Expand, Contract, Collapse.
        GPTrees.treeChange_Click(sender, SuperByPatchesTreeView)
    End Sub

    Private Sub ButtonTreeChangeFiles_Click(sender As Object, e As EventArgs) Handles ButtonTreeChangeFiles.Click
        'Impliments a 3 position button Expand, Contract, Collapse.
        GPTrees.treeChange_Click(sender, TreeViewFiles)

    End Sub

    Private Sub ButtonTreeChangeChanges_Click(sender As Object, e As EventArgs) Handles ButtonTreeChangeChanges.Click
        'Impliments a 3 position button Expand, Contract, Collapse.
        GPTrees.treeChange_Click(sender, TreeViewChanges)

    End Sub


    Shared Sub PreReqPatchesTreeView_node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles PreReqPatchesTreeView.AfterCheck

        GPTrees.CheckChildNodes(e.Node, e.Node.Checked)

    End Sub

    Shared Sub SuperPatchesTreeView_node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles SuperPatchesTreeView.AfterCheck

        GPTrees.CheckChildNodes(e.Node, e.Node.Checked)

    End Sub
    Shared Sub SuperByPatchesTreeView_node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles SuperByPatchesTreeView.AfterCheck

        GPTrees.CheckChildNodes(e.Node, e.Node.Checked)

    End Sub
    Shared Sub TreeViewChanges_node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeViewChanges.AfterCheck

        GPTrees.CheckChildNodes(e.Node, e.Node.Checked)

    End Sub

    Shared Sub TreeViewFiles_node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeViewFiles.AfterCheck

        GPTrees.CheckChildNodes(e.Node, e.Node.Checked)

    End Sub


 
    Private Sub ButtonLastPatch_Click(sender As Object, e As EventArgs) Handles ButtonLastPatch.Click
 
        Dim ChosenChanges As Collection = New Collection
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        GPTrees.ReadCheckedNodes(TreeViewChanges.TopNode, ChosenChanges, True)

        'Requery ALL patches
        RestrictPreReqToBranchCheckBox.Checked = False
        FindPreReqs()


        For Each change In ChosenChanges
            Dim patch_component As String = Common.getLastSegment(change.ToString(), "/")
            Dim LastPatch As String = PatchRunner.FindLastPatch(patch_component)
            If IsNothing(LastPatch) Then
                Logger.Dbg("No previous patch for Change: " & patch_component)
            End If
            'MsgBox("Change: " & patch_component & " LastPatch: " & LastPatch)
            Dim l_found As Boolean = False
            GPTrees.TickNode(PreReqPatchesTreeView.TopNode, LastPatch, l_found)
            If Not l_found Then
                MsgBox("Unable to find patch: " & LastPatch & " for Change: " & patch_component)
            End If

 
        Next

        'Set Prereq tree to Contract view.
        ButtonTreeChangePrereq.Text = "Contract"

        GPTrees.treeChange_Click(ButtonTreeChangePrereq, PreReqPatchesTreeView)

    End Sub

  
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GPTrees.RemoveNodes(TreeViewFiles.Nodes, True)
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        GPTrees.RemoveNodes(TreeViewFiles.Nodes, False)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GPTrees.RemoveNodes(PreReqPatchesTreeView.Nodes, True)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        GPTrees.RemoveNodes(PreReqPatchesTreeView.Nodes, False)
    End Sub
 
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        GPTrees.RemoveNodes(SuperPatchesTreeView.Nodes, True)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        GPTrees.RemoveNodes(SuperPatchesTreeView.Nodes, False)
    End Sub
 
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        GPTrees.RemoveNodes(SuperByPatchesTreeView.Nodes, True)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        GPTrees.RemoveNodes(SuperByPatchesTreeView.Nodes, False)
    End Sub
End Class