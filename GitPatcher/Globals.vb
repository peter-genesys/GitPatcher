Module Globals

    Private gDB As String = My.Settings.CurrentDB

    Private gRepo As String

    Private gParsingSchema As String
    Private gJiraProject As String


    Public Sub setRepo(Repo As String)

        gRepo = Repo

        My.Settings.CurrentRepo = gRepo
        My.Settings.Save()

    End Sub

    Public Function getRepo() As String
        Return gRepo
    End Function


    Private gRepoPath As String

    Public Sub setRepoPath(RepoPath As String)
        gRepoPath = Common.dos_path_trailing_slash(RepoPath)
    End Sub


    Public Function getRepoPath() As String

        Return gRepoPath

    End Function


    Private gApexRelPath As String

    Public Sub setApexRelPath(ApexRelPath As String)
        gApexRelPath = Common.dos_path_trailing_slash(ApexRelPath)
    End Sub

    Public Function getApexRelPath() As String
        Return gApexRelPath
    End Function


    Private gODBCjavaRelPath As String

    Public Sub setODBCjavaRelPath(ODBCjavaRelPath As String)
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
        gDatabaseRelPath = Common.dos_path_trailing_slash(DatabaseRelPath)
    End Sub

    Public Function getDatabaseRelPath() As String
        Return gDatabaseRelPath
    End Function


    Private gExtrasRelPath As String

    Public Sub setExtrasRelPath(ExtrasRelPath As String)
        gExtrasRelPath = Common.dos_path(ExtrasRelPath)
    End Sub

    Public Function getExtrasRelPath() As String
        Return gExtrasRelPath
    End Function

    Private gPatchRelPath As String

    Public Sub setPatchRelPath(PatchRelPath As String)
        gPatchRelPath = Common.dos_path_trailing_slash(PatchRelPath)
    End Sub

    Public Function getPatchRelPath() As String
        Return gPatchRelPath
    End Function


    Private gPatchExportPath As String

    Public Sub setPatchExportPath(PatchExportPath As String)
        gPatchExportPath = Common.dos_path_trailing_slash(PatchExportPath)
    End Sub

    Public Function getPatchExportPath() As String
        Return gPatchExportPath
    End Function


    Private gOrgCode As String

    Public Sub setOrgCode(OrgCode As String)
        gOrgCode = OrgCode
    End Sub

    Public Function getOrgCode() As String
        Return gOrgCode
    End Function


    Private gOrgName As String

    Public Sub setOrgName(OrgName As String)
        gOrgName = OrgName
    End Sub

    Public Function getOrgName() As String
        Return gOrgName
    End Function






    Private gTNS As String

    Public Sub setTNS(TNS As String)
        gTNS = TNS
    End Sub

    Public Function getTNS() As String
        Return gTNS
    End Function

    Private gCONNECT As String

    Public Sub setCONNECT(CONNECT As String)
        gCONNECT = CONNECT
    End Sub

    Public Function getCONNECT() As String
        Return gCONNECT
    End Function


    Private gAppCode As String

    Public Sub setAppCode(AppCode As String)
        gAppCode = AppCode
    End Sub

    Public Function getAppCode() As String
        Return gAppCode
    End Function

    Private gAppId As String

    Public Sub setAppId(AppId As String)
        gAppId = AppId
    End Sub

    Public Function getAppId() As String
        Return gAppId
    End Function


    Private gJira As String

    Public Sub setJira(Jira As String)
        gJira = Jira
    End Sub

    Public Function getJira() As String
        Return gJira
    End Function


    Private gSchema As String

    Public Sub setSchema(Schema As String)
        gSchema = Schema
    End Sub

    Public Function getSchema() As String
        Return gSchema
    End Function

    Private gOrgInFeature As String

    Public Sub setOrgInFeature(OrgInFeature As String)
        gOrgInFeature = OrgInFeature
    End Sub

    Public Function getOrgInFeature() As String
        Return gOrgInFeature
    End Function

    Private gAppInFeature As String

    Public Sub setAppInFeature(AppInFeature As String)
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
        Return GitSharpFascade.currentBranch(gRepoPath)
    End Function

    Public Function currentBranch() As String

        Return Common.getLastSegment(Globals.currentLongBranch, "/")

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
        gUsePatchAdmin = UsePatchAdmin
    End Sub

    Public Function getUsePatchAdmin() As Boolean
        Return gUsePatchAdmin
    End Function






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
            Return gAppCode & "_" & gOrgCode & "/"
        End If

        If gAppInFeature = "Y" Then
            Return gAppCode & "/"
        Else
            Return gOrgCode & "/"
        End If
 

    End Function


    Public Function deriveHotfixBranch(Optional ByVal iDb As String = "") As String
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
