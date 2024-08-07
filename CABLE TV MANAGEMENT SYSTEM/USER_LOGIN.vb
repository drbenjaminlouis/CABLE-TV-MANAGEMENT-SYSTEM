﻿Imports System.Data.OleDb

Public Class USER_LOGIN
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
    Dim dr As OleDbDataReader
    Private Sub USER_LOGIN_LOAD(sender As Object, e As EventArgs) Handles MyBase.Load
        LogType_Detector.LoginType = "CUSTOMER"
        TV_Year_Updater()
        BroadBand_Year_Updater()
    End Sub
    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Me.Hide()
        Admin_Login.Show()
    End Sub
    Private Sub Guna2ControlBox3_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox3.Click
        Application.Exit()
    End Sub
    Private Sub Guna2ControlBox4_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox4.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub LOGIN_BTN_Click(sender As Object, e As EventArgs) Handles LOGIN_BTN.Click
        If textbox1.Text = "" Or textbox2.Text = "" Then
            MessageBox1.Show("", "Please Enter The Credentials")
            textbox1.Clear()
            textbox2.Clear()
        Else
            Try
                conn.Open()
                Dim cmd As New OleDbCommand("SELECT * FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@USERNAME AND CUST_PASSWORD=@PASSWORD", conn)
                cmd.Parameters.AddWithValue("@USERNAME", textbox1.Text)
                cmd.Parameters.AddWithValue("@PASSWORD", textbox2.Text)
                dr = cmd.ExecuteReader
                If dr.HasRows = True Then
                    LogType_Detector.UserName = textbox1.Text
                    LogType_Detector.LoginType = "CUSTOMER"
                    Me.Hide()
                    Dim cust_dash As New CUSTOMER_DASHBOARD
                    cust_dash.Show()
                Else
                    MessageBox1.Show("", "Invalid Username Or Password")
                    textbox1.Clear()
                    textbox2.Clear()
                End If
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("An Error Occured While Cust_Login: " & ex.Message)
                MessageBox1.Show("An Error Occured While Login: Check Log For More Details")
            Finally
                conn.Close()
                dr.Close()
            End Try
        End If
    End Sub

    Private Sub Show_Password_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Password.CheckedChanged
        If Show_Password.Checked Then textbox2.PasswordChar = Convert.ToChar(0) Else textbox2.PasswordChar = Convert.ToChar("*")
        textbox2.UseSystemPasswordChar = Not Show_Password.Checked
    End Sub
End Class