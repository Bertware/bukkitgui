Imports Net.Bertware.BukkitGUI.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LicenseAcceptDialog
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
        Me.ChkAccept = New System.Windows.Forms.CheckBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.WebLicense = New System.Windows.Forms.WebBrowser()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ChkAccept
        '
        Me.ChkAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ChkAccept.AutoSize = True
        Me.ChkAccept.Location = New System.Drawing.Point(13, 533)
        Me.ChkAccept.Name = "ChkAccept"
        Me.ChkAccept.Size = New System.Drawing.Size(155, 17)
        Me.ChkAccept.TabIndex = 1
        Me.ChkAccept.Text = lr("I have read and accept the")
        Me.ChkAccept.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Enabled = False
        Me.btnOk.Location = New System.Drawing.Point(416, 533)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = lr("OK")
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'WebLicense
        '
        Me.WebLicense.AllowNavigation = False
        Me.WebLicense.AllowWebBrowserDrop = False
        Me.WebLicense.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebLicense.IsWebBrowserContextMenuEnabled = False
        Me.WebLicense.Location = New System.Drawing.Point(12, 12)
        Me.WebLicense.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebLicense.Name = "WebLicense"
        Me.WebLicense.Size = New System.Drawing.Size(560, 515)
        Me.WebLicense.TabIndex = 3
        Me.WebLicense.WebBrowserShortcutsEnabled = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Enabled = False
        Me.BtnCancel.Location = New System.Drawing.Point(497, 533)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 4
        Me.BtnCancel.Text = lr("Cancel")
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'LicenseAcceptDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 562)
        Me.ControlBox = False
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.WebLicense)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.ChkAccept)
        Me.Name = "LicenseAcceptDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Accept license...")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ChkAccept As System.Windows.Forms.CheckBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents WebLicense As System.Windows.Forms.WebBrowser
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
End Class
