Imports Net.Bertware.BukkitGUI.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerSettingDialog
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
        Me.TxtValue = New System.Windows.Forms.TextBox()
        Me.TxtSetting = New System.Windows.Forms.TextBox()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = lr("value:")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = lr("Setting:")
        '
        'TxtValue
        '
        Me.TxtValue.Location = New System.Drawing.Point(61, 32)
        Me.TxtValue.Name = "TxtValue"
        Me.TxtValue.Size = New System.Drawing.Size(211, 20)
        Me.TxtValue.TabIndex = 8
        '
        'TxtSetting
        '
        Me.TxtSetting.Location = New System.Drawing.Point(61, 6)
        Me.TxtSetting.Name = "TxtSetting"
        Me.TxtSetting.ReadOnly = True
        Me.TxtSetting.Size = New System.Drawing.Size(211, 20)
        Me.TxtSetting.TabIndex = 9
        '
        'BtnOk
        '
        Me.BtnOk.Location = New System.Drawing.Point(197, 58)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 10
        Me.BtnOk.Text = lr("Ok")
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'ServerSettingDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 93)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.TxtSetting)
        Me.Controls.Add(Me.TxtValue)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ServerSettingDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Server settings...")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtValue As System.Windows.Forms.TextBox
    Friend WithEvents TxtSetting As System.Windows.Forms.TextBox
    Friend WithEvents BtnOk As System.Windows.Forms.Button
End Class

