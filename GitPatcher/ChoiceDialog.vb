Imports System.Windows.Forms

Public Class ChoiceDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Shared Function Ask(ByVal i_question, ByVal i_CSVChoices, ByVal i_default, ByVal i_title)
        Dim l_choice As String = Nothing
        Dim l_default_found As Boolean = False
        Dim l_reordered As String = Nothing

        ' Set the Window Title
        ChoiceDialog.Text = i_title

        ' Display the Question
        ChoiceDialog.QuestionTextBox.Text = i_question

        ' Populate the listbox from the CSV string
        ChoiceDialog.ChoiceComboBox.Items.Clear()

        l_reordered = (Common.orderVersions(i_CSVChoices))

        If l_reordered <> "" Then
            For Each l_choice In l_reordered.Split(",")
                l_choice = Trim(l_choice)
                If (l_choice.Length > 0) Then
                    ChoiceDialog.ChoiceComboBox.Items.Add(l_choice)
                    If l_choice = i_default Then
                        l_default_found = True
                        ChoiceDialog.ChoiceComboBox.SelectedIndex = ChoiceDialog.ChoiceComboBox.Items.Count - 1
                    End If

                End If
            Next
        End If

        ' Set the listbox to the default answer if it's there
        If Not l_default_found Then
            If (ChoiceDialog.ChoiceComboBox.Items.Count > 0) Then
                ChoiceDialog.ChoiceComboBox.SelectedIndex = 0
            Else
                MsgBox("No items for choice box.")
            End If
        End If

        ' Show the dialog
        Dim result = ChoiceDialog.ShowDialog()

        If result = System.Windows.Forms.DialogResult.Cancel Then
            Throw (New Halt("User Cancelled Operation"))
        End If

        Ask = ChoiceDialog.ChoiceComboBox.SelectedItem
    End Function

End Class
