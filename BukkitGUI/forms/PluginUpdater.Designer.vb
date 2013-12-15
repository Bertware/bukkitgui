Imports Net.Bertware.BukkitGUI.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PluginUpdater
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PluginUpdater))
        Me.ALVPlugins = New Net.Bertware.Controls.AdvancedListView()
        Me.ColPlugName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColPlugCurVer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColPlugNewVer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColPlugNewBukkit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Pbar = New Net.Bertware.Controls.VistaProgressBar()
        Me.CBBukkitBuild = New System.Windows.Forms.ComboBox()
        Me.BtnUpdate = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.ChkForce = New System.Windows.Forms.CheckBox()
        Me.LblBukkitBuild = New System.Windows.Forms.Label()
        Me.ChkCheckAll = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'ALVPlugins
        '
        Me.ALVPlugins.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ALVPlugins.CheckBoxes = True
        Me.ALVPlugins.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColPlugName, Me.ColPlugCurVer, Me.ColPlugNewVer, Me.ColPlugNewBukkit})
        Me.ALVPlugins.FullRowSelect = True
        Me.ALVPlugins.Location = New System.Drawing.Point(12, 49)
        Me.ALVPlugins.Name = "ALVPlugins"
        Me.ALVPlugins.Size = New System.Drawing.Size(591, 325)
        Me.ALVPlugins.TabIndex = 0
        Me.ALVPlugins.UseCompatibleStateImageBehavior = False
        Me.ALVPlugins.View = System.Windows.Forms.View.Details
        '
        'ColPlugName
        '
        Me.ColPlugName.Text = lr("Plugin")
        Me.ColPlugName.Width = 294
        '
        'ColPlugCurVer
        '
        Me.ColPlugCurVer.Text = lr("Current Version")
        Me.ColPlugCurVer.Width = 95
        '
        'ColPlugNewVer
        '
        Me.ColPlugNewVer.Text = lr("New version")
        Me.ColPlugNewVer.Width = 81
        '
        'ColPlugNewBukkit
        '
        Me.ColPlugNewBukkit.Text = lr("Bukkit Version")
        Me.ColPlugNewBukkit.Width = 111
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(9, 9)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(60, 13)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = lr("Status: Idle")
        '
        'Pbar
        '
        Me.Pbar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pbar.BackColor = System.Drawing.Color.Transparent
        Me.Pbar.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.None
        Me.Pbar.DisplayText = lr("")
        Me.Pbar.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Pbar.Location = New System.Drawing.Point(12, 28)
        Me.Pbar.Name = "Pbar"
        Me.Pbar.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
        Me.Pbar.Size = New System.Drawing.Size(591, 15)
        Me.Pbar.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Pbar.TabIndex = 2
        '
        'CBBukkitBuild
        '
        Me.CBBukkitBuild.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CBBukkitBuild.Enabled = False
        Me.CBBukkitBuild.FormattingEnabled = True
        Me.CBBukkitBuild.Location = New System.Drawing.Point(478, 380)
        Me.CBBukkitBuild.Name = "CBBukkitBuild"
        Me.CBBukkitBuild.Size = New System.Drawing.Size(121, 21)
        Me.CBBukkitBuild.TabIndex = 3
        Me.CBBukkitBuild.Visible = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdate.Location = New System.Drawing.Point(524, 411)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.BtnUpdate.TabIndex = 4
        Me.BtnUpdate.Text = lr("Update!")
        Me.BtnUpdate.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(443, 410)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(75, 23)
        Me.BtnClose.TabIndex = 5
        Me.BtnClose.Text = lr("Close")
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'ChkForce
        '
        Me.ChkForce.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ChkForce.AutoSize = True
        Me.ChkForce.Enabled = False
        Me.ChkForce.Location = New System.Drawing.Point(164, 382)
        Me.ChkForce.Name = "ChkForce"
        Me.ChkForce.Size = New System.Drawing.Size(185, 17)
        Me.ChkForce.TabIndex = 6
        Me.ChkForce.Text = lr("Force update if already up to date")
        Me.ChkForce.UseVisualStyleBackColor = True
        Me.ChkForce.Visible = False
        '
        'LblBukkitBuild
        '
        Me.LblBukkitBuild.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblBukkitBuild.AutoSize = True
        Me.LblBukkitBuild.Enabled = False
        Me.LblBukkitBuild.Location = New System.Drawing.Point(355, 383)
        Me.LblBukkitBuild.Name = "LblBukkitBuild"
        Me.LblBukkitBuild.Size = New System.Drawing.Size(117, 13)
        Me.LblBukkitBuild.TabIndex = 7
        Me.LblBukkitBuild.Text = lr("Update for bukkit build:")
        Me.LblBukkitBuild.Visible = False
        '
        'ChkCheckAll
        '
        Me.ChkCheckAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ChkCheckAll.AutoSize = True
        Me.ChkCheckAll.Location = New System.Drawing.Point(12, 379)
        Me.ChkCheckAll.Name = "ChkCheckAll"
        Me.ChkCheckAll.Size = New System.Drawing.Size(70, 17)
        Me.ChkCheckAll.TabIndex = 8
        Me.ChkCheckAll.Text = lr("Check all")
        Me.ChkCheckAll.UseVisualStyleBackColor = True
        '
        'PluginUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 446)
        Me.Controls.Add(Me.ChkCheckAll)
        Me.Controls.Add(Me.LblBukkitBuild)
        Me.Controls.Add(Me.ChkForce)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.CBBukkitBuild)
        Me.Controls.Add(Me.Pbar)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.ALVPlugins)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PluginUpdater"
        Me.Text = lr("Update plugins")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ALVPlugins As Net.Bertware.Controls.AdvancedListView
    Friend WithEvents ColPlugName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColPlugCurVer As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColPlugNewVer As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColPlugNewBukkit As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Pbar As Net.Bertware.Controls.VistaProgressBar
    Friend WithEvents CBBukkitBuild As System.Windows.Forms.ComboBox
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents ChkForce As System.Windows.Forms.CheckBox
    Friend WithEvents LblBukkitBuild As System.Windows.Forms.Label
    Friend WithEvents ChkCheckAll As System.Windows.Forms.CheckBox
End Class
