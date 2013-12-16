Imports com.ramblingwood.minecraft.jsonapi
Imports System.Threading
Imports System.IO
Imports Jayrock.Json.Conversion
Imports Jayrock.Json
Imports Net.Bertware.BukkitGUI.Core
Imports System.Net.Sockets

Namespace MCInterop
    ''' <summary>
    ''' A class for remote server communication with the JSONAPI plugin. Inherits from RemoteServerBase.vb and imports the JSONAPI.dll
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Class RemoteJSONAPI
        Inherits RemoteServerBase
        Private api As JSONAPI
        Private thd_connection_recv As Thread
        Private thd_connection_send As Thread
        Private running As Boolean = False


        Public Overrides Sub Run()
            MyBase.Run()
            api = New JSONAPI(Me.Credentials.Host, Me.Credentials.port, Me.Credentials.login, Me.Credentials.password,
                              Me.Credentials.salt)
            running = True
            thd_connection_recv = New Thread(AddressOf run_connection_receive)
            thd_connection_recv.IsBackground = True
            thd_connection_recv.Name = "Remote_JSONAPI_receive_" & Me.Credentials.Host
            thd_connection_recv.Start()
            thd_connection_send = New Thread(AddressOf run_connection_send)
            thd_connection_send.IsBackground = True
            thd_connection_send.Name = "Remote_JSONAPI_send_" & Me.Credentials.Host
            thd_connection_send.Start()
            livebug.write(livebug.loggingLevel.Fine, "RemoteJSONAPI", "Remote server started...")
        End Sub

        Public Overrides Sub Close()
            livebug.write(loggingLevel.Fine, "RemoteJSONAPI", "Remote server stopping...")

            MyBase.Close()

            running = False
            Thread.Sleep(10)
            livebug.write(loggingLevel.Fine, "RemoteJSONAPI", "Remote server stopped")
        End Sub

        Private Sub run_connection_send()
            livebug.write(loggingLevel.Fine, "RemoteJSONAPI", "Remote sender thread started...")
            Try
                While running AndAlso server.running AndAlso Me.StandardIn IsNot Nothing
                    If StandardIn.EOS = False Then
                        Dim cmd As String = StandardIn.read
                        If cmd Is Nothing OrElse cmd = "" Then Continue While
                        cmd = cmd.Replace("&", "")
                        cmd = cmd.Replace("""", "'")
                        AdvancedWebClient.downloadstring(
                            "http://" & Me.Credentials.Host & ":" & Me.Credentials.port &
                            "/api/call?method=runConsoleCommand&args=%5B%22" & cmd & "%22%5D&key=" &
                            api.createKey("runConsoleCommand"), True)
                    End If
                    Thread.Sleep(10)
                End While
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "RemoteJSONAPI", "Severe exception at run_connection_send! ",
                              ex.Message)
            End Try
            livebug.write(loggingLevel.Fine, "RemoteJSONAPI", "Remote sender thread stopped...")
            If running = True Then Me.Close()
        End Sub

        Private Sub run_connection_receive()
            livebug.write(loggingLevel.Fine, "RemoteJSONAPI", "Remote receiver thread started...")
            Try
                Dim _
                    sock As _
                        New Socket(System.Net.Sockets.AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                Try
                    sock.Connect(Me.Credentials.Host, Me.Credentials.port + 1)
                Catch con_ex As Exception
                    MessageBox.Show(lr("Failed to connect to remote server. Are the credentials correct?"),
                                    lr("Failed to connect"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    livebug.write(loggingLevel.Warning, "RemoteJSONAPI",
                                  "Failed to connect to remote server. Are the credentials correct? ", con_ex.Message)
                End Try

                If sock.Connected Then
                    Dim key As String = api.createKey("console")
                    Dim stream As New NetworkStream(sock)
                    Dim sw As New StreamWriter(stream)
                    sw.WriteLine("/api/subscribe?source=console&key=" & key)
                    sw.Flush()
                    Dim sr As New StreamReader(stream)

                    While _
                        running AndAlso server.running AndAlso sock.Connected AndAlso stream IsNot Nothing AndAlso
                        Me.StandardOut IsNot Nothing AndAlso sr IsNot Nothing

                        Dim l As String = ""
                        Try
                            While _
                                running AndAlso server.running AndAlso sock.Connected AndAlso stream IsNot Nothing AndAlso
                                Me.StandardOut IsNot Nothing AndAlso sr IsNot Nothing AndAlso sr.EndOfStream
                                Thread.Sleep(100)
                            End While
                            If sr IsNot Nothing Then l = sr.ReadLine()
                        Catch readex As Exception
                            livebug.write(loggingLevel.Warning, "RemoteJSONAPI",
                                          "exception at run_connection_receive, while reading from networkstream " &
                                          readex.Message) 'don't flag as critical error
                        End Try

                        If _
                            l IsNot Nothing AndAlso l <> "" AndAlso l.Contains("{") And l.Contains(":") And
                            l.Contains("}") AndAlso Me.StandardOut IsNot Nothing Then
                            l = New JSONAPI_Stream_result(l).line
                            If Me.StandardOut IsNot Nothing AndAlso l IsNot Nothing Then Me.StandardOut.write(l)
                        End If

                        Thread.Sleep(10)
                    End While
                End If

                If sock IsNot Nothing AndAlso sock.Connected Then sock.Close()
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "RemoteJSONAPI", "Exception at run_connection_receive!", ex.Message)
            End Try
            livebug.write(loggingLevel.Fine, "RemoteJSONAPI", "Remote receiver thread stopped...")
            If running = True Then Me.Close()
        End Sub
    End Class

    Public Class JSONAPI_Stream_result
        Public result As String, source As String, time As String, line As String

        Public Sub New(text As String)
            Dim obj As JsonObject = JsonConvert.Import(text)
            result = obj("result").ToString
            source = obj("source").ToString
            If result = "success" Then
                Dim sl_obj As JsonObject = JsonConvert.Import(obj("success").ToString)
                time = sl_obj("time").ToString
                line = sl_obj("line").ToString.Trim("/r").Trim("/n").Trim("/r")
            End If
        End Sub
    End Class
End Namespace