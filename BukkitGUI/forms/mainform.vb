Imports System.Threading
Imports System.IO

Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.Utilities
Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.BukkitGUI.Utilities
Imports Microsoft.Win32
Imports System.Text.RegularExpressions

Namespace Core
    Public Class mainform

        'hnd sub: handles something
        'thds sub: sub that runs thread safe
        'thd sub: sub that shouldn't be ran at the main thread
        'tmr sub: sub that is related to a timer

        Private tmr_update_stats As Timers.Timer 'Timer to update some UI stuff, like performance info
        Private thread_read_out As Thread, thread_read_error As Thread 'Threads to read in and output
        Private threads_work As Boolean = True 'False to stop the threads

        Private initialize_completed As Boolean = False

        ''' <summary>
        ''' Allow output emulation in debug. Add debug caption to top
        ''' </summary>
        ''' <remarks></remarks>
#If DEBUG Then
        Private ReadOnly caption As String = "[DEBUG] BukkitGUI v" & My.Application.Info.Version.ToString

        Private Sub debughook(s As Object, e As KeyEventArgs) Handles Me.KeyUp, TabCtrlMain.KeyUp, ARTXTServerOutput.KeyUp
            If e.Control And e.KeyCode = Keys.E Then
                Dim text = InputBox("emulated console output", "debug emulator")
                hnd_handle_output(text)
            End If
        End Sub
#Else

Private ReadOnly caption As String = "BukkitGUI v" & My.Application.Info.Version.ToString 

