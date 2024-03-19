Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class CUSTOMER_DETAILS_NEW
    Dim flag As Integer = 1
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Simulate a button click
            SEARCH_BTN.PerformClick()
        End If
    End Sub
    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        If CUST_CRF_TEXTBOX.Text = "" And CUST_MOBILE_TEXTBOX.Text = "" And CUST_NAME_TEXTBOX.Text = "" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Enter Any Search Parameters.", "ALERT")
        Else
            If Not CUST_NAME_TEXTBOX.Text = "" Then
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                Try
                    connection.Open()
                    Dim query As String = "SELECT CUSTOMER_DETAILS.CRF, CUSTOMER_DETAILS.CUST_NAME AS [CUSTOMER NAME], CUSTOMER_DETAILS.CUST_HOUSE_NAME AS [HOUSE NAME], CUSTOMER_DETAILS.CUST_AREA AS AREA, CUSTOMER_DETAILS.CUST_PINCODE AS [PIN CODE], BROADBAND_CONNECTION_DETAILS.BROADBAND_CONNECTION AS [BROADBAND],BROADBAND_CONNECTION_DETAILS.STATUS,TV_CONNECTION_DETAILS.CUST_TV_CONNECTION AS [TV CONNECTION],TV_CONNECTION_DETAILS.TV_CONNECTION_STATUS AS [TV STATUS],CUSTOMER_DETAILS.CUST_MOBILE AS MOBILE, CUSTOMER_DETAILS.CUST_EMAIL AS EMAIL " &
                                  "FROM ((CUSTOMER_DETAILS " &
                                  "LEFT JOIN BROADBAND_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = BROADBAND_CONNECTION_DETAILS.CRF) " &
                                  "LEFT JOIN TV_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = TV_CONNECTION_DETAILS.CRF)" &
                                  "WHERE CUSTOMER_DETAILS.CUST_NAME = @cust_name;"
                    Dim adapter As New OleDbDataAdapter(query, connection)
                    adapter.SelectCommand.Parameters.AddWithValue("@cust_name", CUST_NAME_TEXTBOX.Text)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    CUST_DATA_GRID.DataSource = dataTable
                    flag = 0
                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("An Error Occured While Fetching Details Based On Customer Name: " & ex.Message)
                    MessageBox.Show("An Error Occured While Fetching Data: Check Log For More Details.", "ALERT")
                Finally
                    connection.Close()
                End Try
                'MessageBox.Show("Please Enter CRF Number.", "ALERT")
            ElseIf Not CUST_MOBILE_TEXTBOX.Text = "" Then
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                Try
                    connection.Open()
                    Dim query As String = "SELECT CUSTOMER_DETAILS.CRF, CUSTOMER_DETAILS.CUST_NAME AS [CUSTOMER NAME], CUSTOMER_DETAILS.CUST_HOUSE_NAME AS [HOUSE NAME], CUSTOMER_DETAILS.CUST_AREA AS AREA, CUSTOMER_DETAILS.CUST_PINCODE AS [PIN CODE], BROADBAND_CONNECTION_DETAILS.BROADBAND_CONNECTION AS [BROADBAND],BROADBAND_CONNECTION_DETAILS.STATUS,TV_CONNECTION_DETAILS.CUST_TV_CONNECTION AS [TV CONNECTION],TV_CONNECTION_DETAILS.TV_CONNECTION_STATUS AS [TV STATUS],CUSTOMER_DETAILS.CUST_MOBILE AS MOBILE, CUSTOMER_DETAILS.CUST_EMAIL AS EMAIL " &
                                  "FROM ((CUSTOMER_DETAILS " &
                                  "LEFT JOIN BROADBAND_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = BROADBAND_CONNECTION_DETAILS.CRF) " &
                                  "LEFT JOIN TV_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = TV_CONNECTION_DETAILS.CRF)" &
                                  "WHERE CUSTOMER_DETAILS.CUST_MOBILE = @CUST_MOBILE;"
                    Dim adapter As New OleDbDataAdapter(query, connection)
                    adapter.SelectCommand.Parameters.AddWithValue("@CUST_MOBILE", CUST_MOBILE_TEXTBOX.Text)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    CUST_DATA_GRID.DataSource = dataTable
                    flag = 0
                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("An Error Occured While Fetching Details Based On Mobile Number: " & ex.Message)
                    MessageBox.Show("An Error Occured While Fetching Data: Check Log For More Details.", "ALERT")
                Finally
                    connection.Close()
                End Try
                'MessageBox.Show("Please Enter Mobile Number.", "ALERT")
            ElseIf Not CUST_CRF_TEXTBOX.Text = "" Then
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                Try
                    connection.Open()
                    Dim query As String = "SELECT CUSTOMER_DETAILS.CRF, CUSTOMER_DETAILS.CUST_NAME AS [CUSTOMER NAME], CUSTOMER_DETAILS.CUST_HOUSE_NAME AS [HOUSE NAME], CUSTOMER_DETAILS.CUST_AREA AS AREA, CUSTOMER_DETAILS.CUST_PINCODE AS [PIN CODE], BROADBAND_CONNECTION_DETAILS.BROADBAND_CONNECTION AS [BROADBAND],BROADBAND_CONNECTION_DETAILS.STATUS,TV_CONNECTION_DETAILS.CUST_TV_CONNECTION AS [TV CONNECTION],TV_CONNECTION_DETAILS.TV_CONNECTION_STATUS AS [TV STATUS],CUSTOMER_DETAILS.CUST_MOBILE AS MOBILE, CUSTOMER_DETAILS.CUST_EMAIL AS EMAIL " &
                                  "FROM ((CUSTOMER_DETAILS " &
                                  "LEFT JOIN BROADBAND_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = BROADBAND_CONNECTION_DETAILS.CRF) " &
                                  "LEFT JOIN TV_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = TV_CONNECTION_DETAILS.CRF)" &
                                  "WHERE CUSTOMER_DETAILS.CRF = @CRF;"
                    Dim adapter As New OleDbDataAdapter(query, connection)
                    adapter.SelectCommand.Parameters.AddWithValue("@CRF", CUST_CRF_TEXTBOX.Text)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)
                    CUST_DATA_GRID.DataSource = dataTable
                    flag = 0
                Catch ex As Exception
                    ErrorAlert.Play()
                    LogError("An Error Occured While Fetching Details Based On Customer Name: " & ex.Message)
                    MessageBox.Show("An Error Occured While Fetching Data: Check Log For More Details.", "ALERT")
                Finally
                    connection.Close()
                End Try
                'MessageBox.Show("Please Enter Mobile Number.", "ALERT")
            End If
        End If
    End Sub
    Public Function DataLoder()
        Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            connection.Open()
            Dim query As String = "SELECT CUSTOMER_DETAILS.CRF, CUSTOMER_DETAILS.CUST_NAME AS [CUSTOMER NAME], CUSTOMER_DETAILS.CUST_HOUSE_NAME AS [HOUSE NAME], CUSTOMER_DETAILS.CUST_AREA AS AREA, CUSTOMER_DETAILS.CUST_PINCODE AS [PIN CODE], BROADBAND_CONNECTION_DETAILS.BROADBAND_CONNECTION AS [BROADBAND],BROADBAND_CONNECTION_DETAILS.STATUS,TV_CONNECTION_DETAILS.CUST_TV_CONNECTION AS [TV CONNECTION],TV_CONNECTION_DETAILS.TV_CONNECTION_STATUS AS [TV STATUS],CUSTOMER_DETAILS.CUST_MOBILE AS MOBILE, CUSTOMER_DETAILS.CUST_EMAIL AS EMAIL " &
                                  "FROM ((CUSTOMER_DETAILS " &
                                  "LEFT JOIN BROADBAND_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = BROADBAND_CONNECTION_DETAILS.CRF) " &
                                  "LEFT JOIN TV_CONNECTION_DETAILS ON CUSTOMER_DETAILS.CRF = TV_CONNECTION_DETAILS.CRF);"
            Dim adapter As New OleDbDataAdapter(query, connection)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            CUST_DATA_GRID.DataSource = dataTable
            CUST_DATA_GRID.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(5).Width = 120
            CUST_DATA_GRID.Columns(0).Width = 50
            CUST_DATA_GRID.Columns(1).Width = 150
            CUST_DATA_GRID.Columns(2).Width = 150
            CUST_DATA_GRID.Columns(3).Width = 100
            CUST_DATA_GRID.Columns(4).Width = 120
            CUST_DATA_GRID.Columns(6).Width = 60
            CUST_DATA_GRID.Columns(7).Width = 150
            CUST_DATA_GRID.Columns(8).Width = 130
            CUST_DATA_GRID.Columns(9).Width = 100
            CUST_DATA_GRID.Columns(10).Width = 230
            CUST_DATA_GRID.Sort(CUST_DATA_GRID.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        Catch ex As Exception
            ErrorAlert.Play()
            LogError("An Error Occured While Fetching Details: " & ex.Message)
            MessageBox.Show("An Error Occured While Fetching Details: Please Check Log For More Details", "ALERT")
        Finally
            connection.Close()
        End Try
        Return 0
    End Function
    Private Sub CUSTOMER_DETAILS_NEW_LOAD(sender As Object, e As EventArgs) Handles MyBase.Load
        CUST_CRF_TEXTBOX.Clear()
        CUST_NAME_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()

        DataLoder()
        flag = 1
    End Sub

    Private Sub RESET_BTN_Click(sender As Object, e As EventArgs) Handles RESET_BTN.Click
        CUST_CRF_TEXTBOX.Clear()
        CUST_NAME_TEXTBOX.Clear()
        CUST_MOBILE_TEXTBOX.Clear()
    End Sub

    Private Sub CUST_CRF_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_CRF_TEXTBOX.TextChanged
        If CUST_CRF_TEXTBOX.Text = "" And flag = 0 Then
            DataLoder()
        Else
            CUST_MOBILE_TEXTBOX.Clear()
            CUST_NAME_TEXTBOX.Clear()
        End If
    End Sub

    Private Sub CUST_NAME_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_NAME_TEXTBOX.TextChanged
        If CUST_NAME_TEXTBOX.Text = "" And flag = 0 Then
            DataLoder()
        Else
            CUST_MOBILE_TEXTBOX.Clear()
            CUST_CRF_TEXTBOX.Clear()
        End If
    End Sub

    Private Sub CUST_MOBILE_TEXTBOX_TextChanged(sender As Object, e As EventArgs) Handles CUST_MOBILE_TEXTBOX.TextChanged
        If CUST_MOBILE_TEXTBOX.Text = "" And flag = 0 Then
            DataLoder()
        Else
            CUST_CRF_TEXTBOX.Clear()
            CUST_NAME_TEXTBOX.Clear()
        End If
    End Sub
    Private Sub CUST_NAME_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CUST_NAME_TEXTBOX.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Letters Are Allowed.", "ALERT")
        End If
    End Sub
    Private Sub CUST_MOBILE_TEXTBOX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CUST_MOBILE_TEXTBOX.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Only Number Are Allowed.", "ALERT")
        End If
    End Sub
End Class