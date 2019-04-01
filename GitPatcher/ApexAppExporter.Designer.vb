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
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Known Apex Apps"
        '
        'KnownAppsTreeView
        '
        Me.KnownAppsTreeView.BackColor = System.Drawing.Color.Wheat
        Me.KnownAppsTreeView.CheckBoxes = True
        Me.KnownAppsTreeView.Location = New System.Drawing.Point(7, 87)
        Me.KnownAppsTreeView.Name = "KnownAppsTreeView"
        Me.KnownAppsTreeView.Size = New System.Drawing.Size(429, 595)
        Me.KnownAppsTreeView.TabIndex = 63
        '
        'ExportApexAppsButton
        '
        Me.ExportApexAppsButton.Location = New System.Drawing.Point(8, 33)
        Me.ExportApexAppsButton.Name = "ExportApexAppsButton"
        Me.ExportApexAppsButton.Size = New System.Drawing.Size(139, 23)
        Me.ExportApexAppsButton.TabIndex = 64
        Me.ExportApexAppsButton.Text = "Export Apex Apps"
        Me.ExportApexAppsButton.UseVisualStyleBackColor = True
        '
        'ApexAppExporter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 756)
        Me.Controls.Add(Me.ExportApexAppsButton)
        Me.Controls.Add(Me.KnownAppsTreeView)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ApexAppExporter"
        Me.Text = "ApexAppExporter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents KnownAppsTreeView As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents ExportApexAppsButton As Button
End Class
