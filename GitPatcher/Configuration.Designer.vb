<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configuration
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
        Dim Label4 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Configuration))
        Me.MailTabPage = New System.Windows.Forms.TabPage()
        Me.TestMailButton = New System.Windows.Forms.Button()
        Me.RecipientDomainTextBox = New System.Windows.Forms.TextBox()
        Me.SMTPportTextBox = New System.Windows.Forms.TextBox()
        Me.SMTPhostTextBox = New System.Windows.Forms.TextBox()
        Me.RecipientTextBox = New System.Windows.Forms.TextBox()
        Me.PatchTabPage = New System.Windows.Forms.TabPage()
        Me.GPScriptsDirTextBox = New System.Windows.Forms.TextBox()
        Me.RunConfigDirTextBox = New System.Windows.Forms.TextBox()
        Me.XMLButton = New System.Windows.Forms.Button()
        Me.XMLRepoFilePathTextBox = New System.Windows.Forms.TextBox()
        Me.GitExeTextBox = New System.Windows.Forms.TextBox()
        Me.SQLpathTextBox = New System.Windows.Forms.TextBox()
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.VBoxDirTextBox = New System.Windows.Forms.TextBox()
        Label11 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Me.MailTabPage.SuspendLayout()
        Me.PatchTabPage.SuspendLayout()
        Me.ConfigTabs.SuspendLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(5, 14)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(114, 13)
        Label4.TabIndex = 8
        Label4.Text = "SQL.exe Path (SQLcl):"
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
        'Label18
        '
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(6, 53)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(68, 13)
        Label18.TabIndex = 14
        Label18.Text = "Git.exe Path:"
        '
        'Label24
        '
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(2, 230)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(97, 13)
        Label24.TabIndex = 18
        Label24.Text = "GitRepos.xml Path:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(2, 191)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(74, 13)
        Label1.TabIndex = 54
        Label1.Text = "GP Config Dir:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(3, 152)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(76, 13)
        Label2.TabIndex = 56
        Label2.Text = "GP Scripts Dir:"
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
        Me.MailTabPage.Size = New System.Drawing.Size(534, 325)
        Me.MailTabPage.TabIndex = 5
        Me.MailTabPage.Text = "Mail"
        Me.MailTabPage.UseVisualStyleBackColor = True
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
        'PatchTabPage
        '
        Me.PatchTabPage.Controls.Add(Me.VBoxDirTextBox)
        Me.PatchTabPage.Controls.Add(Label3)
        Me.PatchTabPage.Controls.Add(Me.GPScriptsDirTextBox)
        Me.PatchTabPage.Controls.Add(Label2)
        Me.PatchTabPage.Controls.Add(Me.RunConfigDirTextBox)
        Me.PatchTabPage.Controls.Add(Label1)
        Me.PatchTabPage.Controls.Add(Me.XMLButton)
        Me.PatchTabPage.Controls.Add(Me.XMLRepoFilePathTextBox)
        Me.PatchTabPage.Controls.Add(Label24)
        Me.PatchTabPage.Controls.Add(Me.GitExeTextBox)
        Me.PatchTabPage.Controls.Add(Label18)
        Me.PatchTabPage.Controls.Add(Me.SQLpathTextBox)
        Me.PatchTabPage.Controls.Add(Label4)
        Me.PatchTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchTabPage.Name = "PatchTabPage"
        Me.PatchTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PatchTabPage.Size = New System.Drawing.Size(534, 325)
        Me.PatchTabPage.TabIndex = 1
        Me.PatchTabPage.Text = "Paths"
        Me.PatchTabPage.UseVisualStyleBackColor = True
        '
        'GPScriptsDirTextBox
        '
        Me.GPScriptsDirTextBox.Location = New System.Drawing.Point(6, 168)
        Me.GPScriptsDirTextBox.Name = "GPScriptsDirTextBox"
        Me.GPScriptsDirTextBox.Size = New System.Drawing.Size(444, 20)
        Me.GPScriptsDirTextBox.TabIndex = 57
        '
        'RunConfigDirTextBox
        '
        Me.RunConfigDirTextBox.Location = New System.Drawing.Point(5, 207)
        Me.RunConfigDirTextBox.Name = "RunConfigDirTextBox"
        Me.RunConfigDirTextBox.Size = New System.Drawing.Size(444, 20)
        Me.RunConfigDirTextBox.TabIndex = 55
        '
        'XMLButton
        '
        Me.XMLButton.Location = New System.Drawing.Point(6, 278)
        Me.XMLButton.Name = "XMLButton"
        Me.XMLButton.Size = New System.Drawing.Size(102, 23)
        Me.XMLButton.TabIndex = 53
        Me.XMLButton.Text = "Edit XML Config"
        Me.XMLButton.UseVisualStyleBackColor = True
        '
        'XMLRepoFilePathTextBox
        '
        Me.XMLRepoFilePathTextBox.Location = New System.Drawing.Point(5, 246)
        Me.XMLRepoFilePathTextBox.Name = "XMLRepoFilePathTextBox"
        Me.XMLRepoFilePathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.XMLRepoFilePathTextBox.TabIndex = 19
        '
        'GitExeTextBox
        '
        Me.GitExeTextBox.Location = New System.Drawing.Point(9, 69)
        Me.GitExeTextBox.Name = "GitExeTextBox"
        Me.GitExeTextBox.Size = New System.Drawing.Size(444, 20)
        Me.GitExeTextBox.TabIndex = 15
        '
        'SQLpathTextBox
        '
        Me.SQLpathTextBox.Location = New System.Drawing.Point(8, 30)
        Me.SQLpathTextBox.Name = "SQLpathTextBox"
        Me.SQLpathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.SQLpathTextBox.TabIndex = 9
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.PatchTabPage)
        Me.ConfigTabs.Controls.Add(Me.MailTabPage)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 28)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(542, 351)
        Me.ConfigTabs.TabIndex = 0
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'VBoxDirTextBox
        '
        Me.VBoxDirTextBox.Location = New System.Drawing.Point(8, 108)
        Me.VBoxDirTextBox.Name = "VBoxDirTextBox"
        Me.VBoxDirTextBox.Size = New System.Drawing.Size(444, 20)
        Me.VBoxDirTextBox.TabIndex = 59
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(5, 92)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(73, 13)
        Label3.TabIndex = 58
        Label3.Text = "VirtualBox Dir:"
        '
        'Configuration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 391)
        Me.Controls.Add(Me.ConfigTabs)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Configuration"
        Me.Text = "Config"
        Me.MailTabPage.ResumeLayout(False)
        Me.MailTabPage.PerformLayout()
        Me.PatchTabPage.ResumeLayout(False)
        Me.PatchTabPage.PerformLayout()
        Me.ConfigTabs.ResumeLayout(False)
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MailTabPage As System.Windows.Forms.TabPage
    Friend WithEvents RecipientDomainTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SMTPportTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SMTPhostTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RecipientTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SQLpathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents TestMailButton As System.Windows.Forms.Button
    Friend WithEvents GitExeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents XMLRepoFilePathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents XMLButton As System.Windows.Forms.Button
    Friend WithEvents RunConfigDirTextBox As TextBox
    Friend WithEvents GPScriptsDirTextBox As TextBox
    Friend WithEvents VBoxDirTextBox As TextBox
End Class
