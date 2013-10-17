Public Class Common


    Public Shared Function cleanString(iString) As String
        Return Trim(iString).Replace(Chr(13), "")

    End Function



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


    Public Shared Function orderVersions(ByVal i_versions As Collection) As Collection
        Dim l_nums As New Collection
        Dim l_ordered As New Collection
        Dim l_chrs As New Collection
        Dim d As Decimal, v As String

        If (i_versions.Count = 0) Then
            Return i_versions
        End If

        For Each v In i_versions
            v = Trim(v)
            If (v.Length > 0) Then
                Try
                    'Test that the string is numeric (after multiple points are removed) 
                    d = System.Convert.ToDecimal(Replace(v, ".", ""))
                    'Add the string to the numeric strings
                    l_nums.Add(v)
                Catch ex As Exception
                    'Add the string to the alpha strings
                    l_chrs.Add(v)
                End Try
            End If
        Next

        Dim l_dictionary = (From entry In l_nums
              Order By entry.Value Descending).ToDictionary(
              Function(pair) pair.Key,
              Function(pair) pair.Value)

        For Each entry In l_dictionary
            l_ordered.Add(entry)
        Next

 
        For Each entry In l_chrs
            l_ordered.Add(entry)
        Next
 
        Return l_ordered
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

        Return getNthSegment(Globals.currentConnection, ":", 1)

    End Function

    Public Shared Function getPort() As String

        Return getNthSegment(Globals.currentConnection, ":", 2)

    End Function

    Public Shared Function getSid() As String

        Return getNthSegment(Globals.currentConnection, ":", 3)

    End Function

 
    Public Shared Sub checkBranch(i_searchString)
        Dim currentBranch As String = GitSharpFascade.currentBranch(Globals.currentRepo)

        If Not currentBranch.Contains(i_searchString) Then
            MsgBox("Current Branch: " & currentBranch & " is not of type " & i_searchString & Environment.NewLine & Environment.NewLine & "Please change branch manually NOW, or CANCEL this workflow.")

        End If

    End Sub

End Class
