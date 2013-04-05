<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PatchFromTags
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
        Me.Tag1TextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tag2TextBox = New System.Windows.Forms.TextBox()
        Me.FindButton = New System.Windows.Forms.Button()
        Me.ChangesCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PatchButton = New System.Windows.Forms.Button()
        Me.ViewFilesCheckBox = New System.Windows.Forms.CheckBox()
        Me.CheckAllCheckBox = New System.Windows.Forms.CheckBox()
        Me.SchemaComboBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.RemoveButton = New System.Windows.Forms.Button()
        Me.PatchDirTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PatchNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Tag1TextBox
        '
        Me.Tag1TextBox.Location = New System.Drawing.Point(75, 34)
        Me.Tag1TextBox.Name = "Tag1TextBox"
        Me.Tag1TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag1TextBox.TabIndex = 0
        Me.Tag1TextBox.Text = "TAG1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tag 1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tag 2"
        '
        'Tag2TextBox
        '
        Me.Tag2TextBox.Location = New System.Drawing.Point(75, 60)
        Me.Tag2TextBox.Name = "Tag2TextBox"
        Me.Tag2TextBox.Size = New System.Drawing.Size(139, 20)
        Me.Tag2TextBox.TabIndex = 2
        Me.Tag2TextBox.Text = "TAG2"
        '
        'FindButton
        '
        Me.FindButton.Location = New System.Drawing.Point(75, 86)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(139, 23)
        Me.FindButton.TabIndex = 4
        Me.FindButton.Text = "Find Changes"
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'ChangesCheckedListBox
        '
        Me.ChangesCheckedListBox.FormattingEnabled = True
        Me.ChangesCheckedListBox.Location = New System.Drawing.Point(75, 154)
        Me.ChangesCheckedListBox.Name = "ChangesCheckedListBox"
        Me.ChangesCheckedListBox.Size = New System.Drawing.Size(505, 394)
        Me.ChangesCheckedListBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 154)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Changes"
        '
        'PatchButton
        '
        Me.PatchButton.Location = New System.Drawing.Point(75, 634)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(142, 23)
        Me.PatchButton.TabIndex = 7
        Me.PatchButton.Text = "Patch Selected Files"
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'ViewFilesCheckBox
        '
        Me.ViewFilesCheckBox.AutoSize = True
        Me.ViewFilesCheckBox.Location = New System.Drawing.Point(229, 92)
        Me.ViewFilesCheckBox.Name = "ViewFilesCheckBox"
        Me.ViewFilesCheckBox.Size = New System.Drawing.Size(73, 17)
        Me.ViewFilesCheckBox.TabIndex = 8
        Me.ViewFilesCheckBox.Text = "View Files"
        Me.ViewFilesCheckBox.UseVisualStyleBackColor = True
        '
        'CheckAllCheckBox
        '
        Me.CheckAllCheckBox.AutoSize = True
        Me.CheckAllCheckBox.Location = New System.Drawing.Point(75, 124)
        Me.CheckAllCheckBox.Name = "CheckAllCheckBox"
        Me.CheckAllCheckBox.Size = New System.Drawing.Size(71, 17)
        Me.CheckAllCheckBox.TabIndex = 9
        Me.CheckAllCheckBox.Text = "Check All"
        Me.CheckAllCheckBox.UseVisualStyleBackColor = True
        '
        'SchemaComboBox
        '
        Me.SchemaComboBox.FormattingEnabled = True
        Me.SchemaComboBox.Location = New System.Drawing.Point(278, 60)
        Me.SchemaComboBox.Name = "SchemaComboBox"
        Me.SchemaComboBox.Size = New System.Drawing.Size(183, 21)
        Me.SchemaComboBox.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(226, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Schema"
        '
        'ViewButton
        '
        Me.ViewButton.Location = New System.Drawing.Point(75, 554)
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.Size = New System.Drawing.Size(142, 23)
        Me.ViewButton.TabIndex = 12
        Me.ViewButton.Text = "View Selected Files"
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'RemoveButton
        '
        Me.RemoveButton.Location = New System.Drawing.Point(75, 663)
        Me.RemoveButton.Name = "RemoveButton"
        Me.RemoveButton.Size = New System.Drawing.Size(142, 23)
        Me.RemoveButton.TabIndex = 13
        Me.RemoveButton.Text = "Remove Selected Files"
        Me.RemoveButton.UseVisualStyleBackColor = True
        '
        'PatchDirTextBox
        '
        Me.PatchDirTextBox.Location = New System.Drawing.Point(75, 608)
        Me.PatchDirTextBox.Name = "PatchDirTextBox"
        Me.PatchDirTextBox.ReadOnly = True
        Me.PatchDirTextBox.Size = New System.Drawing.Size(505, 20)
        Me.PatchDirTextBox.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 611)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Patch Dir"
        '
        'PatchNameTextBox
        '
        Me.PatchNameTextBox.Location = New System.Drawing.Point(75, 583)
        Me.PatchNameTextBox.Name = "PatchNameTextBox"
        Me.PatchNameTextBox.Size = New System.Drawing.Size(505, 20)
        Me.PatchNameTextBox.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 586)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Patch Name"
        '
        'PatchFromTags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 706)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PatchNameTextBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PatchDirTextBox)
        Me.Controls.Add(Me.RemoveButton)
        Me.Controls.Add(Me.ViewButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.SchemaComboBox)
        Me.Controls.Add(Me.CheckAllCheckBox)
        Me.Controls.Add(Me.ViewFilesCheckBox)
        Me.Controls.Add(Me.PatchButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ChangesCheckedListBox)
        Me.Controls.Add(Me.FindButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Tag2TextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Tag1TextBox)
        Me.Name = "PatchFromTags"
        Me.Text = "PatchFromTags"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Tag1TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Tag2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents FindButton As System.Windows.Forms.Button
    Friend WithEvents ChangesCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PatchButton As System.Windows.Forms.Button
    Friend WithEvents ViewFilesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CheckAllCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SchemaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ViewButton As System.Windows.Forms.Button
    Friend WithEvents RemoveButton As System.Windows.Forms.Button
    Friend WithEvents PatchDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PatchNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
