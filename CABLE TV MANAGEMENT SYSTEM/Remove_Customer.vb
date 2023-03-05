Imports System.Data.OleDb
Imports System.Diagnostics.Eventing
Imports System.Globalization

Public Class Remove_Customer
    Private Sub Remove_Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TV_Reg_Picker.MaxDate = Date.Today
        BROADBAND_REG_DATE.MaxDate = Date.Today
        DOB_PICKER.MinDate = DateTime.Today.AddYears(-80)
        DOB_PICKER.MaxDate = DateTime.Today.AddYears(-18)
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not CUST_CRF_TEXTBOX.Text = "" Then
            If e.KeyCode = Keys.Enter Then
                ' Simulate a button click
                SEARCH_BTN.PerformClick()
            End If
        End If
    End Sub
    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        If CUST_CRF_TEXTBOX.Text = "" Then
            REMOVEBTN.Enabled = False
            EDITID_BTN.Enabled = False
            ErrorAlert.Play()
            MessageBox.Show("Please Enter CRF Number.", "ALERT")
        Else
            Dim regDate As Date = BROADBAND_REG_DATE.Value
            Dim previousYear As Integer = 2022

            Dim currentYear As Integer = Date.Today.Year()
            Dim currentMonth As Integer = DateTime.Now.Month
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            Try
                connection.Open()
                Dim command As New OleDbCommand("SELECT CUST_NAME,CUST_DOB,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_COUNTRY,CUST_PINCODE,CUST_IDTYPE,CUST_ID_NUMBER,CUST_MOBILE,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF=@CRF", connection)
                Dim command2 As New OleDbCommand("SELECT CUST_TV_CONNECTION,CUST_TV_PLAN,CHIP_ID,REGISTRATION_DATE FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                Dim command3 As New OleDbCommand("SELECT BROADBAND_CONNECTION,CURRENT_PLAN,REGISTRATION_DATE FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                Dim command4 As New OleDbCommand("SELECT CUST_BROADBAND_USERNAME,CUST_BROADBAND_PASSWORD FROM BROADBAND_LOGIN WHERE CRF=@CRF", connection)
                command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command4.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                Dim Reader As OleDbDataReader = command.ExecuteReader
                Dim Reader2 As OleDbDataReader = command2.ExecuteReader
                Dim Reader3 As OleDbDataReader = command3.ExecuteReader
                Dim Reader4 As OleDbDataReader = command4.ExecuteReader
                If Reader.HasRows Then
                    REMOVEBTN.Enabled = True
                    EDITID_BTN.Enabled = True
                    REMOVEBTN.Visible = True
                    EDITID_BTN.Visible = True
                    While Reader.Read
                        CUST_NAME_TEXTBOX.Text = Reader.GetString(0)
                        DOB_PICKER.Value = Reader.GetDateTime(1)
                        CUST_HOUSENAME_TEXTBOX.Text = Reader.GetString(2)
                        CUST_AREA_TEXTBOX.Text = Reader.GetString(3)
                        CUST_DISTRICT_TEXTBOX.Text = Reader.GetString(4)
                        CUST_STATE_TEXTBOX.Text = Reader.GetString(5)
                        CUST_COUNTRY_TEXTBOX.Text = Reader.GetString(6)
                        CUST_PINCODE_TEXTBOX.Text = Reader.GetInt32(7)
                        CUST_IDTYPE_TEXTBOX.Text = Reader.GetString(8)
                        CUST_IDNUMBER_TEXTBOX.Text = Reader.GetString(9)
                        CUST_MOBILE_TEXTBOX.Text = Reader.GetDouble(10)
                        CUST_EMAIL_TEXTBOX.Text = Reader.GetString(11)
                    End While
                    If Reader2.HasRows Then
                        While Reader2.Read
                            If Reader2.GetString(0) = "YES" Then
                                TV_CONNECTION.Text = Reader2.GetString(0)
                                CABLE_PLAN.Text = Reader2.GetString(1)
                                CUST_CHIP_ID_TEXTBOX.Text = Reader2.GetString(2)
                                TV_Reg_Picker.Value = Reader2.GetDateTime(3)
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

                                Dim command5 As New OleDbCommand(query, connection)
                                command5.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                command5.Parameters.AddWithValue("@YEAR", Date.Today.Year)
                                Dim reader5 As OleDbDataReader = command5.ExecuteReader()
                                Dim pendingPayments As Integer = 0
                                ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid and update pending amount.
                                If reader5.HasRows Then
                                    reader5.Read()
                                    If reader5("january") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("february") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("march") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("april") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("may") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("june") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("july") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("august") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("september") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("october") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("november") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("december") = 1 Then

                                        pendingPayments += 250
                                    End If
                                End If
                                'Assigning value pendingpayments to pending amount textbox
                                TV_PENDING.Text = pendingPayments
                            Else
                                TV_CONNECTION.Text = "NO"
                            End If
                        End While
                    End If
                    If Reader3.HasRows Then
                        While Reader3.Read
                            If Reader3.GetString(0) = "YES" Then
                                BROADBAND_CONNECTION.Text = Reader3.GetString(0)
                                BROADBAND_PLAN.Text = Reader3.GetString(1)
                                BROADBAND_REG_DATE.Value = Reader3.GetDateTime(2)
                                If Reader4.HasRows Then
                                    While Reader4.Read
                                        CUST_BROADBAND_USERNAME_TEXTBOX.Text = Reader4.GetString(0)
                                        CUST_BROADBAND_PASSWORD_TEXTBOX.Text = Reader4.GetString(1)
                                    End While
                                End If
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

                                Dim command5 As New OleDbCommand(query, connection)
                                command5.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                command5.Parameters.AddWithValue("@YEAR", Date.Today.Year)
                                Dim reader5 As OleDbDataReader = command5.ExecuteReader()
                                Dim pendingPayments As Integer = 0
                                ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid and update pending amount.
                                If reader5.HasRows Then
                                    reader5.Read()
                                    If reader5("january") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("february") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("march") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("april") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("may") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("june") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("july") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("august") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("september") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("october") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("november") = 1 Then

                                        pendingPayments += 250
                                    End If
                                    If reader5("december") = 1 Then

                                        pendingPayments += 250
                                    End If
                                End If
                                'Assigning value pendingpayments to pending amount textbox
                                BROADBAND_PENDING.Text = pendingPayments
                            Else
                                BROADBAND_CONNECTION.Text = "NO"
                            End If
                        End While
                    End If
                Else
                    ErrorAlert.Play()
                    MessageBox.Show("CRF Not Exist", "ALERT")
                    REMOVEBTN.Visible = False
                    EDITID_BTN.Visible = False
                End If
            Catch ex As Exception
                LogError("An Error Occured While Fetching Data: " & ex.Message)
                MessageBox.Show("An Error Occured: Check Log For More Details")
            End Try
        End If
    End Sub
    Public Sub clearAll()
        CUST_NAME_TEXTBOX.Clear()
        DOB_PICKER.Value = DOB_PICKER.MaxDate
        CUST_HOUSENAME_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_DISTRICT_TEXTBOX.Clear()
        CUST_STATE_TEXTBOX.Clear()
        CUST_COUNTRY_TEXTBOX.Clear()
        CUST_PINCODE_TEXTBOX.Clear()
        CUST_IDTYPE_TEXTBOX.Clear()
        CUST_IDNUMBER_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()
        CUST_EMAIL_TEXTBOX.Clear()
        TV_CONNECTION.Clear()
        CABLE_PLAN.Clear()
        CUST_CHIP_ID_TEXTBOX.Clear()
        TV_Reg_Picker.Value = TV_Reg_Picker.MaxDate
        TV_PENDING.Clear()
        BROADBAND_CONNECTION.Clear()
        BROADBAND_PLAN.Clear()
        BROADBAND_REG_DATE.Value = BROADBAND_REG_DATE.MaxDate
        CUST_BROADBAND_USERNAME_TEXTBOX.Clear()
        CUST_BROADBAND_PASSWORD_TEXTBOX.Clear()
        BROADBAND_PENDING.Clear()
    End Sub
    Private Sub TV_CONNECTION_TextChanged(sender As Object, e As EventArgs) Handles TV_CONNECTION.TextChanged
        If TV_CONNECTION.Text = "NO" Then
            CABLE_PLAN.Visible = False
            CUST_CABLE_PLAN_LABEL.Visible = False
            CUST_CHIP_ID_TEXTBOX.Visible = False
            CUST_CHIP_ID_LABEL.Visible = False
            TV_Reg_Picker.Visible = False
            TV_Reg_Label.Visible = False
            TV_PENDING.Visible = False
            Tv_pending_label.Visible = False
        Else
            CABLE_PLAN.Visible = True
            CUST_CABLE_PLAN_LABEL.Visible = True
            CUST_CHIP_ID_TEXTBOX.Visible = True
            CUST_CHIP_ID_LABEL.Visible = True
            TV_Reg_Picker.Visible = True
            TV_Reg_Label.Visible = True
            TV_PENDING.Visible = True
            Tv_pending_label.Visible = True
        End If
    End Sub

    Private Sub BROADBAND_CONNECTION_TextChanged(sender As Object, e As EventArgs) Handles BROADBAND_CONNECTION.TextChanged
        If BROADBAND_CONNECTION.Text = "NO" Then
            BROADBAND_PLAN.Visible = False
            CUST_BROADBAND_PLAN_LABEL.Visible = False
            CUST_BROADBAND_USERNAME_TEXTBOX.Visible = False
            CUST_BROADBAND_USERNAME_LABEL.Visible = False
            CUST_BROADBAND_PASSWORD_TEXTBOX.Visible = False
            CUST_BROADBAND_PASSWORD_LABEL.Visible = False
            BROADBAND_REG_DATE.Visible = False
            BB_REG_DATE_LABEL.Visible = False
            BROADBAND_PENDING.Visible = False
            broadband_pending_label.Visible = False
        Else
            BROADBAND_PLAN.Visible = True
            CUST_BROADBAND_PLAN_LABEL.Visible = True
            CUST_BROADBAND_USERNAME_TEXTBOX.Visible = True
            CUST_BROADBAND_USERNAME_LABEL.Visible = True
            CUST_BROADBAND_PASSWORD_TEXTBOX.Visible = True
            CUST_BROADBAND_PASSWORD_LABEL.Visible = True
            BROADBAND_REG_DATE.Visible = True
            BB_REG_DATE_LABEL.Visible = True
            BROADBAND_PENDING.Visible = True
            broadband_pending_label.Visible = True
        End If
    End Sub

    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.TextChanged
        If CUST_CRF_TEXTBOX.Text = "" Then
            REMOVEBTN.Visible = False
            EDITID_BTN.Visible = False
            clearAll()
        Else

        End If
    End Sub

    Private Sub EDITID_BTN_Click(sender As Object, e As EventArgs) Handles EDITID_BTN.Click
        CUST_CRF_TEXTBOX.Clear()
        CUST_CRF_TEXTBOX.Focus()
    End Sub

    Private Sub REMOVEBTN_Click(sender As Object, e As EventArgs) Handles REMOVEBTN.Click


        If TV_PENDING.Text > 0 Or BROADBAND_PENDING.Text > 0 Then
            MessageBox.Show("User With Pending Amount Cannot Be Removed.", "ALERT")
        Else
            Dim result = MessageBox2.Show("", "Are you sure you want to remove?")
            If result = DialogResult.Yes Then
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
                Dim transaction As OleDbTransaction = connection.BeginTransaction()
                Try
                    If BROADBAND_CONNECTION.Text = "YES" Then
                        Dim broadband_login_delete As New OleDbCommand("DELETE * FROM BROADBAND_LOGIN WHERE CRF=@CRF", connection)
                        broadband_login_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        broadband_login_delete.Transaction = transaction
                        broadband_login_delete.ExecuteNonQuery()

                        Dim broadband_payment_delete As New OleDbCommand("DELETE * FROM BROADBAND_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                        broadband_payment_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        broadband_payment_delete.Transaction = transaction
                        broadband_payment_delete.ExecuteNonQuery()

                        Dim broadband_connection_delete As New OleDbCommand("DELETE * FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        broadband_connection_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        broadband_connection_delete.Transaction = transaction
                        broadband_connection_delete.ExecuteNonQuery()
                    End If
                    If TV_CONNECTION.Text = "YES" Then
                        Dim cust_login_delete As New OleDbCommand("DELETE * FROM CUSTOMER_LOGIN_DETAILS WHERE CRF=@CRF", connection)
                        cust_login_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        cust_login_delete.Transaction = transaction
                        cust_login_delete.ExecuteNonQuery()

                        Dim tv_payment_delete As New OleDbCommand("DELETE * FROM TV_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                        tv_payment_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        tv_payment_delete.Transaction = transaction
                        tv_payment_delete.ExecuteNonQuery()

                        Dim tv_connection_delete As New OleDbCommand("DELETE * FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        tv_connection_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        tv_connection_delete.Transaction = transaction
                        tv_connection_delete.ExecuteNonQuery()
                    End If
                    Dim invoice_delete As New OleDbCommand("DELETE * FROM INVOICE_DETAILS WHERE CRF=@CRF", connection)
                    invoice_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    invoice_delete.Transaction = transaction
                    invoice_delete.ExecuteNonQuery()

                    Dim cust_details_delete As New OleDbCommand("DELETE * FROM CUSTOMER_DETAILS WHERE CRF=@CRF", connection)
                    cust_details_delete.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    cust_details_delete.Transaction = transaction
                    cust_details_delete.ExecuteNonQuery()
                    transaction.Commit()
                    clearAll()
                    MessageBox.Show("Customer Removed Successfully.")
                Catch ex As Exception
                    transaction.Rollback()
                    ErrorAlert.Play()
                    LogError("An Error Occured While Deleting: " & ex.Message)
                    MessageBox.Show("An Error Occured While Deleting: Check Log For Details.", "ALERT")
                Finally
                    connection.Close()
                End Try
            Else
            End If
        End If
    End Sub
End Class