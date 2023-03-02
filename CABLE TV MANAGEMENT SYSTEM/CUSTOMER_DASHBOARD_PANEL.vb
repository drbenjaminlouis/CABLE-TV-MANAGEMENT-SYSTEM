Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel

Public Class CUSTOMER_DASHBOARD_PANEL
    Dim crfno As Integer
    Dim tv_plan As String
    Dim tv_expiry As String
    Dim tv_status As String
    Dim tv_pending As Integer
    Dim broadband_plan As String
    Dim broadband_expiry_date As String
    Dim broadband_status_ As String
    Dim broadband_pending As Integer
    Private Sub CUSTOMER_DASHBOARD_PANEL_LOAD(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim user_name = Module1.UserName
        tv_plan = ""
        tv_expiry = ""
        tv_status = ""
        tv_pending = 0
        broadband_plan = ""
        broadband_expiry_date = ""
        broadband_status_ = ""
        broadband_pending = 0
        If Not UserName = "" Then
            Dim connection As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            Try
                connection.Open()
                Dim checker As New OleDbCommand("SELECT CRF FROM CUSTOMER_LOGIN_DETAILS WHERE CUST_USERNAME=@USERNAME", connection)
                checker.Parameters.AddWithValue("@USERNAME", user_name)
                Dim reader As OleDbDataReader = checker.ExecuteReader
                If reader.HasRows = True Then
                    While reader.Read
                        crfno = reader.GetInt32(0)
                    End While
                End If
                reader.Close()
                If Not crfno = Nothing Then
                    Dim checker2 As New OleDbCommand("SELECT CUST_TV_CONNECTION,CUST_TV_PLAN,EXPIRY_DATE,TV_CONNECTION_STATUS FROM TV_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                    checker2.Parameters.AddWithValue("@CRF", crfno)
                    Dim reader2 As OleDbDataReader = checker2.ExecuteReader
                    If reader2.HasRows = True Then
                        While reader2.Read
                            If reader2.GetString(0) = "YES" Then '
                                tv_plan = reader2.GetString(1)
                                tv_expiry = reader2.GetDateTime(2)
                                tv_status = reader2.GetString(3)
                                Dim query As String = "SELECT IIF([january]='Not Paid',1,0) AS january, " &
                                               "IIF([february]='Not Paid',1,0) AS february, " &
                                               "IIF([march]='Not Paid',1,0) AS march, " &
                                               "IIF([april]='Not Paid',1,0) AS april, " &
                                               "IIF([may]='Not Paid',1,0) AS may, " &
                                               "IIF([june]='Not Paid',1,0) AS june, " &
                                               "IIF([july]='Not Paid',1,0) AS july, " &
                                               "IIF([august]='Not Paid',1,0) AS august, " &
                                               "IIF([september]='Not Paid',1,0) AS september, " &
                                               "IIF([october]='Not Paid',1,0) AS october, " &
                                                "IIF([november]='Not Paid',1,0) AS november, " &
                                                "IIF([december]='Not Paid',1,0) AS december " &
                                                "FROM TV_PAYMENT_DETAILS " &
                                                "WHERE CRF=@CRF AND PAYMENT_YEAR=@YEAR"

                                Using command As New OleDbCommand(query, connection)
                                    command.Parameters.AddWithValue("@CRF", crfno)
                                    command.Parameters.AddWithValue("@YEAR", Date.Now.Year)
                                    Dim reader3 As OleDbDataReader = command.ExecuteReader()
                                    Dim pendingPayments As Integer = 0

                                    If reader3.HasRows = True Then
                                        ' Read the first row
                                        reader3.Read()

                                        ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid
                                        If reader3("january") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("february") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("march") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("april") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("may") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("june") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("july") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("august") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("september") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("october") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("november") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader3("december") = 1 Then
                                            pendingPayments += 250
                                        End If
                                    End If
                                    tv_pending = pendingPayments
                                    reader3.Close()
                                End Using
                            End If
                        End While
                        reader2.Close()
                    Else
                        tv_plan = "NILL"
                        tv_expiry = "NILL"
                        tv_status = "NILL"
                        tv_pending = 0
                    End If

                    Dim checker3 As New OleDbCommand("SELECT BROADBAND_CONNECTION,CURRENT_PLAN,EXPIRY_DATE,STATUS FROM BROADBAND_CONNECTION_DETAILS WHERE CRF=@CRF", connection)
                    checker3.Parameters.AddWithValue("@CRF", crfno)
                    Dim reader4 As OleDbDataReader = checker3.ExecuteReader
                    If reader4.HasRows = True Then
                        While reader4.Read
                            If reader4.GetString(0) = "YES" Then '
                                broadband_plan = reader4.GetString(1)
                                broadband_expiry_date = reader4.GetDateTime(2)
                                broadband_status_ = reader4.GetString(3)
                                Dim query2 As String = "SELECT IIF([january]='Not Paid',1,0) AS january, " &
                                                   "IIF([february]='Not Paid',1,0) AS february, " &
                                                   "IIF([march]='Not Paid',1,0) AS march, " &
                                                   "IIF([april]='Not Paid',1,0) AS april, " &
                                                   "IIF([may]='Not Paid',1,0) AS may, " &
                                                   "IIF([june]='Not Paid',1,0) AS june, " &
                                                   "IIF([july]='Not Paid',1,0) AS july, " &
                                                   "IIF([august]='Not Paid',1,0) AS august, " &
                                                   "IIF([september]='Not Paid',1,0) AS september, " &
                                                   "IIF([october]='Not Paid',1,0) AS october, " &
                                                    "IIF([november]='Not Paid',1,0) AS november, " &
                                                    "IIF([december]='Not Paid',1,0) AS december " &
                                                    "FROM BROADBAND_PAYMENT_DETAILS " &
                                                    "WHERE CRF=@CRF AND PAYMENT_YEAR=@YEAR"

                                Using command As New OleDbCommand(query2, connection)
                                    command.Parameters.AddWithValue("@CRF", crfno)
                                    command.Parameters.AddWithValue("@YEAR", Date.Now.Year)
                                    Dim reader5 As OleDbDataReader = command.ExecuteReader()
                                    Dim pendingPayments As Integer = 0

                                    If reader5.HasRows = True Then
                                        ' Read the first row
                                        reader5.Read()

                                        ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid
                                        If reader5("january") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("february") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("march") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("april") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("may") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("june") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("july") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("august") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("september") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("october") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("november") = 1 Then
                                            pendingPayments += 250
                                        End If
                                        If reader5("december") = 1 Then
                                            pendingPayments += 250
                                        End If
                                    End If
                                    broadband_pending = pendingPayments
                                End Using
                            End If
                        End While
                        reader4.Close()
                    Else
                        broadband_plan = "NILL"
                        broadband_expiry_date = "NILL"
                        broadband_status_ = "NILL"
                        broadband_pending = 0
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                connection.Close()

            End Try
        End If
    End Sub
    Private Sub TV_CONNECTION_STATUS_MOUSEHOVER(sender As Object, e As EventArgs) Handles TV_CONNECTION_STATUS.MouseHover
        TV_CONNECTION_STATUS.Image = Nothing
        TV_CONNECTION_STATUS.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        TV_CONNECTION_STATUS.Font = myFont
        TV_CONNECTION_STATUS.Text = tv_status
    End Sub
    Private Sub TV_CONNECTION_STATUS_MOUSE_LEAVE(sender As Object, e As EventArgs) Handles TV_CONNECTION_STATUS.MouseLeave
        TV_CONNECTION_STATUS.Image = My.Resources.icons8_checkmark_50
        TV_CONNECTION_STATUS.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        TV_CONNECTION_STATUS.Font = myFont2
        TV_CONNECTION_STATUS.Text = "TV CONNECTION STATUS"
    End Sub
    Private Sub TV_EXPIRY_DATE_MOUSEHOVER(sender As Object, e As EventArgs) Handles TV_EXPIRY_DATE.MouseHover
        TV_EXPIRY_DATE.Image = Nothing
        TV_EXPIRY_DATE.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        TV_EXPIRY_DATE.Font = myFont
        TV_EXPIRY_DATE.Text = ""
        TV_EXPIRY_DATE.Text = tv_expiry
    End Sub
    Private Sub TV_EXPIRY_DATE_MOUSE_LEAVE(sender As Object, e As EventArgs) Handles TV_EXPIRY_DATE.MouseLeave
        TV_EXPIRY_DATE.Image = My.Resources.EXPIRY_DATE_ICON
        TV_EXPIRY_DATE.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        TV_EXPIRY_DATE.Font = myFont2
        TV_EXPIRY_DATE.Text = "TV EXPIRY DATE"
    End Sub
    Private Sub TV_PENDING_AMOUNT_MOUSEHOVER(sender As Object, e As EventArgs) Handles TV_PENDING_AMOUNT.MouseHover
        TV_PENDING_AMOUNT.Image = Nothing
        TV_PENDING_AMOUNT.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        TV_PENDING_AMOUNT.Font = myFont
        TV_PENDING_AMOUNT.Text = tv_pending
    End Sub
    Private Sub TV_PENDING_AMOUNT_LEAVE(sender As Object, e As EventArgs) Handles TV_PENDING_AMOUNT.MouseLeave
        TV_PENDING_AMOUNT.Image = My.Resources.DUE_AMOUNT_ICON2
        TV_PENDING_AMOUNT.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        TV_PENDING_AMOUNT.Font = myFont2
        TV_PENDING_AMOUNT.Text = "TV DUE AMOUNT"
    End Sub
    Private Sub TV_CURRENT_PLAN_MOUSEHOVER(sender As Object, e As EventArgs) Handles TV_CURRENT_PLAN.MouseHover
        TV_CURRENT_PLAN.Image = Nothing
        TV_CURRENT_PLAN.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        TV_CURRENT_PLAN.Font = myFont
        TV_CURRENT_PLAN.Text = tv_plan
    End Sub
    Private Sub TV_CURRENT_PLAN_LEAVE(sender As Object, e As EventArgs) Handles TV_CURRENT_PLAN.MouseLeave
        TV_CURRENT_PLAN.Image = My.Resources.CURRENT_PLAN_ICON2
        TV_CURRENT_PLAN.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        TV_CURRENT_PLAN.Font = myFont2
        TV_CURRENT_PLAN.Text = "TV CONNECTION PLAN"
    End Sub
    Private Sub BROADBAND_STATUS_MOUSEHOVER(sender As Object, e As EventArgs) Handles BROADBAND_STATUS.MouseHover
        BROADBAND_STATUS.Image = Nothing
        BROADBAND_STATUS.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_STATUS.Font = myFont
        BROADBAND_STATUS.Text = broadband_status_
    End Sub
    Private Sub BROADBAND_STATUS_LEAVE(sender As Object, e As EventArgs) Handles BROADBAND_STATUS.MouseLeave
        BROADBAND_STATUS.Image = My.Resources.icons8_wi_fi_connected
        BROADBAND_STATUS.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_STATUS.Font = myFont2
        BROADBAND_STATUS.Text = "BROADBAND CONNECTION STATUS"
    End Sub
    Private Sub BROADBAND_EXPIRY_MOUSEHOVER(sender As Object, e As EventArgs) Handles BROADBAND_EXPIRY.MouseHover
        BROADBAND_EXPIRY.Image = Nothing
        BROADBAND_EXPIRY.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_EXPIRY.Font = myFont
        BROADBAND_EXPIRY.Text = broadband_expiry_date
    End Sub
    Private Sub BROADBAND_EXPIRY_LEAVE(sender As Object, e As EventArgs) Handles BROADBAND_EXPIRY.MouseLeave
        BROADBAND_EXPIRY.Image = My.Resources.EXPIRY_DATE_ICON2
        BROADBAND_EXPIRY.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_EXPIRY.Font = myFont2
        BROADBAND_EXPIRY.Text = "BROADBAND_EXPIRY DATE"
    End Sub
    Private Sub BROADBAND_DUE_AMOUNT_MOUSEHOVER(sender As Object, e As EventArgs) Handles BROADBAND_DUE_AMOUNT.MouseHover
        BROADBAND_DUE_AMOUNT.Image = Nothing
        BROADBAND_DUE_AMOUNT.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_DUE_AMOUNT.Font = myFont
        BROADBAND_DUE_AMOUNT.Text = broadband_pending
    End Sub
    Private Sub BROADBAND_DUE_AMOUNT_LEAVE(sender As Object, e As EventArgs) Handles BROADBAND_DUE_AMOUNT.MouseLeave
        BROADBAND_DUE_AMOUNT.Image = My.Resources.DUE_AMOUNT_ICON
        BROADBAND_DUE_AMOUNT.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_DUE_AMOUNT.Font = myFont2
        BROADBAND_DUE_AMOUNT.Text = "BROADBAND DUE AMOUNT"
    End Sub
    Private Sub BROADBAND_CURRENT_PLAN_MOUSEHOVER(sender As Object, e As EventArgs) Handles BROADBAND_CURRENT_PLAN.MouseHover
        BROADBAND_CURRENT_PLAN.Image = Nothing
        BROADBAND_CURRENT_PLAN.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 16, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_CURRENT_PLAN.Font = myFont
        BROADBAND_CURRENT_PLAN.Text = broadband_plan
    End Sub
    Private Sub BROADBAND_CURRENT_PLAN_LEAVE(sender As Object, e As EventArgs) Handles BROADBAND_CURRENT_PLAN.MouseLeave
        BROADBAND_CURRENT_PLAN.Image = My.Resources.CURRENT_PLAN_ICON
        BROADBAND_CURRENT_PLAN.ImageAlign = HorizontalAlignment.Center
        Dim myFont2 As System.Drawing.Font
        myFont2 = New System.Drawing.Font("Arial", 12, FontStyle.Bold Or FontStyle.Bold)
        BROADBAND_CURRENT_PLAN.Font = myFont2
        BROADBAND_CURRENT_PLAN.Text = "BROADBAND PLAN"
    End Sub
End Class