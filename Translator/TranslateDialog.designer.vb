
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TranslateDialog
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtOriginal = New System.Windows.Forms.TextBox()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.TxtTranslation = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Translation:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Original:"
        '
        'TxtOriginal
        '
        Me.TxtOriginal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtOriginal.Location = New System.Drawing.Point(80, 6)
        Me.TxtOriginal.Multiline = True
        Me.TxtOriginal.Name = "TxtOriginal"
        Me.TxtOriginal.ReadOnly = True
        Me.TxtOriginal.Size = New System.Drawing.Size(488, 60)
        Me.TxtOriginal.TabIndex = 9
        '
        'BtnOk
        '
        Me.BtnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOk.Location = New System.Drawing.Point(493, 160)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 10
        Me.BtnOk.Text = "Ok"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'TxtTranslation
        '
        Me.TxtTranslation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtTranslation.Location = New System.Drawing.Point(80, 72)
        Me.TxtTranslation.Multiline = True
        Me.TxtTranslation.Name = "TxtTranslation"
        Me.TxtTranslation.Size = New System.Drawing.Size(488, 82)
        Me.TxtTranslation.TabIndex = 8
        '
        'TranslateDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(580, 195)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.TxtOriginal)
        Me.Controls.Add(Me.TxtTranslation)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TranslateDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Translate"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtOriginal As System.Windows.Forms.TextBox
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents TxtTranslation As System.Windows.Forms.TextBox
End Class

