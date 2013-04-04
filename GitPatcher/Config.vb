Public Class Config

    Public Sub New()
        InitializeComponent()
        Repo1TextBox.DataBindings.Add("Text", My.Settings, "Repo1")
        Repo2TextBox.DataBindings.Add("Text", My.Settings, "Repo2")
        Repo3TextBox.DataBindings.Add("Text", My.Settings, "Repo3")
        Repo4TextBox.DataBindings.Add("Text", My.Settings, "Repo4")

    End Sub


 
End Class