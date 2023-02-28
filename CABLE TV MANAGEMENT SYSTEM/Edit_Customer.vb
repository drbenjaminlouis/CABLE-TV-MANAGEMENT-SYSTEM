Imports System.Data.OleDb
Public Class Edit_Customer
    Private Sub Edit_Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DOB_PICKER.MinDate = Date.Now.AddYears(-80)
        DOB_PICKER.MaxDate = Date.Now.AddYears(-18)
        EDIT_BTN.Visible = False
    End Sub
    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.Leave
        If CUST_CRF_TEXTBOX.Text = "" Then
            EDIT_BTN.Visible = False
            clearAll()
        Else
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
                If Reader.HasRows Then
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
                    If Reader2.HasRows Then
                        While Reader2.Read
                            If Reader2.GetString(0) = "YES" Then
                                CUST_TV_CONNECTION_COMBOBOX.SelectedItem = Reader2.GetString(0)
                                CUST_CABLE_PLAN_COMBOBOX.SelectedItem = Reader2.GetString(1)
                                CUST_CHIP_ID_TEXTBOX.Text = Reader2.GetString(2)
                            Else
                                CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO"
                            End If
                        End While
                    End If
                    If Reader3.HasRows Then
                        While Reader3.Read
                            If Reader3.GetString(0) = "YES" Then
                                CUST_BROADBAND_COMBOBOX.SelectedItem = Reader3.GetString(0)
                                CUST_BROADBAND_PLAN_COMBOBOX.SelectedItem = Reader3.GetString(1)
                                If Reader4.HasRows Then
                                    While Reader4.Read
                                        CUST_BROADBAND_USERNAME_TEXTBOX.Text = Reader4.GetString(0)
                                        CUST_BROADBAND_PASSWORD_TEXTBOX.Text = Reader4.GetString(1)
                                    End While
                                End If
                            Else
                                CUST_BROADBAND_COMBOBOX.SelectedItem = "NO"
                            End If
                        End While
                    End If
                    If Reader5.HasRows Then
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

    Private Sub EDIT_BTN_Click(sender As Object, e As EventArgs) Handles EDIT_BTN.Click
        CUST_NAME_TEXTBOX.ReadOnly = False
        DOB_PICKER.Enabled = True
        CUST_HOUSENAME_TEXTBOX.ReadOnly = False
        CUST_AREA_TEXTBOX.ReadOnly = False
        CUST_DISTRICT_TEXTBOX.ReadOnly = False
        CUST_STATE_COMBOBOX.Enabled = True
        CUST_COUNTRY_COMBOBOX.Enabled = True
        CUST_PINCODE_TEXTBOX.ReadOnly = False
        CUST_IDTYPE_COMBOBOX.Enabled = True
        CUST_IDNUMBER_TEXTBOX.ReadOnly = False
        CUST_MOBILE_TEXTBOX.ReadOnly = False
        CUST_EMAIL_TEXTBOX.ReadOnly = False
        CUST_TV_CONNECTION_COMBOBOX.Enabled = True
        CUST_CABLE_PLAN_COMBOBOX.Enabled = True
        CUST_CHIP_ID_TEXTBOX.ReadOnly = False
        CUST_BROADBAND_COMBOBOX.Enabled = True
        CUST_BROADBAND_PLAN_COMBOBOX.Enabled = True
        CUST_BROADBAND_USERNAME_TEXTBOX.ReadOnly = False
        CUST_BROADBAND_PASSWORD_TEXTBOX.ReadOnly = False
        CUST_USERNAME_TEXTBOX.ReadOnly = False
        CUST_PASSWORD_TEXTBOX.ReadOnly = False
    End Sub

    Private Sub SAVE_BTN_Click(sender As Object, e As EventArgs) Handles SAVE_BTN.Click
        If CUST_BROADBAND_COMBOBOX.SelectedItem = "NO" And CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Select Any Service", "ALERT")
        Else
            If Not CUST_CRF_TEXTBOX.Text = "" Then
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
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
                        Dim command2 As New OleDbCommand("UPDATE TV_CONNECTION_DETAILS SET CUST_TV_CONNECTION=@CUST_TV_CONNECTION,CUST_TV_PLAN=@CUST_TV_PLAN,CHIP_ID=@CHIP_ID WHERE CRF=@CRF", connection)
                        command2.Parameters.AddWithValue("@CUST_TV_CONNECTION", CUST_TV_CONNECTION_COMBOBOX.SelectedItem)
                        command2.Parameters.AddWithValue("@CUST_TV_PLAN", CUST_CABLE_PLAN_COMBOBOX.SelectedItem)
                        command2.Parameters.AddWithValue("@CHIP_ID", CUST_CHIP_ID_TEXTBOX.Text)
                        command2.Parameters.AddWithValue("@CRF", CUST_CHIP_ID_TEXTBOX.Text)
                        command2.Transaction = transaction
                        command2.ExecuteNonQuery()
                    Else
                        If CUST_TV_CONNECTION_COMBOBOX.SelectedItem = "NO" Then
                            Dim command3 As New OleDbCommand("DELETE * FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                            command3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                            command3.Transaction = transaction
                            command3.ExecuteNonQuery()
                        End If
                    End If
                    If CUST_BROADBAND_COMBOBOX.SelectedItem = "YES" Then
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
                    Else
                        Dim command5 As New OleDbCommand("DELETE * FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        command5.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        command5.Transaction = transaction
                        command5.ExecuteNonQuery()
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
End Class