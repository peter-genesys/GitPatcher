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



End Class
