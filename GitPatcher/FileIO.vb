Public Class FileIO

    Shared Sub Confirm(ByVal Message, ByVal Title)
        Dim button As MsgBoxResult
        button = MsgBox(Message, MsgBoxStyle.OkCancel, Title)
        If button = MsgBoxResult.Cancel Then
            Throw (New Halt("User Cancelled Operation"))
        End If

    End Sub

    Shared Function fileExists(ByVal i_path) As Boolean

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the file exists
        Return objFSO.FileExists(i_path)


    End Function

    Shared Function folderExists(ByVal i_path) As Boolean

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the file exists
        Return objFSO.FolderExists(i_path)


    End Function


    Shared Sub createFolderIfNotExists(ByVal i_path As String, Optional ByVal i_sep As String = "\")
        'Iteratively creates a full patch starting with Drive:\
        Dim l_incremental_path As String = Nothing
        Dim l_path_folders() As String = i_path.Split(i_sep)
 
        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        For Each l_folder In l_path_folders

            l_incremental_path = l_incremental_path & l_folder & i_sep

            ' Check that the patch_dir folder exists
            If Not objFSO.FolderExists(l_incremental_path) Then
                objFSO.CreateFolder(l_incremental_path)
            End If
        Next

         

    End Sub

    Shared Sub deleteFolderIfExists(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FolderExists(i_path) Then
            objFSO.DeleteFolder(i_path)
        End If


    End Sub

    Shared Sub confirmDeleteFolder(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FolderExists(i_path) Then
            Confirm("Delete this folder: " & i_path, "Confirm Folder Delete")
            objFSO.DeleteFolder(i_path)
        End If


    End Sub

    Shared Sub deleteFileIfExists(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FileExists(i_path) Then
            objFSO.DeleteFile(i_path)
        End If


    End Sub

    Shared Sub confirmDeleteFile(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FileExists(i_path) Then
            Confirm("Delete this file: " & i_path, "Confirm File Delete")
        End If


    End Sub

    Shared Sub writeFile(ByVal filepath As String, ByVal file_string_data As String, Optional overwrite As Boolean = False)
        'Write the file
        If overwrite Then
            deleteFileIfExists(filepath)
        End If

        Dim l_file As New System.IO.StreamWriter(filepath)
        l_file.Write(file_string_data)
        l_file.Close()

    End Sub


    Public Shared Function readFile(ByVal filepath As String) As String

        Dim file_string_data As String

        Dim l_file As New System.IO.StreamReader(filepath)
        file_string_data = l_file.ReadToEnd()
        l_file.Close()

        Return file_string_data

    End Function

    Public Shared Function readFileLine1(ByVal filepath As String) As String

        Dim file_string_data As String

        Dim l_file As New System.IO.StreamReader(filepath)
        file_string_data = l_file.ReadLine()
        l_file.Close()

        Return file_string_data

    End Function

 

    Public Shared Sub RecursiveSearchContainingFolder(ByVal strPath As String, ByVal strPattern As String, ByRef lstTarget As Collection, ByVal removePath As String)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)

        'Add the files
        For Each strFile As String In strFiles
            lstTarget.Add(strPath.Substring(removePath.Length))
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearchContainingFolder(strFolder, strPattern, lstTarget, removePath)
        Next

        Cursor.Current = cursorRevert

    End Sub


    Public Shared Function FileList(ByVal strPath As String, ByVal strPattern As String, ByVal removePath As String, Optional ByVal popKey As Boolean = False) As Collection

        Dim Filenames As Collection = New Collection
        Try
            Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)

            'Add the files
            For Each strFile As String In strFiles
                If popKey Then
                    Filenames.Add(strFile.Substring(removePath.Length), strFile.Substring(removePath.Length).ToString)
                Else
                    Filenames.Add(strFile.Substring(removePath.Length))
                End If

                Logger.Note("Added File", strFile.Substring(removePath.Length))
            Next


        Catch e As System.IO.DirectoryNotFoundException
            MsgBox("Path does not exist: " & strPath, MsgBoxStyle.Critical, "Dir does not exist")

        End Try


        Return Filenames

    End Function

    Public Shared Sub AppendFileList(ByVal strPath As String, ByVal strPattern As String, ByVal removePath As String, ByRef Filenames As Collection, Optional ByVal popKey As Boolean = False)

        Try
            Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)

            'Add the files
            For Each strFile As String In strFiles
                If popKey Then
                    Filenames.Add(strFile.Substring(removePath.Length), strFile.Substring(removePath.Length).ToString)
                Else
                    Filenames.Add(strFile.Substring(removePath.Length))
                End If

                Logger.Note("Added File", strFile.Substring(removePath.Length))
            Next
        Catch e As System.IO.DirectoryNotFoundException
            MsgBox("Path does not exist: " & strPath, MsgBoxStyle.Critical, "Dir does not exist")

        End Try

    End Sub




    Shared Function convertDOStoUNIX(ByVal DOSstring As String) As String
        convertDOStoUNIX = Replace(DOSstring, vbCrLf, vbLf, 1, Len(DOSstring), vbTextCompare)
    End Function


    Shared Function convertUNIXtoDOS(ByVal UNIXstring As String) As String
        convertUNIXtoDOS = Replace(UNIXstring, vbLf, vbCrLf, 1, Len(UNIXstring), vbTextCompare)
    End Function

    Public Shared Sub FileDOStoUNIX(ByVal i_filename As String)
        'Read the dos file into a string
        Dim l_dos_file As New System.IO.StreamReader(i_filename)
        Dim l_dos_text As String = l_dos_file.ReadToEnd
        l_dos_file.Close()
        'Delete the dos file
        Logger.Dbg("Delete dos file: " & i_filename)
        System.IO.File.Delete(i_filename)

        'Convert to unix and add an extra EOL
        Dim l_unix_text As String = convertDOStoUNIX(l_dos_text) & vbLf
 
        Dim l_unix_file As New System.IO.StreamWriter(i_filename)
        l_unix_file.Write(l_unix_text)
        l_unix_file.Close()
 
    End Sub



End Class
