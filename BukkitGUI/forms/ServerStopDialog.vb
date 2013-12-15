Imports Net.Bertware.BukkitGUI.MCInterop
Imports System.Threading

Public Class ServerStopDialog

    Private Sub ServerStopDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If server.running = True Then StopServer() Else Me.Close() : Exit Sub
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
            Core.livebug.write(Core.loggingLevel.Warning, "ServerStopDialog", "Error in SafeFormClose!", ex.Message)
        End Try
    End Sub

    Private Sub BtnKill_Click(sender As System.Object, e As System.EventArgs) Handles BtnKill.Click
        Try
            server.host.Kill()
        Catch ex As Exception
            MessageBox.Show("Couldn't kill server", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class