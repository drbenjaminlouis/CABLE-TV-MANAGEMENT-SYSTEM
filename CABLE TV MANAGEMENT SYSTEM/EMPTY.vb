Public Class EMPTY
    Private Sub EMPTY_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click

        If Guna2TextBox1.Text = 10 Then
            Me.Controls.Remove(Guna2GradientButton1)

        End If
    End Sub
End Class