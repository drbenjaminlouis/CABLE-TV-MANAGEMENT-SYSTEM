Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.OleDb
Imports System.IO
Imports Microsoft.Office.Interop
Imports PdfSharp.Charting

Public Class TV_CONNECTION_REPORT
    ReadOnly current_year As Integer = DateTime.Now.Year
    ReadOnly currentMonth As String = DateTime.Now.ToString("MMMM").ToUpper
    ReadOnly yearList As New List(Of Integer)
    Private Function GetCableTVData() As DataTable
        Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        connection.Open()
        Dim MONTH_NAME = MONTH_COMBOBOX.SelectedItem
        Dim current_year = YEAR_COMBOBOX.SelectedItem
        Dim STATUS = SORT_COMBOBOX.SelectedItem
        If MONTH_COMBOBOX.SelectedItem = "JANUARY" Then
            MONTH_NAME = "JANUARY"
        ElseIf MONTH_COMBOBOX.SelectedItem = "FEBRUARY" Then
            MONTH_NAME = "FEBRUARY"
        ElseIf MONTH_COMBOBOX.SelectedItem = "MARCH" Then
            MONTH_NAME = "MARCH"
        ElseIf MONTH_COMBOBOX.SelectedItem = "APRIL" Then
            MONTH_NAME = "APRIL"
        ElseIf MONTH_COMBOBOX.SelectedItem = "MAY" Then
            MONTH_NAME = "MAY"
        ElseIf MONTH_COMBOBOX.SelectedItem = "JUNE" Then
            MONTH_NAME = "JUNE"
        ElseIf MONTH_COMBOBOX.SelectedItem = "JULY" Then
            MONTH_NAME = "JULY"
        ElseIf MONTH_COMBOBOX.SelectedItem = "AUGUST" Then
            MONTH_NAME = "AUGUST"
        ElseIf MONTH_COMBOBOX.SelectedItem = "SEPTEMBER" Then
            MONTH_NAME = "SEPTEMBER"
        ElseIf MONTH_COMBOBOX.SelectedItem = "OCTOBER" Then
            MONTH_NAME = "OCTOBER"
        ElseIf MONTH_COMBOBOX.SelectedItem = "NOVEMBER" Then
            MONTH_NAME = "NOVEMBER"
        ElseIf MONTH_COMBOBOX.SelectedItem = "DECEMBER" Then
            MONTH_NAME = "DECEMBER"
        Else
            MONTH_COMBOBOX.SelectedItem = Date.Now.Month.ToString("MMMM").ToUpper
        End If
        Dim query As String = "SELECT 
                            CUSTOMER_DETAILS.CRF, 
                            CUSTOMER_DETAILS.CUST_NAME AS [CUSTOMER NAME], 
                            CUSTOMER_DETAILS.CUST_HOUSE_NAME AS [HOUSE NAME],
                            CUSTOMER_DETAILS.CUST_MOBILE AS [MOBILE],
                            CUSTOMER_DETAILS.CUST_EMAIL AS [EMAIL],
                            TV_CONNECTION_DETAILS.TV_CONNECTION_STATUS AS [STATUS],
                            TV_PAYMENT_DETAILS.PAYMENT_YEAR AS [YEAR],
                            TV_PAYMENT_DETAILS.[" & MONTH_NAME & "]" &
                            "FROM ((TV_PAYMENT_DETAILS " &
                            "LEFT JOIN TV_CONNECTION_DETAILS ON TV_PAYMENT_DETAILS.CRF = TV_CONNECTION_DETAILS.CRF)" &
                            "LEFT JOIN CUSTOMER_DETAILS ON TV_PAYMENT_DETAILS.CRF = CUSTOMER_DETAILS.CRF)" &
                            "WHERE TV_PAYMENT_DETAILS.PAYMENT_YEAR = @YEAR AND TV_PAYMENT_DETAILS.[" & MONTH_NAME & "]=@STATUS ORDER BY CUSTOMER_DETAILS.CRF ASC;"
        Dim adapter As New OleDbDataAdapter(query, connection)
        adapter.SelectCommand.Parameters.AddWithValue("@YEAR", YEAR_COMBOBOX.SelectedItem)
        adapter.SelectCommand.Parameters.AddWithValue("@STATUS", SORT_COMBOBOX.SelectedItem)
        Dim dataTable As New DataTable()
        adapter.Fill(dataTable)
        connection.Close()
        Return dataTable
    End Function
    Private Sub TV_CONNECTION_REPORT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        connection.Open()
        Dim command1 As New OleDbCommand("SELECT DISTINCT(PAYMENT_YEAR) FROM TV_PAYMENT_DETAILS", connection)
        Dim reader5 As OleDbDataReader = command1.ExecuteReader()
        If reader5.HasRows Then
            While reader5.Read()
                Dim year As Integer = reader5.GetInt32(0)
                If Not yearList.Contains(year) Then
                    yearList.Add(year)
                End If
            End While
        End If
        For Each year As Integer In yearList
            YEAR_COMBOBOX.Items.Add(year)
        Next
        YEAR_COMBOBOX.SelectedItem = Date.Now.Year
        If YEAR_COMBOBOX.SelectedItem = Date.Now.Year Then
            MONTH_COMBOBOX.Items.Clear()
            Dim currentDate As DateTime = DateTime.Now
            Dim currentMonthNumber As Integer = Month(currentDate)
            For month As Integer = 1 To currentMonthNumber
                ' Create a datetime object with the current year and month
                Dim dateValue As New DateTime(current_year, month, 1)
                ' Add the month name to the MONTH_COMBOBOX
                MONTH_COMBOBOX.Items.Add(dateValue.ToString("MMMM").ToUpper())
            Next
            MONTH_COMBOBOX.SelectedItem = currentMonth
        End If
        SORT_COMBOBOX.Items.Add("PAID")
        SORT_COMBOBOX.Items.Add("NOT PAID")
        SORT_COMBOBOX.SelectedItem = "PAID"


        If GetCableTVData.Rows.Count = 0 Then

        Else
            CUST_DATA_GRID.DataSource = GetCableTVData()
            FILETYPE_COMBOBOX.Items.Clear()
            FILETYPE_COMBOBOX.Items.Add("PDF")
            FILETYPE_COMBOBOX.Items.Add("EXCEL")
            CUST_DATA_GRID.Columns(0).Width = 100
            CUST_DATA_GRID.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(1).Width = 150
            CUST_DATA_GRID.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(2).Width = 150
            CUST_DATA_GRID.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(3).Width = 120
            CUST_DATA_GRID.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(4).Width = 190
            CUST_DATA_GRID.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(5).Width = 150
            CUST_DATA_GRID.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            CUST_DATA_GRID.Columns(6).Width = 120
            CUST_DATA_GRID.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(7).Width = 100
            CUST_DATA_GRID.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            CUST_DATA_GRID.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End If
    End Sub
    Private Sub FETCH_BTN_Click(sender As Object, e As EventArgs) Handles FETCH_BTN.Click
        If YEAR_COMBOBOX.SelectedItem = Nothing Then
            MessageBox.Show("Please Select Year.", "ALERT")
        ElseIf MONTH_COMBOBOX.SelectedItem = "" Then
            MessageBox.Show("Please Select Month.", "ALERT")
        ElseIf SORT_COMBOBOX.SelectedItem = "" Then
            MessageBox.Show("Please Select Sort Type.", "ALERT")
        Else
            If GetCableTVData.Rows.Count = 0 Then
                CUST_DATA_GRID.DataSource = Nothing
                MessageBox.Show("No Data Found.", "ALERT")
            Else
                CUST_DATA_GRID.DataSource = GetCableTVData()
                FILETYPE_COMBOBOX.Items.Clear()
                FILETYPE_COMBOBOX.Items.Add("PDF")
                FILETYPE_COMBOBOX.Items.Add("EXCEL")
            End If
        End If
    End Sub

    Private Sub GENERATE_BTN_Click(sender As Object, e As EventArgs) Handles GENERATE_BTN.Click
        If FILETYPE_COMBOBOX.SelectedItem = "" Then
            ErrorAlert.Play()
            MessageBox.Show("Please Select File Type.", "ALERT")
        End If
        If FILETYPE_COMBOBOX.SelectedItem = "PDF" Then
            ExportToPDF.ExportAsPDF(CUST_DATA_GRID)
        Else
            If FILETYPE_COMBOBOX.SelectedItem = "EXCEL" Then
                ExportToExcel.ExportToExcel(CUST_DATA_GRID)
            End If
        End If
    End Sub
    Private Sub YEAR_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles YEAR_COMBOBOX.SelectedIndexChanged
        If Not YEAR_COMBOBOX.SelectedItem = Date.Now.Year Then
            MONTH_COMBOBOX.Items.Clear()
            Dim selected_year As Integer = YEAR_COMBOBOX.SelectedItem
            For month As Integer = 1 To 12
                ' Create a datetime object with the current year and month
                Dim dateValue As New DateTime(selected_year, month, 1)
                ' Add the month name to the MONTH_COMBOBOX
                MONTH_COMBOBOX.Items.Add(dateValue.ToString("MMMM").ToUpper())
            Next
            MONTH_COMBOBOX.SelectedItem = "JANUARY"
        Else
            MONTH_COMBOBOX.Items.Clear()
            Dim currentDate As DateTime = DateTime.Now
            Dim currentMonthNumber As Integer = Month(currentDate)
            For month As Integer = 1 To currentMonthNumber
                ' Create a datetime object with the current year and month
                Dim dateValue As New DateTime(current_year, month, 1)
                ' Add the month name to the MONTH_COMBOBOX
                MONTH_COMBOBOX.Items.Add(dateValue.ToString("MMMM").ToUpper())
            Next
            MONTH_COMBOBOX.SelectedItem = currentMonth
        End If
        SORT_COMBOBOX.SelectedItem = SORT_COMBOBOX.SelectedItem
    End Sub
End Class
Public Class BlackBackground
    Inherits PdfPageEventHelper

    Public Overrides Sub OnEndPage(ByVal writer As PdfWriter, ByVal document As Document)
        writer.DirectContentUnder.SetColorFill(BaseColor.BLACK)
        writer.DirectContentUnder.Rectangle(document.PageSize.Left, document.PageSize.Bottom, document.PageSize.Width, document.PageSize.Height)
        writer.DirectContentUnder.Fill()
    End Sub

End Class