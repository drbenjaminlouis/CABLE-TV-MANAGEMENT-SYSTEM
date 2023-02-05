Module Payment_Sync
    Public Function Payment_Sync()
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
        Return 0
    End Function
End Module
