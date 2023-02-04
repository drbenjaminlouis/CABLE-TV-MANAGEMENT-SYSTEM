Imports System.Data.OleDb
Public Class Form1
    Private Sub UpdateDatabase()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb")
        con.Open()
        Dim currentYear As Integer = DateTime.Now.Year
        Dim currentMonth As String = DateTime.Now.ToString("MMMM")
        Dim updateSql As String = "UPDATE TV_PAYMENT_DETAILS SET " & currentMonth & " = 'NOT PAID' WHERE CURRENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'PENDING'"
        Dim updateSql2 As String = "UPDATE BROADBAND_PAYMENT_DETAILS SET " & currentMonth & " = 'NOT PAID' WHERE CURRENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'PENDING'"
        Dim cmd As New OleDb.OleDbCommand(updateSql, con)
        Dim cmd2 As New OleDb.OleDbCommand(updateSql2, con)
        cmd.ExecuteNonQuery()
        cmd2.ExecuteNonQuery()
        con.Close()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MyProgress.Increment(1)
        If MyProgress.Value = 100 Then
            Guna2ProgressIndicator1.Start()
            Me.Hide()
            UpdateDatabase()
            Dim log_selector As New Admin_Dashboard
            log_selector.Show()
            Timer1.Enabled = False
            Guna2ProgressIndicator1.Stop()
        End If

    End Sub


End Class