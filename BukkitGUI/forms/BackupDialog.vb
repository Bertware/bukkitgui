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
Imports Net.Bertware.BukkitGUI.Utilities

Public Class BackupDialog
    Dim bs As BackupSetting

    Public ReadOnly Property backup As BackupSetting
        Get
            Return bs
        End Get
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(backup As BackupSetting)
        InitializeComponent()
        bs = backup
    End Sub

    Private Sub BackupDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bs IsNot Nothing Then
            If bs.name IsNot Nothing Then TxtName.Text = bs.name
            If bs.folders IsNot Nothing AndAlso bs.folders.Count > 0 Then _
                TxtFolders.Text = Serialize(bs.folders, ";")
            If bs.destination IsNot Nothing Then TxtDestination.Text = bs.destination
            ChkCompression.Checked = bs.compression
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        bs = New BackupSetting
        bs.name = TxtName.Text
        bs.folders = New List(Of String)
        For Each Dir As String In TxtFolders.Text.Split(";")
            bs.folders.Add(Dir)
        Next
        bs.destination = TxtDestination.Text
        bs.compression = ChkCompression.Checked
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnBrowseSourceFolders_Click(sender As Object, e As EventArgs) _
        Handles BtnBrowseSourceFolders.Click
        Dim fb As New FolderBrowserDialog
        fb.Description = lr("Select folders to backup")
        If fb.ShowDialog() <> DialogResult.OK Then Exit Sub
        If TxtFolders.Text = "" Then TxtFolders.Text = fb.SelectedPath Else TxtFolders.Text += ";" & fb.SelectedPath
    End Sub

    Private Sub BtnBrowseDestination_Click(sender As Object, e As EventArgs) _
        Handles BtnBrowseDestination.Click
        Dim fb As New FolderBrowserDialog
        fb.Description = lr("Select folder to store backup")
        If fb.ShowDialog() <> DialogResult.OK Then Exit Sub
        TxtDestination.Text = fb.SelectedPath
    End Sub

    Private Sub settings_validate() _
        Handles TxtName.LostFocus, TxtDestination.LostFocus, TxtFolders.LostFocus, TxtName.TextChanged,
                TxtDestination.TextChanged, TxtFolders.TextChanged
        BtnOk.Enabled = False
        If TxtName.Text = "" Then ErrProv.SetError(TxtName, lr("The name must be at least 1 character long")) : Exit Sub
        For Each character In TxtName.Text.ToCharArray
            If Not (Char.IsLetterOrDigit(character) Or character = Char.Parse("_") Or character = Char.Parse("-")) Then _
                ErrProv.SetError(TxtName, lr("You can only use the following characters: a-z ; 1-9 ; _ ; -")) : Exit Sub
        Next
        ErrProv.SetError(TxtName, "")
        If TxtFolders.Text = "" Then _
            ErrProv.SetError(TxtFolders, lr("You need to specify at least 1 directory")) : Exit Sub Else _
            ErrProv.SetError(TxtFolders, "")
        If TxtDestination.Text = "" Then _
            ErrProv.SetError(TxtDestination, lr("You need to specify the destination directory")) : Exit Sub Else _
            ErrProv.SetError(TxtDestination, "")

        BtnOk.Enabled = True
    End Sub
End Class