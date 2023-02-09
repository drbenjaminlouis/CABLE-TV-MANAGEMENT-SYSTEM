Imports System.Net
Imports System.Net.Mail
Imports Guna.UI2.WinForms

Module Invoice_Sender
    Public Function Email(ByVal email_to, ByVal email_sub, ByVal email_body, ByVal invoiceFilePath)
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
        Try
            Dim smtp_server As New SmtpClient
            Dim e_mail As New MailMessage
            smtp_server.UseDefaultCredentials = False
            smtp_server.Credentials = New Net.NetworkCredential("MARYMATHACABLETV@gmail.com", "sowsmadxuflomofg")
            smtp_server.Port = 587
            smtp_server.EnableSsl = True
            smtp_server.Host = "smtp.gmail.com"
            e_mail = New MailMessage
            e_mail.From = New MailAddress("MARYMATHACABLETV@gmail.com")
            e_mail.To.Add(email_to)
            e_mail.Subject = email_sub
            e_mail.Body = email_body
            e_mail.Attachments.Add(New Attachment(invoiceFilePath))
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
End Module
