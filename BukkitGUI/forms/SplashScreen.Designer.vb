Imports System.Reflection

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
    Inherits System.Windows.Forms.Form

    WithEvents Adom As AppDomain = AppDomain.CurrentDomain
    Public Function LoadDLL(sender As Object, args As ResolveEventArgs) As System.Reflection.Assembly Handles Adom.AssemblyResolve 'Load embedded DLLs
        Dim resourceName As [String] = "Net.Bertware.BukkitGUI." & New AssemblyName(args.Name).Name & ".dll"
        Using stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
            If stream Is Nothing OrElse stream.Length < 1 Then Return Nothing : Exit Function
            Dim assemblyData As [Byte]() = New [Byte](stream.Length - 1) {}
            stream.Read(assemblyData, 0, assemblyData.Length)
            Return Assembly.Load(assemblyData)
        End Using
    End Function

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

        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Me.Gradient_full = New Net.Bertware.Controls.Gradient.GradientPanel
        Me.lblweb = New System.Windows.Forms.Label()
        Me.lblcopyright = New System.Windows.Forms.Label()
        Me.lblauthors = New System.Windows.Forms.Label()
        Me.lblversion = New System.Windows.Forms.Label()
        Me.lbltitle = New System.Windows.Forms.Label()
        Me.gradient_load = New Net.Bertware.Controls.Gradient.GradientPanel
        Me.lblloadpercent = New System.Windows.Forms.Label()
        Me.lblload = New System.Windows.Forms.Label()
        Me.PBLoad = New Net.Bertware.Controls.VistaProgressBar()
        Me.Gradient_full.SuspendLayout()
        Me.gradient_load.SuspendLayout()
        Me.SuspendLayout()
        '
        'Gradient_full
        '
        Me.Gradient_full.BackColor = System.Drawing.Color.Transparent
        Me.Gradient_full.BorderColor = System.Drawing.Color.White
        Me.Gradient_full.BorderWidth = 2
        Me.Gradient_full.Controls.Add(Me.lblweb)
        Me.Gradient_full.Controls.Add(Me.lblcopyright)
        Me.Gradient_full.Controls.Add(Me.lblauthors)
        Me.Gradient_full.Controls.Add(Me.lblversion)
        Me.Gradient_full.Controls.Add(Me.lbltitle)
        Me.Gradient_full.Controls.Add(Me.gradient_load)
        Me.Gradient_full.GradientEndColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Gradient_full.GradientStartColor = System.Drawing.Color.White
        Me.Gradient_full.Image = CType(resources.GetObject("Gradient_full.Image"), System.Drawing.Image)
        Me.Gradient_full.ImageLocation = New System.Drawing.Point(4, 4)
        Me.Gradient_full.Location = New System.Drawing.Point(0, 0)
        Me.Gradient_full.Name = "Gradient_full"
        Me.Gradient_full.RoundCornerRadius = 1
        Me.Gradient_full.ShadowOffSet = 0
        Me.Gradient_full.Size = New System.Drawing.Size(500, 200)
        Me.Gradient_full.TabIndex = 0
        Me.Gradient_full.UseWaitCursor = True
        '
        'lblweb
        '
        Me.lblweb.AutoSize = True
        Me.lblweb.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblweb.Location = New System.Drawing.Point(146, 85)
        Me.lblweb.Name = "lblweb"
        Me.lblweb.Size = New System.Drawing.Size(38, 19)
        Me.lblweb.TabIndex = 5
        Me.lblweb.Text = "Web"
        Me.lblweb.UseWaitCursor = True
        '
        'lblcopyright
        '
        Me.lblcopyright.AutoSize = True
        Me.lblcopyright.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcopyright.Location = New System.Drawing.Point(146, 66)
        Me.lblcopyright.Name = "lblcopyright"
        Me.lblcopyright.Size = New System.Drawing.Size(71, 19)
        Me.lblcopyright.TabIndex = 4
        Me.lblcopyright.Text = "Copyright"
        Me.lblcopyright.UseWaitCursor = True
        '
        'lblauthors
        '
        Me.lblauthors.AutoSize = True
        Me.lblauthors.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblauthors.Location = New System.Drawing.Point(146, 47)
        Me.lblauthors.Name = "lblauthors"
        Me.lblauthors.Size = New System.Drawing.Size(59, 19)
        Me.lblauthors.TabIndex = 3
        Me.lblauthors.Text = "Authors"
        Me.lblauthors.UseWaitCursor = True
        '
        'lblversion
        '
        Me.lblversion.AutoSize = True
        Me.lblversion.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblversion.Location = New System.Drawing.Point(146, 28)
        Me.lblversion.Name = "lblversion"
        Me.lblversion.Size = New System.Drawing.Size(57, 19)
        Me.lblversion.TabIndex = 2
        Me.lblversion.Text = "Version"
        Me.lblversion.UseWaitCursor = True
        '
        'lbltitle
        '
        Me.lbltitle.AutoSize = True
        Me.lbltitle.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitle.Location = New System.Drawing.Point(146, 9)
        Me.lbltitle.Name = "lbltitle"
        Me.lbltitle.Size = New System.Drawing.Size(38, 19)
        Me.lbltitle.TabIndex = 1
        Me.lbltitle.Text = "Title"
        Me.lbltitle.UseWaitCursor = True
        '
        'gradient_load
        '
        Me.gradient_load.BorderColor = System.Drawing.Color.Gray
        Me.gradient_load.Controls.Add(Me.lblloadpercent)
        Me.gradient_load.Controls.Add(Me.lblload)
        Me.gradient_load.Controls.Add(Me.PBLoad)
        Me.gradient_load.GradientEndColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gradient_load.GradientStartColor = System.Drawing.Color.White
        Me.gradient_load.Image = Nothing
        Me.gradient_load.ImageLocation = New System.Drawing.Point(4, 4)
        Me.gradient_load.Location = New System.Drawing.Point(12, 132)
        Me.gradient_load.Name = "gradient_load"
        Me.gradient_load.RoundCornerRadius = 20
        Me.gradient_load.ShadowOffSet = 0
        Me.gradient_load.Size = New System.Drawing.Size(476, 56)
        Me.gradient_load.TabIndex = 0
        Me.gradient_load.UseWaitCursor = True
        '
        'lblloadpercent
        '
        Me.lblloadpercent.Location = New System.Drawing.Point(369, 8)
        Me.lblloadpercent.Name = "lblloadpercent"
        Me.lblloadpercent.Size = New System.Drawing.Size(100, 23)
        Me.lblloadpercent.TabIndex = 2
        Me.lblloadpercent.Text = "%"
        Me.lblloadpercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblloadpercent.UseWaitCursor = True
        '
        'lblload
        '
        Me.lblload.AutoSize = True
        Me.lblload.Location = New System.Drawing.Point(7, 13)
        Me.lblload.Name = "lblload"
        Me.lblload.Size = New System.Drawing.Size(27, 13)
        Me.lblload.TabIndex = 1
        Me.lblload.Text = "load"
        Me.lblload.UseWaitCursor = True
        '
        'PBLoad
        '
        Me.PBLoad.BackColor = System.Drawing.Color.Transparent
        Me.PBLoad.DisplayText = ""
        Me.PBLoad.EndColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PBLoad.Location = New System.Drawing.Point(10, 31)
        Me.PBLoad.Margin = New System.Windows.Forms.Padding(5)
        Me.PBLoad.Name = "PBLoad"
        Me.PBLoad.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.ValueOnly
        Me.PBLoad.Size = New System.Drawing.Size(459, 15)
        Me.PBLoad.StartColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PBLoad.TabIndex = 0
        Me.PBLoad.UseWaitCursor = True
        Me.PBLoad.Value = 25
        Me.PBLoad.Animate = True
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Magenta
        Me.ClientSize = New System.Drawing.Size(500, 200)
        Me.Controls.Add(Me.Gradient_full)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SplashScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SplashScreen"
        Me.TransparencyKey = System.Drawing.Color.Magenta
        Me.Gradient_full.ResumeLayout(False)
        Me.Gradient_full.PerformLayout()
        Me.gradient_load.ResumeLayout(False)
        Me.gradient_load.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gradient_load As Net.Bertware.Controls.Gradient.GradientPanel
    Friend WithEvents lblloadpercent As System.Windows.Forms.Label
    Friend WithEvents lblload As System.Windows.Forms.Label
    Friend WithEvents PBLoad As Net.Bertware.Controls.VistaProgressBar
    Friend WithEvents lbltitle As System.Windows.Forms.Label
    Friend WithEvents lblversion As System.Windows.Forms.Label
    Friend WithEvents lblauthors As System.Windows.Forms.Label
    Friend WithEvents lblcopyright As System.Windows.Forms.Label
    Friend WithEvents lblweb As System.Windows.Forms.Label
    Friend WithEvents Gradient_full As Net.Bertware.Controls.Gradient.GradientPanel
End Class