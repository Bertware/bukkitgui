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

Imports System.Threading
Imports System.Xml
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.BukkitGUI.Utilities


Namespace TaskManager
    Public Module TaskManager
        Public tasks As List(Of task)

        Public task_xml_path As String = ConfigPath & "/tasks.xml"
        Public task_xml As fxml

        Public Event TasksLoaded()

        Public Event TaskExecuted(executor As task)

        Const version As String = "1.0"

        Public Sub init()
            If Not FileSystem.FileExists(task_xml_path) Then _
                Create_file(task_xml_path, "<tasks version=""" & version & """></tasks>")
            task_xml = New fxml(task_xml_path, "TaskManager", True)
            LoadAllTasks()
        End Sub

        Public Sub LoadAllTasks()
            tasks = New List(Of task)
            Log(loggingLevel.Fine, "TaskManager", "Loading tasks...")
            Dim elements As XmlNodeList = task_xml.GetElementsByName("task")
            For i = 0 To elements.Count - 1
                Log(loggingLevel.Fine, "TaskManager", "Parsing task " & i + 1 & " out of " & elements.Count)
                Dim taskelement As XmlElement = elements(i)

                Try

                    Dim t As task = ParseTaskXML(taskelement, True)
                    tasks.Add(t)
                    AddHandler t.executed, AddressOf RaiseExecutedEvent

                    Log(loggingLevel.Fine, "TaskManager", "Loading task:" & t.name & " :task enabled")
                Catch ex As Exception
                    Log(loggingLevel.Severe, "TaskManager", "Could not load task:" & ex.Message)
                End Try
            Next
            Log(loggingLevel.Fine, "TaskManager", "Loaded tasks. " & tasks.Count & " tasks loaded")
            RaiseEvent TasksLoaded()
        End Sub

        Private Function ParseTaskXML(xml As XmlElement, Optional ByVal doEnable As Boolean = True) As task
            Dim t As New task

            t.name = xml.GetAttribute("name")

            Log(loggingLevel.Fine, "TaskManager", "Loading task:" & t.name)
            Dim trigger As XmlElement = xml.GetElementsByTagName("trigger")(0)
            t.trigger_type = task.ParseTrigger(trigger.GetAttribute("type"))
            t.trigger_parameters = trigger.InnerText

            If t.trigger_parameters Is Nothing Then t.trigger_parameters = ""
            Log(loggingLevel.Fine, "TaskManager", "Loading task:" & t.name & " : trigger settings loaded")

            Dim action As XmlElement = xml.GetElementsByTagName("action")(0)
            t.action_type = task.ParseAction(action.GetAttribute("type"))
            t.action_parameters = action.InnerText

            If t.action_parameters Is Nothing Then t.action_parameters = ""

            Log(loggingLevel.Fine, "TaskManager", "Loading task:" & t.name & " : action settings loaded")

            If xml.GetAttribute("enabled") Is Nothing OrElse xml.GetAttribute("enabled") = "" Then _
                xml.SetAttribute("enabled", "true") : task_xml.save()
            t.canEnable = True
            If doEnable = True Then t.IsEnabled = (xml.GetAttribute("enabled") = "true") 'will also enable if needed

            Return t
        End Function

        Public Sub UnloadAllTasks()
            For Each t As task In tasks
                RemoveHandler t.executed, AddressOf RaiseExecutedEvent
                t.disable()
            Next
        End Sub

        Public Sub ReloadAllTasks()
            Log(loggingLevel.Fine, "TaskManager", "Reloading tasks...")
            UnloadAllTasks()
            LoadAllTasks()
        End Sub

        Public Sub addTask(task_obj As task, enablestate As Boolean)
            Try
                Dim element As XmlElement = task_xml.write("task", "", "", Nothing, True)
                element.SetAttribute("name", task_obj.name)
                element.SetAttribute("enabled", enablestate.ToString.ToLower)

                Dim trigger_element As XmlElement = task_xml.Document.CreateElement("trigger")
                trigger_element.SetAttribute("type", task_obj.trigger_type.ToString)
                trigger_element.InnerText = task_obj.trigger_parameters.ToString
                element.AppendChild(trigger_element)

                Dim action_element As XmlElement = task_xml.Document.CreateElement("action")
                action_element.SetAttribute("type", task_obj.action_type.ToString)
                action_element.InnerText = task_obj.action_parameters.ToString
                element.AppendChild(action_element)

                task_xml.save()

                If task_obj.IsEnabled = False Then
                    task_obj.enable()
                End If

                AddHandler task_obj.executed, AddressOf RaiseExecutedEvent

                tasks.Add(task_obj)
                RaiseEvent TasksLoaded()
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Severe error in AddTask!", ex.Message)
            End Try
        End Sub

        Public Sub disableTask(ByRef Task As task)
            Log(loggingLevel.Fine, "TaskManager",
                "Updating task (disable): " & Task.name & " - Will be replaced by its updated version")
            Task.IsEnabled = False
            deleteTask(Task)
            addTask(Task, False)
            RaiseEvent TasksLoaded()
        End Sub

        Public Sub enableTask(ByRef Task As task)
            Log(loggingLevel.Fine, "TaskManager",
                "Updating task (enable): " & Task.name & " - Will be replaced by its updated version")
            deleteTask(Task)
            Task.IsEnabled = True
            addTask(Task, True)
            RaiseEvent TasksLoaded()
        End Sub

        Public Sub saveTask(ByRef OldTask As task, ByRef NewTask As task)
            Log(loggingLevel.Fine, "TaskManager",
                "Updating task: " & OldTask.name & " - Will be replaced by its updated version")
            OldTask.disable()
            deleteTask(OldTask)
            addTask(NewTask, NewTask.canEnable)
            RaiseEvent TasksLoaded()
        End Sub

        Public Sub deleteTask(ByRef task_obj As task)
            Try
                If task_obj.IsEnabled Then task_obj.disable()
                RemoveHandler task_obj.executed, AddressOf RaiseExecutedEvent

                tasks.Remove(task_obj)
                task_xml.RemoveElement(task_xml.getElementByAttribute("task", "name", task_obj.name))
            Catch ex As Exception
                Log(loggingLevel.Fine, "TaskManager", "Severe error in deleteTask(task)!", ex.Message)
            End Try
            RaiseEvent TasksLoaded()
        End Sub

        Public Sub deleteTask(name As String)
            Try
                deleteTask(GetTaskByName(name))
            Catch ex As Exception
                Log(loggingLevel.Fine, "TaskManager", "Severe error in deleteTask(name)!", ex.Message)
            End Try
            RaiseEvent TasksLoaded()
        End Sub

        Public Function GetTaskByName(name As String) As task
            Try
                Dim result As task = Nothing
                For Each task As task In tasks
                    If task.name = name Then result = task
                Next
                Return result
            Catch ex As Exception
                Log(loggingLevel.Fine, "TaskManager", "Severe error in GetTaskByName!", ex.Message)
                Return Nothing
            End Try
        End Function

        Public Sub import()
            Log(loggingLevel.Fine, "TaskManager", "Starting Import routine")
            Dim ofd As New OpenFileDialog
            ofd.Filter = "Task manager file (*.task)|*.task"
            ofd.Multiselect = False
            ofd.Title = "Import tasks"
            If ofd.ShowDialog() = DialogResult.Cancel Then _
                Log(loggingLevel.Fine, "TaskManager", "Import cancelled") : Exit Sub
            Try
                Dim impxml As New fxml(ofd.FileName, "TaskManager", True)
                For Each element As XmlElement In impxml.GetElementsByName("task")
                    Dim tmpnode As XmlElement = task_xml.Document.ImportNode(element, True)
                    task_xml.Document.DocumentElement.AppendChild(tmpnode)
                Next
                task_xml.save()
                Log(loggingLevel.Fine, "TaskManager", "Import finished!")
                ReloadAllTasks()
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Error while importing tasks", ex.Message)
                MessageBox.Show(lr("Error while importing the task! Is this a valid file?"), lr("Import failed!"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Public Sub export(name As String)
            Log(loggingLevel.Fine, "TaskManager", "Starting Export routine (single), task:" & name)
            Dim sfd As New SaveFileDialog
            sfd.Title = "Export task"
            sfd.Filter = "Task manager file (*.task)|*.task"
            sfd.OverwritePrompt = True
            sfd.SupportMultiDottedExtensions = True
            sfd.DefaultExt = ".task"
            sfd.AddExtension = True
            If sfd.ShowDialog = DialogResult.Cancel Then _
                Log(loggingLevel.Fine, "TaskManager", "Export cancelled") : Exit Sub
            Try
                Create_file(sfd.FileName, "<tasks version=""" & version & """></tasks>")
                Dim expxml As New fxml(sfd.FileName, "TaskManager", True)
                Dim tmpnode As XmlElement = expxml.Document.ImportNode(
                    task_xml.getElementByAttribute("task", "name", name), True)
                expxml.Document.DocumentElement.AppendChild(tmpnode)
                expxml.save()
                Log(loggingLevel.Fine, "TaskManager", "Export finished!")
            Catch ex As Exception
                Log(loggingLevel.Fine, "TaskManager", "Error while exporting task", ex.Message)
                MessageBox.Show(lr("Error while exporting the task!"), lr("Export failed!"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End Try
        End Sub

        Public Sub export(names As List(Of String))
            Log(loggingLevel.Fine, "TaskManager", "Starting Export routine (multiple)")
            Dim sfd As New SaveFileDialog
            sfd.Title = "Export task"
            sfd.Filter = "Task manager file (*.task)|*.task"
            sfd.OverwritePrompt = True
            sfd.DefaultExt = ".task"
            sfd.AddExtension = True
            sfd.SupportMultiDottedExtensions = True
            If sfd.ShowDialog = DialogResult.Cancel Then _
                Log(loggingLevel.Fine, "TaskManager", "Export cancelled") : Exit Sub
            Try
                Create_file(sfd.FileName, "<tasks version=""" & version & """></tasks>")
                Dim expxml As New fxml(sfd.FileName, "TaskManager", True)
                For Each name As String In names
                    Dim tmpnode As XmlElement = expxml.Document.ImportNode(
                        task_xml.getElementByAttribute("task", "name", name), True)
                    expxml.Document.DocumentElement.AppendChild(tmpnode)
                Next
                expxml.save()
                Log(loggingLevel.Fine, "TaskManager", "Export finished!")
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Error while exporting tasks", ex.Message)
                MessageBox.Show(lr("Error while exporting the tasks!"), lr("Export failed!"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub RaiseExecutedEvent(sender As task)
            RaiseEvent TaskExecuted(sender)
        End Sub
    End Module

    Public Class task
        Implements IDisposable

        Public Event triggered(sender As task)
        Public Event enabled(sender As task)
        Public Event disabled(sender As task)
        Public Event executed(sender As task)

        Private tmr_trigger As Timers.Timer
        Private Const currtimeCeck_interval As UInt16 = 2000 'interval to check, in ms

        Enum trigger
            server_start = 0
            server_starting = 1
            server_stop = 2
            server_stopping = 3
            elapsed_time = 4
            current_time = 5 'hh:mm:ss, split by ;
            player_join = 6 'argument optional: player names, ; separated
            player_leave = 7 'argument optional: player names, ; separated
            server_empty = 8
            heartbeat_fail = 9 'argument: hh:mm:ss interval
            task_finished = 10 'argument: name, optional: "/hh:mm:ss timeout"
            server_output = 11
        End Enum

        Enum action
            execute = 0
            shellexecute = 1
            command = 2
            stop_server = 3
            start_server = 4
            restart_server = 5
            restart_server_brute = 6
            backup = 7
            synchronize_list = 8
            kickall = 9
            close_gui = 10
        End Enum

        Public name As String
        Public trigger_type As trigger, trigger_parameters As String
        Public action_type As action, action_parameters As String
        Public canEnable As Boolean = True

        Private tmrHeartBeat As Timers.Timer, tmrHeartBeatResponse As Timers.Timer

        Private _IsEnabled As Boolean = False

        Public Property IsEnabled As Boolean
            Get
                Return _IsEnabled
            End Get
            Set(value As Boolean)
                If value <> _IsEnabled Then
                    If value = True Then
                        enable()
                        Me._IsEnabled = True
                    Else
                        disable()
                        Me._IsEnabled = False
                    End If
                End If
            End Set
        End Property

        Public Sub AddToList()
            addTask(Me, Me.canEnable)
        End Sub

        Public Function enable() As Boolean
            If Not canEnable Then _
                Log(loggingLevel.Fine, "TaskManager", "Can't enable task:" & name & " - canEnable false") : _
                    Return False : Exit Function
            If _IsEnabled Then _
                Log(loggingLevel.Fine, "TaskManager", "Can't enable task:" & name & " - Already enabled") : _
                    Return False : Exit Function
            Log(loggingLevel.Fine, "TaskManager",
                "Enabling task:" & name & " - trigger:" & trigger_type.ToString, Me.name)
            Try
                Select Case trigger_type
                    Case trigger.server_start
                        AddHandler ServerStarted, AddressOf Execute

                    Case trigger.server_starting
                        AddHandler ServerStarting, AddressOf Execute

                    Case trigger.server_stop
                        AddHandler ServerStopped, AddressOf Execute

                    Case trigger.server_stopping
                        AddHandler ServerStopping, AddressOf Execute

                    Case trigger.current_time
                        tmr_trigger = New Timers.Timer
                        tmr_trigger.Interval = currtimeCeck_interval
                        tmr_trigger.Enabled = True
                        AddHandler tmr_trigger.Elapsed, AddressOf tmr_currtimeCheck_elapsed

                    Case trigger.elapsed_time
                        tmr_trigger = New Timers.Timer
                        Dim ms As UInt64 = CULng(parseTimeSpan(trigger_parameters).TotalMilliseconds)
                        If ms < 1000 Then ms = 1000
                        tmr_trigger.Interval = ms
                        AddHandler tmr_trigger.Elapsed, AddressOf Execute
                        AddHandler ServerStarted, AddressOf tmr_trigger.Start
                        AddHandler ServerStopped, AddressOf tmr_trigger.Stop
                        If running = True Then
                            tmr_trigger.Start()
                        End If
                    Case trigger.player_join
                        AddHandler serverOutputHandler.PlayerJoin, AddressOf execute_playerjoin

                    Case trigger.player_leave
                        AddHandler serverOutputHandler.PlayerDisconnect, AddressOf execute_playerdisconnect

                    Case trigger.server_empty
                        AddHandler serverOutputHandler.PlayerDisconnect, AddressOf check_empty

                    Case trigger.heartbeat_fail
                        tmrHeartBeat = New Timers.Timer
                        Dim ms As UInt64 = CULng(parseTimeSpan(trigger_parameters).TotalMilliseconds)
                        If ms < 15000 Then ms = 15000
                        tmrHeartBeat.Interval = ms
                        AddHandler ServerStarted, AddressOf tmrHeartBeat.Start 'if server starts, start heartbeat
                        AddHandler ServerStopped, AddressOf tmrHeartBeat.Stop 'if server stops, stop heartbeat
                        AddHandler tmrHeartBeat.Elapsed, AddressOf trigger_sendlist 'if timer elapses, send heartbeat
                        If running = True Then
                            tmrHeartBeat.Start()
                        End If
                        tmrHeartBeatResponse = New Timers.Timer
                        tmrHeartBeatResponse.Interval = 10000
                        AddHandler tmrHeartBeat.Elapsed, AddressOf tmrHeartBeatResponse.Start _
                        'if heartbeat sent, start waiting for response
                        AddHandler ListUpdate, AddressOf tmrHeartBeatResponse.Stop _
                        'if response received, stop timer
                        AddHandler tmrHeartBeatResponse.Elapsed, AddressOf Execute 'if timed out, execute

                    Case trigger.task_finished
                        AddHandler TaskExecuted, AddressOf check_taskfinished

                    Case trigger.server_output
                        AddHandler TextReceived, AddressOf CheckTextMatch
                End Select
                _IsEnabled = True
                RaiseEvent enabled(Me)
                Log(loggingLevel.Fine, "TaskManager", "Task enabled", Me.name)
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Severe error in task.enable! " & ex.Message, Me.name)
            End Try
            Return _IsEnabled
        End Function

        Public Function disable() As Boolean
            Log(loggingLevel.Fine, "TaskManager",
                "Disabling task:" & name & " - trigger:" & trigger_type.ToString, Me.name)
            Try
                Select Case trigger_type
                    Case trigger.server_start
                        RemoveHandler ServerStarted, AddressOf Execute

                    Case trigger.server_starting
                        RemoveHandler ServerStarting, AddressOf Execute

                    Case trigger.server_stop
                        RemoveHandler ServerStopped, AddressOf Execute

                    Case trigger.server_stopping
                        RemoveHandler ServerStopping, AddressOf Execute

                    Case trigger.current_time
                        If tmr_trigger IsNot Nothing Then
                            tmr_trigger.Enabled = False
                            RemoveHandler tmr_trigger.Elapsed, Nothing
                        End If
                        tmr_trigger = Nothing

                    Case trigger.elapsed_time
                        If tmr_trigger IsNot Nothing Then
                            tmr_trigger.Enabled = False
                            RemoveHandler ServerStarted, AddressOf tmr_trigger.Start
                            RemoveHandler ServerStopped, AddressOf tmr_trigger.Stop
                            RemoveHandler tmr_trigger.Elapsed, Nothing
                        End If

                        tmr_trigger = Nothing
                    Case trigger.player_join
                        RemoveHandler serverOutputHandler.PlayerJoin, AddressOf execute_playerjoin

                    Case trigger.player_leave
                        RemoveHandler serverOutputHandler.PlayerDisconnect, AddressOf execute_playerdisconnect

                    Case trigger.server_empty
                        RemoveHandler serverOutputHandler.PlayerDisconnect, Nothing

                    Case trigger.heartbeat_fail
                        If tmrHeartBeat IsNot Nothing Then
                            tmrHeartBeat.Enabled = False
                            RemoveHandler tmrHeartBeat.Elapsed, Nothing 'if timer elapses, send heartbeat
                            RemoveHandler tmrHeartBeat.Elapsed, Nothing 'if heartbeat sent, start waiting for respons
                        End If

                        tmrHeartBeat = Nothing
                        If tmrHeartBeatResponse IsNot Nothing Then
                            tmrHeartBeatResponse.Enabled = False
                            RemoveHandler tmrHeartBeatResponse.Elapsed, Nothing 'if timed out, execute
                            RemoveHandler ListUpdate, AddressOf tmrHeartBeatResponse.Stop _
                            'if response received, stop timer
                        End If

                        tmrHeartBeatResponse = Nothing
                        RemoveHandler ServerStarted, AddressOf tmrHeartBeat.Start 'if server starts, start heartbeat
                        RemoveHandler ServerStopped, AddressOf tmrHeartBeat.Stop 'if server stops, stop heartbeat

                    Case trigger.task_finished
                        RemoveHandler TaskExecuted, AddressOf check_taskfinished

                    Case trigger.server_output
                        RemoveHandler TextReceived, AddressOf CheckTextMatch
                End Select
                _IsEnabled = False
                RaiseEvent disabled(Me)
                Log(loggingLevel.Fine, "TaskManager", "Task disabled", Me.name)
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Severe error in task.disable! " & ex.Message, Me.name)
            End Try
            Return _IsEnabled
        End Function

        Private Sub CheckTextMatch(text As String, t As serverOutputHandler.MessageType)
            If text.ToLower.Contains(Me.trigger_parameters.ToLower) Then
                Execute()
            End If
        End Sub

        Private Sub check_taskfinished(t As task)
            If Me.trigger_parameters.Contains("/") = False Then
                If Me.trigger_parameters = t.name Then Execute(ParseActionParameters(action_parameters))
            Else
                Dim timeout As TimeSpan = parseTimeSpan(Me.trigger_parameters.Split("/")(1).Trim.Trim("/").Trim)
                Dim tgname As String = Me.trigger_parameters.Split("/")(0).Trim.Trim("/").Trim()
                If tgname = t.name Then _
                    Thread.Sleep(timeout) : Execute(ParseActionParameters(action_parameters))
            End If
        End Sub

        Private Sub tmr_currtimeCheck_elapsed()
            If trigger_parameters.Contains(";") Then
                For Each time As String In trigger_parameters.Split(";")
                    If time <> "" AndAlso time.Trim(":") <> "" Then
                        If _
                            Math.Abs(parseTimeSpan(time.Trim(";")).Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds) <
                            (currtimeCeck_interval/2) Then Execute() : Exit Sub
                    End If
                Next
            Else
                If _
                    Math.Abs(parseTimeSpan(trigger_parameters).Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds) <
                    (currtimeCeck_interval/2) Then Execute()
            End If
        End Sub

        Private Sub check_empty()
            If playerList IsNot Nothing AndAlso playerList.Count = 0 Then Execute()
        End Sub

        Private Sub trigger_sendlist()
            Log(loggingLevel.Fine, "TaskManager", "Sending automated list command for heartbeat", Me.name)
            SendCommand("list", True)
        End Sub

        Public Sub Execute()
            Log(loggingLevel.Fine, "TaskManager", "Parsing parameters for execution", Me.name)
            Execute(ParseActionParameters(action_parameters))
        End Sub

        Private Sub Execute(parameters As String)
            If Not _IsEnabled Then _
                Log(loggingLevel.Fine, "TaskManager", "cancelled execution, task disabled", Me.name) : _
                    Exit Sub 'don't run if not enabled

            Log(loggingLevel.Fine, "TaskManager",
                "Executing task:" & name & " - action:" & action_type.ToString, Me.name)
            Log(loggingLevel.Fine, "TaskManager", "Arguments:" & parameters, Me.name)
            RaiseEvent triggered(Me)
            If parameters Is Nothing Then parameters = ""
            Try
                Select Case action_type
                    Case action.execute
                        Dim p As New Process()
                        With p.StartInfo
                            If Not parameters.Contains("---") Then
                                .FileName = parameters
                            Else
                                .FileName = parameters.Split("---")(0).Trim.Trim("-").Trim
                                .Arguments = parameters.Split("---")(1).Trim.Trim("-").Trim
                            End If
                        End With
                        Log(loggingLevel.Fine, "TaskManager",
                            "Starting process:" & p.StartInfo.FileName & " with arguments " &
                            p.StartInfo.Arguments, Me.name)
                        If FileSystem.FileExists(p.StartInfo.FileName) = False Then
                            Log(loggingLevel.Warning, "TaskManager",
                                "Process wasn't started, file doesn't exist", Me.name)
                        Else
                            p.Start()
                        End If

                    Case action.shellexecute
                        Try
                            Shell(parameters, AppWinStyle.Hide, True, - 1)
                        Catch ex As Exception
                            MessageBox.Show(
                                lr("Task failed:") & name & vbCrLf & lr("Could not execute shell command:") & parameters &
                                vbCrLf & lr("Is this a valid task?"), lr("Task failed"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                            Log(loggingLevel.Warning, "TaskManager",
                                "ShellExecute for task " & name & " failed, parameters: " & parameters,
                                ex.Message)
                        End Try
                    Case action.command

                        If running Then
                            If parameters.Contains(";") Then
                                Dim commands() As String = parameters.Split(";")
                                Log(loggingLevel.Fine, "TaskManager",
                                    "Preparing to send " & commands.Count & " commands", Me.name)
                                For Each cmd As String In commands
                                    If SendCommand(cmd, True) Then _
                                        Log(loggingLevel.Fine, "TaskManager", "Command sent", Me.name) Else _
                                        Log(loggingLevel.Warning, "TaskManager",
                                            "Command sent, but not written to server", Me.name)
                                Next
                                Log(loggingLevel.Fine, "TaskManager", commands.Count & " commands sent",
                                    Me.name)
                            Else
                                If SendCommand(parameters, True) Then _
                                    Log(loggingLevel.Fine, "TaskManager", "Command sent", Me.name) Else _
                                    Log(loggingLevel.Warning, "TaskManager",
                                        "Command sent, but not written to server", Me.name)
                            End If
                        Else
                            Log(loggingLevel.Warning, "TaskManager", "Command not sent: Server not running",
                                Me.name)
                        End If

                    Case action.start_server
                        Log(loggingLevel.Fine, "TaskManager", "Starting server by task", Me.name)
                        If running = False Then
                            Dim mf As mainform = mainform.FromHandle(MainWindowHandle)
                            If mf IsNot Nothing Then mf.start_server() Else _
                                Log(loggingLevel.Warning, "TaskManager",
                                    "Mainform not found, cancelling execution.", Me.name)
                        End If

                    Case action.stop_server
                        Log(loggingLevel.Fine, "TaskManager", "Stopping server by task", Me.name)
                        If running = True Then
                            Dim mf As mainform = mainform.FromHandle(MainWindowHandle)
                            If mf IsNot Nothing Then mf.stop_server() Else _
                                Log(loggingLevel.Warning, "TaskManager",
                                    "Mainform not found, cancelling execution.", Me.name)
                        End If

                    Case action.restart_server
                        Log(loggingLevel.Fine, "TaskManager", "Restarting server by task", Me.name)
                        If running = True Then
                            Dim mf As mainform = mainform.FromHandle(MainWindowHandle)
                            If mf IsNot Nothing Then
                                mf.stop_server()
                                Thread.Sleep(10000) _
                                'workaround for starting server while old server hadn't stopped yet
                                mf.start_server()
                            Else
                                Log(loggingLevel.Warning, "TaskManager",
                                    "Mainform not found, cancelling execution.", Me.name)
                            End If
                        End If
                    Case action.restart_server_brute
                        Log(loggingLevel.Fine, "TaskManager", "Restarting server brute!", Me.name)
                        If running Then
                            If host IsNot Nothing AndAlso host.HasExited = False Then
                                host.Kill()
                            End If
                        End If
                        Dim mf As mainform = mainform.FromHandle(MainWindowHandle)
                        If mf IsNot Nothing Then mf.start_server() Else _
                            Log(loggingLevel.Warning, "TaskManager",
                                "Mainform not found, cancelling execution.", Me.name)

                    Case action.synchronize_list
                        Log(loggingLevel.Fine, "TaskManager", "Sending automated list command", Me.name)
                        If running Then SendCommand("list", True) Else _
                            Log(loggingLevel.Warning, "TaskManager",
                                "Sending automated list command failed, server not running", Me.name)

                    Case action.backup
                        Log(loggingLevel.Fine, "TaskManager", "Creating backup", Me.name)
                        Dim bu = GetBackupByName(parameters)
                        If bu IsNot Nothing Then bu.execute(False) Else _
                            MessageBox.Show(
                                lr("The taskmanager couldn't execute this task:") & Me.name & vbCrLf &
                                lr("Couldn't get the backup settings for backup scheme") & " " & Me.action_parameters,
                                lr("Task failed!"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Case action.kickall
                        Log(loggingLevel.Fine, "TaskManager", "Kicking all players", Me.name)
                        If playerList IsNot Nothing AndAlso playerList.Count > 0 Then
                            For Each Player As Player In playerList
                                SendCommand("kick " & Player.name & " " & parameters)
                            Next
                        End If
                    Case action.close_gui
                        Dim mf As mainform = mainform.FromHandle(MainWindowHandle)
                        If running = True Then
                            If mf IsNot Nothing Then mf.stop_server() Else _
                                Log(loggingLevel.Warning, "TaskManager",
                                    "Mainform not found, cancelling execution.", Me.name)
                        End If
                        If mf IsNot Nothing Then mf.SafeFormClose() Else _
                            Log(loggingLevel.Warning, "TaskManager",
                                "Mainform not found, cancelling execution.", Me.name)
                End Select
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Severe error in task.execute!", ex.Message)
            End Try
            RaiseEvent executed(Me)
        End Sub

        Private Sub execute_playerjoin(e As PlayerJoinEventArgs)
            If Me.trigger_parameters <> "" Then
                If Me.trigger_parameters.Contains(";") = False Then
                    If Me.trigger_parameters = (e.PlayerJoin.player.name) = False Then Exit Sub
                Else
                    If Me.trigger_parameters.Split(";").Contains(e.PlayerJoin.player.name) = False Then Exit Sub
                End If
            End If

            If e.reason = PlayerDisconnectEventArgs.playerleavereason.listupdate Then Exit Sub
            Thread.Sleep(250)
            Execute(ParseActionParameters_playerjoin(ParseActionParameters(action_parameters), e.PlayerJoin))
        End Sub

        Private Sub execute_playerdisconnect(e As PlayerDisconnectEventArgs)
            If Me.trigger_parameters <> "" Then
                If Me.trigger_parameters.Contains(";") = False Then
                    If Me.trigger_parameters = (e.player.name) = False Then Exit Sub
                Else
                    If Me.trigger_parameters.Split(";").Contains(e.player.name) = False Then Exit Sub
                End If
            End If

            If e.reason = PlayerDisconnectEventArgs.playerleavereason.listupdate Then Exit Sub
            Thread.Sleep(250)
            Execute(ParseActionParameters_playerdisconnect(ParseActionParameters(action_parameters), e))
        End Sub

        Public Sub delete()
            deleteTask(Me)
        End Sub

        Public Shared Function ParseTrigger(text As String) As trigger
            text = text.ToLower.Trim.Replace(" ", "_")
            Return DirectCast([Enum].Parse(GetType(trigger), text), trigger)
        End Function

        Public Shared Function ParseAction(text As String) As action
            text = text.ToLower.Trim.Replace(" ", "_")
            Return DirectCast([Enum].Parse(GetType(action), text), action)
        End Function

        Public Shared Function parseTimeSpan(text As String) As TimeSpan
            If text.Split(":").Length = 1 Then text += ":00:00"
            If text.Split(":").Length = 2 Then text += ":00"
            Return New TimeSpan(CInt(text.Split(":")(0)), CInt(text.Split(":")(1)), CInt(text.Split(":")(2)))
        End Function

        Private Function ParseActionParameters(text As String) As String
            Try
                Log(loggingLevel.Fine, "TaskManager", "Parsing action parameters for " & text, Me.name)

                text = text.Replace("%server-cpu%", ServerCpu)
                text = text.Replace("%gui-cpu%", GuiCpu)
                text = text.Replace("%total-cpu%", TotalCpu)

                text = text.Replace("%server-ram%", ServerMem)
                text = text.Replace("%gui-ram%", GuiMem)
                text = text.Replace("%total-ram%", TotalMem)

                text = text.Replace("%gui-dir%", My.Application.Info.DirectoryPath)
                text = text.Replace("%gui-ver%", My.Application.Info.Version.ToString)

                text = text.Replace("%time-long%", Date.Now.ToLongTimeString)
                text = text.Replace("%time-short%", Date.Now.ToShortTimeString)
                text = text.Replace("%date-long%", Date.Now.ToLongDateString)
                text = text.Replace("%date-short%", Date.Now.ToShortDateString)

                text = text.Replace("%server-running%", running.ToString.ToLower)

                If playerList IsNot Nothing Then _
                    text = text.Replace("%players%", Serialize(playerNameList, ",")) Else _
                    text = text.Replace("%players%", "INVALID")
                If playerList IsNot Nothing Then text = text.Replace("%playercount%", playerList.Count) Else _
                    text = text.Replace("%players%", "0")
                If playerList IsNot Nothing AndAlso playerList.Count > 0 Then _
                    text = text.Replace("%lastplayer%", playerList.Last.name) Else _
                    text = text.Replace("%lastplayer%", "INVALID")
                Log(loggingLevel.Fine, "TaskManager", "Parsed action parameters: " & text, Me.name)
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Severe error in ParseActionParameters! " & ex.Message,
                    Me.name)
            End Try
            Return text
        End Function

        Private Function ParseActionParameters_playerjoin(text As String, pj As PlayerJoin) As String
            Try
                Log(loggingLevel.Fine, "TaskManager", "Parsing action parameters (join) for " & text, Me.name)
                If pj.player.name IsNot Nothing Then text = text.Replace("%join-name%", pj.player.name)
                If pj.player.IP IsNot Nothing Then text = text.Replace("%join-ip%", pj.player.IP)
                text = text.Replace("%join-op%", pj.player.OP)
                text = text.Replace("%join-whitelist%", pj.player.WhiteList)
                text = text.Replace("%join-time%", pj.player.time)
                Log(loggingLevel.Fine, "TaskManager", "Parsed action parameters (join): " & text, Me.name)
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager", "Severe error in ParseActionParameters_playerjoin!",
                    ex.Message)
            End Try
            Return (text)
        End Function

        Private Function ParseActionParameters_playerdisconnect(text As String, e As PlayerDisconnectEventArgs) _
            As String
            Try
                Log(loggingLevel.Fine, "TaskManager", "Parsing action parameters (disconnect) for " & text)
                If e.player.name IsNot Nothing Then text = text.Replace("%leave-name%", e.player.name)
                Select Case e.reason
                    Case PlayerDisconnectEventArgs.playerleavereason.leave
                        Dim d As PlayerLeave = e.details
                        If d.reason IsNot Nothing Then _
                            text = text.Replace("%leave-reason%", "disconnected: " & d.reason) Else _
                            text = text.Replace("%leave-reason%", "disconnected")
                    Case PlayerDisconnectEventArgs.playerleavereason.kick
                        Dim d As PlayerKick = e.details
                        If d.CommandSender IsNot Nothing Then _
                            text = text.Replace("%leave-reason%", "kicked by " & d.CommandSender) Else _
                            text = text.Replace("%leave-reason%", "kicked")
                    Case PlayerDisconnectEventArgs.playerleavereason.ban
                        Dim d As playerBan = e.details
                        If d.CommandSender IsNot Nothing Then _
                            text = text.Replace("%leave-reason%", "banned by " & d.CommandSender) Else _
                            text = text.Replace("%leave-reason%", "banned")
                End Select
                Log(loggingLevel.Fine, "TaskManager", "Parsed action parameters (disconnect): " & text)
            Catch ex As Exception
                Log(loggingLevel.Severe, "TaskManager",
                    "Severe error in ParseActionParameters_playerdisconnect!", ex.Message)
            End Try
            Return (text)
        End Function

#Region "IDisposable Support"

        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class
End Namespace