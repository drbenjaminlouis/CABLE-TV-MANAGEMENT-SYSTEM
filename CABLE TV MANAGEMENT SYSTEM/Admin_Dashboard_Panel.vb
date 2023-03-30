Imports System.Data.OleDb
Public Class Admin_Dashboard_Panel
    'Variables For Storing Counts.
    Dim active_tv_count As Integer
    Dim inactive_tv_count As Integer
    Dim suspended_tv_count As Integer
    Dim count As Integer
    Dim inactive_broadband As Integer
    Dim suspended_broadband As Integer
    Dim tv_renewal_count As Integer
    Dim active_broadband_count As Integer
    Dim inactive_broadband_count As Integer
    Dim suspended_broadband_count As Integer
    Dim broadband_renewal_count As Integer
    Dim currentDate As Date = DateTime.Now.Date
    Private Sub TV_ACTIVE_MOUSELEAVE(sender As Object, e As EventArgs) Handles Active_Customers.MouseLeave
        Active_Customers.Image = My.Resources.icons8_checkmark_50
        Active_Customers.ImageAlign = HorizontalAlignment.Center
        Active_Customers.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Active_Customers.Text = "ACTIVE CUSTOMERS"
    End Sub
    Private Sub TV_ACTIVE_MOUSEHOVER(sender As Object, e As EventArgs) Handles Active_Customers.MouseHover
        Active_Customers.Image = Nothing
        Active_Customers.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Active_Customers.Font = myFont
        Active_Customers.Text = active_tv_count
    End Sub
    Private Sub TV_INACTIVE_MOUSELEAVE(sender As Object, e As EventArgs) Handles Inactive_Customers.MouseLeave
        Inactive_Customers.Image = My.Resources.icons8_multiply_50
        Inactive_Customers.ImageAlign = HorizontalAlignment.Center
        Inactive_Customers.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Inactive_Customers.Text = "INACTIVE CUSTOMERS"
    End Sub
    Private Sub TV_INACTIVE_MOUSEHOVER(sender As Object, e As EventArgs) Handles Inactive_Customers.MouseHover
        Inactive_Customers.Image = Nothing
        Inactive_Customers.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Inactive_Customers.Font = myFont
        Inactive_Customers.Text = inactive_tv_count
    End Sub
    Private Sub TV_SUSPENDED_MOUSELEAVE(sender As Object, e As EventArgs) Handles Suspended_Customers.MouseLeave
        Suspended_Customers.Image = My.Resources.icons8_high_importance_50
        Suspended_Customers.ImageAlign = HorizontalAlignment.Center
        Suspended_Customers.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Suspended_Customers.Text = "SUSPENDED CUSTOMERS"
    End Sub
    Private Sub TV_SUSPENDED_MOUSEHOVER(sender As Object, e As EventArgs) Handles Suspended_Customers.MouseHover
        Suspended_Customers.Image = Nothing
        Suspended_Customers.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Suspended_Customers.Font = myFont
        Suspended_Customers.Text = suspended_tv_count
    End Sub
    Private Sub BROADBAND_CUSTOMERS_MOUSEHOVER(sender As Object, e As EventArgs) Handles BroadBand_Customers.MouseHover
        BroadBand_Customers.Image = Nothing
        BroadBand_Customers.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        BroadBand_Customers.Font = myFont
        BroadBand_Customers.Text = active_broadband_count
    End Sub
    Private Sub BROADBAND_CUSTOMERS_MOUSELEAVE(sender As Object, e As EventArgs) Handles BroadBand_Customers.MouseLeave
        BroadBand_Customers.Image = My.Resources.icons8_broadband_50
        BroadBand_Customers.ImageAlign = HorizontalAlignment.Center
        BroadBand_Customers.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        BroadBand_Customers.Text = "BROADBAND CUSTOMERS"
    End Sub
    Private Sub BROADBAND_RENEWALS_MOUSEHOVER(sender As Object, e As EventArgs) Handles BroadBand_Renewals.MouseHover
        BroadBand_Renewals.Image = Nothing
        BroadBand_Renewals.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        BroadBand_Renewals.Font = myFont
        BroadBand_Renewals.Text = broadband_renewal_count
    End Sub
    Private Sub BROADBAND_RENEWALS_MOUSELEAVE(sender As Object, e As EventArgs) Handles BroadBand_Renewals.MouseLeave
        BroadBand_Renewals.Image = My.Resources.icons8_pay_date_50__1_
        BroadBand_Renewals.ImageAlign = HorizontalAlignment.Center
        BroadBand_Renewals.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        BroadBand_Renewals.Text = "BROADBAND RENEWALS"
    End Sub
    Private Sub CABLE_TV_RENEWALS_MOUSEHOVER(sender As Object, e As EventArgs) Handles Cable_TV_Renewals.MouseHover
        Cable_TV_Renewals.Image = Nothing
        Cable_TV_Renewals.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Cable_TV_Renewals.Font = myFont
        Cable_TV_Renewals.Text = tv_renewal_count
    End Sub
    Private Sub CABLE_TV_RENEWALS_MOUSELEAVE(sender As Object, e As EventArgs) Handles Cable_TV_Renewals.MouseLeave
        Cable_TV_Renewals.Image = My.Resources.icons8_renew_50
        Cable_TV_Renewals.ImageAlign = HorizontalAlignment.Center
        Cable_TV_Renewals.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Cable_TV_Renewals.Text = "CABLE TV RENEWALS"
    End Sub
    Private Sub TV_PENDING_PAYMENTS_MOUSEHOVER(sender As Object, e As EventArgs) Handles TV_Pending_Payments.MouseHover, TV_Pending_Payments.MouseHover
        TV_Pending_Payments.Image = Nothing
        TV_Pending_Payments.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        TV_Pending_Payments.Font = myFont
        TV_Pending_Payments.Text = "₹" & PENDING_VS_RECEIVED.TV_PENDING
    End Sub
    Private Sub TV_PENDING_PAYMENTS_MOUSELEAVE(sender As Object, e As EventArgs) Handles TV_Pending_Payments.MouseLeave
        TV_Pending_Payments.Image = My.Resources.icons8_hourglass_50
        TV_Pending_Payments.ImageAlign = HorizontalAlignment.Center
        TV_Pending_Payments.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        TV_Pending_Payments.Text = "PENDING PAYMENTS"
    End Sub
    Private Sub TV_RECEIVED_PAYMENT_MOUSEHOVER(sender As Object, e As EventArgs) Handles TV_Received_Payments.MouseHover
        TV_Received_Payments.Image = Nothing
        TV_Received_Payments.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        TV_Received_Payments.Font = myFont
        TV_Received_Payments.Text = "₹" & PENDING_VS_RECEIVED.TV_RECEIVED
    End Sub
    Private Sub TV_RECEIVED_PAYMENT_MOUSELEAVE(sender As Object, e As EventArgs) Handles TV_Received_Payments.MouseLeave
        TV_Received_Payments.Image = My.Resources.icons8_get_cash_50
        TV_Received_Payments.ImageAlign = HorizontalAlignment.Center
        TV_Received_Payments.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        TV_Received_Payments.Text = "RECEIVED PAYMENTS"
    End Sub
    Private Sub BROADBAND_RECEIVED_MOUSEHOVER(sender As Object, e As EventArgs) Handles BroadBand_Received.MouseHover
        BroadBand_Received.Image = Nothing
        BroadBand_Received.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        BroadBand_Received.Font = myFont
        BroadBand_Received.Text = "₹" & PENDING_VS_RECEIVED.BROADBAND_RECEIVED
    End Sub
    Private Sub BROADBAND_RECEIVED_MOUSELEAVE(sender As Object, e As EventArgs) Handles BroadBand_Received.MouseLeave
        BroadBand_Received.Image = My.Resources.CASH_ICON
        BroadBand_Received.ImageAlign = HorizontalAlignment.Center
        BroadBand_Received.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        BroadBand_Received.Text = "BROADBAND RECEIVED PAYMENTS"
    End Sub
    Private Sub BROADBAND_PENDING_MOUSEHOVER(sender As Object, e As EventArgs) Handles BroadBand_Pending.MouseHover
        BroadBand_Pending.Image = Nothing
        BroadBand_Pending.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        BroadBand_Pending.Font = myFont
        BroadBand_Pending.Text = "₹" & PENDING_VS_RECEIVED.BROADBAND_PENDING
    End Sub
    Private Sub BROADBAND_PENDING_MOUSELEAVE(sender As Object, e As EventArgs) Handles BroadBand_Pending.MouseLeave
        BroadBand_Pending.Image = My.Resources.PENDING_ICON
        BroadBand_Pending.ImageAlign = HorizontalAlignment.Center
        BroadBand_Pending.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        BroadBand_Pending.Text = "BROADBAND PENDING PAYMENTS"
    End Sub
    Private Sub BROADBAND_SUSPENDED_MOUSEHOVER(sender As Object, e As EventArgs) Handles BroadBand_Suspended.MouseHover
        BroadBand_Suspended.Image = Nothing
        BroadBand_Suspended.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        BroadBand_Suspended.Font = myFont
        BroadBand_Suspended.Text = suspended_broadband_count
    End Sub
    Private Sub BROADBAND_SUSPENDED_MOUSELEAVE(sender As Object, e As EventArgs) Handles BroadBand_Suspended.MouseLeave
        BroadBand_Suspended.Image = My.Resources.icons8_wi_fi_off_50
        BroadBand_Suspended.ImageAlign = HorizontalAlignment.Center
        BroadBand_Suspended.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        BroadBand_Suspended.Text = "BROADBAND SUSPENDED"
    End Sub
    Private Sub BROADBAND_INACTIVE_MOUSEHOVER(sender As Object, e As EventArgs) Handles BroadBand_Inactive.MouseHover
        BroadBand_Inactive.Image = Nothing
        BroadBand_Inactive.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        BroadBand_Inactive.Font = myFont
        BroadBand_Inactive.Text = inactive_broadband_count
    End Sub
    Private Sub BROADBAND_INACTIVE_MOUSELEAVE(sender As Object, e As EventArgs) Handles BroadBand_Inactive.MouseLeave
        BroadBand_Inactive.Image = My.Resources.icons8_wi_fi_disconnected_50
        BroadBand_Inactive.ImageAlign = HorizontalAlignment.Center
        BroadBand_Inactive.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        BroadBand_Inactive.Text = "BROADBAND INACTIVE"
    End Sub

    Private Sub Admin_Dashboard_Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        Try
            connection.Open()
            Dim sql_command1 = "SELECT COUNT(*) FROM TV_CONNECTION_DETAILS WHERE TV_CONNECTION_STATUS = 'ACTIVE'"
            Dim sql_command2 = "SELECT COUNT(*) FROM TV_CONNECTION_DETAILS WHERE TV_CONNECTION_STATUS = 'INACTIVE'"
            Dim sql_command3 = "SELECT COUNT(*) FROM TV_CONNECTION_DETAILS WHERE TV_CONNECTION_STATUS = 'SUSPENDED'"
            Dim sql_command4 = "SELECT COUNT(*) FROM TV_CONNECTION_DETAILS WHERE EXPIRY_DATE = @EXPIRY_DATE AND REGISTRATION_DATE < @REG_DATE"
            Dim sql_command5 = "SELECT COUNT(*) FROM BROADBAND_CONNECTION_DETAILS WHERE STATUS = 'ACTIVE'"
            Dim sql_command6 = "SELECT COUNT(*) FROM BROADBAND_CONNECTION_DETAILS WHERE STATUS = 'INACTIVE'"
            Dim sql_command7 As New OleDbCommand("SELECT COUNT(*) FROM BROADBAND_CONNECTION_DETAILS WHERE STATUS = @STATUS", connection)
            Dim sql_command8 = "SELECT COUNT(*) FROM BROADBAND_CONNECTION_DETAILS WHERE EXPIRY_DATE = @EXPIRY_DATE AND REGISTRATION_DATE < @REG_DATE"
            Dim command1 As New OleDbCommand(sql_command1, connection)
            Dim command2 As New OleDbCommand(sql_command2, connection)
            Dim command3 As New OleDbCommand(sql_command3, connection)
            Dim command4 As New OleDbCommand(sql_command4, connection)
            Dim command5 As New OleDbCommand(sql_command5, connection)
            Dim command6 As New OleDbCommand(sql_command6, connection)
            Dim command8 As New OleDbCommand(sql_command8, connection)
            active_tv_count = command1.ExecuteScalar()
            inactive_tv_count = command2.ExecuteScalar()
            suspended_tv_count = command3.ExecuteScalar()
            command4.Parameters.AddWithValue("@EXPIRY_DATE", currentDate)
            command4.Parameters.AddWithValue("@REG_DATE", Date.Today.ToString("dd-MM-yyyy"))
            tv_renewal_count = command4.ExecuteScalar()
            active_broadband_count = command5.ExecuteScalar()
            inactive_broadband_count = command6.ExecuteScalar()
            sql_command7.Parameters.AddWithValue("@STATUS", "SUSPENDED")
            suspended_broadband_count = sql_command7.ExecuteScalar()
            command8.Parameters.AddWithValue("@EXPIRY_DATE", currentDate)
            command8.Parameters.AddWithValue("@REG_DATE", Date.Today.ToString("dd-MM-yyyy"))
            broadband_renewal_count = command8.ExecuteScalar()
        Catch ex As Exception
            LogError("An Error Occured While Updating Counts: " & ex.Message)
            Dim MessageBox As New Guna.UI2.WinForms.Guna2MessageDialog
            MessageBox.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark
            MessageBox.Show("An Error Occured While Updating Counts. Check Log For More Details.", "ALERT")
        Finally
            connection.Close()
        End Try
    End Sub
End Class
