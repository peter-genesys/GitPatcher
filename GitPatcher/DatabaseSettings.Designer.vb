<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseSettings
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
        Dim Label27 As System.Windows.Forms.Label
        Dim Label29 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DatabaseSettings))
        Me.PRODTNSTextBox = New System.Windows.Forms.TextBox()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.OrgComboBox = New System.Windows.Forms.ComboBox()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PRODCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.UATCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.UATTNSTextBox = New System.Windows.Forms.TextBox()
        Me.TESTCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.TESTTNSTextBox = New System.Windows.Forms.TextBox()
        Me.DEVCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.DEVTNSTextBox = New System.Windows.Forms.TextBox()
        Me.VMCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.VMTNSTextBox = New System.Windows.Forms.TextBox()
        Label26 = New System.Windows.Forms.Label()
        Label27 = New System.Windows.Forms.Label()
        Label29 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label9 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label26.Location = New System.Drawing.Point(13, 86)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(93, 26)
        Label26.TabIndex = 31
        Label26.Text = "Promo: PROD  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Hotfix: master"
        '
        'PRODTNSTextBox
        '
        Me.PRODTNSTextBox.Location = New System.Drawing.Point(122, 92)
        Me.PRODTNSTextBox.Name = "PRODTNSTextBox"
        Me.PRODTNSTextBox.Size = New System.Drawing.Size(90, 20)
        Me.PRODTNSTextBox.TabIndex = 32
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(487, 12)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonUpdate.TabIndex = 30
        Me.ButtonUpdate.Text = "Update"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(487, 11)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemove.TabIndex = 11
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(488, 12)
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
        Me.Label25.Size = New System.Drawing.Size(78, 13)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Organisation"
        '
        'OrgComboBox
        '
        Me.OrgComboBox.FormattingEnabled = True
        Me.OrgComboBox.Location = New System.Drawing.Point(93, 41)
        Me.OrgComboBox.Name = "OrgComboBox"
        Me.OrgComboBox.Size = New System.Drawing.Size(469, 21)
        Me.OrgComboBox.TabIndex = 8
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'Label27
        '
        Label27.AutoSize = True
        Label27.Location = New System.Drawing.Point(119, 76)
        Label27.Name = "Label27"
        Label27.Size = New System.Drawing.Size(56, 13)
        Label27.TabIndex = 33
        Label27.Text = "TNS Entry"
        '
        'Label29
        '
        Label29.AutoSize = True
        Label29.Location = New System.Drawing.Point(218, 76)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(77, 13)
        Label29.TabIndex = 35
        Label29.Text = "Connect String"
        '
        'PRODCONNECTTextBox
        '
        Me.PRODCONNECTTextBox.Location = New System.Drawing.Point(218, 92)
        Me.PRODCONNECTTextBox.Name = "PRODCONNECTTextBox"
        Me.PRODCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.PRODCONNECTTextBox.TabIndex = 34
        '
        'UATCONNECTTextBox
        '
        Me.UATCONNECTTextBox.Location = New System.Drawing.Point(218, 134)
        Me.UATCONNECTTextBox.Name = "UATCONNECTTextBox"
        Me.UATCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.UATCONNECTTextBox.TabIndex = 39
        '
        'UATTNSTextBox
        '
        Me.UATTNSTextBox.Location = New System.Drawing.Point(121, 134)
        Me.UATTNSTextBox.Name = "UATTNSTextBox"
        Me.UATTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.UATTNSTextBox.TabIndex = 37
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(12, 128)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(83, 26)
        Label3.TabIndex = 36
        Label3.Text = "Promo: UAT  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Hotfix: uat"
        '
        'TESTCONNECTTextBox
        '
        Me.TESTCONNECTTextBox.Location = New System.Drawing.Point(218, 175)
        Me.TESTCONNECTTextBox.Name = "TESTCONNECTTextBox"
        Me.TESTCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.TESTCONNECTTextBox.TabIndex = 44
        '
        'TESTTNSTextBox
        '
        Me.TESTTNSTextBox.Location = New System.Drawing.Point(121, 175)
        Me.TESTTNSTextBox.Name = "TESTTNSTextBox"
        Me.TESTTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.TESTTNSTextBox.TabIndex = 42
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.Location = New System.Drawing.Point(12, 169)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(90, 26)
        Label6.TabIndex = 41
        Label6.Text = "Promo: TEST  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Hotfix: test"
        '
        'DEVCONNECTTextBox
        '
        Me.DEVCONNECTTextBox.Location = New System.Drawing.Point(218, 216)
        Me.DEVCONNECTTextBox.Name = "DEVCONNECTTextBox"
        Me.DEVCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.DEVCONNECTTextBox.TabIndex = 49
        '
        'DEVTNSTextBox
        '
        Me.DEVTNSTextBox.Location = New System.Drawing.Point(121, 216)
        Me.DEVTNSTextBox.Name = "DEVTNSTextBox"
        Me.DEVTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.DEVTNSTextBox.TabIndex = 47
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label9.Location = New System.Drawing.Point(12, 210)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(93, 26)
        Label9.TabIndex = 46
        Label9.Text = "Promo: DEV  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Hotfix: develop"
        '
        'VMCONNECTTextBox
        '
        Me.VMCONNECTTextBox.Location = New System.Drawing.Point(218, 256)
        Me.VMCONNECTTextBox.Name = "VMCONNECTTextBox"
        Me.VMCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.VMCONNECTTextBox.TabIndex = 54
        '
        'VMTNSTextBox
        '
        Me.VMTNSTextBox.Location = New System.Drawing.Point(121, 256)
        Me.VMTNSTextBox.Name = "VMTNSTextBox"
        Me.VMTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.VMTNSTextBox.TabIndex = 52
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label12.Location = New System.Drawing.Point(13, 256)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(76, 13)
        Label12.TabIndex = 51
        Label12.Text = "Promo: VM  "
        '
        'DatabaseSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 319)
        Me.Controls.Add(Me.VMCONNECTTextBox)
        Me.Controls.Add(Me.VMTNSTextBox)
        Me.Controls.Add(Label12)
        Me.Controls.Add(Me.DEVCONNECTTextBox)
        Me.Controls.Add(Me.DEVTNSTextBox)
        Me.Controls.Add(Label9)
        Me.Controls.Add(Me.TESTCONNECTTextBox)
        Me.Controls.Add(Me.TESTTNSTextBox)
        Me.Controls.Add(Label6)
        Me.Controls.Add(Me.UATCONNECTTextBox)
        Me.Controls.Add(Me.UATTNSTextBox)
        Me.Controls.Add(Label3)
        Me.Controls.Add(Label29)
        Me.Controls.Add(Me.PRODCONNECTTextBox)
        Me.Controls.Add(Label27)
        Me.Controls.Add(Me.PRODTNSTextBox)
        Me.Controls.Add(Label26)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.OrgComboBox)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DatabaseSettings"
        Me.Text = "DatabaseSettings"
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MySettingsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OrgComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents PRODTNSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PRODCONNECTTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UATCONNECTTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UATTNSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TESTCONNECTTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TESTTNSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DEVCONNECTTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DEVTNSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VMCONNECTTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VMTNSTextBox As System.Windows.Forms.TextBox
End Class
