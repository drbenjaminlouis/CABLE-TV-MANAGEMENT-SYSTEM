Imports System.Configuration
Imports System.Data.OleDb
Imports System.DirectoryServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Admin_Login
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Admin_Login_db.accdb")
    Dim dr As OleDbDataReader
    Private Sub Admin_login_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If textbox1.Text = "" Or textbox2.Text = "" Then
            MessageBox1.Show("", "Please Enter The Credentials")
            textbox1.Clear()
            textbox2.Clear()
        Else
            Try
                conn.Open()
                Dim cmd As New OleDbCommand("SELECT * FROM Admin_login WHERE username='" & textbox1.Text & "' AND password=" & textbox2.Text & "", conn)
                dr = cmd.ExecuteReader

                If dr.HasRows = True Then
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
    Private Sub textbox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles textbox2.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = Chr(Keys.Space) Then
            e.Handled = True
            MessageBox1.Show("", "This Field Accept Integer Values Only")
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