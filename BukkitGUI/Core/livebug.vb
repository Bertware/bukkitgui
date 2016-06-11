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

'Livebug 3.0 - Advanced logging system
'=====================================
'-Log to file
'-Ask to send errors
'(c) Bertware

'
' datestamp;timestamp;level;category;message;detail
'
Imports System.IO
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.Utilities
Imports Net.Bertware.Utilities.NetVersionDetector

Namespace Core
    Module livebug
        Enum loggingLevel
            Info
            Fine
            Warning
            Severe
            Critical
        End Enum

        Public log_file As String = LoggingPath & "/BukkitGUI.log"
        Dim tls As TextWriterTraceListener
        Dim fs As Stream
        Dim sw As StreamWriter
        Dim initialized As Boolean = False

        
        ''' <summary>
        '''     Initializes livebug
        ''' </summary>
        ''' <remarks>This routine should be executed first.</remarks>
        Public Sub init()
            log_file = LoggingPath & "/BukkitGUI.log"
            'this module is initialized before the common module. So the folder needs to be created if not present
            Try
                If Not FileSystem.DirectoryExists(LoggingPath) Then _
                    FileSystem.CreateDirectory(LoggingPath)

                fs = New FileStream(log_file, FileMode.Create)
                sw = New StreamWriter(fs)
                sw.AutoFlush = True
                initialized = True

                Log(loggingLevel.Info, "Livebug", "Livebug started")

                WriteHeader()
            Catch ex As Exception
                Debug.WriteLine("Livebug wasn't loaded", "SEVERE ERROR!")
            End Try
        End Sub

        
        ''' <summary>
        '''     Write an entry to the log
        ''' </summary>
        ''' <param name="message">The message that should be logged</param>
        ''' <param name="category">
        '''     The category (e.g. module name, class name). this will be added to the entry, to speed up
        '''     debugging.
        ''' </param>
        ''' <remarks>Livebug must be initialized before use</remarks>
        Public Sub Log(level As loggingLevel, category As String, message As String, Optional detail As String = "-")
            Try
                Debug.WriteLine("[" & category & "] " & message & " ; " & detail, "livebug") _
                'Write to debug, so output can be followed in real time
                If Not initialized Then Exit Sub 'If not initialized,don't write 

                Writeline(
                    datestamp & ";" & timestamp & ";" & level.ToString & ";" & category.ToString & ";" &
                    message.ToString & ";" & detail.ToString & ";")

            Catch ex As Exception
                Trace.WriteLine("Severe error! could not write livebug entry!")
                Trace.WriteLine("exception:" & ex.Message)
                Trace.WriteLine("message:" & message)
            End Try
        End Sub

        
        ''' <summary>
        '''     Write an unhandled error to the log.
        ''' </summary>
        ''' <param name="e">The error object that should be logged.This contains all needed information.</param>
        ''' <remarks>Livebug must be initialized before use</remarks>
        Public Sub WriteUnhandledError(e As UnhandledExceptionEventArgs)
            Try
                If Not initialized Then Exit Sub

                Log(loggingLevel.Critical, "livebug", "Unhandled exception:" & e.ExceptionObject.ToString) _
                'create the element

                If e.ExceptionObject.ToString.Contains("System.Core") Then
                    MessageBox.Show(".NET framework 3.5 is missing or corrupted. Please reinstall .NET 3.5",
                                    ".NET framework error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                Report(True)
            Catch ex As Exception
                Trace.WriteLine("Severe error! could not write unhandled error to livebug!")
                Trace.WriteLine("exception:" & ex.Message)
                Trace.WriteLine("unhandled error:" & e.ExceptionObject.ToString)
            End Try
        End Sub

        Public Sub Report(Optional ByVal Critical As Boolean = False) 'Ask the user if he wants to send a (crash) report
            Dim result As DialogResult = DialogResult.No
            If Critical Then
                result =
                    MessageBox.Show(
                        Lr(
                            "Oh no! something went wrong in the application. The application is unable to continue it's work, and will exit. Please report this error to the developers by mailing the log file to contact@bertware.net" &
                            vbCrLf &
                            "Do you want to copy the log file to your desktop, so you can mail it whenever you want?"),
                        Lr("Critical error"), MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            Else
                result = MessageBox.Show(Lr("The application encountered some problems." & vbCrLf _
                                            &
                                            "If you noticed problems and want to help us to fix these problems, please provide us the log file." &
                                            vbCrLf _
                                            & "You can mail the log file to contact@bertware.net" & vbCrLf _
                                            &
                                            "If you have additional details or information, you can send this info along with the log file in the e-mail." &
                                            vbCrLf & vbCrLf _
                                            &
                                            "Do you want to copy the log file to your desktop, so you can mail it whenever you want?"),
                                         Lr("Bugs detected"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning)


            End If

            If result = DialogResult.Yes Then
                Try
                    File.Copy(log_file,
                              Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "/Bukkitgui-error.log")
                Catch ex As Exception
                    MessageBox.Show(
                        "Couldn't copy the logfile! You can still find it at %appdata%/Bertware/BukkitGUI/Logging/BukkitGUI.log",
                        "Couldn't copy!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        End Sub

        Private Sub Writeline(text As String)
            If initialized = False Then Exit Sub
            sw.WriteLine(text)
        End Sub

        
        ''' <summary>
        '''     Get a timestamp in the hh:mm:ss:xxxx format
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private ReadOnly Property timestamp() As String
            Get
                Return _
                    Date.Now.Hour.ToString.PadLeft(2, "0") & ":" & Date.Now.Minute.ToString.PadLeft(2, "0") & ":" &
                    Date.Now.Second.ToString.PadLeft(2, "0") & ":" & Date.Now.Millisecond.ToString.PadLeft(4, "0")
            End Get
        End Property

        
        ''' <summary>
        '''     Get a datestamp in the dd/mm/yyyy format
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private ReadOnly Property datestamp() As String
            Get
                Return _
                    Date.Now.Year.ToString.PadLeft(4, "0") & "/" & Date.Now.Month.ToString.PadLeft(2, "0") & "/" &
                    Date.Now.Day.ToString.PadLeft(2, "0")
            End Get
        End Property

        
        ''' <summary>
        '''     Write app and computer details to the log, for debugging purposes.
        ''' </summary>
        ''' <remarks>Livebug must be initialized before use</remarks>
        Private Sub WriteHeader()
            Try
                If Not initialized Then Exit Sub
                'Write Header to XML log
                Log(loggingLevel.Info, "Livebug", "app", My.Application.Info.AssemblyName)
                Log(loggingLevel.Info, "Livebug", "version", My.Application.Info.Version.ToString)
                Log(loggingLevel.Info, "Livebug", "path", My.Application.Info.DirectoryPath)

                Try
                    Log(loggingLevel.Info, "Livebug", "netframework",
                        GetHighestInstalledVersion_CatchErrors().Version)
                Catch ex As Exception
                    Log(loggingLevel.Warning, "Livebug", ".NET check error", ex.Message)
                End Try

                Try
                    Log(loggingLevel.Info, "Livebug", "CPU", "")
                    If IsRunningOnMono Then
                        Log(loggingLevel.Info, "Livebug", "CPU", "mfg: unknown/mono")
                        Log(loggingLevel.Info, "Livebug", "CPU", "name: unknown/mono")
                        Log(loggingLevel.Info, "Livebug", "CPU", "cores: unknown/mono")
                        Log(loggingLevel.Info, "Livebug", "CPU", "threads: unknown/mono")
                        Log(loggingLevel.Info, "Livebug", "CPU", "speed: unknown/mono")
                    Else
                        Log(loggingLevel.Info, "Livebug", "CPU",
                            "mfg: " & GetprocessorInfo(WMI.processorprop.Manufacturer).Trim)
                        Log(loggingLevel.Info, "Livebug", "CPU",
                            "name: " & GetprocessorInfo(processorprop.Name))
                        Log(loggingLevel.Info, "Livebug", "CPU",
                            "cores: " & GetprocessorInfo(processorprop.NumberOfCores))
                        Log(loggingLevel.Info, "Livebug", "CPU",
                            "threads: " & GetprocessorInfo(processorprop.NumberOfLogicalProcessors))
                        Log(loggingLevel.Info, "Livebug", "CPU",
                            "speed: " & GetprocessorInfo(processorprop.CurrentClockSpeed))
                    End If
                Catch ex1 As Exception
                    Log(loggingLevel.Warning, "livebug", "could not read CPU information: " & ex1.Message)
                End Try

                Try
                    Log(loggingLevel.Info, "Livebug", "RAM", "")
                    If IsRunningOnMono Then
                        Log(loggingLevel.Info, "Livebug", "RAM", "mfg: unknown/mono")
                        Log(loggingLevel.Info, "Livebug", "RAM", "amount: unknown/mono")
                    Else
                        Log(loggingLevel.Info, "Livebug", "RAM",
                            "amount: " & GetMemoryInfo(Memoryprop.Capacity))
                        Log(loggingLevel.Info, "Livebug", "RAM",
                            "mfg: " & GetMemoryInfo(Memoryprop.Manufacturer))
                    End If

                Catch ex2 As Exception
                    Log(loggingLevel.Warning, "livebug", "could not read RAM information: " & ex2.Message)
                End Try

                Try
                    Log(loggingLevel.Info, "Livebug", "OS", "")
                    Log(loggingLevel.Info, "Livebug", "OS", "Name:" & My.Computer.Info.OSFullName)
                    Log(loggingLevel.Info, "Livebug", "OS", "Platform:" & My.Computer.Info.OSPlatform)
                    Log(loggingLevel.Info, "Livebug", "OS", "Version:" & My.Computer.Info.OSVersion)
                Catch ex3 As Exception
                    Log(loggingLevel.Warning, "livebug", "could not read OS information: " & ex3.Message)
                End Try
            Catch ex As Exception
                Trace.WriteLine("Severe error! could not write livebug head!")
                Trace.WriteLine("exception:" & ex.Message)
            End Try
        End Sub

        Public Sub dispose(Optional no_report As Boolean = False)
            If Not initialized Then Exit Sub
            initialized = False
            'REPORTING DISABLED
            '  no_report = True
            'REPORTING DISABLED

            Log(loggingLevel.Fine, "livebug", "Saving and stopping livebug", "livebug")

            Try
                If tls IsNot Nothing Then tls.Close() : tls = Nothing
                If sw IsNot Nothing Then sw.Close() : sw = Nothing

                Thread.Sleep(50) 'make sure file operations are stopped 

                If no_report = False Then
                    fs = New FileStream(log_file, FileMode.Open)
                    Dim sr As New StreamReader(fs)
                    Dim c As String = sr.ReadToEnd
                    sr.Close()
                    fs.Close()
                    fs.Dispose()
                    If (c.ToLower.Contains(";severe;") Or c.ToLower.Contains(";critical;")) Then
                        Report()
                    End If
                End If
            Catch ex As Exception
                Trace.WriteLine("CAN'T DISPOSE LIVEBUG:" & ex.Message)
            End Try
            Trace.WriteLine("livebug stopped")
            initialized = False
        End Sub
    End Module
End Namespace
