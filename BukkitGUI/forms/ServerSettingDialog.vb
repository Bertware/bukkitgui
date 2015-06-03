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

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        setting = TxtSetting.Text
        value = TxtValue.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtSetting.Text = setting
        TxtValue.Text = value
        TxtValue.Focus()
    End Sub

    Private Sub TxtValue_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtValue.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            setting = TxtSetting.Text
            value = TxtValue.Text
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class