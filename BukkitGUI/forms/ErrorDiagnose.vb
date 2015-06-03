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
Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.BukkitGUI.Utilities

Public Class ErrorDiagnose
    Private type As serverOutputHandler.MessageType, time As String, cause As ErrorCause

    Public Sub New(type As MessageType, time As String, cause As ErrorCause)
        Me.type = type
        Me.time = time
        Me.cause = cause
        InitializeComponent()
    End Sub

    Private Sub ErrorDiagnose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If cause IsNot Nothing AndAlso cause.description IsNot Nothing Then LblCause.Text += " " & cause.description
            If cause IsNot Nothing AndAlso cause.text IsNot Nothing Then lblText.Text += " " & cause.text
            If time IsNot Nothing Then lblTime.Text += " " & time
            lblWarningType.Text += " " & type.ToString

            Select Case cause.type
                Case ErrorCause.ErrorCauseType.plugin
                    If CType(cause, ErrorCause_Plugin).plugin Is Nothing Then Exit Sub
                    Dim lvi As New ListViewItem({"Update plugin:" & CType(cause, ErrorCause_Plugin).plugin.name})
                    lvi.Tag = "plugin:update:" & CType(cause, ErrorCause_Plugin).plugin.name
                    LVsolutions.Items.Add(lvi)
                    lvi = New ListViewItem({"Remove plugin:" & CType(cause, ErrorCause_Plugin).plugin.name})
                    lvi.Tag = "plugin:remove:" & CType(cause, ErrorCause_Plugin).plugin.name
                    LVsolutions.Items.Add(lvi)
                Case ErrorCause.ErrorCauseType.setting
                    Dim _
                        lvi As _
                            New ListViewItem(
                                {"Change value for setting " & CType(cause, ErrorCause_setting).setting & " to " &
                                 CType(cause, ErrorCause_setting).Fixvalue})
                    lvi.Tag = "setting:set:" & CType(cause, ErrorCause_setting).setting & ":" &
                              CType(cause, ErrorCause_setting).Fixvalue
                    LVsolutions.Items.Add(lvi)
                Case ErrorCause.ErrorCauseType.other
                    Dim lvi As New ListViewItem({"This problem can't be solved by the error solver."})
                    lvi.Tag = "unknown:unknown"
                    LVsolutions.Items.Add(lvi)
            End Select

        Catch ex As Exception
            Log(livebug.loggingLevel.Severe, "ErrorDiagnose",
                "Severe Exception while loading error analyzer dialog", ex.Message)
        End Try
    End Sub

    Private Sub BtnApply_Click(sender As Object, e As EventArgs) Handles BtnApply.Click
        Try
            If LVsolutions.SelectedItems Is Nothing OrElse LVsolutions.SelectedItems.Count < 1 Then Exit Sub
            Dim solution As String = LVsolutions.SelectedItems(0).Tag
            If solution.StartsWith("plugin:update") Then
                Dim pld As plugindescriptor = GetInstalledPluginByName(solution.Split(":")(2).Trim(":"))
                If pld IsNot Nothing Then
                    Dim d As New PluginUpdater(pld)
                    d.UpdateOnLoad = True
                    d.ShowDialog()
                Else
                    MessageBox.Show(lr("Couldn't find plugin!"), lr("Failed"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
                End If

            ElseIf solution.StartsWith("plugin:remove") Then
                Dim pld As plugindescriptor = GetInstalledPluginByName(solution.Split(":")(2).Trim(":"))
                If pld IsNot Nothing Then RemoveInstalledplugin(pld) Else _
                    MessageBox.Show(lr("Couldn't find plugin!"), lr("Failed"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
            ElseIf solution.StartsWith("setting:set") Then
                SetSetting(solution.Split(":")(2).Trim(":"), solution.Split(":")(3).Trim(":"))
            Else
                MessageBox.Show(lr("This isn't a valid solution."), lr("Invalid solution"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            Log(loggingLevel.Severe, "ErrorDiagnose", "Severe Exception while aplying fix for error(s)",
                ex.Message)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class