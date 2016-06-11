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

Imports System.Threading
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.MCInterop

Public Class ServerStopDialog
    Private Sub ServerStopDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If running = True Then StopServer() Else Me.Close() : Exit Sub
        AddHandler ServerStopped, AddressOf SafeFormClose
    End Sub

    Private Sub SafeFormClose()
        Try
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf SafeFormClose)
                Me.Invoke(d, New Object())
            Else
                Me.Close()
            End If
        Catch ex As Exception
            Log(livebug.loggingLevel.Warning, "ServerStopDialog", "Error in SafeFormClose!", ex.Message)
        End Try
    End Sub

    Private Sub BtnKill_Click(sender As Object, e As EventArgs) Handles BtnKill.Click
        Try
            host.Kill()
        Catch ex As Exception
            MessageBox.Show("Couldn't kill server", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class