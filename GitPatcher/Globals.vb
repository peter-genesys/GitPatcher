Module Globals
    Private gDB As String
    Private gApex As String
    Private gRepo As String
    Private gApplication As String
    Private gParsingSchema As String

    Public Function currentDB() As String

        Return gDB

    End Function
    Public Function currentApex() As String

        Return gApex

    End Function
    Public Function currentRepo() As String

        Return gRepo

    End Function

    Public Function currentBranch() As String

        Return Common.getLastSegment(GitSharpFascade.currentBranch(gRepo), "/")

    End Function

 
    Public Function currentApplication() As String

        Return gApplication

    End Function

    Public Function currentParsingSchema() As String

        Return gParsingSchema

    End Function


    Public Sub setDB(DB As String)

        gDB = DB
 
    End Sub

    Public Sub setApex(Apex As String)

        gApex = Apex

    End Sub
    Public Sub setRepo(Repo As String)

        gRepo = Repo

    End Sub
    Public Sub setApplication(Application As String)

        gApplication = Application

    End Sub
 
    Public Sub setParsingSchema(ParsingSchema As String)

        gParsingSchema = ParsingSchema

    End Sub


    Public Function deriveConnection() As String
        Dim l_Index As Integer = -1
        For Each db In My.Settings.DBList.Split(Chr(10))
            l_Index = l_Index + 1
            db = Trim(db)
            db = db.Replace(Chr(13), "")
            If db = gDB Then
                Return My.Settings.ConnectionList.Split(Chr(10))(l_Index)
            End If
        Next
        Return ""

    End Function

    Public Function deriveHotfixBranch(Optional ByVal iDb As String = "") As String
        If String.IsNullOrEmpty(iDb) Then
            iDb = gDB
        End If
        Dim l_Index As Integer = -1
        For Each db In My.Settings.DBList.Split(Chr(10))
            l_Index = l_Index + 1
            db = Trim(db)
            db = db.Replace(Chr(13), "")
            If db = iDb Then
                Return My.Settings.HotFixBranches.Split(Chr(10))(l_Index)
            End If
        Next
        Return ""

    End Function

End Module
