'============================================='''
'
' This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
' If a copy of the MPL was not distributed with this file,
' you can obtain one at http://mozilla.org/MPL/2.0/.
' 
' Source and compiled files may only be redistributed if they comply with
' the mozilla MPL2 license, and may not be monetized in any way,
' including but not limited to selling the software or distributing it through ad-sponsored channels.
'
' ©Bertware, visit http://bertware.net
'
'============================================='''

Imports Net.Bertware.BukkitGUI.Core

Public Class FeedBackDialog
    Private Sub BtnSend_Click() Handles BtnSend.Click
        Dim sender As String = TxtMail.Text
        If TxtMail.Text = "" Or TxtMail.Text.Contains("@") = False Or TxtMail.Text.Contains(".") = False Then _
            sender = "no-reply@bertware.net"
        If TxtMessage.Text.Trim.Length < 2 Then _
            MessageBox.Show(lr("You cannot send empty messages!"), lr("Can't send"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error) : Me.Close() : Exit Sub
        Contact(sender, "BukkitGUI v" & My.Application.Info.Version.ToString & " Feedback",
                TxtMessage.Text)
        Me.Close()
    End Sub
End Class