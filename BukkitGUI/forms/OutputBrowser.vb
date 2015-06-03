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

Imports Net.Bertware.Controls

Public Class OutputBrowser
    Dim txtreference As AdvancedRichTextBox

    Public Sub New(ByRef textbox As AdvancedRichTextBox)
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