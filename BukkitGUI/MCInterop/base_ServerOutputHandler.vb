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



'Module with text to handle the server output (Text won't be displayed using this module, but players will be added/removed, errors will be sorted,...)
Imports System.Text
Imports System.Text.RegularExpressions
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities


Namespace MCInterop
    ' <summary>
    '     This module raises events when a certain type of server output passes, and provides functions to handle server
    '     output.
    '     The function Lookup will parse output, raise the needed events, these events will result in player joins etc.
    ' </summary>
    ' <remarks></remarks>
    Public Module serverOutputHandler
        ''' <summary>
        '''     This event is raised when a player joins. Also raised when a player is added on list synchronization
        ''' </summary>
        ''' <remarks>This event should be linked to a sub that adds the player to the UI</remarks>
        Public Event PlayerJoin(ByVal e As PlayerJoinEventArgs)


        ''' <summary>
        '''     This event is raised when a player leaves, gets kicked or banned. Also raised when a player is removed on list
        '''     synchronization
        ''' </summary>
        ''' <remarks>This event should be linked to a sub that removes the player from the UI</remarks>
        Public Event PlayerDisconnect(ByVal e As PlayerDisconnectEventArgs)


        ''' <summary>
        '''     This event is raised when a list synchronization has been ended
        ''' </summary>
        ''' <remarks></remarks>
        Public Event ListUpdate()


        ''' <summary>
        '''     This event is raised when text was received by the server
        ''' </summary>
        ''' <param name="text"></param>
        ''' <remarks></remarks>
        Public Event TextReceived(text As String, type As MessageType)


        ''' <summary>
        '''     This event is raised when a message with a [severe] tag is detected.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event SevereReceived(ByVal e As ErrorReceivedEventArgs)


        ''' <summary>
        '''     This event is raised when a message with a [warning] tag is detected.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event WarningReceived(ByVal e As ErrorReceivedEventArgs)


        ''' <summary>
        '''     This event is raised when a stack trace is detected.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event StackTraceReceived(ByVal e As StackTraceReceivedEventArgs)


        ''' <summary>
        '''     This event will trigger the comparison of the listview containing player names and the list of actual online
        '''     players
        ''' </summary>
        ''' <param name="onlineplayers"></param>
        ''' <remarks></remarks>
        Public Event CheckUILists(onlineplayers As List(Of String))

        Dim _utf8comp As Boolean = False

        Public Property UTF8Compatibility As Boolean
            Get
                Return _utf8comp
            End Get
            Set(value As Boolean)
                _utf8comp = value
            End Set
        End Property


        ''' <summary>
        '''     The output type of a console message
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum MessageType
            info 'info message
            warning 'warning message
            severe 'severe message
            playerjoin 'player joining
            playerleave 'player leaving
            playerkick 'player being kicked
            playerban 'player being banned
            ipban 'ip being banned
            list 'List command output
            listcount '1.3 list count: There are x/x players online:
            javastacktrace 'java stack trace e.g. "at net.minecraft....(java:12)"
            unknown 'unknown type, does not match any of the above
        End Enum


        ''' <summary>
        '''     Initialize the module, load values from config
        ''' </summary>
        ''' <returns>True if successful</returns>
        ''' <remarks></remarks>
        Public Function init() As Boolean
            _clrInfo = ColorTranslator.FromHtml(read("info", "blue", "output"))
            _clrPlayerEvent = ColorTranslator.FromHtml(read("player_event", "green", "output"))
            _clrWarning = ColorTranslator.FromHtml(read("warning", "orange", "output"))
            _clrSevere = ColorTranslator.FromHtml(read("severe", "red", "output"))
            _clrUnknown = ColorTranslator.FromHtml(read("unknown", "black", "output"))

            _show_date = readAsBool("date", False, "output")
            _show_time = readAsBool("time", True, "output")
            Return True
        End Function

        Private _show_date As Boolean = False
        Private _show_time As Boolean = False


        ''' <summary>
        '''     Should the date be shown before each server message
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Show_date As Boolean
            Get
                Return _show_date
            End Get
            Set(value As Boolean)
                _show_date = value
                writeAsBool("date", value, "output")
            End Set
        End Property


        ''' <summary>
        '''     Should the time be shown before each server message
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Show_time As Boolean
            Get
                Return _show_time
            End Get
            Set(value As Boolean)
                _show_time = value
                writeAsBool("time", value, "output")
            End Set
        End Property

#Region "Colors"
        'private variables with default colors
        Dim _clrPlayerEvent As Color = Color.DarkGreen
        Dim _clrSevere As Color = Color.DarkRed
        Dim _clrWarning As Color = Color.Orange
        Dim _clrInfo As Color = Color.Blue
        Dim _clrUnknown As Color = Color.Black

        'public properties
        'these are also responsible for the saving of the values

        Public Property clrPlayerEvent As Color
            Get
                Return _clrPlayerEvent
            End Get
            Set(value As Color)
                _clrPlayerEvent = value
                write("player_event", ColorTranslator.ToHtml(value).Trim, "output")
            End Set
        End Property

        Public Property clrSevere As Color
            Get
                Return _clrSevere
            End Get
            Set(value As Color)
                _clrSevere = value
                write("severe", ColorTranslator.ToHtml(value).Trim, "output")
            End Set
        End Property

        Public Property clrWarning As Color
            Get
                Return _clrWarning
            End Get
            Set(value As Color)
                _clrWarning = value
                write("warning", ColorTranslator.ToHtml(value).Trim, "output")
            End Set
        End Property

        Public Property clrInfo As Color
            Get
                Return _clrInfo
            End Get
            Set(value As Color)
                _clrInfo = value
                write("info", ColorTranslator.ToHtml(value).Trim, "output")
            End Set
        End Property

        Public Property clrUnknown As Color
            Get
                Return _clrUnknown
            End Get
            Set(value As Color)
                _clrUnknown = value
                write("unknown", ColorTranslator.ToHtml(value).Trim, "output")
            End Set
        End Property

#End Region


        ''' <summary>
        '''     Check server output for events etc.
        ''' </summary>
        ''' <param name="text">the text that should be parsed</param>
        ''' <remarks>This routine isn't responsible for output etc., only for parsing an looking at the content</remarks>
        Public Sub Lookup(text As String) _
