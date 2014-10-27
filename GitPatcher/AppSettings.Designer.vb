<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppSettings
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
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AppSettings))
        Me.AppCodeTextBox = New System.Windows.Forms.TextBox()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.AppComboBox = New System.Windows.Forms.ComboBox()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AppIdTextBox = New System.Windows.Forms.TextBox()
        Me.JiraIssueTextBox = New System.Windows.Forms.TextBox()
        Me.ParsingSchemaTextBox = New System.Windows.Forms.TextBox()
        Label26 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label26.Location = New System.Drawing.Point(90, 87)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(99, 13)
        Label26.TabIndex = 31
        Label26.Text = "Patch App Code"
        '
        'AppCodeTextBox
        '
        Me.AppCodeTextBox.Location = New System.Drawing.Point(202, 84)
        Me.AppCodeTextBox.Name = "AppCodeTextBox"
        Me.AppCodeTextBox.Size = New System.Drawing.Size(90, 20)
        Me.AppCodeTextBox.TabIndex = 32
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(93, 12)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonUpdate.TabIndex = 30
        Me.ButtonUpdate.Text = "Update"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(93, 11)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemove.TabIndex = 11
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(94, 12)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAdd.TabIndex = 10
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(13, 44)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label25.Size = New System.Drawing.Size(70, 13)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Application"
        '
        'AppComboBox
        '
        Me.AppComboBox.FormattingEnabled = True
        Me.AppComboBox.Location = New System.Drawing.Point(93, 41)
        Me.AppComboBox.Name = "AppComboBox"
        Me.AppComboBox.Size = New System.Drawing.Size(199, 21)
        Me.AppComboBox.TabIndex = 8
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'AppIdTextBox
        '
        Me.AppIdTextBox.Location = New System.Drawing.Point(201, 126)
        Me.AppIdTextBox.Name = "AppIdTextBox"
        Me.AppIdTextBox.Size = New System.Drawing.Size(91, 20)
        Me.AppIdTextBox.TabIndex = 37
        '
        'JiraIssueTextBox
        '
        Me.JiraIssueTextBox.Location = New System.Drawing.Point(201, 167)
        Me.JiraIssueTextBox.Name = "JiraIssueTextBox"
        Me.JiraIssueTextBox.Size = New System.Drawing.Size(91, 20)
        Me.JiraIssueTextBox.TabIndex = 42
        '
        'ParsingSchemaTextBox
        '
        Me.ParsingSchemaTextBox.Location = New System.Drawing.Point(201, 208)
        Me.ParsingSchemaTextBox.Name = "ParsingSchemaTextBox"
        Me.ParsingSchemaTextBox.Size = New System.Drawing.Size(91, 20)
        Me.ParsingSchemaTextBox.TabIndex = 47
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(90, 129)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(76, 13)
        Label1.TabIndex = 55
        Label1.Text = "Apex App Id"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(90, 170)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(109, 13)
        Label2.TabIndex = 56
        Label2.Text = "Jira or Issue Code"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(90, 211)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(98, 13)
        Label3.TabIndex = 57
        Label3.Text = "Parsing Schema"
        '
        'AppSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 254)
        Me.Controls.Add(Label3)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.ParsingSchemaTextBox)
        Me.Controls.Add(Me.JiraIssueTextBox)
        Me.Controls.Add(Me.AppIdTextBox)
        Me.Controls.Add(Me.AppCodeTextBox)
        Me.Controls.Add(Label26)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.AppComboBox)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AppSettings"
        Me.Text = "ApplicationSettings"
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AppComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents AppCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AppIdTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JiraIssueTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ParsingSchemaTextBox As System.Windows.Forms.TextBox
End Class
