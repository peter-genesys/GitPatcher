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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreatePatchCollection))
        Me.TabPagePatchDefn = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PatchButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RerunCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.UsePatchAdminCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.CopyChangesButton = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.PatchDescTextBox = New System.Windows.Forms.TextBox()
        Me.SupIdTextBox = New System.Windows.Forms.TextBox()
        Me.PatchDirTextBox = New System.Windows.Forms.TextBox()
        Me.PatchNameTextBox = New System.Windows.Forms.TextBox()
        Me.NoteTextBox = New System.Windows.Forms.TextBox()
        Me.PatchPathTextBox = New System.Windows.Forms.TextBox()
        Me.TrackPromoCheckBox = New System.Windows.Forms.CheckBox()
        Me.TreeViewPatchOrder = New TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels()
        Me.ExecutePatchButton = New System.Windows.Forms.Button()
        Me.ComitButton = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExportButton = New System.Windows.Forms.Button()
        Me.TabPagePreReqs = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.PreReqButton = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.PreReqPatchTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.PreReqPatchesTreeView = New TreeViewEnhanced.TreeViewEnhanced()
        Me.TabPagePatches = New System.Windows.Forms.TabPage()
        Me.FindButton = New System.Windows.Forms.Button()
        Me.TagFilterCheckBox = New System.Windows.Forms.CheckBox()
        Me.AvailablePatchesLabel = New System.Windows.Forms.Label()
        Me.ComboBoxPatchesFilter = New System.Windows.Forms.ComboBox()
        Me.AvailablePatchesTreeView = New TreeViewEnhanced.TreeViewEnhanced()
        Me.AppFilterCheckBox = New System.Windows.Forms.CheckBox()
        Me.TabPageTags = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tag2TextBox = New System.Windows.Forms.TextBox()
        Me.Tag1TextBox = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TagsCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.FindTagsButton = New System.Windows.Forms.Button()
        Me.PatchTabControl = New System.Windows.Forms.TabControl()
        Me.TabPagePatchDefn.SuspendLayout()
        Me.TabPagePreReqs.SuspendLayout()
        Me.TabPagePatches.SuspendLayout()
        Me.TabPageTags.SuspendLayout()
        Me.PatchTabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPagePatchDefn
        '
        Me.TabPagePatchDefn.Controls.Add(Me.ExportButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label3)
        Me.TabPagePatchDefn.Controls.Add(Me.ComitButton)
        Me.TabPagePatchDefn.Controls.Add(Me.ExecutePatchButton)
        Me.TabPagePatchDefn.Controls.Add(Me.TreeViewPatchOrder)
        Me.TabPagePatchDefn.Controls.Add(Me.TrackPromoCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchPathTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.NoteTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchNameTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchDirTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.SupIdTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchDescTextBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label18)
        Me.TabPagePatchDefn.Controls.Add(Me.CopyChangesButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label16)
        Me.TabPagePatchDefn.Controls.Add(Me.Label12)
        Me.TabPagePatchDefn.Controls.Add(Me.Label11)
        Me.TabPagePatchDefn.Controls.Add(Me.Label8)
        Me.TabPagePatchDefn.Controls.Add(Me.Label10)
        Me.TabPagePatchDefn.Controls.Add(Me.UsePatchAdminCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label9)
        Me.TabPagePatchDefn.Controls.Add(Me.RerunCheckBox)
        Me.TabPagePatchDefn.Controls.Add(Me.Label5)
        Me.TabPagePatchDefn.Controls.Add(Me.Label7)
        Me.TabPagePatchDefn.Controls.Add(Me.PatchButton)
        Me.TabPagePatchDefn.Controls.Add(Me.Label6)
        Me.TabPagePatchDefn.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePatchDefn.Name = "TabPagePatchDefn"
        Me.TabPagePatchDefn.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePatchDefn.Size = New System.Drawing.Size(521, 743)
        Me.TabPagePatchDefn.TabIndex = 2
        Me.TabPagePatchDefn.Text = "Patch Defn"
        Me.TabPagePatchDefn.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 489)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Patch Name"
        '
        'PatchButton
        '
        Me.PatchButton.Location = New System.Drawing.Point(77, 608)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(230, 23)
        Me.PatchButton.TabIndex = 7
        Me.PatchButton.Text = "Create Patch"
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 559)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Description"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 514)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Patch Dir"
        '
        'RerunCheckBox
        '
        Me.RerunCheckBox.AutoSize = True
        Me.RerunCheckBox.Location = New System.Drawing.Point(80, 537)
        Me.RerunCheckBox.Name = "RerunCheckBox"
        Me.RerunCheckBox.Size = New System.Drawing.Size(81, 17)
        Me.RerunCheckBox.TabIndex = 18
        Me.RerunCheckBox.Text = "Rerunnable"
        Me.RerunCheckBox.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 585)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Notes"
        '
        'UsePatchAdminCheckBox
        '
        Me.UsePatchAdminCheckBox.AutoSize = True
        Me.UsePatchAdminCheckBox.Location = New System.Drawing.Point(317, 641)
        Me.UsePatchAdminCheckBox.Name = "UsePatchAdminCheckBox"
        Me.UsePatchAdminCheckBox.Size = New System.Drawing.Size(108, 17)
        Me.UsePatchAdminCheckBox.TabIndex = 19
        Me.UsePatchAdminCheckBox.Text = "Use Patch Admin"
        Me.UsePatchAdminCheckBox.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(77, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(376, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Tick a patch to skip it during patchset testing. Remove all ticks in final versio" &
    "n."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 433)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Sup Id"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(77, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(267, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "All listed patches will be called  - Drag n' drop to reorder"
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
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(137, 433)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(288, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Add a supplementary label for extra patches from used tags."
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
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(10, 462)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 34
        Me.Label18.Text = "Patch Path"
        '
        'PatchDescTextBox
        '
        Me.PatchDescTextBox.Location = New System.Drawing.Point(80, 556)
        Me.PatchDescTextBox.Name = "PatchDescTextBox"
        Me.PatchDescTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchDescTextBox.TabIndex = 20
        '
        'SupIdTextBox
        '
        Me.SupIdTextBox.Location = New System.Drawing.Point(80, 433)
        Me.SupIdTextBox.Name = "SupIdTextBox"
        Me.SupIdTextBox.Size = New System.Drawing.Size(51, 20)
        Me.SupIdTextBox.TabIndex = 22
        '
        'PatchDirTextBox
        '
        Me.PatchDirTextBox.Location = New System.Drawing.Point(80, 511)
        Me.PatchDirTextBox.Name = "PatchDirTextBox"
        Me.PatchDirTextBox.ReadOnly = True
        Me.PatchDirTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchDirTextBox.TabIndex = 14
        '
        'PatchNameTextBox
        '
        Me.PatchNameTextBox.Location = New System.Drawing.Point(80, 486)
        Me.PatchNameTextBox.Name = "PatchNameTextBox"
        Me.PatchNameTextBox.ReadOnly = True
        Me.PatchNameTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchNameTextBox.TabIndex = 16
        '
        'NoteTextBox
        '
        Me.NoteTextBox.Location = New System.Drawing.Point(80, 582)
        Me.NoteTextBox.Name = "NoteTextBox"
        Me.NoteTextBox.Size = New System.Drawing.Size(429, 20)
        Me.NoteTextBox.TabIndex = 24
        '
        'PatchPathTextBox
        '
        Me.PatchPathTextBox.Location = New System.Drawing.Point(80, 459)
        Me.PatchPathTextBox.Name = "PatchPathTextBox"
        Me.PatchPathTextBox.ReadOnly = True
        Me.PatchPathTextBox.Size = New System.Drawing.Size(429, 20)
        Me.PatchPathTextBox.TabIndex = 33
        '
        'TrackPromoCheckBox
        '
        Me.TrackPromoCheckBox.AutoSize = True
        Me.TrackPromoCheckBox.Location = New System.Drawing.Point(167, 537)
        Me.TrackPromoCheckBox.Name = "TrackPromoCheckBox"
        Me.TrackPromoCheckBox.Size = New System.Drawing.Size(104, 17)
        Me.TrackPromoCheckBox.TabIndex = 36
        Me.TrackPromoCheckBox.Text = "Track Promotion"
        Me.TrackPromoCheckBox.UseVisualStyleBackColor = True
        '
        'TreeViewPatchOrder
        '
        Me.TreeViewPatchOrder.BackColor = System.Drawing.Color.AliceBlue
        Me.TreeViewPatchOrder.CheckBoxes = True
        Me.TreeViewPatchOrder.Location = New System.Drawing.Point(80, 76)
        Me.TreeViewPatchOrder.Name = "TreeViewPatchOrder"
        Me.TreeViewPatchOrder.Size = New System.Drawing.Size(429, 351)
        Me.TreeViewPatchOrder.TabIndex = 45
        '
        'ExecutePatchButton
        '
        Me.ExecutePatchButton.Location = New System.Drawing.Point(77, 637)
        Me.ExecutePatchButton.Name = "ExecutePatchButton"
        Me.ExecutePatchButton.Size = New System.Drawing.Size(230, 23)
        Me.ExecutePatchButton.TabIndex = 46
        Me.ExecutePatchButton.Text = "Execute Patch"
        Me.ExecutePatchButton.UseVisualStyleBackColor = True
        '
        'ComitButton
        '
        Me.ComitButton.Location = New System.Drawing.Point(77, 666)
        Me.ComitButton.Name = "ComitButton"
        Me.ComitButton.Size = New System.Drawing.Size(230, 23)
        Me.ComitButton.TabIndex = 47
        Me.ComitButton.Text = "Commit Patch"
        Me.ComitButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(217, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "(Rechecks dependancy order, can be slow.)"
        '
        'ExportButton
        '
        Me.ExportButton.Location = New System.Drawing.Point(77, 695)
        Me.ExportButton.Name = "ExportButton"
        Me.ExportButton.Size = New System.Drawing.Size(230, 23)
        Me.ExportButton.TabIndex = 49
        Me.ExportButton.Text = "Export Patch"
        Me.ExportButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ExportButton.UseVisualStyleBackColor = True
        '
        'TabPagePreReqs
        '
        Me.TabPagePreReqs.Controls.Add(Me.PreReqPatchesTreeView)
        Me.TabPagePreReqs.Controls.Add(Me.PreReqPatchTypeComboBox)
        Me.TabPagePreReqs.Controls.Add(Me.Label20)
        Me.TabPagePreReqs.Controls.Add(Me.PreReqButton)
        Me.TabPagePreReqs.Controls.Add(Me.Label13)
        Me.TabPagePreReqs.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePreReqs.Name = "TabPagePreReqs"
        Me.TabPagePreReqs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePreReqs.Size = New System.Drawing.Size(521, 743)
        Me.TabPagePreReqs.TabIndex = 3
        Me.TabPagePreReqs.Text = "Pre-Requisites"
        Me.TabPagePreReqs.UseVisualStyleBackColor = True
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
        'PreReqButton
        '
        Me.PreReqButton.Location = New System.Drawing.Point(77, 17)
        Me.PreReqButton.Name = "PreReqButton"
        Me.PreReqButton.Size = New System.Drawing.Size(139, 23)
        Me.PreReqButton.TabIndex = 33
        Me.PreReqButton.Text = "Search"
        Me.PreReqButton.UseVisualStyleBackColor = True
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
        'PreReqPatchTypeComboBox
        '
        Me.PreReqPatchTypeComboBox.FormattingEnabled = True
        Me.PreReqPatchTypeComboBox.Location = New System.Drawing.Point(323, 19)
        Me.PreReqPatchTypeComboBox.Name = "PreReqPatchTypeComboBox"
        Me.PreReqPatchTypeComboBox.Size = New System.Drawing.Size(183, 21)
        Me.PreReqPatchTypeComboBox.TabIndex = 34
        '
        'PreReqPatchesTreeView
        '
        Me.PreReqPatchesTreeView.BackColor = System.Drawing.Color.Wheat
        Me.PreReqPatchesTreeView.CheckBoxes = True
        Me.PreReqPatchesTreeView.Location = New System.Drawing.Point(77, 76)
        Me.PreReqPatchesTreeView.Name = "PreReqPatchesTreeView"
        Me.PreReqPatchesTreeView.Size = New System.Drawing.Size(429, 619)
        Me.PreReqPatchesTreeView.TabIndex = 58
        '
        'TabPagePatches
        '
        Me.TabPagePatches.Controls.Add(Me.AppFilterCheckBox)
        Me.TabPagePatches.Controls.Add(Me.AvailablePatchesTreeView)
        Me.TabPagePatches.Controls.Add(Me.ComboBoxPatchesFilter)
        Me.TabPagePatches.Controls.Add(Me.AvailablePatchesLabel)
        Me.TabPagePatches.Controls.Add(Me.TagFilterCheckBox)
        Me.TabPagePatches.Controls.Add(Me.FindButton)
        Me.TabPagePatches.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePatches.Name = "TabPagePatches"
        Me.TabPagePatches.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePatches.Size = New System.Drawing.Size(521, 743)
        Me.TabPagePatches.TabIndex = 6
        Me.TabPagePatches.Text = "Patches"
        Me.TabPagePatches.UseVisualStyleBackColor = True
        '
        'FindButton
        '
        Me.FindButton.Location = New System.Drawing.Point(77, 17)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(139, 23)
        Me.FindButton.TabIndex = 5
        Me.FindButton.Text = "Find Patches"
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'TagFilterCheckBox
        '
        Me.TagFilterCheckBox.AutoSize = True
        Me.TagFilterCheckBox.Location = New System.Drawing.Point(222, 23)
        Me.TagFilterCheckBox.Name = "TagFilterCheckBox"
        Me.TagFilterCheckBox.Size = New System.Drawing.Size(89, 17)
        Me.TagFilterCheckBox.TabIndex = 40
        Me.TagFilterCheckBox.Text = "Filter by Tags"
        Me.TagFilterCheckBox.UseVisualStyleBackColor = True
        '
        'AvailablePatchesLabel
        '
        Me.AvailablePatchesLabel.AutoSize = True
        Me.AvailablePatchesLabel.Location = New System.Drawing.Point(21, 76)
        Me.AvailablePatchesLabel.Name = "AvailablePatchesLabel"
        Me.AvailablePatchesLabel.Size = New System.Drawing.Size(50, 26)
        Me.AvailablePatchesLabel.TabIndex = 41
        Me.AvailablePatchesLabel.Text = "Available" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Patches"
        '
        'ComboBoxPatchesFilter
        '
        Me.ComboBoxPatchesFilter.FormattingEnabled = True
        Me.ComboBoxPatchesFilter.Items.AddRange(New Object() {"Unapplied", "Uninstalled", "All"})
        Me.ComboBoxPatchesFilter.Location = New System.Drawing.Point(367, 17)
        Me.ComboBoxPatchesFilter.Name = "ComboBoxPatchesFilter"
        Me.ComboBoxPatchesFilter.Size = New System.Drawing.Size(139, 21)
        Me.ComboBoxPatchesFilter.TabIndex = 60
        '
        'AvailablePatchesTreeView
        '
        Me.AvailablePatchesTreeView.BackColor = System.Drawing.Color.Wheat
        Me.AvailablePatchesTreeView.CheckBoxes = True
        Me.AvailablePatchesTreeView.Location = New System.Drawing.Point(77, 76)
        Me.AvailablePatchesTreeView.Name = "AvailablePatchesTreeView"
        Me.AvailablePatchesTreeView.Size = New System.Drawing.Size(429, 619)
        Me.AvailablePatchesTreeView.TabIndex = 61
        '
        'AppFilterCheckBox
        '
        Me.AppFilterCheckBox.AutoSize = True
        Me.AppFilterCheckBox.Location = New System.Drawing.Point(222, 46)
        Me.AppFilterCheckBox.Name = "AppFilterCheckBox"
        Me.AppFilterCheckBox.Size = New System.Drawing.Size(84, 17)
        Me.AppFilterCheckBox.TabIndex = 62
        Me.AppFilterCheckBox.Text = "Filter by App"
        Me.AppFilterCheckBox.UseVisualStyleBackColor = True
        '
        'TabPageTags
        '
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
        Me.TabPageTags.Size = New System.Drawing.Size(521, 743)
        Me.TabPageTags.TabIndex = 0
        Me.TabPageTags.Text = "Tags"
        Me.TabPageTags.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(228, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To Tag - UPTO this tag"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(228, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From Tag - AFTER this tag"
        '
        'Tag2TextBox
        '
        Me.Tag2TextBox.Location = New System.Drawing.Point(367, 46)
        Me.Tag2TextBox.Name = "Tag2TextBox"
        Me.Tag2TextBox.ReadOnly = True
        Me.Tag2TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag2TextBox.TabIndex = 2
        '
        'Tag1TextBox
        '
        Me.Tag1TextBox.Location = New System.Drawing.Point(367, 20)
        Me.Tag1TextBox.Name = "Tag1TextBox"
        Me.Tag1TextBox.ReadOnly = True
        Me.Tag1TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag1TextBox.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(40, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 13)
        Me.Label15.TabIndex = 13
        Me.Label15.Text = "Tags"
        '
        'TagsCheckedListBox
        '
        Me.TagsCheckedListBox.CheckOnClick = True
        Me.TagsCheckedListBox.FormattingEnabled = True
        Me.TagsCheckedListBox.Location = New System.Drawing.Point(77, 76)
        Me.TagsCheckedListBox.Name = "TagsCheckedListBox"
        Me.TagsCheckedListBox.Size = New System.Drawing.Size(429, 619)
        Me.TagsCheckedListBox.TabIndex = 12
        '
        'FindTagsButton
        '
        Me.FindTagsButton.Location = New System.Drawing.Point(77, 17)
        Me.FindTagsButton.Name = "FindTagsButton"
        Me.FindTagsButton.Size = New System.Drawing.Size(139, 23)
        Me.FindTagsButton.TabIndex = 14
        Me.FindTagsButton.Text = "Find Tags"
        Me.FindTagsButton.UseVisualStyleBackColor = True
        '
        'PatchTabControl
        '
        Me.PatchTabControl.Controls.Add(Me.TabPageTags)
        Me.PatchTabControl.Controls.Add(Me.TabPagePatches)
        Me.PatchTabControl.Controls.Add(Me.TabPagePreReqs)
        Me.PatchTabControl.Controls.Add(Me.TabPagePatchDefn)
        Me.PatchTabControl.Location = New System.Drawing.Point(12, 12)
        Me.PatchTabControl.Name = "PatchTabControl"
        Me.PatchTabControl.SelectedIndex = 0
        Me.PatchTabControl.Size = New System.Drawing.Size(529, 769)
        Me.PatchTabControl.TabIndex = 18
        '
        'CreatePatchCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 793)
        Me.Controls.Add(Me.PatchTabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CreatePatchCollection"
        Me.Text = "CreatePatchCollection"
        Me.TabPagePatchDefn.ResumeLayout(False)
        Me.TabPagePatchDefn.PerformLayout()
        Me.TabPagePreReqs.ResumeLayout(False)
        Me.TabPagePreReqs.PerformLayout()
        Me.TabPagePatches.ResumeLayout(False)
        Me.TabPagePatches.PerformLayout()
        Me.TabPageTags.ResumeLayout(False)
        Me.TabPageTags.PerformLayout()
        Me.PatchTabControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPagePatchDefn As TabPage
    Friend WithEvents ExportButton As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ComitButton As Button
    Friend WithEvents ExecutePatchButton As Button
    Friend WithEvents TreeViewPatchOrder As TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels
    Friend WithEvents TrackPromoCheckBox As CheckBox
    Friend WithEvents PatchPathTextBox As TextBox
    Friend WithEvents NoteTextBox As TextBox
    Friend WithEvents PatchNameTextBox As TextBox
    Friend WithEvents PatchDirTextBox As TextBox
    Friend WithEvents SupIdTextBox As TextBox
    Friend WithEvents PatchDescTextBox As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents CopyChangesButton As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents UsePatchAdminCheckBox As CheckBox
    Friend WithEvents Label9 As Label
    Friend WithEvents RerunCheckBox As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents PatchButton As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents TabPagePreReqs As TabPage
    Friend WithEvents PreReqPatchesTreeView As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents PreReqPatchTypeComboBox As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents PreReqButton As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents TabPagePatches As TabPage
    Friend WithEvents AppFilterCheckBox As CheckBox
    Friend WithEvents AvailablePatchesTreeView As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents ComboBoxPatchesFilter As ComboBox
    Friend WithEvents AvailablePatchesLabel As Label
    Friend WithEvents TagFilterCheckBox As CheckBox
    Friend WithEvents FindButton As Button
    Friend WithEvents TabPageTags As TabPage
    Friend WithEvents FindTagsButton As Button
    Friend WithEvents TagsCheckedListBox As CheckedListBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Tag1TextBox As TextBox
    Friend WithEvents Tag2TextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PatchTabControl As TabControl
End Class
