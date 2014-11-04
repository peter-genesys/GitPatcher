
Imports System.IO
Imports System.Xml

Public Class RepoSettings


    Function getRepoNode(ByVal iRepoName) As XmlNode

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        Dim l_RepoNodeList As XmlNodeList
        Dim l_RepoNode As XmlNode
        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        'Get the list of name nodes 
        l_RepoNodeList = l_GitReposXML.SelectNodes("/repos/repo")

        Dim l_result As XmlNode

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_RepoNode In l_RepoNodeList
            'Get the RepoName Attribute Value

            If l_RepoNode.Attributes.GetNamedItem("RepoName").Value = iRepoName Then

                l_result = l_RepoNode
  
            End If

        Next

        Return l_result
 
    End Function

 

    Function checkRepo(ByVal iRepoName) As Boolean

        Dim l_GitReposXML As XmlDocument = New XmlDocument()
        Dim l_RepoNodeList As XmlNodeList
        Dim l_RepoNode As XmlNode
        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        'Get the list of name nodes 
        l_RepoNodeList = l_GitReposXML.SelectNodes("/repos/repo")

        Dim l_found As Boolean = False
        'Loop through the nodes

        For Each l_RepoNode In l_RepoNodeList
            'Get the RepoName Attribute Value

            If l_RepoNode.Attributes.GetNamedItem("RepoName").Value = iRepoName Then

                l_found = True
                'Paths
                RepoPathTextBox.Text = l_RepoNode.Attributes.GetNamedItem("RepoPath").Value

                ApexOffsetTextBox.Text = l_RepoNode.Attributes.GetNamedItem("ApexRelPath").Value
                OJDBCjarFileTextBox.Text = l_RepoNode.Attributes.GetNamedItem("ODBCjavaRelPath").Value

                DBOffsetTextBox.Text = l_RepoNode.Attributes.GetNamedItem("DatabaseRelPath").Value
                ExtrasDirListTextBox.Text = l_RepoNode.Attributes.GetNamedItem("ExtrasRelPath").Value

                PatchOffsetTextBox.Text = l_RepoNode.Attributes.GetNamedItem("PatchRelPath").Value
                PatchExportPathTextBox.Text = l_RepoNode.Attributes.GetNamedItem("PatchExportPath").Value

                hideUpdateButton()

            End If

        Next

        Return l_found



    End Function


    Sub readGitRepos()
        Try
            Dim l_GitReposXML As XmlDocument = New XmlDocument()



            Dim l_RepoNodeList As XmlNodeList
            Dim l_RepoNode As XmlNode
            'Create the XML Document

            'Load the Xml file
            l_GitReposXML.Load(Globals.XMLRepoFilePath())

            Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement


            'Get the list of name nodes 
            l_RepoNodeList = l_GitReposXML.SelectNodes("/repos/repo")
            'Loop through the nodes


            Dim l_current_value As String = RepoComboBox.Text
            Dim l_found As Boolean = False
   
            RepoComboBox.Items.Clear()
            For Each l_RepoNode In l_RepoNodeList
                'Get the RepoName Attribute Value
                Dim RepoNameAttribute = l_RepoNode.Attributes.GetNamedItem("RepoName").Value
                'Console.Write(RepoNameAttribute)
                'Get the firstName Element Value
                '  Dim firstNameValue = m_node.ChildNodes.Item(0).InnerText
                'Get the lastName Element Value
                '   Dim lastNameValue = m_node.ChildNodes.Item(1).InnerText
                'Write Result to the Console


                RepoComboBox.Items.Add(RepoNameAttribute)


                If l_current_value = RepoNameAttribute Then
                    'ReSelect the current item
                    RepoComboBox.SelectedIndex = RepoComboBox.Items.Count - 1
                    l_found = True
                End If

            Next

            If RepoComboBox.Items.Count > 0 And Not l_found Then
                'Select the first item
                RepoComboBox.SelectedIndex = 0
            End If

            ' RepoComboBox.SelectedIndex = RepoComboBox.Items.Count - 1

            ' l_GitReposXML.Save(Globals.XMLRepoFilePath())

        Catch errorVariable As Exception
            'Error trapping
            Console.Write(errorVariable.ToString())
        End Try


    End Sub



    Public Sub New()
        InitializeComponent()

        ButtonUpdate.Visible = False
        ButtonAdd.Visible = False
        ButtonRemove.Visible = False
 
    End Sub

 
    Private Sub RepoSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        readGitRepos()
    End Sub


    Private Sub TestRepoValue()
 

        'MsgBox(RepoComboBox.Text)
        ButtonRemove.Visible = False
        ButtonAdd.Visible = False
        'Check the Repo exists in the XML doc
        If Not String.IsNullOrEmpty(RepoComboBox.Text) Then

            If checkRepo(RepoComboBox.Text) Then
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


    Private Sub RepoComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RepoComboBox.SelectedIndexChanged
        TestRepoValue()
    End Sub

    Private Sub RepoComboBox_Leave(sender As Object, e As EventArgs) Handles RepoComboBox.Leave

        TestRepoValue()
    End Sub

    Private Sub AddRepo()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()


        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement

        Dim l_NewRepoNode As XmlElement = l_GitReposXML.CreateElement("repo")

        l_NewRepoNode.SetAttribute("RepoName", RepoComboBox.Text)

        'Paths

        l_NewRepoNode.SetAttribute("RepoPath", RepoPathTextBox.Text)
        l_NewRepoNode.SetAttribute("ApexRelPath", ApexOffsetTextBox.Text)
        l_NewRepoNode.SetAttribute("ODBCjavaRelPath", OJDBCjarFileTextBox.Text)
        l_NewRepoNode.SetAttribute("DatabaseRelPath", DBOffsetTextBox.Text)
        l_NewRepoNode.SetAttribute("ExtrasRelPath", ExtrasDirListTextBox.Text)
        l_NewRepoNode.SetAttribute("PatchRelPath", PatchOffsetTextBox.Text)
        l_NewRepoNode.SetAttribute("PatchExportPath", PatchExportPathTextBox.Text)

        l_NewRepoNode.AppendChild(l_GitReposXML.CreateElement("orgs"))
        l_NewRepoNode.AppendChild(l_GitReposXML.CreateElement("apps"))
  
        l_ReposNode.AppendChild(l_NewRepoNode)

        l_GitReposXML.Save(Globals.XMLRepoFilePath())


        TestRepoValue()
        readGitRepos()
    End Sub




    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click

        AddRepo()

    End Sub

    Private Sub RemoveRepo()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()

        Dim l_RepoNodeList As XmlNodeList
        Dim l_RepoNode As XmlNode
        'Create the XML Document

        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement

        'Get the list of name nodes 
        l_RepoNodeList = l_GitReposXML.SelectNodes("/repos/repo")
        For Each l_RepoNode In l_RepoNodeList
            'Get the RepoName Attribute Value
            If l_RepoNode.Attributes.GetNamedItem("RepoName").Value = RepoComboBox.Text Then
                'Remove this node
                l_ReposNode.RemoveChild(l_RepoNode)
            End If


        Next

        l_GitReposXML.Save(Globals.XMLRepoFilePath())
 
    End Sub


    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        RemoveRepo()
        TestRepoValue()
        readGitRepos()
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        RemoveRepo()
        AddRepo()
        hideUpdateButton()

    End Sub

    Private Sub hideUpdateButton()
        ButtonUpdate.Visible = False
    End Sub

    Private Sub showUpdateButton()
        ButtonUpdate.Visible = True
    End Sub

    Private Sub RepoPathTextBox_TextChangedX(sender As Object, e As EventArgs) Handles RepoPathTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub ApexOffsetTextBox_TextChanged(sender As Object, e As EventArgs) Handles ApexOffsetTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub OJDBCjarFileTextBox_TextChanged(sender As Object, e As EventArgs) Handles OJDBCjarFileTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub DBOffsetTextBox_TextChanged_1(sender As Object, e As EventArgs) Handles DBOffsetTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub ExtrasDirListTextBox_TextChanged(sender As Object, e As EventArgs) Handles ExtrasDirListTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub PatchExportPathTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchExportPathTextBox.TextChanged
        showUpdateButton()
    End Sub

    Private Sub PatchOffsetTextBox_TextChanged(sender As Object, e As EventArgs) Handles PatchOffsetTextBox.TextChanged
        showUpdateButton()
    End Sub


    


    'Private Sub RepoListTextBox_TextChanged(sender As Object, e As EventArgs) Handles RepoComboBox.TextChanged
    '    Main.loadRepos()
    'End Sub



    Private Sub DBButton_Click(sender As Object, e As EventArgs) Handles DBButton.Click
        'MsgBox(RepoComboBox.SelectedItem)
        Dim theDatabaseSettings As OrgSettings = New OrgSettings(RepoComboBox.SelectedItem)
        'theDatabaseSettings.MdiParent = Me
        theDatabaseSettings.Show()
    End Sub

    Private Sub AppButton_Click(sender As Object, e As EventArgs) Handles AppButton.Click
        'MsgBox(RepoComboBox.SelectedItem)
        Dim theApplicationSettings As AppSettings = New AppSettings(RepoComboBox.SelectedItem)
        theApplicationSettings.Show()
    End Sub
End Class