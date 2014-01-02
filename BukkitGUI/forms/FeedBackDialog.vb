Imports Net.Bertware.BukkitGUI.Core

Public Class FeedBackDialog
    Private Sub BtnSend_Click() Handles BtnSend.Click
        Dim sender As String = TxtMail.Text
        If TxtMail.Text = "" Or TxtMail.Text.Contains("@") = False Or TxtMail.Text.Contains(".") = False Then _
            sender = "no-reply@bertware.net"
        If TxtMessage.Text.Trim.Length < 2 Then _
            MessageBox.Show(lr("You cannot send empty messages!"), lr("Can't send"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error) : Me.Close() : Exit Sub
        serverinteraction.Contact(sender, "BukkitGUI v" & My.Application.Info.Version.ToString & " Feedback",
                                  TxtMessage.Text)
        Me.Close()
    End Sub
End Class