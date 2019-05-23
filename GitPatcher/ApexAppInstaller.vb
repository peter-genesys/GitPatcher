Imports Oracle.ManagedDataAccess.Client

Public Class ApexAppInstaller

    Private tagA As String = Nothing
    Private tagB As String = Nothing

    Private waiting As Boolean

    Public Sub New(Optional ByVal ienqueuedStatus As String = "All", Optional ByVal itagA As String = "", Optional ByVal itagB As String = "", Optional ByVal queuedBy As String = "me")
        InitializeComponent()

        Me.tagA = itagA
        Me.tagB = itagB

        'Other legal values Unapplied and Uninstalled
        ComboBoxAppsFilter.SelectedItem = ienqueuedStatus

        Me.Text = "Apex App Installer - Running Apps on " & Globals.currentTNS


        'hotFixTargetDBFilter = Globals.getDB()
        'If hotFixTargetDBFilter = "VM" Then
        '    hotFixTargetDBFilter = "DEV"
        'End If

        'RadioButtonHotfix.Text = "Hotfixes for " & hotFixTargetDBFilter

        'If iBranchType = "" Then
        '    iBranchType = Globals.getPatchRunnerFilter
        'End If

        Logger.Note("queuedBy", queuedBy)
        Select Case queuedBy
            Case "me"
                RadioButtonMe.Checked = True
            Case "others"
                RadioButtonOthers.Checked = True
            Case "anyone"
                RadioButtonAnyone.Checked = True
            Case Else
                RadioButtonAnyone.Checked = True
        End Select

        UsePatchAdminCheckBox.Checked = Globals.getUseARM


        Me.MdiParent = GitPatcher
        If doSearch() > 0 Then
            Me.Show()
            Wait()
        Else
            Me.Close()
        End If



    End Sub

    Public Sub Wait()
        'This wait routine will halt the caller until the form is closed.
        waiting = True
        Do Until Not waiting
            Common.wait(1000)
        Loop
    End Sub

    Private Sub ApexAppInstaller_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        waiting = False
    End Sub



    Public Sub FindApps(ByRef foundApps As Collection, ByRef queuedOnly As Boolean)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundApps.Clear()
        Dim availableApps As Collection = New Collection
        If IO.Directory.Exists(Globals.RootApexDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootApexDir, "install.sql", availableApps, Globals.RootApexDir)

        End If

        If queuedOnly Then

            Dim conn As New OracleConnection(Globals.getARMconnection)
            Dim sql As String = Nothing
            Dim cmd As OracleCommand
            Dim dr As OracleDataReader

            'Dim patchMatch As Boolean = False

            'Get a list of queued apps from the target database.
            Try

                conn.Open()

                If RadioButtonMe.Checked Then
                    sql = "select schema, app_id from arm_app_queue_v where installed_on is null and queued_by_me = 'Y'"
                ElseIf RadioButtonOthers.Checked Then
                    sql = "select schema, app_id from arm_app_queue_v where installed_on is null and queued_by_me = 'N'"
                ElseIf RadioButtonAnyone.Checked Then
                    sql = "select schema, app_id from arm_app_queue_v where installed_on is null"
                End If



                cmd = New OracleCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                dr = cmd.ExecuteReader()

                While (dr.Read())
                    Dim l_app_id As String = dr.Item("app_id")
                    Dim l_schema As String = dr.Item("schema")
                    Dim l_app_found As Boolean = False

                    For i As Integer = 1 To availableApps.Count

                        If availableApps(i).ToString().Contains(l_schema & "\f" & l_app_id) Then
                            foundApps.Add(availableApps(i))
                            l_app_found = True
                        End If

                    Next

                    If Not l_app_found Then
                        MsgBox("WARNING: " & l_schema & " Apex App " & l_app_id & " is queued, but not present in this branch " & Globals.currentBranch & " of repo " & Globals.getRepoName)
                    End If

                End While

                dr.Close()

                conn.Close()
                conn.Dispose()


            Catch ex As Exception ' catches any error
                MessageBox.Show(ex.Message.ToString())
            Finally
                ' In a real application, put cleanup code here.

            End Try

        Else
            foundApps = availableApps

        End If

        'If foundApps.Count = 0 Then
        ' MsgBox("No Apex Apps matched the search criteria.", MsgBoxStyle.Information, "No Apps found")
        ' End If


        Cursor.Current = cursorRevert

    End Sub


    Private Function doSearch() As Integer
        Logger.Dbg("Searching")

        Dim AvailableApps As Collection = New Collection

        'If ComboBoxAppsFilter.SelectedItem = "Queued" Then
        '    FindApps(AvailableApps, True)
        '    AvailableAppsTreeView.populateTreeFromCollection(AvailableApps, True) 'check the queued apps, by default.
        '    '@TODO ElseIf ComboBoxAppsFilter.SelectedItem = "All" Then
        '    '    FindPatches(AvailableApps, False) 'Find apps without doing any db search.
        '    ' Dont check them.
        'Else
        '    MsgBox("Choose Queued or All.", MsgBoxStyle.Exclamation, "Choose Search criteria")
        'End If


        FindApps(AvailableApps, ComboBoxAppsFilter.SelectedItem = "Queued") 'check for queued apps only
        AvailableAppsTreeView.populateTreeFromCollection(AvailableApps, ComboBoxAppsFilter.SelectedItem = "Queued") 'check the queued apps, by default.


        'Search for modified apps to flag for reinstall.
        If Not String.IsNullOrEmpty(Me.tagA) And Not String.IsNullOrEmpty(Me.tagB) Then
            'Set the commits for the search
            GitOp.setCommitsFromTags(Me.tagA, Me.tagB)

            Dim ModifiedApps As Collection = New Collection()
            For Each change In GitOp.getChanges(Globals.getApexRelPath, False)

                'Search for change to any file, but should ignore duplicates.
                Dim appId As String = Common.getNthSegment(change, "/", 4)


                If Not ModifiedApps.Contains(appId) Then
                    ModifiedApps.Add(appId, appId)
                End If

            Next

            AvailableAppsTreeView.TickNodes(ModifiedApps)

            If ModifiedApps.Count > 0 Then
                MsgBox(ModifiedApps.Count & " apps have been modified.", MsgBoxStyle.Information, "Apex Apps Modifed")
            End If

        End If

        AvailableAppsTreeView.ExpandAll()

        'Logger.Dbg("Filtering")
        'filterQueuedBy(AvailableApps)

        Logger.Dbg("Populate Tree")

        If AvailableApps.Count = 0 Then
            MsgBox("No " & ComboBoxAppsFilter.SelectedItem & " apps were found.", MsgBoxStyle.Information, "No Apps Found")
        End If


        Return AvailableApps.Count


    End Function

    Private Sub SearchApexAppsButton_Click(sender As Object, e As EventArgs) Handles SearchApexAppsButton.Click
        doSearch()
    End Sub

    Public Shared Sub RunMasterScript(scriptData As String)

        Dim masterScriptName As String = Globals.RootApexDir & "temp_master_script.sql"

        FileIO.writeFile(masterScriptName, scriptData, True)

        Host.executeSQLscriptInteractive(masterScriptName, Globals.RootApexDir)

        FileIO.deleteFileIfExists(masterScriptName)

    End Sub


    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles InstallApexAppsButton.Click


        'Confirm run against non-VM target
        If Globals.getDB <> "VM" Then
            Dim result As Integer = MessageBox.Show("Confirm target is " & Globals.getDB &
      Chr(10) & "The Apps will be installed in " & Globals.getDB & ", overwriting existing versions.", "Confirm Target", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If


        'Format as script
        Dim masterList As String = Nothing

        For i As Integer = 0 To MasterScriptListBox.Items.Count - 1

            masterList = masterList & Chr(10) & MasterScriptListBox.Items(i).ToString()

        Next

        RunMasterScript(masterList)


    End Sub

    Private Sub CopySelectedApps()

        'Apps to run
        'Retrieve checked node items from the AvailableAppsTreeView as a collection of files.

        Dim ChosenApps As Collection = New Collection
        AvailableAppsTreeView.ReadCheckedLeafNodes(ChosenApps)

        If ChosenApps.Count = 0 Then
            MsgBox("No apex apps selected.")
        Else
            'Common.listCollection(ChosenApps, "Chosen Apps")


            'Get a list of modified apps
            Dim modifiedApps As Collection = OracleSQL.GetModifiedApps()

            MasterScriptListBox.Items.Clear()
            MasterScriptListBox.Items.Add("SET SERVEROUTPUT ON")
            MasterScriptListBox.Items.Add("WHENEVER OSERROR EXIT FAILURE ROLLBACK")
            MasterScriptListBox.Items.Add("WHENEVER SQLERROR EXIT FAILURE ROLLBACK")
            MasterScriptListBox.Items.Add("DEFINE database = '" & Globals.getDATASOURCE & "'")

            MasterScriptListBox.Items.Add("@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql")

            For Each App In ChosenApps

                Dim lSchema = Common.getFirstSegment(App, "\")
                Dim lAppId = Common.getLastSegment(App, "\")

                If modifiedApps.Contains(lAppId) Then
                    MsgBox("App " & lAppId & " has been modified since the last time it was installed.  " & Environment.NewLine &
                               "Please check.  You may be about to overwrite unexported changes.", MsgBoxStyle.Exclamation, "Modified App")
                End If

                MasterScriptListBox.Items.Add("CONNECT &&" & lSchema & "_user/&&" & lSchema & "_password@&&database")
                MasterScriptListBox.Items.Add("execute arm_installer.set_security_for_apex_import(-")
                MasterScriptListBox.Items.Add("     i_schema => '&&" & lSchema & "_USER'-")
                MasterScriptListBox.Items.Add("   , i_workspace => '&&" & lAppId & "_WORKSPACE');")
                MasterScriptListBox.Items.Add("cd " & App)
                MasterScriptListBox.Items.Add("@install.sql")

                If UsePatchAdminCheckBox.Checked Then
                    MasterScriptListBox.Items.Add("execute arm_installer.done_apex_app(i_app_id => " & lAppId.TrimStart("f") & ");")
                End If

                MasterScriptListBox.Items.Add("cd ..\..")

            Next

        End If



    End Sub


    Private Sub AppInstallerTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AppInstallerTabControl.SelectedIndexChanged


        'If (AppInstallerTabControl.SelectedTab.Name.ToString) = "OrderTabPage" Then
        '    If TreeViewPatchOrder.Nodes.Count = 0 Then
        '        CopySelectedChanges()
        '    End If
        'Else
        If (AppInstallerTabControl.SelectedTab.Name.ToString) = "RunTabPage" Then
            CopySelectedApps()
            'PopMasterScriptListBox()

            'ElseIf (AppInstallerTabControl.SelectedTab.Name.ToString) = "ExportTabPage" Then
            '   PopPatchListBox()

        End If

    End Sub

End Class