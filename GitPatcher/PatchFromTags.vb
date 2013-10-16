
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
                'GPTrees.AddNode(TreeViewChanges.Nodes, change, change, "/", True)

                'TreeViewEnhanced.TreeViewEnhanced.AddNode(TreeViewChanges.Nodes, change, change, "/", True)
                TreeViewChanges.AddNode(change, "/", True)
 
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
                If Common.getFirstSegment(FilePath, "/") <> "database" Then
                    'Screened out repo files
                    Filename = Common.getLastSegment(FilePath, "\")

                    My.Computer.FileSystem.CopyFile(FilePath, patch_dir & "\" & Filename, True)

                    filenames.Add(Filename)
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

        'Get a list of patchable files (filepaths) from the TreeViewPatchOrder to send to exportTagChanges and exportExtraFiles
        Dim patchableFiles As Collection = New Collection
        'GPTrees.ReadTags2Level(TreeViewPatchOrder.Nodes, patchableFiles, False, True, True, False)
        TreeViewPatchOrder.ReadTags(patchableFiles, False, True, True, False)

        Dim filenames As Collection = Nothing
        filenames = GitSharpFascade.exportTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, patchableFiles, PatchDirTextBox.Text)

        'Additional file exports 

        ' Dim ExtraFiles As Collection = New Collection
        ' 'Retrieve checked node items from the TreeViewFiles as a collection of files.
        ' GPTrees.ReadCheckedNodes(TreeViewFiles.TopNode, ExtraFiles, True)

        exportExtraFiles(patchableFiles, filenames, PatchDirTextBox.Text)

        'Add to filenames too


        Dim PreReqPatches As Collection = New Collection

        'Retrieve checked node items from the PreReqPatchesTreeView as a collection of patches.
        'GPTrees.ReadCheckedLeafNodes(PreReqPatchesTreeView.Nodes, PreReqPatches)
        PreReqPatchesTreeView.ReadCheckedLeafNodes(PreReqPatches)


        Dim SuperPatches As Collection = New Collection

        'Retrieve checked node items from the SuperPatchesTreeView as a collection of patches.
        'GPTrees.ReadCheckedLeafNodes(SuperPatchesTreeView.Nodes, SuperPatches)
        SuperPatchesTreeView.ReadCheckedLeafNodes(SuperPatches)

        Dim SuperByPatches As Collection = New Collection


        'Retrieve checked node items from the SuperByPatchesTreeView as a collection of patches.
        'GPTrees.ReadCheckedLeafNodes(SuperByPatchesTreeView.Nodes, SuperByPatches)
        SuperByPatchesTreeView.ReadCheckedLeafNodes(SuperByPatches)

        Dim filelist As Collection = New Collection
        'Ok - no longer need the filenames list created by exportTagChanges and exportExtraFiles
        'Instead we will rederive this list from TreeViewPatchOrder
        'GPTrees.ReadTags2Level(TreeViewPatchOrder.Nodes, filelist, False, False, False, False)
        TreeViewPatchOrder.ReadTags(filelist, False, False, False, False)


        Dim checkedFilelist As Collection = New Collection
        'Ok - no longer need the filenames list created by exportTagChanges and exportExtraFiles
        'Instead we will rederive this list from TreeViewPatchOrder
        'GPTrees.ReadTags2Level(TreeViewPatchOrder.Nodes, checkedFilelist, False, True, False, True)
        TreeViewPatchOrder.ReadTags(checkedFilelist, False, True, False, True)


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
                           filelist, _
                           checkedFilelist, _
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



    Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged

        deriveSchemas()

    End Sub

    Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
        deriveSchemas()
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click

        Dim CheckedChanges As Collection = New Collection

        'Retrieve checked node items from the TreeViewChanges as a collection of changes.
        'GPTrees.ReadCheckedLeafNodes(TreeViewChanges.Nodes, CheckedChanges)
        TreeViewChanges.ReadCheckedLeafNodes(CheckedChanges)


        MsgBox(GitSharpFascade.viewTagChanges(Globals.currentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, CheckedChanges))
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



            'Files have already been sorted into Categories, we only need to list the categories and spit out the files under each.
            For Each l_filename In targetFiles

                'Dim l_filename As String = Common.getLastSegment(l_path, "/")

                'Sort the files by files extention into lists.

         

                If Not l_filename.contains(".") Then
                    'No file extension so assume this is a Category heading.
                    Dim Category As String = l_filename.ToUpper
                    l_install_file_line = Chr(10) & "PROMPT " & Category


                Else

                    l_file_extension = l_filename.Split(".")(1)

                    'This is a releaseable file, so put an entry in the script.
                    If ignoreErrorFiles.Contains(l_filename) Then
                        l_install_file_line = Chr(10) & "WHENEVER SQLERROR CONTINUE" & _
                                              Chr(10) & "PROMPT " & l_filename & " " & _
                                              Chr(10) & "@" & groupPath & patch_name & "\" & l_filename & ";" & _
                                              Chr(10) & "WHENEVER SQLERROR EXIT FAILURE ROLLBACK"

                    Else
                        l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " & _
                                              Chr(10) & "@" & groupPath & patch_name & "\" & l_filename & ";"

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


                l_install_list = l_install_list & Chr(10) & l_install_file_line

            Next



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
            l_master_file.WriteLine(l_install_list)

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

        'Copy to the new Patchable Tree
        TreeViewPatchOrder.PathSeparator = "#"
        TreeViewPatchOrder.Nodes.Clear()

        'Prepopulate Tree with default category nodes.
        'This should become a method on TreeViewDraggableNodes2Levels
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


        Dim ChosenChanges As Collection = New Collection
        'Repo changes
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        'GPTrees.ReadCheckedLeafNodes(TreeViewChanges.Nodes, ChosenChanges)
        TreeViewChanges.ReadCheckedLeafNodes(ChosenChanges)

        'Extra files
        'Retrieve checked node items from the TreeViewFiles as a collection of files.
        'GPTrees.ReadCheckedLeafNodes(TreeViewFiles.Nodes, ChosenChanges)
        TreeViewFiles.ReadCheckedLeafNodes(ChosenChanges)


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
                Case "pks", "pls"
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
                Case "pkb", "plb"
                    l_category = "Package Bodies"
                Case "trg"
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
                Case Else
                    l_category = "Miscellaneous"
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
            'GPTrees.AddFileToCategory(TreeViewPatchOrder.Nodes, l_category, l_label, change) 'This should become a method on TreeViewDraggableNodes2Levels
            TreeViewPatchOrder.AddFileToCategory(l_category, l_label, change)
        Next

        'GPTrees.RemoveChildlessNodes2Levels(TreeViewPatchOrder.Nodes) 'This should become a method on TreeViewDraggableNodes2Levels

        'TreeViewPatchOrder.RemoveChildlessLevel1Nodes(TreeViewPatchOrder.Nodes)

        TreeViewPatchOrder.RemoveChildlessLevel1Nodes()

        'Set tree to expanded.
        TreeViewPatchOrder.ExpandAll()

        'GPTrees.PrependCategory(TreeViewPatchOrder.Nodes, "Initialise")
        TreeViewPatchOrder.PrependCategory("Initialise")
        'GPTrees.AddCategory(TreeViewPatchOrder.Nodes, "Finalise")
        TreeViewPatchOrder.AddCategory("Finalise")

    End Sub





    Private Sub PatchTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchTabControl.SelectedIndexChanged

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

            If TreeViewPatchOrder.Nodes.Count = 0 Then
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


    Shared Sub FindPatches(ByRef foundPatches As TreeViewEnhanced.TreeViewEnhanced, ByVal restrictToBranch As Boolean, ByRef sender As Object, Optional ByVal patchType As String = "ALL")


        Dim searchPath As String = Nothing
        If patchType <> "ALL" Then
            searchPath = patchType & "\"
        End If


        Dim lfoundPatches As Collection = New Collection

        sender.text = "Expand"

        If IO.Directory.Exists(Globals.RootPatchDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir & searchPath, "install.sql", lfoundPatches, Globals.RootPatchDir)

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


        'GPTrees.populateTreeFromCollection(foundPatches, lfoundPatches)

        foundPatches.populateTreeFromCollection(lfoundPatches)


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


    'Shared Sub PreReqPatchesTreeView_node_AfterCheck(sender As Object, e As TreeViewEventArgs)
    '
    '    GPTrees.CheckChildNodes(e.Node, e.Node.Checked)
    '
    'End Sub
    '
    'Shared Sub SuperPatchesTreeView_node_AfterCheck(sender As Object, e As TreeViewEventArgs)
    '
    '    GPTrees.CheckChildNodes(e.Node, e.Node.Checked)
    '
    'End Sub
    'Shared Sub SuperByPatchesTreeView_node_AfterCheck(sender As Object, e As TreeViewEventArgs)
    '
    '    GPTrees.CheckChildNodes(e.Node, e.Node.Checked)
    '
    'End Sub
    'Shared Sub TreeViewChanges_node_AfterCheck(sender As Object, e As TreeViewEventArgs)
    '
    '    GPTrees.CheckChildNodes(e.Node, e.Node.Checked)
    '
    'End Sub
    '
    'Shared Sub TreeViewFiles_node_AfterCheck(sender As Object, e As TreeViewEventArgs)
    '
    '    GPTrees.CheckChildNodes(e.Node, e.Node.Checked)
    '
    'End Sub



    Private Sub ButtonLastPatch_Click(sender As Object, e As EventArgs) Handles ButtonLastPatch.Click

        Dim ChosenChanges As Collection = New Collection
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        'GPTrees.ReadCheckedLeafNodes(TreeViewChanges.Nodes, ChosenChanges)
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
                'GPTrees.TickNode(PreReqPatchesTreeView.Nodes, LastPatch, l_found)

                PreReqPatchesTreeView.TickNode(LastPatch, l_found)

                If Not l_found Then
                    MsgBox("Unable to find patch: " & LastPatch & " for Change: " & patch_component)
                End If

            End If



        Next

        'Set Prereq tree to Contract view.
        ButtonTreeChangePrereq.Text = "Contract"

        GPTrees.treeChange_Click(ButtonTreeChangePrereq, PreReqPatchesTreeView)

    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveButton.Click

        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(TreeViewChanges.Nodes, True)

    End Sub
 
    Private Sub ButtonCropTo_Click(sender As Object, e As EventArgs) Handles ButtonCropTo.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(TreeViewChanges.Nodes, False)
    End Sub
 
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(TreeViewFiles.Nodes, True)
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(TreeViewFiles.Nodes, False)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PreReqPatchesTreeView.RemoveNodes(True)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PreReqPatchesTreeView.RemoveNodes(False)
    End Sub
 
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(SuperPatchesTreeView.Nodes, True)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(SuperPatchesTreeView.Nodes, False)
    End Sub
 
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(SuperByPatchesTreeView.Nodes, True)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TreeViewEnhanced.TreeViewEnhanced.RemoveNodes(SuperByPatchesTreeView.Nodes, False)
    End Sub
End Class