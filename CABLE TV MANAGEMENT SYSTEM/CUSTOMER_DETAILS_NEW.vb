Imports System.Data.OleDb

Public Class CUSTOMER_DETAILS_NEW
    Private Sub ADD_CUST_CREATEBTN_Click(sender As Object, e As EventArgs) Handles ADD_CUST_CREATEBTN.Click

    End Sub
    Private Sub CUSTOMER_DETAILS_NEW_LOAD(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath
        Dim connection As New OleDbConnection(connectionString)
        connection.Open()
        Dim query As String = "SELECT CUSTOMER_DETAILS.CRF, CUSTOMER_DETAILS.CUST_NAME, CUSTOMER_DETAILS.CUST_HOUSE_NAME, CUSTOMER_DETAILS.CUST_AREA, CUSTOMER_DETAILS.CUST_PINCODE, BROADBAND_CONNECTION_DETAILS.BROADBAND_CONNECTION,TV_CONNECTION_DETAILS.CUST_TV_CONNECTION, CUSTOMER_DETAILS.CUST_MOBILE, CUSTOMER_DETAILS.CUST_EMAIL " &
                      "FROM ((CUSTOMER_DETAILS " &
                      "LEFT JOIN BROADBAND_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = BROADBAND_CONNECTION_DETAILS.CRF) " &
                      "LEFT JOIN TV_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = TV_CONNECTION_DETAILS.CRF);"
        Dim adapter As New OleDbDataAdapter(query, connection)
        Dim dataTable As New DataTable()
        adapter.Fill(dataTable)
        CUST_DATA_GRID.DataSource = dataTable
        connection.Close()
    End Sub
End Class