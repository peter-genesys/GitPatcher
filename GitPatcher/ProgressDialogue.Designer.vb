<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressDialogue
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
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.ProgressCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.CheckAllCheckBox = New System.Windows.Forms.CheckBox()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(12, 399)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(756, 23)
        Me.ProgressBar.TabIndex = 0
        '
        'ProgressCheckedListBox
        '
        Me.ProgressCheckedListBox.CheckOnClick = True
        Me.ProgressCheckedListBox.FormattingEnabled = True
        Me.ProgressCheckedListBox.Location = New System.Drawing.Point(12, 71)
        Me.ProgressCheckedListBox.Name = "ProgressCheckedListBox"
        Me.ProgressCheckedListBox.Size = New System.Drawing.Size(375, 319)
        Me.ProgressCheckedListBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(268, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Uncheck to skip item, Close dialogue to skip remainder."
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(350, 439)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(75, 23)
        Me.StartButton.TabIndex = 3
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'CheckAllCheckBox
        '
        Me.CheckAllCheckBox.AutoSize = True
        Me.CheckAllCheckBox.Location = New System.Drawing.Point(15, 48)
        Me.CheckAllCheckBox.Name = "CheckAllCheckBox"
        Me.CheckAllCheckBox.Size = New System.Drawing.Size(71, 17)
        Me.CheckAllCheckBox.TabIndex = 10
        Me.CheckAllCheckBox.Text = "Check All"
        Me.CheckAllCheckBox.UseVisualStyleBackColor = True
        '
        'NotesTextBox
        '
        Me.NotesTextBox.Location = New System.Drawing.Point(393, 71)
        Me.NotesTextBox.Multiline = True
        Me.NotesTextBox.Name = "NotesTextBox"
        Me.NotesTextBox.ReadOnly = True
        Me.NotesTextBox.Size = New System.Drawing.Size(375, 319)
        Me.NotesTextBox.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(399, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Notes"
        '
        'ProgressDialogue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 474)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.NotesTextBox)
        Me.Controls.Add(Me.CheckAllCheckBox)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressCheckedListBox)
        Me.Controls.Add(Me.ProgressBar)
        Me.Name = "ProgressDialogue"
        Me.Text = "Workflow"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents CheckAllCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents NotesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
