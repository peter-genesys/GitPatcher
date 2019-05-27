'https://www.codeguru.com/csharp/csharp/cs_data/streaming/article.php/c11777/Redirect-IO-to-a-TextBoxWriter-in-NET.htm

Imports System.Windows.Forms
Imports System.Text

Public Class TextBoxWriter
    Inherits System.IO.TextWriter

    Private control As TextBoxBase
    Private Builder As StringBuilder

    Public Sub New(ByVal control As TextBox)
        Me.control = control
        AddHandler control.HandleCreated,
           New EventHandler(AddressOf OnHandleCreated)
    End Sub

    Public Overrides Sub Write(ByVal ch As Char)
        Write(ch.ToString())
    End Sub

    Public Overrides Sub Write(ByVal s As String)
        If (control.IsHandleCreated) Then
            AppendText(s)
        Else
            BufferText(s)
        End If
    End Sub

    Public Overrides Sub WriteLine(ByVal s As String)
        Write(s + Environment.NewLine)
    End Sub

    Private Sub BufferText(ByVal s As String)
        If (Builder Is Nothing) Then
            Builder = New StringBuilder()
        End If
        Builder.Append(s)
    End Sub

    Private Sub AppendText(ByVal s As String)
        If (Builder Is Nothing = False) Then
            control.AppendText(Builder.ToString())
            Builder = Nothing
        End If

        control.AppendText(s)
    End Sub

    Private Sub OnHandleCreated(ByVal sender As Object,
       ByVal e As EventArgs)
        If (Builder Is Nothing = False) Then
            control.AppendText(Builder.ToString())
            Builder = Nothing
        End If
    End Sub

    Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
        Get
            Return Encoding.Default
        End Get
    End Property
End Class
