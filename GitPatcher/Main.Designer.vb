<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.GITToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatchFromTagsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PatchRunnerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateDBPatchSetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.APEXToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GITToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MergeAndPushFeatureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewFeatureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RepoComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CurrentBranchTextBox = New System.Windows.Forms.TextBox()
        Me.RootPatchDirTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DBListComboBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ApplicationListComboBox = New System.Windows.Forms.ComboBox()
        Me.RootApexDirTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CurrentConnectionTextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ParsingSchemaTextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BranchPathTextBox = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.MinPatchTextBox = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ApexAppTextBox = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PatchSchemasTextBox = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PatchSetTextBox = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DBPatchSetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GITToolStripMenuItem, Me.APEXToolStripMenuItem, Me.GITToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(488, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GITToolStripMenuItem
        '
        Me.GITToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PatchFromTagsToolStripMenuItem, Me.PatchRunnerToolStripMenuItem, Me.CreateDBPatchSetToolStripMenuItem, Me.DBPatchSetToolStripMenuItem})
        Me.GITToolStripMenuItem.Name = "GITToolStripMenuItem"
        Me.GITToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.GITToolStripMenuItem.Text = "PATCH"
        '
        'PatchFromTagsToolStripMenuItem
        '
        Me.PatchFromTagsToolStripMenuItem.Name = "PatchFromTagsToolStripMenuItem"
        Me.PatchFromTagsToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.PatchFromTagsToolStripMenuItem.Text = "Create DB Patch"
        '
        'PatchRunnerToolStripMenuItem
        '
        Me.PatchRunnerToolStripMenuItem.Name = "PatchRunnerToolStripMenuItem"
        Me.PatchRunnerToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.PatchRunnerToolStripMenuItem.Text = "PatchRunner"
        '
        'CreateDBPatchSetToolStripMenuItem
        '
        Me.CreateDBPatchSetToolStripMenuItem.Name = "CreateDBPatchSetToolStripMenuItem"
        Me.CreateDBPatchSetToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.CreateDBPatchSetToolStripMenuItem.Text = "Create DB Patch Set"
        '
        'APEXToolStripMenuItem
        '
        Me.APEXToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.APEXToolStripMenuItem.Name = "APEXToolStripMenuItem"
        Me.APEXToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.APEXToolStripMenuItem.Text = "APEX"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ImportToolStripMenuItem.Text = "Import"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'GITToolStripMenuItem1
        '
        Me.GITToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergeAndPushFeatureToolStripMenuItem, Me.NewFeatureToolStripMenuItem})
        Me.GITToolStripMenuItem1.Name = "GITToolStripMenuItem1"
        Me.GITToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.GITToolStripMenuItem1.Text = "GIT"
        '
        'MergeAndPushFeatureToolStripMenuItem
        '
        Me.MergeAndPushFeatureToolStripMenuItem.Name = "MergeAndPushFeatureToolStripMenuItem"
        Me.MergeAndPushFeatureToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.MergeAndPushFeatureToolStripMenuItem.Text = "Merge and Push Feature"
        '
        'NewFeatureToolStripMenuItem
        '
        Me.NewFeatureToolStripMenuItem.Name = "NewFeatureToolStripMenuItem"
        Me.NewFeatureToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.NewFeatureToolStripMenuItem.Text = "New Feature"
        '
        'RepoComboBox
        '
        Me.RepoComboBox.FormattingEnabled = True
        Me.RepoComboBox.Location = New System.Drawing.Point(103, 27)
        Me.RepoComboBox.Name = "RepoComboBox"
        Me.RepoComboBox.Size = New System.Drawing.Size(373, 21)
        Me.RepoComboBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Git Repo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Branch Name"
        '
        'CurrentBranchTextBox
        '
        Me.CurrentBranchTextBox.Location = New System.Drawing.Point(103, 80)
        Me.CurrentBranchTextBox.Name = "CurrentBranchTextBox"
        Me.CurrentBranchTextBox.ReadOnly = True
        Me.CurrentBranchTextBox.Size = New System.Drawing.Size(373, 20)
        Me.CurrentBranchTextBox.TabIndex = 4
        '
        'RootPatchDirTextBox
        '
        Me.RootPatchDirTextBox.Location = New System.Drawing.Point(103, 106)
        Me.RootPatchDirTextBox.Name = "RootPatchDirTextBox"
        Me.RootPatchDirTextBox.ReadOnly = True
        Me.RootPatchDirTextBox.Size = New System.Drawing.Size(373, 20)
        Me.RootPatchDirTextBox.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Root Patch Dir"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 161)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Database"
        '
        'DBListComboBox
        '
        Me.DBListComboBox.FormattingEnabled = True
        Me.DBListComboBox.Location = New System.Drawing.Point(103, 158)
        Me.DBListComboBox.Name = "DBListComboBox"
        Me.DBListComboBox.Size = New System.Drawing.Size(373, 21)
        Me.DBListComboBox.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 214)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Application"
        '
        'ApplicationListComboBox
        '
        Me.ApplicationListComboBox.FormattingEnabled = True
        Me.ApplicationListComboBox.Location = New System.Drawing.Point(103, 211)
        Me.ApplicationListComboBox.Name = "ApplicationListComboBox"
        Me.ApplicationListComboBox.Size = New System.Drawing.Size(373, 21)
        Me.ApplicationListComboBox.TabIndex = 9
        '
        'RootApexDirTextBox
        '
        Me.RootApexDirTextBox.Location = New System.Drawing.Point(103, 132)
        Me.RootApexDirTextBox.Name = "RootApexDirTextBox"
        Me.RootApexDirTextBox.ReadOnly = True
        Me.RootApexDirTextBox.Size = New System.Drawing.Size(373, 20)
        Me.RootApexDirTextBox.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 135)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Root Apex Dir"
        '
        'CurrentConnectionTextBox
        '
        Me.CurrentConnectionTextBox.Location = New System.Drawing.Point(103, 185)
        Me.CurrentConnectionTextBox.Name = "CurrentConnectionTextBox"
        Me.CurrentConnectionTextBox.ReadOnly = True
        Me.CurrentConnectionTextBox.Size = New System.Drawing.Size(373, 20)
        Me.CurrentConnectionTextBox.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 188)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Connection"
        '
        'ParsingSchemaTextBox
        '
        Me.ParsingSchemaTextBox.Location = New System.Drawing.Point(103, 316)
        Me.ParsingSchemaTextBox.Name = "ParsingSchemaTextBox"
        Me.ParsingSchemaTextBox.ReadOnly = True
        Me.ParsingSchemaTextBox.Size = New System.Drawing.Size(373, 20)
        Me.ParsingSchemaTextBox.TabIndex = 16
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 319)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Parsing Schema"
        '
        'BranchPathTextBox
        '
        Me.BranchPathTextBox.Location = New System.Drawing.Point(103, 54)
        Me.BranchPathTextBox.Name = "BranchPathTextBox"
        Me.BranchPathTextBox.ReadOnly = True
        Me.BranchPathTextBox.Size = New System.Drawing.Size(373, 20)
        Me.BranchPathTextBox.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 57)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Branch Path"
        '
        'MinPatchTextBox
        '
        Me.MinPatchTextBox.Location = New System.Drawing.Point(103, 342)
        Me.MinPatchTextBox.Name = "MinPatchTextBox"
        Me.MinPatchTextBox.ReadOnly = True
        Me.MinPatchTextBox.Size = New System.Drawing.Size(373, 20)
        Me.MinPatchTextBox.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 345)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Minimum Patch"
        '
        'ApexAppTextBox
        '
        Me.ApexAppTextBox.Location = New System.Drawing.Point(103, 290)
        Me.ApexAppTextBox.Name = "ApexAppTextBox"
        Me.ApexAppTextBox.ReadOnly = True
        Me.ApexAppTextBox.Size = New System.Drawing.Size(373, 20)
        Me.ApexAppTextBox.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 293)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Apex App"
        '
        'PatchSchemasTextBox
        '
        Me.PatchSchemasTextBox.Location = New System.Drawing.Point(103, 238)
        Me.PatchSchemasTextBox.Name = "PatchSchemasTextBox"
        Me.PatchSchemasTextBox.ReadOnly = True
        Me.PatchSchemasTextBox.Size = New System.Drawing.Size(373, 20)
        Me.PatchSchemasTextBox.TabIndex = 24
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 241)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Patch Schemas"
        '
        'PatchSetTextBox
        '
        Me.PatchSetTextBox.Location = New System.Drawing.Point(103, 264)
        Me.PatchSetTextBox.Name = "PatchSetTextBox"
        Me.PatchSetTextBox.ReadOnly = True
        Me.PatchSetTextBox.Size = New System.Drawing.Size(373, 20)
        Me.PatchSetTextBox.TabIndex = 26
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 267)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Patch Set"
        '
        'DBPatchSetToolStripMenuItem
        '
        Me.DBPatchSetToolStripMenuItem.Name = "DBPatchSetToolStripMenuItem"
        Me.DBPatchSetToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DBPatchSetToolStripMenuItem.Text = "DBPatchSet"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 377)
        Me.Controls.Add(Me.PatchSetTextBox)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.PatchSchemasTextBox)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.ApexAppTextBox)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.MinPatchTextBox)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.BranchPathTextBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ParsingSchemaTextBox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CurrentConnectionTextBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.RootApexDirTextBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ApplicationListComboBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DBListComboBox)
        Me.Controls.Add(Me.RootPatchDirTextBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CurrentBranchTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RepoComboBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Main"
        Me.Text = "Main"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents GITToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RepoComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PatchFromTagsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CurrentBranchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RootPatchDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PatchRunnerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DBListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents APEXToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ApplicationListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents RootApexDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CurrentConnectionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ParsingSchemaTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BranchPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GITToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MergeAndPushFeatureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewFeatureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MinPatchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CreateDBPatchSetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ApexAppTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PatchSchemasTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PatchSetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DBPatchSetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
