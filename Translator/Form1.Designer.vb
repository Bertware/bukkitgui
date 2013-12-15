<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TxtPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnBrowse = New System.Windows.Forms.Button()
        Me.ALVItems = New Translator.AdvancedListView()
        Me.ColID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColDefault = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColTranslation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'TxtPath
        '
        Me.TxtPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPath.Location = New System.Drawing.Point(41, 15)
        Me.TxtPath.Name = "TxtPath"
        Me.TxtPath.Size = New System.Drawing.Size(822, 20)
        Me.TxtPath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "File:"
        '
        'BtnBrowse
        '
        Me.BtnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBrowse.Location = New System.Drawing.Point(869, 13)
        Me.BtnBrowse.Name = "BtnBrowse"
        Me.BtnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.BtnBrowse.TabIndex = 3
        Me.BtnBrowse.Text = "Browse..."
        Me.BtnBrowse.UseVisualStyleBackColor = True
        '
        'ALVItems
        '
        Me.ALVItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ALVItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColID, Me.ColDefault, Me.ColTranslation})
        Me.ALVItems.FullRowSelect = True
        Me.ALVItems.Location = New System.Drawing.Point(12, 41)
        Me.ALVItems.Name = "ALVItems"
        Me.ALVItems.Size = New System.Drawing.Size(932, 540)
        Me.ALVItems.TabIndex = 5
        Me.ALVItems.UseCompatibleStateImageBehavior = False
        Me.ALVItems.View = System.Windows.Forms.View.Details
        '
        'ColID
        '
        Me.ColID.Text = "ID"
        '
        'ColDefault
        '
        Me.ColDefault.Text = "Default"
        Me.ColDefault.Width = 440
        '
        'ColTranslation
        '
        Me.ColTranslation.Text = "Translation"
        Me.ColTranslation.Width = 440
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 593)
        Me.Controls.Add(Me.ALVItems)
        Me.Controls.Add(Me.BtnBrowse)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtPath)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "BukkitGUI translator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnBrowse As System.Windows.Forms.Button
    Friend WithEvents ALVItems As Translator.AdvancedListView
    Friend WithEvents ColDefault As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColTranslation As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColID As System.Windows.Forms.ColumnHeader

End Class
