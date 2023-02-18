Imports System.Data.OleDb
Module Module1
    Public UserName As String
End Module
Public Class Admin_Login
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
    Dim dr As OleDbDataReader
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If textbox1.Text = "" Or textbox2.Text = "" Then
            MessageBox1.Show("", "Please Enter The Credentials")
            textbox1.Clear()
            textbox2.Clear()
        Else
            Try
                conn.Open()
                Dim cmd As New OleDbCommand("SELECT * FROM ADMIN_LOGIN_DETAILS WHERE username=@USERNAME AND password=@PASSWORD", conn)
                cmd.Parameters.AddWithValue("@USERNAME", textbox1.Text)
                cmd.Parameters.AddWithValue("@PASSWORD", textbox2.Text)
                dr = cmd.ExecuteReader
                If dr.HasRows = True Then
                    Module1.UserName = textbox1.Text
                    Me.Hide()
                    Dim admin_dash As New Admin_Dashboard
                    admin_dash.Show()
                Else
                    MessageBox1.Show("", "Invalid Username Or Password")
                    textbox1.Clear()
                    textbox2.Clear()
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
        Dim log_selector As New Login_Selector
        log_selector.Show()
    End Sub
    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2ToggleSwitch1.CheckedChanged
        If Guna2ToggleSwitch1.Checked Then textbox2.PasswordChar = Convert.ToChar(0) Else textbox2.PasswordChar = Convert.ToChar("*")
        textbox2.UseSystemPasswordChar = Not Guna2ToggleSwitch1.Checked
    End Sub
End Class
