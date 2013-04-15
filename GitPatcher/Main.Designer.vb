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
        Me.APEXToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RepoComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CurrentBranchTextBox = New System.Windows.Forms.TextBox()
        Me.RootPatchDirTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DBListComboBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ApexListComboBox = New System.Windows.Forms.ComboBox()
        Me.RootApexDirTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GITToolStripMenuItem, Me.APEXToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(488, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GITToolStripMenuItem
        '
        Me.GITToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PatchFromTagsToolStripMenuItem, Me.PatchRunnerToolStripMenuItem})
        Me.GITToolStripMenuItem.Name = "GITToolStripMenuItem"
        Me.GITToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.GITToolStripMenuItem.Text = "GIT"
        '
        'PatchFromTagsToolStripMenuItem
        '
        Me.PatchFromTagsToolStripMenuItem.Name = "PatchFromTagsToolStripMenuItem"
        Me.PatchFromTagsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.PatchFromTagsToolStripMenuItem.Text = "PatchFromTags"
        '
        'PatchRunnerToolStripMenuItem
        '
        Me.PatchRunnerToolStripMenuItem.Name = "PatchRunnerToolStripMenuItem"
        Me.PatchRunnerToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.PatchRunnerToolStripMenuItem.Text = "PatchRunner"
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
        Me.Label1.Location = New System.Drawing.Point(23, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Current Repo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Current Branch"
        '
        'CurrentBranchTextBox
        '
        Me.CurrentBranchTextBox.Location = New System.Drawing.Point(103, 106)
        Me.CurrentBranchTextBox.Name = "CurrentBranchTextBox"
        Me.CurrentBranchTextBox.ReadOnly = True
        Me.CurrentBranchTextBox.Size = New System.Drawing.Size(373, 20)
        Me.CurrentBranchTextBox.TabIndex = 4
        '
        'RootPatchDirTextBox
        '
        Me.RootPatchDirTextBox.Location = New System.Drawing.Point(103, 54)
        Me.RootPatchDirTextBox.Name = "RootPatchDirTextBox"
        Me.RootPatchDirTextBox.ReadOnly = True
        Me.RootPatchDirTextBox.Size = New System.Drawing.Size(373, 20)
        Me.RootPatchDirTextBox.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Root Patch Dir"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Current DB"
        '
        'DBListComboBox
        '
        Me.DBListComboBox.FormattingEnabled = True
        Me.DBListComboBox.Location = New System.Drawing.Point(103, 132)
        Me.DBListComboBox.Name = "DBListComboBox"
        Me.DBListComboBox.Size = New System.Drawing.Size(373, 21)
        Me.DBListComboBox.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(23, 162)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Current Apex"
        '
        'ApexListComboBox
        '
        Me.ApexListComboBox.FormattingEnabled = True
        Me.ApexListComboBox.Location = New System.Drawing.Point(103, 159)
        Me.ApexListComboBox.Name = "ApexListComboBox"
        Me.ApexListComboBox.Size = New System.Drawing.Size(373, 21)
        Me.ApexListComboBox.TabIndex = 9
        '
        'RootApexDirTextBox
        '
        Me.RootApexDirTextBox.Location = New System.Drawing.Point(103, 80)
        Me.RootApexDirTextBox.Name = "RootApexDirTextBox"
        Me.RootApexDirTextBox.ReadOnly = True
        Me.RootApexDirTextBox.Size = New System.Drawing.Size(373, 20)
        Me.RootApexDirTextBox.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Root Apex Dir"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 262)
        Me.Controls.Add(Me.RootApexDirTextBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ApexListComboBox)
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
    Friend WithEvents ApexListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents RootApexDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
