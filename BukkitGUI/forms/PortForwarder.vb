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

Imports System.Text.RegularExpressions
Imports System.Threading
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities

Public Class PortForwarder
    Public Event MappingUpdateReceived(mapping As List(Of PortMappingEntry))
    Public Event PortForwardApplied()
    Public LastMapping As UPnP

    Private Sub PortForwarder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not available() Then
            MessageBox.Show(
                lr("Port forwarding isn't available") & vbCrLf & lr("Your network device (router) doesn't support UPnP"),
                lr("Port forwarding unavailable"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If

        TxtIp.Text = UPnP.LocalIP
        CBProtocol.SelectedIndex = 0

        UpdateMappingAsync()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub UpdateMappingAsync() Handles BtnRefresh.Click, Me.PortForwardApplied
        If Me.InvokeRequired Then
            Dim c As New ContextCallback(AddressOf UpdateMappingAsync)
            Me.Invoke(c, New Object())
        Else
            Me.BtnAdd.Enabled = False
            Me.BtnRefresh.Enabled = False
            Dim t As New Thread(AddressOf Me.GetMaps)
            t.Start()
            Me.Cursor = Cursors.WaitCursor
            Me.lblStatus.Text = "Loading info. This could take a while..."
        End If
    End Sub

    Private Sub Displaymapping(mapping As List(Of PortMappingEntry)) Handles Me.MappingUpdateReceived
        If Me.InvokeRequired Then
            Dim c As New ContextCallback(AddressOf Displaymapping)
            Me.Invoke(c, New Object() {mapping})
        Else
            ALVMapping.Items.Clear()
            For Each entry As PortMappingEntry In mapping
                Dim lvi As New ListViewItem({entry.Name, entry.Ip, entry.Port, entry.Protocol.ToString})
                lvi.Tag = entry.Port
                ALVMapping.Items.Add(lvi)
            Next
            Me.BtnAdd.Enabled = True
            Me.BtnRefresh.Enabled = True
            Me.Cursor = Cursors.Default
            Me.lblStatus.Text = "idle"
        End If
    End Sub


    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim protocol As UPnP.Protocol
        If CBProtocol.SelectedIndex = 1 Then protocol = UPnP.Protocol.UDP Else protocol = UPnP.Protocol.TCP
        If Not Regex.IsMatch(TxtIp.Text, "(\d{1,3}\.){3}\d{1,3}") Then
            MessageBox.Show(
                lr("The IP address you entered is invalid. It should be something like for example 192.168.1.2"),
                lr("Invalid inpunt"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        forward(TxtName.Text, CUInt(NumPort.Value), TxtIp.Text, protocol, True)
    End Sub

    Private Function available() As Boolean
        Return New UPnP().UPnPEnabled
    End Function

    Private Function forward(name As String, port As UInteger, ip As String,
                             Optional ByVal protocol As UPnP.Protocol = UPnP.Protocol.TCP,
                             Optional ByVal async As Boolean = True) As Boolean
        Try
            Me.Cursor = Cursors.WaitCursor
            lblStatus.Text = "adding port forward..."
            If LastMapping Is Nothing Then LastMapping = New UPnP()
            If LastMapping.Exists(port, protocol) Then
                MessageBox.Show(lr("This port is already forwarded"), lr("Port already in use"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Return False 'already in use
            Else

                If Not async Then
                    LastMapping.Add(ip, port, protocol, name)
                Else
                    Dim t As New Thread(AddressOf ApplyForward)
                    t.Start(New portForwardInfo(name, port, ip, protocol))
                End If
                Return True
            End If
        Catch ex As Exception
            Log(loggingLevel.Warning, "PortForwarder", "Couldn't forward ports", ex.InnerException.Message)
            MessageBox.Show(
                lr("This port couldn't be forwarded. Something went wrong while communicating with your router"),
                lr("Can't forward"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function forward(name As String, port_start As UInteger, port_end As UInteger, ip As String) As Boolean
        Dim success = True

        Dim port As UInteger = port_start
        While port < port_end
            success = success And forward(name, port, ip)
        End While
        Return success
    End Function

    Private Function GetMaps() As List(Of PortMappingEntry)
        Try
            Dim pnp = New UPnP()
            Me.LastMapping = pnp

            If pnp Is Nothing Then
                MessageBox.Show(
                    Lr(
                        "Port forwarding requires Plug-and-play support from your router. This function seems unavailable. Ensure your router supports uPnP, and that uPnP is enabled."),
                    Lr("Unavailable"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            End If

            Dim mapping = pnp.GetMapping
            RaiseEvent MappingUpdateReceived(mapping)
            Return mapping
        Catch ex As Exception
            Log(loggingLevel.Warning, "PortForwarder", "Error while loading mapping: " & ex.Message)
            RaiseEvent MappingUpdateReceived(New List(Of PortMappingEntry))
            Return New List(Of PortMappingEntry)
        End Try
    End Function

    Private Sub ApplyForward(fwi As portForwardInfo)
        Try
            LastMapping.Add(fwi.ip, fwi.port, fwi.protocol, fwi.name)
        Catch ex As Exception
            Log(loggingLevel.Warning, "PortForwarder", "Error while loading mapping: " & ex.Message)
        End Try
        RaiseEvent PortForwardApplied()
    End Sub
End Class

Friend Class portForwardInfo
    Public name As String, port As UInteger, ip As String, protocol As UPnP.Protocol = UPnP.Protocol.TCP

    Public Sub New(name As String, port As UInteger, ip As String,
                   Optional ByVal protocol As UPnP.Protocol = UPnP.Protocol.TCP)
        Me.name = name
        Me.port = port
        Me.ip = ip
        Me.protocol = protocol
    End Sub
End Class