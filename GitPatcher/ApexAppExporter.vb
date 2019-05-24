Imports Oracle.ManagedDataAccess.Client

Public Class ApexAppExporter

    Private waiting As Boolean

    Public Sub New()

        InitializeComponent()
        DoSearch()
        Me.MdiParent = GitPatcher
        Me.Show()
        Wait()

    End Sub

    Private Sub Wait()
        'Wait until the form is closed.
        waiting = True
        Do Until Not waiting
            Common.Wait()
        Loop
    End Sub

    Private Sub ApexAppExporter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        waiting = False
    End Sub


    Public Sub FindApps(ByRef foundApps As Collection)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundApps.Clear()
        Dim availableApps As Collection = New Collection
        If IO.Directory.Exists(Globals.RootApexDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootApexDir, "install.sql", availableApps, Globals.RootApexDir)

        End If

        foundApps = availableApps

        If foundApps.Count = 0 Then
            MsgBox("No Apex Apps found in Apex Dir " & Globals.RootApexDir, MsgBoxStyle.Information, "No Apps found")
        End If

        Cursor.Current = cursorRevert

    End Sub

    Private Sub DoSearch()
        Logger.Dbg("Searching")

        Dim AvailableApps As Collection = New Collection

        FindApps(AvailableApps) 'check for any apps
        KnownAppsTreeView.populateTreeFromCollection(AvailableApps, False)

        'Get a list of modified apps
        Dim modifiedApps As Collection = OracleSQL.GetModifiedApps()

        'Tick the modified apps in the treeview
        KnownAppsTreeView.TickNodes(modifiedApps)

        KnownAppsTreeView.ExpandAll()

        Logger.Dbg("Populate Tree")


    End Sub

    Private Sub ExportSelectedApps()

        'Confirm run against non-VM source
        If Globals.getDB <> "VM" Then
            Dim result As Integer = MessageBox.Show("Confirm source is " & Globals.getDB &
      Chr(10) & "The Apps will be exported from " & Globals.getDB & ".", "Confirm Source", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        'Apps to run
        'Retrieve checked node items from the AvailableAppsTreeView as a collection of files.

        Dim ChosenApps As Collection = New Collection
        KnownAppsTreeView.ReadCheckedLeafNodes(ChosenApps)

        If ChosenApps.Count = 0 Then
            MsgBox("No apex apps selected.")
        Else
            'Common.listCollection(ChosenApps, "Chosen Apps")

            'MasterScriptListBox.Items.Clear()
            'MasterScriptListBox.Items.Add("SET SERVEROUTPUT ON")
            'MasterScriptListBox.Items.Add("WHENEVER OSERROR EXIT FAILURE ROLLBACK")
            'MasterScriptListBox.Items.Add("WHENEVER SQLERROR EXIT FAILURE ROLLBACK")
            'MasterScriptListBox.Items.Add("DEFINE database = '" & Globals.getDATASOURCE & "'")

            For Each App In ChosenApps
                Dim lSchema = Common.getFirstSegment(App, "\")
                Dim lAppId = Common.getLastSegment(App, "\")

                WF_Apex.ApexSplitExportCommit(lSchema, lAppId)

                'MasterScriptListBox.Items.Add("CONNECT " & lSchema & "/&&" & lSchema & "_password@&&database")

                'MasterScriptListBox.Items.Add("cd " & App)
                'MasterScriptListBox.Items.Add("@install.sql")

                'MasterScriptListBox.Items.Add("cd ..\..")

            Next

        End If



    End Sub


    Private Sub ExportApexAppsButton_Click(sender As Object, e As EventArgs) Handles ExportApexAppsButton.Click
        ExportSelectedApps()
    End Sub
End Class