Imports System.Data.OleDb

Module Update_Pending_Amout_And_Months
    Public Function GetPendingPayments(ByVal crf As String, ByVal year As Integer) As Integer
        Using con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            con.Open()
            Dim query As String = "SELECT IIF([january]='Not Paid',1,0) AS january, " &
                                   "IIF([february]='Not Paid',1,0) AS february, " &
                                   "IIF([march]='Not Paid',1,0) AS march, " &
                                   "IIF([april]='Not Paid',1,0) AS april, " &
                                   "IIF([may]='Not Paid',1,0) AS may, " &
                                   "IIF([june]='Not Paid',1,0) AS june, " &
                                   "IIF([july]='Not Paid',1,0) AS july, " &
                                   "IIF([august]='Not Paid',1,0) AS august, " &
                                   "IIF([september]='Not Paid',1,0) AS september, " &
                                   "IIF([october]='Not Paid',1,0) AS october, " &
                                    "IIF([november]='Not Paid',1,0) AS november, " &
                                    "IIF([december]='Not Paid',1,0) AS december " &
                                    "FROM TV_PAYMENT_DETAILS " &
                                    "WHERE CRF=@CRF AND CURRENT_YEAR=@YEAR"

            Using command As New OleDbCommand(query, con)
                command.Parameters.AddWithValue("@CRF", crf)
                command.Parameters.AddWithValue("@YEAR", year)
                Dim reader As OleDbDataReader = command.ExecuteReader()
                Dim pendingPayments As Integer = 0

                If reader.HasRows Then
                    ' Read the first row
                    reader.Read()

                    ' Check the value of each month and add the corresponding month name to the ComboBox if it's not paid
                    If reader("january") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("february") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("march") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("april") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("may") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("june") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("july") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("august") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("september") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("october") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("november") = 1 Then
                        pendingPayments += 250
                    End If
                    If reader("december") = 1 Then
                        pendingPayments += 250
                    End If
                End If
            End Using
            con.Close()
        End Using
        Return 0
    End Function
End Module
