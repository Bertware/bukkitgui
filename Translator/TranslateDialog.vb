Public Class TranslateDialog
    Public Original As String, Translation As String


    Private Sub BtnOk_Click(sender As System.Object, e As System.EventArgs) Handles BtnOk.Click
        Original = TxtOriginal.Text
        Translation = TxtTranslation.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TxtOriginal.Text = Original
        TxtTranslation.Text = Translation
        TxtTranslation.Focus()
    End Sub

    Private Sub TxtValue_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtTranslation.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            Original = TxtOriginal.Text
            Translation = TxtTranslation.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

End Class