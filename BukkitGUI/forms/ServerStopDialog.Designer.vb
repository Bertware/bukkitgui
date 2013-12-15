Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerStopDialog
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
        Me.ProgressBar = New Net.Bertware.Controls.VistaProgressBar()
        Me.BtnKill = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ProgressBar
        '
        Me.ProgressBar.BackColor = System.Drawing.Color.Transparent
        Me.ProgressBar.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.None
        Me.ProgressBar.DisplayText = lr("")
        Me.ProgressBar.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ProgressBar.Location = New System.Drawing.Point(12, 12)
        Me.ProgressBar.MarqueeSpeed = 10
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.ProgressBarStyle = Net.Bertware.Controls.VistaProgressBar.BarStyle.Marquee
        Me.ProgressBar.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
        Me.ProgressBar.Size = New System.Drawing.Size(272, 15)
        Me.ProgressBar.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ProgressBar.TabIndex = 0
        '
        'BtnKill
        '
        Me.BtnKill.Location = New System.Drawing.Point(12, 34)
        Me.BtnKill.Name = "BtnKill"
        Me.BtnKill.Size = New System.Drawing.Size(272, 23)
        Me.BtnKill.TabIndex = 1
        Me.BtnKill.Text = lr("&Kill")
        Me.BtnKill.UseVisualStyleBackColor = True
        '
        'ServerStopDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(296, 65)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnKill)
        Me.Controls.Add(Me.ProgressBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ServerStopDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Waiting for server to stop")
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressBar As Net.Bertware.Controls.VistaProgressBar
    Friend WithEvents BtnKill As System.Windows.Forms.Button
End Class


