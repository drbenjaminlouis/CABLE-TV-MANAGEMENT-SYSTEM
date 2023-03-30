Public Class AppLoading
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MyProgress.Increment(1)
        If MyProgress.Value = 100 Then
            Guna2ProgressIndicator1.Start()
            'Functions For Updating Connection Status.
            StatusSync.InactiveUpdater()
            StatusSync.SuspenderTV()
            StatusSync.ActivatorBroadband()
            StatusSync.SuspenderBroadband()
            StatusSync.ActivatorTV()
            Me.Hide()
            Dim ad_log As New Admin_Login
            ad_log.Show()
            Timer1.Enabled = False
            Guna2ProgressIndicator1.Stop()
        End If
    End Sub
End Class