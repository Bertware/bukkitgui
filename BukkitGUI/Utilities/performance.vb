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

'Read the memory and CPU usage of the server.host process, the GUI and total
'After init, other threads will continue to sample.
'Values can be read at any moment.
'
Imports System.Timers
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.Utilities

Namespace Utilities
    Module Performance
        Dim _measureCpu As Boolean = True
        Dim _measureMemory As Boolean = True
        Dim _pcCpu As PerformanceCounter

        
        ''' <summary>
        '''     The total CPU usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public TotalCpu As Integer = - 1

        
        ''' <summary>
        '''     The GUI CPU usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public GuiCpu As Integer = - 1

        
        ''' <summary>
        '''     The server CPU usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public ServerCpu As Integer = - 1

        '_mbytes: usage in MB
        'without suffix: %

        
        ''' <summary>
        '''     The total memory usage, in MegaBytes
        ''' </summary>
        ''' <remarks></remarks>
        Public TotalMemMbytes As UInt64

        
        ''' <summary>
        '''     The total memory usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public TotalMem As Integer = - 1

        
        ''' <summary>
        '''     The server memory usage, in MegaBytes
        ''' </summary>
        ''' <remarks></remarks>
        Public GuiMemMbytes As UInt64

        
        ''' <summary>
        '''     The gui memory usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public GuiMem As Integer = - 1

        
        ''' <summary>
        '''     The server memory usage, in MegaBytes
        ''' </summary>
        ''' <remarks></remarks>
        Public ServerMemMbytes As UInt64

        
        ''' <summary>
        '''     The server memory usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public ServerMem As Integer = - 1

        
        ''' <summary>
        '''     The total amount of RAM installed, in megabytes
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly TotalMemoryMb As UInt64 = Math.Round(My.Computer.Info.TotalPhysicalMemory/1048576)

        
        ''' <summary>
        '''     The amount of physical cores in the computer
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Cores As Byte = GetprocessorInfo(WMI.processorprop.NumberOfLogicalProcessors)

        Private _tmrMeasureCpu As Timer, _tmrMeasureRam As Timer

        
        ''' <summary>
        '''     Initialize and start measurments
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Init()
            Log(livebug.loggingLevel.Fine, "performance", "Initializing...")

            If IsRunningOnMono Then
                Log(loggingLevel.Fine, "performance", "Initialization cancelled: running mono...")
                _prevGuiMs = 0
                _currGuiMs = 0

                _prevServerMs = 0
                _currServerMs = 0

                _prevTime = New TimeSpan(0)
                _currTime = New TimeSpan(0)
                Exit Sub
            End If

            Try
                _tmrMeasureCpu = New Timer
                _tmrMeasureCpu.Interval = 1000
                _tmrMeasureCpu.Enabled = True
                AddHandler _tmrMeasureCpu.Elapsed, AddressOf MeasureCPU
                _tmrMeasureCpu.Start()
                Log(loggingLevel.Fine, "performance", "CPU timer started")
            Catch ex As Exception
                Log(loggingLevel.Warning, "performance", "CPU initialization failed", ex.Message)
            End Try

            Try
                _tmrMeasureRam = New Timer
                _tmrMeasureRam.Interval = 500
                _tmrMeasureRam.Enabled = True
                AddHandler _tmrMeasureRam.Elapsed, AddressOf MeasureRAM
                _tmrMeasureRam.Start()
                Log(loggingLevel.Fine, "performance", "RAM timer started")
            Catch ex As Exception
                Log(loggingLevel.Warning, "performance", "RAM initialization failed:" & ex.Message)
            End Try

            Try
                _pcCpu = New PerformanceCounter
                _pcCpu.CounterName = "% Processor Time"
                _pcCpu.CategoryName = "Processor"
                _pcCpu.InstanceName = "_Total"
                Log(loggingLevel.Fine, "performance", "CPU counter started")
            Catch ex As Exception
                Log(loggingLevel.Warning, "performance", "total CPU initialization failed", ex.Message)
            End Try

            _prevGuiMs = 0
            _currGuiMs = 0

            _prevServerMs = 0
            _currServerMs = 0

            _prevTime = New TimeSpan(0)
            _currTime = New TimeSpan(0)

            If Cores = 0 Then
                _measureCpu = False
                Log(loggingLevel.Warning, "performance",
                    "Amount of cores unknown, CPU measurement disabled")
            End If
            Log(loggingLevel.Fine, "performance", "Performance initialized")
        End Sub

        
        ''' <summary>
        '''     Stop measurements
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Disable()
            If _tmrMeasureCpu IsNot Nothing Then
                RemoveHandler _tmrMeasureCpu.Elapsed, Nothing
                _tmrMeasureCpu.Stop()
                _tmrMeasureCpu = Nothing
            End If
            If _tmrMeasureRam IsNot Nothing Then
                RemoveHandler _tmrMeasureRam.Elapsed, Nothing
                _tmrMeasureRam.Stop()
                _tmrMeasureRam = Nothing
            End If
        End Sub


        Dim _prevGuiMs As Single
        Dim _currGuiMs As Single

        Dim _prevServerMs As Single
        Dim _currServerMs As Single

        Dim _prevTime As TimeSpan
        Dim _currTime As TimeSpan

        Dim _deltams As UInt64
        Dim _fail As Byte

        
        ''' <summary>
        '''     Measure CPU values
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub MeasureCpu()
            If _measureCpu Then
                Try

                    _currTime = GetTimeSpan(DateTime.Now)
                    _currGuiMs = Process.GetCurrentProcess.TotalProcessorTime.TotalMilliseconds
                    If running Then _currServerMs = host.TotalProcessorTime.TotalMilliseconds

                    Dim dt As UInt64 = (_currTime - _prevTime).TotalMilliseconds

                    If _currGuiMs > _prevGuiMs Then _deltams = (_currGuiMs - _prevGuiMs)
                    Dim gc As UInt64 = Math.Round((_deltams/dt)*100/Cores)
                    If gc > 255 Then gc = 255
                    If dt <> 0 And gc < 256 Then GuiCpu = CByte(gc)
                    If GuiCpu > 100 Then GuiCpu = 100
                    If GuiCpu < 0 Then GuiCpu = 0

                    If running Then
                        If _currServerMs > _prevServerMs Then _deltams = (_currServerMs - _prevServerMs)

                        Dim gs As UInt64 = 0
                        If dt <> 0 Then gs = Math.Round((_deltams/dt)*100/Cores)
                        If gs > 255 Then gs = 255

                        If dt <> 0 Then ServerCpu = CByte(gs)
                        If ServerCpu > 100 Then ServerCpu = 100
                        If ServerCpu < 0 Then ServerCpu = 0
                    Else
                        ServerCpu = 0
                    End If

                    _prevTime = _currTime
                    _prevGuiMs = _currGuiMs
                    _prevServerMs = _currServerMs
                    Try 
                        TotalCpu = Math.Round(_pcCpu.NextValue())
                        If TotalCpu > 100 Then TotalCpu = 100
                        If TotalCpu < 0 Then TotalCpu = 0
                    Catch totalcpuex As Exception
                    End Try

                    _fail = 0
                Catch ovf As OverflowException
                    _prevGuiMs = 0
                    _currGuiMs = 0

                    _prevServerMs = 0
                    _currServerMs = 0

                    _prevTime = New TimeSpan(0)
                    _currTime = New TimeSpan(0)

                    Log(loggingLevel.Warning, "performance", "Overflow in CPU measurement. Values resetted",
                        ovf.Message) _
                    'don't report this as an error. Holding the app too long will cause this too, nothing bad.

                    _fail = _fail + 1
                    If _fail = 16 Then
                        Log(loggingLevel.Warning, "performance", "Too many overflows, measurement disabled") _
                        'don't report this as an error. Holding the app too long will cause this too, nothing bad.
                        _measureCpu = False
                        TotalCpu = -1
                        ServerCpu = -1
                        GuiCpu = -1
                        If _tmrMeasureCpu IsNot Nothing Then _tmrMeasureCpu.Enabled = False
                    End If
                Catch ex As Exception _
