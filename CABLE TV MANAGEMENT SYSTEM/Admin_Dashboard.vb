Imports System.Drawing.Imaging
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms.Design
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports TheArtOfDevHtmlRenderer.Adapters
Imports WinFormAnimation

Public Class Admin_Dashboard
    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Dim result = MessageBox1.Show("", "Are you sure you want to quit?")
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Guna2GradientButton12_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton12.Click
        Dim result = MessageBox1.Show("", "Are You Sure You Want To Logout?")
        If result = DialogResult.Yes Then
            Me.Hide()
            Admin_Login.Show()
        End If
    End Sub

    Private Sub Admin_Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ADD_CUSTOMER_PANEL.Hide()
    End Sub

    Private Sub Guna2GradientPanel3_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Guna2GradientPanel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Me.Guna2GroupBox3.Hide()

    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Me.ADD_CUSTOMER_PANEL.Hide()
        Me.Guna2GroupBox3.Show()
    End Sub

    Private Sub Guna2GroupBox2_Click(sender As Object, e As EventArgs) Handles Guna2GroupBox2.Click

    End Sub

    Private Sub Guna2GradientTileButton1_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton1.MouseLeave
        Guna2GradientTileButton1.Image = My.Resources.icons8_checkmark_50
        Guna2GradientTileButton1.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton1.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton1.Text = "ACTIVE CUSTOMERS"
    End Sub
    Private Sub Guna2GradientTileButton1_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton1.MouseHover
        Guna2GradientTileButton1.Image = Nothing
        Guna2GradientTileButton1.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton1.Font = myFont
        Guna2GradientTileButton1.Text = "656"
    End Sub
    Private Sub Guna2GradientTileButton2_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton2.MouseLeave
        Guna2GradientTileButton2.Image = My.Resources.icons8_multiply_50
        Guna2GradientTileButton2.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton2.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton2.Text = "INACTIVE CUSTOMERS"
    End Sub
    Private Sub Guna2GradientTileButton2_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton2.MouseHover
        Guna2GradientTileButton2.Image = Nothing
        Guna2GradientTileButton2.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton2.Font = myFont
        Guna2GradientTileButton2.Text = "98"
    End Sub
    Private Sub Guna2GradientTileButton3_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton3.MouseLeave
        Guna2GradientTileButton3.Image = My.Resources.icons8_high_importance_50
        Guna2GradientTileButton3.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton3.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton3.Text = "SUUSPENDED CUSTOMERS"
    End Sub
    Private Sub Guna2GradientTileButton3_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton3.MouseHover
        Guna2GradientTileButton3.Image = Nothing
        Guna2GradientTileButton3.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton3.Font = myFont
        Guna2GradientTileButton3.Text = "98"
    End Sub
    Private Sub Guna2GradientTileButton4_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton4.MouseHover
        Guna2GradientTileButton4.Image = Nothing
        Guna2GradientTileButton4.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton4.Font = myFont
        Guna2GradientTileButton4.Text = "567"
    End Sub
    Private Sub Guna2GradientTileButton4_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton4.MouseLeave
        Guna2GradientTileButton4.Image = My.Resources.icons8_broadband_50
        Guna2GradientTileButton4.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton4.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton4.Text = "BROADBAND CUSTOMERS"
    End Sub
    Private Sub Guna2GradientTileButton5_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton5.MouseHover
        Guna2GradientTileButton5.Image = Nothing
        Guna2GradientTileButton5.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton5.Font = myFont
        Guna2GradientTileButton5.Text = "344"
    End Sub
    Private Sub Guna2GradientTileButton5_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton5.MouseLeave
        Guna2GradientTileButton5.Image = My.Resources.icons8_wi_fi_disconnected_50
        Guna2GradientTileButton5.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton5.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton5.Text = "BROADBAND RENEWALS"
    End Sub
    Private Sub Guna2GradientTileButton6_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton6.MouseHover
        Guna2GradientTileButton6.Image = Nothing
        Guna2GradientTileButton6.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton6.Font = myFont
        Guna2GradientTileButton6.Text = "32"
    End Sub
    Private Sub Guna2GradientTileButton6_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton6.MouseLeave
        Guna2GradientTileButton6.Image = My.Resources.icons8_renew_50
        Guna2GradientTileButton6.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton6.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton6.Text = "UPCOMING RENEWALS"
    End Sub
    Private Sub Guna2GradientTileButton7_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton7.MouseHover
        Guna2GradientTileButton7.Image = Nothing
        Guna2GradientTileButton7.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton7.Font = myFont
        Guna2GradientTileButton7.Text = "₹98000"
    End Sub
    Private Sub Guna2GradientTileButton7_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton7.MouseLeave
        Guna2GradientTileButton7.Image = My.Resources.icons8_hourglass_50
        Guna2GradientTileButton7.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton7.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton7.Text = "PENDING PAYMENTS"
    End Sub
    Private Sub Guna2GradientTileButton8_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton8.MouseHover
        Guna2GradientTileButton8.Image = Nothing
        Guna2GradientTileButton8.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton8.Font = myFont
        Guna2GradientTileButton8.Text = "₹250000"
    End Sub
    Private Sub Guna2GradientTileButton8_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton8.MouseLeave

        Guna2GradientTileButton8.Image = My.Resources.icons8_get_cash_50
        Guna2GradientTileButton8.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton8.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton8.Text = "RECEIVED PAYMENTS"
    End Sub
    Private Sub Guna2GradientTileButton9_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton9.MouseHover
        Guna2GradientTileButton9.Image = Nothing
        Guna2GradientTileButton9.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton9.Font = myFont
        Guna2GradientTileButton9.Text = "121"
    End Sub
    Private Sub Guna2GradientTileButton9_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton9.MouseLeave
        Guna2GradientTileButton9.Image = My.Resources.complaintsicon
        Guna2GradientTileButton9.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton9.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton9.Text = "COMPLAINTS"
    End Sub
    Private Sub Guna2GradientTileButton10_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton10.MouseHover
        Guna2GradientTileButton10.Image = Nothing
        Guna2GradientTileButton10.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton10.Font = myFont
        Guna2GradientTileButton10.Text = "10"
    End Sub
    Private Sub Guna2GradientTileButton10_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton10.MouseLeave
        Guna2GradientTileButton10.Image = My.Resources.icons8_feedback_50
        Guna2GradientTileButton10.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton10.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton10.Text = "FEEDBACKS"
    End Sub
    Private Sub Guna2GradientTileButton11_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton11.MouseHover
        Guna2GradientTileButton11.Image = Nothing
        Guna2GradientTileButton11.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton11.Font = myFont
        Guna2GradientTileButton11.Text = "34"
    End Sub
    Private Sub Guna2GradientTileButton11_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton11.MouseLeave
        Guna2GradientTileButton11.Image = My.Resources.icons8_add_50
        Guna2GradientTileButton11.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton11.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton11.Text = "CONNECTION REQUESTS"
    End Sub
    Private Sub Guna2GradientTileButton12_MouseHover(sender As Object, e As EventArgs) Handles Guna2GradientTileButton12.MouseHover
        Guna2GradientTileButton12.Image = Nothing
        Guna2GradientTileButton12.ImageAlign = Left
        Dim myFont As System.Drawing.Font
        myFont = New System.Drawing.Font("Arial", 20, FontStyle.Bold Or FontStyle.Bold)
        Guna2GradientTileButton12.Font = myFont
        Guna2GradientTileButton12.Text = "23"
    End Sub
    Private Sub Guna2GradientTileButton12_MouseLeave(sender As Object, e As EventArgs) Handles Guna2GradientTileButton12.MouseLeave
        Guna2GradientTileButton12.Image = My.Resources.icons8_add_user_male_50
        Guna2GradientTileButton12.ImageAlign = HorizontalAlignment.Center
        Guna2GradientTileButton12.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Guna2GradientTileButton12.Text = "NEW CUSTOMERS"
    End Sub

    Private Sub Guna2GradientButton7_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton7.Click
        Me.Guna2GroupBox3.Hide()
        Me.ADD_CUSTOMER_PANEL.Show()
    End Sub

    Private Sub Guna2GradientTileButton14_Click(sender As Object, e As EventArgs) Handles ADD_CUST_RESETBTN.Click


    End Sub
End Class