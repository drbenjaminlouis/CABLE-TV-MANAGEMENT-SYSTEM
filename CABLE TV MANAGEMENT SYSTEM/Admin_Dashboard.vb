Public Class Admin_Dashboard
    Public flag As Integer
    'Declaration for ChildForm'
    Private CurrentChildFrom As Form
    'For Inter-Changing Panels'
    Public Sub OpenChildForm(ChildForm As Form)
        Progress.Show()
        Progress.Start()
        If CurrentChildFrom IsNot Nothing Then
            CurrentChildFrom.Close()

        End If
        CurrentChildFrom = ChildForm
        ChildForm.TopLevel = False
        ChildForm.FormBorderStyle = FormBorderStyle.None
        ChildForm.Dock = DockStyle.None
        PanelDesktop.Controls.Add(ChildForm)
        PanelDesktop.Tag = ChildForm
        ChildForm.BringToFront()
        ChildForm.Show()
        Progress.Stop()
        Progress.Hide()
    End Sub
    Private Sub DASHBOARD_BTN_Click_1(sender As Object, e As EventArgs) Handles DASHBOARD_BTN.Click
        OpenChildForm(New Admin_Dashboard_Panel)
    End Sub

    Private Sub CHANGE_PASS_BTN_Click_1(sender As Object, e As EventArgs) Handles CHANGE_PASS_BTN.Click
        OpenChildForm(New Change_Password)
    End Sub

    Private Sub COLLECT_PAYMENT_BTN_Click_1(sender As Object, e As EventArgs) Handles COLLECT_PAYMENT_BTN.Click
        OpenChildForm(New Collect_Payment_Admin)
    End Sub

    Private Sub ADD_CUST_BTN_Click_1(sender As Object, e As EventArgs) Handles ADD_CUST_BTN.Click
        OpenChildForm(New add_customer)
    End Sub

    Private Sub REMOVE_CUST_BTN_Click_1(sender As Object, e As EventArgs) Handles REMOVE_CUST_BTN.Click
        OpenChildForm(New Remove_Customer)
    End Sub

    Private Sub CUST_DETAILS_BTN_Click_1(sender As Object, e As EventArgs) Handles CUST_DETAILS_BTN.Click
        OpenChildForm(New Customer_Details)
    End Sub

    Private Sub CUST_EDIT_BTN_Click_1(sender As Object, e As EventArgs) Handles CUST_EDIT_BTN.Click
        OpenChildForm(New Edit_Customer)
    End Sub

    Private Sub TV_CONNECTION_REPORT_BTN_Click_1(sender As Object, e As EventArgs) Handles TV_CONNECTION_REPORT_BTN.Click
        OpenChildForm(New TV_CONNECTION_REPORT)
    End Sub

    Private Sub REMINDER_BTN_Click_1(sender As Object, e As EventArgs) Handles REMINDER_BTN.Click
        OpenChildForm(New REMINDER)
    End Sub
    Private Sub PAYMENT_DETAILS_BTN_Click_1(sender As Object, e As EventArgs) Handles PAYMENT_DETAILS_BTN.Click
        OpenChildForm(New Payment_Details)
    End Sub
    Private Sub BROADBAND_CONNECTION_REPORT_BTN_Click(sender As Object, e As EventArgs) Handles BROADBAND_CONNECTION_REPORT_BTN.Click
    End Sub
    Private Sub Admin_Dashboard_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        App_Name.Text = app_name_text
        OpenChildForm(New Admin_Dashboard_Panel)
        Payment_Sync.Payment_Sync()
    End Sub
    Private Sub CLOSE_CONTROL_Click_1(sender As Object, e As EventArgs) Handles CLOSE_CONTROL.Click
        Dim result = MessageBox1.Show("", "Are you sure you want to quit?")
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
