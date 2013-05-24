
Public Class CreatePatchCollection
    Private pCreatePatchType As String = Nothing
    Private pFindPatchTypes As String = Nothing
    Private pFindPatchFilters As String = Nothing
    Private pPrereqPatchTypes As String = Nothing
    Private pSupPatchTypes As String = Nothing

    Public Sub New(ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String)


        pCreatePatchType = iCreatePatchType
        pFindPatchTypes = iFindPatchTypes
        pFindPatchFilters = iFindPatchFilters
        pPrereqPatchTypes = iPrereqPatchTypes
        pSupPatchTypes = iSupPatchTypes

        InitializeComponent()

        FindTagsButton.Text = "Find Tags like " & Main.ApplicationListComboBox.SelectedItem & "-X.XX.XX"

        Findtags()


    End Sub


    'Shared Sub TortoiseMerge(ByVal i_WorkingDir As String, ByVal i_merge_branch As String, Optional ByVal i_wait As Boolean = True)
    '    Dim Tortoise As New TortoiseFacade(i_wait)
    '    Tortoise.Merge(i_WorkingDir, i_merge_branch)
    'End Sub

    Private Sub Findtags()
        TagsCheckedListBox.Items.Clear()
        For Each tagname In GitSharpFascade.getTagList(My.Settings.CurrentRepo)
            If PatchRunner.get_first_split(tagname, "-") = Main.CurrentBranchTextBox.Text Then
                TagsCheckedListBox.Items.Add(tagname)
            End If
        Next
    End Sub


    Public Sub RecursiveSearchContainingFolder(ByVal strPath As String, ByVal strPattern As String, ByRef patches As Collection, ByVal removePath As String)

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)

        'Add the files
        For Each strFile As String In strFiles
            patches.Add(strPath.Substring(removePath.Length))
            'Logger.Dbg(strPath.Substring(removePath.Length), "All Patches")
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearchContainingFolder(strFolder, strPattern, patches, removePath)
        Next

    End Sub



    '  Private Sub FindPatches()
    '
    '
    '      Dim allPatches As New Collection()
    '
    '      If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then
    '
    '          RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text, "install.sql", allPatches, Main.RootPatchDirTextBox.Text)
    '
    '      End If
    '
    '      'Try
    '      'If SchemaComboBox.Text = "" Then
    '      '    Throw (New Halt("Schema not selected"))
    '      'End If
    '
    '      PatchesCheckedListBox.Items.Clear()
    '
    '      'For Each patchname As String In allPatches
    '      '    PatchesCheckedListBox.Items.Add(patchname)
    '      '    PatchesCheckedListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
    '      '    'For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, patchname, False)
    '      '    'PatchesCheckedListBox.Items.Add(change)
    '      '    'PatchesCheckedListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
    '      '    'Next
    '      'Next
    '
    '      For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "patch/", False)
    '          For Each patchname As String In allPatches
    '              If change.contains(Replace(patchname, "\", "/")) And change.contains("install.sql") Then
    '                  PatchesCheckedListBox.Items.Add(change)
    '                  PatchesCheckedListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
    '              End If
    '          Next
    '      Next
    '
    '
    '      'Catch schema_not_selected As Halt
    '      'MsgBox("Please select a schema")
    '      'End Try
    '  End Sub


    Function stringContainsSetMember(iString As String, iSet As String, idelim As String) As Boolean
        Dim lResult As Boolean = False
        For Each member In iSet.Split(idelim)
            lResult = lResult Or iString.Contains(member)
        Next

        Return lResult

    End Function


    Private Sub FindPatches()



        PatchesCheckedListBox.Items.Clear()

        If Not String.IsNullOrEmpty(Tag1TextBox.Text) And Not String.IsNullOrEmpty(Tag2TextBox.Text) Then

            For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "patch/", False)

                If change.contains("install.sql") And stringContainsSetMember(change, pFindPatchTypes, ",") And stringContainsSetMember(change, pFindPatchFilters, ",") Then
                    'PatchesCheckedListBox.Items.Add(change)
                    PatchesCheckedListBox.Items.Add(dropLastSegment(dropFirstSegment(change, "/"), "/"))
                    'PatchesCheckedListBox.Items.Add(dropFirstSegment(change, "/"))
                    'PatchesCheckedListBox.Items.Add(dropLastSegment(change, "/"))

                    PatchesCheckedListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
                End If

            Next

        Else

            Dim allPatches As New Collection()

            If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

                RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text, "install.sql", allPatches, Main.RootPatchDirTextBox.Text)

            End If
            For Each patchname As String In allPatches
                If stringContainsSetMember(patchname, pFindPatchTypes, ",") And stringContainsSetMember(patchname, pFindPatchFilters, ",") Then
                    PatchesCheckedListBox.Items.Add(patchname)
                    PatchesCheckedListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
                End If
            Next

        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindPatches()


    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click

        'Create Patch Dir
        Try
            FileIO.confirmDeleteFolder(PatchDirTextBox.Text)
        Catch cancelled_delete As Halt
            MsgBox("Warning: Now overwriting existing patch")
        End Try

        Dim l_create_folder As String = Main.RootPatchDirTextBox.Text
        For Each folder In Main.BranchPathTextBox.Text.Split("/")
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

        filenames = GitSharpFascade.exportTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, PatchesCheckedListBox.CheckedItems, PatchDirTextBox.Text)

        'Write the install script
        writeInstallScript(PatchNameTextBox.Text, _
                           SchemaComboBox.Text, _
                           Main.BranchPathTextBox.Text, _
                           Tag1TextBox.Text, _
                           Tag2TextBox.Text, _
                           SupIdTextBox.Text, _
                           PatchDescTextBox.Text, _
                           NoteTextBox.Text, _
                           UsePatchAdminCheckBox.Checked, _
                           RerunCheckBox.Checked, _
                           filenames, _
                           PatchableCheckedListBox.CheckedItems, _
                           PrereqsCheckedListBox.CheckedItems, _
                           SupersedesCheckedListBox.CheckedItems, _
                           PatchDirTextBox.Text, _
                           PatchPathTextBox.Text)

        Host.RunExplorer(PatchDirTextBox.Text)



    End Sub

    ' Private Sub deriveSchemas()
    '     SchemaComboBox.Items.Clear()
    '     SchemaComboBox.Text = ""
    '     For Each schema In GitSharpFascade.getSchemaList(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database")
    '         SchemaComboBox.Items.Add(schema)
    '     Next
    '
    '     SchemaCountTextBox.Text = SchemaComboBox.Items.Count
    '
    '     'If exactly one schema found then select it
    '     'otherwise force user to choose one.
    '     If SchemaComboBox.Items.Count = 1 Then
    '         SchemaComboBox.SelectedIndex = 0
    '     End If
    ' End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAllCheckBox.CheckedChanged
        'Loop thru items.
        For i As Integer = 0 To PatchesCheckedListBox.Items.Count - 1
            PatchesCheckedListBox.SetItemChecked(i, CheckAllCheckBox.Checked)

        Next
    End Sub

    Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged

        ' deriveSchemas()

    End Sub

    Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
        ' deriveSchemas()
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click
        MsgBox(GitSharpFascade.viewTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, PatchesCheckedListBox.CheckedItems))
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveButton.Click
        Dim temp As Collection = New Collection


        For i As Integer = 0 To PatchesCheckedListBox.Items.Count - 1
            If Not PatchesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)
                temp.Add(PatchesCheckedListBox.Items(i).ToString)

            End If


        Next

        PatchesCheckedListBox.Items.Clear()

        For i As Integer = 1 To temp.Count
            If Not PatchesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)
                ' temp.Add(ChangesCheckedListBox.Items(i).ToString)

                PatchesCheckedListBox.Items.Add(temp(i), CheckAllCheckBox.Checked)

            End If


        Next



    End Sub

    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        'PatchDirTextBox.Text = Main.RepoComboBox.SelectedItem.ToString & "\patch\" & PatchNameTextBox.Text & "\"
        PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & Replace(PatchNameTextBox.Text, "/", "\") & "\"
    End Sub

    Public Shared Function getLastSegment(ByVal ipath As String, ByVal idelim As String) As String
        Dim Path() As String = ipath.Split(idelim)
        Dim SplitCount = Path.Length
        Dim l_last As String = ipath.Split(idelim)(SplitCount - 1)

        Return l_last
    End Function

    Public Shared Function dropFirstSegment(ByVal ipath As String, ByVal idelim As String) As String

        Dim l_from_first As String = Nothing
        Dim delim_pos As Integer = ipath.IndexOf(idelim)
        If delim_pos > 0 Then
            l_from_first = ipath.Remove(0, delim_pos + 1)
        End If

        Return l_from_first
    End Function

    Public Shared Function dropLastSegment(ByVal ipath As String, ByVal idelim As String) As String

        Dim l_to_last As String = Nothing
        Dim delim_pos As Integer = ipath.LastIndexOf(idelim)
        If delim_pos > 0 Then
            l_to_last = ipath.Remove(delim_pos, ipath.Length - delim_pos)
        End If

        Return l_to_last
    End Function




    Shared Sub writeInstallScript(ByVal patch_name As String, _
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
                                  ByRef prereq_patches As CheckedListBox.CheckedItemCollection, _
                                  ByRef supersedes_patches As CheckedListBox.CheckedItemCollection, _
                                  ByVal patchDir As String, _
                                  ByVal groupPath As String)


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



        For Each l_path In targetFiles

            Dim l_filename As String = getLastSegment(l_path, "/")

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
                l_master_file.WriteLine("CONNECT " & db_schema & "/&&" & db_schema & "_password@&&database as sysdba")
            Else
                l_master_file.WriteLine("CONNECT " & db_schema & "/&&" & db_schema & "_password@&&database")
            End If


            l_master_file.WriteLine("set serveroutput on;")

            If use_patch_admin Then

                l_master_file.WriteLine( _
                "execute patch_admin.patch_installer.patch_started( -" _
    & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
    & Chr(10) & " ,i_db_schema          => '" & db_schema & "' -" _
    & Chr(10) & " ,i_branch_name        => '" & branch_path & "' -" _
    & Chr(10) & " ,i_tag_from           => '" & tag1_name & "' -" _
    & Chr(10) & " ,i_tag_to             => '" & tag2_name & "' -" _
    & Chr(10) & " ,i_supplementary      => '" & supplementary & "' -" _
    & Chr(10) & " ,i_patch_desc         => '" & patch_desc & "' -" _
    & Chr(10) & " ,i_patch_componants   => '" & l_all_programs & "' -" _
    & Chr(10) & " ,i_patch_create_date  => '" & DateString & "' -" _
    & Chr(10) & " ,i_patch_created_by   => '" & Environment.UserName & "' -" _
    & Chr(10) & " ,i_note               => '" & note & "' -" _
    & Chr(10) & " ,i_rerunnable_yn      => '" & rerunnable_yn & "' -" _
    & Chr(10) & " ,i_remove_prereqs     => 'N' -" _
    & Chr(10) & " ,i_remove_sups        => 'N'); " _
    & Chr(10))


                Dim l_prereq_short_name As String = Nothing
                For Each l_prereq_patch In prereq_patches
                    l_prereq_short_name = PatchFromTags.get_last_split(l_prereq_patch, "\")
                    l_master_file.WriteLine("PROMPT")
                    l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_short_name)
                    l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_prereq( -")
                    l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                    l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")

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
                l_master_file.WriteLine("Prompt installing USERS" & Chr(10) & l_db_objects_users)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_tables) Then
                l_master_file.WriteLine("Prompt installing TABLES" & Chr(10) & l_db_objects_tables)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_sequences) Then
                l_master_file.WriteLine("Prompt installing SEQUENCES" & Chr(10) & l_db_objects_sequences)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_type_specs) Then
                l_master_file.WriteLine("Prompt installing TYPE SPECS" & Chr(10) & l_db_objects_type_specs)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_roles) Then
                l_master_file.WriteLine("Prompt installing ROLES" & Chr(10) & l_db_objects_roles)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_dblinks) Then
                l_master_file.WriteLine("Prompt installing DB_LINKS" & Chr(10) & l_db_objects_dblinks)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_functions) Then
                l_master_file.WriteLine("Prompt installing FUNCTIONS" & Chr(10) & l_db_objects_functions)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_procedures) Then
                l_master_file.WriteLine("Prompt installing PROCEDURES" & Chr(10) & l_db_objects_procedures)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_package_specs) Then
                l_master_file.WriteLine("Prompt installing PACKAGE SPECS" & Chr(10) & l_db_objects_package_specs)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_views) Then
                l_master_file.WriteLine("Prompt installing VIEWS" & Chr(10) & l_db_objects_views)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_mviews) Then
                l_master_file.WriteLine("Prompt installing MATERIALISED VIEWS" & Chr(10) & l_db_objects_mviews)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_grants) Then
                l_master_file.WriteLine("Prompt installing GRANTS" & Chr(10) & l_db_objects_grants)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_synonyms) Then
                l_master_file.WriteLine("Prompt installing SYNONYMS" & Chr(10) & l_db_objects_synonyms)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_type_bodies) Then
                l_master_file.WriteLine("Prompt installing TYPE BODIES" & Chr(10) & l_db_objects_type_bodies)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_package_bodies) Then
                l_master_file.WriteLine("Prompt installing PACKAGE BODIES" & Chr(10) & l_db_objects_package_bodies)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_triggers) Then
                l_master_file.WriteLine("Prompt installing TRIGGERS" & Chr(10) & l_db_objects_triggers)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_indexes) Then
                l_master_file.WriteLine("Prompt installing INDEXES" & Chr(10) & l_db_objects_indexes)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_primary_keys) Then
                l_master_file.WriteLine("Prompt installing PRIMARY KEYS" & Chr(10) & l_db_objects_primary_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_unique_keys) Then
                l_master_file.WriteLine("Prompt installing UNIQUE KEYS" & Chr(10) & l_db_objects_unique_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_non_unique_keys) Then
                l_master_file.WriteLine("Prompt installing NON-UNIQUE KEYS" & Chr(10) & l_db_objects_non_unique_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_data) Then
                l_master_file.WriteLine("Prompt installing DATA" & Chr(10) & l_db_objects_data)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_foreign_keys) Then
                l_master_file.WriteLine("Prompt installing FOREIGN KEYS" & Chr(10) & l_db_objects_foreign_keys)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_constraints) Then
                l_master_file.WriteLine("Prompt installing CONSTRAINTS" & Chr(10) & l_db_objects_constraints)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_configuration) Then
                l_master_file.WriteLine("Prompt installing CONFIGURATION" & Chr(10) & l_db_objects_configuration)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_jobs) Then
                l_master_file.WriteLine("Prompt installing JOBS" & Chr(10) & l_db_objects_jobs)
            End If
            If Not String.IsNullOrEmpty(l_db_objects_misc) Then
                l_master_file.WriteLine("Prompt installing MISCELLANIOUS" & Chr(10) & l_db_objects_misc)
            End If


            l_master_file.WriteLine("COMMIT;")

            If use_patch_admin Then
                l_master_file.WriteLine("PROMPT Compiling objects in schema " & db_schema)
                l_master_file.WriteLine("execute patch_admin.patch_invoker.compile_post_patch;")
                l_master_file.WriteLine("execute patch_admin.patch_installer.patch_completed;")

                Dim l_sup_short_name As String = Nothing
                For Each l_sup_patch In supersedes_patches
                    l_sup_short_name = PatchFromTags.get_last_split(l_sup_patch, "\")
                    l_master_file.WriteLine("PROMPT")
                    l_master_file.WriteLine("PROMPT Superseding patch " & l_sup_short_name)
                    l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_supersedes( -")
                    l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                    l_master_file.WriteLine(",i_supersedes_patch  => '" & l_sup_short_name & "' );")

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

        For i As Integer = 0 To PatchesCheckedListBox.Items.Count - 1
            If PatchesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)

                PatchableCheckedListBox.Items.Add(PatchesCheckedListBox.Items(i).ToString)

            End If


        Next
    End Sub


    Private Sub PatchTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchTabControl.SelectedIndexChanged

        'MessageBox.Show("you selected the fifth tab:  " & PatchTabControl.SelectedTab.Name.ToString)


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageTags" Then
            If TagsCheckedListBox.Items.Count = 0 Then
                Findtags()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePatches" Then
            deriveTags()

            If PatchesCheckedListBox.Items.Count = 0 Then
                FindPatches()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePreReqs" Then
            If PrereqsCheckedListBox.Items.Count = 0 Then
                FindPreReqs()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageSuper" Then
            If SupersedesCheckedListBox.Items.Count = 0 Then
                FindSuper()
            End If


        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePatchDefn" Then
            'Copy Patchable items to the next list.

            PatchPathTextBox.Text = Replace(Main.BranchPathTextBox.Text, "/", "\") & "\"


            derivePatchName()

            PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & PatchPathTextBox.Text & PatchNameTextBox.Text & "\"

            UsePatchAdminCheckBox.Checked = True

            RerunCheckBox.Checked = True

            If PatchableCheckedListBox.Items.Count = 0 Then
                CopySelectedChanges()
            End If
        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageExecute" Then

            ExecutePatchButton.Text = "Execute Patch on " & My.Settings.CurrentDB

        End If

    End Sub

    Private Sub derivePatchName()

        PatchNameTextBox.Text = Tag2TextBox.Text

        If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then
            PatchNameTextBox.Text = PatchNameTextBox.Text & "_" & SupIdTextBox.Text

        End If

    End Sub


    Private Sub SupIdTextBox_TextChanged(sender As Object, e As EventArgs) Handles SupIdTextBox.TextChanged
        derivePatchName()
    End Sub


    Private Sub FindPreReqs()
        PrereqsCheckedListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            PatchRunner.RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text & "patchset\", "install.sql", PrereqsCheckedListBox, Main.RootPatchDirTextBox.Text & "patchset\")

            'For Each foldername As String In IO.Directory.GetDirectories(Main.RootPatchDirTextBox.Text)
            '    PrereqsCheckedListBox.Items.Add(get_last_split(foldername, "\"))
            'Next

        End If
    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub



    Private Sub FindSuper()
        SupersedesCheckedListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            PatchRunner.RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text & "patchset\", "install.sql", SupersedesCheckedListBox, Main.RootPatchDirTextBox.Text & "patchset\")
            'For Each foldername As String In IO.Directory.GetDirectories(Main.RootPatchDirTextBox.Text)
            '    SupersedesCheckedListBox.Items.Add(get_last_split(foldername, "\"))
            'Next

        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        FindSuper()
    End Sub

    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click
        'Host.executeSQLscriptInteractive(PatchNameTextBox.Text & "\install.sql", Main.RootPatchDirTextBox.Text)
        'Use patch runner to execute with a master script.
        PatchRunner.RunMasterScript("DEFINE database = '" & My.Settings.CurrentDB & "'" & Chr(10) & "@" & PatchPathTextBox.Text & PatchNameTextBox.Text & "\install.sql")

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

    Private Sub ComitButton_Click(sender As Object, e As EventArgs) Handles ComitButton.Click
        Tortoise.Commit(PatchDirTextBox.Text, "NEW Patch: " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, True)

        'Mail.SendNotification("NEW Patch: " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, "Patch created.")

        'user
        'branch
        'tags
        'desc
        'note


    End Sub


    Public Shared Sub createCollectionProcess(ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String)


        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim createPatchSetProgress As ProgressDialogue = New ProgressDialogue("Create DB " & iCreatePatchType)
        createPatchSetProgress.MdiParent = GitPatcher

        createPatchSetProgress.addStep("Switch to Master branch", 10)
        createPatchSetProgress.addStep("Pull from origin", 20)
        createPatchSetProgress.addStep("Create tag on master branch: " & Main.PatchSetTextBox.Text & ".X.XX.XX", 30)
        createPatchSetProgress.addStep("Create and Switch to " & iCreatePatchType & " Branch: " & iCreatePatchType & "/" & Main.PatchSetTextBox.Text, 40)
        createPatchSetProgress.addStep("Create, edit and test " & iCreatePatchType, 50)
        createPatchSetProgress.addStep("Commit to " & iCreatePatchType & " Branch: " & iCreatePatchType & "/" & Main.PatchSetTextBox.Text, 60)
        createPatchSetProgress.addStep("Switch to Master branch", 70)
        createPatchSetProgress.addStep("Pull from origin", 80)
        createPatchSetProgress.addStep("Merge from " & iCreatePatchType & " Branch: " & iCreatePatchType & "/" & Main.PatchSetTextBox.Text, 90)
        createPatchSetProgress.addStep("Push to origin", 95)
        createPatchSetProgress.addStep("Execute on target database.", 100)

        createPatchSetProgress.Show()

        'createPatchSetProgress.setStep(0)

        'Switch to Master branch
        Tortoise.Switch(My.Settings.CurrentRepo)

        createPatchSetProgress.gonext()

        'Create collection branch
        GitSharpFascade.createBranch(My.Settings.CurrentRepo, iCreatePatchType & "/" & "tag123")


        'Pull from Origin 
        Tortoise.Pull(My.Settings.CurrentRepo)

        createPatchSetProgress.gonext()

        'Switch to patchset Branch
        Tortoise.Switch(My.Settings.CurrentRepo)

        createPatchSetProgress.gonext()

        'Merge from master
        Tortoise.Merge(My.Settings.CurrentRepo)

        createPatchSetProgress.gonext()

        'Show log, to create a tag
        Tortoise.Log(My.Settings.CurrentRepo)

        createPatchSetProgress.gonext()

        Dim Wizard As New CreatePatchCollection(iCreatePatchType, iFindPatchTypes, iFindPatchFilters, iPrereqPatchTypes, iSupPatchTypes)
        Wizard.ShowDialog()

        'NEED TO WAIT HERE!!
        createPatchSetProgress.gonext()


        'Committing changed files to GIT"
        Tortoise.Commit(My.Settings.CurrentRepo, "Commit any patch sets you've not yet committed", True)

        createPatchSetProgress.gonext()

        'Push to Origin 
        Tortoise.Push(My.Settings.CurrentRepo)

        createPatchSetProgress.gonext()

        'Switch back to master
        Tortoise.Switch(My.Settings.CurrentRepo)

        'Done
        createPatchSetProgress.done()

    End Sub





End Class