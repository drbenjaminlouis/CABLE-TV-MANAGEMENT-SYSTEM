Imports System.Configuration
Imports System.IO

Module Config
    Public dbFilePath As String = "C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\App_Data\CABLE_TV_DB.accdb"
    Public invoicepath As String = ("Invoices\")
    Public img_path As String = ("C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\Assets\mail_bg.jpg")
    Public logo_path As String = ("C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\Assets\LOGO.gif")
    Public seal_path As String = ("C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\Assets\SEAL.png")
    Public log_path As String = ("C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\log.txt")
    Public app_name_text As String = "CABLE TV MANAGEMENT SYSTEM"
    Public ErrorAlert As New System.Media.SoundPlayer("C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\Assets\Error.wav")
    Public SuccessAlert As New System.Media.SoundPlayer("C:\Users\abyjo\source\repos\CABLE-TV-MANAGEMENT-SYSTEM\CABLE TV MANAGEMENT SYSTEM\Assets\Gpay.wav")

End Module
