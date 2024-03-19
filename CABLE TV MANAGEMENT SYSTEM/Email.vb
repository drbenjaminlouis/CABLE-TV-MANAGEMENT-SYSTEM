Imports Guna.UI2.WinForms
Imports System.Net.Mail

Module Email
    Public Function Email(ByVal email_to, ByVal email_sub, ByVal email_body)
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
        Try
            Dim smtp_server As New SmtpClient
            Dim e_mail As New MailMessage
            smtp_server.UseDefaultCredentials = False
            smtp_server.Credentials = New Net.NetworkCredential(smtpID, smtpPass)
            smtp_server.Port = 587
            smtp_server.EnableSsl = True
            smtp_server.Host = "smtp.gmail.com"
            e_mail = New MailMessage
            e_mail.From = New MailAddress("MARYMATHACABLETV@GMAIL.COM")
            e_mail.To.Add(email_to)
            e_mail.Subject = email_sub
            e_mail.IsBodyHtml = True
            e_mail.Body = email_body
            smtp_server.Send(e_mail)
            MessageBox.Show("Payment Reminder Send Sucessfully", "ALERT")
        Catch ex As Exception

            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
    Public Function WelcomeEmail(ByVal email_to, ByVal user_name, ByVal password, ByVal cust_name)
        Dim messageHtml As String
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
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
            e_mail.Subject = "WELCOME TO BHARATH CABLE NETWORK"
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
                                        <p>We are delighted to welcome you to <strong>BHARATH CABLE NETWORK</strong>, and we thank you for choosing us as your preferred service provider. As a valued customer, we are committed to providing you with the best possible viewing experience and customer support.</p>
                                        <p>We are pleased to inform you that your account has been set up, and you can now access our cable TV services with the following login credentials:</p>
                                        <br>
                                        <p>Username: <strong>{user_name}</strong></p>
                                        <p>Password: <strong>{password}</strong></p>
                                        <br>
                                        <p>Please keep these credentials secure and do not share them with anyone. You can log in to your account on our app to manage your subscription, view or your account deatils, and access our previous bills etc.</p>
                                        <p>If you have any questions or concerns regarding your account or our services, please feel free to reach out to our customer service team by clicking the below contact us button. We are always ready to assist you.</p>
                                        <p>Thank you again for choosing our services. We look forward to providing you with a top-notch entertainment experience.</p>		                            
                                        <br>
                                        <p>Best regards,</p>
                                        <p style='font-weight:bold;'>BHARATH CABLE NETWORK</p>
                                        <button class='button'><a href='mailto:marymathacabletv@gmail.com' style='text-decoration: none; color: white;'>CONTACT US</a></button>
		                                <div class='footer'>
                                        <p style='color: grey; text-align: center; font-size:12px'>**Please do not reply to this mail as it is an auto generated mail**</p>
			                            <img src='https://content3.jdmagicbox.com/comp/lucknow/w4/0522px522.x522.180411092220.d3w4/catalogue/maurya-cable-network-lda-colony-lucknow-cable-tv-operators-44iv1qoz5l.jpg?clr=3e3328' alt='Footer Image'>
		                                </div></div></div></div></div></body></html>"
            e_mail.Body = messageHtml
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
    Public Function OTP_Sender(ByVal email_to, ByVal otp, ByVal cust_name)
        Dim messageHtml As String
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
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
            e_mail.Subject = "RESET PASSWORD"
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
                                        <p>We have received your request to change your password for BHARATH CABLE NETWORK. To ensure the security of your account, we require that you enter a one-time password (OTP) before proceeding with the password reset process.</p>
                                        <p>Your OTP is:<strong> {otp}<strong>.</p>                                      
                                        <p>Please enter this code on the password reset page to continue with the process. Please note that this OTP is valid for 5 minutes and will expire after that time.</p>
                                        <p>If you did not request a password reset, please ignore this email and take the necessary steps to secure your account.</p>
                                        <p>If you have any questions or concerns, please contact our customer support team by clicking the below contact us button. We are always ready to assist you.</p>
                                        <p>Thank you for using our services.</p>		                            
                                        <br>
                                        <p>Best regards,</p>
                                        <p style='font-weight:bold;'>BHARATH CABLE NETWORK</p>
                                        <button class='button'><a href='mailto:marymathacabletv@gmail.com' style='text-decoration: none; color: white;'>CONTACT US</a></button>
		                                <div class='footer'>
                                        <p style='color: grey; text-align: center; font-size:12px'>**Please do not reply to this mail as it is an auto generated mail**</p>
			                            <img src='https://content3.jdmagicbox.com/comp/lucknow/w4/0522px522.x522.180411092220.d3w4/catalogue/maurya-cable-network-lda-colony-lucknow-cable-tv-operators-44iv1qoz5l.jpg?clr=3e3328' alt='Footer Image'>
		                                </div></div></div></div></div></body></html>"
            e_mail.Body = messageHtml
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
    Public Function Complaint_Raise(ByVal email_to, ByVal c_id, ByVal cust_name, ByVal c_type)
        Dim messageHtml As String
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
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
            e_mail.Subject = "COMPLAINT" & "[#" & c_id & "] RECEIVED REGARDING " & c_type & " - " & Date.Today
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
                                        <p>We are writing to acknowledge the complaint that you raised regarding the {c_type} of the service provided by our company on {Date.Today.ToString("dd-MM-yyyy")}. Your complaint number is {c_id}. We value your feedback and want to ensure that we address all concerns promptly and efficiently.</p>
                                        <p>Firstly, we want to apologize for any inconvenience caused to you due to the issue you faced. We understand the importance of reliable and uninterrupted services, and we are committed to providing our customers with the best possible experience.</p>                                      
                                        <p>To address your complaint, our team has thoroughly reviewed the details you provided, and we have taken immediate steps to rectify the issue. We have also initiated a thorough investigation to identify the root cause of the problem and prevent similar occurrences in the future.</p>
                                        <p>We assure you that we will do everything in our power to ensure that you receive the quality service that you deserve. If you have any further concerns or questions regarding your complaint number {c_id}, please feel free to reach out to us, and we will be more than happy to assist you.</p>
                                        <p>Once again, we apologize for any inconvenience caused and thank you for bringing this issue to our attention.</p>                            
                                        <br>
                                        <p>Best regards,</p>
                                        <p style='font-weight:bold;'>BHARATH CABLE NETWORK</p>
                                        <button class='button'><a href='mailto:marymathacabletv@gmail.com' style='text-decoration: none; color: white;'>CONTACT US</a></button>
		                                <div class='footer'>
                                        <p style='color: grey; text-align: center; font-size:12px'>**Please do not reply to this mail as it is an auto generated mail**</p>
			                            <img src='https://content3.jdmagicbox.com/comp/lucknow/w4/0522px522.x522.180411092220.d3w4/catalogue/maurya-cable-network-lda-colony-lucknow-cable-tv-operators-44iv1qoz5l.jpg?clr=3e3328' alt='Footer Image'>
		                                </div></div></div></div></div></body></html>"
            e_mail.Body = messageHtml
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
    Public Function Complaint_Resolved(ByVal email_to, ByVal c_id, ByVal cust_name, ByVal c_type)
        Dim messageHtml As String
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
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
            e_mail.Subject = "COMPLAINT" & "[#" & c_id & "] RESOLVED - " & Date.Today
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
                                        <p>We are writing to inform you that the complaint you raised regarding the {c_type} of the service provided by our company has been resolved. Your complaint number was {c_id}.</p>
                                        <p>We apologize for any inconvenience caused to you due to this issue. We understand how important it is to have reliable and uninterrupted services, and we are committed to providing our customers with the best possible experience.</p>
                                        <p>Our team has worked diligently to address your complaint, and we have taken steps to ensure that such issues do not arise again in the future.</p>
                                        <p>We value your feedback, and we appreciate your patience and understanding throughout this process.</p> 
                                        <p>If you have any further concerns or questions, please do not hesitate to reach out to us, and we will be more than happy to assist you.</p>
                                        <br>
                                        <p>Best regards,</p>
                                        <p style='font-weight:bold;'>BHARATH CABLE NETWORK</p>
                                        <button class='button'><a href='mailto:marymathacabletv@gmail.com' style='text-decoration: none; color: white;'>CONTACT US</a></button>
		                                <div class='footer'>
                                        <p style='color: grey; text-align: center; font-size:12px'>**Please do not reply to this mail as it is an auto generated mail**</p>
			                            <img src='https://content3.jdmagicbox.com/comp/lucknow/w4/0522px522.x522.180411092220.d3w4/catalogue/maurya-cable-network-lda-colony-lucknow-cable-tv-operators-44iv1qoz5l.jpg?clr=3e3328' alt='Footer Image'>
		                                </div></div></div></div></div></body></html>"
            e_mail.Body = messageHtml
            smtp_server.Send(e_mail)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ALERT")
        End Try
        Return 0
    End Function
End Module
