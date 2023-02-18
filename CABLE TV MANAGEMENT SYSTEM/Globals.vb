Imports System.Configuration
Module Globals
    Public dbFilePath As String = "C:\Users\abyjo\source\repos\CABLE TV MANAGEMENT SYSTEM\CABLE TV MANAGEMENT SYSTEM\App_Data\CABLE_TV_DB.accdb"
    Public invoicepath As String = System.IO.Path.Combine(Application.StartupPath, "Invoices\")
    Public img_path As String = System.IO.Path.Combine(Application.StartupPath, "Assets\mail_bg.jpg")
    Public seal_path As String = System.IO.Path.Combine(Application.StartupPath, "Assets\SEAL.png")
End Module
