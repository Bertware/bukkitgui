Imports System.IO
Imports Net.Bertware.BukkitGUI.Core

Namespace MCInterop
    ''' <summary>
    ''' Module to read minecraft server settings + whitelist, ops, banned players and Ips
    ''' </summary>
    ''' <remarks></remarks>
    Module ServerSettings

        Private _ops As List(Of String), _whitelist As List(Of String), _player_bans As List(Of String), _ip_bans As List(Of String),
        _settings As Dictionary(Of String, String)

        Public ReadOnly ops_file As String = My.Application.Info.DirectoryPath & "/ops.txt"
        Public ReadOnly whitelist_file As String = My.Application.Info.DirectoryPath & "/white-list.txt"
        Public ReadOnly playerban_file As String = My.Application.Info.DirectoryPath & "/banned-players.txt"
        Public ReadOnly ipban_file As String = My.Application.Info.DirectoryPath & "/banned-ips.txt"
        Public ReadOnly server_properties_file As String = My.Application.Info.DirectoryPath & "/server.properties"

        Public Property ops As List(Of String)
            Get
                Return _ops
            End Get
            Set(value As List(Of String))
                WriteList(value, ops_file)
            End Set
        End Property

        Public Property whitelist As List(Of String)
            Get
                Return _whitelist
            End Get
            Set(value As List(Of String))
                WriteList(value, whitelist_file)
            End Set
        End Property

        Public Property Playerbans As List(Of String)
            Get
                Return _player_bans
            End Get
            Set(value As List(Of String))
                WriteList(value, playerban_file)
            End Set
        End Property

        Public Property IPBans As List(Of String)
            Get
                Return _ip_bans
            End Get
            Set(value As List(Of String))
                WriteList(value, ipban_file)
            End Set
        End Property

        Public ReadOnly Property settings As Dictionary(Of String, String)
            Get
                Return _settings
            End Get
        End Property

        Public ReadOnly Property MOTD As String
            Get
                If common.isRunningLight Then Return ""
                If _settings Is Nothing Then LoadSettings()
                If _settings Is Nothing Then Return "" : Exit Property
                Return GetSetting("motd")
            End Get
        End Property

        Public Sub init()
            LoadSettings()
            LoadLists()
        End Sub

        Public Sub LoadSettings()
            _settings = ReadServerSettings(server_properties_file)
        End Sub

        Public Sub LoadLists()
            _ops = ReadList(ops_file)
            _whitelist = ReadList(whitelist_file)
            _player_bans = ReadList(playerban_file)
            _ip_bans = ReadList(ipban_file)
        End Sub

        Private Sub WriteList(list As List(Of String), path As String)
            Dim fs As New FileStream(path, IO.FileMode.Create)
            Dim sw As New StreamWriter(fs)
            Dim text As String = ""
            For Each entry As String In list
                text += entry & vbCrLf
            Next
            text = text.Trim(vbCrLf).Trim
            sw.WriteLine(text)
            sw.Close()
        End Sub

        Private Function ReadList(path As String) As List(Of String)
            Try
                If Not FileIO.FileSystem.FileExists(path) Then Return New List(Of String) : Exit Function

                Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read)
                Dim sr As New StreamReader(fs)
                Dim list As New List(Of String)
                While sr.EndOfStream = False
                    list.Add(sr.ReadLine)
                End While
                sr.Close()

                Return list

            Catch ioex As System.IO.IOException
                livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read list from file: " & path, ioex.Message)
                Return New List(Of String)
            Catch ex As Exception
                Try
                    Dim fs2 As New FileStream(path, FileMode.Open, FileAccess.Read)
                    Dim sr2 As New StreamReader(fs2)
                    Dim list As New List(Of String)
                    While sr2.EndOfStream = False
                        list.Add(sr2.ReadLine)
                    End While
                    sr2.Close()

                    Return list
                Catch ioex As System.IO.IOException
                    livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read list from file: " & path, ioex.Message)
                    Return New List(Of String)
                Catch ex2 As Exception
                    livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read list from file: " & path, ex.Message)
                    Return New List(Of String)
                End Try
            End Try
        End Function

        Private Sub WriteServerSettings(settings As Dictionary(Of String, String), path As String)
            livebug.write(loggingLevel.Fine, "ServerSettings", "Writing server settings to " & path)
            Dim fs As New FileStream(path, IO.FileMode.Create)
            Dim sw As New StreamWriter(fs)

            For Each entry In settings 'add all lines to list
                sw.WriteLine(entry.Key + "=" + entry.Value)
            Next

            sw.Close()

        End Sub

        Private Function ReadServerSettings(path As String) As Dictionary(Of String, String)
            Try
                If Not FileIO.FileSystem.FileExists(path) Then Return New Dictionary(Of String, String) : Exit Function
                livebug.write(loggingLevel.Fine, "ServerSettings", "Reading server settings from " & path)
                Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read)

                Dim sr As New StreamReader(fs)
                Dim content As String = ""

                While sr.EndOfStream = False 'read all config lines
                    Dim l As String = sr.ReadLine
                    If l.Contains("#") = False Then content += l & vbCrLf
                End While

                _settings = New Dictionary(Of String, String)
                If content IsNot Nothing AndAlso content <> "" AndAlso content.Trim(vbCrLf).Contains(vbCrLf) Then
                    For Each item As String In content.Trim.Trim(vbCrLf).Trim.Split(vbCrLf) 'add all lines to list
                        If item.Contains("=") Then _settings.Add(item.Trim.Split("=")(0), item.Trim.Split("=")(1))
                    Next
                Else
                    Return New Dictionary(Of String, String) : Exit Function
                End If

                sr.Close()

                Return _settings
            Catch ioex As IO.IOException
                livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read server settings from file: " & path, ioex.Message)
                Return New Dictionary(Of String, String)
            Catch aex As ArgumentException
                livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read server settings from file: " & path, aex.Message)
                Return New Dictionary(Of String, String)
            Catch pex As Security.SecurityException
                livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read server settings from file: " & path, pex.Message)
                Return New Dictionary(Of String, String)
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "ServerSettings", "Could not read server settings from file: " & path, ex.Message)
                Return New Dictionary(Of String, String)
            End Try
        End Function

        Public Sub AddOp(player As String)
            If server.running Then
                server.SendCommand("op " & player)
            Else
                _ops.Add(player)
                WriteList(_ops, ops_file)
            End If
        End Sub

        Public Sub AddWhitelist(player As String)
            If server.running Then
                server.SendCommand("whitelist add " & player)
            Else
                _whitelist.Add(player)
                WriteList(_whitelist, whitelist_file)
            End If
        End Sub

        Public Sub AddPlayerBan(player As String)
            If server.running Then
                server.SendCommand("ban " & player)
            Else
                _player_bans.Add(player)
                WriteList(_player_bans, playerban_file)
            End If
        End Sub

        Public Sub AddIpBan(ip As String)
            If server.running Then
                server.SendCommand("ban-ip " & ip)
            Else
                _ip_bans.Add(ip)
                WriteList(_ip_bans, ipban_file)
            End If
        End Sub

        Public Sub RemoveOp(player As String)
            If server.running Then
                server.SendCommand("deop " & player)
            Else
                _ops.Remove(player)
                WriteList(_ops, ops_file)
            End If
        End Sub

        Public Sub RemoveWhitelist(player As String)
            If server.running Then
                server.SendCommand("whitelist remove " & player)
            Else
                _whitelist.Remove(player)
                WriteList(_whitelist, whitelist_file)
            End If
        End Sub

        Public Sub RemovePlayerBan(player As String)
            If server.running Then
                server.SendCommand("pardon " & player)
            Else
                _player_bans.Remove(player)
                WriteList(_player_bans, playerban_file)
            End If
        End Sub

        Public Sub RemoveIpBan(ip As String)
            If server.running Then
                server.SendCommand("pardon-ip " & ip)
            Else
                _ip_bans.Remove(ip)
                WriteList(_ip_bans, ipban_file)
            End If
        End Sub

        ''' <summary>
        ''' Check if a player is an OP
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        ''' <remarks>Always returns false in case of light mode</remarks>
        Public Function IsOP(name As String) As Boolean
            If common.isRunningLight Then Return False 'ops not loaded
            For Each entry As String In _ops
                If entry.ToLower = name.ToLower Then Return True : Exit Function
            Next
            Return False
        End Function

        ''' <summary>
        ''' Check if a player is whitelisted
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        ''' <remarks>Always returns false in case of light mode</remarks>
        Public Function IsWhitelisted(name As String) As Boolean
            If common.isRunningLight Then Return False
            For Each entry As String In _whitelist
                If entry.ToLower = name.ToLower Then Return True : Exit Function
            Next
            Return False
        End Function

        Public Sub SetSetting(name As String, value As String)
            If _settings.ContainsKey(name) Then _settings(name) = value Else _settings.Add(name, value)
            WriteServerSettings(_settings, server_properties_file) : Exit Sub
        End Sub

        Public Function GetSetting(name As String) As String
            If _settings.ContainsKey(name) Then Return _settings(name) Else Return Nothing
        End Function

        Public Sub AddSetting(name As String, value As String)
            If Not _settings.ContainsKey(name) Then _settings.Add(name, value) Else _settings(name) = value
            WriteServerSettings(_settings, server_properties_file)
            Exit Sub
        End Sub

        Public Sub RemoveSetting(name As String)
            If _settings.ContainsKey(name) Then _settings.Remove(name)
            WriteServerSettings(_settings, server_properties_file)
        End Sub

    End Module

    Public Class ServerSetting
        Public name As String = "", value As String = ""

        Public Sub New()
            name = ""
            value = ""
        End Sub

        Public Sub New(name As String, value As String)
            Me.name = name
            Me.value = value
        End Sub

        Public Overrides Function ToString() As String
            Return name & "=" & value
        End Function

        Public Shared Function parse(text As String) As ServerSetting
            Return New ServerSetting(text.Split("=")(0), text.Split("=")(1))
        End Function
    End Class
End Namespace