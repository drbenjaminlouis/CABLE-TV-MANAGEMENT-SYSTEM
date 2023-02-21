Public Class Admin_Dashboard
    Public flag As Integer
    'Declaration for ChildForm'
    Private CurrentChildFrom As Form

    'For Exit Control'
    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Dim result = MessageBox1.Show("", "Are you sure you want to quit?")
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    'For Logout Button'
    Private Sub Guna2GradientButton12_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton12.Click
        Dim result = MessageBox1.Show("", "Are You Sure You Want To Logout?")
        If result = DialogResult.Yes Then
            Me.Hide()
            Admin_Login.Show()
        End If
    End Sub
    Private Sub Admin_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        App_Name.Text = app_name_text
        OpenChildForm(New Admin_Dashboard_Panel)
        Payment_Sync.Payment_Sync()
    End Sub

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


    Private Sub Guna2GradientButton7_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton7.Click
        OpenChildForm(New add_customer)
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        OpenChildForm(New Admin_Dashboard_Panel)
    End Sub

    Private Sub Guna2GradientButton13_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton13.Click
        OpenChildForm(New Change_Password)
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        OpenChildForm(New Add_Employee)
    End Sub
    Private Sub Guna2GradientButton5_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton5.Click
        OpenChildForm(New CUSTOMER_DETAILS_NEW)
    End Sub

    Private Sub Guna2GradientButton11_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton11.Click
        OpenChildForm(New Edit_Customer)
    End Sub

    Private Sub Guna2GradientButton6_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton6.Click

        OpenChildForm(New Remove_Customer)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Guna2GradientButton15_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton15.Click
        OpenChildForm(New Collect_Payment_Admin)
    End Sub

    Private Sub Guna2GradientButton4_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton4.Click
        OpenChildForm(New Edit_Employee)
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        OpenChildForm(New Remove_Employee)
    End Sub

    Private Sub Guna2GradientButton9_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton9.Click
        OpenChildForm(New Payment_Details)
    End Sub

    Private Sub Guna2GradientButton8_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton8.Click
        OpenChildForm(New REMINDER)
    End Sub
End Class