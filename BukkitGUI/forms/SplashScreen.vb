'the splash screen loads the application, it initializes all modules.
Imports System.Threading
Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities

Public Class SplashScreen
    Dim ThdLoad As Thread

    'Temporary workaround to catch infinite loading loop.
    Dim InitCount As Integer = 0

    Private Sub loaded() Handles Me.Load
        'Set splashscreen texts
        Me.lbltitle.Text = My.Application.Info.ProductName
        Me.lblversion.Text = "Version " & My.Application.Info.Version.ToString
        Me.lblauthors.Text = "Author(s): Bertware"
        Me.lblcopyright.Text = My.Application.Info.Copyright
        Me.lblweb.Text = "Web: http://dev.bukkit.org/projects/bukkitGUI"

        'Start initialization on separate thread
        ThdLoad = New Thread(AddressOf init)
        ThdLoad.IsBackground = True
        ThdLoad.Name = "Splashscreen_loader"
        ThdLoad.SetApartmentState(ApartmentState.STA)
        ThdLoad.Start()
    End Sub

    Public Sub init()
        'Initialize all modules
        'In meanwhile, display progress to splashscreen
        'This routine runs on another thread, INVOKES REQUIRED WHEN UPDATING THE UI

        'NOTE: The order of initialization is important. Changing the order might lead to errors or unknown/unexpected behaviour


        '
        'Check .NET version. Program start will be aborted if needed version is not present.
        '
        Try
            'If Not IsRunningOnMono Then Net.Bertware.Utilities.NetVersionDetector.NetFrameworkVersionDetector.CanApplicationRun(Bertware.Utilities.NetVersionDetector.NetFrameworkVersion.MajorFrameworkVersion.v35, True, True, True)
        Catch ex As Exception
            Trace.WriteLine(".NET check error:" & ex.Message)
        End Try
        '======================= START OF INITIALIZATION ======================='
        Dim Inet As Boolean = My.Computer.Network.IsAvailable

        setload("Initializing file location", 0) 'set UI (text and progress %)

        filelocation.init() _
        'initialize filelocation module, which determines wether files should be stored in appdata or local

        If filelocation.location = filelocation.filelocation.global_files Then _
            'NO TRANSLATIONS! TRANSLATIONS AREN'T INITIALIZED YET
            If Process.GetProcessesByName("BukkitGUI").Length > 1 Then
                MessageBox.Show(
                    "You cannot run multiple instances of the GUI while the files are saved in appdata. Change in ""Options and Settings"" and try again.",
                    "Multiple GUI instances detected", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Process.GetCurrentProcess.Kill()
            End If
        End If

        InitCount = InitCount + 1 'maximum 1
        If InitCount > 5 Then Me.CloseForm() : Exit Sub

        Try
            setload("Initializing livebug", 10) 'set UI (text and progress %)
            livebug.init() _
            'initialize livebug module, needed for the GUI log and to report errors. must be first to initiate.
        Catch ex As Exception
            Trace.WriteLine("Livebug init error:" & ex.Message)
        End Try

        InitCount = InitCount + 1 'maximum 2
        If InitCount > 5 Then Me.CloseForm() : Exit Sub

        'Don't log before livebug is initialized
        livebug.write(loggingLevel.Fine, "Splashscreen", "Initializing application") 'log to livebug
        If Not Inet Then _
            livebug.write(loggingLevel.Warning, "Splashscreen",
                          "No network connection available. Skipping initialization for network dependent items.")

        InitCount = InitCount + 1 'maximum 3
        If InitCount > 5 Then Me.CloseForm() : Exit Sub

        setload("Initializing common items", 20) 'set UI (text and progress %)
        common.Init() _
        'Initialize the module with common functions. This will also check if all folders are available, or createm if they don't exist.

        InitCount = InitCount + 1 'maximum 4
        If InitCount > 5 Then Me.CloseForm() : Exit Sub

        setload("Initializing config", 40) 'set UI (text and progress %)
        Try
            config.init() _
            'initialize the config module, needed to read settings from config.XML, Must be initiated after livebug, so other modules can initiate properly.
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: Config")
        End Try

        InitCount = InitCount + 1 'maximum 5
        If InitCount > 6 Then Me.CloseForm() : Exit Sub

        common.isRunningLight = config.readAsBool("LightMode", False)

        setload("running update check...", 30)
        Try
            If Inet And config.readAsBool("auto_update", True, "options") Then _
                Thread.Sleep(100) : Net.Bertware.Get.api.RunUpdateCheck(True, False) : Thread.Sleep(100) _
            'Make sure the requests are spread so the server doesn't block
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: Updater", ex.Message)
        End Try

        InitCount = InitCount + 1 'maximum 6
        If InitCount > 10 Then Me.CloseForm() : Exit Sub

        setload("Initializing localization", 50) 'set UI (text and progress %)
        Try
            If Not common.isRunningLight Then language.Init() _
            'Initialize the localization module, this allows translations of the GUI.
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: Language")
        End Try

        InitCount = InitCount + 1 'maximum 7
        If InitCount > 10 Then Me.CloseForm() : Exit Sub
        '========================================================================='
        'From here, everything can run. All needed modules are loaded.
        '--------------------
        '

        'Accept licenses:
        'setload("Initializing licenses", 45)
        'Try
        ' LicenseManager.init() 'LICENSE MANAGER DISABLED RIGHT NOW
        ' Catch ex As Exception
        ' End Try

        'some small async tasks can start here too
        Try
            If Inet Then BukkitTools.FetchLatestVersionsAsync()
            If Not common.isRunningLight Then SoundNotificator.init()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: SoundNotificator")
        End Try

        InitCount = InitCount + 1 'maximum 8
        If InitCount > 10 Then Me.CloseForm() : Exit Sub
        '
        '--------------------

        setload("Initializing performance", 60) 'set UI (text and progress %)
        Try
            If common.IsRunningOnMono = False AndAlso Not common.isRunningLight Then performance.Init() _
            'Initialize the performance module, this allows CPU and RAM measurement.
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: Performance")
        End Try

        InitCount = InitCount + 1 'maximum 9
        If InitCount > 10 Then Me.CloseForm() : Exit Sub

        setload("Initializing output handler", 70)
        Try
            serverOutputHandler.init()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: ServerOutputHandler")
        End Try

        InitCount = InitCount + 1 'maximum 10
        If InitCount > 11 Then Me.CloseForm() : Exit Sub

        setload("Initializing plugin manager", 75)
        Try
            If Not common.isRunningLight Then InstalledPluginManager.InitAsync()
            If Not common.isRunningLight And Inet Then LoadMostPopularPluginsAsync()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: Pluginmanager")
        End Try

        InitCount = InitCount + 1 'maximum 11
        If InitCount > 15 Then Me.CloseForm() : Exit Sub

        setload("Initializing server settings...", 85)
        Try
            If Not common.isRunningLight Then ServerSettings.init()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: ServerSettings")
        End Try

        InitCount = InitCount + 1 'maximum 12
        If InitCount > 15 Then Me.CloseForm() : Exit Sub

        setload("Initializing error manager...", 85)
        Try
            If Not common.isRunningLight Then ErrorManager.init()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: ErrorManager")
        End Try

        InitCount = InitCount + 1 'maximum 13
        If InitCount > 15 Then Me.CloseForm() : Exit Sub

        setload("Initializing task manager...", 90)
        Try
            If Not common.isRunningLight Then TaskManager.init()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: TaskManager")
        End Try

        InitCount = InitCount + 1 'maximum 14
        If InitCount > 15 Then Me.CloseForm() : Exit Sub

        setload("Initializing backup manager...", 90)
        Try
            If Not common.isRunningLight Then BackupManager.init()
        Catch ex As Exception
            livebug.write(loggingLevel.Severe, "Splashscreen", "Failed to initialize module: BackupManager")
        End Try

        InitCount = InitCount + 1 'maximum 15
        If InitCount > 16 Then Me.CloseForm() : Exit Sub

        setload("Done...", 100)

        '======================= END OF INITIALIZATION ======================='

        CloseForm()
        livebug.write(loggingLevel.Fine, "Splashscreen", "Application initialized, Closing splashscreen") _
        'log to livebug

        If IsRunningOnMono Then
            mainform.Show()
        End If
    End Sub


#Region "Private Methods"

    Private Sub setload(text As String, progress As Byte) 'set both description and percent.
        livebug.write(loggingLevel.Fine, "Splashscreen", "Loading:" & text & "(" & progress & "%)")
        SetLoadAction(text)
        SetLoadPercent(progress)
    End Sub

    Private Sub SetLoadAction(text As String) 'set the description for the currently executed action.
        If Me.Created = False OrElse Me.IsHandleCreated = False Then Exit Sub
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf SetLoadAction)
            Me.Invoke(d, New Object() {text})
        Else
            If text.EndsWith("...") = False Then text = text & "..."
            Me.lblload.Text = text
        End If
    End Sub

    Private Sub SetLoadPercent(progress As Byte) 'set the percent label and the progressbar
        If Me.Created = False OrElse Me.IsHandleCreated = False Then Exit Sub
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf SetLoadPercent)
            Me.Invoke(d, New Object() {progress})
        Else
            Me.lblloadpercent.Text = progress & "%"
            PBLoad.Value = progress
        End If
    End Sub

    Private Sub CloseForm() 'Close the form in a thread safe way
        If Me.Created = False OrElse Me.IsHandleCreated = False Then Exit Sub
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf CloseForm)
            Me.Invoke(d, New Object())
        Else
            mainform.Show()
            Me.Close()
        End If
    End Sub

#End Region
End Class