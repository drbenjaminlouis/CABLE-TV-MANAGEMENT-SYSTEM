<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppLoading
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(AppLoading))
        Label1 = New Label()
        Label2 = New Label()
        Timer1 = New Timer(components)
        Guna2ProgressIndicator1 = New Guna.UI2.WinForms.Guna2ProgressIndicator()
        MyProgress = New Guna.UI2.WinForms.Guna2CircleProgressBar()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.Transparent
        Label1.Dock = DockStyle.Left
        Label1.Font = New Font("Arial", 27.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = Color.White
        Label1.Location = New Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(462, 450)
        Label1.TabIndex = 1
        Label1.Text = "CABLE TV MANAGEMENT SYSTEM"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point)
        Label2.ForeColor = Color.White
        Label2.Location = New Point(590, 394)
        Label2.Name = "Label2"
        Label2.Size = New Size(123, 25)
        Label2.TabIndex = 2
        Label2.Text = "LOADING....."' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        ' 
        ' Guna2ProgressIndicator1
        ' 
        Guna2ProgressIndicator1.AutoStart = True
        Guna2ProgressIndicator1.BackColor = Color.Transparent
        Guna2ProgressIndicator1.Location = New Point(719, 371)
        Guna2ProgressIndicator1.Name = "Guna2ProgressIndicator1"
        Guna2ProgressIndicator1.ProgressColor = Color.FromArgb(CByte(0), CByte(153), CByte(0))
        Guna2ProgressIndicator1.ShadowDecoration.CustomizableEdges = CustomizableEdges1
        Guna2ProgressIndicator1.Size = New Size(69, 67)
        Guna2ProgressIndicator1.TabIndex = 89
        ' 
        ' MyProgress
        ' 
        MyProgress.AnimationSpeed = 0.8F
        MyProgress.BackColor = Color.Transparent
        MyProgress.FillColor = Color.WhiteSmoke
        MyProgress.FillThickness = 15
        MyProgress.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        MyProgress.ForeColor = Color.Transparent
        MyProgress.Location = New Point(12, 371)
        MyProgress.Minimum = 0
        MyProgress.Name = "MyProgress"
        MyProgress.ProgressColor = Color.Empty
        MyProgress.ProgressThickness = 15
        MyProgress.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        MyProgress.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        MyProgress.Size = New Size(67, 67)
        MyProgress.TabIndex = 90
        MyProgress.Text = "Guna2CircleProgressBar1"
        MyProgress.Value = 75
        MyProgress.Visible = False
        ' 
        ' AppLoading
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources._360_F_222074985_CwcuLMkQ0NBU2Qv1lqHD5XpHYtkY8mAB3
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(800, 450)
        Controls.Add(Guna2ProgressIndicator1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(MyProgress)
        FormBorderStyle = FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MaximumSize = New Size(800, 450)
        MinimumSize = New Size(800, 450)
        Name = "AppLoading"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Guna2ProgressIndicator1 As Guna.UI2.WinForms.Guna2ProgressIndicator
    Friend WithEvents MyProgress As Guna.UI2.WinForms.Guna2CircleProgressBar
End Class
