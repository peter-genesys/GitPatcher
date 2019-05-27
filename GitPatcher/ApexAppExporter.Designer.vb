<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApexAppExporter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApexAppExporter))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.KnownAppsTreeView = New TreeViewEnhanced.TreeViewEnhanced()
        Me.ExportApexAppsButton = New System.Windows.Forms.Button()
        Me.SearchTypeGroupBox = New System.Windows.Forms.GroupBox()
        Me.DBRadioButton = New System.Windows.Forms.RadioButton()
        Me.RepoRadioButton = New System.Windows.Forms.RadioButton()
        Me.SearchTypeGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(221, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Found Apex Apps - Auto ticks changed apps."
        '
        'KnownAppsTreeView
        '
        Me.KnownAppsTreeView.BackColor = System.Drawing.Color.Wheat
        Me.KnownAppsTreeView.CheckBoxes = True
        Me.KnownAppsTreeView.Location = New System.Drawing.Point(21, 126)
        Me.KnownAppsTreeView.Name = "KnownAppsTreeView"
        Me.KnownAppsTreeView.Size = New System.Drawing.Size(429, 595)
        Me.KnownAppsTreeView.TabIndex = 63
        '
        'ExportApexAppsButton
        '
        Me.ExportApexAppsButton.Location = New System.Drawing.Point(311, 48)
        Me.ExportApexAppsButton.Name = "ExportApexAppsButton"
        Me.ExportApexAppsButton.Size = New System.Drawing.Size(139, 23)
        Me.ExportApexAppsButton.TabIndex = 64
        Me.ExportApexAppsButton.Text = "Export Apex Apps"
        Me.ExportApexAppsButton.UseVisualStyleBackColor = True
        '
        'SearchTypeGroupBox
        '
        Me.SearchTypeGroupBox.Controls.Add(Me.DBRadioButton)
        Me.SearchTypeGroupBox.Controls.Add(Me.RepoRadioButton)
        Me.SearchTypeGroupBox.Location = New System.Drawing.Point(22, 12)
        Me.SearchTypeGroupBox.Name = "SearchTypeGroupBox"
        Me.SearchTypeGroupBox.Size = New System.Drawing.Size(273, 75)
        Me.SearchTypeGroupBox.TabIndex = 68
        Me.SearchTypeGroupBox.TabStop = False
        Me.SearchTypeGroupBox.Text = "Search Type"
        '
        'DBRadioButton
        '
        Me.DBRadioButton.AutoSize = True
        Me.DBRadioButton.Location = New System.Drawing.Point(20, 42)
        Me.DBRadioButton.Name = "DBRadioButton"
        Me.DBRadioButton.Size = New System.Drawing.Size(230, 17)
        Me.DBRadioButton.TabIndex = 1
        Me.DBRadioButton.Text = "Database - Apps in all granted Workspaces"
        Me.DBRadioButton.UseVisualStyleBackColor = True
        '
        'RepoRadioButton
        '
        Me.RepoRadioButton.AutoSize = True
        Me.RepoRadioButton.Checked = True
        Me.RepoRadioButton.Location = New System.Drawing.Point(20, 19)
        Me.RepoRadioButton.Name = "RepoRadioButton"
        Me.RepoRadioButton.Size = New System.Drawing.Size(193, 17)
        Me.RepoRadioButton.TabIndex = 0
        Me.RepoRadioButton.TabStop = True
        Me.RepoRadioButton.Text = "Repo - App folders in the Filesystem"
        Me.RepoRadioButton.UseVisualStyleBackColor = True
        '
        'ApexAppExporter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 756)
        Me.Controls.Add(Me.SearchTypeGroupBox)
        Me.Controls.Add(Me.ExportApexAppsButton)
        Me.Controls.Add(Me.KnownAppsTreeView)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ApexAppExporter"
        Me.Text = "ApexAppExporter"
        Me.SearchTypeGroupBox.ResumeLayout(False)
        Me.SearchTypeGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents KnownAppsTreeView As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents ExportApexAppsButton As Button
    Friend WithEvents SearchTypeGroupBox As GroupBox
    Friend WithEvents DBRadioButton As RadioButton
    Friend WithEvents RepoRadioButton As RadioButton
End Class
