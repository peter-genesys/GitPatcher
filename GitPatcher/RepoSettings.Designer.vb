<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RepoSettings
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
        Dim Label26 As System.Windows.Forms.Label
        Dim Label22 As System.Windows.Forms.Label
        Dim Label24 As System.Windows.Forms.Label
        Dim Label21 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RepoSettings))
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
        Me.RepoComboBox = New System.Windows.Forms.ComboBox()
        Me.DBButton = New System.Windows.Forms.Button()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AppButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Label26 = New System.Windows.Forms.Label()
        Label22 = New System.Windows.Forms.Label()
        Label24 = New System.Windows.Forms.Label()
        Label21 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Location = New System.Drawing.Point(65, 68)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(61, 13)
        Label26.TabIndex = 50
        Label26.Text = "Repo Path:"
        '
        'Label22
        '
        Label22.AutoSize = True
        Label22.Location = New System.Drawing.Point(65, 233)
        Label22.Name = "Label22"
        Label22.Size = New System.Drawing.Size(309, 13)
        Label22.TabIndex = 47
        Label22.Text = "Extra Files Search Dirs Relative Path (separated by semi-colon) :"
        '
        'Label24
        '
        Label24.AutoSize = True
        Label24.Location = New System.Drawing.Point(65, 277)
        Label24.Name = "Label24"
        Label24.Size = New System.Drawing.Size(115, 13)
        Label24.TabIndex = 45
        Label24.Text = "Patch Export Full Path:"
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(65, 193)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(139, 13)
        Label21.TabIndex = 43
        Label21.Text = "Database Dir Relative Path:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Location = New System.Drawing.Point(65, 146)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(75, 13)
        Label10.TabIndex = 41
        Label10.Text = "OJDBC jar file:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(65, 107)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(117, 13)
        Label2.TabIndex = 39
        Label2.Text = "Apex Dir Relative Path:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(65, 321)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(121, 13)
        Label3.TabIndex = 37
        Label3.Text = "Patch Dir Relative Path:"
        '
        'RepoPathTextBox
        '
        Me.RepoPathTextBox.Location = New System.Drawing.Point(68, 84)
        Me.RepoPathTextBox.Name = "RepoPathTextBox"
        Me.RepoPathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.RepoPathTextBox.TabIndex = 51
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(68, 15)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonUpdate.TabIndex = 49
        Me.ButtonUpdate.Text = "Update"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'ExtrasDirListTextBox
        '
        Me.ExtrasDirListTextBox.Location = New System.Drawing.Point(68, 249)
        Me.ExtrasDirListTextBox.Multiline = True
        Me.ExtrasDirListTextBox.Name = "ExtrasDirListTextBox"
        Me.ExtrasDirListTextBox.Size = New System.Drawing.Size(444, 22)
        Me.ExtrasDirListTextBox.TabIndex = 48
        '
        'PatchExportPathTextBox
        '
        Me.PatchExportPathTextBox.Location = New System.Drawing.Point(68, 293)
        Me.PatchExportPathTextBox.Name = "PatchExportPathTextBox"
        Me.PatchExportPathTextBox.Size = New System.Drawing.Size(444, 20)
        Me.PatchExportPathTextBox.TabIndex = 46
        '
        'DBOffsetTextBox
        '
        Me.DBOffsetTextBox.Location = New System.Drawing.Point(68, 209)
        Me.DBOffsetTextBox.Name = "DBOffsetTextBox"
        Me.DBOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.DBOffsetTextBox.TabIndex = 44
        '
        'OJDBCjarFileTextBox
        '
        Me.OJDBCjarFileTextBox.Location = New System.Drawing.Point(68, 162)
        Me.OJDBCjarFileTextBox.Name = "OJDBCjarFileTextBox"
        Me.OJDBCjarFileTextBox.Size = New System.Drawing.Size(444, 20)
        Me.OJDBCjarFileTextBox.TabIndex = 42
        '
        'ApexOffsetTextBox
        '
        Me.ApexOffsetTextBox.Location = New System.Drawing.Point(68, 123)
        Me.ApexOffsetTextBox.Name = "ApexOffsetTextBox"
        Me.ApexOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.ApexOffsetTextBox.TabIndex = 40
        '
        'PatchOffsetTextBox
        '
        Me.PatchOffsetTextBox.Location = New System.Drawing.Point(68, 337)
        Me.PatchOffsetTextBox.Name = "PatchOffsetTextBox"
        Me.PatchOffsetTextBox.Size = New System.Drawing.Size(444, 20)
        Me.PatchOffsetTextBox.TabIndex = 38
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(68, 14)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemove.TabIndex = 36
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(69, 15)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAdd.TabIndex = 35
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(13, 47)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(57, 13)
        Me.Label25.TabIndex = 34
        Me.Label25.Text = "Git Repo"
        '
        'RepoComboBox
        '
        Me.RepoComboBox.FormattingEnabled = True
        Me.RepoComboBox.Location = New System.Drawing.Point(68, 44)
        Me.RepoComboBox.Name = "RepoComboBox"
        Me.RepoComboBox.Size = New System.Drawing.Size(444, 21)
        Me.RepoComboBox.TabIndex = 33
        '
        'DBButton
        '
        Me.DBButton.Location = New System.Drawing.Point(150, 15)
        Me.DBButton.Name = "DBButton"
        Me.DBButton.Size = New System.Drawing.Size(75, 23)
        Me.DBButton.TabIndex = 52
        Me.DBButton.Text = "Databases"
        Me.DBButton.UseVisualStyleBackColor = True
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'AppButton
        '
        Me.AppButton.Location = New System.Drawing.Point(231, 15)
        Me.AppButton.Name = "AppButton"
        Me.AppButton.Size = New System.Drawing.Size(75, 23)
        Me.AppButton.TabIndex = 53
        Me.AppButton.Text = "Applications"
        Me.AppButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(66, 385)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(403, 26)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "Warning: Performing an Update will remove the Repo's Databases and Applications." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "I need to fix this to preserve the Databases and Applications."
        '
        'RepoSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 460)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.AppButton)
        Me.Controls.Add(Me.DBButton)
        Me.Controls.Add(Me.RepoPathTextBox)
        Me.Controls.Add(Label26)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.ExtrasDirListTextBox)
        Me.Controls.Add(Label22)
        Me.Controls.Add(Me.PatchExportPathTextBox)
        Me.Controls.Add(Label24)
        Me.Controls.Add(Me.DBOffsetTextBox)
        Me.Controls.Add(Label21)
        Me.Controls.Add(Me.OJDBCjarFileTextBox)
        Me.Controls.Add(Me.ApexOffsetTextBox)
        Me.Controls.Add(Me.PatchOffsetTextBox)
        Me.Controls.Add(Label10)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Label3)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.RepoComboBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "RepoSettings"
        Me.Text = "RepoSettings"
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RepoPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents ExtrasDirListTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchExportPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DBOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OJDBCjarFileTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ApexOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PatchOffsetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents RepoComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DBButton As System.Windows.Forms.Button
    Friend WithEvents AppButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
