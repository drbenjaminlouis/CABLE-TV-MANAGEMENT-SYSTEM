Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.OleDb
Imports System.IO

Public Class BROADBAND_CONNECTION_REPORT
    Dim current_year As Integer = DateTime.Now.Year
    Dim currentMonth As String = DateTime.Now.ToString("MMMM").ToUpper
    Dim status_text As String = "PAID"
    Dim yearList As New List(Of Integer)
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
                            BROADBAND_CONNECTION_DETAILS.STATUS AS [STATUS],
                            BROADBAND_PAYMENT_DETAILS.PAYMENT_YEAR AS [YEAR],
                            BROADBAND_PAYMENT_DETAILS.[" & MONTH_NAME & "]" &
                            "FROM ((BROADBAND_PAYMENT_DETAILS " &
                            "LEFT JOIN BROADBAND_CONNECTION_DETAILS ON BROADBAND_PAYMENT_DETAILS.CRF = BROADBAND_CONNECTION_DETAILS.CRF)" &
                            "LEFT JOIN CUSTOMER_DETAILS ON BROADBAND_PAYMENT_DETAILS.CRF = CUSTOMER_DETAILS.CRF)" &
                            "WHERE BROADBAND_PAYMENT_DETAILS.PAYMENT_YEAR = @YEAR AND BROADBAND_PAYMENT_DETAILS.[" & MONTH_NAME & "]=@STATUS;"
        Dim adapter As New OleDbDataAdapter(query, connection)
        adapter.SelectCommand.Parameters.AddWithValue("@YEAR", YEAR_COMBOBOX.SelectedItem)
        adapter.SelectCommand.Parameters.AddWithValue("@STATUS", SORT_COMBOBOX.SelectedItem)
        Dim dataTable As New DataTable()
        adapter.Fill(dataTable)
        connection.Close()
        Return dataTable
    End Function
    Private Sub ExportToPDF()
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "PDF Files|*.pdf"
        saveFileDialog1.Title = "Save PDF File"
        saveFileDialog1.ShowDialog()

        If saveFileDialog1.FileName <> "" Then
            Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
            Dim pdfWriter As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(saveFileDialog1.FileName, FileMode.Create))
            pdfDoc.Open()
            pdfWriter.PageEvent = New BlackBackground()
            Dim fontTopText As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 35, iTextSharp.text.Font.BOLD, BaseColor.WHITE)
            Dim topText As New Paragraph("BHARATH CABLE NETWORK", fontTopText) ' Add top text
            topText.Alignment = Element.ALIGN_CENTER ' Align text to the center
            topText.SpacingAfter = 20 ' Add margin below top text
            pdfDoc.Add(topText)

            Dim table As New PdfPTable(CUST_DATA_GRID.Columns.Count)
            table.WidthPercentage = 100 ' Set the table width to 100% of the page width
            Dim columnWidths(CUST_DATA_GRID.Columns.Count - 1) As Single
            columnWidths(0) = 40
            columnWidths(1) = 130
            columnWidths(2) = 100
            columnWidths(3) = 80
            columnWidths(4) = 175
            columnWidths(5) = 60
            columnWidths(6) = 45
            columnWidths(7) = 90

            For i As Integer = 0 To CUST_DATA_GRID.Columns.Count - 1
                ' Set the width of each column to 150
                Dim fontTopText1 As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE)
                Dim cell As New PdfPCell(New Phrase(CUST_DATA_GRID.Columns(i).HeaderText, fontTopText1))
                cell.BackgroundColor = New BaseColor(Color.Green)
                cell.HorizontalAlignment = Element.ALIGN_CENTER ' Align content to the left
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                cell.BorderWidthTop = 0
                cell.BorderWidthBottom = 0
                cell.BorderWidthLeft = 0
                cell.BorderColorRight = BaseColor.WHITE
                If i = 7 Then
                    cell.BorderWidthRight = 0
                End If
                cell.MinimumHeight = 40
                table.AddCell(cell)

            Next
            table.SetWidths(columnWidths)

            For i As Integer = 0 To CUST_DATA_GRID.Rows.Count - 1
                For j As Integer = 0 To CUST_DATA_GRID.Columns.Count - 1
                    Dim fontTopText2 As New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE)
                    Dim cell As New PdfPCell(New Phrase(CUST_DATA_GRID(j, i).Value.ToString(), fontTopText2))
                    cell.MinimumHeight = 30
                    cell.HorizontalAlignment = Element.ALIGN_LEFT ' Align content to the left
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    If j = 7 Then
                        cell.HorizontalAlignment = Element.ALIGN_CENTER ' Align content to the left
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    End If
                    ' cell.BorderWidthBottom = 1
                    ' cell.BorderColorBottom = BaseColor.WHITE ' Set border color to white
                    table.AddCell(cell)

                Next
            Next

            pdfDoc.Add(table)

            pdfDoc.Close()

            MessageBox.Show("PDF File exported successfully.", "Export PDF")
        End If
    End Sub
    Private Sub BROADBAND_CONNECTION_REPORT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            MessageBox.Show("No Data Found.", "ALERT")
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
        If FILETYPE_COMBOBOX.SelectedItem = "PDF" Then
            ExportToPDF()
        End If
<<<<<<< Updated upstream
<<<<<<< HEAD
        If FILETYPE_COMBOBOX.SelectedItem = "EXCEL" Then
            ExportToExcel.ExportToExcel(CUST_DATA_GRID)
        End If
=======
>>>>>>> 3a83c465034fc9331d67fabb34f7a0db1ec24a27
=======
        If FILETYPE_COMBOBOX.SelectedItem = "EXCEL" Then
            ExportToExcel.ExportToExcel(CUST_DATA_GRID)
        End If
>>>>>>> Stashed changes
    End Sub
End Class
