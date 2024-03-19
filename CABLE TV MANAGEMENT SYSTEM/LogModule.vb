Imports System.IO
Public Module LogModule
    Public Sub LogError(message As String)
        Dim logFile As String = log_path
        Dim writer As StreamWriter
        If Not File.Exists(logFile) Then
            writer = File.CreateText(logFile)
        Else
            writer = File.AppendText(logFile)
        End If
        writer.WriteLine("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message)
        writer.Close()
    End Sub
End Module
