Imports System.Data.OleDb
Public Class Complaints_user
    Public Sub New()
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf Complaints_user_Load
    End Sub
    Private Sub Complaints_user_Load(sender As Object, e As EventArgs)

        C_ID_TEXT.Text = generateComplaint()
        Dim cust_crf As Integer
        Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)

        Try
            connection.Open()
            Dim crfpicker As New OleDbCommand("SELECT CRF FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@USERNAME", connection)
            Dim username As String = LogType_Detector.UserName
            crfpicker.Parameters.AddWithValue("@USERNAME", username)
            Dim crfreader As OleDbDataReader = crfpicker.ExecuteReader
            If crfreader.HasRows Then
                While crfreader.Read
                    cust_crf = crfreader.GetInt32(0)
                End While
            End If
            CRF_TEXTBOX.Text = cust_crf
            CRF_TEXTBOX.ReadOnly = True
        Catch ex As Exception
            ErrorAlert.Play()
            LogError("An Error Occured While Fetching CRF: " & ex.Message)
            MessageBox.Show("An Error Occured While Fetching CRF: Please Contact Administrator", "ALERT")
            REGISTER_BTN.Enabled = False
        Finally
            connection.Close()
        End Try
        Try
            connection.Open()
            Dim datafetcher As New OleDbCommand("SELECT CUST_NAME,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF=@CRF", connection)
            datafetcher.Parameters.AddWithValue("@CRF", CRF_TEXTBOX.Text)
            Dim datareader As OleDbDataReader = datafetcher.ExecuteReader
            If datareader.HasRows = True Then
                While datareader.Read
                    NAME_TEXTBOX.Text = datareader.GetString(0)
                    EMAIL_TEXTBOX.Text = datareader.GetString(1)
                End While
            End If
        Catch ex As Exception
            ErrorAlert.Play()
            LogError("An Error Occured While Fetching Name And Email : " & ex.Message)
            MessageBox.Show("An Error Occured While Fetching Details. Please Contact Administrator.", "ALERT")
            REGISTER_BTN.Enabled = False
        End Try
        AddHandler REGISTER_BTN.Click, AddressOf REGISTER_BTN_Click
    End Sub

    Private Sub REGISTER_BTN_Click(sender As Object, e As EventArgs)
        If CRF_TEXTBOX.Text = "" Then

        ElseIf NAME_TEXTBOX.Text = "" Then

        ElseIf COMPLAINT_TYPE.SelectedIndex = -1 Then
            ErrorAlert.Play()
            MessageBox.Show("Please Select Complaint Type.", "ALERT")
        ElseIf MessageTextbox.Text = "" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Enter Complaint Details.", "ALERT")
        Else
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            Try
                connection.Open()
                Dim complaint_reg As New OleDbCommand("INSERT INTO CUST_COMPLAINTS (C_ID,CRF,CUST_NAME,CUST_EMAIL,C_TYPE,MESSAGE,R_STATUS,C_STATUS) VALUES (@C_ID,@CRF,@CUST_NAME,@CUST_EMAIL,@C_TYPE,@MESSAGE,@R_STATUS,@C_STATUS)", connection)
                complaint_reg.Parameters.AddWithValue("@C_ID", C_ID_TEXT.Text)
                complaint_reg.Parameters.AddWithValue("@CRF", CRF_TEXTBOX.Text)
                complaint_reg.Parameters.AddWithValue("@CUST_NAME", NAME_TEXTBOX.Text)
                complaint_reg.Parameters.AddWithValue("@CUST_EMAIL", EMAIL_TEXTBOX.Text)
                complaint_reg.Parameters.AddWithValue("@C_TYPE", COMPLAINT_TYPE.SelectedItem)
                complaint_reg.Parameters.AddWithValue("@MESSAGE", MessageTextbox.Text)
                complaint_reg.Parameters.AddWithValue("@R_STATUS", "UNREAD")
                complaint_reg.Parameters.AddWithValue("@C_STATUS", "UN RESOLVED")
                complaint_reg.ExecuteNonQuery()
                Email.Complaint_Raise(EMAIL_TEXTBOX.Text, C_ID_TEXT.Text, NAME_TEXTBOX.Text, COMPLAINT_TYPE.SelectedItem)
                MessageBox.Show("Complaint Registered Successfully.", "ALERT")
                COMPLAINT_TYPE.SelectedIndex = -1
                MessageTextbox.Clear()
                C_ID_TEXT.Text = COMPLAINT_NO_GENERATOR.generateComplaint
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Registering Complaint: " & ex.Message)
                MessageBox.Show("An Error Occured While Registering Complaint. Contact Administrator.", "ALERT")
            End Try
        End If
    End Sub
End Class