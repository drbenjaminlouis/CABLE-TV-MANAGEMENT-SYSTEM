Imports System.Data.OleDb

Module COMPLAINT_NO_GENERATOR
    Public Function generateComplaint()
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()
        Dim cmd As New OleDbCommand("SELECT MAX(C_ID) FROM CUST_COMPLAINTS", con)
        Dim result As Object = cmd.ExecuteScalar()
        Dim maxInvoiceNumber As Integer = 0
        If Not IsDBNull(result) Then
            maxInvoiceNumber = CInt(result)
        End If
        Dim new_c_id As Integer = maxInvoiceNumber + 1
        If new_c_id < 1000 Then
            new_c_id = 1000
        End If
        con.Close()
        Return new_c_id
    End Function
End Module
