<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config
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
        Dim Repo1Label As System.Windows.Forms.Label
        Dim Repo2Label As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Repo4TextBox = New System.Windows.Forms.TextBox()
        Me.Repo1TextBox = New System.Windows.Forms.TextBox()
        Me.Repo2TextBox = New System.Windows.Forms.TextBox()
        Me.Repo3TextBox = New System.Windows.Forms.TextBox()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Repo1Label = New System.Windows.Forms.Label()
        Repo2Label = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Me.ConfigTabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Repo1Label
        '
        Repo1Label.AutoSize = True
        Repo1Label.Location = New System.Drawing.Point(7, 9)
        Repo1Label.Name = "Repo1Label"
        Repo1Label.Size = New System.Drawing.Size(42, 13)
        Repo1Label.TabIndex = 0
        Repo1Label.Text = "Repo1:"
        '
        'Repo2Label
        '
        Repo2Label.AutoSize = True
        Repo2Label.Location = New System.Drawing.Point(7, 38)
        Repo2Label.Name = "Repo2Label"
        Repo2Label.Size = New System.Drawing.Size(42, 13)
        Repo2Label.TabIndex = 2
        Repo2Label.Text = "Repo2:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(7, 64)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(42, 13)
        Label1.TabIndex = 5
        Label1.Text = "Repo3:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(7, 90)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(42, 13)
        Label2.TabIndex = 9
        Label2.Text = "Repo4:"
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPage1)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 28)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(466, 234)
        Me.ConfigTabs.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Label2)
        Me.TabPage1.Controls.Add(Me.Repo4TextBox)
        Me.TabPage1.Controls.Add(Me.Repo1TextBox)
        Me.TabPage1.Controls.Add(Me.Repo2TextBox)
        Me.TabPage1.Controls.Add(Label1)
        Me.TabPage1.Controls.Add(Me.Repo3TextBox)
        Me.TabPage1.Controls.Add(Repo2Label)
        Me.TabPage1.Controls.Add(Repo1Label)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(458, 208)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Git Repos"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Repo4TextBox
        '
        Me.Repo4TextBox.Location = New System.Drawing.Point(55, 87)
        Me.Repo4TextBox.Name = "Repo4TextBox"
        Me.Repo4TextBox.Size = New System.Drawing.Size(359, 20)
        Me.Repo4TextBox.TabIndex = 8
        '
        'Repo1TextBox
        '
        Me.Repo1TextBox.Location = New System.Drawing.Point(55, 9)
        Me.Repo1TextBox.Name = "Repo1TextBox"
        Me.Repo1TextBox.Size = New System.Drawing.Size(359, 20)
        Me.Repo1TextBox.TabIndex = 7
        '
        'Repo2TextBox
        '
        Me.Repo2TextBox.Location = New System.Drawing.Point(55, 35)
        Me.Repo2TextBox.Name = "Repo2TextBox"
        Me.Repo2TextBox.Size = New System.Drawing.Size(359, 20)
        Me.Repo2TextBox.TabIndex = 6
        '
        'Repo3TextBox
        '
        Me.Repo3TextBox.Location = New System.Drawing.Point(55, 61)
        Me.Repo3TextBox.Name = "Repo3TextBox"
        Me.Repo3TextBox.Size = New System.Drawing.Size(359, 20)
        Me.Repo3TextBox.TabIndex = 4
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 262)
        Me.Controls.Add(Me.ConfigTabs)
        Me.Name = "Config"
        Me.Text = "Config"
        Me.ConfigTabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Repo3TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Repo1TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Repo2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Repo4TextBox As System.Windows.Forms.TextBox
End Class
