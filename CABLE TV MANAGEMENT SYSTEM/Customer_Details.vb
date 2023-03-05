Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports Guna.UI2.WinForms

Public Class Customer_Details
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf Customer_Details_Load
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Customer_Details_Load(sender As Object, e As EventArgs)
        DOB_PICKER.MinDate = DateTime.Today.AddYears(-80)
        DOB_PICKER.MaxDate = DateTime.Today.AddYears(-18)
        DOB_PICKER.Value = DateTime.Today.AddYears(-18)
        Dim cust_crf As Integer
        If LoginType = "CUSTOMER" Then
            CUST_CRF_TEXTBOX.ReadOnly = True
            AddHandler CUST_CRF_TEXTBOX.TextChanged, AddressOf CUST_CRF_TEXTBOX_TextChanged
            AddHandler EDIT_BTN.Click, AddressOf EDIT_BTN_Click
            AddHandler CLOSE_BTN.Click, AddressOf CLOSE_BTN_Click
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
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Fetching CRF: " & ex.Message)
                MessageBox.Show("An Error Occured While Fetching CRF: Please Contact Administrator", "ALERT")
            End Try
        End If
    End Sub
    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs)
        If CUST_CRF_TEXTBOX.Text = "" Then
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
                If Reader.HasRows = True Then
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

    Private Sub EDIT_BTN_Click(sender As Object, e As EventArgs)
        My.Forms.CUSTOMER_DASHBOARD.CUST_EDIT_BTN.PerformClick()
    End Sub

    Private Sub CLOSE_BTN_Click(sender As Object, e As EventArgs)
        CUSTOMER_DASHBOARD.DASHBOARD_BTN.PerformClick()
    End Sub
End Class