Public Class OutputBrowser
    Dim txtreference As Bertware.Controls.AdvancedRichTextBox

    Public Sub New(ByRef textbox As Bertware.Controls.AdvancedRichTextBox)
        InitializeComponent()
        SetTextbox(textbox.Rtf)
        txtreference = textbox
    End Sub

    Public Sub SetTextbox(ByVal rtf As String)
        Me.TxtOutput.Rtf = rtf
        Me.TxtOutput.ContextMenuStrip = RightClickMenu
        Me.TxtOutput.Refresh()
    End Sub

    Private Sub BtnCopy_Click(sender As Object, e As EventArgs) Handles BtnCopy.Click
        My.Computer.Clipboard.SetText(TxtOutput.SelectedRtf)
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        SetTextbox(txtreference.Rtf)
    End Sub
End Class