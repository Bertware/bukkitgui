Imports Net.Bertware.BukkitGUI.Core
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ErrorDiagnose
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorDiagnose))
        Me.lblWarningType = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.LblCause = New System.Windows.Forms.Label()
        Me.lblText = New System.Windows.Forms.Label()
        Me.lblSolutions = New System.Windows.Forms.Label()
        Me.LVsolutions = New System.Windows.Forms.ListView()
        Me.ColSolution = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BtnApply = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblWarningType
        '
        Me.lblWarningType.AutoSize = True
        Me.lblWarningType.Location = New System.Drawing.Point(12, 9)
        Me.lblWarningType.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblWarningType.Name = "lblWarningType"
        Me.lblWarningType.Size = New System.Drawing.Size(73, 13)
        Me.lblWarningType.TabIndex = 1
        Me.lblWarningType.Text = lr("Warning type:")
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(12, 24)
        Me.lblTime.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(33, 13)
        Me.lblTime.TabIndex = 2
        Me.lblTime.Text = lr("Time:")
        '
        'LblCause
        '
        Me.LblCause.AutoSize = True
        Me.LblCause.Location = New System.Drawing.Point(12, 54)
        Me.LblCause.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.LblCause.Name = "LblCause"
        Me.LblCause.Size = New System.Drawing.Size(40, 13)
        Me.LblCause.TabIndex = 4
        Me.LblCause.Text = lr("Cause:")
        '
        'lblText
        '
        Me.lblText.AutoSize = True
        Me.lblText.Location = New System.Drawing.Point(12, 39)
        Me.lblText.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(31, 13)
        Me.lblText.TabIndex = 3
        Me.lblText.Text = lr("Text:")
        '
        'lblSolutions
        '
        Me.lblSolutions.AutoSize = True
        Me.lblSolutions.Location = New System.Drawing.Point(12, 71)
        Me.lblSolutions.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblSolutions.Name = "lblSolutions"
        Me.lblSolutions.Size = New System.Drawing.Size(53, 13)
        Me.lblSolutions.TabIndex = 6
        Me.lblSolutions.Text = lr("Solutions:")
        '
        'LVsolutions
        '
        Me.LVsolutions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LVsolutions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColSolution})
        Me.LVsolutions.FullRowSelect = True
        Me.LVsolutions.Location = New System.Drawing.Point(15, 87)
        Me.LVsolutions.Name = "LVsolutions"
        Me.LVsolutions.Size = New System.Drawing.Size(610, 158)
        Me.LVsolutions.TabIndex = 7
        Me.LVsolutions.UseCompatibleStateImageBehavior = False
        Me.LVsolutions.View = System.Windows.Forms.View.Details
        '
        'ColSolution
        '
        Me.ColSolution.Text = lr("Solution")
        Me.ColSolution.Width = 500
        '
        'BtnApply
        '
        Me.BtnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnApply.Location = New System.Drawing.Point(550, 251)
        Me.BtnApply.Name = "BtnApply"
        Me.BtnApply.Size = New System.Drawing.Size(75, 23)
        Me.BtnApply.TabIndex = 8
        Me.BtnApply.Text = lr("Apply")
        Me.BtnApply.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Location = New System.Drawing.Point(469, 251)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 9
        Me.BtnCancel.Text = lr("Cancel")
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'ErrorDiagnose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 286)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnApply)
        Me.Controls.Add(Me.LVsolutions)
        Me.Controls.Add(Me.lblSolutions)
        Me.Controls.Add(Me.LblCause)
        Me.Controls.Add(Me.lblText)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.lblWarningType)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ErrorDiagnose"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("ErrorDiagnose")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblWarningType As System.Windows.Forms.Label
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents LblCause As System.Windows.Forms.Label
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents lblSolutions As System.Windows.Forms.Label
    Friend WithEvents LVsolutions As System.Windows.Forms.ListView
    Friend WithEvents BtnApply As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents ColSolution As System.Windows.Forms.ColumnHeader
End Class
