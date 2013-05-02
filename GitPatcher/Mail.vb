Imports System
Imports System.Net.Mail


Public Class Mail

    Shared Sub SendEmail(ByVal i_EmailFrom, ByVal i_EmailTo, ByVal i_MailSubject, ByVal i_MessageBody)
 
        'This is the code that sends out the email based on whatever server information you've set
        'Enclosed within Try-Catch block to catch exceptions in case of errors
        Try
            Dim SmtpServer As New SmtpClient(My.Settings.SMTPhost, My.Settings.SMTPport)
            SmtpServer.Send(i_EmailFrom, i_EmailTo, i_MailSubject, i_MessageBody)
        Catch ex As Exception
            MsgBox("Delivery Failure: " & ex.Source & ex.Message)
        End Try
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
