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
        Me.RepoComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PatchFromTagsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CurrentBranchTextBox = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GITToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(488, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'GITToolStripMenuItem
        '
        Me.GITToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PatchFromTagsToolStripMenuItem})
        Me.GITToolStripMenuItem.Name = "GITToolStripMenuItem"
        Me.GITToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.GITToolStripMenuItem.Text = "GIT"
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
        'PatchFromTagsToolStripMenuItem
        '
        Me.PatchFromTagsToolStripMenuItem.Name = "PatchFromTagsToolStripMenuItem"
        Me.PatchFromTagsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.PatchFromTagsToolStripMenuItem.Text = "PatchFromTags"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Current Branch"
        '
        'CurrentBranchTextBox
        '
        Me.CurrentBranchTextBox.Location = New System.Drawing.Point(103, 58)
        Me.CurrentBranchTextBox.Name = "CurrentBranchTextBox"
        Me.CurrentBranchTextBox.ReadOnly = True
        Me.CurrentBranchTextBox.Size = New System.Drawing.Size(373, 20)
        Me.CurrentBranchTextBox.TabIndex = 4
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 262)
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

End Class
