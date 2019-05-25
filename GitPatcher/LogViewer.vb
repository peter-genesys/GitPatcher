Public Class LogViewer
    Private writer As TextBoxWriter = Nothing

    Private Sub LogViewer_Load(ByVal sender As System.Object,
      ByVal e As System.EventArgs) Handles MyBase.Load
        writer = New TextBoxWriter(LogTextBox)
        Console.SetOut(writer)
    End Sub


    'Private Sub Button1_Click(ByVal sender As System.Object,
    '  ByVal e As System.EventArgs) Handles Button1.Click
    '    Console.WriteLine("This is some redirected text")
    'End Sub
End Class