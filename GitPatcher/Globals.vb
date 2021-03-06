﻿Imports LibGit2Sharp
Imports System.Text.RegularExpressions

Module Globals

    Private gWaitTime As Integer = My.Settings.WaitTime
    Function waitTime() As Integer
        Return gWaitTime
    End Function

    Private gFlow As String = My.Settings.Flow
    Private gDB As String = My.Settings.CurrentDB


    Private gRepoName As String

    Private gParsingSchema As String
    Private gJiraProject As String


    Private gCommit1 As Commit
    Private gCommit2 As Commit
    Private gRepo As Repository
    Private gRepoConfig As IDictionary
    Private gPromoList As Collection


    Public Function getRunConfigDir() As String
        Return Common.dos_path_trailing_slash(My.Settings.RunConfigDir)
    End Function

    Public Function getGPScriptsDir() As String
        Return Common.dos_path_trailing_slash(My.Settings.GPScriptsDir)
    End Function

    Public Function getRepoScriptsDir() As String
        Return Globals.getRepoPath & "tools\db-spooler\script\"
    End Function

    Public Function getVBoxDir() As String
        Return Common.dos_path_trailing_slash(My.Settings.VBoxDir)
    End Function

    Public Function getPromoList(orgName) As Collection
        'Derive promolevels
        Dim promoList As Collection = New Collection()
        'promoList = OrgSettings.retrieveOrgPromos(OrgComboBox.Text, "PROD|UAT|TEST|DEV|VM", Globals.getRepoName())
        promoList = OrgSettings.retrieveOrgPromos(orgName, "PROD|UAT|TEST|DEV|VM", Globals.getRepoName())
    End Function


    Public Sub setRepo(repoPath As String)
        Logger.Debug("Globals.setRepo(" & repoPath & ")")
        gRepo = New Repository(repoPath)
        gRepoConfig = New Dictionary(Of String, String)

        'Convert the gRepo.Config collection to a dictionary, for ease of searching later.
        For i As Integer = 0 To gRepo.Config.Count - 1
            Try
                gRepoConfig.Add(gRepo.Config(i).Key, gRepo.Config(i).Value)
            Catch ex As Exception
                'Ignore duplicate entries
                Logger.Debug(ex.Message & gRepo.Config(i).Key)
            End Try
        Next

    End Sub


    Public Function getRepo() As Repository
        Return gRepo
    End Function

    Public Function getRepoConfig() As IDictionary
        Return gRepoConfig
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
        Logger.Debug("Globals.setRepoName(" & RepoName & ")")
        gRepoName = RepoName

        My.Settings.CurrentRepo = gRepoName
        My.Settings.Save()

    End Sub

    Public Function getRepoName() As String
        Return gRepoName
    End Function


    Private gRepoPath As String

    Public Sub setRepoPath(RepoPath As String)
        Logger.Debug("Globals.setRepoPath(" & RepoPath & ")")
        setRepo(RepoPath) 'Set the repository (before adding trailing slash
        gRepoPath = Common.dos_path_trailing_slash(RepoPath)

    End Sub


    Public Function getRepoPath() As String

        Return gRepoPath

    End Function


    Private gApexRelPath As String

    Public Sub setApexRelPath(ApexRelPath As String)
        Logger.Debug("Globals.setApexRelPath(" & ApexRelPath & ")")
        gApexRelPath = Common.dos_path_trailing_slash(ApexRelPath)
    End Sub

    Public Function getApexRelPath() As String
        Return gApexRelPath
    End Function


    Private gODBCjavaRelPath As String

    Public Sub setODBCjavaRelPath(ODBCjavaRelPath As String)
        Logger.Debug("Globals.setODBCjavaRelPath(" & ODBCjavaRelPath & ")")
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
        Logger.Debug("Globals.setDatabaseRelPath(" & DatabaseRelPath & ")")
        gDatabaseRelPath = Common.dos_path_trailing_slash(DatabaseRelPath)
    End Sub

    Public Function getDatabaseRelPath() As String
        Return gDatabaseRelPath
    End Function


    Private gExtrasRelPath As String

    Public Sub setExtrasRelPath(ExtrasRelPath As String)
        Logger.Debug("Globals.setExtrasRelPath(" & ExtrasRelPath & ")")
        gExtrasRelPath = Common.dos_path(ExtrasRelPath)
    End Sub

    Public Function getExtrasRelPath() As String
        Return gExtrasRelPath
    End Function

    Private gPatchRelPath As String

    Public Sub setPatchRelPath(PatchRelPath As String)
        Logger.Debug("Globals.setPatchRelPath(" & PatchRelPath & ")")
        gPatchRelPath = Common.dos_path_trailing_slash(PatchRelPath)
    End Sub

    Public Function getPatchRelPath() As String
        Return gPatchRelPath
    End Function


    Private gPatchExportPath As String

    Public Sub setPatchExportPath(PatchExportPath As String)
        Logger.Debug("Globals.setPatchExportPath(" & PatchExportPath & ")")
        gPatchExportPath = Common.dos_path_trailing_slash(PatchExportPath)
    End Sub

    Public Function getPatchExportPath() As String
        Return gPatchExportPath
    End Function


    Private gOrgCode As String

    Public Sub setOrgCode(OrgCode As String)
        Logger.Debug("Globals.setOrgCode(" & OrgCode & ")")
        gOrgCode = OrgCode

    End Sub

    Public Function getOrgCode() As String
        Return gOrgCode
    End Function


    Private gOrgName As String = My.Settings.CurrentOrg

    Public Sub setOrgName(OrgName As String)
        Logger.Debug("Globals.setOrgName(" & OrgName & ")")
        gOrgName = OrgName

        If Not String.IsNullOrEmpty(OrgName) Then


            My.Settings.CurrentOrg = OrgName
            My.Settings.Save()
            gPromoList = OrgSettings.retrieveOrgPromos(OrgName, "PROD|UAT|TEST|DEV|VM", Globals.getRepoName())



        End If

    End Sub

    Public Function getOrgName() As String
        Return gOrgName
    End Function

    Public Function getPromoList() As Collection
        Return gPromoList
    End Function


    Private gTNS As String

    Public Sub setTNS(TNS As String)
        Logger.Debug("Globals.setTNS(" & TNS & ")")
        gTNS = TNS
    End Sub

    Public Function getTNS() As String
        Return gTNS
    End Function

    Private gCONNECT As String

    Public Sub setCONNECT(CONNECT As String)
        Logger.Debug("Globals.setCONNECT(" & CONNECT & ")")
        gCONNECT = CONNECT
    End Sub

    Public Function getCONNECT() As String
        Return gCONNECT
    End Function

    Public Function getDATASOURCE() As String
        'Return Connect details if given, otherwise use the TNS entry name.
        Return If(gCONNECT, gTNS)
    End Function


    Private gARMuser As String

    Public Sub setARMuser(ARMuser As String)
        Logger.Debug("Globals.setARMuser(" & ARMuser & ")")
        gARMuser = ARMuser
    End Sub

    Public Function getARMuser() As String
        Return gARMuser
    End Function


    Private gARMpword As String

    Public Sub setARMpword(ARMpword As String)
        Logger.Debug("Globals.setARMpword(" & ARMpword & ")")
        gARMpword = ARMpword
    End Sub

    Public Function getARMpword() As String
        Return gARMpword
    End Function


    Public Function getARMconnection() As String
        Logger.Debug("Data Source=" & Globals.getDATASOURCE & ";User Id=" & Globals.getARMuser & ";Password=" & Globals.getARMpword & ";")
        Return "Data Source=" & Globals.getDATASOURCE & ";User Id=" & Globals.getARMuser & ";Password=" & Globals.getARMpword & ";"
    End Function


    Private gAppCode As String

    Public Sub setAppCode(AppCode As String)
        Logger.Debug("Globals.setAppCode(" & AppCode & ")")
        gAppCode = AppCode
    End Sub

    Public Function getAppCode() As String
        Return gAppCode
    End Function

    Private gAppId As String

    Public Sub setAppId(AppId As String)
        Logger.Debug("Globals.setAppId(" & AppId & ")")
        gAppId = AppId
    End Sub

    Public Function getAppId() As String
        Return gAppId
    End Function


    Private gJira As String

    Public Sub setJira(Jira As String)
        Logger.Debug("Globals.setJira(" & Jira & ")")
        gJira = Jira
    End Sub

    Public Function getJira() As String
        Return gJira
    End Function


    Private gSchema As String

    Public Sub setSchema(Schema As String)
        Logger.Debug("Globals.setSchema(" & Schema & ")")
        gSchema = Schema
    End Sub

    Public Function getSchema() As String
        Return gSchema
    End Function

    Private gOrgInFeature As String

    Public Sub setOrgInFeature(OrgInFeature As String)
        Logger.Debug("Globals.setOrgInFeature(" & OrgInFeature & ")")
        gOrgInFeature = OrgInFeature
    End Sub

    Public Function getOrgInFeature() As String
        Return gOrgInFeature
    End Function

    Private gAppInFeature As String

    Public Sub setAppInFeature(AppInFeature As String)
        Logger.Debug("Globals.setAppInFeature(" & AppInFeature & ")")
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

        Return GitOp.CurrentFriendlyBranch()
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
        Logger.Debug("Globals.setAppName(" & AppName & ")")
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

        'Read the org settings again whenever DB is changed.
        'This will refresh these values :
        '  TNS 
        '  CONNECT
        '  ARMUSER
        '  ARMPWORD

        OrgSettings.retrieveOrg(Globals.getOrgName, Globals.getDB, Globals.getRepoName)

    End Sub


    Private gUsePatchAdmin As Boolean

    Public Sub setUsePatchAdmin(UsePatchAdmin As Boolean)
        Logger.Debug("Globals.setUsePatchAdmin(" & UsePatchAdmin & ")")
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
        Logger.Debug("Globals.appendAppCollection(" & app & ")")
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

        'If gFlow = "GitHubFlow" Then
        'Return "master"
        'End If

        If String.IsNullOrEmpty(iDb) Then
            iDb = gDB
        End If

        If iDb = "PROD" Then
            Return "release"
        ElseIf iDb = "UAT" Then
            Return "release"
        ElseIf iDb = "TEST" Then
            Return "master"
        ElseIf iDb = "DEV" Then
            Return "master"
        ElseIf iDb = "VM" Then
            Return "master"
        End If

        Return ""

    End Function


    Public Function extrasDirCollection() As Collection

        Dim extrasDirCol As New Collection
        Dim l_Index As Integer = -1
        Dim l_extras As String = Regex.Replace(getExtrasRelPath(), "[;:|,]", ",") 'Use any of these delimiters
        For Each dirname In l_extras.Split(",")
            l_Index = l_Index + 1
            extrasDirCol.Add(dirname)
        Next
        Return extrasDirCol

    End Function

    Public Sub confirmNonVMTarget(ByVal cancellationMessage As String)
        'Confirm run against non-VM target
        If Globals.getDB <> "VM" Then
            Dim result As Integer = MessageBox.Show("Confirm target is " & Globals.getDB &
        Chr(10) & "The current database is " & Globals.getDB & ".", "Confirm Target", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Throw New Exception(cancellationMessage)
            End If
        End If
    End Sub

    Public Sub checkBranchTypeList(ByVal iBranchList As String)

        If Not iBranchList.Contains(Globals.currentBranchType) Then
            Throw New System.Exception("Current Branch " & Globals.currentBranch & " is not of branch type " & iBranchList & Environment.NewLine & Environment.NewLine &
                                       "Please switch to an appropriate branch.")
        End If

    End Sub


    Public Sub checkBranchNameCase()
        Dim lBranchType As String = Globals.currentBranchType
        Dim shortBranch As String = Globals.currentBranch()
        Dim shortBranchUpper As String = shortBranch.ToUpper

        If Not shortBranch.Equals(shortBranchUpper) Then
            Throw New System.Exception("Please rename " & lBranchType & " " & shortBranch & " in uppercase " & shortBranchUpper)
        End If

    End Sub

End Module
