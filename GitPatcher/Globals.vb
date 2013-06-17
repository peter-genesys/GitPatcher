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

End Module
