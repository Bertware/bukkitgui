<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LangGen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LangGen))
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.VPBar = New Bertware.Controls.VistaProgressBar()
        Me.TxtLog = New Bertware.Controls.AdvancedRichTextBox(Me.components)
        Me.SuspendLayout()
        '
        'BtnGo
        '
        Me.BtnGo.Location = New System.Drawing.Point(703, 12)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(103, 23)
        Me.BtnGo.TabIndex = 1
        Me.BtnGo.Text = "Open and Go"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'VPBar
        '
        Me.VPBar.BackColor = System.Drawing.Color.Transparent
        Me.VPBar.DisplayText = ""
        Me.VPBar.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.VPBar.Location = New System.Drawing.Point(12, 20)
        Me.VPBar.Name = "VPBar"
        Me.VPBar.ShowPercentage = Bertware.Controls.VistaProgressBar.TextShowFormats.None
        Me.VPBar.Size = New System.Drawing.Size(685, 15)
        Me.VPBar.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.VPBar.TabIndex = 2
        '
        'TxtLog
        '
        Me.TxtLog.Location = New System.Drawing.Point(12, 41)
        Me.TxtLog.Name = "TxtLog"
        Me.TxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.TxtLog.Size = New System.Drawing.Size(794, 435)
        Me.TxtLog.TabIndex = 3
        Me.TxtLog.Text = ""
        '
        'LangGen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 488)
        Me.Controls.Add(Me.TxtLog)
        Me.Controls.Add(Me.VPBar)
        Me.Controls.Add(Me.BtnGo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LangGen"
        Me.Text = "Language generator for bukkitGUI"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnGo As System.Windows.Forms.Button
    Friend WithEvents VPBar As Bertware.Controls.VistaProgressBar
    Friend WithEvents TxtLog As Bertware.Controls.AdvancedRichTextBox
End Class
