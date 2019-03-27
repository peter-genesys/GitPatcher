<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ApexAppInstaller
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ApexAppInstaller))
        Me.PatchRunnerTabControl = New System.Windows.Forms.TabControl()
        Me.AppSelectorTabPage = New System.Windows.Forms.TabPage()
        Me.UsePatchAdminCheckBox = New System.Windows.Forms.CheckBox()
        Me.AvailableAppsTreeView = New TreeViewEnhanced.TreeViewEnhanced()
        Me.ComboBoxAppsFilter = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonAnyone = New System.Windows.Forms.RadioButton()
        Me.RadioButtonMe = New System.Windows.Forms.RadioButton()
        Me.RadioButtonOthers = New System.Windows.Forms.RadioButton()
        Me.SearchApexAppsButton = New System.Windows.Forms.Button()
        Me.OrderTabPage = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TreeViewPatchOrder = New TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels()
        Me.CopyChangesButton = New System.Windows.Forms.Button()
        Me.RunTabPage = New System.Windows.Forms.TabPage()
        Me.MasterScriptListBox = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ExecutePatchButton = New System.Windows.Forms.Button()
        Me.PatchRunnerTabControl.SuspendLayout()
        Me.AppSelectorTabPage.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.OrderTabPage.SuspendLayout()
        Me.RunTabPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'PatchRunnerTabControl
        '
        Me.PatchRunnerTabControl.Controls.Add(Me.AppSelectorTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.OrderTabPage)
        Me.PatchRunnerTabControl.Controls.Add(Me.RunTabPage)
        Me.PatchRunnerTabControl.Location = New System.Drawing.Point(9, 9)
        Me.PatchRunnerTabControl.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.PatchRunnerTabControl.Name = "PatchRunnerTabControl"
        Me.PatchRunnerTabControl.SelectedIndex = 0
        Me.PatchRunnerTabControl.Size = New System.Drawing.Size(458, 738)
        Me.PatchRunnerTabControl.TabIndex = 1
        '
        'AppSelectorTabPage
        '
        Me.AppSelectorTabPage.Controls.Add(Me.UsePatchAdminCheckBox)
        Me.AppSelectorTabPage.Controls.Add(Me.AvailableAppsTreeView)
        Me.AppSelectorTabPage.Controls.Add(Me.ComboBoxAppsFilter)
        Me.AppSelectorTabPage.Controls.Add(Me.Label1)
        Me.AppSelectorTabPage.Controls.Add(Me.GroupBox1)
        Me.AppSelectorTabPage.Controls.Add(Me.SearchApexAppsButton)
        Me.AppSelectorTabPage.Location = New System.Drawing.Point(4, 22)
        Me.AppSelectorTabPage.Name = "AppSelectorTabPage"
        Me.AppSelectorTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AppSelectorTabPage.Size = New System.Drawing.Size(450, 712)
        Me.AppSelectorTabPage.TabIndex = 0
        Me.AppSelectorTabPage.Text = "Selection"
        Me.AppSelectorTabPage.UseVisualStyleBackColor = True
        '
        'UsePatchAdminCheckBox
        '
        Me.UsePatchAdminCheckBox.AutoSize = True
        Me.UsePatchAdminCheckBox.Location = New System.Drawing.Point(316, 104)
        Me.UsePatchAdminCheckBox.Name = "UsePatchAdminCheckBox"
        Me.UsePatchAdminCheckBox.Size = New System.Drawing.Size(115, 17)
        Me.UsePatchAdminCheckBox.TabIndex = 63
        Me.UsePatchAdminCheckBox.Text = "Use Apex-Rel-Man"
        Me.UsePatchAdminCheckBox.UseVisualStyleBackColor = True
        '
        'AvailableAppsTreeView
        '
        Me.AvailableAppsTreeView.BackColor = System.Drawing.Color.Wheat
        Me.AvailableAppsTreeView.CheckBoxes = True
        Me.AvailableAppsTreeView.Location = New System.Drawing.Point(9, 129)
        Me.AvailableAppsTreeView.Name = "AvailableAppsTreeView"
        Me.AvailableAppsTreeView.Size = New System.Drawing.Size(429, 577)
        Me.AvailableAppsTreeView.TabIndex = 62
        '
        'ComboBoxAppsFilter
        '
        Me.ComboBoxAppsFilter.FormattingEnabled = True
        Me.ComboBoxAppsFilter.Items.AddRange(New Object() {"Queued", "All"})
        Me.ComboBoxAppsFilter.Location = New System.Drawing.Point(8, 6)
        Me.ComboBoxAppsFilter.Name = "ComboBoxAppsFilter"
        Me.ComboBoxAppsFilter.Size = New System.Drawing.Size(139, 21)
        Me.ComboBoxAppsFilter.TabIndex = 61
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Available Apex Apps"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonAnyone)
        Me.GroupBox1.Controls.Add(Me.RadioButtonMe)
        Me.GroupBox1.Controls.Add(Me.RadioButtonOthers)
        Me.GroupBox1.Location = New System.Drawing.Point(296, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(139, 92)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Queued-By Filter"
        '
        'RadioButtonAnyone
        '
        Me.RadioButtonAnyone.AutoSize = True
        Me.RadioButtonAnyone.Location = New System.Drawing.Point(20, 65)
        Me.RadioButtonAnyone.Name = "RadioButtonAnyone"
        Me.RadioButtonAnyone.Size = New System.Drawing.Size(61, 17)
        Me.RadioButtonAnyone.TabIndex = 38
        Me.RadioButtonAnyone.Text = "Anyone"
        Me.RadioButtonAnyone.UseVisualStyleBackColor = True
        '
        'RadioButtonMe
        '
        Me.RadioButtonMe.AutoSize = True
        Me.RadioButtonMe.Checked = True
        Me.RadioButtonMe.Location = New System.Drawing.Point(20, 19)
        Me.RadioButtonMe.Name = "RadioButtonMe"
        Me.RadioButtonMe.Size = New System.Drawing.Size(40, 17)
        Me.RadioButtonMe.TabIndex = 35
        Me.RadioButtonMe.TabStop = True
        Me.RadioButtonMe.Text = "Me"
        Me.RadioButtonMe.UseVisualStyleBackColor = True
        '
        'RadioButtonOthers
        '
        Me.RadioButtonOthers.AutoSize = True
        Me.RadioButtonOthers.Location = New System.Drawing.Point(20, 42)
        Me.RadioButtonOthers.Name = "RadioButtonOthers"
        Me.RadioButtonOthers.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonOthers.TabIndex = 36
        Me.RadioButtonOthers.Text = "Others"
        Me.RadioButtonOthers.UseVisualStyleBackColor = True
        '
        'SearchApexAppsButton
        '
        Me.SearchApexAppsButton.Location = New System.Drawing.Point(8, 33)
        Me.SearchApexAppsButton.Name = "SearchApexAppsButton"
        Me.SearchApexAppsButton.Size = New System.Drawing.Size(139, 23)
        Me.SearchApexAppsButton.TabIndex = 34
        Me.SearchApexAppsButton.Text = "Search"
        Me.SearchApexAppsButton.UseVisualStyleBackColor = True
        '
        'OrderTabPage
        '
        Me.OrderTabPage.Controls.Add(Me.Label4)
        Me.OrderTabPage.Controls.Add(Me.Label2)
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(210, 13)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Order of Execution - Drag n' drop to reorder"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(153, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(217, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "(Rechecks dependancy order, can be slow.)"
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
        Me.ExecutePatchButton.Text = "Install Apex Apps"
        Me.ExecutePatchButton.UseVisualStyleBackColor = True
        '
        'ApexAppInstaller
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 756)
        Me.Controls.Add(Me.PatchRunnerTabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ApexAppInstaller"
        Me.Text = "ApexAppInstaller"
        Me.PatchRunnerTabControl.ResumeLayout(False)
        Me.AppSelectorTabPage.ResumeLayout(False)
        Me.AppSelectorTabPage.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.OrderTabPage.ResumeLayout(False)
        Me.OrderTabPage.PerformLayout()
        Me.RunTabPage.ResumeLayout(False)
        Me.RunTabPage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PatchRunnerTabControl As TabControl
    Friend WithEvents AppSelectorTabPage As TabPage
    Friend WithEvents AvailableAppsTreeView As TreeViewEnhanced.TreeViewEnhanced
    Friend WithEvents ComboBoxAppsFilter As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButtonAnyone As RadioButton
    Friend WithEvents RadioButtonMe As RadioButton
    Friend WithEvents RadioButtonOthers As RadioButton
    Friend WithEvents SearchApexAppsButton As Button
    Friend WithEvents OrderTabPage As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TreeViewPatchOrder As TreeViewDraggableNodes2Levels.TreeViewDraggableNodes2Levels
    Friend WithEvents CopyChangesButton As Button
    Friend WithEvents RunTabPage As TabPage
    Friend WithEvents MasterScriptListBox As ListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ExecutePatchButton As Button
    Friend WithEvents UsePatchAdminCheckBox As CheckBox
End Class
