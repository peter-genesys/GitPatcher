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
        Me.SuspendLayout()
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(12, 359)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(375, 23)
        Me.ProgressBar.TabIndex = 0
        '
        'ProgressCheckedListBox
        '
        Me.ProgressCheckedListBox.CheckOnClick = True
        Me.ProgressCheckedListBox.FormattingEnabled = True
        Me.ProgressCheckedListBox.Location = New System.Drawing.Point(12, 46)
        Me.ProgressCheckedListBox.Name = "ProgressCheckedListBox"
        Me.ProgressCheckedListBox.Size = New System.Drawing.Size(375, 304)
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
        'ProgressDialogue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 391)
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
End Class
