Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class add_customer
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
    Private Sub add_customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CUST_STATE_COMBOBOX.Items.Add("PLEASE SELECT COUNTRY FIRST")
        Dim AsianCountries As String() = {"Afghanistan", "Armenia", "Azerbaijan", "Bahrain", "Bangladesh", "Bhutan", "Brunei", "Cambodia", "China", "Cyprus", "Georgia", "India", "Indonesia", "Iran", "Iraq", "Israel", "Japan", "Jordan", "Kazakhstan", "Kuwait", "Kyrgyzstan", "Laos", "Lebanon", "Malaysia", "Maldives", "Mongolia", "Myanmar", "Nepal", "North Korea", "Oman", "Pakistan", "Palestine", "Philippines", "Qatar", "Russia", "Saudi Arabia", "Singapore", "South Korea", "Sri Lanka", "Syria", "Taiwan", "Tajikistan", "Thailand", "Timor-Leste", "Turkey", "Turkmenistan", "United Arab Emirates", "Uzbekistan", "Vietnam", "Yemen"}
        For Each country As String In AsianCountries
            CUST_COUNTRY_COMBOBOX.Items.Add(country)
        Next
        CUST_COUNTRY_COMBOBOX.Items.Cast(Of String)().ToList().Sort()
    End Sub
    Private Sub CUST_MOBILE_TEXTBOX_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CUST_MOBILE_TEXTBOX.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox2.Show("PLEASE ENTER A VALID MOBILE NUMBER")
        End If

    End Sub
    Private Sub CUST_COUNTRY_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CUST_COUNTRY_COMBOBOX.SelectedIndexChanged
        Dim IndianStates As String() = {"Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand", "West Bengal"}
        CUST_STATE_COMBOBOX.Items.Clear()
        If CUST_COUNTRY_COMBOBOX.SelectedItem = "India" Then
            For Each state As String In IndianStates
                CUST_STATE_COMBOBOX.Items.Add(state)
            Next
        Else
        End If
    End Sub
    Private Sub ADD_CUST_CREATEBTN_Click_1(sender As Object, e As EventArgs) Handles ADD_CUST_CREATEBTN.Click
        Dim emailRegex As New Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")

        If Not emailRegex.IsMatch(CUST_EMAIL_TEXTBOX.Text) Then
            MessageBox2.Show("Invalid email address. Please enter a valid email address.")
            CUST_EMAIL_TEXTBOX.Text = ""
        ElseIf CUST_STATE_COMBOBOX.Text = "PLEASE SELECT COUNTRY FIRST" Then
            MessageBox2.Show("", "PLEASE SELECT STATE")
        ElseIf CUST_CRF_TEXTBOX.Text = "" Or CUST_NAME_TEXTBOX.Text = "" Or DOB_PICKER.Text = "" Or CUST_HOUSENAME_TEXTBOX.Text = "" Or CUST_AREA_TEXTBOX.Text = "" Or CUST_DISTRICT_TEXTBOX.Text = "" Or CUST_STATE_COMBOBOX.Text = "" Or CUST_COUNTRY_COMBOBOX.Text = "" Or CUST_IDTYPE_COMBOBOX.Text = "" Or CUST_IDNUMBER_TEXTBOX.Text = "" Or CUST_MOBILE_TEXTBOX.Text = "" Or CUST_EMAIL_TEXTBOX.Text = "" Or CUST_BROADBAND_COMBOBOX.Text = "" Or CUST_BROADBAND_PLAN_COMBOBOX.Text = "" Or CUST_BROADBAND_USERNAME_TEXTBOX.Text = "" Or CUST_BROADBAND_PASSWORD_TEXTBOX.Text = "" Or CUST_TV_CONNECTION_COMBOBOX.Text = "" Or CUST_CABLE_PLAN_COMBOBOX.Text = "" Or CUST_CHIP_ID_TEXTBOX.Text = "" Or CUST_USERNAME_TEXTBOX.Text = "" Or CUST_PASSWORD_TEXTBOX.Text = "" Then
            MessageBox2.Show("", "Please Enter All The Details")
        Else
            Try
                Using connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb;Persist Security Info=True")
                    connection.Open()
                    Dim command1 As New OleDb.OleDbCommand("INSERT INTO CUSTOMER_DETAILS VALUES (@CRF,@CUST_NAME,@CUST_DOB,@CUST_HOUSE_NAME,@CUST_AREA,@CUST_DISTRICT,@CUST_STATE,@CUST_COUNTRY,@CUST_IDTYPE,@CUST_ID_NUMBER,@CUST_MOBILE,@CUST_EMAIL,@CUST_TV_CONNECTION,@CUST_TV_PLAN,@CHIP_ID,@CUST_USERNAME,@CUST_PASSWORD,@BROADBAND_CONNECTION,@TV_CONNECTION_STATUS);")
                    Dim command2 As New OleDb.OleDbCommand("INSERT INTO BROADBAND_CONNECTION_DETAILS VALUES (@CRF,@REGISTRATION_DATE,@LAST_RENEWAL_DATE,@EXPIRY_DATE,@STATUS,@RECHARGED_BY,@CURRENT_PLAN,@CUST_BROADBAND_USERNAME,@CUST_BROADBAND_PASSWORD,)")
                    Dim I As New Integer
                    connection.Close()
                End Using

            Catch ex As Exception
                MessageBox2.Show(ex.Message)
            Finally

            End Try
        End If
    End Sub

    Private Sub ADD_CUST_RESETBTN_Click(sender As Object, e As EventArgs) Handles ADD_CUST_RESETBTN.Click
        CUST_CRF_TEXTBOX.Clear()
        CUST_NAME_TEXTBOX.Clear()
        DOB_PICKER.ResetText()
        CUST_HOUSENAME_TEXTBOX.Clear()
        CUST_AREA_TEXTBOX.Clear()
        CUST_DISTRICT_TEXTBOX.Clear()
        CUST_STATE_COMBOBOX.SelectedIndex = -1
        CUST_COUNTRY_COMBOBOX.SelectedIndex = -1
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
    End Sub
End Class