'Most importante routine. Will lookup a text to a set of rules, in order to raise events, add/remove players,...
            Try
                Dim t As MessageType = getMessageType(text)
                Lookup(text, t)
            Catch global_ex As Exception
                If text IsNot Nothing Then _
                    Log(loggingLevel.Severe, "ServerOutputHandler",
                        "Exception at LookUp (text)! Parameter:" & text, global_ex.Message) Else _
                    Log(loggingLevel.Severe, "ServerOutputHandler", "Exception at LookUp! Text is null!",
                        global_ex.Message)
            End Try
        End Sub


        ''' <summary>
        '''     Check server output for events etc.
        ''' </summary>
        ''' <param name="passargs">the text and type that should be parsed</param>
        ''' <remarks>This routine isn't responsible for output etc., only for parsing an looking at the content</remarks>
        Public Sub LookupAsync(PassArgs As thds_pass_lookup) _
'Most importante routine. Will lookup a text to a set of rules, in order to raise events, add/remove players,...
            Lookup(PassArgs.text, PassArgs.type)
        End Sub


        ''' <summary>
        '''     Check server output for events etc.
        ''' </summary>
        ''' <param name="text">the text that should be parsed</param>
        ''' <remarks>This routine isn't responsible for output etc., only for parsing an looking at the content</remarks>
        Public Sub Lookup(text As String, type As MessageType) _
