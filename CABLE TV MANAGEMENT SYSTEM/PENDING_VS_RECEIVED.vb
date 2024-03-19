Imports System.Data.OleDb
Module PENDING_VS_RECEIVED
    'Function For Calculating TV Received Payments For Current Month
    Public Function TV_RECEIVED() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
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
        Catch ex As Exception
            LogError("An Error Ocuured While Fetching TV Received Payments: " & ex.Message)
        Finally
            con.Close()
        End Try
        Return value
    End Function

    'Function For Calculating TV Pending Payments For Current Month
    Public Function TV_PENDING() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
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
        Catch ex As Exception
            LogError("An Error Ocuured While Fetching TV Pending Payments: " & ex.Message)
        Finally
            con.Close()
        End Try
        Return value
    End Function

    'Function For Calculating BroadBand Received Payments For Current Month
    Public Function BROADBAND_RECEIVED() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value2 As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim command As New OleDbCommand("SELECT DISTINCT(CRF) FROM BROADBAND_PAYMENT_DETAILS WHERE " & month_name & "=@status AND PAYMENT_YEAR=@YEAR", con)
            command.Parameters.AddWithValue("@status", "PAID")
            command.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
            Dim reader As OleDbDataReader = command.ExecuteReader
            If reader.HasRows = True Then
                While reader.Read
                    value2 = value2 + 250
                End While
            End If
        Catch ex As Exception
            LogError("An Error Ocuured While Fetching BroadBand Received Payments: " & ex.Message)
        Finally
            con.Close()
        End Try
        Return value2
    End Function

    'Function For Calculating BroadBand Pending Payments For Current Month
    Public Function BROADBAND_PENDING() As Integer
        Dim month_name As String = Date.Today.ToString("MMMM").ToUpper
        Dim value As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            con.Open()
            Dim command As New OleDbCommand("SELECT DISTINCT(CRF) FROM BROADBAND_PAYMENT_DETAILS WHERE " & month_name & "=@status AND PAYMENT_YEAR=@YEAR", con)
            command.Parameters.AddWithValue("@status", "NOT PAID")
            command.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
            Dim reader As OleDbDataReader = command.ExecuteReader
            If reader.HasRows = True Then
                While reader.Read
                    value = value + 250
                End While
            End If
            Return value
        Catch ex As Exception
            LogError("An Error Ocuured While Fetching BroadBand Pending Payments: " & ex.Message)
        Finally
            con.Close()
        End Try
        Return value
    End Function
End Module
