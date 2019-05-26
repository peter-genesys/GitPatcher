Public Class Apex

    Public Shared Sub modCreateApplicationSQL(ByVal i_label As String, ByVal i_buildStatus As String) 'Deprecated, keep code examples


        'Relabel Apex 
        '  open script create_application.sql
        '  read input line at a time until line starting "  p_flow_version=> "
        '  replace this line with " p_flow_version=> " & new_version & " " & today
        '  write rest of file and close it.

        Dim l_create_application_new As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql"
        Dim l_create_application_old As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql.old"
        Dim l_create_application_orig As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql.orig"

        FileIO.deleteFileIfExists(l_create_application_old)
        If Not FileIO.fileExists(l_create_application_orig) Then
            'Backup the create_application.sql file
            My.Computer.FileSystem.CopyFile(l_create_application_new, l_create_application_orig)

        End If
        My.Computer.FileSystem.RenameFile(l_create_application_new, "create_application.sql.old")

        Dim l_old_file As New System.IO.StreamReader(l_create_application_old)
        Dim l_new_file As New System.IO.StreamWriter(l_create_application_new)
        Dim l_line As String = Nothing

        Do
            'For each line
            If l_old_file.EndOfStream Then Exit Do

            l_line = l_old_file.ReadLine()
            If l_line.Contains("p_flow_version") And Not String.IsNullOrEmpty(i_label) Then
                l_line = "  p_flow_version=> '" & i_label & "',"
            End If

            If l_line.Contains("p_build_status") And Not String.IsNullOrEmpty(i_buildStatus) Then
                l_line = "  p_build_status=> '" & i_buildStatus & "',"
            End If

            l_new_file.WriteLine(l_line)

        Loop

        l_old_file.Close()
        l_new_file.Close()


        FileIO.deleteFileIfExists(l_create_application_old)


    End Sub





    Public Shared Sub restoreCreateApplicationSQL() 'Deprecated, keep code examples

        Dim l_create_application_new As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql"
        Dim l_create_application_orig As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql.orig"

        If FileIO.fileExists(l_create_application_orig) Then
            'Restore Backup of create_application.sql file
            FileIO.deleteFileIfExists(l_create_application_new)
            My.Computer.FileSystem.RenameFile(l_create_application_orig, "create_application.sql")
        End If

    End Sub


    Public Shared Sub modInstallSQL() 'Deprecated, keep code examples


        'Change the install.sql
        'For install into ISDEVL we want to skip the reports queries and layouts, to speed up the import.

        Dim l_install_new As String = Globals.RootApexDir & Globals.currentApex & "\install.sql"
        Dim l_install_old As String = Globals.RootApexDir & Globals.currentApex & "\install.sql.old"
        Dim l_install_orig As String = Globals.RootApexDir & Globals.currentApex & "\install.orig"

        FileIO.deleteFileIfExists(l_install_old)
        If Not FileIO.fileExists(l_install_orig) Then
            'Backup the install.sql file
            My.Computer.FileSystem.CopyFile(l_install_new, l_install_orig)

        End If
        My.Computer.FileSystem.RenameFile(l_install_new, "install.sql.old")

        Dim l_old_file As New System.IO.StreamReader(l_install_old)
        Dim l_new_file As New System.IO.StreamWriter(l_install_new)
        Dim l_line As String = Nothing

        Do
            'For each line
            If l_old_file.EndOfStream Then Exit Do

            l_line = l_old_file.ReadLine()
            If l_line.Contains("@application/shared_components/reports/") Then
                l_line = Replace(l_line, "@application/shared_components/reports/", "PROMPT Skipping: ")
            End If

            l_new_file.WriteLine(l_line)

        Loop

        l_old_file.Close()
        l_new_file.Close()


        FileIO.deleteFileIfExists(l_install_old)


    End Sub


    Public Shared Sub restoreInstallSQL() 'Deprecated, keep code examples

        Dim l_install_new As String = Globals.RootApexDir & Globals.currentApex & "\install.sql"
        Dim l_install_orig As String = Globals.RootApexDir & Globals.currentApex & "\install.orig"

        If FileIO.fileExists(l_install_orig) Then
            'Restore Backup of install.sql file
            FileIO.deleteFileIfExists(l_install_new)
            My.Computer.FileSystem.RenameFile(l_install_orig, "install.sql")
        End If

    End Sub




    Public Shared Sub Install1Page(ApexPageName As String, applicationDir As String, iSchema As String) 'Current

        Dim l_page_num As String = ApexPageName.Substring(5, 5)

        'Use Host class to execute with a master script.
        Host.RunMasterScript("prompt Installing " & ApexPageName & " from " & applicationDir &
Environment.NewLine & "@" & Globals.getRunConfigDir & Globals.getOrgCode & "_" & Globals.getDB & ".sql" &
Environment.NewLine & "CONNECT &&" & iSchema & "_user/&&" & iSchema & "_password@" & Globals.getDATASOURCE &
Environment.NewLine & "@init.sql" &
Environment.NewLine & "@set_environment" &
Environment.NewLine & "PROMPT ...Remove page " & l_page_num &
Environment.NewLine & "begin" &
Environment.NewLine & " wwv_flow_api.remove_page (p_flow_id=>wwv_flow.g_flow_id, p_page_id=>" & l_page_num & ");" &
Environment.NewLine & "end;" &
Environment.NewLine & "/" &
Environment.NewLine & "@pages/" & ApexPageName &
Environment.NewLine & "@end_environment.sql" &
Environment.NewLine & "exit;" _
                  , applicationDir)


    End Sub




End Class