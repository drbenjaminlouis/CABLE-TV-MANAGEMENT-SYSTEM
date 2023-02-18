Imports System.Net
Imports System.Net.Mail
Imports Guna.UI2.WinForms

Module Invoice_Sender
    Public Function Email(ByVal email_to, ByVal email_sub, ByVal invoiceFilePath, ByVal cust_name, ByVal pending_amt)
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
            e_mail.IsBodyHtml = True
            e_mail.Body = "<html><body style='background: url(https://www.linkpicture.com/q/mail_bg.jpg) no-repeat center center fixed; background-size: cover;'><div style='padding: 20px;'><p>Dear " & cust_name & ",</p><p>We hope this email finds you well. We would like to confirm the receipt of your recent payment for your cable TV service. Thank you for choosing us as your service provider.</p><p>Please find attached to this email a copy of your latest bill, which includes a breakdown of your current balance and payment history. As of " & Date.Today & ", your outstanding balance is " & pending_amt & ".</p><p>We kindly request that you continue to make payments on time to ensure uninterrupted service. If you have any questions or concerns, please don't hesitate to reach out to our customer service team.</p><p>Thank you for your prompt payment.</p><p>Best regards,</p><p>BHARATH CABLE TV NETWORK</p></div></body></html>"
            e_mail.Attachments.Add(New Attachment(invoiceFilePath))
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
End Module
