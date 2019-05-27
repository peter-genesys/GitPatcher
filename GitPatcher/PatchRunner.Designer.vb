<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.AvailablePatchesTreeView = New TreeViewEnhanced.TreeViewEnhanced()
        Me.ComboBoxPatchesFilter = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonAll2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonFeature = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPatchSet = New System.Windows.Forms.RadioButton()
        Me.RadioButtonHotfix = New System.Windows.Forms.RadioButton()
        Me.SearchPatchesButton = New System.Windows.Forms.Button()
        Me.OrderTabPage = New System.Windows.Forms.TabPage()
        Me.UsePatchAdminCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TreeViewPatchOrder = New TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels()
        Me.CopyChangesButton = New System.Windows.Forms.Button()
        Me.RunTabPage = New System.Windows.Forms.TabPage()
        Me.MasterScriptListBox = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExecutePatchButton = New System.Windows.Forms.Button()
        Me.ExportTabPage = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ExportPatchesButton = New System.Windows.Forms.Button()
        Me.PatchListBox = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PatchRunnerTabControl.SuspendLayout()
        Me.PatchSelectorTabPage.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.OrderTabPage.SuspendLayout()
        Me.RunTabPage.SuspendLayout()
        Me.ExportTabPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'PatchRunnerTabControl
        '
        Me.PatchRunnerTabControl.Controls.Add(Me.PatchSelectorTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.OrderTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.RunTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.ExportTabPage)
        Me.PatchRunnerTabControl.Location = New System.Drawing.Point(12, 12)
        Me.PatchRunnerTabControl.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.PatchRunnerTabControl.Name = "PatchRunnerTabControl"
        Me.PatchRunnerTabControl.SelectedIndex = 0
        Me.PatchRunnerTabControl.Size = New System.Drawing.Size(458, 738)
        Me.PatchRunnerTabControl.TabIndex = 0
        '
        'PatchSelectorTabPage
        '
        Me.PatchSelectorTabPage.Controls.Add(Me.AvailablePatchesTreeView)
        Me.PatchSelectorTabPage.Controls.Add(Me.ComboBoxPatchesFilter)
        Me.PatchSelectorTabPage.Controls.Add(Me.Label1)
        Me.PatchSelectorTabPage.Controls.Add(Me.GroupBox1)
        Me.PatchSelectorTabPage.Controls.Add(Me.SearchPatchesButton)
        Me.PatchSelectorTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchSelectorTabPage.Name = "PatchSelectorTabPage"
        Me.PatchSelectorTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PatchSelectorTabPage.Size = New System.Drawing.Size(450, 712)
        Me.PatchSelectorTabPage.TabIndex = 0
        Me.PatchSelectorTabPage.Text = "Selection"
        Me.PatchSelectorTabPage.UseVisualStyleBackColor = True
        '
        'AvailablePatchesTreeView
        '
        Me.AvailablePatchesTreeView.BackColor = System.Drawing.Color.Wheat
        Me.AvailablePatchesTreeView.CheckBoxes = True
        Me.AvailablePatchesTreeView.Location = New System.Drawing.Point(9, 129)
        Me.AvailablePatchesTreeView.Name = "AvailablePatchesTreeView"
        Me.AvailablePatchesTreeView.Size = New System.Drawing.Size(429, 577)
        Me.AvailablePatchesTreeView.TabIndex = 62
        '
        'ComboBoxPatchesFilter
        '
        Me.ComboBoxPatchesFilter.FormattingEnabled = True
        Me.ComboBoxPatchesFilter.Items.AddRange(New Object() {"Unapplied", "Uninstalled", "All"})
        Me.ComboBoxPatchesFilter.Location = New System.Drawing.Point(8, 6)
        Me.ComboBoxPatchesFilter.Name = "ComboBoxPatchesFilter"
        Me.ComboBoxPatchesFilter.Size = New System.Drawing.Size(139, 21)
        Me.ComboBoxPatchesFilter.TabIndex = 61
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Available Patches"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonAll2)
        Me.GroupBox1.Controls.Add(Me.RadioButtonFeature)
        Me.GroupBox1.Controls.Add(Me.RadioButtonPatchSet)
        Me.GroupBox1.Controls.Add(Me.RadioButtonHotfix)
        Me.GroupBox1.Location = New System.Drawing.Point(296, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(139, 117)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter"
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
        'SearchPatchesButton
        '
        Me.SearchPatchesButton.Location = New System.Drawing.Point(8, 33)
        Me.SearchPatchesButton.Name = "SearchPatchesButton"
        Me.SearchPatchesButton.Size = New System.Drawing.Size(139, 23)
        Me.SearchPatchesButton.TabIndex = 34
        Me.SearchPatchesButton.Text = "Search"
        Me.SearchPatchesButton.UseVisualStyleBackColor = True
        '
        'OrderTabPage
        '
        Me.OrderTabPage.Controls.Add(Me.UsePatchAdminCheckBox)
        Me.OrderTabPage.Controls.Add(Me.Label4)
        Me.OrderTabPage.Controls.Add(Me.TreeViewPatchOrder)
        Me.OrderTabPage.Controls.Add(Me.CopyChangesButton)
        Me.OrderTabPage.Location = New System.Drawing.Point(4, 22)
        Me.OrderTabPage.Name = "OrderTabPage"
        Me.OrderTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OrderTabPage.Size = New System.Drawing.Size(450, 712)
        Me.OrderTabPage.TabIndex = 2
        Me.OrderTabPage.Text = "Order"
        Me.OrderTabPage.UseVisualStyleBackColor = True
        '
        'UsePatchAdminCheckBox
        '
        Me.UsePatchAdminCheckBox.AutoSize = True
        Me.UsePatchAdminCheckBox.Location = New System.Drawing.Point(321, 64)
        Me.UsePatchAdminCheckBox.Name = "UsePatchAdminCheckBox"
        Me.UsePatchAdminCheckBox.Size = New System.Drawing.Size(115, 17)
        Me.UsePatchAdminCheckBox.TabIndex = 53
        Me.UsePatchAdminCheckBox.Text = "Use Apex-Rel-Man"
        Me.UsePatchAdminCheckBox.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(210, 13)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Order of Execution - Drag n' drop to reorder"
        '
        'TreeViewPatchOrder
        '
        Me.TreeViewPatchOrder.BackColor = System.Drawing.Color.AliceBlue
        Me.TreeViewPatchOrder.Location = New System.Drawing.Point(7, 87)
        Me.TreeViewPatchOrder.Name = "TreeViewPatchOrder"
        Me.TreeViewPatchOrder.Size = New System.Drawing.Size(429, 615)
        Me.TreeViewPatchOrder.TabIndex = 50
        '
        'CopyChangesButton
        '
        Me.CopyChangesButton.Location = New System.Drawing.Point(8, 33)
        Me.CopyChangesButton.Name = "CopyChangesButton"
        Me.CopyChangesButton.Size = New System.Drawing.Size(139, 23)
        Me.CopyChangesButton.TabIndex = 49
        Me.CopyChangesButton.Text = "Copy Patches"
        Me.CopyChangesButton.UseVisualStyleBackColor = True
        Me.CopyChangesButton.Visible = False
        '
        'RunTabPage
        '
        Me.RunTabPage.Controls.Add(Me.MasterScriptListBox)
        Me.RunTabPage.Controls.Add(Me.Label3)
        Me.RunTabPage.Controls.Add(Me.ExecutePatchButton)
        Me.RunTabPage.Location = New System.Drawing.Point(4, 22)
        Me.RunTabPage.Name = "RunTabPage"
        Me.RunTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.RunTabPage.Size = New System.Drawing.Size(450, 712)
        Me.RunTabPage.TabIndex = 1
        Me.RunTabPage.Text = "Run"
        Me.RunTabPage.UseVisualStyleBackColor = True
        '
        'MasterScriptListBox
        '
        Me.MasterScriptListBox.FormattingEnabled = True
        Me.MasterScriptListBox.Location = New System.Drawing.Point(7, 87)
        Me.MasterScriptListBox.Name = "MasterScriptListBox"
        Me.MasterScriptListBox.Size = New System.Drawing.Size(429, 615)
        Me.MasterScriptListBox.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Master List Script"
        '
        'ExecutePatchButton
        '
        Me.ExecutePatchButton.Location = New System.Drawing.Point(8, 33)
        Me.ExecutePatchButton.Name = "ExecutePatchButton"
        Me.ExecutePatchButton.Size = New System.Drawing.Size(139, 23)
        Me.ExecutePatchButton.TabIndex = 1
        Me.ExecutePatchButton.Text = "Execute Patches"
        Me.ExecutePatchButton.UseVisualStyleBackColor = True
        '
        'ExportTabPage
        '
        Me.ExportTabPage.Controls.Add(Me.Label6)
        Me.ExportTabPage.Controls.Add(Me.ExportPatchesButton)
        Me.ExportTabPage.Controls.Add(Me.PatchListBox)
        Me.ExportTabPage.Controls.Add(Me.Label5)
        Me.ExportTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ExportTabPage.Name = "ExportTabPage"
        Me.ExportTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ExportTabPage.Size = New System.Drawing.Size(450, 712)
        Me.ExportTabPage.TabIndex = 3
        Me.ExportTabPage.Text = "Export"
        Me.ExportTabPage.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(156, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(280, 39)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "This is intended for individual patches only." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Patchsets should be exported while" &
    " creating the patchset." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The is no solution yet for Minor and Major releases."
        '
        'ExportPatchesButton
        '
        Me.ExportPatchesButton.Location = New System.Drawing.Point(8, 33)
        Me.ExportPatchesButton.Name = "ExportPatchesButton"
        Me.ExportPatchesButton.Size = New System.Drawing.Size(139, 23)
        Me.ExportPatchesButton.TabIndex = 7
        Me.ExportPatchesButton.Text = "Export Patches"
        Me.ExportPatchesButton.UseVisualStyleBackColor = True
        '
        'PatchListBox
        '
        Me.PatchListBox.FormattingEnabled = True
        Me.PatchListBox.Location = New System.Drawing.Point(7, 87)
        Me.PatchListBox.Name = "PatchListBox"
        Me.PatchListBox.Size = New System.Drawing.Size(429, 615)
        Me.PatchListBox.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Patch List"
        '
        'PatchRunner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 756)
        Me.Controls.Add(Me.PatchRunnerTabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PatchRunner"
        Me.Text = "PatchRunner"
        Me.PatchRunnerTabControl.ResumeLayout(False)
        Me.PatchSelectorTabPage.ResumeLayout(False)
        Me.PatchSelectorTabPage.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.OrderTabPage.ResumeLayout(False)
        Me.OrderTabPage.PerformLayout()
        Me.RunTabPage.ResumeLayout(False)
        Me.RunTabPage.PerformLayout()
        Me.ExportTabPage.ResumeLayout(False)
        Me.ExportTabPage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PatchRunnerTabControl As System.Windows.Forms.TabControl
    Friend WithEvents PatchSelectorTabPage As System.Windows.Forms.TabPage
    Friend WithEvents RunTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ExecutePatchButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MasterScriptListBox As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonAll2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonFeature As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPatchSet As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonHotfix As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SearchPatchesButton As System.Windows.Forms.Button
    Friend WithEvents OrderTabPage As System.Windows.Forms.TabPage
    Friend WithEvents TreeViewPatchOrder As TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels
    Friend WithEvents CopyChangesButton As System.Windows.Forms.Button
    Friend WithEvents ComboBoxPatchesFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AvailablePatchesTreeView As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents UsePatchAdminCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ExportTabPage As System.Windows.Forms.TabPage
    Friend WithEvents ExportPatchesButton As System.Windows.Forms.Button
    Friend WithEvents PatchListBox As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
