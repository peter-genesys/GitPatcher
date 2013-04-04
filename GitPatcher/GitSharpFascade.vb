Imports GitSharp
 
Public Class GitSharpFascade

    Dim repo As GitSharp.Repository

    Public Sub New(ByVal path)
        'Dim dir As System.IO.DirectoryInfo
        'dir = New IO.DirectoryInfo(path)
        repo = New Repository(path)

    End Sub



    ' checkPath : return dir
    Shared Function checkPath(ByVal path) As String

        Dim repo As GitSharp.Repository
        'Dim dir As System.IO.DirectoryInfo
        'Dir = New IO.DirectoryInfo(path)
        repo = New GitSharp.Repository(path)

        Dim result As String = Nothing

        'dir = repo.WorkingDirectory

        Return repo.WorkingDirectory

    End Function

    Shared Function checkRevs(ByVal path) As String

        Dim repo As GitSharp.Repository
        Dim dir As System.IO.DirectoryInfo
        'dir = New IO.DirectoryInfo(path)
        repo = New Repository(path)

        Dim result As String = Nothing

        'dir = repo.WorkingDirectory

        result = repo.ToString

        Return result


    End Function

    Shared Function getDiff(ByVal path) As String

        Dim repo As GitSharp.Repository
        'Dim dir As System.IO.DirectoryInfo
        'dir = New IO.DirectoryInfo(path)


        repo = New GitSharp.Repository(path)

        ' Get the message of the previous commit
        Dim msg As String = New Commit(repo, "HEAD^").Message
        Debug.WriteLine(msg = repo.CurrentBranch.CurrentCommit.Parent.Message)


        'Print a list of changes between two commits c1 and c2:
        'Dim c1 As Commit = repo.[Get](Of Commit)("04e4f1fddfd989c368430674baa5efe2e5772585")
        'Dim c1 As Commit = repo.[Get](Of Tag)("TAG1")
        'Dim c1 As Commit = repo.[Get](Of Commit)(repo.[Get](Of Tag)("TAG1").Hash)
        Dim c1 As Commit = repo.[Get](Of Tag)("TAG1").Target
        ' <-- note: short hashes are not yet supported  
        'Dim c2 As New Commit(repo, "3e663c85995c7d5a5e1543382ebac12da21025d8")
        Dim c2 As Commit = repo.[Get](Of Tag)("TAG2").Target
        For Each change As Change In c1.CompareAgainst(c2)
            Console.WriteLine(change.ChangeType & ": " & change.Path)
        Next



        '
        '   Dim l_tree As GitSharp.Tree
        '
        '   l_tree = repo.Head.Target
        '
        '   Dim myGit As GitSharp.Git
        '
        '   XAttribute = myGit.GetHashCode
        '
        '   Dim commit1 As GitSharp.Commit
        '
        '   commit1 = repo.Get(
        '
        '
        '
        ' Get("C", "979829389f136bfabb5956c68d909e7bf3092a4e")
        '
        Dim result As String = Nothing
        '
        '   dir = repo.WorkingDirectory
        '
        '   Git.DefaultRepository = path
        '   Git.Status()
        '
        '
        '   'result = repo.ToString

        Return repo.CurrentBranch.CurrentCommit.Parent.Message


    End Function

    Shared Function getTagDiff(ByVal path, ByVal tag1_name, ByVal tag2_name) As String

        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)

        Dim result As String = Nothing

        ' Get the message of the previous commit
        'Dim msg As String = New Commit(repo, "HEAD^").Message
        'Debug.WriteLine(msg = repo.CurrentBranch.CurrentCommit.Parent.Message)


        'Print a list of changes between two commits c1 and c2:
        'Dim c1 As Commit = repo.[Get](Of Commit)("04e4f1fddfd989c368430674baa5efe2e5772585")
        'Dim c1 As Commit = repo.[Get](Of Tag)("TAG1")
        'Dim c1 As Commit = repo.[Get](Of Commit)(repo.[Get](Of Tag)("TAG1").Hash)
        Dim c1 As Commit = repo.[Get](Of Tag)(tag1_name).Target
        ' <-- note: short hashes are not yet supported  
        'Dim c2 As New Commit(repo, "3e663c85995c7d5a5e1543382ebac12da21025d8")
        Dim c2 As Commit = repo.[Get](Of Tag)(tag2_name).Target
        For Each change As Change In c1.CompareAgainst(c2)
            Console.WriteLine(change.ChangeType & ": " & change.Path)
            result = result & Chr(10) & change.ChangeType & ": " & change.Path
        Next
 
        Return result


    End Function

    Shared Function isRepo(ByVal path) As Boolean
        ' Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Return True
    End Function

    Shared Function currentBranch(ByVal path) As String
        Dim repo As GitSharp.Repository = New GitSharp.Repository(path)
        Return repo.CurrentBranch.ToString

    End Function

End Class