'Most importante routine. Will lookup a text to a set of rules, in order to raise events, add/remove players,...
            If text Is Nothing Then Exit Sub
            Try
                RaiseEvent TextReceived(text, type)
                Select Case type
                    Case MessageType.info
                    Case MessageType.warning 'warning message
                        RaiseEvent WarningReceived(New ErrorReceivedEventArgs(text, MessageType.warning))
                    Case MessageType.severe 'severe message
                        RaiseEvent SevereReceived(New ErrorReceivedEventArgs(text, MessageType.severe))
                    Case MessageType.playerjoin 'player joining
                        Dim pj As PlayerJoin = AnalyzeAction(PlayerAction.player_join, text)
                        RaiseEvent _
                            PlayerJoin(New PlayerJoinEventArgs(PlayerJoinEventArgs.playerjoinreason.join, text, pj))
                    Case MessageType.playerleave 'player leaving
                        Dim pl As PlayerLeave = AnalyzeAction(PlayerAction.player_leave, text)
                        RaiseEvent _
                            PlayerDisconnect(New PlayerDisconnectEventArgs(pl.player,
                                                                           PlayerDisconnectEventArgs.playerleavereason.
                                                                              leave, text, pl))
                    Case MessageType.playerkick 'player being kicked
                        Dim pk As PlayerKick = AnalyzeAction(PlayerAction.player_kick, text)
                        RaiseEvent _
                            PlayerDisconnect(New PlayerDisconnectEventArgs(pk.player,
                                                                           PlayerDisconnectEventArgs.playerleavereason.
                                                                              kick, text, pk))
                    Case MessageType.playerban 'player being banned
                        Dim pb As playerBan = AnalyzeAction(PlayerAction.player_ban, text)
                        RaiseEvent _
                            PlayerDisconnect(New PlayerDisconnectEventArgs(pb.player,
                                                                           PlayerDisconnectEventArgs.playerleavereason.
                                                                              ban, text, pb))
                    Case MessageType.ipban 'ip being banned

                    Case MessageType.listcount 'List command output
                        Log(loggingLevel.Fine, "ServerOutputHandler",
                            "List count recognized, will update player list")
                    Case MessageType.list 'List command output
                        Log(loggingLevel.Fine, "ServerOutputHandler",
                            "List command output recognized, will update player list")
                        RaiseEvent ListUpdate()
                        HandleList(text)
                    Case MessageType.javastacktrace 'java stack trace e.g. "at net.minecraft....(java:12)"
                        RaiseEvent StackTraceReceived(New StackTraceReceivedEventArgs(text))
                    Case MessageType.unknown 'unknown type, does not match any of the above
                End Select
            Catch global_ex As Exception
                If text IsNot Nothing Then _
                    Log(loggingLevel.Severe, "ServerOutputHandler",
                        "Severe exception at LookUp (text,type)! Parameter:" & text, global_ex.Message) Else _
                    Log(loggingLevel.Severe, "ServerOutputHandler",
                        "Severe exception at LookUp! Text is null!", global_ex.Message)
            End Try
        End Sub


        ''' <summary>
        '''     This function reads the output and provides data for the event
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getWarningArgs(text As String) As ErrorReceivedEventArgs
            Return New ErrorReceivedEventArgs(text, MessageType.warning)
        End Function


        ''' <summary>
        '''     This function reads the output and provides data for the event
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getSevereArgs(text As String) As ErrorReceivedEventArgs
            Return New ErrorReceivedEventArgs(text, MessageType.severe)
        End Function


        ''' <summary>
        '''     This function reads the output and provides data for the event
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getPlayerJoinArgs(text As String) As PlayerJoinEventArgs 'player joining
            Dim pj As PlayerJoin = AnalyzeAction(PlayerAction.player_join, text)
            Log(loggingLevel.Fine, "ServerOutputHandler", "PlayerJoinEvent Raised!")
            Return New PlayerJoinEventArgs(PlayerJoinEventArgs.playerjoinreason.join, text, pj)
        End Function


        ''' <summary>
        '''     This function reads the output and provides data for the event
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getPlayerLeaveArgs(text As String) As PlayerDisconnectEventArgs 'player leaving
            Dim pl As PlayerLeave = AnalyzeAction(PlayerAction.player_leave, text)
            Return New PlayerDisconnectEventArgs(pl.player, PlayerDisconnectEventArgs.playerleavereason.leave, text, pl)
        End Function


        ''' <summary>
        '''     This function reads the output and provides data for the event
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getPlayerKickArgs(text As String) As PlayerDisconnectEventArgs 'player being kicked
            Dim pk As PlayerKick = AnalyzeAction(PlayerAction.player_kick, text)
            Return New PlayerDisconnectEventArgs(pk.player, PlayerDisconnectEventArgs.playerleavereason.kick, text, pk)
        End Function


        ''' <summary>
        '''     This function reads the output and provides data for the event
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getPlayerBanArgs(text As String) As PlayerDisconnectEventArgs 'player being banned
            Dim pb As playerBan = AnalyzeAction(PlayerAction.player_ban, text)
            Return New PlayerDisconnectEventArgs(pb.player, PlayerDisconnectEventArgs.playerleavereason.ban, text, pb)
        End Function


        ''' <summary>
        '''     Remove unknown characters from the server output
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks>These characters are (in most cases) coused by the use of --nojline</remarks>
        Public Function FixJLine(text As String) As String
            Debug.WriteLine("FixJLine input: " & text)
            ' Dim pattern As String = "\x1B\[(\d\d|\d)m"
            If _utf8comp = False Then
                text = ASCIIEncoding.ASCII.GetString(Encoding.ASCII.GetBytes(text))
            End If

            Dim pattern As String = "(\x1B|)\[m(\x1B|)$" 'fix <[m at end
            text = Regex.Replace(text, pattern, "")

            pattern = "\x3F\x65" 'fix ?e
            text = Regex.Replace(text, pattern, "")

            pattern = "\xA7(\d|\w)" 'spigot has another format for color codes
            text = Regex.Replace(text, pattern, "")

            pattern = "\x1B\[\d{1,2}(\;\d{1,2}|){1,2}m"
            Return Regex.Replace(text, pattern, "") ' [0;33;22m etc
        End Function

        Dim AcceptList As Boolean = True


        ''' <summary>
        '''     Get the message type for a given text
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getMessageType(text As String) As MessageType
            Try
                Dim infotag As String = "\[info\]"
                Dim warntag As String = "\[warning\]"
                Dim severetag As String = "\[severe\]"

                'text = RemoveTimeStamp(text).ToLower 'Remove any date or timestamps in front, to lowercase
                text = FixTextCompat(text)

                If text Is Nothing Then Return MessageType.unknown : Exit Function

                text = Regex.Replace(text, "\s{0,1}\[minecraft[^\]]*\]", "", RegexOptions.IgnoreCase) _
                '[minecraft], [minecraft-server] will also be filtered out,
                '
                'All text will be handled as lowercase, starting with the [info/warning/severe] tag (if present)
                '

                If Regex.IsMatch(text, "^" & warntag, RegexOptions.IgnoreCase) Then 'Detect warning
                    Return MessageType.warning

                ElseIf _
                    Regex.IsMatch(text, "^" & severetag) Or Regex.IsMatch(text, "^\[stderr\]") Or
                    Regex.IsMatch(text, "^\[info\]\s{0,1}\[stderr\]", RegexOptions.IgnoreCase) Then 'Detect severe
                    Return MessageType.severe

                ElseIf _
                    Regex.IsMatch(text,
                                  "^" & infotag &
                                  "\s{0,1}\w{1,16}\[/\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}(:\d{3,5}|)\] logged in with entity id",
                                  RegexOptions.IgnoreCase) Then 'Detect playerjoin
                    '[INFO] Bertware[/127.0.0.1:58189] logged in with entity id 27 at ([world] -1001.0479985318618, 2.0, 1409.300000011921)
                    '[INFO] Bertware[/127.0.0.1:58260] logged in with entity id 0 at (-1001.0479985318618, 2.0, 1409.300000011921)
                    Return MessageType.playerjoin

                ElseIf _
                    Regex.IsMatch(text, "^" & infotag & " \w{1,16} lost connection", RegexOptions.IgnoreCase) Or
                    Regex.IsMatch(text, "^\[info\] \w{1,16} left the game.", RegexOptions.IgnoreCase) Then _
