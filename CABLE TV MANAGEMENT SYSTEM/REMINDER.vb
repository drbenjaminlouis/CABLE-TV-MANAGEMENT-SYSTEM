Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Reflection.Metadata
Imports System.Security.Policy
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports CABLE_TV_MANAGEMENT_SYSTEM.Email
Imports Microsoft.VisualBasic.Devices
Imports TheArtOfDevHtmlRenderer.Adapters.Entities

Public Class REMINDER
    Dim messageHtml As String
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
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
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
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
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
                                            "WHERE CRF=@CRF AND CURRENT_YEAR=@YEAR"

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
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
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
                                            "WHERE CRF=@CRF AND CURRENT_YEAR=@YEAR"

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
        Message.Clear()
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

            messageHtml = $"<html>
                                <head>
                                    <style>
                                        #container 
                                        {{
                                            width:100%;
                                            height:600px;
                                            background-color: black;
                                        }}
                                        #header 
                                        {{
                                              display: flex;
                                              flex-direction: column;
                                              align-items: center;
                                              justify-content: center;
                                              background-color: black;
                                              color:white;
                                              border-top-left-radius: 10px;
                                              border-top-right-radius: 10px;
                                              padding: 20px;
                                              font-size: 24px;
                                        }}
                                        #header img 
                                        {{
                                            margin-bottom: 10px;
                                        }}
                                        #header h1  
                                        {{
                                            margin-top:20px;
                                        }}
                                        #message {{
                                            text-align: justify;
                                            background-color: black;
                                            color:white;
                                            border-bottom-left-radius: 10px;
                                            border-bottom-right-radius: 10px;
                                            padding: 20px;
                                        }}
                                        #message p 
                                        {{
                                            margin: 0;
                                            font-size: 16px;
                                        }}
                                       #button 
                                       {{
                                            display: flex;
                                            align-items: center;
                                            justify-content: center;
                                            margin: 20px;
                                       }}
                                        #button a 
                                        {{
                                            display: block;
                                            background-color: black;
                                            color: white;
                                            padding: 10px 20px;
                                            border-radius: 5px;
                                            text-decoration: none;
                                        }}
                                </style>
                            </head>
                        <body>
                            <div id='container'>
                                <div id='header'>
                                    <img src='https://www.linkpicture.com/q/LOGO.gif' alt='Logo' width='100' height='100'><br>
                                    <br>
                                    <h1>BHARATH CABLE NETWORK</h1>
                                </div>
                                <div id='message'>
                                        <p><strong>Dear {name},</strong>,</p>
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
                                    
                                    <a href='mailto:your_email_address@example.com' style='background-color: #f7f7f7; border-radius: 5px; color: #333; display: inline-block; font-size: 16px; font-weight: bold; padding: 10px 20px; text-decoration: none;'>Contact Us</a>" &
    "</div></div></div></div></body></html>"
        End If
    End Sub

    Private Sub COLLECT_BTN_Click(sender As Object, e As EventArgs) Handles COLLECT_BTN.Click
        Email.Email(CUST_EMAIL_TEXTBOX.Text, "Payment Reminder", messageHtml)
    End Sub
    Private Sub ClearAll()
        CUST_NAME_TEXTBOX.Clear()
        CUST_EMAIL_TEXTBOX.Clear()
        PAYMENT_YEAR.SelectedIndex = -1
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        Message.Clear()
        SERVICE_COMBOBOX.SelectedIndex = -1
    End Sub
    Private Sub RESET_BTN_Click(sender As Object, e As EventArgs) Handles RESET_BTN.Click
        ClearAll()
        CUST_CRF_TEXTBOX.Clear()
    End Sub
    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.TextChanged
        ClearAll()
        Message.Clear()
    End Sub
End Class