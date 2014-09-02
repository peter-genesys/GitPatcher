Public Class FileIO

    Shared Sub Confirm(ByVal Message, ByVal Title)
        Dim button As MsgBoxResult
        button = MsgBox(Message, MsgBoxStyle.OkCancel, Title)
        If button = MsgBoxResult.Cancel Then
            Throw (New Halt("User Cancelled Operation"))
        End If

    End Sub


    'Shared Sub zip_dir(ByVal i_zip_file As String,
    '                   ByVal i_zip_dir As String)


    '    'Create empty ZIP file.
    '    'Dim objFSO = CreateObject("Scripting.FileSystemObject")
    '    'objFSO.CreateTextFile(i_zip_file, True)


    '    Dim l_empty_zip_file As New System.IO.StreamWriter(i_zip_file)
    '    l_empty_zip_file.Write("PK" & Chr(5) & Chr(6) & StrDup(18, vbNullChar))
    '    l_empty_zip_file.Close()


    '    'CreateObject("Scripting.FileSystemObject").CreateTextFile(i_zip_file, True).Write "PK" & Chr(5) & Chr(6) & String(18, vbNullChar)

    '    'Dim objShell = CreateObject("Shell.Application")
    '    Dim objShell As Object = CreateObject("Shell.Application")

    '    Dim source = objShell.NameSpace(i_zip_dir).Items

    '    objShell.NameSpace(i_zip_file).CopyHere(source)

    '    objShell.WaitForExit()

    '    'Required!
    '    'wScript.Sleep 2000

    'End Sub


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

    Shared Sub confirmDeleteFolder(ByVal i_path As String)
        Dim l_path As String = i_path.Trim("\")

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FolderExists(l_path) Then
            Confirm("Delete this folder: " & l_path, "Confirm Folder Delete")
            objFSO.DeleteFolder(l_path)
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


    'Not currently used, - but good generic routine, that may come in handy
    Private Sub ReplaceWithinFile(ByVal i_filename As String, ByVal i_label As String, ByVal i_value As String)
        Logger.Dbg("i_filename: " & i_filename)
        Logger.Dbg("i_label: " & i_label)
        Logger.Dbg("i_value: " & i_value)

        'Read the original file into a string
        Dim l_orig_file As New System.IO.StreamReader(i_filename)
        Dim l_orig_text As String = l_orig_file.ReadToEnd
        l_orig_file.Close()
        'Delete the original file
        Logger.Dbg("Delete orig file: " & i_filename)
        System.IO.File.Delete(i_filename)

        'Create the replacement text for the replacement file 
        Dim l_new_text As String = Replace(l_orig_text, i_label, i_value, 1, Len(l_orig_text), vbTextCompare) & vbLf

        'Replace the file
        Dim l_new_file As New System.IO.StreamWriter(i_filename)
        l_new_file.Write(l_new_text)
        l_new_file.Close()
    End Sub

    'Not currently used, - but good generic routine, that may come in handy
    Private Sub ReplaceWithinFilesRecursive(ByVal i_dir As String, ByVal i_label As String, ByVal i_value As String)

        Logger.Dbg("i_dir: " & i_dir)
        Logger.Dbg("i_label: " & i_label)
        Logger.Dbg("i_value: " & i_value)

        Dim objfso = CreateObject("Scripting.FileSystemObject")
        Dim objFolder As Object
        Dim objSubFolder As Object
        objFolder = objfso.GetFolder(i_dir)
        Dim objFile As Object

        If objFolder.Files.Count > 0 Then
            For Each objFile In objFolder.Files
                ReplaceWithinFile(objFile.path, i_label, i_value)
            Next
        End If

        If objFolder.SubFolders.Count > 0 Then
            For Each objSubFolder In objFolder.SubFolders
                ReplaceWithinFilesRecursive(objSubFolder.path, i_label, i_value)
            Next
        End If

    End Sub


End Class
