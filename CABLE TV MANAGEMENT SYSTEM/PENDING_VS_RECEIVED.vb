Imports System.Data.OleDb
Module PENDING_VS_RECEIVED
    Public Function TV_PENDING_AMT() As Integer
        Dim pending_amt_tv As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim MONTH_NAME As String = Date.Today.ToString("MMMM").ToUpper
        Dim tv_pend_fetcher As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & MONTH_NAME & "=@STATUS AND PAYMENT_YEAR=@YEAR", con)
        tv_pend_fetcher.Parameters.AddWithValue("@STATUS", "NOT PAID")
        tv_pend_fetcher.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim tv_pend_reader As OleDbDataReader = tv_pend_fetcher.ExecuteReader
        If tv_pend_reader.HasRows = True Then
            While tv_pend_reader.Read
                pending_amt_tv = pending_amt_tv + 250
            End While
        End If
        con.Close()
        Return pending_amt_tv
    End Function
    Public Function TV_RECEIVED_AMT() As Integer
        Dim received_amt_tv As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim MONTH_NAME As String = Date.Today.ToString("MMMM").ToUpper
        Dim tv_received_fetcher As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & MONTH_NAME & "=@STATUS AND PAYMENT_YEAR=@YEAR", con)
        tv_received_fetcher.Parameters.AddWithValue("@STATUS", "PAID")
        tv_received_fetcher.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim tv_received_reader As OleDbDataReader = tv_received_fetcher.ExecuteReader
        If tv_received_reader.HasRows = True Then
            While tv_received_reader.Read
                received_amt_tv = received_amt_tv + 250
            End While
        End If
        con.Close()
        Return received_amt_tv
    End Function
    Public Function BROADBAND_PENDING_AMT() As Integer
        Dim pending_amt_broadband As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim MONTH_NAME As String = Date.Today.ToString("MMMM").ToUpper
        Dim broadband_pend_fetcher As New OleDbCommand("SELECT DISTINCT(CRF) FROM BROADBAND_PAYMENT_DETAILS WHERE " & MONTH_NAME & "=@STATUS AND PAYMENT_YEAR=@YEAR", con)
        broadband_pend_fetcher.Parameters.AddWithValue("@STATUS", "NOT PAID")
        broadband_pend_fetcher.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim broadband_pend_reader As OleDbDataReader = broadband_pend_fetcher.ExecuteReader
        If broadband_pend_reader.HasRows = True Then
            While broadband_pend_reader.Read
                pending_amt_broadband = pending_amt_broadband + 250
            End While
        End If
        con.Close()
        Return pending_amt_broadband
    End Function
    Public Function BROADBAND_RECEIVED_AMT() As Integer
        Dim received_amt_broadband As Integer = 0
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim MONTH_NAME As String = Date.Today.ToString("MMMM").ToUpper
        Dim broadband_received_fetcher As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_PAYMENT_DETAILS WHERE " & MONTH_NAME & "=@STATUS AND PAYMENT_YEAR=@YEAR", con)
        broadband_received_fetcher.Parameters.AddWithValue("@STATUS", "PAID")
        broadband_received_fetcher.Parameters.AddWithValue("@YEAR", Date.Today.ToString("yyyy"))
        Dim broadband_received_reader As OleDbDataReader = broadband_received_fetcher.ExecuteReader
        If broadband_received_reader.HasRows = True Then
            While broadband_received_reader.Read
                received_amt_broadband = received_amt_broadband + 250
            End While
        End If
        con.Close()
        Return received_amt_broadband
    End Function
End Module
