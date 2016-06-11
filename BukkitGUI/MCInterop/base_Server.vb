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



'This module will handle the minecraft server.
'Most important: the process (public property host) and the streams, as those are needed
'Whatever method is used, the streams should be available at serverin, serverout and servererror
'If the GUI hosts the server connection itself (for example built-in remote support, instead of classic java server) the host should point at the current process.

'Other things: Time since start, events for easy handling, player list
Imports System.ComponentModel
Imports System.IO
Imports System.Security
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Core
Imports Timer = System.Timers.Timer

Namespace MCInterop
    Public Module server
        Public Enum McInteropType
            'these are for other classes
            bukkit 'bukkit server
            vanilla 'vanilla server

            'These 3 are used for the server.Currentype
            remote 'remote server
            exexcutable 'general executable server
            java 'general java server
            spigot
        End Enum

        Private _running As Boolean,
                _serverout As Stream,
                _serverin As Stream,
                _servererror As Stream,
                _timerunning As TimeSpan,
                _playerlist As List(Of Player)

        Private _current_type As McInteropType
        Private WithEvents tmrcounttime As Timers.Timer
        Private WithEvents _host As Process
        Private csw As StreamWriter

        Private _remote_obj As RemoteServerBase


        ''' <summary>
        '''     Indicates if the server is running or not
        ''' </summary>
        ''' <value>Boolean to indicate if the server is running or not</value>
        ''' <returns>Boolean to indicate if the server is running or not</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property running As Boolean 'boolean to show if server is running or not
            Get
                Return _running
            End Get
        End Property


        ''' <summary>
        '''     The process that hosts the server
        ''' </summary>
        ''' <value>the process object</value>
        ''' <returns>the process object</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property host As Process 'process that hosts the server
            Get
                Return _host
            End Get
        End Property


        ''' <summary>
        '''     The standardOut stream of the process
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>Bukkit outputs data using both standardout and standarderror</remarks>
        Public ReadOnly Property serverOut As Stream 'server out stream, to receive output
            Get
                Return _serverout
            End Get
        End Property


        ''' <summary>
        '''     The standardError stream of the server
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>Bukkit outputs data using both standardout and standarderror</remarks>
        Public ReadOnly Property serverError As Stream 'server error stream, to receive output
            Get
                Return _servererror
            End Get
        End Property


        ''' <summary>
        '''     The standardIn stream of the server, used for sending of commands etc.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property serverIn As Stream 'server input stream, to write commands
            Get
                Return _serverin
            End Get
        End Property


        ''' <summary>
        '''     The time that has elapsed since the start of the server
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property timeRunning As TimeSpan 'time since server started
            Get
                Return _timerunning
            End Get
        End Property


        ''' <summary>
        '''     The list of currently online players
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property playerList As List(Of Player) 'list of player names
            Get
                Return _playerlist
            End Get
            Set(value As List(Of Player))
                _playerlist = playerList
            End Set
        End Property

        Public ReadOnly Property RemoteServerObject() As RemoteServerBase
            Get
                Return _remote_obj
            End Get
        End Property

        Public ReadOnly Property CurrentServerType As McInteropType
            Get
                Return _current_type
            End Get
        End Property

        Public ReadOnly Property playerNameList As List(Of String)
            Get
                If _playerlist Is Nothing Then Return Nothing : Exit Property
                Dim l As New List(Of String)
                For Each pl As Player In _playerlist
                    l.Add(pl.name)
                Next
                Return l
            End Get
        End Property

        Public Function GetPlayerByName(name As String) As Player
            Dim p As Player = Nothing
            For Each Pl As Player In playerList
                If Pl.name = name Then p = Pl
            Next
            Return p
        End Function


        ''' <summary>
        '''     Raised at the beginning of the server start routine
        ''' </summary>
        ''' <remarks></remarks>
        Public Event ServerStarting() 'Raised when server is being initialized

        ''' <summary>
        '''     Raised when the server is fully started
        ''' </summary>
        ''' <remarks></remarks>
        Public Event ServerStarted() 'Raised when server start is finished

        ''' <summary>
        '''     Raised when the server stopped
        ''' </summary>
        ''' <remarks></remarks>
        Public Event ServerStopped() 'Raised when server stopped

        Public Event ServerStopping()


        ''' <summary>
        '''     Start a new minecraft server, using Java
        ''' </summary>
        ''' <param name="Jsa">The start information to start the server</param>
        ''' <returns>Returns the ID of the started process</returns>
        ''' <remarks></remarks>
        Public Function StartServer(Jsa As javaStartArgs, type As McInteropType) As UInt64 'return process ID
            Dim p As New Process
            Try
                _current_type = type
                Log(livebug.loggingLevel.Fine, "Server", "Starting server (java)", "server")
                Log(loggingLevel.Fine, "Server", "Executable:" & Jsa.executable, "Server-StartJavaServer")
                Log(loggingLevel.Fine, "Server", "Jar:" & Jsa.jar, "Server-StartJavaServer")
                Log(loggingLevel.Fine, "Server", "Options:" & Jsa.args, "Server-StartJavaServer")
                Log(loggingLevel.Fine, "Server", "Flags:" & Jsa.switches, "Server-StartJavaServer")
                Log(loggingLevel.Fine, "Server", "Total argument:" & Jsa.buildArgs, "Server-StartJavaServer")

                'check for valid path
                If _
                    Jsa.executable Is Nothing OrElse Jsa.executable = "" OrElse Not Jsa.executable.EndsWith(".exe") OrElse
                    FileSystem.FileExists(Jsa.executable) = False Then
                    MessageBox.Show(
                        lr("The java path could not be found, server start aborted") & vbCrLf &
                        lr("Please select the correct java version in the superstart tab"), lr("Could not start server"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Log(loggingLevel.Warning, "Server",
                        "File not found, server start cancelled:" & Jsa.executable)
                    Return 0
                    Exit Function
                End If

                'only raise after validation
                RaiseEvent ServerStarting()
                Thread.Sleep(50)

                With p.StartInfo
                    .FileName = Jsa.executable
                    .Arguments = Jsa.buildArgs
                    .CreateNoWindow = True
                    .ErrorDialog = False
                    .RedirectStandardError = True
                    .RedirectStandardInput = True
                    .RedirectStandardOutput = True
                    .StandardErrorEncoding = Encoding.UTF8
                    .StandardOutputEncoding = Encoding.UTF8
                    .UseShellExecute = False
                    .WorkingDirectory = ServerRoot
                End With
                Log(loggingLevel.Fine, "Server", "Starting process")
                If p.StartInfo.FileName IsNot Nothing Then p.Start() Else _
                    MessageBox.Show(Lr("Could not start the process - no executable given"),
                                    Lr("Could not start server"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Fine, "Server", "Process started")
                _host = p
                ServerStart_Common()
                Log(loggingLevel.Fine, "Server", "Server started succesfully")
                RaiseEvent ServerStarted()
                If p IsNot Nothing AndAlso p.Id > 0 Then Return (p.Id) Else Return 0
            Catch winex As Win32Exception
                MessageBox.Show(
                    Lr(
                        "The server could not be started. It seems like you don't have permissions to access needed files. Try running the GUI as administator"),
                    Lr("File I/O error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Warning, "Server", "Security error in Startserver (java)! " & winex.Message)
                If p IsNot Nothing Then Return (p.Id) Else Return 0
            Catch ioex As IOException
                MessageBox.Show(
                    Lr(
                        "The server could not be started. It seems like you don't have permissions to access needed files. Try running the GUI as administator"),
                    Lr("File I/O error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Warning, "Server", "Security error in Startserver (java)! " & ioex.Message)
                If p IsNot Nothing Then Return (p.Id) Else Return 0
            Catch pex As SecurityException
                MessageBox.Show(
                    Lr(
                        "The server could not be started. It seems like you don't have permissions to do this. Try running the GUI as administator"),
                    Lr("Insufficient rights"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Warning, "Server", "Security error in Startserver (java)! " & pex.Message)
                If p IsNot Nothing Then Return (p.Id) Else Return 0
            Catch ex As Exception
                Log(loggingLevel.Severe, "Server", "Severe error in Startserver (java)! " & ex.Message)
                If p IsNot Nothing Then Return (p.Id) Else Return 0
            End Try
        End Function


        ''' <summary>
        '''     Start a new executable minecraft server
        ''' </summary>
        ''' <param name="esa">The start information to start the server</param>
        ''' <returns>Returns the ID of the started process</returns>
        ''' <remarks></remarks>
        Public Function StartServer(Esa As execStartArgs, type As McInteropType) As UInt16
            Try
                _current_type = type
                Log(loggingLevel.Fine, "Server", "Starting server (executable)")

                If Not FileSystem.FileExists(Esa.executable) Then
                    MessageBox.Show(
                        Lr(
                            "The program couldn't find this executable: " & Esa.executable & vbCrLf &
                            "Server start cancelled"), Lr("File not found"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Log(loggingLevel.Warning, "Server",
                        "File not found, server start cancelled:" & Esa.executable)
                    Return 0
                    Exit Function
                End If

                RaiseEvent ServerStarting()
                Thread.Sleep(50)

                Dim p As New Process
                With p.StartInfo
                    .FileName = Esa.executable
                    .Arguments = Esa.arguments
                    .CreateNoWindow = True
                    .ErrorDialog = False
                    .RedirectStandardError = True
                    .RedirectStandardInput = True
                    .RedirectStandardOutput = True
                    Console.InputEncoding = Encoding.GetEncoding(ServerEncoding)
                    .StandardErrorEncoding = Encoding.GetEncoding(ServerEncoding)
                    .StandardOutputEncoding = Encoding.GetEncoding(ServerEncoding)
                    .UseShellExecute = False
                    .WorkingDirectory = ServerRoot
                End With
                Log(loggingLevel.Fine, "Server", "Starting process")
                p.Start()
                Log(loggingLevel.Fine, "Server", "Process started")
                _host = p
                ServerStart_Common()
                Log(loggingLevel.Fine, "Server", "Server started succesfully")
                RaiseEvent ServerStarted()
                If p IsNot Nothing AndAlso p.Id > 0 AndAlso p.HasExited = False Then Return (p.Id) Else Return 0
            Catch ex As Exception
                Log(loggingLevel.Severe, "Server", "Severe error in Startserver (executable)!", ex.Message)
                Return Process.GetCurrentProcess.Id
            End Try
        End Function


        ''' <summary>
        '''     Start an embedded remote server connection
        ''' </summary>
        ''' <param name="Rm">A remoteServer class that inherits from RemoteServerBase</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function StartServer(Rm As RemoteServerBase) As UInt16
            Try
                _current_type = McInteropType.remote
                Log(loggingLevel.Fine, "Server", "Starting server (Remote)")
                RaiseEvent ServerStarting()

                _remote_obj = Rm
                _running = True

                _host = Process.GetCurrentProcess 'the server is hosted in the GUI

                'Serverstart_common can't be used as this server is ran inside the GUI
                'initialize variables for new server, set streams

                Log(loggingLevel.Fine, "Server", "Initializing handling for new remote server")
                _playerlist = New List(Of Player)
                _serverout = Nothing
                _servererror = Nothing
                _serverin = Nothing
                _timerunning = New TimeSpan(0)

                tmrcounttime = New Timer
                tmrcounttime.Interval = 1000
                tmrcounttime.AutoReset = True
                tmrcounttime.Enabled = True
                tmrcounttime.Start()
                Log(loggingLevel.Fine, "Server", "New remote server initialized")

                Rm.Run()

                csw = Nothing

                RaiseEvent ServerStarted()

                Log(loggingLevel.Fine, "Server", "Server started succesfully")
                Return Process.GetCurrentProcess.Id
            Catch ex As Exception
                Log(loggingLevel.Severe, "Server", "Severe error in Startserver (remote)!", ex.Message)
                Return Process.GetCurrentProcess.Id
            End Try
        End Function

        Public Sub StopServer()
            Log(loggingLevel.Info, "server", "StopServer() was called")
            RaiseEvent ServerStopping()
            Thread.Sleep(10) 'Allow last actions before stop, triggered by event
            If CurrentServerType = McInteropType.remote Then
                _ServerStopped()
            Else
                SendCommand("stop", True)
            End If
            Log(loggingLevel.Info, "server", "StopServer() was executed")
        End Sub

        Private Sub _ServerStopped() Handles _host.Exited
            Try
                Log(loggingLevel.Info, "server", "The server has stopped... Cleaning up...")

                If tmrcounttime IsNot Nothing Then
                    tmrcounttime.Enabled = False
                    _running = False
                End If

                _host = Nothing
                _serverout = Nothing
                _servererror = Nothing
                _serverin = Nothing
                _playerlist = Nothing
                _remote_obj = Nothing
                RaiseEvent ServerStopped()
            Catch ex As Exception
                Log(loggingLevel.Severe, "Server", "Error in _serverstopped! " & ex.Message)
            End Try
            Log(loggingLevel.Info, "server", "The server has stopped... Cleanup finished")
        End Sub


        ''' <summary>
        '''     Send a command to the server
        ''' </summary>
        ''' <param name="command">The command to be sent</param>
        ''' <param name="suspressErrors">If errors should be suspressed</param>
        ''' <remarks></remarks>
        Public Function SendCommand(command As String, Optional suspressErrors As Boolean = False) As Boolean
            Try
                If CurrentServerType <> McInteropType.remote Then
                    If _host IsNot Nothing Then
                        If csw Is Nothing AndAlso _host.Id <> Process.GetCurrentProcess.Id Then _
                            csw = _host.StandardInput

                        Dim buffer() As Byte =
                                Encoding.GetEncoding(ServerEncoding).GetBytes(command & vbCr)
                        csw.BaseStream.Write(buffer, 0, buffer.Length)
                        csw.BaseStream.Flush()

                        Return True
                    Else
                        If Not suspressErrors Then _
                            MessageBox.Show(Lr("The server should be running before you can send commands."),
                                            Lr("Could not send command"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                Else
                    If RemoteServerObject IsNot Nothing AndAlso RemoteServerObject.StandardIn IsNot Nothing Then _
                        RemoteServerObject.StandardIn.write(command) : Return True
                End If
            Catch ex As Exception
                If Not suspressErrors Then _
                    MessageBox.Show(
                        Lr("The command could not be sent due to an error. See the log file for more information"),
                        Lr("Could not send command"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Severe, "Server", "Could not send command """ & command & """ exception",
                    ex.Message)
                Return False
            End Try
            Return False
        End Function

        Private Sub ServerStart_Common()
            Try
                Log(loggingLevel.Fine, "Server", "Initializing handling for new server")
                _playerlist = New List(Of Player)
                _serverout = _host.StandardOutput.BaseStream
                _servererror = _host.StandardError.BaseStream
                _serverin = _host.StandardInput.BaseStream
                _timerunning = New TimeSpan(0)

                tmrcounttime = New Timer
                tmrcounttime.Interval = 1000
                tmrcounttime.AutoReset = True
                tmrcounttime.Enabled = True
                tmrcounttime.Start()

                csw = _host.StandardInput

                Log(loggingLevel.Fine, "Server", "New server initialized")
                _running = True
            Catch ex As Exception
                Log(loggingLevel.Severe, "Server", "Severe error in ServerStart_common! ", ex.Message)
            End Try
        End Sub

        Private Sub Counttime() Handles tmrcounttime.Elapsed
            _timerunning = _timerunning.Add(New TimeSpan(0, 0, 1))
        End Sub
    End Module
End Namespace