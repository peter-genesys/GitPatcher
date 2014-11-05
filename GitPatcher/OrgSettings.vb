
Imports System.IO
Imports System.Xml

Public Class OrgSettings

    Private repoName As String
    Private repoOrgSearch As String

    Public Sub New(ByVal irepoName As String)
        Me.Location = New Point(0, 0)
        repoName = irepoName
        repoOrgSearch = "/repos/repo[@RepoName='" & repoName & "']/orgs"


        InitializeComponent()
        Me.Text = "Database Settings for Repo " & repoName

        ButtonUpdate.Visible = False
        ButtonAdd.Visible = False
        ButtonRemove.Visible = False

    End Sub

    Shared Function retrieveOrg(ByVal iOrgName As String, ByVal ipromo As String, ByVal repoName As String) As Boolean


        Dim l_OrgNode As XmlNode

        'Load the Xml file
        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

 
        Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo[@RepoName='" & repoName & "']/orgs")
  

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_OrgNode In l_OrgsNode.ChildNodes
            'Get the RepoName Attribute Value

            If l_OrgNode.Attributes.GetNamedItem("OrgName").Value = iOrgName Then

                l_found = True

                Globals.setOrgCode(l_OrgNode.Attributes.GetNamedItem("OrgCode").Value)
                Globals.setOrgInFeature(l_OrgNode.Attributes.GetNamedItem("OrgInFeature").Value)
                Globals.setTNS(l_OrgNode.Attributes.GetNamedItem(ipromo & "TNS").Value)
                Globals.setCONNECT(l_OrgNode.Attributes.GetNamedItem(ipromo & "CONNECT").Value)

            End If

        Next

        Return l_found

    End Function



    Function checkOrg(ByVal iOrgName As String) As Boolean


        Dim l_OrgNode As XmlNode
 
        'Load the Xml file
        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load(Globals.XMLRepoFilePath())
 
        Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode(repoOrgSearch)



        'l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_OrgNode In l_OrgsNode.ChildNodes
            'Get the RepoName Attribute Value

            If l_OrgNode.Attributes.GetNamedItem("OrgName").Value = iOrgName Then

                l_found = True

                'OrgCode
                OrgCodeTextBox.Text = l_OrgNode.Attributes.GetNamedItem("OrgCode").Value

                OrgInFeatureCheckBox.Checked = l_OrgNode.Attributes.GetNamedItem("OrgInFeature").Value = "Y"

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



    Shared Sub readOrgs(ByRef anOrgComboBox As Windows.Forms.ComboBox, ByVal currentValue As String, ByVal repoName As String)

        Dim l_OrgNode As XmlNode

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        l_GitReposXML.Load(Globals.XMLRepoFilePath())



        Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode("/repos/repo[@RepoName='" & repoName & "']/orgs")



        'Dim l_current_value As String = OrgComboBox.Text
        Dim l_found As Boolean = False


        'Loop through the nodes
        anOrgComboBox.Items.Clear()
        For Each l_OrgNode In l_OrgsNode.ChildNodes
            'Get the RepoName Attribute Value
            Dim OrgNameAttribute = l_OrgNode.Attributes.GetNamedItem("OrgName").Value

            anOrgComboBox.Items.Add(OrgNameAttribute)

            If currentValue = OrgNameAttribute Then
                'ReSelect the current item
                anOrgComboBox.SelectedIndex = anOrgComboBox.Items.Count - 1
                l_found = True
            End If

        Next

        If anOrgComboBox.Items.Count > 0 And Not l_found Then
            'Select the first item
            anOrgComboBox.SelectedIndex = 0
        End If


    End Sub


    Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs)
        Main.loadRepos()
    End Sub





    Private Sub DatabaseSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        readOrgs(OrgComboBox, OrgComboBox.Text, repoName)
    End Sub


    Private Sub TestOrgValue()
 
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
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        With l_GitReposXML.SelectSingleNode(repoOrgSearch).CreateNavigator().AppendChild()
            .WriteStartElement("org")
            .WriteAttributeString("OrgName", OrgComboBox.Text)

            'OrgCode
            .WriteAttributeString("OrgCode", OrgCodeTextBox.Text)

            'OrgInFeature
            If OrgInFeatureCheckBox.Checked Then
                .WriteAttributeString("OrgInFeature", "Y")
            Else
                .WriteAttributeString("OrgInFeature", "N")
            End If
 
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

        l_GitReposXML.Save(Globals.XMLRepoFilePath())


        TestOrgValue()
        readOrgs(OrgComboBox, OrgComboBox.Text, repoName)
    End Sub




    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click

        AddOrg()

    End Sub

    Private Sub RemoveOrg()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()

        'Dim l_OrgNodeList As XmlNodeList
        ' Dim l_RepoNode As XmlNode
        'Create the XML Document

        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        ' Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement

        'Get the list of name nodes 
        ' l_OrgNodeList = l_GitReposXML.SelectNodes("/repos/repo/")


        Dim l_OrgsNode As XmlNode = l_GitReposXML.SelectSingleNode(repoOrgSearch)



        'l_GitReposXML.SelectNodes("/repos/repo/" & RepoSettings.RepoComboBox.Text)

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_OrgNode In l_OrgsNode.ChildNodes
            'Get the RepoName Attribute Value

            If l_OrgNode.Attributes.GetNamedItem("OrgName").Value = OrgComboBox.Text Then

                'Remove this node
                l_OrgsNode.RemoveChild(l_OrgNode)
            End If


        Next

        l_GitReposXML.Save(Globals.XMLRepoFilePath())


    End Sub


    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        RemoveOrg()
        TestOrgValue()
        readOrgs(OrgComboBox, OrgComboBox.Text, repoName)
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        RemoveOrg()
        AddOrg()
        hideUpdateButton()

    End Sub

    Private Sub hideUpdateButton()
        ButtonUpdate.Visible = False
    End Sub

    Private Sub showUpdateButton()
        ButtonUpdate.Visible = True
    End Sub

    Private Sub OrgCodeTextBox_TextChanged(sender As Object, e As EventArgs) Handles OrgCodeTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub PRODTNSTextBox_TextChanged(sender As Object, e As EventArgs) Handles PRODTNSTextBox.TextChanged
        showUpdateButton()
    End Sub


    Private Sub PRODCONNECTTextBox_TextChanged(sender As Object, e As EventArgs) Handles PRODCONNECTTextBox.TextChanged
        showUpdateButton()
    End Sub
    Private Sub UATTNSTextBox_TextChanged(sender As Object, e As EventArgs) Handles UATTNSTextBox.TextChanged
        showUpdateButton()
    End Sub
    Private Sub UATCONNECTTextBox_TextChanged(sender As Object, e As EventArgs) Handles UATCONNECTTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub TESTTNSTextBox_TextChanged(sender As Object, e As EventArgs) Handles TESTTNSTextBox.TextChanged
        showUpdateButton()
    End Sub
    Private Sub TESTCONNECTTextBox_TextChanged(sender As Object, e As EventArgs) Handles TESTCONNECTTextBox.TextChanged
        showUpdateButton()
    End Sub


    Private Sub DEVTNSTextBox_TextChanged(sender As Object, e As EventArgs) Handles DEVTNSTextBox.TextChanged
        showUpdateButton()
    End Sub
    Private Sub DEVCONNECTTextBox_TextChanged(sender As Object, e As EventArgs) Handles DEVCONNECTTextBox.TextChanged
        showUpdateButton()
    End Sub
        
    Private Sub VMTNSTextBox_TextChanged(sender As Object, e As EventArgs) Handles VMTNSTextBox.TextChanged
        showUpdateButton()
    End Sub
    Private Sub VMCONNECTTextBox_TextChanged(sender As Object, e As EventArgs) Handles VMCONNECTTextBox.TextChanged
        showUpdateButton()
    End Sub
 
 
  
    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub OrgInFeatureCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles OrgInFeatureCheckBox.CheckedChanged
        showUpdateButton()
    End Sub
End Class