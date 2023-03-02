<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EDIT_CUST_PANEL
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
        Dim Animation1 As Guna.UI2.AnimatorNS.Animation = New Guna.UI2.AnimatorNS.Animation()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(EDIT_CUST_PANEL))
        Label1 = New Label()
        Label2 = New Label()
        Guna2ControlBox1 = New Guna.UI2.WinForms.Guna2ControlBox()
        Guna2Transition1 = New Guna.UI2.WinForms.Guna2Transition()
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Guna2Transition1.SetDecoration(Label1, Guna.UI2.AnimatorNS.DecorationType.None)
        Label1.Font = New Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = Color.LimeGreen
        Label1.Location = New Point(12, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(715, 37)
        Label1.TabIndex = 0
        Label1.Text = "FOR EDITING THE FOLLOWING DETAILS CONTACT ADMINISTRATOR"' 
        ' Label2
        ' 
        Label2.BackColor = Color.Transparent
        Guna2Transition1.SetDecoration(Label2, Guna.UI2.AnimatorNS.DecorationType.None)
        Label2.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(12, 61)
        Label2.Name = "Label2"
        Label2.Size = New Size(482, 262)
        Label2.TabIndex = 1
        Label2.Text = "# Name" & vbCrLf & vbCrLf & "# ID Type" & vbCrLf & vbCrLf & "# ID Number" & vbCrLf & vbCrLf & "# Chip ID" & vbCrLf & vbCrLf & "# Cable Plan" & vbCrLf & vbCrLf & "# BroadBand Plan" & vbCrLf & vbCrLf & "# Username"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Guna2ControlBox1
        ' 
        Guna2ControlBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Guna2ControlBox1.CustomizableEdges = CustomizableEdges1
        Guna2Transition1.SetDecoration(Guna2ControlBox1, Guna.UI2.AnimatorNS.DecorationType.None)
        Guna2ControlBox1.FillColor = Color.FromArgb(CByte(25), CByte(25), CByte(25))
        Guna2ControlBox1.IconColor = Color.White
        Guna2ControlBox1.Location = New Point(752, 12)
        Guna2ControlBox1.Name = "Guna2ControlBox1"
        Guna2ControlBox1.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Guna2ControlBox1.Size = New Size(45, 37)
        Guna2ControlBox1.TabIndex = 2
        ' 
        ' Guna2Transition1
        ' 
        Guna2Transition1.Cursor = Nothing
        Animation1.AnimateOnlyDifferences = True
        Animation1.BlindCoeff = CType(resources.GetObject("Animation1.BlindCoeff"), PointF)
        Animation1.LeafCoeff = 0F
        Animation1.MaxTime = 1F
        Animation1.MinTime = 0F
        Animation1.MosaicCoeff = CType(resources.GetObject("Animation1.MosaicCoeff"), PointF)
        Animation1.MosaicShift = CType(resources.GetObject("Animation1.MosaicShift"), PointF)
        Animation1.MosaicSize = 0
        Animation1.Padding = New Padding(0)
        Animation1.RotateCoeff = 0F
        Animation1.RotateLimit = 0F
        Animation1.ScaleCoeff = CType(resources.GetObject("Animation1.ScaleCoeff"), PointF)
        Animation1.SlideCoeff = CType(resources.GetObject("Animation1.SlideCoeff"), PointF)
        Animation1.TimeCoeff = 0F
        Animation1.TransparencyCoeff = 0F
        Guna2Transition1.DefaultAnimation = Animation1
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 20
        Guna2Elipse1.TargetControl = Me
        ' 
        ' EDIT_CUST_PANEL
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        ClientSize = New Size(809, 352)
        Controls.Add(Guna2ControlBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Guna2Transition1.SetDecoration(Me, Guna.UI2.AnimatorNS.DecorationType.None)
        FormBorderStyle = FormBorderStyle.None
        Name = "EDIT_CUST_PANEL"
        StartPosition = FormStartPosition.CenterScreen
        Text = "EDIT_CUST_PANEL"
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Guna2ControlBox1 As Guna.UI2.WinForms.Guna2ControlBox
    Friend WithEvents Guna2Transition1 As Guna.UI2.WinForms.Guna2Transition
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
End Class
