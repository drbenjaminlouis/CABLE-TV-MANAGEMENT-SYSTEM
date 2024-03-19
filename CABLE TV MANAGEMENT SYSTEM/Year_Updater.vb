Imports System.Data.OleDb
Imports Guna.UI2.WinForms
Module Year_Updater
    Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
    Public Function TV_Year_Updater()
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
        connection.Open()
        Dim transaction As OleDbTransaction = connection.BeginTransaction
        Try

            Dim cmd As New OleDbCommand
            Dim maxVal As Integer
            cmd.CommandText = "SELECT MAX(PAYMENT_YEAR) AS max_val FROM TV_PAYMENT_DETAILS"
            cmd.Connection = connection
            cmd.Transaction = transaction
            maxVal = CInt(cmd.ExecuteScalar())
            If maxVal < Date.Today.Year Then
                Dim crf_counter As New OleDbCommand("SELECT DISTINCT(CRF) FROM TV_CONNECTION_DETAILS WHERE CUST_TV_CONNECTION=@STATUS", connection)
                crf_counter.Parameters.AddWithValue("@STATUS", "YES")
                crf_counter.Transaction = transaction
                Dim crf_reader As OleDbDataReader = crf_counter.ExecuteReader
                If crf_reader.HasRows = True Then
                    While crf_reader.Read
                        Dim Year_Update_Command As New OleDbCommand("INSERT INTO TV_PAYMENT_DETAILS (CRF,PAYMENT_YEAR) VALUES (@CRF,@PAYMENT_YEAR)", connection)
                        Year_Update_Command.Parameters.AddWithValue("@CRF", crf_reader.GetInt32(0))
                        Year_Update_Command.Parameters.AddWithValue("@PAYMENT_YEAR", Date.Now.Year)
                        Year_Update_Command.Transaction = transaction
                        Year_Update_Command.ExecuteNonQuery()
                    End While
                End If
                crf_reader.Close()
            End If
            transaction.Commit()
        Catch ex As Exception
            transaction.Rollback()
            ErrorAlert.Play()
            LogError("An Error Occured During Updating Payment Year: " & ex.Message)
            If LoginType = "ADMIN" Then
                MessageBox.Show("An Error Occured While Updating TV Payment Year. Check Log For More Details.", "ALERT")
            End If
            If LoginType = "CUSTOMER" Then
                MessageBox.Show("An Error Occured. Contact Admin.", "ALERT")
                Application.Exit()
            End If
        Finally
            connection.Close()
        End Try
        Return 0
    End Function
    Public Function BroadBand_Year_Updater()
        Dim MessageBox As New Guna2MessageDialog
        MessageBox.Style = MessageDialogStyle.Dark
        connection.Open()
        Dim transaction2 As OleDbTransaction = connection.BeginTransaction
        Try
            Dim cmd As New OleDbCommand
            Dim maxVal As Integer
            cmd.CommandText = "SELECT MAX(PAYMENT_YEAR) AS max_val FROM BROADBAND_PAYMENT_DETAILS"
            cmd.Connection = connection
            cmd.Transaction = transaction2
            maxVal = CInt(cmd.ExecuteScalar())
            If maxVal < Date.Today.Year Then
                Dim crf_counter As New OleDbCommand("SELECT DISTINCT(CRF) FROM BROADBAND_CONNECTION_DETAILS WHERE BROADBAND_CONNECTION=@STATUS", connection)
                crf_counter.Parameters.AddWithValue("@STATUS", "YES")
                crf_counter.Transaction = transaction2
                Dim crf_reader As OleDbDataReader = crf_counter.ExecuteReader
                If crf_reader.HasRows = True Then
                    While crf_reader.Read
                        Dim Year_Update_Command As New OleDbCommand("INSERT INTO BROADBAND_PAYMENT_DETAILS (CRF,PAYMENT_YEAR) VALUES (@CRF,@PAYMENT_YEAR)", connection)
                        Year_Update_Command.Parameters.AddWithValue("@CRF", crf_reader.GetInt32(0))
                        Year_Update_Command.Parameters.AddWithValue("@PAYMENT_YEAR", Date.Now.Year)
                        Year_Update_Command.Transaction = transaction2
                        Year_Update_Command.ExecuteNonQuery()
                    End While
                End If
                crf_reader.Close()
            End If
            transaction2.Commit()
        Catch ex As Exception
            transaction2.Rollback()
            ErrorAlert.Play()
            LogError("An Error Occured During Updating Broadband Payment Year: " & ex.Message)
            If LoginType = "ADMIN" Then
                MessageBox.Show("An Error Occured While Updating BroadBand Payment Year. Check Log For More Details.", "ALERT")
            End If
            If LoginType = "CUSTOMER" Then
                MessageBox.Show("An Error Occured. Contact Admin.", "ALERT")
                Application.Exit()
            End If
        Finally
            connection.Close()
        End Try
        Return 0
    End Function
End Module
