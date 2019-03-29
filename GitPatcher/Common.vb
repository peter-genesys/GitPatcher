Public Class Common

    Public Shared Function unix_path(ipath As String) As String
        Return Replace(ipath, "\", "/")
    End Function


    Public Shared Function unix_path_trailing_slash(ipath As String) As String
        Dim l_path As String
        'Use only /
        l_path = unix_path(ipath)

        'Remove leading /
        l_path = l_path.TrimStart("/")
 
        'Remove trailing /
        l_path = l_path.TrimEnd("/")
 
        'Add trailing /
        Return l_path & "/"
 
    End Function

  
    Public Shared Function dos_path(ipath As String) As String
        Return Replace(ipath, "/", "\")
    End Function
 
    Public Shared Function dos_path_trailing_slash(ipath As String) As String
        Dim l_path As String
        'Use only \
        l_path = dos_path(ipath)

        'Remove leading \
        l_path = l_path.TrimStart("\")

        'Remove trailing \
        l_path = l_path.TrimEnd("\")

        'Add trailing \
        Return l_path & "\"

    End Function



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
        Dim currentBranch As String = GitOp.CurrentBranch()

        If Not currentBranch.Contains(i_searchString) Then
            MsgBox("Current Branch: " & currentBranch & " is not of type " & i_searchString & Environment.NewLine & Environment.NewLine & "Please change branch manually NOW, or CANCEL this workflow.")

        End If

    End Sub


    Public Shared Sub listCollection(ByVal i_collection As Collection, iTitle As String)

        For Each lcollectionRow In i_collection
            MsgBox(lcollectionRow.ToString, MsgBoxStyle.Information, iTitle)
        Next

    End Sub

    Public Shared Sub MsgBoxCollection(ByVal i_collection As Collection, iTitle As String)

        Dim BigList As String = ""
        For Each lcollectionRow In i_collection
            BigList = BigList & lcollectionRow.ToString & Chr(10)
        Next
        MsgBox(BigList, MsgBoxStyle.Information, iTitle)
    End Sub

    Public Shared Function CollectionToText(ByVal i_collection As Collection) As String

        Dim Text As String = ""
        For Each lcollectionRow In i_collection
            Text = Text & lcollectionRow.ToString & Chr(10)
        Next
        Return Text

    End Function



    Shared Sub zip7_dir(ByVal i_zip_file As String,
                       ByVal i_zip_dir As String)

        FileIO.confirmDeleteFile(i_zip_file)
        FileIO.deleteFileIfExists(i_zip_file)

        Dim l_command_filename As String = "C:\PROGRA~1\7-Zip\7z.exe"
        Dim l_path As String = Nothing
        Dim l_arguments As String = " a " & i_zip_file & " " & i_zip_dir
        Logger.Dbg(l_arguments)
        Dim l_workingDir As String = Nothing

        Host.run_shell(l_command_filename, l_path, l_arguments, l_workingDir)

    End Sub

    Shared Sub unzip7_to_dir(ByVal i_zip_file As String,
                            ByVal i_zip_dir As String,
                            ByVal i_tag_dir As String)

        FileIO.confirmDeleteFolder(i_zip_dir)
        FileIO.deleteFolderIfExists(i_zip_dir)

        Dim l_command_filename As String = "C:\PROGRA~1\7-Zip\7z.exe"
        Dim l_path As String = Nothing
        Dim l_arguments As String = " x " & i_zip_file & " -o" & i_tag_dir
        Logger.Dbg(l_arguments)
        Dim l_workingDir As String = Nothing

        Host.run_shell(l_command_filename, l_path, l_arguments, l_workingDir)

    End Sub




End Class
