Imports System.Data.OleDb
Public Class Edit_Customer
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf Edit_Customer_Load
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Edit_Customer_Load(sender As Object, e As EventArgs)
        AddHandler SEARCH_BTN.Click, AddressOf SEARCH_BTN_Click
        Dim cust_crf As Integer
        DOB_PICKER.MinDate = Date.Now.AddYears(-80)
        DOB_PICKER.MaxDate = Date.Now.AddYears(-18)
        EDIT_BTN.Visible = False
        If LoginType = "CUSTOMER" Then
            CUST_PASSWORD_TEXTBOX.PasswordChar = Convert.ToChar("●")
            AddHandler CUST_CRF_TEXTBOX.TextChanged, AddressOf CUST_CRF_TEXTBOX_TextChanged
            Label9.Text = "EDIT DETAILS"
            AddHandler CUST_NAME_TEXTBOX.Click, AddressOf NotAllowed
            AddHandler CUST_IDNUMBER_TEXTBOX.Click, AddressOf NotAllowed
            AddHandler CUST_IDTYPE_COMBOBOX.MouseClick, AddressOf NotAllowed
            AddHandler CUST_CHIP_ID_TEXTBOX.Click, AddressOf NotAllowed
            AddHandler CUST_CABLE_PLAN_COMBOBOX.Click, AddressOf NotAllowed
            AddHandler CUST_BROADBAND_PLAN_COMBOBOX.Click, AddressOf NotAllowed
            AddHandler CUST_USERNAME_TEXTBOX.Click, AddressOf NotAllowed
            AddHandler CUST_PASSWORD_TEXTBOX.Click, AddressOf NotAllowed
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
                CUST_CRF_TEXTBOX.ReadOnly = True

                SEARCH_BTN.Visible = False
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Fetching CRF: " & ex.Message)
                MessageBox.Show("An Error Occured While Fetching CRF: Please Contact Administrator", "ALERT")
            End Try
            SEARCH_BTN.PerformClick()
        End If
        If LoginType = "ADMIN" Then
            Label9.Text = "EDIT CUSTOMER DETAILS"
            AddHandler CUST_CRF_TEXTBOX.TextChanged, AddressOf CUST_CRF_TEXTBOX_TextChanged
        End If
        AddHandler SAVE_BTN.Click, AddressOf SAVE_BTN_Click
        AddHandler EDIT_BTN.Click, AddressOf EDIT_BTN_Click

    End Sub
    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs)
        If CUST_CRF_TEXTBOX.Text = "" Then
            EDIT_BTN.Visible = False
            clearAll()
        End If
    End Sub
    Public Sub clearAll()
        CUST_NAME_TEXTBOX.Clear()
        DOB_PICKER.Value = DOB_PICKER.MaxDate
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
        CUST_TV_CONNECTION_COMBOBOX.SelectedIndex = -1
        CUST_CABLE_PLAN_COMBOBOX.SelectedIndex = -1
        CUST_CHIP_ID_TEXTBOX.Clear()
        CUST_BROADBAND_COMBOBOX.SelectedIndex = -1
        CUST_BROADBAND_PLAN_COMBOBOX.SelectedIndex = -1
        CUST_BROADBAND_USERNAME_TEXTBOX.Clear()
        CUST_BROADBAND_PASSWORD_TEXTBOX.Clear()
        CUST_USERNAME_TEXTBOX.Clear()
        CUST_PASSWORD_TEXTBOX.Clear()
    End Sub
    Private Sub NotAllowed(sender As Object, e As EventArgs)
        If LoginType = "CUSTOMER" Then
            MessageBox.Show("Not Allowed. Contact Admin", "ALERT")
        End If
    End Sub

    Private Sub EDIT_BTN_Click(sender As Object, e As EventArgs)
        DOB_PICKER.Enabled = True
        CUST_HOUSENAME_TEXTBOX.ReadOnly = False
        CUST_AREA_TEXTBOX.ReadOnly = False
        CUST_DISTRICT_TEXTBOX.ReadOnly = False
        CUST_STATE_COMBOBOX.Enabled = True
        CUST_COUNTRY_COMBOBOX.Enabled = True
        CUST_PINCODE_TEXTBOX.ReadOnly = False
        CUST_MOBILE_TEXTBOX.ReadOnly = False
        CUST_EMAIL_TEXTBOX.ReadOnly = False
        If LoginType = "ADMIN" Then

            CUST_NAME_TEXTBOX.ReadOnly = False
            CUST_IDTYPE_COMBOBOX.Enabled = True
            CUST_IDNUMBER_TEXTBOX.ReadOnly = False
            CUST_TV_CONNECTION_COMBOBOX.Enabled = True
            CUST_CHIP_ID_TEXTBOX.ReadOnly = False
            CUST_CABLE_PLAN_COMBOBOX.Enabled = True
            CUST_BROADBAND_COMBOBOX.Enabled = True
            CUST_BROADBAND_PLAN_COMBOBOX.Enabled = True
            CUST_BROADBAND_USERNAME_TEXTBOX.ReadOnly = False
            CUST_BROADBAND_PASSWORD_TEXTBOX.ReadOnly = False
            CUST_USERNAME_TEXTBOX.ReadOnly = False
        End If
        CUST_PASSWORD_TEXTBOX.ReadOnly = False
    End Sub
    Private Sub SAVE_BTN_Click(sender As Object, e As EventArgs)
        If CUST_BROADBAND_COMBOBOX.SelectedItem = "NO" And CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Select Any Service", "ALERT")
        Else
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            connection.Open()
            If Not CUST_CRF_TEXTBOX.Text = "" Then
                Dim transaction As OleDbTransaction = connection.BeginTransaction
                Try
                    Dim command As New OleDbCommand("UPDATE CUSTOMER_DETAILS SET CUST_NAME=@CUST_NAME,CUST_DOB=@CUST_DOB,CUST_HOUSE_NAME=@CUST_HOUSE_NAME,CUST_AREA=@CUST_AREA,CUST_DISTRICT=@CUST_DISTRICT,CUST_STATE=@CUST_STATE,CUST_COUNTRY=@CUST_COUNTRY,CUST_PINCODE=@CUST_PINCODE,CUST_IDTYPE=@CUST_IDTYPE,CUST_ID_NUMBER=@CUST_ID_NUMBER,CUST_MOBILE=@CUST_MOBILE,CUST_EMAIL=@CUST_EMAIL WHERE CRF=@CRF", connection)
                    command.Parameters.AddWithValue("@CUST_NAME", CUST_NAME_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_DOB", DOB_PICKER.Value)
                    command.Parameters.AddWithValue("@CUST_HOUSE_NAME", CUST_HOUSENAME_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_AREA", CUST_AREA_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_DISTRICT", CUST_DISTRICT_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_STATE", CUST_DISTRICT_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_COUNTRY", CUST_COUNTRY_COMBOBOX.SelectedItem)
                    command.Parameters.AddWithValue("@CUST_PINCODE", CUST_PINCODE_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_IDTYPE", CUST_IDTYPE_COMBOBOX.SelectedItem)
                    command.Parameters.AddWithValue("@CUST_ID_NUMBER", CUST_CRF_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_MOBILE", CUST_MOBILE_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CUST_EMAIL", CUST_EMAIL_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    command.Transaction = transaction
                    command.ExecuteNonQuery()
                    Dim command7 As New OleDbCommand("UPDATE CUSTOMER_LOGIN_DETAILS SET CUST_USERNAME=@CUST_USERNAME, CUST_PASSWORD=@CUST_PASSWORD WHERE CRF=@CRF", connection)
                    command7.Parameters.AddWithValue("@CUST_USERNAME", CUST_USERNAME_TEXTBOX.Text)
                    command7.Parameters.AddWithValue("@CUST_PASSWORD", CUST_PASSWORD_TEXTBOX.Text)
                    command7.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    command7.Transaction = transaction
                    command7.ExecuteNonQuery()

                    If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "YES" Then
                        Dim checker As New OleDbCommand("SELECT CUST_TV_CONNECTION FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        checker.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        checker.Transaction = transaction
                        Dim checker_reader As OleDbDataReader = checker.ExecuteReader
                        If checker_reader.HasRows = True Then
                            While checker_reader.Read
                                Dim command2 As New OleDbCommand("UPDATE TV_CONNECTION_DETAILS SET CUST_TV_CONNECTION=@CUST_TV_CONNECTION,CUST_TV_PLAN=@CUST_TV_PLAN,CHIP_ID=@CHIP_ID WHERE CRF=@CRF", connection)
                                command2.Parameters.AddWithValue("@CUST_TV_CONNECTION", CUST_TV_CONNECTION_COMBOBOX.SelectedItem)
                                command2.Parameters.AddWithValue("@CUST_TV_PLAN", CUST_CABLE_PLAN_COMBOBOX.SelectedItem)
                                command2.Parameters.AddWithValue("@CHIP_ID", CUST_CHIP_ID_TEXTBOX.Text)
                                command2.Parameters.AddWithValue("@CRF", CUST_CHIP_ID_TEXTBOX.Text)
                                command2.Transaction = transaction
                                command2.ExecuteNonQuery()
                            End While
                        Else
                            Dim tv_con_adder As New OleDbCommand("INSERT INTO TV_CONNECTION_DETAILS (CRF,CUST_TV_CONNECTION,CUST_TV_PLAN,CHIP_ID,REGISTRATION_DATE,LAST_RENEWAL_DATE,EXPIRY_DATE,TV_CONNECTION_STATUS) VALUES (@CRF,@CUST_TV_CONNECTION,@CUST_TV_PLAN,@CHIP_ID,@REGISTRATION_DATE,@LAST_RENEWAL_DATE,@EXPIRY_DATE,@TV_CONNECTION_STATUS)", connection)
                            tv_con_adder.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            tv_con_adder.Parameters.AddWithValue("@CUST_TV_CONNECTION", "YES")
                            tv_con_adder.Parameters.AddWithValue("@CUST_TV_PLAN", CUST_CABLE_PLAN_COMBOBOX.SelectedItem)
                            tv_con_adder.Parameters.AddWithValue("@CHIP_ID", CUST_CHIP_ID_TEXTBOX.Text)
                            tv_con_adder.Parameters.AddWithValue("@REGISTRATION_DATE", Date.Today)
                            tv_con_adder.Parameters.AddWithValue("@LAST_RENEWAL_DATE", Date.Today)
                            Dim expiry As Date = Date.Today.AddDays(30)
                            tv_con_adder.Parameters.AddWithValue("@EXPIRY_DATE", expiry)
                            tv_con_adder.Parameters.AddWithValue("@TV_CONNECTION_STATUS", "INACTIVE")
                            tv_con_adder.Transaction = transaction
                            tv_con_adder.ExecuteNonQuery()
                            Dim tv_payment_adder As New OleDbCommand("INSERT INTO TV_PAYMENT_DETAILS (CRF,PAYMENT_YEAR) VALUES (@CRF,@PAYMENT_YEAR)", connection)
                            tv_payment_adder.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            MessageBox.Show(Date.Today.Year)
                            tv_payment_adder.Parameters.AddWithValue("@PAYMENT_YEAR", Date.Today.Year)
                            tv_payment_adder.Transaction = transaction
                            tv_payment_adder.ExecuteNonQuery()
                        End If
                    Else
                        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
                            Dim command3 As New OleDbCommand("DELETE * FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                            command3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            command3.Transaction = transaction
                            command3.ExecuteNonQuery()
                            Dim command6 As New OleDbCommand("DELETE * FROM TV_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                            command6.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            command6.Transaction = transaction
                            command6.ExecuteNonQuery()
                        End If
                    End If
                    If CUST_BROADBAND_COMBOBOX.SelectedItem = "YES" Then
                        Dim checker2 As New OleDbCommand("SELECT BROADBAND_CONNECTION FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        checker2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        checker2.Transaction = transaction
                        Dim checker_reader2 As OleDbDataReader = checker2.ExecuteReader
                        If checker_reader2.HasRows = True Then
                            While checker_reader2.Read
                                Dim command4 As New OleDbCommand("UPDATE BROADBAND_CONNECTION_DETAILS SET BROADBAND_CONNECTION=@BROADBAND_CONNECTION, CURRENT_PLAN=@CURRENT_PLAN WHERE CRF=@CRF", connection)
                                command4.Parameters.AddWithValue("@BROADBAND_CONNECTION", CUST_BROADBAND_COMBOBOX.SelectedItem)
                                command4.Parameters.AddWithValue("@CURRENT_PLAN", CUST_BROADBAND_PLAN_COMBOBOX.SelectedItem)
                                command4.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                command4.Transaction = transaction
                                command4.ExecuteNonQuery()
                                Dim command6 As New OleDbCommand("UPDATE BROADBAND_LOGIN SET CUST_BROADBAND_USERNAME=@CUST_BROADBAND_USERNAME, CUST_BROADBAND_PASSWORD=@CUST_BROADBAND_PASSWORD WHERE CRF=@CRF", connection)
                                command6.Parameters.AddWithValue("@CUST_BROADBAND_USERNAME", CUST_BROADBAND_USERNAME_TEXTBOX.Text)
                                command6.Parameters.AddWithValue("@CUST_BROADBAND_PASSWORD", CUST_BROADBAND_PASSWORD_TEXTBOX.Text)
                                command6.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                                command6.Transaction = transaction
                                command6.ExecuteNonQuery()
                            End While
                        Else
                            Dim broadband_con_adder As New OleDbCommand("INSERT INTO BROADBAND_CONNECTION_DETAILS (CRF,REGISTRATION_DATE,LAST_RENEWAL_DATE,EXPIRY_DATE,STATUS,RECHARGED_BY,CURRENT_PLAN,BROADBAND_CONNECTION) VALUES (@CRF,@REGISTRATION_DATE,@LAST_RENEWAL_DATE,@EXPIRY_DATE,@STATUS,@RECHARGED_BY,@CURRENT_PLAN,@BROADBAND_CONNECTION)", connection)
                            broadband_con_adder.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            broadband_con_adder.Parameters.AddWithValue("@REGISTRATION_DATE", Date.Today)
                            broadband_con_adder.Parameters.AddWithValue("@LAST_RENEWAL_DATE", Date.Today)
                            Dim expiry As Date = Date.Today.AddDays(30)
                            broadband_con_adder.Parameters.AddWithValue("@EXPIRY_DATE", expiry)
                            broadband_con_adder.Parameters.AddWithValue("@STATUS", "INACTIVE")
                            broadband_con_adder.Parameters.AddWithValue("@RECHARGED_BY", "NILL")
                            broadband_con_adder.Parameters.AddWithValue("@CURRENT_PLAN", CUST_BROADBAND_PLAN_COMBOBOX.SelectedItem)
                            broadband_con_adder.Parameters.AddWithValue("@BROADBAND_CONNECTION", "YES")
                            broadband_con_adder.Transaction = transaction
                            broadband_con_adder.ExecuteNonQuery()
                            Dim BroadBand_Payment_Adder As New OleDbCommand("INSERT INTO BROADBAND_PAYMENT_DETAILS (CRF,PAYMENT_YEAR) VALUES (@CRF,@PAYMENT_YEAR)", connection)
                            BroadBand_Payment_Adder.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            BroadBand_Payment_Adder.Parameters.AddWithValue("@PAYMENT_YEAR", Date.Today.Year)
                            BroadBand_Payment_Adder.Transaction = transaction
                            BroadBand_Payment_Adder.ExecuteNonQuery()
                            Dim BroadBand_Login_Adder As New OleDbCommand("INSERT INTO BROADBAND_LOGIN (CRF,CUST_BROADBAND_USERNAME,CUST_BROADBAND_PASSWORD) VALUES (@CRF,@CUST_BROADBAND_USERNAME,@CUST_BROADBAND_PASSWORD)", connection)
                            BroadBand_Login_Adder.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            BroadBand_Login_Adder.Parameters.AddWithValue("@CUST_BROADBAND_USERNAME", CUST_BROADBAND_USERNAME_TEXTBOX.Text)
                            BroadBand_Login_Adder.Parameters.AddWithValue("@CUST_BROADBAND_PASSWORD", CUST_BROADBAND_PASSWORD_TEXTBOX.Text)
                            BroadBand_Login_Adder.Transaction = transaction
                            BroadBand_Login_Adder.ExecuteNonQuery()
                        End If
                    Else
                        If CUST_BROADBAND_COMBOBOX.SelectedItem = "NO" Then
                            Dim command11 As New OleDbCommand("DELETE * FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                            command11.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            command11.Transaction = transaction
                            command11.ExecuteNonQuery()
                            Dim command9 As New OleDbCommand("DELETE * FROM BROADBAND_LOGIN WHERE CRF=@CRF", connection)
                            command9.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            command9.Transaction = transaction
                            command9.ExecuteNonQuery()
                            Dim command8 As New OleDbCommand("DELETE * FROM BROADBAND_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                            command8.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            command8.Transaction = transaction
                            command8.ExecuteNonQuery()
                        End If
                    End If
                    transaction.Commit()
                    MessageBox.Show("Customer Details Updated Successfully.", "ALERT")
                    My.Forms.Admin_Dashboard.CUST_DETAILS_BTN.PerformClick()
                Catch ex As Exception
                    transaction.Rollback()
                    ErrorAlert.Play()
                    LogError("An Error Occured While Updating Data: " & ex.Message)
                    MessageBox.Show("An Error Occured While Updating Data: Check Log For More Details.", "ALERT")
                Finally
                    connection.Close()
                End Try
            Else
                ErrorAlert.Play()
                MessageBox.Show("Please Enter CRF Number.", "ALERT")
            End If
        End If
    End Sub

    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs)
        CUST_COUNTRY_COMBOBOX.Items.Clear()
        CUST_STATE_COMBOBOX.Items.Clear()
        If CUST_CRF_TEXTBOX.Text = "" Then
            MessageBox.Show("Please Enter CRF.", "ALERT")
        Else
            clearAll()
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            Try
                connection.Open()
                Dim command As New OleDbCommand("SELECT CUST_NAME,CUST_DOB,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_COUNTRY,CUST_PINCODE,CUST_IDTYPE,CUST_ID_NUMBER,CUST_MOBILE,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF=@CRF", connection)
                Dim command2 As New OleDbCommand("SELECT CUST_TV_CONNECTION,CUST_TV_PLAN,CHIP_ID FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                Dim command3 As New OleDbCommand("SELECT BROADBAND_CONNECTION,CURRENT_PLAN FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                Dim command4 As New OleDbCommand("SELECT CUST_BROADBAND_USERNAME,CUST_BROADBAND_PASSWORD FROM BROADBAND_LOGIN WHERE CRF=@CRF", connection)
                Dim command5 As New OleDbCommand("SELECT CUST_USERNAME,CUST_PASSWORD FROM CUSTOMER_LOGIN_DETAILS WHERE CRF=@CRF", connection)
                command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command4.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                command5.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                Dim Reader As OleDbDataReader = command.ExecuteReader
                Dim Reader2 As OleDbDataReader = command2.ExecuteReader
                Dim Reader3 As OleDbDataReader = command3.ExecuteReader
                Dim Reader4 As OleDbDataReader = command4.ExecuteReader
                Dim Reader5 As OleDbDataReader = command5.ExecuteReader
                If Reader.HasRows = True Then
                    EDIT_BTN.Visible = True
                    While Reader.Read
                        CUST_NAME_TEXTBOX.Text = Reader.GetString(0)
                        DOB_PICKER.Value = Reader.GetDateTime(1)
                        CUST_HOUSENAME_TEXTBOX.Text = Reader.GetString(2)
                        CUST_AREA_TEXTBOX.Text = Reader.GetString(3)
                        CUST_DISTRICT_TEXTBOX.Text = Reader.GetString(4)
                        CUST_STATE_COMBOBOX.Items.Add(Reader.GetString(5))
                        CUST_STATE_COMBOBOX.SelectedItem = Reader.GetString(5)
                        CUST_COUNTRY_COMBOBOX.Items.Add(Reader.GetString(6))
                        CUST_COUNTRY_COMBOBOX.SelectedItem = Reader.GetString(6)
                        CUST_PINCODE_TEXTBOX.Text = Reader.GetInt32(7)
                        CUST_IDTYPE_COMBOBOX.SelectedItem = Reader.GetString(8)
                        CUST_IDNUMBER_TEXTBOX.Text = Reader.GetString(9)
                        CUST_MOBILE_TEXTBOX.Text = Reader.GetDouble(10)
                        CUST_EMAIL_TEXTBOX.Text = Reader.GetString(11)
                    End While
                    If Reader2.HasRows = True Then
                        While Reader2.Read
                            If Reader2.GetString(0) = "YES" Then
                                CUST_CHIP_ID_LABEL.Visible = True
                                CUST_CHIP_ID_TEXTBOX.Visible = True
                                CUST_CABLE_PLAN_LABEL.Visible = True
                                CUST_CABLE_PLAN_COMBOBOX.Visible = True
                                CUST_TV_CONNECTION_COMBOBOX.SelectedItem = Reader2.GetString(0)
                                CUST_CABLE_PLAN_COMBOBOX.SelectedItem = Reader2.GetString(1)
                                CUST_CHIP_ID_TEXTBOX.Text = Reader2.GetString(2)
                            Else

                            End If
                        End While
                    Else
                        CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO"
                        CUST_CHIP_ID_LABEL.Visible = False
                        CUST_CHIP_ID_TEXTBOX.Visible = False
                        CUST_CABLE_PLAN_LABEL.Visible = False
                        CUST_CABLE_PLAN_COMBOBOX.Visible = False
                    End If
                    If Reader3.HasRows = True Then
                        While Reader3.Read
                            If Reader3.GetString(0) = "YES" Then
                                CUST_BROADBAND_PLAN_LABEL.Visible = True
                                CUST_BROADBAND_PLAN_COMBOBOX.Visible = True
                                CUST_BROADBAND_USERNAME_LABEL.Visible = True
                                CUST_BROADBAND_USERNAME_TEXTBOX.Visible = True
                                CUST_BROADBAND_PASSWORD_LABEL.Visible = True
                                CUST_BROADBAND_PASSWORD_TEXTBOX.Visible = True
                                CUST_BROADBAND_COMBOBOX.SelectedItem = Reader3.GetString(0)
                                CUST_BROADBAND_PLAN_COMBOBOX.SelectedItem = Reader3.GetString(1)
                                If Reader4.HasRows = True Then
                                    While Reader4.Read
                                        CUST_BROADBAND_USERNAME_TEXTBOX.Text = Reader4.GetString(0)
                                        CUST_BROADBAND_PASSWORD_TEXTBOX.Text = Reader4.GetString(1)
                                    End While
                                End If
                            Else

                            End If
                        End While
                    Else
                        CUST_BROADBAND_COMBOBOX.SelectedItem = "NO"
                        CUST_BROADBAND_PLAN_LABEL.Visible = False
                        CUST_BROADBAND_PLAN_COMBOBOX.Visible = False
                        CUST_BROADBAND_USERNAME_LABEL.Visible = False
                        CUST_BROADBAND_USERNAME_TEXTBOX.Visible = False
                        CUST_BROADBAND_PASSWORD_LABEL.Visible = False
                        CUST_BROADBAND_PASSWORD_TEXTBOX.Visible = False
                    End If
                    If Reader5.HasRows = True Then
                        While Reader5.Read
                            CUST_USERNAME_TEXTBOX.Text = Reader5.GetString(0)
                            CUST_PASSWORD_TEXTBOX.Text = Reader5.GetString(1)
                        End While
                    End If
                Else
                    ErrorAlert.Play()
                    MessageBox.Show("CRF Not Exist", "ALERT")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub CUST_BROADBAND_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs)
        If CUST_BROADBAND_COMBOBOX.SelectedItem = "YES" Then
            CUST_BROADBAND_PLAN_LABEL.Visible = True
            CUST_BROADBAND_PLAN_COMBOBOX.Visible = True
            CUST_BROADBAND_USERNAME_LABEL.Visible = True
            CUST_BROADBAND_USERNAME_TEXTBOX.Visible = True
            CUST_BROADBAND_PASSWORD_LABEL.Visible = True
            CUST_BROADBAND_PASSWORD_TEXTBOX.Visible = True
        End If
        If CUST_BROADBAND_COMBOBOX.SelectedItem = "NO" Then
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            connection.Open()
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
            If pendingPayments = 0 Then
                CUST_BROADBAND_PLAN_LABEL.Visible = False
                CUST_BROADBAND_PLAN_COMBOBOX.Visible = False
                CUST_BROADBAND_USERNAME_LABEL.Visible = False
                CUST_BROADBAND_USERNAME_TEXTBOX.Visible = False
                CUST_BROADBAND_PASSWORD_LABEL.Visible = False
                CUST_BROADBAND_PASSWORD_TEXTBOX.Visible = False
            Else
                ErrorAlert.Play()
                MessageBox.Show("Customer Has Pending Amount. Not Allowed To Disconnect.", "ALERT")
                CUST_BROADBAND_COMBOBOX.SelectedItem = "YES"
            End If
        End If
    End Sub
    Private Sub CUST_TV_CONNECTION_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs)
        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "YES" Then
            CUST_CHIP_ID_LABEL.Visible = True
            CUST_CHIP_ID_TEXTBOX.Visible = True
            CUST_CABLE_PLAN_LABEL.Visible = True
            CUST_CABLE_PLAN_COMBOBOX.Visible = True
        End If
        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            connection.Open()
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
            Dim command10 As New OleDbCommand(query, connection)
            command10.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
            command10.Parameters.AddWithValue("@YEAR", Date.Today.Year)
            Dim reader5 As OleDbDataReader = command10.ExecuteReader()
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
            If pendingPayments = 0 Then
                CUST_CHIP_ID_LABEL.Visible = False
                CUST_CHIP_ID_TEXTBOX.Visible = False
                CUST_CABLE_PLAN_LABEL.Visible = False
                CUST_CABLE_PLAN_COMBOBOX.Visible = False
            Else
                ErrorAlert.Play()
                MessageBox.Show("Customer Has Pending Amount. Not Allowed To Disconnect.", "ALERT")
                CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "YES"
            End If
        End If
    End Sub
    Private Sub CUST_PINCODE_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Number Are Allowed.", "ALERT")
        End If
    End Sub
    Private Sub CUST_NAME_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Letters Are Allowed.", "ALERT")
        End If
    End Sub
    Private Sub CUST_HOUSE_NAME_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Letters Are Allowed.", "ALERT")
        End If
    End Sub
    Private Sub CUST_AREA_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Letters Are Allowed.", "ALERT")
        End If
    End Sub
    Private Sub CUST_DISTRICT_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Letters Are Allowed.", "ALERT")
        End If
    End Sub
End Class