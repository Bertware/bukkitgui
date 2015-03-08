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