#End If

        Public usedfont As Font = New Font("Arial", 10)

        Private starttime As Date
        Private stoptime As Date

        Const WAIT_CREATION_TIMEOUT As UInt16 = 100
        Const MAX_WAIT_CREATION_CYCLES As Byte = 100

        Private Sub mainform_Load(sender As System.Object, e As System.EventArgs) Handles Me.Load

            livebug.write(loggingLevel.Fine, "mainform", "Starting to load mainform")
            Me.Text = caption

            common.mainwinhnd = Me.Handle
            livebug.write(loggingLevel.Fine, "mainform", "Mainform window handle saved!")

            'Removing disabled tasks + altering layout of general tab
            '
            If common.isRunningLight Then 'in light mode, tabs are hidden, bit smaller window
                TabCtrlMain.TabPages.Remove(TabErrorLogging)
                TabCtrlMain.TabPages.Remove(TabServerOptions)
                TabCtrlMain.TabPages.Remove(TabPlugins)
                TabCtrlMain.TabPages.Remove(TabTaskManager)
                TabCtrlMain.TabPages.Remove(TabBackups)
                PanelPerformanceInfo.Visible = False
                BtnGeneralRestart.Location = New Drawing.Point(87, 19)
                BtnGeneralReload.Location = New Drawing.Point(168, 19)
                BtnGeneralKill.Location = New Drawing.Point(249, 19)
                GbGeneralInfo.Size = New Drawing.Size(817, 50)
                lblGeneralTimeSinceStartText.Location = New Drawing.Point(330, 30)
                GBGeneralGeneral.Anchor -= AnchorStyles.Bottom
                GbGeneralInfo.Anchor -= AnchorStyles.Bottom
                Me.Size = New Drawing.Size(Me.Width, Me.Height - 100)
                GBGeneralGeneral.Anchor += AnchorStyles.Bottom
                GbGeneralInfo.Anchor += AnchorStyles.Bottom
                GbGeneralInfo.Location = New Drawing.Point(GbGeneralInfo.Location.X, GbGeneralInfo.Location.Y + 50)
                Me.Size = New Drawing.Size(850, 600)
            End If


            tmr_update_stats = New Timers.Timer
            set_handles()

            ARTXTServerOutput.AutoScrollDown = True

            starttime = Date.Now
            'Load UI
            'We're running all init and load calls that couldn't be ran from the splashscreen
            'Everything is ran inside try..catch blocks so if one thing fails, other stuff continues to load.
            Try
                superstart_init() 'init the superstart tab

                Try 'init the plugin manager
                    If Not common.isRunningLight Then Me.pluginmanager_install_init()
                    If Not common.isRunningLight And InstalledPluginManager.loaded = True Then LoadInstalledPlugins(plugins)
                Catch plex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Error: Could not initialize pluginmanager UI", plex.Message)
                End Try

                info_load() 'load info in settings & info tab

                'tray and sound
                If Not common.isRunningLight Then tray_sound_init() Else GBOptionsInfoAboutTray.Enabled = False : GBOptionsInfoSound.Enabled = False

                Settings_init() 'GUI settings (UI only, already loaded in splashscreen)

                If Not common.isRunningLight Then ErrorManager_InitUI() 'error manager tab

                Try 'backups
                    If Not common.isRunningLight Then TaskManager_UpdateUI()
                    If Not common.isRunningLight Then BackupManager_UpdateUI()
                Catch plex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Error: Could not initialize Task/Backupmanager UI", plex.Message)
                End Try

                text_options_init() 'console output coloring

                Try
                    If Not common.isRunningLight Then Server_settings_refresh_UI() 'server settings
                Catch plex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Error: Could not initialize ServerSettings UI", plex.Message)
                End Try

                'all needed stuff is loaded. Set this as true so the GUI knows it can do everything.
                initialize_completed = True


                'Some additional checks, that aren't required.
                '===========================================================

                'Do we need to start the server right away?
                autoserverstart_check() 'check if the server should be started, by the autostart on GUI start setting.

                'resource monitor
                If Not common.isRunningLight Then
                    tmr_update_stats.Interval = 500 ' Timer to update the UI with new RAM and CPU data
                    tmr_update_stats.Enabled = True
                    tmr_update_stats.Start()
                End If

                If Not common.isRunningLight Then AdvancedOptions.AdvancedOptions_Load()

                'Sometimes, due to downloading at high speed, downloads are finished before mainform is loaded
                'In this case, events might not be handles
                'These lines fix most of these problems.
                hnd_ip_loaded(serverinteraction.external_ip_chache) 'update the IP
                If Net.Bertware.Get.LatestRecommendedVersion IsNot Nothing Then hnd_last_version_loaded(Net.Bertware.Get.LatestRecommendedVersion) 'update last retrieved version
                If Not common.isRunningLight Then thds_SetBukGetPlugins(BukGetAPI.pluginlist) 'set plugin list
                livebug.write(loggingLevel.Fine, "mainform", "finished to load mainform")

                If common.isRunningLight Then
                    livebug.dispose(True)
                End If

            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Error: Could not initialize mainform", ex.Message)
            End Try
        End Sub

        Private Sub mainform_closing(sender As System.Object, e As FormClosingEventArgs) Handles Me.FormClosing
            livebug.write(loggingLevel.Fine, "mainform", "Closing mainform")
            If server.running Then
                If lblStatusBarServerState.Text.ToLower.Contains("stopping") Then
                    If MessageBox.Show(lr("It seems that the server stopped responding. Do you want to kill the server?"), lr("Server not responding"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        kill_server()
                    Else
                        e.Cancel = True
                    End If
                Else
                    If server.CurrentServerType <> McInteropType.remote And e.CloseReason <> CloseReason.WindowsShutDown Then
                        Dim res As DialogResult = MessageBox.Show(lr("The server is still running. Do you want to stop the server and exit the GUI?"), lr("Stop server?"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If res = DialogResult.Yes Then
                            StopServer()
                            AddHandler ServerStopped, AddressOf SafeFormClose
                            e.Cancel = True
                        ElseIf res = Windows.Forms.DialogResult.Cancel Then
                            e.Cancel = True
                        End If
                    End If
                    If e.CloseReason = CloseReason.WindowsShutDown Then
                        StopServer()
                        AddHandler ServerStopped, AddressOf SafeFormClose
                        e.Cancel = True
                    End If
                End If
            End If
            If e.Cancel = False Then
                Me.Tray.Visible = False
                stoptime = Date.Now

            End If
        End Sub

        ''' <summary>
        ''' If we need to go to tray, make sure we do. (prevents the GUI to be minimized instead of hidden due to a bug on some systems)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub mainform_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
            If tray_minimize And Me.WindowState = FormWindowState.Minimized Then SendToTray()
        End Sub

        ''' <summary>
        ''' Close form thread-safe
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub SafeFormClose()
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf SafeFormClose)
                Me.Invoke(d, New Object())
            Else
                Me.Close()
            End If
        End Sub

        ''' <summary>
        ''' Set all dynamic handles
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub set_handles()
            'Way not all events are handled this way
            'Though, some important are done this way
            'This allows to remove the handlers dynamicly

            If common.mainwinhnd <> Me.Handle Then livebug.write(loggingLevel.Warning, "mainform", "Mainform window handle save invalid!") : common.mainwinhnd = Me.Handle : livebug.write(loggingLevel.Fine, "mainform", "Mainform window handle saved!")

            AddHandler Me.FormClosed, AddressOf common.dispose
            AddHandler Me.FormClosed, AddressOf KillThreads

            AddHandler tmr_update_stats.Elapsed, AddressOf hnd_tmr_update_stats

            AddHandler BukkitTools.BukkitVersionFetchComplete, AddressOf BukkitVersionFetchComplete

            AddHandler server.ServerStarted, AddressOf hnd_server_started
            AddHandler server.ServerStopped, AddressOf hdn_server_stop
            AddHandler server.ServerStarting, AddressOf hnd_server_starting

            AddHandler serverOutputHandler.PlayerJoin, AddressOf hnd_PlayerJoin
            AddHandler serverOutputHandler.PlayerDisconnect, AddressOf hnd_PlayerDisconnect

            AddHandler InstalledPluginManager.InstalledPluginsLoaded_Base, AddressOf LoadInstalledPlugins
            AddHandler InstalledPluginManager.InstalledPluginsLoaded_Full, AddressOf LoadInstalledPlugins

            AddHandler BukGetAPI.PluginListLoaded, AddressOf thds_SetBukGetPlugins

            AddHandler TaskManager.TasksLoaded, AddressOf TaskManager_UpdateUI

            AddHandler serverinteraction.IP_Loaded, AddressOf hnd_ip_loaded
            AddHandler Bertware.Get.api.LatestRecommededVersionLoaded, AddressOf hnd_last_version_loaded

            AddHandler Tray.DoubleClick, AddressOf ShowFromTray

            AddHandler ErrorManager.ErrorLVICreated, AddressOf errorLVICreated
            AddHandler BackupManager.BackupsLoaded, AddressOf BackupManager_UpdateUI

            AddHandler serverOutputHandler.CheckUILists, AddressOf playerlist_CheckUIList
        End Sub

        Private Sub remove_handles()
            'Way not all events are handled this way
            'Though, some important are done this way
            'This allows to remove the handlers dynamicly

            RemoveHandler Me.FormClosed, Nothing
            RemoveHandler Me.FormClosed, Nothing

            RemoveHandler tmr_update_stats.Elapsed, Nothing

            RemoveHandler BukkitTools.BukkitVersionFetchComplete, AddressOf BukkitVersionFetchComplete

            RemoveHandler server.ServerStarted, AddressOf hnd_server_started
            RemoveHandler server.ServerStopped, AddressOf hdn_server_stop
            RemoveHandler server.ServerStarting, AddressOf hnd_server_starting

            RemoveHandler serverOutputHandler.PlayerJoin, AddressOf hnd_PlayerJoin
            RemoveHandler serverOutputHandler.PlayerDisconnect, AddressOf hnd_PlayerDisconnect

            RemoveHandler InstalledPluginManager.InstalledPluginsLoaded_Base, AddressOf LoadInstalledPlugins
            RemoveHandler InstalledPluginManager.InstalledPluginsLoaded_Full, AddressOf LoadInstalledPlugins

            RemoveHandler BukGetAPI.PluginListLoaded, AddressOf thds_SetBukGetPlugins

            RemoveHandler TaskManager.TasksLoaded, AddressOf TaskManager_UpdateUI

            RemoveHandler serverinteraction.IP_Loaded, AddressOf hnd_ip_loaded
            RemoveHandler Bertware.Get.api.LatestRecommededVersionLoaded, AddressOf hnd_last_version_loaded

            RemoveHandler Tray.DoubleClick, Nothing

            RemoveHandler ErrorManager.ErrorLVICreated, AddressOf errorLVICreated
        End Sub

        ''' <summary>
        ''' Kill the threads that read the server output
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub KillThreads()
            livebug.write(loggingLevel.Fine, "mainform", "Killing threads...")
            If thread_read_error IsNot Nothing AndAlso thread_read_error.IsAlive AndAlso (thread_read_error.ThreadState = ThreadState.Running Or thread_read_error.ThreadState = ThreadState.Background) Then
                Try
                    thread_read_error.Abort()
                    Thread.Sleep(10)
                    thread_read_error = Nothing
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Could not dispose thread_read_Error")
                End Try
            End If
            If thread_read_out IsNot Nothing AndAlso thread_read_out.IsAlive AndAlso (thread_read_out.ThreadState = ThreadState.Running Or thread_read_out.ThreadState = ThreadState.Background) Then
                Try
                    thread_read_out.Abort()
                    Thread.Sleep(10)
                    thread_read_out = Nothing
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Could not dispose thread_read_out")
                End Try
            End If

            If tmr_update_stats IsNot Nothing Then
                tmr_update_stats.Enabled = False
            End If
        End Sub

#Region "Server management"
        ''' <summary>
        ''' Starts or stops the server, depending on the current state
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub toggle_server() Handles BtnGeneralStartStop.Click
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf toggle_server)
                Me.Invoke(d, New Object())
            Else
                If server.running = False Then start_server() Else stop_server()
            End If
        End Sub

        ''' <summary>
        ''' Start the server
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub start_server() Handles BtnSuperStartLaunch.Click

            RemoveHandler server.ServerStopped, AddressOf start_server

            serverOutputHandler.UTF8Compatibility = ChkTextUTF8.Checked

            If Me.Created = False Then
                Dim i As Byte = 0
                While Me.Created = False
                    Thread.Sleep(WAIT_CREATION_TIMEOUT)
                    i = i + 1
                    If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                End While
            End If

            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf start_server)
                Me.Invoke(d, New Object())
            Else
                Tray.Visible = False
                Tray.Visible = True

                Try
                    If CBSuperstartServerType.SelectedItem Is Nothing Then
                        livebug.write(loggingLevel.Warning, "Mainform", "No settings for superstart available. Checking if they can be loaded")
                        superstart_init()
                        If CBSuperstartServerType.SelectedItem Is Nothing Then Exit Sub
                    End If
                    livebug.write(loggingLevel.Fine, "Mainform", "Starting new server - selected type:" & CBSuperstartServerType.SelectedItem.ToString)

                    ALVPlayersPlayers.Items.Clear()
                    ALVGeneralPlayers.Items.Clear()
                    ALVErrors.Items.Clear()

                    ARTXTServerOutput.Text = ""

                    If Not superstart_validate(True) Then Exit Sub 'Check if everything is filled in

                    'DETECT WRONG SERVER TYPES
                    Dim jarfile = TxtSuperstartJavaJarFile.Text.ToLower
                    Select Case CBSuperstartServerType.SelectedIndex

                        Case 0 'bukkit


                            If Regex.IsMatch(jarfile, "minecraft_server\.jar") Then
                                WrongServerTypePopup("bukkit", 1) 'bukkit (0) must become 1
                            End If

                            If Regex.IsMatch(jarfile, "(.)*technic([^/\\])*\.jar") Then
                                WrongServerTypePopup("bukkit", 1) 'bukkit (0) must become 1
                            End If

                            If Regex.IsMatch(jarfile, "(.*)hexxit([^/\\])*\.jar") Then
                                WrongServerTypePopup("bukkit", 1) 'bukkit (0) must become 1
                            End If

                        Case 1 'vanilla
                            If Regex.IsMatch(jarfile, "(.)*bukkit([^/\\])*\.jar") Then
                                WrongServerTypePopup("vanilla", 0) 'vanilla (1) must become 0
                            End If

                            If Regex.IsMatch(jarfile, "(.)*spigot([^/\\])*\.jar") Then
                                WrongServerTypePopup("vanilla", 0) 'vanilla (1) must become 0
                            End If

                            If Regex.IsMatch(jarfile, "(.)*mcpc([^/\\])*\.jar") Then
                                WrongServerTypePopup("vanilla", 0) 'vanilla (1) must become 0
                            End If
                        Case 3 'Generic java
                            If Regex.IsMatch(jarfile, "(.)*bukkit([^/\\])*\.jar") Then
                                WrongServerTypePopup("Generic java", 0) 'vanilla (1) must become 0
                            End If

                            If Regex.IsMatch(jarfile, "(.)*spigot([^/\\])*\.jar") Then
                                WrongServerTypePopup("Generic java", 0) 'vanilla (1) must become 0
                            End If

                            If Regex.IsMatch(jarfile, "(.)*mcpc([^/\\])*\.jar") Then
                                WrongServerTypePopup("Generic java", 0) 'vanilla (1) must become 0
                            End If
                            If Regex.IsMatch(jarfile, "minecraft_server\.jar") Then
                                WrongServerTypePopup("Generic java", 1) 'bukkit (0) must become 1
                            End If

                            If Regex.IsMatch(jarfile, "(.*)technic([^/\\])*\.jar") Then
                                WrongServerTypePopup("Generic java", 1) 'bukkit (0) must become 1
                            End If

                            If Regex.IsMatch(jarfile, "(.*)hexxit([^/\\])*\.jar") Then
                                WrongServerTypePopup("Generic java", 1) 'bukkit (0) must become 1
                            End If
                    End Select



                    Select Case CBSuperstartServerType.SelectedIndex
                        Case 0
                            livebug.write(loggingLevel.Fine, "Mainform", "Starting new bukkit server...")

                            If FileIO.FileSystem.FileExists(TxtSuperstartJavaJarFile.Text) = False Then
                                MessageBox.Show(lr("The server could not be started: the .jar file specified could not be found. Please make sure all superstart settings are valid."), lr("Could't start server"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            Try
                                If ChkSuperStartRetrieveCurrent.Checked Then
                                    If Regex.IsMatch(jarfile, "(.*)bukkit(.*)\.jar") Then
                                        CheckBukkitVersion()
                                    Else
                                        thds_write_output(New thds_pass_servermessage("[GUI] Didn't check bukkit version, you're not running bukkit.", clrWarning))
                                    End If

                                End If
                            Catch ex As Exception
                                livebug.write(loggingLevel.Warning, "Mainform", "Error in bukkit version check/auto update", ex.Message)
                                thds_write_output(New thds_pass_servermessage("[GUI] Error in bukkit version check/auto update. Server continues to start...", clrSevere))
                                thds_write_output(New thds_pass_servermessage("[GUI] If this error keeps coming up, disable ""retrieve version"" and ""auto update"" functions in superstart tab", clrSevere))
                            End Try
                            thds_write_output(New thds_pass_servermessage("[GUI] " & lr("Starting bukkit server") & " - " & lr("min. RAM:") & TBSuperstartJavaMinRam.Value & " " & lr("max. RAM:") & TBSuperstartJavaMaxRam.Value, clrInfo))

                            Dim ja As New javaStartArgs(CBSuperstartJavaJRE.SelectedIndex, TBSuperstartJavaMinRam.Value, TBSuperstartJavaMaxRam.Value, TxtSuperstartJavaJarFile.Text, TxtSuperstartJavaCustomArgs.Text, TxtSuperstartJavaCustomSwitch.Text, McInteropType.bukkit)
                            server.StartServer(ja, McInteropType.bukkit)
                        Case 1
                            livebug.write(loggingLevel.Fine, "Mainform", "Starting new vanilla server...")
                            thds_write_output(New thds_pass_servermessage("[GUI]" & lr("Starting vanilla server") & " - " & lr("min. RAM:") & TBSuperstartJavaMinRam.Value & " - " & lr("max. RAM:") & TBSuperstartJavaMaxRam.Value, clrInfo))
                            Dim ja As New javaStartArgs(CBSuperstartJavaJRE.SelectedIndex, TBSuperstartJavaMinRam.Value, TBSuperstartJavaMaxRam.Value, TxtSuperstartJavaJarFile.Text, TxtSuperstartJavaCustomArgs.Text, TxtSuperstartJavaCustomSwitch.Text, McInteropType.vanilla)
                            server.StartServer(ja, McInteropType.vanilla)
                        Case 2
                            thds_write_output(New thds_pass_servermessage("[GUI]" & lr("Starting new remote jsonapi server..."), clrInfo))
                            Dim rs As New RemoteJSONAPI
                            Dim rcred As New RemoteCredentials()
                            With rcred
                                .Host = TxtSuperstartRemoteHost.Text
                                .login = TxtSuperstartRemoteUsername.Text
                                .password = MTxtSuperstartRemotePassword.Text
                                .salt = MTxtSuperstartRemoteSalt.Text
                                .port = NumSuperstartRemotePort.Value
                            End With
                            rs.Credentials = rcred
                            server.StartServer(rs)
                        Case 3
                            livebug.write(loggingLevel.Fine, "Mainform", "Starting new java server...")
                            thds_write_output(New thds_pass_servermessage("[GUI] " & lr("Starting java server") & " - " & lr("min. RAM:") & TBSuperstartJavaMinRam.Value & lr("max. RAM:") & TBSuperstartJavaMaxRam.Value))
                            thds_write_output(New thds_pass_servermessage("[GUI] Generic java isn't a recommended server type.", clrWarning))
                            Dim ja As New javaStartArgs(CBSuperstartJavaJRE.SelectedIndex, TBSuperstartJavaMinRam.Value, TBSuperstartJavaMaxRam.Value, TxtSuperstartJavaJarFile.Text, TxtSuperstartJavaCustomArgs.Text, TxtSuperstartJavaCustomSwitch.Text, McInteropType.java)
                            server.StartServer(ja, McInteropType.java)
                    End Select
                    TabCtrlMain.SelectedTab = TabGeneral
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "Mainform", "Severe exception at start_server!", ex.Message)
                End Try
            End If
        End Sub

        Private Sub WrongServerTypePopup(currentserver As String, newid As Byte)
            livebug.write(loggingLevel.Fine, "Mainform", "Wrong server type detected, asking user", TxtSuperstartJavaJarFile.Text)
            Dim server = "bukkit"
            If newid = 1 Then server = "vanilla"

            Select Case MessageBox.Show("This seems to be a " & server & " based server. You have currently set the server type to " & currentserver _
                                        & ". Please set the server type to " & server & " in the superstart tab. Do you want to fix this automaticly?" _
                                          & " A wrong server type might cause the server to misbehave." _
                                        , "Wrong server type", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                Case Windows.Forms.DialogResult.Yes
                    CBSuperstartServerType.SelectedIndex = newid
                    config.write("server", newid, "superstart")
                    livebug.write(loggingLevel.Fine, "Mainform", "Fixed wrong server type")
                    Exit Sub
                Case Windows.Forms.DialogResult.No
                    livebug.write(loggingLevel.Fine, "Mainform", "Cancelling server start")
                    Exit Sub
                Case Windows.Forms.DialogResult.Cancel
                    livebug.write(loggingLevel.Fine, "Mainform", "Ignoring server type error")
            End Select
        End Sub

        Private Sub CheckBukkitVersion()
            thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Retrieving your current bukkit version..."), clrInfo))

            If New FileInfo(TxtSuperstartJavaJarFile.Text).Name.ToLower.Contains("bukkit") = False Then
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("WARNING: No craftbukkit file selected, bukkit version can't be determined. Auto update check cancelled."), clrWarning))
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("WARNING: Turn of ""retrieve current version"" in superstart > maintainance"), clrWarning))
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("WARNING: or make sure the .jar file contains bukkit"), clrWarning))
                Exit Sub
            End If


            Dim bukkitVersion As BukkitVersionDetails = BukkitTools.GetCurrentBukkitVersion(CBSuperstartJavaJRE.SelectedIndex, TxtSuperstartJavaJarFile.Text)
            Dim ver As UInt16 = bukkitVersion.Build
            If ver < 1 Then
                livebug.write(loggingLevel.Warning, "mainform", "Could not retrieve current bukkit version", "CheckBukkitVersion()")
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Your current bukkit version couldn't be determined"), clrWarning))
                Exit Sub
            End If

            livebug.write(loggingLevel.Fine, "mainform", "Current bukkit version:" & ver, "CheckBukkitVersion()")
            thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Your current bukkit version is") & " " & bukkitVersion.ToString, clrInfo))
            If BukkitTools.Fetched Then livebug.write(loggingLevel.Fine, "mainform", "Latest recommended build:" & BukkitTools.Recommended_info.build, "CheckBukkitVersion()") Else livebug.write(loggingLevel.Fine, "mainform", "Latest recommended build not fetched. Auto update not available", "CheckBukkitVersion()")

            If BukkitTools.Fetched AndAlso ver < BukkitTools.Latest_Recommended Then
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("A new bukkit version is available:") & " #" & BukkitTools.Recommended_info.build & " : " & BukkitTools.Recommended_info.version, Color.Green))

                If ChkSuperstartAutoUpdate.Checked Or ChkSuperstartAutoUpdateNotify.Checked Then
                    Dim res As DialogResult
                    If ChkSuperstartAutoUpdateNotify.Checked Then
                        livebug.write(loggingLevel.Fine, "mainform", "Asking for auto update", "CheckBukkitVersion()")
                        res = MessageBox.Show(lr("A new version for bukkit is available:") & " #" & BukkitTools.Recommended_info.build & " : " & BukkitTools.Recommended_info.version & vbCrLf & "Auto-update now?", "Bukkit update available", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    End If

                    If ChkSuperstartAutoUpdate.Checked Then res = Windows.Forms.DialogResult.OK

                    If res = Windows.Forms.DialogResult.OK Or res = Windows.Forms.DialogResult.Yes Then
                        livebug.write(loggingLevel.Fine, "mainform", "Starting auto update", "CheckBukkitVersion()")
                        BukkitTools.Download(BukkitVersionType.rb, TxtSuperstartJavaJarFile.Text)
                        livebug.write(loggingLevel.Fine, "mainform", "Auto update completed, resuming server start", "mainform")
                        thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Bukkit was updated to the latest version:") & " #" & BukkitTools.Recommended_info.build & " (" & BukkitTools.Recommended_info.version & ")", Color.Green))
                    End If
                End If

            End If

        End Sub
        ''' <summary>
        ''' Stop the server
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub stop_server()

            If Me.Created = False Then
                Dim i As Byte = 0
                While Me.Created = False
                    Thread.Sleep(WAIT_CREATION_TIMEOUT)
                    i = i + 1
                    If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                End While
            End If

            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf stop_server)
                Me.Invoke(d, New Object())
            Else
                livebug.write(loggingLevel.Fine, "mainform", "Stopping server...", "stop_server()")
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Stopping the server..."), clrInfo))
                Try
                    If server.CurrentServerType = McInteropType.remote Then
                        If server.RemoteServerObject IsNot Nothing Then server.RemoteServerObject.Close()
                    Else
                        server.SendCommand("stop")
                        thds_setserverstate(lr("Stopping server"))
                        BtnGeneralReload.Enabled = False
                        BtnGeneralRestart.Enabled = False
                        BtnGeneralSendCmd.Enabled = False
                        BtnGeneralStartStop.Enabled = False
                        BtnSuperStartLaunch.Enabled = False
                    End If
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Severe exception at stop_server!", ex.Message)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Restart the server completely
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub restart_server() Handles BtnGeneralRestart.Click
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf restart_server)
                Me.Invoke(d, New Object())
            Else
                livebug.write(loggingLevel.Fine, "mainform", "Restarting server")
                thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Restarting the server..."), clrInfo))
                stop_server()
                AddHandler server.ServerStopped, AddressOf start_server
            End If
        End Sub

        ''' <summary>
        ''' Reload the server
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub reload_server() Handles BtnGeneralReload.Click
            server.SendCommand("reload", True)
            thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Reloading the server..."), clrInfo))
        End Sub

        Private Sub kill_server() Handles BtnGeneralKill.Click
            If server.host IsNot Nothing AndAlso server.host.HasExited = False AndAlso MessageBox.Show(lr("Do you really want to kill the server? This can corrupt your data!"), lr("Kill server?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Try
                    server.host.Kill()
                Catch ex As Exception
                    MessageBox.Show(lr("The server couldn't be killed - probably it has been shut down already"), lr("Failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

        End Sub

        Private Sub server_send_command_keyhandler(s As Object, e As KeyEventArgs) Handles TxtGeneralServerIn.KeyUp
            If e.KeyCode = Keys.Enter Then
                server_send_command_override() 'no override
            ElseIf e.Control And e.KeyCode = Keys.S Then
                TxtGeneralServerIn.Text = TxtGeneralServerIn.Text.Trim.TrimStart("/")
                server_send_command_override("say " & Me.TxtGeneralServerIn.Text)
            End If
        End Sub

        ''' <summary>
        ''' If enter pressed in textbox, or "send command" button pressed, send command to server
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub server_send_command_buttonhandler() Handles BtnGeneralSendCmd.Click
            Try
                Dim prefix As String = "" 'Will be added in front of command
                If ChkGeneralSay.Checked Then prefix = "say " 'Say checkbox

                TxtGeneralServerIn.Text = TxtGeneralServerIn.Text.Trim.TrimStart("/")

                server_send_command_override(prefix & Me.TxtGeneralServerIn.Text)  'send command
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at server_send_command!", ex.Message)
            End Try
        End Sub

        Private Sub server_send_command_override(Optional ByVal command As String = "")
            Try
                TxtGeneralServerIn.Text = TxtGeneralServerIn.Text.Trim.TrimStart("/") 'to be sure this is removed

                If command = "" Then
                    Dim prefix As String = "" 'Will be added in front of command
                    If ChkGeneralSay.Checked Then prefix = "say " 'Say checkbox
                    command = prefix & Me.TxtGeneralServerIn.Text
                End If

                server.SendCommand(command)  'send command

                TxtGeneralServerIn.AutoCompleteCustomSource.Add(Me.TxtGeneralServerIn.Text) 'add to textbox history
                Me.TxtGeneralServerIn.Text = "" 'clear textbox
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at server_send_command!", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' handle starting server
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub hnd_server_starting()
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf hnd_server_starting)
                Me.Invoke(d, New Object())
            Else
                Try
                    thds_write_output(New thds_pass_servermessage("[GUI] " + lr("The server is starting..."), clrInfo))
                    thds_setserverstate(lr("Starting server"))
                    BtnGeneralKill.Enabled = False
                    BtnGeneralReload.Enabled = False
                    BtnGeneralRestart.Enabled = False
                    BtnGeneralSendCmd.Enabled = False
                    BtnGeneralStartStop.Enabled = False
                    BtnSuperStartLaunch.Enabled = False
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Severe exception at hnd_server_starting!", ex.Message)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Handle the started server
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub hnd_server_started()
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf hnd_server_started)
                Me.Invoke(d, New Object())
            Else
                Try
                    thds_setserverstate(lr("Server started"))
                    thds_write_output(New thds_pass_servermessage("[GUI] " + lr("Server started..."), clrInfo))

                    Dim ip As String = serverinteraction.external_ip_chache

                    ServerSettings.LoadSettings()
                    Dim motd As String = ""
                    If Not common.isRunningLight Then motd = ServerSettings.MOTD

                    If server.CurrentServerType <> McInteropType.remote Then
                        If motd = "" Then
                            If ip IsNot Nothing AndAlso ip <> "" AndAlso ip.Contains("error") = False Then
                                thds_setformCaption(caption & " - " & lr("Server running") & " - " & ip)
                            Else
                                thds_setformCaption(caption & " - " & lr("Server running"))
                            End If
                        Else
                            If ip IsNot Nothing AndAlso ip <> "" AndAlso ip.Contains("error") = False Then
                                thds_setformCaption(caption & " - " & lr("Server running") & " - " & ip & " - " & motd)
                            Else
                                thds_setformCaption(caption & " - " & lr("Server running") & " - " & motd)
                            End If
                        End If
                    Else
                        thds_setformCaption(caption & " - " & lr("Connected to remote server") & " - " & config.read("remote_host", "", "superstart"))
                    End If

                    threads_work = True
                    'Initialize threads
                    If server.CurrentServerType <> McInteropType.remote Then
                        thread_read_error = New Thread(AddressOf thd_read_error)
                        thread_read_error.Name = "read_error"
                        thread_read_error.IsBackground = True
                        thread_read_error.Start()
                        thread_read_out = New Thread(AddressOf thd_read_output)
                        thread_read_out.Name = "read_output"
                        thread_read_out.IsBackground = True
                        thread_read_out.Start()
                        BtnGeneralStartStop.Text = lr("Stop")
                    Else
                        thread_read_out = New Thread(AddressOf thd_read_remote)
                        thread_read_out.Name = "read_remote"
                        thread_read_out.IsBackground = True
                        thread_read_out.Start()
                        BtnGeneralStartStop.Text = lr("Disconnect")
                    End If

                    BtnGeneralKill.Enabled = True
                    BtnGeneralReload.Enabled = True
                    BtnGeneralRestart.Enabled = True
                    BtnGeneralSendCmd.Enabled = True
                    BtnGeneralStartStop.Enabled = True
                    BtnSuperStartLaunch.Enabled = False
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Severe exception at hnd_server_started!", ex.Message)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' Handle the stopped server
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub hdn_server_stop()
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf hdn_server_stop)
                Me.Invoke(d, New Object())
            Else
                Try
                    livebug.write(loggingLevel.Info, "mainform", "The server has stopped. Updating UI")
                    thds_setserverstate(lr("Server stopped"))
                    thds_setformCaption(caption & " - " & lr("Server stopped"))

                    ALVPlayersPlayers.Items.Clear()
                    ALVGeneralPlayers.Items.Clear()

                    threads_work = False

                    BtnGeneralKill.Enabled = False
                    BtnGeneralReload.Enabled = False
                    BtnGeneralRestart.Enabled = False
                    BtnGeneralSendCmd.Enabled = False
                    BtnGeneralStartStop.Enabled = True
                    BtnGeneralStartStop.Text = lr("Start")
                    BtnSuperStartLaunch.Enabled = True
                    TxtGeneralServerIn.AutoCompleteCustomSource.Clear()
                    thds_write_output(New thds_pass_servermessage("[GUI] " & lr("The server has stopped..."), clrInfo))
                    livebug.write(loggingLevel.Info, "mainform", "The server has stopped. Updated UI")
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "mainform", "Severe exception at hnd_server_stop!", ex.Message)
                End Try
            End If
        End Sub


        ''' <summary>
        ''' Routine to read StandardOut
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub thd_read_output()

            Try
                Thread.Sleep(50) 'make sure the server is fully started
                'Actually a pretty small sub
                'Most of the work is passed to hnd_handle_output, who handles a part of the content, and passes the rest for further handling
                If server.serverOut Is Nothing Then
                    Thread.Sleep(50)
                    If server.serverOut Is Nothing Then livebug.write(loggingLevel.Warning, "mainform", "Output thread start aborted...") : Exit Sub
                End If


                livebug.write(loggingLevel.Fine, "mainform", "Output thread started...")
                If server.running = False OrElse server.serverOut Is Nothing Then livebug.write(loggingLevel.Warning, "mainform", "Output thread stopped, server stopped immediatly...") : Exit Sub
                Dim sr As New StreamReader(server.serverOut, System.Text.Encoding.GetEncoding(common.Server_encoding)) 'ISO-8859-1
                While server.serverOut IsNot Nothing AndAlso server.running AndAlso threads_work AndAlso server.host.HasExited = False
                    Dim rc As String = ""
                    Try
                        While server.serverOut IsNot Nothing AndAlso server.running AndAlso threads_work AndAlso server.host.HasExited = False AndAlso sr.EndOfStream
                            Thread.Sleep(100)
                        End While
                        rc = sr.ReadLine() 'rc - received content
                    Catch readex As Exception
                        livebug.write(loggingLevel.Warning, "mainform", "Could not read from stream at thd_read_output!", readex.Message)
                    End Try
                    If rc Is Nothing OrElse rc = ">" OrElse rc = "" Then
                    Else

                        hnd_handle_output(rc) 'if not empty and if not default, handle it

                    End If
                End While
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at thd_read_output!", ex.Message)
            End Try
            livebug.write(loggingLevel.Info, "mainform", "thd_read_output exited")
        End Sub


        ''' <summary>
        ''' Routine to read StandardError
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub thd_read_error()

            Try
                Thread.Sleep(50) 'make sure the server is fully started
                'Actually a pretty small sub. Identical to thd_read_output, only different stream.
                'Most of the work is passed to hnd_handle_output, who handles a part of the content, and passes the rest for further handling
                If server.serverError Is Nothing Then
                    Thread.Sleep(50)
                    If server.serverError Is Nothing Then livebug.write(loggingLevel.Warning, "mainform", "Error thread start aborted...") : Exit Sub
                End If

                livebug.write(loggingLevel.Fine, "mainform", "Error thread started...")
                If server.running = False OrElse server.serverError Is Nothing Then livebug.write(loggingLevel.Warning, "mainform", "Error thread stopped, server stopped immediatly...") : Exit Sub
                Dim sr As New StreamReader(server.serverError, System.Text.Encoding.GetEncoding(common.Server_encoding))
                While server.serverError IsNot Nothing AndAlso server.running AndAlso threads_work AndAlso server.host.HasExited = False AndAlso sr IsNot Nothing
                    Dim rc As String = ""
                    Try
                        While server.serverOut IsNot Nothing AndAlso server.running AndAlso threads_work AndAlso server.host.HasExited = False AndAlso sr.EndOfStream
                            Thread.Sleep(100)
                        End While
                        rc = sr.ReadLine() 'rc - received content
                    Catch readex As Exception
                        livebug.write(loggingLevel.Warning, "mainform", "Could not read from stream at thd_read_error!", readex.Message)
                    End Try

                    If rc Is Nothing OrElse rc = ">" OrElse rc = "" Then
                    Else
                        hnd_handle_output(rc) 'if not empty and if not default, handle it
                    End If

                End While
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at thd_read_error!", ex.Message)
            End Try
            livebug.write(loggingLevel.Info, "mainform", "thd_read_error exited")
        End Sub

        ''' <summary>
        ''' Routine to read StandardError
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub thd_read_remote()
            Try
                Thread.Sleep(10) 'make sure the server is fully started
                'Actually a pretty small sub. Identical to thd_read_output, only different stream.
                'Most of the work is passed to hnd_handle_output, who handles a part of the content, and passes the rest for further handling
                livebug.write(loggingLevel.Fine, "mainform", "remote reader thread started...")

                While server.RemoteServerObject IsNot Nothing AndAlso server.running AndAlso threads_work AndAlso server.host.HasExited = False AndAlso server.RemoteServerObject.StandardOut IsNot Nothing
                    If server.RemoteServerObject IsNot Nothing AndAlso server.RemoteServerObject.StandardOut IsNot Nothing AndAlso server.RemoteServerObject.StandardOut.EOS = False Then
                        Dim rcv As String = server.RemoteServerObject.StandardOut.read
                        If rcv IsNot Nothing AndAlso rcv.ToLower.Contains("[api call]") = False Then hnd_handle_output(rcv)
                    End If
                    Thread.Sleep(10)
                End While
            Catch nulex As NullReferenceException
                livebug.write(loggingLevel.Warning, "mainform", "Nullreferenceexception at thd_read_remote!", nulex.Message)
                If server.running OrElse server.RemoteServerObject IsNot Nothing OrElse threads_work Then
                    livebug.write(loggingLevel.Warning, "mainform", "Terminating remote server, due to stopped reader thread!")
                    thds_write_output(New thds_pass_servermessage("[GUI] " & lr("Killed remote server connection due to an error in remote reader thread."), clrSevere))
                    StopServer()
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at thd_read_remote!", ex.Message)
                If server.running OrElse server.RemoteServerObject IsNot Nothing OrElse threads_work Then
                    livebug.write(loggingLevel.Severe, "mainform", "Terminating remote server, due to stopped reader thread!")
                    thds_write_output(New thds_pass_servermessage("[GUI] " & lr("Killed remote server connection, severe error in remote reader thread. Please report this bug."), clrSevere))
                    StopServer()
                End If
            End Try

        End Sub

        ''' <summary>
        ''' Handle output gathered by the read_output and read_error routines
        ''' </summary>
        ''' <remarks>This routine is called by thd_read_error or thd_read_output. This means it's still on a different thread then the main thread.</remarks>
        Private Sub hnd_handle_output(ByVal text As String)
            'We don't want high delays on the text output.
            'To reduce the delays, only vital things (like color, modifying,..) are done in this routine
            'Detection of player joins etc. is done in anothor routine, on a separate thread
            Try
                If text Is Nothing OrElse text = "" OrElse text.Trim = "" OrElse text.Contains("[INFO] Command run by remote user") OrElse text = vbCrLf Then Exit Sub 'check if text is valid

                text = serverOutputHandler.FixJLine(text) 'Fix JLine coolor codes

                Debug.WriteLine("Handling text:" & text, "debug")

                text = FixTextCompat(text) 'fix 1.7.2 output

                Debug.WriteLine("Fixed 1.7.2:" & text, "debug")

                If server.CurrentServerType = McInteropType.remote And text.Contains("[INFO] Command run by remote user") Then Exit Sub 'hide jsonapi, to prevent annoying output every time you connect to a jsonapi server

                Dim Type As MessageType = getMessageType(text) 'get the type of this sentence

                Try 'Create a new thread for parsing output
                    Dim tpo As New Thread(AddressOf serverOutputHandler.LookupAsync)
                    tpo.IsBackground = True
                    tpo.Name = "outputhandler_lookupasync"
                    Dim p As New thds_pass_lookup 'we use this class so everything's inside one argument (for multithreading)
                    p.text = text
                    p.type = Type
                    tpo.Start(p)
                Catch thdex As ThreadStartException
                    livebug.write(loggingLevel.Warning, "mainform", "ThreadStart exception at hnd_handle_output!", thdex.Message)
                End Try




                'HIDDEN TEXT
                'These texts will be hidden:
                If Type = MessageType.list Or Type = MessageType.listcount Then Exit Sub 'Don't output list results

                'Those are /list output strings from plugins, might interfere with /list. Hide these to prevent console spamming
                If text.Contains("[AutoMod] Example: /list") Then Exit Sub '[AutoMod] Example: /list blocked  | [AutoMod] Example: /list trusted
                If text.Contains("[AutoMod] Usage: /list") Then Exit Sub '[AutoMod] Usage: /list <list_name>[action][player_name]
                If text.Contains("[INFO] There are ") And text.Contains(" out of maximum ") And text.Contains(" players online.") Then Exit Sub '[INFO] There are 0 out of maximum 8 players online.

                If AdvancedOptions.SpamFilter IsNot Nothing AndAlso AdvancedOptions.SpamFilter.Count > 0 Then 'if there are strings in the spamfilter, check for them and hide if needed
                    For Each entry As String In SpamFilter
                        If text.Contains(entry) Then Exit Sub
                    Next
                End If

                'Write to output
                If Not ((Type = MessageType.warning And ErrorManager.Hide_Warnings = True) Or (Type = MessageType.severe And ErrorManager.Hide_Errors = True) Or (Type = MessageType.javastacktrace And ErrorManager.Hide_Stacktrace = True)) Then
                    Dim clr As Color = serverOutputHandler.getMessageColor(Type) 'get the color

                    text = serverOutputHandler.rewrite_date(text) 'adjust the date
                    Dim sm As New thds_pass_servermessage(text, clr) 'create message object
                    thds_write_output(sm) 'print
                End If

                'if something MIGHT have changed, run a /list command to be sure
                If Not (Type = MessageType.playerjoin Or Type = MessageType.playerban Or Type = MessageType.playerleave Or Type = MessageType.playerkick) Then
                    If text.Contains("disconnected") Or text.Contains("joined") Or text.Contains("logged in") Or text.Contains("was caught breaking a honeypot block") Then SendCommand("list", True)
                End If

                'Check if the server failed to launch
                If text.Contains("Perhaps a server is already running on that port") And text.ToLower.Contains("[warning]") And (text.ToLower.Contains("[info]")) = False Then 'not contains [info] to make it chat-proof
                    If server.host IsNot Nothing AndAlso server.host.HasExited = False Then server.host.Kill()
                    livebug.write(loggingLevel.Fine, "mainform", "Server couldn't be started, failed to bind to port. Asking user if java must be killed and restarted.")
                    If MessageBox.Show(lr("The server could not be started, reason:") & "Failed to bind to port" & vbCrLf & lr("This is probably caused by another server that is running already.") & vbCrLf _
                                    & lr("Kill all java instances, and retry to start this server?"), lr("Server couldn't be started"), MessageBoxButtons.YesNo, MessageBoxIcon.Error) = Windows.Forms.DialogResult.Yes Then
                        javaAPI.KillAll() 'kill all
                        If server.running = False Then start_server() 'restart
                    End If
                End If

                'tray balloons etc
                'tray must be in this routine
                If ((Not ShowInTaskbar) Or tray_always) Then
                    If Type = MessageType.playerjoin And tray_onPlayerJoin Then
                        Dim e As PlayerJoinEventArgs = serverOutputHandler.getPlayerJoinArgs(text)
                        Tray.ShowBalloonTip(500, lr("Player joined"), e.PlayerJoin.player.name & "(" & e.PlayerJoin.player.IP & ") " & lr("logged in to the server"), ToolTipIcon.Info)
                    End If

                    If (Type = MessageType.playerleave Or Type = MessageType.playerban Or Type = MessageType.playerkick) And tray_onPlayerDisconnect Then
                        Dim e As PlayerDisconnectEventArgs = Nothing
                        Select Case Type
                            Case MessageType.playerleave
                                e = serverOutputHandler.getPlayerLeaveArgs(text)
                                Dim pll = CType(e.details, PlayerLeave)
                                Dim balloontext As String = "Player left"
                                If pll.reason IsNot Nothing AndAlso pll.reason <> "" Then
                                    balloontext = e.player.name & " " & lr("disconnected") & ", " & lr("reason:") & pll.reason.ToString
                                Else
                                    balloontext = e.player.name & " " & lr("disconnected")
                                End If
                                Tray.ShowBalloonTip(500, lr("Player disconnected (Leave)"), balloontext, ToolTipIcon.Info)

                            Case MessageType.playerkick
                                e = serverOutputHandler.getPlayerKickArgs(text)
                                Tray.ShowBalloonTip(500, lr("Player disconnected (Kick)"), e.player.name & " " & lr("disconnected") & ", " & lr("reason:") & " Kicked by " & CType(e.details, PlayerKick).CommandSender.ToString, ToolTipIcon.Info)
                            Case MessageType.playerban
                                e = serverOutputHandler.getPlayerBanArgs(text)
                                Tray.ShowBalloonTip(500, lr("Player disconnected (Ban)"), e.player.name & " " & lr("disconnected") & ", " & lr("reason:") & " Banned by " & CType(e.details, playerBan).CommandSender.ToString, ToolTipIcon.Info)
                        End Select

                    End If

                    If Type = MessageType.warning And tray_onWarning Then
                        Tray.BalloonTipIcon = ToolTipIcon.Warning
                        Tray.BalloonTipText = serverOutputHandler.getWarningArgs(text).message
                        Tray.BalloonTipTitle = lr("Warning")
                        Tray.ShowBalloonTip(500)

                    End If

                    If Type = MessageType.severe And tray_onSevere Then
                        Tray.BalloonTipIcon = ToolTipIcon.Error
                        Tray.BalloonTipText = serverOutputHandler.getSevereArgs(text).message
                        Tray.BalloonTipTitle = lr("Severe")
                        Tray.ShowBalloonTip(500)
                    End If
                End If
            Catch memex As OutOfMemoryException
                If Not isRunningLight Then
                    Try
                        thds_write_output(New thds_pass_servermessage("[GUI] OUT OF MEMORY EXCEPTION! Try running light mode, enable it in the options tab.", clrSevere))
                    Catch ex As Exception
                        livebug.write(loggingLevel.Warning, "mainform", "hnd_handle_output out of memory. Could not broadcast outofmemory exception to console", ex.Message)
                    End Try
                Else
                    livebug.write(loggingLevel.Warning, "mainform", "hnd_handle_output out of memory! (light mode enabled)", memex.Message)
                End If

            Catch uaex As UnauthorizedAccessException
                livebug.write(loggingLevel.Warning, "mainform", "UnauthorizedAccess exception at hnd_handle_output!", uaex.Message)
            Catch sex As Security.SecurityException
                livebug.write(loggingLevel.Warning, "mainform", "Security exception at hnd_handle_output!", sex.Message)
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at hnd_handle_output!", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Write server output to the textbox
        ''' </summary>
        ''' <param name="servermsg">The text to write, as a servermessage object</param>
        ''' <remarks></remarks>
        Private Sub thds_write_output(servermsg As thds_pass_servermessage)
            Try
                If servermsg Is Nothing OrElse servermsg.fulltext Is Nothing Then Exit Sub

                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If

                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf thds_write_output)
                    Me.Invoke(d, New Object() {servermsg})
                Else
                    servermsg.fulltext = servermsg.fulltext.Normalize
                    LblStatusBarServerOutput.Text = servermsg.fulltext
                    ARTXTServerOutput.AppendText(servermsg.fulltext, servermsg.color)
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in thds_write_output", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Set the server status at the bottom of the page. Status will be translated automaticly
        ''' </summary>
        ''' <param name="state">the state, will be translated</param>
        ''' <remarks></remarks>
        Private Sub thds_setserverstate(state As String)
            Try
                If state Is Nothing Then Exit Sub

                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If

                If Me.InvokeRequired Then
                    Dim ccb As New ContextCallback(AddressOf thds_setserverstate)
                    Me.Invoke(ccb, New Object() {state})
                Else
                    lblStatusBarServerState.Text = lr("status") & ": " & lr(state)
                    Tray.Text = "BukkitGUI - " & lr(state)
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in thds_setserverstate", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Set the caption of the mainform
        ''' </summary>
        ''' <param name="caption">The caption to set</param>
        ''' <remarks></remarks>
        Private Sub thds_setformCaption(caption As String)
            Try
                If caption Is Nothing Then Exit Sub

                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If

                If Me.InvokeRequired Then
                    Dim ccb As New ContextCallback(AddressOf thds_setformCaption)
                    Me.Invoke(ccb, New Object() {caption})
                Else
                    Me.Text = caption
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in thds_setFormCaption", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' This routine is called by a timer, to update RAM / CPU / Time running
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        Private _hnd_tmr_update_stats_cpuerror As Boolean = False
        Private _hnd_tmr_update_stats_ramerror As Boolean = False
        Private Sub hnd_tmr_update_stats()
            Try

                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If

                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf hnd_tmr_update_stats)
                    Me.Invoke(d, New Object())
                Else
                    LblGeneralTimeSinceStartValue.Text = server.timeRunning.ToString


                    If gui_cpu >= 0 Then
                        If performance.gui_cpu <= 100 Then PBGeneralCPUGUI.Value = performance.gui_cpu
                        If performance.total_cpu <= 100 Then PBGeneralCPUTotal.Value = performance.total_cpu
                        If performance.server_cpu <= 100 Then PBGeneralCPUServer.Value = performance.server_cpu
                        lblGeneralCPUGUIValue.Text = performance.gui_cpu.ToString.PadLeft(3) & "%"
                        lblGeneralCPUServerValue.Text = performance.server_cpu.ToString.PadLeft(3) & "%"
                        lblGeneralCPUTotalValue.Text = performance.total_cpu.ToString.PadLeft(3) & "%"
                    Else
                        If _hnd_tmr_update_stats_cpuerror = False Then
                            lblGeneralCPUGUIValue.Text = lr("Unknown")
                            lblGeneralCPUServerValue.Text = lr("Unknown")
                            lblGeneralCPUTotalValue.Text = lr("Unknown")
                        Else
                            _hnd_tmr_update_stats_cpuerror = True
                        End If
                    End If

                    If gui_mem >= 0 Then
                        If performance.gui_mem <= 100 Then PBGeneralRAMGUI.Value = performance.gui_mem
                        If performance.total_mem <= 100 Then PBGeneralRAMTotal.Value = performance.total_mem
                        If performance.server_mem <= 100 Then PBGeneralRAMServer.Value = performance.server_mem
                        lblGeneralRAMGUIValue.Text = performance.gui_mem.ToString.PadLeft(3) & "% [" & performance.gui_mem_mbytes & "MB]"
                        lblGeneralRAMServerValue.Text = performance.server_mem.ToString.PadLeft(3) & "% [" & performance.server_mem_mbytes & "MB]"
                        lblGeneralRAMTotalValue.Text = performance.total_mem.ToString.PadLeft(3) & "% [" & performance.total_mem_mbytes & "MB]"
                    Else
                        If _hnd_tmr_update_stats_ramerror = False Then
                            lblGeneralRAMGUIValue.Text = lr("Unknown")
                            lblGeneralRAMServerValue.Text = lr("Unknown")
                            lblGeneralRAMTotalValue.Text = lr("Unknown")
                        Else
                            _hnd_tmr_update_stats_ramerror = True
                        End If

                    End If

                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception at hnd_tmr_update_stats!", ex.Message)
            End Try
        End Sub
#End Region


#Region "SuperStart"

        ''' <summary>
        ''' Load server settings, prepare UI of this tab
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub superstart_init()
            Try
                CBSuperstartServerType.SelectedIndex = 0 'set to 0, as this field must always be filled in
                CBSuperstartJavaJRE.SelectedIndex = 0 'set to 0, as this field must always be filled in

                If common.IsRunningOnMono = False Then
                    TBSuperstartJavaMinRam.Maximum = Math.Round(My.Computer.Info.TotalPhysicalMemory / 1048576)
                    TBSuperstartJavaMaxRam.Maximum = Math.Round(My.Computer.Info.TotalPhysicalMemory / 1048576)
                Else
                    TBSuperstartJavaMinRam.Maximum = 32768
                    TBSuperstartJavaMaxRam.Maximum = 32768
                End If

                If TBSuperstartJavaMinRam.Maximum < 1024 Then TBSuperstartJavaMinRam.Maximum = 1024
                If TBSuperstartJavaMaxRam.Maximum < 1024 Then TBSuperstartJavaMaxRam.Maximum = 1024

                '-Load server settings-----------------
                CBSuperstartJavaJRE.SelectedIndex = CInt(config.read("jre", "0", "superstart"))
                CBSuperstartServerType.SelectedIndex = CInt(config.read("server", "0", "superstart"))
                Try
                    Dim minval = CInt(config.read("minram", "128", "superstart"))
                    Dim maxval = CInt(config.read("maxram", "1024", "superstart"))
                    If minval <= TBSuperstartJavaMinRam.Maximum And minval >= TBSuperstartJavaMinRam.Minimum Then TBSuperstartJavaMinRam.Value = minval Else TBSuperstartJavaMinRam.Value = 128
                    If maxval <= TBSuperstartJavaMaxRam.Maximum And maxval >= TBSuperstartJavaMaxRam.Minimum Then TBSuperstartJavaMaxRam.Value = maxval Else TBSuperstartJavaMaxRam.Value = 1024
                Catch ex As Exception
                    livebug.write(loggingLevel.Warning, "mainform", "Exception in superstart_init: could not load memory settings", ex.Message)
                End Try


                TxtSuperstartJavaJarFile.Text = config.read("jar", "", "superstart")
                TxtSuperstartJavaCustomArgs.Text = config.read("custom_args", "", "superstart")
                TxtSuperstartJavaCustomSwitch.Text = config.read("custom_switch", "", "superstart")

                TxtSuperstartRemoteHost.Text = config.read("remote_host", "", "superstart")
                TxtSuperstartRemoteUsername.Text = config.read("remote_username", "", "superstart")
                NumSuperstartRemotePort.Value = CInt(config.read("remote_port", "20059", "superstart"))
                MTxtSuperstartRemotePassword.Text = config.read("remote_password", "", "superstart")
                MTxtSuperstartRemoteSalt.Text = config.read("remote_salt", "", "superstart")

                ChkSuperStartRetrieveCurrent.Checked = config.readAsBool("bukkit_get_version", True, "superstart")
                ChkSuperstartAutoUpdateNotify.Checked = config.readAsBool("bukkit_auto_update", False, "superstart")
                '--------------------------------------
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in superstart_init", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Update the UI depending on the selected server. Enable/disable the functions based upon selected server.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub CBSuperstartServerType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBSuperstartServerType.SelectedIndexChanged
            Try
                livebug.write(loggingLevel.Fine, "mainform", "Setting User Interface for server type " & CBSuperstartServerType.SelectedItem.ToString)

                'Enable/disable the needed buttons
                Select Case CBSuperstartServerType.SelectedIndex 'we're working with the index value to prevent translation problems
                    Case 0 'bukkit
                        GBSuperstartJavaServer.Enabled = True
                        GBSuperStartMaintainance.Enabled = True
                        GBSuperStartRemoteServer.Enabled = False

                        BtnSuperStartGetCurrent.Enabled = True
                        BtnSuperStartDownloadDev.Enabled = True
                        BtnSuperStartDownloadBeta.Enabled = True
                        BtnSuperStartDownloadRecommended.Enabled = True
                        BtnSuperStartDownloadCustomBuild.Enabled = True

                        'bukkit has version info
                        If BukkitTools.Fetched Then
                            lblSuperStartLatestStable.Visible = True
                            lblSuperStartLatestBeta.Visible = True
                            lblSuperStartLatestDev.Visible = True

                            lblSuperStartLatestStable.Text = lr("Latest stable:") & " " & BukkitTools.Recommended_info.version & " (#" & BukkitTools.Recommended_info.build & ")"
                            lblSuperStartLatestBeta.Text = lr("Latest beta:") & " " & BukkitTools.Beta_info.version & " (#" & BukkitTools.Beta_info.build & ")"
                            lblSuperStartLatestDev.Text = lr("Latest dev:") & " " & BukkitTools.Dev_info.version & " (#" & BukkitTools.Dev_info.build & ")"
                            Try
                                NumSuperstartCustomBuild.Maximum = BukkitTools.Latest_Dev 'dev build is always the latest
                                If BukkitTools.Latest_Recommended <= BukkitTools.Latest_Dev AndAlso BukkitTools.Latest_Recommended > 1335 Then NumSuperstartCustomBuild.Value = BukkitTools.Latest_Recommended Else NumSuperstartCustomBuild.Value = 1335
                            Catch ex As Exception 'if something goes wrong, set these limits. 
                                NumSuperstartCustomBuild.Maximum = 9999 'this can't be set to a specific value, because we don't know the maximum
                                NumSuperstartCustomBuild.Value = 1335 'minimum available at dl.bukkit.org
                            End Try
                        End If
                        llblSuperStartsite.Text = lr("Site:") & " http://bukkit.org"

                        ChkSuperStartRetrieveCurrent.Enabled = True
                        ChkSuperstartAutoUpdateNotify.Enabled = True
                        ChkSuperStartRetrieveCurrent.Checked = config.readAsBool("bukkit_get_version", True, "superstart")
                        ChkSuperstartAutoUpdateNotify.Checked = config.readAsBool("bukkit_auto_update", True, "superstart")
                        ChkSuperstartAutoUpdate.Checked = config.readAsBool("bukkit_auto_update_automatic", False, "superstart")
                        PBSuperStartServerIcon.Image = My.Resources.bukkit_logo

                    Case 1 'vanilla
                        GBSuperstartJavaServer.Enabled = True
                        GBSuperStartMaintainance.Enabled = True
                        GBSuperStartRemoteServer.Enabled = False

                        lblSuperStartLatestStable.Visible = False
                        lblSuperStartLatestBeta.Visible = False
                        lblSuperStartLatestDev.Visible = False

                        ChkSuperStartRetrieveCurrent.Enabled = False
                        ChkSuperstartAutoUpdateNotify.Enabled = False
                        ChkSuperstartAutoUpdate.Enabled = False

                        BtnSuperStartGetCurrent.Enabled = False
                        BtnSuperStartDownloadDev.Enabled = False
                        BtnSuperStartDownloadBeta.Enabled = False
                        BtnSuperStartDownloadRecommended.Enabled = False
                        BtnSuperStartDownloadCustomBuild.Enabled = False

                        llblSuperStartsite.Text = lr("Site:") & " http://www.minecraft.net/download"

                        PBSuperStartServerIcon.Image = My.Resources.vanilla_logo

                    Case 2 'remote
                        GBSuperstartJavaServer.Enabled = False
                        GBSuperStartMaintainance.Enabled = False 'no tools available yet
                        GBSuperStartRemoteServer.Enabled = True

                        lblSuperStartLatestStable.Visible = False
                        lblSuperStartLatestBeta.Visible = False
                        lblSuperStartLatestDev.Visible = False

                        ChkSuperStartRetrieveCurrent.Enabled = False
                        ChkSuperstartAutoUpdateNotify.Enabled = False
                        ChkSuperstartAutoUpdate.Enabled = False

                        TxtSuperstartRemoteHost.Enabled = True
                        NumSuperstartRemotePort.Enabled = True
                        TxtSuperstartRemoteUsername.Enabled = True
                        MTxtSuperstartRemoteSalt.Enabled = True
                        MTxtSuperstartRemotePassword.Enabled = True

                        llblSuperStartsite.Text = ""

                        PBSuperStartServerIcon.Image = Nothing

                    Case 3 'general java
                        GBSuperstartJavaServer.Enabled = True
                        GBSuperStartMaintainance.Enabled = False
                        GBSuperStartRemoteServer.Enabled = False

                        lblSuperStartLatestStable.Visible = False
                        lblSuperStartLatestBeta.Visible = False
                        lblSuperStartLatestDev.Visible = False

                        ChkSuperStartRetrieveCurrent.Enabled = False
                        ChkSuperstartAutoUpdateNotify.Enabled = False
                        ChkSuperstartAutoUpdate.Enabled = False

                        llblSuperStartsite.Text = ""

                        PBSuperStartServerIcon.Image = Nothing
                End Select
                If initialize_completed Then config.write("server", CBSuperstartServerType.SelectedIndex, "superstart")
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in CBSuperstartType_SelectedIndexChanged", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Sub to handle event, will be ran when the latest build numbers are downloaded from dl.bukkit.org, to update UI
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BukkitVersionFetchComplete() 'this sub handles bukkitTools.BukkitVersionFetchComplete
            Try
                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf BukkitVersionFetchComplete)
                    Me.Invoke(d, New Object())
                Else
                    If BukkitTools.Fetched And CBSuperstartServerType.SelectedIndex = 0 Then
                        lblSuperStartLatestStable.Text = lr("Latest stable:") & " " & BukkitTools.Recommended_info.version & " (#" & BukkitTools.Recommended_info.build & ")"
                        lblSuperStartLatestBeta.Text = lr("Latest beta:") & " " & BukkitTools.Beta_info.version & " (#" & BukkitTools.Beta_info.build & ")"
                        lblSuperStartLatestDev.Text = lr("Latest dev:") & " " & BukkitTools.Dev_info.version & " (#" & BukkitTools.Dev_info.build & ")"
                        Try
                            NumSuperstartCustomBuild.Maximum = BukkitTools.Latest_Dev
                            If BukkitTools.Latest_Recommended <= BukkitTools.Latest_Dev AndAlso BukkitTools.Latest_Recommended > 1335 Then NumSuperstartCustomBuild.Value = BukkitTools.Latest_Recommended Else NumSuperstartCustomBuild.Value = 1335
                        Catch ex As Exception
                            NumSuperstartCustomBuild.Maximum = 2500
                            NumSuperstartCustomBuild.Value = 1335
                        End Try
                    End If
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in BukkitVersionFetchComplete", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Download latest recommended version
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub superstart_DownloadRecommended() Handles BtnSuperStartDownloadRecommended.Click
            Try
                Select Case CBSuperstartServerType.SelectedIndex
                    Case 0
                        If TxtSuperstartJavaJarFile.Text IsNot Nothing AndAlso TxtSuperstartJavaJarFile.Text.EndsWith(".jar") Then
                            BukkitTools.Download(BukkitVersionType.rb, TxtSuperstartJavaJarFile.Text)
                        Else
                            BukkitTools.Download(BukkitVersionType.rb, common.Server_root & "\craftbukkit.jar")
                            TxtSuperstartJavaJarFile.Text = common.Server_root & "\craftbukkit.jar"
                        End If
                        superstart_jar_validate(Nothing, Nothing)
                    Case 1
                        If TxtSuperstartJavaJarFile.Text IsNot Nothing AndAlso TxtSuperstartJavaJarFile.Text.EndsWith(".jar") Then
                            VanillaTools.Download(TxtSuperstartJavaJarFile.Text)
                        Else
                            VanillaTools.Download(common.Server_root & "\minecraft_server.jar")
                            TxtSuperstartJavaJarFile.Text = common.Server_root & "\minecraft_server.jar"
                        End If
                    Case 2
                        'not supported
                End Select
            Catch ex As Exception
                livebug.write(loggingLevel.Fine, "mainform", "Severe exception in superstart_DownloadRecommended()", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Display the current bukkit version
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnSuperStartGetCurrent_Click(sender As System.Object, e As System.EventArgs) Handles BtnSuperStartGetCurrent.Click
            Try
                MessageBox.Show(lr("Your current bukkit version is") & " " & BukkitTools.GetCurrentBukkitVersion(CBSuperstartJavaJRE.SelectedIndex, TxtSuperstartJavaJarFile.Text).ToString, lr("Bukkit version"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch
                MessageBox.Show(lr("Your current bukkit version could not be determined"), lr("Bukkit version"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End Sub

        ''' <summary>
        ''' Download latest bèta version, ony supported for bukkit
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub superstart_DownloadBeta() Handles BtnSuperStartDownloadBeta.Click
            Select Case CBSuperstartServerType.SelectedIndex
                Case 0
                    If TxtSuperstartJavaJarFile.Text IsNot Nothing AndAlso TxtSuperstartJavaJarFile.Text.EndsWith(".jar") Then
                        BukkitTools.Download(BukkitVersionType.beta, TxtSuperstartJavaJarFile.Text)
                    Else
                        BukkitTools.Download(BukkitVersionType.beta, common.Server_root & "/craftbukkit.jar")
                        TxtSuperstartJavaJarFile.Text = common.Server_root & "/craftbukkit.jar"
                    End If
                    superstart_jar_validate(Nothing, Nothing)
                Case 1
                    'not supported
                Case 2
                    'not supported
            End Select
        End Sub

        ''' <summary>
        ''' Download latest dev version, ony supported for bukkit
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub superstart_DownloadDev() Handles BtnSuperStartDownloadDev.Click
            Select Case CBSuperstartServerType.SelectedIndex
                Case 0
                    If TxtSuperstartJavaJarFile.Text IsNot Nothing AndAlso TxtSuperstartJavaJarFile.Text.EndsWith(".jar") Then
                        BukkitTools.Download(BukkitVersionType.dev, TxtSuperstartJavaJarFile.Text)
                    Else
                        BukkitTools.Download(BukkitVersionType.dev, common.Server_root & "/craftbukkit.jar")
                        TxtSuperstartJavaJarFile.Text = common.Server_root & "/craftbukkit.jar"
                    End If
                    superstart_jar_validate(Nothing, Nothing)
                Case 1
                    'not supported
                Case 2
                    'not supported
            End Select
        End Sub

        ''' <summary>
        ''' Download a custom version, ony supported for bukkit
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BtnSuperStartDownloadCustomBuild_Click(sender As System.Object, e As System.EventArgs) Handles BtnSuperStartDownloadCustomBuild.Click
            Select Case CBSuperstartServerType.SelectedIndex
                Case 0
                    If TxtSuperstartJavaJarFile.Text IsNot Nothing AndAlso TxtSuperstartJavaJarFile.Text.EndsWith(".jar") Then
                        BukkitTools.DownloadCustom(NumSuperstartCustomBuild.Value, TxtSuperstartJavaJarFile.Text)
                    Else
                        BukkitTools.DownloadCustom(NumSuperstartCustomBuild.Value, common.Server_root & "/craftbukkit.jar")
                        TxtSuperstartJavaJarFile.Text = common.Server_root & "/craftbukkit.jar"
                    End If
                Case 1
                    'not supported
                Case 2
                    'not supported
            End Select
        End Sub

        ''' <summary>
        ''' Checks if all necessary field are filled in correctly
        ''' </summary>
        ''' <param name="warn">Should the function show a messagebox if the settings are invalid</param>
        ''' <returns>True if valid</returns>
        ''' <remarks></remarks>
        Private Function superstart_validate(warn As Boolean) As Boolean
            Try
                livebug.write(loggingLevel.Fine, "mainform", "Validating settings for server type " & CBSuperstartServerType.SelectedItem.ToString)
                Dim is_correct As Boolean = True
                Select Case CBSuperstartServerType.SelectedIndex
                    Case 0
                        If TxtSuperstartJavaJarFile.Text Is Nothing Then is_correct = False : Exit Select
                        If TxtSuperstartJavaJarFile.Text = "" Then is_correct = False : Exit Select
                        If TxtSuperstartJavaJarFile.Text.EndsWith(".jar") = False Then is_correct = False : Exit Select
                        is_correct = is_correct And (TBSuperstartJavaMinRam.Value <= TBSuperstartJavaMaxRam.Value)
                        If javaAPI.Is32bit(CBSuperstartJavaJRE.SelectedIndex) Then is_correct = (is_correct And TBSuperstartJavaMaxRam.Value < 1500)
                    Case 1
                        If TxtSuperstartJavaJarFile.Text Is Nothing Then is_correct = False : Exit Select
                        If TxtSuperstartJavaJarFile.Text = "" Then is_correct = False : Exit Select
                        If TxtSuperstartJavaJarFile.Text.EndsWith(".jar") = False Then is_correct = False : Exit Select
                        is_correct = is_correct And (TBSuperstartJavaMinRam.Value <= TBSuperstartJavaMaxRam.Value)
                        If javaAPI.Is32bit(CBSuperstartJavaJRE.SelectedIndex) Then is_correct = (is_correct And TBSuperstartJavaMaxRam.Value < 1500)
                    Case 2
                        If TxtSuperstartRemoteHost.Text Is Nothing OrElse TxtSuperstartRemoteHost.Text = "" Then is_correct = False : Exit Select
                        If TxtSuperstartRemoteUsername.Text Is Nothing OrElse TxtSuperstartRemoteUsername.Text = "" Then is_correct = False : Exit Select
                        If MTxtSuperstartRemotePassword.Text Is Nothing OrElse MTxtSuperstartRemotePassword.Text = "" Then is_correct = False : Exit Select
                        If MTxtSuperstartRemoteSalt.Text Is Nothing OrElse MTxtSuperstartRemoteSalt.Text = "" Then is_correct = False : Exit Select
                End Select
                If Not is_correct And warn Then MessageBox.Show(lr("The server settings you entered are invalid" & vbCrLf & "Please enter valid settings and try again"), lr("Invalid server settings"), MessageBoxButtons.OK, MessageBoxIcon.Error)

                If is_correct And (CBSuperstartServerType.SelectedIndex = 0 Or CBSuperstartServerType.SelectedIndex = 1) Then
                    Dim jarfilesize = GetFileSize(TxtSuperstartJavaJarFile.Text) < 1
                    livebug.write(loggingLevel.Info, "mainform", "Size of server file: " & jarfilesize & " bytes (" & common.ConvertByte(jarfilesize, ByteConverter.size.size_mbyte) & "mb)")
                    If (jarfilesize <> False AndAlso jarfilesize < 128) Then
                        livebug.write(loggingLevel.Warning, "mainform", "Validated settings, result: corrupt file!")
                        If warn Then
                            MessageBox.Show(lr("The server.jar file you entered is invalid or corrupted" & vbCrLf & "Re-download the file and try again"), lr("Corrupt server"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        is_correct = False
                    Else
                        livebug.write(loggingLevel.Info, "mainform", "Size of server file seems ok")
                    End If
                End If
                livebug.write(loggingLevel.Fine, "mainform", "Validated settings, result:" & is_correct)
                Return is_correct
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in thds_setserverstate", ex.Message)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Update label on change
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TBSuperstartJavaMinRam_ValueChanged(sender As System.Object, e As System.EventArgs) Handles TBSuperstartJavaMinRam.ValueChanged, TBSuperstartJavaMinRam.Scroll
            If TBSuperstartJavaMinRam.Value > TBSuperstartJavaMaxRam.Value Then TBSuperstartJavaMaxRam.Value = TBSuperstartJavaMinRam.Value
            LblSuperStartMinRam.Text = "Min. Ram: " & TBSuperstartJavaMinRam.Value.ToString & "MB"
        End Sub

        ''' <summary>
        ''' Update label on change
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TBSuperstartJavaMaxRam_ValueChanged(sender As System.Object, e As System.EventArgs) Handles TBSuperstartJavaMaxRam.ValueChanged, TBSuperstartJavaMaxRam.Scroll
            If TBSuperstartJavaMinRam.Value > TBSuperstartJavaMaxRam.Value Then TBSuperstartJavaMinRam.Value = TBSuperstartJavaMaxRam.Value
            lblSuperStartMaxRam.Text = "Max. Ram: " & TBSuperstartJavaMaxRam.Value.ToString & "MB"
        End Sub

        ''' <summary>
        ''' Check if valid, if valid save
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub superstart_jar_validate(sender As System.Object, e As System.EventArgs) Handles TxtSuperstartJavaJarFile.TextChanged
            If TxtSuperstartJavaJarFile.Text Is Nothing OrElse TxtSuperstartJavaJarFile.Text = "" Then
                ErrProv.SetError(TxtSuperstartJavaJarFile, lr("You must enter a path to the .jar file"))
            ElseIf Not TxtSuperstartJavaJarFile.Text.EndsWith(".jar") Then
                ErrProv.SetError(TxtSuperstartJavaJarFile, lr("Invalid file path, must be a .jar file"))
            ElseIf Not FileIO.FileSystem.FileExists(TxtSuperstartJavaJarFile.Text) Then
                ErrProv.SetError(TxtSuperstartJavaJarFile, lr("File doesn't exist!"))
            Else
                ErrProv.SetError(TxtSuperstartJavaJarFile, "")
                config.write("jar", TxtSuperstartJavaJarFile.Text, "superstart")
            End If
        End Sub

        ''' <summary>
        ''' Browse to jar file
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnSuperstartJavaJarFileBrowse_Click(sender As System.Object, e As System.EventArgs) Handles BtnSuperstartJavaJarFileBrowse.Click
            Dim ofd As New OpenFileDialog
            ofd.Filter = "*.jar|*.jar"
            ofd.Title = "Select server file"
            ofd.InitialDirectory = My.Application.Info.DirectoryPath
            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then TxtSuperstartJavaJarFile.Text = ofd.FileName
        End Sub

        ''' <summary>
        ''' Check if valid, if valid, save
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TBSuperstartJavaMaxRam_Scroll(sender As System.Object, e As System.EventArgs) Handles TBSuperstartJavaMaxRam.Scroll
            If (TBSuperstartJavaMaxRam.Value > 1024 And javaAPI.Is32bit(CBSuperstartJavaJRE.SelectedIndex.ToString)) Then
                ErrProv.SetError(TBSuperstartJavaMaxRam, lr("You can't use more then 1024MB RAM with Java x32"))
            Else
                ErrProv.SetError(TBSuperstartJavaMaxRam, "")
                config.write("maxram", TBSuperstartJavaMaxRam.Value, "superstart")
            End If
        End Sub

        ''' <summary>
        ''' Check if valid, if valid, save
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TBSuperstartJavaMinRam_Scroll(sender As System.Object, e As System.EventArgs) Handles TBSuperstartJavaMinRam.Scroll
            If (TBSuperstartJavaMinRam.Value > 1024 And javaAPI.Is32bit(CBSuperstartJavaJRE.SelectedIndex.ToString)) Then
                ErrProv.SetError(TBSuperstartJavaMinRam, lr("You can't use more then 1024MB RAM with Java x32"))
            Else
                ErrProv.SetError(TBSuperstartJavaMinRam, "")
                config.write("minram", TBSuperstartJavaMinRam.Value, "superstart")
            End If
        End Sub

        ''' <summary>
        ''' Save on change
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub CBSuperstartJavaJRE_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBSuperstartJavaJRE.SelectedIndexChanged
            If Not initialize_completed Then Exit Sub

            config.write("jre", CBSuperstartJavaJRE.SelectedIndex, "superstart")

            If (TBSuperstartJavaMinRam.Value > 1024 And javaAPI.Is32bit(CBSuperstartJavaJRE.SelectedIndex.ToString)) Then
                ErrProv.SetError(TBSuperstartJavaMinRam, lr("You can't use more then 1024MB RAM with Java x32"))
            Else
                ErrProv.SetError(TBSuperstartJavaMinRam, "")
                config.write("minram", TBSuperstartJavaMinRam.Value, "superstart")
            End If

            If (TBSuperstartJavaMaxRam.Value > 1024 And javaAPI.Is32bit(CBSuperstartJavaJRE.SelectedIndex.ToString)) Then
                ErrProv.SetError(TBSuperstartJavaMaxRam, lr("You can't use more then 1024MB RAM with Java x32"))
            Else
                ErrProv.SetError(TBSuperstartJavaMaxRam, "")
                config.write("maxram", TBSuperstartJavaMaxRam.Value, "superstart")
            End If

            If CBSuperstartJavaJRE.SelectedIndex = 4 Then
                javaAPI.SelectAlternativeJava()
            End If

        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TxtSuperstartJavaCustomArgs_TextChanged() Handles TxtSuperstartJavaCustomArgs.TextChanged
            config.write("custom_args", TxtSuperstartJavaCustomArgs.Text, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TxtSuperstartJavaCustomswitch_TextChanged() Handles TxtSuperstartJavaCustomSwitch.TextChanged
            config.write("custom_switch", TxtSuperstartJavaCustomSwitch.Text, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub NumSuperstartRemotePort_LostFocus() Handles NumSuperstartRemotePort.LostFocus
            config.write("remote_port", NumSuperstartRemotePort.Value.ToString, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TxtSuperstartRemotehost_LostFocus() Handles TxtSuperstartRemoteHost.LostFocus
            config.write("remote_host", TxtSuperstartRemoteHost.Text, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TxtSuperstartRemoteUsername_LostFocus() Handles TxtSuperstartRemoteUsername.LostFocus
            config.write("remote_username", TxtSuperstartRemoteUsername.Text, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MTxtSuperstartRemotepassword_LostFocus() Handles MTxtSuperstartRemotePassword.LostFocus
            config.write("remote_password", MTxtSuperstartRemotePassword.Text, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MTxtSuperstartRemoteSalt_LostFocus() Handles MTxtSuperstartRemoteSalt.LostFocus
            config.write("remote_salt", MTxtSuperstartRemoteSalt.Text, "superstart")
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ChkSuperStartRetrieveCurrent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkSuperStartRetrieveCurrent.CheckedChanged
            config.writeAsBool("bukkit_get_version", ChkSuperStartRetrieveCurrent.Checked, "superstart")
            If ChkSuperStartRetrieveCurrent.Checked = False Then
                ChkSuperstartAutoUpdateNotify.Checked = False : ChkSuperstartAutoUpdateNotify.Enabled = False
                ChkSuperstartAutoUpdate.Checked = False : ChkSuperstartAutoUpdate.Enabled = False
            Else
                If ChkSuperstartAutoUpdateNotify.Checked Then ChkSuperstartAutoUpdate.Checked = False : ChkSuperstartAutoUpdate.Enabled = False Else ChkSuperstartAutoUpdate.Enabled = True
                If ChkSuperstartAutoUpdate.Checked Then ChkSuperstartAutoUpdateNotify.Checked = False : ChkSuperstartAutoUpdateNotify.Enabled = False Else ChkSuperstartAutoUpdateNotify.Enabled = True
            End If
        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ChkSuperstartAutoUpdateNotify_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkSuperstartAutoUpdateNotify.CheckedChanged
            config.writeAsBool("bukkit_auto_update", ChkSuperstartAutoUpdateNotify.Checked, "superstart")

            If ChkSuperStartRetrieveCurrent.Checked = False Then
                ChkSuperstartAutoUpdateNotify.Checked = False : ChkSuperstartAutoUpdateNotify.Enabled = False
                ChkSuperstartAutoUpdate.Checked = False : ChkSuperstartAutoUpdate.Enabled = False
            Else
                If ChkSuperstartAutoUpdateNotify.Checked Then ChkSuperstartAutoUpdate.Checked = False : ChkSuperstartAutoUpdate.Enabled = False Else ChkSuperstartAutoUpdate.Enabled = True
                If ChkSuperstartAutoUpdate.Checked Then ChkSuperstartAutoUpdateNotify.Checked = False : ChkSuperstartAutoUpdateNotify.Enabled = False Else ChkSuperstartAutoUpdateNotify.Enabled = True
            End If

        End Sub

        ''' <summary>
        ''' save field on change
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ChkSuperstartAutoUpdate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkSuperstartAutoUpdate.CheckedChanged
            config.writeAsBool("bukkit_auto_update_automatic", ChkSuperstartAutoUpdateNotify.Checked, "superstart")
            If ChkSuperStartRetrieveCurrent.Checked = False Then
                ChkSuperstartAutoUpdateNotify.Checked = False : ChkSuperstartAutoUpdateNotify.Enabled = False
                ChkSuperstartAutoUpdate.Checked = False : ChkSuperstartAutoUpdate.Enabled = False
            Else
                If ChkSuperstartAutoUpdateNotify.Checked Then ChkSuperstartAutoUpdate.Checked = False : ChkSuperstartAutoUpdate.Enabled = False Else ChkSuperstartAutoUpdate.Enabled = True
                If ChkSuperstartAutoUpdate.Checked Then ChkSuperstartAutoUpdateNotify.Checked = False : ChkSuperstartAutoUpdateNotify.Enabled = False Else ChkSuperstartAutoUpdateNotify.Enabled = True
            End If
        End Sub

#End Region

#Region "Playerlist"

        ''' <summary>
        ''' Handle the event, add the player
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub hnd_PlayerJoin(ByVal e As PlayerJoinEventArgs)
            If e Is Nothing OrElse e.PlayerJoin Is Nothing OrElse e.PlayerJoin.player Is Nothing Then livebug.write(loggingLevel.Warning, "Mainform", "Player joined - invalid PlayerJoinEventArgs passed!") : Exit Sub
            livebug.write(loggingLevel.Fine, "Mainform", "Player joined, event catched by UI. Player:" & e.PlayerJoin.player.name)
            AddPlayer(e.PlayerJoin.player)
        End Sub

        ''' <summary>
        ''' Add a player to the listviews + start routine to get more details (location, avatar,...)
        ''' </summary>
        ''' <param name="p"></param>
        ''' <remarks></remarks>
        Private Sub AddPlayer(p As Player)
            Try
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf AddPlayer)
                    Me.Invoke(d, New Object() {p})
                Else
                    If p Is Nothing OrElse p.name Is Nothing Then Exit Sub

                    ALVGeneralPlayers.Items.Add(GetGeneralPlayersLVI(p))
                    ALVPlayersPlayers.Items.Add(GetPlayersPlayersLVI(p))

                    server.playerList.Add(p)

                    Dim t As New Thread(AddressOf thd_playerlist_getDataAsync)
                    t.IsBackground = True
                    t.Name = "player_update_" & p.name 'for debugging purposes
                    t.Start(p)

                    playerlist_checkduplicate()

                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "Mainform", "Severe exception at AddPlayer(p as player)!", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Handle the event on a player disconnect, remove it from the listviews
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub hnd_PlayerDisconnect(ByVal e As PlayerDisconnectEventArgs)
            livebug.write(loggingLevel.Fine, "mainform", "Player disconnected, event catched by UI. Player:" & e.player.name, "hnd_PlayerDisconnect()")
            RemovePlayer(e.player.name)
        End Sub

        ''' <summary>
        ''' Remove a player from the listviews based upon the player name
        ''' </summary>
        ''' <param name="name"></param>
        ''' <remarks></remarks>
        Private Sub RemovePlayer(ByVal name As String)
            Try
                If name Is Nothing OrElse name = "" Then Exit Sub
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf RemovePlayer)
                    Me.Invoke(d, New Object() {name})
                Else
                    If server.playerList Is Nothing Then Exit Sub

                    For Each Player As Player In server.playerList
                        If Player.name = name Then
                            server.playerList.Remove(Player)
                            Exit For
                        End If
                    Next

                    livebug.write(loggingLevel.Fine, "Mainform", "Removed player from player list")

                    For Each item As ListViewItem In ALVGeneralPlayers.Items 'remove from listview
                        If item.SubItems(0).Text = name Then
                            item.Remove()
                        End If
                    Next
                    livebug.write(loggingLevel.Fine, "Mainform", "Removed player from UI general player list")

                    For Each item As ListViewItem In ALVPlayersPlayers.Items 'remove from listview
                        If item.SubItems(0).Text = name Then
                            item.Remove()
                        End If
                    Next

                    livebug.write(loggingLevel.Fine, "Mainform", "Removed player from UI players player list")
                    If ImgListPlayerAvatars.Images.ContainsKey(name) Then ImgListPlayerAvatars.Images.RemoveByKey(name) 'remove unneeded images from the list, reduce memory!
                    livebug.write(loggingLevel.Fine, "Mainform", "Removed player avatar from imagelist")
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Fine, "mainform", "Severe exception at RemovePlayer(name as string)!", ex.Message)
            End Try
            livebug.write(loggingLevel.Fine, "Mainform", "Finished player removal")
        End Sub

        ''' <summary>
        ''' Build the ListViewItem for the players tab players list.
        ''' </summary>
        ''' <param name="p">The player details as a player object</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetPlayersPlayersLVI(p As Player) As ListViewItem
            Dim player_item As New ListViewItem({p.name, p.IP, lr("locating..."), p.time.ToShortTimeString, p.WhiteList.ToString, p.OP.ToString}, 0)
            player_item.Tag = p.name
            Return player_item
        End Function

        ''' <summary>
        ''' Build the listviewitem for the general tab players list
        ''' </summary>
        ''' <param name="p">The player details as a player object</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetGeneralPlayersLVI(p As Player) As ListViewItem
            Dim general_item As New ListViewItem({p.name}, 0)
            general_item.Tag = p.name
            general_item.ToolTipText = "from " & p.IP & " at " & p.time
            Return (general_item)
        End Function

        ' These routines could also be changed to take the player object.
        '
        ''' <summary>
        ''' Get additiol data about a player, async
        ''' </summary>
        ''' <param name="player">the player in the server.playerlist</param>
        ''' <remarks></remarks>
        Private Sub thd_playerlist_getDataAsync(ByVal player As Player)
            Try
                If server.playerList Is Nothing Then Exit Sub

                Dim id As UInt16 = server.playerList.IndexOf(player)
                If id < 0 Then Exit Sub

                If server.playerList.Count - 1 < id Then Exit Sub

                If player Is Nothing Then Exit Sub
                If player.name Is Nothing Then Exit Sub
                If player.IP Is Nothing Then Exit Sub

                livebug.write(loggingLevel.Fine, "mainform", "Getting additional details for player " & player.name & "(" & id & ")")

                Dim a As Image = serverinteraction.getPlayerMinotar(player.name) ' get data

                If server.playerList.Count = 0 OrElse playerList.Contains(player) = False Then Exit Sub
                If id > server.playerList.Count - 1 OrElse Not server.playerList(id).Equals(player) Then id = server.playerList.IndexOf(player) 'if invalid id refresh
                If id < 0 Then Exit Sub 'if still invalid exit
                If id >= 0 AndAlso a IsNot Nothing Then 'if valid update
                    player.avatar = a
                    server.playerList(id).avatar = player.avatar
                End If
                livebug.write(loggingLevel.Fine, "Mainform", "Avatar loaded")
                Dim l As String = serverinteraction.getPlayerLocation(player.IP)
                'Dim l As String = "unavailable"

                If server.playerList.Count = 0 OrElse playerList.Contains(player) = False Then Exit Sub
                If id > server.playerList.Count - 1 OrElse Not server.playerList(id).Equals(player) Then id = server.playerList.IndexOf(player)
                If id < 0 Then Exit Sub
                If id >= 0 Then
                    player.location = l
                    If server.playerList IsNot Nothing AndAlso id < server.playerList.Count Then server.playerList(id).location = player.location
                End If
                livebug.write(loggingLevel.Fine, "Mainform", "Location loaded")
                Update_player(player)
                livebug.write(loggingLevel.Fine, "Mainform", "Player updated")
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "mainform", "Severe exception at thd_playerlist_getDataAsync!", ex.Message) 'Lowered logging level to prevent "send report" on internet issues.
            End Try
        End Sub

        ''' <summary>
        ''' Update a player in the listviews, given it's current player data. New data will be loaded from server.Playerlist(id)
        ''' </summary>
        ''' <param name="player">The player Item</param>
        ''' <remarks></remarks>
        Private Sub Update_player(player As Player)
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf Update_player)
                Me.Invoke(d, New Object() {player})
            Else
                Try
                    If player Is Nothing Then Exit Sub
                    If player.name Is Nothing Then Exit Sub
                    If player.IP Is Nothing Then Exit Sub
                    livebug.write(loggingLevel.Fine, "Mainform", "Updating player " & player.name & "  in listviews")

                    Dim id As UInt16

                    'For security, preventing crashes or errors. This part returns always to check if the object/id are still valid, and to see if the player didn't disconnect
                    If server.playerList.Count = 0 OrElse playerList.Contains(player) = False Then Exit Sub
                    If id > server.playerList.Count - 1 OrElse Not server.playerList(id).Equals(player) Then id = server.playerList.IndexOf(player)
                    If id < 0 Then Exit Sub


                    If ALVPlayersPlayers.Items.Count - 1 < id OrElse ALVGeneralPlayers.Items.Count - 1 < id OrElse server.playerList Is Nothing OrElse server.playerList.Count - 1 < id Then
                        livebug.write(loggingLevel.Warning, "Mainform", "Invalid ID! cancelling", "Update_player")
                        Exit Sub
                    End If

                    Dim item As ListViewItem

                    If server.playerList.Count = 0 OrElse playerList.Contains(player) = False Then Exit Sub
                    If id > server.playerList.Count - 1 OrElse Not server.playerList(id).Equals(player) Then id = server.playerList.IndexOf(player)
                    If id < 0 Then Exit Sub

                    Try
                        If ImgListPlayerAvatars IsNot Nothing AndAlso ImgListPlayerAvatars.Images IsNot Nothing Then ImgListPlayerAvatars.Images.Add(player.name, player.avatar)
                    Catch ex As Exception
                        livebug.write(loggingLevel.Warning, "Mainform", "Couldn't add player face to imagelist !", ex.Message)
                    End Try

                    If ALVPlayersPlayers.Items.Count - 1 < id OrElse ALVGeneralPlayers.Items.Count - 1 < id OrElse server.playerList Is Nothing OrElse server.playerList.Count - 1 < id Then Exit Sub
                    If server.playerList.Count = 0 OrElse playerList.Contains(player) = False Then Exit Sub
                    If id > server.playerList.Count - 1 OrElse Not server.playerList(id).Equals(player) Then id = server.playerList.IndexOf(player)
                    If id < 0 Then Exit Sub

                    Try
                        item = ALVGeneralPlayers.Items(id)
                        item.ToolTipText = lr("from") & " " & player.location & " (" & player.IP & ") " & lr("at") & " " & player.time
                        item.ImageKey = player.name
                        item.Tag = player.name
                    Catch ex As Exception
                        livebug.write(loggingLevel.Severe, "Mainform", "Couldn't update player in general listview!", ex.Message)
                    End Try

                    If ALVPlayersPlayers.Items.Count - 1 < id OrElse ALVGeneralPlayers.Items.Count - 1 < id OrElse server.playerList Is Nothing OrElse server.playerList.Count - 1 < id Then Exit Sub
                    If server.playerList.Count = 0 OrElse playerList.Contains(player) = False Then Exit Sub
                    If id > server.playerList.Count - 1 OrElse Not server.playerList(id).Equals(player) Then id = server.playerList.IndexOf(player)
                    If id < 0 Then Exit Sub
                    Try
                        item = ALVPlayersPlayers.Items(id)
                        item.SubItems(2).Text = player.location
                        item.ImageKey = player.name
                        item.Tag = player.name
                    Catch ex As Exception
                        livebug.write(loggingLevel.Severe, "Mainform", "Couldn't update player in players listview!", ex.Message)
                    End Try
                Catch genex As Exception
                    livebug.write(loggingLevel.Severe, "Mainform", "Severe exception at Update_Player!", genex.Message)
                End Try
            End If
        End Sub

        Private Sub BtnCMenuPlayerListOp_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListOp.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = CmenuPlayerList.SourceControl
            If ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("op " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListDeop_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListDeop.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = CmenuPlayerList.SourceControl
            If ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("deop " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListKick_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListKick.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = CmenuPlayerList.SourceControl
            If ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("kick " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListBan_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListBan.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = CmenuPlayerList.SourceControl
            If ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("ban " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListGamemodeSurvival_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListGamemodeSurvival.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = ALVGeneralPlayers
            If ALVPlayersPlayers.Focused Then ctrl = ALVPlayersPlayers
            If ctrl Is Nothing OrElse ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("gamemode 0 " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListGamemodeCreative_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListGamemodeCreative.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = ALVGeneralPlayers
            If ALVPlayersPlayers.Focused Then ctrl = ALVPlayersPlayers
            If ctrl Is Nothing OrElse ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("gamemode 1 " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListGamemodeAdventure_Click(sender As System.Object, e As System.EventArgs)
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = ALVGeneralPlayers
            If ALVPlayersPlayers.Focused Then ctrl = ALVPlayersPlayers
            If ctrl Is Nothing OrElse ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            server.SendCommand("gamemode 2 " & ctrl.SelectedItems(0).Text)
        End Sub

        Private Sub BtnCMenuPlayerListGive_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuPlayerListGive.Click
            Dim ctrl As Net.Bertware.Controls.AdvancedListView = CmenuPlayerList.SourceControl
            If ctrl.SelectedItems Is Nothing OrElse ctrl.SelectedItems.Count < 1 OrElse ctrl.SelectedItems(0) Is Nothing Then Exit Sub
            TxtGeneralServerIn.Text = "give " & ctrl.SelectedItems(0).SubItems(0).Text & " "
            TxtGeneralServerIn.Select(Text.Length, 0)
            TxtGeneralServerIn.Focus()
        End Sub

        Private Sub BtnCmenuPlayerListRefresh_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuPlayerListRefresh.Click
            If server.running Then server.SendCommand("list", True)
        End Sub

        ''' <summary>
        ''' Change the display mode for the players list view.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub TBPlayersPlayersView_Scroll() Handles TBPlayersPlayersView.Scroll
            ALVPlayersPlayers.View = TBPlayersPlayersView.Value
        End Sub

        ''' <summary>
        ''' Compare the playerlist in the UI to the playerlist in the GUI code (server class)
        ''' </summary>
        ''' <param name="onlineplayers">List of actual amount of online players right now</param>
        ''' <remarks></remarks>
        Private Sub playerlist_CheckUIList(onlineplayers As List(Of String))
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf playerlist_CheckUIList)
                Me.Invoke(d, New Object() {onlineplayers})
            Else
                If onlineplayers.Count <> ALVGeneralPlayers.Items.Count Then

                    If onlineplayers.Count > ALVGeneralPlayers.Items.Count Then 'more registered players than shown

                        For Each Player In onlineplayers
                            Dim found As Boolean = False

                            'check if item is present in playerlist
                            For Each item As ListViewItem In ALVGeneralPlayers.Items
                                If item.SubItems(0).Text = Name Then found = True
                            Next

                            If found = False Then
                                livebug.write(loggingLevel.Fine, "mainform", "Player registered but not shown! " & Player)
                                Dim p As Player = server.GetPlayerByName(Player)
                                If p Is Nothing Then p = New Player(Player, "unknown")
                                AddPlayer(p)
                            End If
                        Next

                    Else 'more shown players than registered

                        For Each item As ListViewItem In ALVGeneralPlayers.Items
                            If onlineplayers.Contains(item.SubItems(0).Text) = False Then livebug.write(loggingLevel.Fine, "mainform", "Player shown but not registered! " & item.SubItems(0).Text) : RemovePlayer(item.SubItems(0).Text)
                        Next

                    End If
                End If

            End If
        End Sub

        Private Sub playerlist_checkduplicate()
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf playerlist_checkduplicate)
                Me.Invoke(d, New Object() {})
            Else
                Dim l As New List(Of String)
                For Each item As ListViewItem In ALVGeneralPlayers.Items
                    If l.Contains(item.SubItems(0).Text) = False Then l.Add(item.SubItems(0).Text) Else ALVGeneralPlayers.Items.Remove(item)
                Next

                l = New List(Of String)
                For Each item As ListViewItem In ALVPlayersPlayers.Items
                    If l.Contains(item.SubItems(0).Text) = False Then l.Add(item.SubItems(0).Text) Else ALVPlayersPlayers.Items.Remove(item)
                Next

            End If
        End Sub
