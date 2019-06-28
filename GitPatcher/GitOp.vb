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

    Shared Sub Confirm(ByVal Message, ByVal Title)
        Dim button As MsgBoxResult
        button = MsgBox(Message, MsgBoxStyle.OkCancel, Title)
        If button = MsgBoxResult.Cancel Then
            Throw New System.Exception("User Cancelled Operation")
        End If

    End Sub

    Shared Function Retry(ByVal Message, ByVal Title, Optional ByVal buttonMeaning = "Yes to Retry, No to use TortoiseGit, or Cancel to abort process.") As Boolean
        Dim button As MsgBoxResult
        button = MsgBox(Message & Environment.NewLine & Environment.NewLine &
                        buttonMeaning,
                        MsgBoxStyle.YesNoCancel, Title)
        If button = MsgBoxResult.Cancel Then
            Throw New System.Exception("User Cancelled Operation")
        End If

        Return button = MsgBoxResult.Yes

    End Function



    Shared Function GetCommitFromSHA(ByVal SHA As String, Optional ByVal shaAlias As String = Nothing) As Commit

        If SHA = "" Then
            Throw New Exception(shaAlias & "SHA is required")
        End If

        'Dim theTag As Tag = Globals.getRepo.Tags(SHA)

        'If theTag Is Nothing Then
        '    Throw New Exception(shaAlias & "SHA (" & SHA & ") is unrecognised.")
        'End If

        Try
            Dim theCommit As Commit = Globals.getRepo().Lookup(Of Commit)(SHA)
            Return theCommit
        Catch ex As Exception
            Throw New Exception(shaAlias & "SHA (" & SHA & ") is unrecognised.")
        End Try



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


    Shared Function GetCommitFromBranchName(ByVal branchName As String, Optional ByVal branchAlias As String = Nothing) As Commit

        Dim theBranch As Branch
        Dim theCommit As Commit

        If branchName = "" Then
            Throw New System.Exception(branchAlias & "Branch is required")
        End If

        If branchName = "HEAD" Then
            'If branch is HEAD, find the tip commit of the HEAD branch 
            theBranch = Globals.getRepo().Head

        Else
            theBranch = Globals.getRepo.Branches(branchName)

            If theBranch Is Nothing Then
                Throw New System.Exception(branchAlias & "Branch (" & branchName & ") is unrecognised.")
            End If

        End If

        theCommit = theBranch.Tip

        Return theCommit

    End Function


    Shared Function getTipSHA() As String

        Dim theBranch As Branch = Globals.getRepo().Head
        Return theBranch.Tip.Sha

    End Function


    Shared Function getBranchNameList(ByVal inNames As Collection, Optional ByVal nameFilter As String = Nothing) As Collection
        'Input inNames         - a collection of Names
        'Input nameFilter      - search for matching names
        'Output outNames       - a collection of Names, initialised with inNames
        '  If nameFilter is null - then all branches found in the repo are appended to outNames.
        '  If nameFilter is NOT null then only branches with branches name that contain the filter will be appended to outNames.
        Logger.Note("getBranchNameList(inNames,nameFilter)", nameFilter)

        Dim outNames As Collection = inNames

        For Each branch In Globals.getRepo.Branches
            If String.IsNullOrEmpty(nameFilter) Then
                outNames.Add(branch.FriendlyName)
            ElseIf branch.FriendlyName.Contains(nameFilter) Then
                outNames.Add(branch.FriendlyName)
            End If

        Next

        Return outNames

    End Function

    Shared Function getBranchNameList(Optional ByVal nameFilter As String = Nothing) As Collection
        'Overloaded
        'Input nameFilter      - search for matching names
        'Output outNames       - a collection of Names, initialised with inNames
        '  If nameFilter is null - then all branches found in the repo are appended to outNames.
        '  If nameFilter is NOT null then only branches with branches name that contain the filter will be appended to outNames.
        Logger.Note("getBranchNameList(nameFilter)", nameFilter)

        Dim outNames As Collection = New Collection

        Return getBranchNameList(outNames, nameFilter)

    End Function




    Shared Function getReleaseList(ByVal appCode As String) As Collection

        Return getBranchNameList("release/" & appCode & "/")

    End Function


    Shared Function getPatchVersionReleaseList(ByVal appCode As String) As Collection

        Dim PatchVersionReleaseList As Collection = New Collection
        For Each release In getBranchNameList("release/" & appCode & "/")

            Dim PatchId As Integer
            Try
                PatchId = Common.getLastSegment(release, ".")
            Catch ex As Exception
                MsgBox(ex.Message)
                Throw New System.Exception("Unable to read patch version id")
            End Try

            If PatchId > 0 Then
                'Patch version release branch
                PatchVersionReleaseList.Add(release, release)
            End If

        Next

        If PatchVersionReleaseList.Count = 0 Then
            Throw New System.Exception("No release branches found!")
        End If

        Return PatchVersionReleaseList

    End Function





    Shared Function getReleaseAppCodeList(Optional ByVal nameFilter As String = Nothing) As Collection
        Logger.Note("getReleaseAppCodeList(nameFilter)", nameFilter)

        Dim releaseBranches As Collection = getBranchNameList("release/" & nameFilter)

        Dim appCodeList As Collection = New Collection

        For Each branch In releaseBranches
            Dim lAppCode As String = Common.getNthSegment(branch, "/", 2)
            If lAppCode = "release" Then
                lAppCode = Common.getNthSegment(branch, "/", 3)
            End If

            If Not appCodeList.Contains(lAppCode) Then
                appCodeList.Add(lAppCode, lAppCode)
            End If

        Next

        Return appCodeList

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
        'Overloaded
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

    Shared Function CurrentFriendlyBranch() As String
        'Return the name of the branch at the head.

        Try

            Return Globals.getRepo.Head.FriendlyName
        Catch
            MsgBox("Cannot determine current branch")
            Logger.Dbg(Globals.getRepo.ToString)
            Return "Oops"
        End Try

    End Function

    Shared Sub createBranch(ByVal branchName As String)
        'Create a new local branch
        Logger.Note("createBranch(branchName)", branchName)

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


    'Shared Sub CheckoutBranch(ByVal branchName As String)
    'Checkout a remote branch

    '    Const String localBranchName = "theBranch";

    '// The local branch doesn't exist yet
    'Assert.Null(repo.Branches[localBranchName]);

    '// Let's get a reference on the remote tracking branch...
    'Const String trackedBranchName = "origin/theBranch";
    'Branch trackedBranch = repo.Branches[trackedBranchName];

    '// ...And create a local branch pointing at the same Commit
    'Branch branch = repo.CreateBranch(localBranchName, trackedBranch.Tip);

    '// The local branch Is Not configured to track anything
    'Assert.False(branch.IsTracking);

    '// So, let's configure the local branch to track the remote one.
    'Branch updatedBranch = repo.Branches.Update(branch,
    '    b >= b.TrackedBranch = trackedBranch.CanonicalName);

    '// Bam! It's done.
    'Assert.True(updatedBranch.IsTracking);
    'Assert.Equal(trackedBranchName, updatedBranch.TrackedBranch.Name);
    'End Sub



    Shared Sub SwitchBranch(ByVal branchName As String)
        'Switch to an existing local branch
        Logger.Note("SwitchBranch(branchName)", branchName)

        Dim theSetting As String = My.Settings.SwitchTool
        Dim ToolName As String = "SwitchTool"

        ' Loop until swiched to correct branch
        While Globals.currentLongBranch <> branchName

            Try

                Select Case theSetting
                    Case "TGIT"
                        Tortoise.Switch(Globals.getRepoPath)
                'MsgBox(ToolName + " TortoiseGit not currently implimented")
                    Case "BGIT"
                        GitBash.Switch(Globals.getRepoPath, branchName)
                'MsgBox(ToolName + " GitBash not currently implimented")
                    Case "LGIT"
                        Dim existingBranch As Branch = Globals.getRepo.Branches(branchName)

                        Try
                            Commands.Checkout(Globals.getRepo, existingBranch)
                        Catch ex As Exception
                            '@TODO If branch is not local, confirm checkout from origin repo

                            MsgBox(ex.Message)
                            Throw ex
                        End Try



                'MsgBox(ToolName + " LibGit2 not currently implimented")

                    Case "SGIT"
                        MsgBox(ToolName + " GitSharp not currently implimented")
                    Case Else
                        Throw New System.Exception("Unknown " + ToolName + " setting " + theSetting)
                End Select

            Catch e As Exception
                MsgBox(e.Message)
            End Try

            Logger.Note("Globals.currentLongBranch ", Globals.currentLongBranch)
            Logger.Note("Globals.currentBranch ", Globals.currentBranch)

            'Verify that the switch occurred
            If Globals.currentLongBranch = branchName Then
                Exit While
            End If

            'Confirm retry or use tortoiseGIT
            If Not Retry("Switch to " & branchName & "Failed." & Environment.NewLine & Environment.NewLine &
                        "Before continuing, please remove locks on files or folders at this path: " & Globals.getRepoPath &
                        "Eg close these programs:" & Environment.NewLine &
                        "+ Text editors: Sublime, Notepad++, WinMerge" & Environment.NewLine &
                        "+ Executables : SQL, Powershell",
                        "Retry Switch") Then
                theSetting = "TGIT"
            End If

        End While


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
        Logger.Note("createAndSwitchBranch(branchName)", branchName)

        createBranch(branchName)

        SwitchBranch(branchName)

    End Sub


    '    Public void CommitChanges() {
    '    Try {

    '        repo.Commit("updating files..", New Signature(username, email, DateTimeOffset.Now),
    '            New Signature(username, email, DateTimeOffset.Now));
    '    }
    '    Catch (Exception e) {
    '        Console.WriteLine("Exception:RepoActions:CommitChanges " + e.Message);
    '    }
    '}


    Shared Sub Merge(ByVal targetBranch As String, Optional ByVal iFastForward As Boolean = False)
        'Merge the targetBranch into the current branch 
        'NB NoFastForward

        Dim theSetting As String = My.Settings.MergeTool
        Dim ToolName As String = "MergeTool"

        Do
            Try

                Select Case theSetting
                    Case "TGIT"
                        Tortoise.Merge(Globals.getRepoPath)
                'MsgBox(ToolName + " TortoiseGit not currently implimented")
                    Case "BGIT"
                        'GitBash.Merge(Globals.getRepoPath, branchName)
                        MsgBox(ToolName + " GitBash not currently implimented")
                    Case "LGIT"

                        Dim options As MergeOptions = New MergeOptions()
                        If iFastForward Then
                            options.FastForwardStrategy = FastForwardStrategy.FastForwardOnly
                        Else
                            options.FastForwardStrategy = FastForwardStrategy.NoFastForward
                        End If

                        'To allow for a custom message - set commt on success to false and commit afterwards
                        options.CommitOnSuccess = False

                        Dim UserName As String = Globals.getRepoConfig("user.name")
                        Dim UserEmail As String = Globals.getRepoConfig("user.email")

                        Logger.Note("UserName", UserName)
                        Logger.Note("UserEmail", UserEmail)

                        Dim mySignature As Signature = New Signature(UserName, UserEmail, New DateTimeOffset(DateTime.Now))

                        Globals.getRepo.Merge(Globals.getRepo.Branches(targetBranch).Tip, mySignature, options)

                        'Now commit with message
                        Globals.getRepo.Commit("Merge " & targetBranch & " to " & Globals.currentBranch, mySignature, mySignature)

                'MsgBox(ToolName + " LibGit2 not currently implimented")

                    Case "SGIT"
                        MsgBox(ToolName + " GitSharp not currently implimented")
                    Case Else
                        Throw New System.Exception("Unknown " + ToolName + " setting " + theSetting)
                End Select

                Exit Do

            Catch e As Exception
                MsgBox(e.Message)

                'Confirm retry or use tortoiseGIT
                If Not Retry("Merge of " & targetBranch & "Failed.", "Retry Merge") Then
                    theSetting = "TGIT"
                End If

            End Try


        Loop

    End Sub


    Shared Sub deleteTag(ByVal tagName As String)
        'delete a tag

        Try

            Globals.getRepo.Tags.Remove(tagName)
            Logger.Dbg("Tag " & tagName & " deleted.")

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub



    Shared Sub createTagHead(ByVal tagName As String, Optional ByVal force As Boolean = False)
        'create a tag at the head
        Dim newTag As Tag

        Try

            newTag = Globals.getRepo.ApplyTag(tagName)

        Catch ex As LibGit2SharpException

            If Not force Then
                Logger.Dbg(ex.Message)
                'Confirm move of tag
                Dim result As Integer = MessageBox.Show("Update existing Tag " & tagName &
                    Chr(10) & "The tag will be moved to the head of the current branch " & Globals.currentBranch & ".", "Confirm Tag Update", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

            Globals.getRepo.Tags.Remove(tagName)

            newTag = Globals.getRepo.ApplyTag(tagName)
            MessageBox.Show("Tag updated to head.")

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub

    Shared Sub createTagSHA(ByVal tagName As String, ByVal tagSHA As String, Optional ByVal force As Boolean = False)
        'create a tag at the SHA

        Dim newTag As Tag

        Try

            newTag = Globals.getRepo.ApplyTag(tagName, tagSHA) 'Use the given SHA

        Catch ex As LibGit2SharpException

            If Not force Then
                Logger.Dbg(ex.Message)
                'Confirm move of tag
                Dim result As Integer = MessageBox.Show("Update existing Tag " & tagName &
                    Chr(10) & "The tag will be moved to the SHA " & tagSHA & ".", "Confirm Tag Update", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

            Globals.getRepo.Tags.Remove(tagName)

            newTag = Globals.getRepo.ApplyTag(tagName, tagSHA) 'Use the given SHA
            MessageBox.Show("Tag updated to SHA " & tagSHA)

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub



    Shared Sub pushBranch(ByVal ibranch_name As String, Optional ByVal iremote_name As String = "origin", Optional ByVal iSetUpstream As Boolean = False)
        Logger.Dbg("GitOp.pushBranch(" & ibranch_name & "," & iremote_name & ")")

        Dim theSetting As String = My.Settings.PushTool
        Dim ToolName As String = "PushTool"

        Do
            Try

                Select Case theSetting
                    Case "TGIT"
                        Tortoise.Push(Globals.getRepoPath)
                'MsgBox(ToolName + " TortoiseGit not currently implimented")
                    Case "BGIT"
                        Try
                            If Not GitBash.PushSuccess(Globals.getRepoPath, iremote_name, ibranch_name, True, True, iSetUpstream) Then
                                MsgBox("Push has failed.  The Synchronisation screen may help you resolve the issue.")
                                'If push fails show the synch screen.
                                Tortoise.Sync(Globals.getRepoPath)
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message)
                            Throw New System.Exception("Check GitBash configuration.")
                        End Try

                'MsgBox(ToolName + " GitBash not currently implimented")
                    Case "LGIT"
                        ''push any branch
                        ''? Does this push tags
                        ''? Is it synchronous /  asynchronous
                        ''Dim thePushOptions As PushOptions = New PushOptions()

                        ''BUG - THE CODE BELOW SEEMED TO WORK - BUT CORRUPTS the INDEX or PACKFILES
                        ''Resulting PackFile Index mismatch errors that "infect" the repo

                        'Dim existingBranch As Branch = Globals.getRepo.Branches(ibranch_name)
                        'Globals.getRepo().Network.Push(existingBranch)

                        MsgBox(ToolName + " LibGit2 not currently used - contains bug")

                    Case "SGIT"
                        MsgBox(ToolName + " GitSharp not currently implimented")
                    Case Else
                        Throw New System.Exception("Unknown " + ToolName + " setting " + theSetting)
                End Select

                Exit Do

            Catch e As Exception
                MsgBox(e.Message)

                'Confirm retry or use tortoiseGIT
                If Not Retry("Push of " & ibranch_name & " to " & iremote_name & " Failed.", "Retry Push") Then
                    theSetting = "TGIT"
                End If

            End Try


        Loop



    End Sub

    Shared Sub pushCurrentBranch(Optional ByVal iremote_name As String = "origin", Optional ByVal iSetUpstream As Boolean = False)
        'push current branch
        Logger.Dbg("GitOp.pushCurrentBranch")
        pushBranch(CurrentFriendlyBranch, iremote_name, iSetUpstream)

    End Sub

    Shared Sub pullBranch(ByVal ibranch_name As String, Optional ByVal iremote_name As String = "origin")


        Dim theSetting As String = My.Settings.PullTool
        Dim ToolName As String = "PullTool"

        Do
            Try

                Select Case theSetting
                    Case "TGIT"
                        Tortoise.Pull(Globals.getRepoPath)
                'MsgBox(ToolName + " TortoiseGit not currently implimented")
                    Case "BGIT"
                        GitBash.Pull(Globals.getRepoPath, iremote_name, ibranch_name)
                'MsgBox(ToolName + " GitBash not currently implimented")
                    Case "LGIT"
                        'LGIT pulls from current upstream

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

                        Dim UserName As String = Globals.getRepoConfig("user.name")
                        Dim UserEmail As String = Globals.getRepoConfig("user.email")

                        Logger.Note("UserName", UserName)
                        Logger.Note("UserEmail", UserEmail)

                        Dim mySignature As Signature = New Signature(UserName, UserEmail, New DateTimeOffset(DateTime.Now))

                        'Pull the branch
                        Dim myMergeResult As MergeResult = Commands.Pull(Globals.getRepo, mySignature, options)

                            'MsgBox(ToolName + " LibGit2 not currently implimented")

                    Case "SGIT"
                        MsgBox(ToolName + " GitSharp not currently implimented")
                    Case Else
                        Throw New System.Exception("Unknown " + ToolName + " setting " + theSetting)
                End Select

                Exit Do

            Catch e As Exception
                MsgBox(e.Message)

                'If GitOp.Merge fails try Tortoise.Pull instead.
                If Globals.currentBranchType = "feature" Then

                    Try
                        'Confirm retry or use tortoiseGIT on feature branch
                        If Not Retry("Pull of " & ibranch_name & " from " & iremote_name & " Failed." & Environment.NewLine & Environment.NewLine &
                                 "Looks like you are on a feature branch.  This pull may have failed because your feature does not exist on the remote. ",
                                 "Yes to Retry, No to use TortoiseGit, or Cancel to skip this Pull.") Then
                            theSetting = "TGIT"
                        End If
                    Catch ex As Exception
                        MsgBox("User cancelled Pull")
                    End Try

                Else

                    'Confirm retry or use tortoiseGIT
                    If Not Retry("Pull of " & ibranch_name & " from " & iremote_name & " Failed.", "Retry Pull") Then
                        theSetting = "TGIT"
                    End If

                End If

            End Try

        Loop

    End Sub

    Shared Sub pullCurrentBranch()
        'pull current branch

        pullBranch(CurrentFriendlyBranch())

    End Sub


    Shared Sub pullWhenMasterBranch()
        Logger.Dbg("pullWhenMasterBranch is disabled")
        'pull current branch - but only when it is master, or at least does not contain feature
        ' If CurrentBranch() = "master" Or Not CurrentBranch().Contains("feature") Then
        ' Logger.Dbg("Pulling " & CurrentBranch())
        ' pullBranch(CurrentBranch())
        ' End If

    End Sub

    Shared Sub Checkout(ByVal theBranch As Branch)
        'Checkout to any existing branch

        'GitBash.Switch(Globals.getRepoPath, branchName)
        'Tortoise.Switch(ibranch_name, True)

        Try

            Commands.Checkout(Globals.getRepo, theBranch)

        Catch e As Exception
            MsgBox(e.Message)
        End Try

        ''Verify that the switch occurred and if not, use tortoise to do it.
        ''Thus exposing the issue, so the developer can resolve it, before proceeding.
        'If Globals.currentBranch <> branchName Then
        '    Tortoise.Switch(Globals.getRepoPath)
        'End If

    End Sub

    Shared Sub Checkout(ByVal theCommit As Commit)
        'Checkout any existing commit

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


    Shared Sub setCommitsFromBranches(ByVal branch1_name As String, ByVal branch2_name As String)

        'Find the commits referred to by tip of Branch 1 and tip of Branch 2 and store them in persistent variables in the Globals module

        Dim commit1 As Commit = GetCommitFromBranchName(branch1_name, "1st ")
        Dim commit2 As Commit = GetCommitFromBranchName(branch2_name, "2nd ")

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


    Shared Function getChanges(ByVal pathmask As String, ByVal viewFiles As Boolean,
                               Optional ByVal pathAsValue As Boolean = True,
                               Optional ByVal pathAsIndex As Boolean = True) As Collection
        'Get a list of changes found in the paths of files modified between the stored commits in Globals module
        'pathAsValue and pathAsIndex are used to specify whether to use fullpath or filename for the value and index of each added item

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

                Dim filename As String = Common.getLastSegment(change.Path, "/")

                Dim lvalue As String = Nothing
                If pathAsValue Then
                    lvalue = change.Path
                Else
                    lvalue = filename
                End If

                Dim lindex As String = Nothing
                If pathAsIndex Then
                    lindex = change.Path
                Else
                    lindex = filename
                End If


                changePaths.Add(lvalue, lindex)

                If viewFiles Then
                    Dim file_string_data As String = Globals.getRepo.Lookup(Of Blob)(change.Oid).ToString
                    MsgBox(file_string_data)
                End If

            End If
        Next

        Cursor.Current = cursorRevert

        Return changePaths

    End Function


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

        Dim repoStatus As RepositoryStatus = Globals.getRepo.RetrieveStatus()

        Logger.Dbg("repoStatus.IsDirty " & repoStatus.IsDirty.ToString)

        Return repoStatus.IsDirty

    End Function

    Public Shared Function RepoStatus() As RepositoryStatus

        Return Globals.getRepo.RetrieveStatus()

    End Function

    Public Shared Function ChangedFiles() As Integer

        Logger.Dbg("Globals.getRepoPath " & Globals.getRepoPath)
        Dim repoStatus As RepositoryStatus = Globals.getRepo.RetrieveStatus()
        Logger.Dbg("repoStatus.Missing.Count " & repoStatus.Missing.Count)
        Logger.Dbg("repoStatus.Removed.Count " & repoStatus.Removed.Count)
        Logger.Dbg("repoStatus.Modified.Count " & repoStatus.Modified.Count)
        Logger.Dbg("repoStatus.Added.Count " & repoStatus.Added.Count)
        Logger.Dbg("repoStatus.Staged.Count " & repoStatus.Staged.Count)
        Dim changedFilesCount As Integer = repoStatus.Missing.Count + repoStatus.Modified.Count + repoStatus.Removed.Count + repoStatus.Added.Count + repoStatus.Staged.Count

        Return changedFilesCount

    End Function



End Class

