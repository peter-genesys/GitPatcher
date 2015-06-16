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

    Shared Function Ask(ByVal i_question As String, ByVal i_Choices As Collection, ByVal i_default As String, ByVal i_title As String, Optional ByVal i_reorder As Boolean = True, Optional ByVal i_hideCancelButton As Boolean = False)
        Dim l_choice As String = Nothing
        Dim l_default_found As Boolean = False
        Dim l_reordered As New Collection

        If i_hideCancelButton Then
            ChoiceDialog.Cancel_Button.Visible = False
        End If

        ' Set the Window Title
        ChoiceDialog.Text = i_title

        ' Display the Question
        ChoiceDialog.QuestionTextBox.Text = i_question

        ' Populate the listbox from the CSV string
        ChoiceDialog.ChoiceComboBox.Items.Clear()
        If i_reorder Then
            l_reordered = Common.orderVersions(i_Choices)
        Else
            l_reordered = i_Choices
        End If

        For Each line In l_reordered
            ChoiceDialog.ChoiceComboBox.Items.Add(line)
            If line = i_default Then
                l_default_found = True
                ChoiceDialog.ChoiceComboBox.SelectedIndex = ChoiceDialog.ChoiceComboBox.Items.Count - 1
            End If
        Next


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