#End Region

        Private Sub BtnGeneralClearOutput_Click(sender As System.Object, e As System.EventArgs) Handles BtnGeneralClearOutput.Click
            ARTXTServerOutput.Text = ""
        End Sub

        Private Sub BtnCmenuGeneralOutputCopy_Click(sender As System.Object, e As System.EventArgs) Handles BtnBrowseOutput.Click
            Try
                Dim outbrowser As New OutputBrowser(ARTXTServerOutput)
                outbrowser.Show()
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "Mainform", "Couldn't open output browse dialog", ex.Message)
            End Try
        End Sub

        Private Sub ARTXTServerOutput_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkClickedEventArgs) Handles ARTXTServerOutput.LinkClicked
            If e.LinkText.Contains("http") Then
                Dim p As New Process
                p.StartInfo.FileName = e.LinkText
                p.Start()
            End If
        End Sub


#Region "pluginmanager"

#Region "install"
        Private Sub pluginmanager_install_init()
            Try
                livebug.write(loggingLevel.Fine, "mainform", "Loading plugin installer items")
                Dim lst As List(Of String) = BukGetAPI.GetPluginCategories()

                CBInstallPluginsCategory.Items.Clear()
                CBInstallPluginsCategory.Items.Add("All")
                CBInstallPluginsCategory.Items.Add("Popular")
                CBInstallPluginsCategory.SelectedIndex = 0

                For Each item In lst
                    CBInstallPluginsCategory.Items.Add(item.Replace("_", " "))
                Next
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "mainform", "Couldn't load plugin categories!", ex.Message)

                CBInstallPluginsCategory.Items.Clear()
                CBInstallPluginsCategory.Items.Add("All")
                CBInstallPluginsCategory.Items.Add("Popular")
                CBInstallPluginsCategory.Items.Add("Categories temporary unavailable")
                CBInstallPluginsCategory.SelectedIndex = 0

            End Try
        End Sub

        Private Sub TabCtrlMain_IndexChanged(sender As System.Object, e As System.EventArgs) Handles TabCtrlMain.SelectedIndexChanged
            If TabCtrlMain.SelectedTab.Equals(TabServerOptions) Then Server_settings_refresh_UI()
            If TabCtrlMain.SelectedTab.Equals(TabPlugins) And BukGetAPI.IsPluginListLoaded = False Then BukGetAPI.LoadMostPopularPluginsAsync()
        End Sub

        Private Sub CBInstallPluginsCategory_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBInstallPluginsCategory.SelectedIndexChanged
            If initialize_completed = False Then Exit Sub
            If CBInstallPluginsCategory.SelectedItem.ToString = "All" Then
                BukGetAPI.LoadAllPluginsAsync() 'event will update UI
            ElseIf CBInstallPluginsCategory.SelectedItem.ToString = "Popular" Then
                BukGetAPI.LoadMostPopularPluginsAsync() 'event will update UI
            Else
                BukGetAPI.GetPluginsByCategoryAsync(CBInstallPluginsCategory.SelectedItem.ToString) 'event will update UI
            End If
        End Sub

        Private Sub TxtInstallPluginsFilter_TextChanged(sender As System.Object, e As System.EventArgs) Handles BtnInstallPluginsSearch.Click
            ALVBukGetPlugins.Items.Clear()
            Dim Searchtext = TxtInstallPluginsFilter.Text.ToLower
            If Searchtext = "" Then
                BukGetAPI.LoadMostPopularPluginsAsync()
            Else
                GetPluginSearchOnlineAsync(Searchtext)
            End If

        End Sub

        Private Sub btnCMenuBukGetPluginsMoreInfo_Click(sender As System.Object, e As System.EventArgs) Handles btnCMenuBukGetPluginsMoreInfo.Click
            If ALVBukGetPlugins.SelectedItems Is Nothing OrElse ALVBukGetPlugins.SelectedItems.Count < 1 Then Exit Sub
            For Each item As ListViewItem In ALVBukGetPlugins.SelectedItems
                BukGetAPI.ShowPluginDialogByNamespace(item.Tag)
            Next
        End Sub

        Private Sub BtnCMenuBukGetPluginsInstallPlugin_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuBukGetPluginsInstallPlugin.Click
            If ALVBukGetPlugins.SelectedItems Is Nothing OrElse ALVBukGetPlugins.SelectedItems.Count < 1 Then Exit Sub
            For Each item As ListViewItem In ALVBukGetPlugins.SelectedItems
                BukGetAPI.InstallPlugin(BukGetAPI.GetPluginInfoByNamespace(item.Tag))
            Next
        End Sub

        Private Sub BtnCMenuBukGetPluginsProjectPage_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuBukGetPluginsProjectPage.Click
            If ALVBukGetPlugins.SelectedItems Is Nothing OrElse ALVBukGetPlugins.SelectedItems.Count < 1 Then Exit Sub
            For Each item As ListViewItem In ALVBukGetPlugins.SelectedItems
                BukGetAPI.OpenProjectPageByNamespace(item.Tag)
            Next
        End Sub

        Private Sub BtnCMenuBukGetPluginsRefresh_Click(sender As System.Object, e As System.EventArgs) Handles BtnCMenuBukGetPluginsRefresh.Click
            BukGetAPI.LoadAllPluginsAsync()
        End Sub

        Private Sub thds_SetBukGetPlugins(ByVal lst As List(Of SimpleBukgetPlugin))
            If lst Is Nothing Then Exit Sub
            Try
                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf thds_SetBukGetPlugins)
                    Me.Invoke(d, New Object() {lst})
                Else
                    livebug.write(loggingLevel.Fine, "mainform", "Adding " & lst.Count & " Items to BukGet Plugin list", "thds_SetBukgetPlugins")
                    Me.LblInstallPluginsLoading.Visible = False
                    ALVBukGetPlugins.Items.Clear()
                    If lst Is Nothing OrElse lst.Count = 0 Then
                        ALVBukGetPlugins.Items.Add(New ListViewItem({lr("Error while loading plugins. Probably the server could not be reached.")}))
                        ALVBukGetPlugins.Items.Add(New ListViewItem({lr("You can right click and try refreshing.")}))
                        ALVBukGetPlugins.Items.Add(New ListViewItem({lr("If you selected a category, it's possible that this category is empty.")}))
                        Exit Sub
                    End If

                    Dim BukkitTrim() As Char = {"C", "B", " "}
                    For tmp As UInt32 = 0 To lst.Count - 1
                        If lst(tmp).slug IsNot Nothing AndAlso lst(tmp).slug.Trim <> "" Then
                            Dim lvi As ListViewItem
                            If lst(tmp).name IsNot Nothing AndAlso lst(tmp).name.Trim <> "" Then
                                If lst(tmp).descr IsNot Nothing AndAlso lst(tmp).descr <> "null" Then
                                    lvi = New ListViewItem({lst(tmp).name, lst(tmp).descr, lst(tmp).LastVersion, lst(tmp).LastBukkit.Trim(BukkitTrim)})
                                Else
                                    Continue For
                                End If
                            Else
                                Continue For
                            End If

                            lvi.Tag = lst(tmp).main
                            lvi.ToolTipText = "Double click for more information. Right Click for options."
                            ALVBukGetPlugins.Items.Add(lvi)
                        End If
                    Next
                    LblInstallPluginsInfo.Text = lr("Loaded") & " " & ALVBukGetPlugins.Items.Count & " " & lr("plugins") & ". " & lr("Right click for options, double click for details.") & " " & lr("Ignored") & " " & lst.Count - ALVBukGetPlugins.Items.Count & " " & lr("plugins")
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in thd_SetBukgetPlugins!", ex.Message)
            End Try
        End Sub


        Private Sub ALVBukGetPlugins_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ALVBukGetPlugins.MouseDoubleClick

            If ALVBukGetPlugins.SelectedItems Is Nothing OrElse ALVBukGetPlugins.SelectedItems.Count < 1 Then Exit Sub

            Dim plugindialog As New BukgetPluginDialog(BukGetAPI.GetPluginInfoByNamespace(ALVBukGetPlugins.SelectedItems(0).Tag))
            plugindialog.Show()
        End Sub

