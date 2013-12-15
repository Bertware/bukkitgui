Imports Net.Bertware.BukkitGUI.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InstalledPluginDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InstalledPluginDialog))
        Me.LblPath = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.LblAuthors = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblNamespace = New System.Windows.Forms.Label()
        Me.lblSoftdepend = New System.Windows.Forms.Label()
        Me.ALVCommands = New Net.Bertware.Controls.AdvancedListView()
        Me.ColCommand = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColCommandDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColUsage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColAliases = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ALVPermissions = New Net.Bertware.Controls.AdvancedListView()
        Me.ColPermission = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColPermDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnLatestVersion = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LblPath
        '
        Me.LblPath.AutoSize = True
        Me.LblPath.Location = New System.Drawing.Point(12, 24)
        Me.LblPath.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.LblPath.Name = "LblPath"
        Me.LblPath.Size = New System.Drawing.Size(32, 13)
        Me.LblPath.TabIndex = 0
        Me.LblPath.Text = lr("Path:")
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(13, 54)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(45, 13)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = lr("Version:")
        '
        'LblAuthors
        '
        Me.LblAuthors.AutoSize = True
        Me.LblAuthors.Location = New System.Drawing.Point(12, 39)
        Me.LblAuthors.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.LblAuthors.Name = "LblAuthors"
        Me.LblAuthors.Size = New System.Drawing.Size(52, 13)
        Me.LblAuthors.TabIndex = 2
        Me.LblAuthors.Text = lr("Author(s):")
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 9)
        Me.lblName.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(38, 13)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = lr("Name:")
        '
        'lblNamespace
        '
        Me.lblNamespace.AutoSize = True
        Me.lblNamespace.Location = New System.Drawing.Point(13, 84)
        Me.lblNamespace.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblNamespace.Name = "lblNamespace"
        Me.lblNamespace.Size = New System.Drawing.Size(91, 13)
        Me.lblNamespace.TabIndex = 4
        Me.lblNamespace.Text = lr("Main namespace:")
        '
        'lblSoftdepend
        '
        Me.lblSoftdepend.AutoSize = True
        Me.lblSoftdepend.Location = New System.Drawing.Point(13, 69)
        Me.lblSoftdepend.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.lblSoftdepend.Name = "lblSoftdepend"
        Me.lblSoftdepend.Size = New System.Drawing.Size(65, 13)
        Me.lblSoftdepend.TabIndex = 5
        Me.lblSoftdepend.Text = lr("Softdepend:")
        '
        'ALVCommands
        '
        Me.ALVCommands.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColCommand, Me.ColCommandDescription, Me.ColUsage, Me.ColAliases})
        Me.ALVCommands.FullRowSelect = True
        Me.ALVCommands.Location = New System.Drawing.Point(16, 101)
        Me.ALVCommands.Name = "ALVCommands"
        Me.ALVCommands.Size = New System.Drawing.Size(750, 200)
        Me.ALVCommands.TabIndex = 7
        Me.ALVCommands.UseCompatibleStateImageBehavior = False
        Me.ALVCommands.View = System.Windows.Forms.View.Details
        '
        'ColCommand
        '
        Me.ColCommand.Text = lr("Command")
        Me.ColCommand.Width = 146
        '
        'ColCommandDescription
        '
        Me.ColCommandDescription.Text = lr("Description")
        Me.ColCommandDescription.Width = 343
        '
        'ColUsage
        '
        Me.ColUsage.Text = lr("Usage")
        Me.ColUsage.Width = 149
        '
        'ColAliases
        '
        Me.ColAliases.Text = lr("Aliases")
        Me.ColAliases.Width = 100
        '
        'ALVPermissions
        '
        Me.ALVPermissions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColPermission, Me.ColPermDescription})
        Me.ALVPermissions.FullRowSelect = True
        Me.ALVPermissions.Location = New System.Drawing.Point(16, 307)
        Me.ALVPermissions.Name = "ALVPermissions"
        Me.ALVPermissions.ShowItemToolTips = True
        Me.ALVPermissions.Size = New System.Drawing.Size(750, 200)
        Me.ALVPermissions.TabIndex = 6
        Me.ALVPermissions.UseCompatibleStateImageBehavior = False
        Me.ALVPermissions.View = System.Windows.Forms.View.Details
        '
        'ColPermission
        '
        Me.ColPermission.Text = lr("Permission")
        Me.ColPermission.Width = 132
        '
        'ColPermDescription
        '
        Me.ColPermDescription.Text = lr("Description")
        Me.ColPermDescription.Width = 408
        '
        'btnLatestVersion
        '
        Me.btnLatestVersion.Location = New System.Drawing.Point(646, 4)
        Me.btnLatestVersion.Name = "btnLatestVersion"
        Me.btnLatestVersion.Size = New System.Drawing.Size(120, 23)
        Me.btnLatestVersion.TabIndex = 8
        Me.btnLatestVersion.Text = lr("View latest version")
        Me.btnLatestVersion.UseVisualStyleBackColor = True
        '
        'InstalledPluginDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 518)
        Me.Controls.Add(Me.btnLatestVersion)
        Me.Controls.Add(Me.ALVCommands)
        Me.Controls.Add(Me.ALVPermissions)
        Me.Controls.Add(Me.lblSoftdepend)
        Me.Controls.Add(Me.lblNamespace)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.LblAuthors)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.LblPath)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InstalledPluginDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Plugin details:")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblPath As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents LblAuthors As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblNamespace As System.Windows.Forms.Label
    Friend WithEvents lblSoftdepend As System.Windows.Forms.Label
    Friend WithEvents ALVPermissions As Net.Bertware.Controls.AdvancedListView
    Friend WithEvents ColPermission As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColPermDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents ALVCommands As Net.Bertware.Controls.AdvancedListView
    Friend WithEvents ColCommand As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColCommandDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColUsage As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColAliases As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnLatestVersion As System.Windows.Forms.Button
End Class
