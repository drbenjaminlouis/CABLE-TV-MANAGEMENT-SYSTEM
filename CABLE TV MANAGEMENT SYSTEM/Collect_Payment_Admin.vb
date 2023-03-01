Imports System.Data.OleDb
Imports System.Drawing.Printing
Imports System.IO
Imports System.Security.Cryptography
Imports System.Media
Imports System.Security.Policy

Public Class Collect_Payment_Admin


    'variable for selected payment mode
    Dim payment_mode As String
    'generating invoice number
    Dim invoice_no As Integer = generateInvoice()
    'Variable for last_renewal date
    Dim last_renewal_date_tv As Date
    'Variable for last_renewal date  of broadband
    Dim last_renewal_date_broadband As Date
    Dim LOGTYPE As String = LoginType
    Dim cust_crf As Integer
    Private Sub Collect_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'storing current year and previous year to payment_year combobox
        Dim currentYear As Integer = DateTime.Now.Year
        PAYMENT_YEAR.Items.Add(currentYear)
        PAYMENT_YEAR.Items.Add(currentYear - 1)
        If LOGTYPE = "CUSTOMER" Then
            CASH_LABEL.Visible = False
            CASH_RADIO.Visible = False
            CASH_RADIO.Checked = False
            HEADER_LABEL.Text = "MAKE PAYMENT"
            CUST_CRF_TEXTBOX.ReadOnly = True
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            Try
                connection.Open()
                Dim crfpicker As New OleDbCommand("SELECT CRF FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@USERNAME", connection)
                Dim username As String = Module1.UserName
                crfpicker.Parameters.AddWithValue("@USERNAME", username)
                Dim crfreader As OleDbDataReader = crfpicker.ExecuteReader
                If crfreader.HasRows Then
                    While crfreader.Read
                        cust_crf = crfreader.GetInt32(0)
                    End While
                End If
                CUST_CRF_TEXTBOX.Text = cust_crf
                SEARCH_BTN.PerformClick()
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Fetching CRF: " & ex.Message)
                MessageBox.Show("An Error Occured While Fetching CRF: Please Contact Administrator", "ALERT")
            End Try
        Else
            HEADER_LABEL.Text = "COLLECT PAYMENT"
        End If
        QR_RADIO.Checked = True
        QR_CODE.Visible = True
        REFERANCE_NO.Visible = True
        REFERANCE_NO_LABEL.Visible = True
        PRINT_BTN.Visible = False
        INVOICE_NUMBER_TEXTBOX.Text = invoice_no
    End Sub

    'Method For Updating Pending Amounts 
    Private Sub updatepending()
        If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
            If PAYMENT_YEAR.SelectedItem = Nothing Then

                'If Year Not Selected

                MessageBox.Show("Please Select Year", "ALERT")
            Else

                'If Year Is Selected

                CUST_PENDING_AMOUNT_TEXTBOX.Clear()
                Try
                    Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                        con.Open()
                        'Query for selecting months which are not paid 
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
                                reader.Read()

                                ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid and updaing pending amount
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
                            'assigning value pendingpayments to pending amount textbox
                            CUST_PENDING_AMOUNT_TEXTBOX.Text = pendingPayments
                        End Using
                        con.Close()
                    End Using
                Catch ex As Exception
                    'If An Exception Occured, Updating It In Log File And Showing A Message
                    LogError("An Error Occured While Updating Cable TV Pending Payments" & ex.Message)
                    MessageBox.Show("An Error Occured : Check Log For More Details", "ALERT")
                End Try
            End If
        End If
    End Sub

    'Method For Updating Broadband Pending Amount
    Private Sub updatependingbroadband()
        If SERVICE_COMBOBOX.SelectedItem = "BROADBAND" Then
            'if year not selected
            If PAYMENT_YEAR.SelectedItem = Nothing Then
                MessageBox.Show("Please Select Year", "ALERT")
            Else
                'If Year Is Selected
                CUST_PENDING_AMOUNT_TEXTBOX.Clear()
                Try
                    Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                        con.Open()
                        'Query for selecting months which are not paid 
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
                            ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid and update pending amount.
                            If reader.HasRows Then
                                reader.Read()
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
                            'Assigning value pendingpayments to pending amount textbox
                            CUST_PENDING_AMOUNT_TEXTBOX.Text = pendingPayments
                        End Using
                        con.Close()
                    End Using
                Catch ex As Exception
                    'If An Exception Occured, Updating It In Log File And Showing A Message
                    LogError("An Error Occured While Updating Broadband Pending Payments" & ex.Message)
                    MessageBox.Show("An Error Occured : Check Log For More Details", "ALERT")
                End Try
            End If
        End If
    End Sub
    'Method For Generating Invoice With Invoice Number As File Name And Saving To Invoices Folder.
    Public Sub Generate_Invoice()
        Dim printDoc As New PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf PrintPageHandler
        Dim invoiceNumber As String = invoice_no
        Dim invoicesFolder As String = (invoicepath)
        'If Folder Doesn't Exist Creating A New Folder Named Invoices
        If Not Directory.Exists(invoicesFolder) Then
            Directory.CreateDirectory(invoicesFolder)
        End If
        'File Path
        Dim filePath As String = Path.Combine(invoicesFolder, invoiceNumber & ".pdf")
        printDoc.PrinterSettings.PrinterName = "Microsoft Print to PDF"
        printDoc.PrinterSettings.PrintToFile = True
        printDoc.PrinterSettings.PrintFileName = filePath
        printDoc.Print()
    End Sub

    'Configuring The PrintPageHandler
    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        'Declaring New Fonts
        Dim font1 As New Font("Arial Black", 28, FontStyle.Bold)
        Dim font2 As New Font("Arial", 14, FontStyle.Bold)
        'Declaring Text For Header
        Dim text As String = "BHARATH CABLE TV NETWORK"
        Dim text1 As String = "KARIKKATTOOR CENTRE & MAKKAPUZHA"
        Dim text2 As String = "KOTTAYAM, KERALA, 686544"
        Dim text3 As String = "MOB: 6282522127"
        'Measuring Size Of String For Allignment
        Dim textSize As SizeF = e.Graphics.MeasureString(text, font1)
        Dim text1Size As SizeF = e.Graphics.MeasureString(text1, font2)
        Dim text2Size As SizeF = e.Graphics.MeasureString(text2, font2)
        Dim text3Size As SizeF = e.Graphics.MeasureString(text3, font2)
        'Adding Contents To Page
        'Parameters Are (text to be displayed,font,Brush Color,X - Position,Y - Position)
        e.Graphics.DrawString(text, font1, Brushes.Red, (e.PageBounds.Width - textSize.Width) / 2, 10)
        e.Graphics.DrawString(text1, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, (e.PageBounds.Width - text1Size.Width) / 2, textSize.Height)
        e.Graphics.DrawString(text2, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, (e.PageBounds.Width - text2Size.Width) / 2, textSize.Height + text1Size.Height)
        e.Graphics.DrawString(text3, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, (e.PageBounds.Width - text3Size.Width) / 2, textSize.Height + text1Size.Height + text2Size.Height)
        'Drawing Line
        Dim pen As New Pen(Color.Black, 5)
        e.Graphics.DrawLine(pen, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 20, e.PageBounds.Width - 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 20)
        e.Graphics.DrawString("INVOICE NO: " & invoice_no, New Font("Arial Black", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 40)
        e.Graphics.DrawString("DATE: " & Date.Today, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, e.PageBounds.Width - 185, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 40)
        e.Graphics.DrawString("BILL TO", New Font("Arial Black", 14), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 120)
        e.Graphics.DrawString("CUSTOMER NAME          : " & CUST_NAME_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 180)
        e.Graphics.DrawString("HOUSE NAME                  : " & CUST_HOUSENAME_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 210)
        e.Graphics.DrawString("CRF NUMBER                  : " & CUST_CRF_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 240)
        e.Graphics.DrawString("SERVICE                          : " & SERVICE_COMBOBOX.SelectedItem, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 270)
        'Selecting Months
        Dim monthsToUpdate As Integer = CInt(AMOUNT.Text) / 250

        Dim selectedMonths As New List(Of String)
        For i As Integer = 0 To PAYMENT_MONTH_LISTBOX.Items.Count - 1
            If monthsToUpdate > 0 Then
                selectedMonths.Add(PAYMENT_MONTH_LISTBOX.Items(i).ToString())
                monthsToUpdate -= 1
            End If
        Next

        Dim selectedMonthsString As String = String.Join(", ", selectedMonths)
        e.Graphics.DrawString("MONTH                             : " & selectedMonthsString, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 300)
        e.Graphics.DrawString("AMOUNT PAID                 : " & AMOUNT.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 330)
        e.Graphics.DrawString("PENDING AMOUNT         : " & CUST_PENDING_AMOUNT_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 360)
        e.Graphics.DrawLine(pen, 15, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 420, e.PageBounds.Width - 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 420)
        'Adding Seal To Document, X Is X Axis And Y Is Y Axis
        Dim sealImage As Image = Image.FromFile(seal_path)
        Dim x As Integer = e.PageBounds.Width - sealImage.Width + 40
        Dim y As Integer = 520
        'Reducing Size Of Seal
        Dim newWidth As Integer = sealImage.Width - 50
        Dim newHeight As Integer = sealImage.Height - 50
        e.Graphics.DrawImage(sealImage, x, y, newWidth, newHeight)
        e.Graphics.DrawString("SEAL & SIGNATURE", New Font("Arial", 14, FontStyle.Bold), Brushes.Black, e.PageBounds.Width - 230, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 600)
        e.Graphics.DrawString("• PLEASE PAY THE MONTHLY RENTAL ON OR BEFORE 10TH EVERY MONTH.", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 660)
        e.Graphics.DrawString("• DUE FOR MORE THAN 1 MONTH WILL LEAD TO DISCONNECTION WITHOUT ANY PRIOR NOTICE.", New Font("Arial", 12, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 700)
        e.HasMorePages = False
    End Sub
    'Printing Invoice
    Private Sub PRINT_BTN_Click(sender As Object, e As EventArgs)
        Dim printDoc As New PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf PrintPageHandler
        Dim printDialog As New PrintDialog()
        printDialog.Document = printDoc
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDoc.Print()
        End If
    End Sub
    'Method For Clearing All Values
    Public Sub clearAll()
        CUST_NAME_TEXTBOX.Clear()
        CUST_HOUSENAME_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_DISTRICT_TEXTBOX.Clear()
        CUST_STATE_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()
        CUST_EMAIL_TEXTBOX.Clear()
        PAYMENT_YEAR.SelectedIndex = -1
        SERVICE_COMBOBOX.SelectedIndex = -1
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        AMOUNT.Clear()
        REFERANCE_NO.Clear()
    End Sub
    'For Getting Customer Details While Clicking Search BTN Based on CRF Number
    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        'For Clearing All Values
        clearAll()
        'If CRF Not Entered
        If CUST_CRF_TEXTBOX.Text = "" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Enter CRF Number", "ALERT")
        Else
            'If CRF Entered
            Try
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                    con.Open()
                    'Query For Cheking CRF Present Or NOT
                    Dim sqlCheck As String = "SELECT * FROM [CUSTOMER_DETAILS] WHERE [CRF] =@CRF"
                    'Query For Fetching Details Based On CRF Number
                    Dim sqlFetch As String = "SELECT CUST_NAME,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_MOBILE,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF=@CRF"
                    Dim sqlFetch2 As String = "SELECT EXPIRY_DATE FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF AND CUST_TV_CONNECTION=@CUST_TV_CONNECTION"
                    Dim sqlFetch3 As String = "SELECT EXPIRY_DATE FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF AND BROADBAND_CONNECTION=@BROADBAND_CONNECTION"
                    'Qurey For Fetching Connection Details
                    Dim sqlService As String = "SELECT * FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF AND CUST_TV_CONNECTION=@STATUS"
                    Dim sqlService2 As String = "SELECT * FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF AND BROADBAND_CONNECTION=@STATUS"

                    Using cmdCheck As New OleDbCommand(sqlCheck, con)
                        ' Add parameters to the command
                        cmdCheck.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        ' Executing the check query
                        Dim reader As OleDbDataReader = cmdCheck.ExecuteReader()
                        If reader.HasRows Then
                            'If CRF Found
                            reader.Close()
                        Else
                            'IF CRF Not Found
                            MessageBox.Show("Enter Correct CRF Number", "ALERT")
                        End If
                    End Using
                    'Fetching Customer Details And Adding To Respective Fields
                    Using cmdfetch As New OleDbCommand(sqlFetch, con)
                        cmdfetch.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        Dim reader As OleDbDataReader = cmdfetch.ExecuteReader()
                        If reader.HasRows Then
                            While reader.Read()
                                ' Updating the textboxes with the data
                                CUST_NAME_TEXTBOX.Text = reader.GetString(0)
                                CUST_HOUSENAME_TEXTBOX.Text = reader.GetString(1)
                                CUST_AREA_TEXTBOX.Text = reader.GetString(2)
                                CUST_DISTRICT_TEXTBOX.Text = reader.GetString(3)
                                CUST_STATE_TEXTBOX.Text = reader.GetString(4)
                                CUST_MOBILE_TEXTBOX.Text = reader.GetValue(5)
                                CUST_EMAIL_TEXTBOX.Text = reader.GetValue(6)
                            End While
                        End If
                        reader.Close()
                    End Using
                    Using cmdfetch2 As New OleDbCommand(sqlFetch2, con)
                        cmdfetch2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmdfetch2.Parameters.AddWithValue("@CUST_TV_CONNECTION", "YES")
                        Dim reader As OleDbDataReader = cmdfetch2.ExecuteReader
                        If reader.HasRows Then
                            While reader.Read()
                                last_renewal_date_tv = reader.GetDateTime(0)
                            End While
                        End If
                    End Using
                    Using cmdfetch3 As New OleDbCommand(sqlFetch3, con)
                        cmdfetch3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmdfetch3.Parameters.AddWithValue("@BROADBAND_CONNECTION", "YES")
                        Dim reader As OleDbDataReader = cmdfetch3.ExecuteReader
                        If reader.HasRows Then
                            While reader.Read()
                                last_renewal_date_broadband = reader.GetDateTime(0)
                            End While
                        End If
                    End Using
                    'Fetching Service Details
                    Using cmdService As New OleDbCommand(sqlService, con)
                        cmdService.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmdService.Parameters.AddWithValue("@STATUS", "YES")
                        Dim reader As OleDbDataReader = cmdService.ExecuteReader()
                        'If a Customer Of Cable TV Service
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
                        'If A Customer Of Broadband Service
                        If reader.HasRows Then
                            SERVICE_COMBOBOX.Items.Add("BROADBAND")
                            reader.Close()
                        End If
                    End Using
                    con.Close()
                End Using
            Catch ex As Exception
                'If An Exception Occured, Updating It In Log File And Showing A Message
                LogError("An Error occurred in Fetching Customer And Service Details:" & ex.Message)
                MessageBox.Show("An Error Occured : Check Log For More Details", "ALERT")
            End Try
        End If
    End Sub

    'Code For Automatically Changing Pending Amount Based On Amount Paying
    Private Sub AMOUNT_LOSTFOCUS(sender As Object, e As EventArgs) Handles AMOUNT.LostFocus
        If AMOUNT.Text = "" Then

        Else
            If Not CUST_PENDING_AMOUNT_TEXTBOX.Text = "0" Then
                CUST_PENDING_AMOUNT_TEXTBOX.Text = CUST_PENDING_AMOUNT_TEXTBOX.Text - AMOUNT.Text
            End If
        End If
    End Sub

    'Code For Updating Pending Amount When Selected Service Changed
    Private Sub SERVICE_COMBOBOX_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles SERVICE_COMBOBOX.SelectedIndexChanged
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        updatepending()
        updatependingbroadband()
        AMOUNT.Clear()
    End Sub
    'Code For Clearing Service Combobox,Payment_Month_Listbox,Pending Amount,Payment Amount
    Private Sub PAYMENT_YEAR_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles PAYMENT_YEAR.SelectedIndexChanged
        SERVICE_COMBOBOX.SelectedIndex = -1
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        AMOUNT.Clear()
    End Sub

    'If Online Payment Is Selected As Payemnt Mode
    Private Sub QR_RADIO_CheckedChanged_1(sender As Object, e As EventArgs) Handles QR_RADIO.CheckedChanged
        QR_CODE.Visible = True
        REFERANCE_NO.Visible = True
        REFERANCE_NO_LABEL.Visible = True
        payment_mode = "ONLINE"
    End Sub

    'If Cash Is Selected As Payemnt Mode
    Private Sub CASH_RADIO_CheckedChanged_1(sender As Object, e As EventArgs) Handles CASH_RADIO.CheckedChanged
        QR_CODE.Visible = False
        REFERANCE_NO.Visible = False
        REFERANCE_NO_LABEL.Visible = False
        If CASH_RADIO.Checked = True Then
            payment_mode = "CASH"
        End If
    End Sub
    Private Sub RESET_BTN_Click(sender As Object, e As EventArgs) Handles RESET_BTN.Click
        'Clearing All Values
        CUST_CRF_TEXTBOX.Clear()
        clearAll()
    End Sub
    Private Sub COLLECT_BTN_Click(sender As Object, e As EventArgs) Handles COLLECT_BTN.Click
        Dim multiple As Integer = CInt(AMOUNT.Text / 250)
        Dim month_count As Integer = PAYMENT_MONTH_LISTBOX.Items.Count
        Dim enteredAmount As Integer
        If Integer.TryParse(AMOUNT.Text, enteredAmount) Then
            ' Use enteredAmount for further processing
            If CASH_RADIO.Checked = False And QR_RADIO.Checked = False Then
                MessageBox.Show("Please Select Any Payment Method", "ALERT")
            ElseIf AMOUNT.Text = "" Then
                MessageBox.Show("Please Enter Amount", "ALERT")
            ElseIf enteredAmount Mod 250 <> 0 Then
                MessageBox.Show("Please Enter Amount As Multiples Of 250", "ALERT")

            ElseIf month_count < PAYMENT_MONTH_LISTBOX.SelectedIndex Then
                MessageBox.Show("Please Select Month", "ALERT")
            ElseIf QR_RADIO.Checked = True And REFERANCE_NO.Text = "" Then
                MessageBox.Show("Please Enter Referance Number", "ALERT")
            Else

                If (AMOUNT.Text Mod 250) = 0 Then
                    If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Or SERVICE_COMBOBOX.SelectedItem = "BROADBAND" Then
                        Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                            con.Open()
                            'Database Transaction Begins
                            Dim transaction As OleDbTransaction = con.BeginTransaction()
                            Try
                                Dim monthsToUpdate As Integer = AMOUNT.Text / 250
                                For i As Integer = 0 To PAYMENT_MONTH_LISTBOX.Items.Count - 1
                                    If (PAYMENT_MONTH_LISTBOX.Items(i) = "PAID") Then
                                        MessageBox.Show("Selected month(s) have already been paid", "ALERT")
                                        Return
                                    End If
                                Next
                                'UPDATING PAYMENT STATUS FOR CABLE TV IF SELECTED SERVICE IS CABLE TV
                                If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
                                    For i As Integer = 0 To monthsToUpdate - 1
                                        'UPDATE PAYMENT QUERY FOR CABLE TV
                                        Dim query As String = "UPDATE TV_PAYMENT_DETAILS SET " & PAYMENT_MONTH_LISTBOX.Items(i) & " = @status WHERE PAYMENT_YEAR = @YEAR AND CRF = @CRF"
                                        Dim cmd As New OleDbCommand(query, con)
                                        cmd.Transaction = transaction
                                        cmd.Parameters.AddWithValue("@status", "PAID")
                                        cmd.Parameters.AddWithValue("@YEAR", PAYMENT_YEAR.SelectedItem)
                                        cmd.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                        cmd.ExecuteNonQuery()
                                    Next

                                    Dim query2 As String = "UPDATE TV_CONNECTION_DETAILS SET LAST_RENEWAL_DATE=@last_renewal_date,EXPIRY_DATE=@expiry_date,TV_CONNECTION_STATUS=@status WHERE CRF=@CRF "
                                    Dim cmd1 As New OleDbCommand(query2, con)
                                    cmd1.Transaction = transaction
                                    cmd1.Parameters.AddWithValue("@last_renewal_date", last_renewal_date_tv)
                                    Dim lastRenewalDate As Date = last_renewal_date_tv
                                    Dim thirtyDaysLater As Date = lastRenewalDate.AddDays(30 * multiple)
                                    'To store date without time'
                                    Dim expiryDateWithoutTime As Date = New Date(thirtyDaysLater.Year, thirtyDaysLater.Month, thirtyDaysLater.Day)
                                    cmd1.Parameters.AddWithValue("@EXPIRY_DATE", expiryDateWithoutTime)
                                    If CUST_PENDING_AMOUNT_TEXTBOX.Text = 0 Then
                                        cmd1.Parameters.AddWithValue("@STATUS", "ACTIVE")
                                    Else
                                        cmd1.Parameters.AddWithValue("@STATUS", "INACTIVE")
                                    End If
                                    cmd1.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                    cmd1.ExecuteNonQuery()
                                End If
                                'UPDATING PAYMENT STATUS FOR BROADBAND IF SELECTED SERVICE IS BROADBAND
                                If SERVICE_COMBOBOX.SelectedItem = "BROADBAND" Then
                                    For i As Integer = 0 To monthsToUpdate - 1
                                        'UPDATE PAYMENT QUERY FOR BROADBAND
                                        Dim query As String = "UPDATE BROADBAND_PAYMENT_DETAILS SET " & PAYMENT_MONTH_LISTBOX.Items(i) & " = @status WHERE PAYMENT_YEAR = @YEAR AND CRF = @CRF"
                                        Dim cmd As New OleDbCommand(query, con)
                                        cmd.Transaction = transaction
                                        cmd.Parameters.AddWithValue("@status", "PAID")
                                        cmd.Parameters.AddWithValue("@YEAR", PAYMENT_YEAR.SelectedItem)
                                        cmd.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)

                                        cmd.ExecuteNonQuery()
                                    Next
                                    Dim query2 As String = "UPDATE BROADBAND_CONNECTION_DETAILS SET LAST_RENEWAL_DATE=@last_renewal_date,EXPIRY_DATE=@expiry_date,STATUS=@status WHERE CRF=@CRF "
                                    Dim cmd2 As New OleDbCommand(query2, con)
                                    cmd2.Transaction = transaction
                                    cmd2.Parameters.AddWithValue("@last_renewal_date", last_renewal_date_broadband)
                                    Dim lastRenewalDate As Date = last_renewal_date_broadband
                                    Dim thirtyDaysLater As Date = lastRenewalDate.AddDays(30 * multiple)
                                    'To store date without time'
                                    Dim expiryDateWithoutTime As Date = New Date(thirtyDaysLater.Year, thirtyDaysLater.Month, thirtyDaysLater.Day)
                                    cmd2.Parameters.AddWithValue("@EXPIRY_DATE", expiryDateWithoutTime)
                                    If CUST_PENDING_AMOUNT_TEXTBOX.Text = 0 Then
                                        cmd2.Parameters.AddWithValue("@STATUS", "ACTIVE")
                                    Else
                                        cmd2.Parameters.AddWithValue("@STATUS", "INACTIVE")
                                    End If
                                    cmd2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                    cmd2.ExecuteNonQuery()
                                End If
                                'QUERY FOR ADDING INVOICE DETAILS
                                Dim cmd_INVOICE As New OleDbCommand("INSERT INTO INVOICE_DETAILS (CRF,SERVICE,PAYMENT_DATE,INVOICE_NO,REFERANCE_NO,MODE,INVOICE) VALUES (@crf,@service, @payment_date, @invoice_no, @mode,@referance_no, @invoice_file)", con)
                                cmd_INVOICE.Transaction = transaction
                                cmd_INVOICE.Parameters.AddWithValue("@crf", CUST_CRF_TEXTBOX.Text)
                                cmd_INVOICE.Parameters.AddWithValue("@service", SERVICE_COMBOBOX.SelectedItem)
                                cmd_INVOICE.Parameters.AddWithValue("@payment_date", Date.Today)
                                cmd_INVOICE.Parameters.AddWithValue("@invoice_no", invoice_no)
                                If QR_RADIO.Checked = True Then
                                    cmd_INVOICE.Parameters.AddWithValue("@referance_no", REFERANCE_NO.Text)
                                End If
                                If CASH_RADIO.Checked = True Then
                                    cmd_INVOICE.Parameters.AddWithValue("@referance_no", "NILL")
                                End If
                                cmd_INVOICE.Parameters.AddWithValue("@mode", payment_mode)
                                Dim invoiceFilePath As String = invoicepath & invoice_no & ".pdf"
                                cmd_INVOICE.Parameters.AddWithValue("@invoice_file", invoiceFilePath)
                                cmd_INVOICE.ExecuteNonQuery()

                                'CALLING METHOD FOR GENERATING INVOICE
                                Generate_Invoice()
                                'CALLING FUNCTION FOR SENDING INVOICE TO CUSTOMER MAIL
                                If CUST_EMAIL_TEXTBOX.Text IsNot Nothing Then
                                    Dim email_to As String = CUST_EMAIL_TEXTBOX.Text
                                    Dim pending_amt As Integer = CUST_PENDING_AMOUNT_TEXTBOX.Text
                                    Dim service As String = SERVICE_COMBOBOX.SelectedItem
                                    Dim cust_name As String = CUST_NAME_TEXTBOX.Text
                                    Dim invoiceNumber As String = invoice_no
                                    Dim invoicesFolder As String = invoicepath
                                    Dim filePath As String = Path.Combine(invoicesFolder, invoiceNumber & ".pdf")
                                    Invoice_Sender.Email(email_to, "PAYMENT CONFIRMATION", filePath, cust_name, pending_amt)
                                End If
                                'COMMITTING ALL THE TRANSACTIONS
                                transaction.Commit()
                                con.Close()
                                'IF PAYMENT IS SUCCESS
                                SuccessAlert.Play()
                                MessageBox.Show("Payment Successful", "ALERT")
                                'AFTER SUCCESSFULL PAYMENT, PRINT BUTTON BECOMES VISIBLE
                                PRINT_BTN.Visible = True
                            Catch ex As Exception
                                'IF ANY ERROR OCCURED TRANSACTION IS ROLLBACKED AND ADD THE ERROR MESSAGE TO LOG
                                transaction.Rollback()
                                LogError("An Error Occoured During Payment: " & ex.Message)
                                MessageBox.Show("Payment Declined. Check Log For More Details", "ERROR")
                            End Try
                            con.Close()
                        End Using
                    End If
                End If

            End If
        Else
            ' Display an error message to the user
            MessageBox.Show("Please enter a valid integer value for AMOUNT.")
        End If
    End Sub

    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.TextChanged
        If CUST_CRF_TEXTBOX.Text = "" Then
            clearAll()
        End If
    End Sub
End Class
