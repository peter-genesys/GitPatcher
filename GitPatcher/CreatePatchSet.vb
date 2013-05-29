﻿
Public Class CreatePatchCollection
    Private pPatchName As String = Nothing
    Private pCreatePatchType As String = Nothing
    Private pFindPatchTypes As String = Nothing
    Private pFindPatchFilters As String = Nothing
    Private pPrereqPatchTypes As String = Nothing
    Private pSupPatchTypes As String = Nothing

    Public Sub New(ByVal iPatchName As String, ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String)

        If String.IsNullOrEmpty(iPatchName) Then
            Dim l_app_version = InputBox("Please enter a new version for " & Main.AppCodeTextBox.Text & " in the format: 2.17.01", "New " & Main.ApplicationListComboBox.SelectedItem & " Version")

            pPatchName = Main.AppCodeTextBox.Text & "-" & l_app_version
        Else
            pPatchName = iPatchName
        End If


        pCreatePatchType = iCreatePatchType
        pFindPatchTypes = iFindPatchTypes
        pFindPatchFilters = iFindPatchFilters
        pPrereqPatchTypes = iPrereqPatchTypes
        pSupPatchTypes = iSupPatchTypes

        InitializeComponent()


        PreReqPatchTypeComboBox.Items.Clear()
        For Each PatchType In pPrereqPatchTypes.Split(",")
            PatchType = Trim(PatchType)
            PatchType = PatchType.Replace(Chr(13), "")
            If (PatchType.Length > 0) Then
                PreReqPatchTypeComboBox.Items.Add(PatchType)
                PreReqPatchTypeComboBox.SelectedIndex = 0
            End If
        Next


        SupPatchTypeComboBox.Items.Clear()
        For Each PatchType In pSupPatchTypes.Split(",")
            PatchType = Trim(PatchType)
            PatchType = PatchType.Replace(Chr(13), "")
            If (PatchType.Length > 0) Then
                SupPatchTypeComboBox.Items.Add(PatchType)
                SupPatchTypeComboBox.SelectedIndex = 0
            End If
        Next


        FindTagsButton.Text = "Find Tags like " & Main.AppCodeTextBox.Text & "-X.XX.XX"

        Findtags()


    End Sub


    'Shared Sub TortoiseMerge(ByVal i_WorkingDir As String, ByVal i_merge_branch As String, Optional ByVal i_wait As Boolean = True)
    '    Dim Tortoise As New TortoiseFacade(i_wait)
    '    Tortoise.Merge(i_WorkingDir, i_merge_branch)
    'End Sub

    Private Sub Findtags()

        Dim tagsearch As String = Replace(RTrim(Main.AppCodeTextBox.Text), Chr(13), "")
        Dim tagseg As String = Nothing

        TagsCheckedListBox.Items.Clear()
        For Each tagname In GitSharpFascade.getTagList(My.Settings.CurrentRepo)
            tagseg = Common.getFirstSegment(tagname, "-")
            If tagseg = tagsearch Then
                TagsCheckedListBox.Items.Add(tagname)
            End If
        Next

        TagsCheckedListBox.SetItemChecked(TagsCheckedListBox.Items.Count - 1, True)

        TagsCheckedListBox.Items.Add("HEAD")

        TagsCheckedListBox.SetItemChecked(TagsCheckedListBox.Items.Count - 1, True)
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





    Private Sub FindPatches()



        AvailablePatchesListBox.Items.Clear()

        If Not String.IsNullOrEmpty(Tag1TextBox.Text) And Not String.IsNullOrEmpty(Tag2TextBox.Text) Then

            For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "patch/", False)

                If change.contains("install.sql") And Common.stringContainsSetMember(change, pFindPatchTypes, ",") And Common.stringContainsSetMember(change, pFindPatchFilters, ",") Then
                    'PatchesCheckedListBox.Items.Add(change)
                    AvailablePatchesListBox.Items.Add(Common.dropLastSegment(Common.dropFirstSegment(change, "/"), "/"))
                    'PatchesCheckedListBox.Items.Add(dropFirstSegment(change, "/"))
                    'PatchesCheckedListBox.Items.Add(dropLastSegment(change, "/"))

                    ' AvailablePatchesListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
                End If

            Next

        Else

            Dim allPatches As New Collection()

            If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

                RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text, "install.sql", allPatches, Main.RootPatchDirTextBox.Text)

            End If
            For Each patchname As String In allPatches
                If (String.IsNullOrEmpty(pFindPatchTypes) Or Common.stringContainsSetMember(patchname, pFindPatchTypes, ",")) And Common.stringContainsSetMember(patchname, pFindPatchFilters, ",") Then
                    AvailablePatchesListBox.Items.Add(patchname)
                    'PatchesCheckedListBox.SetItemChecked(PatchesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
                End If
            Next

        End If


    End Sub

    Private Sub AvailablePatchesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AvailablePatchesListBox.DoubleClick
        If Not ChosenPatchesListBox.Items.Contains(AvailablePatchesListBox.SelectedItem) Then
            ChosenPatchesListBox.Items.Add(AvailablePatchesListBox.SelectedItem)
        End If

    End Sub

    Private Sub ChosenPatchesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChosenPatchesListBox.DoubleClick

        If ChosenPatchesListBox.Items.Count > 0 Then
            ChosenPatchesListBox.Items.RemoveAt(ChosenPatchesListBox.SelectedIndex)
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

        'Iterate through the PatchableCheckedListBox
        'convert Patchable items to a collection
        Dim filenames As Collection = New Collection

        For i = 0 To PatchableCheckedListBox.Items.Count - 1
            filenames.Add(PatchableCheckedListBox.Items(i))

        Next


        'Create filenames from the ChosenPatches

        'filenames = GitSharpFascade.exportTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, PatchesCheckedListBox.CheckedItems, PatchDirTextBox.Text)




        'Write the install script
        writeInstallScript(PatchNameTextBox.Text, _
                           pCreatePatchType, _
                           "PATCH_ADMIN", _
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

    ' Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
    '     'Loop thru items.
    '     For i As Integer = 0 To PatchesCheckedListBox.Items.Count - 1
    '         PatchesCheckedListBox.SetItemChecked(i, CheckAllCheckBox.Checked)
    '
    '     Next
    ' End Sub

    Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged

        ' deriveSchemas()

    End Sub

    Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
        ' deriveSchemas()
    End Sub

    'Private Sub ViewButton_Click(sender As Object, e As EventArgs)
    '    MsgBox(GitSharpFascade.viewTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, PatchesCheckedListBox.CheckedItems))
    'End Sub

    ' Private Sub RemoveButton_Click(sender As Object, e As EventArgs)
    '     Dim temp As Collection = New Collection
    '
    '
    '     For i As Integer = 0 To PatchesCheckedListBox.Items.Count - 1
    '         If Not PatchesCheckedListBox.CheckedIndices.Contains(i) Then
    '             'MsgBox(ChangesCheckedListBox.Items(i).ToString)
    '             temp.Add(PatchesCheckedListBox.Items(i).ToString)
    '
    '         End If
    '
    '
    '     Next
    '
    '     PatchesCheckedListBox.Items.Clear()
    '
    '     For i As Integer = 1 To temp.Count
    '         If Not PatchesCheckedListBox.CheckedIndices.Contains(i) Then
    '             'MsgBox(ChangesCheckedListBox.Items(i).ToString)
    '             ' temp.Add(ChangesCheckedListBox.Items(i).ToString)
    '
    '             PatchesCheckedListBox.Items.Add(temp(i), CheckAllCheckBox.Checked)
    '
    '         End If
    '
    '
    '     Next
    '
    '
    '
    ' End Sub

    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        'PatchDirTextBox.Text = Main.RepoComboBox.SelectedItem.ToString & "\patch\" & PatchNameTextBox.Text & "\"
        PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & Replace(PatchNameTextBox.Text, "/", "\") & "\"
    End Sub






    Shared Sub writeInstallScript(ByVal patch_name As String, _
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
                                  ByRef iSkipFiles As CheckedListBox.CheckedItemCollection, _
                                  ByRef prereq_patches As CheckedListBox.CheckedItemCollection, _
                                  ByRef supersedes_patches As CheckedListBox.CheckedItemCollection, _
                                  ByVal patchDir As String, _
                                  ByVal groupPath As String)




        Dim l_file_extension As String = Nothing
        Dim l_install_file_line As String = Nothing

        Dim l_all_programs As String = Nothing
        Dim l_patches As String = Nothing

        Dim l_show_error As String = Chr(10) & "Show error;"

        Dim l_patchable_count As Integer = 0

        Dim rerunnable_yn As String = "N"
        If rerunnable Then
            rerunnable_yn = "Y"
        End If



        For Each l_path In targetFiles

            Dim l_filename As String = Common.getLastSegment(l_path, "/")
            Dim l_dos_path As String = Replace(l_path, "/", "\")

            If iSkipFiles.Contains(l_path) Then
                l_install_file_line = Chr(10) & "PROMPT SKIPPED FOR TESTING " & l_filename & " " & _
                                      Chr(10) & "--@" & l_dos_path & "\install.sql;"

            Else
                l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " & _
                                      Chr(10) & "@" & l_dos_path & "\install.sql;"

            End If


            l_patches = l_patches & l_install_file_line

            If String.IsNullOrEmpty(l_all_programs) Then
                l_all_programs = l_filename
            Else
                l_all_programs = l_all_programs & "' -" & Chr(10) & "||'," & l_filename
            End If


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

                l_master_file.WriteLine( _
                "execute patch_admin.patch_installer.patch_started( -" _
    & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
    & Chr(10) & " ,i_patch_type         => '" & patch_type & "' -" _
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
                    l_prereq_short_name = Common.getLastSegment(l_prereq_patch, "\")
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

            l_master_file.WriteLine("Prompt installing PATCHES" & Chr(10) & l_patches)


            l_master_file.WriteLine("COMMIT;")

            If use_patch_admin Then

                l_master_file.WriteLine("execute patch_admin.patch_installer.patch_completed(i_patch_name  => '" & patch_name & "');")

                Dim l_sup_short_name As String = Nothing
                For Each l_sup_patch In supersedes_patches
                    l_sup_short_name = Common.getLastSegment(l_sup_patch, "\")
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

        For i As Integer = 0 To ChosenPatchesListBox.Items.Count - 1

            PatchableCheckedListBox.Items.Add(ChosenPatchesListBox.Items(i).ToString)

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
            Application.DoEvents()
            deriveTags()

            If AvailablePatchesListBox.Items.Count = 0 Then
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

            PatchPathTextBox.Text = pCreatePatchType & "\" & Main.AppCodeTextBox.Text & "\" 'Replace(Main.BranchPathTextBox.Text, "/", "\") & "\"

            derivePatchName()

            PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & PatchPathTextBox.Text & PatchNameTextBox.Text & "\"

            UsePatchAdminCheckBox.Checked = True

            RerunCheckBox.Checked = False

            If PatchableCheckedListBox.Items.Count = 0 Then
                CopySelectedChanges()
            End If
        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageExecute" Then

            ExecutePatchButton.Text = "Execute Patch on " & My.Settings.CurrentDB

        End If

    End Sub

    Private Sub derivePatchName()



        PatchNameTextBox.Text = pPatchName

        If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then
            PatchNameTextBox.Text = PatchNameTextBox.Text & "_" & SupIdTextBox.Text

        End If

    End Sub


    Private Sub SupIdTextBox_TextChanged(sender As Object, e As EventArgs) Handles SupIdTextBox.TextChanged
        derivePatchName()
    End Sub


    Private Sub FindPreReqs()
        Dim searchPath As String = Nothing
        If PreReqPatchTypeComboBox.SelectedItem <> "ALL" Then
            searchPath = PreReqPatchTypeComboBox.SelectedItem & "\"
        End If

        PrereqsCheckedListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            PatchRunner.RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text & searchPath, "install.sql", PrereqsCheckedListBox, Main.RootPatchDirTextBox.Text)

        End If
    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub



    Private Sub FindSuper()

        Dim searchPath As String = Nothing
        If SupPatchTypeComboBox.SelectedItem <> "ALL" Then
            searchPath = SupPatchTypeComboBox.SelectedItem & "\"
        End If

        SupersedesCheckedListBox.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            PatchRunner.RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text & searchPath, "install.sql", SupersedesCheckedListBox, Main.RootPatchDirTextBox.Text)

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

        'If TagsCheckedListBox.CheckedItems.Count > 0 Then
        '    Tag1TextBox.Text = TagsCheckedListBox.CheckedItems.Item(0)
        'Else
        '    Tag1TextBox.Text = ""
        'End If
        'If TagsCheckedListBox.CheckedItems.Count > 1 Then
        '    Tag2TextBox.Text = TagsCheckedListBox.CheckedItems.Item(1)
        'Else
        '    Tag2TextBox.Text = ""
        'End If

        If TagsCheckedListBox.CheckedItems.Count = 0 Then
            'Nothing checked so select no tags
            Tag1TextBox.Text = ""
            Tag2TextBox.Text = ""
            If TagsCheckedListBox.Items.Count > 0 Then
                'But select the last available tag as the tag2, to be used as patch_name
                Tag2TextBox.Text = TagsCheckedListBox.Items(TagsCheckedListBox.Items.Count - 1)
            End If

        ElseIf TagsCheckedListBox.CheckedItems.Count = 1 Then
            'Only 1 tag selected set as the tag2, to be used as patch_name
            Tag1TextBox.Text = ""
            Tag2TextBox.Text = TagsCheckedListBox.CheckedItems.Item(0)

        ElseIf TagsCheckedListBox.CheckedItems.Count > 1 Then
            'Select 1st and 2nd checked tags as tag1 and tag2
            Tag1TextBox.Text = TagsCheckedListBox.CheckedItems.Item(0)
            Tag2TextBox.Text = TagsCheckedListBox.CheckedItems.Item(1)
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


        Dim l_app_version = InputBox("Please enter a new version for " & Main.AppCodeTextBox.Text & " in the format: 2.17.01", "New " & Main.ApplicationListComboBox.SelectedItem & " Version")

        l_app_version = Main.AppCodeTextBox.Text & "-" & l_app_version

        Dim newBranch As String = "release/" & iCreatePatchType & "/" & Main.AppCodeTextBox.Text & "/" & l_app_version

        Dim currentBranch As String = GitSharpFascade.currentBranch(My.Settings.CurrentRepo)

        Dim createPatchSetProgress As ProgressDialogue = New ProgressDialogue("Create DB " & iCreatePatchType)
        createPatchSetProgress.MdiParent = GitPatcher

        createPatchSetProgress.addStep("Switch to develop branch", 5)
        createPatchSetProgress.addStep("Pull from origin/develop", 10)
        createPatchSetProgress.addStep("Create and Switch to release Branch: " & newBranch, 15)
        createPatchSetProgress.addStep("Create, edit and test " & iCreatePatchType, 20)
        createPatchSetProgress.addStep("Bump Apex version to " & l_app_version, 25)
        createPatchSetProgress.addStep("Commit Apex version " & l_app_version, 30)
        createPatchSetProgress.addStep("Tag this release as " & l_app_version, 35)
        createPatchSetProgress.addStep("Push to origin/" & newBranch, 40)

        createPatchSetProgress.addStep("Switch to develop branch", 45)
        createPatchSetProgress.addStep("Pull from origin/develop", 50)
        createPatchSetProgress.addStep("Merge from release Branch: " & newBranch, 55)
        createPatchSetProgress.addStep("Commit - incase of merge conflict", 60)
        createPatchSetProgress.addStep("Push to origin/develop", 65)


        createPatchSetProgress.addStep("Manually Execute " & iCreatePatchType & " " & l_app_version & " on target database.", 70)
        createPatchSetProgress.addStep("Manually Import Apex " & l_app_version & " into target database.", 100)
        'Import

        createPatchSetProgress.Show()

        'Switch to develop branch
        GitBash.Switch(My.Settings.CurrentRepo, "develop")
        createPatchSetProgress.goNextStep()

        'Pull from origin/develop
        GitBash.Pull(My.Settings.CurrentRepo, "origin", "develop")
        createPatchSetProgress.goNextStep()

        'Create and Switch to new collection branch
        GitBash.createBranch(My.Settings.CurrentRepo, newBranch)
        createPatchSetProgress.goNextStep()


        'Create, edit And test collection
        Dim Wizard As New CreatePatchCollection(l_app_version, iCreatePatchType, iFindPatchTypes, iFindPatchFilters, iPrereqPatchTypes, iSupPatchTypes)
        Wizard.ShowDialog() 'WAITING HERE!!
        createPatchSetProgress.goNextStep()


        'Bump Apex version 
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

        Do
            'For each line
            If l_old_file.EndOfStream Then Exit Do

            l_line = l_old_file.ReadLine()
            If l_line.Contains("p_flow_version") Then
                l_line = "  p_flow_version=> '" & l_app_version & " " & Today.ToString("dd-MMM-yyyy") & "',"
            End If


            l_new_file.WriteLine(l_line)

        Loop

        l_old_file.Close()
        l_new_file.Close()

        FileIO.deleteFileIfExists(l_create_application_old)

        createPatchSetProgress.goNextStep()

        'Commit Apex version 
        Tortoise.Commit(My.Settings.CurrentRepo, "Bump Apex " & Main.ApexAppTextBox.Text & " to " & l_app_version)
        createPatchSetProgress.goNextStep()

        'Tag this commit
        GitBash.Tag(My.Settings.CurrentRepo, l_app_version, "New " & Main.ApplicationListComboBox.SelectedItem & " " & iCreatePatchType & " " & l_app_version)
        createPatchSetProgress.goNextStep()

        'Push release to origin with tags
        GitBash.Push(My.Settings.CurrentRepo, "origin", newBranch, True)
        createPatchSetProgress.goNextStep()

        'Switch to develop branch
        GitBash.Switch(My.Settings.CurrentRepo, "develop")
        createPatchSetProgress.goNextStep()

        'Pull from origin/develop
        GitBash.Pull(My.Settings.CurrentRepo, "origin", "develop")
        createPatchSetProgress.goNextStep()

        'Merge from release
        Tortoise.Merge(My.Settings.CurrentRepo)
        createPatchSetProgress.goNextStep()

        'Commit - incase of merge conflict
        Tortoise.Commit(My.Settings.CurrentRepo, "Merge " & newBranch & " CANCEL IF NO MERGE CONFLICTS")
        createPatchSetProgress.goNextStep()

        'Push to origin/develop 
        GitBash.Push(My.Settings.CurrentRepo, "origin", "develop")
        createPatchSetProgress.goNextStep()

        'MsgBox("Execute " & iCreatePatchType & " on target database.")
        'createPatchSetProgress.goNextStep()
        '
        'MsgBox("Execute " & iCreatePatchType & " on target database.")
        'createPatchSetProgress.goNextStep()



        'Done
        createPatchSetProgress.done()

    End Sub


End Class