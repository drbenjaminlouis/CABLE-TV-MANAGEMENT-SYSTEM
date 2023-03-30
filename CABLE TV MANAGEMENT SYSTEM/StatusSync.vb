Imports System.Data.OleDb
Imports Guna.UI2.WinForms
Module StatusSync
    'Function For Updating Connection Status To INACTIVE And Payment Status To NOT PAID 
    Public Function InactiveUpdater()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim currentYear As Integer = DateTime.Now.Year
            Dim currentMonth As String = DateTime.Now.ToString("MMMM")
            Dim currentdate As Date = DateAndTime.Now.Date
            Dim updateSql As String = "UPDATE TV_PAYMENT_DETAILS SET " & currentMonth & " = 'NOT PAID' WHERE PAYMENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'NILL'"
            Dim checker1 As String = "UPDATE TV_CONNECTION_DETAILS SET TV_CONNECTION_STATUS = 'INACTIVE' WHERE CUST_TV_CONNECTION = 'YES' AND TV_CONNECTION_STATUS= 'ACTIVE' AND EXPIRY_DATE < #" & Format(CDate(currentdate), "yyyy-MM-dd") & "#"
            Dim updateSql2 As String = "UPDATE BROADBAND_PAYMENT_DETAILS Set " & currentMonth & " = 'NOT PAID' WHERE PAYMENT_YEAR = " & currentYear & " AND " & currentMonth & " = 'NILL'"
            Dim checker2 As String = "UPDATE BROADBAND_CONNECTION_DETAILS SET STATUS = 'INACTIVE' WHERE BROADBAND_CONNECTION = 'YES' AND STATUS='ACTIVE' AND EXPIRY_DATE < #" & Format(CDate(currentdate), "yyyy-MM-dd") & "#"
            Dim cmd As New OleDb.OleDbCommand(updateSql, con)
            Dim cmd2 As New OleDb.OleDbCommand(updateSql2, con)
            Dim cmd3 As New OleDbCommand(checker1, con)
            Dim cmd4 As New OleDbCommand(checker2, con)
            cmd.ExecuteNonQuery()
            cmd2.ExecuteNonQuery()
            cmd3.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()
        Catch ex As Exception
            LogError("An Error Occured While Payment Sync: " & ex.Message)
            Dim messagebox As New Guna2MessageDialog
            messagebox.Style = MessageDialogStyle.Dark
            messagebox.Show("An Error Occured While Payment Sync: Please Check Log For More Details.", "ALERT")
        Finally
            con.Close()
        End Try
        Return 0
    End Function

    'Function For Updating TV Connection Status To Suspended
    Public Function SuspenderTV()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim cmd As New OleDbCommand("UPDATE TV_CONNECTION_DETAILS SET TV_CONNECTION_STATUS=@STATUS WHERE EXPIRY_DATE < @EXDATE", con)
            cmd.Parameters.AddWithValue("@STATUS", "SUSPENDED")
            Dim exdate As Date = Date.Today.AddDays(-60)
            cmd.Parameters.AddWithValue("@EXDATE", exdate)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
        Return 0
    End Function

    'Function For Uodating TV Connection Status To ACTIVE.
    Public Function ActivatorTV()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim cmd As New OleDbCommand("UPDATE TV_CONNECTION_DETAILS SET TV_CONNECTION_STATUS=@STATUS WHERE EXPIRY_DATE > @EXDATE AND REGISTRATION_DATE < @REG_DATE", con)
            cmd.Parameters.AddWithValue("@STATUS", "ACTIVE")
            Dim exdate As Date = Date.Today.ToString("dd-MM-yyyy")
            cmd.Parameters.AddWithValue("@EXDATE", exdate)
            cmd.Parameters.AddWithValue("@REG_DATE", Date.Today.ToString("dd-MM-yyyy"))
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
        Return 0
    End Function

    'Function For Updating BroadBand Connection Status To Suspended
    Public Function SuspenderBroadband()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim cmd As New OleDbCommand("UPDATE BROADBAND_CONNECTION_DETAILS SET STATUS=@STATUS WHERE EXPIRY_DATE < @EXDATE", con)
            cmd.Parameters.AddWithValue("@STATUS", "SUSPENDED")
            Dim exdate As Date = Date.Today.AddDays(-60)
            cmd.Parameters.AddWithValue("@EXDATE", exdate)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
        Return 0
    End Function

    'Function For Uodating BroadBand Connection Status To ACTIVE.
    Public Function ActivatorBroadband()
        Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim cmd As New OleDbCommand("UPDATE BROADBAND_CONNECTION_DETAILS SET STATUS=@STATUS WHERE EXPIRY_DATE > @EXDATE AND REGISTRATION_DATE < @REG_DATE", con)
            cmd.Parameters.AddWithValue("@STATUS", "ACTIVE")
            Dim exdate As Date = Date.Today.ToString("dd-MM-yyyy")
            cmd.Parameters.AddWithValue("@EXDATE", exdate)
            cmd.Parameters.AddWithValue("@REG_DATE", Date.Today.ToString("dd-MM-yyyy"))
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
        Return 0
    End Function
End Module
