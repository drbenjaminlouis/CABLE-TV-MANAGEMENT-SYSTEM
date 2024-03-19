Public Class Login_Selector

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Me.Hide()
        Dim admin_log As New Admin_Login
        admin_log.Show()
    End Sub

    Private Sub Guna2ControlBox2_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox2.Click
        Application.Exit()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub
End Class