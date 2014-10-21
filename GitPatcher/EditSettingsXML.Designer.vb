<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditSettingsXML
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
        Dim Label1 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Repo1Label As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim Label17 As System.Windows.Forms.Label
        Dim Label16 As System.Windows.Forms.Label
        Dim Label20 As System.Windows.Forms.Label
        Dim Label23 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label18 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label26 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditSettingsXML))
        Me.MailTabPage = New System.Windows.Forms.TabPage()
        Me.TestMailButton = New System.Windows.Forms.Button()
        Me.RecipientDomainTextBox = New System.Windows.Forms.TextBox()
        Me.SMTPportTextBox = New System.Windows.Forms.TextBox()
        Me.SMTPhostTextBox = New System.Windows.Forms.TextBox()
        Me.RecipientTextBox = New System.Windows.Forms.TextBox()
        Me.AppsTabPage = New System.Windows.Forms.TabPage()
        Me.JiraProjectTextBox = New System.Windows.Forms.TextBox()
        Me.AppCodeTextBox = New System.Windows.Forms.TextBox()
        Me.ApplicationsTextBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ParsingSchemaTextbox = New System.Windows.Forms.TextBox()
        Me.AppListTextBox = New System.Windows.Forms.TextBox()
        Me.DBTabPage = New System.Windows.Forms.TabPage()
        Me.TNSListTextbox = New System.Windows.Forms.TextBox()
        Me.HotFixBranchesTextBox = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ConnectionTextBox = New System.Windows.Forms.TextBox()
        Me.DBListTextBox = New System.Windows.Forms.TextBox()
        Me.TabPageGitRepo = New System.Windows.Forms.TabPage()
        Me.RepoPathTextBox = New System.Windows.Forms.TextBox()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ExtrasDirListTextBox = New System.Windows.Forms.TextBox()
        Me.PatchExportPathTextBox = New System.Windows.Forms.TextBox()
        Me.DBOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.OJDBCjarFileTextBox = New System.Windows.Forms.TextBox()
        Me.ApexOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.PatchOffsetTextBox = New System.Windows.Forms.TextBox()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.XMLRepoComboBox = New System.Windows.Forms.ComboBox()
        Me.ConfigTabs = New System.Windows.Forms.TabControl()
        Me.PatchTabPage = New System.Windows.Forms.TabPage()
        Me.GitExeTextBox = New System.Windows.Forms.TextBox()
        Me.SQLpathTextBox = New System.Windows.Forms.TextBox()
        Me.ExtrasTabPage = New System.Windows.Forms.TabPage()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Label11 = New System.Windows.Forms.Label()
        Label13 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Repo1Label = New System.Windows.Forms.Label()
        Label14 = New System.Windows.Forms.Label()
        Label15 = New System.Windows.Forms.Label()
        Label17 = New System.Windows.Forms.Label()
        Label16 = New System.Windows.Forms.Label()
        Label20 = New System.Windows.Forms.Label()
        Label23 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label18 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        Me.MailTabPage.SuspendLayout()
        Me.AppsTabPage.SuspendLayout()
        Me.DBTabPage.SuspendLayout()
        Me.TabPageGitRepo.SuspendLayout()
        Me.ConfigTabs.SuspendLayout()
        Me.PatchTabPage.SuspendLayout()
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
        'Label9
        '
        Label9.AutoSize = True
        Label9.Location = New System.Drawing.Point(266, 16)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(41, 13)
        Label9.TabIndex = 13
        Label9.Text = "App Id:"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Location = New System.Drawing.Point(428, 16)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(87, 13)
        Label8.TabIndex = 15
        Label8.Text = "Parsing Schema:"
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
        Label5.Location = New System.Drawing.Point(272, 16)
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
        Repo1Label.Size = New System.Drawing.Size(0, 13)
        Repo1Label.TabIndex = 0
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
        'Label15
        '
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(3, 16)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(67, 13)
        Label15.TabIndex = 19
        Label15.Text = "Applications:"
        '
        'Label17
        '
        Label17.AutoSize = True
        Label17.Location = New System.Drawing.Point(185, 16)
        Label17.Name = "Label17"
        Label17.Size = New System.Drawing.Size(35, 13)
        Label17.TabIndex = 22
        Label17.Text = "Code:"
        '
        'Label16
        '
        Label16.AutoSize = True
        Label16.Location = New System.Drawing.Point(94, 16)
        Label16.Name = "Label16"
        Label16.Size = New System.Drawing.Size(77, 13)
        Label16.TabIndex = 8
        Label16.Text = "HotFix Branch:"
        '
        'Label20
        '
        Label20.AutoSize = True
        Label20.Location = New System.Drawing.Point(183, 16)
        Label20.Name = "Label20"
        Label20.Size = New System.Drawing.Size(51, 13)
        Label20.TabIndex = 14
        Label20.Text = "TNS List:"
        '
        'Label23
        '
        Label23.AutoSize = True
        Label23.Location = New System.Drawing.Point(347, 16)
        Label23.Name = "Label23"
        Label23.Size = New System.Drawing.Size(62, 13)
        Label23.TabIndex = 24
        Label23.Text = "Jira Project:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(5, 184)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(76, 13)
        Label4.TabIndex = 8
        Label4.Text = "SQL exe Path:"
        '
        'Label18
        '
        Label18.AutoSize = True
        Label18.Location = New System.Drawing.Point(5, 228)
        Label18.Name = "Label18"
        Label18.Size = New System.Drawing.Size(68, 13)
        Label18.TabIndex = 14
        Label18.Text = "Git exe Path:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(59, 190)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(139, 13)
        Label21.TabIndex = 24
        Label21.Text = "Database Dir Relative Path:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(59, 143)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(75, 13)
        Label10.TabIndex = 22
        Label10.Text = "OJDBC jar file:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(59, 104)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(117, 13)
        Label2.TabIndex = 20
        Label2.Text = "Apex Dir Relative Path:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(59, 318)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(121, 13)
        Label3.TabIndex = 18
        Label3.Text = "Patch Dir Relative Path:"
        '
        'Label24
        '
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(59, 274)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(115, 13)
        Label24.TabIndex = 26
        Label24.Text = "Patch Export Full Path:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(59, 230)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(183, 13)
        Label22.TabIndex = 28
        Label22.Text = "Extra Files Search Dirs Relative Path:"
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(59, 65)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(61, 13)
        Label26.TabIndex = 31
        Label26.Text = "Repo Path:"
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
        Me.MailTabPage.Size = New System.Drawing.Size(534, 360)
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
        'AppsTabPage
        '
        Me.AppsTabPage.Controls.Add(Me.JiraProjectTextBox)
        Me.AppsTabPage.Controls.Add(Label23)
        Me.AppsTabPage.Controls.Add(Me.AppCodeTextBox)
        Me.AppsTabPage.Controls.Add(Label17)
        Me.AppsTabPage.Controls.Add(Label15)
        Me.AppsTabPage.Controls.Add(Me.ApplicationsTextBox)
        Me.AppsTabPage.Controls.Add(Me.Label7)
        Me.AppsTabPage.Controls.Add(Me.ParsingSchemaTextbox)
        Me.AppsTabPage.Controls.Add(Me.AppListTextBox)
        Me.AppsTabPage.Controls.Add(Label8)
        Me.AppsTabPage.Controls.Add(Label9)
        Me.AppsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.AppsTabPage.Name = "AppsTabPage"
        Me.AppsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AppsTabPage.Size = New System.Drawing.Size(534, 360)
        Me.AppsTabPage.TabIndex = 4
        Me.AppsTabPage.Text = "Apps"
        Me.AppsTabPage.UseVisualStyleBackColor = True
        '
        'JiraProjectTextBox
        '
        Me.JiraProjectTextBox.Location = New System.Drawing.Point(350, 32)
        Me.JiraProjectTextBox.Multiline = True
        Me.JiraProjectTextBox.Name = "JiraProjectTextBox"
        Me.JiraProjectTextBox.Size = New System.Drawing.Size(75, 229)
        Me.JiraProjectTextBox.TabIndex = 25
        '
        'AppCodeTextBox
        '
        Me.AppCodeTextBox.Location = New System.Drawing.Point(188, 32)
        Me.AppCodeTextBox.Multiline = True
        Me.AppCodeTextBox.Name = "AppCodeTextBox"
        Me.AppCodeTextBox.Size = New System.Drawing.Size(75, 229)
        Me.AppCodeTextBox.TabIndex = 23
        '
        'ApplicationsTextBox
        '
        Me.ApplicationsTextBox.Location = New System.Drawing.Point(6, 32)
        Me.ApplicationsTextBox.Multiline = True
        Me.ApplicationsTextBox.Name = "ApplicationsTextBox"
        Me.ApplicationsTextBox.Size = New System.Drawing.Size(177, 229)
        Me.ApplicationsTextBox.TabIndex = 18
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
        Me.ParsingSchemaTextbox.Location = New System.Drawing.Point(431, 32)
        Me.ParsingSchemaTextbox.Multiline = True
        Me.ParsingSchemaTextbox.Name = "ParsingSchemaTextbox"
        Me.ParsingSchemaTextbox.Size = New System.Drawing.Size(75, 229)
        Me.ParsingSchemaTextbox.TabIndex = 16
        '
        'AppListTextBox
        '
        Me.AppListTextBox.Location = New System.Drawing.Point(269, 32)
        Me.AppListTextBox.Multiline = True
        Me.AppListTextBox.Name = "AppListTextBox"
        Me.AppListTextBox.Size = New System.Drawing.Size(75, 229)
        Me.AppListTextBox.TabIndex = 14
        '
        'DBTabPage
        '
        Me.DBTabPage.Controls.Add(Me.TNSListTextbox)
        Me.DBTabPage.Controls.Add(Me.HotFixBranchesTextBox)
        Me.DBTabPage.Controls.Add(Label16)
        Me.DBTabPage.Controls.Add(Label20)
        Me.DBTabPage.Controls.Add(Me.Label19)
        Me.DBTabPage.Controls.Add(Me.Label6)
        Me.DBTabPage.Controls.Add(Me.ConnectionTextBox)
        Me.DBTabPage.Controls.Add(Me.DBListTextBox)
        Me.DBTabPage.Controls.Add(Label5)
        Me.DBTabPage.Controls.Add(Label1)
        Me.DBTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DBTabPage.Name = "DBTabPage"
        Me.DBTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DBTabPage.Size = New System.Drawing.Size(534, 360)
        Me.DBTabPage.TabIndex = 3
        Me.DBTabPage.Text = "Databases"
        Me.DBTabPage.UseVisualStyleBackColor = True
        '
        'TNSListTextbox
        '
        Me.TNSListTextbox.Location = New System.Drawing.Point(186, 32)
        Me.TNSListTextbox.Multiline = True
        Me.TNSListTextbox.Name = "TNSListTextbox"
        Me.TNSListTextbox.Size = New System.Drawing.Size(83, 208)
        Me.TNSListTextbox.TabIndex = 15
        '
        'HotFixBranchesTextBox
        '
        Me.HotFixBranchesTextBox.Enabled = False
        Me.HotFixBranchesTextBox.Location = New System.Drawing.Point(97, 32)
        Me.HotFixBranchesTextBox.Multiline = True
        Me.HotFixBranchesTextBox.Name = "HotFixBranchesTextBox"
        Me.HotFixBranchesTextBox.Size = New System.Drawing.Size(83, 208)
        Me.HotFixBranchesTextBox.TabIndex = 9
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 264)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(232, 13)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "Provide a Hotfix Branch, if applicable for the DB"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 243)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(241, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Provide a TNS entry and Connection for each DB"
        '
        'ConnectionTextBox
        '
        Me.ConnectionTextBox.Location = New System.Drawing.Point(275, 32)
        Me.ConnectionTextBox.Multiline = True
        Me.ConnectionTextBox.Name = "ConnectionTextBox"
        Me.ConnectionTextBox.Size = New System.Drawing.Size(253, 208)
        Me.ConnectionTextBox.TabIndex = 11
        '
        'DBListTextBox
        '
        Me.DBListTextBox.Enabled = False
        Me.DBListTextBox.Location = New System.Drawing.Point(8, 32)
        Me.DBListTextBox.Multiline = True
        Me.DBListTextBox.Name = "DBListTextBox"
        Me.DBListTextBox.Size = New System.Drawing.Size(83, 208)
        Me.DBListTextBox.TabIndex = 9
        '
        'TabPageGitRepo
        '
        Me.TabPageGitRepo.AutoScroll = True
        Me.TabPageGitRepo.Controls.Add(Me.RepoPathTextBox)
        Me.TabPageGitRepo.Controls.Add(Label26)
        Me.TabPageGitRepo.Controls.Add(Me.ButtonUpdate)
        Me.TabPageGitRepo.Controls.Add(Me.ExtrasDirListTextBox)
        Me.TabPageGitRepo.Controls.Add(Label22)
        Me.TabPageGitRepo.Controls.Add(Me.PatchExportPathTextBox)
        Me.TabPageGitRepo.Controls.Add(Label24)
        Me.TabPageGitRepo.Controls.Add(Me.DBOffsetTextBox)
        Me.TabPageGitRepo.Controls.Add(Label21)
        Me.TabPageGitRepo.Controls.Add(Me.OJDBCjarFileTextBox)
        Me.TabPageGitRepo.Controls.Add(Me.ApexOffsetTextBox)
        Me.TabPageGitRepo.Controls.Add(Me.PatchOffsetTextBox)
        Me.TabPageGitRepo.Controls.Add(Label10)
        Me.TabPageGitRepo.Controls.Add(Label2)
        Me.TabPageGitRepo.Controls.Add(Label3)
        Me.TabPageGitRepo.Controls.Add(Me.ButtonRemove)
        Me.TabPageGitRepo.Controls.Add(Me.ButtonAdd)
        Me.TabPageGitRepo.Controls.Add(Me.Label25)
        Me.TabPageGitRepo.Controls.Add(Me.XMLRepoComboBox)
        Me.TabPageGitRepo.Controls.Add(Repo1Label)
        Me.TabPageGitRepo.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGitRepo.Name = "TabPageGitRepo"
        Me.TabPageGitRepo.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGitRepo.Size = New System.Drawing.Size(534, 360)
        Me.TabPageGitRepo.TabIndex = 0
        Me.TabPageGitRepo.Text = "Git Repos"
        Me.TabPageGitRepo.UseVisualStyleBackColor = True
        '
        'RepoPathTextBox
        '
        Me.RepoPathTextBox.Location = New System.Drawing.Point(62, 81)
        Me.RepoPathTextBox.Name = "RepoPathTextBox"
        Me.RepoPathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.RepoPathTextBox.TabIndex = 32
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(456, 12)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonUpdate.TabIndex = 30
        Me.ButtonUpdate.Text = "Update"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'ExtrasDirListTextBox
        '
        Me.ExtrasDirListTextBox.Location = New System.Drawing.Point(62, 246)
        Me.ExtrasDirListTextBox.Multiline = True
        Me.ExtrasDirListTextBox.Name = "ExtrasDirListTextBox"
        Me.ExtrasDirListTextBox.Size = New System.Drawing.Size(444, 22)
        Me.ExtrasDirListTextBox.TabIndex = 29
        '
        'PatchExportPathTextBox
        '
        Me.PatchExportPathTextBox.Location = New System.Drawing.Point(62, 290)
        Me.PatchExportPathTextBox.Name = "PatchExportPathTextBox"
        Me.PatchExportPathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.PatchExportPathTextBox.TabIndex = 27
        '
        'DBOffsetTextBox
        '
        Me.DBOffsetTextBox.Location = New System.Drawing.Point(62, 206)
        Me.DBOffsetTextBox.Name = "DBOffsetTextBox"
        Me.DBOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.DBOffsetTextBox.TabIndex = 25
        '
        'OJDBCjarFileTextBox
        '
        Me.OJDBCjarFileTextBox.Location = New System.Drawing.Point(62, 159)
        Me.OJDBCjarFileTextBox.Name = "OJDBCjarFileTextBox"
        Me.OJDBCjarFileTextBox.Size = New System.Drawing.Size(444, 20)
        Me.OJDBCjarFileTextBox.TabIndex = 23
        '
        'ApexOffsetTextBox
        '
        Me.ApexOffsetTextBox.Location = New System.Drawing.Point(62, 120)
        Me.ApexOffsetTextBox.Name = "ApexOffsetTextBox"
        Me.ApexOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.ApexOffsetTextBox.TabIndex = 21
        '
        'PatchOffsetTextBox
        '
        Me.PatchOffsetTextBox.Location = New System.Drawing.Point(62, 334)
        Me.PatchOffsetTextBox.Name = "PatchOffsetTextBox"
        Me.PatchOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.PatchOffsetTextBox.TabIndex = 19
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(456, 11)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemove.TabIndex = 11
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(457, 12)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAdd.TabIndex = 10
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(7, 44)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(49, 13)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Git Repo"
        '
        'XMLRepoComboBox
        '
        Me.XMLRepoComboBox.FormattingEnabled = True
        Me.XMLRepoComboBox.Location = New System.Drawing.Point(62, 41)
        Me.XMLRepoComboBox.Name = "XMLRepoComboBox"
        Me.XMLRepoComboBox.Size = New System.Drawing.Size(444, 21)
        Me.XMLRepoComboBox.TabIndex = 8
        '
        'ConfigTabs
        '
        Me.ConfigTabs.Controls.Add(Me.TabPageGitRepo)
        Me.ConfigTabs.Controls.Add(Me.DBTabPage)
        Me.ConfigTabs.Controls.Add(Me.PatchTabPage)
        Me.ConfigTabs.Controls.Add(Me.ExtrasTabPage)
        Me.ConfigTabs.Controls.Add(Me.AppsTabPage)
        Me.ConfigTabs.Controls.Add(Me.MailTabPage)
        Me.ConfigTabs.Location = New System.Drawing.Point(12, 28)
        Me.ConfigTabs.Name = "ConfigTabs"
        Me.ConfigTabs.SelectedIndex = 0
        Me.ConfigTabs.Size = New System.Drawing.Size(542, 386)
        Me.ConfigTabs.TabIndex = 0
        '
        'PatchTabPage
        '
        Me.PatchTabPage.Controls.Add(Me.GitExeTextBox)
        Me.PatchTabPage.Controls.Add(Label18)
        Me.PatchTabPage.Controls.Add(Me.SQLpathTextBox)
        Me.PatchTabPage.Controls.Add(Label4)
        Me.PatchTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchTabPage.Name = "PatchTabPage"
        Me.PatchTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PatchTabPage.Size = New System.Drawing.Size(534, 360)
        Me.PatchTabPage.TabIndex = 1
        Me.PatchTabPage.Text = "Paths"
        Me.PatchTabPage.UseVisualStyleBackColor = True
        '
        'GitExeTextBox
        '
        Me.GitExeTextBox.Location = New System.Drawing.Point(8, 244)
        Me.GitExeTextBox.Name = "GitExeTextBox"
        Me.GitExeTextBox.Size = New System.Drawing.Size(444, 20)
        Me.GitExeTextBox.TabIndex = 15
        '
        'SQLpathTextBox
        '
        Me.SQLpathTextBox.Location = New System.Drawing.Point(8, 200)
        Me.SQLpathTextBox.Name = "SQLpathTextBox"
        Me.SQLpathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.SQLpathTextBox.TabIndex = 9
        '
        'ExtrasTabPage
        '
        Me.ExtrasTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ExtrasTabPage.Name = "ExtrasTabPage"
        Me.ExtrasTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ExtrasTabPage.Size = New System.Drawing.Size(534, 360)
        Me.ExtrasTabPage.TabIndex = 6
        Me.ExtrasTabPage.Text = "Extras"
        Me.ExtrasTabPage.UseVisualStyleBackColor = True
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'EditSettingsXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 426)
        Me.Controls.Add(Me.ConfigTabs)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EditSettingsXML"
        Me.Text = "EditXML"
        Me.MailTabPage.ResumeLayout(False)
        Me.MailTabPage.PerformLayout()
        Me.AppsTabPage.ResumeLayout(False)
        Me.AppsTabPage.PerformLayout()
        Me.DBTabPage.ResumeLayout(False)
        Me.DBTabPage.PerformLayout()
        Me.TabPageGitRepo.ResumeLayout(False)
        Me.TabPageGitRepo.PerformLayout()
        Me.ConfigTabs.ResumeLayout(False)
        Me.PatchTabPage.ResumeLayout(False)
        Me.PatchTabPage.PerformLayout()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MailTabPage As System.Windows.Forms.TabPage
    Friend WithEvents RecipientDomainTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SMTPportTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SMTPhostTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RecipientTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AppsTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ParsingSchemaTextbox As System.Windows.Forms.TextBox
    Friend WithEvents AppListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBTabPage As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ConnectionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabPageGitRepo As System.Windows.Forms.TabPage
    Friend WithEvents ConfigTabs As System.Windows.Forms.TabControl
    Friend WithEvents TestMailButton As System.Windows.Forms.Button
    Friend WithEvents ApplicationsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AppCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents HotFixBranchesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TNSListTextbox As System.Windows.Forms.TextBox
    Friend WithEvents ExtrasTabPage As System.Windows.Forms.TabPage
    Friend WithEvents JiraProjectTextBox As System.Windows.Forms.TextBox
    Friend WithEvents XMLRepoComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents PatchExportPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OJDBCjarFileTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ApexOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchTabPage As System.Windows.Forms.TabPage
    Friend WithEvents GitExeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SQLpathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents ExtrasDirListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RepoPathTextBox As System.Windows.Forms.TextBox
End Class
