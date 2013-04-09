﻿ 
Public Class PatchFromTags

    Private Sub FindChanges()
        Try
            If SchemaComboBox.Text = "" Then
                Throw (New Halt("Schema not selected"))
            End If

            ChangesCheckedListBox.Items.Clear()
            For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, ViewFilesCheckBox.Checked)
                ChangesCheckedListBox.Items.Add(change)
                ChangesCheckedListBox.SetItemChecked(ChangesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
            Next


        Catch schema_not_selected As Halt
            MsgBox("Please select a schema")
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindChanges()


    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click

        'Create Patch Dir

        FileIO.confirmDeleteFolder(PatchDirTextBox.Text)

        FileIO.createFolderIfNotExists(PatchDirTextBox.Text)

        Dim filenames As Collection = Nothing

        filenames = GitSharpFascade.exportTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, ChangesCheckedListBox.CheckedItems, PatchDirTextBox.Text)

        'Write the install script
        writeInstallScript(PatchNameTextBox.Text, _
                           SchemaComboBox.Text, _
                           Main.CurrentBranchTextBox.Text, _
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
                           PatchDirTextBox.Text)

        Host.RunExplorer(PatchDirTextBox.Text)



    End Sub

    Private Sub deriveSchemas()
        SchemaComboBox.Items.Clear()
        SchemaComboBox.Text = ""
        For Each schema In GitSharpFascade.getSchemaList(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database")
            SchemaComboBox.Items.Add(schema)
        Next

        'If exactly one schema found then select it
        'otherwise force user to choose one.
        If SchemaComboBox.Items.Count = 1 Then
            SchemaComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAllCheckBox.CheckedChanged
        'Loop thru items.
        For i As Integer = 0 To ChangesCheckedListBox.Items.Count - 1
            ChangesCheckedListBox.SetItemChecked(i, CheckAllCheckBox.Checked)

        Next
    End Sub

    Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged

        deriveSchemas()

    End Sub

    Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
        deriveSchemas()
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click
        MsgBox(GitSharpFascade.viewTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, ChangesCheckedListBox.CheckedItems))
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveButton.Click
        Dim temp As Collection = New Collection


        For i As Integer = 0 To ChangesCheckedListBox.Items.Count - 1
            If Not ChangesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)
                temp.Add(ChangesCheckedListBox.Items(i).ToString)

            End If


        Next

        ChangesCheckedListBox.Items.Clear()

        For i As Integer = 1 To temp.Count
            If Not ChangesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)
                ' temp.Add(ChangesCheckedListBox.Items(i).ToString)

                ChangesCheckedListBox.Items.Add(temp(i), CheckAllCheckBox.Checked)

            End If


        Next



    End Sub

    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        'PatchDirTextBox.Text = Main.RepoComboBox.SelectedItem.ToString & "\patch\" & PatchNameTextBox.Text & "\"
        PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & PatchNameTextBox.Text & "\"
    End Sub

    Shared Function get_last_split(ByVal ipath As String, ByVal idelim As String) As String
        Dim Path() As String = ipath.Split(idelim)
        Dim SplitCount = Path.Length
        Dim l_last As String = ipath.Split(idelim)(SplitCount - 1)

        Return l_last
    End Function



    Shared Sub writeInstallScript(ByVal patch_name As String, _
                                  ByVal db_schema As String, _
                                  ByVal branch_name As String, _
                                  ByVal tag1_name As String, _
                                  ByVal tag2_name As String, _
                                  ByVal supplementary As String, _
                                  ByVal patch_desc As String, _
                                  ByVal note As String, _
                                  ByVal use_patch_admin As String, _
                                  ByVal rerunnable As Boolean, _
                                  ByRef targetFiles As Collection, _
                                  ByRef ignoreErrorFiles As CheckedListBox.CheckedItemCollection, _
                                  ByRef prereq_patches As CheckedListBox.CheckedItemCollection, _
                                  ByRef supersedes_patches As CheckedListBox.CheckedItemCollection, _
                                  ByVal patchDir As String
)


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

        'Dim l_list_sorted_by_path As System.Collections.Generic.List(Of SvnDiffSummaryEventArgs)
        'l_list_sorted_by_path = New System.Collections.Generic.List(Of SvnDiffSummaryEventArgs)(l_list)
        'l_list_sorted_by_path.Sort(Function(p1, p2) p1.Path.CompareTo(p2.Path))
        '
        'Dim l_11g_db_objects As String = Nothing

        Dim l_all_programs As String = Nothing


        Dim l_show_error As String = Chr(10) & "Show error;"

        Dim l_patchable_count As Integer = 0

        Dim rerunnable_yn As String = "N"
        If rerunnable Then
            rerunnable_yn = "Y"
        End If



        For Each l_path In targetFiles

            Dim l_filename As String = get_last_split(l_path, "/")

            'Sort the files by files extention into lists.

            l_file_extension = l_filename.Split(".")(1)

            If ignoreErrorFiles.Contains(l_path) Then
                l_install_file_line = Chr(10) & "WHENEVER SQLERROR CONTINUE" & _
                                      Chr(10) & "PROMPT " & l_filename & " " & _
                                      Chr(10) & "@" & l_filename & ";" & _
                                      Chr(10) & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK"

            Else
                l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " & _
                                      Chr(10) & "@" & l_filename & ";"

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
                        System.IO.File.Delete(l_filename)
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

            l_master_file.WriteLine("CONNECT " & db_schema & "/&&" & db_schema & "_password@&&database")

            l_master_file.WriteLine("set serveroutput on;")

            If use_patch_admin Then

                l_master_file.WriteLine( _
                "execute patch_admin.patch_installer.patch_started( -" _
    & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
    & Chr(10) & " ,i_db_schema          => '" & db_schema & "' -" _
    & Chr(10) & " ,i_branch_name        => '" & branch_name & "' -" _
    & Chr(10) & " ,i_tag_from           => '" & tag1_name & "' -" _
    & Chr(10) & " ,i_tag_to             => '" & tag2_name & "' -" _
    & Chr(10) & " ,i_supplementary      => '" & supplementary & "' -" _
    & Chr(10) & " ,i_patch_desc         => '" & patch_desc & "' -" _
    & Chr(10) & " ,i_patch_componants   => '" & l_all_programs & "' -" _
    & Chr(10) & " ,i_patch_create_date  => '" & DateString & "' -" _
    & Chr(10) & " ,i_patch_created_by   => '" & Environment.UserName & "' -" _
    & Chr(10) & " ,i_note               => '" & note & "' -" _
    & Chr(10) & " ,i_rerunnable_yn      => '" & rerunnable_yn & "');" _
    & Chr(10))



                For Each l_prereq_patch In prereq_patches

                    l_master_file.WriteLine("PROMPT")
                    l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_patch)
                    l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_prereq( -")
                    l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                    l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_patch & "' );")

                Next


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

            l_master_file.WriteLine("PROMPT ")
            l_master_file.WriteLine("PROMPT " & l_master_filename & " - COMPLETED.")

            If use_patch_admin Then

                l_master_file.WriteLine("execute patch_admin.patch_installer.patch_completed;")


                For Each l_sup_patch In supersedes_patches

                    l_master_file.WriteLine("PROMPT")
                    l_master_file.WriteLine("PROMPT Superseding patch " & l_sup_patch)
                    l_master_file.WriteLine("execute patch_admin.patch_installer.add_patch_supersedes( -")
                    l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                    l_master_file.WriteLine(",i_supersedes_patch  => '" & l_sup_patch & "' );")

                Next


                l_master_file.WriteLine("spool off;")


                l_master_file.Close()

            End If
        End If


    End Sub

    Private Sub CopySelectedChanges()
        'Copy Selected Changes to the next list box.
        PatchableCheckedListBox.Items.Clear()

        For i As Integer = 0 To ChangesCheckedListBox.Items.Count - 1
            If ChangesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)

                PatchableCheckedListBox.Items.Add(ChangesCheckedListBox.Items(i).ToString)

            End If


        Next
    End Sub


    Private Sub PatchTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchTabControl.SelectedIndexChanged

        'MessageBox.Show("you selected the fifth tab:  " & PatchTabControl.SelectedTab.Name.ToString)
        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageChanges" Then
            If ChangesCheckedListBox.Items.Count = 0 Then
                FindChanges()
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

            derivePatchName()

            PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & PatchNameTextBox.Text & "\"

            UsePatchAdminCheckBox.Checked = True

            RerunCheckBox.Checked = True

            If PatchableCheckedListBox.Items.Count = 0 Then
                CopySelectedChanges()
            End If
        End If



    End Sub

    Private Sub derivePatchName()
        If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then

            PatchNameTextBox.Text = SchemaComboBox.SelectedItem.ToString & "_" & Main.CurrentBranchTextBox.Text & "_" & Tag1TextBox.Text & "_" & Tag2TextBox.Text & "_" & SupIdTextBox.Text
        Else
            PatchNameTextBox.Text = SchemaComboBox.SelectedItem.ToString & "_" & Main.CurrentBranchTextBox.Text & "_" & Tag1TextBox.Text & "_" & Tag2TextBox.Text
        End If

    End Sub


    Private Sub SupIdTextBox_TextChanged(sender As Object, e As EventArgs) Handles SupIdTextBox.TextChanged
        derivePatchName()
    End Sub


    Private Sub FindPreReqs()
        PrereqsCheckedListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            For Each foldername As String In IO.Directory.GetDirectories(Main.RootPatchDirTextBox.Text)
                PrereqsCheckedListBox.Items.Add(get_last_split(foldername, "\"))
            Next

        End If
    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub



    Private Sub FindSuper()
        SupersedesCheckedListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            For Each foldername As String In IO.Directory.GetDirectories(Main.RootPatchDirTextBox.Text)
                SupersedesCheckedListBox.Items.Add(get_last_split(foldername, "\"))
            Next

        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
       FindSuper
    End Sub

    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click
        Host.executeSQLscriptInteractive("install.sql", PatchDirTextBox.Text)
    End Sub

    Private Sub CopyChangesButton_Click(sender As Object, e As EventArgs) Handles CopyChangesButton.Click
        CopySelectedChanges()
    End Sub
End Class