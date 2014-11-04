
Imports System.IO
Imports System.Xml

Public Class AppSettings

    Private repoName As String
    Private repoAppSearch As String

    Public Sub New(ByVal irepoName As String)
        Me.Location = New Point(0, 0)
        repoName = irepoName

        repoAppSearch = "/repos/repo[@RepoName='" & repoName & "']/apps"

        InitializeComponent()
        Me.Text = "Apex App Settings for Repo " & repoName

        ButtonUpdate.Visible = False
        ButtonAdd.Visible = False
        ButtonRemove.Visible = False

    End Sub


    Function checkApp(ByVal iAppName) As Boolean


        Dim l_AppNode As XmlNode



        'Load the Xml file
        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        'Get the list of name nodes 
        ' Dim l_RepoNode As XmlNode = RepoSettings.getRepoNode(RepoSettings.RepoComboBox.Text)
        ' Dim l_OrgNodeList As XmlNodeList = l_RepoNode.SelectNodes("/org")

        Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode(repoAppSearch)
        ' Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/apps")



        'l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_AppNode In l_AppsNode.ChildNodes
            'Get the RepoName Attribute Value

            If l_AppNode.Attributes.GetNamedItem("AppName").Value = iAppName Then

                l_found = True

                'App Code
                AppCodeTextBox.Text = l_AppNode.Attributes.GetNamedItem("AppCode").Value

                'App Id
                AppIdTextBox.Text = l_AppNode.Attributes.GetNamedItem("AppId").Value

                'Jira Issue
                JiraIssueTextBox.Text = l_AppNode.Attributes.GetNamedItem("Jira").Value

                'Parsing Schema
                ParsingSchemaTextBox.Text = l_AppNode.Attributes.GetNamedItem("Schema").Value

                hideUpdateButton()

            End If

        Next

        Return l_found



    End Function



    Sub readApps()

        'Dim l_GitReposXML As XmlDocument = New XmlDocument()

        'Dim l_OrgNodeList As XmlNodeList
        Dim l_AppNode As XmlNode
        'Create the XML Document

        'Load the Xml file
        'l_GitReposXML.Load(Globals.XMLRepoFilePath())

        'Dim l_RepoNode As XmlNode = RepoSettings.getRepoNode(RepoSettings.RepoComboBox.Text)
        ' Dim l_OrgNodeList As XmlNodeList = l_RepoNode.SelectNodes("/orgs")

        'With l_GitReposXML.SelectSingleNode("/repos/repo['" & RepoSettings.RepoComboBox.Text & "']/orgs").CreateNavigator().AppendChild()
        '    .WriteStartElement("org")
        '    .WriteElementString("OrgName", OrgComboBox.Text)
        '    .WriteEndElement()
        '    .Close()
        'End With

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load(Globals.XMLRepoFilePath())


        Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode(repoAppSearch)
        'Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/apps")
        ' Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['Tupperware']/orgs")



        'Get the list of name nodes 
        ' l_OrgNodeList = l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)
        'Loop through the nodes


        Dim l_current_value As String = AppComboBox.Text
        Dim l_found As Boolean = False


        AppComboBox.Items.Clear()
        For Each l_AppNode In l_AppsNode.ChildNodes
            'Get the RepoName Attribute Value
            Dim AppNameAttribute = l_AppNode.Attributes.GetNamedItem("AppName").Value

            AppComboBox.Items.Add(AppNameAttribute)

            If l_current_value = AppNameAttribute Then
                'ReSelect the current item
                AppComboBox.SelectedIndex = AppComboBox.Items.Count - 1
                l_found = True
            End If

        Next

        If AppComboBox.Items.Count > 0 And Not l_found Then
            'Select the first item
            AppComboBox.SelectedIndex = 0
        End If


        'If AppComboBox.Items.Count > 0 Then
        ' AppComboBox.SelectedIndex = 0
        ' End If

        ' l_GitReposXML.Save(Globals.XMLRepoFilePath())


    End Sub


    Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs)
        Main.loadRepos()
    End Sub





    Private Sub DatabaseSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        readApps()
    End Sub


    Private Sub TestOrgValue()
        'Clear all dependant fields

        'Paths
        'RepoPathTextBox.Text = ""

        'ApexOffsetTextBox.Text = ""
        'OJDBCjarFileTextBox.Text = ""

        'DBOffsetTextBox.Text = ""
        'ExtrasDirListTextBox.Text = ""

        'PatchOffsetTextBox.Text = ""
        'PatchExportPathTextBox.Text = ""

        'SQLpathTextBox.DataBindings.Add("Text", My.Settings, "SQLpath")
        'GitExeTextBox.DataBindings.Add("Text", My.Settings, "GITpath")


        'MsgBox(RepoComboBox.Text)
        ButtonRemove.Visible = False
        ButtonAdd.Visible = False
        'Check the Repo exists in the XML doc
        If Not String.IsNullOrEmpty(AppComboBox.Text) Then

            If checkApp(AppComboBox.Text) Then
                'If exists 
                '  - look up dependant fields
                '  - show the "Remove" button
                ButtonRemove.Visible = True

            Else
                'If not exists
                '  - show the "Add" button
                ButtonAdd.Visible = True

            End If
        End If
    End Sub


    Private Sub XMLRepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AppComboBox.SelectedIndexChanged
        TestOrgValue()
    End Sub

    Private Sub RepoComboBox_Leave(sender As Object, e As EventArgs) Handles AppComboBox.Leave

        TestOrgValue()
    End Sub

    Private Sub AddApp()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        'Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo[@RepoName='" & repoName & "']/apps")

        With l_GitReposXML.SelectSingleNode(repoAppSearch).CreateNavigator().AppendChild()
            'With l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/apps").CreateNavigator().AppendChild()
            .WriteStartElement("app")
            'MsgBox(AppComboBox.SelectedText)
            'MsgBox(AppComboBox.SelectedItem)
            'MsgBox(AppComboBox.SelectedValue)
            'MsgBox(AppComboBox.Text)
            'MsgBox(AppComboBox.ToString)
            .WriteAttributeString("AppName", AppComboBox.Text)


            'App Code
            .WriteAttributeString("AppCode", AppCodeTextBox.Text)


            'App Id
            .WriteAttributeString("AppId", AppIdTextBox.Text)

            'Jira Issue
            .WriteAttributeString("Jira", JiraIssueTextBox.Text)

            'Parsing Schema
            .WriteAttributeString("Schema", ParsingSchemaTextBox.Text)

            .WriteEndElement()
            .Close()
        End With


        'Dim l_RepoNode As XmlNode = RepoSettings.getRepoNode(RepoSettings.RepoComboBox.Text)
        'Dim l_OrgNodeList As XmlNodeList = l_RepoNode.SelectNodes("/org")


        '' Dim l_RepoNode As XmlNode = l_GitReposXML.GetElementById(

        'Dim l_NewRepoNode As XmlElement = l_GitReposXML.CreateElement(

        'CreateElement("org")

        'l_NewRepoNode.SetAttribute("OrgName", OrgComboBox.Text)


        ''Prod
        'l_NewRepoNode.SetAttribute("PRODTNS", PRODTNSTextBox.Text)
        'l_NewRepoNode.SetAttribute("PRODCONNECT", PRODCONNECTTextBox.Text)

        ''Uat
        'l_NewRepoNode.SetAttribute("UATTNS", UATTNSTextBox.Text)
        'l_NewRepoNode.SetAttribute("UATCONNECT", UATCONNECTTextBox.Text)


        ''Test
        'l_NewRepoNode.SetAttribute("TESTTNS", TESTTNSTextBox.Text)
        'l_NewRepoNode.SetAttribute("TESTCONNECT", TESTCONNECTTextBox.Text)

        ''Dev
        'l_NewRepoNode.SetAttribute("DEVTNS", DEVTNSTextBox.Text)
        'l_NewRepoNode.SetAttribute("DEVCONNECT", DEVCONNECTTextBox.Text)

        ''VM
        'l_NewRepoNode.SetAttribute("VMTNS", VMTNSTextBox.Text)
        'l_NewRepoNode.SetAttribute("VMCONNECT", VMCONNECTTextBox.Text)


        'l_RepoNode.AppendChild(l_NewRepoNode)

        l_GitReposXML.Save(Globals.XMLRepoFilePath())


        TestOrgValue()
        readApps()
    End Sub




    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click

        AddApp()

    End Sub

    Private Sub RemoveApp()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()

        'Dim l_OrgNodeList As XmlNodeList
        ' Dim l_RepoNode As XmlNode
        'Create the XML Document

        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        ' Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement

        'Get the list of name nodes 
        ' l_OrgNodeList = l_GitReposXML.SelectNodes("/repos/repo/")

        Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode(repoAppSearch)
        'Dim l_AppsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/apps")



        'l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_AppNode In l_AppsNode.ChildNodes
            'Get the RepoName Attribute Value

            If l_AppNode.Attributes.GetNamedItem("AppName").Value = AppComboBox.Text Then

                'Remove this node
                l_AppsNode.RemoveChild(l_AppNode)
            End If


        Next

        l_GitReposXML.Save(Globals.XMLRepoFilePath())


    End Sub


    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        RemoveApp()
        TestOrgValue()
        readApps()
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        RemoveApp()
        AddApp()
        hideUpdateButton()

    End Sub

    Private Sub hideUpdateButton()
        ButtonUpdate.Visible = False
    End Sub

    Private Sub showUpdateButton()
        ButtonUpdate.Visible = True
    End Sub

    Private Sub AppCodeTextBox_TextChanged(sender As Object, e As EventArgs) Handles AppCodeTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub AppIdTextBox_TextChanged(sender As Object, e As EventArgs) Handles AppIdTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub JiraIssueTextBox_TextChanged(sender As Object, e As EventArgs) Handles JiraIssueTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub ParsingSchemaTextBox_TextChanged(sender As Object, e As EventArgs) Handles ParsingSchemaTextBox.TextChanged
        showUpdateButton()
    End Sub



End Class