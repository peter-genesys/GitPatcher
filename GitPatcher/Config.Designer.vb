﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Dim Label1 As System.Windows.Forms.Label
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.TabPageGitRepo = New System.Windows.Forms.TabPage()
        Me.RepoListTextBox = New System.Windows.Forms.TextBox()
        Me.PatchTabPage = New System.Windows.Forms.TabPage()
        Me.PatchOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.SQLTabPage = New System.Windows.Forms.TabPage()
        Me.SQLpathTextBox = New System.Windows.Forms.TextBox()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DBTabPage = New System.Windows.Forms.TabPage()
        Me.DBListTextBox = New System.Windows.Forms.TextBox()
        Repo1Label = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Me.ConfigTabs.SuspendLayout()
        Me.TabPageGitRepo.SuspendLayout()
        Me.PatchTabPage.SuspendLayout()
        Me.SQLTabPage.SuspendLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DBTabPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'Repo1Label
        '
        Repo1Label.AutoSize = True
        Repo1Label.Location = New System.Drawing.Point(5, 16)
        Repo1Label.Name = "Repo1Label"
        Repo1Label.Size = New System.Drawing.Size(79, 13)
        Repo1Label.TabIndex = 0
        Repo1Label.Text = "Repository List:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(5, 16)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(121, 13)
        Label3.TabIndex = 8
        Label3.Text = "Patch Dir Relative Path:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(5, 16)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(76, 13)
        Label4.TabIndex = 8
        Label4.Text = "SQL exe Path:"
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPageGitRepo)
        Me.ConfigTabs.Controls.Add(Me.DBTabPage)
        Me.ConfigTabs.Controls.Add(Me.SQLTabPage)
        Me.ConfigTabs.Controls.Add(Me.PatchTabPage)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 28)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(466, 234)
        Me.ConfigTabs.TabIndex = 0
        '
        'TabPageGitRepo
        '
        Me.TabPageGitRepo.AutoScroll = True
        Me.TabPageGitRepo.Controls.Add(Me.RepoListTextBox)
        Me.TabPageGitRepo.Controls.Add(Repo1Label)
        Me.TabPageGitRepo.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGitRepo.Name = "TabPageGitRepo"
        Me.TabPageGitRepo.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGitRepo.Size = New System.Drawing.Size(458, 208)
        Me.TabPageGitRepo.TabIndex = 0
        Me.TabPageGitRepo.Text = "Git Repos"
        Me.TabPageGitRepo.UseVisualStyleBackColor = True
        '
        'RepoListTextBox
        '
        Me.RepoListTextBox.Location = New System.Drawing.Point(8, 32)
        Me.RepoListTextBox.Multiline = True
        Me.RepoListTextBox.Name = "RepoListTextBox"
        Me.RepoListTextBox.Size = New System.Drawing.Size(444, 163)
        Me.RepoListTextBox.TabIndex = 7
        '
        'PatchTabPage
        '
        Me.PatchTabPage.Controls.Add(Me.PatchOffsetTextBox)
        Me.PatchTabPage.Controls.Add(Label3)
        Me.PatchTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchTabPage.Name = "PatchTabPage"
        Me.PatchTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PatchTabPage.Size = New System.Drawing.Size(458, 208)
        Me.PatchTabPage.TabIndex = 1
        Me.PatchTabPage.Text = "Patch"
        Me.PatchTabPage.UseVisualStyleBackColor = True
        '
        'PatchOffsetTextBox
        '
        Me.PatchOffsetTextBox.Location = New System.Drawing.Point(8, 32)
        Me.PatchOffsetTextBox.Name = "PatchOffsetTextBox"
        Me.PatchOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.PatchOffsetTextBox.TabIndex = 9
        '
        'SQLTabPage
        '
        Me.SQLTabPage.Controls.Add(Me.SQLpathTextBox)
        Me.SQLTabPage.Controls.Add(Label4)
        Me.SQLTabPage.Location = New System.Drawing.Point(4, 22)
        Me.SQLTabPage.Name = "SQLTabPage"
        Me.SQLTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SQLTabPage.Size = New System.Drawing.Size(458, 208)
        Me.SQLTabPage.TabIndex = 2
        Me.SQLTabPage.Text = "SQL"
        Me.SQLTabPage.UseVisualStyleBackColor = True
        '
        'SQLpathTextBox
        '
        Me.SQLpathTextBox.Location = New System.Drawing.Point(8, 32)
        Me.SQLpathTextBox.Name = "SQLpathTextBox"
        Me.SQLpathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.SQLpathTextBox.TabIndex = 9
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'DBTabPage
        '
        Me.DBTabPage.Controls.Add(Me.DBListTextBox)
        Me.DBTabPage.Controls.Add(Label1)
        Me.DBTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DBTabPage.Name = "DBTabPage"
        Me.DBTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DBTabPage.Size = New System.Drawing.Size(458, 208)
        Me.DBTabPage.TabIndex = 3
        Me.DBTabPage.Text = "Databases"
        Me.DBTabPage.UseVisualStyleBackColor = True
        '
        'DBListTextBox
        '
        Me.DBListTextBox.Location = New System.Drawing.Point(8, 32)
        Me.DBListTextBox.Multiline = True
        Me.DBListTextBox.Name = "DBListTextBox"
        Me.DBListTextBox.Size = New System.Drawing.Size(444, 163)
        Me.DBListTextBox.TabIndex = 9
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(5, 16)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(75, 13)
        Label1.TabIndex = 8
        Label1.Text = "Database List:"
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
        Me.TabPageGitRepo.ResumeLayout(False)
        Me.TabPageGitRepo.PerformLayout()
        Me.PatchTabPage.ResumeLayout(False)
        Me.PatchTabPage.PerformLayout()
        Me.SQLTabPage.ResumeLayout(False)
        Me.SQLTabPage.PerformLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DBTabPage.ResumeLayout(False)
        Me.DBTabPage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGitRepo As System.Windows.Forms.TabPage
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RepoListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchTabPage As System.Windows.Forms.TabPage
    Friend WithEvents PatchOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SQLTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SQLpathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBTabPage As System.Windows.Forms.TabPage
    Friend WithEvents DBListTextBox As System.Windows.Forms.TextBox
End Class
