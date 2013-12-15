Imports Net.Bertware.BukkitGUI.Core.language

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OutputBrowser
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
        Me.TxtOutput = New Net.Bertware.Controls.AdvancedRichTextBox(Me.components)
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnCopy = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.RightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BtnRightClickCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.RightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtOutput
        '
        Me.TxtOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtOutput.Location = New System.Drawing.Point(12, 12)
        Me.TxtOutput.Name = "TxtOutput"
        Me.TxtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.TxtOutput.Size = New System.Drawing.Size(852, 483)
        Me.TxtOutput.TabIndex = 0
        Me.TxtOutput.Text = lr("")
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(789, 501)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(75, 23)
        Me.BtnClose.TabIndex = 1
        Me.BtnClose.Text = lr("Close")
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnCopy
        '
        Me.BtnCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCopy.Location = New System.Drawing.Point(708, 501)
        Me.BtnCopy.Name = "BtnCopy"
        Me.BtnCopy.Size = New System.Drawing.Size(75, 23)
        Me.BtnCopy.TabIndex = 2
        Me.BtnCopy.Text = lr("Copy")
        Me.BtnCopy.UseVisualStyleBackColor = True
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRefresh.Location = New System.Drawing.Point(627, 501)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.BtnRefresh.TabIndex = 3
        Me.BtnRefresh.Text = lr("Refresh")
        Me.BtnRefresh.UseVisualStyleBackColor = True
        '
        'RightClickMenu
        '
        Me.RightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnRightClickCopy})
        Me.RightClickMenu.Name = "RightClickMenu"
        Me.RightClickMenu.Size = New System.Drawing.Size(103, 26)
        '
        'BtnRightClickCopy
        '
        Me.BtnRightClickCopy.Name = "BtnRightClickCopy"
        Me.BtnRightClickCopy.Size = New System.Drawing.Size(102, 22)
        Me.BtnRightClickCopy.Text = lr("Copy")
        '
        'OutputBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 534)
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.BtnCopy)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.TxtOutput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OutputBrowser"
        Me.Text = lr("Browse console output")
        Me.RightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TxtOutput As Net.Bertware.Controls.AdvancedRichTextBox
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnCopy As System.Windows.Forms.Button
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents RightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BtnRightClickCopy As System.Windows.Forms.ToolStripMenuItem
End Class
