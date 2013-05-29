Public Class Common
    Public Shared Function sortable_tag_string(ByVal i_tag_string As String) As String
        'converts a tag string "5.5.03" to a sortable string "0005.0005.0003"    

        Dim l_num_componant As String
        Dim l_result As String = Nothing

        For Each l_num_componant In i_tag_string.Split(".")
            l_result = l_result & Right("0000" & l_num_componant, 4) & "."
        Next

        Return l_result.TrimEnd(".")

    End Function


    Public Shared Function orderVersions(ByVal i_versions As String) As String
        Dim l_nums As String = ""
        Dim l_chrs As String = ""
        Dim d As Decimal, v As String

        If (i_versions = "") Then
            Return i_versions
        End If

        For Each v In i_versions.Split(",")
            v = Trim(v)
            If (v.Length > 0) Then
                Try
                    'Test that the string is numeric (after multiple points are removed) 
                    d = System.Convert.ToDecimal(Replace(v, ".", ""))
                    'Add the string to the numeric strings
                    l_nums = l_nums & v & ","
                Catch ex As Exception
                    'Add the string to the alpha strings
                    l_chrs = l_chrs & v & ","
                End Try
            End If
        Next
        l_nums.TrimEnd(",")
        l_chrs.TrimEnd(",")

        ' Perform a simple bubble sort on the numbers
        Dim n() As String = Split(l_nums, ",")

        Dim tmp As String
        Dim a As String, b As String
        Dim i As Integer
        Dim sorted As Boolean = False
        If (UBound(n) > 1) Then
            ' only need to sort if there are elements to sort.
            Do While Not sorted
                sorted = True
                For i = 0 To n.Length - 1
                    If (n(i + 1) = "") Then
                        Exit For
                    End If

                    a = sortable_tag_string(n(i))
                    b = sortable_tag_string(n(i + 1))

                    If a < b Then
                        'swap the elements
                        tmp = n(i)
                        n(i) = n(i + 1)
                        n(i + 1) = tmp
                        sorted = False
                    End If
                Next
            Loop
            l_nums = Nothing
            For i = 0 To UBound(n)
                l_nums = l_nums & n(i) & ","
            Next
        End If

        ' Return the numbers, then the chars.
        Return l_nums.TrimEnd(",") & "," & l_chrs.TrimEnd(",")
    End Function
End Class
