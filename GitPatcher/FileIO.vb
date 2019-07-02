Public Class FileIO

 

    Shared Sub Confirm(ByVal Message, ByVal Title)
        Dim button As MsgBoxResult
        button = MsgBox(Message, MsgBoxStyle.OkCancel, Title)
        If button = MsgBoxResult.Cancel Then
            Throw New System.Exception("User Cancelled Operation")
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

        Logger.Debug("fileExists(" & i_path & ")")

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the file exists
        Return objFSO.FileExists(Common.dos_path(i_path))


    End Function

    Shared Function folderExists(ByVal i_path) As Boolean
        Logger.Debug("folderExists(" & i_path & ")")

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the file exists
        Return objFSO.FolderExists(Common.dos_path(i_path))


    End Function


    Shared Sub createFolderIfNotExists(ByVal i_path As String, Optional ByVal i_sep As String = "\")
        'Iteratively creates a full patch starting with Drive:\
        Dim l_incremental_path As String = Nothing
        Dim l_path_folders() As String = Common.dos_path(i_path).Split(i_sep)
 
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
        While objFSO.FolderExists(Common.dos_path(i_path))
            Try
                objFSO.DeleteFolder(Common.dos_path(i_path))
            Catch ex As Exception
                MsgBox(ex.Message)
                Confirm("Folder Delete Failed: " & i_path & Environment.NewLine & Environment.NewLine &
                        "To retry, please close programs that may be using this folder. Eg" & Environment.NewLine &
                        "+ Text editors: Sublime, Notepad++, WinMerge" & Environment.NewLine &
                        "+ Executables : SQL, Powershell" & Environment.NewLine &
                        "then press Ok retry." & Environment.NewLine & Environment.NewLine &
                        "To abort process, press Cancel", "Retry Folder Delete")
            End Try

        End While


    End Sub

    Shared Sub confirmDeleteFolder(ByVal i_path As String)
        Dim l_path As String = Common.dos_path(i_path).Trim("\")

        ' Check that the patch_dir folder exists
        If folderExists(l_path) Then
            Confirm("Delete this folder: " & l_path, "Confirm Folder Delete")
            deleteFolderIfExists(l_path)
        End If


    End Sub

    Shared Sub deleteFileIfExists(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FileExists(Common.dos_path(i_path)) Then
            objFSO.DeleteFile(Common.dos_path(i_path))
        End If


    End Sub

    Shared Sub confirmDeleteFile(ByVal i_path)

        ' Create the File System Object
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        ' Check that the patch_dir folder exists
        If objFSO.FileExists(Common.dos_path(i_path)) Then
            Confirm("Delete this file: " & Common.dos_path(i_path), "Confirm File Delete")
            objFSO.DeleteFile(Common.dos_path(i_path))
        End If


    End Sub

    Shared Sub writeFile(ByVal i_path As String, ByVal file_string_data As String, Optional overwrite As Boolean = False)
        'Write the file
        If overwrite Then
            deleteFileIfExists(Common.dos_path(i_path))
        End If

        Dim l_file As New System.IO.StreamWriter(Common.dos_path(i_path))
        l_file.Write(file_string_data)
        l_file.Close()

    End Sub


    Public Shared Function readFile(ByVal i_path As String) As String

        Dim file_string_data As String

        Dim l_file As New System.IO.StreamReader(Common.dos_path(i_path))
        file_string_data = l_file.ReadToEnd()
        l_file.Close()

        Return file_string_data

    End Function

    Public Shared Function readFileLine1(ByVal i_path As String) As String

        Dim file_string_data As String

        Dim l_file As New System.IO.StreamReader(Common.dos_path(i_path))
        file_string_data = l_file.ReadLine()
        l_file.Close()

        Return file_string_data

    End Function

 

    Public Shared Sub RecursiveSearchContainingFolder(ByVal strPath As String, ByVal strPattern As String, ByRef lstTarget As Collection, ByVal removePath As String)


        Dim lstrPath As String = Common.dos_path(strPath)
        Dim lremovePath As String = Common.dos_path(removePath)
        Dim lstrPattern As String = Common.dos_path(strPattern)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        FileIO.createFolderIfNotExists(lstrPath)
        Dim strFolders() As String = System.IO.Directory.GetDirectories(lstrPath)
        Dim strFiles() As String = System.IO.Directory.GetFiles(lstrPath, lstrPattern)

        'Add the files
        For Each strFile As String In strFiles
            lstTarget.Add(lstrPath.Substring(lremovePath.Length))
        Next

        'Look through the other folders
        For Each strFolder As String In strFolders
            'Call the procedure again to perform the same operation
            RecursiveSearchContainingFolder(strFolder, lstrPattern, lstTarget, lremovePath)
        Next

        Cursor.Current = cursorRevert

    End Sub


    Public Shared Function FileList(ByVal strPath As String, ByVal strPattern As String, ByVal removePath As String, Optional ByVal popKey As Boolean = False) As Collection

        Dim lstrPath As String = Common.dos_path(strPath)
        Dim lremovePath As String = Common.dos_path(removePath)
        Dim lstrPattern As String = Common.dos_path(strPattern)

        FileIO.createFolderIfNotExists(lstrPath)

        Dim Filenames As Collection = New Collection
        Try
            Dim strFiles() As String = System.IO.Directory.GetFiles(lstrPath, lstrPattern)

            'Add the files
            For Each strFile As String In strFiles
                If popKey Then
                    Filenames.Add(strFile.Substring(lremovePath.Length), strFile.Substring(lremovePath.Length).ToString)
                Else
                    Filenames.Add(strFile.Substring(lremovePath.Length))
                End If

                Logger.Note("Added File", strFile.Substring(lremovePath.Length))
            Next


        Catch e As System.IO.DirectoryNotFoundException
            MsgBox("Path does not exist: " & lstrPath, MsgBoxStyle.Critical, "Dir does not exist")

        End Try


        Return Filenames

    End Function

    Public Shared Sub AppendFileList(ByVal strPath As String, ByVal strPattern As String, ByVal removePath As String, ByRef Filenames As Collection, Optional ByVal popKey As Boolean = False)

        Dim lstrPath As String = Common.dos_path(strPath)
        Dim lremovePath As String = Common.dos_path(removePath)
        Dim lstrPattern As String = Common.dos_path(strPattern)

        FileIO.createFolderIfNotExists(lstrPath)

        Try
            Dim strFiles() As String = System.IO.Directory.GetFiles(lstrPath, lstrPattern)

            'Add the files
            For Each strFile As String In strFiles
                If popKey Then
                    Filenames.Add(strFile.Substring(lremovePath.Length), strFile.Substring(lremovePath.Length).ToString)
                Else
                    Filenames.Add(strFile.Substring(lremovePath.Length))
                End If

                Logger.Note("Added File", strFile.Substring(lremovePath.Length))
            Next
        Catch e As System.IO.DirectoryNotFoundException
            MsgBox("Path does not exist: " & lstrPath, MsgBoxStyle.Critical, "Dir does not exist")

        End Try

    End Sub

    Public Shared Function FolderList(ByVal strPath As String, ByVal strPattern As String, ByVal removePath As String, Optional ByVal popKey As Boolean = False) As Collection


        Logger.Debug("FileIO.FolderList")
        Logger.Note("strPath", strPath)
        Logger.Note("strPattern", strPattern)
        Logger.Note("removePath", removePath)
        Logger.Note("popKey", popKey.ToString)

        Dim lstrPath As String = Common.dos_path(strPath)
        Dim lremovePath As String = Common.dos_path(removePath)
        Dim lstrPattern As String = Common.dos_path(strPattern)

        'FileIO.createFolderIfNotExists(lstrPath)

        Dim Foldernames As Collection = New Collection
        Try

            Dim strFolders() As String = System.IO.Directory.GetDirectories(lstrPath, lstrPattern)

            'Add the files
            For Each strFolder As String In strFolders
                Dim folder As String = Nothing
                If String.IsNullOrEmpty(lremovePath) Then
                    folder = strFolder
                Else
                    folder = strFolder.Substring(lremovePath.Length)
                End If

                If popKey Then
                    Foldernames.Add(folder, folder)
                Else
                    Foldernames.Add(folder)
                End If

                Logger.Note("Added File", folder)
            Next


        Catch e As System.IO.DirectoryNotFoundException
            MsgBox("Path does not exist: " & lstrPath, MsgBoxStyle.Critical, "Dir does not exist")

        End Try

        Logger.Note("Foldernames.count", Foldernames.Count)

        Return Foldernames

    End Function




    Shared Function convertDOStoUNIX(ByVal DOSstring As String) As String
        convertDOStoUNIX = Replace(DOSstring, vbCrLf, vbLf, 1, Len(DOSstring), vbTextCompare)
    End Function


    Shared Function convertUNIXtoDOS(ByVal UNIXstring As String) As String
        convertUNIXtoDOS = Replace(UNIXstring, vbLf, vbCrLf, 1, Len(UNIXstring), vbTextCompare)
    End Function

    Public Shared Sub FileDOStoUNIX(ByVal i_filename As String)

        Dim lfilename As String = Common.dos_path(i_filename)

        'Read the dos file into a string
        Dim l_dos_file As New System.IO.StreamReader(lfilename)
        Dim l_dos_text As String = l_dos_file.ReadToEnd
        l_dos_file.Close()
        'Delete the dos file
        Logger.Debug("Delete dos file: " & lfilename)
        System.IO.File.Delete(lfilename)

        'Convert to unix and add an extra EOL
        Dim l_unix_text As String = convertDOStoUNIX(l_dos_text) & vbLf

        Dim l_unix_file As New System.IO.StreamWriter(lfilename)
        l_unix_file.Write(l_unix_text)
        l_unix_file.Close()

    End Sub


    'Not currently used, - but good generic routine, that may come in handy
    'Now used by hotfix rebase.
    Public Shared Sub ReplaceWithinFile(ByVal i_filename As String, ByVal i_label As String, ByVal i_value As String)
        Logger.Debug("i_filename: " & i_filename)
        Logger.Debug("i_label: " & i_label)
        Logger.Debug("i_value: " & i_value)

        Dim lfilename As String = Common.dos_path(i_filename)

        'Read the original file into a string
        Dim l_orig_file As New System.IO.StreamReader(lfilename)
        Dim l_orig_text As String = l_orig_file.ReadToEnd
        l_orig_file.Close()
        'Delete the original file
        Logger.Debug("Delete orig file: " & lfilename)
        System.IO.File.Delete(lfilename)

        'Create the replacement text for the replacement file 
        Dim l_new_text As String = Replace(l_orig_text, i_label, i_value, 1, Len(l_orig_text), vbTextCompare) & vbLf

        'Replace the file
        Dim l_new_file As New System.IO.StreamWriter(lfilename)
        l_new_file.Write(l_new_text)
        l_new_file.Close()
    End Sub

    'Not currently used, - but good generic routine, that may come in handy
    Public Sub ReplaceWithinFilesRecursive(ByVal i_dir As String, ByVal i_label As String, ByVal i_value As String)

        Logger.Debug("i_dir: " & i_dir)
        Logger.Debug("i_label: " & i_label)
        Logger.Debug("i_value: " & i_value)

        Dim objfso = CreateObject("Scripting.FileSystemObject")
        Dim objFolder As Object
        Dim objSubFolder As Object
        objFolder = objfso.GetFolder(Common.dos_path(i_dir))
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

    Public Shared Sub CopyDir(ifrompath As String, itopath As String)
        Logger.Debug("CopyDir frompath " & Common.dos_path(ifrompath) & " topath " & Common.dos_path(itopath))
        My.Computer.FileSystem.CopyDirectory(Common.dos_path(ifrompath), Common.dos_path(itopath), True)
    End Sub

    Public Shared Sub CopyFile(ifrompath As String, itopath As String)
        Logger.Debug("CopyFile frompath " & Common.dos_path(ifrompath) & " topath " & Common.dos_path(itopath))
        My.Computer.FileSystem.CopyFile(Common.dos_path(ifrompath), Common.dos_path(itopath), True)
    End Sub

    Public Shared Sub CopyFileToDir(ifrompath As String, itodir As String)
        Logger.Debug("CopyFileToDir frompath " & Common.dos_path(ifrompath) & " todir " & itodir)
        Dim Filename As String = Common.getLastSegment(Common.dos_path(ifrompath), "\")

        CopyFile(ifrompath, itodir & "\" & Filename)

    End Sub

    Public Shared Sub RenameFile(ifrompath As String, itoname As String)
        My.Computer.FileSystem.RenameFile(Common.dos_path(ifrompath), itoname)
    End Sub

    Public Shared Function getTextBetween(ByVal iFilename As String, ByVal iString1 As String, ByVal iString2 As String)
        Dim fileText As String = readFile(iFilename)
        Dim startPos As Integer = fileText.IndexOf(iString1) + iString1.Length
        Dim endPos As Integer = fileText.IndexOf(iString2, startPos)

        If startPos > 0 And endPos > startPos And endPos - startPos < 100 Then
            Return fileText.Substring(startPos, endPos - startPos)
        Else
            Return "Unable to find Application Name!!"
        End If

    End Function

    Public Shared Function FindRepoPatches() As Collection

        Dim repoPatches As Collection = New Collection

        If IO.Directory.Exists(Globals.RootPatchDir) Then
            FileIO.RecursiveSearchContainingFolder(Globals.RootPatchDir, "install.sql", repoPatches, Globals.RootPatchDir)
        End If

        Return repoPatches

    End Function

End Class
