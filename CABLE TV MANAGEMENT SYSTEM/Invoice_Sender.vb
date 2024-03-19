Imports System.Net
Imports System.Net.Mail
Imports Guna.UI2.WinForms

Module Invoice_Sender
    Public Function Email(ByVal email_to, ByVal email_sub, ByVal invoiceFilePath, ByVal cust_name, ByVal pending_amt, ByVal amount, ByVal service, ByVal payment_mode)
        Dim messageHtml As String
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
        Dim currentDateOnly As Date = DateTime.Today
        Dim formattedDate As String = currentDateOnly.ToString("dd-MM-yyyy")
        Try
            Dim smtp_server As New SmtpClient
            Dim e_mail As New MailMessage
            smtp_server.UseDefaultCredentials = False
            smtp_server.Credentials = New Net.NetworkCredential(smtpID, smtpPass)
            smtp_server.Port = 587
            smtp_server.EnableSsl = True
            smtp_server.Host = "smtp.gmail.com"
            e_mail = New MailMessage
            e_mail.From = New MailAddress("MARYMATHACABLETV@gmail.com")
            e_mail.To.Add(email_to)
            e_mail.Subject = email_sub
            e_mail.IsBodyHtml = True
            messageHtml = $"<html>
                                <head>
                                   <style>
                                        body{{
                                            background-color: black;
                                        }}
                                        #cust_name{{
                                            font-size: 30px;
                                            font-weight: bold;
                                            color: #10af04;
                                            margin-top:  40px;
                                        }}
		                                .container {{
                                             border: 5px solid #10af04;
			                                padding: 30px;
			                                max-width: 700px;
			                                margin: 0 auto;
                                            border-radius:  10px;
		                                }}
		                                .header {{
			                                display: flex;
			                                align-items: center;
			                                justify-content: center;
			                                margin-bottom:  20px;
		                                }}
		                                .header img {{
                                            align-items: center;
                                            max-width: 180px;
			                                height: 150px;
                                            margin-right: 15px;
		                                }}
		                                .title {{
			                               text-align: center;
			                                font-size:  36px;
			                                margin-bottom: 20px;
                                            color: green;
		
                                        }}
                                        .message{{
            
                                        }}
		                                .message p{{
			                                text-align: justify;
			                                margin-bottom: 40px;
                                            color: black;
                                            margin-top: 40px;
                                            font-family:  'Times New Roman', Times, serif;
		                                }}
		                                .button {{
                                            display: block;
			                                margin: 0 auto;
                                            margin-top:  50px;
			                                padding: 10px 20px;
			                                background-color: #4CAF50;
			                                color: #fff;
			                                border: none;
			                                border-radius:  5px;
			                                font-size:  16px;
                                            font-weight: bold;
			                                cursor: pointer;
			                                transition: all 0.3s ease;
		                                }}
		                                .button:hover {{
			                                background-color: #3e8e41;
		                                }}
		                                .footer {{
			                                margin-top:  40px;
		                                }}
		                                .footer img {{
                                            max-width: 100%;
			                                height: auto;
		                                }}
	                                </style>
                            </head>
                        <body>
                            <div id='container'>
                            <div id='header'>
                            <img src='https://www.linkpicture.com/q/360_F_76147505_eXZ7ed7u7ZN3X352MX42B9Q6xabQ0HdU-removebg-preview.png' alt='Logo' width='170px' height='150px' style='display: block; margin: 0 auto;'>
                                    <h1 class='title'>BHARATH CABLE NETWORK</h1>
                                </div>
                                <div id='message'style='color: black;'>
                                        <pid='cust_name'><strong>Dear {cust_name},</strong></p>
                                        <p>This is an auto-generated mail to confirm that we have received your payment for the {service} service you have subscribed to. We are grateful for your prompt payment and would like to assure you of our continued commitment to providing quality services.</p>
                                        <p>As per our records, your payment of {amount} was received on {formattedDate} via {payment_mode}. We have updated your account accordingly, and your {service} services will remain active as long as your account is in good standing.</p>"
            If pending_amt > 0 Then
                messageHtml &= "<p>Please note that you have a due amount of " & pending_amt & " till current month." & "We encourage you to pay the due amount also as soon as possible to avoid any service interuptions.</p>"
            End If
            messageHtml &= "<p>If you have any questions or concerns regarding your account or our services, please feel free to reach out to our customer service team at the below button. We are always ready to assist you.</p>
			                            <p>Thank you again for your timely payment and for choosing our " & service & " services. We look forward to serving you in the future.</p>
                                        <p style='font-weight:bold;'>Please note that the bill of your current payment is attached below.</p>			                            
                                        <p>Best regards,</p>
                                        <p style='font-weight:bold;'>BHARATH CABLE NETWORK</p>
                                        <button class='button'><a href='mailto:marymathacabletv@gmail.com' style='text-decoration: none; color: white;'>CONTACT US</a></button>
		                                <div class='footer'>
                                        <p style='color: grey; text-align: center; font-size:12px'>**Please do not reply to this mail as it is an auto generated mail**</p>
			                            <img src='https://content3.jdmagicbox.com/comp/lucknow/w4/0522px522.x522.180411092220.d3w4/catalogue/maurya-cable-network-lda-colony-lucknow-cable-tv-operators-44iv1qoz5l.jpg?clr=3e3328' alt='Footer Image'>
		                                </div></div></div></div></div></body></html>"
            e_mail.Body = messageHtml
            e_mail.Attachments.Add(New Attachment(invoiceFilePath))
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
End Module
