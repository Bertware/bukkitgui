Imports Net.Bertware.BukkitGUI.Core

Namespace forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class LanguageInstaller
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LanguageInstaller))
            Me.BtnInstall = New System.Windows.Forms.Button()
            Me.BtnCancel = New System.Windows.Forms.Button()
            Me.ALVLanguages = New Net.Bertware.Controls.AdvancedListView()
            Me.ColLanguage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColTranslator = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColComment = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.SuspendLayout()
            '
            'BtnInstall
            '
            Me.BtnInstall.Location = New System.Drawing.Point(536, 346)
            Me.BtnInstall.Name = "BtnInstall"
            Me.BtnInstall.Size = New System.Drawing.Size(75, 23)
            Me.BtnInstall.TabIndex = 1
            Me.BtnInstall.Text = lr("Install")
            Me.BtnInstall.UseVisualStyleBackColor = True
            '
            'BtnCancel
            '
            Me.BtnCancel.Location = New System.Drawing.Point(455, 346)
            Me.BtnCancel.Name = "BtnCancel"
            Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
            Me.BtnCancel.TabIndex = 2
            Me.BtnCancel.Text = lr("Cancel")
            Me.BtnCancel.UseVisualStyleBackColor = True
            '
            'ALVLanguages
            '
            Me.ALVLanguages.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColLanguage, Me.ColVersion, Me.ColTranslator, Me.ColComment})
            Me.ALVLanguages.FullRowSelect = True
            Me.ALVLanguages.Location = New System.Drawing.Point(12, 12)
            Me.ALVLanguages.Name = "ALVLanguages"
            Me.ALVLanguages.Size = New System.Drawing.Size(599, 328)
            Me.ALVLanguages.TabIndex = 3
            Me.ALVLanguages.UseCompatibleStateImageBehavior = False
            Me.ALVLanguages.View = System.Windows.Forms.View.Details
            '
            'ColLanguage
            '
            Me.ColLanguage.Text = lr("Language")
            Me.ColLanguage.Width = 134
            '
            'ColVersion
            '
            Me.ColVersion.Text = lr("GUI Version")
            Me.ColVersion.Width = 108
            '
            'ColTranslator
            '
            Me.ColTranslator.Text = lr("Translator")
            Me.ColTranslator.Width = 123
            '
            'ColComment
            '
            Me.ColComment.Text = lr("Comment")
            Me.ColComment.Width = 140
            '
            'LanguageInstaller
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(623, 381)
            Me.Controls.Add(Me.ALVLanguages)
            Me.Controls.Add(Me.BtnCancel)
            Me.Controls.Add(Me.BtnInstall)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "LanguageInstaller"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = lr("Install languages....")
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BtnInstall As System.Windows.Forms.Button
        Friend WithEvents BtnCancel As System.Windows.Forms.Button
        Friend WithEvents ALVLanguages As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColLanguage As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColVersion As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColTranslator As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColComment As System.Windows.Forms.ColumnHeader
    End Class
End Namespace