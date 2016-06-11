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

Module SoundNotificator
    Private _onPlayerJoin As Boolean, _onplayerDisconnect As Boolean, _onWarning As Boolean, _onsevere As Boolean

    Public Property onPlayerJoin As Boolean
        Get
            Return _onPlayerJoin
        End Get
        Set(value As Boolean)
            writeAsBool("join", value, "sound")
            _onPlayerJoin = value
        End Set
    End Property

    Public Property onPlayerDisconnect As Boolean
        Get
            Return _onplayerDisconnect
        End Get
        Set(value As Boolean)
            writeAsBool("disconnect", value, "sound")
            _onplayerDisconnect = value
        End Set
    End Property

    Public Property onWarning As Boolean
        Get
            Return _onWarning
        End Get
        Set(value As Boolean)
            writeAsBool("warning", value, "sound")
            _onWarning = value
        End Set
    End Property

    Public Property onSevere As Boolean
        Get
            Return _onsevere
        End Get
        Set(value As Boolean)
            writeAsBool("severe", value, "sound")
            _onsevere = value
        End Set
    End Property

    Public Sub init()
        _onPlayerJoin = readAsBool("join", False, "sound")
        _onplayerDisconnect = readAsBool("disconnect", False, "sound")
        _onWarning = readAsBool("warning", False, "sound")
        _onsevere = readAsBool("severe", False, "sound")

        AddHandler serverOutputHandler.PlayerJoin, AddressOf hnd_PlayerJoin
        AddHandler serverOutputHandler.PlayerDisconnect, AddressOf hnd_PlayerDisconnect
        AddHandler serverOutputHandler.WarningReceived, AddressOf hnd_WarningReceived
        AddHandler serverOutputHandler.SevereReceived, AddressOf hnd_SeverReceived
    End Sub

    Private Sub hnd_PlayerJoin(e As PlayerJoinEventArgs)
        If e.reason = PlayerDisconnectEventArgs.playerleavereason.listupdate Then Exit Sub
        If onPlayerJoin Then
            My.Computer.Audio.Play(My.Resources.sound_connect, AudioPlayMode.Background)
        End If
    End Sub

    Private Sub hnd_PlayerDisconnect(e As PlayerDisconnectEventArgs)
        If e.reason = PlayerDisconnectEventArgs.playerleavereason.listupdate Then Exit Sub
        If onPlayerDisconnect Then
            My.Computer.Audio.Play(My.Resources.sound_disconnect, AudioPlayMode.Background)
        End If
    End Sub

    Private Sub hnd_WarningReceived(e As ErrorReceivedEventArgs)
        If onWarning Then
            My.Computer.Audio.Play(My.Resources.sound_warning, AudioPlayMode.Background)
        End If
    End Sub

    Private Sub hnd_SeverReceived(e As ErrorReceivedEventArgs)
        If onSevere Then
            My.Computer.Audio.Play(My.Resources.sound_severe, AudioPlayMode.Background)
        End If
    End Sub
End Module