#End Region

#Region "Installed"

        Private Sub ALVInstalledPlugins_MouseDoubleClick() Handles ALVInstalledPlugins.MouseDoubleClick, CmenuInstalledPluginsMoreInfo.Click
            If ALVInstalledPlugins.SelectedItems Is Nothing OrElse ALVInstalledPlugins.SelectedItems.Count < 1 Then Exit Sub

            Dim plugindialog As New InstalledPluginDialog(GetPluginByFileName(ALVInstalledPlugins.SelectedItems(0).Tag))
            plugindialog.Show()

        End Sub

        Private Sub CmenuInstalledPluginsViewVersions_Click(sender As System.Object, e As System.EventArgs) Handles CmenuInstalledPluginsViewVersions.Click
            If ALVInstalledPlugins.SelectedItems Is Nothing OrElse ALVInstalledPlugins.SelectedItems.Count < 1 Then Exit Sub
            Dim plugindialog As New BukgetPluginDialog(GetPluginMainspaceByFileName(ALVInstalledPlugins.SelectedItems(0).Tag))
            plugindialog.Show()
        End Sub

        Private Sub CmenuInstalledPluginsUpdate_Click(sender As System.Object, e As System.EventArgs) Handles CmenuInstalledPluginsUpdate.Click
            If ALVInstalledPlugins.SelectedItems Is Nothing OrElse ALVInstalledPlugins.SelectedItems.Count < 1 Then Exit Sub

            Dim ud As PluginUpdater
            If ALVInstalledPlugins.SelectedItems.Count = 1 Then
                ud = New PluginUpdater(GetPluginByFileName(ALVInstalledPlugins.SelectedItems(0).Tag))
            Else
                Dim l As New List(Of plugindescriptor)
                For Each item As ListViewItem In ALVInstalledPlugins.SelectedItems
                    l.Add(GetPluginByFileName(item.Tag))
                Next
                ud = New PluginUpdater(l)
            End If
            ud.ShowDialog()
            If ud.Updated Then RefreshAllInstalledPluginsAsync()
        End Sub

        Private Sub CmenuInstalledPluginsProjectPage_Click(sender As System.Object, e As System.EventArgs) Handles CmenuInstalledPluginsProjectPage.Click
            If ALVInstalledPlugins.SelectedItems Is Nothing OrElse ALVInstalledPlugins.SelectedItems.Count < 1 Then Exit Sub
            Dim item = ALVInstalledPlugins.SelectedItems(0)
            OpenProjectPageByFileName(item.Tag)
        End Sub

        Private Sub CmenuInstalledPluginsRemove_Click(sender As System.Object, e As System.EventArgs) Handles CmenuInstalledPluginsRemove.Click
            If ALVInstalledPlugins.SelectedItems Is Nothing OrElse ALVInstalledPlugins.SelectedItems.Count < 1 Then Exit Sub

            For Each item As ListViewItem In ALVInstalledPlugins.SelectedItems
                RemoveInstalledplugin(GetPluginByFileName(item.Tag))
            Next

        End Sub

        Private Sub RefreshListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CmenuInstalledPluginsRefresh.Click
            InstalledPluginManager.ClearPluginCache()
            InstalledPluginManager.InitAsync()
        End Sub

        Private Sub CmenuInstalledPluginsOpenFolder_Click(sender As System.Object, e As System.EventArgs) Handles CmenuInstalledPluginsOpenFolder.Click
            InstalledPluginManager.ShowPluginsFolder()
        End Sub

        Private Sub LoadInstalledPlugins(l As Dictionary(Of String, plugindescriptor))
            Try
                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If

                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf LoadInstalledPlugins)
                    Me.Invoke(d, New Object() {l})
                Else
                    Try
                        ALVInstalledPlugins.Items.Clear()
                        If l Is Nothing Then livebug.write(loggingLevel.Fine, "mainform", "Refreshing installed plugins: NULL dictionary passed!", "mainform-pluginmanager") : Exit Sub
                        livebug.write(loggingLevel.Fine, "mainform", "Refreshing installed plugins... (" & l.Count & ")", "mainform-pluginmanager")
                        If l.Count > 0 Then
                            Dim lvc As UInt32 = l.Values.Count
                            For i As UInt16 = 0 To lvc - 1
                                Try
                                    Dim lvi As ListViewItem = CreateInstalledPluginLVI(l.Values(i))
                                    If lvi IsNot Nothing Then ALVInstalledPlugins.Items.Add(lvi)
                                Catch ex As Exception
                                    livebug.write(loggingLevel.Warning, "mainform-pluginmanager", "Could not add plugin to plugin list, id:" & i, ex.Message)
                                End Try
                            Next
                        Else
                            livebug.write(loggingLevel.Fine, "mainform", "No plugins found, nothing added to list", "mainform-pluginmanager")
                        End If
                        ALVInstalledPlugins.Refresh()
                        livebug.write(loggingLevel.Fine, "mainform", "Updated installed plugins list", "mainform-pluginmanager")
                        lblinstalledpluginsInfo.Text = lr("Loaded") & " " & l.Values.Count & " " & lr("plugins") & ". " & lr("Right click for options, double click for details. Ctrl+click or Shift+click to select multiple items.")
                    Catch ex As Exception
                        livebug.write(loggingLevel.Severe, "mainform", "An exception occured when trying to load installed plugins:" & ex.Message)
                    End Try
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in LoadInstalledPlugins!", ex.Message)
            End Try
        End Sub

        Private Function CreateInstalledPluginLVI(pld As plugindescriptor) As ListViewItem
            Try
                If pld Is Nothing Then Return Nothing : Exit Function

                Dim lvi As ListViewItem



                If pld.main Is Nothing OrElse pld.main = "" Then
                    If pld.filename IsNot Nothing AndAlso pld.name IsNot Nothing Then
                        Dim fname As String = pld.filename
                        If fname.Contains(".") Then fname = fname.Split(".")(0)
                        lvi = New ListViewItem({fname, "", "", "[GUI] Plugin details not available"})
                        lvi.Tag = pld.filename
                        lvi.ToolTipText = lr("No information available")
                        Return lvi
                        Exit Function
                    Else
                        Return Nothing
                        Exit Function
                    End If
                End If

                If pld.name Is Nothing Then
                    If pld.filename IsNot Nothing Then
                        Dim fname As String = pld.filename
                        If fname.Contains(".") Then fname = fname.Split(".")(0)
                        lvi = New ListViewItem({fname, "", "", "[GUI] Plugin details not available"})
                        lvi.Tag = pld.filename
                        lvi.ToolTipText = lr("No information available")
                        Return lvi
                        Exit Function
                    Else
                        Return Nothing
                        Exit Function
                    End If
                End If

                If pld.authors Is Nothing Then pld.authors = Array.CreateInstance(GetType(String), 1)
                If pld.version Is Nothing Then pld.version = ""
                If pld.description Is Nothing Then pld.description = ""
                If pld.FileCreationDate.Year > 2005 Then
                    lvi = New ListViewItem({pld.name, pld.version, common.serialize(pld.authors), pld.description, pld.FileCreationDate.Year.ToString.PadLeft(4, "0") & "/" & pld.FileCreationDate.Month.ToString.PadLeft(2, "0") & "/" & pld.FileCreationDate.Day.ToString.PadLeft(2, "0")})
                Else
                    lvi = New ListViewItem({pld.name, pld.version, common.serialize(pld.authors), pld.description, "unknown"})
                End If

                lvi.Tag = pld.filename
                lvi.ToolTipText = lr("Double click for more information. Right Click for options.")
                Return lvi
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in CreateInstalledPluginLVI", ex.Message)
                Return Nothing
            End Try
        End Function
