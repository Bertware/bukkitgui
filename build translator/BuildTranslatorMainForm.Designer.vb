<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BuildTranslatorMainForm
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
        Me.PBarProgress = New System.Windows.Forms.ProgressBar()
        Me.TxtLog = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'PBarProgress
        '
        Me.PBarProgress.Location = New System.Drawing.Point(12, 12)
        Me.PBarProgress.Name = "PBarProgress"
        Me.PBarProgress.Size = New System.Drawing.Size(544, 15)
        Me.PBarProgress.TabIndex = 1
        '
        'TxtLog
        '
        Me.TxtLog.Location = New System.Drawing.Point(12, 33)
        Me.TxtLog.Name = "TxtLog"
        Me.TxtLog.Size = New System.Drawing.Size(544, 422)
        Me.TxtLog.TabIndex = 2
        Me.TxtLog.Text = ""
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 467)
        Me.Controls.Add(Me.TxtLog)
        Me.Controls.Add(Me.PBarProgress)
        Me.Name = "Form1"
        Me.Text = "Build translator"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PBarProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents TxtLog As System.Windows.Forms.RichTextBox

End Class
