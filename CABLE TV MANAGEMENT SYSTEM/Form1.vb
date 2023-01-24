Public Class Form1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MyProgress.Increment(1)
        If MyProgress.Value = 100 Then
            Me.Hide()
            Dim log_selector As New Login_Selector
            log_selector.Show()
            Timer1.Enabled = False
        End If
    End Sub
End Class