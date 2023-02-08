Imports System.Buffers
Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports System.Drawing.Printing
Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic.Logging

Public Class Collect_Payment_Admin
    Dim payment_mode As String
    Private Sub Collect_Payment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim currentYear As Integer = DateTime.Now.Year
        PAYMENT_YEAR.Items.Add(currentYear)
        PAYMENT_YEAR.Items.Add(currentYear - 1)
        QR_RADIO.Checked = True
        QR_CODE.Visible = True
        REFERANCE_NO.Visible = True
        REFERANCE_NO_LABEL.Visible = True
    End Sub
    Private Sub updatepending()
        If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
            If PAYMENT_YEAR.SelectedItem = Nothing Then
                MessageBox.Show("Please Select Year", "ALERT")
            Else
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb")
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
    End Sub
    Private Sub SERVICE_COMBOBOX_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles SERVICE_COMBOBOX.SelectedIndexChanged
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        updatepending()
    End Sub

    Private Sub PAYMENT_YEAR_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles PAYMENT_YEAR.SelectedIndexChanged
        SERVICE_COMBOBOX.SelectedIndex = -1
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()

    End Sub

    Private Sub QR_RADIO_CheckedChanged_1(sender As Object, e As EventArgs) Handles QR_RADIO.CheckedChanged
        QR_CODE.Visible = True
        REFERANCE_NO.Visible = True
        REFERANCE_NO_LABEL.Visible = True
    End Sub

    Private Sub CASH_RADIO_CheckedChanged_1(sender As Object, e As EventArgs) Handles CASH_RADIO.CheckedChanged
        QR_CODE.Visible = False
        REFERANCE_NO.Visible = False
        REFERANCE_NO_LABEL.Visible = False
    End Sub

    Private Function clear_all()
        CUST_CRF_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_DISTRICT_TEXTBOX.Clear()
        CUST_HOUSENAME_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_STATE_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()
        PAYMENT_YEAR.SelectedIndex = -1
        SERVICE_COMBOBOX.SelectedIndex = -1
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        PAYMENT_MONTH_LISTBOX.Items.Clear()
        AMOUNT.Clear()
        REFERANCE_NO.Clear()
        QR_RADIO.Checked = True
        CASH_RADIO.Checked = False
        Return 0
    End Function
    Private Sub RESET_BTN_Click(sender As Object, e As EventArgs) Handles RESET_BTN.Click
        clear_all()
    End Sub

    Private Sub COLLECT_BTN_Click(sender As Object, e As EventArgs) Handles COLLECT_BTN.Click
        If CASH_RADIO.Checked = False And QR_RADIO.Checked = False Then
            MessageBox.Show("Please Select Any Payment Method", "ALERT")
        End If
        If CASH_RADIO.Checked = True Then
            payment_mode = "CASH"
        End If
        If QR_RADIO.Checked = True Then
            payment_mode = "ONLINE"
            If REFERANCE_NO.Text = "" Then
                MessageBox.Show("Please Enter Referance Number", "ALERT")
            End If
        End If
        If QR_RADIO.Checked = False And CASH_RADIO.Checked = False Then
            MessageBox.Show("Please Select A Payment Method", "ALERT")
        End If
        If AMOUNT.Text = "" Then
            MessageBox.Show("Please Enter Amount", "ALERT")
        End If

        Dim enteredAmount As Integer = CInt(AMOUNT.Text)
        If enteredAmount Mod 250 <> 0 Then
            MessageBox.Show("Please Enter Amount As Multiples Of 250", "ALERT")
            Return
        End If

        If AMOUNT.Text = "" Then
            MessageBox.Show("Please Enter Amount", "ALERT")
        End If
        If (AMOUNT.Text Mod 250) = 0 Then
            If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
                Try
                    Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb")
                        con.Open()
                        Dim monthsToUpdate As Integer = AMOUNT.Text / 250
                        For i As Integer = 0 To PAYMENT_MONTH_LISTBOX.Items.Count - 1
                            If (PAYMENT_MONTH_LISTBOX.Items(i) = "PAID") Then
                                MessageBox.Show("Selected month(s) have already been paid", "ALERT")
                                Return
                            End If
                        Next

                        For i As Integer = 0 To monthsToUpdate - 1
                            Dim query As String = "UPDATE TV_PAYMENT_DETAILS SET " & PAYMENT_MONTH_LISTBOX.Items(i) & " = @status WHERE CURRENT_YEAR = @YEAR AND CRF = @CRF"
                            Dim cmd As New OleDbCommand(query, con)
                            cmd.Parameters.AddWithValue("@status", "PAID")
                            cmd.Parameters.AddWithValue("@YEAR", PAYMENT_YEAR.SelectedItem)
                            cmd.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            cmd.ExecuteNonQuery()
                        Next
                        Dim ref As String = "INSERT INTO PAYMENT (CRF,PAYMENT_DATE,REFERANCE_NO,MODE) VALUES (@CRF,@PAYMENT_DATE,@REFERANCE_NO,@MODE)"
                        Dim refcmd As New OleDbCommand(ref, con)
                        refcmd.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        refcmd.Parameters.AddWithValue("@PAYMENT_DATE", Date.Today)
                        refcmd.Parameters.AddWithValue("@REFERANCE_NO", "121")
                        refcmd.Parameters.AddWithValue("@MODE", payment_mode)
                        refcmd.ExecuteNonQuery()
                        con.Close()
                        MessageBox.Show("Payment Successful", "ALERT")
                        AMOUNT.Clear()
                        PAYMENT_MONTH_LISTBOX.Items.Clear()
                        updatepending()
                    End Using
                Catch ex As Exception
                    MessageBox.Show("An error occurred while updating the payment status: " & ex.Message, "ERROR")
                End Try
            End If
        End If
    End Sub

    Private Sub SEARCH_BTN_Click_1(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        CUST_PENDING_AMOUNT_TEXTBOX.Clear()
        PAYMENT_YEAR.SelectedItem = ""
        SERVICE_COMBOBOX.Items.Clear()
        If CUST_CRF_TEXTBOX.Text = "" Then
        Else
            Try
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb")
                    con.Open()
                    Dim sqlCheck As String = "SELECT * FROM [CUSTOMER_DETAILS] WHERE [CRF] =@CRF"
                    Dim sqlFetch As String = "SELECT CUST_NAME,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_MOBILE FROM CUSTOMER_DETAILS WHERE CRF=@CRF"
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
                                CUST_HOUSENAME_TEXTBOX.Text = reader.GetString(1)
                                CUST_AREA_TEXTBOX.Text = reader.GetString(2)
                                CUST_DISTRICT_TEXTBOX.Text = reader.GetString(3)
                                CUST_STATE_TEXTBOX.Text = reader.GetString(4)
                                CUST_MOBILE_TEXTBOX.Text = reader.GetValue(5)
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
                End Using
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub PRINT_BTN_Click(sender As Object, e As EventArgs) Handles PRINT_BTN.Click

        Dim printDoc As New PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf PrintPageHandler
        printDoc.Print()
    End Sub

    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim font1 As New Font("Arial Black", 28, FontStyle.Bold)
        Dim font2 As New Font("Arial", 14, FontStyle.Bold)
        Dim text As String = "BHARATH CABLE TV NETWORK"
        Dim text1 As String = "KARIKKATTOOR CENTRE & MAKKAPUZHA"
        Dim text2 As String = "KOTTAYAM, KERALA, 686544"
        Dim text3 As String = "MOB: 6282522127"
        Dim textSize As SizeF = e.Graphics.MeasureString(text, font1)
        Dim text1Size As SizeF = e.Graphics.MeasureString(text1, font2)
        Dim text2Size As SizeF = e.Graphics.MeasureString(text2, font2)
        Dim text3Size As SizeF = e.Graphics.MeasureString(text3, font2)
        ' Define the font and the text to print
        e.Graphics.DrawString(text, font1, Brushes.Red, (e.PageBounds.Width - textSize.Width) / 2, 10)
        e.Graphics.DrawString(text1, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, (e.PageBounds.Width - text1Size.Width) / 2, textSize.Height)
        e.Graphics.DrawString(text2, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, (e.PageBounds.Width - text2Size.Width) / 2, textSize.Height + text1Size.Height)
        e.Graphics.DrawString(text3, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, (e.PageBounds.Width - text3Size.Width) / 2, textSize.Height + text1Size.Height + text2Size.Height)
        Dim pen As New Pen(Color.Black, 5)
        e.Graphics.DrawLine(pen, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 20, e.PageBounds.Width - 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 20)
        e.Graphics.DrawString("INVOICE NO: 100" & CUST_CRF_TEXTBOX.Text, New Font("Arial Black", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 40)
        e.Graphics.DrawString("DATE: " & Date.Today, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, e.PageBounds.Width - 185, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 40)
        e.Graphics.DrawString("BILL TO", New Font("Arial Black", 14), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 120)
        e.Graphics.DrawString("CUSTOMER NAME          : " & CUST_NAME_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 180)
        e.Graphics.DrawString("HOUSE NAME                  : " & CUST_HOUSENAME_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 210)
        e.Graphics.DrawString("CRF NUMBER                  : " & CUST_CRF_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 240)
        e.Graphics.DrawString("SERVICE                          : " & SERVICE_COMBOBOX.SelectedItem, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 270)
        e.Graphics.DrawString("MONTH                             : " & PAYMENT_MONTH_LISTBOX.SelectedItem, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 300)
        e.Graphics.DrawString("AMOUNT PAID                 : " & AMOUNT.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 330)
        e.Graphics.DrawString("PENDING AMOUNT         : " & CUST_PENDING_AMOUNT_TEXTBOX.Text, New Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 360)
        e.Graphics.DrawLine(pen, 15, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 420, e.PageBounds.Width - 20, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 420)
        Dim sealImage As Image = Image.FromFile("C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Resources\SEAL.png")
        Dim x As Integer = e.PageBounds.Width - sealImage.Width + 40
        Dim y As Integer = 520
        Dim newWidth As Integer = sealImage.Width - 50
        Dim newHeight As Integer = sealImage.Height - 50
        e.Graphics.DrawImage(sealImage, x, y, newWidth, newHeight)
        e.Graphics.DrawString("SEAL & SIGNATURE", New Font("Arial", 14, FontStyle.Bold), Brushes.Black, e.PageBounds.Width - 230, textSize.Height + text1Size.Height + text2Size.Height + text3Size.Height + 600)
        ' e.Graphics.DrawString("*PLEASE PAY THE MONTHLY RENTAL ON OR BEFORE 10TH EVERY MONTH.", New Font("Arial", 12), Brushes.Black, 10, 330) '
        ' e.Graphics.DrawString("*DUE FOR MORE THAN 1 MONTH WILL LEAD TO DISCONNECTION WITHOUT ANY PRIOR NOTICE.", New Font("Arial", 12), Brushes.Black, 10, 350) '
        e.HasMorePages = False
    End Sub
End Class