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
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.RepoListTextBox = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.PatchOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.SQLpathTextBox = New System.Windows.Forms.TextBox()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Repo1Label = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Me.ConfigTabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Repo1Label
        '
        Repo1Label.AutoSize = True
        Repo1Label.Location = New System.Drawing.Point(7, 9)
        Repo1Label.Name = "Repo1Label"
        Repo1Label.Size = New System.Drawing.Size(76, 13)
        Repo1Label.TabIndex = 0
        Repo1Label.Text = "Repository List"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(5, 16)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(69, 13)
        Label3.TabIndex = 8
        Label3.Text = "Patch Offset:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(6, 12)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(32, 13)
        Label4.TabIndex = 8
        Label4.Text = "Path:"
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPage1)
        Me.ConfigTabs.Controls.Add(Me.TabPage2)
        Me.ConfigTabs.Controls.Add(Me.TabPage3)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 28)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(466, 234)
        Me.ConfigTabs.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.RepoListTextBox)
        Me.TabPage1.Controls.Add(Repo1Label)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(458, 208)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Git Repos"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'RepoListTextBox
        '
        Me.RepoListTextBox.Location = New System.Drawing.Point(10, 25)
        Me.RepoListTextBox.Multiline = True
        Me.RepoListTextBox.Name = "RepoListTextBox"
        Me.RepoListTextBox.Size = New System.Drawing.Size(432, 175)
        Me.RepoListTextBox.TabIndex = 7
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.PatchOffsetTextBox)
        Me.TabPage2.Controls.Add(Label3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(458, 208)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Patch"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'PatchOffsetTextBox
        '
        Me.PatchOffsetTextBox.Location = New System.Drawing.Point(80, 16)
        Me.PatchOffsetTextBox.Name = "PatchOffsetTextBox"
        Me.PatchOffsetTextBox.Size = New System.Drawing.Size(332, 20)
        Me.PatchOffsetTextBox.TabIndex = 9
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.SQLpathTextBox)
        Me.TabPage3.Controls.Add(Label4)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(458, 208)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "SQL"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'SQLpathTextBox
        '
        Me.SQLpathTextBox.Location = New System.Drawing.Point(55, 9)
        Me.SQLpathTextBox.Name = "SQLpathTextBox"
        Me.SQLpathTextBox.Size = New System.Drawing.Size(359, 20)
        Me.SQLpathTextBox.TabIndex = 9
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
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RepoListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents PatchOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents SQLpathTextBox As System.Windows.Forms.TextBox
End Class