#End Region

#End Region

#Region "Options & info"

#Region "info"
        Private Sub info_load()
            livebug.write(loggingLevel.Fine, "mainform", "Loading info items")
            lblInfoAppName.Text = lr("Name:") & " " & My.Application.Info.AssemblyName
            lblInfoAppAuthors.Text = lr("Author(s):") & " Bertware"
            lblInfoAppVersion.Text = lr("Version:") & " " & My.Application.Info.Version.ToString
            lblInfoAppCopyright.Text = lr("Copyright:") & " " & My.Application.Info.Copyright
            ALlblInfoAppWeb.Text = lr("Web:") & " http://dev.bukkit.org/projects/bukkitGUI"

            LblInfoComputerComputerName.Text = lr("Computer name:") & " " & My.Computer.Name
            LblInfoComputerCPU.Text = lr("CPU:") & " " & WMI.GetprocessorInfo(WMI.processorprop.Name)
            If common.IsRunningOnMono = False Then
                LblInfoComputerRAM.Text = lr("RAM:") & " " & Math.Round(My.Computer.Info.TotalPhysicalMemory / 1024 / 1024) & "MB"
                LblInfoComputerOS.Text = lr("OS:") & " " & My.Computer.Info.OSFullName
            Else
                LblInfoComputerRAM.Text = lr("RAM:") & "Unkown (running mono)"
                LblInfoComputerOS.Text = lr("OS:") & " Mono"
            End If

            LblInfoComputerLocIP.Text = lr("Local IP:") & " " & WMI.GetInternalIP

            Dim t As New Thread(AddressOf serverinteraction.getIp)
            t.Name = "info_get_external_ip"
            t.IsBackground = True
            t.Start()
        End Sub

        Private Sub hnd_ip_loaded(ip As String)
            Try
                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf hnd_ip_loaded)
                    Me.Invoke(d, New Object() {ip})
                Else
                    LblInfoComputerExtIP.Text = lr("Ext. IP:") & " " & ip
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "Mainform", "External IP cannot be shown!", ex.Message)
            End Try
        End Sub

        Private Sub hnd_last_version_loaded(upd As Bertware.Get.UpdateInfo)
            Try
                If Me.Created = False Then
                    Dim i As Byte = 0
                    While Me.Created = False
                        Thread.Sleep(WAIT_CREATION_TIMEOUT)
                        i = i + 1
                        If i = MAX_WAIT_CREATION_CYCLES Then Exit Sub
                    End While
                End If
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf hnd_last_version_loaded)
                    Me.Invoke(d, New Object() {upd.Version})
                Else
                    If upd IsNot Nothing AndAlso upd.Version IsNot Nothing Then lblInfoAppLatest.Text = lr("Latest version:") & " " & upd.Version
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "Mainform", "Latest version cannot be shown!", ex.Message)
            End Try
        End Sub

        Private Sub BtnInfoAppUpdater_Click(sender As System.Object, e As System.EventArgs) Handles BtnInfoAppUpdater.Click
            Try
                Net.Bertware.Get.ShowUpdater()
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "Mainform", "Can't open updater!", ex.Message)
            End Try

        End Sub
