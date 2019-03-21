﻿Imports LibGit2Sharp
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

    Shared Function GetCommitFromSHA(ByVal SHA As String, Optional ByVal shaAlias As String = Nothing) As Commit

        If SHA = "" Then
            Throw New System.Exception(shaAlias & "SHA is required")
        End If

        Dim theTag As Tag = Globals.getRepo.Tags(SHA)

        If theTag Is Nothing Then
            Throw New System.Exception(shaAlias & "SHA (" & shaAlias & ") is unrecognised.")
        End If

        Dim theCommit As Commit = Globals.getRepo().Lookup(Of Commit)(SHA)

        Return theCommit

    End Function


    Shared Function GetCommitFromTagName(ByVal tagName As String, Optional ByVal tagAlias As String = Nothing) As Commit

        Dim theCommit As Commit

        If tagName = "" Then
            Throw New System.Exception(tagAlias & "Tag is required")
        End If

        If tagName = "HEAD" Then
            'If Tag is HEAD, find the tip commit of the HEAD branch 
            Dim theBranch As Branch = Globals.getRepo().Head
            theCommit = theBranch.Tip

        Else
            Dim theTag As Tag = Globals.getRepo.Tags(tagName)

            If theTag Is Nothing Then
                Throw New System.Exception(tagAlias & "Tag (" & tagName & ") is unrecognised.")
            End If

            theCommit = Globals.getRepo().Lookup(Of Commit)(theTag.Target.Sha)

        End If

        Return theCommit

    End Function

    Shared Function getTagNameList(ByVal inTagNames As Collection, Optional ByVal tagNameFilter As String = Nothing) As Collection
        'Input inTagNames         - a collection of tagNames
        'Input tagNameFilter  - search for matching tag names
        'Output outTagNames       - a collection of tagNames, initialised with inTagNames
        '  If tagNameFilter is null then all tags found in the repo are appended to outTagNames.
        '  If tagNameFilter is NOT null then only tags with tag name that contain the filter will be appended to outTagNames.


        Dim outTagNames As Collection = inTagNames

        For Each Tag In Globals.getRepo.Tags
            If String.IsNullOrEmpty(tagNameFilter) Then
                outTagNames.Add(Tag.FriendlyName)
            ElseIf Tag.FriendlyName.Contains(tagNameFilter) Then
                outTagNames.Add(Tag.FriendlyName)
            End If

        Next

        Return outTagNames

    End Function


    Shared Function getTagNameList(Optional ByVal tagNameFilter As String = Nothing) As Collection

        'Input tagNameFilter  - search for matching tag names
        'Output outTagNames       - a collection of tagNames, initialised with an empty set of tagNames
        '  If tagNameFilter is null then all tags found in the repo are appended to outTagNames.
        '  If tagNameFilter is NOT null then only tags with tag name that contain the filter will be appended to outTagNames.

        Dim outTagNames As Collection = New Collection

        Return getTagNameList(outTagNames, tagNameFilter)

    End Function

    Shared Function getTagList(ByVal inTags As Collection, Optional ByVal tagNameFilter As String = Nothing) As Collection
        'Input inTags         - a collection of tags
        'Input tagNameFilter  - search for matching tag names
        'Output outTags       - a collection of tags, initialised with inTags
        '  If tagNameFilter is null then all tags found in the repo are appended to outTags.
        '  If tagNameFilter is NOT null then only tags with tag name that contain the filter will be appended to outTags.


        Dim outTags As Collection = inTags

        For Each Tag In Globals.getRepo.Tags
            If String.IsNullOrEmpty(tagNameFilter) Then
                outTags.Add(Tag)
            ElseIf Tag.FriendlyName.Contains(tagNameFilter) Then
                outTags.Add(Tag)
            End If

        Next

        Return outTags

    End Function


    Shared Function getTagList(Optional ByVal tagNameFilter As String = Nothing) As Collection

        'Input tagNameFilter  - search for matching tag names
        'Output outTags       - a collection of tags, initialised with an empty set of tags
        '  If tagNameFilter is null then all tags found in the repo are appended to outTags.
        '  If tagNameFilter is NOT null then only tags with tag name that contain the filter will be appended to outTags.

        Dim outTags As Collection = New Collection

        Return getTagList(outTags, tagNameFilter)

    End Function


    'Shared Function isRepo(ByVal path) As Boolean
    ' ' Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
    ' Return True
    ' End Function

    Shared Function CurrentBranch() As String
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

    Shared Sub SwitchCommit(ByVal theCommit As Commit)
        'Switch to any existing commit
        'GitBash.Switch(Globals.getRepoPath, branchName)
        'Tortoise.Switch(ibranch_name, True)

        Try

            Commands.Checkout(Globals.getRepo, theCommit)

        Catch e As Exception
            MsgBox(e.Message)
        End Try

        ''Verify that the switch occurred and if not, use tortoise to do it.
        ''Thus exposing the issue, so the developer can resolve it, before proceeding.
        'If Globals.currentBranch <> branchName Then
        '    Tortoise.Switch(Globals.getRepoPath)
        'End If


    End Sub





    Shared Sub SwitchBranch(ByVal branchName As String)
        'Switch to an existing local branch
        'GitBash.Switch(Globals.getRepoPath, branchName)
        'Tortoise.Switch(ibranch_name, True)

        Dim existingBranch As Branch = Globals.getRepo.Branches(branchName)

        Try
            Commands.Checkout(Globals.getRepo, existingBranch)

        Catch e As Exception
            MsgBox(e.Message)
        End Try

        Logger.Note("Globals.currentLongBranch ", Globals.currentLongBranch)
        Logger.Note("Globals.currentBranch ", Globals.currentBranch)

        'Verify that the switch occurred and if not, use tortoise to do it.
        'Thus exposing the issue, so the developer can resolve it, before proceeding.
        If Globals.currentLongBranch <> branchName Then
            Tortoise.Switch(Globals.getRepoPath)
        End If


    End Sub

    Shared Sub SwitchHead()
        'Switch to an existing local branch, pointed to by head

        Dim theBranch As Branch = Globals.getRepo().Head

        SwitchBranch(theBranch.FriendlyName)

    End Sub


    Shared Sub SwitchTagName(ByVal theTagName As String)

        If theTagName = "HEAD" Then
            'Checkout the head
            GitOp.SwitchHead()

            'This would also Switch to HEAD.tip 
            'But may be better to switch to HEAD by branch name than by commit.
            'GitOp.SwitchCommit(GitOp.GetCommitFromTagName('HEAD'))
        Else
            'Checkout the tag
            GitOp.SwitchCommit(GitOp.GetCommitFromTagName(theTagName))
        End If

    End Sub


    Shared Sub createAndSwitchBranch(ByVal branchName As String)
        'Create then switch to a local branch
        'GitBash.createBranch(Globals.getRepoPath, newBranch)


        createBranch(branchName)
        SwitchBranch(branchName)

    End Sub


    Shared Sub Merge(ByVal targetBranch As String)
        'Merge the targetBranch into the current branch 
        'NB NoFastForward

        'Temporary fix - disable GitOp.Merge - ALWAYS use Tortoise.Merge
        Tortoise.Merge(Globals.getRepoPath)

        'Try
        '    Dim options As MergeOptions = New MergeOptions()
        '    options.FastForwardStrategy = FastForwardStrategy.NoFastForward

        '    Dim UserName As String = Globals.getRepo.Config(10).Value
        '    Dim UserEmail As String = Globals.getRepo.Config(11).Value
        '    Logger.Note("UserName", UserName)
        '    Logger.Note("UserEmail", UserEmail)

        '    Dim mySignature As Signature = New Signature(UserName, UserEmail, New DateTimeOffset(DateTime.Now))

        '    Globals.getRepo.Merge(Globals.getRepo.Branches(targetBranch).Tip, mySignature, options)

        'Catch e As Exception
        '    MsgBox(e.Message)
        '    'If GitOp.Merge fails try Tortoise.Merge instead.
        '    Tortoise.Merge(Globals.getRepoPath)
        'End Try



    End Sub






    Shared Sub createTag(ByVal tagName As String)
        'create a tag at the head

        Dim newTag As Tag = Globals.getRepo.ApplyTag(tagName)

    End Sub


    Shared Sub pushBranch(ByVal ibranch_name As String)

        'TEMP workaround use TGit or GitBash
        Tortoise.Push(ibranch_name, True)
        'GitBash.Push(Globals.getRepoPath, "origin", ibranch_name, True)


        ''push any branch
        ''? Does this push tags
        ''? Is it synchronous /  asynchronous
        ''Dim thePushOptions As PushOptions = New PushOptions()

        'BUG - code below MAY be corrupting the index.

        'Dim existingBranch As Branch = Globals.getRepo.Branches(ibranch_name)
        'Globals.getRepo().Network.Push(existingBranch)

    End Sub

    Shared Sub pushCurrentBranch()
        'push current branch

        pushBranch(CurrentBranch)

    End Sub

    Shared Sub pullBranch(ByVal ibranch_name As String)

        'GitBash.Pull(Globals.getRepoPath, "origin", ibranch_name)
        'Pull(ibranch_name, True)


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
        'pull current branch

        pullBranch(CurrentBranch())

    End Sub


    Shared Sub getIndexedChanges()
        'NOT CURRENTLY USED - LINKED TO HIDDEN MENU ITEM "ShowIndex"
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

        Dim commit1 As Commit = GetCommitFromSHA(SHA_1, "1st ")
        Dim commit2 As Commit = GetCommitFromSHA(SHA_2, "2nd ")

        Globals.setCommits(commit1, commit2)

    End Sub

    Shared Sub setCommitsFromTags(ByVal tag1_name As String, ByVal tag2_name As String)

        'Find the commits referred to by Tag 1 and Tag 2 and store them in persistent variables in the Globals module

        Dim commit1 As Commit = GetCommitFromTagName(tag1_name, "1st ")
        Dim commit2 As Commit = GetCommitFromTagName(tag2_name, "2nd ")

        Globals.setCommits(commit1, commit2)

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


    Shared Function ViewChanges(ByVal pathmask As String, ByRef targetFiles As Collection) As String

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
                        Dim file_string_data As String = Globals.getRepo.Lookup(Of Blob)(change.Oid).GetContentText
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

    Shared Function ExportChanges(ByVal pathmask As String, ByRef targetFiles As Collection, patchDir As String) As Collection

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
                        Dim file_string_data As String = Globals.getRepo.Lookup(Of Blob)(change.Oid).GetContentText

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



    Shared Function Log(Optional ByVal headerOnly As Boolean = True) As Collection
        Application.DoEvents()
        Dim cursorRevert As System.Windows.Forms.Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        'Returns a log list from Commit 1 to Commit 2

        Dim filter = New CommitFilter() With {
        .IncludeReachableFrom = Globals.getCommit2.Sha,  'TO
        .ExcludeReachableFrom = Globals.getCommit1.Sha   'FROM
        }

        Dim commitList = Globals.getRepo.Commits.QueryBy(filter).ToList()

        Dim logList As Collection = New Collection

        Dim ancestorCommit As Commit

        For Each ancestorCommit In commitList
            If ancestorCommit = Globals.getCommit1 Then Exit For

            Dim logMessage As String = Nothing
            If headerOnly Then
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

    Public Shared Function IsDirty() As Boolean

        ' Dim branchTip = Globals.getRepo.Branches(branchName).Tip

        Dim repoStatus As RepositoryStatus = Globals.getRepo.RetrieveStatus()

        Return repoStatus.IsDirty

    End Function

    Public Shared Function RepoStatus() As RepositoryStatus

        Return Globals.getRepo.RetrieveStatus()

    End Function

    Public Shared Function ChangedFiles() As Integer

        Dim repoStatus As RepositoryStatus = Globals.getRepo.RetrieveStatus()
        Dim changedFilesCount As Integer = repoStatus.Missing.Count + repoStatus.Modified.Count + repoStatus.Removed.Count + repoStatus.Added.Count

        Return changedFilesCount

    End Function



End Class

