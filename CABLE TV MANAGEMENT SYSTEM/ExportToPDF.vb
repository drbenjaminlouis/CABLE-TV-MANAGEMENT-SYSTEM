Imports Guna.UI2.WinForms
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Module ExportToPDF
    Public Sub ExportAsPDF(ByVal CUST_DATA_GRID As DataGridView)
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
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
            columnWidths(4) = 170
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

            MessageBox.Show("PDF File Exported Successfully.", "SUCCESS")
        End If
    End Sub
End Module
