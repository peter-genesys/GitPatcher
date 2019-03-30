<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PatchFromTags
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatchFromTags))
        Me.TabPagePatchDefn = New System.Windows.Forms.TabPage()
        Me.AppOnlyCheckBox = New System.Windows.Forms.CheckBox()
        Me.AlternateSchemasCheckBox = New System.Windows.Forms.CheckBox()
        Me.SYSDBACheckBox = New System.Windows.Forms.CheckBox()
        Me.ExportPatchButton = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.ButtonPopNotes = New System.Windows.Forms.Button()
        Me.ButtonPopDesc = New System.Windows.Forms.Button()
        Me.TreeViewPatchOrder = New TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels()
        Me.CommitButton = New System.Windows.Forms.Button()
        Me.TrackPromoCheckBox = New System.Windows.Forms.CheckBox()
        Me.PatchPathTextBox = New System.Windows.Forms.TextBox()
        Me.NoteTextBox = New System.Windows.Forms.TextBox()
        Me.PatchNameTextBox = New System.Windows.Forms.TextBox()
        Me.PatchDirTextBox = New System.Windows.Forms.TextBox()
        Me.SupIdTextBox = New System.Windows.Forms.TextBox()
        Me.PatchDescTextBox = New System.Windows.Forms.TextBox()
        Me.ExecuteButton = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CopyChangesButton = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.UseARMCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RerunCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PatchButton = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabPagePreReqsA = New System.Windows.Forms.TabPage()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.PreReqPatchesTreeViewA = New TreeViewEnhanced.TreeViewEnhanced()
        Me.ButtonLastPatch = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabPageTags = New System.Windows.Forms.TabPage()
        Me.UseSHA1Button = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.FindTagsButton = New System.Windows.Forms.Button()
        Me.TagsCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Tag1TextBox = New System.Windows.Forms.TextBox()
        Me.Tag2TextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PatchTabControl = New System.Windows.Forms.TabControl()
        Me.TabPageSHA1 = New System.Windows.Forms.TabPage()
        Me.FindsSHA1Button = New System.Windows.Forms.Button()
        Me.UseTagsButton = New System.Windows.Forms.Button()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.SHA1TextBox1 = New System.Windows.Forms.TextBox()
        Me.SHA1TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.TabPageChanges = New System.Windows.Forms.TabPage()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TreeViewChanges = New TreeViewEnhanced.TreeViewEnhanced()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.SchemaCountTextBox = New System.Windows.Forms.TextBox()
        Me.SchemaComboBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FindButton = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.TabPageExtras = New System.Windows.Forms.TabPage()
        Me.RestrictExtraFilesToSchemaCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TreeViewFiles = New TreeViewEnhanced.TreeViewEnhanced()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ButtonFindFiles = New System.Windows.Forms.Button()
        Me.TabPageApexApps = New System.Windows.Forms.TabPage()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.TreeViewApps = New TreeViewEnhanced.TreeViewEnhanced()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.FindAppsButton = New System.Windows.Forms.Button()
        Me.TabPagePreReqsB = New System.Windows.Forms.TabPage()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.RestrictPreReqToBranchCheckBox = New System.Windows.Forms.CheckBox()
        Me.PreReqButton = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.PreReqPatchesTreeViewB = New TreeViewEnhanced.TreeViewEnhanced()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TagsContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MoveTag = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPagePatchDefn.SuspendLayout()
        Me.TabPagePreReqsA.SuspendLayout()
        Me.TabPageTags.SuspendLayout()
        Me.PatchTabControl.SuspendLayout()
        Me.TabPageSHA1.SuspendLayout()
        Me.TabPageChanges.SuspendLayout()
        Me.TabPageExtras.SuspendLayout()
        Me.TabPageApexApps.SuspendLayout()
        Me.TabPagePreReqsB.SuspendLayout()
        Me.TagsContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPagePatchDefn
        '
        Me.TabPagePatchDefn.Controls.Add(Me.AppOnlyCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.AlternateSchemasCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.SYSDBACheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.ExportPatchButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label26)
        Me.TabPagePatchDefn.Controls.Add(Me.ButtonPopNotes)
        Me.TabPagePatchDefn.Controls.Add(Me.ButtonPopDesc)
        Me.TabPagePatchDefn.Controls.Add(Me.TreeViewPatchOrder)
        Me.TabPagePatchDefn.Controls.Add(Me.CommitButton)
        Me.TabPagePatchDefn.Controls.Add(Me.TrackPromoCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchPathTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.NoteTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchNameTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchDirTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.SupIdTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchDescTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.ExecuteButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label18)
        Me.TabPagePatchDefn.Controls.Add(Me.CopyChangesButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label16)
        Me.TabPagePatchDefn.Controls.Add(Me.Label12)
        Me.TabPagePatchDefn.Controls.Add(Me.Label11)
        Me.TabPagePatchDefn.Controls.Add(Me.Label8)
        Me.TabPagePatchDefn.Controls.Add(Me.Label10)
        Me.TabPagePatchDefn.Controls.Add(Me.UseARMCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label9)
        Me.TabPagePatchDefn.Controls.Add(Me.RerunCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label5)
        Me.TabPagePatchDefn.Controls.Add(Me.Label7)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label6)
        Me.TabPagePatchDefn.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePatchDefn.Name = "TabPagePatchDefn"
        Me.TabPagePatchDefn.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePatchDefn.Size = New System.Drawing.Size(525, 757)
        Me.TabPagePatchDefn.TabIndex = 2
        Me.TabPagePatchDefn.Text = "Create Patch"
        Me.TabPagePatchDefn.UseVisualStyleBackColor = True
        '
        'AppOnlyCheckBox
        '
        Me.AppOnlyCheckBox.AutoSize = True
        Me.AppOnlyCheckBox.Enabled = False
        Me.AppOnlyCheckBox.Location = New System.Drawing.Point(77, 13)
        Me.AppOnlyCheckBox.Name = "AppOnlyCheckBox"
        Me.AppOnlyCheckBox.Size = New System.Drawing.Size(101, 17)
        Me.AppOnlyCheckBox.TabIndex = 52
        Me.AppOnlyCheckBox.Text = "Apex-Apps-Only"
        Me.AppOnlyCheckBox.UseVisualStyleBackColor = True
        '
        'AlternateSchemasCheckBox
        '
        Me.AlternateSchemasCheckBox.AutoSize = True
        Me.AlternateSchemasCheckBox.Location = New System.Drawing.Point(395, 567)
        Me.AlternateSchemasCheckBox.Name = "AlternateSchemasCheckBox"
        Me.AlternateSchemasCheckBox.Size = New System.Drawing.Size(115, 17)
        Me.AlternateSchemasCheckBox.TabIndex = 51
        Me.AlternateSchemasCheckBox.Text = "Alternate Schemas"
        Me.AlternateSchemasCheckBox.UseVisualStyleBackColor = True
        '
        'SYSDBACheckBox
        '
        Me.SYSDBACheckBox.AutoSize = True
        Me.SYSDBACheckBox.Location = New System.Drawing.Point(277, 567)
        Me.SYSDBACheckBox.Name = "SYSDBACheckBox"
        Me.SYSDBACheckBox.Size = New System.Drawing.Size(116, 17)
        Me.SYSDBACheckBox.TabIndex = 50
        Me.SYSDBACheckBox.Text = "Use SYSDBA Role"
        Me.SYSDBACheckBox.UseVisualStyleBackColor = True
        '
        'ExportPatchButton
        '
        Me.ExportPatchButton.Location = New System.Drawing.Point(80, 726)
        Me.ExportPatchButton.Name = "ExportPatchButton"
        Me.ExportPatchButton.Size = New System.Drawing.Size(230, 23)
        Me.ExportPatchButton.TabIndex = 49
        Me.ExportPatchButton.Text = "Export Patch"
        Me.ExportPatchButton.UseVisualStyleBackColor = True
        Me.ExportPatchButton.Visible = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(222, 40)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(221, 13)
        Me.Label26.TabIndex = 47
        Me.Label26.Text = "Files are copied from Changes and Extra Files"
        Me.Label26.Visible = False
        '
        'ButtonPopNotes
        '
        Me.ButtonPopNotes.Image = Global.GitPatcher.My.Resources.Resources.Search_16
        Me.ButtonPopNotes.Location = New System.Drawing.Point(489, 611)
        Me.ButtonPopNotes.Name = "ButtonPopNotes"
        Me.ButtonPopNotes.Size = New System.Drawing.Size(22, 22)
        Me.ButtonPopNotes.TabIndex = 46
        Me.ButtonPopNotes.UseVisualStyleBackColor = True
        '
        'ButtonPopDesc
        '
        Me.ButtonPopDesc.Image = Global.GitPatcher.My.Resources.Resources.Search_16
        Me.ButtonPopDesc.Location = New System.Drawing.Point(489, 584)
        Me.ButtonPopDesc.Name = "ButtonPopDesc"
        Me.ButtonPopDesc.Size = New System.Drawing.Size(22, 22)
        Me.ButtonPopDesc.TabIndex = 45
        Me.ButtonPopDesc.UseVisualStyleBackColor = True
        '
        'TreeViewPatchOrder
        '
        Me.TreeViewPatchOrder.BackColor = System.Drawing.Color.AliceBlue
        Me.TreeViewPatchOrder.CheckBoxes = True
        Me.TreeViewPatchOrder.Location = New System.Drawing.Point(80, 103)
        Me.TreeViewPatchOrder.Name = "TreeViewPatchOrder"
        Me.TreeViewPatchOrder.Size = New System.Drawing.Size(429, 351)
        Me.TreeViewPatchOrder.TabIndex = 44
        '
        'CommitButton
        '
        Me.CommitButton.Location = New System.Drawing.Point(80, 697)
        Me.CommitButton.Name = "CommitButton"
        Me.CommitButton.Size = New System.Drawing.Size(230, 23)
        Me.CommitButton.TabIndex = 1
        Me.CommitButton.Text = "Commit Patch"
        Me.CommitButton.UseVisualStyleBackColor = True
        '
        'TrackPromoCheckBox
        '
        Me.TrackPromoCheckBox.AutoSize = True
        Me.TrackPromoCheckBox.Location = New System.Drawing.Point(167, 567)
        Me.TrackPromoCheckBox.Name = "TrackPromoCheckBox"
        Me.TrackPromoCheckBox.Size = New System.Drawing.Size(104, 17)
        Me.TrackPromoCheckBox.TabIndex = 35
        Me.TrackPromoCheckBox.Text = "Track Promotion"
        Me.TrackPromoCheckBox.UseVisualStyleBackColor = True
        '
        'PatchPathTextBox
        '
        Me.PatchPathTextBox.Location = New System.Drawing.Point(80, 489)
        Me.PatchPathTextBox.Name = "PatchPathTextBox"
        Me.PatchPathTextBox.ReadOnly = True
        Me.PatchPathTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchPathTextBox.TabIndex = 33
        '
        'NoteTextBox
        '
        Me.NoteTextBox.Location = New System.Drawing.Point(80, 612)
        Me.NoteTextBox.Name = "NoteTextBox"
        Me.NoteTextBox.Size = New System.Drawing.Size(411, 20)
        Me.NoteTextBox.TabIndex = 24
        '
        'PatchNameTextBox
        '
        Me.PatchNameTextBox.Location = New System.Drawing.Point(80, 516)
        Me.PatchNameTextBox.Name = "PatchNameTextBox"
        Me.PatchNameTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchNameTextBox.TabIndex = 16
        '
        'PatchDirTextBox
        '
        Me.PatchDirTextBox.Location = New System.Drawing.Point(80, 541)
        Me.PatchDirTextBox.Name = "PatchDirTextBox"
        Me.PatchDirTextBox.ReadOnly = True
        Me.PatchDirTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchDirTextBox.TabIndex = 14
        '
        'SupIdTextBox
        '
        Me.SupIdTextBox.Location = New System.Drawing.Point(80, 460)
        Me.SupIdTextBox.Name = "SupIdTextBox"
        Me.SupIdTextBox.Size = New System.Drawing.Size(51, 20)
        Me.SupIdTextBox.TabIndex = 22
        '
        'PatchDescTextBox
        '
        Me.PatchDescTextBox.Location = New System.Drawing.Point(80, 586)
        Me.PatchDescTextBox.Name = "PatchDescTextBox"
        Me.PatchDescTextBox.Size = New System.Drawing.Size(411, 20)
        Me.PatchDescTextBox.TabIndex = 20
        '
        'ExecuteButton
        '
        Me.ExecuteButton.Location = New System.Drawing.Point(80, 668)
        Me.ExecuteButton.Name = "ExecuteButton"
        Me.ExecuteButton.Size = New System.Drawing.Size(230, 23)
        Me.ExecuteButton.TabIndex = 0
        Me.ExecuteButton.Text = "Execute Patch"
        Me.ExecuteButton.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(10, 492)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 34
        Me.Label18.Text = "Patch Path"
        '
        'CopyChangesButton
        '
        Me.CopyChangesButton.Location = New System.Drawing.Point(77, 36)
        Me.CopyChangesButton.Name = "CopyChangesButton"
        Me.CopyChangesButton.Size = New System.Drawing.Size(139, 23)
        Me.CopyChangesButton.TabIndex = 32
        Me.CopyChangesButton.Text = "Copy Files"
        Me.CopyChangesButton.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(137, 463)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(288, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Add a supplementary label for extra patches from used tags."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(10, 103)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Files"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(77, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(281, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "All listed changes will be patched  - Drag n' drop to reorder"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 463)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Sup Id"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(77, 78)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(256, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "A tick indicates errors are to be ignored for this script."
        '
        'UseARMCheckBox
        '
        Me.UseARMCheckBox.AutoSize = True
        Me.UseARMCheckBox.Location = New System.Drawing.Point(317, 668)
        Me.UseARMCheckBox.Name = "UseARMCheckBox"
        Me.UseARMCheckBox.Size = New System.Drawing.Size(115, 17)
        Me.UseARMCheckBox.TabIndex = 19
        Me.UseARMCheckBox.Text = "Use Apex-Rel-Man"
        Me.UseARMCheckBox.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 615)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Notes"
        '
        'RerunCheckBox
        '
        Me.RerunCheckBox.AutoSize = True
        Me.RerunCheckBox.Location = New System.Drawing.Point(80, 567)
        Me.RerunCheckBox.Name = "RerunCheckBox"
        Me.RerunCheckBox.Size = New System.Drawing.Size(81, 17)
        Me.RerunCheckBox.TabIndex = 18
        Me.RerunCheckBox.Text = "Rerunnable"
        Me.RerunCheckBox.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 544)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Patch Dir"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 589)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Description"
        '
        'PatchButton
        '
        Me.PatchButton.Location = New System.Drawing.Point(80, 639)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(230, 23)
        Me.PatchButton.TabIndex = 7
        Me.PatchButton.Text = "Create Patch"
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 519)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Patch Name"
        '
        'TabPagePreReqsA
        '
        Me.TabPagePreReqsA.Controls.Add(Me.Label25)
        Me.TabPagePreReqsA.Controls.Add(Me.Label23)
        Me.TabPagePreReqsA.Controls.Add(Me.PreReqPatchesTreeViewA)
        Me.TabPagePreReqsA.Controls.Add(Me.ButtonLastPatch)
        Me.TabPagePreReqsA.Controls.Add(Me.Label13)
        Me.TabPagePreReqsA.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePreReqsA.Name = "TabPagePreReqsA"
        Me.TabPagePreReqsA.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePreReqsA.Size = New System.Drawing.Size(525, 757)
        Me.TabPagePreReqsA.TabIndex = 3
        Me.TabPagePreReqsA.Text = "Last Patches"
        Me.TabPagePreReqsA.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(77, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(390, 13)
        Me.Label25.TabIndex = 55
        Me.Label25.Text = "The Last Patch of any component of this patch, will be deemed a Pre-Req Patch."
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(74, 61)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(413, 26)
        Me.Label23.TabIndex = 53
        Me.Label23.Text = "Before this patch will install, these prerequisite Last Patches must have been in" &
    "stalled. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Then once this patch is installed, these Last Patches will no longer " &
    "be rerunnable."
        '
        'PreReqPatchesTreeViewA
        '
        Me.PreReqPatchesTreeViewA.BackColor = System.Drawing.Color.Wheat
        Me.PreReqPatchesTreeViewA.CheckBoxes = True
        Me.PreReqPatchesTreeViewA.Location = New System.Drawing.Point(77, 93)
        Me.PreReqPatchesTreeViewA.Name = "PreReqPatchesTreeViewA"
        Me.PreReqPatchesTreeViewA.Size = New System.Drawing.Size(429, 571)
        Me.PreReqPatchesTreeViewA.TabIndex = 50
        '
        'ButtonLastPatch
        '
        Me.ButtonLastPatch.Location = New System.Drawing.Point(77, 36)
        Me.ButtonLastPatch.Name = "ButtonLastPatch"
        Me.ButtonLastPatch.Size = New System.Drawing.Size(139, 23)
        Me.ButtonLastPatch.TabIndex = 45
        Me.ButtonLastPatch.Text = "Find Last Patches"
        Me.ButtonLastPatch.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(28, 93)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Prereqs"
        '
        'TabPageTags
        '
        Me.TabPageTags.Controls.Add(Me.UseSHA1Button)
        Me.TabPageTags.Controls.Add(Me.Label28)
        Me.TabPageTags.Controls.Add(Me.Label29)
        Me.TabPageTags.Controls.Add(Me.Label27)
        Me.TabPageTags.Controls.Add(Me.FindTagsButton)
        Me.TabPageTags.Controls.Add(Me.TagsCheckedListBox)
        Me.TabPageTags.Controls.Add(Me.Label15)
        Me.TabPageTags.Controls.Add(Me.Tag1TextBox)
        Me.TabPageTags.Controls.Add(Me.Tag2TextBox)
        Me.TabPageTags.Controls.Add(Me.Label1)
        Me.TabPageTags.Controls.Add(Me.Label2)
        Me.TabPageTags.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTags.Name = "TabPageTags"
        Me.TabPageTags.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTags.Size = New System.Drawing.Size(525, 757)
        Me.TabPageTags.TabIndex = 0
        Me.TabPageTags.Text = "Tags"
        Me.TabPageTags.UseVisualStyleBackColor = True
        '
        'UseSHA1Button
        '
        Me.UseSHA1Button.Location = New System.Drawing.Point(367, 36)
        Me.UseSHA1Button.Name = "UseSHA1Button"
        Me.UseSHA1Button.Size = New System.Drawing.Size(139, 23)
        Me.UseSHA1Button.TabIndex = 56
        Me.UseSHA1Button.Text = "Use SHA-1"
        Me.UseSHA1Button.UseVisualStyleBackColor = True
        Me.UseSHA1Button.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(222, 661)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(79, 13)
        Me.Label28.TabIndex = 54
        Me.Label28.Text = "AFTER this tag"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(222, 687)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(74, 13)
        Me.Label29.TabIndex = 55
        Me.Label29.Text = "UPTO this tag"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(74, 77)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(173, 13)
        Me.Label27.TabIndex = 53
        Me.Label27.Text = "Tick 2 tags - From Tag and To Tag"
        '
        'FindTagsButton
        '
        Me.FindTagsButton.Location = New System.Drawing.Point(77, 36)
        Me.FindTagsButton.Name = "FindTagsButton"
        Me.FindTagsButton.Size = New System.Drawing.Size(139, 23)
        Me.FindTagsButton.TabIndex = 14
        Me.FindTagsButton.Text = "Find Tags"
        Me.FindTagsButton.UseVisualStyleBackColor = True
        '
        'TagsCheckedListBox
        '
        Me.TagsCheckedListBox.CheckOnClick = True
        Me.TagsCheckedListBox.ContextMenuStrip = Me.TagsContextMenuStrip
        Me.TagsCheckedListBox.FormattingEnabled = True
        Me.TagsCheckedListBox.Location = New System.Drawing.Point(77, 93)
        Me.TagsCheckedListBox.Name = "TagsCheckedListBox"
        Me.TagsCheckedListBox.Size = New System.Drawing.Size(429, 559)
        Me.TagsCheckedListBox.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(40, 93)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 13)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Tags"
        '
        'Tag1TextBox
        '
        Me.Tag1TextBox.Location = New System.Drawing.Point(77, 658)
        Me.Tag1TextBox.Name = "Tag1TextBox"
        Me.Tag1TextBox.ReadOnly = True
        Me.Tag1TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag1TextBox.TabIndex = 0
        '
        'Tag2TextBox
        '
        Me.Tag2TextBox.Location = New System.Drawing.Point(77, 684)
        Me.Tag2TextBox.Name = "Tag2TextBox"
        Me.Tag2TextBox.ReadOnly = True
        Me.Tag2TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag2TextBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 661)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Tag"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 687)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To Tag"
        '
        'PatchTabControl
        '
        Me.PatchTabControl.Controls.Add(Me.TabPageTags)
        Me.PatchTabControl.Controls.Add(Me.TabPageSHA1)
        Me.PatchTabControl.Controls.Add(Me.TabPageChanges)
        Me.PatchTabControl.Controls.Add(Me.TabPageExtras)
        Me.PatchTabControl.Controls.Add(Me.TabPageApexApps)
        Me.PatchTabControl.Controls.Add(Me.TabPagePreReqsA)
        Me.PatchTabControl.Controls.Add(Me.TabPagePreReqsB)
        Me.PatchTabControl.Controls.Add(Me.TabPagePatchDefn)
        Me.PatchTabControl.Location = New System.Drawing.Point(12, 12)
        Me.PatchTabControl.Name = "PatchTabControl"
        Me.PatchTabControl.SelectedIndex = 0
        Me.PatchTabControl.Size = New System.Drawing.Size(533, 783)
        Me.PatchTabControl.TabIndex = 18
        '
        'TabPageSHA1
        '
        Me.TabPageSHA1.Controls.Add(Me.FindsSHA1Button)
        Me.TabPageSHA1.Controls.Add(Me.UseTagsButton)
        Me.TabPageSHA1.Controls.Add(Me.Label31)
        Me.TabPageSHA1.Controls.Add(Me.Label32)
        Me.TabPageSHA1.Controls.Add(Me.SHA1TextBox1)
        Me.TabPageSHA1.Controls.Add(Me.SHA1TextBox2)
        Me.TabPageSHA1.Controls.Add(Me.Label33)
        Me.TabPageSHA1.Controls.Add(Me.Label34)
        Me.TabPageSHA1.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSHA1.Name = "TabPageSHA1"
        Me.TabPageSHA1.Size = New System.Drawing.Size(525, 757)
        Me.TabPageSHA1.TabIndex = 8
        Me.TabPageSHA1.Text = "SHA-1"
        Me.TabPageSHA1.UseVisualStyleBackColor = True
        '
        'FindsSHA1Button
        '
        Me.FindsSHA1Button.Location = New System.Drawing.Point(77, 36)
        Me.FindsSHA1Button.Name = "FindsSHA1Button"
        Me.FindsSHA1Button.Size = New System.Drawing.Size(139, 23)
        Me.FindsSHA1Button.TabIndex = 63
        Me.FindsSHA1Button.Text = "Find SHA-1"
        Me.FindsSHA1Button.UseVisualStyleBackColor = True
        '
        'UseTagsButton
        '
        Me.UseTagsButton.Location = New System.Drawing.Point(367, 36)
        Me.UseTagsButton.Name = "UseTagsButton"
        Me.UseTagsButton.Size = New System.Drawing.Size(139, 23)
        Me.UseTagsButton.TabIndex = 62
        Me.UseTagsButton.Text = "Use Tags"
        Me.UseTagsButton.UseVisualStyleBackColor = True
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(364, 89)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(97, 13)
        Me.Label31.TabIndex = 60
        Me.Label31.Text = "AFTER this commit"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(364, 115)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(137, 13)
        Me.Label32.TabIndex = 61
        Me.Label32.Text = "UPTO including this commit"
        '
        'SHA1TextBox1
        '
        Me.SHA1TextBox1.Location = New System.Drawing.Point(77, 86)
        Me.SHA1TextBox1.Name = "SHA1TextBox1"
        Me.SHA1TextBox1.Size = New System.Drawing.Size(281, 20)
        Me.SHA1TextBox1.TabIndex = 56
        '
        'SHA1TextBox2
        '
        Me.SHA1TextBox2.Location = New System.Drawing.Point(77, 112)
        Me.SHA1TextBox2.Name = "SHA1TextBox2"
        Me.SHA1TextBox2.Size = New System.Drawing.Size(281, 20)
        Me.SHA1TextBox2.TabIndex = 58
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(10, 89)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(64, 13)
        Me.Label33.TabIndex = 57
        Me.Label33.Text = "From SHA-1"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(20, 115)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(54, 13)
        Me.Label34.TabIndex = 59
        Me.Label34.Text = "To SHA-1"
        '
        'TabPageChanges
        '
        Me.TabPageChanges.Controls.Add(Me.Label20)
        Me.TabPageChanges.Controls.Add(Me.TreeViewChanges)
        Me.TabPageChanges.Controls.Add(Me.Label17)
        Me.TabPageChanges.Controls.Add(Me.SchemaCountTextBox)
        Me.TabPageChanges.Controls.Add(Me.SchemaComboBox)
        Me.TabPageChanges.Controls.Add(Me.Label4)
        Me.TabPageChanges.Controls.Add(Me.FindButton)
        Me.TabPageChanges.Controls.Add(Me.Label3)
        Me.TabPageChanges.Controls.Add(Me.ViewButton)
        Me.TabPageChanges.Location = New System.Drawing.Point(4, 22)
        Me.TabPageChanges.Name = "TabPageChanges"
        Me.TabPageChanges.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageChanges.Size = New System.Drawing.Size(525, 757)
        Me.TabPageChanges.TabIndex = 1
        Me.TabPageChanges.Text = "Changes"
        Me.TabPageChanges.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(74, 77)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(198, 13)
        Me.Label20.TabIndex = 52
        Me.Label20.Text = "Ticked files will be included in the patch."
        '
        'TreeViewChanges
        '
        Me.TreeViewChanges.BackColor = System.Drawing.Color.Wheat
        Me.TreeViewChanges.CheckBoxes = True
        Me.TreeViewChanges.Location = New System.Drawing.Point(77, 93)
        Me.TreeViewChanges.Name = "TreeViewChanges"
        Me.TreeViewChanges.Size = New System.Drawing.Size(429, 571)
        Me.TreeViewChanges.TabIndex = 51
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(386, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(77, 13)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Schema Count"
        '
        'SchemaCountTextBox
        '
        Me.SchemaCountTextBox.Location = New System.Drawing.Point(469, 10)
        Me.SchemaCountTextBox.Name = "SchemaCountTextBox"
        Me.SchemaCountTextBox.ReadOnly = True
        Me.SchemaCountTextBox.Size = New System.Drawing.Size(37, 20)
        Me.SchemaCountTextBox.TabIndex = 14
        '
        'SchemaComboBox
        '
        Me.SchemaComboBox.FormattingEnabled = True
        Me.SchemaComboBox.Location = New System.Drawing.Point(367, 36)
        Me.SchemaComboBox.Name = "SchemaComboBox"
        Me.SchemaComboBox.Size = New System.Drawing.Size(139, 21)
        Me.SchemaComboBox.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(315, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Schema"
        '
        'FindButton
        '
        Me.FindButton.Location = New System.Drawing.Point(77, 36)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(139, 23)
        Me.FindButton.TabIndex = 4
        Me.FindButton.Text = "Find Changes"
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Changes"
        '
        'ViewButton
        '
        Me.ViewButton.Location = New System.Drawing.Point(77, 670)
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.Size = New System.Drawing.Size(230, 23)
        Me.ViewButton.TabIndex = 12
        Me.ViewButton.Text = "View Ticked Files"
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'TabPageExtras
        '
        Me.TabPageExtras.Controls.Add(Me.RestrictExtraFilesToSchemaCheckBox)
        Me.TabPageExtras.Controls.Add(Me.Label24)
        Me.TabPageExtras.Controls.Add(Me.Label22)
        Me.TabPageExtras.Controls.Add(Me.TreeViewFiles)
        Me.TabPageExtras.Controls.Add(Me.Label21)
        Me.TabPageExtras.Controls.Add(Me.ButtonFindFiles)
        Me.TabPageExtras.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExtras.Name = "TabPageExtras"
        Me.TabPageExtras.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExtras.Size = New System.Drawing.Size(525, 757)
        Me.TabPageExtras.TabIndex = 6
        Me.TabPageExtras.Text = "Extra Files"
        Me.TabPageExtras.UseVisualStyleBackColor = True
        '
        'RestrictExtraFilesToSchemaCheckBox
        '
        Me.RestrictExtraFilesToSchemaCheckBox.AutoSize = True
        Me.RestrictExtraFilesToSchemaCheckBox.Location = New System.Drawing.Point(222, 39)
        Me.RestrictExtraFilesToSchemaCheckBox.Name = "RestrictExtraFilesToSchemaCheckBox"
        Me.RestrictExtraFilesToSchemaCheckBox.Size = New System.Drawing.Size(116, 17)
        Me.RestrictExtraFilesToSchemaCheckBox.TabIndex = 62
        Me.RestrictExtraFilesToSchemaCheckBox.Text = "Restrict to Schema"
        Me.RestrictExtraFilesToSchemaCheckBox.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(77, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(413, 13)
        Me.Label24.TabIndex = 54
        Me.Label24.Text = "Add any file direct from the current checkout.  Need not be to committed nor chan" &
    "ged."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(74, 77)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(198, 13)
        Me.Label22.TabIndex = 53
        Me.Label22.Text = "Ticked files will be included in the patch."
        '
        'TreeViewFiles
        '
        Me.TreeViewFiles.BackColor = System.Drawing.Color.Wheat
        Me.TreeViewFiles.CheckBoxes = True
        Me.TreeViewFiles.Location = New System.Drawing.Point(77, 93)
        Me.TreeViewFiles.Name = "TreeViewFiles"
        Me.TreeViewFiles.Size = New System.Drawing.Size(429, 571)
        Me.TreeViewFiles.TabIndex = 52
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(35, 93)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(36, 13)
        Me.Label21.TabIndex = 46
        Me.Label21.Text = "Extras"
        '
        'ButtonFindFiles
        '
        Me.ButtonFindFiles.Location = New System.Drawing.Point(77, 36)
        Me.ButtonFindFiles.Name = "ButtonFindFiles"
        Me.ButtonFindFiles.Size = New System.Drawing.Size(139, 23)
        Me.ButtonFindFiles.TabIndex = 5
        Me.ButtonFindFiles.Text = "Find Files"
        Me.ButtonFindFiles.UseVisualStyleBackColor = True
        '
        'TabPageApexApps
        '
        Me.TabPageApexApps.Controls.Add(Me.Label35)
        Me.TabPageApexApps.Controls.Add(Me.TreeViewApps)
        Me.TabPageApexApps.Controls.Add(Me.Label36)
        Me.TabPageApexApps.Controls.Add(Me.FindAppsButton)
        Me.TabPageApexApps.Location = New System.Drawing.Point(4, 22)
        Me.TabPageApexApps.Name = "TabPageApexApps"
        Me.TabPageApexApps.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageApexApps.Size = New System.Drawing.Size(525, 757)
        Me.TabPageApexApps.TabIndex = 9
        Me.TabPageApexApps.Text = "Apex Apps"
        Me.TabPageApexApps.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(74, 77)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(436, 13)
        Me.Label35.TabIndex = 55
        Me.Label35.Text = "Ticked apps will be imported by the patch. Apps from any parsing schema may be im" &
    "ported."
        '
        'TreeViewApps
        '
        Me.TreeViewApps.BackColor = System.Drawing.Color.Wheat
        Me.TreeViewApps.CheckBoxes = True
        Me.TreeViewApps.Location = New System.Drawing.Point(77, 93)
        Me.TreeViewApps.Name = "TreeViewApps"
        Me.TreeViewApps.Size = New System.Drawing.Size(429, 571)
        Me.TreeViewApps.TabIndex = 54
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(13, 93)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(58, 13)
        Me.Label36.TabIndex = 53
        Me.Label36.Text = "Apex Apps"
        '
        'FindAppsButton
        '
        Me.FindAppsButton.Location = New System.Drawing.Point(77, 36)
        Me.FindAppsButton.Name = "FindAppsButton"
        Me.FindAppsButton.Size = New System.Drawing.Size(139, 23)
        Me.FindAppsButton.TabIndex = 5
        Me.FindAppsButton.Text = "Find Apex Apps"
        Me.FindAppsButton.UseVisualStyleBackColor = True
        '
        'TabPagePreReqsB
        '
        Me.TabPagePreReqsB.Controls.Add(Me.Label30)
        Me.TabPagePreReqsB.Controls.Add(Me.RestrictPreReqToBranchCheckBox)
        Me.TabPagePreReqsB.Controls.Add(Me.PreReqButton)
        Me.TabPagePreReqsB.Controls.Add(Me.Label14)
        Me.TabPagePreReqsB.Controls.Add(Me.PreReqPatchesTreeViewB)
        Me.TabPagePreReqsB.Controls.Add(Me.Label19)
        Me.TabPagePreReqsB.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePreReqsB.Name = "TabPagePreReqsB"
        Me.TabPagePreReqsB.Size = New System.Drawing.Size(525, 757)
        Me.TabPagePreReqsB.TabIndex = 7
        Me.TabPagePreReqsB.Text = "Best Order"
        Me.TabPagePreReqsB.UseVisualStyleBackColor = True
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(77, 16)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(443, 13)
        Me.Label30.TabIndex = 62
        Me.Label30.Text = "Nominate any recent patch that this patch, or its components, are expected to dep" &
    "end upon."
        '
        'RestrictPreReqToBranchCheckBox
        '
        Me.RestrictPreReqToBranchCheckBox.AutoSize = True
        Me.RestrictPreReqToBranchCheckBox.Location = New System.Drawing.Point(222, 39)
        Me.RestrictPreReqToBranchCheckBox.Name = "RestrictPreReqToBranchCheckBox"
        Me.RestrictPreReqToBranchCheckBox.Size = New System.Drawing.Size(111, 17)
        Me.RestrictPreReqToBranchCheckBox.TabIndex = 61
        Me.RestrictPreReqToBranchCheckBox.Text = "Restrict to Branch"
        Me.RestrictPreReqToBranchCheckBox.UseVisualStyleBackColor = True
        '
        'PreReqButton
        '
        Me.PreReqButton.Location = New System.Drawing.Point(77, 36)
        Me.PreReqButton.Name = "PreReqButton"
        Me.PreReqButton.Size = New System.Drawing.Size(139, 23)
        Me.PreReqButton.TabIndex = 60
        Me.PreReqButton.Text = "Search"
        Me.PreReqButton.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(74, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(443, 26)
        Me.Label14.TabIndex = 59
        Me.Label14.Text = "Before this patch will install, these prerequisite Best Order Patches must have b" &
    "een installed. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Once this patch is installed, these prerequisites may still be " &
    "rerun, if required."
        '
        'PreReqPatchesTreeViewB
        '
        Me.PreReqPatchesTreeViewB.BackColor = System.Drawing.Color.Wheat
        Me.PreReqPatchesTreeViewB.CheckBoxes = True
        Me.PreReqPatchesTreeViewB.Location = New System.Drawing.Point(77, 93)
        Me.PreReqPatchesTreeViewB.Name = "PreReqPatchesTreeViewB"
        Me.PreReqPatchesTreeViewB.Size = New System.Drawing.Size(429, 571)
        Me.PreReqPatchesTreeViewB.TabIndex = 58
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(28, 93)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(43, 13)
        Me.Label19.TabIndex = 54
        Me.Label19.Text = "Prereqs"
        '
        'TagsContextMenuStrip
        '
        Me.TagsContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MoveTag})
        Me.TagsContextMenuStrip.Name = "TagsContextMenuStrip"
        Me.TagsContextMenuStrip.Size = New System.Drawing.Size(181, 48)
        '
        'MoveTag
        '
        Me.MoveTag.Name = "MoveTag"
        Me.MoveTag.Size = New System.Drawing.Size(180, 22)
        Me.MoveTag.Text = "Update Tag"
        Me.MoveTag.ToolTipText = "Move this tag to the head of the current branch.  IE to the latest commit"
        '
        'PatchFromTags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 796)
        Me.Controls.Add(Me.PatchTabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PatchFromTags"
        Me.Text = "PatchFromTags"
        Me.TabPagePatchDefn.ResumeLayout(False)
        Me.TabPagePatchDefn.PerformLayout()
        Me.TabPagePreReqsA.ResumeLayout(False)
        Me.TabPagePreReqsA.PerformLayout()
        Me.TabPageTags.ResumeLayout(False)
        Me.TabPageTags.PerformLayout()
        Me.PatchTabControl.ResumeLayout(False)
        Me.TabPageSHA1.ResumeLayout(False)
        Me.TabPageSHA1.PerformLayout()
        Me.TabPageChanges.ResumeLayout(False)
        Me.TabPageChanges.PerformLayout()
        Me.TabPageExtras.ResumeLayout(False)
        Me.TabPageExtras.PerformLayout()
        Me.TabPageApexApps.ResumeLayout(False)
        Me.TabPageApexApps.PerformLayout()
        Me.TabPagePreReqsB.ResumeLayout(False)
        Me.TabPagePreReqsB.PerformLayout()
        Me.TagsContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabPagePatchDefn As System.Windows.Forms.TabPage
    Friend WithEvents CommitButton As System.Windows.Forms.Button
    Friend WithEvents TrackPromoCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents PatchPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NoteTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SupIdTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchDescTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExecuteButton As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CopyChangesButton As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents UseARMCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents RerunCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PatchButton As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabPagePreReqsA As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TabPageTags As System.Windows.Forms.TabPage
    Friend WithEvents FindTagsButton As System.Windows.Forms.Button
    Friend WithEvents TagsCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Tag1TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Tag2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PatchTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPageExtras As System.Windows.Forms.TabPage
    Friend WithEvents ButtonFindFiles As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ButtonLastPatch As System.Windows.Forms.Button
    Friend WithEvents TreeViewPatchOrder As TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels
    Friend WithEvents PreReqPatchesTreeViewA As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents TreeViewFiles As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents TabPageChanges As System.Windows.Forms.TabPage
    Friend WithEvents TreeViewChanges As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents SchemaCountTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SchemaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents FindButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ViewButton As System.Windows.Forms.Button
    Friend WithEvents ButtonPopDesc As System.Windows.Forms.Button
    Friend WithEvents ButtonPopNotes As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents ExportPatchButton As System.Windows.Forms.Button
    Friend WithEvents SYSDBACheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AlternateSchemasCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TabPagePreReqsB As TabPage
    Friend WithEvents Label14 As Label
    Friend WithEvents PreReqPatchesTreeViewB As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents Label19 As Label
    Friend WithEvents RestrictPreReqToBranchCheckBox As CheckBox
    Friend WithEvents PreReqButton As Button
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents UseSHA1Button As Button
    Friend WithEvents TabPageSHA1 As TabPage
    Friend WithEvents UseTagsButton As Button
    Friend WithEvents Label31 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents SHA1TextBox1 As TextBox
    Friend WithEvents SHA1TextBox2 As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents FindsSHA1Button As Button
    Friend WithEvents TabPageApexApps As TabPage
    Friend WithEvents Label35 As Label
    Friend WithEvents TreeViewApps As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents Label36 As Label
    Friend WithEvents FindAppsButton As Button
    Friend WithEvents RestrictExtraFilesToSchemaCheckBox As CheckBox
    Friend WithEvents AppOnlyCheckBox As CheckBox
    Friend WithEvents TagsContextMenuStrip As ContextMenuStrip
    Friend WithEvents MoveTag As ToolStripMenuItem
End Class
