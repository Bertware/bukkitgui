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

'module to handle ALL interaction with bertware.net
'from Bukkitgui v1.0, server interaction is done in XML. This is in order to allow extensions, without breaking older versions. 
Imports System.IO
Imports System.Net
Imports Net.Bertware.BukkitGUI.Utilities

Namespace Core
    Module serverinteraction
        Const bukkitgui_api As String = "http://bukkitgui.bertware.net/api/1.1/bukkitgui.php"
        Const geoip_api As String = "http://geoip.bertware.net/"

        Public Const translations_list As String = "http://bukkitgui.bertware.net/api/1.1/translations.xml"

        Public ReadOnly mail As String = "contact@bertware.net"

#If DEBUG Then
        Public ReadOnly UA As String = "Bertware 1.3/" & My.Application.Info.AssemblyName & " " & My.Application.Info. _
Version.ToString & " DEBUG/" & mail
#Else

        Public ReadOnly _
            UA As String = "Bertware 1.3/" & My.Application.Info.AssemblyName & " " &
                           My.Application.Info.Version.ToString & "/" & mail

#End If


        Public Event IP_Loaded(ip As String)

        Private _ip As String

        
        ''' <summary>
        '''     The IP that was retrieved on the last GetIP. If cache is emtpy, GetIP() wil be ran.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property external_ip_chache As String
            Get
                If _ip IsNot Nothing Then Return _ip Else Return getIp()
            End Get
        End Property

        Public ReadOnly Property header As WebHeaderCollection
            Get
                Dim c As New WebHeaderCollection
                c.Add(HttpRequestHeader.UserAgent, UA)
                c.Add(HttpRequestHeader.From, mail)
                'c.Add(HttpRequestHeader.Authorization, "bertware:bukkitgui")
                Return c
            End Get
        End Property

        Public Function getIp() As String
            Try
                Dim ip = downloadstring(geoip_api & "me")
                If ip Is Nothing Then ip = "unknown"
                _ip = ip
                Return ip
            Catch ex As Exception
                Log(loggingLevel.Warning, "ServerInteraction", "Exception at getIP! " & ex.Message)
                Return "error"
            End Try
        End Function

        Public Function getPlayerLocation(ip As String) As String
            Try
                Dim result = downloadstring(geoip_api & ip)
                If result Is Nothing Then result = "unknown"
                Return result
            Catch ex As Exception
                Log(loggingLevel.Severe, "ServerInteraction", "Severe exception at getPlayerLocation!",
                    ex.Message)
                Return "error"
            End Try
        End Function

        Public Function getPlayerMinotar(player As String) As Image
            'Gets the player face for a certain playername
            'Send request, and get the image from the stream
            'using bertware.net as a compatibility layer towards minotar.net

            Log(loggingLevel.Fine, "ServerInteraction", "Getting minotar for " & player)
            Dim img As Image = My.Resources.player_face

            If My.Computer.Network.IsAvailable = False Then
                Log(loggingLevel.Warning, "ServerInteraction",
                    "Error: could not get Minotar. No internet available. Returning default")
                img = My.Resources.player_face 'use default
                Return img
                Exit Function
            End If

            Try
                Dim url As String = MinotarSource & "/helm/" & player & "/" &
                                    MinotarSize & ".png"
                Log(loggingLevel.Fine, "ServerInteraction", "Minotar Url: " & url)

                Dim webreq As HttpWebRequest

                webreq = WebRequest.Create(New Uri(url)) 'Download to temporary location

                Dim ck As New CookieContainer
                webreq.CookieContainer = ck

                webreq.UserAgent = UA 'set useragent - currently onyl for statistics
                Dim resp As WebResponse = webreq.GetResponse() 'get the response from the server

                If resp Is Nothing Then
                    Log(loggingLevel.Warning, "ServerInteraction",
                        "Could not get Minotar.Returned response is null")
                    img = My.Resources.player_face 'use default
                    Return img
                    Exit Function
                End If

                Dim imgstream As Stream = resp.GetResponseStream()

                If imgstream Is Nothing OrElse imgstream.Equals(Stream.Null) Then
                    Log(loggingLevel.Warning, "ServerInteraction",
                        "Could not get Minotar.Returned stream is null")
                    img = My.Resources.player_face 'use default
                    Return img
                    Exit Function
                End If

                img = Image.FromStream(imgstream) 'Get image from file
            Catch ex As Exception
                Log(loggingLevel.Warning, "ServerInteraction",
                    "Could not get Minotar. Probably no internet is available, or an invalid image was received. " &
                    ex.Message)
                img = My.Resources.player_face 'use default
            End Try
            Log(loggingLevel.Fine, "ServerInteraction", "Function done... Returning result")
            Trace.Unindent()
            Return img
        End Function


        Public Function Contact(sender As String, subject As String, content As String) As Boolean
            Try
                Dim res As String =
                        downloadstring(
                            bukkitgui_api & "?action=contact&mail=" & sender & "&subject=" & subject & "&msg=" & content)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Module

    Module AdvancedWebClient
        Public Function downloadstring(url As String, Optional ByVal silent As Boolean = False) As String
            Try
                If url Is Nothing Then Return "" : Exit Function

