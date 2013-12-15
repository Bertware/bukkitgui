Public Class ServerSettingDialog
    Public setting As String, value As String
    Public Property NameReadOnly As Boolean
        Get
            Return TxtSetting.ReadOnly
        End Get
        Set(value As Boolean)
            TxtSetting.ReadOnly = value
        End Set
    End Property

    Private Sub BtnOk_Click(sender As System.Object, e As System.EventArgs) Handles BtnOk.Click
        setting = TxtSetting.Text
        value = TxtValue.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TxtSetting.Text = setting
        TxtValue.Text = value
        TxtValue.Focus()
    End Sub

    Private Sub TxtValue_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtValue.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            setting = TxtSetting.Text
            value = TxtValue.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

End Class