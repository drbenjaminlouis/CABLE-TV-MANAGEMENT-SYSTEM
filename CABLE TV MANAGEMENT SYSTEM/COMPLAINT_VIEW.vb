Imports System.Data.OleDb

Public Class COMPLAINT_VIEW
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf COMPLAINT_VIEW_Load
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Dim i As Integer = 0
    Private Sub COMPLAINT_VIEW_Load(sender As Object, e As EventArgs)
        complaints_fetcher()
        If C_ID_TEXT.Text = "" Then
            NEXT_BTN.PerformClick()
        End If
        AddHandler RESOLVE_BTN.Click, AddressOf RESOLVE_BTN_Click
        AddHandler NEXT_BTN.Click, AddressOf NEXT_BTN_Click
    End Sub
    Private Sub complaints_fetcher()
        C_ID_TEXT.Clear()
        CRF_TEXTBOX.Clear()
        NAME_TEXTBOX.Clear()
        EMAIL_TEXTBOX.Clear()
        COMPLAINT_TYPE.SelectedIndex = -1
        MessageTextbox.Clear()
        Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
        connection.Open()
        Dim count_command As New OleDbCommand("SELECT COUNT(*) FROM CUST_COMPLAINTS WHERE R_STATUS=@RSTATUS AND C_STATUS=@C_STATUS", connection)
        count_command.Parameters.Clear()
        count_command.Parameters.AddWithValue("@RSTATUS", "UNREAD")
        count_command.Parameters.AddWithValue("@C_STATUS", "UN RESOLVED")
        Dim num_rows As Integer = CInt(count_command.ExecuteScalar())
        If num_rows > 0 Then
            Dim complaint_fetcher As New OleDbCommand("SELECT TOP 1 C_ID, CRF, CUST_NAME, CUST_EMAIL, C_TYPE, MESSAGE FROM CUST_COMPLAINTS WHERE R_STATUS=@RSTATUS AND C_STATUS=@C_STATUS ORDER BY C_ID", connection)
            complaint_fetcher.Parameters.AddWithValue("@RSTATUS", "UNREAD")
            complaint_fetcher.Parameters.AddWithValue("@C_STATUS", "UN RESOLVED")
            Dim datareader As OleDbDataReader = complaint_fetcher.ExecuteReader()
            If datareader.HasRows = True Then
                While datareader.Read
                    C_ID_TEXT.Clear()
                    CRF_TEXTBOX.Clear()
                    NAME_TEXTBOX.Clear()
                    EMAIL_TEXTBOX.Clear()
                    COMPLAINT_TYPE.SelectedIndex = -1
                    MessageTextbox.Clear()
                    C_ID_TEXT.Text = datareader.GetInt32(0)
                    CRF_TEXTBOX.Text = datareader.GetInt32(1)
                    NAME_TEXTBOX.Text = datareader.GetString(2)
                    EMAIL_TEXTBOX.Text = datareader.GetString(3)
                    COMPLAINT_TYPE.SelectedItem = datareader.GetString(4)
                    MessageTextbox.Text = datareader.GetString(5)
                End While
                Dim read_maeker As New OleDbCommand("UPDATE CUST_COMPLAINTS SET R_STATUS=@RSTATUS WHERE C_ID=@CID", connection)
                read_maeker.Parameters.AddWithValue("@RSTATUS", "READ")
                read_maeker.Parameters.AddWithValue("@CID", C_ID_TEXT.Text)
                read_maeker.ExecuteNonQuery()
            Else

            End If
        Else
            C_ID_TEXT.Clear()
            CRF_TEXTBOX.Clear()
            NAME_TEXTBOX.Clear()
            EMAIL_TEXTBOX.Clear()
            COMPLAINT_TYPE.SelectedIndex = -1
            MessageTextbox.Clear()


            Dim count_command2 As New OleDbCommand("SELECT COUNT(*) FROM CUST_COMPLAINTS WHERE R_STATUS=@RSTATUS AND C_STATUS=@CSTATUS", connection)
            count_command2.Parameters.AddWithValue("@RSTATUS", "READ")
            count_command2.Parameters.AddWithValue("@C_STATUS", "UN RESOLVED")
            Dim num_rows2 As Integer = CInt(count_command2.ExecuteScalar())
            If i >= num_rows2 Then
                C_ID_TEXT.Clear()
                CRF_TEXTBOX.Clear()
                NAME_TEXTBOX.Clear()
                EMAIL_TEXTBOX.Clear()
                COMPLAINT_TYPE.SelectedIndex = -1
                MessageTextbox.Clear()


                i = i + 1
            Else
                i = i + 1
            End If
            If num_rows2 > 0 Then
                Dim min_c_id2 As New OleDbCommand("SELECT TOP " & i & " C_ID FROM CUST_COMPLAINTS WHERE R_STATUS=@RSTATUS AND C_STATUS=@CSTATUS", connection)

                min_c_id2.Parameters.AddWithValue("@RSTATUS", "READ")
                min_c_id2.Parameters.AddWithValue("@CSTATUS", "UN RESOLVED")
                Dim min_cid_reader2 As OleDbDataReader = min_c_id2.ExecuteReader()

                While min_cid_reader2.Read()
                    If i > num_rows2 Then
                        Exit While
                    End If

                    Dim datafetcher As New OleDbCommand("SELECT C_ID,CRF,CUST_NAME,CUST_EMAIL,C_TYPE,MESSAGE FROM CUST_COMPLAINTS WHERE R_STATUS=@RSTATUS AND C_STATUS=@C_STATUS AND C_ID=@CID", connection)
                    datafetcher.Parameters.AddWithValue("@RSTATUS", "READ")
                    datafetcher.Parameters.AddWithValue("@CSTATUS", "UN RESOLVED")
                    datafetcher.Parameters.AddWithValue("@CID", min_cid_reader2.GetInt32(0))
                    Dim datareader2 As OleDbDataReader = datafetcher.ExecuteReader()
                    If datareader2.HasRows Then
                        While datareader2.Read()
                            C_ID_TEXT.Text = datareader2.GetInt32(0)
                            CRF_TEXTBOX.Text = datareader2.GetInt32(1)
                            NAME_TEXTBOX.Text = datareader2.GetString(2)
                            EMAIL_TEXTBOX.Text = datareader2.GetString(3)
                            COMPLAINT_TYPE.SelectedItem = datareader2.GetString(4)
                            MessageTextbox.Text = datareader2.GetString(5)
                        End While
                    Else
                    End If
                End While
            End If
        End If
        If C_ID_TEXT.Text = "" And NAME_TEXTBOX.Text = "" Then
            MessageBox.Show("No More Complaints.", "ALERT")
        End If

        'Catch ex As Exception
        '    mess
        'End Try
    End Sub

    Private Sub RESOLVE_BTN_Click(sender As Object, e As EventArgs)
        If Not C_ID_TEXT.Text = "" Then
            Dim connection As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbFilePath)
            connection.Open()
            Dim resolver As New OleDbCommand("UPDATE CUST_COMPLAINTS SET C_STATUS=@CSTATUS WHERE C_ID=@CID", connection)
            resolver.Parameters.AddWithValue("@CSTATUS", "RESOLVED")
            resolver.Parameters.AddWithValue("@CID", C_ID_TEXT.Text)
            resolver.ExecuteNonQuery()
            Email.Complaint_Resolved(EMAIL_TEXTBOX.Text, C_ID_TEXT.Text, NAME_TEXTBOX.Text, COMPLAINT_TYPE.SelectedItem)
            MessageBox.Show("Complaint Marked As Resolved.", "ALERT")
            C_ID_TEXT.Clear()
            CRF_TEXTBOX.Clear()
            NAME_TEXTBOX.Clear()
            EMAIL_TEXTBOX.Clear()
            COMPLAINT_TYPE.SelectedIndex = -1
            MessageTextbox.Clear()
        End If
        complaints_fetcher()
    End Sub

    Private Sub NEXT_BTN_Click(sender As Object, e As EventArgs)
        complaints_fetcher()
    End Sub
End Class