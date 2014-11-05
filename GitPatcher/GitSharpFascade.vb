﻿Imports GitSharp
Imports GitSharp.Commands

Class Halt
    Inherits Exception
    Public Sub New(ByVal str As String)
        Console.WriteLine(str) 'User defined exception
    End Sub 'New
End Class 'Halt


Public Class GitSharpFascade
    Dim halt As Exception

    Dim repo As GitSharp.Repository

    Public Sub New(ByVal path)
        'Dim dir As System.IO.DirectoryInfo
        'dir = New IO.DirectoryInfo(path)
        repo = New Repository(path)

    End Sub


    Shared Function getTagList(ByVal path As String, ByVal currentTags As Collection, Optional ByVal filter As String = Nothing) As Collection

        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)

        Dim tagnames As Collection = currentTags

        For Each Tag In repo.Tags
            If String.IsNullOrEmpty(filter) Then
                tagnames.Add(Tag.Key)
            ElseIf Tag.Key.Contains(filter) Then
                tagnames.Add(Tag.Key)
            End If

        Next

        Return tagnames

    End Function

    Shared Function getTagList(ByVal path As String, Optional ByVal filter As String = Nothing) As Collection

        Dim tagnames As Collection = New Collection

        Return getTagList(path, tagnames, filter)

    End Function


    Shared Function isRepo(ByVal path) As Boolean
        ' Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Return True
    End Function

    Shared Function currentBranch(ByVal path) As String
        Try
            Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
            Return repo.CurrentBranch.Name
        Catch
            MsgBox("Oops")
            Return "Oops"
        End Try


    End Function

    Shared Sub switchBranch(ByVal path, ByVal branchName)
        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Dim existingBranch As GitSharp.Branch = New Branch(repo, branchName)
        repo.SwitchToBranch(existingBranch)

    End Sub


    Shared Sub createBranch(ByVal path, ByVal branchName)
        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Dim newBranch As GitSharp.Branch = GitSharp.Branch.Create(repo, branchName)

        repo.SwitchToBranch(newBranch)

    End Sub


    'Shared Sub createTag(ByVal path, ByVal tagName)
    '    Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
    '    Dim newTag As GitSharp.Tag = GitSharp.Tag.
    '
    '
    'End Sub


    Shared Sub getIndexedChanges(ByVal path As String)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)


        For Each entry In repo.Index.Entries
            MsgBox(entry)
            Dim x As GitSharp.PathStatus = New GitSharp.PathStatus(repo, entry)
            If x.WorkingPathStatus <> 0 Or x.IndexPathStatus <> 0 Then

                MsgBox(entry & " WorkingPathStatus " & x.WorkingPathStatus)
                MsgBox(entry & " IndexPathStatus " & x.IndexPathStatus)
            End If

        Next

        Cursor.Current = cursorRevert

    End Sub

    Shared Sub revertItems(ByVal repoPath As String, ByVal itemPaths() As String)

        Dim repo As GitSharp.Repository = New GitSharp.Repository(repoPath)

        repo.Index.Unstage(itemPaths)

    End Sub

    Shared Sub revertItem(ByVal repoPath As String, ByVal itemPath As String)

        Dim itemPaths() As String = {itemPath}

        revertItems(repoPath, itemPaths)
 
    End Sub

    Shared Function getSchemaList(ByVal path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String) As Collection

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)

        Dim result As String = Nothing

        Dim t1 As Tag = repo.[Get](Of Tag)(tag1_name)
        Dim t2 As Tag = repo.[Get](Of Tag)(tag2_name)

        Dim schemas As Collection = New Collection
        Dim schema As String

        Try
            If Not t1.IsTag Then
                Throw (New Halt("Tag 1 (" & tag1_name & ") does not exist."))
            End If

            If Not t2.IsTag Then
                Throw (New Halt("Tag 2 (" & tag2_name & ") does not exist."))
            End If

            Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
            Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target
 
            For Each change As Change In c1.CompareAgainst(c2)

                If InStr(change.Path, pathmask_UNIX) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    schema = change.Path.Substring(Len(pathmask_UNIX)).Split("/")(0)

                    If Not schemas.Contains(schema) Then
                        schemas.Add(schema, schema)
                        Console.WriteLine(schema)
                    End If

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Cursor.Current = cursorRevert

        Return schemas

    End Function



    Shared Function getTagChanges(ByVal path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByVal viewFiles As Boolean) As Collection

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor


        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)

        Dim result As String = Nothing

        Dim t1 As Tag = repo.[Get](Of Tag)(tag1_name)
        Dim t2 As Tag = repo.[Get](Of Tag)(tag2_name)

        Dim changes As Collection = New Collection

        Try
            If Not t1.IsTag Then
                Throw (New Halt("Tag 1 (" & tag1_name & ") does not exist."))
            End If

            If Not t2.IsTag Then
                Throw (New Halt("Tag 2 (" & tag2_name & ") does not exist."))
            End If

            Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
            Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target


            For Each change As Change In c1.CompareAgainst(c2)
                If InStr(change.Path, pathmask_UNIX) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    Console.WriteLine(change.ChangeType & ": " & change.Path)
                    result = result & Chr(10) & change.ChangeType & ": " & change.Path
                    changes.Add(change.Path)


                    If viewFiles Then
                        Dim file_string_data As String = New Blob(repo, change.ChangedObject.Hash).Data
                        MsgBox(file_string_data)

                    End If

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Cursor.Current = cursorRevert

        Return changes

    End Function


    Shared Function viewTagChanges(ByVal repo_path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByRef targetFiles As Collection) As String

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim repo As GitSharp.Repository = New GitSharp.Repository(repo_path)

        Dim result As String = Nothing

        Dim t1 As Tag = repo.[Get](Of Tag)(tag1_name)
        Dim t2 As Tag = repo.[Get](Of Tag)(tag2_name)

        Dim changes As Collection = New Collection

        Try
            If Not t1.IsTag Then
                Throw (New Halt("Tag 1 (" & tag1_name & ") does not exist."))
            End If

            If Not t2.IsTag Then
                Throw (New Halt("Tag 2 (" & tag2_name & ") does not exist."))
            End If

            Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
            Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target


            For Each change As Change In c1.CompareAgainst(c2)
                If InStr(change.Path, pathmask_UNIX) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    For Each file In targetFiles
                        If change.Path = file.ToString Then
                            Dim file_string_data As String = New Blob(repo, change.ChangedObject.Hash).Data
                            MsgBox(file_string_data)

                            Clipboard.Clear()
                            Clipboard.SetText(file_string_data)

                            result = result & Chr(10) & change.Path

                        End If
                    Next

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Cursor.Current = cursorRevert

        Return result


    End Function


    Shared Function exportTagChanges(ByVal repo_path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByRef targetFiles As Collection, patchDir As String) As Collection

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        'Any files in the targetFiles that are not found are simply not exported.
        'This is currently being exploited as the filelist will be sent from the total patchable files treeview, even though it could contain files from the extras list, that are not to be exported from the repo.
        Dim repo As GitSharp.Repository = New GitSharp.Repository(repo_path)

        Dim result As String = Nothing

        Dim t1 As Tag = repo.[Get](Of Tag)(tag1_name)
        Dim t2 As Tag = repo.[Get](Of Tag)(tag2_name)

        Dim filePathList As Collection = New Collection

        Try
            If Not t1.IsTag Then
                Throw (New Halt("Tag 1 (" & tag1_name & ") does not exist."))
            End If

            If Not t2.IsTag Then
                Throw (New Halt("Tag 2 (" & tag2_name & ") does not exist."))
            End If

            Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
            Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target


            For Each change As Change In c1.CompareAgainst(c2)
                If InStr(change.Path, pathmask_UNIX) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    For Each file In targetFiles
                        If change.Path = file.ToString Then
                            Dim file_string_data As String = New Blob(repo, change.ChangedObject.Hash).Data
                            'MsgBox(file_string_data)

                            FileIO.writeFile(patchDir & "\" & change.Name, file_string_data)

                            result = result & Chr(10) & change.Path

                            filePathList.Add(change.Path)

                        End If
                    Next

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Cursor.Current = cursorRevert

        Return filePathList


    End Function



    Shared Function TagLog(ByVal repo_path As String, ByVal tag1_name As String, ByVal tag2_name As String, Optional ByVal headeronly As Boolean = True) As Collection

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        'Returns a log list from Tag 1 to Tag 2
        Dim repo As GitSharp.Repository = New GitSharp.Repository(repo_path)

        Dim result As String = Nothing

        Dim t1 As Tag = repo.[Get](Of Tag)(tag1_name)
        Dim t2 As Tag = repo.[Get](Of Tag)(tag2_name)

        Dim logList As Collection = New Collection

        Try
            If Not t1.IsTag Then
                Throw (New Halt("Tag 1 (" & tag1_name & ") does not exist."))
            End If

            If Not t2.IsTag Then
                Throw (New Halt("Tag 2 (" & tag2_name & ") does not exist."))
            End If

            Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
            Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target

            Dim ancestorCommit As Commit
            For Each ancestorCommit In c2.Ancestors
                If ancestorCommit = c1 Then Exit For
 
                Dim logMessage As String = Nothing
                If headeronly Then
                    logMessage = Common.getFirstSegment(ancestorCommit.Message, Chr(10))
                Else
                    logMessage = ancestorCommit.Message
                End If
                logList.Add(logMessage)
            Next

        Catch tag_not_found As Halt

        End Try

        Cursor.Current = cursorRevert

        Return logList


    End Function



End Class

