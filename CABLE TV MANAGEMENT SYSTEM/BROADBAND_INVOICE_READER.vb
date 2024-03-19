Imports System.Data.OleDb
Imports System.Globalization
Imports System.IO
Public Class BROADBAND_INVOICE_READER
    Dim flag As Boolean
    Dim FILE_PATH As String
    Dim cust_crf As Integer
    ReadOnly current_year As Integer = DateTime.Now.Year
    ReadOnly currentMonth As String = DateTime.Now.ToString("MMMM").ToUpper
    ReadOnly yearList As New List(Of Integer)
    Public Sub New()
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf BROADBAND_INVOICE_READER_Load
    End Sub
    Private Sub BROADBAND_INVOICE_READER_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PDF_VIEWER.Source = New Uri("file:///" & FileNotFound)
        If Not UserName = Nothing Then
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            connection.Open()
            Dim crfpicker As New OleDbCommand("SELECT CRF FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@USERNAME", connection)
            Dim username As String = LogType_Detector.UserName
            crfpicker.Parameters.AddWithValue("@USERNAME", username)
            Dim crfreader As OleDbDataReader = crfpicker.ExecuteReader
            If crfreader.HasRows = True Then
                While crfreader.Read
                    cust_crf = crfreader.GetInt32(0)
                End While
                Dim command1 As New OleDbCommand("SELECT DISTINCT(PAYMENT_YEAR) FROM BROADBAND_PAYMENT_DETAILS WHERE CRF=@CRF", connection)
                command1.Parameters.AddWithValue("@CRF", cust_crf)
                Dim reader5 As OleDbDataReader = command1.ExecuteReader()
                If reader5.HasRows = True Then
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
            End If
            connection.Close()
        End If
        AddHandler YEAR_COMBOBOX.Click, AddressOf YEAR_COMBOBOX_SelectedIndexChanged
        AddHandler CUST_DATA_GRID.SelectionChanged, AddressOf CUST_DATA_GRID_SelectionChanged
        AddHandler DOWNLOAD_BTN.Click, AddressOf DOWNLOAD_BTN_Click_1
        ' AddHandler pd.PrintPage, AddressOf pd_PrintPage
    End Sub
    Private Sub CUST_DATA_GRID_SelectionChanged(sender As Object, e As EventArgs)
        If flag = True Then
            If CUST_DATA_GRID.SelectedRows.Count > 0 Then
                Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
                connection.Open()
                'Try
                Dim invoicepicker As New OleDbCommand("SELECT INVOICE FROM INVOICE_DETAILS WHERE CRF=@CRF AND INVOICE_NO=@INVOICE_NO", connection)
                invoicepicker.Parameters.AddWithValue("@CRF", cust_crf)
                Dim selectedRow As DataGridViewRow = CUST_DATA_GRID.SelectedRows(0)
                Dim columnValue As Integer = selectedRow.Cells("INVOICE NO").Value
                invoicepicker.Parameters.AddWithValue("@INVOICE_NO", columnValue)
                Dim reader As OleDbDataReader = invoicepicker.ExecuteReader
                If reader.HasRows = True Then
                    While reader.Read
                        Dim path2 As String
                        path2 = reader.GetString(0)
                        Dim filePath As String = ".\" & path2
                        Dim fileUri As New Uri(filePath, UriKind.RelativeOrAbsolute)
                        FILE_PATH = filePath
                        If Not fileUri.IsAbsoluteUri Then
                            fileUri = New Uri(System.IO.Path.Combine(Application.StartupPath, filePath))
                        End If
                        PDF_VIEWER.Source = fileUri
                    End While
                End If
                'Catch ex As Exception
                ' MessageBox.Show(ex.Message)
                'End Try
            End If
        End If
    End Sub
    Private Sub YEAR_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs)

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
    End Sub
    Private Sub PRINT_BTN_Click(sender As Object, e As EventArgs) Handles PRINT_BTN.Click
        'JavaScript to print the PDF
        PDF_VIEWER.ExecuteScriptAsync("window.print();")
    End Sub
    Private Sub SEARCH_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_BTN.Click
        Flag = False
        CUST_DATA_GRID.DataSource = Nothing
        Dim connection As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        connection.Open()
        Dim year2 As Integer = CInt(YEAR_COMBOBOX.SelectedItem)
        Dim monthName As String = MONTH_COMBOBOX.SelectedItem
        Dim monthNumber As Integer = DateTime.ParseExact(monthName, "MMMM", CultureInfo.InvariantCulture).Month
        Dim daysInMonth As Integer = DateTime.DaysInMonth(year2, monthNumber)
        Dim startDate As New DateTime(year2, monthNumber, 1)
        Dim endDate As New DateTime(year2, monthNumber, daysInMonth)

        Dim transfetcher As New OleDbCommand("SELECT INVOICE_NO AS [INVOICE NO],PAYMENT_DATE AS [DATE],REFERANCE_NO AS [REFERANCE NO],MODE FROM INVOICE_DETAILS WHERE CRF=@CRF AND SERVICE=@SERVICE AND PAYMENT_DATE BETWEEN @DATE1 AND @DATE2", connection)
        transfetcher.Parameters.AddWithValue("@CRF", cust_crf)
        transfetcher.Parameters.AddWithValue("@SERVICE", "BROADBAND")
        transfetcher.Parameters.AddWithValue("@DATE1", startDate)
        transfetcher.Parameters.AddWithValue("@DATE2", endDate)
        Dim adapter As New OleDbDataAdapter(transfetcher)
        Dim table As New Data.DataTable()
        adapter.Fill(table)
        connection.Close()
        CUST_DATA_GRID.DataSource = table
        CUST_DATA_GRID.Columns(0).Width = 150
        CUST_DATA_GRID.Columns(1).Width = 150
        CUST_DATA_GRID.Columns(2).Width = 150
        CUST_DATA_GRID.Columns(3).Width = 150
        flag = True
        If CUST_DATA_GRID.Rows.Count > 0 Then
            CUST_DATA_GRID.Rows(0).Selected = True
            CUST_DATA_GRID.Sort(CUST_DATA_GRID.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        End If
    End Sub

    Private Sub MONTH_COMBOBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MONTH_COMBOBOX.SelectedIndexChanged
        PDF_VIEWER.Source = New Uri("file:///" & FileNotFound)
    End Sub
    Private Sub DOWNLOAD_BTN_Click_1(sender As Object, e As EventArgs)
        Dim fileName As String = FILE_PATH
        SaveFileDialog1.FileName = fileName
        SaveFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                File.Copy(FILE_PATH, SaveFileDialog1.FileName, True)
                MessageBox.Show("File Downloaded Successfully.", "Download Complete")
            Catch ex As Exception
                ErrorAlert.Play()
                LogError("Error Downloading File: " & ex.Message)
                MessageBox.Show("Error Downloading File.", "Download Error")
            End Try
        End If
    End Sub
End Class