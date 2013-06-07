Imports System
Imports System.Net.Mail

Imports Outlook = Microsoft.Office.Interop.Outlook

Public Class Mail

    'Shared Sub SendEmailSMTP(ByVal i_EmailFrom, ByVal i_EmailTo, ByVal i_MailSubject, ByVal i_MessageBody)
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


    Shared Sub SendEmailSMTP(ByVal i_EmailFrom, ByVal i_EmailTo, ByVal i_MailSubject, ByVal i_MessageBody)

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




    ' Private Sub CreateSendItem(OutlookApp As Outlook._Application)
    '     Dim mail As Outlook.MailItem = Nothing
    '     Dim mailRecipients As Outlook.Recipients = Nothing
    '     Dim mailRecipient As Outlook.Recipient = Nothing
    '     Try
    '         mail = OutlookApp.CreateItem(Outlook.OlItemType.olMailItem)
    '         mail.Subject = "A programatically generated e-mail"
    '         mailRecipients = mail.Recipients
    '         mailRecipient = mailRecipients.Add("Eugene Astafiev")
    '         mailRecipient.Resolve()
    '         If (mailRecipient.Resolved) Then
    '             mail.Send()
    '         Else
    '             System.Windows.Forms.MessageBox.Show(
    '                 "There is no such record in your address book.")
    '         End If
    '     Catch ex As Exception
    '         System.Windows.Forms.MessageBox.Show(ex.Message,
    '             "An exception is occured in the code of add-in.")
    '     Finally
    '         If Not IsNothing(mailRecipient) Then Marshal.ReleaseComObject(mailRecipient)
    '         If Not IsNothing(mailRecipients) Then Marshal.ReleaseComObject(mailRecipients)
    '         If Not IsNothing(mail) Then Marshal.ReleaseComObject(mail)
    '     End Try
    ' End Sub


    Shared Sub SendEmailOutlook(ByVal i_EmailFrom, ByVal i_EmailTo, ByVal i_MailSubject, ByVal i_MessageBody, ByVal i_attachments)


        Try

            Dim oOutL As New Outlook.Application
            Dim oMail As Outlook.MailItem

            oMail = oOutL.CreateItem(Outlook.OlItemType.olMailItem)
            oMail.UnRead = True
            oMail.To = i_EmailTo
            oMail.Subject = i_MailSubject
            oMail.HTMLBody = True
            oMail.Body = i_MessageBody
            If Not String.IsNullOrEmpty(i_attachments) Then
                For Each l_attachment In i_attachments.split(",")
                    Try
                        oMail.Attachments.Add(l_attachment)
                        oMail.Body = oMail.Body & Chr(10) & "Attached file: " & l_attachment
                    Catch ex As Exception
                        oMail.Body = oMail.Body & Chr(10) & "Failed to attached file: " & l_attachment
                    End Try

                Next

            End If

            oMail.Send()


        Catch ex As Exception

            MsgBox("Delivery Failure: " & ex.Source & ex.Message)

        End Try


    End Sub


    Shared Sub SendNotification(ByVal i_MailSubject, ByVal i_MessageBody, Optional ByVal i_attachments = Nothing)
 
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
                    Recipients = Recipients & ";" & Recipient
                End If
            End If

        Next

        Logger.Dbg(Sender)

        Logger.Dbg(Recipients)

        Logger.Dbg(i_MailSubject)

        Logger.Dbg(i_MessageBody)

        SendEmailOutlook(Sender, Recipients, i_MailSubject, i_MessageBody, i_attachments)

    End Sub


End Class
