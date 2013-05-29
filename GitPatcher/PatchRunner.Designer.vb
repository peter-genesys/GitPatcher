﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PatchRunner
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
        Me.PatchRunnerTabControl = New System.Windows.Forms.TabControl()
        Me.PatchSelectorTabPage = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SearchPatchesButton = New System.Windows.Forms.Button()
        Me.AvailablePatchesListBox = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChosenPatchesListBox = New System.Windows.Forms.ListBox()
        Me.RunTabPage = New System.Windows.Forms.TabPage()
        Me.MasterScriptListBox = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExecutePatchButton = New System.Windows.Forms.Button()
        Me.RadioButtonUnapplied = New System.Windows.Forms.RadioButton()
        Me.RadioButtonUninstalled = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll = New System.Windows.Forms.RadioButton()
        Me.PatchFilterGroupBox = New System.Windows.Forms.GroupBox()
        Me.PatchRunnerTabControl.SuspendLayout()
        Me.PatchSelectorTabPage.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.RunTabPage.SuspendLayout()
        Me.PatchFilterGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'PatchRunnerTabControl
        '
        Me.PatchRunnerTabControl.Controls.Add(Me.PatchSelectorTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.RunTabPage)
        Me.PatchRunnerTabControl.Location = New System.Drawing.Point(12, 12)
        Me.PatchRunnerTabControl.Name = "PatchRunnerTabControl"
        Me.PatchRunnerTabControl.SelectedIndex = 0
        Me.PatchRunnerTabControl.Size = New System.Drawing.Size(760, 738)
        Me.PatchRunnerTabControl.TabIndex = 0
        '
        'PatchSelectorTabPage
        '
        Me.PatchSelectorTabPage.Controls.Add(Me.SplitContainer1)
        Me.PatchSelectorTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchSelectorTabPage.Name = "PatchSelectorTabPage"
        Me.PatchSelectorTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PatchSelectorTabPage.Size = New System.Drawing.Size(752, 712)
        Me.PatchSelectorTabPage.TabIndex = 0
        Me.PatchSelectorTabPage.Text = "Selection"
        Me.PatchSelectorTabPage.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.PatchFilterGroupBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SearchPatchesButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.AvailablePatchesListBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChosenPatchesListBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(746, 706)
        Me.SplitContainer1.SplitterDistance = 359
        Me.SplitContainer1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Available Patches"
        '
        'SearchPatchesButton
        '
        Me.SearchPatchesButton.Location = New System.Drawing.Point(7, 10)
        Me.SearchPatchesButton.Name = "SearchPatchesButton"
        Me.SearchPatchesButton.Size = New System.Drawing.Size(139, 23)
        Me.SearchPatchesButton.TabIndex = 34
        Me.SearchPatchesButton.Text = "Search"
        Me.SearchPatchesButton.UseVisualStyleBackColor = True
        '
        'AvailablePatchesListBox
        '
        Me.AvailablePatchesListBox.FormattingEnabled = True
        Me.AvailablePatchesListBox.Location = New System.Drawing.Point(7, 113)
        Me.AvailablePatchesListBox.Name = "AvailablePatchesListBox"
        Me.AvailablePatchesListBox.Size = New System.Drawing.Size(349, 589)
        Me.AvailablePatchesListBox.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Chosen Patches"
        '
        'ChosenPatchesListBox
        '
        Me.ChosenPatchesListBox.FormattingEnabled = True
        Me.ChosenPatchesListBox.Location = New System.Drawing.Point(3, 113)
        Me.ChosenPatchesListBox.Name = "ChosenPatchesListBox"
        Me.ChosenPatchesListBox.Size = New System.Drawing.Size(377, 589)
        Me.ChosenPatchesListBox.TabIndex = 1
        '
        'RunTabPage
        '
        Me.RunTabPage.Controls.Add(Me.MasterScriptListBox)
        Me.RunTabPage.Controls.Add(Me.Label3)
        Me.RunTabPage.Controls.Add(Me.ExecutePatchButton)
        Me.RunTabPage.Location = New System.Drawing.Point(4, 22)
        Me.RunTabPage.Name = "RunTabPage"
        Me.RunTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.RunTabPage.Size = New System.Drawing.Size(752, 712)
        Me.RunTabPage.TabIndex = 1
        Me.RunTabPage.Text = "Run"
        Me.RunTabPage.UseVisualStyleBackColor = True
        '
        'MasterScriptListBox
        '
        Me.MasterScriptListBox.FormattingEnabled = True
        Me.MasterScriptListBox.Location = New System.Drawing.Point(7, 61)
        Me.MasterScriptListBox.Name = "MasterScriptListBox"
        Me.MasterScriptListBox.Size = New System.Drawing.Size(739, 641)
        Me.MasterScriptListBox.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Master List Script"
        '
        'ExecutePatchButton
        '
        Me.ExecutePatchButton.Location = New System.Drawing.Point(7, 10)
        Me.ExecutePatchButton.Name = "ExecutePatchButton"
        Me.ExecutePatchButton.Size = New System.Drawing.Size(139, 23)
        Me.ExecutePatchButton.TabIndex = 1
        Me.ExecutePatchButton.Text = "Execute Patches"
        Me.ExecutePatchButton.UseVisualStyleBackColor = True
        '
        'RadioButtonUnapplied
        '
        Me.RadioButtonUnapplied.AutoSize = True
        Me.RadioButtonUnapplied.Location = New System.Drawing.Point(27, 19)
        Me.RadioButtonUnapplied.Name = "RadioButtonUnapplied"
        Me.RadioButtonUnapplied.Size = New System.Drawing.Size(73, 17)
        Me.RadioButtonUnapplied.TabIndex = 35
        Me.RadioButtonUnapplied.TabStop = True
        Me.RadioButtonUnapplied.Text = "Unapplied"
        Me.RadioButtonUnapplied.UseVisualStyleBackColor = True
        '
        'RadioButtonUninstalled
        '
        Me.RadioButtonUninstalled.AutoSize = True
        Me.RadioButtonUninstalled.Location = New System.Drawing.Point(27, 42)
        Me.RadioButtonUninstalled.Name = "RadioButtonUninstalled"
        Me.RadioButtonUninstalled.Size = New System.Drawing.Size(77, 17)
        Me.RadioButtonUninstalled.TabIndex = 36
        Me.RadioButtonUninstalled.TabStop = True
        Me.RadioButtonUninstalled.Text = "Uninstalled"
        Me.RadioButtonUninstalled.UseVisualStyleBackColor = True
        '
        'RadioButtonAll
        '
        Me.RadioButtonAll.AutoSize = True
        Me.RadioButtonAll.Location = New System.Drawing.Point(27, 65)
        Me.RadioButtonAll.Name = "RadioButtonAll"
        Me.RadioButtonAll.Size = New System.Drawing.Size(36, 17)
        Me.RadioButtonAll.TabIndex = 37
        Me.RadioButtonAll.TabStop = True
        Me.RadioButtonAll.Text = "All"
        Me.RadioButtonAll.UseVisualStyleBackColor = True
        '
        'PatchFilterGroupBox
        '
        Me.PatchFilterGroupBox.Controls.Add(Me.RadioButtonUnapplied)
        Me.PatchFilterGroupBox.Controls.Add(Me.RadioButtonAll)
        Me.PatchFilterGroupBox.Controls.Add(Me.RadioButtonUninstalled)
        Me.PatchFilterGroupBox.Location = New System.Drawing.Point(163, 10)
        Me.PatchFilterGroupBox.Name = "PatchFilterGroupBox"
        Me.PatchFilterGroupBox.Size = New System.Drawing.Size(182, 93)
        Me.PatchFilterGroupBox.TabIndex = 1
        Me.PatchFilterGroupBox.TabStop = False
        Me.PatchFilterGroupBox.Text = "Filter"
        '
        'PatchRunner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 762)
        Me.Controls.Add(Me.PatchRunnerTabControl)
        Me.Name = "PatchRunner"
        Me.Text = "PatchRunner"
        Me.PatchRunnerTabControl.ResumeLayout(False)
        Me.PatchSelectorTabPage.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.RunTabPage.ResumeLayout(False)
        Me.RunTabPage.PerformLayout()
        Me.PatchFilterGroupBox.ResumeLayout(False)
        Me.PatchFilterGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PatchRunnerTabControl As System.Windows.Forms.TabControl
    Friend WithEvents PatchSelectorTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents AvailablePatchesListBox As System.Windows.Forms.ListBox
    Friend WithEvents ChosenPatchesListBox As System.Windows.Forms.ListBox
    Friend WithEvents RunTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SearchPatchesButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ExecutePatchButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MasterScriptListBox As System.Windows.Forms.ListBox
    Friend WithEvents PatchFilterGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonUnapplied As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAll As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonUninstalled As System.Windows.Forms.RadioButton
End Class
