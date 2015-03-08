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
