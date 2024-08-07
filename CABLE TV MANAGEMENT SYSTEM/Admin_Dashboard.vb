﻿Public Class Admin_Dashboard
    'Initialization Of Form
    Public Sub New()
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf Admin_Dashboard_Load
    End Sub
    Public flag As Integer
    'Declaration for ChildForm'
    Private CurrentChildFrom As Form

    'Method For Inter-Changing Panels'
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
    Private Sub DASHBOARD_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Admin_Dashboard_Panel)
    End Sub
    Private Sub CHANGE_PASS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Change_Password)
    End Sub
    Public Sub COLLECT_PAYMENT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Collect_Payment_Admin)
    End Sub
    Private Sub ADD_CUST_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New add_customer)
    End Sub
    Private Sub REMOVE_CUST_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Remove_Customer)
    End Sub
    Private Sub CUST_DETAILS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New CUSTOMER_DETAILS_NEW)
    End Sub
    Private Sub CUST_EDIT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Edit_Customer)
    End Sub
    Private Sub TV_CONNECTION_REPORT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New TV_CONNECTION_REPORT)
    End Sub
    Private Sub REMINDER_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New REMINDER)
    End Sub
    Private Sub PAYMENT_DETAILS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New Payment_Details)
    End Sub
    Private Sub BROADBAND_CONNECTION_REPORT_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New BROADBAND_CONNECTION_REPORT)
    End Sub
    Private Sub CLOSE_CONTROL_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox1.Show("Are You Sure You Want To Quit?", "ALERT")
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
    Private Sub LOGOUT_BTN_Click(sender As Object, e As EventArgs)
        Dim result = MessageBox1.Show("Are You Sure You Want To Logout?", "ALERT")
        If result = DialogResult.Yes Then
            Dim Admin_log As New Admin_Login
            Admin_Login.Show()
            Me.Hide()
        End If
    End Sub
    Private Sub COMPLAINTS_BTN_Click(sender As Object, e As EventArgs)
        OpenChildForm(New COMPLAINT_VIEW)
        NOTIFICATION_ICON.Visible = False
    End Sub
    Private Sub Admin_Dashboard_Load(sender As Object, e As EventArgs)
        OpenChildForm(New Admin_Dashboard_Panel)
        App_Name.Text = Config.app_name_text
        AddHandler DASHBOARD_BTN.Click, AddressOf DASHBOARD_BTN_Click
        AddHandler CHANGE_PASS_BTN.Click, AddressOf CHANGE_PASS_BTN_Click
        AddHandler COLLECT_PAYMENT_BTN.Click, AddressOf COLLECT_PAYMENT_BTN_Click
        AddHandler ADD_CUST_BTN.Click, AddressOf ADD_CUST_BTN_Click
        AddHandler REMOVE_CUST_BTN.Click, AddressOf REMOVE_CUST_BTN_Click
        AddHandler CUST_EDIT_BTN.Click, AddressOf CUST_EDIT_BTN_Click
        AddHandler BROADBAND_CONNECTION_REPORT_BTN.Click, AddressOf BROADBAND_CONNECTION_REPORT_BTN_Click
        AddHandler TV_CONNECTION_REPORT_BTN.Click, AddressOf TV_CONNECTION_REPORT_BTN_Click
        AddHandler CUST_DETAILS_BTN.Click, AddressOf CUST_DETAILS_BTN_Click
        AddHandler PAYMENT_DETAILS_BTN.Click, AddressOf PAYMENT_DETAILS_BTN_Click
        AddHandler REMINDER_BTN.Click, AddressOf REMINDER_BTN_Click
        AddHandler CLOSE_CONTROL.Click, AddressOf CLOSE_CONTROL_Click
        AddHandler LOGOUT_BTN.Click, AddressOf LOGOUT_BTN_Click
        AddHandler COMPLAINTS_BTN.Click, AddressOf COMPLAINTS_BTN_Click
    End Sub
End Class
