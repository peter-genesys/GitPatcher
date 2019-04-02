Imports LibGit2Sharp


Module Globals

    Private gFlow As String = My.Settings.Flow
    Private gDB As String = My.Settings.CurrentDB

    Private gRepoName As String

    Private gParsingSchema As String
    Private gJiraProject As String


    Private gCommit1 As Commit
    Private gCommit2 As Commit
    Private gRepo As Repository

    Public Sub setRepo(repoPath As String)
        Logger.Dbg("Globals.setRepo(" & repoPath & ")")
        gRepo = New Repository(repoPath)
    End Sub


    Public Function getRepo() As Repository
        Return gRepo
    End Function

    Public Sub setCommits(commit1 As Commit, commit2 As Commit)

        gCommit1 = commit1
        gCommit2 = commit2

    End Sub

    Public Function getCommit1() As Commit
        Return gCommit1
    End Function

    Public Function getCommit2() As Commit
        Return gCommit2
    End Function

    Public Sub setRepoName(RepoName As String)
        Logger.Dbg("Globals.setRepoName(" & RepoName & ")")
        gRepoName = RepoName

        My.Settings.CurrentRepo = gRepoName
        My.Settings.Save()

    End Sub

    Public Function getRepoName() As String
        Return gRepoName
    End Function


    Private gRepoPath As String

    Public Sub setRepoPath(RepoPath As String)
        Logger.Dbg("Globals.setRepoPath(" & RepoPath & ")")
        setRepo(RepoPath) 'Set the repository (before adding trailing slash
        gRepoPath = Common.dos_path_trailing_slash(RepoPath)

    End Sub


    Public Function getRepoPath() As String

        Return gRepoPath

    End Function


    Private gApexRelPath As String

    Public Sub setApexRelPath(ApexRelPath As String)
        Logger.Dbg("Globals.setApexRelPath(" & ApexRelPath & ")")
        gApexRelPath = Common.dos_path_trailing_slash(ApexRelPath)
    End Sub

    Public Function getApexRelPath() As String
        Return gApexRelPath
    End Function


    Private gODBCjavaRelPath As String

    Public Sub setODBCjavaRelPath(ODBCjavaRelPath As String)
        Logger.Dbg("Globals.setODBCjavaRelPath(" & ODBCjavaRelPath & ")")
        gODBCjavaRelPath = Common.dos_path(ODBCjavaRelPath)
    End Sub

    Public Function getODBCjavaRelPath() As String
        Return gODBCjavaRelPath
    End Function


    Public Function getODBCjavaAbsPath() As String
        Return getRepoPath() & gODBCjavaRelPath
    End Function




    Private gDatabaseRelPath As String

    Public Sub setDatabaseRelPath(DatabaseRelPath As String)
        Logger.Dbg("Globals.setDatabaseRelPath(" & DatabaseRelPath & ")")
        gDatabaseRelPath = Common.dos_path_trailing_slash(DatabaseRelPath)
    End Sub

    Public Function getDatabaseRelPath() As String
        Return gDatabaseRelPath
    End Function


    Private gExtrasRelPath As String

    Public Sub setExtrasRelPath(ExtrasRelPath As String)
        Logger.Dbg("Globals.setExtrasRelPath(" & ExtrasRelPath & ")")
        gExtrasRelPath = Common.dos_path(ExtrasRelPath)
    End Sub

    Public Function getExtrasRelPath() As String
        Return gExtrasRelPath
    End Function

    Private gPatchRelPath As String

    Public Sub setPatchRelPath(PatchRelPath As String)
        Logger.Dbg("Globals.setPatchRelPath(" & PatchRelPath & ")")
        gPatchRelPath = Common.dos_path_trailing_slash(PatchRelPath)
    End Sub

    Public Function getPatchRelPath() As String
        Return gPatchRelPath
    End Function


    Private gPatchExportPath As String

    Public Sub setPatchExportPath(PatchExportPath As String)
        Logger.Dbg("Globals.setPatchExportPath(" & PatchExportPath & ")")
        gPatchExportPath = Common.dos_path_trailing_slash(PatchExportPath)
    End Sub

    Public Function getPatchExportPath() As String
        Return gPatchExportPath
    End Function


    Private gOrgCode As String

    Public Sub setOrgCode(OrgCode As String)
        Logger.Dbg("Globals.setOrgCode(" & OrgCode & ")")
        gOrgCode = OrgCode
    End Sub

    Public Function getOrgCode() As String
        Return gOrgCode
    End Function


    Private gOrgName As String

    Public Sub setOrgName(OrgName As String)
        Logger.Dbg("Globals.setOrgName(" & OrgName & ")")
        gOrgName = OrgName
    End Sub

    Public Function getOrgName() As String
        Return gOrgName
    End Function


    Private gTNS As String

    Public Sub setTNS(TNS As String)
        Logger.Dbg("Globals.setTNS(" & TNS & ")")
        gTNS = TNS
    End Sub

    Public Function getTNS() As String
        Return gTNS
    End Function

    Private gCONNECT As String

    Public Sub setCONNECT(CONNECT As String)
        Logger.Dbg("Globals.setCONNECT(" & CONNECT & ")")
        gCONNECT = CONNECT
    End Sub

    Public Function getCONNECT() As String
        Return gCONNECT
    End Function

    Public Function getDATASOURCE() As String
        'Return Connect details if given, otherwise use the TNS entry name.
        Return If(gCONNECT, gTNS)
    End Function



    Private gAppCode As String

    Public Sub setAppCode(AppCode As String)
        Logger.Dbg("Globals.setAppCode(" & AppCode & ")")
        gAppCode = AppCode
    End Sub

    Public Function getAppCode() As String
        Return gAppCode
    End Function

    Private gAppId As String

    Public Sub setAppId(AppId As String)
        Logger.Dbg("Globals.setAppId(" & AppId & ")")
        gAppId = AppId
    End Sub

    Public Function getAppId() As String
        Return gAppId
    End Function


    Private gJira As String

    Public Sub setJira(Jira As String)
        Logger.Dbg("Globals.setJira(" & Jira & ")")
        gJira = Jira
    End Sub

    Public Function getJira() As String
        Return gJira
    End Function


    Private gSchema As String

    Public Sub setSchema(Schema As String)
        Logger.Dbg("Globals.setSchema(" & Schema & ")")
        gSchema = Schema
    End Sub

    Public Function getSchema() As String
        Return gSchema
    End Function

    Private gOrgInFeature As String

    Public Sub setOrgInFeature(OrgInFeature As String)
        Logger.Dbg("Globals.setOrgInFeature(" & OrgInFeature & ")")
        gOrgInFeature = OrgInFeature
    End Sub

    Public Function getOrgInFeature() As String
        Return gOrgInFeature
    End Function

    Private gAppInFeature As String

    Public Sub setAppInFeature(AppInFeature As String)
        Logger.Dbg("Globals.setAppInFeature(" & AppInFeature & ")")
        gAppInFeature = AppInFeature
    End Sub

    Public Function getAppInFeature() As String
        Return gAppInFeature
    End Function



    Public Function XMLRepoFilePath() As String

        Return My.Settings.XMLRepoFilePath

    End Function

    Public Function getDB() As String

        Return gDB

    End Function

    Public Function currentApex() As String

        Return getAppId()

    End Function
    Public Function currentAppCode() As String

        Return getAppCode()

    End Function




    Public Function currentLongBranch() As String

        Return GitOp.CurrentBranch()
    End Function

    Public Function currentBranch() As String

        Return Common.getLastSegment(Globals.currentLongBranch, "/")

    End Function

    Public Function currentBranchType() As String

        Return Common.getFirstSegment(Globals.currentLongBranch, "/")

    End Function


    Public Function RootPatchDir() As String


        Return gRepoPath & gPatchRelPath

    End Function

    Public Function RootApexDir() As String

        Return gRepoPath & gApexRelPath

    End Function

    Public Function RootDBDir() As String


        Return gRepoPath & gDatabaseRelPath


    End Function


    Public Function DBRepoPathMask() As String

        Return gDatabaseRelPath

    End Function


    Public Function PatchExportDir() As String


        Return gPatchExportPath

    End Function






    Private gAppName As String

    Public Sub setAppName(AppName As String)
        Logger.Dbg("Globals.setAppName(" & AppName & ")")
        gAppName = AppName
    End Sub

    Public Function getAppName() As String
        Return gAppName
    End Function




    Public Function currentParsingSchema() As String

        Return getSchema()

    End Function

    Public Function currentJiraProject() As String

        Return getJira()

    End Function



    Public Sub setDB(DB As String)

        gDB = DB

        My.Settings.CurrentDB = gDB
        My.Settings.Save()

    End Sub


    Private gUsePatchAdmin As Boolean

    Public Sub setUsePatchAdmin(UsePatchAdmin As Boolean)
        Logger.Dbg("Globals.setUsePatchAdmin(" & UsePatchAdmin & ")")
        gUsePatchAdmin = UsePatchAdmin
    End Sub

    Public Function getUseARM() As Boolean
        Return gUsePatchAdmin
    End Function



    Private gAppCollection As Collection

    Public Sub setAppCollection(AppCollection As Collection)
        gAppCollection = AppCollection
    End Sub

    Public Function getAppCollection() As Collection
        Return gAppCollection
    End Function

    Public Sub resetAppCollection()
        gAppCollection = New Collection
    End Sub
    Public Sub appendAppCollection(app As String)
        Logger.Dbg("Globals.appendAppCollection(" & app & ")")
        gAppCollection.Add(app)
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



    Public Function currentConnection() As String

        Return getCONNECT()


    End Function

    Public Function currentTNS() As String

        Return getTNS()


    End Function


    Public Function deriveFeatureCode() As String


        If gAppInFeature = "N" And gOrgInFeature = "N" Then
            Return ""
        End If
        If gAppInFeature = "Y" And gOrgInFeature = "Y" Then
            Return gAppCode & "_" & gOrgCode
        End If

        If gAppInFeature = "Y" Then
            Return gAppCode
        Else
            Return gOrgCode
        End If


    End Function


    Public Function deriveHotfixBranch(Optional ByVal iDb As String = "") As String

        If gFlow = "GitHubFlow" Then
            Return "master"
        End If

        If String.IsNullOrEmpty(iDb) Then
            iDb = gDB
        End If

        If iDb = "PROD" Then
            Return "master"
        ElseIf iDb = "UAT" Then
            Return "uat"
        ElseIf iDb = "TEST" Then
            Return "test"
        ElseIf iDb = "DEV" Then
            Return "develop"
        ElseIf iDb = "VM" Then
            Return ""
        End If

        Return ""

    End Function


    Public Function extrasDirCollection() As Collection

        Dim extrasDirCol As New Collection
        Dim l_Index As Integer = -1
        Dim l_extras As String = getExtrasRelPath()
        For Each dirname In l_extras.Split(";")
            l_Index = l_Index + 1
            'dirname = Trim(dirname).Replace(Chr(13), "")
            extrasDirCol.Add(dirname)
        Next
        Return extrasDirCol

    End Function


End Module
