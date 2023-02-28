Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Guna.UI2.WinForms

Public Class Payment_Details
    Dim yearList As New List(Of Integer)
    Private Sub Payment_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Payment_Sync.Payment_Sync()
        SERVICE_COMBOBOX.Items.Clear()
        YEAR_COMBOBOX.Items.Clear()
        ' TV_Reg_Picker.MaxDate = Date.Today
        'BROADBAND_REG_DATE.MaxDate = Date.Today
        DOB_PICKER.MinDate = DateTime.Today.AddYears(-80)
        DOB_PICKER.MaxDate = DateTime.Today.AddYears(-18)
        SERVICE_COMBOBOX.Enabled = False
        YEAR_COMBOBOX.Enabled = False
    End Sub
    Private Sub CheckTextBoxValues()
        Dim monthNames() As Guna2TextBox = {JANUARY_TEXTBOX, FEBRUARY_TEXTBOX, MARCH_TEXTBOX, APRIL_TEXTBOX, MAY_TEXTBOX, JUNE_TEXTBOX, JULY_TEXTBOX, AUGUST_TEXTBOX, SEPTEMBER_TEXTBOX, OCTOBER_TEXTBOX, NOVEMBER_TEXTBOX, DECEMBER_TEXTBOX}
        Dim paidColor As Color = Color.Lime
        Dim notPaidColor As Color = Color.Red
        Dim pendingColor As Color = Color.Yellow
        For i As Integer = 0 To 11
            If monthNames(i).Text = "PAID" Then
                monthNames(i).ForeColor = paidColor
            ElseIf monthNames(i).Text = "NOT PAID" Then
                monthNames(i).ForeColor = notPaidColor
            ElseIf monthNames(i).Text = "NILL" Then
                monthNames(i).ForeColor = pendingColor
            Else
                monthNames(i).ForeColor = Color.White
            End If
        Next
    End Sub
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If CUST_CRF_TEXTBOX.Text = "" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Enter CRF", "ALERT")
        ElseIf String.IsNullOrEmpty(YEAR_COMBOBOX.SelectedItem) Then
            ' Handle case where nothing is selected in the combobox
            ' Handle case where 2022 is selected in the combobox
            ErrorAlert.Play()
            MessageBox.Show("Please Select Year.", "ALERT")
        ElseIf SERVICE_COMBOBOX.SelectedItem = "" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Select Service.", "ALERT")
        Else
            Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath
            Dim connection As New OleDbConnection(connectionString)
            Try
                connection.Open()
                Dim sql2 As String = "SELECT CUST_NAME,CUST_DOB,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_COUNTRY,CUST_PINCODE,CUST_MOBILE,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF = @CRF"
                Dim command2 As New OleDbCommand(sql2, connection)
                command2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                Dim reader2 As OleDbDataReader = command2.ExecuteReader()
                If reader2.Read() Then
                    CUST_NAME_TEXTBOX.Text = reader2("CUST_NAME").ToString()
                    DOB_PICKER.Value = reader2.GetDateTime(1)
                    CUST_HOUSENAME_TEXTBOX.Text = reader2("CUST_HOUSE_NAME").ToString()
                    CUST_AREA_TEXTBOX.Text = reader2("CUST_AREA").ToString()
                    CUST_DISTRICT_TEXTBOX.Text = reader2("CUST_DISTRICT").ToString()
                    CUST_STATE_TEXTBOX.Text = reader2("CUST_STATE").ToString()
                    CUST_COUNTRY_TEXTBOX.Text = reader2("CUST_COUNTRY").ToString()
                    CUST_PINCODE_TEXTBOX.Text = reader2("CUST_PINCODE").ToString()
                    CUST_MOBILE_TEXTBOX.Text = reader2("CUST_MOBILE").ToString()
                    CUST_EMAIL_TEXTBOX.Text = reader2("CUST_EMAIL").ToString()
                End If
                If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
                    Dim sql As String = "SELECT JANUARY,FEBRUARY,MARCH,APRIL,MAY,JUNE,JULY,AUGUST,SEPTEMBER,OCTOBER,NOVEMBER,DECEMBER FROM TV_PAYMENT_DETAILS WHERE CRF = @CRF AND PAYMENT_YEAR = @YEAR"
                    Dim command As New OleDbCommand(sql, connection)
                    command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    command.Parameters.AddWithValue("@YEAR", YEAR_COMBOBOX.SelectedItem)
                    Dim reader As OleDbDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        JANUARY_TEXTBOX.Text = reader("JANUARY").ToString()
                        FEBRUARY_TEXTBOX.Text = reader("FEBRUARY").ToString()
                        MARCH_TEXTBOX.Text = reader("MARCH").ToString()
                        APRIL_TEXTBOX.Text = reader("APRIL").ToString()
                        MAY_TEXTBOX.Text = reader("MAY").ToString()
                        JUNE_TEXTBOX.Text = reader("JUNE").ToString()
                        JULY_TEXTBOX.Text = reader("JULY").ToString()
                        AUGUST_TEXTBOX.Text = reader("AUGUST").ToString()
                        SEPTEMBER_TEXTBOX.Text = reader("SEPTEMBER").ToString()
                        OCTOBER_TEXTBOX.Text = reader("OCTOBER").ToString()
                        NOVEMBER_TEXTBOX.Text = reader("NOVEMBER").ToString()
                        DECEMBER_TEXTBOX.Text = reader("DECEMBER").ToString()
                    End If
                End If
                If SERVICE_COMBOBOX.SelectedItem = "BROADBAND" Then
                    Dim sql3 As String = "SELECT JANUARY,FEBRUARY,MARCH,APRIL,MAY,JUNE,JULY,AUGUST,SEPTEMBER,OCTOBER,NOVEMBER,DECEMBER FROM BROADBAND_PAYMENT_DETAILS WHERE CRF = @CRF AND PAYMENT_YEAR = @YEAR"
                    Dim command3 As New OleDbCommand(sql3, connection)
                    command3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    command3.Parameters.AddWithValue("@YEAR", YEAR_COMBOBOX.SelectedItem)
                    Dim reader3 As OleDbDataReader = command3.ExecuteReader()
                    If reader3.Read() Then
                        JANUARY_TEXTBOX.Text = reader3("JANUARY").ToString()
                        FEBRUARY_TEXTBOX.Text = reader3("FEBRUARY").ToString()
                        MARCH_TEXTBOX.Text = reader3("MARCH").ToString()
                        APRIL_TEXTBOX.Text = reader3("APRIL").ToString()
                        MAY_TEXTBOX.Text = reader3("MAY").ToString()
                        JUNE_TEXTBOX.Text = reader3("JUNE").ToString()
                        JULY_TEXTBOX.Text = reader3("JULY").ToString()
                        AUGUST_TEXTBOX.Text = reader3("AUGUST").ToString()
                        SEPTEMBER_TEXTBOX.Text = reader3("SEPTEMBER").ToString()
                        OCTOBER_TEXTBOX.Text = reader3("OCTOBER").ToString()
                        NOVEMBER_TEXTBOX.Text = reader3("NOVEMBER").ToString()
                        DECEMBER_TEXTBOX.Text = reader3("DECEMBER").ToString()
                    End If
                End If
                CheckTextBoxValues()
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Fetching Payment Details: " & ex.Message)
                MessageBox.Show("An Error Occured While Fetching Payment: Check Log For More Details.")
            Finally
                connection.Close()
            End Try
        End If
    End Sub
    Public Sub clearALl()
        CUST_NAME_TEXTBOX.Clear()
        DOB_PICKER.Value = DOB_PICKER.MaxDate
        CUST_HOUSENAME_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_DISTRICT_TEXTBOX.Clear()
        CUST_STATE_TEXTBOX.Clear()
        CUST_COUNTRY_TEXTBOX.Clear()
        CUST_PINCODE_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()
        CUST_EMAIL_TEXTBOX.Clear()
        JANUARY_TEXTBOX.Clear()
        FEBRUARY_TEXTBOX.Clear()
        MARCH_TEXTBOX.Clear()
        APRIL_TEXTBOX.Clear()
        MAY_TEXTBOX.Clear()
        JUNE_TEXTBOX.Clear()
        JULY_TEXTBOX.Clear()
        AUGUST_TEXTBOX.Clear()
        SEPTEMBER_TEXTBOX.Clear()
        OCTOBER_TEXTBOX.Clear()
        NOVEMBER_TEXTBOX.Clear()
        DECEMBER_TEXTBOX.Clear()
    End Sub
    Private Sub YEAR_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles YEAR_COMBOBOX.SelectedIndexChanged
        clearALl()
    End Sub

    Private Sub CUST_CRF_TEXTBOX_Leave(sender As Object, e As KeyEventArgs) Handles CUST_CRF_TEXTBOX.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not String.IsNullOrEmpty(CUST_CRF_TEXTBOX.Text) Then

            yearList.Clear()

            If CUST_CRF_TEXTBOX.Text = "" Then
                SERVICE_COMBOBOX.Items.Clear()
                YEAR_COMBOBOX.Items.Clear()
            Else
                SERVICE_COMBOBOX.Items.Clear()
                YEAR_COMBOBOX.Items.Clear()
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                Try
                    connection.Open()
                    Dim checker As New OleDbCommand("SELECT CRF FROM CUSTOMER_DETAILS WHERE CRF=@CRF", connection)
                    checker.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    Dim reader As OleDbDataReader = checker.ExecuteReader
                    If Not reader.HasRows Then
                        MessageBox.Show("CRF Not Exist.", "ALERT")
                        CUST_CRF_TEXTBOX.Clear()
                        SERVICE_COMBOBOX.Enabled = False
                        YEAR_COMBOBOX.Enabled = False
                    Else
                        Dim command3 As New OleDbCommand("SELECT CUST_TV_CONNECTION FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        command3.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        Dim command4 As New OleDbCommand("SELECT BROADBAND_CONNECTION FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                        command4.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                        Dim reader3 As OleDbDataReader = command3.ExecuteReader
                        If reader3.HasRows Then
                            While reader3.Read
                                If reader3.GetString(0) = "YES" Then
                                    SERVICE_COMBOBOX.Items.Add("CABLE TV")
                                Else
                                End If
                            End While
                        End If
                        Dim reader4 As OleDbDataReader = command4.ExecuteReader
                        If reader4.HasRows Then
                            While reader4.Read
                                If reader4.GetString(0) = "YES" Then
                                    SERVICE_COMBOBOX.Items.Add("BROADBAND")
                                Else

                                End If
                            End While
                        End If
                        SERVICE_COMBOBOX.Enabled = True
                        YEAR_COMBOBOX.Enabled = True
                    End If

                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("An Error Occured While Fetching Payment Details: " & ex.Message)
                    MessageBox.Show("An Error Occured While Fetching Payment: Check Log For More Details.")
                End Try
            End If
        End If
    End Sub

    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.TextChanged
        If CUST_CRF_TEXTBOX.Text = "" Then
            clearALl()
        End If
    End Sub

    Private Sub SERVICE_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SERVICE_COMBOBOX.SelectedIndexChanged
        YEAR_COMBOBOX.Items.Clear()
        If SERVICE_COMBOBOX.SelectedItem = "" Then
            clearALl()
        Else
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            connection.Open()
            If SERVICE_COMBOBOX.SelectedItem = "CABLE TV" Then
                Dim command5 As New OleDbCommand("SELECT DISTINCT(PAYMENT_YEAR) FROM TV_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                command5.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                Dim reader5 As OleDbDataReader = command5.ExecuteReader()
                If reader5.HasRows Then
                    yearList.Clear()
                    While reader5.Read()
                        Dim year As Integer = reader5.GetInt32(0)
                        If Not yearList.Contains(year) Then
                            yearList.Add(year)
                        End If
                    End While
                End If
                For Each year As Integer In yearList
                        YEAR_COMBOBOX.Items.Add(year)
                    Next
                    reader5.Close()
                End If
                If SERVICE_COMBOBOX.SelectedItem = "BROADBAND" Then
                Dim command6 As New OleDbCommand("SELECT DISTINCT(PAYMENT_YEAR) FROM BROADBAND_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                command6.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                Dim reader6 As OleDbDataReader = command6.ExecuteReader
                If reader6.HasRows Then
                    yearList.Clear()
                    While reader6.Read()
                        Dim year As Integer = reader6.GetInt32(0)
                        If Not yearList.Contains(year) Then
                            yearList.Add(year)
                        End If
                    End While

                    For Each year As Integer In yearList
                        YEAR_COMBOBOX.Items.Add(year)
                    Next
                    reader6.Close()
                End If
            End If
        End If
    End Sub
End Class