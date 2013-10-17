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


    Shared Sub createFolderIfNotExists(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If Not objFSO.FolderExists(i_path) Then
            objFSO.CreateFolder(i_path)
        End If


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


    Public Shared Function FileList(ByVal strPath As String, ByVal strPattern As String, ByVal removePath As String) As Collection

        Dim Filenames As Collection = New Collection
        Dim strFolders() As String = System.IO.Directory.GetDirectories(strPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(strPath, strPattern)

        'Add the files
        For Each strFile As String In strFiles
            Filenames.Add(strFile.Substring(removePath.Length))
        Next

        Return Filenames

    End Function


End Class
