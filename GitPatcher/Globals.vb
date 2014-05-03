Module Globals

    Private gDB As String
    Private gApex As String
    Private gRepo As String
    Private gApplication As String
    Private gAppCode As String
    'Private gApexApp As String
    Private gParsingSchema As String
    Private gJiraProject As String
    'Private gDBRepoPathMask As String


    Public Function currentDB() As String

        Return gDB

    End Function

    Public Function currentApex() As String

        Return gApex

    End Function
    Public Function currentAppCode() As String

        Return gAppCode

    End Function

    'Public Function currentApexApp() As String
    '
    '    Return gApexApp
    '
    'End Function

    Public Function currentRepo() As String

        Return gRepo

    End Function



    Public Function currentLongBranch() As String
        Return GitSharpFascade.currentBranch(gRepo)
    End Function

    Public Function currentBranch() As String

        Return Common.getLastSegment(Globals.currentLongBranch, "/")

    End Function

    Public Function RootPatchDir() As String


        Return gRepo & My.Settings.PatchDirOffset & "\"

    End Function

    Public Function RootApexDir() As String

        Return gRepo & My.Settings.ApexDirOffset & "\"
    End Function

    Public Function RootDBDir() As String


        Return gRepo & My.Settings.DBDirOffset & "\"

    End Function


    Public Function DBRepoPathMask() As String

        Dim RepoPathMask As String = My.Settings.DBDirOffset.Substring(1) & "/"
        Return RepoPathMask

    End Function

 

    Public Function currentApplication() As String

        Return gApplication

    End Function

    Public Function currentParsingSchema() As String

        Return gParsingSchema

    End Function

    Public Function currentJiraProject() As String

        Return gJiraProject

    End Function



    Public Sub setDB(DB As String)

        gDB = DB

        My.Settings.CurrentDB = gDB
        My.Settings.Save()

    End Sub

    '  Public Sub setDBRepoPathMask(DBRepoPathMask As String)
    '
    '      gDBRepoPathMask = DBRepoPathMask
    '
    '      My.Settings.DBRepoPathMask = gDBRepoPathMask
    '      My.Settings.Save()
    '
    '  End Sub





    'Public Sub setApex(Apex As String)
    '
    '    gApex = Apex
    '
    'End Sub
    Public Sub setRepo(Repo As String)

        gRepo = Repo

        My.Settings.CurrentRepo = gRepo
        My.Settings.Save()

    End Sub


    Public Sub setPatchRunnerFilter(filter As String)

        Logger.Note("SAVE PatchRunnerFilter", filter)
        My.Settings.PatchRunnerFilter = filter
        My.Settings.Save()

        getPatchRunnerFilter()
    End Sub

    Public Function getPatchRunnerFilter() As String
        Logger.Note("My.Settings.PatchRunnerFilter", My.Settings.PatchRunnerFilter)
        Return My.Settings.PatchRunnerFilter

    End Function


    Public Sub setApplication(ByVal Application As String, applicationIndex As Integer)

        gApplication = Application

        'derive when application changes
        gAppCode = Trim(My.Settings.AppCodeList.Split(Chr(10))(applicationIndex)).Replace(Chr(13), "")
        gApex = Trim(My.Settings.AppList.Split(Chr(10))(applicationIndex)).Replace(Chr(13), "")
        gParsingSchema = Trim(My.Settings.ParsingSchemaList.Split(Chr(10))(applicationIndex)).Replace(Chr(13), "")
        gJiraProject = Common.cleanString((My.Settings.JiraProject.Split(Chr(10))(applicationIndex)))

        My.Settings.CurrentApex = gApex
        My.Settings.CurrentApp = gApplication
        My.Settings.Save()

    End Sub

    'Public Sub setParsingSchema(ParsingSchema As String)
    '
    '    gParsingSchema = ParsingSchema
    '
    'End Sub


    Public Function currentConnection() As String
        Dim l_Index As Integer = -1
        For Each db In My.Settings.DBList.Split(Chr(10))
            l_Index = l_Index + 1
            db = Trim(db).Replace(Chr(13), "")
            If db = gDB Then
                Return Trim(My.Settings.ConnectionList.Split(Chr(10))(l_Index)).Replace(Chr(13), "")
            End If
        Next
        Return ""

    End Function

    Public Function currentTNS() As String
        Dim l_Index As Integer = -1
        For Each db In My.Settings.DBList.Split(Chr(10))
            l_Index = l_Index + 1
            db = Trim(db).Replace(Chr(13), "")
            If db = gDB Then
                Return Trim(My.Settings.TNSList.Split(Chr(10))(l_Index)).Replace(Chr(13), "")
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
            db = Trim(db).Replace(Chr(13), "")
            If db = iDb Then
                Try
                    Return Trim(My.Settings.HotFixBranches.Split(Chr(10))(l_Index)).Replace(Chr(13), "")
                Catch e As System.IndexOutOfRangeException
                    Return ""
                End Try

            End If
        Next
        Return ""

    End Function


    Public Function extrasDirCollection() As Collection

        Dim extrasDirCol As New Collection
        Dim l_Index As Integer = -1
        For Each dirname In My.Settings.ExtrasDirList.Split(Chr(10))
            l_Index = l_Index + 1
            dirname = Trim(dirname).Replace(Chr(13), "")
            extrasDirCol.Add(dirname)
        Next
        Return extrasDirCol

    End Function


End Module
