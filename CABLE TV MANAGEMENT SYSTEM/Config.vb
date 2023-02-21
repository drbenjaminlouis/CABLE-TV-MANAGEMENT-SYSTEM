Imports System.Configuration
Module Config
    Public dbFilePath As String = "C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\App_Data\CABLE_TV_DB.accdb"
    Public invoicepath As String = System.IO.Path.Combine(Application.StartupPath, "Invoices\")
    Public img_path As String = System.IO.Path.Combine(Application.StartupPath, "Assets\mail_bg.jpg")
    Public seal_path As String = System.IO.Path.Combine(Application.StartupPath, "Assets\SEAL.png")
    Public error_tone_path As String = System.IO.Path.Combine(Application.StartupPath, "Assets\Error.wav")
    Public success_tone_path As String = System.IO.Path.Combine(Application.StartupPath, "Assets\Gpay.wav")
    Public app_name_text As String = "CABLE TV MANAGEMENT SYSTEM"
    Public ErrorAlert As New System.Media.SoundPlayer(error_tone_path)
    Public SuccessAlert As New System.Media.SoundPlayer(success_tone_path)
End Module
