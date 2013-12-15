Imports Net.Bertware.BukkitGUI.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TaskDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TaskDialog))
        Me.TxtTaskName = New System.Windows.Forms.TextBox()
        Me.lblTaskName = New System.Windows.Forms.Label()
        Me.CBTriggerType = New System.Windows.Forms.ComboBox()
        Me.lblTriggerType = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTriggerHelp = New System.Windows.Forms.Label()
        Me.TxtTriggerParam = New System.Windows.Forms.TextBox()
        Me.lblTriggerParameters = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblActionHelp = New System.Windows.Forms.Label()
        Me.lblActionParameters = New System.Windows.Forms.Label()
        Me.TxtActionParam = New System.Windows.Forms.TextBox()
        Me.CBActionType = New System.Windows.Forms.ComboBox()
        Me.lblActionType = New System.Windows.Forms.Label()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.ErrProv = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ChkEnable = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtTaskName
        '
        Me.TxtTaskName.Location = New System.Drawing.Point(56, 12)
        Me.TxtTaskName.Name = "TxtTaskName"
        Me.TxtTaskName.Size = New System.Drawing.Size(321, 20)
        Me.TxtTaskName.TabIndex = 0
        Me.ToolTip.SetToolTip(Me.TxtTaskName, lr("The name of the task. Doesn't affect the working of the task."))
        '
        'lblTaskName
        '
        Me.lblTaskName.AutoSize = True
        Me.lblTaskName.Location = New System.Drawing.Point(12, 15)
        Me.lblTaskName.Name = "lblTaskName"
        Me.lblTaskName.Size = New System.Drawing.Size(38, 13)
        Me.lblTaskName.TabIndex = 1
        Me.lblTaskName.Text = lr("Name:")
        '
        'CBTriggerType
        '
        Me.CBTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBTriggerType.FormattingEnabled = True
        Me.CBTriggerType.Items.AddRange(New Object() {lr("server start"), lr("server starting"), lr("server stop"), lr("server stopping"), lr("elapsed time"), lr("current time"), lr("player join"), lr("player leave"), lr("server empty"), lr("heartbeat fail"), lr("task finished"), lr("server output")})
        Me.CBTriggerType.Location = New System.Drawing.Point(74, 13)
        Me.CBTriggerType.Name = "CBTriggerType"
        Me.CBTriggerType.Size = New System.Drawing.Size(187, 21)
        Me.CBTriggerType.TabIndex = 2
        '
        'lblTriggerType
        '
        Me.lblTriggerType.AutoSize = True
        Me.lblTriggerType.Location = New System.Drawing.Point(6, 16)
        Me.lblTriggerType.Name = "lblTriggerType"
        Me.lblTriggerType.Size = New System.Drawing.Size(34, 13)
        Me.lblTriggerType.TabIndex = 3
        Me.lblTriggerType.Text = lr("Type:")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTriggerHelp)
        Me.GroupBox1.Controls.Add(Me.TxtTriggerParam)
        Me.GroupBox1.Controls.Add(Me.lblTriggerParameters)
        Me.GroupBox1.Controls.Add(Me.CBTriggerType)
        Me.GroupBox1.Controls.Add(Me.lblTriggerType)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(362, 141)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = lr("Trigger")
        '
        'lblTriggerHelp
        '
        Me.lblTriggerHelp.AutoSize = True
        Me.lblTriggerHelp.Location = New System.Drawing.Point(6, 63)
        Me.lblTriggerHelp.MaximumSize = New System.Drawing.Size(350, 0)
        Me.lblTriggerHelp.Name = "lblTriggerHelp"
        Me.lblTriggerHelp.Size = New System.Drawing.Size(182, 13)
        Me.lblTriggerHelp.TabIndex = 6
        Me.lblTriggerHelp.Text = lr("Explanation: Please select a type first")
        '
        'TxtTriggerParam
        '
        Me.TxtTriggerParam.Location = New System.Drawing.Point(74, 40)
        Me.TxtTriggerParam.Name = "TxtTriggerParam"
        Me.TxtTriggerParam.Size = New System.Drawing.Size(187, 20)
        Me.TxtTriggerParam.TabIndex = 5
        Me.ToolTip.SetToolTip(Me.TxtTriggerParam, "The trigger parameters. Provide settings for the trigger here, if needed.")
        '
        'lblTriggerParameters
        '
        Me.lblTriggerParameters.AutoSize = True
        Me.lblTriggerParameters.Location = New System.Drawing.Point(6, 43)
        Me.lblTriggerParameters.Name = "lblTriggerParameters"
        Me.lblTriggerParameters.Size = New System.Drawing.Size(62, 13)
        Me.lblTriggerParameters.TabIndex = 4
        Me.lblTriggerParameters.Text = lr("parameters:")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblActionHelp)
        Me.GroupBox2.Controls.Add(Me.lblActionParameters)
        Me.GroupBox2.Controls.Add(Me.TxtActionParam)
        Me.GroupBox2.Controls.Add(Me.CBActionType)
        Me.GroupBox2.Controls.Add(Me.lblActionType)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 185)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(362, 141)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = lr("Action")
        '
        'lblActionHelp
        '
        Me.lblActionHelp.AutoSize = True
        Me.lblActionHelp.Location = New System.Drawing.Point(5, 70)
        Me.lblActionHelp.MaximumSize = New System.Drawing.Size(350, 0)
        Me.lblActionHelp.Name = "lblActionHelp"
        Me.lblActionHelp.Size = New System.Drawing.Size(182, 13)
        Me.lblActionHelp.TabIndex = 7
        Me.lblActionHelp.Text = lr("Explanation: Please select a type first")
        '
        'lblActionParameters
        '
        Me.lblActionParameters.AutoSize = True
        Me.lblActionParameters.Location = New System.Drawing.Point(5, 50)
        Me.lblActionParameters.Name = "lblActionParameters"
        Me.lblActionParameters.Size = New System.Drawing.Size(62, 13)
        Me.lblActionParameters.TabIndex = 5
        Me.lblActionParameters.Text = lr("parameters:")
        '
        'TxtActionParam
        '
        Me.TxtActionParam.Location = New System.Drawing.Point(74, 47)
        Me.TxtActionParam.Name = "TxtActionParam"
        Me.TxtActionParam.Size = New System.Drawing.Size(187, 20)
        Me.TxtActionParam.TabIndex = 4
        Me.ToolTip.SetToolTip(Me.TxtActionParam, "The action parameters. Provide settings for the action here, if needed.")
        '
        'CBActionType
        '
        Me.CBActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBActionType.FormattingEnabled = True
        Me.CBActionType.Items.AddRange(New Object() {lr("execute"), lr("shellexecute"), lr("command"), lr("stop server"), lr("start server"), lr("restart server"), lr("restart server brute"), lr("backup"), lr("synchronize list"), lr("kickall"), lr("close gui")})
        Me.CBActionType.Location = New System.Drawing.Point(74, 19)
        Me.CBActionType.Name = "CBActionType"
        Me.CBActionType.Size = New System.Drawing.Size(187, 21)
        Me.CBActionType.TabIndex = 2
        '
        'lblActionType
        '
        Me.lblActionType.AutoSize = True
        Me.lblActionType.Location = New System.Drawing.Point(6, 22)
        Me.lblActionType.Name = "lblActionType"
        Me.lblActionType.Size = New System.Drawing.Size(34, 13)
        Me.lblActionType.TabIndex = 3
        Me.lblActionType.Text = lr("Type:")
        '
        'BtnSave
        '
        Me.BtnSave.Location = New System.Drawing.Point(302, 339)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnSave.TabIndex = 6
        Me.BtnSave.Text = lr("Save")
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(221, 339)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 7
        Me.BtnCancel.Text = lr("Cancel")
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'ErrProv
        '
        Me.ErrProv.ContainerControl = Me
        '
        'ChkEnable
        '
        Me.ChkEnable.AutoSize = True
        Me.ChkEnable.Checked = True
        Me.ChkEnable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkEnable.Location = New System.Drawing.Point(15, 345)
        Me.ChkEnable.Name = "ChkEnable"
        Me.ChkEnable.Size = New System.Drawing.Size(82, 17)
        Me.ChkEnable.TabIndex = 8
        Me.ChkEnable.Text = lr("Enable task")
        Me.ChkEnable.UseVisualStyleBackColor = True
        '
        'TaskDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(389, 374)
        Me.Controls.Add(Me.ChkEnable)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblTaskName)
        Me.Controls.Add(Me.TxtTaskName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TaskDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Edit task...")
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtTaskName As System.Windows.Forms.TextBox
    Friend WithEvents lblTaskName As System.Windows.Forms.Label
    Friend WithEvents CBTriggerType As System.Windows.Forms.ComboBox
    Friend WithEvents lblTriggerType As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtTriggerParam As System.Windows.Forms.TextBox
    Friend WithEvents lblTriggerParameters As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CBActionType As System.Windows.Forms.ComboBox
    Friend WithEvents lblActionType As System.Windows.Forms.Label
    Friend WithEvents lblActionParameters As System.Windows.Forms.Label
    Friend WithEvents TxtActionParam As System.Windows.Forms.TextBox
    Friend WithEvents lblTriggerHelp As System.Windows.Forms.Label
    Friend WithEvents lblActionHelp As System.Windows.Forms.Label
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents ErrProv As System.Windows.Forms.ErrorProvider
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ChkEnable As System.Windows.Forms.CheckBox
End Class

