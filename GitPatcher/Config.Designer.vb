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
        Dim Label11 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Repo1Label As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MailTabPage = New System.Windows.Forms.TabPage()
        Me.RecipientDomainTextBox = New System.Windows.Forms.TextBox()
        Me.SMTPportTextBox = New System.Windows.Forms.TextBox()
        Me.SMTPhostTextBox = New System.Windows.Forms.TextBox()
        Me.RecipientTextBox = New System.Windows.Forms.TextBox()
        Me.ApexTabPage = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ParsingSchemaTextbox = New System.Windows.Forms.TextBox()
        Me.AppListTextBox = New System.Windows.Forms.TextBox()
        Me.PatchTabPage = New System.Windows.Forms.TabPage()
        Me.SQLpathTextBox = New System.Windows.Forms.TextBox()
        Me.OJDBCjarFileTextBox = New System.Windows.Forms.TextBox()
        Me.ApexOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.PatchOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.DBTabPage = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ConnectionTextBox = New System.Windows.Forms.TextBox()
        Me.DBListTextBox = New System.Windows.Forms.TextBox()
        Me.TabPageGitRepo = New System.Windows.Forms.TabPage()
        Me.RepoListTextBox = New System.Windows.Forms.TextBox()
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.TestMailButton = New System.Windows.Forms.Button()
        Label11 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Repo1Label = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MailTabPage.SuspendLayout()
        Me.ApexTabPage.SuspendLayout()
        Me.PatchTabPage.SuspendLayout()
        Me.DBTabPage.SuspendLayout()
        Me.TabPageGitRepo.SuspendLayout()
        Me.ConfigTabs.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Location = New System.Drawing.Point(5, 149)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(74, 13)
        Label11.TabIndex = 8
        Label11.Text = "Recipient List:"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.Location = New System.Drawing.Point(5, 16)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(65, 13)
        Label13.TabIndex = 12
        Label13.Text = "SMTP Host:"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(5, 58)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(62, 13)
        Label12.TabIndex = 14
        Label12.Text = "SMTP Port:"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(5, 16)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(48, 13)
        Label9.TabIndex = 13
        Label9.Text = "App List:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(228, 16)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(106, 13)
        Label8.TabIndex = 15
        Label8.Text = "Parsing Schema List:"
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
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(5, 58)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(117, 13)
        Label2.TabIndex = 10
        Label2.Text = "Apex Dir Relative Path:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(5, 98)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(75, 13)
        Label10.TabIndex = 12
        Label10.Text = "OJDBC jar file:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(5, 140)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(76, 13)
        Label4.TabIndex = 8
        Label4.Text = "SQL exe Path:"
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
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(230, 16)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(69, 13)
        Label5.TabIndex = 10
        Label5.Text = "Connect List:"
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
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(5, 101)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(131, 13)
        Label14.TabIndex = 16
        Label14.Text = "Recipient Default Domain:"
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'MailTabPage
        '
        Me.MailTabPage.Controls.Add(Me.TestMailButton)
        Me.MailTabPage.Controls.Add(Me.RecipientDomainTextBox)
        Me.MailTabPage.Controls.Add(Label14)
        Me.MailTabPage.Controls.Add(Me.SMTPportTextBox)
        Me.MailTabPage.Controls.Add(Me.SMTPhostTextBox)
        Me.MailTabPage.Controls.Add(Me.RecipientTextBox)
        Me.MailTabPage.Controls.Add(Label12)
        Me.MailTabPage.Controls.Add(Label13)
        Me.MailTabPage.Controls.Add(Label11)
        Me.MailTabPage.Location = New System.Drawing.Point(4, 22)
        Me.MailTabPage.Name = "MailTabPage"
        Me.MailTabPage.Size = New System.Drawing.Size(458, 280)
        Me.MailTabPage.TabIndex = 5
        Me.MailTabPage.Text = "Mail"
        Me.MailTabPage.UseVisualStyleBackColor = True
        '
        'RecipientDomainTextBox
        '
        Me.RecipientDomainTextBox.Location = New System.Drawing.Point(8, 117)
        Me.RecipientDomainTextBox.Name = "RecipientDomainTextBox"
        Me.RecipientDomainTextBox.Size = New System.Drawing.Size(444, 20)
        Me.RecipientDomainTextBox.TabIndex = 17
        '
        'SMTPportTextBox
        '
        Me.SMTPportTextBox.Location = New System.Drawing.Point(8, 74)
        Me.SMTPportTextBox.Name = "SMTPportTextBox"
        Me.SMTPportTextBox.Size = New System.Drawing.Size(444, 20)
        Me.SMTPportTextBox.TabIndex = 15
        '
        'SMTPhostTextBox
        '
        Me.SMTPhostTextBox.Location = New System.Drawing.Point(8, 32)
        Me.SMTPhostTextBox.Name = "SMTPhostTextBox"
        Me.SMTPhostTextBox.Size = New System.Drawing.Size(444, 20)
        Me.SMTPhostTextBox.TabIndex = 13
        '
        'RecipientTextBox
        '
        Me.RecipientTextBox.Location = New System.Drawing.Point(8, 165)
        Me.RecipientTextBox.Multiline = True
        Me.RecipientTextBox.Name = "RecipientTextBox"
        Me.RecipientTextBox.Size = New System.Drawing.Size(397, 104)
        Me.RecipientTextBox.TabIndex = 9
        '
        'ApexTabPage
        '
        Me.ApexTabPage.Controls.Add(Me.Label7)
        Me.ApexTabPage.Controls.Add(Me.ParsingSchemaTextbox)
        Me.ApexTabPage.Controls.Add(Me.AppListTextBox)
        Me.ApexTabPage.Controls.Add(Label8)
        Me.ApexTabPage.Controls.Add(Label9)
        Me.ApexTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ApexTabPage.Name = "ApexTabPage"
        Me.ApexTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ApexTabPage.Size = New System.Drawing.Size(458, 280)
        Me.ApexTabPage.TabIndex = 4
        Me.ApexTabPage.Text = "Apex"
        Me.ApexTabPage.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 264)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(219, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Ensure there is a Schema for each Apex App"
        '
        'ParsingSchemaTextbox
        '
        Me.ParsingSchemaTextbox.Location = New System.Drawing.Point(231, 32)
        Me.ParsingSchemaTextbox.Multiline = True
        Me.ParsingSchemaTextbox.Name = "ParsingSchemaTextbox"
        Me.ParsingSchemaTextbox.Size = New System.Drawing.Size(219, 229)
        Me.ParsingSchemaTextbox.TabIndex = 16
        '
        'AppListTextBox
        '
        Me.AppListTextBox.Location = New System.Drawing.Point(8, 32)
        Me.AppListTextBox.Multiline = True
        Me.AppListTextBox.Name = "AppListTextBox"
        Me.AppListTextBox.Size = New System.Drawing.Size(219, 229)
        Me.AppListTextBox.TabIndex = 14
        '
        'PatchTabPage
        '
        Me.PatchTabPage.Controls.Add(Me.SQLpathTextBox)
        Me.PatchTabPage.Controls.Add(Me.OJDBCjarFileTextBox)
        Me.PatchTabPage.Controls.Add(Me.ApexOffsetTextBox)
        Me.PatchTabPage.Controls.Add(Me.PatchOffsetTextBox)
        Me.PatchTabPage.Controls.Add(Label4)
        Me.PatchTabPage.Controls.Add(Label10)
        Me.PatchTabPage.Controls.Add(Label2)
        Me.PatchTabPage.Controls.Add(Label3)
        Me.PatchTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchTabPage.Name = "PatchTabPage"
        Me.PatchTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PatchTabPage.Size = New System.Drawing.Size(458, 280)
        Me.PatchTabPage.TabIndex = 1
        Me.PatchTabPage.Text = "Paths"
        Me.PatchTabPage.UseVisualStyleBackColor = True
        '
        'SQLpathTextBox
        '
        Me.SQLpathTextBox.Location = New System.Drawing.Point(8, 156)
        Me.SQLpathTextBox.Name = "SQLpathTextBox"
        Me.SQLpathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.SQLpathTextBox.TabIndex = 9
        '
        'OJDBCjarFileTextBox
        '
        Me.OJDBCjarFileTextBox.Location = New System.Drawing.Point(8, 114)
        Me.OJDBCjarFileTextBox.Name = "OJDBCjarFileTextBox"
        Me.OJDBCjarFileTextBox.Size = New System.Drawing.Size(444, 20)
        Me.OJDBCjarFileTextBox.TabIndex = 13
        '
        'ApexOffsetTextBox
        '
        Me.ApexOffsetTextBox.Location = New System.Drawing.Point(8, 74)
        Me.ApexOffsetTextBox.Name = "ApexOffsetTextBox"
        Me.ApexOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.ApexOffsetTextBox.TabIndex = 11
        '
        'PatchOffsetTextBox
        '
        Me.PatchOffsetTextBox.Location = New System.Drawing.Point(8, 32)
        Me.PatchOffsetTextBox.Name = "PatchOffsetTextBox"
        Me.PatchOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.PatchOffsetTextBox.TabIndex = 9
        '
        'DBTabPage
        '
        Me.DBTabPage.Controls.Add(Me.Label6)
        Me.DBTabPage.Controls.Add(Me.ConnectionTextBox)
        Me.DBTabPage.Controls.Add(Me.DBListTextBox)
        Me.DBTabPage.Controls.Add(Label5)
        Me.DBTabPage.Controls.Add(Label1)
        Me.DBTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DBTabPage.Name = "DBTabPage"
        Me.DBTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DBTabPage.Size = New System.Drawing.Size(458, 280)
        Me.DBTabPage.TabIndex = 3
        Me.DBTabPage.Text = "Databases"
        Me.DBTabPage.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 264)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(234, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Ensure there is a Connection for each Database"
        '
        'ConnectionTextBox
        '
        Me.ConnectionTextBox.Location = New System.Drawing.Point(233, 32)
        Me.ConnectionTextBox.Multiline = True
        Me.ConnectionTextBox.Name = "ConnectionTextBox"
        Me.ConnectionTextBox.Size = New System.Drawing.Size(219, 229)
        Me.ConnectionTextBox.TabIndex = 11
        '
        'DBListTextBox
        '
        Me.DBListTextBox.Location = New System.Drawing.Point(8, 32)
        Me.DBListTextBox.Multiline = True
        Me.DBListTextBox.Name = "DBListTextBox"
        Me.DBListTextBox.Size = New System.Drawing.Size(219, 229)
        Me.DBListTextBox.TabIndex = 9
        '
        'TabPageGitRepo
        '
        Me.TabPageGitRepo.AutoScroll = True
        Me.TabPageGitRepo.Controls.Add(Me.RepoListTextBox)
        Me.TabPageGitRepo.Controls.Add(Repo1Label)
        Me.TabPageGitRepo.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGitRepo.Name = "TabPageGitRepo"
        Me.TabPageGitRepo.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGitRepo.Size = New System.Drawing.Size(458, 280)
        Me.TabPageGitRepo.TabIndex = 0
        Me.TabPageGitRepo.Text = "Git Repos"
        Me.TabPageGitRepo.UseVisualStyleBackColor = True
        '
        'RepoListTextBox
        '
        Me.RepoListTextBox.Location = New System.Drawing.Point(8, 32)
        Me.RepoListTextBox.Multiline = True
        Me.RepoListTextBox.Name = "RepoListTextBox"
        Me.RepoListTextBox.Size = New System.Drawing.Size(444, 242)
        Me.RepoListTextBox.TabIndex = 7
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPageGitRepo)
        Me.ConfigTabs.Controls.Add(Me.DBTabPage)
        Me.ConfigTabs.Controls.Add(Me.PatchTabPage)
        Me.ConfigTabs.Controls.Add(Me.ApexTabPage)
        Me.ConfigTabs.Controls.Add(Me.MailTabPage)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 28)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(466, 306)
        Me.ConfigTabs.TabIndex = 0
        '
        'TestMailButton
        '
        Me.TestMailButton.Location = New System.Drawing.Point(411, 165)
        Me.TestMailButton.Name = "TestMailButton"
        Me.TestMailButton.Size = New System.Drawing.Size(41, 104)
        Me.TestMailButton.TabIndex = 18
        Me.TestMailButton.Text = "Send Test Mail"
        Me.TestMailButton.UseVisualStyleBackColor = True
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 346)
        Me.Controls.Add(Me.ConfigTabs)
        Me.Name = "Config"
        Me.Text = "Config"
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MailTabPage.ResumeLayout(False)
        Me.MailTabPage.PerformLayout()
        Me.ApexTabPage.ResumeLayout(False)
        Me.ApexTabPage.PerformLayout()
        Me.PatchTabPage.ResumeLayout(False)
        Me.PatchTabPage.PerformLayout()
        Me.DBTabPage.ResumeLayout(False)
        Me.DBTabPage.PerformLayout()
        Me.TabPageGitRepo.ResumeLayout(False)
        Me.TabPageGitRepo.PerformLayout()
        Me.ConfigTabs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MailTabPage As System.Windows.Forms.TabPage
    Friend WithEvents RecipientDomainTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SMTPportTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SMTPhostTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RecipientTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ApexTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ParsingSchemaTextbox As System.Windows.Forms.TextBox
    Friend WithEvents AppListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SQLpathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OJDBCjarFileTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ApexOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ConnectionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabPageGitRepo As System.Windows.Forms.TabPage
    Friend WithEvents RepoListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents TestMailButton As System.Windows.Forms.Button
End Class
