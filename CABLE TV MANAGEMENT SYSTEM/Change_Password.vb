Imports System.Data.OleDb
Public Class Change_Password
    Private randomValue As Integer = 0
    Private WithEvents tmrRandom As New Timer()
    Dim rnd As New Random()
    Private Sub CHANGE_PASSWORD_BTN_Click(sender As Object, e As EventArgs) Handles CHANGE_PASSWORD_BTN.Click
        If LoginType = "ADMIN" Then
            If NEW_PASSWORD_TEXTBOX.Text <> CONFIRM_PASSWORD_TEXTBOX.Text Then
                ErrorAlert.Play()
                MessageBox.Show("New password and confirm password do not match.", "ALERT")
                NEW_PASSWORD_TEXTBOX.Clear()
                CONFIRM_PASSWORD_TEXTBOX.Clear()
            ElseIf USERNAME_TEXTBOX.Text <> Module1.UserName Then
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
                ' Define the connection string
                Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath
                ' Define the SQL query for checking the username and old password
                Dim sqlCheck As String = "SELECT * FROM [ADMIN_LOGIN_DETAILS] WHERE [USERNAME] = @UserName AND [PASSWORD] = @Password"
                ' Define the SQL query for updating the password
                Dim sqlUpdate As String = "UPDATE [ADMIN_LOGIN_DETAILS] SET [PASSWORD] = @NewPassword WHERE [USERNAME] = @UserName"
                ' Create a connection to the database
                Using conn As New OleDbConnection(connString)
                    Try
                        ' Open the connection
                        conn.Open()
                        ' Create a command to run the check query
                        Using cmdCheck As New OleDbCommand(sqlCheck, conn)
                            ' Add parameters to the command
                            cmdCheck.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                            cmdCheck.Parameters.AddWithValue("@Password", CURRENT_PASSWORD_TEXTBOX.Text)
                            ' Execute the check query and retrieve the result
                            Dim reader As OleDbDataReader = cmdCheck.ExecuteReader()
                            If reader.HasRows Then
                                ' User name and old password match, proceed with update
                                reader.Close()
                                ' Create a command to run the update query
                                Using cmdUpdate As New OleDbCommand(sqlUpdate, conn)
                                    ' Add parameters to the command
                                    cmdUpdate.Parameters.AddWithValue("@NewPassword", NEW_PASSWORD_TEXTBOX.Text)
                                    cmdUpdate.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                                    ' Execute the update query
                                    cmdUpdate.ExecuteNonQuery()
                                    MessageBox.Show("Password updated successfully.", "ALERT")
                                    USERNAME_TEXTBOX.Clear()
                                    CURRENT_PASSWORD_TEXTBOX.Clear()
                                    NEW_PASSWORD_TEXTBOX.Clear()
                                    CONFIRM_PASSWORD_TEXTBOX.Clear()
                                End Using
                            Else
                                ' User name or old password doesn't match, show error message
                                reader.Close()
                                MessageBox.Show("User name or old password is incorrect.", "ALERT")
                            End If
                        End Using
                        conn.Close()
                    Catch ex As Exception
                        ErrorAlert.Play()
                        LogError("An Error Occured While Changing Password.Check Log For More Details.")
                    End Try
                End Using
            End If
        End If
        If LoginType = "CUSTOMER" Then
            If NEW_PASSWORD_TEXTBOX.Text <> CONFIRM_PASSWORD_TEXTBOX.Text Then
                ErrorAlert.Play()
                MessageBox.Show("New password and confirm password do not match.", "ALERT")
                NEW_PASSWORD_TEXTBOX.Clear()
                CONFIRM_PASSWORD_TEXTBOX.Clear()
            ElseIf USERNAME_TEXTBOX.Text <> Module1.UserName Then
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
                ' Define the connection string
                Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath
                ' Define the SQL query for checking the username and old password
                Dim sqlCheck As String = "SELECT * FROM [CUSTOMER_LOGIN_DETAILS] WHERE [CUST_USERNAME] = @UserName AND [CUST_PASSWORD] = @Password"
                ' Define the SQL query for updating the password
                Dim sqlUpdate As String = "UPDATE [CUSTOMER_LOGIN_DETAILS] SET [CUST_PASSWORD] = @NewPassword WHERE [CUST_USERNAME] = @UserName"
                ' Create a connection to the database
                Using conn As New OleDbConnection(connString)
                    Try
                        ' Open the connection
                        conn.Open()
                        ' Create a command to run the check query
                        Using cmdCheck As New OleDbCommand(sqlCheck, conn)
                            ' Add parameters to the command
                            cmdCheck.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                            cmdCheck.Parameters.AddWithValue("@Password", CURRENT_PASSWORD_TEXTBOX.Text)
                            ' Execute the check query and retrieve the result
                            Dim reader As OleDbDataReader = cmdCheck.ExecuteReader()
                            If reader.HasRows Then
                                ' User name and old password match, proceed with update
                                reader.Close()
                                ' Create a command to run the update query
                                Using cmdUpdate As New OleDbCommand(sqlUpdate, conn)
                                    ' Add parameters to the command
                                    cmdUpdate.Parameters.AddWithValue("@NewPassword", NEW_PASSWORD_TEXTBOX.Text)
                                    cmdUpdate.Parameters.AddWithValue("@UserName", USERNAME_TEXTBOX.Text)
                                    ' Execute the update query
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
                                ' User name or old password doesn't match, show error message
                                reader.Close()
                                MessageBox.Show("User name or old password is incorrect.", "ALERT")
                            End If
                        End Using
                        conn.Close()
                    Catch ex As Exception
                        ErrorAlert.Play()
                        LogError("An Error Occured While Changing Password.Check Log For More Details.")
                    End Try
                End Using
            End If
        End If
    End Sub
    Private Sub CLEAR_BTN_Click(sender As Object, e As EventArgs) Handles CLEAR_BTN.Click
        USERNAME_TEXTBOX.Clear()
        CURRENT_PASSWORD_TEXTBOX.Clear()
        NEW_PASSWORD_TEXTBOX.Clear()
        CONFIRM_PASSWORD_TEXTBOX.Clear()
        OTP_TEXTBOX.Clear()
    End Sub
    Private Sub tmrRandom_Tick(sender As Object, e As EventArgs) Handles tmrRandom.Tick
        ' Generate a new random value every 5 minutes
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
        ' Initialize the timer with a 5-minute interval
        tmrRandom.Interval = 300000 ' 5 minutes in milliseconds
        tmrRandom.Start()
    End Sub
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
                Dim username As String = Module1.UserName
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
                    End While
                End If
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Fetching CRF or Email: " & ex.Message)
                MessageBox.Show("An Error Occured: Please Contact Administrator", "ALERT")
                CHANGE_PASSWORD_BTN.Enabled = False
            End Try
            If Not cust_email = "" And Not customer_name = "" Then
                Try
                    OTP_Sender(cust_email, rand_value, customer_name)
                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("Error Sending CRF : " & ex.Message)
                    MessageBox.Show("Error Sending OTP. Contact Admin.", "ALERT")
                End Try
            End If
        End If
    End Sub
End Class