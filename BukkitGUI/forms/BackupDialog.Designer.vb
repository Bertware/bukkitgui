Imports Net.Bertware.BukkitGUI.Core
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BackupDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BackupDialog))
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnBrowseSourceFolders = New System.Windows.Forms.Button()
        Me.ChkCompression = New System.Windows.Forms.CheckBox()
        Me.TxtFolders = New System.Windows.Forms.TextBox()
        Me.TxtDestination = New System.Windows.Forms.TextBox()
        Me.BtnBrowseDestination = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ErrProv = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnOk
        '
        Me.BtnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOk.Location = New System.Drawing.Point(274, 175)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 0
        Me.BtnOk.Text = lr("Ok")
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Location = New System.Drawing.Point(193, 175)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = lr("Cancel")
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnBrowseSourceFolders
        '
        Me.BtnBrowseSourceFolders.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBrowseSourceFolders.Location = New System.Drawing.Point(274, 63)
        Me.BtnBrowseSourceFolders.Name = "BtnBrowseSourceFolders"
        Me.BtnBrowseSourceFolders.Size = New System.Drawing.Size(75, 23)
        Me.BtnBrowseSourceFolders.TabIndex = 2
        Me.BtnBrowseSourceFolders.Text = lr("Browse...")
        Me.BtnBrowseSourceFolders.UseVisualStyleBackColor = True
        '
        'ChkCompression
        '
        Me.ChkCompression.AutoSize = True
        Me.ChkCompression.Location = New System.Drawing.Point(12, 146)
        Me.ChkCompression.Name = "ChkCompression"
        Me.ChkCompression.Size = New System.Drawing.Size(216, 17)
        Me.ChkCompression.TabIndex = 4
        Me.ChkCompression.Text = lr("Compress backup to reduce disk usage.")
        Me.ChkCompression.UseVisualStyleBackColor = True
        '
        'TxtFolders
        '
        Me.TxtFolders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFolders.Location = New System.Drawing.Point(12, 65)
        Me.TxtFolders.Name = "TxtFolders"
        Me.TxtFolders.Size = New System.Drawing.Size(256, 20)
        Me.TxtFolders.TabIndex = 5
        '
        'TxtDestination
        '
        Me.TxtDestination.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtDestination.Location = New System.Drawing.Point(12, 120)
        Me.TxtDestination.Name = "TxtDestination"
        Me.TxtDestination.Size = New System.Drawing.Size(256, 20)
        Me.TxtDestination.TabIndex = 7
        '
        'BtnBrowseDestination
        '
        Me.BtnBrowseDestination.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBrowseDestination.Location = New System.Drawing.Point(274, 117)
        Me.BtnBrowseDestination.Name = "BtnBrowseDestination"
        Me.BtnBrowseDestination.Size = New System.Drawing.Size(75, 23)
        Me.BtnBrowseDestination.TabIndex = 6
        Me.BtnBrowseDestination.Text = lr("Browse...")
        Me.BtnBrowseDestination.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = lr("Folders to backup, separated by ;")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = lr("Backup name:")
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(94, 12)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(255, 20)
        Me.TxtName.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = lr("Folder to store backup:")
        '
        'ErrProv
        '
        Me.ErrProv.ContainerControl = Me
        '
        'BackupDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 210)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDestination)
        Me.Controls.Add(Me.BtnBrowseDestination)
        Me.Controls.Add(Me.TxtFolders)
        Me.Controls.Add(Me.ChkCompression)
        Me.Controls.Add(Me.BtnBrowseSourceFolders)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BackupDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = lr("Backup settings")
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnBrowseSourceFolders As System.Windows.Forms.Button
    Friend WithEvents ChkCompression As System.Windows.Forms.CheckBox
    Friend WithEvents TxtFolders As System.Windows.Forms.TextBox
    Friend WithEvents TxtDestination As System.Windows.Forms.TextBox
    Friend WithEvents BtnBrowseDestination As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ErrProv As System.Windows.Forms.ErrorProvider
End Class
