Imports System.Data.OleDb
Module PENDING_VS_RECEIVED
    Public Function TV_RECEIVED() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim command As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & month_name & "=@status AND PAYMENT_YEAR=@YEAR", con)
        command.Parameters.AddWithValue("@status", "PAID")
        command.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim reader As OleDbDataReader = command.ExecuteReader
        If reader.HasRows = True Then
            While reader.Read
                value = value + 250
            End While
        End If
        Return value
    End Function
    Public Function TV_PENDING() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim command As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & month_name & "=@status AND PAYMENT_YEAR=@YEAR", con)
        command.Parameters.AddWithValue("@status", "NOT PAID")
        command.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim reader As OleDbDataReader = command.ExecuteReader
        If reader.HasRows = True Then
            While reader.Read
                value = value + 250
            End While
        End If
        Return value
    End Function
    Public Function BROADBAND_RECEIVED() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim command As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & month_name & "=@status AND PAYMENT_YEAR=@YEAR", con)
        command.Parameters.AddWithValue("@status", "PAID")
        command.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim reader As OleDbDataReader = command.ExecuteReader
        If reader.HasRows = True Then
            While reader.Read
                value = value + 250
            End While
        End If
        Return value
    End Function
    Public Function BROADBAND_PENDING() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim command As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & month_name & "=@status AND PAYMENT_YEAR=@YEAR", con)
        command.Parameters.AddWithValue("@status", "NOT PAID")
        command.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim reader As OleDbDataReader = command.ExecuteReader
        If reader.HasRows = True Then
            While reader.Read
                value = value + 250
            End While
        End If
        Return value
    End Function
End Module