'Detect playerleave
                    '[INFO] Bertware lost connection: disconnect.endOfStream
                    Return MessageType.playerleave

                ElseIf _
                    Regex.IsMatch(text, "^" & infotag & "( )player( )(\w{1,16})( )kicked( )(\w{1,16})( )for",
                                  RegexOptions.IgnoreCase) _
                    Or Regex.IsMatch(text, "^" & infotag & " (\w){1,16}: kicking \w{1,16}", RegexOptions.IgnoreCase) _
                    Or Regex.IsMatch(text, "^" & infotag & " kicked \w{1,16} from the game", RegexOptions.IgnoreCase) _
                    Or
                    Regex.IsMatch(text, "^" & infotag & " (\w){1,16}: kicked player (\w){1,16}. with reason:",
                                  RegexOptions.IgnoreCase) Then 'Detect playerkick
                    'Player Console kicked Bertware for Kicked from server..
                    '14:42:36 [INFO] CONSOLE: Kicking bertware
                    '[INFO] Kicked Bertware from the game: 'zomaar'
                    '2013-06-20 19:11:35 [INFO] CONSOLE: Kicked player Bertware. With reason:


                    If _
                        Regex.IsMatch(text, "^" & infotag & " (\w){1,16}: kicked player (\w){1,16}. with reason:",
                                      RegexOptions.IgnoreCase) Then AcceptList = False 'Next will be the ban reason
                    Return MessageType.playerkick

                ElseIf _
                    Regex.IsMatch(text,
                                  "^" & infotag &
                                  " {0,1}(\w{1,16}: |)banned ip address \d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}",
                                  RegexOptions.IgnoreCase) Then 'Detect ipban
                    'CONSOLE: Banned IP Address 127.1.1.1
                    '[INFO] Banned IP address 127.1.1.1
                    Return MessageType.ipban

                ElseIf _
                    Regex.IsMatch(text, "^" & infotag & " {0,1}(\w{1,16}: |)banned player address \w{1,16}",
                                  RegexOptions.IgnoreCase) Then 'detect player ban
                    '[INFO] Banned player bertware
                    'CONSOLE: Banned player bertware
                    Return MessageType.playerban

                ElseIf Regex.IsMatch(text, "^" & infotag & " connected players:", RegexOptions.IgnoreCase) Then _
'Detect list
                    '[info] connected players: bertware, ...
                    Return MessageType.list
                ElseIf _
                    Regex.IsMatch(text, "there are (.*) players online", RegexOptions.IgnoreCase) AndAlso
                    Regex.IsMatch(text, "\d{1,3}/\d{1,3}") Then
                    Return MessageType.listcount

                    'Note: numbers etc are stripped, so "27 achievements" becomes "achievements"
                    'Therefore, this has to be filtered out
                ElseIf (Regex.IsMatch(text, "^(" & infotag & " |)\w{2,16}(, |$)" _
                                            & "((.{1,16})\w{2,16}(, |$))*$") Or
                        Regex.IsMatch(text, "^(" & infotag & " |)( |)$")) And
                       Not (text = "recipes" Or text = "achievements") Then
                    'First player can't have a tag!
                    '[info] Bertware, other,
                    If AcceptList = False Then AcceptList = True : Return MessageType.info 'next list will be accepted
                    Return MessageType.list

                ElseIf _
                    Regex.IsMatch(text, "^" & infotag) Or
                    (text.Contains("aliasing material") And text.Contains("name:")) Then _
