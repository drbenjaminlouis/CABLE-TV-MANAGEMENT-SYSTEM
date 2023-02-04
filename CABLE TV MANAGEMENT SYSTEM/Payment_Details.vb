Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Guna.UI2.WinForms

Public Class Payment_Details
    Private Sub Payment_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            ElseIf monthNames(i).Text = "PENDING" Then
                monthNames(i).ForeColor = pendingColor
            Else
                monthNames(i).ForeColor = Color.White
            End If
        Next
    End Sub
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb"
        Dim sql As String = "SELECT JANUARY,FEBRUARY,MARCH,APRIL,MAY,JUNE,JULY,AUGUST,SEPTEMBER,OCTOBER,NOVEMBER,DECEMBER FROM TV_PAYMENT_DETAILS WHERE CRF = @CRF AND YEAR = @YEAR"
        Dim sql2 As String = "SELECT CUST_NAME,CUST_DOB,CUST_HOUSE_NAME,CUST_AREA,CUST_DISTRICT,CUST_STATE,CUST_COUNTRY,CUST_PINCODE,CUST_MOBILE,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF = @CRF"
        Using connection As New OleDbConnection(connectionString)
            connection.Open()
            Dim command As New OleDbCommand(sql, connection)
            Dim command2 As New OleDbCommand(sql2, connection)
            command2.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
            command.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
            command.Parameters.AddWithValue("@YEAR", YEAR_COMBOBOX.Text)
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
            Dim reader2 As OleDbDataReader = command2.ExecuteReader()
            If reader2.Read() Then
                CUST_NAME_TEXTBOX.Text = reader2("CUST_NAME").ToString()
                DOB_PICKER.Value = reader2("CUST_DOB").ToString()
                CUST_HOUSENAME_TEXTBOX.Text = reader2("CUST_HOUSE_NAME").ToString()
                CUST_AREA_TEXTBOX.Text = reader2("CUST_AREA").ToString()
                CUST_DISTRICT_TEXTBOX.Text = reader2("CUST_DISTRICT").ToString()
                CUST_STATE_TEXTBOX.Text = reader2("CUST_STATE").ToString()
                CUST_COUNTRY_TEXTBOX.Text = reader2("CUST_COUNTRY").ToString()
                CUST_PINCODE_TEXTBOX.Text = reader2("CUST_PINCODE").ToString()
                CUST_MOBILE_TEXTBOX.Text = reader2("CUST_MOBILE").ToString()
                CUST_EMAIL_TEXTBOX.Text = reader2("CUST_EMAIL").ToString()
            End If
            CheckTextBoxValues()
        End Using
    End Sub


End Class