Imports Bertware.BukkitGUI.core
Imports Bertware.BukkitGUI.debugging
Imports System.Threading

Module TrayIconManager

    Private _onPlayerJoin As Boolean, _onplayerDisconnect As Boolean, _onWarning As Boolean, _onsevere As Boolean, _showTime As UInt16, _minimize As Boolean, _always As Boolean
    Private tray As NotifyIcon

    Public Property onPlayerJoin As Boolean
        Get
            Return _onPlayerJoin
        End Get
        Set(value As Boolean)
            config.writeAsBool("join", value, "tray")
            _onPlayerJoin = value
        End Set
    End Property

    Public Property onPlayerDisconnect As Boolean
        Get
            Return _onplayerDisconnect
        End Get
        Set(value As Boolean)
            config.writeAsBool("disconnect", value, "tray")
            _onPlayerJoin = value
        End Set
    End Property

    Public Property onWarning As Boolean
        Get
            Return _onWarning
        End Get
        Set(value As Boolean)
            config.writeAsBool("warning", value, "tray")
            _onPlayerJoin = value
        End Set
    End Property

    Public Property onSevere As Boolean
        Get
            Return _onsevere
        End Get
        Set(value As Boolean)
            config.writeAsBool("severe", value, "tray")
            _onPlayerJoin = value
        End Set
    End Property

    Public Property minimize As Boolean
        Get
            Return _minimize
        End Get
        Set(value As Boolean)
            config.writeAsBool("minimize", value, "tray")
            _minimize = value
        End Set
    End Property

    Public Property always As Boolean
        Get
            Return _always
        End Get
        Set(value As Boolean)
            config.writeAsBool("always", value, "tray")
            _always = value
        End Set
    End Property

    Public Property showtime As UInt16
        Get
            Return _showTime
        End Get
        Set(value As UInt16)
            config.write("time", value.ToString, "tray")
            _showTime = value
        End Set
    End Property

    Public Sub linktray()
        tray = mainform.Tray
    End Sub

    Public Sub init()

        _showTime = CInt(config.read("time", "500", "tray"))
        _onPlayerJoin = config.readAsBool("join", False, "tray")
        _onplayerDisconnect = config.readAsBool("disconnect", False, "tray")
        _onWarning = config.readAsBool("warning", False, "tray")
        _onsevere = config.readAsBool("severe", False, "tray")
        _always = config.readAsBool("always", False, "tray")
        _minimize = config.readAsBool("minimize", False, "tray")

        AddHandler serverOutputHandler.PlayerJoin, AddressOf hnd_PlayerJoin
        AddHandler serverOutputHandler.PlayerDisconnect, AddressOf hnd_PlayerDisconnect
        AddHandler serverOutputHandler.WarningReceived, AddressOf hnd_WarningReceived
        AddHandler serverOutputHandler.SevereReceived, AddressOf hnd_SevereReceived
    End Sub


    Private Sub hnd_PlayerJoin(e As PlayerJoinEventArgs)
        If onPlayerJoin And ((Not mainform.ShowInTaskbar) Or always) Then
            tray.ShowBalloonTip(500, lr("Player joined"), e.PlayerJoin.player.name & "(" & e.PlayerJoin.player.IP & ") " & lr("logged in to the server") & vbCrLf & lr("at world") & " " & e.PlayerJoin.World, ToolTipIcon.Info)
        End If
    End Sub

    Private Sub hnd_PlayerDisconnect(e As PlayerDisconnectEventArgs)
        If onPlayerDisconnect And ((Not mainform.ShowInTaskbar) Or always) Then
            tray.ShowBalloonTip(500, lr("Player disconnected"), e.player.name & " " & lr("disconnected") & ", " & lr("reason:") & CType(e.details, PlayerLeave).reason.ToString, ToolTipIcon.Info)
        End If
    End Sub



    Private Sub hnd_WarningReceived(e As ErrorReceivedEventArgs)
        If onWarning And ((Not mainform.ShowInTaskbar) Or always) Then
            tray.BalloonTipIcon = ToolTipIcon.Warning
            tray.BalloonTipText = e.message
            tray.BalloonTipTitle = lr("Warning")
            tray.ShowBalloonTip(500)

        End If
    End Sub

    Private Sub hnd_SevereReceived(e As ErrorReceivedEventArgs)
        If onSevere And ((Not mainform.ShowInTaskbar) Or always) Then
            tray.BalloonTipIcon = ToolTipIcon.Error
            tray.BalloonTipText = e.message
            tray.BalloonTipTitle = lr("Severe")
            tray.ShowBalloonTip(500)
        End If
    End Sub


    Public Sub SendToTray()
        mainform.WindowState = FormWindowState.Minimized
        mainform.ShowInTaskbar = False
        mainform.Visible = False
    End Sub

    Public Sub ShowFromTray()
        mainform.Visible = True
        mainform.ShowInTaskbar = True
        mainform.WindowState = FormWindowState.Normal
        mainform.Refresh()
        mainform.ARTXTServerOutput.ScrollToBottom()
    End Sub

End Module
