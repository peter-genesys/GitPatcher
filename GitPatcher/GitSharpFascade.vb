Imports GitSharp
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



    Shared Function getTagList(ByVal path) As Collection

        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)

        Dim tagnames As Collection = New Collection
  
        For Each Tag In repo.Tags
            tagnames.Add(Tag.Key)

        Next
 
        Return tagnames


    End Function

    Shared Function isRepo(ByVal path) As Boolean
        ' Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Return True
    End Function

    Shared Function currentBranch(ByVal path) As String
        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Return repo.CurrentBranch.Name

    End Function


    Shared Function getSchemaList(ByVal path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String) As Collection

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
                If InStr(change.Path, pathmask) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    schema = change.Path.Split("/")(1)
 
                    If Not schemas.Contains(schema) Then
                        schemas.Add(schema, schema)
                        Console.WriteLine(schema)
                    End If

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Return schemas

    End Function

 

    Shared Function getTagChanges(ByVal path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByVal viewFiles As Boolean) As Collection

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
                If InStr(change.Path, pathmask) > 0 And change.ChangeType <> ChangeType.Deleted Then

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

        Return changes

    End Function


    Shared Function viewTagChanges(ByVal repo_path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByRef targetFiles As CheckedListBox.CheckedItemCollection) As String

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
                If InStr(change.Path, pathmask) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    For Each file In targetFiles
                        If change.Path = file.ToString Then
                            Dim file_string_data As String = New Blob(repo, change.ChangedObject.Hash).Data
                            MsgBox(file_string_data)

                            result = result & Chr(10) & change.Path

                        End If
                    Next

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Return result


    End Function


    Shared Function exportTagChanges(ByVal repo_path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByRef targetFiles As CheckedListBox.CheckedItemCollection, patchDir As String) As Collection

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
                If InStr(change.Path, pathmask) > 0 And change.ChangeType <> ChangeType.Deleted Then

                    For Each file In targetFiles
                        If change.Path = file.ToString Then
                            Dim file_string_data As String = New Blob(repo, change.ChangedObject.Hash).Data
                            'MsgBox(file_string_data)

                            'Write the unix file
                            Dim l_filename As String = patchDir & "\" & change.Name

                            Dim l_file As New System.IO.StreamWriter(l_filename)
                            l_file.Write(file_string_data)
                            l_file.Close()

                            result = result & Chr(10) & change.Path

                            filePathList.Add(change.Path)

                        End If
                    Next

                End If
            Next

        Catch tag_not_found As Halt

        End Try

        Return filePathList


    End Function

 

End Class

