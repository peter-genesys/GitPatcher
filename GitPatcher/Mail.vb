Imports System
Imports System.Net.Mail


Public Class Mail

    'Shared Sub SendEmail(ByVal i_EmailFrom, ByVal i_EmailTo, ByVal i_MailSubject, ByVal i_MessageBody)
    '
    '    'This is the code that sends out the email based on whatever server information you've set
    '    'Enclosed within Try-Catch block to catch exceptions in case of errors
    '    Try
    '        Dim SmtpServer As New SmtpClient(My.Settings.SMTPhost, My.Settings.SMTPport)
    '        SmtpServer.Send(i_EmailFrom, i_EmailTo, i_MailSubject, i_MessageBody)
    '    Catch ex As Exception
    '        MsgBox("Delivery Failure: " & ex.Source & ex.Message)
    '    End Try
    'End Sub
 

    'GMAIL example
    'Imports System.Net.Mail
    'Public Class Form1
    '    Private Sub Button1_Click(ByVal sender As System.Object, _
    '    ByVal e As System.EventArgs) Handles Button1.Click
    '        Try
    '            Dim SmtpServer As New SmtpClient()
    '            Dim mail As New MailMessage()
    '            SmtpServer.Credentials = New  _
    '        Net.NetworkCredential("username@gmail.com", "password")
    '            SmtpServer.Port = 587
    '            SmtpServer.Host = "smtp.gmail.com"
    '            mail = New MailMessage()
    '            mail.From = New MailAddress("YOURusername@gmail.com")
    '            mail.To.Add("TOADDRESS")
    '            mail.Subject = "Test Mail"
    '            mail.Body = "This is for testing SMTP mail from GMAIL"
    '            SmtpServer.Send(mail)
    '            MsgBox("mail send")
    '        Catch ex As Exception
    '            MsgBox(ex.ToString)
    '        End Try
    '    End Sub
    'End Class


    Shared Sub SendEmail(ByVal i_EmailFrom, ByVal i_EmailTo, ByVal i_MailSubject, ByVal i_MessageBody)

        'This is the code that sends out the email based on whatever server information you've set
        'Enclosed within Try-Catch block to catch exceptions in case of errors
        Try
            Dim SmtpServer As New SmtpClient(My.Settings.SMTPhost, My.Settings.SMTPport)
            SmtpServer.Send(i_EmailFrom, i_EmailTo, i_MailSubject, i_MessageBody)
        Catch ex As Exception
            'MsgBox("Delivery Failure: " & ex.Source & ex.Message)
            MsgBox(ex.ToString)
        End Try

        'Try
        '    Dim SmtpServer As New SmtpClient()
        '    Dim mail As New MailMessage()
        '    'SmtpServer.Credentials = New Net.NetworkCredential("username@gmail.com", "password")
        '    SmtpServer.Port = My.Settings.SMTPport
        '    SmtpServer.Host = My.Settings.SMTPhost
        '    mail = New MailMessage()
        '    mail.From = New MailAddress(i_EmailFrom)
        '    mail.To.Add(i_EmailTo)
        '    mail.Subject = i_MailSubject
        '    mail.Body = i_MessageBody
        '    SmtpServer.Send(mail)
        '    MsgBox("mail sent")
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
 

    End Sub



    Shared Sub SendNotification(ByVal i_MailSubject, ByVal i_MessageBody)



        Dim Sender As String = Environment.UserName & My.Settings.RecipientDomain
        Dim Recipients As String = Nothing


        For Each Recipient In My.Settings.RecipientList.Split(Chr(10))
            Recipient = Trim(Recipient)
            Recipient = Recipient.Replace(Chr(13), "")
            If (Recipient.Length > 0) Then
 
                If Not Recipient.Contains("@") Then
                    Recipient = Recipient & My.Settings.RecipientDomain
                End If

                If String.IsNullOrEmpty(Recipients) Then
                    Recipients = Recipient
                Else
                    Recipients = Recipients & "," & Recipient
                End If
            End If

        Next
 
        Logger.Dbg(Sender)

        Logger.Dbg(Recipients)

        Logger.Dbg(i_MailSubject)

        Logger.Dbg(i_MessageBody)

        SendEmail(Sender, Recipients, i_MailSubject, i_MessageBody)

    End Sub


End Class
