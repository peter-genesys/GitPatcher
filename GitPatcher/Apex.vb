Public Class Apex
 
    Public Shared Sub modCreateApplicationSQL(ByVal i_label As String, ByVal i_buildStatus As String)


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





    Public Shared Sub restoreCreateApplicationSQL()

        Dim l_create_application_new As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql"
        Dim l_create_application_orig As String = Globals.RootApexDir & Globals.currentApex & "\application\create_application.sql.orig"

        If FileIO.fileExists(l_create_application_orig) Then
            'Restore Backup of create_application.sql file
            FileIO.deleteFileIfExists(l_create_application_new)
            My.Computer.FileSystem.RenameFile(l_create_application_orig, "create_application.sql")
        End If

    End Sub


    Public Shared Sub modInstallSQL()


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


    Public Shared Sub restoreInstallSQL()

        Dim l_install_new As String = Globals.RootApexDir & Globals.currentApex & "\install.sql"
        Dim l_install_orig As String = Globals.RootApexDir & Globals.currentApex & "\install.orig"

        If FileIO.fileExists(l_install_orig) Then
            'Restore Backup of install.sql file
            FileIO.deleteFileIfExists(l_install_new)
            My.Computer.FileSystem.RenameFile(l_install_orig, "install.sql")
        End If

    End Sub




    Public Shared Sub Install1Page(ApexPageName As String)

        Dim applicationDir As String = Globals.RootApexDir & Globals.currentApex & "\application\"
        Dim l_page_num As String = ApexPageName.Substring(5, 5)
        Dim script As String = _
            "@init.sql" _
& Chr(10) & "@set_environment" _
& Chr(10) & "PROMPT ...Remove page " & l_page_num & "" _
& Chr(10) & "begin" _
& Chr(10) & " wwv_flow_api.remove_page (p_flow_id=>wwv_flow.g_flow_id, p_page_id=>" & l_page_num & ");" _
& Chr(10) & "end;" _
& Chr(10) & "/" _
& Chr(10) & "@pages/" & ApexPageName & "" _
& Chr(10) & "@end_environment.sql" _
& Chr(10)

        Dim pageInstallScriptName As String = applicationDir & "temp_install_page_" & l_page_num & "_script.sql"

        FileIO.writeFile(pageInstallScriptName, script, True)

        'Import Apex
        Host.executeSQLscriptInteractive(pageInstallScriptName _
                                       , applicationDir _
                                       , Main.get_connect_string(Globals.currentParsingSchema, Globals.currentTNS, Globals.getDATASOURCE))
        'Remove the temp file again
        FileIO.deleteFileIfExists(pageInstallScriptName)

    End Sub




End Class