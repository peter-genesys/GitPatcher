Public Class FileInfoEx

    Public FileInfo As System.IO.FileInfo = Nothing

    Public Sub New(ByVal Path As String)
        Me.FileInfo = New System.IO.FileInfo(Path)
    End Sub

    Public Overrides Function ToString() As String
        Return Me.FileInfo.Name
    End Function

End Class
