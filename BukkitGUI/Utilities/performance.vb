'Read the memory and CPU usage of the server.host process, the GUI and total
'After init, other threads will continue to sample.
'Values can be read at any moment.
'
Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.Utilities

Namespace Utilities
    Module performance
        Dim measure_cpu As Boolean = True
        Dim measure_memory As Boolean = True
        Dim pcCPU As PerformanceCounter

        ''' <summary>
        ''' The total CPU usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public total_cpu As Integer = - 1

        ''' <summary>
        ''' The GUI CPU usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public gui_cpu As Integer = - 1

        ''' <summary>
        ''' The server CPU usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public server_cpu As Integer = - 1

        '_mbytes: usage in MB
        'without suffix: %

        ''' <summary>
        ''' The total memory usage, in MegaBytes
        ''' </summary>
        ''' <remarks></remarks>
        Public total_mem_mbytes As UInt64

        ''' <summary>
        ''' The total memory usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public total_mem As Integer = - 1

        ''' <summary>
        ''' The server memory usage, in MegaBytes
        ''' </summary>
        ''' <remarks></remarks>
        Public gui_mem_mbytes As UInt64

        ''' <summary>
        ''' The gui memory usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public gui_mem As Integer = - 1

        ''' <summary>
        ''' The server memory usage, in MegaBytes
        ''' </summary>
        ''' <remarks></remarks>
        Public server_mem_mbytes As UInt64

        ''' <summary>
        ''' The server memory usage, in %
        ''' </summary>
        ''' <remarks></remarks>
        Public server_mem As Integer = - 1

        ''' <summary>
        ''' The total amount of RAM installed, in megabytes
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly TotalMemoryMB As UInt64 = Math.Round(My.Computer.Info.TotalPhysicalMemory/1048576)

        ''' <summary>
        ''' The amount of physical cores in the computer
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly cores As Byte = WMI.GetprocessorInfo(WMI.processorprop.NumberOfLogicalProcessors)

        Private tmrMeasureCPU As Timers.Timer, tmrMeasureRAM As Timers.Timer


        ''' <summary>
        ''' Initialize and start measurments
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub init()
            livebug.write(livebug.loggingLevel.Fine, "performance", "Initializing...")

            If common.IsRunningOnMono Then
                livebug.write(loggingLevel.Fine, "performance", "Initialization cancelled: running mono...")
                prev_gui_ms = 0
                curr_gui_ms = 0

                prev_server_ms = 0
                curr_server_ms = 0

                prev_time = New TimeSpan(0)
                curr_time = New TimeSpan(0)
                Exit Sub
            End If

            Try
                tmrMeasureCPU = New Timers.Timer
                tmrMeasureCPU.Interval = 1000
                tmrMeasureCPU.Enabled = True
                AddHandler tmrMeasureCPU.Elapsed, AddressOf MeasureCPU
                tmrMeasureCPU.Start()
                livebug.write(loggingLevel.Fine, "performance", "CPU timer started")
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "performance", "CPU initialization failed", ex.Message)
            End Try

            Try
                tmrMeasureRAM = New Timers.Timer
                tmrMeasureRAM.Interval = 500
                tmrMeasureRAM.Enabled = True
                AddHandler tmrMeasureRAM.Elapsed, AddressOf MeasureRAM
                tmrMeasureRAM.Start()
                livebug.write(loggingLevel.Fine, "performance", "RAM timer started")
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "performance", "RAM initialization failed:" & ex.Message)
            End Try

            Try
                pcCPU = New PerformanceCounter
                pcCPU.CounterName = "% Processor Time"
                pcCPU.CategoryName = "Processor"
                pcCPU.InstanceName = "_Total"
                livebug.write(loggingLevel.Fine, "performance", "CPU counter started")
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "performance", "total CPU initialization failed", ex.Message)
            End Try

            prev_gui_ms = 0
            curr_gui_ms = 0

            prev_server_ms = 0
            curr_server_ms = 0

            prev_time = New TimeSpan(0)
            curr_time = New TimeSpan(0)

            If cores = 0 Then _
                measure_cpu = False : _
                    livebug.write(loggingLevel.Warning, "performance",
                                  "Amount of cores unknown, CPU measurement disabled")

            livebug.write(loggingLevel.Fine, "performance", "Performance initialized")
        End Sub

        ''' <summary>
        ''' Stop measurements
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub disable()
            If tmrMeasureCPU IsNot Nothing Then
                RemoveHandler tmrMeasureCPU.Elapsed, Nothing
                tmrMeasureCPU.Stop()
                tmrMeasureCPU = Nothing
            End If
            If tmrMeasureRAM IsNot Nothing Then
                RemoveHandler tmrMeasureRAM.Elapsed, Nothing
                tmrMeasureRAM.Stop()
                tmrMeasureRAM = Nothing
            End If
        End Sub


        Dim prev_gui_ms As Single
        Dim curr_gui_ms As Single

        Dim prev_server_ms As Single
        Dim curr_server_ms As Single

        Dim prev_time As TimeSpan
        Dim curr_time As TimeSpan

        Dim dms As UInt64
        Dim fail As Byte

        ''' <summary>
        ''' Measure CPU values
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub MeasureCPU()
            If measure_cpu Then
                Try

                    curr_time = GetTimeSpan(DateTime.Now)
                    curr_gui_ms = Process.GetCurrentProcess.TotalProcessorTime.TotalMilliseconds
                    If server.running Then curr_server_ms = server.host.TotalProcessorTime.TotalMilliseconds

                    Dim dt As UInt64 = (curr_time - prev_time).TotalMilliseconds

                    If curr_gui_ms > prev_gui_ms Then dms = (curr_gui_ms - prev_gui_ms)
                    Dim gc As UInt64 = Math.Round((dms/dt)*100/cores)
                    If gc > 255 Then gc = 255
                    If dt <> 0 And gc < 256 Then gui_cpu = CByte(gc)
                    If gui_cpu > 100 Then gui_cpu = 100
                    If gui_cpu < 0 Then gui_cpu = 0

                    If server.running Then
                        If curr_server_ms > prev_server_ms Then dms = (curr_server_ms - prev_server_ms)

                        Dim gs As UInt64 = 0
                        If dt <> 0 Then gs = Math.Round((dms/dt)*100/cores)
                        If gs > 255 Then gs = 255

                        If dt <> 0 Then server_cpu = CByte(gs)
                        If server_cpu > 100 Then server_cpu = 100
                        If server_cpu < 0 Then server_cpu = 0
                    Else
                        server_cpu = 0
                    End If

                    prev_time = curr_time
                    prev_gui_ms = curr_gui_ms
                    prev_server_ms = curr_server_ms

                    total_cpu = Math.Round(pcCPU.NextValue())
                    If total_cpu > 100 Then total_cpu = 100
                    If total_cpu < 0 Then total_cpu = 0
                Catch ovf As OverflowException
                    prev_gui_ms = 0
                    curr_gui_ms = 0

                    prev_server_ms = 0
                    curr_server_ms = 0

                    prev_time = New TimeSpan(0)
                    curr_time = New TimeSpan(0)

                    livebug.write(loggingLevel.Warning, "performance", "Overflow in CPU measurement. Values resetted",
                                  ovf.Message) _
                    'don't report this as an error. Holding the app too long will cause this too, nothing bad.

                    fail = fail + 1
                    If fail = 16 Then
                        livebug.write(loggingLevel.Warning, "performance", "Too many overflows, measurement disabled") _
                        'don't report this as an error. Holding the app too long will cause this too, nothing bad.
                        measure_cpu = False
                        total_cpu = - 1
                        server_cpu = - 1
                        gui_cpu = - 1
                        If tmrMeasureCPU IsNot Nothing Then tmrMeasureCPU.Enabled = False
                    End If
                Catch ex As Exception _
                    'If an exception is caught, log, and disable cpu logging (to prevent spam of errors)
                    livebug.write(loggingLevel.Warning, "performance",
                                  "Could not get CPU value. CPU measurement disabled.", ex.Message)
                    measure_cpu = False
                    total_cpu = - 1
                    server_cpu = - 1
                    gui_cpu = - 1
                    If tmrMeasureCPU IsNot Nothing Then tmrMeasureCPU.Enabled = False
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Measure RAM values
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub MeasureRAM()
            If measure_memory = False Then Exit Sub
            Try
                total_mem_mbytes = TotalMemoryMB - common.ByteToMb(My.Computer.Info.AvailablePhysicalMemory)
                total_mem = CByte((total_mem_mbytes/TotalMemoryMB)*100)
                If total_mem > 100 Then total_mem = 100
                If total_mem < 0 Then total_mem = 0

                gui_mem_mbytes = common.ByteToMb(GetProcessMemory(Process.GetCurrentProcess.Id)/2) _
                'divide by 2 to correct wrong numbers.
                gui_mem = CByte((gui_mem_mbytes/TotalMemoryMB)*100)
                If gui_mem > 100 Then gui_mem = 100
                If gui_mem < 0 Then gui_mem = 0

                If server.running AndAlso server.host IsNot Nothing Then
                    server_mem_mbytes = common.ByteToMb(GetProcessMemory(server.host.Id))
                    server_mem = CByte((server_mem_mbytes/TotalMemoryMB)*100)
                    If server_mem > 100 Then server_mem = 100
                    If server_mem < 0 Then server_mem = 0
                Else
                    server_mem = 0
                    server_mem_mbytes = 0
                End If

            Catch ex As Exception _
                'If an exception is caught, log, and disable memory logging (to prevent spam of errors)
                livebug.write(loggingLevel.Warning, "performance",
                              "Could not get memory value. Memory measurement disabled.", ex.Message)
                measure_memory = False

                server_mem = - 1
                server_mem_mbytes = 0
                gui_mem = - 1
                gui_mem_mbytes = 0
                total_mem = - 1
                total_mem_mbytes = 0

                If tmrMeasureRAM IsNot Nothing Then tmrMeasureRAM.Enabled = False
            End Try
        End Sub

        ''' <summary>
        ''' Get the memory usage for a certain process
        ''' </summary>
        ''' <param name="Process_id">The ID of the process which memory usage is needed</param>
        ''' <returns>the memory usage in bytes</returns>
        ''' <remarks>Don't use too much, could cause memory leaks in the WMI service provider host. Leaks up to 1 full processor Core</remarks>
        Private Function GetProcessMemory(ByVal Process_id As Integer) As UInt64
            Dim dmemory As UInt64 = CUInt(WMI.GetprocessInfo(WMI.processprop.WorkingSetSize, Process_id))
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