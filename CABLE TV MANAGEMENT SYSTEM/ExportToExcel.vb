Imports Guna.UI2.WinForms
Imports Microsoft.Office.Interop

Module ExportToExcel
    Public Sub ExportToExcel(ByVal CUST_DATA_GRID As DataGridView)
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
        Try
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx"
            saveFileDialog1.Title = "Save Excel File"
            saveFileDialog1.ShowDialog()

            If saveFileDialog1.FileName <> "" Then
                'Save the workbook to the user-selected file path
                'Create an instance of Excel application
                Dim xlApp As New Excel.Application

                'Create a new workbook
                Dim xlWorkbook As Excel.Workbook = xlApp.Workbooks.Add()

                'Create a new worksheet
                Dim xlWorksheet As Excel.Worksheet = CType(xlWorkbook.Sheets.Add(), Excel.Worksheet)

                'Set the worksheet name
                xlWorksheet.Name = "Customer Data"

                'Set the header row
                For j As Integer = 0 To CUST_DATA_GRID.Columns.Count - 1
                    xlWorksheet.Cells(1, j + 1) = CUST_DATA_GRID.Columns(j).HeaderText
                Next

                'Format the header row
                Dim headerRange As Excel.Range = xlWorksheet.Range("A1", xlWorksheet.Cells(1, CUST_DATA_GRID.Columns.Count))
                headerRange.Font.Bold = True
                headerRange.RowHeight = 25
                headerRange.ColumnWidth = 25
                headerRange.Interior.Color = Color.Green
                headerRange.Font.Color = Color.White
                headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter

                'Set the data rows
                For i As Integer = 0 To CUST_DATA_GRID.Rows.Count - 1
                    Dim dataRow As DataGridViewRow = CUST_DATA_GRID.Rows(i)
                    For j As Integer = 0 To dataRow.Cells.Count - 1
                        xlWorksheet.Cells(i + 2, j + 1) = dataRow.Cells(j).Value
                    Next
                Next
                xlWorksheet.Range("A1:H1").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

                'Display the SaveFileDialog and get the user-selected file path

                xlWorkbook.SaveAs(saveFileDialog1.FileName)
                'Close the workbook and release the resources
                xlWorkbook.Close()
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
                MessageBox.Show("Excel File Exported Successfully.", "SUCCESS")
            End If
        Catch ex As Exception
            ErrorAlert.Play()
            LogError("An Error Occured While Exporting As Excel: " & ex.Message)
            MessageBox.Show("An Error Occured While Exporting As Excel: Check Log For More Details", "ALERT")
        End Try
    End Sub
End Module
