Public Class Remove_Employee
    Private Sub REMOVEBTN_Click(sender As Object, e As EventArgs) Handles REMOVEBTN.Click

    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If CUST_CRF_TEXTBOX.Text = "1234" Then
            REMOVEBTN.Enabled = True
            EDITID_BTN.Enabled = True
        Else
            REMOVEBTN.Enabled = False
            EDITID_BTN.Enabled = False
        End If
    End Sub
End Class