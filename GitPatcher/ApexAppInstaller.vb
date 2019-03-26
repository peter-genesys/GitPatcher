Imports Oracle.ManagedDataAccess.Client

Public Class ApexAppInstaller


    Public Sub New(Optional ByVal ienqueuedStatus As String = "All", Optional ByVal iBranchType As String = "")
        InitializeComponent()

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

        'Logger.Note("iBranchType", iBranchType)
        'Select Case iBranchType
        '    Case "feature"
        '        RadioButtonFeature.Checked = True
        '    Case "hotfix"
        '        RadioButtonHotfix.Checked = True
        '    Case "patchset"
        '        RadioButtonPatchSet.Checked = True
        '    Case "all"
        '        RadioButtonAll2.Checked = True
        '    Case Else
        '        RadioButtonAll2.Checked = True
        'End Select

        UsePatchAdminCheckBox.Checked = Globals.getUseARM

        doSearch()


    End Sub


    Public Shared Sub FindQueuedApps(ByRef foundApps As Collection)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundApps.Clear()
        Dim availableApps As Collection = New Collection
        If IO.Directory.Exists(Globals.RootApexDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootApexDir, "install.sql", availableApps, Globals.RootApexDir)

        End If

        Dim oradb As String = "Data Source=" & Globals.getDATASOURCE & ";User Id=apexrm;Password=apexrm;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim patchMatch As Boolean = False

        'This time loop through unapplied patches first and show in list if available in dir.
        Try

            conn.Open()

            sql = "select schema, app_id from arm_app_queue_v where installed_on is null"
            'sql = "select schema, app_id from arm_app_queue_v where installed_on is not null and queued_by_me = 'Y'"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                Dim l_app_id As String = dr.Item("app_id")
                Dim l_schema As String = dr.Item("schema")
                Dim l_app_found As Boolean = False

                For i As Integer = 1 To availableApps.Count

                    If availableApps(i).ToString().Contains(l_schema & "/f" & l_app_id) Then
                        foundApps.Add(availableApps(i))
                        l_app_found = True
                    End If

                Next

                If Not l_app_found Then
                    MsgBox("WARNING: Apex App " & l_app_id & " is not present in the local checkout.")
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

        If foundApps.Count = 0 Then
            MsgBox("No Apex Apps matched the search criteria.", MsgBoxStyle.Information, "No Apps found")
        End If


        Cursor.Current = cursorRevert

    End Sub


    Private Sub doSearch()
        Logger.Dbg("Searching")

        Dim AvailableApps As Collection = New Collection

        If ComboBoxAppsFilter.SelectedItem = "Queued" Then
            FindQueuedApps(AvailableApps)
            'ElseIf ComboBoxAppsFilter.SelectedItem = "Uninstalled" Then
            '    FindPatches(AvailableApps, ComboBoxAppsFilter.SelectedItem = "Uninstalled")
            'ElseIf ComboBoxAppsFilter.SelectedItem = "All" Then
            '    FindPatches(AvailableApps, False) 'Find patches without doing any db search.
        Else
            MsgBox("Choose type of patch to search for.", MsgBoxStyle.Exclamation, "Choose Search criteria")
        End If

        'Logger.Dbg("Filtering")
        'filterQueuedBy(AvailableApps)

        Logger.Dbg("Populate Tree")

        AvailableAppsTreeView.populateTreeFromCollection(AvailableApps)


    End Sub

    Private Sub SearchApexAppsButton_Click(sender As Object, e As EventArgs) Handles SearchApexAppsButton.Click
        doSearch()
    End Sub
End Class