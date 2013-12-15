Imports Net.Bertware.BukkitGUI.Core
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BukgetPluginDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BukgetPluginDialog))
        Me.LVVersions = New Net.Bertware.Controls.AdvancedListView()
        Me.ColDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColCBversion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblAuthors = New System.Windows.Forms.Label()
        Me.ALlblWebsite = New Net.Bertware.Controls.AdvancedLinkLabel()
        Me.lblcategories = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnInstall = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LVVersions
        '
        Me.LVVersions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LVVersions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColDate, Me.ColVersion, Me.ColFile, Me.ColCBversion, Me.ColType})
        Me.LVVersions.FullRowSelect = True
        Me.LVVersions.Location = New System.Drawing.Point(12, 127)
        Me.LVVersions.MultiSelect = False
        Me.LVVersions.Name = "LVVersions"
        Me.LVVersions.Size = New System.Drawing.Size(567, 153)
        Me.LVVersions.TabIndex = 0
        Me.LVVersions.UseCompatibleStateImageBehavior = False
        Me.LVVersions.View = System.Windows.Forms.View.Details
        '
        'ColDate
        '
        Me.ColDate.Text = lr("Date")
        Me.ColDate.Width = 120
        '
        'ColVersion
        '
        Me.ColVersion.Text = lr("Version")
        Me.ColVersion.Width = 100
        '
        'ColFile
        '
        Me.ColFile.Text = lr("File")
        Me.ColFile.Width = 180
        '
        'ColCBversion
        '
        Me.ColCBversion.Text = lr("Craftbukkit")
        Me.ColCBversion.Width = 80
        '
        'ColType
        '
        Me.ColType.Text = lr("Type")
        Me.ColType.Width = 75
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 13)
        Me.lblName.Margin = New System.Windows.Forms.Padding(3)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(38, 13)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = lr("Name:")
        '
        'lblAuthors
        '
        Me.lblAuthors.AutoSize = True
        Me.lblAuthors.Location = New System.Drawing.Point(12, 51)
        Me.lblAuthors.Margin = New System.Windows.Forms.Padding(3)
        Me.lblAuthors.Name = "lblAuthors"
        Me.lblAuthors.Size = New System.Drawing.Size(46, 13)
        Me.lblAuthors.TabIndex = 2
        Me.lblAuthors.Text = lr("Authors:")
        '
        'ALlblWebsite
        '
        Me.ALlblWebsite.AutoSize = True
        Me.ALlblWebsite.Location = New System.Drawing.Point(12, 108)
        Me.ALlblWebsite.Margin = New System.Windows.Forms.Padding(3)
        Me.ALlblWebsite.Name = "ALlblWebsite"
        Me.ALlblWebsite.Size = New System.Drawing.Size(28, 13)
        Me.ALlblWebsite.TabIndex = 3
        Me.ALlblWebsite.TabStop = True
        Me.ALlblWebsite.Text = lr("Site:")
        '
        'lblcategories
        '
        Me.lblcategories.AutoSize = True
        Me.lblcategories.Location = New System.Drawing.Point(12, 70)
        Me.lblcategories.Margin = New System.Windows.Forms.Padding(3)
        Me.lblcategories.Name = "lblcategories"
        Me.lblcategories.Size = New System.Drawing.Size(60, 13)
        Me.lblcategories.TabIndex = 4
        Me.lblcategories.Text = lr("Categories:")
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(12, 89)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(40, 13)
        Me.lblStatus.TabIndex = 5
        Me.lblStatus.Text = lr("Status:")
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(504, 286)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(75, 23)
        Me.BtnClose.TabIndex = 6
        Me.BtnClose.Text = lr("Close")
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnInstall
        '
        Me.BtnInstall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnInstall.Location = New System.Drawing.Point(358, 286)
        Me.BtnInstall.Name = "BtnInstall"
        Me.BtnInstall.Size = New System.Drawing.Size(140, 23)
        Me.BtnInstall.TabIndex = 7
        Me.BtnInstall.Text = lr("Install Selected Version")
        Me.BtnInstall.UseVisualStyleBackColor = True
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(12, 32)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(3)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 13)
        Me.lblDescription.TabIndex = 8
        Me.lblDescription.Text = lr("Description:")
        '
        'BukgetPluginDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 321)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.BtnInstall)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblcategories)
        Me.Controls.Add(Me.ALlblWebsite)
        Me.Controls.Add(Me.lblAuthors)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.LVVersions)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BukgetPluginDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Plugin information")
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ColDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColCBversion As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblAuthors As System.Windows.Forms.Label
    Friend WithEvents ALlblWebsite As Net.Bertware.Controls.AdvancedLinkLabel
    Friend WithEvents lblcategories As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnInstall As System.Windows.Forms.Button
    Friend WithEvents ColFile As System.Windows.Forms.ColumnHeader
    Friend WithEvents LVVersions As Net.Bertware.Controls.AdvancedListView
    Friend WithEvents ColType As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblDescription As System.Windows.Forms.Label
End Class