#If DEBUG Then
                Debug.WriteLine("Retrieving contents from " & url)
#End If
                Dim baseURL As String = ""
                Try
                    If url.StartsWith("http://") Then baseURL = url.Substring(7).Split("/").First Else _
                        baseURL = url.Split("/").First
                    Log(loggingLevel.Fine, "AdvancedWebClient", "Retrieving contents from " & baseURL)
                Catch logex As Exception
                    baseURL = url
                End Try


                If My.Computer.Network.IsAvailable = False Then
                    Log(loggingLevel.Fine, "AdvancedWebClient",
                        "Request to " & baseURL & " cancelled, no internet available or page not available")
                    Return ""
                    Exit Function
                End If

                If silent = True Then baseURL = "[silent url]" 'hide silent url's

                Dim httpreq As HttpWebRequest
                httpreq = HttpWebRequest.Create(url)
                httpreq.Timeout = 10000
                httpreq.Proxy = Nothing
                httpreq.UserAgent = UA
                Dim responseStr = ""
                Using sr As StreamReader = New StreamReader(httpreq.GetResponse.GetResponseStream)
                    responseStr = sr.ReadToEnd
                End Using
                Return responseStr

                'Dim webc As WebClient
                'webc = New System.Net.WebClient
                'webc.Headers.Add(HttpRequestHeader.UserAgent, UA)
                'Return webc.DownloadString(url)
            Catch tm As TimeoutException
                Log(loggingLevel.Warning, "AdvancedWebClient",
                    "Could not download data from " & url & " : Timed Out : " & tm.Message)
                Return ""
            Catch webex As WebException
                If webex.Message.Contains("503") Then
                    Log(loggingLevel.Warning, "AdvancedWebClient",
                        "Could not download data from " & url & " : WebException/503 : " & webex.Message)
                    Return ""
                ElseIf webex.Message.Contains("502") Then
                    Log(loggingLevel.Warning, "AdvancedWebClient",
                        "Could not download data from " & url & " : WebException/502 : " & webex.Message)
                    Return ""
                ElseIf webex.Message.Contains("timed out") Then
                    Log(loggingLevel.Warning, "AdvancedWebClient",
                        "Could not download data from " & url & " : WebException/TimedOut : " & webex.Message)
                    Return ""
                Else
                    Log(loggingLevel.Warning, "AdvancedWebClient",
                        "Could not download data from " & url & " : WebException/Unknown : " & webex.Message)
                    Return ""
                End If
            Catch ex As Exception
                Log(loggingLevel.Warning, "ServerInteraction",
                    "Could not download data from " & url & " : " & ex.Message)
                Return ""
            End Try
        End Function
    End Module
End Namespace