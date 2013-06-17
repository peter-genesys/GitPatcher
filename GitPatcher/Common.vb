Public Class Common

 

    Public Shared Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            Application.DoEvents()
        Loop
        sw.Stop()
    End Sub


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


    Public Shared Function stringContainsSetMember(iString As String, iSet As String, idelim As String) As Boolean
        Dim lResult As Boolean = False
        For Each member In iSet.Split(idelim)
            lResult = lResult Or iString.Contains(member)
        Next

        Return lResult

    End Function


    Public Shared Function getFirstSegment(ByVal ipath As String, ByVal idelim As String) As String

        Return ipath.Split(idelim)(0)
    End Function

    Public Shared Function getNthSegment(ByVal ipath As String, ByVal idelim As String, ByVal n As Integer) As String

        Return ipath.Split(idelim)(n - 1)
    End Function


    Public Shared Function getLastSegment(ByVal ipath As String, ByVal idelim As String) As String
        Dim Path() As String = ipath.Split(idelim)
        Dim SplitCount = Path.Length
        Dim l_last As String = ipath.Split(idelim)(SplitCount - 1)

        Return l_last
    End Function

    Public Shared Function dropFirstSegment(ByVal ipath As String, ByVal idelim As String) As String

        Dim l_from_first As String = Nothing
        Dim delim_pos As Integer = ipath.IndexOf(idelim)
        If delim_pos > 0 Then
            l_from_first = ipath.Remove(0, delim_pos + 1)
        End If

        Return l_from_first
    End Function

    Public Shared Function dropLastSegment(ByVal ipath As String, ByVal idelim As String) As String

        Dim l_to_last As String = Nothing
        Dim delim_pos As Integer = ipath.LastIndexOf(idelim)
        If delim_pos > 0 Then
            l_to_last = ipath.Remove(delim_pos, ipath.Length - delim_pos)
        End If

        Return l_to_last
    End Function

    Public Shared Function getHost() As String

        Return getNthSegment(Main.CurrentConnectionTextBox.Text, ":", 1)

    End Function

    Public Shared Function getPort() As String

        Return getNthSegment(Main.CurrentConnectionTextBox.Text, ":", 2)

    End Function

    Public Shared Function getSid() As String

        Return getNthSegment(Main.CurrentConnectionTextBox.Text, ":", 3)

    End Function

 
End Class
