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
    Private Sub DASHBOARD_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Admin_Dashboard_Panel)
    End Sub

    Private Sub CHANGE_PASS_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Change_Password)
    End Sub
<<<<<<< HEAD

    Private Sub COLLECT_PAYMENT_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Collect_Payment_Admin)
    End Sub

=======
    Private Sub COLLECT_PAYMENT_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Collect_Payment_Admin)
    End Sub
>>>>>>> 3a83c465034fc9331d67fabb34f7a0db1ec24a27
    Private Sub ADD_CUST_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New add_customer)
    End Sub

    Private Sub REMOVE_CUST_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Remove_Customer)
    End Sub

    Private Sub CUST_DETAILS_BTN_Click_1(sender As Object, e As EventArgs)
<<<<<<< HEAD
        OpenChildForm(New Customer_Details)
=======
        OpenChildForm(New CUSTOMER_DETAILS_NEW)
>>>>>>> 3a83c465034fc9331d67fabb34f7a0db1ec24a27
    End Sub

    Private Sub CUST_EDIT_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Edit_Customer)
    End Sub

    Private Sub TV_CONNECTION_REPORT_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New TV_CONNECTION_REPORT)
    End Sub

    Private Sub REMINDER_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New REMINDER)
    End Sub
    Private Sub PAYMENT_DETAILS_BTN_Click_1(sender As Object, e As EventArgs)
        OpenChildForm(New Payment_Details)
    End Sub
    Private Sub BROADBAND_CONNECTION_REPORT_BTN_Click(sender As Object, e As EventArgs)
<<<<<<< HEAD
=======
        OpenChildForm(New BROADBAND_CONNECTION_REPORT)
>>>>>>> 3a83c465034fc9331d67fabb34f7a0db1ec24a27
    End Sub
    Private Sub Admin_Dashboard_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        App_Name.Text = app_name_text
        OpenChildForm(New Admin_Dashboard_Panel)
        Payment_Sync.Payment_Sync()
        AddHandler DASHBOARD_BTN.Click, AddressOf DASHBOARD_BTN_Click_1
        AddHandler CHANGE_PASS_BTN.Click, AddressOf CHANGE_PASS_BTN_Click_1
        AddHandler COLLECT_PAYMENT_BTN.Click, AddressOf COLLECT_PAYMENT_BTN_Click_1
        AddHandler ADD_CUST_BTN.Click, AddressOf ADD_CUST_BTN_Click_1
        AddHandler REMOVE_CUST_BTN.Click, AddressOf REMOVE_CUST_BTN_Click_1
        AddHandler CUST_EDIT_BTN.Click, AddressOf CUST_EDIT_BTN_Click_1
        AddHandler BROADBAND_CONNECTION_REPORT_BTN.Click, AddressOf BROADBAND_CONNECTION_REPORT_BTN_Click
        AddHandler TV_CONNECTION_REPORT_BTN.Click, AddressOf TV_CONNECTION_REPORT_BTN_Click_1
        AddHandler CUST_DETAILS_BTN.Click, AddressOf CUST_DETAILS_BTN_Click_1
        AddHandler PAYMENT_DETAILS_BTN.Click, AddressOf PAYMENT_DETAILS_BTN_Click_1
        AddHandler REMINDER_BTN.Click, AddressOf REMINDER_BTN_Click_1
        AddHandler CLOSE_CONTROL.Click, AddressOf CLOSE_CONTROL_Click_1
        AddHandler LOGOUT_BTN.Click, AddressOf LOGOUT_BTN_Click
    End Sub
    Private Sub CLOSE_CONTROL_Click_1(sender As Object, e As EventArgs)
        Dim result = MessageBox1.Show("Are You Sure You Want To Quit?", "ALERT")
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
    Private Sub LOGOUT_BTN_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox1.Show("Are You Sure You Want To Logout?", "ALERT")
        If result = DialogResult.Yes Then
            Dim Admin_log As New Admin_Login
            USER_LOGIN.Show()
            Me.Hide()
        End If
    End Sub
End Class
