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
        Me.SuspendLayout()
        '
        'Tag1TextBox
        '
        Me.Tag1TextBox.Location = New System.Drawing.Point(57, 34)
        Me.Tag1TextBox.Name = "Tag1TextBox"
        Me.Tag1TextBox.Size = New System.Drawing.Size(122, 20)
        Me.Tag1TextBox.TabIndex = 0
        Me.Tag1TextBox.Text = "TAG1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tag 1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tag 2"
        '
        'Tag2TextBox
        '
        Me.Tag2TextBox.Location = New System.Drawing.Point(57, 60)
        Me.Tag2TextBox.Name = "Tag2TextBox"
        Me.Tag2TextBox.Size = New System.Drawing.Size(122, 20)
        Me.Tag2TextBox.TabIndex = 2
        Me.Tag2TextBox.Text = "TAG2"
        '
        'FindButton
        '
        Me.FindButton.Location = New System.Drawing.Point(57, 86)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(122, 23)
        Me.FindButton.TabIndex = 4
        Me.FindButton.Text = "Find Changes"
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'ChangesCheckedListBox
        '
        Me.ChangesCheckedListBox.FormattingEnabled = True
        Me.ChangesCheckedListBox.Location = New System.Drawing.Point(57, 126)
        Me.ChangesCheckedListBox.Name = "ChangesCheckedListBox"
        Me.ChangesCheckedListBox.Size = New System.Drawing.Size(377, 394)
        Me.ChangesCheckedListBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(2, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Changes"
        '
        'PatchButton
        '
        Me.PatchButton.Location = New System.Drawing.Point(57, 535)
        Me.PatchButton.Name = "PatchButton"
        Me.PatchButton.Size = New System.Drawing.Size(122, 23)
        Me.PatchButton.TabIndex = 7
        Me.PatchButton.Text = "Patch Selected Files"
        Me.PatchButton.UseVisualStyleBackColor = True
        '
        'PatchFromTags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 570)
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
End Class
