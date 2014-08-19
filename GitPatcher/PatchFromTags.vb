
Public Class PatchFromTags

    Dim gBranchType As String
    Dim gDBtarget As String
    Dim gRebaseBranchOn As String
    Dim gtag_base As String


    Function unix_path(ipath As String) As String
        Return Replace(ipath, "\", "/")
    End Function



    Public Sub New(iBranchType As String, iDBtarget As String, iRebaseBranchOn As String, itag_base As String)
        InitializeComponent()

        FindTagsButton.Text = "Find Tags like " & Globals.currentBranch & "."

        Findtags()

        gBranchType = iBranchType
        gDBtarget = iDBtarget
        gRebaseBranchOn = iRebaseBranchOn
        gtag_base = itag_base

        'NOT CURRENTLY USING THE TabPageSuperBy TABPAGE
        PatchTabControl.TabPages.Remove(TabPageSuperBy)

        HideTabs()

        'If gBranchType <> "hotfix" Then
        '  PatchTabControl.TabPages.Remove(TabPageSuperBy)
        'End If

        'PatchTabControl.TabPages("TabPagePatchDefn").Hide()
        'PatchTabControl.TabPages(2).Hide()

        'PatchTabControl.TabPages.TabPagePatchDefn Remove(TabPageSuperBy)

        ExecuteButton.Text = "Execute Patch on " & Globals.currentTNS

    End Sub

    Private Sub HideTabs()
        PatchTabControl.TabPages.Remove(TabPageExtras)
        PatchTabControl.TabPages.Remove(TabPagePreReqs)
        PatchTabControl.TabPages.Remove(TabPageSuper)
        PatchTabControl.TabPages.Remove(TabPagePatchDefn)
        FindButton.Visible = False
        CopyChangesButton.Visible = False

    End Sub
    Private Sub ShowTabs()
        PatchTabControl.TabPages.Insert(2, TabPageExtras)
        PatchTabControl.TabPages.Insert(3, TabPagePreReqs)
        PatchTabControl.TabPages.Insert(4, TabPageSuper)
        PatchTabControl.TabPages.Insert(5, TabPagePatchDefn)
    End Sub


    Private Sub Findtags()

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim tag_no_padding As Integer = 2

        TagsCheckedListBox.Items.Clear()
        For Each tagname In GitSharpFascade.getTagList(Globals.currentRepo)
            If Common.getFirstSegment(tagname, ".") = Globals.currentBranch Then
                'This is a tag worth listing
                Dim ticked As Boolean = (gtag_base = Common.getLastSegment(tagname, ".").Substring(0, tag_no_padding)) 'This is a tag worth ticking
                TagsCheckedListBox.Items.Add(tagname, ticked)

            End If
        Next

        Cursor.Current = cursorRevert

    End Sub

    Private Sub FindChanges()
        Try
            If SchemaComboBox.Text = "" Then
                Throw (New Halt("Schema not selected"))
            End If



            TreeViewChanges.PathSeparator = "/"
            TreeViewChanges.Nodes.Clear()


            For Each change In GitSharpFascade.getTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, Globals.DBRepoPathMask & SchemaComboBox.Text, False)

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


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindChanges()


    End Sub


    Private Sub exportExtraFiles(ByRef extrasCollection As Collection, ByRef filenames As Collection, ByVal patch_dir As String)

        Dim Filename As String = Nothing
        If extrasCollection.Count > 0 Then
            For Each FilePath In extrasCollection
                If InStr(FilePath, Globals.DBRepoPathMask) = 0 Then
                    'Screened out repo files
                    Filename = Common.getLastSegment(FilePath, "\")
                    Try
                        My.Computer.FileSystem.CopyFile(FilePath, patch_dir & "\" & Filename, True)

                        filenames.Add(Filename)
                    Catch ex As Exception
                        MsgBox("Warning: File " & Filename & " could not be exported, but will be in the install file.  It may be a folder.  Deselect, then recreate Patch.")

                    End Try
                End If

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
        filenames = GitSharpFascade.exportTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, Globals.DBRepoPathMask & SchemaComboBox.Text, changesFiles, PatchDirTextBox.Text)

 
        'Additional file exports 
        Dim ExtraFiles As Collection = New Collection
        'Extra files
        'Retrieve checked node items from the TreeViewFiles as a collection of files.
        TreeViewFiles.ReadCheckedLeafNodes(ExtraFiles)


        exportExtraFiles(ExtraFiles, filenames, PatchDirTextBox.Text)

        Dim PreReqPatches As Collection = New Collection
        'Retrieve checked node items from the PreReqPatchesTreeView as a collection of patches.
        PreReqPatchesTreeView.ReadCheckedLeafNodes(PreReqPatches)


        Dim SuperPatches As Collection = New Collection

        'Retrieve checked node items from the SuperPatchesTreeView as a collection of patches.
        SuperPatchesTreeView.ReadCheckedLeafNodes(SuperPatches)

        Dim SuperByPatches As Collection = New Collection


        'Retrieve checked node items from the SuperByPatchesTreeView as a collection of patches.
        SuperByPatchesTreeView.ReadCheckedLeafNodes(SuperByPatches)
        Try
            Dim filelist As Collection = New Collection
            'Ok - no longer need the filenames list created by exportTagChanges and exportExtraFiles
            'Instead we will rederive this list from TreeViewPatchOrder
            TreeViewPatchOrder.ReadTags(filelist, False, False, False, False)


            Dim checkedFilelist As Collection = New Collection
            'List of files that have been ticked.
            TreeViewPatchOrder.ReadTags(checkedFilelist, False, True, False, True)


            'Write the install script - using patch admin
            writeInstallScript(PatchNameTextBox.Text, _
                               Common.getFirstSegment(Globals.currentLongBranch, "/"), _
                               SchemaComboBox.Text, _
                               Globals.currentLongBranch, _
                               Tag1TextBox.Text, _
                               Tag2TextBox.Text, _
                               SupIdTextBox.Text, _
                               PatchDescTextBox.Text, _
                               NoteTextBox.Text, _
                               True, _
                               RerunCheckBox.Checked, _
                               filelist, _
                               checkedFilelist, _
                               PreReqPatches, _
                               SuperPatches, _
                               SuperByPatches, _
                               PatchDirTextBox.Text, _
                               PatchPathTextBox.Text, _
                               TrackPromoCheckBox.Checked)
            'Write the install script lite - without patch admin
            writeInstallScript(PatchNameTextBox.Text, _
                               Common.getFirstSegment(Globals.currentLongBranch, "/"), _
                               SchemaComboBox.Text, _
                               Globals.currentLongBranch, _
                               Tag1TextBox.Text, _
                               Tag2TextBox.Text, _
                               SupIdTextBox.Text, _
                               PatchDescTextBox.Text, _
                               NoteTextBox.Text, _
                               False, _
                               RerunCheckBox.Checked, _
                               filelist, _
                               checkedFilelist, _
                               PreReqPatches, _
                               SuperPatches, _
                               SuperByPatches, _
                               PatchDirTextBox.Text, _
                               PatchPathTextBox.Text, _
                               TrackPromoCheckBox.Checked)


            Host.RunExplorer(PatchDirTextBox.Text)
        Catch ex As ArgumentException
            'MsgBox(ex.ToString)
            MsgBox("There are duplicated filenames in the patch.  You may have selected an Extra File that is already in the Patch.")

        End Try


    End Sub

    Private Sub deriveSchemas()
        If String.IsNullOrEmpty(SchemaComboBox.Text) Then
            SchemaComboBox.Items.Clear()


            For Each schema In GitSharpFascade.getSchemaList(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, Globals.DBRepoPathMask)
                SchemaComboBox.Items.Add(schema)
            Next

            SchemaCountTextBox.Text = SchemaComboBox.Items.Count

            'If exactly one schema found then select it
            'otherwise force user to choose one.
            If SchemaComboBox.Items.Count = 1 Then
                SchemaComboBox.SelectedIndex = 0
            Else
                Logger.Dbg("Multiple schemas")
            End If
        End If
    End Sub



    '  Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged
    '
    '      deriveSchemas()
    '
    '  End Sub
    '
    '  Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
    '      deriveSchemas()
    '  End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim CheckedChanges As Collection = New Collection

        'Retrieve checked node items from the TreeViewChanges as a collection of changes.
        TreeViewChanges.ReadCheckedLeafNodes(CheckedChanges)


        MsgBox(GitSharpFascade.viewTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, CheckedChanges))
    End Sub




    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        derivePatchDir()
        'PatchDirTextBox.Text = Globals.RootPatchDir & Replace(PatchNameTextBox.Text, "/", "\") & "\"
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
                                  ByRef ignoreErrorFiles As Collection, _
                                  ByRef prereq_patches As Collection, _
                                  ByRef supersedes_patches As Collection, _
                                  ByRef superseded_by_patches As Collection, _
                                  ByVal patchDir As String, _
                                  ByVal groupPath As String, _
                                  ByVal track_promotion As Boolean)



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



        Dim l_patch_started As String = Nothing

        Dim l_install_list As String = Nothing
        Dim l_post_install_list As String = Nothing

        Dim Category As String = Nothing

   

        If targetFiles.Count > 0 Then

            Dim l_log_filename As String = patch_name & ".log"
            Dim l_master_filename As String = Nothing

            If use_patch_admin Then
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

            If db_schema = "SYS" Then
                l_master_file.WriteLine("CONNECT &&SYS_user/&&SYS_password@&&database as sysdba")
            Else
                l_master_file.WriteLine("CONNECT &&" & db_schema & "_user/&&" & db_schema & "_password@&&database")
            End If


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
                        l_install_file_line = Chr(10) & "WHENEVER SQLERROR CONTINUE" & _
                                              Chr(10) & "PROMPT " & l_filename & " " & _
                                              Chr(10) & unix_path("@" & branch_path & "\" & patch_name & "\" & l_filename & ";") & _
                                              Chr(10) & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK"

                    Else
                        l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " & _
                                              Chr(10) & unix_path("@" & branch_path & "\" & patch_name & "\" & l_filename & ";")

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



            If use_patch_admin Then

                l_patch_started = _
                    "execute &&PATCH_ADMIN_user..patch_installer.patch_started( -" _
        & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
        & Chr(10) & " ,i_patch_type         => '" & patch_type & "' -" _
        & Chr(10) & " ,i_db_schema          => '" & "&&" & db_schema & "_user" & "' -" _
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
                        l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -")
                        l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                        l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")
                    End If

                Next

                l_prereq_short_name = My.Settings.MinPatch
                l_master_file.WriteLine("PROMPT Ensure Patch Admin is late enough for this patch")
                l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -")
                l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")

            End If

            l_master_file.WriteLine("select user||'@'||global_name Connection from global_name;")

            'Write the list of files to execute.
            l_master_file.WriteLine(l_install_list)

            l_master_file.WriteLine(Chr(10) & "COMMIT;")

            If use_patch_admin Then

                l_master_file.WriteLine("PROMPT Compiling objects in schema " & db_schema)
                l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;")

                If db_schema = "PATCH_ADMIN" Then
                    l_master_file.WriteLine("--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.")
                    l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => '" & patch_name & "');")
                Else
                    l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.patch_completed;")
                End If

                Dim l_sup_short_name As String = Nothing
                For Each l_sup_patch In supersedes_patches
                    l_sup_short_name = Common.getLastSegment(l_sup_patch, "\")
                    If l_sup_short_name = PatchNameTextBox.Text Then
                        MsgBox("A Patch may NOT supersede itself, skipping Supersedes Patch: " & l_sup_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
                    Else
                        l_master_file.WriteLine("PROMPT")
                        l_master_file.WriteLine("PROMPT Supersedes patch " & l_sup_short_name)
                        l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_supersedes( -")
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
                        l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_supersedes( -")
                        l_master_file.WriteLine("i_patch_name     => '" & l_sup_short_name & "' -")
                        l_master_file.WriteLine(",i_supersedes_patch  => '" & patch_name & "' );")
                    End If
                Next

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
            log = GitSharpFascade.TagLog(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text)

            target.Text = ChoiceDialog.Ask("You may choose a log message for the " & targetControlName, log, "", "Choose log message", False)

        Catch noneChosen As Halt
            target.Text = l_old_text
        End Try

    End Sub



    Private Sub derivePatchName()

        If Not String.IsNullOrEmpty(Tag1TextBox.Text) And Not String.IsNullOrEmpty(Tag2TextBox.Text) And SchemaComboBox.Items.Count > 0 Then

            PatchNameTextBox.Text = Globals.currentBranch & "_" & Common.dropFirstSegment(Tag1TextBox.Text, ".") & "_" & Common.dropFirstSegment(Tag2TextBox.Text, ".") & "_" & SchemaComboBox.SelectedItem.ToString

            If Not String.IsNullOrEmpty(SupIdTextBox.Text.Trim) Then
                PatchNameTextBox.Text = PatchNameTextBox.Text & "_" & SupIdTextBox.Text

            End If
        Else
            MsgBox("Please select two tags, then review changes ensuring you select a schema, to allow derivation of PatchName")

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


    Shared Sub FindPatches(ByRef foundPatches As TreeViewEnhanced.TreeViewEnhanced, ByVal restrictToBranch As Boolean, Optional ByVal patchType As String = "ALL")

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim searchPath As String = Nothing
        If patchType <> "ALL" Then
            searchPath = patchType & "\"
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
        FindPatches(PreReqPatchesTreeView, RestrictPreReqToBranchCheckBox.Checked)

    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub

    Private Sub FindSuper()
        FindPatches(SuperPatchesTreeView, RestrictSupToBranchCheckBox.Checked)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles SupButton.Click
        FindSuper()
    End Sub

    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecuteButton.Click
        'Use patch runner to execute with a master script.
        Dim l_master_filename As String = Nothing

        If UsePatchAdminCheckBox.Checked Then
            l_master_filename = "install.sql"
        Else
            l_master_filename = "install_lite.sql"
        End If
 
        PatchRunner.RunMasterScript("DEFINE database = '" & Globals.currentTNS & "'" & Chr(10) & "@" & PatchPathTextBox.Text & PatchNameTextBox.Text & "\" & l_master_filename)

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
            l_tag_base = Main.rebaseBranch(iBranchType, iRebaseBranchOn)

        End If


        If createPatchProgress.toDoNextStep() Then
            'Review tags on the branch
            Tortoise.Log(Globals.currentRepo)
        End If


        If createPatchProgress.toDoNextStep() Then

            Dim Wizard As New PatchFromTags(iBranchType, iDBtarget, iRebaseBranchOn, l_tag_base)
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
        FindPatches(SuperByPatchesTreeView, RestrictSupByToBranchCheckBox.Checked)
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

    Private Sub FindExtras()
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



    Private Sub ButtonFindFiles_Click(sender As Object, e As EventArgs) Handles ButtonFindFiles.Click
        FindExtras()
 

    End Sub


    Private Sub ButtonLastPatch_Click(sender As Object, e As EventArgs) Handles ButtonLastPatch.Click

        Dim ChosenChanges As Collection = New Collection
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        TreeViewChanges.ReadCheckedLeafNodes(ChosenChanges)

        'Requery ALL patches
        RestrictPreReqToBranchCheckBox.Checked = False
        FindPreReqs()


        For Each change In ChosenChanges
            Dim patch_component As String = Common.getLastSegment(change.ToString(), "/")
            Dim LastPatch As String = PatchRunner.FindLastPatch(patch_component)
            If String.IsNullOrEmpty(LastPatch) Then
                Logger.Dbg("No previous patch for Change: " & patch_component)
            Else
                'MsgBox("Change: " & patch_component & " LastPatch: " & LastPatch)
                Dim l_found As Boolean = False
                PreReqPatchesTreeView.TickNode(LastPatch, l_found)

                If Not l_found Then
                    MsgBox("Unable to find patch: " & LastPatch & " for Change: " & patch_component)
                End If

            End If



        Next

        'Set Prereq tree to Contract view.
        PreReqPatchesTreeView.showCheckedNodes()

    End Sub


    Private Sub ButtonPopDesc_Click(sender As Object, e As EventArgs) Handles ButtonPopDesc.Click
        PopDesc(PatchDescTextBox, "Patch Description")
    End Sub

    Private Sub ButtonPopNotes_Click(sender As Object, e As EventArgs) Handles ButtonPopNotes.Click
        PopDesc(NoteTextBox, "Notes")
    End Sub

    Private Sub TagsCheckedListBox_Click(sender As Object, e As EventArgs) Handles TagsCheckedListBox.Click
        'If tags are changed then we will clear the selected changes and the schema list.
        SchemaComboBox.Text = ""
        TreeViewChanges.Nodes.Clear()
        HideTabs()
    End Sub

    Private Sub ResetForNewPatch()

        TreeViewFiles.Nodes.Clear()
        PreReqPatchesTreeView.Nodes.Clear()
        SuperPatchesTreeView.Nodes.Clear()
        SuperByPatchesTreeView.Nodes.Clear()
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



    Private Sub writeUnInstallScript(ByVal patch_name As String, _
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
                                  ByRef ignoreErrorFiles As Collection, _
                                  ByVal patchDir As String, _
                                  ByVal groupPath As String, _
                                  ByVal track_promotion As Boolean)



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



        Dim l_patch_started As String = Nothing

        Dim l_install_list As String = Nothing
        Dim l_post_install_list As String = Nothing

        Dim Category As String = Nothing



        If targetFiles.Count > 0 Then

            Dim l_log_filename As String = patch_name & "_uninstall.log"
            Dim l_master_filename As String = Nothing

            If use_patch_admin Then
                l_master_filename = "uninstall.sql"
            Else
                l_master_filename = "uninstall_lite.sql"
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

            If db_schema = "SYS" Then
                l_master_file.WriteLine("CONNECT &&SYS_user/&&SYS_password@&&database as sysdba")
            Else
                l_master_file.WriteLine("CONNECT &&" & db_schema & "_user/&&" & db_schema & "_password@&&database")
            End If


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
                        l_install_file_line = Chr(10) & "WHENEVER SQLERROR CONTINUE" & _
                                              Chr(10) & "PROMPT " & l_filename & " " & _
                                              Chr(10) & unix_path("@" & branch_path & "\" & patch_name & "\" & l_filename & ";") & _
                                              Chr(10) & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK"

                    Else
                        l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " & _
                                              Chr(10) & unix_path("@" & branch_path & "\" & patch_name & "\" & l_filename & ";")

                    End If
 


                    If String.IsNullOrEmpty(l_all_programs) Then
                        l_all_programs = l_filename
                    Else
                        l_all_programs = l_all_programs & "' -" & Chr(10) & "||'," & l_filename
                    End If


                End If

                'Add this file to the normal list
                l_install_list = l_install_list & Chr(10) & l_install_file_line


            Next



            '    If use_patch_admin Then

            '        l_patch_started = _
            '            "execute &&PATCH_ADMIN_user..patch_installer.patch_started( -" _
            '& Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
            '& Chr(10) & " ,i_patch_type         => '" & patch_type & "' -" _
            '& Chr(10) & " ,i_db_schema          => '" & "&&" & db_schema & "_user" & "' -" _
            '& Chr(10) & " ,i_branch_name        => '" & branch_path & "' -" _
            '& Chr(10) & " ,i_tag_from           => '" & tag1_name & "' -" _
            '& Chr(10) & " ,i_tag_to             => '" & tag2_name & "' -" _
            '& Chr(10) & " ,i_supplementary      => '" & supplementary & "' -" _
            '& Chr(10) & " ,i_patch_desc         => '" & patch_desc.Replace("'", "''") & "' -" _
            '& Chr(10) & " ,i_patch_componants   => '" & l_all_programs & "' -" _
            '& Chr(10) & " ,i_patch_create_date  => '" & DateString & "' -" _
            '& Chr(10) & " ,i_patch_created_by   => '" & Environment.UserName & "' -" _
            '& Chr(10) & " ,i_note               => '" & note.Replace("'", "''") & "' -" _
            '& Chr(10) & " ,i_rerunnable_yn      => '" & rerunnable_yn & "' -" _
            '& Chr(10) & " ,i_remove_prereqs     => 'N' -" _
            '& Chr(10) & " ,i_remove_sups        => 'N' -" _
            '& Chr(10) & " ,i_track_promotion    => '" & track_promotion_yn & "'); " _
            '& Chr(10)


            '        l_master_file.WriteLine(l_patch_started)


            '        Dim l_prereq_short_name As String = Nothing
            '        For Each l_prereq_patch In prereq_patches
            '            l_prereq_short_name = Common.getLastSegment(l_prereq_patch, "\")
            '            If l_prereq_short_name = PatchNameTextBox.Text Then
            '                MsgBox("A Patch may NOT have itself as a prerequisite, skipping Prerequisite Patch: " & l_prereq_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
            '            Else
            '                l_master_file.WriteLine("PROMPT")
            '                l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_short_name)
            '                l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -")
            '                l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
            '                l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")
            '            End If

            '        Next

            '        l_prereq_short_name = My.Settings.MinPatch
            '        l_master_file.WriteLine("PROMPT Ensure Patch Admin is late enough for this patch")
            '        l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -")
            '        l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
            '        l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")

            '    End If

            l_master_file.WriteLine("select user||'@'||global_name Connection from global_name;")

            'Write the list of files to execute.
            l_master_file.WriteLine(l_install_list)

            l_master_file.WriteLine(Chr(10) & "COMMIT;")

            'If use_patch_admin Then

            '    l_master_file.WriteLine("PROMPT Compiling objects in schema " & db_schema)
            '    l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;")

            '    If db_schema = "PATCH_ADMIN" Then
            '        l_master_file.WriteLine("--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.")
            '        l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => '" & patch_name & "');")
            '    Else
            '        l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.patch_completed;")
            '    End If

            '    Dim l_sup_short_name As String = Nothing
            '    For Each l_sup_patch In supersedes_patches
            '        l_sup_short_name = Common.getLastSegment(l_sup_patch, "\")
            '        If l_sup_short_name = PatchNameTextBox.Text Then
            '            MsgBox("A Patch may NOT supersede itself, skipping Supersedes Patch: " & l_sup_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
            '        Else
            '            l_master_file.WriteLine("PROMPT")
            '            l_master_file.WriteLine("PROMPT Supersedes patch " & l_sup_short_name)
            '            l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_supersedes( -")
            '            l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
            '            l_master_file.WriteLine(",i_supersedes_patch  => '" & l_sup_short_name & "' );")
            '        End If
            '    Next

            '    For Each l_sup_patch In superseded_by_patches
            '        l_sup_short_name = Common.getLastSegment(l_sup_patch, "\")
            '        If l_sup_short_name = PatchNameTextBox.Text Then
            '            MsgBox("A Patch may NOT be superseded by itself, skipping Superseded By Patch: " & l_sup_short_name, MsgBoxStyle.Exclamation, "Illegal Patch Dependency")
            '        Else

            '            l_master_file.WriteLine("PROMPT")
            '            l_master_file.WriteLine("PROMPT Superseded by patch " & l_sup_short_name)
            '            l_master_file.WriteLine("execute &&PATCH_ADMIN_user..patch_installer.add_patch_supersedes( -")
            '            l_master_file.WriteLine("i_patch_name     => '" & l_sup_short_name & "' -")
            '            l_master_file.WriteLine(",i_supersedes_patch  => '" & patch_name & "' );")
            '        End If
            '    Next

            'End If

            l_master_file.WriteLine("COMMIT;")

            l_master_file.WriteLine("PROMPT ")
            l_master_file.WriteLine("PROMPT " & l_master_filename & " - COMPLETED.")

            l_master_file.WriteLine("spool off;")



            l_master_file.Close()

            'Convert the file to unix
            FileIO.FileDOStoUNIX(patchDir & "\" & l_master_filename)

        End If


    End Sub



    Private Sub AddUninstallButton_Click(sender As Object, e As EventArgs) Handles AddUninstallButton.Click
        'Find .del files and .drop files and call them from a new uninstall.sql

        Dim ignoreErrorFiles As Collection = New Collection
        'Will ignore errors form the Drop files
        ignoreErrorFiles = FileIO.FileList(PatchDirTextBox.Text, "*.drop", PatchDirTextBox.Text, True)

        'Common.listCollection(ignoreErrorFiles, "ignore files")

        'List the *.drop files 3 times, they will run 3 times.
        Dim uninstallFiles As Collection = New Collection

        uninstallFiles = FileIO.FileList(PatchDirTextBox.Text, "*.drop", PatchDirTextBox.Text)
        FileIO.AppendFileList(PatchDirTextBox.Text, "*.drop", PatchDirTextBox.Text, uninstallFiles)
        FileIO.AppendFileList(PatchDirTextBox.Text, "*.drop", PatchDirTextBox.Text, uninstallFiles)
 
        'List the del files once.
        FileIO.AppendFileList(PatchDirTextBox.Text, "*.del", PatchDirTextBox.Text, uninstallFiles)
  
        'Common.listCollection(uninstallFiles, "del files")
   
        'Write uninstall.sql

 
        'Write the uninstall script lite - without patch admin
        writeUnInstallScript(PatchNameTextBox.Text, _
                           Common.getFirstSegment(Globals.currentLongBranch, "/"), _
                           SchemaComboBox.Text, _
                           Globals.currentLongBranch, _
                           Tag1TextBox.Text, _
                           Tag2TextBox.Text, _
                           SupIdTextBox.Text, _
                           PatchDescTextBox.Text, _
                           NoteTextBox.Text, _
                           False, _
                           RerunCheckBox.Checked, _
                           uninstallFiles, _
                           ignoreErrorFiles, _
                           PatchDirTextBox.Text, _
                           PatchPathTextBox.Text, _
                           TrackPromoCheckBox.Checked)





    End Sub
End Class