#End Region

#Region "Tray and sound settings"

        Private Sub ChkInfoTrayMinimize_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoTrayMinimize.CheckedChanged
            tray_minimize = ChkInfoTrayMinimize.Checked
        End Sub

        Private Sub ChkInfoTrayAlways_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoTrayAlways.CheckedChanged
            tray_always = ChkInfoTrayAlways.Checked
        End Sub

        Private Sub ChkInfoTrayOnJoin_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoTrayOnJoin.CheckedChanged
            tray_onPlayerJoin = ChkInfoTrayOnJoin.Checked
        End Sub

        Private Sub OnLeave_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoTrayOnLeave.CheckedChanged
            tray_onPlayerDisconnect = ChkInfoTrayOnLeave.Checked
        End Sub

        Private Sub ChkInfoTrayOnWarning_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoTrayOnWarning.CheckedChanged
            tray_onWarning = ChkInfoTrayOnWarning.Checked
        End Sub

        Private Sub ChkInfoTrayOnSevere_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoTrayOnSevere.CheckedChanged
            tray_onSevere = ChkInfoTrayOnSevere.Checked
        End Sub

        Private Sub btnCmenuTrayClose_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuTrayExit.Click
            Me.Close()
        End Sub

        Private _tray_onPlayerJoin As Boolean, _tray_onplayerDisconnect As Boolean, _tray_onWarning As Boolean, _tray_onsevere As Boolean, _tray_showTime As UInt16, _tray_minimize As Boolean, _tray_always As Boolean

        Public Property tray_onPlayerJoin As Boolean
            Get
                Return _tray_onPlayerJoin
            End Get
            Set(value As Boolean)
                config.writeAsBool("join", value, "tray")
                _tray_onPlayerJoin = value
            End Set
        End Property

        Public Property tray_onPlayerDisconnect As Boolean
            Get
                Return _tray_onplayerDisconnect
            End Get
            Set(value As Boolean)
                config.writeAsBool("disconnect", value, "tray")
                _tray_onPlayerJoin = value
            End Set
        End Property

        Public Property tray_onWarning As Boolean
            Get
                Return _tray_onWarning
            End Get
            Set(value As Boolean)
                config.writeAsBool("warning", value, "tray")
                _tray_onPlayerJoin = value
            End Set
        End Property

        Public Property tray_onSevere As Boolean
            Get
                Return _tray_onsevere
            End Get
            Set(value As Boolean)
                config.writeAsBool("severe", value, "tray")
                _tray_onPlayerJoin = value
            End Set
        End Property

        Public Property tray_minimize As Boolean
            Get
                Return _tray_minimize
            End Get
            Set(value As Boolean)
                config.writeAsBool("minimize", value, "tray")
                _tray_minimize = value
            End Set
        End Property

        Public Property tray_always As Boolean
            Get
                Return _tray_always
            End Get
            Set(value As Boolean)
                config.writeAsBool("always", value, "tray")
                _tray_always = value
            End Set
        End Property

        Public Property tray_showtime As UInt16
            Get
                Return _tray_showTime
            End Get
            Set(value As UInt16)
                config.write("time", value.ToString, "tray")
                _tray_showTime = value
            End Set
        End Property

        Public Sub tray_sound_init()
            _tray_showTime = CInt(config.read("time", "500", "tray"))
            _tray_onPlayerJoin = config.readAsBool("join", False, "tray")
            _tray_onplayerDisconnect = config.readAsBool("disconnect", False, "tray")
            _tray_onWarning = config.readAsBool("warning", False, "tray")
            _tray_onsevere = config.readAsBool("severe", False, "tray")
            _tray_always = config.readAsBool("always", False, "tray")
            _tray_minimize = config.readAsBool("minimize", False, "tray")

            ChkInfoTrayMinimize.Checked = tray_minimize
            ChkInfoTrayAlways.Checked = tray_always
            ChkInfoTrayOnJoin.Checked = tray_onPlayerJoin
            ChkInfoTrayOnLeave.Checked = tray_onPlayerDisconnect
            ChkInfoTrayOnWarning.Checked = tray_onWarning
            ChkInfoTrayOnSevere.Checked = tray_onSevere

            ChkInfoSoundOnJoin.Checked = SoundNotificator.onPlayerJoin
            ChkInfoSoundOnLeave.Checked = SoundNotificator.onPlayerDisconnect
            ChkInfoSoundOnWarning.Checked = SoundNotificator.onWarning
            ChkInfoSoundOnSevere.Checked = SoundNotificator.onSevere
        End Sub

        Public Sub SendToTray()
            livebug.write(loggingLevel.Fine, "Mainform", "Sending GUI to tray...")
            WindowState = FormWindowState.Minimized
            ShowInTaskbar = False
            Visible = False

            common.mainwinhnd = Me.Handle
            livebug.write(loggingLevel.Fine, "mainform", "Mainform window handle saved!")

            livebug.write(loggingLevel.Fine, "Mainform", "GUI minimized to tray...")
        End Sub

        Public Sub ShowFromTray()
            Try
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf ShowFromTray)
                    Me.Invoke(d)
                Else
                    livebug.write(loggingLevel.Fine, "Mainform", "Showing GUI from tray...")
                    Visible = True
                    ShowInTaskbar = True
                    WindowState = FormWindowState.Normal
                    ARTXTServerOutput.ScrollToBottom()
                    'text dissapears, fix
                    ARTXTServerOutput.SelectionStart = ARTXTServerOutput.TextLength 'make sure cursor is at end of the text
                    'Setting the correct color
                    ARTXTServerOutput.SelectionColor = Color.Black
                    ARTXTServerOutput.SelectedText = " "
                    ARTXTServerOutput.SelectedText = ""
                    common.mainwinhnd = Me.Handle
                    livebug.write(loggingLevel.Fine, "mainform", "Mainform window handle saved!")

                    livebug.write(loggingLevel.Fine, "Mainform", "Showed GUI from tray...")
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "Mainform", "Error while showing GUI from tray!", ex.Message)
            End Try
        End Sub

        Public ReadOnly Property IsToTray As Boolean
            Get
                Return Me.Visible = False AndAlso Me.ShowInTaskbar = False AndAlso Me.WindowState = FormWindowState.Minimized
            End Get
        End Property

        Private Sub ChkInfoSoundOnJoin_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoSoundOnJoin.CheckedChanged
            SoundNotificator.onPlayerJoin = CType(sender, CheckBox).Checked
        End Sub

        Private Sub ChkInfoSoundOnLeave_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoSoundOnLeave.CheckedChanged
            SoundNotificator.onPlayerDisconnect = CType(sender, CheckBox).Checked
        End Sub

        Private Sub ChkInfoSoundOnWarning_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoSoundOnWarning.CheckedChanged
            SoundNotificator.onWarning = CType(sender, CheckBox).Checked
        End Sub

        Private Sub ChkInfoSoundOnSevere_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkInfoSoundOnSevere.CheckedChanged
            SoundNotificator.onSevere = CType(sender, CheckBox).Checked
        End Sub