'Detect info, with support for mods
                    Return MessageType.info

                ElseIf _
                    text.Contains(".java:") Or text.Contains("(unknown source)") Or text.Contains("(native method)") Or
                    text.Contains("(sourcefile:") Or
                    text.Contains("java.lang.") Or text.Contains("java.util") Then 'Detect java error stacktrace
                    Return MessageType.javastacktrace
                Else
                    Return MessageType.info 'Even if it's unknown, we'll mark it as info.
                    ' If text.Length > 0 AndAlso Regex.IsMatch(text, "\d{0,4} {0,1}\w{4,24}") Then
                    ' Return MessageType.info
                    'Else
                    ' Return MessageType.unknown
                    'End If
                End If
            Catch ex As Exception
                Log(loggingLevel.Severe, "ServerOutputHandler", "Severe exception at GetMessageType!",
                    ex.Message)
                Return MessageType.unknown
            End Try
        End Function


        ''' <summary>
        '''     Fix issues due to new text formatting in 1.7.2
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FixTextCompat(text As String) As String
            text = RemoveTimeStamp(text)
            ' text = Regex.Replace(text, "\[(\d(-|:|\s|/|))*(am|pm|)(\s|)\]", "")
            text = Regex.Replace(text, "\[\d\d\]", "")
            text = Regex.Replace(text, "\[Server (shutdown |)thread\/INFO\](\:|)", "[INFO]", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "\[Server (shutdown |)thread\/WARN\](\:|)", "[WARNING]", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "\[Server (shutdown |)thread\/SEV\](\:|)", "[SEVERE]", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "\[INFO\](\:|)", "[INFO]", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "\[WARN\](\:|)", "[WARNING]", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "^WARN\s", "[WARNING]", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "\[(SEV|ERROR)\](\:|)", "[SEVERE]", RegexOptions.IgnoreCase)
            Return text
        End Function


        ''' <summary>
        '''     Remove the timestamp in front of the text, compatible with 1.7.2 output
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks>Works for all formats</remarks>
        Public Function RemoveTimeStamp(text As String) As String
            text = Regex.Replace(text, "^(\d{1,3}(-|:|\s|/|,|)){2,4}(am|pm|)\s{0,1}", "", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "^\[(\d(-|:|\s|/|))*(am|pm|)\]\s{0,1}", "", RegexOptions.IgnoreCase)
            text = Regex.Replace(text, "\[(\d{1,3}(-|:|\s|/|,|)){2,4}(am|pm|)\s", "[", RegexOptions.IgnoreCase)
            Return text
        End Function


        ''' <summary>
        '''     Convert a string into an enum value
        ''' </summary>
        ''' <param name="text">String to be converted in enum</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ParseMessageType(text As String) As MessageType
            text = text.ToLower.Trim.Replace(" ", "_")
            Return DirectCast([Enum].Parse(GetType(MessageType), text), MessageType)
        End Function


        ''' <summary>
        '''     Get the color corresponding to the text type, the text type will be detected first
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getMessageColor(text As String) As Color
            Try
                Select Case getMessageType(text)
                    Case MessageType.info
                        Return clrInfo
                    Case MessageType.playerjoin
                        Return clrPlayerEvent
                    Case MessageType.list
                        Return clrInfo
                    Case MessageType.playerleave
                        Return clrPlayerEvent
                    Case MessageType.playerkick
                        Return clrPlayerEvent
                    Case MessageType.playerban
                        Return clrPlayerEvent
                    Case MessageType.ipban
                        Return clrPlayerEvent
                    Case MessageType.warning
                        Return clrWarning
                    Case MessageType.severe
                        Return clrSevere
                    Case MessageType.javastacktrace
                        Return clrSevere
                    Case Else
                        Return clrUnknown
                End Select
            Catch ex As Exception
                Log(loggingLevel.Severe, "ServerOutputHandler",
                    "Severe exception at getMessageColor! (text as String)", ex.Message)
                Return Color.Black
            End Try
        End Function


        ''' <summary>
        '''     Get the color corresponding to the given type
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getMessageColor(type As MessageType) As Color
            Try
                Select Case type
                    Case MessageType.info
                        Return clrInfo
                    Case MessageType.playerjoin
                        Return clrPlayerEvent
                    Case MessageType.list
                        Return clrInfo
                    Case MessageType.playerleave
                        Return clrPlayerEvent
                    Case MessageType.playerkick
                        Return clrPlayerEvent
                    Case MessageType.playerban
                        Return clrPlayerEvent
                    Case MessageType.ipban
                        Return clrPlayerEvent
                    Case MessageType.warning
                        Return clrWarning
                    Case MessageType.severe
                        Return clrSevere
                    Case MessageType.javastacktrace
                        Return clrSevere
                    Case Else
                        Return clrUnknown
                End Select
            Catch ex As Exception
                Log(loggingLevel.Severe, "ServerOutputHandler",
                    "Severe exception at getMessageColor! (type as MessageType)", ex.Message)
                Return Color.Black
            End Try
        End Function


        ''' <summary>
        '''     Handles the list output, and add/remove players to fix bugs
        ''' </summary>
        ''' <param name="text">the /list output</param>
        ''' <returns></returns>
        ''' <remarks>This function will also raise the needed events to update the UI</remarks>
        Public Function HandleList(text As String) As Boolean
            Try
                If running = False Then Return True : Exit Function 'if server isn't running, get out of here
                If playerList Is Nothing OrElse playerNameList Is Nothing Then _
                    Return False : Exit Function 'if playerlist isn't set to an object, exit
                Dim Plist As List(Of String) = playerNameList ' get the current player list from the GUI
                Dim olist As List(Of String) = ResolvePlayerList(text) 'real player list in server

                If olist Is Nothing OrElse Plist Is Nothing Then Return False : Exit Function _
                'if something went wrong, exit
                Log(loggingLevel.Fine, "ServerOutputHandler", "Checking player lists...")
                Log(loggingLevel.Fine, "ServerOutputHandler",
                    "Players online now:" & Serialize(olist, ";"))
                'Check 1: Duplicate Items
                Log(loggingLevel.Fine, "ServerOutputHandler",
                    "Checking lists (1/4) - players that are show multiple times")

                If Plist.Count > 1 Then _