'If an exception is caught, log, and disable cpu logging (to prevent spam of errors)
                    Log(loggingLevel.Warning, "performance",
                        "Could not get CPU value. CPU measurement disabled.", ex.Message)
                    _measureCpu = False
                    TotalCpu = -1
                    ServerCpu = -1
                    GuiCpu = -1
                    If _tmrMeasureCpu IsNot Nothing Then _tmrMeasureCpu.Enabled = False
                End Try
            End If
        End Sub

        
        ''' <summary>
        '''     Measure RAM values
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub MeasureRam()
            If _measureMemory = False Then Exit Sub
            Try
                TotalMemMbytes = TotalMemoryMb - ByteToMb(My.Computer.Info.AvailablePhysicalMemory)
                TotalMem = CByte((TotalMemMbytes/TotalMemoryMb)*100)
                If TotalMem > 100 Then TotalMem = 100
                If TotalMem < 0 Then TotalMem = 0

                GuiMemMbytes = ByteToMb(GetProcessMemory(Process.GetCurrentProcess.Id)/2) _
                'divide by 2 to correct wrong numbers.
                GuiMem = CByte((GuiMemMbytes/TotalMemoryMb)*100)
                If GuiMem > 100 Then GuiMem = 100
                If GuiMem < 0 Then GuiMem = 0

                If running AndAlso host IsNot Nothing Then
                    ServerMemMbytes = ByteToMb(GetProcessMemory(host.Id))
                    ServerMem = CByte((ServerMemMbytes/TotalMemoryMb)*100)
                    If ServerMem > 100 Then ServerMem = 100
                    If ServerMem < 0 Then ServerMem = 0
                Else
                    ServerMem = 0
                    ServerMemMbytes = 0
                End If

            Catch ex As Exception _
'If an exception is caught, log, and disable memory logging (to prevent spam of errors)
                Log(loggingLevel.Warning, "performance",
                    "Could not get memory value. Memory measurement disabled.", ex.Message)
                _measureMemory = False

                ServerMem = - 1
                ServerMemMbytes = 0
                GuiMem = - 1
                GuiMemMbytes = 0
                TotalMem = - 1
                TotalMemMbytes = 0

                If _tmrMeasureRam IsNot Nothing Then _tmrMeasureRam.Enabled = False
            End Try
        End Sub

        
        ''' <summary>
        '''     Get the memory usage for a certain process
        ''' </summary>
        ''' <param name="Process_id">The ID of the process which memory usage is needed</param>
        ''' <returns>the memory usage in bytes</returns>
        ''' <remarks>
        '''     Don't use too much, could cause memory leaks in the WMI service provider host. Leaks up to 1 full processor
        '''     Core
        ''' </remarks>
        Private Function GetProcessMemory(ByVal Process_id As Integer) As UInt64
            Dim dmemory As UInt64 = CUInt(GetprocessInfo(processprop.WorkingSetSize, Process_id))
            If dmemory > 0 Then
                Return dmemory
            Else
                Return 0
            End If
        End Function

        Private Function GetTimeSpan(time As DateTime) As TimeSpan
            Return New TimeSpan(time.Day, time.Hour, time.Minute, time.Second, time.Millisecond)
        End Function
    End Module
End Namespace