#End Region

#Region "settings"
        Private Sub Settings_init()
            Dim faking As Boolean = initialize_completed
            initialize_completed = False

            ChkOptionsLightMode.Checked = common.isRunningLight

            If Not common.isRunningLight Then
                Dim current As String = language.current_language
                CBInfoSettingsLanguage.Items.Clear()
                For Each lang As String In language.languages
                    CBInfoSettingsLanguage.Items.Add(lang)
                    If lang = current Then
                        CBInfoSettingsLanguage.SelectedIndex = CBInfoSettingsLanguage.Items.IndexOf(lang)
                    End If
                Next
                CBInfoSettingsLanguage.Items.Add("Install more languages...")
            Else
                CBInfoSettingsLanguage.Enabled = False
            End If

            CBInfoSettingsFileLocation.Items.Clear()
            CBInfoSettingsFileLocation.Items.Add("Appdata")
            CBInfoSettingsFileLocation.Items.Add("Server folder")
            If filelocation.location = filelocation.filelocation.local_files Then CBInfoSettingsFileLocation.SelectedIndex = 1 Else CBInfoSettingsFileLocation.SelectedIndex = 0

#If DEBUG Then
            CBInfoSettingsFileLocation.Enabled = False
#End If

            ChkOptionsCheckUpdates.Checked = config.readAsBool("auto_update", "true", "options")
            ChkOptionsTabplayers.Checked = config.readAsBool("enable_tabplayers", True, "options")
            ChkOptionsTabplayers_CheckedChanged(ChkOptionsTabplayers, Nothing)

            If Not common.isRunningLight Then
                ChkOptionsTaberrors.Checked = config.readAsBool("enable_taberrorlogging", True, "options")
                ChkOptionsTaberrors_CheckedChanged(ChkOptionsTaberrors, Nothing)
                ChkOptionsTabTaskManager.Checked = config.readAsBool("enable_tabtaskmanager", True, "options")
                ChkOptionsTabTaskManager_CheckedChanged(ChkOptionsTabTaskManager, Nothing)
                ChkOptionsTabPlugins.Checked = config.readAsBool("enable_tabplugins", True, "options")
                ChkOptionsTabPlugins_CheckedChanged(ChkOptionsTabPlugins, Nothing)
                ChkOptionsTabServeroptions.Checked = config.readAsBool("enable_tabserveroptions", True, "options")
                ChkOptionsTabServeroptions_CheckedChanged(ChkOptionsTabServeroptions, Nothing)
            Else
                ChkOptionsTaberrors.Enabled = False
                ChkOptionsTabTaskManager.Enabled = False
                ChkOptionsTabPlugins.Enabled = False
                ChkOptionsTabServeroptions.Enabled = False
            End If


            Try
                Dim regKey As Microsoft.Win32.RegistryKey
                regKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", False)
                Dim res = regKey.GetValue(My.Application.Info.AssemblyName)
                If res IsNot Nothing Then
                    ChkAutoStartWindows.Checked = True
                Else
                    ChkAutoStartWindows.Checked = False
                End If
                regKey.Close()
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "mainform", "Couldn't load autostart settings", ex.Message)
            End Try

            If faking Then initialize_completed = True
        End Sub

        Private Sub CBInfoSettingsLanguage_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBInfoSettingsLanguage.SelectedIndexChanged
            If Not initialize_completed Then Exit Sub

            If CBInfoSettingsLanguage.SelectedItem.ToString = "Install more languages..." Then
                LanguageInstaller.ShowDialog()
                Settings_init()
            Else
                language_file = GetLanguageFilePath(CBInfoSettingsLanguage.SelectedItem.ToString)
                MessageBox.Show(lr("You changed the language to ") & CBInfoSettingsLanguage.SelectedItem.ToString & vbCrLf & lr("Restart the GUI for the changes to take effect"), lr("Restart required"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub CBInfoSettingsFileLocation_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBInfoSettingsFileLocation.SelectedIndexChanged
#If Not Debug Then

            If Not initialize_completed Then Exit Sub
            Select Case CBInfoSettingsFileLocation.SelectedItem.ToString.ToLower
                Case "appdata"
                    filelocation.location = filelocation.filelocation.global_files
                Case "server folder"
                    filelocation.location = filelocation.filelocation.local_files
            End Select
#End If
        End Sub

        Private Sub ChkOptionsTabplayers_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkOptionsTabplayers.CheckedChanged
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim tab As TabPage = TabPlayers
            Dim i As Byte = 1

            If chk.Checked Then
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    Exit Sub
                Else
                    TabCtrlMain.TabPages.Add(tab)
                End If

            Else
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    TabCtrlMain.TabPages.Remove(tab)
                Else
                    Exit Sub
                End If

            End If

        End Sub

        Private Sub ChkOptionsTaberrors_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkOptionsTaberrors.CheckedChanged
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim tab As TabPage = TabErrorLogging

            If chk.Checked Then
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    Exit Sub
                Else
                    TabCtrlMain.TabPages.Add(tab)
                End If

            Else
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    TabCtrlMain.TabPages.Remove(tab)
                Else
                    Exit Sub
                End If

            End If
        End Sub

        Private Sub ChkOptionsTabTaskManager_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkOptionsTabTaskManager.CheckedChanged
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim tab As TabPage = TabTaskManager

            If chk.Checked Then
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    Exit Sub
                Else
                    TabCtrlMain.TabPages.Add(tab)
                End If

            Else
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    TabCtrlMain.TabPages.Remove(tab)
                Else
                    Exit Sub
                End If

            End If
        End Sub

        Private Sub ChkOptionsTabPlugins_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkOptionsTabPlugins.CheckedChanged
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim tab As TabPage = TabPlugins

            If chk.Checked Then
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    Exit Sub
                Else
                    TabCtrlMain.TabPages.Add(tab)
                End If

            Else
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    TabCtrlMain.TabPages.Remove(tab)
                Else
                    Exit Sub
                End If

            End If
        End Sub

        Private Sub ChkOptionsTabServeroptions_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkOptionsTabServeroptions.CheckedChanged
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim tab As TabPage = TabServerOptions

            If chk.Checked Then
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    Exit Sub
                Else
                    TabCtrlMain.TabPages.Add(tab)
                End If

            Else
                If initialize_completed Then config.writeAsBool("enable_" & tab.Name, chk.Checked, "options")
                If TabCtrlMain.TabPages.Contains(tab) Then
                    TabCtrlMain.TabPages.Remove(tab)
                Else
                    Exit Sub
                End If

            End If
        End Sub

        Private Sub lightmodechanged() Handles ChkOptionsLightMode.Click
            MessageBox.Show(lr("These changes will take effect the next time you start the GUI"), lr("Restart required"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            config.writeAsBool("LightMode", ChkOptionsLightMode.Checked)
        End Sub


        Private Sub ChkOptionsCheckUpdates_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkOptionsCheckUpdates.CheckedChanged
            config.writeAsBool("auto_update", ChkOptionsCheckUpdates.Checked, "options")
        End Sub
#End Region


        Private Sub BtnOptionsResetAll_Click(sender As System.Object, e As System.EventArgs) Handles BtnOptionsResetAll.Click
            If MessageBox.Show(lr("Do you really want to reset all GUI settings?"), lr("Reset settings"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
            livebug.write(loggingLevel.Fine, "Mainform", "Resetting GUI!")
            If server.running Then
                Dim ssd As New ServerStopDialog
                ssd.ShowDialog()
            End If
            MessageBox.Show(lr("All settings will be resetted. You need to restart the GUI in order for the changes to take effect"), lr("Reset settings"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            livebug.dispose(True)
            common.Reset()
            Me.Close()
        End Sub

#End Region

        Private Sub chkAutoStartWindows_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkAutoStartWindows.CheckedChanged
            Try
                Dim regKey As Microsoft.Win32.RegistryKey
                regKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)

                If ChkAutoStartWindows.Checked Then
                    regKey.SetValue(My.Application.Info.AssemblyName, My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".exe")
                    regKey.Close()
                    livebug.write(loggingLevel.Fine, "mainform", "Enabled windows Autostart")
                Else
                    regKey.DeleteValue(My.Application.Info.AssemblyName, False)
                    regKey.Close()
                    livebug.write(loggingLevel.Fine, "mainform", "Disabled windows Autostart")
                End If
            Catch ex As Exception
                MessageBox.Show(lr("Can't access autostart functions! Are you running this as administrator?"), lr("Can't access autostart"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ChkRunServerOnGUIStart_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkRunServerOnGUIStart.CheckedChanged
            config.writeAsBool("autostart", ChkRunServerOnGUIStart.Checked)
        End Sub

        Private Sub autoserverstart_check()
            ChkRunServerOnGUIStart.Checked = config.readAsBool("autostart", False)
            If ChkRunServerOnGUIStart.Checked Then start_server()
        End Sub

#Region "text options"

        Private Sub text_options_init()
            Try
                ChkShowTime.Checked = serverOutputHandler.Show_time
                ChkShowDate.Checked = serverOutputHandler.Show_date
                ARTXTServerOutput.Font = usedfont

                TxtSettingsTextColorInfo.BackColor = serverOutputHandler.clrInfo
                TxtSettingsTextColorPlayer.BackColor = serverOutputHandler.clrPlayerEvent
                TxtSettingsTextColorWarning.BackColor = serverOutputHandler.clrWarning
                TxtSettingsTextColorSevere.BackColor = serverOutputHandler.clrSevere
                TxtSettingsTextColorUnknown.BackColor = serverOutputHandler.clrUnknown

                CBTextOptionsFont.Populate(True)

                Dim Fname As String = config.read("font_name", "arial", "output").ToString
                Dim Fsize As Integer = CInt(config.read("font_size", "9", "output"))
                ChkTextUTF8.Checked = config.readAsBool("utf_8_compatibility", False, "output")
                CBTextOptionsFont.SelectedIndex = CBTextOptionsFont.FindStringExact(Fname)
                NumTextOptionsFontSize.Value = Fsize
                usedfont = New Font(Fname, Fsize)

                SetFont(usedfont)
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Could not initialize fonts", ex.Message)
            End Try
        End Sub


        Private Sub ChkTextChineseCompatibility_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkTextUTF8.CheckedChanged
            config.writeAsBool("utf_8_compatibility", ChkTextUTF8.Checked, "output")
        End Sub

        Private Sub SetFont(font As Font)
            TxtSettingsTextFontPreview.Font = font
            ARTXTServerOutput.Font = font
            ALVErrors.Font = font
            ALVPlayersPlayers.Font = font
            ALVInstalledPlugins.Font = font
            ALVBukGetPlugins.Font = font
            ALVGeneralPlayers.Font = font
        End Sub

        Private Sub ChkShowTime_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkShowTime.CheckedChanged
            serverOutputHandler.Show_time = ChkShowTime.Checked
        End Sub

        Private Sub ChkShowDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkShowDate.CheckedChanged
            serverOutputHandler.Show_date = ChkShowDate.Checked
        End Sub

        Private Sub CBTextOptionsFont_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CBTextOptionsFont.SelectedIndexChanged
            If Not initialize_completed Then Exit Sub
            Dim Fname As String = CBTextOptionsFont.SelectedItem.ToString
            Dim Fsize As Integer = NumTextOptionsFontSize.Value
            usedfont = New Font(Fname, Fsize)
            SetFont(usedfont)
            config.write("font_name", Fname, "output")
            config.write("font_size", Fsize, "output")
        End Sub

        Private Sub NumTextOptionsFontSize_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumTextOptionsFontSize.ValueChanged
            If Not initialize_completed Then Exit Sub
            Dim Fname As String = CBTextOptionsFont.SelectedItem.ToString
            Dim Fsize As Integer = NumTextOptionsFontSize.Value
            usedfont = New Font(Fname, Fsize)
            SetFont(usedfont)
            config.write("font_name", Fname, "output")
            config.write("font_size", Fsize, "output")
        End Sub


        Private Sub TxtSettingsTextColorInfo_click(sender As System.Object, e As System.EventArgs) Handles TxtSettingsTextColorInfo.Click
            Dim cd As New ColorDialog
            cd.FullOpen = True
            cd.AnyColor = True
            cd.AllowFullOpen = True
            cd.SolidColorOnly = False
            If cd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(sender, TextBox).BackColor = cd.Color
                serverOutputHandler.clrInfo = cd.Color
            End If
        End Sub

        Private Sub TxtSettingsTextColorPlayer_click(sender As System.Object, e As System.EventArgs) Handles TxtSettingsTextColorPlayer.Click
            Dim cd As New ColorDialog
            cd.FullOpen = True
            cd.AnyColor = True
            cd.AllowFullOpen = True
            cd.SolidColorOnly = False
            If cd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(sender, TextBox).BackColor = cd.Color
                serverOutputHandler.clrPlayerEvent = cd.Color
            End If

        End Sub

        Private Sub TxtSettingsTextColorWarning_click(sender As System.Object, e As System.EventArgs) Handles TxtSettingsTextColorWarning.Click
            Dim cd As New ColorDialog
            cd.FullOpen = True
            cd.AnyColor = True
            cd.AllowFullOpen = True
            cd.SolidColorOnly = False
            If cd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(sender, TextBox).BackColor = cd.Color
                serverOutputHandler.clrWarning = cd.Color
            End If

        End Sub

        Private Sub TxtSettingsTextColorSevere_click(sender As System.Object, e As System.EventArgs) Handles TxtSettingsTextColorSevere.Click
            Dim cd As New ColorDialog
            cd.FullOpen = True
            cd.AnyColor = True
            cd.AllowFullOpen = True
            cd.SolidColorOnly = False
            If cd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(sender, TextBox).BackColor = cd.Color
                serverOutputHandler.clrSevere = cd.Color
            End If

        End Sub

        Private Sub TxtSettingsTextColorUnknown_click(sender As System.Object, e As System.EventArgs) Handles TxtSettingsTextColorUnknown.Click
            Dim cd As New ColorDialog
            cd.FullOpen = True
            cd.AnyColor = True
            cd.AllowFullOpen = True
            cd.SolidColorOnly = False
            If cd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                CType(sender, TextBox).BackColor = cd.Color
                serverOutputHandler.clrUnknown = cd.Color
            End If

        End Sub

#End Region

#Region "task manager"
        Private Sub TaskManager_UpdateUI()
            Try
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf TaskManager_UpdateUI)
                    Me.Invoke(d, New Object())
                Else
                    If TaskManager.tasks Is Nothing Then Exit Sub
                    ALVTaskPlanner.Items.Clear()
                    For Each task As TaskManager.task In TaskManager.tasks
                        ALVTaskPlanner.Items.Add(GetTaskListLVI(task))
                    Next
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in TaskManager_UpdateUI!", ex.Message)
            End Try
        End Sub

        Private Function GetTaskListLVI(t As TaskManager.task) As ListViewItem
            Dim lvi As New ListViewItem({t.name, t.trigger_type.ToString.Replace("_", " "), t.trigger_parameters.ToString, t.action_type.ToString.Replace("_", " "), t.action_parameters.ToString, t.IsEnabled.ToString.ToLower})
            lvi.Tag = t.name
            lvi.Checked = t.IsEnabled
            Return lvi
        End Function

        Private Sub BtnTaskManagerAdd_Click(sender As System.Object, e As System.EventArgs) Handles BtnTaskManagerAdd.Click
            Dim td As New TaskDialog
            td.ShowDialog()
        End Sub

        Private Sub BtnTaskManagerEdit_Click(sender As System.Object, e As System.EventArgs) Handles BtnTaskManagerEdit.Click, ALVTaskPlanner.DoubleClick
            If ALVTaskPlanner.SelectedItems Is Nothing OrElse ALVTaskPlanner.SelectedItems.Count < 1 OrElse ALVTaskPlanner.SelectedItems(0) Is Nothing Then Exit Sub
            Dim t As TaskManager.task = TaskManager.GetTaskByName(ALVTaskPlanner.SelectedItems(0).SubItems(0).Text)
            Dim td As New TaskDialog
            td.task = t
            td.ShowDialog()
        End Sub

        Private Sub BtnTaskManagerRemove_Click(sender As System.Object, e As System.EventArgs) Handles BtnTaskManagerRemove.Click
            If ALVTaskPlanner.SelectedItems Is Nothing OrElse ALVTaskPlanner.SelectedItems.Count < 1 OrElse ALVTaskPlanner.SelectedItems(0) Is Nothing Then Exit Sub
            For Each item As ListViewItem In ALVTaskPlanner.SelectedItems
                TaskManager.deleteTask(item.SubItems(0).Text)
            Next
        End Sub

        Private Sub BtnTaskManagerImport_Click(sender As System.Object, e As System.EventArgs) Handles BtnTaskManagerImport.Click
            TaskManager.import()
        End Sub

        Private Sub BtnTaskManagerExport_Click(sender As System.Object, e As System.EventArgs) Handles BtnTaskManagerExport.Click
            If ALVTaskPlanner.SelectedItems Is Nothing OrElse ALVTaskPlanner.SelectedItems.Count < 1 OrElse ALVTaskPlanner.SelectedItems(0) Is Nothing Then Exit Sub

            If ALVTaskPlanner.SelectedItems.Count = 1 Then
                TaskManager.export(ALVTaskPlanner.SelectedItems(0).SubItems(0).Text)
            Else
                Dim l As New List(Of String)
                For Each item As ListViewItem In ALVTaskPlanner.SelectedItems
                    l.Add(item.SubItems(0).Text)
                Next
                TaskManager.export(l)
            End If
        End Sub

        Private Sub BtnTaskManagerTest_Click(sender As System.Object, e As System.EventArgs) Handles BtnTaskManagerTest.Click
            If ALVTaskPlanner.SelectedItems Is Nothing OrElse ALVTaskPlanner.SelectedItems.Count < 1 OrElse ALVTaskPlanner.SelectedItems(0) Is Nothing Then Exit Sub
            TaskManager.GetTaskByName(ALVTaskPlanner.SelectedItems(0).SubItems(0).Text).Execute()
        End Sub

        '  Private Sub ALVTaskPlanner_CheckChanged(s As Object, e As ItemCheckEventArgs) 'Handles ALVTaskPlanner.ItemCheck
        '      If ALVTaskPlanner.SelectedItems Is Nothing OrElse ALVTaskPlanner.SelectedItems.Count < 1 OrElse ALVTaskPlanner.SelectedItems(0) Is Nothing Then Exit Sub
        '  Dim item As ListViewItem = ALVTaskPlanner.Items(e.Index)
        '      If item.Checked = True Then TaskManager.TaskManager.disableTask(TaskManager.TaskManager.GetTaskByName(item.SubItems(0).Text)) : item.Checked = False ' TaskManager.TaskManager.GetTaskByName(item.SubItems(0).Text).IsEnabled
        '      If item.Checked = False Then TaskManager.TaskManager.enableTask(TaskManager.TaskManager.GetTaskByName(item.SubItems(0).Text)) : item.Checked = True 'TaskManager.TaskManager.GetTaskByName(item.SubItems(0).Text).IsEnabled
        '  End Sub
        '
        '        Private Sub ALVTaskPlanner_CheckedChanged(s As Object, e As ItemCheckedEventArgs) Handles ALVTaskPlanner.ItemChecked
        '            If ALVTaskPlanner.SelectedItems Is Nothing OrElse ALVTaskPlanner.SelectedItems.Count < 1 OrElse ALVTaskPlanner.SelectedItems(0) Is Nothing Then Exit Sub
        '            If e.Item.Checked = True Then TaskManager.TaskManager.disableTask(TaskManager.TaskManager.GetTaskByName(e.Item.SubItems(0).Text)) : e.Item.Checked = False 'TaskManager.TaskManager.GetTaskByName(e.Item.SubItems(0).Text).IsEnabled
        '            If e.Item.Checked = False Then TaskManager.TaskManager.enableTask(TaskManager.TaskManager.GetTaskByName(e.Item.SubItems(0).Text)) : e.Item.Checked = True 'TaskManager.TaskManager.GetTaskByName(e.Item.SubItems(0).Text).IsEnabled
        '        End Sub
#End Region

#Region "Error manager"
        Private Sub errorLVICreated(lvi As ListViewItem) 'ID | Type  | Time | Text
            If lvi Is Nothing Then Exit Sub
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf errorLVICreated)
                Me.Invoke(d, New Object() {lvi})
            Else
                ALVErrors.Items.Add(lvi)
            End If

        End Sub

        Private Sub ErrorManager_InitUI()
            ChkErrorsHideWarning.Checked = ErrorManager.Hide_Warnings
            ChkErrorsHideError.Checked = ErrorManager.Hide_Errors
            ChkErrorsHideStackTrace.Checked = ErrorManager.Hide_Stacktrace
        End Sub

        Private Sub ChkErrorsHideWarning_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkErrorsHideWarning.CheckedChanged
            ErrorManager.Hide_Warnings = ChkErrorsHideWarning.Checked
        End Sub

        Private Sub ChkErrorsHideError_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkErrorsHideError.CheckedChanged
            ErrorManager.Hide_Errors = ChkErrorsHideError.Checked
        End Sub

        Private Sub ChkErrorsHideStackTrace_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkErrorsHideStackTrace.CheckedChanged
            ErrorManager.Hide_Stacktrace = ChkErrorsHideStackTrace.Checked
        End Sub

        Private Sub BtnErrorLoggingDetails_Click(sender As System.Object, e As System.EventArgs) Handles BtnErrorLoggingDetails.Click, ALVErrors.DoubleClick
            If ALVErrors.SelectedItems.Count < 1 Then Exit Sub
            Dim ed As New ErrorDiagnose(serverOutputHandler.ParseMessageType(ALVErrors.SelectedItems(0).SubItems(1).Text), ALVErrors.SelectedItems(0).SubItems(2).Text, ErrorAnalyzer.GetCause(ALVErrors.SelectedItems(0).SubItems(3).Text))
            ed.ShowDialog()
        End Sub


        Private Sub BtnErrorLoggingCopy_Click(sender As System.Object, e As System.EventArgs) Handles BtnErrorLoggingCopy.Click
            If ALVErrors.SelectedItems.Count < 1 Then Exit Sub
            Dim clipboardtext As String = ""
            For Each item As ListViewItem In ALVErrors.SelectedItems
                clipboardtext += item.SubItems(2).Text & " [" & item.SubItems(1).Text & "] " & item.SubItems(3).Text & vbCrLf
            Next
            My.Computer.Clipboard.SetText(clipboardtext)
        End Sub
#End Region

#Region "Server Settings Editor"
        Private Sub Server_settings_refresh_UI()
            Try
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf Server_settings_refresh_UI)
                    Me.Invoke(d, New Object())
                Else
                    ServerSettings.LoadSettings()

                    ALVServerSettings.Items.Clear()
                    If ServerSettings.settings IsNot Nothing Then
                        For Each setting In ServerSettings.settings
                            ALVServerSettings.Items.Add(New ListViewItem({setting.Key, setting.Value}))
                        Next
                    End If

                    ServerSettings.LoadLists()

                    ALVServerSettingsWhiteList.Items.Clear()
                    If ServerSettings.whitelist IsNot Nothing Then
                        For Each p As String In ServerSettings.whitelist
                            If p.StartsWith("#") = False And p <> "" Then ALVServerSettingsWhiteList.Items.Add(New ListViewItem({p}))
                        Next
                    End If

                    ALVServerSettingsOPs.Items.Clear()
                    If ServerSettings.ops IsNot Nothing Then
                        For Each p As String In ServerSettings.ops
                            If p.StartsWith("#") = False And p <> "" Then ALVServerSettingsOPs.Items.Add(New ListViewItem({p}))
                        Next
                    End If

                    ALVServerSettingsBannedPlayer.Items.Clear()
                    If ServerSettings.Playerbans IsNot Nothing Then
                        For Each p As String In ServerSettings.Playerbans
                            If p.StartsWith("#") = False And p <> "" Then ALVServerSettingsBannedPlayer.Items.Add(New ListViewItem({p}))
                        Next
                    End If

                    ALVServerSettingsBannedIP.Items.Clear()
                    If ServerSettings.IPBans IsNot Nothing Then
                        For Each p As String In ServerSettings.IPBans
                            If p.StartsWith("#") = False And p <> "" Then ALVServerSettingsBannedIP.Items.Add(New ListViewItem({p}))
                        Next
                    End If
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in Server_Settings_Refresh_UI!", ex.Message)
            End Try
        End Sub

        Private Sub BtnServerSettingsAddWhitelist_Click() Handles BtnServerSettingsAddWhitelist.Click, TxtServerSettingsAddWhitelist.KeyPressEnter
            ServerSettings.AddWhitelist(TxtServerSettingsAddWhitelist.Text)
            TxtServerSettingsAddWhitelist.Text = ""
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnServerSettingsAddOP_Click() Handles BtnServerSettingsAddOP.Click, TxtServerSettingsAddOP.KeyPressEnter
            ServerSettings.AddOp(TxtServerSettingsAddOP.Text)
            TxtServerSettingsAddOP.Text = ""
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnServerSettingsAddPlayerBan_Click() Handles BtnServerSettingsAddPlayerBan.Click, TxtServerSettingsAddPlayerBan.KeyPressEnter
            ServerSettings.AddPlayerBan(TxtServerSettingsAddPlayerBan.Text)
            TxtServerSettingsAddPlayerBan.Text = ""
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnServerSettingsAddIPBan_Click() Handles BtnServerSettingsAddIPBan.Click, TxtServerSettingsAddIPBan.KeyPressEnter
            ServerSettings.AddIpBan(TxtServerSettingsAddIPBan.Text)
            TxtServerSettingsAddIPBan.Text = ""
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnCMenuServerListsRemove_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuServerListsRemove.Click
            Dim lv As Net.Bertware.Controls.AdvancedListView = CType(CmenuServerLists.SourceControl, Net.Bertware.Controls.AdvancedListView)
            If lv.SelectedItems Is Nothing OrElse lv.SelectedItems.Count < 1 Then Exit Sub

            If lv.Equals(ALVServerSettingsWhiteList) Then
                ServerSettings.RemoveWhitelist(lv.SelectedItems(0).SubItems(0).Text)
            ElseIf lv.Equals(ALVServerSettingsOPs) Then
                ServerSettings.RemoveOp(lv.SelectedItems(0).SubItems(0).Text)
            ElseIf lv.Equals(ALVServerSettingsBannedPlayer) Then
                ServerSettings.RemovePlayerBan(lv.SelectedItems(0).SubItems(0).Text)
            ElseIf lv.Equals(ALVServerSettingsBannedIP) Then
                ServerSettings.RemoveIpBan(lv.SelectedItems(0).SubItems(0).Text)
            End If
            Thread.Sleep(100) 'So files can be changed if server is running
            Server_settings_refresh_UI()
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnCmenuServerSettingsAdd_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuServerSettingsAdd.Click
            Dim sd As New ServerSettingDialog
            sd.NameReadOnly = False
            If sd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
            ServerSettings.AddSetting(sd.setting, sd.value)
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnCmenuServerSettingsEdit_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuServerSettingsEdit.Click, ALVServerSettings.DoubleClick
            If ALVServerSettings.SelectedItems Is Nothing OrElse ALVServerSettings.SelectedItems.Count < 1 Then Exit Sub
            Dim sd As New ServerSettingDialog
            sd.setting = ALVServerSettings.SelectedItems(0).SubItems(0).Text
            sd.value = ALVServerSettings.SelectedItems(0).SubItems(1).Text
            If sd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
            ServerSettings.SetSetting(sd.setting, sd.value)
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnCmenuServerSettingsRemove_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuServerSettingsRemove.Click
            If ALVServerSettings.SelectedItems Is Nothing OrElse ALVServerSettings.SelectedItems.Count < 1 Then Exit Sub
            ServerSettings.RemoveSetting(ALVServerSettings.SelectedItems(0).SubItems(0).Text)
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnCmenuServerListRefresh_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuServerListRefresh.Click
            ServerSettings.LoadLists()
            Server_settings_refresh_UI()
        End Sub

        Private Sub BtnCmenuServerSettingsRefresh_Click(sender As System.Object, e As System.EventArgs) Handles BtnCmenuServerSettingsRefresh.Click
            ServerSettings.LoadSettings()
            Server_settings_refresh_UI()
        End Sub

#End Region

#Region "Backup"
        Public Sub BackupManager_UpdateUI()
            If BackupManager.Loaded = False Then Exit Sub
            Try
                If Me.InvokeRequired Then
                    Dim d As New ContextCallback(AddressOf BackupManager_UpdateUI)
                    Me.Invoke(d, New Object())
                Else
                    ALVBackups.Items.Clear()
                    For Each bs As BackupSetting In BackupManager.backups
                        ALVBackups.Items.Add(New ListViewItem({bs.name, common.serialize(bs.folders, ","), bs.destination, bs.compression.ToString}))
                    Next
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "mainform", "Severe exception in Backupmanager_UpdateUI! " & ex.Message, "mainform")
            End Try
        End Sub

        Private Sub BtnBackupAdd_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackupAdd.Click
            Dim bd As New BackupDialog()
            If bd.ShowDialog() = Windows.Forms.DialogResult.OK Then BackupManager.addBackup(bd.backup)
        End Sub

        Private Sub BtnBackupEdit_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackupEdit.Click
            If ALVBackups.SelectedItems Is Nothing OrElse ALVBackups.SelectedItems.Count < 1 OrElse ALVBackups.SelectedItems(0) Is Nothing Then Exit Sub
            Dim bs As BackupSetting = BackupManager.GetBackupByName(ALVBackups.SelectedItems(0).SubItems(0).Text)
            Dim bd As New BackupDialog(bs)
            If bd.ShowDialog() = Windows.Forms.DialogResult.OK Then BackupManager.saveBackup(bs, bd.backup)
        End Sub

        Private Sub BtnBackupRemove_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackupRemove.Click
            If ALVBackups.SelectedItems Is Nothing OrElse ALVBackups.SelectedItems.Count < 1 OrElse ALVBackups.SelectedItems(0) Is Nothing Then Exit Sub
            Dim bs As BackupSetting = BackupManager.GetBackupByName(ALVBackups.SelectedItems(0).SubItems(0).Text)
            BackupManager.deleteBackup(bs)
        End Sub

        Private Sub BtnBackupExecute_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackupExecute.Click
            If ALVBackups.SelectedItems Is Nothing OrElse ALVBackups.SelectedItems.Count < 1 OrElse ALVBackups.SelectedItems(0) Is Nothing Then Exit Sub
            Dim bs As BackupSetting = BackupManager.GetBackupByName(ALVBackups.SelectedItems(0).SubItems(0).Text)
            bs.execute(True)
        End Sub

        Private Sub BtnBackupImport_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackupImport.Click
            BackupManager.import()
        End Sub

        Private Sub BtnBackupExport_Click(sender As System.Object, e As System.EventArgs) Handles BtnBackupExport.Click
            If ALVBackups.SelectedItems Is Nothing OrElse ALVBackups.SelectedItems.Count < 1 OrElse ALVBackups.SelectedItems(0) Is Nothing Then Exit Sub

            If ALVBackups.SelectedItems.Count = 1 Then
                BackupManager.export(ALVBackups.SelectedItems(0).SubItems(0).Text)
            Else
                Dim l As New List(Of String)
                For Each item As ListViewItem In ALVBackups.SelectedItems
                    l.Add(item.SubItems(0).Text)
                Next
                BackupManager.export(l)
            End If
        End Sub
#End Region

        Private Sub PBPaypalDonate_Click(sender As System.Object, e As System.EventArgs) Handles PBPaypalDonate.Click, LinkDonate.Click
            Dim p As New Process
            p.StartInfo.FileName = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=R8XCX7YASJPP2"
            p.Start()
        End Sub

        Private Sub PBFlattr_Click(sender As System.Object, e As System.EventArgs) Handles PBFlattr.Click
            Dim p As New Process
            p.StartInfo.FileName = "http://flattr.com/thing/728461/The-Bukkit-GUI-project"
            p.Start()
        End Sub

        Private Sub BtnFeedback_Click(sender As System.Object, e As System.EventArgs) Handles BtnFeedback.Click
            Dim fbd As New FeedBackDialog
            fbd.ShowDialog()
        End Sub


        Private Sub LblInfoComputerLocIP_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblInfoComputerLocIP.LinkClicked
            Try
                My.Computer.Clipboard.SetText(LblInfoComputerLocIP.Text.Split(":")(1).Trim)
            Catch ex As Exception
                MessageBox.Show(lr("Couldn't copy your IP!"), lr("Failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub LblInfoComputerExtIP_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblInfoComputerExtIP.LinkClicked
            Try
                My.Computer.Clipboard.SetText(LblInfoComputerExtIP.Text.Split(":")(1).Trim)
            Catch ex As Exception
                MessageBox.Show(lr("Couldn't copy your IP!"), lr("Failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub BtnSuperStartPortForwarding_Click(sender As Object, e As EventArgs) Handles BtnSuperStartPortForwarding.Click
            Dim pf As New PortForwarder
            pf.Show()
        End Sub
    End Class

End Namespace