'when more than one player is shown, we need to check if the same player is shown twice
                    For i As Byte = 0 To Plist.Count - 1
                        If i = Plist.Count Then Exit For
                        Dim Player As String = Plist(i)
                        If Player IsNot Nothing Then
                            While Plist.IndexOf(Player) <> Plist.LastIndexOf(Player) _
                                'if first and last occurence are different: 2 or more items. While loop, to make sure all incorrect occurences are removed
                                Log(loggingLevel.Fine, "ServerOutputHandler",
                                    "Removing player " & Player & " - shown more than 1 time")
                                Plist.RemoveAt(Plist.LastIndexOf(Player))
                                'IMPORTANT:
                                '
                                'player new simpleplayer(player) should be removed
                                RaiseEvent _
                                    PlayerDisconnect(New PlayerDisconnectEventArgs(New SimplePlayer(Player),
                                                                                   PlayerDisconnectEventArgs.
                                                                                      playerleavereason.listupdate, ""))

                            End While
                        End If
                    Next
                Else
                    Log(loggingLevel.Fine, "ServerOutputHandler",
                        "Skipped duplicate player check, there are less than 2 players shown.",
                        "ServerOutputHandler")
                End If

                Log(loggingLevel.Fine, "ServerOutputHandler",
                    "Checking lists (2/4) - Disconnected players are still shown", "ServerOutputHandler")
                'Check 2: Disconnected players are still shown.
                If Plist.Count > 0 Then
                    For i As Byte = 0 To Plist.Count - 1
                        If i = Plist.Count Then Exit For
                        Dim Player As String = Plist(i)
                        If Player IsNot Nothing AndAlso Not olist.Contains(Player) Then
                            'IMPORTANT:
                            '
                            'player new simpleplayer(player) should be removed
                            Log(loggingLevel.Fine, "ServerOutputHandler",
                                "Removing player " & Player & " - still shown while disconnected",
                                "ServerOutputHandler")
                            RaiseEvent _
                                PlayerDisconnect(New PlayerDisconnectEventArgs(New SimplePlayer(Player),
                                                                               PlayerDisconnectEventArgs.
                                                                                  playerleavereason.listupdate, ""))

                        End If
                    Next
                End If
                Log(loggingLevel.Fine, "ServerOutputHandler",
                    "Checking lists (3/4) - Online players aren't shown")
                'Check 3: Players that aren't shown
                If olist.Count > 0 Then
                    For i As Byte = 0 To olist.Count - 1
                        If i = olist.Count Then Exit For
                        Dim onlinePlayer As String = olist(i)

                        If Plist.Contains(onlinePlayer.Trim) = False Or Plist.IndexOf(onlinePlayer.Trim) = -1 Then

                            Log(loggingLevel.Fine, "ServerOutputHandler",
                                "Player not shown: " & onlinePlayer & " - Adding now...")
                            'Routine to set all details. Ip will be set to unknown. Same routine as used in the player join detection
                            Dim pl As Player = Player.FromSimplePlayer(New SimplePlayer(onlinePlayer, "Unknown"))
                            If pl IsNot Nothing Then
                                pl.OP = IsOP(pl.name)
                                pl.WhiteList = IsWhitelisted(pl.name)
                            End If
                            'IMPORTANT:
                            '
                            'player pl should be added
                            RaiseEvent _
                                PlayerJoin(New PlayerJoinEventArgs(PlayerJoinEventArgs.playerjoinreason.listupdate, ""))
                            'other player details, like face and location should be downloaded on other thread, and the GUI should be updated later.

                        End If
                    Next
                End If

                Log(loggingLevel.Fine, "ServerOutputHandler",
                    "Checking lists (4/4) - detected players aren't shown in playerlist")
                'Check 4: Players that aren't shown
                If olist IsNot Nothing Then RaiseEvent CheckUILists(olist) _
                'this event will trigger the mainform to compare online players to shown players

                'End of checks
                Log(loggingLevel.Fine, "ServerOutputHandler", "Checked Lists succesfully")
            Catch ex As Exception
                Log(loggingLevel.Warning, "ServerOutputHandler", "ERROR: could not complete list update",
                    ex.Message)
            End Try
            Log(loggingLevel.Fine, "ServerOutputHandler", "Finalized list check")
            Return True
        End Function


        ''' <summary>
        '''     Resolve the /list output into a list of the current online players
        ''' </summary>
        ''' <param name="text">The text to resolve, output of /list</param>
        ''' <returns>The list of all online players ATM</returns>
        ''' <remarks></remarks>
        Private Function ResolvePlayerList(text As String) As List(Of String) _
