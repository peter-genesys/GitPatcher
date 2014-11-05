<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrgSettings
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
        Dim Label12 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OrgSettings))
        Me.PRODTNSTextBox = New System.Windows.Forms.TextBox()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.OrgComboBox = New System.Windows.Forms.ComboBox()
        Me.PRODCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.UATCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.UATTNSTextBox = New System.Windows.Forms.TextBox()
        Me.TESTCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.TESTTNSTextBox = New System.Windows.Forms.TextBox()
        Me.DEVCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.DEVTNSTextBox = New System.Windows.Forms.TextBox()
        Me.VMCONNECTTextBox = New System.Windows.Forms.TextBox()
        Me.VMTNSTextBox = New System.Windows.Forms.TextBox()
        Me.OrgCodeTextBox = New System.Windows.Forms.TextBox()
        Me.MySettingsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OrgInFeatureCheckBox = New System.Windows.Forms.CheckBox()
        Label26 = New System.Windows.Forms.Label()
        Label27 = New System.Windows.Forms.Label()
        Label29 = New System.Windows.Forms.Label()
        Label12 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label8 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label10 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Label11 = New System.Windows.Forms.Label()
        CType(Me.MySettingsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label26
        '
        Label26.AutoSize = True
        Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label26.Location = New System.Drawing.Point(24, 132)
        Label26.Name = "Label26"
        Label26.Size = New System.Drawing.Size(38, 13)
        Label26.TabIndex = 31
        Label26.Text = "PROD"
        '
        'Label27
        '
        Label27.AutoSize = True
        Label27.Location = New System.Drawing.Point(119, 113)
        Label27.Name = "Label27"
        Label27.Size = New System.Drawing.Size(56, 13)
        Label27.TabIndex = 33
        Label27.Text = "TNS Entry"
        '
        'Label29
        '
        Label29.AutoSize = True
        Label29.Location = New System.Drawing.Point(218, 113)
        Label29.Name = "Label29"
        Label29.Size = New System.Drawing.Size(77, 13)
        Label29.TabIndex = 35
        Label29.Text = "Connect String"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label12.Location = New System.Drawing.Point(24, 296)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(29, 13)
        Label12.TabIndex = 51
        Label12.Text = "VM  "
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(414, 44)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(52, 13)
        Label1.TabIndex = 56
        Label1.Text = "Org Code"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(24, 113)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(37, 13)
        Label2.TabIndex = 57
        Label2.Text = "Promo"
        AddHandler Label2.Click, AddressOf Me.Label2_Click
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(67, 113)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(37, 13)
        Label4.TabIndex = 58
        Label4.Text = "HotFix"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label5.Location = New System.Drawing.Point(67, 132)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(38, 13)
        Label5.TabIndex = 59
        Label5.Text = "master"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.Location = New System.Drawing.Point(67, 174)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(22, 13)
        Label7.TabIndex = 61
        Label7.Text = "uat"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label8.Location = New System.Drawing.Point(24, 174)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(29, 13)
        Label8.TabIndex = 60
        Label8.Text = "UAT"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(67, 215)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(24, 13)
        Label3.TabIndex = 63
        Label3.Text = "test"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label10.Location = New System.Drawing.Point(24, 215)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(35, 13)
        Label10.TabIndex = 62
        Label10.Text = "TEST"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.Location = New System.Drawing.Point(67, 256)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(45, 13)
        Label6.TabIndex = 65
        Label6.Text = "develop"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label11.Location = New System.Drawing.Point(24, 256)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(29, 13)
        Label11.TabIndex = 64
        Label11.Text = "DEV"
        '
        'PRODTNSTextBox
        '
        Me.PRODTNSTextBox.Location = New System.Drawing.Point(122, 129)
        Me.PRODTNSTextBox.Name = "PRODTNSTextBox"
        Me.PRODTNSTextBox.Size = New System.Drawing.Size(90, 20)
        Me.PRODTNSTextBox.TabIndex = 32
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
        Me.Label25.Size = New System.Drawing.Size(78, 13)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "Organisation"
        '
        'OrgComboBox
        '
        Me.OrgComboBox.FormattingEnabled = True
        Me.OrgComboBox.Location = New System.Drawing.Point(93, 41)
        Me.OrgComboBox.Name = "OrgComboBox"
        Me.OrgComboBox.Size = New System.Drawing.Size(315, 21)
        Me.OrgComboBox.TabIndex = 8
        '
        'PRODCONNECTTextBox
        '
        Me.PRODCONNECTTextBox.Location = New System.Drawing.Point(218, 129)
        Me.PRODCONNECTTextBox.Name = "PRODCONNECTTextBox"
        Me.PRODCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.PRODCONNECTTextBox.TabIndex = 34
        '
        'UATCONNECTTextBox
        '
        Me.UATCONNECTTextBox.Location = New System.Drawing.Point(218, 171)
        Me.UATCONNECTTextBox.Name = "UATCONNECTTextBox"
        Me.UATCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.UATCONNECTTextBox.TabIndex = 39
        '
        'UATTNSTextBox
        '
        Me.UATTNSTextBox.Location = New System.Drawing.Point(121, 171)
        Me.UATTNSTextBox.Name = "UATTNSTextBox"
        Me.UATTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.UATTNSTextBox.TabIndex = 37
        '
        'TESTCONNECTTextBox
        '
        Me.TESTCONNECTTextBox.Location = New System.Drawing.Point(218, 212)
        Me.TESTCONNECTTextBox.Name = "TESTCONNECTTextBox"
        Me.TESTCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.TESTCONNECTTextBox.TabIndex = 44
        '
        'TESTTNSTextBox
        '
        Me.TESTTNSTextBox.Location = New System.Drawing.Point(121, 212)
        Me.TESTTNSTextBox.Name = "TESTTNSTextBox"
        Me.TESTTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.TESTTNSTextBox.TabIndex = 42
        '
        'DEVCONNECTTextBox
        '
        Me.DEVCONNECTTextBox.Location = New System.Drawing.Point(218, 253)
        Me.DEVCONNECTTextBox.Name = "DEVCONNECTTextBox"
        Me.DEVCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.DEVCONNECTTextBox.TabIndex = 49
        '
        'DEVTNSTextBox
        '
        Me.DEVTNSTextBox.Location = New System.Drawing.Point(121, 253)
        Me.DEVTNSTextBox.Name = "DEVTNSTextBox"
        Me.DEVTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.DEVTNSTextBox.TabIndex = 47
        '
        'VMCONNECTTextBox
        '
        Me.VMCONNECTTextBox.Location = New System.Drawing.Point(218, 293)
        Me.VMCONNECTTextBox.Name = "VMCONNECTTextBox"
        Me.VMCONNECTTextBox.Size = New System.Drawing.Size(344, 20)
        Me.VMCONNECTTextBox.TabIndex = 54
        '
        'VMTNSTextBox
        '
        Me.VMTNSTextBox.Location = New System.Drawing.Point(121, 293)
        Me.VMTNSTextBox.Name = "VMTNSTextBox"
        Me.VMTNSTextBox.Size = New System.Drawing.Size(91, 20)
        Me.VMTNSTextBox.TabIndex = 52
        '
        'OrgCodeTextBox
        '
        Me.OrgCodeTextBox.Location = New System.Drawing.Point(472, 41)
        Me.OrgCodeTextBox.Name = "OrgCodeTextBox"
        Me.OrgCodeTextBox.Size = New System.Drawing.Size(90, 20)
        Me.OrgCodeTextBox.TabIndex = 55
        '
        'MySettingsBindingSource
        '
        Me.MySettingsBindingSource.DataSource = GetType(System.Configuration.ApplicationSettingsBase)
        '
        'OrgInFeatureCheckBox
        '
        Me.OrgInFeatureCheckBox.AutoSize = True
        Me.OrgInFeatureCheckBox.Location = New System.Drawing.Point(472, 67)
        Me.OrgInFeatureCheckBox.Name = "OrgInFeatureCheckBox"
        Me.OrgInFeatureCheckBox.Size = New System.Drawing.Size(95, 30)
        Me.OrgInFeatureCheckBox.TabIndex = 66
        Me.OrgInFeatureCheckBox.Text = "Use in Feature" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Path"
        Me.OrgInFeatureCheckBox.UseVisualStyleBackColor = True
        '
        'OrgSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 351)
        Me.Controls.Add(Me.OrgInFeatureCheckBox)
        Me.Controls.Add(Label6)
        Me.Controls.Add(Label11)
        Me.Controls.Add(Label3)
        Me.Controls.Add(Label10)
        Me.Controls.Add(Label7)
        Me.Controls.Add(Label8)
        Me.Controls.Add(Label5)
        Me.Controls.Add(Label4)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.OrgCodeTextBox)
        Me.Controls.Add(Me.VMCONNECTTextBox)
        Me.Controls.Add(Me.VMTNSTextBox)
        Me.Controls.Add(Label12)
        Me.Controls.Add(Me.DEVCONNECTTextBox)
        Me.Controls.Add(Me.DEVTNSTextBox)
        Me.Controls.Add(Me.TESTCONNECTTextBox)
        Me.Controls.Add(Me.TESTTNSTextBox)
        Me.Controls.Add(Me.UATCONNECTTextBox)
        Me.Controls.Add(Me.UATTNSTextBox)
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
        Me.Name = "OrgSettings"
        Me.Text = "OrganisationSettings"
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
    Friend WithEvents OrgCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OrgInFeatureCheckBox As System.Windows.Forms.CheckBox
End Class
