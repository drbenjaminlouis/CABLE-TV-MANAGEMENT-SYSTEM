Imports System.Data.OleDb

Module Invoice_No_Generator
    Public Function generateInvoice()
        Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        con.Open()

        Dim cmd As New OleDbCommand("SELECT MAX(INVOICE_NO) FROM INVOICE_DETAILS", con)
        Dim result As Object = cmd.ExecuteScalar()

        Dim maxInvoiceNumber As Integer = 0
        If Not IsDBNull(result) Then
            maxInvoiceNumber = CInt(result)
        End If

        Dim newInvoiceNumber As Integer = maxInvoiceNumber + 1
        If newInvoiceNumber < 1000 Then
            newInvoiceNumber = 1000
        End If

        con.Close()

        Return newInvoiceNumber
    End Function
End Module
