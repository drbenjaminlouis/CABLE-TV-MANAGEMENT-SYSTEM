Imports System.Data.OleDb
Imports System.Globalization
Imports System.Transactions
Imports Guna.UI2.WinForms

Module Payment_Sync
    Public Function Payment_Sync()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Try
            Dim currentYear As Integer = DateTime.Now.Year
            Dim currentMonth As String = DateTime.Now.ToString("MMMM")
            Dim currentdate As Date = DateAndTime.Now.Date
            Dim updateSql As String = "UPDATE TV_PAYMENT_DETAILS SET " & currentMonth & " = 'NOT PAID' WHERE PAYMENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'NILL'"
            Dim checker1 As String = "UPDATE TV_CONNECTION_DETAILS SET TV_CONNECTION_STATUS = 'INACTIVE' WHERE CUST_TV_CONNECTION = 'YES' AND EXPIRY_DATE < #" & Format(CDate(currentdate), "yyyy-MM-dd") & "#"
            Dim updateSql2 As String = "UPDATE BROADBAND_PAYMENT_DETAILS Set " & currentMonth & " = 'NOT PAID' WHERE PAYMENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'NILL'"
            Dim checker2 As String = "UPDATE BROADBAND_CONNECTION_DETAILS SET STATUS = 'INACTIVE' WHERE BROADBAND_CONNECTION = 'YES' AND EXPIRY_DATE < #" & Format(CDate(currentdate), "yyyy-MM-dd") & "#"
            Dim cmd As New OleDb.OleDbCommand(updateSql, con)
            Dim cmd2 As New OleDb.OleDbCommand(updateSql2, con)
            Dim cmd3 As New OleDbCommand(checker1, con)
            Dim cmd4 As New OleDbCommand(checker2, con)
            cmd.ExecuteNonQuery()
            cmd2.ExecuteNonQuery()
            cmd3.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()
            con.Close()
            Return 0
        Catch ex As Exception
            LogError("An Error Occured While Payment Sync: " & ex.Message)
            Dim messagebox As New Guna2MessageDialog
            messagebox.Style = MessageDialogStyle.Dark
            messagebox.Show("An Error Occured While Payment Sync: Please Check Log For More Details.", "ALERT")
        End Try
        Return 0
    End Function
End Module
