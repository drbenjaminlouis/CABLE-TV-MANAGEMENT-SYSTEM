Imports System.Configuration
Imports System.IO

Module Config
    Public dbFilePath As String = "C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\App_Data\CABLE_TV_DB.accdb"
    Public invoicepath As String = ("Invoices\")
    Public img_path As String = ("Assets\mail_bg.jpg")
    Public logo_path As String = ("Assets\LOGO.gif")
    Public seal_path As String = ("Assets\SEAL.png")
    Public app_name_text As String = "CABLE TV MANAGEMENT SYSTEM"
    Public ErrorAlert As New System.Media.SoundPlayer("Assets\Error.wav")
    Public SuccessAlert As New System.Media.SoundPlayer("Assets\Gpay.wav")

End Module
