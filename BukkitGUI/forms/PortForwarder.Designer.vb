Imports Net.Bertware.BukkitGUI.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PortForwarder
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
        Me.ALVMapping = New Net.Bertware.Controls.AdvancedListView()
        Me.ColName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColIP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColPort = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColProtocol = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtIp = New System.Windows.Forms.TextBox()
        Me.NumPort = New System.Windows.Forms.NumericUpDown()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnRefresh = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CBProtocol = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        CType(Me.NumPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ALVMapping
        '
        Me.ALVMapping.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ALVMapping.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColName, Me.ColIP, Me.ColPort, Me.ColProtocol})
        Me.ALVMapping.Location = New System.Drawing.Point(12, 12)
        Me.ALVMapping.Name = "ALVMapping"
        Me.ALVMapping.Size = New System.Drawing.Size(635, 326)
        Me.ALVMapping.TabIndex = 0
        Me.ALVMapping.UseCompatibleStateImageBehavior = False
        Me.ALVMapping.View = System.Windows.Forms.View.Details
        '
        'ColName
        '
        Me.ColName.Text = Lr("Description")
        Me.ColName.Width = 240
        '
        'ColIP
        '
        Me.ColIP.Text = Lr("IP")
        Me.ColIP.Width = 120
        '
        'ColPort
        '
        Me.ColPort.Text = Lr("Port")
        Me.ColPort.Width = 120
        '
        'ColProtocol
        '
        Me.ColProtocol.Text = Lr("Protocol")
        '
        'TxtName
        '
        Me.TxtName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtName.Location = New System.Drawing.Point(81, 346)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(100, 20)
        Me.TxtName.TabIndex = 1
        Me.TxtName.Text = Lr("Minecraft-server")
        '
        'TxtIp
        '
        Me.TxtIp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtIp.Location = New System.Drawing.Point(213, 346)
        Me.TxtIp.Name = "TxtIp"
        Me.TxtIp.Size = New System.Drawing.Size(100, 20)
        Me.TxtIp.TabIndex = 2
        '
        'NumPort
        '
        Me.NumPort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumPort.Location = New System.Drawing.Point(354, 347)
        Me.NumPort.Maximum = New Decimal(New Integer() {65532, 0, 0, 0})
        Me.NumPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumPort.Name = "NumPort"
        Me.NumPort.Size = New System.Drawing.Size(76, 20)
        Me.NumPort.TabIndex = 3
        Me.NumPort.Value = New Decimal(New Integer() {25565, 0, 0, 0})
        '
        'BtnAdd
        '
        Me.BtnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnAdd.Location = New System.Drawing.Point(491, 344)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(75, 23)
        Me.BtnAdd.TabIndex = 4
        Me.BtnAdd.Text = Lr("Add")
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRefresh.Location = New System.Drawing.Point(572, 344)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.BtnRefresh.TabIndex = 5
        Me.BtnRefresh.Text = Lr("Refresh")
        Me.BtnRefresh.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(572, 373)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(75, 23)
        Me.BtnClose.TabIndex = 6
        Me.BtnClose.Text = Lr("Close")
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(319, 349)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = Lr("Port:")
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 349)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = Lr("Description:")
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(187, 349)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = Lr("IP:")
        '
        'CBProtocol
        '
        Me.CBProtocol.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CBProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBProtocol.FormattingEnabled = True
        Me.CBProtocol.Items.AddRange(New Object() {"tcp", "udp"})
        Me.CBProtocol.Location = New System.Drawing.Point(436, 346)
        Me.CBProtocol.Name = "CBProtocol"
        Me.CBProtocol.Size = New System.Drawing.Size(49, 21)
        Me.CBProtocol.TabIndex = 10
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(12, 386)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(24, 13)
        Me.lblStatus.TabIndex = 12
        Me.lblStatus.Text = Lr("Idle")
        '
        'PortForwarder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(659, 408)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.CBProtocol)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnRefresh)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.NumPort)
        Me.Controls.Add(Me.TxtIp)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.ALVMapping)
        Me.Name = "PortForwarder"
        Me.ShowIcon = False
        Me.Text = Lr("Port forwarding ( EXPERIMENTAL ! )")
        CType(Me.NumPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ALVMapping As Net.Bertware.Controls.AdvancedListView
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtIp As System.Windows.Forms.TextBox
    Friend WithEvents NumPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents ColName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColIP As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColPort As System.Windows.Forms.ColumnHeader
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ColProtocol As System.Windows.Forms.ColumnHeader
    Friend WithEvents CBProtocol As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
