Imports System.Data.OleDb
Public Class Change_Password
    Private randomValue As Integer = 0
    Private WithEvents tmrRandom As New Timer()
    Dim rnd As New Random()
    Private Sub CHANGE_PASSWORD_BTN_Click(sender As Object, e As EventArgs) Handles CHANGE_PASSWORD_BTN.Click
        'While Admin Login
        If LoginType = "ADMIN" Then
            If NEW_PASSWORD_TEXTBOX.Text <> CONFIRM_PASSWORD_TEXTBOX.Text Then
                ErrorAlert.Play()
                MessageBox.Show("New password and confirm password do not match.", "ALERT")
                NEW_PASSWORD_TEXTBOX.Clear()
                CONFIRM_PASSWORD_TEXTBOX.Clear()
            ElseIf USERNAME_TEXTBOX.Text <> LogType_Detector.UserName Then
                ErrorAlert.Play()
                MessageBox.Show("Please Enter Your Username", "ALERT")
                USERNAME_TEXTBOX.Clear()
            ElseIf NEW_PASSWORD_TEXTBOX.Text = "" Then
                ErrorAlert.Play()
                MessageBox.Show("Please Enter New Password", "ALERT")
            ElseIf NEW_PASSWORD_TEXTBOX.Text = CURRENT_PASSWORD_TEXTBOX.Text Then
                ErrorAlert.Play()
                MessageBox.Show("New Password Should Be Different From Current Password", "ALERT")
                NEW_PASSWORD_TEXTBOX.Clear()
                CONFIRM_PASSWORD_TEXTBOX.Clear()
            Else
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                Dim sqlCheck As String = "SELECT * FROM [ADMIN_LOGIN_DETAILS] WHERE [USERNAME] = @UserName AND [PASSWORD] = @Password"
                Dim sqlUpdate As String = "UPDATE [ADMIN_LOGIN_DETAILS] SET [PASSWORD] = @NewPassword WHERE [USERNAME] = @UserName"
                Try
                    conn.Open()
                    Using cmdCheck As New OleDbCommand(sqlCheck, conn)
                        cmdCheck.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                        cmdCheck.Parameters.AddWithValue("@Password", CURRENT_PASSWORD_TEXTBOX.Text)
                        Dim reader As OleDbDataReader = cmdCheck.ExecuteReader()
                        If reader.HasRows = True Then
                            reader.Close()
                            Using cmdUpdate As New OleDbCommand(sqlUpdate, conn)
                                cmdUpdate.Parameters.AddWithValue("@NewPassword", NEW_PASSWORD_TEXTBOX.Text)
                                cmdUpdate.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                                cmdUpdate.ExecuteNonQuery()
                                MessageBox.Show("Password updated successfully.", "ALERT")
                                USERNAME_TEXTBOX.Clear()
                                CURRENT_PASSWORD_TEXTBOX.Clear()
                                NEW_PASSWORD_TEXTBOX.Clear()
                                CONFIRM_PASSWORD_TEXTBOX.Clear()
                            End Using
                        Else
                            reader.Close()
                            MessageBox.Show("User name or old password is incorrect.", "ALERT")
                        End If
                    End Using
                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("An Error Occured While Changing Password.Check Log For More Details.")
                Finally
                    conn.Close()
                End Try
            End If
        End If
        'While Customer Login
        If LoginType = "CUSTOMER" Then
            If NEW_PASSWORD_TEXTBOX.Text <> CONFIRM_PASSWORD_TEXTBOX.Text Then
                ErrorAlert.Play()
                MessageBox.Show("New password and confirm password do not match.", "ALERT")
                NEW_PASSWORD_TEXTBOX.Clear()
                CONFIRM_PASSWORD_TEXTBOX.Clear()
            ElseIf USERNAME_TEXTBOX.Text <> LogType_Detector.UserName Then
                ErrorAlert.Play()
                MessageBox.Show("Please Enter Your Username", "ALERT")
                USERNAME_TEXTBOX.Clear()
            ElseIf NEW_PASSWORD_TEXTBOX.Text = "" Then
                ErrorAlert.Play()
                MessageBox.Show("Please Enter New Password", "ALERT")
            ElseIf NEW_PASSWORD_TEXTBOX.Text = CURRENT_PASSWORD_TEXTBOX.Text Then
                ErrorAlert.Play()
                MessageBox.Show("New Password Should Be Different From Current Password", "ALERT")
                NEW_PASSWORD_TEXTBOX.Clear()
                CONFIRM_PASSWORD_TEXTBOX.Clear()
            ElseIf OTP_TEXTBOX.Text = "" Then
                ErrorAlert.Play()
                MessageBox.Show("Please Enter OTP.", "ALERT")
            ElseIf Not OTP_TEXTBOX.Text = randomValue Then
                ErrorAlert.Play()
                MessageBox.Show("Invalid OTP", "ALERT")
            Else
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                Dim sqlCheck As String = "SELECT * FROM [CUSTOMER_LOGIN_DETAILS] WHERE [CUST_USERNAME] = @UserName AND [CUST_PASSWORD] = @Password"
                Dim sqlUpdate As String = "UPDATE [CUSTOMER_LOGIN_DETAILS] SET [CUST_PASSWORD] = @NewPassword WHERE [CUST_USERNAME] = @UserName"
                Try
                    conn.Open()
                    Using cmdCheck As New OleDbCommand(sqlCheck, conn)
                        cmdCheck.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                        cmdCheck.Parameters.AddWithValue("@Password", CURRENT_PASSWORD_TEXTBOX.Text)
                        Dim reader As OleDbDataReader = cmdCheck.ExecuteReader()
                        If reader.HasRows Then
                            reader.Close()
                            Using cmdUpdate As New OleDbCommand(sqlUpdate, conn)
                                cmdUpdate.Parameters.AddWithValue("@NewPassword", NEW_PASSWORD_TEXTBOX.Text)
                                cmdUpdate.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                                cmdUpdate.ExecuteNonQuery()
                                MessageBox.Show("Password updated successfully.", "ALERT")
                                randomValue = rnd.Next(100000, 999999)
                                USERNAME_TEXTBOX.Clear()
                                CURRENT_PASSWORD_TEXTBOX.Clear()
                                NEW_PASSWORD_TEXTBOX.Clear()
                                CONFIRM_PASSWORD_TEXTBOX.Clear()
                                OTP_TEXTBOX.Clear()
                            End Using
                        Else
                            reader.Close()
                            MessageBox.Show("User name or old password is incorrect.", "ALERT")
                        End If
                    End Using
                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("An Error Occured While Changing Password.Check Log For More Details.")
                Finally
                    conn.Close()
                End Try
            End If
        End If
    End Sub

    'For Clearing All
    Private Sub CLEAR_BTN_Click(sender As Object, e As EventArgs) Handles CLEAR_BTN.Click
        USERNAME_TEXTBOX.Clear()
        CURRENT_PASSWORD_TEXTBOX.Clear()
        NEW_PASSWORD_TEXTBOX.Clear()
        CONFIRM_PASSWORD_TEXTBOX.Clear()
        OTP_TEXTBOX.Clear()
    End Sub

    'For Generating a new random value every 5 minutes.
    Private Sub tmrRandom_Tick(sender As Object, e As EventArgs) Handles tmrRandom.Tick
        randomValue = rnd.Next(100000, 999999)
    End Sub
    Private Sub Change_Password_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CURRENT_PASSWORD_TEXTBOX.PasswordChar = Convert.ToChar("●")
        NEW_PASSWORD_TEXTBOX.PasswordChar = Convert.ToChar("●")
        CONFIRM_PASSWORD_TEXTBOX.PasswordChar = Convert.ToChar("●")
        If LoginType = "ADMIN" Then
            OTP_LABEL.Visible = False
            OTP_TEXTBOX.Visible = False
            OTP_BTN.Visible = False
        End If
        If LoginType = "CUSTOMER" Then
            OTP_LABEL.Visible = True
            OTP_TEXTBOX.Visible = True
            OTP_BTN.Visible = True
        End If
        'Initialization of timer with a 5-minute interval
        tmrRandom.Interval = 300000
        tmrRandom.Start()
    End Sub

    'For Sending OTP
    Private Sub OTP_BTN_Click(sender As Object, e As EventArgs) Handles OTP_BTN.Click
        randomValue = rnd.Next(100000, 999999)
        Dim rand_value = randomValue
        Dim cust_crf As Integer
        Dim cust_email As String
        Dim customer_name As String
        If LoginType = "CUSTOMER" Then
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            Try
                connection.Open()
                Dim crfpicker As New OleDbCommand("SELECT CRF FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@USERNAME", connection)
                Dim username As String = LogType_Detector.UserName
                crfpicker.Parameters.AddWithValue("@USERNAME", username)
                Dim crfreader As OleDbDataReader = crfpicker.ExecuteReader
                If crfreader.HasRows = True Then
                    While crfreader.Read
                        cust_crf = crfreader.GetInt32(0)
                    End While
                End If
                Dim emailpicker As New OleDbCommand("SELECT CUST_NAME,CUST_EMAIL FROM CUSTOMER_DETAILS WHERE CRF=@CRF", connection)
                emailpicker.Parameters.AddWithValue("@CRF", cust_crf)
                Dim emailreader As OleDbDataReader = emailpicker.ExecuteReader
                If emailreader.HasRows = True Then
                    While emailreader.Read
                        customer_name = emailreader.GetString(0)
                        cust_email = emailreader.GetString(1)
                        If Not cust_email = "" And Not customer_name = "" Then
                            Try
                                OTP_Sender(cust_email, rand_value, customer_name)
                            Catch ex As Exception
                                ErrorAlert.Play()
                                LogError("Error Sending CRF : " & ex.Message)
                                MessageBox.Show("Error Sending OTP. Contact Admin.", "ALERT")
                            End Try
                        End If
                    End While
                End If
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Fetching CRF or Email: " & ex.Message)
                MessageBox.Show("An Error Occured: Please Contact Administrator", "ALERT")
                CHANGE_PASSWORD_BTN.Enabled = False
            Finally
                connection.Close()
            End Try
        End If
    End Sub
End Class