Imports LibGit2Sharp
Public Class CreateRelease
    Private pPatchName As String = Nothing
    Private pCreatePatchType As String = Nothing
    Private pFindPatchTypes As String = Nothing
    Private pFindPatchFilters As String = Nothing
    Private pPrereqPatchTypes As String = Nothing
    Private pSupPatchTypes As String = Nothing

    Dim AvailablePatches As Collection = New Collection

    Private waiting As Boolean

    Public Sub New(ByVal iPatchName As String, ByVal iCreatePatchType As String, ByVal iFindPatchTypes As String, ByVal iFindPatchFilters As String, ByVal iPrereqPatchTypes As String, ByVal iSupPatchTypes As String)

        If String.IsNullOrEmpty(iPatchName) Then
            Dim l_app_version = InputBox("Please enter a new version for " & Globals.currentAppCode & " in the format: 2.17.01", "New " & Globals.getAppName & " Version")

            pPatchName = Globals.currentAppCode & "-" & l_app_version
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



        FindTagsButton.Text = "Find Tags like " & Globals.currentAppCode & "-X.XX.XX"

        Findtags()

        TagFilterCheckBox.Checked = True
        ComboBoxPatchesFilter.SelectedItem = "All" '"Unapplied"

        Me.Text = "CreatePatchSet - Creating a " & iCreatePatchType & " for " & Globals.currentTNS

        AvailablePatchesLabel.Text = "Available" & Chr(10) & iFindPatchTypes & Chr(10) & "Patches"

        Me.MdiParent = GitPatcher
        Me.Show()
        Wait()

    End Sub

    Private Sub Wait()
        'Wait until the form is closed.
        waiting = True
        Do Until Not waiting
            Common.Wait()
        Loop
    End Sub

    Private Sub CreateRelease_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        waiting = False
    End Sub

    Private Sub Findtags()

        Dim tagsearch As String = Replace(RTrim(Globals.currentAppCode), Chr(13), "")
        Dim tagseg As String = Nothing

        TagsCheckedListBox.Items.Clear()
        For Each thisTag As Tag In GitOp.getTagList()
            tagseg = Common.getFirstSegment(thisTag.FriendlyName, "-")
            'Looks like this used to search for tags by appCode, but not doing this ATM.
            'If tagseg = tagsearch Then
            TagsCheckedListBox.Items.Add(thisTag.FriendlyName)
            'End If
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


    Private Sub FindPatches()

        'Dim AvailablePatches As Collection = New Collection
        Dim taggedPatches As Collection = New Collection

        'First use the Filter box to find available patches from the file system and with reference to the current DB.
        If ComboBoxPatchesFilter.SelectedItem = "Unapplied" Then
            PatchRunner.FindUnappliedPatches(AvailablePatches)
        Else
            PatchRunner.FindPatches(AvailablePatches, ComboBoxPatchesFilter.SelectedItem = "Uninstalled")
        End If

        Cursor.Current = Cursors.WaitCursor
        'Next get a list of patches between the 2 tags and filter the orginal list by this.
        If String.IsNullOrEmpty(Tag1TextBox.Text) Or String.IsNullOrEmpty(Tag2TextBox.Text) Then
            TagFilterCheckBox.Checked = False
        End If

        GitOp.setCommitsFromTags(Tag1TextBox.Text, Tag2TextBox.Text)

        If TagFilterCheckBox.Checked Then

            'Find patches between 2 tags
            For Each change In GitOp.getChanges("patch/", False)
                'Apply Filters
                If change.contains("install.sql") And Common.stringContainsSetMember(change, pFindPatchTypes, ",") And Common.stringContainsSetMember(change, pFindPatchFilters, ",") Then

                    taggedPatches.Add(Replace(Common.dropLastSegment(Common.dropFirstSegment(change, "/"), "/"), "/", "\"))

                End If

            Next


        End If

        Dim patchMatch As Boolean = False
        Dim patchTagged As Boolean = True

        'process in reverse order, because removing items from the list, changes the indexes.  reverse order will not be affected.
        For i As Integer = AvailablePatches.Count To 1 Step -1
            Dim availablePatch As String = AvailablePatches(i)
            'Check patch matches filter
            patchMatch = False

            If (String.IsNullOrEmpty(pFindPatchTypes) Or Common.stringContainsSetMember(availablePatch, pFindPatchTypes, ",")) And
                (Not Me.AppFilterCheckBox.Checked Or Common.stringContainsSetMember(availablePatch, pFindPatchFilters, ",")) Then
                patchMatch = True
            End If

            If TagFilterCheckBox.Checked Then
                patchTagged = False

                For Each change In taggedPatches
                    If change = availablePatch Then
                        patchTagged = True
                    End If
                Next
            End If


            If Not patchMatch Or Not patchTagged Then
                'patch is to be filtered from the list.
                AvailablePatches.Remove(i)

            End If

        Next

        'Populate the treeview, tick unapplied by default
        AvailablePatchesTreeView.populateTreeFromCollection(AvailablePatches, ComboBoxPatchesFilter.SelectedItem = "Unapplied")

        Cursor.Current = Cursors.Default

        If AvailablePatches.Count = 0 Then
            MsgBox("No " & pFindPatchTypes & " patches are " & ComboBoxPatchesFilter.SelectedItem & " to " & Globals.getDB)

        End If



    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click

        'Create Patch Dir
        Try
            FileIO.confirmDeleteFolder(PatchDirTextBox.Text)
        Catch ex As Exception
            MsgBox("Warning: Now overwriting existing patch")
        End Try

        Dim l_create_folder As String = Globals.RootPatchDir
        For Each folder In Globals.currentLongBranch.Split("/")
            If String.IsNullOrEmpty(l_create_folder) Then
                l_create_folder = folder
            Else
                l_create_folder = l_create_folder & "\" & folder
            End If

            FileIO.createFolderIfNotExists(l_create_folder)
        Next

        FileIO.createFolderIfNotExists(PatchDirTextBox.Text)

        Dim patchableFiles As Collection = New Collection
        TreeViewPatchOrder.ReadTags(patchableFiles, False, True, True, False)

        Dim skipFiles As Collection = New Collection
        TreeViewPatchOrder.ReadTags(skipFiles, False, True, True, True)


        Dim PreReqPatches As Collection = New Collection
        'Retrieve checked node items from the PreReqPatchesTreeView as a collection of patches.
        PreReqPatchesTreeView.ReadCheckedLeafNodes(PreReqPatches)



        'Write the install script with Patch Admin
        writeInstallScript(PatchNameTextBox.Text,
                           pCreatePatchType,
                           "APEXRM",
                           Globals.currentLongBranch,
                           Tag1TextBox.Text,
                           Tag2TextBox.Text,
                           SupIdTextBox.Text,
                           PatchDescTextBox.Text,
                           NoteTextBox.Text,
                           True,
                           RerunCheckBox.Checked,
                           patchableFiles,
                           skipFiles,
                           PreReqPatches,
                           PatchDirTextBox.Text,
                           PatchPathTextBox.Text,
                           TrackPromoCheckBox.Checked,
                           True,
                           False)

        'Write the install_lite script without Patch Admin
        writeInstallScript(PatchNameTextBox.Text,
                           pCreatePatchType,
                           "APEXRM",
                           Globals.currentLongBranch,
                           Tag1TextBox.Text,
                           Tag2TextBox.Text,
                           SupIdTextBox.Text,
                           PatchDescTextBox.Text,
                           NoteTextBox.Text,
                           False,
                           RerunCheckBox.Checked,
                           patchableFiles,
                           skipFiles,
                           PreReqPatches,
                           PatchDirTextBox.Text,
                           PatchPathTextBox.Text,
                           TrackPromoCheckBox.Checked,
                           True,
                           False)

        Host.RunExplorer(PatchDirTextBox.Text)



    End Sub

    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged

        PatchDirTextBox.Text = Globals.RootPatchDir & Replace(PatchNameTextBox.Text, "/", "\") & "\"
    End Sub



    Shared Sub writeInstallScript(ByVal patch_name As String,
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
                                  ByRef iSkipFiles As Collection,
                                  ByRef prereq_patches As Collection,
                                  ByVal patchDir As String,
                                  ByVal groupPath As String,
                                  ByVal track_promotion As Boolean,
                                  ByVal alt_schema As Boolean,
                                  ByVal retired As Boolean)


        Dim l_master_filename As String = Nothing

        If use_arm Then
            l_master_filename = "install.sql"
        Else
            l_master_filename = "install_lite.sql"
        End If

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

        Dim track_promotion_yn As String = "N"
        If track_promotion Then
            track_promotion_yn = "Y"
        End If

        Dim alt_schema_yn As String = "N"
        If alt_schema Then
            alt_schema_yn = "Y"
        End If

        Dim retired_yn As String = "N"
        If retired Then
            retired_yn = "Y"
        End If



        For Each l_path In targetFiles

            Dim l_filename As String = Common.getLastSegment(l_path, "/")
            'Dim l_dos_path As String = Replace(l_path, "/", "\")

            If iSkipFiles.Contains(l_path) Then
                l_install_file_line = Chr(10) & "PROMPT SKIPPED FOR TESTING " & l_filename & " " &
                                      Chr(10) & "--@" & l_path & "\" & l_master_filename & ";"

            Else
                l_install_file_line = Chr(10) & "PROMPT " & l_filename & " " &
                                      Chr(10) & "@" & l_path & "\" & l_master_filename & ";"

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

            If use_arm Then

                'Always connects as PATCH_ADMIN
                l_master_file.WriteLine("CONNECT &&" & db_schema & "_user/&&" & db_schema & "_password@&&database")

                l_master_file.WriteLine("set serveroutput on;")


                l_master_file.WriteLine(
                "execute &&APEXRM_user..arm_installer.patch_started( -" _
    & Chr(10) & "  i_patch_name         => '" & patch_name & "' -" _
    & Chr(10) & " ,i_patch_type         => '" & patch_type & "' -" _
    & Chr(10) & " ,i_db_schema          => '" & db_schema & "' -" _
    & Chr(10) & " ,i_app_code           => '' -" _
    & Chr(10) & " ,i_branch_name        => '" & branch_path & "' -" _
    & Chr(10) & " ,i_tag_from           => '" & tag1_name & "' -" _
    & Chr(10) & " ,i_tag_to             => '" & tag2_name & "' -" _
    & Chr(10) & " ,i_suffix             => '" & suffix & "' -" _
    & Chr(10) & " ,i_patch_desc         => '" & patch_desc & "' -" _
    & Chr(10) & " ,i_patch_components   => '" & l_all_programs & "' -" _
    & Chr(10) & " ,i_patch_create_date  => '" & DateString & "' -" _
    & Chr(10) & " ,i_patch_created_by   => '" & Environment.UserName & "' -" _
    & Chr(10) & " ,i_note               => '" & note & "' -" _
    & Chr(10) & " ,i_rerunnable_yn      => '" & rerunnable_yn & "' -" _
    & Chr(10) & " ,i_tracking_yn        => '" & track_promotion_yn & "' -" _
    & Chr(10) & " ,i_alt_schema_yn      => '" & alt_schema_yn & "' -" _
    & Chr(10) & " ,i_retired_yn         => '" & retired_yn & "'); " _
    & Chr(10))


                Dim l_prereq_short_name As String = Nothing
                For Each l_prereq_patch In prereq_patches
                    l_prereq_short_name = Common.getLastSegment(l_prereq_patch, "\")
                    l_master_file.WriteLine("PROMPT")
                    l_master_file.WriteLine("PROMPT Checking Prerequisite patch " & l_prereq_short_name)
                    l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.add_patch_prereq( -")
                    l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                    l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")

                Next

                l_prereq_short_name = My.Settings.MinPatch
                l_master_file.WriteLine("PROMPT Check ARM version supports this patch.")
                l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.add_patch_prereq( -")
                l_master_file.WriteLine("i_patch_name     => '" & patch_name & "' -")
                l_master_file.WriteLine(",i_prereq_patch  => '" & l_prereq_short_name & "' );")



                l_master_file.WriteLine("select user||'@'||global_name Connection from global_name;")
                'Write the list of files to execute.

                l_master_file.WriteLine("COMMIT;")

            End If

            l_master_file.WriteLine("Prompt installing PATCHES" & Chr(10) & l_patches)

            If use_arm Then

                l_master_file.WriteLine("execute &&APEXRM_user..arm_installer.patch_completed(i_patch_name  => '" & patch_name & "');")

                l_master_file.WriteLine("COMMIT;")
            End If



            l_master_file.WriteLine("PROMPT ")
            l_master_file.WriteLine("PROMPT " & l_master_filename & " - COMPLETED.")

            l_master_file.WriteLine("spool off;")


            l_master_file.Close()


        End If


    End Sub



    Shared Sub exportPatchSet(ByVal patch_name As String,
                                  ByVal patch_type As String,
                                  ByVal db_schema As String,
                                  ByVal branch_path As String,
                                  ByVal tag1_name As String,
                                  ByVal tag2_name As String,
                                  ByVal supplementary As String,
                                  ByVal patch_desc As String,
                                  ByVal note As String,
                                  ByVal use_patch_admin As Boolean,
                                  ByVal rerunnable As Boolean,
                                  ByRef targetFiles As Collection,
                                  ByVal patchDir As String,
                                  ByVal patchExportDir As String,
                                  ByVal patchSetPath As String,
                                  ByVal groupPath As String,
                                  ByVal track_promotion As Boolean)



        For Each l_path In targetFiles


            Dim l_source_path As String = Globals.RootPatchDir & l_path
            Dim l_target_path As String = patchExportDir & "\" & l_path


            FileIO.createFolderIfNotExists(l_target_path)

            Dim objfso = CreateObject("Scripting.FileSystemObject")
            Dim objFolder As Object
            Dim objFile As Object

            objFolder = objfso.GetFolder(l_source_path)
            For Each objFile In objFolder.Files
                objfso.CopyFile(l_source_path & "\" & objFile.Name, l_target_path & "\" & objFile.Name, True)
                'Info("using reference file " & objFile.Name)
            Next

            'Copy README.txt
            Try
                objfso.CopyFile(Globals.RootPatchDir & "README.txt", patchExportDir & "\README.txt", True)
            Catch ex As Exception
                MsgBox("No README.txt found, to copy to the patchset.")

            End Try



        Next

        If targetFiles.Count > 0 Then

            Dim l_master_lite_filename As String = "install_patchset_lite.sql"
            Dim l_master_lite_file As New System.IO.StreamWriter(patchExportDir & "\" & l_master_lite_filename)

            l_master_lite_file.WriteLine(Common.unix_path("@" & patchSetPath & "\" & "install_lite.sql"))

            l_master_lite_file.Close()

            Dim l_master_filename As String = "install_patchset.sql"
            Dim l_master_file As New System.IO.StreamWriter(patchExportDir & "\" & l_master_filename)

            l_master_file.WriteLine(Common.unix_path("@" & patchSetPath & "\" & "install.sql"))

            l_master_file.Close()


        End If


    End Sub



    Private Sub CopySelectedChanges()

        'Copy to the new Patchable Tree
        TreeViewPatchOrder.PathSeparator = "#"
        TreeViewPatchOrder.Nodes.Clear()


        'Prepopulate Tree with default category nodes.
        'This should become a method on TreeViewDraggableNodes2Levels
        Dim l_patches_category As String = "Patches"
        'GPTrees.AddCategory(TreeViewPatchOrder.Nodes, l_patches_category)
        TreeViewPatchOrder.AddCategory(l_patches_category)

        Dim ChosenChanges As Collection = New Collection
        'Repo changes
        'Retrieve checked node items from the TreeViewChanges as a collection of files.
        'GPTrees.ReadCheckedLeafNodes(AvailablePatchesTreeView.Nodes, ChosenChanges)
        AvailablePatchesTreeView.ReadCheckedLeafNodes(ChosenChanges)

        If ChosenChanges.Count = 0 Then
            MsgBox("No patches selected.")
        Else
            Dim ReorderedChanges As Collection = New Collection


            If ComboBoxPatchesFilter.SelectedItem = "Unapplied" Then

                ReorderedChanges = PatchRunner.ReorderByDependancy(ChosenChanges, AvailablePatches)
            Else
                ReorderedChanges = ChosenChanges
                If ChosenChanges.Count > 1 Then
                    MsgBox("WARNING: Unordered patches.  Dependancy order can only be determined when using the 'Unapplied' filter. " & Chr(10) &
                           "Please drag and drop to re-order the patches.")
                End If
            End If



            Dim l_label As String
            For Each change In ReorderedChanges
                Dim pathSeparator As String = "\"

                l_label = Common.getLastSegment(change, pathSeparator)
                TreeViewPatchOrder.AddFileToCategory(l_patches_category, l_label, change)
            Next

            TreeViewPatchOrder.ExpandAll()
        End If



    End Sub


    Private Sub PatchTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchTabControl.SelectedIndexChanged

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageTags" Then
            If TagsCheckedListBox.Items.Count = 0 Then
                Findtags()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePatches" Then
            Application.DoEvents()
            deriveTags()

            If AvailablePatchesTreeView.Nodes.Count = 0 Then
                FindPatches()
            End If


        End If

        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePreReqs" Then
            If PreReqPatchesTreeView.Nodes.Count = 0 Then
                FindPreReqs()
            End If


        End If



        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPagePatchDefn" Then
            'Copy Patchable items to the next list.

            'PatchPathTextBox.Text = pCreatePatchType & "\" & Globals.currentAppCode & "\" 'Replace(Globals.currentLongBranch, "/", "\") & "\"

            'PatchPathTextBox.Text = pCreatePatchType
            PatchPathTextBox.Text = "release\"
            If Globals.getAppInFeature() = "Y" Then
                PatchPathTextBox.Text = PatchPathTextBox.Text & Globals.currentAppCode & "\"
            End If
            'PatchPathTextBox.Text = PatchPathTextBox.Text & Globals.currentAppCode




            derivePatchName()

            PatchDirTextBox.Text = Globals.RootPatchDir & PatchPathTextBox.Text & PatchNameTextBox.Text & "\"

            UsePatchAdminCheckBox.Checked = Globals.getUseARM

            RerunCheckBox.Checked = False

            TrackPromoCheckBox.Checked = True

            If TreeViewPatchOrder.Nodes.Count = 0 Then
                CopySelectedChanges()
            End If
        End If


        If (PatchTabControl.SelectedTab.Name.ToString) = "TabPageExecute" Then

            ExecutePatchButton.Text = "Execute Patch on " & Globals.currentTNS

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

        PatchFromTags.FindPatches(PreReqPatchesTreeView, False, PreReqPatchTypeComboBox.SelectedItem)

    End Sub


    Private Sub PreReqButton_Click(sender As Object, e As EventArgs) Handles PreReqButton.Click
        FindPreReqs()

    End Sub




    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click


        'Confirm run against non-VM target
        If Globals.getDB <> "VM" Then
            Dim result As Integer = MessageBox.Show("Confirm target is " & Globals.getDB &
      Chr(10) & "The release will be installed in " & Globals.getDB & ".", "Confirm Target", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Dim l_install_file As String = Nothing
        If UsePatchAdminCheckBox.Checked Then
            l_install_file = "\install.sql"
        Else
            l_install_file = "\install_lite.sql"
        End If

        'Use Host class to execute with a master script.
        Host.RunMasterScript("DEFINE database = '" & Globals.getDATASOURCE & "'" &
                Environment.NewLine & "@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql" &
                Environment.NewLine & "@" & PatchPathTextBox.Text & PatchNameTextBox.Text & l_install_file, Globals.RootPatchDir)


    End Sub

    Private Sub CopyChangesButton_Click(sender As Object, e As EventArgs) Handles CopyChangesButton.Click
        CopySelectedChanges()
    End Sub

    Private Sub FindTagsButton_Click(sender As Object, e As EventArgs) Handles FindTagsButton.Click
        Findtags()
    End Sub

    Private Sub deriveTags()


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


    Public Shared Sub bumpApexVersion(ByVal i_app_version As String)  'Deprecated, keep code examples
        Apex.modCreateApplicationSQL(i_app_version & " " & Today.ToString("dd-MMM-yyyy"), "")
    End Sub




    Private Sub FindButton_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        FindPatches()
    End Sub



    Private Sub ExportButton_Click(sender As Object, e As EventArgs) Handles ExportButton.Click

        Dim l_repo_patch_dir As String = Globals.PatchExportDir & Globals.getRepoName & "\"

        Dim l_repo_patch_export_dir As String = l_repo_patch_dir & PatchNameTextBox.Text


        'Remove previous patch set
        Try
            FileIO.confirmDeleteFolder(l_repo_patch_export_dir)
        Catch ex As Exception
            MsgBox("Warning: Now overwriting previously exported patchset")
        End Try


        FileIO.createFolderIfNotExists(l_repo_patch_export_dir)

        Dim patchableFiles As Collection = New Collection
        TreeViewPatchOrder.ReadTags(patchableFiles, False, True, True, False)

        'Add the PatchSet itself to the list.
        patchableFiles.Add(PatchPathTextBox.Text & PatchNameTextBox.Text, _
                           PatchPathTextBox.Text & PatchNameTextBox.Text)



        'Export each patch, and create the patch_install.sql
        exportPatchSet(PatchNameTextBox.Text,
                           pCreatePatchType,
                           "APEXRM",
                           Globals.currentLongBranch,
                           Tag1TextBox.Text,
                           Tag2TextBox.Text,
                           SupIdTextBox.Text,
                           PatchDescTextBox.Text,
                           NoteTextBox.Text,
                           UsePatchAdminCheckBox.Checked,
                           RerunCheckBox.Checked,
                           patchableFiles,
                           PatchDirTextBox.Text,
                           l_repo_patch_export_dir,
                           PatchPathTextBox.Text & PatchNameTextBox.Text,
                           PatchPathTextBox.Text,
                           TrackPromoCheckBox.Checked)


        zip.zip_dir(l_repo_patch_export_dir & ".zip",
                    l_repo_patch_export_dir)

        Host.RunExplorer(l_repo_patch_export_dir)


    End Sub

   
  
    Private Sub ComitButton_Click(sender As Object, e As EventArgs) Handles ComitButton.Click

        Tortoise.Commit(PatchDirTextBox.Text, "NEW " & pCreatePatchType & ": " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, True)

        Mail.SendNotification("NEW " & pCreatePatchType & ": " & PatchNameTextBox.Text & " - " & PatchDescTextBox.Text, pCreatePatchType & " created.", PatchDirTextBox.Text & "install.sql," & Globals.RootPatchDir & PatchNameTextBox.Text & ".log")

        'user
        'branch
        'tags
        'desc
        'note


    End Sub

  
End Class