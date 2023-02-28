﻿Imports System.Collections.ObjectModel
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports CABLE_TV_MANAGEMENT_SYSTEM.LogModule
Imports CABLE_TV_MANAGEMENT_SYSTEM.Payment_Sync
Imports Guna.UI2.WinForms

Public Class add_customer
    ReadOnly months As New List(Of String)
    'Function for clearing all inputs'
    Public Function ClearAll()
        CUST_CRF_TEXTBOX.Clear()
        CUST_CRF_TEXTBOX.Text = GenerateCRF()
        CUST_NAME_TEXTBOX.Clear()
        DOB_PICKER.ResetText()
        CUST_HOUSENAME_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_DISTRICT_TEXTBOX.Clear()
        CUST_STATE_COMBOBOX.SelectedIndex = -1
        CUST_COUNTRY_COMBOBOX.SelectedIndex = -1
        CUST_PINCODE_TEXTBOX.Clear()
        CUST_IDTYPE_COMBOBOX.SelectedIndex = -1
        CUST_IDNUMBER_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()
        CUST_EMAIL_TEXTBOX.Clear()
        CUST_BROADBAND_COMBOBOX.SelectedIndex = -1
        CUST_BROADBAND_PLAN_COMBOBOX.SelectedIndex = -1
        CUST_BROADBAND_USERNAME_TEXTBOX.Clear()
        CUST_BROADBAND_PASSWORD_TEXTBOX.Clear()
        CUST_TV_CONNECTION_COMBOBOX.SelectedIndex = -1
        CUST_CABLE_PLAN_COMBOBOX.SelectedIndex = -1
        CUST_CHIP_ID_TEXTBOX.Clear()
        CUST_USERNAME_TEXTBOX.Clear()
        CUST_PASSWORD_TEXTBOX.Clear()
        BROADBAND_REG_DATE.ResetText()
        TV_Reg_Picker.ResetText()
        Return 0
    End Function
    'For Storing Current Year'
    ReadOnly currentYear As Integer = CInt(DateTime.Now.Year)

    'Function For Generating Unique CRF Number'
    Public Function GenerateCRF() As Integer
        Try
            Using connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
                Dim uniqueNumber As Integer = 100
                Dim found As Boolean = False
                While Not found
                    Dim command As New OleDb.OleDbCommand("SELECT COUNT(*) FROM CUSTOMER_DETAILS WHERE CRF = " & uniqueNumber & "", connection)
                    Dim count As Integer = command.ExecuteScalar()
                    If count = 0 Then
                        found = True
                    Else
                        uniqueNumber += 1
                    End If
                End While
                connection.Close()
                Return uniqueNumber
                connection.Close()
            End Using
        Catch ex As Exception
            ErrorAlert.Play()
            LogError("An Error Occured While Generating New CRF: " & ex.Message)
            MessageBox2.Show("An Error Occured While Generating New CRF: Check Log For More Details.")
        End Try
        Return 0
    End Function
    'Function For Adding Asian Countries To Combobox
    Function IsAsianCountry(country As String) As Boolean
        ' Create a list of Asian countries
        Dim asianCountries As New List(Of String) From {"China", "India", "Indonesia", "Pakistan", "Bangladesh", "Japan", "Philippines", "Vietnam", "Iran", "Thailand", "Myanmar", "South Korea", "Sri Lanka", "Afghanistan", "Nepal", "North Korea", "Mongolia", "Laos", "Cambodia", "Bhutan", "Taiwan"}
        ' Check if the input country is in the list of Asian countries
        If asianCountries.Contains(country) Then
            Return True
        Else
            Return False
        End If
    End Function
    'Form Loading'
    Private Sub add_customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ADD_CUSTOMER_PROGRESS.Visible = False
        CUST_STATE_COMBOBOX.Items.Add("PLEASE SELECT COUNTRY FIRST")
        Dim AsianCountries As String() = {"Afghanistan", "Armenia", "Azerbaijan", "Bahrain", "Bangladesh", "Bhutan", "Brunei", "Cambodia", "China", "Cyprus", "Georgia", "India", "Indonesia", "Iran", "Iraq", "Israel", "Japan", "Jordan", "Kazakhstan", "Kuwait", "Kyrgyzstan", "Laos", "Lebanon", "Malaysia", "Maldives", "Mongolia", "Myanmar", "Nepal", "North Korea", "Oman", "Pakistan", "Palestine", "Philippines", "Qatar", "Russia", "Saudi Arabia", "Singapore", "South Korea", "Sri Lanka", "Syria", "Taiwan", "Tajikistan", "Thailand", "Timor-Leste", "Turkey", "Turkmenistan", "United Arab Emirates", "Uzbekistan", "Vietnam", "Yemen"}
        For Each country As String In AsianCountries
            CUST_COUNTRY_COMBOBOX.Items.Add(country)
        Next
        CUST_COUNTRY_COMBOBOX.Items.Cast(Of String)().ToList().Sort()

        CUST_CRF_TEXTBOX.Text = GenerateCRF()

        'ROADBAND_REG_DATE.MinDate = DateTime.Now.Date
        BROADBAND_REG_DATE.MaxDate = Date.Today
        TV_Reg_Picker.MaxDate = Date.Today
        TV_Reg_Picker.MinDate = Date.Today
        DOB_PICKER.MinDate = DateTime.Today.AddYears(-80)
        DOB_PICKER.MaxDate = DateTime.Today.AddYears(-18)
        DOB_PICKER.Value = DateTime.Today.AddYears(-18)


    End Sub

    'For Adding Indian States If Selected Country Is India'
    Private Sub CUST_COUNTRY_COMBOBOX_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles CUST_COUNTRY_COMBOBOX.SelectedIndexChanged
        Dim IndianStates As String() = {"Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand", "West Bengal"}
        If CUST_COUNTRY_COMBOBOX.SelectedItem = "India" Then
            CUST_STATE_COMBOBOX.Items.Clear()
            For Each state As String In IndianStates
                CUST_STATE_COMBOBOX.Items.Add(state)
            Next
        Else
            CUST_STATE_COMBOBOX.Items.Clear()
        End If
    End Sub

    'If Create Button Clicked'
    Private Sub ADD_CUST_CREATEBTN_Click_1(sender As Object, e As EventArgs) Handles ADD_CUST_CREATEBTN.Click
        'Dim startDate As Date = Date.Today.AddMonths(1)
        ' Dim endDate As Date = Date.Today.AddMonths(3)

        ' Iterate over the months and update the values
        ' Dim loopDate As Date = startDate


        Dim found As Boolean = False
        'To Check Whether The Entered Value in Email Textbox Is Email or Not'
        Dim emailRegex As New Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
        If Not emailRegex.IsMatch(CUST_EMAIL_TEXTBOX.Text) Then
            MessageBox2.Show("Invalid email address. Please enter a valid email address.", "ALERT")
            CUST_EMAIL_TEXTBOX.Clear()
        ElseIf CUST_STATE_COMBOBOX.Text = "PLEASE SELECT COUNTRY FIRST" Then
            MessageBox2.Show("PLEASE SELECT STATE", "ALERT")
        ElseIf CUST_CRF_TEXTBOX.Text = "" Or CUST_NAME_TEXTBOX.Text = "" Or DOB_PICKER.Text = "" Or CUST_HOUSENAME_TEXTBOX.Text = "" Or CUST_AREA_TEXTBOX.Text = "" Or CUST_DISTRICT_TEXTBOX.Text = "" Or CUST_STATE_COMBOBOX.Text = "" Or CUST_COUNTRY_COMBOBOX.Text = "" Or CUST_IDTYPE_COMBOBOX.Text = "" Or CUST_IDNUMBER_TEXTBOX.Text = "" Or CUST_MOBILE_TEXTBOX.Text = "" Or CUST_EMAIL_TEXTBOX.Text = "" Or CUST_BROADBAND_COMBOBOX.Text = "" Or CUST_TV_CONNECTION_COMBOBOX.Text = "" Then
            MessageBox2.Show("Please Enter All The Details", "ALERT")
        ElseIf CUST_BROADBAND_COMBOBOX.SelectedItem = "NO" And CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
            MessageBox2.Show("Please Select Atleast One Service", "ALERT")
        ElseIf CUST_BROADBAND_COMBOBOX.SelectedItem = "YES" And CUST_BROADBAND_USERNAME_TEXTBOX.Text = "" And CUST_BROADBAND_PASSWORD_TEXTBOX.Text = "" Then
            MessageBox2.Show("Please Enter All Broadband Connection Details", "ALERT")
        ElseIf CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "YES" And CUST_CABLE_PLAN_COMBOBOX.Text = "" And CUST_CHIP_ID_TEXTBOX.Text = "" Then
            MessageBox2.Show("Please Enter All TV Connection Details", "ALERT")
        Else
            Try
                'For Inserting Data To Database'
                ADD_CUSTOMER_PROGRESS.Visible = True
                ADD_CUSTOMER_PROGRESS.Start()
                Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                    con.Open()
                    'For Begining The Transaction'
                    Dim transaction As OleDbTransaction = con.BeginTransaction()
                    Try
                        Dim cmd As New OleDbCommand("INSERT INTO CUSTOMER_DETAILS (CRF,CUST_NAME,CUST_DOB,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_COUNTRY,CUST_PINCODE,CUST_IDTYPE,CUST_ID_NUMBER,CUST_MOBILE,CUST_EMAIL) VALUES (@CRF,@NAME,@DOB,@HOUSE_NAME,@AREA,@DISTRICT,@STATE,@COUNTRY,@PINCODE,@IDTYPE,@ID_NUMBER,@MOBILE,@EMAIL)", con)
                        Dim cmd2 As New OleDbCommand("INSERT INTO CUSTOMER_LOGIN_DETAILS (CRF,CUST_USERNAME,CUST_PASSWORD) VALUES (@CRF,@CUST_USERNAME,@CUST_PASSWORD)", con)
                        Dim cmd3 As New OleDbCommand("INSERT INTO TV_CONNECTION_DETAILS (CRF,TV_CONNECTION_ID,CUST_TV_CONNECTION,CUST_TV_PLAN,CHIP_ID,REGISTRATION_DATE,LAST_RENEWAL_DATE,EXPIRY_DATE,TV_CONNECTION_STATUS) VALUES (@CRF,@TV_CONNECTION_ID,@CUST_TV_CONNECTION,@CUST_TV_PLAN,@CHIP_ID,@TV_REGISTRATION_DATE,@TV_LAST_RENEWAL_DATE,@EXPIRY_DATE,@TV_CONNECTION_STATUS)", con)
                        Dim cmd4 As New OleDbCommand("INSERT INTO TV_PAYMENT_DETAILS (CRF,PAYMENT_YEAR) VALUES (@CRF,@YEAR)", con)
                        Dim cmd5 As New OleDbCommand("INSERT INTO BROADBAND_CONNECTION_DETAILS (CRF,REGISTRATION_DATE,LAST_RENEWAL_DATE,EXPIRY_DATE,STATUS,RECHARGED_BY,CURRENT_PLAN,BROADBAND_CONNECTION) VALUES (@CRF,@REGISTRATION_DATE,@LAST_RENEWAL_DATE,@EXPIRY_DATE,@STATUS,@RECHARGED_BY,@CURRENT_PLAN,@BROADBAND_CONNECTION)", con)
                        Dim cmd6 As New OleDbCommand("INSERT INTO BROADBAND_LOGIN (CRF,CUST_BROADBAND_USERNAME,CUST_BROADBAND_PASSWORD) VALUES (@CRF,@CUST_BROADBAND_USERNAME,@CUST_BROADBAND_PASSWORD)", con)
                        Dim cmd7 As New OleDbCommand("INSERT INTO BROADBAND_PAYMENT_DETAILS (CRF,BROADBAND_ID,PAYMENT_YEAR) VALUES (@CRF,@BROADBAND_ID,@YEAR)", con)

                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@NAME", CUST_NAME_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@DOB", DOB_PICKER.Value)
                        cmd.Parameters.AddWithValue("@HOUSE_NAME", CUST_HOUSENAME_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@AREA", CUST_AREA_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@DISTRICT", CUST_DISTRICT_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@STATE", CUST_STATE_COMBOBOX.SelectedItem)
                        cmd.Parameters.AddWithValue("@COUNTRY", CUST_COUNTRY_COMBOBOX.SelectedItem)
                        cmd.Parameters.AddWithValue("@PINCODE", CUST_PINCODE_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@IDTYPE", CUST_IDTYPE_COMBOBOX.SelectedItem)
                        cmd.Parameters.AddWithValue("@ID_NUMBER", CUST_IDNUMBER_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@MOBILE", CUST_MOBILE_TEXTBOX.Text)
                        cmd.Parameters.AddWithValue("@EMAIL", CUST_EMAIL_TEXTBOX.Text)
                        cmd.ExecuteNonQuery()
                        cmd2.Transaction = transaction
                        cmd2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd2.Parameters.AddWithValue("@CUST_USERNAME", CUST_USERNAME_TEXTBOX.Text)
                        cmd2.Parameters.AddWithValue("CUST_PASSWORD", CUST_PASSWORD_TEXTBOX.Text)
                        cmd2.ExecuteNonQuery()
                        cmd3.Transaction = transaction
                        cmd3.Parameters.Clear()
                        cmd3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd3.Parameters.AddWithValue("@TV_CONNECTION_ID", CUST_CRF_TEXTBOX.Text)
                        cmd3.Parameters.AddWithValue("@CUST_TV_CONNECTION", CUST_TV_CONNECTION_COMBOBOX.SelectedItem)
                        cmd3.Parameters.AddWithValue("@CUST_TV_PLAN", CUST_CABLE_PLAN_COMBOBOX.SelectedItem)
                        cmd3.Parameters.AddWithValue("@CHIP_ID", CUST_CHIP_ID_TEXTBOX.Text)
                        cmd3.Parameters.AddWithValue("@TV_REGISTRATION_DATE", TV_Reg_Picker.Value)
                        cmd3.Parameters.AddWithValue("@TV_LAST_RENEWAL_DATE", TV_Reg_Picker.Value)
                        cmd3.Parameters.AddWithValue("@EXPIRY_DATE", TV_Reg_Picker.Value)
                        cmd3.Parameters.AddWithValue("@TV_CONNECTION_STATUS", "INACTIVE")
                        cmd4.Parameters.Clear()
                        cmd4.Transaction = transaction
                        cmd4.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd4.Parameters.AddWithValue("@YEAR", currentYear)
                        cmd5.Parameters.Clear()
                        cmd5.Transaction = transaction
                        cmd5.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd5.Parameters.AddWithValue("@REGISTRATION_DATE", BROADBAND_REG_DATE.Value)
                        cmd5.Parameters.AddWithValue("@LAST_RENEWAL_DATE", BROADBAND_REG_DATE.Value)
                        cmd5.Parameters.AddWithValue("@EXPIRY_DATE", BROADBAND_REG_DATE.Value)
                        cmd5.Parameters.AddWithValue("@STATUS", "INACTIVE")
                        cmd5.Parameters.AddWithValue("@RECHARGED_BY", "ADMIN")
                        cmd5.Parameters.AddWithValue("@CURRENT_PLAN", CUST_BROADBAND_PLAN_COMBOBOX.SelectedItem)
                        cmd5.Parameters.AddWithValue("@BROADBAND_CONNECTION", "YES")
                        cmd6.Parameters.Clear()
                        cmd6.Transaction = transaction
                        cmd6.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd6.Parameters.AddWithValue("@CUST_BROADBAND_USERNAME", CUST_BROADBAND_USERNAME_TEXTBOX.Text)
                        cmd6.Parameters.AddWithValue("@CUST_BROADBAND_PASSWORD", CUST_BROADBAND_PASSWORD_TEXTBOX.Text)
                        cmd7.Parameters.Clear()
                        cmd7.Transaction = transaction
                        cmd7.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cmd7.Parameters.AddWithValue("@BROADBAND_ID", CUST_CRF_TEXTBOX.Text)
                        cmd7.Parameters.AddWithValue("@YEAR", currentYear)
                        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "YES" Then
                            cmd3.ExecuteNonQuery()
                            cmd4.ExecuteNonQuery()
                            Dim todayWithoutTime As Date = Date.Today
                            Dim fromdate As Date = TV_Reg_Picker.Value
                            Dim todate As Date = todayWithoutTime
                            Dim crf As Integer = CUST_CRF_TEXTBOX.Text
                            Dim months As List(Of String) = GetMonthsBetween(fromdate, todate)
                            Dim count As Integer = months.Count

                            If count < 0 Then
                                MessageBox.Show("Less than 0")
                            Else
                                For i = 0 To count - 1
                                    Dim command As New OleDbCommand("UPDATE TV_PAYMENT_DETAILS SET " & months(i) & "='NOT PAID' WHERE CRF=@CRF AND " & months(i) & "='NILL'", con)
                                    command.Transaction = transaction
                                    command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                    command.ExecuteNonQuery()
                                Next
                            End If
                        Else
                            cmd3.Parameters.Clear()
                            cmd4.Parameters.Clear()
                        End If
                        If CUST_BROADBAND_COMBOBOX.SelectedItem = "YES" Then
                            Dim todayWithoutTime As Date = Date.Today
                            cmd5.ExecuteNonQuery()
                            cmd6.ExecuteNonQuery()
                            cmd7.ExecuteNonQuery()
                            Dim fromdate As Date = BROADBAND_REG_DATE.Value
                            Dim todate As Date = todayWithoutTime
                            Dim crf As Integer = CUST_CRF_TEXTBOX.Text
                            Dim months As List(Of String) = GetMonthsBetween(fromdate, todate)
                            Dim count As Integer = months.Count

                            If count < 0 Then
                                MessageBox.Show("Less than 0")
                            Else
                                For i = 0 To count - 1
                                    Dim command As New OleDbCommand("UPDATE BROADBAND_PAYMENT_DETAILS SET " & months(i) & "='NOT PAID' WHERE CRF=@CRF AND " & months(i) & "='NILL'", con)
                                    command.Transaction = transaction
                                    command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                    command.ExecuteNonQuery()
                                Next
                            End If
                        Else
                            cmd5.Parameters.Clear()
                            cmd6.Parameters.Clear()
                            cmd7.Parameters.Clear()
                        End If

                        transaction.Commit()
                        MessageBox2.Show("Registration Sucessfull", "ALERT")
                        Payment_Sync.Payment_Sync()
                        ADD_CUSTOMER_PROGRESS.Stop()
                        ADD_CUSTOMER_PROGRESS.Visible = False
                        ClearAll()

                    Catch ex As Exception
                        transaction.Rollback()
                        'Storing Error To Log File'
                        LogError("An error occurred: " & ex.Message)
                        MessageBox2.Show("Registration Unsucessfull", "ALERT")
                        ADD_CUSTOMER_PROGRESS.Stop()
                        ADD_CUSTOMER_PROGRESS.Visible = False
                    End Try
                    con.Close()
                End Using
            Catch ex As Exception
                LogError("An error occurred: " & ex.Message)
            Finally

            End Try
        End If
    End Sub
    Private Function GetMonthsBetween(ByVal startDate As Date, ByVal endDate As Date) As List(Of String)
        Dim months As New List(Of String)
        Dim currentMonth As Integer = startDate.Month
        Dim currentYear As Integer = startDate.Year

        While currentMonth <= endDate.Month And currentYear <= endDate.Year
            Dim monthName As String = DateTimeFormatInfo.CurrentInfo.GetMonthName(currentMonth)
            months.Add(monthName)
            currentMonth += 1
            If currentMonth > 12 Then
                currentMonth = 1
                currentYear += 1
            End If
        End While

        Return months
    End Function

    'Now you can use the "months" array to do whatever you need to do

    'For Reset Button'
    Private Sub ADD_CUST_RESETBTN_Click(sender As Object, e As EventArgs) Handles ADD_CUST_RESETBTN.Click
        ClearAll()
    End Sub
    'For Hiding Rest Of The Inputs When Tv Connection Not Selected.'
    Private Sub CUST_TV_CONNECTION_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CUST_TV_CONNECTION_COMBOBOX.SelectedIndexChanged
        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "YES" Then
            CUST_CABLE_PLAN_COMBOBOX.Show()
            CUST_CABLE_PLAN_LABEL.Show()
            CUST_CHIP_ID_LABEL.Show()
            CUST_CHIP_ID_TEXTBOX.Show()
            TV_Reg_Label.Show()
            TV_Reg_Picker.Show()
        End If
        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
            CUST_CABLE_PLAN_COMBOBOX.Hide()
            CUST_CABLE_PLAN_LABEL.Hide()
            CUST_CHIP_ID_LABEL.Hide()
            CUST_CHIP_ID_TEXTBOX.Hide()
            CUST_CABLE_PLAN_COMBOBOX.Text = ""
            CUST_CHIP_ID_TEXTBOX.Text = ""
            TV_Reg_Label.Hide()
            TV_Reg_Picker.Hide()
        End If
    End Sub
    'For Hiding Rest Of The Inputs When Broadband Connection Not Selected.'
    Private Sub CUST_BROADBAND_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CUST_BROADBAND_COMBOBOX.SelectedIndexChanged
        If CUST_BROADBAND_COMBOBOX.SelectedItem = "YES" Then
            CUST_BROADBAND_PLAN_COMBOBOX.Show()
            CUST_BROADBAND_PLAN_LABEL.Show()
            CUST_BROADBAND_USERNAME_LABEL.Show()
            CUST_BROADBAND_USERNAME_TEXTBOX.Show()
            CUST_BROADBAND_PASSWORD_LABEL.Show()
            CUST_BROADBAND_PASSWORD_TEXTBOX.Show()
            BB_REG_DATE_LABEL.Show()
            BROADBAND_REG_DATE.Show()
        End If
        If CUST_BROADBAND_COMBOBOX.SelectedItem = "NO" Then
            CUST_BROADBAND_PLAN_COMBOBOX.Hide()
            CUST_BROADBAND_PLAN_LABEL.Hide()
            CUST_BROADBAND_USERNAME_LABEL.Hide()
            CUST_BROADBAND_USERNAME_TEXTBOX.Hide()
            CUST_BROADBAND_PASSWORD_LABEL.Hide()
            CUST_BROADBAND_PASSWORD_TEXTBOX.Hide()
            BB_REG_DATE_LABEL.Hide()
            BROADBAND_REG_DATE.Hide()
            CUST_BROADBAND_PLAN_COMBOBOX.Text = ""
            CUST_BROADBAND_USERNAME_TEXTBOX.Text = ""
            CUST_BROADBAND_PASSWORD_TEXTBOX.Text = ""
            BROADBAND_REG_DATE.Text = ""

        End If
    End Sub
    'For Checking If Username is Already Seleccted'
    Private Sub CUST_USERNAME_TEXTBOX_Leave(sender As Object, e As EventArgs) Handles CUST_USERNAME_TEXTBOX.Leave
        Try
            Using connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
                Dim found As Boolean = False
                Dim cmd As New OleDbCommand("SELECT * FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@CUST_USERNAME", connection)
                cmd.Parameters.AddWithValue("@CUST_USERNAME", CUST_USERNAME_TEXTBOX.Text)
                Dim count As Integer = CType(cmd.ExecuteScalar(), Integer)
                connection.Close()
                If count > 0 Then
                    MessageBox2.Show("This username is already taken. Please choose a different username.", "ALERT")
                    CUST_USERNAME_TEXTBOX.Clear()
                    CUST_USERNAME_TEXTBOX.Focus()
                End If
                connection.Close()
            End Using
        Catch ex As Exception
            LogError("ADD CUSTOMER - CUST_USERNAME_TEXTBOX_LEAVE")
            LogError("An error occurred: " & ex.Message)
        End Try

    End Sub
    'For Checking If Broadband Username Already Taken'
    Private Sub CUST_BROADBAND_USERNAME_TEXTBOX_Leave(sender As Object, e As EventArgs) Handles CUST_BROADBAND_USERNAME_TEXTBOX.Leave
        Try
            Using connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
                Dim found As Boolean = False
                Dim cmd As New OleDbCommand("SELECT * FROM BROADBAND_LOGIN WHERE CUST_BROADBAND_USERNAME=@CUST_USERNAME", connection)
                cmd.Parameters.AddWithValue("@CUST_USERNAME", CUST_BROADBAND_USERNAME_TEXTBOX.Text)
                Dim count As Integer = CType(cmd.ExecuteScalar(), Integer)
                connection.Close()
                If count > 0 Then
                    MessageBox2.Show("This username is already taken. Please choose a different username.", "ALERT")
                    CUST_BROADBAND_USERNAME_TEXTBOX.Clear()
                    CUST_BROADBAND_USERNAME_TEXTBOX.Focus()
                End If
                connection.Close()
            End Using
        Catch ex As Exception
            LogError("ADD CUSTOMER - CUST_BROADBAND_USERNAME_TEXTBOX_LEAVE")
            LogError("An error occurred: " & ex.Message)
        End Try
    End Sub
    'For Checking If CHIP ID Already Added'
    Private Sub CUST_CHIP_ID_TEXTBOX_Leave(sender As Object, e As EventArgs) Handles CUST_CHIP_ID_TEXTBOX.Leave
        Try
            Using connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
                Dim found As Boolean = False
                Dim cmd As New OleDbCommand("SELECT * FROM TV_CONNECTION_DETAILS WHERE CHIP_ID=@CHIP_ID", connection)
                cmd.Parameters.AddWithValue("@CHIP_ID", CUST_CHIP_ID_TEXTBOX.Text)
                Dim count As Integer = CType(cmd.ExecuteScalar(), Integer)
                connection.Close()
                If count > 0 Then
                    MessageBox2.Show("CHIP ID Already Exist", "ALERT")
                    CUST_CHIP_ID_TEXTBOX.Clear()
                    CUST_CHIP_ID_TEXTBOX.Focus()
                End If
                connection.Close()
            End Using
        Catch ex As Exception
            LogError("ADD CUSTOMER - CUST_CHIP_ID_TEXTBOX_LEAVE")
            LogError("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub CUST_EMAIL_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_EMAIL_TEXTBOX.Leave
        If CUST_EMAIL_TEXTBOX.Text = "" Then
        Else
            Dim emailRegex As New Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            If Not emailRegex.IsMatch(CUST_EMAIL_TEXTBOX.Text) Then
                MessageBox2.Show("Invalid email address. Please enter a valid email address.", "ALERT")
                CUST_EMAIL_TEXTBOX.Clear()
            End If
        End If
    End Sub
    'For Checking Entered Value Is Number Or Not'
    Private Sub CUST_MOBILE_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CUST_MOBILE_TEXTBOX.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox2.Show("Only Number Are Allowed.", "ALERT")
        End If
    End Sub
    'For Checking Entered Value Is Pincode Or Not
    Private Sub CUST_PINCODE_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CUST_PINCODE_TEXTBOX.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox2.Show("Only Number Are Allowed.", "ALERT")
        End If
    End Sub
End Class