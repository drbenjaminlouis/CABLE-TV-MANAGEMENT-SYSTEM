Imports System.Data.OleDb

Public Class Admin_Login
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
    Dim dr As OleDbDataReader
    Private Sub Admin_Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.LoginType = "ADMIN"
        Year_Updater.TV_Year_Updater()
        Year_Updater.BroadBand_Year_Updater()
    End Sub
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles LOGIN_BTN.Click
        If USERNAME_TEXTBOX.Text = "" Or PASSWORD_TEXTBOX.Text = "" Then
            MessageBox1.Show("", "Please Enter The Credentials")
            USERNAME_TEXTBOX.Clear()
            PASSWORD_TEXTBOX.Clear()
        Else
            Try
                conn.Open()
                Dim cmd As New OleDbCommand("SELECT * FROM ADMIN_LOGIN_DETAILS WHERE username=@USERNAME AND password=@PASSWORD", conn)
                cmd.Parameters.AddWithValue("@USERNAME", USERNAME_TEXTBOX.Text)
                cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD_TEXTBOX.Text)
                dr = cmd.ExecuteReader
                If dr.HasRows = True Then
                    Module1.UserName = USERNAME_TEXTBOX.Text
                    Module1.LoginType = "ADMIN"
                    Me.Hide()
                    Admin_Dashboard.Show()
                    Admin_Dashboard.DASHBOARD_BTN.PerformClick()
                    Dim connection As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                    Try
                        connection.Open()
                        Dim complaint_selector As New OleDbCommand("SELECT COUNT(C_ID) FROM CUST_COMPLAINTS WHERE R_STATUS=@R_STATUS", connection)
                        complaint_selector.Parameters.AddWithValue("@R_STATUS", "UNREAD")
                        Dim c_val As Integer = complaint_selector.ExecuteScalar

                        If c_val <= 0 Then
                            Admin_Dashboard.NOTIFICATION_ICON.Visible = False
                        Else
                            Admin_Dashboard.NOTIFICATION_ICON.Visible = True
                            Admin_Dashboard.NOTIFICATION_ICON.Text = c_val
                        End If
                        Payment_Sync.Suspender()
                    Catch ex As Exception
                        ErrorAlert.Play()
                        LogError("An Error While Fetching Complaints Data: " & ex.Message)
                        MessageBox1.Show("An Error Occured While Fetching Complaints Data. Check Log For More Details.", "ALERT")
                    Finally
                        connection.Close()
                    End Try
                Else
                    MessageBox1.Show("", "Invalid Username Or Password")
                    USERNAME_TEXTBOX.Clear()
                    PASSWORD_TEXTBOX.Clear()
                End If
            Catch ex As Exception

            Finally
                conn.Close()
                dr.Close()
            End Try
        End If
    End Sub
    Private Sub Guna2ControlBox2_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox2.Click
        Application.Exit()
    End Sub
    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub
    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Me.Hide()
        USER_LOGIN.Show()
    End Sub
    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles SHOW_PASSWORD_TOOGLE.CheckedChanged, SHOW_PASSWORD_TOOGLE.CheckedChanged
        If SHOW_PASSWORD_TOOGLE.Checked Then PASSWORD_TEXTBOX.PasswordChar = Convert.ToChar(0) Else PASSWORD_TEXTBOX.PasswordChar = Convert.ToChar("*")
        PASSWORD_TEXTBOX.UseSystemPasswordChar = Not SHOW_PASSWORD_TOOGLE.Checked
    End Sub

End Class
