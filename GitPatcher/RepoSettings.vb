
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
        Logger.Dbg("RepoSettings.checkRepo")

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

                UsePatchAdminCheckBox.Checked = (l_RepoNode.Attributes.GetNamedItem("UsePatchAdmin").Value = "True")

                hideUpdateButton()

                'Set the Global Values
                Globals.setRepoPath(l_RepoNode.Attributes.GetNamedItem("RepoPath").Value)
                Globals.setApexRelPath(l_RepoNode.Attributes.GetNamedItem("ApexRelPath").Value)
                Globals.setODBCjavaRelPath(l_RepoNode.Attributes.GetNamedItem("ODBCjavaRelPath").Value)
                Globals.setDatabaseRelPath(l_RepoNode.Attributes.GetNamedItem("DatabaseRelPath").Value)
                Globals.setExtrasRelPath(l_RepoNode.Attributes.GetNamedItem("ExtrasRelPath").Value)
                Globals.setPatchRelPath(l_RepoNode.Attributes.GetNamedItem("PatchRelPath").Value)
                Globals.setPatchExportPath(l_RepoNode.Attributes.GetNamedItem("PatchExportPath").Value)
                Globals.setUsePatchAdmin(UsePatchAdminCheckBox.Checked)


            End If

        Next

        Logger.Dbg("DONE-RepoSettings.checkRepo")

        Return l_found



    End Function


    Sub readGitRepos(ByRef aRepoComboBox As Windows.Forms.ComboBox, ByVal currentValue As String)
        Logger.Dbg("RepoSettings.readGitRepos")
        Try
            Dim l_GitReposXML As XmlDocument = New XmlDocument()

            Logger.Note("currentRepo", currentValue)

            Logger.Note("XMLRepoFilePath", Globals.XMLRepoFilePath())

            Dim l_RepoNodeList As XmlNodeList
            Dim l_RepoNode As XmlNode
            'Create the XML Document

            'Load the Xml file
            l_GitReposXML.Load(Globals.XMLRepoFilePath())

            Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement


            'Get the list of name nodes 
            l_RepoNodeList = l_GitReposXML.SelectNodes("/repos/repo")
            'Loop through the nodes


            'Dim l_current_value As String = aRepoComboBox.Text
            Dim l_found As Boolean = False

            aRepoComboBox.Items.Clear()
            For Each l_RepoNode In l_RepoNodeList
                'Get the RepoName Attribute Value
                Dim RepoNameAttribute = l_RepoNode.Attributes.GetNamedItem("RepoName").Value

                Logger.Note("RepoNameAttribute", RepoNameAttribute)
                aRepoComboBox.Items.Add(RepoNameAttribute)


                If currentValue = RepoNameAttribute Then
                    'ReSelect the current item
                    aRepoComboBox.SelectedIndex = aRepoComboBox.Items.Count - 1
                    l_found = True
                End If

            Next

            If aRepoComboBox.Items.Count > 0 And Not l_found Then
                'Select the first item
                aRepoComboBox.SelectedIndex = 0
                l_found = True
            End If

            If Not l_found Then
                Logger.Dbg("No repo set")
                Logger.Note("RepoComboBox.Items.Count", aRepoComboBox.Items.Count)

            End If

        Catch errorVariable As Exception
            'Error trapping
            Logger.ShowError(errorVariable.ToString())
            Console.Write(errorVariable.ToString())
        End Try
        Logger.Dbg("DONE-RepoSettings.readGitRepos")

    End Sub



    Public Sub New()
        InitializeComponent()

        ButtonUpdate.Visible = False
        ButtonAdd.Visible = False
        ButtonRemove.Visible = False
 
    End Sub

 
    Private Sub RepoSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        readGitRepos(RepoComboBox, Globals.getRepoName)
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

    Private Sub setRepoAttributes(iRepo As XmlElement)
        'Paths

        iRepo.SetAttribute("RepoPath", RepoPathTextBox.Text)
        iRepo.SetAttribute("ApexRelPath", ApexOffsetTextBox.Text)
        iRepo.SetAttribute("ODBCjavaRelPath", OJDBCjarFileTextBox.Text)
        iRepo.SetAttribute("DatabaseRelPath", DBOffsetTextBox.Text)
        iRepo.SetAttribute("ExtrasRelPath", ExtrasDirListTextBox.Text)
        iRepo.SetAttribute("PatchRelPath", PatchOffsetTextBox.Text)
        iRepo.SetAttribute("PatchExportPath", PatchExportPathTextBox.Text)
        iRepo.SetAttribute("UsePatchAdmin", UsePatchAdminCheckBox.Checked.ToString)
 
    End Sub

    Private Sub AddRepo()

        Dim l_GitReposXML As XmlDocument = New XmlDocument()


        'Load the Xml file
        l_GitReposXML.Load(Globals.XMLRepoFilePath())

        Dim l_ReposNode As XmlNode = l_GitReposXML.DocumentElement

        Dim l_NewRepoNode As XmlElement = l_GitReposXML.CreateElement("repo")

        l_NewRepoNode.SetAttribute("RepoName", RepoComboBox.Text)

        setRepoAttributes(l_NewRepoNode)

        l_NewRepoNode.AppendChild(l_GitReposXML.CreateElement("orgs"))
        l_NewRepoNode.AppendChild(l_GitReposXML.CreateElement("apps"))

        l_ReposNode.AppendChild(l_NewRepoNode)

        l_GitReposXML.Save(Globals.XMLRepoFilePath())


        TestRepoValue()
        readGitRepos(RepoComboBox, RepoComboBox.Text)
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
        readGitRepos(RepoComboBox, RepoComboBox.Text)
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click

        'Modify the repo level attributes, without touching Databases and Applications
        '1. Load the XML
        '2. Find the repo
        '3. Modify the attributes
        '4. Re-write the XML


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
                'Modify attributes of this node

                setRepoAttributes(l_RepoNode)


            End If


        Next

        l_GitReposXML.Save(Globals.XMLRepoFilePath())
 

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
    Private Sub UsePatchAdminCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles UsePatchAdminCheckBox.CheckedChanged
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