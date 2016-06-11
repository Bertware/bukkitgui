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

Public Class LicenseAcceptDialog
    Public License_url As String
    Public License_name As String

    Public Sub New(name As String, url As String)
        InitializeComponent()
        Me.License_name = name
        Me.License_url = url
    End Sub

    Private Sub LicenseAcceptDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = lr("Accept") & " " & License_name
        ChkAccept.Text = lr("I have read and accept the") & " " & License_name
        WebLicense.Navigate(License_url)
    End Sub

    Private Sub ChkAccept_CheckedChanged(sender As Object, e As EventArgs) _
        Handles ChkAccept.CheckedChanged
        If ChkAccept.Checked Then btnOk.Enabled = True
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
