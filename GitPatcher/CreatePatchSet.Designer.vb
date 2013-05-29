<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreatePatchCollection
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
        Me.Tag1TextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tag2TextBox = New System.Windows.Forms.TextBox()
        Me.FindButton = New System.Windows.Forms.Button()
        Me.PatchButton = New System.Windows.Forms.Button()
        Me.PatchDirTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PatchNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PatchTabControl = New System.Windows.Forms.TabControl()
        Me.TabPageTags = New System.Windows.Forms.TabPage()
        Me.FindTagsButton = New System.Windows.Forms.Button()
        Me.TagsCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TabPagePatches = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AvailablePatchesListBox = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ChosenPatchesListBox = New System.Windows.Forms.ListBox()
        Me.TabPagePreReqs = New System.Windows.Forms.TabPage()
        Me.PreReqPatchTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.PreReqButton = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.PrereqsCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.TabPageSuper = New System.Windows.Forms.TabPage()
        Me.SupPatchTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.SupersedesCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.TabPagePatchDefn = New System.Windows.Forms.TabPage()
        Me.PatchPathTextBox = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CopyChangesButton = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PatchableCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NoteTextBox = New System.Windows.Forms.TextBox()
        Me.UsePatchAdminCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RerunCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SupIdTextBox = New System.Windows.Forms.TextBox()
        Me.PatchDescTextBox = New System.Windows.Forms.TextBox()
        Me.TabPageExecute = New System.Windows.Forms.TabPage()
        Me.ComitButton = New System.Windows.Forms.Button()
        Me.ExecutePatchButton = New System.Windows.Forms.Button()
        Me.PatchTabControl.SuspendLayout()
        Me.TabPageTags.SuspendLayout()
        Me.TabPagePatches.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabPagePreReqs.SuspendLayout()
        Me.TabPageSuper.SuspendLayout()
        Me.TabPagePatchDefn.SuspendLayout()
        Me.TabPageExecute.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tag1TextBox
        '
        Me.Tag1TextBox.Location = New System.Drawing.Point(596, 20)
        Me.Tag1TextBox.Name = "Tag1TextBox"
        Me.Tag1TextBox.ReadOnly = True
        Me.Tag1TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag1TextBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(396, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Tag - Find Patches after this tag"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(396, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(173, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To Tag - Find patches upto this tag"
        '
        'Tag2TextBox
        '
        Me.Tag2TextBox.Location = New System.Drawing.Point(596, 46)
        Me.Tag2TextBox.Name = "Tag2TextBox"
        Me.Tag2TextBox.ReadOnly = True
        Me.Tag2TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag2TextBox.TabIndex = 2
        '
        'FindButton
        '
        Me.FindButton.Location = New System.Drawing.Point(77, 17)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(139, 23)
        Me.FindButton.TabIndex = 4
        Me.FindButton.Text = "Find Patches"
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'PatchButton
        '
        Me.PatchButton.Location = New System.Drawing.Point(77, 664)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(142, 23)
        Me.PatchButton.TabIndex = 7
        Me.PatchButton.Text = "Create Patch"
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'PatchDirTextBox
        '
        Me.PatchDirTextBox.Location = New System.Drawing.Point(77, 567)
        Me.PatchDirTextBox.Name = "PatchDirTextBox"
        Me.PatchDirTextBox.ReadOnly = True
        Me.PatchDirTextBox.Size = New System.Drawing.Size(401, 20)
        Me.PatchDirTextBox.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 570)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Patch Dir"
        '
        'PatchNameTextBox
        '
        Me.PatchNameTextBox.Location = New System.Drawing.Point(77, 542)
        Me.PatchNameTextBox.Name = "PatchNameTextBox"
        Me.PatchNameTextBox.ReadOnly = True
        Me.PatchNameTextBox.Size = New System.Drawing.Size(401, 20)
        Me.PatchNameTextBox.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 545)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Patch Name"
        '
        'PatchTabControl
        '
        Me.PatchTabControl.Controls.Add(Me.TabPageTags)
        Me.PatchTabControl.Controls.Add(Me.TabPagePatches)
        Me.PatchTabControl.Controls.Add(Me.TabPagePreReqs)
        Me.PatchTabControl.Controls.Add(Me.TabPageSuper)
        Me.PatchTabControl.Controls.Add(Me.TabPagePatchDefn)
        Me.PatchTabControl.Controls.Add(Me.TabPageExecute)
        Me.PatchTabControl.Location = New System.Drawing.Point(12, 12)
        Me.PatchTabControl.Name = "PatchTabControl"
        Me.PatchTabControl.SelectedIndex = 0
        Me.PatchTabControl.Size = New System.Drawing.Size(760, 738)
        Me.PatchTabControl.TabIndex = 18
        '
        'TabPageTags
        '
        Me.TabPageTags.Controls.Add(Me.FindTagsButton)
        Me.TabPageTags.Controls.Add(Me.TagsCheckedListBox)
        Me.TabPageTags.Controls.Add(Me.Label15)
        Me.TabPageTags.Controls.Add(Me.Tag1TextBox)
        Me.TabPageTags.Controls.Add(Me.Label1)
        Me.TabPageTags.Controls.Add(Me.Tag2TextBox)
        Me.TabPageTags.Controls.Add(Me.Label2)
        Me.TabPageTags.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTags.Name = "TabPageTags"
        Me.TabPageTags.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTags.Size = New System.Drawing.Size(752, 712)
        Me.TabPageTags.TabIndex = 0
        Me.TabPageTags.Text = "Tags"
        Me.TabPageTags.UseVisualStyleBackColor = True
        '
        'FindTagsButton
        '
        Me.FindTagsButton.Location = New System.Drawing.Point(77, 17)
        Me.FindTagsButton.Name = "FindTagsButton"
        Me.FindTagsButton.Size = New System.Drawing.Size(230, 23)
        Me.FindTagsButton.TabIndex = 14
        Me.FindTagsButton.Text = "Find Tags"
        Me.FindTagsButton.UseVisualStyleBackColor = True
        '
        'TagsCheckedListBox
        '
        Me.TagsCheckedListBox.FormattingEnabled = True
        Me.TagsCheckedListBox.Location = New System.Drawing.Point(77, 76)
        Me.TagsCheckedListBox.Name = "TagsCheckedListBox"
        Me.TagsCheckedListBox.Size = New System.Drawing.Size(658, 619)
        Me.TagsCheckedListBox.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(20, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 13)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Tags"
        '
        'TabPagePatches
        '
        Me.TabPagePatches.Controls.Add(Me.SplitContainer1)
        Me.TabPagePatches.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePatches.Name = "TabPagePatches"
        Me.TabPagePatches.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePatches.Size = New System.Drawing.Size(752, 712)
        Me.TabPagePatches.TabIndex = 1
        Me.TabPagePatches.Text = "Patches"
        Me.TabPagePatches.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(6, 6)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.AvailablePatchesListBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.FindButton)
        Me.SplitContainer1.Panel1MinSize = 50
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChosenPatchesListBox)
        Me.SplitContainer1.Panel2MinSize = 50
        Me.SplitContainer1.Size = New System.Drawing.Size(740, 700)
        Me.SplitContainer1.SplitterDistance = 372
        Me.SplitContainer1.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Available Patches"
        '
        'AvailablePatchesListBox
        '
        Me.AvailablePatchesListBox.FormattingEnabled = True
        Me.AvailablePatchesListBox.Location = New System.Drawing.Point(3, 56)
        Me.AvailablePatchesListBox.Name = "AvailablePatchesListBox"
        Me.AvailablePatchesListBox.Size = New System.Drawing.Size(350, 628)
        Me.AvailablePatchesListBox.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Chosen Patches"
        '
        'ChosenPatchesListBox
        '
        Me.ChosenPatchesListBox.FormattingEnabled = True
        Me.ChosenPatchesListBox.Location = New System.Drawing.Point(3, 56)
        Me.ChosenPatchesListBox.Name = "ChosenPatchesListBox"
        Me.ChosenPatchesListBox.Size = New System.Drawing.Size(350, 628)
        Me.ChosenPatchesListBox.TabIndex = 36
        '
        'TabPagePreReqs
        '
        Me.TabPagePreReqs.Controls.Add(Me.PreReqPatchTypeComboBox)
        Me.TabPagePreReqs.Controls.Add(Me.Label20)
        Me.TabPagePreReqs.Controls.Add(Me.PreReqButton)
        Me.TabPagePreReqs.Controls.Add(Me.Label13)
        Me.TabPagePreReqs.Controls.Add(Me.PrereqsCheckedListBox)
        Me.TabPagePreReqs.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePreReqs.Name = "TabPagePreReqs"
        Me.TabPagePreReqs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePreReqs.Size = New System.Drawing.Size(752, 712)
        Me.TabPagePreReqs.TabIndex = 3
        Me.TabPagePreReqs.Text = "Pre-Requisites"
        Me.TabPagePreReqs.UseVisualStyleBackColor = True
        '
        'PreReqPatchTypeComboBox
        '
        Me.PreReqPatchTypeComboBox.FormattingEnabled = True
        Me.PreReqPatchTypeComboBox.Location = New System.Drawing.Point(291, 19)
        Me.PreReqPatchTypeComboBox.Name = "PreReqPatchTypeComboBox"
        Me.PreReqPatchTypeComboBox.Size = New System.Drawing.Size(183, 21)
        Me.PreReqPatchTypeComboBox.TabIndex = 34
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(223, 22)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(62, 13)
        Me.Label20.TabIndex = 35
        Me.Label20.Text = "Patch Type"
        '
        'PreReqButton
        '
        Me.PreReqButton.Location = New System.Drawing.Point(77, 17)
        Me.PreReqButton.Name = "PreReqButton"
        Me.PreReqButton.Size = New System.Drawing.Size(139, 23)
        Me.PreReqButton.TabIndex = 33
        Me.PreReqButton.Text = "Search"
        Me.PreReqButton.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(28, 76)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Prereqs"
        '
        'PrereqsCheckedListBox
        '
        Me.PrereqsCheckedListBox.FormattingEnabled = True
        Me.PrereqsCheckedListBox.Location = New System.Drawing.Point(77, 76)
        Me.PrereqsCheckedListBox.Name = "PrereqsCheckedListBox"
        Me.PrereqsCheckedListBox.Size = New System.Drawing.Size(669, 619)
        Me.PrereqsCheckedListBox.TabIndex = 31
        '
        'TabPageSuper
        '
        Me.TabPageSuper.Controls.Add(Me.SupPatchTypeComboBox)
        Me.TabPageSuper.Controls.Add(Me.Label17)
        Me.TabPageSuper.Controls.Add(Me.Button1)
        Me.TabPageSuper.Controls.Add(Me.Label14)
        Me.TabPageSuper.Controls.Add(Me.SupersedesCheckedListBox)
        Me.TabPageSuper.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSuper.Name = "TabPageSuper"
        Me.TabPageSuper.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSuper.Size = New System.Drawing.Size(752, 712)
        Me.TabPageSuper.TabIndex = 4
        Me.TabPageSuper.Text = "Supersedes"
        Me.TabPageSuper.UseVisualStyleBackColor = True
        '
        'SupPatchTypeComboBox
        '
        Me.SupPatchTypeComboBox.FormattingEnabled = True
        Me.SupPatchTypeComboBox.Location = New System.Drawing.Point(291, 19)
        Me.SupPatchTypeComboBox.Name = "SupPatchTypeComboBox"
        Me.SupPatchTypeComboBox.Size = New System.Drawing.Size(183, 21)
        Me.SupPatchTypeComboBox.TabIndex = 36
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(223, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "Patch Type"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(77, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(139, 23)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Search"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(8, 76)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(63, 13)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "Supersedes"
        '
        'SupersedesCheckedListBox
        '
        Me.SupersedesCheckedListBox.FormattingEnabled = True
        Me.SupersedesCheckedListBox.Location = New System.Drawing.Point(77, 76)
        Me.SupersedesCheckedListBox.Name = "SupersedesCheckedListBox"
        Me.SupersedesCheckedListBox.Size = New System.Drawing.Size(669, 619)
        Me.SupersedesCheckedListBox.TabIndex = 31
        '
        'TabPagePatchDefn
        '
        Me.TabPagePatchDefn.Controls.Add(Me.PatchPathTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label18)
        Me.TabPagePatchDefn.Controls.Add(Me.CopyChangesButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label16)
        Me.TabPagePatchDefn.Controls.Add(Me.Label12)
        Me.TabPagePatchDefn.Controls.Add(Me.Label11)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchableCheckedListBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label8)
        Me.TabPagePatchDefn.Controls.Add(Me.Label10)
        Me.TabPagePatchDefn.Controls.Add(Me.NoteTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchNameTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchDirTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.UsePatchAdminCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label9)
        Me.TabPagePatchDefn.Controls.Add(Me.RerunCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label5)
        Me.TabPagePatchDefn.Controls.Add(Me.Label7)
        Me.TabPagePatchDefn.Controls.Add(Me.SupIdTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchDescTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label6)
        Me.TabPagePatchDefn.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePatchDefn.Name = "TabPagePatchDefn"
        Me.TabPagePatchDefn.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePatchDefn.Size = New System.Drawing.Size(752, 712)
        Me.TabPagePatchDefn.TabIndex = 2
        Me.TabPagePatchDefn.Text = "Patch Defn"
        Me.TabPagePatchDefn.UseVisualStyleBackColor = True
        '
        'PatchPathTextBox
        '
        Me.PatchPathTextBox.Location = New System.Drawing.Point(77, 515)
        Me.PatchPathTextBox.Name = "PatchPathTextBox"
        Me.PatchPathTextBox.ReadOnly = True
        Me.PatchPathTextBox.Size = New System.Drawing.Size(401, 20)
        Me.PatchPathTextBox.TabIndex = 33
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(7, 518)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 34
        Me.Label18.Text = "Patch Path"
        '
        'CopyChangesButton
        '
        Me.CopyChangesButton.Location = New System.Drawing.Point(77, 17)
        Me.CopyChangesButton.Name = "CopyChangesButton"
        Me.CopyChangesButton.Size = New System.Drawing.Size(139, 23)
        Me.CopyChangesButton.TabIndex = 32
        Me.CopyChangesButton.Text = "Copy Patches"
        Me.CopyChangesButton.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(134, 489)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(407, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Add a supplementary label if additional patches are to be created from the same t" & _
    "ags."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(10, 76)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Patches"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(77, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "All listed patches will be called."
        '
        'PatchableCheckedListBox
        '
        Me.PatchableCheckedListBox.FormattingEnabled = True
        Me.PatchableCheckedListBox.Location = New System.Drawing.Point(77, 76)
        Me.PatchableCheckedListBox.Name = "PatchableCheckedListBox"
        Me.PatchableCheckedListBox.Size = New System.Drawing.Size(661, 409)
        Me.PatchableCheckedListBox.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 489)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Sup Id"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(77, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(268, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Uncheck a patch to comment it out in the master script."
        '
        'NoteTextBox
        '
        Me.NoteTextBox.Location = New System.Drawing.Point(77, 638)
        Me.NoteTextBox.Name = "NoteTextBox"
        Me.NoteTextBox.Size = New System.Drawing.Size(401, 20)
        Me.NoteTextBox.TabIndex = 24
        '
        'UsePatchAdminCheckBox
        '
        Me.UsePatchAdminCheckBox.AutoSize = True
        Me.UsePatchAdminCheckBox.Location = New System.Drawing.Point(77, 593)
        Me.UsePatchAdminCheckBox.Name = "UsePatchAdminCheckBox"
        Me.UsePatchAdminCheckBox.Size = New System.Drawing.Size(108, 17)
        Me.UsePatchAdminCheckBox.TabIndex = 19
        Me.UsePatchAdminCheckBox.Text = "Use Patch Admin"
        Me.UsePatchAdminCheckBox.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 641)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Notes"
        '
        'RerunCheckBox
        '
        Me.RerunCheckBox.AutoSize = True
        Me.RerunCheckBox.Location = New System.Drawing.Point(191, 593)
        Me.RerunCheckBox.Name = "RerunCheckBox"
        Me.RerunCheckBox.Size = New System.Drawing.Size(81, 17)
        Me.RerunCheckBox.TabIndex = 18
        Me.RerunCheckBox.Text = "Rerunnable"
        Me.RerunCheckBox.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 615)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Description"
        '
        'SupIdTextBox
        '
        Me.SupIdTextBox.Location = New System.Drawing.Point(77, 489)
        Me.SupIdTextBox.Name = "SupIdTextBox"
        Me.SupIdTextBox.Size = New System.Drawing.Size(51, 20)
        Me.SupIdTextBox.TabIndex = 22
        '
        'PatchDescTextBox
        '
        Me.PatchDescTextBox.Location = New System.Drawing.Point(77, 612)
        Me.PatchDescTextBox.Name = "PatchDescTextBox"
        Me.PatchDescTextBox.Size = New System.Drawing.Size(401, 20)
        Me.PatchDescTextBox.TabIndex = 20
        '
        'TabPageExecute
        '
        Me.TabPageExecute.Controls.Add(Me.ComitButton)
        Me.TabPageExecute.Controls.Add(Me.ExecutePatchButton)
        Me.TabPageExecute.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExecute.Name = "TabPageExecute"
        Me.TabPageExecute.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExecute.Size = New System.Drawing.Size(752, 712)
        Me.TabPageExecute.TabIndex = 5
        Me.TabPageExecute.Text = "Execute Patch"
        Me.TabPageExecute.UseVisualStyleBackColor = True
        '
        'ComitButton
        '
        Me.ComitButton.Location = New System.Drawing.Point(77, 90)
        Me.ComitButton.Name = "ComitButton"
        Me.ComitButton.Size = New System.Drawing.Size(230, 23)
        Me.ComitButton.TabIndex = 1
        Me.ComitButton.Text = "Commit Patch"
        Me.ComitButton.UseVisualStyleBackColor = True
        '
        'ExecutePatchButton
        '
        Me.ExecutePatchButton.Location = New System.Drawing.Point(77, 17)
        Me.ExecutePatchButton.Name = "ExecutePatchButton"
        Me.ExecutePatchButton.Size = New System.Drawing.Size(230, 23)
        Me.ExecutePatchButton.TabIndex = 0
        Me.ExecutePatchButton.Text = "Execute Patch"
        Me.ExecutePatchButton.UseVisualStyleBackColor = True
        '
        'CreatePatchCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 762)
        Me.Controls.Add(Me.PatchTabControl)
        Me.Name = "CreatePatchCollection"
        Me.Text = "CreatePatchCollection"
        Me.PatchTabControl.ResumeLayout(False)
        Me.TabPageTags.ResumeLayout(False)
        Me.TabPageTags.PerformLayout()
        Me.TabPagePatches.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabPagePreReqs.ResumeLayout(False)
        Me.TabPagePreReqs.PerformLayout()
        Me.TabPageSuper.ResumeLayout(False)
        Me.TabPageSuper.PerformLayout()
        Me.TabPagePatchDefn.ResumeLayout(False)
        Me.TabPagePatchDefn.PerformLayout()
        Me.TabPageExecute.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tag1TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Tag2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents FindButton As System.Windows.Forms.Button
    Friend WithEvents PatchButton As System.Windows.Forms.Button
    Friend WithEvents PatchDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PatchNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PatchTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPageTags As System.Windows.Forms.TabPage
    Friend WithEvents TabPagePatches As System.Windows.Forms.TabPage
    Friend WithEvents TabPagePatchDefn As System.Windows.Forms.TabPage
    Friend WithEvents TabPagePreReqs As System.Windows.Forms.TabPage
    Friend WithEvents UsePatchAdminCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RerunCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents PatchDescTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents NoteTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents SupIdTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PatchableCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabPageSuper As System.Windows.Forms.TabPage
    Friend WithEvents TabPageExecute As System.Windows.Forms.TabPage
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TagsCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents PrereqsCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents SupersedesCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents PreReqButton As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ExecutePatchButton As System.Windows.Forms.Button
    Friend WithEvents CopyChangesButton As System.Windows.Forms.Button
    Friend WithEvents FindTagsButton As System.Windows.Forms.Button
    Friend WithEvents ComitButton As System.Windows.Forms.Button
    Friend WithEvents PatchPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PreReqPatchTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AvailablePatchesListBox As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ChosenPatchesListBox As System.Windows.Forms.ListBox
    Friend WithEvents SupPatchTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
