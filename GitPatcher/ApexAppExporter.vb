Imports Oracle.ManagedDataAccess.Client

Public Class ApexAppExporter

    Private waiting As Boolean
    Private Reimport As Boolean

    Public Sub New(Optional ByVal iReimport As Boolean = False)

        InitializeComponent()
        Me.Reimport = iReimport
        'DoSearch(RepoRadioButton.Checked) 'See repoRadioButton_CheckedChanged
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


    Public Shared Sub FindApps(ByRef foundApps As Collection) 


        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        foundApps.Clear()
        Dim availableApps As Collection = New Collection
        If IO.Directory.Exists(Globals.RootApexDir) Then

            FileIO.RecursiveSearchContainingFolder(Globals.RootApexDir, "install.sql", availableApps, Globals.RootApexDir)

        End If

        foundApps = availableApps
        Logger.Debug(foundApps.Count & " Apps found.")

        If foundApps.Count = 0 Then
            MsgBox("No Apex Apps found in Apex Dir " & Globals.RootApexDir, MsgBoxStyle.Information, "No Apps found")
        End If

        Cursor.Current = cursorRevert

    End Sub



    Private Sub DoSearch(iRestrict As Boolean)
        Logger.Debug("Searching")

        Dim AvailableApps As Collection = New Collection

        If iRestrict Then
            Logger.Debug("look for apps in checkout")
            FindApps(AvailableApps)             'look for apps in checkout - limited to this repo
        Else
            Logger.Debug("look for apps in workspaces")
            AvailableApps = OracleSQL.GetApps() 'look for apps in DB       - any apps from any repo
        End If

        Logger.Debug("Populate Tree")
        KnownAppsTreeView.populateTreeFromCollection(AvailableApps, False)

        'Get a list of modified apps
        Logger.Debug("look for modified apps in workspaces")
        Dim modifiedApps As Collection = OracleSQL.GetModifiedApps()

        'Tick the modified apps in the treeview
        KnownAppsTreeView.TickNodes(modifiedApps)

        KnownAppsTreeView.ExpandAll()




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

            For Each App In ChosenApps
                Dim lSchema = Common.getFirstSegment(App, "\")
                Dim lAppId = Common.getLastSegment(App, "\")

                WF_Apex.ApexSplitExportCommit(lSchema, lAppId, Me.Reimport)

            Next

        End If



    End Sub


    Private Sub ExportApexAppsButton_Click(sender As Object, e As EventArgs) Handles ExportApexAppsButton.Click
        ExportSelectedApps()
    End Sub

    Private Sub RepoRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles RepoRadioButton.CheckedChanged
        DoSearch(RepoRadioButton.Checked)
    End Sub
End Class