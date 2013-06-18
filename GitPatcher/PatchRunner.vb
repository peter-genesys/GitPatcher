Imports Oracle.DataAccess.Client ' VB.NET

Public Class PatchRunner



    Public Sub New()
        InitializeComponent()
        RadioButtonUnapplied.Checked = True
        PatchFilterGroupBox.Text = Globals.currentDB & " Filter"
    End Sub



    Shared Function get_last_split(ByVal ipath As String, ByVal idelim As String) As String
        Dim Path() As String = ipath.Split(idelim)
        Dim SplitCount = Path.Length
        Dim l_last As String = ipath.Split(idelim)(SplitCount - 1)

        Return l_last
    End Function

    Shared Function get_first_split(ByVal ipath As String, ByVal idelim As String) As String

        Dim l_first As String = ipath.Split(idelim)(0)

        Return l_first
    End Function


    Private Sub RecursiveSearch(ByVal strPath As String, ByVal strPattern As String, ByRef lstTarget As ListBox)

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)
        Dim clsFile As FileInfoEx = Nothing

        'Add the files
        For Each strFile As String In strFiles
            clsFile = New FileInfoEx(strFile)
            lstTarget.Items.Add(clsFile)
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearch(strFolder, strPattern, lstTarget)
        Next

    End Sub

    Public Sub RecursiveSearchContainingFolder(ByVal strPath As String, ByVal strPattern As String, ByRef lstTarget As ListBox, ByVal removePath As String)

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)

        'Add the files
        For Each strFile As String In strFiles
            lstTarget.Items.Add(strPath.Substring(removePath.Length))
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearchContainingFolder(strFolder, strPattern, lstTarget, removePath)
        Next

    End Sub


    Public Sub FindPatches(ByRef foundPatches As ListBox, ByVal iHideInstalled As Boolean)

        'Simple but replies on TNSNAMES File
        Dim oradb As String = "Data Source=" & Globals.currentDB & ";User Id=patch_admin;Password=patch_admin;"

        'Harder to get working but no need for TNSNAMES File
        'Dim oradb As String = "Data Source=(DESCRIPTION=" _
        '   + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" & Common.getHost & ")(PORT=" & Common.getPort & ")))" _
        '   + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" & Common.getSid & ")));" _
        '   + "User Id=patch_admin;Password=patch_admin;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader


        foundPatches.Items.Clear()
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text, "install.sql", foundPatches, Main.RootPatchDirTextBox.Text)

        End If

        If iHideInstalled Then

            'http://www.oracle.com/technetwork/articles/dotnet/cook-dotnet-101788.html

            'Now remove patches that have already been applied to the database.

            Dim patchMatch As Boolean = False


            Try

                conn.Open()

                'process in reverse order, because removing items from the list, changes the indexes.  reverse order will not be affected.
                For i As Integer = foundPatches.Items.Count - 1 To 0 Step -1

                    'Check whether the patch has been successfully installed.
                    patchMatch = False

                    'If iHideApplied Then
                    ' sql = "select max(patch_name) patch_name from patches_unapplied_v where patch_name = '" & Common.getLastSegment(AvailablePatchesListBox.Items(i), "\") & "'"
                    'If iHideInstalled Then
                    sql = "select max(patch_name) patch_name from patches where patch_name = '" & Common.getLastSegment(foundPatches.Items(i), "\") & "' and success_yn = 'Y'"

                    ' End If


                    cmd = New OracleCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    dr = cmd.ExecuteReader()
                    dr.Read()

                    If Not IsDBNull(dr.Item("patch_name")) Then
                        'patch matches the search
                        patchMatch = True
                    End If


                    'If (iHideInstalled And patchMatch) Or (iHideApplied And Not patchMatch) Then
                    If patchMatch Then
                        'patch is to be filtered from the list.
                        foundPatches.Items.RemoveAt(i)

                    End If


                Next


                conn.Close()   ' Visual Basic
                conn.Dispose() ' Visual Basic


            Catch ex As Exception ' catches any error
                MessageBox.Show(ex.Message.ToString())
            Finally
                ' In a real application, put cleanup code here.

            End Try

        End If




    End Sub


    Public Sub FindUnappliedPatches(ByRef foundPatches As ListBox)


        foundPatches.Items.Clear()
        Dim availableList As ListBox = New ListBox
        If IO.Directory.Exists(Main.RootPatchDirTextBox.Text) Then

            RecursiveSearchContainingFolder(Main.RootPatchDirTextBox.Text, "install.sql", availableList, Main.RootPatchDirTextBox.Text)

        End If

        'Simple but replies on TNSNAMES File
        Dim oradb As String = "Data Source=" & Globals.currentDB & ";User Id=patch_admin;Password=patch_admin;"

        Dim conn As New OracleConnection(oradb)
        Dim sql As String = Nothing
        Dim cmd As OracleCommand
        Dim dr As OracleDataReader

        Dim patchMatch As Boolean = False

        'This time loop through unapplied patches first and show in list if available in dir.
        Try

            conn.Open()

            sql = "select patch_name from patches_unapplied_v"

            cmd = New OracleCommand(sql, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()

            While (dr.Read())
                Dim l_patch_name As String = dr.Item("patch_name")
                Dim l_patch_found As Boolean = False

                For i As Integer = 0 To availableList.Items.Count - 1

                    If availableList.Items(i).ToString().Contains(l_patch_name) Then
                        foundPatches.Items.Add(availableList.Items(i))
                        l_patch_found = True
                    End If

                Next

                If Not l_patch_found Then
                    MsgBox("WARNING: Unapplied patch " & l_patch_name & " is not present in the local checkout.")
                End If

            End While

            conn.Close()   ' Visual Basic
            conn.Dispose() ' Visual Basic


        Catch ex As Exception ' catches any error
            MessageBox.Show(ex.Message.ToString())
        Finally
            ' In a real application, put cleanup code here.

        End Try




    End Sub


    Private Sub SearchPatchesButton_Click(sender As Object, e As EventArgs) Handles SearchPatchesButton.Click
        If RadioButtonUnapplied.Checked Then
            FindUnappliedPatches(AvailablePatchesListBox)
        Else
            FindPatches(AvailablePatchesListBox, RadioButtonUninstalled.Checked)
        End If

        'FindPatches(True, False)

    End Sub

    Private Sub AvailablePatchesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AvailablePatchesListBox.Click
        If Not ChosenPatchesListBox.Items.Contains(AvailablePatchesListBox.SelectedItem) Then
            ChosenPatchesListBox.Items.Add(AvailablePatchesListBox.SelectedItem)
        End If

    End Sub

    Private Sub ChosenPatchesListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChosenPatchesListBox.Click

        If ChosenPatchesListBox.Items.Count > 0 Then
            ChosenPatchesListBox.Items.RemoveAt(ChosenPatchesListBox.SelectedIndex)
        End If

    End Sub

    Public Shared Sub RunMasterScript(scriptData As String)

        Dim masterScriptName As String = Main.RootPatchDirTextBox.Text & "temp_master_script.sql"

        FileIO.writeFile(masterScriptName, scriptData, True)

        Host.executeSQLscriptInteractive(masterScriptName, Main.RootPatchDirTextBox.Text)

        FileIO.deleteFileIfExists(masterScriptName)

    End Sub


    Private Sub ExecutePatchButton_Click(sender As Object, e As EventArgs) Handles ExecutePatchButton.Click

        'Format as script
        Dim masterList As String = Nothing

        For i As Integer = 0 To MasterScriptListBox.Items.Count - 1

            masterList = masterList & Chr(10) & MasterScriptListBox.Items(i).ToString()

        Next

        RunMasterScript(masterList)


    End Sub


    Private Sub PopMasterScriptListBox()

        MasterScriptListBox.Items.Clear()

        MasterScriptListBox.Items.Add("DEFINE database = '" & Globals.currentDB & "'")

        For i As Integer = 0 To ChosenPatchesListBox.Items.Count - 1

            MasterScriptListBox.Items.Add("@" & ChosenPatchesListBox.Items(i).ToString() & "\install.sql")

        Next


    End Sub


    Private Sub PatchRunnerTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PatchRunnerTabControl.SelectedIndexChanged


        If (PatchRunnerTabControl.SelectedTab.Name.ToString) = "RunTabPage" Then
            PopMasterScriptListBox()

        End If

    End Sub

 
    Private Sub CopyAllPatches()
        'Copy Selected Changes to the next list box.
        ChosenPatchesListBox.Items.Clear()

        For i As Integer = 0 To AvailablePatchesListBox.Items.Count - 1

            ChosenPatchesListBox.Items.Add(AvailablePatchesListBox.Items(i).ToString)

        Next
    End Sub

    Private Sub ChooseAllButton_Click(sender As Object, e As EventArgs) Handles ChooseAllButton.Click
        CopyAllPatches()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        ChosenPatchesListBox.Items.Clear()
    End Sub
End Class