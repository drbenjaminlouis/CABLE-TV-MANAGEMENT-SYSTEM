Imports System.Threading

Public Class CUSTOMER_DASHBOARD
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf CUSTOMER_DASHBOARD_LOAD
        ' Add any initialization after the InitializeComponent() call.
    End Sub
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
    Private Sub CUSTOMER_DASHBOARD_LOAD(sender As Object, e As EventArgs)
        OpenChildForm(New CUSTOMER_DASHBOARD_PANEL)
        AddHandler LOGOUT_BTN.Click, AddressOf LOGOUT_BTN_Click
        AddHandler CHANGE_PASS_BTN.Click, AddressOf CHANGE_PASS_BTN_Click
        AddHandler DASHBOARD_BTN.Click, AddressOf DASHBOARD_BTN_Click
        AddHandler COLLECT_PAYMENT_CUST_BTN.Click, AddressOf COLLECT_PAYMENT_CUST_BTN_Click
        AddHandler PAYMENT_DETAILS_BTN.Click, AddressOf PAYMENT_DETAILS_BTN_Click
        AddHandler CUST_DETAILS_BTN.Click, AddressOf CUST_DETAILS_BTN_Click
        AddHandler CUST_EDIT_BTN.Click, AddressOf CUST_EDIT_BTN_Click
        AddHandler TV_CONNECTION_REPORT_BTN.Click, AddressOf TV_CONNECTION_REPORT_BTN_Click
        AddHandler BROADBAND_CONNECTION_REPORT_BTN.Click, AddressOf BROADBAND_CONNECTION_REPORT_BTN_Click
        AddHandler CLOSE_CONTROL.Click, AddressOf CLOSE_CONTROL_Click
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

    Private Sub CUST_DETAILS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Customer_Details)
    End Sub
    Private Sub CUST_EDIT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Edit_Customer)
    End Sub
    Private Sub PAYMENT_DETAILS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Payment_Details)
    End Sub
    Private Sub TV_CONNECTION_REPORT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New TV_CONNECTION_INVOICE_READER)
    End Sub

    Private Sub BROADBAND_CONNECTION_REPORT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New BROADBAND_INVOICE_READER)
    End Sub

    Private Sub CLOSE_CONTROL_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox1.Show("Are You Sure You Want To Quit?", "ALERT")
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class