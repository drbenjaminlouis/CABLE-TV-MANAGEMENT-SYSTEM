﻿Imports System.Data.OleDb
Imports CABLE_TV_MANAGEMENT_SYSTEM.Payment_Sync
Public Class Loading
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MyProgress.Increment(1)
        If MyProgress.Value = 100 Then
            Guna2ProgressIndicator1.Start()
            Payment_Sync.Payment_Sync()
            Me.Hide()
            Dim log_selector As New Admin_Login
            log_selector.Show()
            Timer1.Enabled = False
            Guna2ProgressIndicator1.Stop()
        End If

    End Sub


End Class