Imports System.Data.OleDb
Imports System.Globalization
Imports System.Transactions

Module Payment_Sync
    Public Function Payment_Sync()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb")
        con.Open()
        Dim currentYear As Integer = DateTime.Now.Year
        Dim currentMonth As String = DateTime.Now.ToString("MMMM")
        Dim updateSql As String = "UPDATE TV_PAYMENT_DETAILS SET " & currentMonth & " = 'NOT PAID' WHERE CURRENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'NILL'"
        Dim updateSql2 As String = "UPDATE BROADBAND_PAYMENT_DETAILS SET " & currentMonth & " = 'NOT PAID' WHERE CURRENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'NILL'"
        Dim cmd As New OleDb.OleDbCommand(updateSql, con)
        Dim cmd2 As New OleDb.OleDbCommand(updateSql2, con)
        cmd.ExecuteNonQuery()
        cmd2.ExecuteNonQuery()
        con.Close()
        Return 0
    End Function
    Public Function UpdatePaymentStatus(ByVal conn As OleDbConnection, ByVal crf As String, ByVal registrationDate As Date) As Boolean
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\Database\Customer_Details_Db.accdb")
        Dim sql As String = "UPDATE [TV_PAYMENT_DETAILS] SET "
        Dim currentMonth As Integer = DateTime.Today.Month
        Dim startMonth As Integer = registrationDate.Month

        For i As Integer = startMonth To currentMonth
            Dim monthName As String = DateTimeFormatInfo.CurrentInfo.GetMonthName(i).ToLower()
            sql += monthName + "='Not Paid', "
        Next

        sql = sql.Substring(0, sql.Length - 2) 'Remove the last comma and space
        sql += " WHERE CRF='" + crf + "' AND " + startMonth.ToString() + "<=" + currentMonth.ToString() + " AND " + currentMonth.ToString() + "NILL;"

        Dim cmd As New OleDbCommand(sql, con)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
        Return 1
    End Function
End Module
