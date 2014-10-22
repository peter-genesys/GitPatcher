
Imports System.IO
Imports System.Xml

Public Class DatabaseSettings

    Private repoName As String

    Public Sub New(ByVal irepoName As String)
        Me.Location = New Point(0, 0)
        repoName = irepoName

        InitializeComponent()
        Me.Text = "Database Settings for Repo " & repoName

        ButtonUpdate.Visible = False
        ButtonAdd.Visible = False
        ButtonRemove.Visible = False

    End Sub


    Function checkOrg(ByVal iOrgName) As Boolean

     
        Dim l_OrgNode As XmlNode



        'Load the Xml file
        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")

        'Get the list of name nodes 
        ' Dim l_RepoNode As XmlNode = RepoSettings.getRepoNode(RepoSettings.RepoComboBox.Text)
        ' Dim l_OrgNodeList As XmlNodeList = l_RepoNode.SelectNodes("/org")


        Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/orgs")



        'l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_OrgNode In l_OrgsNode.ChildNodes
            'Get the RepoName Attribute Value

            If l_OrgNode.Attributes.GetNamedItem("OrgName").Value = iOrgName Then

                l_found = True
                'Prod
                PRODTNSTextBox.Text = l_OrgNode.Attributes.GetNamedItem("PRODTNS").Value
                PRODCONNECTTextBox.Text = l_OrgNode.Attributes.GetNamedItem("PRODCONNECT").Value

                'Uat
                UATTNSTextBox.Text = l_OrgNode.Attributes.GetNamedItem("UATTNS").Value
                UATCONNECTTextBox.Text = l_OrgNode.Attributes.GetNamedItem("UATCONNECT").Value

                'Test
                TESTTNSTextBox.Text = l_OrgNode.Attributes.GetNamedItem("TESTTNS").Value
                TESTCONNECTTextBox.Text = l_OrgNode.Attributes.GetNamedItem("TESTCONNECT").Value

                'Dev
                DEVTNSTextBox.Text = l_OrgNode.Attributes.GetNamedItem("DEVTNS").Value
                DEVCONNECTTextBox.Text = l_OrgNode.Attributes.GetNamedItem("DEVCONNECT").Value

                'VM
                VMTNSTextBox.Text = l_OrgNode.Attributes.GetNamedItem("VMTNS").Value
                VMCONNECTTextBox.Text = l_OrgNode.Attributes.GetNamedItem("VMCONNECT").Value

                hideUpdateButton()

            End If

        Next

        Return l_found



    End Function



    Sub readOrgs()

        'Dim l_GitReposXML As XmlDocument = New XmlDocument()

        'Dim l_OrgNodeList As XmlNodeList
        Dim l_OrgNode As XmlNode
        'Create the XML Document

        'Load the Xml file
        'l_GitReposXML.Load("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")

        'Dim l_RepoNode As XmlNode = RepoSettings.getRepoNode(RepoSettings.RepoComboBox.Text)
        ' Dim l_OrgNodeList As XmlNodeList = l_RepoNode.SelectNodes("/orgs")

        'With l_GitReposXML.SelectSingleNode("/repos/repo['" & RepoSettings.RepoComboBox.Text & "']/orgs").CreateNavigator().AppendChild()
        '    .WriteStartElement("org")
        '    .WriteElementString("OrgName", OrgComboBox.Text)
        '    .WriteEndElement()
        '    .Close()
        'End With

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")

  

        Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/orgs")
        ' Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo['Tupperware']/orgs")



        'Get the list of name nodes 
        ' l_OrgNodeList = l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)
        'Loop through the nodes


        OrgComboBox.Items.Clear()
        For Each l_OrgNode In l_OrgsNode.ChildNodes
            'Get the RepoName Attribute Value
            Dim OrgNameAttribute = l_OrgNode.Attributes.GetNamedItem("OrgName").Value

            OrgComboBox.Items.Add(OrgNameAttribute)

        Next

        ' l_GitReposXML.Save("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")


    End Sub


    Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs)
        Main.loadRepos()
    End Sub





    Private Sub DatabaseSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        readOrgs()
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
        If Not String.IsNullOrEmpty(OrgComboBox.Text) Then

            If checkOrg(OrgComboBox.Text) Then
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


    Private Sub XMLRepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OrgComboBox.SelectedIndexChanged
        TestOrgValue()
    End Sub

    Private Sub RepoComboBox_Leave(sender As Object, e As EventArgs) Handles OrgComboBox.Leave

        TestOrgValue()
    End Sub

    Private Sub AddOrg()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")

        With l_GitReposXML.SelectSingleNode("/repos/repo['" & repoName & "']/orgs").CreateNavigator().AppendChild()
            .WriteStartElement("org")
            .WriteAttributeString("OrgName", OrgComboBox.Text)
 
            'Prod
            .WriteAttributeString("PRODTNS", PRODTNSTextBox.Text)
            .WriteAttributeString("PRODCONNECT", PRODCONNECTTextBox.Text)
 

            'Uat
            .WriteAttributeString("UATTNS", UATTNSTextBox.Text)
            .WriteAttributeString("UATCONNECT", UATCONNECTTextBox.Text)
 

            'Test
            .WriteAttributeString("TESTTNS", TESTTNSTextBox.Text)
            .WriteAttributeString("TESTCONNECT", TESTCONNECTTextBox.Text)

        
            'Dev
            .WriteAttributeString("DEVTNS", DEVTNSTextBox.Text)
            .WriteAttributeString("DEVCONNECT", DEVCONNECTTextBox.Text)
 
            'VM
            .WriteAttributeString("VMTNS", VMTNSTextBox.Text)
            .WriteAttributeString("VMCONNECT", VMCONNECTTextBox.Text)
 
  
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

        l_GitReposXML.Save("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")


        TestOrgValue()
        readOrgs()
    End Sub




    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click

        AddOrg()

    End Sub

    Private Sub RemoveRepo()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()

        Dim l_RepoNodeList As XmlNodeList
        Dim l_RepoNode As XmlNode
        'Create the XML Document

        'Load the Xml file
        l_GitReposXML.Load("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")

        Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement

        'Get the list of name nodes 
        l_RepoNodeList = l_GitReposXML.SelectNodes("/repos/repo")
        For Each l_RepoNode In l_RepoNodeList
            'Get the RepoName Attribute Value
            If l_RepoNode.Attributes.GetNamedItem("RepoName").Value = OrgComboBox.Text Then
                'Remove this node
                l_ReposNode.RemoveChild(l_RepoNode)
            End If


        Next

        l_GitReposXML.Save("F:\GitPatcher\GitPatcher\My Project\GitRepos.xml")

        TestOrgValue()
        readOrgs()
    End Sub


    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        RemoveRepo()

    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        RemoveRepo()
        AddOrg()
        hideUpdateButton()

    End Sub

    Private Sub hideUpdateButton()
        ButtonUpdate.Visible = False
    End Sub

    Private Sub showUpdateButton()
        ButtonUpdate.Visible = True
    End Sub

    Private Sub PRODTNSTextBox_TextChanged(sender As Object, e As EventArgs) Handles PRODTNSTextBox.TextChanged
        showUpdateButton()
    End Sub



End Class