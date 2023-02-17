<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Loading
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
        Me.components = New System.ComponentModel.Container()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Guna2ProgressIndicator1 = New Guna.UI2.WinForms.Guna2ProgressIndicator()
        Me.MyProgress = New Guna.UI2.WinForms.Guna2CircleProgressBar()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Arial", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(462, 450)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "CABLE TV MANAGEMENT SYSTEM"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(590, 394)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 25)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "LOADING....."
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Guna2ProgressIndicator1
        '
        Me.Guna2ProgressIndicator1.AutoStart = True
        Me.Guna2ProgressIndicator1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2ProgressIndicator1.Location = New System.Drawing.Point(719, 371)
        Me.Guna2ProgressIndicator1.Name = "Guna2ProgressIndicator1"
        Me.Guna2ProgressIndicator1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Guna2ProgressIndicator1.ShadowDecoration.CustomizableEdges = CustomizableEdges1
        Me.Guna2ProgressIndicator1.Size = New System.Drawing.Size(69, 67)
        Me.Guna2ProgressIndicator1.TabIndex = 89
        '
        'MyProgress
        '
        Me.MyProgress.AnimationSpeed = 0.8!
        Me.MyProgress.BackColor = System.Drawing.Color.Transparent
        Me.MyProgress.FillColor = System.Drawing.Color.WhiteSmoke
        Me.MyProgress.FillThickness = 15
        Me.MyProgress.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.MyProgress.ForeColor = System.Drawing.Color.Transparent
        Me.MyProgress.Location = New System.Drawing.Point(12, 371)
        Me.MyProgress.Minimum = 0
        Me.MyProgress.Name = "MyProgress"
        Me.MyProgress.ProgressColor = System.Drawing.Color.Empty
        Me.MyProgress.ProgressThickness = 15
        Me.MyProgress.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Me.MyProgress.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.MyProgress.Size = New System.Drawing.Size(67, 67)
        Me.MyProgress.TabIndex = 90
        Me.MyProgress.Text = "Guna2CircleProgressBar1"
        Me.MyProgress.Value = 75
        Me.MyProgress.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.CABLE_TV_MANAGEMENT_SYSTEM.My.Resources.Resources._360_F_222074985_CwcuLMkQ0NBU2Qv1lqHD5XpHYtkY8mAB3
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Guna2ProgressIndicator1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MyProgress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(800, 450)
        Me.MinimumSize = New System.Drawing.Size(800, 450)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Guna2ProgressIndicator1 As Guna.UI2.WinForms.Guna2ProgressIndicator
    Friend WithEvents MyProgress As Guna.UI2.WinForms.Guna2CircleProgressBar
End Class
