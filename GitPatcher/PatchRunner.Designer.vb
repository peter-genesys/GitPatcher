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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PatchRunner))
        Me.PatchRunnerTabControl = New System.Windows.Forms.TabControl()
        Me.PatchSelectorTabPage = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.PatchFilterGroupBox = New System.Windows.Forms.GroupBox()
        Me.RadioButtonUnapplied = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll = New System.Windows.Forms.RadioButton()
        Me.RadioButtonUninstalled = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SearchPatchesButton = New System.Windows.Forms.Button()
        Me.AvailablePatchesListBox = New System.Windows.Forms.ListBox()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.ChooseAllButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChosenPatchesListBox = New System.Windows.Forms.ListBox()
        Me.RunTabPage = New System.Windows.Forms.TabPage()
        Me.MasterScriptListBox = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExecutePatchButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonFeature = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPatchSet = New System.Windows.Forms.RadioButton()
        Me.RadioButtonHotfix = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll2 = New System.Windows.Forms.RadioButton()
        Me.PatchRunnerTabControl.SuspendLayout()
        Me.PatchSelectorTabPage.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.PatchFilterGroupBox.SuspendLayout()
        Me.RunTabPage.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PatchRunnerTabControl
        '
        Me.PatchRunnerTabControl.Controls.Add(Me.PatchSelectorTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.RunTabPage)
        Me.PatchRunnerTabControl.Location = New System.Drawing.Point(12, 12)
        Me.PatchRunnerTabControl.MaximumSize = New System.Drawing.Size(1000, 1000)
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PatchFilterGroupBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SearchPatchesButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.AvailablePatchesListBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ClearButton)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChooseAllButton)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChosenPatchesListBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(746, 706)
        Me.SplitContainer1.SplitterDistance = 362
        Me.SplitContainer1.TabIndex = 0
        '
        'PatchFilterGroupBox
        '
        Me.PatchFilterGroupBox.Controls.Add(Me.RadioButtonUnapplied)
        Me.PatchFilterGroupBox.Controls.Add(Me.RadioButtonAll)
        Me.PatchFilterGroupBox.Controls.Add(Me.RadioButtonUninstalled)
        Me.PatchFilterGroupBox.Location = New System.Drawing.Point(7, 10)
        Me.PatchFilterGroupBox.Name = "PatchFilterGroupBox"
        Me.PatchFilterGroupBox.Size = New System.Drawing.Size(144, 93)
        Me.PatchFilterGroupBox.TabIndex = 1
        Me.PatchFilterGroupBox.TabStop = False
        Me.PatchFilterGroupBox.Text = "Filter"
        '
        'RadioButtonUnapplied
        '
        Me.RadioButtonUnapplied.AutoSize = True
        Me.RadioButtonUnapplied.Checked = True
        Me.RadioButtonUnapplied.Location = New System.Drawing.Point(27, 19)
        Me.RadioButtonUnapplied.Name = "RadioButtonUnapplied"
        Me.RadioButtonUnapplied.Size = New System.Drawing.Size(73, 17)
        Me.RadioButtonUnapplied.TabIndex = 35
        Me.RadioButtonUnapplied.TabStop = True
        Me.RadioButtonUnapplied.Text = "Unapplied"
        Me.RadioButtonUnapplied.UseVisualStyleBackColor = True
        '
        'RadioButtonAll
        '
        Me.RadioButtonAll.AutoSize = True
        Me.RadioButtonAll.Location = New System.Drawing.Point(27, 65)
        Me.RadioButtonAll.Name = "RadioButtonAll"
        Me.RadioButtonAll.Size = New System.Drawing.Size(36, 17)
        Me.RadioButtonAll.TabIndex = 37
        Me.RadioButtonAll.Text = "All"
        Me.RadioButtonAll.UseVisualStyleBackColor = True
        '
        'RadioButtonUninstalled
        '
        Me.RadioButtonUninstalled.AutoSize = True
        Me.RadioButtonUninstalled.Location = New System.Drawing.Point(27, 42)
        Me.RadioButtonUninstalled.Name = "RadioButtonUninstalled"
        Me.RadioButtonUninstalled.Size = New System.Drawing.Size(77, 17)
        Me.RadioButtonUninstalled.TabIndex = 36
        Me.RadioButtonUninstalled.Text = "Uninstalled"
        Me.RadioButtonUninstalled.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Available Patches"
        '
        'SearchPatchesButton
        '
        Me.SearchPatchesButton.Location = New System.Drawing.Point(202, 136)
        Me.SearchPatchesButton.Name = "SearchPatchesButton"
        Me.SearchPatchesButton.Size = New System.Drawing.Size(139, 23)
        Me.SearchPatchesButton.TabIndex = 34
        Me.SearchPatchesButton.Text = "Search"
        Me.SearchPatchesButton.UseVisualStyleBackColor = True
        '
        'AvailablePatchesListBox
        '
        Me.AvailablePatchesListBox.FormattingEnabled = True
        Me.AvailablePatchesListBox.Location = New System.Drawing.Point(7, 165)
        Me.AvailablePatchesListBox.Name = "AvailablePatchesListBox"
        Me.AvailablePatchesListBox.Size = New System.Drawing.Size(523, 537)
        Me.AvailablePatchesListBox.TabIndex = 0
        '
        'ClearButton
        '
        Me.ClearButton.Location = New System.Drawing.Point(13, 39)
        Me.ClearButton.Name = "ClearButton"
        Me.ClearButton.Size = New System.Drawing.Size(75, 23)
        Me.ClearButton.TabIndex = 41
        Me.ClearButton.Text = "Clear"
        Me.ClearButton.UseVisualStyleBackColor = True
        '
        'ChooseAllButton
        '
        Me.ChooseAllButton.Location = New System.Drawing.Point(13, 10)
        Me.ChooseAllButton.Name = "ChooseAllButton"
        Me.ChooseAllButton.Size = New System.Drawing.Size(75, 23)
        Me.ChooseAllButton.TabIndex = 40
        Me.ChooseAllButton.Text = "Choose All"
        Me.ChooseAllButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Chosen Patches"
        '
        'ChosenPatchesListBox
        '
        Me.ChosenPatchesListBox.FormattingEnabled = True
        Me.ChosenPatchesListBox.Location = New System.Drawing.Point(3, 165)
        Me.ChosenPatchesListBox.Name = "ChosenPatchesListBox"
        Me.ChosenPatchesListBox.Size = New System.Drawing.Size(559, 537)
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonAll2)
        Me.GroupBox1.Controls.Add(Me.RadioButtonFeature)
        Me.GroupBox1.Controls.Add(Me.RadioButtonPatchSet)
        Me.GroupBox1.Controls.Add(Me.RadioButtonHotfix)
        Me.GroupBox1.Location = New System.Drawing.Point(197, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(144, 120)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter"
        '
        'RadioButtonFeature
        '
        Me.RadioButtonFeature.AutoSize = True
        Me.RadioButtonFeature.Checked = True
        Me.RadioButtonFeature.Location = New System.Drawing.Point(27, 19)
        Me.RadioButtonFeature.Name = "RadioButtonFeature"
        Me.RadioButtonFeature.Size = New System.Drawing.Size(61, 17)
        Me.RadioButtonFeature.TabIndex = 35
        Me.RadioButtonFeature.TabStop = True
        Me.RadioButtonFeature.Text = "Feature"
        Me.RadioButtonFeature.UseVisualStyleBackColor = True
        '
        'RadioButtonPatchSet
        '
        Me.RadioButtonPatchSet.AutoSize = True
        Me.RadioButtonPatchSet.Location = New System.Drawing.Point(27, 65)
        Me.RadioButtonPatchSet.Name = "RadioButtonPatchSet"
        Me.RadioButtonPatchSet.Size = New System.Drawing.Size(67, 17)
        Me.RadioButtonPatchSet.TabIndex = 37
        Me.RadioButtonPatchSet.Text = "Patchset"
        Me.RadioButtonPatchSet.UseVisualStyleBackColor = True
        '
        'RadioButtonHotfix
        '
        Me.RadioButtonHotfix.AutoSize = True
        Me.RadioButtonHotfix.Location = New System.Drawing.Point(27, 42)
        Me.RadioButtonHotfix.Name = "RadioButtonHotfix"
        Me.RadioButtonHotfix.Size = New System.Drawing.Size(52, 17)
        Me.RadioButtonHotfix.TabIndex = 36
        Me.RadioButtonHotfix.Text = "Hotfix"
        Me.RadioButtonHotfix.UseVisualStyleBackColor = True
        '
        'RadioButtonAll2
        '
        Me.RadioButtonAll2.AutoSize = True
        Me.RadioButtonAll2.Location = New System.Drawing.Point(27, 88)
        Me.RadioButtonAll2.Name = "RadioButtonAll2"
        Me.RadioButtonAll2.Size = New System.Drawing.Size(36, 17)
        Me.RadioButtonAll2.TabIndex = 38
        Me.RadioButtonAll2.Text = "All"
        Me.RadioButtonAll2.UseVisualStyleBackColor = True
        '
        'PatchRunner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 762)
        Me.Controls.Add(Me.PatchRunnerTabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
        Me.PatchFilterGroupBox.ResumeLayout(False)
        Me.PatchFilterGroupBox.PerformLayout()
        Me.RunTabPage.ResumeLayout(False)
        Me.RunTabPage.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents ClearButton As System.Windows.Forms.Button
    Friend WithEvents ChooseAllButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonAll2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonFeature As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPatchSet As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonHotfix As System.Windows.Forms.RadioButton
End Class
