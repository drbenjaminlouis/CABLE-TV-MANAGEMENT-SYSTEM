Public Class CUSTOMER_DASHBOARD
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
    Private Sub CUSTOMER_DASHBOARD_LOAD(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenChildForm(New CUSTOMER_DASHBOARD_PANEL)
        AddHandler LOGOUT_BTN.Click, AddressOf LOGOUT_BTN_Click
        AddHandler CHANGE_PASS_BTN.Click, AddressOf CHANGE_PASS_BTN_Click
        AddHandler DASHBOARD_BTN.Click, AddressOf DASHBOARD_BTN_Click
        AddHandler COLLECT_PAYMENT_CUST_BTN.Click, AddressOf COLLECT_PAYMENT_CUST_BTN_Click
    End Sub
    Private Sub DASHBOARD_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New CUSTOMER_DASHBOARD_PANEL)
    End Sub
    Private Sub CHANGE_PASS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Change_Password)
    End Sub
    Private Sub LOGOUT_BTN_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox1.Show("Are You Sure You Want To Logout?", "ALERT")
        If result = DialogResult.Yes Then
            Dim Admin_log As New Admin_Login
            USER_LOGIN.Show()
            Me.Hide()
        End If
    End Sub
    Private Sub COLLECT_PAYMENT_CUST_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Collect_Payment_Admin)
    End Sub
End Class