'resolve server player list output to list of playernames
            If text Is Nothing Then Return Nothing : Exit Function
            If Regex.IsMatch(text, "^\d{1,4}(-|:|\s|/)") Then text = RemoveTimeStamp(text)
            text = Regex.Replace(text, "\s{0,1}\[Minecraft[^\]]*\]", "") _
            '[minecraft], [minecraft-server] will also be filtered out,
            text = Regex.Replace(text, "\[[\w\d]*\]", "") 'Remove prefixes like [Admin]
            text = Regex.Replace(text, "[\w\s]*(\s|):", "") 'remove "connected players:"
            Dim names As New List(Of String)
            For Each item As String In text.Trim().Split(",")
                Dim handled As String = ""
                handled = Regex.Replace(item, "(\[(.*)\]|[^A-Za-z0-9_-])", "")
                If handled <> "" Then names.Add(handled)
            Next
            Return names
        End Function

        Public Function rewrite_date(text As String) As String
            Try
                text = RemoveTimeStamp(text)
                If Show_time Then text = Date.Now.ToLongTimeString & " " & text
                If Show_date Then text = Date.Now.ToShortDateString & " " & text
                Return text
            Catch ex As Exception
                Return text
                Log(loggingLevel.Severe, "ServerOutputHandler",
                    "Something went wrong while handling rewriting the date - text:" & text, ex.Message)
            End Try
        End Function
    End Module


    '======================================================
    'Code to get player names and info from server messages

    Public Module ServerActionsFilter
        ''' <summary>
        '''     Things a player could do
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum PlayerAction
            player_join
            player_leave
            player_kick
            player_ban
            ip_ban
        End Enum


        ''' <summary>
        '''     Parse the text for a specified action, retrieve the details
        ''' </summary>
        ''' <param name="action">The action we're parsing</param>
        ''' <param name="text">The text (console output) of the action</param>
        ''' <returns></returns>
        ''' <remarks>We need to parse both different server versions and different servers (vanilla, bukkit, ...)!</remarks>
        Public Function AnalyzeAction(action As PlayerAction, text As String) As Object _
'analyze a player action. Note: the kind of action (join,leave,...) should be determined first
            Try
                text = RemoveTimeStamp(text) 'Remove any date or timestamps in front, to lowercase
                text = text.Replace("[INFO]", "")
                text = Regex.Replace(text, "\[[\w\d]*\]", "") 'Remove prefixes like [Admin]
                text = Regex.Replace(text, "\s{0,1}\[Minecraft[^\]]*\]", "") _
                '[minecraft], [minecraft-server] will also be filtered out,
                Dim match As Match
                Dim trimarray As Char() = {"[", "]", "(", ")", " ", "."}

                Select Case action
                    Case PlayerAction.player_join
                        'bot[/127.0.0.1:61159] logged in with entity id 289 at ([world] 30.5, 61.0, 399.5)
                        'bertware [/127.0.0.1:51417] logged in with entity id 401 at ([world] 309.713105541731, 63.0, 309.018407580967)
                        'domin8err logged in with entity id 3014 at (-223.54209738248414, 45.0, -50.65845467659237)
                        Dim pj As New PlayerJoin

                        'Filter for player joins (Player name and IP address)
                        '[INFO] bertware [/127.0.0.1:51417] logged in with entity id 401 at ([world] 309.713105541731, 63.0, 309.018407580967)

                        Try
                            match = Regex.Match(text,
                                                "\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3}")
                            If match IsNot Nothing AndAlso match.Value IsNot Nothing Then pj.player.IP = match.Value _
                                Else pj.player.IP = "unknown"
                            match = Regex.Match(text, "^\s{0,1}\w{1,16}\s{0,1}")
                            If match IsNot Nothing AndAlso match.Value IsNot Nothing Then _
                                pj.player.name = match.Value.Trim(trimarray) Else pj.player.name = "unknown"
                        Catch ex As Exception
                            Log(loggingLevel.Warning, "ServerOutputHandler",
                                "Could not get login player name/ip for text " & text, ex.Message)
                        End Try

                        pj.message = text

                        If pj.player.name IsNot Nothing Then
                            pj.player.OP = IsOP(pj.player.name)
                            pj.player.WhiteList = IsWhitelisted(pj.player.name)
                        End If

                        Return pj
                    Case PlayerAction.player_leave
                        '[INFO] player lost connection: disconnect.quitting
                        Dim pl As New PlayerLeave

                        Try
                            pl.player = New SimplePlayer()
                            match = Regex.Match(text, "^\s{0,1}\w{1,16}\s{0,1}")
                            If match IsNot Nothing AndAlso match.Value IsNot Nothing Then _
                                pl.player.name = match.Value.Trim(trimarray) Else pl.player.name = "unknown"
                        Catch ex As Exception
                            Log(loggingLevel.Warning, "ServerOutputHandler",
                                "Could not get disconnected player for text " & text, ex.Message)
                        End Try

                        Try
                            If text.Contains(":") Then pl.reason = text.Split(":")(1).Trim Else pl.reason = "unknown" _
                            'get additional details
                        Catch ex As Exception
                            Log(loggingLevel.Warning, "ServerOutputHandler",
                                "Could not get disconnected reason for text " & text, ex.Message)
                        End Try

                        Return pl
                    Case PlayerAction.player_kick
                        '14:42:36 [INFO] CONSOLE: Kicking bertware
                        '[INFO] Kicked Bertware from the game: 'zomaar'

                        '2013-06-20 19:11:35 [INFO] CONSOLE: Kicked player Bertware. With reason:
                        'zomaar, dit is de reden
                        '

                        Dim pk As New PlayerKick

                        Try

                            If Regex.IsMatch(text, "Kicked \w{1,16} from the game") Then _
'[INFO] Kicked Bertware from the game: 'zomaar'

                                pk.CommandSender = "unknown"

                                match = Regex.Match(text, "Kicked \w{1,16} ")
                                If match IsNot Nothing AndAlso match.Value IsNot Nothing Then
                                    pk.player.name = match.Value.Split(" ")(1).Trim(trimarray)
                                Else
                                    pk.player.name = "unknown"
                                End If

                            ElseIf Regex.IsMatch(text, "\w{1,16}: Kicking \w{1,16}") Then _
'14:42:36 [INFO] CONSOLE: Kicking bertware
                                match = Regex.Match(text, "^\s{0,1}\w{1,16}")
                                If match IsNot Nothing AndAlso match.Value IsNot Nothing Then _
                                    pk.CommandSender = match.Value.Trim(trimarray) Else pk.CommandSender = "unknown"

                                match = Regex.Match(text, "\w{1,16}\s{0,1}$")
                                If match IsNot Nothing AndAlso match.Value IsNot Nothing Then _
                                    pk.player.name = match.Value.Trim(trimarray) Else pk.player.name = "unknown"

                            ElseIf Regex.IsMatch(text, "\w{1,16}: Kicked player \w{1,16}") Then _
'2013-06-20 19:11:35 [INFO] CONSOLE: Kicked player Bertware. With reason:
                                match = Regex.Match(text, "^\s{0,1}\w{1,16}")
                                If match IsNot Nothing AndAlso match.Value IsNot Nothing Then _
                                    pk.CommandSender = match.Value.Trim(trimarray) Else pk.CommandSender = "unknown"

                                match = Regex.Match(text, "\w{1,16}\.")
                                If match IsNot Nothing AndAlso match.Value IsNot Nothing Then _
                                    pk.player.name = match.Value.Trim(trimarray) Else pk.player.name = "unknown"
                            End If

                        Catch ex As Exception
                            Log(loggingLevel.Warning, "ServerOutputHandler",
                                "Could not get kick information for text " & text, ex.Message)
                        End Try

                        Return pk
                    Case PlayerAction.player_ban
                        '14:42:36 [INFO] CONSOLE: Banning bertware
                        '14:42:36 [INFO] CONSOLE: Banned player bertware
                        Dim pb As New playerBan
                        If text.Contains("Banning") Then
                            Try
                                pb.player = New SimplePlayer(text.Split("]")(1).Trim.Split(" ")(2)) 'get player
                                pb.CommandSender = text.Split("]")(1).Split(":")(0).Trim 'get additional details
                            Catch ex As Exception
                                Log(loggingLevel.Warning, "ServerOutputHandler",
                                    "Could not get player ban information for text " & text, ex.Message)
                            End Try
                        ElseIf text.Contains("Banned player") Then
                            Try
                                pb.player = New SimplePlayer(text.Split("]")(1).Trim.Split(" ")(3)) 'get player
                                pb.CommandSender = text.Split("]")(1).Split(":")(0).Trim 'get additional details
                            Catch ex As Exception
                                Log(loggingLevel.Warning, "ServerOutputHandler",
                                    "Could not get player ban information for text " & text, ex.Message)
                            End Try
                        End If

                        Return pb
                    Case PlayerAction.ip_ban
                        '19:19:13 [INFO] CONSOLE: Banning ip 127.0.0.1
                        Dim ib As New IPBan
                        Try
                            ib.IP = text.Split("]")(1).Split(":")(1).Trim.Split(" ")(2).Trim 'get details
                            ib.CommandSender = text.Split("]")(1).Split(":")(0).Trim 'get details
                        Catch ex As Exception
                            Log(loggingLevel.Warning, "ServerOutputHandler",
                                "Could not get player ban information for text " & text, ex.Message)
                        End Try
                        Return ib
                    Case Else
                        Return Nothing
                End Select
            Catch ex As Exception
                Log(loggingLevel.Severe, "ServerOutputHandler", "Severe exception at AnalyzeAction!",
                    ex.Message)
                Return Nothing
            End Try
        End Function
    End Module


    ''' <summary>
    '''     These classes will be returned along with the event
    ''' </summary>
    ''' <remarks></remarks>
#Region "EventClasses"
    Public Class PlayerJoin
        Public player As Player, message As String

        Public Sub New()
            player = New Player
            message = ""
        End Sub

        Public Sub New(player As SimplePlayer)
            Me.player = MCInterop.Player.FromSimplePlayer(player)
            Me.message = ""
        End Sub

        Public Sub New(player As Player)
            Me.player = player
            Me.message = ""
        End Sub
    End Class

    Public Class PlayerLeave
        Public player As SimplePlayer, reason As String

        Public Sub New()
            player = New SimplePlayer
            reason = ""
        End Sub

        Public Sub New(Splayer As SimplePlayer)
            player = Splayer
            reason = ""
        End Sub

        Public Sub New(Splayer As SimplePlayer, DisconnectReason As String)
            player = Splayer
            reason = DisconnectReason
        End Sub
    End Class

    Public Class PlayerKick
        Public player As SimplePlayer, CommandSender As String

        Public Sub New()
            player = New SimplePlayer
            CommandSender = ""
        End Sub

        Public Sub New(Splayer As SimplePlayer)
            player = Splayer
            CommandSender = ""
        End Sub

        Public Sub New(Splayer As SimplePlayer, sender As String)
            player = Splayer
            CommandSender = sender
        End Sub
    End Class

    Public Class playerBan
        Public player As SimplePlayer, CommandSender As String

        Public Sub New()
            player = New SimplePlayer
            CommandSender = ""
        End Sub

        Public Sub New(Splayer As SimplePlayer)
            player = Splayer
            CommandSender = ""
        End Sub

        Public Sub New(Splayer As SimplePlayer, sender As String)
            player = Splayer
            CommandSender = sender
        End Sub
    End Class

    Public Class IPBan
        Public IP As String, CommandSender As String

        Public Sub New()
            IP = ""
            CommandSender = ""
        End Sub

        Public Sub New(BannedIP As String)
            IP = BannedIP
            CommandSender = ""
        End Sub

        Public Sub New(BannedIp As String, sender As String)
            IP = BannedIp
            CommandSender = sender
        End Sub
    End Class

#End Region
End Namespace