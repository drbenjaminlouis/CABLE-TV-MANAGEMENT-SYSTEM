Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Public Class REMINDER
    Dim messageHtml As String
    Private Sub REMINDER_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Simulate a button click
            SEND_BTN.PerformClick()
        End If
    End Sub
    Private Sub REMINDER_LOAD(SENDER As Object, e As EventArgs) Handles MyBase.Load
        Payment_Sync.Payment_Sync()
        Dim currentYear As Integer = DateTime.Now.Year
        PAYMENT_YEAR.Items.Add(currentYear)
        PAYMENT_YEAR.Items.Add(currentYear - 1)
    End Sub
    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        If CUST_CRF_TEXTBOX.Text = "" Then
        Else
            Try
                Using con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                    con.Open()
                    Dim sqlCheck As String = "SELECT * FROM [CUSTOMER_DETAILS] WHERE [CRF] =@CRF"
                    Dim sqlFetch As String = "SELECT CUST_NAME,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF=@CRF"
                    Dim sqlService As String = "SELECT * FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF AND CUST_TV_CONNECTION=@STATUS"
                    Dim sqlService2 As String = "SELECT * FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF AND BROADBAND_CONNECTION=@STATUS"
                    Using cmdCheck As New OleDbCommand(sqlCheck, con)
                        ' Add parameters to the command
                        cmdCheck.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        ' Execute the check query and retrieve the result
                        Dim reader As OleDbDataReader = cmdCheck.ExecuteReader()
                        If reader.HasRows Then
                            ' User name and old password match, proceed with update
                            reader.Close()
                        Else
                            MessageBox.Show("Enter Correct CRF Number", "ALERT")
                        End If
                    End Using
                    Using cmdfetch As New OleDbCommand(sqlFetch, con)
                        cmdfetch.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        Dim reader As OleDbDataReader = cmdfetch.ExecuteReader()
                        If reader.HasRows Then
                            ' Retrieve the data
                            While reader.Read()
                                ' Update the textboxes with the data
                                CUST_NAME_TEXTBOX.Text = reader.GetString(0)
                                CUST_EMAIL_TEXTBOX.Text = reader.GetValue(1)
                            End While
                        End If
                        ' Close the reader
                        reader.Close()
                    End Using
                    Using cmdService As New OleDbCommand(sqlService, con)
                        cmdService.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmdService.Parameters.AddWithValue("@STATUS", "YES")
                        Dim reader As OleDbDataReader = cmdService.ExecuteReader()
                        If reader.HasRows Then
                            SERVICE_COMBOBOX.Items.Clear()
                            SERVICE_COMBOBOX.Items.Add("CABLE TV")
                            reader.Close()
                        End If
                    End Using
                    Using cmdService2 As New OleDbCommand(sqlService2, con)
                        cmdService2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmdService2.Parameters.AddWithValue("@STATUS", "YES")
                        Dim reader As OleDbDataReader = cmdService2.ExecuteReader()
                        If reader.HasRows Then
                            SERVICE_COMBOBOX.Items.Add("BROADBAND")
                            reader.Close()
                        End If
                    End Using
                    con.Close()
                End Using
            Catch ex As Exception

            End Try
        End If

    End Sub
    Private Sub updatepending()
        If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
            If PAYMENT_YEAR.SelectedItem = Nothing Then
                MessageBox.Show("Please Select Year", "ALERT")
            Else
                Using con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                    con.Open()
                    Dim query As String = "SELECT IIF([january]='Not Paid',1,0) AS january, " &
                                           "IIF([february]='Not Paid',1,0) AS february, " &
                                           "IIF([march]='Not Paid',1,0) AS march, " &
                                           "IIF([april]='Not Paid',1,0) AS april, " &
                                           "IIF([may]='Not Paid',1,0) AS may, " &
                                           "IIF([june]='Not Paid',1,0) AS june, " &
                                           "IIF([july]='Not Paid',1,0) AS july, " &
                                           "IIF([august]='Not Paid',1,0) AS august, " &
                                           "IIF([september]='Not Paid',1,0) AS september, " &
                                           "IIF([october]='Not Paid',1,0) AS october, " &
                                            "IIF([november]='Not Paid',1,0) AS november, " &
                                            "IIF([december]='Not Paid',1,0) AS december " &
                                            "FROM TV_PAYMENT_DETAILS " &
                                            "WHERE CRF=@CRF AND PAYMENT_YEAR=@YEAR"

                    Using command As New OleDbCommand(query, con)
                        command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        command.Parameters.AddWithValue("@YEAR", PAYMENT_YEAR.SelectedItem)
                        Dim reader As OleDbDataReader = command.ExecuteReader()
                        Dim pendingPayments As Integer = 0

                        If reader.HasRows Then
                            ' Read the first row
                            reader.Read()

                            ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid
                            If reader("january") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("JANUARY")
                                pendingPayments += 250
                            End If
                            If reader("february") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("FEBRUARY")
                                pendingPayments += 250
                            End If
                            If reader("march") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("MARCH")
                                pendingPayments += 250
                            End If
                            If reader("april") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("APRIL")
                                pendingPayments += 250
                            End If
                            If reader("may") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("MAY")
                                pendingPayments += 250
                            End If
                            If reader("june") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("JUNE")
                                pendingPayments += 250
                            End If
                            If reader("july") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("JULY")
                                pendingPayments += 250
                            End If
                            If reader("august") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("AUGUST")
                                pendingPayments += 250
                            End If
                            If reader("september") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("SEPTEMBER")
                                pendingPayments += 250
                            End If
                            If reader("october") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("OCTOBER")
                                pendingPayments += 250
                            End If
                            If reader("november") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("NOVEMBER")
                                pendingPayments += 250
                            End If
                            If reader("december") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("DECEMBER")
                                pendingPayments += 250
                            End If
                        End If
                        CUST_PENDING_AMOUNT_TEXTBOX.Text = pendingPayments
                    End Using
                End Using
            End If
        End If
        If SERVICE_COMBOBOX.SelectedItem = "BROADBAND" Then
            If PAYMENT_YEAR.SelectedItem = Nothing Then
                MessageBox.Show("Please Select Year", "ALERT")
            Else
                Using con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                    con.Open()
                    Dim query As String = "SELECT IIF([january]='Not Paid',1,0) AS january, " &
                                           "IIF([february]='Not Paid',1,0) AS february, " &
                                           "IIF([march]='Not Paid',1,0) AS march, " &
                                           "IIF([april]='Not Paid',1,0) AS april, " &
                                           "IIF([may]='Not Paid',1,0) AS may, " &
                                           "IIF([june]='Not Paid',1,0) AS june, " &
                                           "IIF([july]='Not Paid',1,0) AS july, " &
                                           "IIF([august]='Not Paid',1,0) AS august, " &
                                           "IIF([september]='Not Paid',1,0) AS september, " &
                                           "IIF([october]='Not Paid',1,0) AS october, " &
                                            "IIF([november]='Not Paid',1,0) AS november, " &
                                            "IIF([december]='Not Paid',1,0) AS december " &
                                            "FROM BROADBAND_PAYMENT_DETAILS " &
                                            "WHERE CRF=@CRF AND PAYMENT_YEAR=@YEAR"

                    Using command As New OleDbCommand(query, con)
                        command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        command.Parameters.AddWithValue("@YEAR", PAYMENT_YEAR.SelectedItem)
                        Dim reader As OleDbDataReader = command.ExecuteReader()
                        Dim pendingPayments As Integer = 0

                        If reader.HasRows Then
                            ' Read the first row
                            reader.Read()

                            ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid
                            If reader("january") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("JANUARY")
                                pendingPayments += 250
                            End If
                            If reader("february") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("FEBRUARY")
                                pendingPayments += 250
                            End If
                            If reader("march") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("MARCH")
                                pendingPayments += 250
                            End If
                            If reader("april") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("APRIL")
                                pendingPayments += 250
                            End If
                            If reader("may") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("MAY")
                                pendingPayments += 250
                            End If
                            If reader("june") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("JUNE")
                                pendingPayments += 250
                            End If
                            If reader("july") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("JULY")
                                pendingPayments += 250
                            End If
                            If reader("august") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("AUGUST")
                                pendingPayments += 250
                            End If
                            If reader("september") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("SEPTEMBER")
                                pendingPayments += 250
                            End If
                            If reader("october") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("OCTOBER")
                                pendingPayments += 250
                            End If
                            If reader("november") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("NOVEMBER")
                                pendingPayments += 250
                            End If
                            If reader("december") = 1 Then
                                PAYMENT_MONTH_LISTBOX.Items.Add("DECEMBER")
                                pendingPayments += 250
                            End If
                        End If
                        CUST_PENDING_AMOUNT_TEXTBOX.Text = pendingPayments
                    End Using
                End Using
            End If
        End If
    End Sub
    Private Sub service_combobox2(sender As Object, e As EventArgs) Handles SERVICE_COMBOBOX.MouseClick
        If PAYMENT_YEAR.SelectedIndex = -1 Then
            Message.Clear()
            MessageBox.Show("Please Select Year", "ALERT")
        End If
    End Sub
    Private Sub SERVICE_COMBOBOX_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles SERVICE_COMBOBOX.SelectedIndexChanged
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        MessageText.Clear()
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        updatepending()
        If Not CUST_PENDING_AMOUNT_TEXTBOX.Text = "0" Then
            Dim name As String = CUST_NAME_TEXTBOX.Text
            Dim amount As String = CUST_PENDING_AMOUNT_TEXTBOX.Text
            Dim service As String = SERVICE_COMBOBOX.SelectedItem
            Dim yourName As String = "MARY MATHA CABLE TV NETWORK"
            Dim pendingMonths As New List(Of String)
            For Each item As Object In PAYMENT_MONTH_LISTBOX.Items
                pendingMonths.Add(item.ToString())
            Next
            MessageText.Padding = New Padding(10)
            Dim message As String = "Dear " & name & "," & vbCrLf & vbCrLf &
                            "I hope this email finds you well. This is a gentle reminder that your payment for " & service & " is pending. The due amount is " & amount & "." & vbCrLf
            If pendingMonths.Count > 1 Then
                message &= "Please note that your payments for the following months are pending: " & String.Join(", ", pendingMonths) & vbCrLf & vbCrLf &
                    "We kindly request that you make the payment for these pending months as soon as possible to avoid any late fees or service interruptions. If you are unable to make the payment in full at this time, please contact us to discuss payment options." & vbCrLf
            ElseIf pendingMonths.Count = 1 Then
                message &= "Please note that your payment for the following month is pending: " & pendingMonths(0) & vbCrLf & vbCrLf &
                    "We kindly request that you make the payment for this pending month as soon as possible to avoid any late fees or service interruptions." & vbCrLf &
                    "If you are unable to make the payment in full at this time, please contact us to discuss payment options." & vbCrLf & vbCrLf
            End If
            message &= vbCrLf & "If you have already made the payment, please ignore this email. If you need assistance with your payment or have any questions, please do not hesitate to contact us." & vbCrLf & vbCrLf &
                "We value your business and appreciate your timely attention to this matter." & vbCrLf & "Thank you for choosing our services."
            MessageText.Text = message

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
                                            max-width: 150px;
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
                                        <pid='cust_name'><strong>Dear {name},</strong></p>
                                        <p>I hope this email finds you well. This is a gentle reminder that your payment for {service} is pending. The due amount is {amount}.</p>"
            If pendingMonths.Count > 1 Then
                messageHtml &= "<p>Please note that your payments for the following months are pending: " & String.Join(", ", pendingMonths) & ".</p>
                    <p>We kindly request that you make the payment for these pending months as soon as possible to avoid any late fees or service interruptions. If you are unable to make the payment in full at this time, please contact us to discuss payment options.</p>"
            End If
            If pendingMonths.Count = 1 Then
                messageHtml &= "<p>Please note that your payment for the following month is pending: " & String.Join(", ", pendingMonths) & ".</p>
                    <p>We kindly request that you make the payment for this pending month as soon as possible to avoid any late fees or service interruptions.<br> 
                       If you are unable to make the payment in full at this time, please contact us to discuss payment options.</p>"
            End If
            messageHtml &= "<p>If you have already made the payment, please ignore this email. If you need assistance with your payment or have any questions, please do not hesitate to contact us.</p>
                <p>We value your business and appreciate your timely attention to this matter. Thank you for choosing our services.</p>
                                    
            <button class='button'><a href='mailto:marymathacabletv@gmail.com' style='text-decoration: none; color: white;'>CONTACT US</a></button>
		<div class='footer'>
            <p style='color: grey; text-align: center; font-size:12px'>**Please do not reply to this mail as it is an auto generated mail**</p>
			<img src='https://content3.jdmagicbox.com/comp/lucknow/w4/0522px522.x522.180411092220.d3w4/catalogue/maurya-cable-network-lda-colony-lucknow-cable-tv-operators-44iv1qoz5l.jpg?clr=3e3328' alt='Footer Image'>
		</div></div></div></div></div></body></html>"
        End If
    End Sub

    Private Sub SEND_BTN_Click(sender As Object, e As EventArgs) Handles SEND_BTN.Click
        If Not CUST_CRF_LABEL.Text = "" And Not CUST_NAME_TEXTBOX.Text = "" And Not CUST_EMAIL_TEXTBOX.Text = "" And Not SERVICE_COMBOBOX.SelectedItem = "" And Not CUST_PENDING_AMOUNT_TEXTBOX.Text = "" Then
            Email.Email(CUST_EMAIL_TEXTBOX.Text, "Payment Reminder", messageHtml)
        Else
            MessageBox.Show("Please Enter All The Details.", "ALERT")
        End If

    End Sub
    Private Sub ClearAll()
        CUST_NAME_TEXTBOX.Clear()
        CUST_EMAIL_TEXTBOX.Clear()
        PAYMENT_YEAR.SelectedIndex = -1
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        MessageText.Clear()
        SERVICE_COMBOBOX.SelectedIndex = -1
    End Sub
    Private Sub RESET_BTN_Click(sender As Object, e As EventArgs) Handles RESET_BTN.Click
        ClearAll()
        CUST_CRF_TEXTBOX.Clear()
    End Sub
    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.TextChanged
        ClearAll()
        MessageText.Clear()
    End Sub
    Private Sub CUST_EMAIL_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_EMAIL_TEXTBOX.Leave
        If CUST_EMAIL_TEXTBOX.Text = "" Then
        Else
            Dim emailRegex As New Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            If Not emailRegex.IsMatch(CUST_EMAIL_TEXTBOX.Text) Then
                MessageBox.Show("Invalid email address. Please enter a valid email address.", "ALERT")
                CUST_EMAIL_TEXTBOX.Clear()
            End If
        End If
    End Sub
End Class