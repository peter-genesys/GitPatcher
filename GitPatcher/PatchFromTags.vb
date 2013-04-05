Public Class PatchFromTags

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        'GitSharpFascade.getTagDiff(Main.RepoComboBox.SelectedItem, Tag1TextBox.Text, Tag2TextBox.Text)
        'GitSharpFascade.getTagDiff(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text)
        ChangesCheckedListBox.Items.Clear()
        For Each change In GitSharpFascade.getTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, ViewFilesCheckBox.Checked)
            ChangesCheckedListBox.Items.Add(change)
            ChangesCheckedListBox.SetItemChecked(ChangesCheckedListBox.Items.Count - 1, CheckAllCheckBox.Checked)
        Next

        PatchNameTextBox.Text = SchemaComboBox.SelectedItem.ToString & "_" & Main.CurrentBranchTextBox.Text & "_" & Tag1TextBox.Text & "_" & Tag2TextBox.Text

        PatchDirTextBox.Text = Main.RootPatchDirTextBox.Text & "\" & PatchNameTextBox.Text & "\"
 
    End Sub

    Private Sub PatchButton_Click(sender As Object, e As EventArgs) Handles PatchButton.Click
 
        'Create Patch Dir

        FileIO.createFolderIfNotExists(PatchDirTextBox.Text)

 
        MsgBox(GitSharpFascade.exportTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, ChangesCheckedListBox.CheckedItems, PatchDirTextBox.Text))


        'Loop thru items in the list removing checked items and checking those unchecked.

        'Loop thru checked items.
        'For i As Integer = 0 To ChangesCheckedListBox.CheckedItems.Count - 1
        '    Console.WriteLine(ChangesCheckedListBox.CheckedItems.Item(i).ToString())
        'Next




    End Sub

    Private Sub deriveSchemas()
        SchemaComboBox.Items.Clear()
        SchemaComboBox.Text = ""
        For Each schema In GitSharpFascade.getSchemaList(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database")
            SchemaComboBox.Items.Add(schema)
        Next

        If SchemaComboBox.Items.Count > 0 Then
            SchemaComboBox.SelectedIndex = 0
        End If
    End Sub
   
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckAllCheckBox.CheckedChanged
        'Loop thru items.
        For i As Integer = 0 To ChangesCheckedListBox.Items.Count - 1
            ChangesCheckedListBox.SetItemChecked(i, CheckAllCheckBox.Checked)

        Next
    End Sub

    Private Sub Tag2TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag2TextBox.TextChanged

        deriveSchemas()
 
    End Sub

    Private Sub Tag1TextBox_TextChanged(sender As Object, e As EventArgs) Handles Tag1TextBox.TextChanged
        deriveSchemas()
    End Sub

    Private Sub ViewButton_Click(sender As Object, e As EventArgs) Handles ViewButton.Click
        MsgBox(GitSharpFascade.viewTagChanges(My.Settings.CurrentRepo, Tag1TextBox.Text, Tag2TextBox.Text, "database/" & SchemaComboBox.Text, ChangesCheckedListBox.CheckedItems))
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveButton.Click
        Dim temp As Collection = New Collection
 

        For i As Integer = 0 To ChangesCheckedListBox.Items.Count - 1
            If Not ChangesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)
                temp.Add(ChangesCheckedListBox.Items(i).ToString)

            End If


        Next

        ChangesCheckedListBox.Items.Clear()

        For i As Integer = 1 To temp.Count
            If Not ChangesCheckedListBox.CheckedIndices.Contains(i) Then
                'MsgBox(ChangesCheckedListBox.Items(i).ToString)
                ' temp.Add(ChangesCheckedListBox.Items(i).ToString)

                ChangesCheckedListBox.Items.Add(temp(i), CheckAllCheckBox.Checked)

            End If


        Next



    End Sub

    Private Sub PatchNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchNameTextBox.TextChanged
        PatchDirTextBox.Text = Main.RepoComboBox.SelectedItem.ToString & "\patch\" & PatchNameTextBox.Text & "\"
    End Sub
End Class