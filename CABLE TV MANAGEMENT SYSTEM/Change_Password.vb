Imports System.Data.OleDb
Imports System.Diagnostics.Eventing.Reader

Public Class Change_Password
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
    End Sub
    Private Sub CLEAR_BTN_Click(sender As Object, e As EventArgs) Handles CLEAR_BTN.Click
        USERNAME_TEXTBOX.Clear()
        CURRENT_PASSWORD_TEXTBOX.Clear()
        NEW_PASSWORD_TEXTBOX.Clear()
        CONFIRM_PASSWORD_TEXTBOX.Clear()
    End Sub
End Class