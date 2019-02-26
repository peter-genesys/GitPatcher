Imports LibGit2Sharp
Imports LibGit2Sharp.Commands

Class Halt
    Inherits Exception
    Public Sub New(ByVal str As String)
        Console.WriteLine(str) 'User defined exception
    End Sub 'New
End Class 'Halt


Public Class GitOp
    Dim halt As Exception

    'Dim repo As GitSharp.Repository

    'Public Sub New(ByVal path)
    '    'Dim dir As System.IO.DirectoryInfo
    '    'dir = New IO.DirectoryInfo(path)
    '    repo = New Repository(path)
    '
    'End Sub


    Shared Function getTagList(ByVal currentTags As Collection, Optional ByVal filter As String = Nothing) As Collection
        'Input currentTags - a collection of tag names
        'Input filter      - search for matching tag names
        'Output tagnames   - a collection of tag names, initialised with currentTags
        '  If filter is null then all tag names found in the repo are appended to tagnames.
        '  If filter is NOT null then only tag names that contain the filter will be appended to tagnames.


        Dim tagnames As Collection = currentTags

        For Each Tag In Globals.getRepo.Tags
            If String.IsNullOrEmpty(filter) Then
                tagnames.Add(Tag.Reference)
            ElseIf Tag.ToString.Contains(filter) Then
                tagnames.Add(Tag.Reference)
            End If

        Next

        Return tagnames

    End Function

    Shared Function getTagList(Optional ByVal filter As String = Nothing) As Collection

        'Input filter      - search for matching tag names
        'Output  tagnames  - a collection of tag names.  
        '  If filter is null then all tag names found in the repo are returned.
        '  If filter is NOT nutl then only tag names that contain the filter will be returned.

        Dim tagnames As Collection = New Collection

        Return getTagList(tagnames, filter)

    End Function


    'Shared Function isRepo(ByVal path) As Boolean
    ' ' Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
    ' Return True
    ' End Function

    Shared Function currentBranch() As String
        'Return the name of the branch at the head.

        Try

            Return Globals.getRepo.Head.FriendlyName
        Catch
            MsgBox("Oops")
            Logger.Dbg(Globals.getRepo.ToString)
            Return "Oops"
        End Try

    End Function

    Shared Sub createBranch(ByVal branchName As String)
        'Create a new local branch

        Dim newBranch As Branch = Globals.getRepo.CreateBranch(branchName)

    End Sub

    Shared Sub switchBranch(ByVal branchName As String)
        'Switch to an existing local branch

        Dim existingBranch As Branch = Globals.getRepo.Branches(branchName)

        Try
            Commands.Checkout(Globals.getRepo, existingBranch)

        Catch e As Exception
            MsgBox(e.Message)
        End Try

        'Verify that the switch occurred and if not, use tortoise to do it.
        'Thus exposing the issue, so the developer can resolve it, before proceeding.
        If Globals.currentBranch <> branchName Then
            Tortoise.Switch(Globals.getRepoPath)
        End If


    End Sub

    Shared Sub createAndSwitchBranch(ByVal branchName As String)
        'Create then switch to a local branch

        createBranch(branchName)
        switchBranch(branchName)

    End Sub


    Shared Sub createTag(ByVal tagName As String)
        'create a tag at the head

        Dim newTag As Tag = Globals.getRepo.ApplyTag(tagName)

    End Sub


    Shared Sub pushBranch(ByVal ibranch_name As String)
        'push any branch
        '? Does this push tags
        '? Is it synchronous /  asynchronous

        'Dim thePushOptions As PushOptions = New PushOptions()

        Dim existingBranch As Branch = Globals.getRepo.Branches(ibranch_name)
        Globals.getRepo().Network.Push(existingBranch)

    End Sub

    Shared Sub pushCurrentBranch()
        'push current branch

        pushBranch(currentBranch)

    End Sub

    Shared Sub pullBranch(ByVal ibranch_name As String)
        'pull any branch
        Dim options As PullOptions = New PullOptions()

        options.MergeOptions = New MergeOptions()
        options.MergeOptions.FailOnConflict = True


        'May need this later to embed credential details..

        'options.FetchOptions.CredentialsProvider = New LibGit2Sharp.Credentials(CredentialsProvider()
        'options.FetchOptions.CredentialsProvider = New CredentialsProvider(DefaultCredentials)
        'options.FetchOptions.CredentialsProvider() = New CredentialsProvider(
        '(url, usernameFromUrl, types) >=
        '    New UsernamePasswordCredentials()
        '    {
        '        Username = "",
        '        Password = ""
        '    })

        'Get signature details from the current repo
        Dim UserName As String = Globals.getRepo.Config(10).Value
        Dim UserEmail As String = Globals.getRepo.Config(11).Value
        Logger.Note("UserName", UserName)
        Logger.Note("UserEmail", UserEmail)

        Dim mySignature As Signature = New Signature(UserName, UserEmail, New DateTimeOffset(DateTime.Now))

        'Pull the branch
        Dim myMergeResult As MergeResult = Commands.Pull(Globals.getRepo, mySignature, options)


    End Sub

    Shared Sub pullCurrentBranch()
        'push current branch

        pullBranch(currentBranch())

    End Sub


    Shared Sub getIndexedChanges()
        'I have not yet figured out what this is for
        'I think it might have been a debugging tool from a hidden menu item. ShowindexToolStripMenuItem

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor


        'COMMENTED THE SECTION BELOW ONLY SO THIS COMPILES - NEEDS WORK.
        'For Each entry In repo.Index.ToList
        '    MsgBox(entry.)
        '    Dim x As PathStatus = New GitSharp.PathStatus(repo, entry)
        '    If x.WorkingPathStatus <> 0 Or x.IndexPathStatus <> 0 Then

        '        MsgBox(entry & " WorkingPathStatus " & x.WorkingPathStatus)
        '        MsgBox(entry & " IndexPathStatus " & x.IndexPathStatus)
        '    End If

        'Next

        Cursor.Current = cursorRevert

    End Sub

    Shared Sub revertItems(ByVal itemPaths() As String)

        Commands.Unstage(Globals.getRepo, itemPaths)

    End Sub

    Shared Sub revertItem(ByVal itemPath As String)

        Dim itemPaths() As String = {itemPath}

        revertItems(itemPaths)

    End Sub


    Shared Sub setCommitsFromSHA(ByVal SHA_1 As String, ByVal SHA_2 As String)
        'Find the commits referred to by SHA 1 and SHA 2 and store them in persistent variables in the Globals module

        If SHA_1 = "" Then
            Throw New System.Exception("1st SHA is required")
        End If
        If SHA_2 = "" Then
            Throw New System.Exception("2nd SHA is required")
        End If

        Dim commit1 As Commit = Globals.getRepo().Lookup(Of Commit)(SHA_1)
        Dim commit2 As Commit = Globals.getRepo().Lookup(Of Commit)(SHA_2)

        If commit1 Is Nothing Then
            Throw New System.Exception("1st SHA (" & SHA_1 & ") is unrecognised.")
        End If
        If commit2 Is Nothing Then
            Throw New System.Exception("2nd SHA (" & SHA_2 & ") is unrecognised.")
        End If

        Globals.setCommits(commit1, commit2)

    End Sub

    Shared Sub setCommitsFromTags(ByVal tag1_name As String, ByVal tag2_name As String)

        'Find the commits referred to by Tag 1 and Tag 2 and store them in persistent variables in the Globals module

        If tag1_name = "" Then
            Throw New System.Exception("1st Tag is required")
        End If
        If tag2_name = "" Then
            Throw New System.Exception("2nd Tag is required")
        End If

        Dim tag1 As Tag = Globals.getRepo.Tags(tag1_name)
        Dim tag2 As Tag = Globals.getRepo.Tags(tag2_name)

        If tag1 Is Nothing Then
            Throw New System.Exception("1st Tag (" & tag1_name & ") is unrecognised.")
        End If
        If tag2 Is Nothing Then
            Throw New System.Exception("2nd Tag (" & tag2_name & ") is unrecognised.")
        End If

        setCommitsFromSHA(tag1.Target.Sha, tag2.Target.Sha)

    End Sub


    Shared Function getSchemaList(ByVal pathmask As String) As Collection
        'Get a list of schemas found in the paths of files modified between the stored commits in Globals module

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim schemas As Collection = New Collection
        Dim schema As String

        Dim changes As TreeChanges = Globals.getRepo().Diff.Compare(Of TreeChanges)(Globals.getCommit1.Tree, Globals.getCommit2.Tree)

        For Each change In changes

            If InStr(change.Path, pathmask_UNIX) > 0 And change.Status <> ChangeKind.Deleted Then

                schema = change.Path.Substring(Len(pathmask_UNIX)).Split("/")(0)

                If Not schemas.Contains(schema) Then
                    schemas.Add(schema, schema)
                    Console.WriteLine(schema)
                End If

            End If
        Next

        Cursor.Current = cursorRevert

        Return schemas

    End Function


    Shared Function getChanges(ByVal pathmask As String, ByVal viewFiles As Boolean) As Collection
        'Get a list of changes found in the paths of files modified between the stored commits in Globals module

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim result As String = Nothing

        Dim changes As TreeChanges = Globals.getRepo().Diff.Compare(Of TreeChanges)(Globals.getCommit1.Tree, Globals.getCommit2.Tree)
        Dim changePaths As Collection = New Collection

        For Each change In changes
            If InStr(change.Path, pathmask_UNIX) > 0 And change.Status <> ChangeKind.Deleted Then

                Console.WriteLine(change.Status & ": " & change.Path)
                result = result & Chr(10) & change.Status & ": " & change.Path
                changePaths.Add(change.Path)

                If viewFiles Then
                    Dim file_string_data As String = Globals.getRepo.Lookup(Of Blob)(change.Oid).ToString
                    MsgBox(file_string_data)
                End If

            End If
        Next

        Cursor.Current = cursorRevert

        Return changePaths

    End Function


    'DEPRECATED.  Now set the commits from tags then call getChanges.
    'Shared Function getTagChanges(ByVal path As String, ByVal tag1_name As String, ByVal tag2_name As String, ByVal pathmask As String, ByVal viewFiles As Boolean) As Collection

    '    Dim pathmask_UNIX As String = Common.unix_path(pathmask)

    '    Application.DoEvents()
    '    Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
    '    Cursor.Current = Cursors.WaitCursor


    '    Dim repo As GitSharp.Repository = New GitSharp.Repository(path)

    '    Dim result As String = Nothing

    '    Dim t1 As Tag = repo.[Get](Of Tag)(tag1_name)
    '    Dim t2 As Tag = repo.[Get](Of Tag)(tag2_name)

    '    Dim changes As Collection = New Collection

    '    Try
    '        If Not t1.IsTag Then
    '            Throw (New Halt("Tag 1 (" & tag1_name & ") does not exist."))
    '        End If

    '        If Not t2.IsTag Then
    '            Throw (New Halt("Tag 2 (" & tag2_name & ") does not exist."))
    '        End If

    '        Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
    '        Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target


    '        For Each change As Change In c1.CompareAgainst(c2)
    '            If InStr(change.Path, pathmask_UNIX) > 0 And change.ChangeType <> ChangeType.Deleted Then

    '                Console.WriteLine(change.ChangeType & ": " & change.Path)
    '                result = result & Chr(10) & change.ChangeType & ": " & change.Path
    '                changes.Add(change.Path)


    '                If viewFiles Then
    '                    Dim file_string_data As String = New Blob(repo, change.ChangedObject.Hash).Data
    '                    MsgBox(file_string_data)

    '                End If

    '            End If
    '        Next

    '    Catch tag_not_found As Halt

    '    End Try

    '    Cursor.Current = cursorRevert

    '    Return changes

    'End Function


    Shared Function viewChanges(ByVal repo_path As String, ByVal pathmask As String, ByRef targetFiles As Collection) As String

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        Dim result As String = Nothing

        Dim changes As TreeChanges = Globals.getRepo().Diff.Compare(Of TreeChanges)(Globals.getCommit1.Tree, Globals.getCommit2.Tree)
        Dim changePaths As Collection = New Collection


        For Each change In changes
            If InStr(change.Path, pathmask_UNIX) > 0 And change.Status <> ChangeKind.Deleted Then

                For Each file In targetFiles

                    If change.Path = file.ToString Then
                        Dim file_string_data As String = Globals.getRepo.Lookup(Of Blob)(change.Oid).ToString
                        MsgBox(file_string_data)

                        Clipboard.Clear()
                        Clipboard.SetText(file_string_data)

                        result = result & Chr(10) & change.Path

                    End If

                Next

            End If
        Next


        Cursor.Current = cursorRevert

        Return result


    End Function

    Shared Function exportChanges(ByVal pathmask As String, ByRef targetFiles As Collection, patchDir As String) As Collection

        Dim pathmask_UNIX As String = Common.unix_path(pathmask)

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        'Any files in the targetFiles that are not found are simply not exported.
        'This is currently being exploited as the filelist will be sent from the total patchable files treeview, 
        'even though it could contain files from the extras list, that are Not to be exported from the repo.

        Dim result As String = Nothing

        Dim filePathList As Collection = New Collection
        Dim changes As TreeChanges = Globals.getRepo().Diff.Compare(Of TreeChanges)(Globals.getCommit1.Tree, Globals.getCommit2.Tree)

        For Each change In changes
            If InStr(change.Path, pathmask_UNIX) > 0 And change.Status <> ChangeKind.Deleted Then

                For Each file In targetFiles

                    If change.Path = file.ToString Then
                        Dim file_string_data As String = Globals.getRepo.Lookup(Of Blob)(change.Oid).ToString
                        'MsgBox(file_string_data)

                        FileIO.writeFile(patchDir & "\" & change.Path.Split("/").Last, file_string_data)
                        result = result & Chr(10) & change.Path
                        filePathList.Add(change.Path)

                    End If

                Next

            End If
        Next



        Cursor.Current = cursorRevert

        Return filePathList


    End Function


    Shared Function Log(Optional ByVal headeronly As Boolean = True) As Collection

        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        'Returns a log list from Commit 1 to Commit 2

        Dim logList As Collection = New Collection

        Dim ancestorCommit As Commit
        For Each ancestorCommit In Globals.getCommit2.Parents
            If ancestorCommit = Globals.getCommit1 Then Exit For

            Dim logMessage As String = Nothing
            If headeronly Then
                logMessage = Common.getFirstSegment(ancestorCommit.Message, Chr(10))
            Else
                logMessage = ancestorCommit.Message
            End If
            logList.Add(logMessage)
        Next


        Cursor.Current = cursorRevert

        Return logList


    End Function


    Public Shared Function describe(ByVal branchName As String)

        Dim branchTip = Globals.getRepo.Branches(branchName).Tip

        Return Globals.getRepo.Describe(branchTip, New DescribeOptions With {.Strategy = DescribeStrategy.All})

    End Function

    Public Shared Function describeTags(ByVal branchName As String)

        Dim branchTip = Globals.getRepo.Branches(branchName).Tip

        Return Globals.getRepo.Describe(branchTip, New DescribeOptions With {.Strategy = DescribeStrategy.Tags})

    End Function

End Class

