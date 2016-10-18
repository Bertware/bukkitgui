Namespace Core

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Mainform
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mainform))
            Me.GBGeneralGeneral = New System.Windows.Forms.GroupBox()
            Me.SplitGeneral = New System.Windows.Forms.SplitContainer()
            Me.BtnGeneralClearOutput = New System.Windows.Forms.Button()
            Me.ChkGeneralSay = New System.Windows.Forms.CheckBox()
            Me.ALVGeneralPlayers = New Net.Bertware.Controls.AdvancedListView()
            Me.GeneralColName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CmenuPlayerList = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.BtnCMenuPlayerListOp = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListDeop = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListKick = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListBan = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListGameMode = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListGamemodeSurvival = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListGamemodeCreative = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListGamemodeAdventure = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuPlayerListGive = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCmenuPlayerListRefresh = New System.Windows.Forms.ToolStripMenuItem()
            Me.ImgListPlayerAvatars = New System.Windows.Forms.ImageList(Me.components)
            Me.BtnBrowseOutput = New System.Windows.Forms.Button()
            Me.TxtGeneralServerIn = New System.Windows.Forms.TextBox()
            Me.ARTXTServerOutput = New Net.Bertware.Controls.AdvancedRichTextBox(Me.components)
            Me.BtnGeneralSendCmd = New System.Windows.Forms.Button()
            Me.TabCtrlMain = New System.Windows.Forms.TabControl()
            Me.TabGeneral = New System.Windows.Forms.TabPage()
            Me.GbGeneralInfo = New System.Windows.Forms.GroupBox()
            Me.LblGeneralTimeSinceStartValue = New System.Windows.Forms.Label()
            Me.PanelPerformanceInfo = New System.Windows.Forms.Panel()
            Me.lblGeneralCPUGUI = New System.Windows.Forms.Label()
            Me.lblGeneralRAMServerValue = New System.Windows.Forms.Label()
            Me.PBGeneralCPUGUI = New Net.Bertware.Controls.VistaProgressBar()
            Me.lblGeneralRAMGUIValue = New System.Windows.Forms.Label()
            Me.PBGeneralCPUServer = New Net.Bertware.Controls.VistaProgressBar()
            Me.lblGeneralRAMTotalValue = New System.Windows.Forms.Label()
            Me.PBGeneralCPUTotal = New Net.Bertware.Controls.VistaProgressBar()
            Me.lblGeneralCPUServer = New System.Windows.Forms.Label()
            Me.lblGeneralRAMTotal = New System.Windows.Forms.Label()
            Me.lblGeneralCPUTotal = New System.Windows.Forms.Label()
            Me.lblGeneralRAMServer = New System.Windows.Forms.Label()
            Me.lblGeneralCPUTotalValue = New System.Windows.Forms.Label()
            Me.lblGeneralRAMGUI = New System.Windows.Forms.Label()
            Me.PBGeneralRAMTotal = New Net.Bertware.Controls.VistaProgressBar()
            Me.lblGeneralCPUGUIValue = New System.Windows.Forms.Label()
            Me.PBGeneralRAMServer = New Net.Bertware.Controls.VistaProgressBar()
            Me.lblGeneralCPUServerValue = New System.Windows.Forms.Label()
            Me.PBGeneralRAMGUI = New Net.Bertware.Controls.VistaProgressBar()
            Me.lblGeneralTimeSinceStartText = New System.Windows.Forms.Label()
            Me.BtnGeneralKill = New System.Windows.Forms.Button()
            Me.BtnGeneralReload = New System.Windows.Forms.Button()
            Me.BtnGeneralRestart = New System.Windows.Forms.Button()
            Me.BtnGeneralStartStop = New System.Windows.Forms.Button()
            Me.TabPlayers = New System.Windows.Forms.TabPage()
            Me.LblPlayersViewMode = New System.Windows.Forms.Label()
            Me.ALVPlayersPlayers = New Net.Bertware.Controls.AdvancedListView()
            Me.ColPlayersPlayersName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColPlayersPlayersIP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColPlayersPlayersLocation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColPlayersPlayersTJoined = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColPlayersPlayersWhitelist = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColPlayersPlayersOP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.TBPlayersPlayersView = New System.Windows.Forms.TrackBar()
            Me.TabSuperStart = New System.Windows.Forms.TabPage()
            Me.BtnSuperStartPortForwarding = New System.Windows.Forms.Button()
            Me.BtnSuperStartLaunch = New System.Windows.Forms.Button()
            Me.GBSuperStartMaintainance = New System.Windows.Forms.GroupBox()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.NumSuperstartCustomBuild = New System.Windows.Forms.NumericUpDown()
            Me.ChkSuperstartAutoUpdate = New System.Windows.Forms.CheckBox()
            Me.BtnSuperStartGetCurrent = New System.Windows.Forms.Button()
            Me.ChkSuperStartRetrieveCurrent = New System.Windows.Forms.CheckBox()
            Me.ChkSuperstartAutoUpdateNotify = New System.Windows.Forms.CheckBox()
            Me.llblSuperStartsite = New Net.Bertware.Controls.AdvancedLinkLabel()
            Me.lblSuperStartLatestDev = New System.Windows.Forms.Label()
            Me.lblSuperStartLatestBeta = New System.Windows.Forms.Label()
            Me.lblSuperStartLatestStable = New System.Windows.Forms.Label()
            Me.BtnSuperStartDownloadCustomBuild = New System.Windows.Forms.Button()
            Me.BtnSuperStartDownloadDev = New System.Windows.Forms.Button()
            Me.BtnSuperStartDownloadBeta = New System.Windows.Forms.Button()
            Me.BtnSuperStartDownloadRecommended = New System.Windows.Forms.Button()
            Me.PBSuperStartServerIcon = New System.Windows.Forms.PictureBox()
            Me.GBSuperStartRemoteServer = New System.Windows.Forms.GroupBox()
            Me.MTxtSuperstartRemotePassword = New System.Windows.Forms.MaskedTextBox()
            Me.MTxtSuperstartRemoteSalt = New System.Windows.Forms.MaskedTextBox()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.NumSuperstartRemotePort = New System.Windows.Forms.NumericUpDown()
            Me.TxtSuperstartRemoteUsername = New System.Windows.Forms.TextBox()
            Me.TxtSuperstartRemoteHost = New System.Windows.Forms.TextBox()
            Me.GBSuperstartJavaServer = New System.Windows.Forms.GroupBox()
            Me.TxtSuperstartJavaCustomSwitch = New System.Windows.Forms.TextBox()
            Me.LblSuperStartCustomSwitches = New System.Windows.Forms.Label()
            Me.BtnSuperstartJavaJarFileBrowse = New System.Windows.Forms.Button()
            Me.TxtSuperstartJavaJarFile = New System.Windows.Forms.TextBox()
            Me.TBSuperstartJavaMaxRam = New System.Windows.Forms.TrackBar()
            Me.TBSuperstartJavaMinRam = New System.Windows.Forms.TrackBar()
            Me.TxtSuperstartJavaCustomArgs = New System.Windows.Forms.TextBox()
            Me.lblSuperStartCustomArg = New System.Windows.Forms.Label()
            Me.lblSuperStartJarFile = New System.Windows.Forms.Label()
            Me.lblSuperStartMaxRam = New System.Windows.Forms.Label()
            Me.LblSuperStartMinRam = New System.Windows.Forms.Label()
            Me.lblSuperStartJavaVersion = New System.Windows.Forms.Label()
            Me.CBSuperstartJavaJRE = New System.Windows.Forms.ComboBox()
            Me.CBSuperstartServerType = New System.Windows.Forms.ComboBox()
            Me.lblSuperStartType = New System.Windows.Forms.Label()
            Me.TabErrorLogging = New System.Windows.Forms.TabPage()
            Me.BtnErrorLoggingCopy = New System.Windows.Forms.Button()
            Me.ChkErrorsHideStackTrace = New System.Windows.Forms.CheckBox()
            Me.ChkErrorsHideError = New System.Windows.Forms.CheckBox()
            Me.ChkErrorsHideWarning = New System.Windows.Forms.CheckBox()
            Me.BtnErrorLoggingDetails = New System.Windows.Forms.Button()
            Me.ALVErrors = New Net.Bertware.Controls.AdvancedListView()
            Me.ColErrorID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColErrorType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColErrorTime = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColErrorMsg = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ImgListErrorManager = New System.Windows.Forms.ImageList(Me.components)
            Me.TabTaskManager = New System.Windows.Forms.TabPage()
            Me.BtnTaskManagerTest = New System.Windows.Forms.Button()
            Me.BtnTaskManagerImport = New System.Windows.Forms.Button()
            Me.BtnTaskManagerExport = New System.Windows.Forms.Button()
            Me.BtnTaskManagerAdd = New System.Windows.Forms.Button()
            Me.BtnTaskManagerEdit = New System.Windows.Forms.Button()
            Me.ALVTaskPlanner = New Net.Bertware.Controls.AdvancedListView()
            Me.ALVTaskPlannerColName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVTaskPlannerColTriggerType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVTaskPlannerColTriggerParam = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVTaskPlannerColActionType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVTaskPlannerColActionParam = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVTaskPlannerColisEnabled = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.BtnTaskManagerRemove = New System.Windows.Forms.Button()
            Me.TabPlugins = New System.Windows.Forms.TabPage()
            Me.TabCtrlPlugins = New System.Windows.Forms.TabControl()
            Me.TabPluginsInstall = New System.Windows.Forms.TabPage()
            Me.LblInstallPluginsLoading = New System.Windows.Forms.Label()
            Me.BtnInstallPluginsSearch = New System.Windows.Forms.Button()
            Me.LblInstallPluginsInfo = New System.Windows.Forms.Label()
            Me.lblInstallPluginsCategory = New System.Windows.Forms.Label()
            Me.LblInstallPluginsFilter = New System.Windows.Forms.Label()
            Me.CBInstallPluginsCategory = New System.Windows.Forms.ComboBox()
            Me.TxtInstallPluginsFilter = New System.Windows.Forms.TextBox()
            Me.ALVBukGetPlugins = New Net.Bertware.Controls.AdvancedListView()
            Me.ColBukgetPluginName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColBukgetPluginDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColBukgetPluginVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColBukgetPluginBukkitVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CMenuBukGetPlugins = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.btnCMenuBukGetPluginsMoreInfo = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuBukGetPluginsInstallPlugin = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuBukGetPluginsProjectPage = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCMenuBukGetPluginsRefresh = New System.Windows.Forms.ToolStripMenuItem()
            Me.TabPluginsInstalled = New System.Windows.Forms.TabPage()
            Me.lblinstalledpluginsInfo = New System.Windows.Forms.Label()
            Me.ALVInstalledPlugins = New Net.Bertware.Controls.AdvancedListView()
            Me.ColName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColAuthor = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColUpdateDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CMenuInstalledPlugins = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.CmenuInstalledPluginsMoreInfo = New System.Windows.Forms.ToolStripMenuItem()
            Me.CmenuInstalledPluginsViewVersions = New System.Windows.Forms.ToolStripMenuItem()
            Me.CmenuInstalledPluginsUpdate = New System.Windows.Forms.ToolStripMenuItem()
            Me.CmenuInstalledPluginsProjectPage = New System.Windows.Forms.ToolStripMenuItem()
            Me.CmenuInstalledPluginsRemove = New System.Windows.Forms.ToolStripMenuItem()
            Me.CmenuInstalledPluginsRefresh = New System.Windows.Forms.ToolStripMenuItem()
            Me.CmenuInstalledPluginsOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
            Me.ImgListTabIcons = New System.Windows.Forms.ImageList(Me.components)
            Me.TabServerOptions = New System.Windows.Forms.TabPage()
            Me.TxtServerSettingsAddIPBan = New Net.Bertware.Controls.AdvancedTextBox()
            Me.TxtServerSettingsAddPlayerBan = New Net.Bertware.Controls.AdvancedTextBox()
            Me.TxtServerSettingsAddOP = New Net.Bertware.Controls.AdvancedTextBox()
            Me.TxtServerSettingsAddWhitelist = New Net.Bertware.Controls.AdvancedTextBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.BtnServerSettingsAddIPBan = New System.Windows.Forms.Button()
            Me.BtnServerSettingsAddPlayerBan = New System.Windows.Forms.Button()
            Me.BtnServerSettingsAddOP = New System.Windows.Forms.Button()
            Me.BtnServerSettingsAddWhitelist = New System.Windows.Forms.Button()
            Me.ALVServerSettingsBannedIP = New Net.Bertware.Controls.AdvancedListView()
            Me.ColServerSettingsBannedIp = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CmenuServerLists = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.BtnCmenuServerListsRemove = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCmenuServerListRefresh = New System.Windows.Forms.ToolStripMenuItem()
            Me.ALVServerSettingsBannedPlayer = New Net.Bertware.Controls.AdvancedListView()
            Me.ColServerSettingsBannedPlayers = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVServerSettingsOPs = New Net.Bertware.Controls.AdvancedListView()
            Me.ColServerSettingsOps = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVServerSettingsWhiteList = New Net.Bertware.Controls.AdvancedListView()
            Me.ColServerSettingsWhitelist = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ALVServerSettings = New Net.Bertware.Controls.AdvancedListView()
            Me.ColServerSettingsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColServerSettingsValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.CmenuServerSettings = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.BtnCmenuServerSettingsAdd = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCmenuServerSettingsEdit = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCmenuServerSettingsRemove = New System.Windows.Forms.ToolStripMenuItem()
            Me.BtnCmenuServerSettingsRefresh = New System.Windows.Forms.ToolStripMenuItem()
            Me.TabBackups = New System.Windows.Forms.TabPage()
            Me.BtnBackupExecute = New System.Windows.Forms.Button()
            Me.BtnBackupImport = New System.Windows.Forms.Button()
            Me.BtnBackupExport = New System.Windows.Forms.Button()
            Me.BtnBackupAdd = New System.Windows.Forms.Button()
            Me.BtnBackupEdit = New System.Windows.Forms.Button()
            Me.ALVBackups = New Net.Bertware.Controls.AdvancedListView()
            Me.ColBackupName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColBackupFolders = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColBackupDestination = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.ColBackupCompression = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
            Me.BtnBackupRemove = New System.Windows.Forms.Button()
            Me.TabOptionsInfo = New System.Windows.Forms.TabPage()
            Me.LblDonateInfo = New System.Windows.Forms.Label()
            Me.BtnFeedback = New System.Windows.Forms.Button()
            Me.GBInfoSettings = New System.Windows.Forms.GroupBox()
            Me.ChkOptionsLightMode = New System.Windows.Forms.CheckBox()
            Me.ChkAutoStartWindows = New System.Windows.Forms.CheckBox()
            Me.BtnOptionsResetAll = New System.Windows.Forms.Button()
            Me.ChkOptionsCheckUpdates = New System.Windows.Forms.CheckBox()
            Me.LbloptionsRestartRequiredInfo = New System.Windows.Forms.Label()
            Me.lblSettingsTabPages = New System.Windows.Forms.Label()
            Me.ChkOptionsTabServeroptions = New System.Windows.Forms.CheckBox()
            Me.ChkOptionsTabPlugins = New System.Windows.Forms.CheckBox()
            Me.ChkOptionsTabTaskManager = New System.Windows.Forms.CheckBox()
            Me.ChkOptionsTaberrors = New System.Windows.Forms.CheckBox()
            Me.ChkOptionsTabplayers = New System.Windows.Forms.CheckBox()
            Me.lblOptionsConfigLocation = New System.Windows.Forms.Label()
            Me.CBInfoSettingsFileLocation = New System.Windows.Forms.ComboBox()
            Me.ChkRunServerOnGUIStart = New System.Windows.Forms.CheckBox()
            Me.lblInfoSettingsLanguage = New System.Windows.Forms.Label()
            Me.CBInfoSettingsLanguage = New System.Windows.Forms.ComboBox()
            Me.GBOptionsInfoSound = New System.Windows.Forms.GroupBox()
            Me.ChkInfoSoundOnSevere = New System.Windows.Forms.CheckBox()
            Me.ChkInfoSoundOnWarning = New System.Windows.Forms.CheckBox()
            Me.ChkInfoSoundOnLeave = New System.Windows.Forms.CheckBox()
            Me.ChkInfoSoundOnJoin = New System.Windows.Forms.CheckBox()
            Me.GBOptionsInfoAboutTray = New System.Windows.Forms.GroupBox()
            Me.ChkInfoTrayOnWarning = New System.Windows.Forms.CheckBox()
            Me.ChkInfoTrayOnLeave = New System.Windows.Forms.CheckBox()
            Me.ChkInfoTrayOnJoin = New System.Windows.Forms.CheckBox()
            Me.ChkInfoTrayOnSevere = New System.Windows.Forms.CheckBox()
            Me.ChkInfoTrayAlways = New System.Windows.Forms.CheckBox()
            Me.ChkInfoTrayMinimize = New System.Windows.Forms.CheckBox()
            Me.GBOptionsInfoAboutText = New System.Windows.Forms.GroupBox()
            Me.ChkTextUTF8 = New System.Windows.Forms.CheckBox()
            Me.CBTextOptionsFont = New FontCombo.FontComboBox()
            Me.TxtSettingsTextFontPreview = New System.Windows.Forms.TextBox()
            Me.LblTextSettingsFontSize = New System.Windows.Forms.Label()
            Me.LblTextSettingsFont = New System.Windows.Forms.Label()
            Me.NumTextOptionsFontSize = New System.Windows.Forms.NumericUpDown()
            Me.LblSettingsTextFontPreview = New System.Windows.Forms.Label()
            Me.TxtSettingsTextColorUnknown = New System.Windows.Forms.TextBox()
            Me.LblSettingsTextColorUnknown = New System.Windows.Forms.Label()
            Me.TxtSettingsTextColorSevere = New System.Windows.Forms.TextBox()
            Me.LblSettingsTextColorSevere = New System.Windows.Forms.Label()
            Me.TxtSettingsTextColorWarning = New System.Windows.Forms.TextBox()
            Me.LblSettingsTextColorWarning = New System.Windows.Forms.Label()
            Me.TxtSettingsTextColorPlayer = New System.Windows.Forms.TextBox()
            Me.LblSettingsTextColorPlayer = New System.Windows.Forms.Label()
            Me.TxtSettingsTextColorInfo = New System.Windows.Forms.TextBox()
            Me.LblSettingsTextColorInfo = New System.Windows.Forms.Label()
            Me.ChkShowDate = New System.Windows.Forms.CheckBox()
            Me.ChkShowTime = New System.Windows.Forms.CheckBox()
            Me.GBOptionsInfoAboutComputerInfo = New System.Windows.Forms.GroupBox()
            Me.LblInfoComputerExtIP = New System.Windows.Forms.LinkLabel()
            Me.LblInfoComputerLocIP = New System.Windows.Forms.LinkLabel()
            Me.LblInfoComputerOS = New System.Windows.Forms.Label()
            Me.LblInfoComputerCPU = New System.Windows.Forms.Label()
            Me.LblInfoComputerRAM = New System.Windows.Forms.Label()
            Me.LblInfoComputerComputerName = New System.Windows.Forms.Label()
            Me.GBOptionsInfoAbout = New System.Windows.Forms.GroupBox()
            Me.ALlblInfoAppWeb = New Net.Bertware.Controls.AdvancedLinkLabel()
            Me.BtnInfoAppUpdater = New System.Windows.Forms.Button()
            Me.lblInfoAppLatest = New System.Windows.Forms.Label()
            Me.lblInfoAppCopyright = New System.Windows.Forms.Label()
            Me.lblInfoAppVersion = New System.Windows.Forms.Label()
            Me.lblInfoAppAuthors = New System.Windows.Forms.Label()
            Me.lblInfoAppName = New System.Windows.Forms.Label()
            Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.lblStatusBarServerState = New System.Windows.Forms.Label()
            Me.LblStatusBarServerOutput = New System.Windows.Forms.Label()
            Me.ErrProv = New System.Windows.Forms.ErrorProvider(Me.components)
            Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
            Me.CmenuTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.BtnCmenuTrayExit = New System.Windows.Forms.ToolStripMenuItem()
            Me.lblInfo2 = New System.Windows.Forms.Label()
            Me.GBGeneralGeneral.SuspendLayout()
            Me.SplitGeneral.Panel1.SuspendLayout()
            Me.SplitGeneral.Panel2.SuspendLayout()
            Me.SplitGeneral.SuspendLayout()
            Me.CmenuPlayerList.SuspendLayout()
            Me.TabCtrlMain.SuspendLayout()
            Me.TabGeneral.SuspendLayout()
            Me.GbGeneralInfo.SuspendLayout()
            Me.PanelPerformanceInfo.SuspendLayout()
            Me.TabPlayers.SuspendLayout()
            CType(Me.TBPlayersPlayersView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabSuperStart.SuspendLayout()
            Me.GBSuperStartMaintainance.SuspendLayout()
            CType(Me.NumSuperstartCustomBuild, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PBSuperStartServerIcon, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GBSuperStartRemoteServer.SuspendLayout()
            CType(Me.NumSuperstartRemotePort, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GBSuperstartJavaServer.SuspendLayout()
            CType(Me.TBSuperstartJavaMaxRam, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.TBSuperstartJavaMinRam, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabErrorLogging.SuspendLayout()
            Me.TabTaskManager.SuspendLayout()
            Me.TabPlugins.SuspendLayout()
            Me.TabCtrlPlugins.SuspendLayout()
            Me.TabPluginsInstall.SuspendLayout()
            Me.CMenuBukGetPlugins.SuspendLayout()
            Me.TabPluginsInstalled.SuspendLayout()
            Me.CMenuInstalledPlugins.SuspendLayout()
            Me.TabServerOptions.SuspendLayout()
            Me.CmenuServerLists.SuspendLayout()
            Me.CmenuServerSettings.SuspendLayout()
            Me.TabBackups.SuspendLayout()
            Me.TabOptionsInfo.SuspendLayout()
            Me.GBInfoSettings.SuspendLayout()
            Me.GBOptionsInfoSound.SuspendLayout()
            Me.GBOptionsInfoAboutTray.SuspendLayout()
            Me.GBOptionsInfoAboutText.SuspendLayout()
            CType(Me.NumTextOptionsFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GBOptionsInfoAboutComputerInfo.SuspendLayout()
            Me.GBOptionsInfoAbout.SuspendLayout()
            CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.CmenuTray.SuspendLayout()
            Me.SuspendLayout()
            '
            'GBGeneralGeneral
            '
            Me.GBGeneralGeneral.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBGeneralGeneral.Controls.Add(Me.SplitGeneral)
            Me.GBGeneralGeneral.Location = New System.Drawing.Point(8, 6)
            Me.GBGeneralGeneral.Name = "GBGeneralGeneral"
            Me.GBGeneralGeneral.Size = New System.Drawing.Size(817, 349)
            Me.GBGeneralGeneral.TabIndex = 0
            Me.GBGeneralGeneral.TabStop = False
            Me.GBGeneralGeneral.Text = Lr("General")
            '
            'SplitGeneral
            '
            Me.SplitGeneral.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitGeneral.Location = New System.Drawing.Point(3, 16)
            Me.SplitGeneral.Name = "SplitGeneral"
            '
            'SplitGeneral.Panel1
            '
            Me.SplitGeneral.Panel1.Controls.Add(Me.BtnGeneralClearOutput)
            Me.SplitGeneral.Panel1.Controls.Add(Me.ChkGeneralSay)
            Me.SplitGeneral.Panel1.Controls.Add(Me.ALVGeneralPlayers)
            '
            'SplitGeneral.Panel2
            '
            Me.SplitGeneral.Panel2.Controls.Add(Me.BtnBrowseOutput)
            Me.SplitGeneral.Panel2.Controls.Add(Me.TxtGeneralServerIn)
            Me.SplitGeneral.Panel2.Controls.Add(Me.ARTXTServerOutput)
            Me.SplitGeneral.Panel2.Controls.Add(Me.BtnGeneralSendCmd)
            Me.SplitGeneral.Size = New System.Drawing.Size(811, 330)
            Me.SplitGeneral.SplitterDistance = 158
            Me.SplitGeneral.TabIndex = 8
            '
            'BtnGeneralClearOutput
            '
            Me.BtnGeneralClearOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnGeneralClearOutput.Location = New System.Drawing.Point(3, 300)
            Me.BtnGeneralClearOutput.Name = "BtnGeneralClearOutput"
            Me.BtnGeneralClearOutput.Size = New System.Drawing.Size(63, 23)
            Me.BtnGeneralClearOutput.TabIndex = 4
            Me.BtnGeneralClearOutput.Text = Lr("Clear")
            Me.MainToolTip.SetToolTip(Me.BtnGeneralClearOutput, "Clear the output of the log textbox")
            Me.BtnGeneralClearOutput.UseVisualStyleBackColor = True
            '
            'ChkGeneralSay
            '
            Me.ChkGeneralSay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ChkGeneralSay.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ChkGeneralSay.Location = New System.Drawing.Point(3, 306)
            Me.ChkGeneralSay.Name = "ChkGeneralSay"
            Me.ChkGeneralSay.Size = New System.Drawing.Size(152, 17)
            Me.ChkGeneralSay.TabIndex = 2
            Me.ChkGeneralSay.Text = Lr("Say (Ctrl+S)")
            Me.ChkGeneralSay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.MainToolTip.SetToolTip(Me.ChkGeneralSay, "put the /say prefix in front of everything in the textbox, so you can easily chat" & _
            " with players")
            Me.ChkGeneralSay.UseVisualStyleBackColor = True
            '
            'ALVGeneralPlayers
            '
            Me.ALVGeneralPlayers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVGeneralPlayers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GeneralColName})
            Me.ALVGeneralPlayers.ContextMenuStrip = Me.CmenuPlayerList
            Me.ALVGeneralPlayers.LargeImageList = Me.ImgListPlayerAvatars
            Me.ALVGeneralPlayers.Location = New System.Drawing.Point(3, 3)
            Me.ALVGeneralPlayers.Name = "ALVGeneralPlayers"
            Me.ALVGeneralPlayers.Size = New System.Drawing.Size(152, 295)
            Me.ALVGeneralPlayers.SmallImageList = Me.ImgListPlayerAvatars
            Me.ALVGeneralPlayers.TabIndex = 0
            Me.MainToolTip.SetToolTip(Me.ALVGeneralPlayers, "Currently online players. Right click for options.")
            Me.ALVGeneralPlayers.UseCompatibleStateImageBehavior = False
            Me.ALVGeneralPlayers.View = System.Windows.Forms.View.Details
            '
            'GeneralColName
            '
            Me.GeneralColName.Text = Lr("Name")
            Me.GeneralColName.Width = 125
            '
            'CmenuPlayerList
            '
            Me.CmenuPlayerList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnCMenuPlayerListOp, Me.BtnCMenuPlayerListDeop, Me.BtnCMenuPlayerListKick, Me.BtnCMenuPlayerListBan, Me.BtnCMenuPlayerListGameMode, Me.BtnCMenuPlayerListGive, Me.BtnCmenuPlayerListRefresh})
            Me.CmenuPlayerList.Name = "CmenuPlayerList"
            Me.CmenuPlayerList.Size = New System.Drawing.Size(137, 158)
            '
            'BtnCMenuPlayerListOp
            '
            Me.BtnCMenuPlayerListOp.Image = CType(resources.GetObject("BtnCMenuPlayerListOp.Image"), System.Drawing.Image)
            Me.BtnCMenuPlayerListOp.Name = "BtnCMenuPlayerListOp"
            Me.BtnCMenuPlayerListOp.Size = New System.Drawing.Size(136, 22)
            Me.BtnCMenuPlayerListOp.Text = Lr("Op")
            '
            'BtnCMenuPlayerListDeop
            '
            Me.BtnCMenuPlayerListDeop.Image = CType(resources.GetObject("BtnCMenuPlayerListDeop.Image"), System.Drawing.Image)
            Me.BtnCMenuPlayerListDeop.Name = "BtnCMenuPlayerListDeop"
            Me.BtnCMenuPlayerListDeop.Size = New System.Drawing.Size(136, 22)
            Me.BtnCMenuPlayerListDeop.Text = Lr("De-op")
            '
            'BtnCMenuPlayerListKick
            '
            Me.BtnCMenuPlayerListKick.Image = CType(resources.GetObject("BtnCMenuPlayerListKick.Image"), System.Drawing.Image)
            Me.BtnCMenuPlayerListKick.Name = "BtnCMenuPlayerListKick"
            Me.BtnCMenuPlayerListKick.Size = New System.Drawing.Size(136, 22)
            Me.BtnCMenuPlayerListKick.Text = Lr("Kick")
            '
            'BtnCMenuPlayerListBan
            '
            Me.BtnCMenuPlayerListBan.Image = CType(resources.GetObject("BtnCMenuPlayerListBan.Image"), System.Drawing.Image)
            Me.BtnCMenuPlayerListBan.Name = "BtnCMenuPlayerListBan"
            Me.BtnCMenuPlayerListBan.Size = New System.Drawing.Size(136, 22)
            Me.BtnCMenuPlayerListBan.Text = Lr("Ban")
            '
            'BtnCMenuPlayerListGameMode
            '
            Me.BtnCMenuPlayerListGameMode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnCMenuPlayerListGamemodeSurvival, Me.BtnCMenuPlayerListGamemodeCreative, Me.BtnCMenuPlayerListGamemodeAdventure})
            Me.BtnCMenuPlayerListGameMode.Name = "BtnCMenuPlayerListGameMode"
            Me.BtnCMenuPlayerListGameMode.Size = New System.Drawing.Size(136, 22)
            Me.BtnCMenuPlayerListGameMode.Text = Lr("Gamemode")
            '
            'BtnCMenuPlayerListGamemodeSurvival
            '
            Me.BtnCMenuPlayerListGamemodeSurvival.Name = "BtnCMenuPlayerListGamemodeSurvival"
            Me.BtnCMenuPlayerListGamemodeSurvival.Size = New System.Drawing.Size(129, 22)
            Me.BtnCMenuPlayerListGamemodeSurvival.Text = Lr("Survival")
            '
            'BtnCMenuPlayerListGamemodeCreative
            '
            Me.BtnCMenuPlayerListGamemodeCreative.Name = "BtnCMenuPlayerListGamemodeCreative"
            Me.BtnCMenuPlayerListGamemodeCreative.Size = New System.Drawing.Size(129, 22)
            Me.BtnCMenuPlayerListGamemodeCreative.Text = Lr("Creative")
            '
            'BtnCMenuPlayerListGamemodeAdventure
            '
            Me.BtnCMenuPlayerListGamemodeAdventure.Name = "BtnCMenuPlayerListGamemodeAdventure"
            Me.BtnCMenuPlayerListGamemodeAdventure.Size = New System.Drawing.Size(129, 22)
            Me.BtnCMenuPlayerListGamemodeAdventure.Text = Lr("Adventure")
            '
            'BtnCMenuPlayerListGive
            '
            Me.BtnCMenuPlayerListGive.Name = "BtnCMenuPlayerListGive"
            Me.BtnCMenuPlayerListGive.Size = New System.Drawing.Size(136, 22)
            Me.BtnCMenuPlayerListGive.Text = Lr("Give...")
            '
            'BtnCmenuPlayerListRefresh
            '
            Me.BtnCmenuPlayerListRefresh.Image = CType(resources.GetObject("BtnCmenuPlayerListRefresh.Image"), System.Drawing.Image)
            Me.BtnCmenuPlayerListRefresh.Name = "BtnCmenuPlayerListRefresh"
            Me.BtnCmenuPlayerListRefresh.Size = New System.Drawing.Size(136, 22)
            Me.BtnCmenuPlayerListRefresh.Text = Lr("Refresh list")
            '
            'ImgListPlayerAvatars
            '
            Me.ImgListPlayerAvatars.ImageStream = CType(resources.GetObject("ImgListPlayerAvatars.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ImgListPlayerAvatars.TransparentColor = System.Drawing.Color.Transparent
            Me.ImgListPlayerAvatars.Images.SetKeyName(0, "player_face.png")
            '
            'BtnBrowseOutput
            '
            Me.BtnBrowseOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnBrowseOutput.Location = New System.Drawing.Point(548, 300)
            Me.BtnBrowseOutput.Name = "BtnBrowseOutput"
            Me.BtnBrowseOutput.Size = New System.Drawing.Size(98, 23)
            Me.BtnBrowseOutput.TabIndex = 6
            Me.BtnBrowseOutput.Text = Lr("Browse output")
            Me.MainToolTip.SetToolTip(Me.BtnBrowseOutput, "Send a command")
            Me.BtnBrowseOutput.UseVisualStyleBackColor = True
            '
            'TxtGeneralServerIn
            '
            Me.TxtGeneralServerIn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TxtGeneralServerIn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.TxtGeneralServerIn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
            Me.TxtGeneralServerIn.Location = New System.Drawing.Point(3, 303)
            Me.TxtGeneralServerIn.Name = "TxtGeneralServerIn"
            Me.TxtGeneralServerIn.Size = New System.Drawing.Size(458, 20)
            Me.TxtGeneralServerIn.TabIndex = 5
            '
            'ARTXTServerOutput
            '
            Me.ARTXTServerOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ARTXTServerOutput.AutoScrollDown = False
            Me.ARTXTServerOutput.BackColor = System.Drawing.Color.White
            Me.ARTXTServerOutput.Location = New System.Drawing.Point(3, 3)
            Me.ARTXTServerOutput.Name = "ARTXTServerOutput"
            Me.ARTXTServerOutput.ReadOnly = True
            Me.ARTXTServerOutput.Size = New System.Drawing.Size(643, 295)
            Me.ARTXTServerOutput.TabIndex = 1
            Me.ARTXTServerOutput.Text = Lr("")
            '
            'BtnGeneralSendCmd
            '
            Me.BtnGeneralSendCmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnGeneralSendCmd.Location = New System.Drawing.Point(467, 300)
            Me.BtnGeneralSendCmd.Name = "BtnGeneralSendCmd"
            Me.BtnGeneralSendCmd.Size = New System.Drawing.Size(75, 23)
            Me.BtnGeneralSendCmd.TabIndex = 4
            Me.BtnGeneralSendCmd.Text = Lr("Send")
            Me.MainToolTip.SetToolTip(Me.BtnGeneralSendCmd, "Send a command")
            Me.BtnGeneralSendCmd.UseVisualStyleBackColor = True
            '
            'TabCtrlMain
            '
            Me.TabCtrlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TabCtrlMain.Controls.Add(Me.TabGeneral)
            Me.TabCtrlMain.Controls.Add(Me.TabPlayers)
            Me.TabCtrlMain.Controls.Add(Me.TabSuperStart)
            Me.TabCtrlMain.Controls.Add(Me.TabErrorLogging)
            Me.TabCtrlMain.Controls.Add(Me.TabTaskManager)
            Me.TabCtrlMain.Controls.Add(Me.TabPlugins)
            Me.TabCtrlMain.Controls.Add(Me.TabServerOptions)
            Me.TabCtrlMain.Controls.Add(Me.TabBackups)
            Me.TabCtrlMain.Controls.Add(Me.TabOptionsInfo)
            Me.TabCtrlMain.ImageList = Me.ImgListTabIcons
            Me.TabCtrlMain.Location = New System.Drawing.Point(-1, 0)
            Me.TabCtrlMain.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
            Me.TabCtrlMain.Name = "TabCtrlMain"
            Me.TabCtrlMain.SelectedIndex = 0
            Me.TabCtrlMain.Size = New System.Drawing.Size(839, 543)
            Me.TabCtrlMain.TabIndex = 1
            '
            'TabGeneral
            '
            Me.TabGeneral.Controls.Add(Me.GbGeneralInfo)
            Me.TabGeneral.Controls.Add(Me.GBGeneralGeneral)
            Me.TabGeneral.ImageKey = "information.png"
            Me.TabGeneral.Location = New System.Drawing.Point(4, 23)
            Me.TabGeneral.Name = "TabGeneral"
            Me.TabGeneral.Padding = New System.Windows.Forms.Padding(3)
            Me.TabGeneral.Size = New System.Drawing.Size(831, 516)
            Me.TabGeneral.TabIndex = 0
            Me.TabGeneral.Text = Lr("General")
            Me.TabGeneral.UseVisualStyleBackColor = True
            '
            'GbGeneralInfo
            '
            Me.GbGeneralInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GbGeneralInfo.Controls.Add(Me.LblGeneralTimeSinceStartValue)
            Me.GbGeneralInfo.Controls.Add(Me.PanelPerformanceInfo)
            Me.GbGeneralInfo.Controls.Add(Me.lblGeneralTimeSinceStartText)
            Me.GbGeneralInfo.Controls.Add(Me.BtnGeneralKill)
            Me.GbGeneralInfo.Controls.Add(Me.BtnGeneralReload)
            Me.GbGeneralInfo.Controls.Add(Me.BtnGeneralRestart)
            Me.GbGeneralInfo.Controls.Add(Me.BtnGeneralStartStop)
            Me.GbGeneralInfo.Location = New System.Drawing.Point(8, 361)
            Me.GbGeneralInfo.Name = "GbGeneralInfo"
            Me.GbGeneralInfo.Size = New System.Drawing.Size(814, 139)
            Me.GbGeneralInfo.TabIndex = 1
            Me.GbGeneralInfo.TabStop = False
            Me.GbGeneralInfo.Text = Lr("Info")
            '
            'LblGeneralTimeSinceStartValue
            '
            Me.LblGeneralTimeSinceStartValue.AutoSize = True
            Me.LblGeneralTimeSinceStartValue.Location = New System.Drawing.Point(227, 89)
            Me.LblGeneralTimeSinceStartValue.Name = "LblGeneralTimeSinceStartValue"
            Me.LblGeneralTimeSinceStartValue.Size = New System.Drawing.Size(49, 13)
            Me.LblGeneralTimeSinceStartValue.TabIndex = 26
            Me.LblGeneralTimeSinceStartValue.Text = Lr("00:00:00")
            '
            'PanelPerformanceInfo
            '
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralCPUGUI)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralRAMServerValue)
            Me.PanelPerformanceInfo.Controls.Add(Me.PBGeneralCPUGUI)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralRAMGUIValue)
            Me.PanelPerformanceInfo.Controls.Add(Me.PBGeneralCPUServer)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralRAMTotalValue)
            Me.PanelPerformanceInfo.Controls.Add(Me.PBGeneralCPUTotal)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralCPUServer)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralRAMTotal)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralCPUTotal)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralRAMServer)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralCPUTotalValue)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralRAMGUI)
            Me.PanelPerformanceInfo.Controls.Add(Me.PBGeneralRAMTotal)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralCPUGUIValue)
            Me.PanelPerformanceInfo.Controls.Add(Me.PBGeneralRAMServer)
            Me.PanelPerformanceInfo.Controls.Add(Me.lblGeneralCPUServerValue)
            Me.PanelPerformanceInfo.Controls.Add(Me.PBGeneralRAMGUI)
            Me.PanelPerformanceInfo.Location = New System.Drawing.Point(87, 19)
            Me.PanelPerformanceInfo.Name = "PanelPerformanceInfo"
            Me.PanelPerformanceInfo.Size = New System.Drawing.Size(721, 67)
            Me.PanelPerformanceInfo.TabIndex = 25
            '
            'lblGeneralCPUGUI
            '
            Me.lblGeneralCPUGUI.Location = New System.Drawing.Point(3, 4)
            Me.lblGeneralCPUGUI.Margin = New System.Windows.Forms.Padding(3)
            Me.lblGeneralCPUGUI.Name = "lblGeneralCPUGUI"
            Me.lblGeneralCPUGUI.Size = New System.Drawing.Size(135, 13)
            Me.lblGeneralCPUGUI.TabIndex = 11
            Me.lblGeneralCPUGUI.Text = Lr("GUI CPU Usage:")
            Me.lblGeneralCPUGUI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblGeneralRAMServerValue
            '
            Me.lblGeneralRAMServerValue.AutoSize = True
            Me.lblGeneralRAMServerValue.Location = New System.Drawing.Point(480, 27)
            Me.lblGeneralRAMServerValue.Margin = New System.Windows.Forms.Padding(1, 0, 3, 0)
            Me.lblGeneralRAMServerValue.Name = "lblGeneralRAMServerValue"
            Me.lblGeneralRAMServerValue.Size = New System.Drawing.Size(82, 13)
            Me.lblGeneralRAMServerValue.TabIndex = 23
            Me.lblGeneralRAMServerValue.Text = Lr("000% [0000MB]")
            '
            'PBGeneralCPUGUI
            '
            Me.PBGeneralCPUGUI.Animate = False
            Me.PBGeneralCPUGUI.BackColor = System.Drawing.Color.Transparent
            Me.PBGeneralCPUGUI.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.OnThreshold
            Me.PBGeneralCPUGUI.DisplayText = ""
            Me.PBGeneralCPUGUI.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralCPUGUI.Location = New System.Drawing.Point(179, 4)
            Me.PBGeneralCPUGUI.Name = "PBGeneralCPUGUI"
            Me.PBGeneralCPUGUI.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
            Me.PBGeneralCPUGUI.Size = New System.Drawing.Size(150, 15)
            Me.PBGeneralCPUGUI.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralCPUGUI.TabIndex = 24
            Me.PBGeneralCPUGUI.Value = 10
            '
            'lblGeneralRAMGUIValue
            '
            Me.lblGeneralRAMGUIValue.AutoSize = True
            Me.lblGeneralRAMGUIValue.Location = New System.Drawing.Point(480, 7)
            Me.lblGeneralRAMGUIValue.Margin = New System.Windows.Forms.Padding(1, 0, 3, 0)
            Me.lblGeneralRAMGUIValue.Name = "lblGeneralRAMGUIValue"
            Me.lblGeneralRAMGUIValue.Size = New System.Drawing.Size(82, 13)
            Me.lblGeneralRAMGUIValue.TabIndex = 22
            Me.lblGeneralRAMGUIValue.Text = Lr("000% [0000MB]")
            '
            'PBGeneralCPUServer
            '
            Me.PBGeneralCPUServer.Animate = False
            Me.PBGeneralCPUServer.BackColor = System.Drawing.Color.Transparent
            Me.PBGeneralCPUServer.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.OnThreshold
            Me.PBGeneralCPUServer.DisplayText = ""
            Me.PBGeneralCPUServer.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralCPUServer.Location = New System.Drawing.Point(179, 25)
            Me.PBGeneralCPUServer.Name = "PBGeneralCPUServer"
            Me.PBGeneralCPUServer.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
            Me.PBGeneralCPUServer.Size = New System.Drawing.Size(150, 15)
            Me.PBGeneralCPUServer.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralCPUServer.TabIndex = 9
            Me.PBGeneralCPUServer.Value = 10
            '
            'lblGeneralRAMTotalValue
            '
            Me.lblGeneralRAMTotalValue.AutoSize = True
            Me.lblGeneralRAMTotalValue.Location = New System.Drawing.Point(480, 48)
            Me.lblGeneralRAMTotalValue.Margin = New System.Windows.Forms.Padding(1, 0, 3, 0)
            Me.lblGeneralRAMTotalValue.Name = "lblGeneralRAMTotalValue"
            Me.lblGeneralRAMTotalValue.Size = New System.Drawing.Size(82, 13)
            Me.lblGeneralRAMTotalValue.TabIndex = 21
            Me.lblGeneralRAMTotalValue.Text = Lr("000% [0000MB]")
            '
            'PBGeneralCPUTotal
            '
            Me.PBGeneralCPUTotal.Animate = False
            Me.PBGeneralCPUTotal.BackColor = System.Drawing.Color.Transparent
            Me.PBGeneralCPUTotal.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.OnThreshold
            Me.PBGeneralCPUTotal.DisplayText = ""
            Me.PBGeneralCPUTotal.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralCPUTotal.Location = New System.Drawing.Point(179, 46)
            Me.PBGeneralCPUTotal.Name = "PBGeneralCPUTotal"
            Me.PBGeneralCPUTotal.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
            Me.PBGeneralCPUTotal.Size = New System.Drawing.Size(150, 15)
            Me.PBGeneralCPUTotal.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralCPUTotal.TabIndex = 10
            Me.PBGeneralCPUTotal.Value = 10
            '
            'lblGeneralCPUServer
            '
            Me.lblGeneralCPUServer.Location = New System.Drawing.Point(6, 25)
            Me.lblGeneralCPUServer.Margin = New System.Windows.Forms.Padding(3)
            Me.lblGeneralCPUServer.Name = "lblGeneralCPUServer"
            Me.lblGeneralCPUServer.Size = New System.Drawing.Size(132, 13)
            Me.lblGeneralCPUServer.TabIndex = 12
            Me.lblGeneralCPUServer.Text = Lr("Server CPU Usage:")
            Me.lblGeneralCPUServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblGeneralRAMTotal
            '
            Me.lblGeneralRAMTotal.Location = New System.Drawing.Point(335, 48)
            Me.lblGeneralRAMTotal.Margin = New System.Windows.Forms.Padding(3, 0, 1, 0)
            Me.lblGeneralRAMTotal.Name = "lblGeneralRAMTotal"
            Me.lblGeneralRAMTotal.Size = New System.Drawing.Size(143, 13)
            Me.lblGeneralRAMTotal.TabIndex = 16
            Me.lblGeneralRAMTotal.Text = Lr("Total RAM Usage:")
            Me.lblGeneralRAMTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblGeneralCPUTotal
            '
            Me.lblGeneralCPUTotal.Location = New System.Drawing.Point(3, 46)
            Me.lblGeneralCPUTotal.Margin = New System.Windows.Forms.Padding(3)
            Me.lblGeneralCPUTotal.Name = "lblGeneralCPUTotal"
            Me.lblGeneralCPUTotal.Size = New System.Drawing.Size(135, 13)
            Me.lblGeneralCPUTotal.TabIndex = 13
            Me.lblGeneralCPUTotal.Text = Lr("Total CPU Usage:")
            Me.lblGeneralCPUTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblGeneralRAMServer
            '
            Me.lblGeneralRAMServer.Location = New System.Drawing.Point(338, 27)
            Me.lblGeneralRAMServer.Margin = New System.Windows.Forms.Padding(3, 0, 1, 0)
            Me.lblGeneralRAMServer.Name = "lblGeneralRAMServer"
            Me.lblGeneralRAMServer.Size = New System.Drawing.Size(140, 13)
            Me.lblGeneralRAMServer.TabIndex = 15
            Me.lblGeneralRAMServer.Text = Lr("Server RAM Usage:")
            Me.lblGeneralRAMServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblGeneralCPUTotalValue
            '
            Me.lblGeneralCPUTotalValue.Location = New System.Drawing.Point(140, 48)
            Me.lblGeneralCPUTotalValue.Margin = New System.Windows.Forms.Padding(3)
            Me.lblGeneralCPUTotalValue.Name = "lblGeneralCPUTotalValue"
            Me.lblGeneralCPUTotalValue.Size = New System.Drawing.Size(33, 13)
            Me.lblGeneralCPUTotalValue.TabIndex = 20
            Me.lblGeneralCPUTotalValue.Text = Lr("00%")
            Me.lblGeneralCPUTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblGeneralRAMGUI
            '
            Me.lblGeneralRAMGUI.Location = New System.Drawing.Point(335, 6)
            Me.lblGeneralRAMGUI.Margin = New System.Windows.Forms.Padding(3, 0, 1, 0)
            Me.lblGeneralRAMGUI.Name = "lblGeneralRAMGUI"
            Me.lblGeneralRAMGUI.Size = New System.Drawing.Size(143, 13)
            Me.lblGeneralRAMGUI.TabIndex = 14
            Me.lblGeneralRAMGUI.Text = Lr("GUI RAM Usage:")
            Me.lblGeneralRAMGUI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PBGeneralRAMTotal
            '
            Me.PBGeneralRAMTotal.Animate = False
            Me.PBGeneralRAMTotal.BackColor = System.Drawing.Color.Transparent
            Me.PBGeneralRAMTotal.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.OnThreshold
            Me.PBGeneralRAMTotal.DisplayText = "test"
            Me.PBGeneralRAMTotal.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralRAMTotal.Location = New System.Drawing.Point(568, 46)
            Me.PBGeneralRAMTotal.Name = "PBGeneralRAMTotal"
            Me.PBGeneralRAMTotal.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.TextAfterValue
            Me.PBGeneralRAMTotal.Size = New System.Drawing.Size(150, 15)
            Me.PBGeneralRAMTotal.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralRAMTotal.TabIndex = 7
            Me.PBGeneralRAMTotal.Value = 10
            '
            'lblGeneralCPUGUIValue
            '
            Me.lblGeneralCPUGUIValue.Location = New System.Drawing.Point(140, 6)
            Me.lblGeneralCPUGUIValue.Margin = New System.Windows.Forms.Padding(3)
            Me.lblGeneralCPUGUIValue.Name = "lblGeneralCPUGUIValue"
            Me.lblGeneralCPUGUIValue.Size = New System.Drawing.Size(33, 13)
            Me.lblGeneralCPUGUIValue.TabIndex = 18
            Me.lblGeneralCPUGUIValue.Text = Lr("00%")
            Me.lblGeneralCPUGUIValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PBGeneralRAMServer
            '
            Me.PBGeneralRAMServer.Animate = False
            Me.PBGeneralRAMServer.BackColor = System.Drawing.Color.Transparent
            Me.PBGeneralRAMServer.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.OnThreshold
            Me.PBGeneralRAMServer.DisplayText = ""
            Me.PBGeneralRAMServer.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralRAMServer.Location = New System.Drawing.Point(568, 25)
            Me.PBGeneralRAMServer.Name = "PBGeneralRAMServer"
            Me.PBGeneralRAMServer.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
            Me.PBGeneralRAMServer.Size = New System.Drawing.Size(150, 15)
            Me.PBGeneralRAMServer.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralRAMServer.TabIndex = 6
            Me.PBGeneralRAMServer.Value = 10
            '
            'lblGeneralCPUServerValue
            '
            Me.lblGeneralCPUServerValue.Location = New System.Drawing.Point(140, 27)
            Me.lblGeneralCPUServerValue.Margin = New System.Windows.Forms.Padding(3)
            Me.lblGeneralCPUServerValue.Name = "lblGeneralCPUServerValue"
            Me.lblGeneralCPUServerValue.Size = New System.Drawing.Size(33, 13)
            Me.lblGeneralCPUServerValue.TabIndex = 19
            Me.lblGeneralCPUServerValue.Text = Lr("00%")
            Me.lblGeneralCPUServerValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'PBGeneralRAMGUI
            '
            Me.PBGeneralRAMGUI.Animate = False
            Me.PBGeneralRAMGUI.BackColor = System.Drawing.Color.Transparent
            Me.PBGeneralRAMGUI.BarColorMethod = Net.Bertware.Controls.VistaProgressBar.BarColorsWhen.OnThreshold
            Me.PBGeneralRAMGUI.DisplayText = ""
            Me.PBGeneralRAMGUI.EndColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralRAMGUI.Location = New System.Drawing.Point(568, 4)
            Me.PBGeneralRAMGUI.Name = "PBGeneralRAMGUI"
            Me.PBGeneralRAMGUI.ShowPercentage = Net.Bertware.Controls.VistaProgressBar.TextShowFormats.None
            Me.PBGeneralRAMGUI.Size = New System.Drawing.Size(150, 15)
            Me.PBGeneralRAMGUI.StartColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.PBGeneralRAMGUI.TabIndex = 5
            Me.PBGeneralRAMGUI.Value = 10
            '
            'lblGeneralTimeSinceStartText
            '
            Me.lblGeneralTimeSinceStartText.Location = New System.Drawing.Point(93, 89)
            Me.lblGeneralTimeSinceStartText.Margin = New System.Windows.Forms.Padding(1, 0, 3, 0)
            Me.lblGeneralTimeSinceStartText.Name = "lblGeneralTimeSinceStartText"
            Me.lblGeneralTimeSinceStartText.Size = New System.Drawing.Size(132, 13)
            Me.lblGeneralTimeSinceStartText.TabIndex = 17
            Me.lblGeneralTimeSinceStartText.Text = Lr("Time since start:")
            Me.lblGeneralTimeSinceStartText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'BtnGeneralKill
            '
            Me.BtnGeneralKill.Enabled = False
            Me.BtnGeneralKill.Location = New System.Drawing.Point(6, 106)
            Me.BtnGeneralKill.Name = "BtnGeneralKill"
            Me.BtnGeneralKill.Size = New System.Drawing.Size(75, 23)
            Me.BtnGeneralKill.TabIndex = 8
            Me.BtnGeneralKill.Text = Lr("Kill")
            Me.MainToolTip.SetToolTip(Me.BtnGeneralKill, "Kill the server, this will instantly close the java process. This might corrupt y" & _
            "our data!")
            Me.BtnGeneralKill.UseVisualStyleBackColor = True
            '
            'BtnGeneralReload
            '
            Me.BtnGeneralReload.Enabled = False
            Me.BtnGeneralReload.Location = New System.Drawing.Point(6, 77)
            Me.BtnGeneralReload.Name = "BtnGeneralReload"
            Me.BtnGeneralReload.Size = New System.Drawing.Size(75, 23)
            Me.BtnGeneralReload.TabIndex = 7
            Me.BtnGeneralReload.Text = Lr("Reload")
            Me.MainToolTip.SetToolTip(Me.BtnGeneralReload, "Reload the server" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.BtnGeneralReload.UseVisualStyleBackColor = True
            '
            'BtnGeneralRestart
            '
            Me.BtnGeneralRestart.Enabled = False
            Me.BtnGeneralRestart.Location = New System.Drawing.Point(6, 48)
            Me.BtnGeneralRestart.Name = "BtnGeneralRestart"
            Me.BtnGeneralRestart.Size = New System.Drawing.Size(75, 23)
            Me.BtnGeneralRestart.TabIndex = 6
            Me.BtnGeneralRestart.Text = Lr("Restart")
            Me.MainToolTip.SetToolTip(Me.BtnGeneralRestart, "Restart the server. In the case of a remote server, restart the connection." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.BtnGeneralRestart.UseVisualStyleBackColor = True
            '
            'BtnGeneralStartStop
            '
            Me.BtnGeneralStartStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnGeneralStartStop.Location = New System.Drawing.Point(6, 19)
            Me.BtnGeneralStartStop.Name = "BtnGeneralStartStop"
            Me.BtnGeneralStartStop.Size = New System.Drawing.Size(75, 23)
            Me.BtnGeneralStartStop.TabIndex = 5
            Me.BtnGeneralStartStop.Text = Lr("Start")
            Me.MainToolTip.SetToolTip(Me.BtnGeneralStartStop, "Start/Stop the server. In the case of a remote server, connect to/disconnect from" & _
            " the server.")
            Me.BtnGeneralStartStop.UseVisualStyleBackColor = True
            '
            'TabPlayers
            '
            Me.TabPlayers.Controls.Add(Me.LblPlayersViewMode)
            Me.TabPlayers.Controls.Add(Me.ALVPlayersPlayers)
            Me.TabPlayers.Controls.Add(Me.TBPlayersPlayersView)
            Me.TabPlayers.ImageKey = "group.png"
            Me.TabPlayers.Location = New System.Drawing.Point(4, 23)
            Me.TabPlayers.Name = "TabPlayers"
            Me.TabPlayers.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPlayers.Size = New System.Drawing.Size(831, 516)
            Me.TabPlayers.TabIndex = 1
            Me.TabPlayers.Text = Lr("Players")
            Me.TabPlayers.UseVisualStyleBackColor = True
            '
            'LblPlayersViewMode
            '
            Me.LblPlayersViewMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.LblPlayersViewMode.Location = New System.Drawing.Point(6, 489)
            Me.LblPlayersViewMode.Name = "LblPlayersViewMode"
            Me.LblPlayersViewMode.Size = New System.Drawing.Size(70, 13)
            Me.LblPlayersViewMode.TabIndex = 2
            Me.LblPlayersViewMode.Text = Lr("View:")
            Me.LblPlayersViewMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ALVPlayersPlayers
            '
            Me.ALVPlayersPlayers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVPlayersPlayers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColPlayersPlayersName, Me.ColPlayersPlayersIP, Me.ColPlayersPlayersLocation, Me.ColPlayersPlayersTJoined, Me.ColPlayersPlayersWhitelist, Me.ColPlayersPlayersOP})
            Me.ALVPlayersPlayers.ContextMenuStrip = Me.CmenuPlayerList
            Me.ALVPlayersPlayers.FullRowSelect = True
            Me.ALVPlayersPlayers.GridLines = True
            Me.ALVPlayersPlayers.LargeImageList = Me.ImgListPlayerAvatars
            Me.ALVPlayersPlayers.Location = New System.Drawing.Point(3, 3)
            Me.ALVPlayersPlayers.Name = "ALVPlayersPlayers"
            Me.ALVPlayersPlayers.Size = New System.Drawing.Size(816, 473)
            Me.ALVPlayersPlayers.SmallImageList = Me.ImgListPlayerAvatars
            Me.ALVPlayersPlayers.TabIndex = 0
            Me.MainToolTip.SetToolTip(Me.ALVPlayersPlayers, "These are the players who are currently online. Right click for options" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.ALVPlayersPlayers.UseCompatibleStateImageBehavior = False
            Me.ALVPlayersPlayers.View = System.Windows.Forms.View.Details
            '
            'ColPlayersPlayersName
            '
            Me.ColPlayersPlayersName.Text = Lr("Name")
            Me.ColPlayersPlayersName.Width = 180
            '
            'ColPlayersPlayersIP
            '
            Me.ColPlayersPlayersIP.Text = Lr("IP")
            Me.ColPlayersPlayersIP.Width = 115
            '
            'ColPlayersPlayersLocation
            '
            Me.ColPlayersPlayersLocation.Text = Lr("Location")
            Me.ColPlayersPlayersLocation.Width = 180
            '
            'ColPlayersPlayersTJoined
            '
            Me.ColPlayersPlayersTJoined.Text = Lr("Time Joined")
            Me.ColPlayersPlayersTJoined.Width = 109
            '
            'ColPlayersPlayersWhitelist
            '
            Me.ColPlayersPlayersWhitelist.Text = Lr("Whitelisted")
            Me.ColPlayersPlayersWhitelist.Width = 67
            '
            'ColPlayersPlayersOP
            '
            Me.ColPlayersPlayersOP.Text = Lr("OP")
            '
            'TBPlayersPlayersView
            '
            Me.TBPlayersPlayersView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.TBPlayersPlayersView.BackColor = System.Drawing.Color.White
            Me.TBPlayersPlayersView.LargeChange = 1
            Me.TBPlayersPlayersView.Location = New System.Drawing.Point(82, 482)
            Me.TBPlayersPlayersView.Maximum = 4
            Me.TBPlayersPlayersView.Name = "TBPlayersPlayersView"
            Me.TBPlayersPlayersView.Size = New System.Drawing.Size(128, 45)
            Me.TBPlayersPlayersView.TabIndex = 1
            Me.MainToolTip.SetToolTip(Me.TBPlayersPlayersView, "Set the view mode of the player list")
            Me.TBPlayersPlayersView.Value = 1
            '
            'TabSuperStart
            '
            Me.TabSuperStart.Controls.Add(Me.BtnSuperStartPortForwarding)
            Me.TabSuperStart.Controls.Add(Me.BtnSuperStartLaunch)
            Me.TabSuperStart.Controls.Add(Me.GBSuperStartMaintainance)
            Me.TabSuperStart.Controls.Add(Me.GBSuperStartRemoteServer)
            Me.TabSuperStart.Controls.Add(Me.GBSuperstartJavaServer)
            Me.TabSuperStart.Controls.Add(Me.CBSuperstartServerType)
            Me.TabSuperStart.Controls.Add(Me.lblSuperStartType)
            Me.TabSuperStart.ImageKey = "control_play.png"
            Me.TabSuperStart.Location = New System.Drawing.Point(4, 23)
            Me.TabSuperStart.Name = "TabSuperStart"
            Me.TabSuperStart.Padding = New System.Windows.Forms.Padding(3)
            Me.TabSuperStart.Size = New System.Drawing.Size(831, 516)
            Me.TabSuperStart.TabIndex = 2
            Me.TabSuperStart.Text = Lr("SuperStart")
            Me.TabSuperStart.UseVisualStyleBackColor = True
            '
            'BtnSuperStartPortForwarding
            '
            Me.BtnSuperStartPortForwarding.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnSuperStartPortForwarding.Location = New System.Drawing.Point(15, 377)
            Me.BtnSuperStartPortForwarding.Name = "BtnSuperStartPortForwarding"
            Me.BtnSuperStartPortForwarding.Size = New System.Drawing.Size(804, 23)
            Me.BtnSuperStartPortForwarding.TabIndex = 14
            Me.BtnSuperStartPortForwarding.Text = Lr("Set-up port forwarding... (experimental)")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartPortForwarding, "Set-up port forwarding. Port forwarding is required to allow people from outside " & _
            "your network to join your server. Warning! Experimental feature" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.BtnSuperStartPortForwarding.UseVisualStyleBackColor = True
            '
            'BtnSuperStartLaunch
            '
            Me.BtnSuperStartLaunch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnSuperStartLaunch.Location = New System.Drawing.Point(15, 348)
            Me.BtnSuperStartLaunch.Name = "BtnSuperStartLaunch"
            Me.BtnSuperStartLaunch.Size = New System.Drawing.Size(804, 23)
            Me.BtnSuperStartLaunch.TabIndex = 13
            Me.BtnSuperStartLaunch.Text = Lr("Launch Server")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartLaunch, "Launch the server. This button does the same as the ""start"" button in the general" & _
            " tab" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.BtnSuperStartLaunch.UseVisualStyleBackColor = True
            '
            'GBSuperStartMaintainance
            '
            Me.GBSuperStartMaintainance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBSuperStartMaintainance.Controls.Add(Me.Label7)
            Me.GBSuperStartMaintainance.Controls.Add(Me.NumSuperstartCustomBuild)
            Me.GBSuperStartMaintainance.Controls.Add(Me.ChkSuperstartAutoUpdate)
            Me.GBSuperStartMaintainance.Controls.Add(Me.BtnSuperStartGetCurrent)
            Me.GBSuperStartMaintainance.Controls.Add(Me.ChkSuperStartRetrieveCurrent)
            Me.GBSuperStartMaintainance.Controls.Add(Me.ChkSuperstartAutoUpdateNotify)
            Me.GBSuperStartMaintainance.Controls.Add(Me.llblSuperStartsite)
            Me.GBSuperStartMaintainance.Controls.Add(Me.lblSuperStartLatestDev)
            Me.GBSuperStartMaintainance.Controls.Add(Me.lblSuperStartLatestBeta)
            Me.GBSuperStartMaintainance.Controls.Add(Me.lblSuperStartLatestStable)
            Me.GBSuperStartMaintainance.Controls.Add(Me.BtnSuperStartDownloadCustomBuild)
            Me.GBSuperStartMaintainance.Controls.Add(Me.BtnSuperStartDownloadDev)
            Me.GBSuperStartMaintainance.Controls.Add(Me.BtnSuperStartDownloadBeta)
            Me.GBSuperStartMaintainance.Controls.Add(Me.BtnSuperStartDownloadRecommended)
            Me.GBSuperStartMaintainance.Controls.Add(Me.PBSuperStartServerIcon)
            Me.GBSuperStartMaintainance.Location = New System.Drawing.Point(519, 4)
            Me.GBSuperStartMaintainance.Name = "GBSuperStartMaintainance"
            Me.GBSuperStartMaintainance.Size = New System.Drawing.Size(300, 337)
            Me.GBSuperStartMaintainance.TabIndex = 7
            Me.GBSuperStartMaintainance.TabStop = False
            Me.GBSuperStartMaintainance.Text = Lr("Maintainance")
            '
            'Label7
            '
            Me.Label7.Location = New System.Drawing.Point(9, 232)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(101, 13)
            Me.Label7.TabIndex = 13
            Me.Label7.Text = Lr("Download #")
            Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'NumSuperstartCustomBuild
            '
            Me.NumSuperstartCustomBuild.Location = New System.Drawing.Point(116, 230)
            Me.NumSuperstartCustomBuild.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
            Me.NumSuperstartCustomBuild.Minimum = New Decimal(New Integer() {1325, 0, 0, 0})
            Me.NumSuperstartCustomBuild.Name = "NumSuperstartCustomBuild"
            Me.NumSuperstartCustomBuild.Size = New System.Drawing.Size(63, 20)
            Me.NumSuperstartCustomBuild.TabIndex = 24
            Me.NumSuperstartCustomBuild.Value = New Decimal(New Integer() {1325, 0, 0, 0})
            '
            'ChkSuperstartAutoUpdate
            '
            Me.ChkSuperstartAutoUpdate.AutoSize = True
            Me.ChkSuperstartAutoUpdate.Location = New System.Drawing.Point(9, 314)
            Me.ChkSuperstartAutoUpdate.Name = "ChkSuperstartAutoUpdate"
            Me.ChkSuperstartAutoUpdate.Size = New System.Drawing.Size(219, 17)
            Me.ChkSuperstartAutoUpdate.TabIndex = 23
            Me.ChkSuperstartAutoUpdate.Text = Lr("Auto-update when an update is available")
            Me.MainToolTip.SetToolTip(Me.ChkSuperstartAutoUpdate, resources.GetString("ChkSuperstartAutoUpdate.ToolTip"))
            Me.ChkSuperstartAutoUpdate.UseVisualStyleBackColor = True
            '
            'BtnSuperStartGetCurrent
            '
            Me.BtnSuperStartGetCurrent.Image = CType(resources.GetObject("BtnSuperStartGetCurrent.Image"), System.Drawing.Image)
            Me.BtnSuperStartGetCurrent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnSuperStartGetCurrent.Location = New System.Drawing.Point(125, 106)
            Me.BtnSuperStartGetCurrent.Name = "BtnSuperStartGetCurrent"
            Me.BtnSuperStartGetCurrent.Size = New System.Drawing.Size(169, 23)
            Me.BtnSuperStartGetCurrent.TabIndex = 22
            Me.BtnSuperStartGetCurrent.Text = Lr("Get current version #")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartGetCurrent, "Get the current version of the server. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This is not supported by vanilla!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.BtnSuperStartGetCurrent.UseVisualStyleBackColor = True
            '
            'ChkSuperStartRetrieveCurrent
            '
            Me.ChkSuperStartRetrieveCurrent.AutoSize = True
            Me.ChkSuperStartRetrieveCurrent.Location = New System.Drawing.Point(9, 276)
            Me.ChkSuperStartRetrieveCurrent.Name = "ChkSuperStartRetrieveCurrent"
            Me.ChkSuperStartRetrieveCurrent.Size = New System.Drawing.Size(227, 17)
            Me.ChkSuperStartRetrieveCurrent.TabIndex = 21
            Me.ChkSuperStartRetrieveCurrent.Text = Lr("Retrieve the current version on server start")
            Me.MainToolTip.SetToolTip(Me.ChkSuperStartRetrieveCurrent, "Retrieve the current version when the server starts, and display this in the GUI." & _
            "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "IMPORTANT: this is NOT supported by vanilla servers!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Vanilla servers will fai" & _
            "l to start if this option is enabled.")
            Me.ChkSuperStartRetrieveCurrent.UseVisualStyleBackColor = True
            '
            'ChkSuperstartAutoUpdateNotify
            '
            Me.ChkSuperstartAutoUpdateNotify.AutoSize = True
            Me.ChkSuperstartAutoUpdateNotify.Location = New System.Drawing.Point(9, 294)
            Me.ChkSuperstartAutoUpdateNotify.Name = "ChkSuperstartAutoUpdateNotify"
            Me.ChkSuperstartAutoUpdateNotify.Size = New System.Drawing.Size(231, 17)
            Me.ChkSuperstartAutoUpdateNotify.TabIndex = 20
            Me.ChkSuperstartAutoUpdateNotify.Text = Lr("Notify me when a server update is available")
            Me.MainToolTip.SetToolTip(Me.ChkSuperstartAutoUpdateNotify, resources.GetString("ChkSuperstartAutoUpdateNotify.ToolTip"))
            Me.ChkSuperstartAutoUpdateNotify.UseVisualStyleBackColor = True
            '
            'llblSuperStartsite
            '
            Me.llblSuperStartsite.AutoSize = True
            Me.llblSuperStartsite.Location = New System.Drawing.Point(6, 260)
            Me.llblSuperStartsite.Name = "llblSuperStartsite"
            Me.llblSuperStartsite.Size = New System.Drawing.Size(28, 13)
            Me.llblSuperStartsite.TabIndex = 19
            Me.llblSuperStartsite.TabStop = True
            Me.llblSuperStartsite.Text = Lr("Site:")
            Me.MainToolTip.SetToolTip(Me.llblSuperStartsite, "The homepage of the selected server." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            '
            'lblSuperStartLatestDev
            '
            Me.lblSuperStartLatestDev.AutoSize = True
            Me.lblSuperStartLatestDev.Location = New System.Drawing.Point(122, 54)
            Me.lblSuperStartLatestDev.Name = "lblSuperStartLatestDev"
            Me.lblSuperStartLatestDev.Size = New System.Drawing.Size(60, 13)
            Me.lblSuperStartLatestDev.TabIndex = 9
            Me.lblSuperStartLatestDev.Text = Lr("Latest dev:")
            '
            'lblSuperStartLatestBeta
            '
            Me.lblSuperStartLatestBeta.AutoSize = True
            Me.lblSuperStartLatestBeta.Location = New System.Drawing.Point(122, 36)
            Me.lblSuperStartLatestBeta.Name = "lblSuperStartLatestBeta"
            Me.lblSuperStartLatestBeta.Size = New System.Drawing.Size(63, 13)
            Me.lblSuperStartLatestBeta.TabIndex = 8
            Me.lblSuperStartLatestBeta.Text = Lr("Latest beta:")
            '
            'lblSuperStartLatestStable
            '
            Me.lblSuperStartLatestStable.AutoSize = True
            Me.lblSuperStartLatestStable.Location = New System.Drawing.Point(122, 19)
            Me.lblSuperStartLatestStable.Name = "lblSuperStartLatestStable"
            Me.lblSuperStartLatestStable.Size = New System.Drawing.Size(70, 13)
            Me.lblSuperStartLatestStable.TabIndex = 7
            Me.lblSuperStartLatestStable.Text = Lr("Latest stable:")
            '
            'BtnSuperStartDownloadCustomBuild
            '
            Me.BtnSuperStartDownloadCustomBuild.Location = New System.Drawing.Point(185, 227)
            Me.BtnSuperStartDownloadCustomBuild.Name = "BtnSuperStartDownloadCustomBuild"
            Me.BtnSuperStartDownloadCustomBuild.Size = New System.Drawing.Size(109, 23)
            Me.BtnSuperStartDownloadCustomBuild.TabIndex = 18
            Me.BtnSuperStartDownloadCustomBuild.Text = Lr("Download")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartDownloadCustomBuild, "Download the build, given by its build number, of the selected server")
            Me.BtnSuperStartDownloadCustomBuild.UseVisualStyleBackColor = True
            '
            'BtnSuperStartDownloadDev
            '
            Me.BtnSuperStartDownloadDev.Image = CType(resources.GetObject("BtnSuperStartDownloadDev.Image"), System.Drawing.Image)
            Me.BtnSuperStartDownloadDev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnSuperStartDownloadDev.Location = New System.Drawing.Point(6, 198)
            Me.BtnSuperStartDownloadDev.Name = "BtnSuperStartDownloadDev"
            Me.BtnSuperStartDownloadDev.Size = New System.Drawing.Size(288, 23)
            Me.BtnSuperStartDownloadDev.TabIndex = 16
            Me.BtnSuperStartDownloadDev.Text = Lr("Download Latest Development Build")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartDownloadDev, "Download the latest development build of the selected server")
            Me.BtnSuperStartDownloadDev.UseVisualStyleBackColor = True
            '
            'BtnSuperStartDownloadBeta
            '
            Me.BtnSuperStartDownloadBeta.Image = CType(resources.GetObject("BtnSuperStartDownloadBeta.Image"), System.Drawing.Image)
            Me.BtnSuperStartDownloadBeta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnSuperStartDownloadBeta.Location = New System.Drawing.Point(6, 169)
            Me.BtnSuperStartDownloadBeta.Name = "BtnSuperStartDownloadBeta"
            Me.BtnSuperStartDownloadBeta.Size = New System.Drawing.Size(288, 23)
            Me.BtnSuperStartDownloadBeta.TabIndex = 15
            Me.BtnSuperStartDownloadBeta.Text = Lr("Download Latest Beta Build")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartDownloadBeta, "Download the latest beta build of the selected server")
            Me.BtnSuperStartDownloadBeta.UseVisualStyleBackColor = True
            '
            'BtnSuperStartDownloadRecommended
            '
            Me.BtnSuperStartDownloadRecommended.Image = CType(resources.GetObject("BtnSuperStartDownloadRecommended.Image"), System.Drawing.Image)
            Me.BtnSuperStartDownloadRecommended.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnSuperStartDownloadRecommended.Location = New System.Drawing.Point(6, 140)
            Me.BtnSuperStartDownloadRecommended.Name = "BtnSuperStartDownloadRecommended"
            Me.BtnSuperStartDownloadRecommended.Size = New System.Drawing.Size(288, 23)
            Me.BtnSuperStartDownloadRecommended.TabIndex = 14
            Me.BtnSuperStartDownloadRecommended.Text = Lr("Download Latest Recommended Build")
            Me.MainToolTip.SetToolTip(Me.BtnSuperStartDownloadRecommended, "Download the latest recommended build of the selected server")
            Me.BtnSuperStartDownloadRecommended.UseVisualStyleBackColor = True
            '
            'PBSuperStartServerIcon
            '
            Me.PBSuperStartServerIcon.Location = New System.Drawing.Point(6, 19)
            Me.PBSuperStartServerIcon.Name = "PBSuperStartServerIcon"
            Me.PBSuperStartServerIcon.Size = New System.Drawing.Size(110, 110)
            Me.PBSuperStartServerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PBSuperStartServerIcon.TabIndex = 0
            Me.PBSuperStartServerIcon.TabStop = False
            '
            'GBSuperStartRemoteServer
            '
            Me.GBSuperStartRemoteServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.MTxtSuperstartRemotePassword)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.MTxtSuperstartRemoteSalt)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.Label5)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.Label4)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.Label3)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.Label2)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.Label1)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.NumSuperstartRemotePort)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.TxtSuperstartRemoteUsername)
            Me.GBSuperStartRemoteServer.Controls.Add(Me.TxtSuperstartRemoteHost)
            Me.GBSuperStartRemoteServer.Location = New System.Drawing.Point(15, 234)
            Me.GBSuperStartRemoteServer.Name = "GBSuperStartRemoteServer"
            Me.GBSuperStartRemoteServer.Size = New System.Drawing.Size(498, 107)
            Me.GBSuperStartRemoteServer.TabIndex = 7
            Me.GBSuperStartRemoteServer.TabStop = False
            Me.GBSuperStartRemoteServer.Text = Lr("Remote Server")
            '
            'MTxtSuperstartRemotePassword
            '
            Me.MTxtSuperstartRemotePassword.Location = New System.Drawing.Point(342, 39)
            Me.MTxtSuperstartRemotePassword.Name = "MTxtSuperstartRemotePassword"
            Me.MTxtSuperstartRemotePassword.Size = New System.Drawing.Size(150, 20)
            Me.MTxtSuperstartRemotePassword.TabIndex = 11
            Me.MainToolTip.SetToolTip(Me.MTxtSuperstartRemotePassword, "The password for the remote server. This is set in the config of the plugin you'r" & _
            "e using." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.MTxtSuperstartRemotePassword.UseSystemPasswordChar = True
            '
            'MTxtSuperstartRemoteSalt
            '
            Me.MTxtSuperstartRemoteSalt.Location = New System.Drawing.Point(87, 65)
            Me.MTxtSuperstartRemoteSalt.Name = "MTxtSuperstartRemoteSalt"
            Me.MTxtSuperstartRemoteSalt.Size = New System.Drawing.Size(150, 20)
            Me.MTxtSuperstartRemoteSalt.TabIndex = 12
            Me.MainToolTip.SetToolTip(Me.MTxtSuperstartRemoteSalt, "The salt for the remote server. This is set in the config of the plugin you're us" & _
            "ing.")
            Me.MTxtSuperstartRemoteSalt.UseSystemPasswordChar = True
            '
            'Label5
            '
            Me.Label5.Location = New System.Drawing.Point(9, 68)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(72, 13)
            Me.Label5.TabIndex = 9
            Me.Label5.Text = Lr("salt:")
            Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label4
            '
            Me.Label4.Location = New System.Drawing.Point(246, 15)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(90, 13)
            Me.Label4.TabIndex = 8
            Me.Label4.Text = Lr("port:")
            Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label3
            '
            Me.Label3.Location = New System.Drawing.Point(243, 42)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(93, 13)
            Me.Label3.TabIndex = 7
            Me.Label3.Text = Lr("password:")
            Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label2
            '
            Me.Label2.Location = New System.Drawing.Point(9, 42)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(72, 13)
            Me.Label2.TabIndex = 6
            Me.Label2.Text = Lr("username:")
            Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(9, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(72, 13)
            Me.Label1.TabIndex = 5
            Me.Label1.Text = Lr("host:")
            Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'NumSuperstartRemotePort
            '
            Me.NumSuperstartRemotePort.Location = New System.Drawing.Point(342, 13)
            Me.NumSuperstartRemotePort.Maximum = New Decimal(New Integer() {65532, 0, 0, 0})
            Me.NumSuperstartRemotePort.Minimum = New Decimal(New Integer() {1024, 0, 0, 0})
            Me.NumSuperstartRemotePort.Name = "NumSuperstartRemotePort"
            Me.NumSuperstartRemotePort.Size = New System.Drawing.Size(150, 20)
            Me.NumSuperstartRemotePort.TabIndex = 9
            Me.NumSuperstartRemotePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
            Me.MainToolTip.SetToolTip(Me.NumSuperstartRemotePort, "The port to use. Default for JSONAPI is 20059." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "In case of JSONAPI, ports 20059-2" & _
            "0060-20061 should be forwarded on the computer that hosts the server!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.NumSuperstartRemotePort.Value = New Decimal(New Integer() {20059, 0, 0, 0})
            '
            'TxtSuperstartRemoteUsername
            '
            Me.TxtSuperstartRemoteUsername.Location = New System.Drawing.Point(87, 39)
            Me.TxtSuperstartRemoteUsername.Name = "TxtSuperstartRemoteUsername"
            Me.TxtSuperstartRemoteUsername.Size = New System.Drawing.Size(150, 20)
            Me.TxtSuperstartRemoteUsername.TabIndex = 10
            Me.MainToolTip.SetToolTip(Me.TxtSuperstartRemoteUsername, "The username for the remote server. This is set in the config of the plugin you'r" & _
            "e using.")
            '
            'TxtSuperstartRemoteHost
            '
            Me.TxtSuperstartRemoteHost.Location = New System.Drawing.Point(87, 13)
            Me.TxtSuperstartRemoteHost.Name = "TxtSuperstartRemoteHost"
            Me.TxtSuperstartRemoteHost.Size = New System.Drawing.Size(150, 20)
            Me.TxtSuperstartRemoteHost.TabIndex = 8
            Me.MainToolTip.SetToolTip(Me.TxtSuperstartRemoteHost, "The hostname for the remote server. This is the IP or hostname of the server you " & _
            "want to connect to." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            '
            'GBSuperstartJavaServer
            '
            Me.GBSuperstartJavaServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBSuperstartJavaServer.Controls.Add(Me.TxtSuperstartJavaCustomSwitch)
            Me.GBSuperstartJavaServer.Controls.Add(Me.LblSuperStartCustomSwitches)
            Me.GBSuperstartJavaServer.Controls.Add(Me.BtnSuperstartJavaJarFileBrowse)
            Me.GBSuperstartJavaServer.Controls.Add(Me.TxtSuperstartJavaJarFile)
            Me.GBSuperstartJavaServer.Controls.Add(Me.TBSuperstartJavaMaxRam)
            Me.GBSuperstartJavaServer.Controls.Add(Me.TBSuperstartJavaMinRam)
            Me.GBSuperstartJavaServer.Controls.Add(Me.TxtSuperstartJavaCustomArgs)
            Me.GBSuperstartJavaServer.Controls.Add(Me.lblSuperStartCustomArg)
            Me.GBSuperstartJavaServer.Controls.Add(Me.lblSuperStartJarFile)
            Me.GBSuperstartJavaServer.Controls.Add(Me.lblSuperStartMaxRam)
            Me.GBSuperstartJavaServer.Controls.Add(Me.LblSuperStartMinRam)
            Me.GBSuperstartJavaServer.Controls.Add(Me.lblSuperStartJavaVersion)
            Me.GBSuperstartJavaServer.Controls.Add(Me.CBSuperstartJavaJRE)
            Me.GBSuperstartJavaServer.Location = New System.Drawing.Point(15, 33)
            Me.GBSuperstartJavaServer.Name = "GBSuperstartJavaServer"
            Me.GBSuperstartJavaServer.Size = New System.Drawing.Size(498, 195)
            Me.GBSuperstartJavaServer.TabIndex = 6
            Me.GBSuperstartJavaServer.TabStop = False
            Me.GBSuperstartJavaServer.Text = Lr("Java Server")
            '
            'TxtSuperstartJavaCustomSwitch
            '
            Me.TxtSuperstartJavaCustomSwitch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ErrProv.SetIconPadding(Me.TxtSuperstartJavaCustomSwitch, -20)
            Me.TxtSuperstartJavaCustomSwitch.Location = New System.Drawing.Point(120, 160)
            Me.TxtSuperstartJavaCustomSwitch.Name = "TxtSuperstartJavaCustomSwitch"
            Me.TxtSuperstartJavaCustomSwitch.Size = New System.Drawing.Size(334, 20)
            Me.TxtSuperstartJavaCustomSwitch.TabIndex = 14
            Me.MainToolTip.SetToolTip(Me.TxtSuperstartJavaCustomSwitch, resources.GetString("TxtSuperstartJavaCustomSwitch.ToolTip"))
            '
            'LblSuperStartCustomSwitches
            '
            Me.LblSuperStartCustomSwitches.Location = New System.Drawing.Point(6, 163)
            Me.LblSuperStartCustomSwitches.Name = "LblSuperStartCustomSwitches"
            Me.LblSuperStartCustomSwitches.Size = New System.Drawing.Size(108, 13)
            Me.LblSuperStartCustomSwitches.TabIndex = 15
            Me.LblSuperStartCustomSwitches.Text = Lr("Custom switches:")
            Me.LblSuperStartCustomSwitches.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'BtnSuperstartJavaJarFileBrowse
            '
            Me.BtnSuperstartJavaJarFileBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnSuperstartJavaJarFileBrowse.Location = New System.Drawing.Point(460, 106)
            Me.BtnSuperstartJavaJarFileBrowse.Name = "BtnSuperstartJavaJarFileBrowse"
            Me.BtnSuperstartJavaJarFileBrowse.Size = New System.Drawing.Size(32, 23)
            Me.BtnSuperstartJavaJarFileBrowse.TabIndex = 6
            Me.BtnSuperstartJavaJarFileBrowse.Text = Lr("...")
            Me.MainToolTip.SetToolTip(Me.BtnSuperstartJavaJarFileBrowse, "Browse for .jar file")
            Me.BtnSuperstartJavaJarFileBrowse.UseVisualStyleBackColor = True
            '
            'TxtSuperstartJavaJarFile
            '
            Me.TxtSuperstartJavaJarFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ErrProv.SetIconPadding(Me.TxtSuperstartJavaJarFile, -20)
            Me.TxtSuperstartJavaJarFile.Location = New System.Drawing.Point(120, 108)
            Me.TxtSuperstartJavaJarFile.Name = "TxtSuperstartJavaJarFile"
            Me.TxtSuperstartJavaJarFile.Size = New System.Drawing.Size(334, 20)
            Me.TxtSuperstartJavaJarFile.TabIndex = 5
            Me.MainToolTip.SetToolTip(Me.TxtSuperstartJavaJarFile, "the server .jar file. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "e.g. craftbukkit.jar, minecraft_server.jar, ...")
            '
            'TBSuperstartJavaMaxRam
            '
            Me.TBSuperstartJavaMaxRam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TBSuperstartJavaMaxRam.BackColor = System.Drawing.Color.White
            Me.ErrProv.SetIconPadding(Me.TBSuperstartJavaMaxRam, -20)
            Me.TBSuperstartJavaMaxRam.LargeChange = 512
            Me.TBSuperstartJavaMaxRam.Location = New System.Drawing.Point(120, 72)
            Me.TBSuperstartJavaMaxRam.Maximum = 8192
            Me.TBSuperstartJavaMaxRam.Name = "TBSuperstartJavaMaxRam"
            Me.TBSuperstartJavaMaxRam.Size = New System.Drawing.Size(372, 45)
            Me.TBSuperstartJavaMaxRam.SmallChange = 128
            Me.TBSuperstartJavaMaxRam.TabIndex = 4
            Me.TBSuperstartJavaMaxRam.TickFrequency = 256
            '
            'TBSuperstartJavaMinRam
            '
            Me.TBSuperstartJavaMinRam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TBSuperstartJavaMinRam.BackColor = System.Drawing.Color.White
            Me.ErrProv.SetIconPadding(Me.TBSuperstartJavaMinRam, -20)
            Me.TBSuperstartJavaMinRam.LargeChange = 256
            Me.TBSuperstartJavaMinRam.Location = New System.Drawing.Point(120, 42)
            Me.TBSuperstartJavaMinRam.Maximum = 8192
            Me.TBSuperstartJavaMinRam.Name = "TBSuperstartJavaMinRam"
            Me.TBSuperstartJavaMinRam.Size = New System.Drawing.Size(372, 45)
            Me.TBSuperstartJavaMinRam.SmallChange = 128
            Me.TBSuperstartJavaMinRam.TabIndex = 3
            Me.TBSuperstartJavaMinRam.TickFrequency = 256
            '
            'TxtSuperstartJavaCustomArgs
            '
            Me.TxtSuperstartJavaCustomArgs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ErrProv.SetIconPadding(Me.TxtSuperstartJavaCustomArgs, -20)
            Me.TxtSuperstartJavaCustomArgs.Location = New System.Drawing.Point(120, 134)
            Me.TxtSuperstartJavaCustomArgs.Name = "TxtSuperstartJavaCustomArgs"
            Me.TxtSuperstartJavaCustomArgs.Size = New System.Drawing.Size(334, 20)
            Me.TxtSuperstartJavaCustomArgs.TabIndex = 7
            Me.MainToolTip.SetToolTip(Me.TxtSuperstartJavaCustomArgs, resources.GetString("TxtSuperstartJavaCustomArgs.ToolTip"))
            '
            'lblSuperStartCustomArg
            '
            Me.lblSuperStartCustomArg.Location = New System.Drawing.Point(6, 137)
            Me.lblSuperStartCustomArg.Name = "lblSuperStartCustomArg"
            Me.lblSuperStartCustomArg.Size = New System.Drawing.Size(108, 13)
            Me.lblSuperStartCustomArg.TabIndex = 13
            Me.lblSuperStartCustomArg.Text = Lr("Custom arguments:")
            Me.lblSuperStartCustomArg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSuperStartJarFile
            '
            Me.lblSuperStartJarFile.Location = New System.Drawing.Point(9, 111)
            Me.lblSuperStartJarFile.Name = "lblSuperStartJarFile"
            Me.lblSuperStartJarFile.Size = New System.Drawing.Size(105, 13)
            Me.lblSuperStartJarFile.TabIndex = 12
            Me.lblSuperStartJarFile.Text = Lr("JAR file:")
            Me.lblSuperStartJarFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSuperStartMaxRam
            '
            Me.lblSuperStartMaxRam.Location = New System.Drawing.Point(6, 74)
            Me.lblSuperStartMaxRam.Name = "lblSuperStartMaxRam"
            Me.lblSuperStartMaxRam.Size = New System.Drawing.Size(108, 13)
            Me.lblSuperStartMaxRam.TabIndex = 11
            Me.lblSuperStartMaxRam.Text = Lr("Max. RAM:")
            Me.lblSuperStartMaxRam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'LblSuperStartMinRam
            '
            Me.LblSuperStartMinRam.Location = New System.Drawing.Point(9, 46)
            Me.LblSuperStartMinRam.Name = "LblSuperStartMinRam"
            Me.LblSuperStartMinRam.Size = New System.Drawing.Size(105, 13)
            Me.LblSuperStartMinRam.TabIndex = 10
            Me.LblSuperStartMinRam.Text = Lr("Min. RAM:")
            Me.LblSuperStartMinRam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblSuperStartJavaVersion
            '
            Me.lblSuperStartJavaVersion.Location = New System.Drawing.Point(6, 22)
            Me.lblSuperStartJavaVersion.Name = "lblSuperStartJavaVersion"
            Me.lblSuperStartJavaVersion.Size = New System.Drawing.Size(108, 13)
            Me.lblSuperStartJavaVersion.TabIndex = 9
            Me.lblSuperStartJavaVersion.Text = Lr("Java Version:")
            Me.lblSuperStartJavaVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CBSuperstartJavaJRE
            '
            Me.CBSuperstartJavaJRE.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CBSuperstartJavaJRE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CBSuperstartJavaJRE.FormattingEnabled = True
            Me.CBSuperstartJavaJRE.Items.AddRange(New Object() {"Jre6x32", "Jre6x64", "Jre7x32", "Jre7x64", "Jre8x32", "Jre8x64", "Alternative java path (select to set)"})
            Me.CBSuperstartJavaJRE.Location = New System.Drawing.Point(120, 19)
            Me.CBSuperstartJavaJRE.Name = "CBSuperstartJavaJRE"
            Me.CBSuperstartJavaJRE.Size = New System.Drawing.Size(372, 21)
            Me.CBSuperstartJavaJRE.TabIndex = 2
            Me.MainToolTip.SetToolTip(Me.CBSuperstartJavaJRE, "Set the java version that should be used." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Java 7 is recommended." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "64bit is recom" & _
            "mended if available." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "The selected java version should be installed on the compu" & _
            "ter." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            '
            'CBSuperstartServerType
            '
            Me.CBSuperstartServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CBSuperstartServerType.FormattingEnabled = True
            Me.CBSuperstartServerType.Items.AddRange(New Object() {"Bukkit", "Vanilla", "Remote JSONAPI", "Generic Java", "Spigot"})
            Me.CBSuperstartServerType.Location = New System.Drawing.Point(135, 6)
            Me.CBSuperstartServerType.Name = "CBSuperstartServerType"
            Me.CBSuperstartServerType.Size = New System.Drawing.Size(372, 21)
            Me.CBSuperstartServerType.TabIndex = 1
            Me.MainToolTip.SetToolTip(Me.CBSuperstartServerType, resources.GetString("CBSuperstartServerType.ToolTip"))
            '
            'lblSuperStartType
            '
            Me.lblSuperStartType.Location = New System.Drawing.Point(15, 9)
            Me.lblSuperStartType.Name = "lblSuperStartType"
            Me.lblSuperStartType.Size = New System.Drawing.Size(114, 13)
            Me.lblSuperStartType.TabIndex = 3
            Me.lblSuperStartType.Text = Lr("Server Type:")
            Me.lblSuperStartType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'TabErrorLogging
            '
            Me.TabErrorLogging.Controls.Add(Me.BtnErrorLoggingCopy)
            Me.TabErrorLogging.Controls.Add(Me.ChkErrorsHideStackTrace)
            Me.TabErrorLogging.Controls.Add(Me.ChkErrorsHideError)
            Me.TabErrorLogging.Controls.Add(Me.ChkErrorsHideWarning)
            Me.TabErrorLogging.Controls.Add(Me.BtnErrorLoggingDetails)
            Me.TabErrorLogging.Controls.Add(Me.ALVErrors)
            Me.TabErrorLogging.ImageKey = "error.png"
            Me.TabErrorLogging.Location = New System.Drawing.Point(4, 23)
            Me.TabErrorLogging.Name = "TabErrorLogging"
            Me.TabErrorLogging.Size = New System.Drawing.Size(831, 516)
            Me.TabErrorLogging.TabIndex = 6
            Me.TabErrorLogging.Text = Lr("Error Logging")
            Me.TabErrorLogging.UseVisualStyleBackColor = True
            '
            'BtnErrorLoggingCopy
            '
            Me.BtnErrorLoggingCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnErrorLoggingCopy.Location = New System.Drawing.Point(672, 489)
            Me.BtnErrorLoggingCopy.Name = "BtnErrorLoggingCopy"
            Me.BtnErrorLoggingCopy.Size = New System.Drawing.Size(75, 23)
            Me.BtnErrorLoggingCopy.TabIndex = 5
            Me.BtnErrorLoggingCopy.Text = Lr("Copy")
            Me.MainToolTip.SetToolTip(Me.BtnErrorLoggingCopy, "Copy the selected items to your clipboard")
            Me.BtnErrorLoggingCopy.UseVisualStyleBackColor = True
            '
            'ChkErrorsHideStackTrace
            '
            Me.ChkErrorsHideStackTrace.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ChkErrorsHideStackTrace.Location = New System.Drawing.Point(441, 489)
            Me.ChkErrorsHideStackTrace.Name = "ChkErrorsHideStackTrace"
            Me.ChkErrorsHideStackTrace.Size = New System.Drawing.Size(210, 24)
            Me.ChkErrorsHideStackTrace.TabIndex = 4
            Me.ChkErrorsHideStackTrace.Text = Lr("Hide stacktraces from server output")
            Me.ChkErrorsHideStackTrace.UseVisualStyleBackColor = True
            '
            'ChkErrorsHideError
            '
            Me.ChkErrorsHideError.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ChkErrorsHideError.Location = New System.Drawing.Point(225, 490)
            Me.ChkErrorsHideError.Name = "ChkErrorsHideError"
            Me.ChkErrorsHideError.Size = New System.Drawing.Size(210, 24)
            Me.ChkErrorsHideError.TabIndex = 3
            Me.ChkErrorsHideError.Text = Lr("Hide errors from server output")
            Me.ChkErrorsHideError.UseVisualStyleBackColor = True
            '
            'ChkErrorsHideWarning
            '
            Me.ChkErrorsHideWarning.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ChkErrorsHideWarning.Location = New System.Drawing.Point(9, 489)
            Me.ChkErrorsHideWarning.Name = "ChkErrorsHideWarning"
            Me.ChkErrorsHideWarning.Size = New System.Drawing.Size(210, 24)
            Me.ChkErrorsHideWarning.TabIndex = 2
            Me.ChkErrorsHideWarning.Text = Lr("Hide warnings from server output")
            Me.ChkErrorsHideWarning.UseVisualStyleBackColor = True
            '
            'BtnErrorLoggingDetails
            '
            Me.BtnErrorLoggingDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnErrorLoggingDetails.Location = New System.Drawing.Point(753, 490)
            Me.BtnErrorLoggingDetails.Name = "BtnErrorLoggingDetails"
            Me.BtnErrorLoggingDetails.Size = New System.Drawing.Size(75, 23)
            Me.BtnErrorLoggingDetails.TabIndex = 1
            Me.BtnErrorLoggingDetails.Text = Lr("Details...")
            Me.BtnErrorLoggingDetails.UseVisualStyleBackColor = True
            '
            'ALVErrors
            '
            Me.ALVErrors.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVErrors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColErrorID, Me.ColErrorType, Me.ColErrorTime, Me.ColErrorMsg})
            Me.ALVErrors.FullRowSelect = True
            Me.ALVErrors.LargeImageList = Me.ImgListErrorManager
            Me.ALVErrors.Location = New System.Drawing.Point(0, 0)
            Me.ALVErrors.Name = "ALVErrors"
            Me.ALVErrors.Size = New System.Drawing.Size(828, 484)
            Me.ALVErrors.SmallImageList = Me.ImgListErrorManager
            Me.ALVErrors.TabIndex = 0
            Me.ALVErrors.UseCompatibleStateImageBehavior = False
            Me.ALVErrors.View = System.Windows.Forms.View.Details
            '
            'ColErrorID
            '
            Me.ColErrorID.Text = Lr("ID")
            Me.ColErrorID.Width = 68
            '
            'ColErrorType
            '
            Me.ColErrorType.Text = Lr("Type")
            Me.ColErrorType.Width = 119
            '
            'ColErrorTime
            '
            Me.ColErrorTime.Text = Lr("Time")
            '
            'ColErrorMsg
            '
            Me.ColErrorMsg.Text = Lr("Text")
            Me.ColErrorMsg.Width = 540
            '
            'ImgListErrorManager
            '
            Me.ImgListErrorManager.ImageStream = CType(resources.GetObject("ImgListErrorManager.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ImgListErrorManager.TransparentColor = System.Drawing.Color.Transparent
            Me.ImgListErrorManager.Images.SetKeyName(0, "error.png")
            Me.ImgListErrorManager.Images.SetKeyName(1, "exclamation.png")
            '
            'TabTaskManager
            '
            Me.TabTaskManager.Controls.Add(Me.BtnTaskManagerTest)
            Me.TabTaskManager.Controls.Add(Me.BtnTaskManagerImport)
            Me.TabTaskManager.Controls.Add(Me.BtnTaskManagerExport)
            Me.TabTaskManager.Controls.Add(Me.BtnTaskManagerAdd)
            Me.TabTaskManager.Controls.Add(Me.BtnTaskManagerEdit)
            Me.TabTaskManager.Controls.Add(Me.ALVTaskPlanner)
            Me.TabTaskManager.Controls.Add(Me.BtnTaskManagerRemove)
            Me.TabTaskManager.ImageKey = "clock.png"
            Me.TabTaskManager.Location = New System.Drawing.Point(4, 23)
            Me.TabTaskManager.Name = "TabTaskManager"
            Me.TabTaskManager.Padding = New System.Windows.Forms.Padding(3)
            Me.TabTaskManager.Size = New System.Drawing.Size(831, 516)
            Me.TabTaskManager.TabIndex = 5
            Me.TabTaskManager.Text = Lr("Task manager")
            Me.TabTaskManager.UseVisualStyleBackColor = True
            '
            'BtnTaskManagerTest
            '
            Me.BtnTaskManagerTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnTaskManagerTest.Image = CType(resources.GetObject("BtnTaskManagerTest.Image"), System.Drawing.Image)
            Me.BtnTaskManagerTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnTaskManagerTest.Location = New System.Drawing.Point(447, 487)
            Me.BtnTaskManagerTest.Name = "BtnTaskManagerTest"
            Me.BtnTaskManagerTest.Size = New System.Drawing.Size(90, 23)
            Me.BtnTaskManagerTest.TabIndex = 6
            Me.BtnTaskManagerTest.Text = Lr("Test")
            Me.BtnTaskManagerTest.UseVisualStyleBackColor = True
            '
            'BtnTaskManagerImport
            '
            Me.BtnTaskManagerImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnTaskManagerImport.Image = CType(resources.GetObject("BtnTaskManagerImport.Image"), System.Drawing.Image)
            Me.BtnTaskManagerImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnTaskManagerImport.Location = New System.Drawing.Point(9, 487)
            Me.BtnTaskManagerImport.Name = "BtnTaskManagerImport"
            Me.BtnTaskManagerImport.Size = New System.Drawing.Size(90, 23)
            Me.BtnTaskManagerImport.TabIndex = 5
            Me.BtnTaskManagerImport.Text = Lr("Import...")
            Me.BtnTaskManagerImport.UseVisualStyleBackColor = True
            '
            'BtnTaskManagerExport
            '
            Me.BtnTaskManagerExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnTaskManagerExport.Image = CType(resources.GetObject("BtnTaskManagerExport.Image"), System.Drawing.Image)
            Me.BtnTaskManagerExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnTaskManagerExport.Location = New System.Drawing.Point(105, 487)
            Me.BtnTaskManagerExport.Name = "BtnTaskManagerExport"
            Me.BtnTaskManagerExport.Size = New System.Drawing.Size(90, 23)
            Me.BtnTaskManagerExport.TabIndex = 4
            Me.BtnTaskManagerExport.Text = Lr("Export...")
            Me.BtnTaskManagerExport.UseVisualStyleBackColor = True
            '
            'BtnTaskManagerAdd
            '
            Me.BtnTaskManagerAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnTaskManagerAdd.Image = CType(resources.GetObject("BtnTaskManagerAdd.Image"), System.Drawing.Image)
            Me.BtnTaskManagerAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnTaskManagerAdd.Location = New System.Drawing.Point(543, 487)
            Me.BtnTaskManagerAdd.Name = "BtnTaskManagerAdd"
            Me.BtnTaskManagerAdd.Size = New System.Drawing.Size(90, 23)
            Me.BtnTaskManagerAdd.TabIndex = 1
            Me.BtnTaskManagerAdd.Text = Lr("Add...")
            Me.BtnTaskManagerAdd.UseVisualStyleBackColor = True
            '
            'BtnTaskManagerEdit
            '
            Me.BtnTaskManagerEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnTaskManagerEdit.Image = CType(resources.GetObject("BtnTaskManagerEdit.Image"), System.Drawing.Image)
            Me.BtnTaskManagerEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnTaskManagerEdit.Location = New System.Drawing.Point(639, 487)
            Me.BtnTaskManagerEdit.Name = "BtnTaskManagerEdit"
            Me.BtnTaskManagerEdit.Size = New System.Drawing.Size(90, 23)
            Me.BtnTaskManagerEdit.TabIndex = 2
            Me.BtnTaskManagerEdit.Text = Lr("Edit...")
            Me.BtnTaskManagerEdit.UseVisualStyleBackColor = True
            '
            'ALVTaskPlanner
            '
            Me.ALVTaskPlanner.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVTaskPlanner.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ALVTaskPlannerColName, Me.ALVTaskPlannerColTriggerType, Me.ALVTaskPlannerColTriggerParam, Me.ALVTaskPlannerColActionType, Me.ALVTaskPlannerColActionParam, Me.ALVTaskPlannerColisEnabled})
            Me.ALVTaskPlanner.FullRowSelect = True
            Me.ALVTaskPlanner.Location = New System.Drawing.Point(9, 6)
            Me.ALVTaskPlanner.Name = "ALVTaskPlanner"
            Me.ALVTaskPlanner.Size = New System.Drawing.Size(816, 475)
            Me.ALVTaskPlanner.TabIndex = 1
            Me.ALVTaskPlanner.UseCompatibleStateImageBehavior = False
            Me.ALVTaskPlanner.View = System.Windows.Forms.View.Details
            '
            'ALVTaskPlannerColName
            '
            Me.ALVTaskPlannerColName.Text = Lr("Name")
            Me.ALVTaskPlannerColName.Width = 150
            '
            'ALVTaskPlannerColTriggerType
            '
            Me.ALVTaskPlannerColTriggerType.Text = Lr("Trigger Type")
            Me.ALVTaskPlannerColTriggerType.Width = 110
            '
            'ALVTaskPlannerColTriggerParam
            '
            Me.ALVTaskPlannerColTriggerParam.Text = Lr("Trigger Parameters")
            Me.ALVTaskPlannerColTriggerParam.Width = 187
            '
            'ALVTaskPlannerColActionType
            '
            Me.ALVTaskPlannerColActionType.Text = Lr("Action type")
            Me.ALVTaskPlannerColActionType.Width = 110
            '
            'ALVTaskPlannerColActionParam
            '
            Me.ALVTaskPlannerColActionParam.Text = Lr("Action parameters")
            Me.ALVTaskPlannerColActionParam.Width = 190
            '
            'ALVTaskPlannerColisEnabled
            '
            Me.ALVTaskPlannerColisEnabled.Text = Lr("Enabled")
            '
            'BtnTaskManagerRemove
            '
            Me.BtnTaskManagerRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnTaskManagerRemove.Image = CType(resources.GetObject("BtnTaskManagerRemove.Image"), System.Drawing.Image)
            Me.BtnTaskManagerRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BtnTaskManagerRemove.Location = New System.Drawing.Point(735, 487)
            Me.BtnTaskManagerRemove.Name = "BtnTaskManagerRemove"
            Me.BtnTaskManagerRemove.Size = New System.Drawing.Size(90, 23)
            Me.BtnTaskManagerRemove.TabIndex = 3
            Me.BtnTaskManagerRemove.Text = Lr("Remove")
            Me.BtnTaskManagerRemove.UseVisualStyleBackColor = True
            '
            'TabPlugins
            '
            Me.TabPlugins.Controls.Add(Me.TabCtrlPlugins)
            Me.TabPlugins.ImageKey = "plugin.png"
            Me.TabPlugins.Location = New System.Drawing.Point(4, 23)
            Me.TabPlugins.Name = "TabPlugins"
            Me.TabPlugins.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPlugins.Size = New System.Drawing.Size(831, 516)
            Me.TabPlugins.TabIndex = 3
            Me.TabPlugins.Text = Lr("Plugins")
            Me.TabPlugins.UseVisualStyleBackColor = True
            '
            'TabCtrlPlugins
            '
            Me.TabCtrlPlugins.Controls.Add(Me.TabPluginsInstall)
            Me.TabCtrlPlugins.Controls.Add(Me.TabPluginsInstalled)
            Me.TabCtrlPlugins.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TabCtrlPlugins.ImageList = Me.ImgListTabIcons
            Me.TabCtrlPlugins.Location = New System.Drawing.Point(3, 3)
            Me.TabCtrlPlugins.Name = "TabCtrlPlugins"
            Me.TabCtrlPlugins.SelectedIndex = 0
            Me.TabCtrlPlugins.Size = New System.Drawing.Size(825, 510)
            Me.TabCtrlPlugins.TabIndex = 0
            '
            'TabPluginsInstall
            '
            Me.TabPluginsInstall.Controls.Add(Me.LblInstallPluginsLoading)
            Me.TabPluginsInstall.Controls.Add(Me.BtnInstallPluginsSearch)
            Me.TabPluginsInstall.Controls.Add(Me.LblInstallPluginsInfo)
            Me.TabPluginsInstall.Controls.Add(Me.lblInstallPluginsCategory)
            Me.TabPluginsInstall.Controls.Add(Me.LblInstallPluginsFilter)
            Me.TabPluginsInstall.Controls.Add(Me.CBInstallPluginsCategory)
            Me.TabPluginsInstall.Controls.Add(Me.TxtInstallPluginsFilter)
            Me.TabPluginsInstall.Controls.Add(Me.ALVBukGetPlugins)
            Me.TabPluginsInstall.ImageKey = "plugin_add.png"
            Me.TabPluginsInstall.Location = New System.Drawing.Point(4, 23)
            Me.TabPluginsInstall.Name = "TabPluginsInstall"
            Me.TabPluginsInstall.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPluginsInstall.Size = New System.Drawing.Size(817, 483)
            Me.TabPluginsInstall.TabIndex = 0
            Me.TabPluginsInstall.Text = Lr("Install Plugins")
            Me.TabPluginsInstall.UseVisualStyleBackColor = True
            '
            'LblInstallPluginsLoading
            '
            Me.LblInstallPluginsLoading.Cursor = System.Windows.Forms.Cursors.WaitCursor
            Me.LblInstallPluginsLoading.Dock = System.Windows.Forms.DockStyle.Fill
            Me.LblInstallPluginsLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblInstallPluginsLoading.Location = New System.Drawing.Point(3, 3)
            Me.LblInstallPluginsLoading.Name = "LblInstallPluginsLoading"
            Me.LblInstallPluginsLoading.Size = New System.Drawing.Size(811, 477)
            Me.LblInstallPluginsLoading.TabIndex = 8
            Me.LblInstallPluginsLoading.Text = Lr("Loading...")
            Me.LblInstallPluginsLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'BtnInstallPluginsSearch
            '
            Me.BtnInstallPluginsSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnInstallPluginsSearch.Location = New System.Drawing.Point(736, 5)
            Me.BtnInstallPluginsSearch.Name = "BtnInstallPluginsSearch"
            Me.BtnInstallPluginsSearch.Size = New System.Drawing.Size(75, 23)
            Me.BtnInstallPluginsSearch.TabIndex = 9
            Me.BtnInstallPluginsSearch.Text = Lr("Search!")
            Me.BtnInstallPluginsSearch.UseVisualStyleBackColor = True
            '
            'LblInstallPluginsInfo
            '
            Me.LblInstallPluginsInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.LblInstallPluginsInfo.AutoSize = True
            Me.LblInstallPluginsInfo.Location = New System.Drawing.Point(13, 460)
            Me.LblInstallPluginsInfo.Name = "LblInstallPluginsInfo"
            Me.LblInstallPluginsInfo.Size = New System.Drawing.Size(223, 13)
            Me.LblInstallPluginsInfo.TabIndex = 6
            Me.LblInstallPluginsInfo.Text = Lr("Right click for options, double click for details.")
            '
            'lblInstallPluginsCategory
            '
            Me.lblInstallPluginsCategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblInstallPluginsCategory.Location = New System.Drawing.Point(293, 9)
            Me.lblInstallPluginsCategory.Name = "lblInstallPluginsCategory"
            Me.lblInstallPluginsCategory.Size = New System.Drawing.Size(247, 13)
            Me.lblInstallPluginsCategory.TabIndex = 5
            Me.lblInstallPluginsCategory.Text = Lr("or browse a category:")
            Me.lblInstallPluginsCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'LblInstallPluginsFilter
            '
            Me.LblInstallPluginsFilter.AutoSize = True
            Me.LblInstallPluginsFilter.Location = New System.Drawing.Point(6, 9)
            Me.LblInstallPluginsFilter.Name = "LblInstallPluginsFilter"
            Me.LblInstallPluginsFilter.Size = New System.Drawing.Size(44, 13)
            Me.LblInstallPluginsFilter.TabIndex = 4
            Me.LblInstallPluginsFilter.Text = Lr("Search:")
            '
            'CBInstallPluginsCategory
            '
            Me.CBInstallPluginsCategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CBInstallPluginsCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CBInstallPluginsCategory.FormattingEnabled = True
            Me.CBInstallPluginsCategory.Location = New System.Drawing.Point(546, 6)
            Me.CBInstallPluginsCategory.Name = "CBInstallPluginsCategory"
            Me.CBInstallPluginsCategory.Size = New System.Drawing.Size(184, 21)
            Me.CBInstallPluginsCategory.TabIndex = 3
            '
            'TxtInstallPluginsFilter
            '
            Me.TxtInstallPluginsFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TxtInstallPluginsFilter.Location = New System.Drawing.Point(56, 6)
            Me.TxtInstallPluginsFilter.Name = "TxtInstallPluginsFilter"
            Me.TxtInstallPluginsFilter.Size = New System.Drawing.Size(231, 20)
            Me.TxtInstallPluginsFilter.TabIndex = 2
            '
            'ALVBukGetPlugins
            '
            Me.ALVBukGetPlugins.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVBukGetPlugins.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColBukgetPluginName, Me.ColBukgetPluginDescription, Me.ColBukgetPluginVersion, Me.ColBukgetPluginBukkitVersion})
            Me.ALVBukGetPlugins.ContextMenuStrip = Me.CMenuBukGetPlugins
            Me.ALVBukGetPlugins.FullRowSelect = True
            Me.ALVBukGetPlugins.Location = New System.Drawing.Point(9, 32)
            Me.ALVBukGetPlugins.Name = "ALVBukGetPlugins"
            Me.ALVBukGetPlugins.ShowItemToolTips = True
            Me.ALVBukGetPlugins.Size = New System.Drawing.Size(802, 423)
            Me.ALVBukGetPlugins.TabIndex = 1
            Me.ALVBukGetPlugins.UseCompatibleStateImageBehavior = False
            Me.ALVBukGetPlugins.View = System.Windows.Forms.View.Details
            '
            'ColBukgetPluginName
            '
            Me.ColBukgetPluginName.Text = Lr("Name")
            Me.ColBukgetPluginName.Width = 275
            '
            'ColBukgetPluginDescription
            '
            Me.ColBukgetPluginDescription.Text = Lr("Description")
            Me.ColBukgetPluginDescription.Width = 275
            '
            'ColBukgetPluginVersion
            '
            Me.ColBukgetPluginVersion.Tag = ""
            Me.ColBukgetPluginVersion.Text = Lr("Version")
            Me.ColBukgetPluginVersion.Width = 80
            '
            'ColBukgetPluginBukkitVersion
            '
            Me.ColBukgetPluginBukkitVersion.Tag = ""
            Me.ColBukgetPluginBukkitVersion.Text = Lr("Bukkit")
            Me.ColBukgetPluginBukkitVersion.Width = 160
            '
            'CMenuBukGetPlugins
            '
            Me.CMenuBukGetPlugins.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCMenuBukGetPluginsMoreInfo, Me.BtnCMenuBukGetPluginsInstallPlugin, Me.BtnCMenuBukGetPluginsProjectPage, Me.BtnCMenuBukGetPluginsRefresh})
            Me.CMenuBukGetPlugins.Name = "ContextMenuStrip1"
            Me.CMenuBukGetPlugins.Size = New System.Drawing.Size(173, 92)
            '
            'btnCMenuBukGetPluginsMoreInfo
            '
            Me.btnCMenuBukGetPluginsMoreInfo.Image = CType(resources.GetObject("btnCMenuBukGetPluginsMoreInfo.Image"), System.Drawing.Image)
            Me.btnCMenuBukGetPluginsMoreInfo.Name = "btnCMenuBukGetPluginsMoreInfo"
            Me.btnCMenuBukGetPluginsMoreInfo.Size = New System.Drawing.Size(172, 22)
            Me.btnCMenuBukGetPluginsMoreInfo.Text = Lr("More info")
            '
            'BtnCMenuBukGetPluginsInstallPlugin
            '
            Me.BtnCMenuBukGetPluginsInstallPlugin.Image = CType(resources.GetObject("BtnCMenuBukGetPluginsInstallPlugin.Image"), System.Drawing.Image)
            Me.BtnCMenuBukGetPluginsInstallPlugin.Name = "BtnCMenuBukGetPluginsInstallPlugin"
            Me.BtnCMenuBukGetPluginsInstallPlugin.Size = New System.Drawing.Size(172, 22)
            Me.BtnCMenuBukGetPluginsInstallPlugin.Text = Lr("Install plugin")
            '
            'BtnCMenuBukGetPluginsProjectPage
            '
            Me.BtnCMenuBukGetPluginsProjectPage.Image = CType(resources.GetObject("BtnCMenuBukGetPluginsProjectPage.Image"), System.Drawing.Image)
            Me.BtnCMenuBukGetPluginsProjectPage.Name = "BtnCMenuBukGetPluginsProjectPage"
            Me.BtnCMenuBukGetPluginsProjectPage.Size = New System.Drawing.Size(172, 22)
            Me.BtnCMenuBukGetPluginsProjectPage.Text = Lr("Go to project page")
            '
            'BtnCMenuBukGetPluginsRefresh
            '
            Me.BtnCMenuBukGetPluginsRefresh.Image = CType(resources.GetObject("BtnCMenuBukGetPluginsRefresh.Image"), System.Drawing.Image)
            Me.BtnCMenuBukGetPluginsRefresh.Name = "BtnCMenuBukGetPluginsRefresh"
            Me.BtnCMenuBukGetPluginsRefresh.Size = New System.Drawing.Size(172, 22)
            Me.BtnCMenuBukGetPluginsRefresh.Text = Lr("Refresh list")
            '
            'TabPluginsInstalled
            '
            Me.TabPluginsInstalled.Controls.Add(Me.lblinstalledpluginsInfo)
            Me.TabPluginsInstalled.Controls.Add(Me.ALVInstalledPlugins)
            Me.TabPluginsInstalled.ImageKey = "plugin_edit.png"
            Me.TabPluginsInstalled.Location = New System.Drawing.Point(4, 23)
            Me.TabPluginsInstalled.Name = "TabPluginsInstalled"
            Me.TabPluginsInstalled.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPluginsInstalled.Size = New System.Drawing.Size(817, 483)
            Me.TabPluginsInstalled.TabIndex = 1
            Me.TabPluginsInstalled.Text = Lr("Installed Plugins")
            Me.TabPluginsInstalled.UseVisualStyleBackColor = True
            '
            'lblinstalledpluginsInfo
            '
            Me.lblinstalledpluginsInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblinstalledpluginsInfo.AutoSize = True
            Me.lblinstalledpluginsInfo.Location = New System.Drawing.Point(6, 469)
            Me.lblinstalledpluginsInfo.Name = "lblinstalledpluginsInfo"
            Me.lblinstalledpluginsInfo.Size = New System.Drawing.Size(444, 13)
            Me.lblinstalledpluginsInfo.TabIndex = 1
            Me.lblinstalledpluginsInfo.Text = Lr("Right click for options, double click for details. Ctrl+click or Shift+click to s") & _
        "elect multiple items."
            '
            'ALVInstalledPlugins
            '
            Me.ALVInstalledPlugins.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVInstalledPlugins.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColName, Me.ColVersion, Me.ColAuthor, Me.ColDescription, Me.ColUpdateDate})
            Me.ALVInstalledPlugins.ContextMenuStrip = Me.CMenuInstalledPlugins
            Me.ALVInstalledPlugins.FullRowSelect = True
            Me.ALVInstalledPlugins.Location = New System.Drawing.Point(6, 6)
            Me.ALVInstalledPlugins.Name = "ALVInstalledPlugins"
            Me.ALVInstalledPlugins.ShowItemToolTips = True
            Me.ALVInstalledPlugins.Size = New System.Drawing.Size(805, 460)
            Me.ALVInstalledPlugins.TabIndex = 0
            Me.ALVInstalledPlugins.UseCompatibleStateImageBehavior = False
            Me.ALVInstalledPlugins.View = System.Windows.Forms.View.Details
            '
            'ColName
            '
            Me.ColName.Text = Lr("Name")
            Me.ColName.Width = 160
            '
            'ColVersion
            '
            Me.ColVersion.Text = Lr("Version")
            Me.ColVersion.Width = 100
            '
            'ColAuthor
            '
            Me.ColAuthor.Text = Lr("Author(s)")
            Me.ColAuthor.Width = 160
            '
            'ColDescription
            '
            Me.ColDescription.Text = Lr("Description")
            Me.ColDescription.Width = 275
            '
            'ColUpdateDate
            '
            Me.ColUpdateDate.Tag = "nosort"
            Me.ColUpdateDate.Text = Lr("Updated")
            Me.ColUpdateDate.Width = 98
            '
            'CMenuInstalledPlugins
            '
            Me.CMenuInstalledPlugins.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CmenuInstalledPluginsMoreInfo, Me.CmenuInstalledPluginsViewVersions, Me.CmenuInstalledPluginsUpdate, Me.CmenuInstalledPluginsProjectPage, Me.CmenuInstalledPluginsRemove, Me.CmenuInstalledPluginsRefresh, Me.CmenuInstalledPluginsOpenFolder})
            Me.CMenuInstalledPlugins.Name = "CMenuInstalledPlugins"
            Me.CMenuInstalledPlugins.Size = New System.Drawing.Size(150, 158)
            '
            'CmenuInstalledPluginsMoreInfo
            '
            Me.CmenuInstalledPluginsMoreInfo.Image = CType(resources.GetObject("CmenuInstalledPluginsMoreInfo.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsMoreInfo.Name = "CmenuInstalledPluginsMoreInfo"
            Me.CmenuInstalledPluginsMoreInfo.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsMoreInfo.Text = Lr("More info")
            '
            'CmenuInstalledPluginsViewVersions
            '
            Me.CmenuInstalledPluginsViewVersions.Image = CType(resources.GetObject("CmenuInstalledPluginsViewVersions.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsViewVersions.Name = "CmenuInstalledPluginsViewVersions"
            Me.CmenuInstalledPluginsViewVersions.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsViewVersions.Text = Lr("View versions")
            '
            'CmenuInstalledPluginsUpdate
            '
            Me.CmenuInstalledPluginsUpdate.Image = CType(resources.GetObject("CmenuInstalledPluginsUpdate.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsUpdate.Name = "CmenuInstalledPluginsUpdate"
            Me.CmenuInstalledPluginsUpdate.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsUpdate.Text = Lr("Update")
            '
            'CmenuInstalledPluginsProjectPage
            '
            Me.CmenuInstalledPluginsProjectPage.Image = CType(resources.GetObject("CmenuInstalledPluginsProjectPage.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsProjectPage.Name = "CmenuInstalledPluginsProjectPage"
            Me.CmenuInstalledPluginsProjectPage.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsProjectPage.Text = Lr("Project page...")
            '
            'CmenuInstalledPluginsRemove
            '
            Me.CmenuInstalledPluginsRemove.Image = CType(resources.GetObject("CmenuInstalledPluginsRemove.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsRemove.Name = "CmenuInstalledPluginsRemove"
            Me.CmenuInstalledPluginsRemove.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsRemove.Text = Lr("Remove")
            '
            'CmenuInstalledPluginsRefresh
            '
            Me.CmenuInstalledPluginsRefresh.Image = CType(resources.GetObject("CmenuInstalledPluginsRefresh.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsRefresh.Name = "CmenuInstalledPluginsRefresh"
            Me.CmenuInstalledPluginsRefresh.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsRefresh.Text = Lr("Refresh list")
            '
            'CmenuInstalledPluginsOpenFolder
            '
            Me.CmenuInstalledPluginsOpenFolder.Image = CType(resources.GetObject("CmenuInstalledPluginsOpenFolder.Image"), System.Drawing.Image)
            Me.CmenuInstalledPluginsOpenFolder.Name = "CmenuInstalledPluginsOpenFolder"
            Me.CmenuInstalledPluginsOpenFolder.Size = New System.Drawing.Size(149, 22)
            Me.CmenuInstalledPluginsOpenFolder.Text = Lr("Open folder...")
            '
            'ImgListTabIcons
            '
            Me.ImgListTabIcons.ImageStream = CType(resources.GetObject("ImgListTabIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ImgListTabIcons.TransparentColor = System.Drawing.Color.Transparent
            Me.ImgListTabIcons.Images.SetKeyName(0, "information.png")
            Me.ImgListTabIcons.Images.SetKeyName(1, "group.png")
            Me.ImgListTabIcons.Images.SetKeyName(2, "control_play.png")
            Me.ImgListTabIcons.Images.SetKeyName(3, "plugin.png")
            Me.ImgListTabIcons.Images.SetKeyName(4, "settings.png")
            Me.ImgListTabIcons.Images.SetKeyName(5, "save.png")
            Me.ImgListTabIcons.Images.SetKeyName(6, "drive-download.png")
            Me.ImgListTabIcons.Images.SetKeyName(7, "clock.png")
            Me.ImgListTabIcons.Images.SetKeyName(8, "error.png")
            Me.ImgListTabIcons.Images.SetKeyName(9, "wrench.png")
            Me.ImgListTabIcons.Images.SetKeyName(10, "Drive-Backup-icon.png")
            Me.ImgListTabIcons.Images.SetKeyName(11, "arrow_down.png")
            Me.ImgListTabIcons.Images.SetKeyName(12, "plugin_add.png")
            Me.ImgListTabIcons.Images.SetKeyName(13, "plugin_edit.png")
            '
            'TabServerOptions
            '
            Me.TabServerOptions.Controls.Add(Me.TxtServerSettingsAddIPBan)
            Me.TabServerOptions.Controls.Add(Me.TxtServerSettingsAddPlayerBan)
            Me.TabServerOptions.Controls.Add(Me.TxtServerSettingsAddOP)
            Me.TabServerOptions.Controls.Add(Me.TxtServerSettingsAddWhitelist)
            Me.TabServerOptions.Controls.Add(Me.Label6)
            Me.TabServerOptions.Controls.Add(Me.BtnServerSettingsAddIPBan)
            Me.TabServerOptions.Controls.Add(Me.BtnServerSettingsAddPlayerBan)
            Me.TabServerOptions.Controls.Add(Me.BtnServerSettingsAddOP)
            Me.TabServerOptions.Controls.Add(Me.BtnServerSettingsAddWhitelist)
            Me.TabServerOptions.Controls.Add(Me.ALVServerSettingsBannedIP)
            Me.TabServerOptions.Controls.Add(Me.ALVServerSettingsBannedPlayer)
            Me.TabServerOptions.Controls.Add(Me.ALVServerSettingsOPs)
            Me.TabServerOptions.Controls.Add(Me.ALVServerSettingsWhiteList)
            Me.TabServerOptions.Controls.Add(Me.ALVServerSettings)
            Me.TabServerOptions.ImageKey = "wrench.png"
            Me.TabServerOptions.Location = New System.Drawing.Point(4, 23)
            Me.TabServerOptions.Name = "TabServerOptions"
            Me.TabServerOptions.Size = New System.Drawing.Size(831, 516)
            Me.TabServerOptions.TabIndex = 7
            Me.TabServerOptions.Text = Lr("Server options")
            Me.TabServerOptions.UseVisualStyleBackColor = True
            '
            'TxtServerSettingsAddIPBan
            '
            Me.TxtServerSettingsAddIPBan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.TxtServerSettingsAddIPBan.Location = New System.Drawing.Point(632, 439)
            Me.TxtServerSettingsAddIPBan.Name = "TxtServerSettingsAddIPBan"
            Me.TxtServerSettingsAddIPBan.Size = New System.Drawing.Size(144, 20)
            Me.TxtServerSettingsAddIPBan.TabIndex = 17
            '
            'TxtServerSettingsAddPlayerBan
            '
            Me.TxtServerSettingsAddPlayerBan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.TxtServerSettingsAddPlayerBan.Location = New System.Drawing.Point(434, 439)
            Me.TxtServerSettingsAddPlayerBan.Name = "TxtServerSettingsAddPlayerBan"
            Me.TxtServerSettingsAddPlayerBan.Size = New System.Drawing.Size(148, 20)
            Me.TxtServerSettingsAddPlayerBan.TabIndex = 16
            '
            'TxtServerSettingsAddOP
            '
            Me.TxtServerSettingsAddOP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.TxtServerSettingsAddOP.Location = New System.Drawing.Point(197, 439)
            Me.TxtServerSettingsAddOP.Name = "TxtServerSettingsAddOP"
            Me.TxtServerSettingsAddOP.Size = New System.Drawing.Size(148, 20)
            Me.TxtServerSettingsAddOP.TabIndex = 15
            '
            'TxtServerSettingsAddWhitelist
            '
            Me.TxtServerSettingsAddWhitelist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.TxtServerSettingsAddWhitelist.Location = New System.Drawing.Point(0, 440)
            Me.TxtServerSettingsAddWhitelist.Name = "TxtServerSettingsAddWhitelist"
            Me.TxtServerSettingsAddWhitelist.Size = New System.Drawing.Size(148, 20)
            Me.TxtServerSettingsAddWhitelist.TabIndex = 14
            '
            'Label6
            '
            Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(4, 486)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(184, 13)
            Me.Label6.TabIndex = 13
            Me.Label6.Text = Lr("Right-click to see all available actions")
            '
            'BtnServerSettingsAddIPBan
            '
            Me.BtnServerSettingsAddIPBan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnServerSettingsAddIPBan.Location = New System.Drawing.Point(782, 437)
            Me.BtnServerSettingsAddIPBan.Name = "BtnServerSettingsAddIPBan"
            Me.BtnServerSettingsAddIPBan.Size = New System.Drawing.Size(36, 23)
            Me.BtnServerSettingsAddIPBan.TabIndex = 12
            Me.BtnServerSettingsAddIPBan.Text = Lr("Add")
            Me.BtnServerSettingsAddIPBan.UseVisualStyleBackColor = True
            '
            'BtnServerSettingsAddPlayerBan
            '
            Me.BtnServerSettingsAddPlayerBan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnServerSettingsAddPlayerBan.Location = New System.Drawing.Point(588, 437)
            Me.BtnServerSettingsAddPlayerBan.Name = "BtnServerSettingsAddPlayerBan"
            Me.BtnServerSettingsAddPlayerBan.Size = New System.Drawing.Size(36, 23)
            Me.BtnServerSettingsAddPlayerBan.TabIndex = 10
            Me.BtnServerSettingsAddPlayerBan.Text = Lr("Add")
            Me.BtnServerSettingsAddPlayerBan.UseVisualStyleBackColor = True
            '
            'BtnServerSettingsAddOP
            '
            Me.BtnServerSettingsAddOP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnServerSettingsAddOP.Location = New System.Drawing.Point(351, 440)
            Me.BtnServerSettingsAddOP.Name = "BtnServerSettingsAddOP"
            Me.BtnServerSettingsAddOP.Size = New System.Drawing.Size(36, 23)
            Me.BtnServerSettingsAddOP.TabIndex = 8
            Me.BtnServerSettingsAddOP.Text = Lr("Add")
            Me.BtnServerSettingsAddOP.UseVisualStyleBackColor = True
            '
            'BtnServerSettingsAddWhitelist
            '
            Me.BtnServerSettingsAddWhitelist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnServerSettingsAddWhitelist.Location = New System.Drawing.Point(155, 440)
            Me.BtnServerSettingsAddWhitelist.Name = "BtnServerSettingsAddWhitelist"
            Me.BtnServerSettingsAddWhitelist.Size = New System.Drawing.Size(36, 23)
            Me.BtnServerSettingsAddWhitelist.TabIndex = 6
            Me.BtnServerSettingsAddWhitelist.Text = Lr("Add")
            Me.BtnServerSettingsAddWhitelist.UseVisualStyleBackColor = True
            '
            'ALVServerSettingsBannedIP
            '
            Me.ALVServerSettingsBannedIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ALVServerSettingsBannedIP.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColServerSettingsBannedIp})
            Me.ALVServerSettingsBannedIP.ContextMenuStrip = Me.CmenuServerLists
            Me.ALVServerSettingsBannedIP.FullRowSelect = True
            Me.ALVServerSettingsBannedIP.Location = New System.Drawing.Point(632, 277)
            Me.ALVServerSettingsBannedIP.Margin = New System.Windows.Forms.Padding(4)
            Me.ALVServerSettingsBannedIP.Name = "ALVServerSettingsBannedIP"
            Me.ALVServerSettingsBannedIP.Size = New System.Drawing.Size(186, 160)
            Me.ALVServerSettingsBannedIP.TabIndex = 4
            Me.MainToolTip.SetToolTip(Me.ALVServerSettingsBannedIP, "Right-click to see al available actions.")
            Me.ALVServerSettingsBannedIP.UseCompatibleStateImageBehavior = False
            Me.ALVServerSettingsBannedIP.View = System.Windows.Forms.View.Details
            '
            'ColServerSettingsBannedIp
            '
            Me.ColServerSettingsBannedIp.Text = Lr("Banned IPs")
            Me.ColServerSettingsBannedIp.Width = 180
            '
            'CmenuServerLists
            '
            Me.CmenuServerLists.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnCmenuServerListsRemove, Me.BtnCmenuServerListRefresh})
            Me.CmenuServerLists.Name = "CmenuServerLists"
            Me.CmenuServerLists.Size = New System.Drawing.Size(118, 48)
            '
            'BtnCmenuServerListsRemove
            '
            Me.BtnCmenuServerListsRemove.Image = CType(resources.GetObject("BtnCmenuServerListsRemove.Image"), System.Drawing.Image)
            Me.BtnCmenuServerListsRemove.Name = "BtnCmenuServerListsRemove"
            Me.BtnCmenuServerListsRemove.Size = New System.Drawing.Size(117, 22)
            Me.BtnCmenuServerListsRemove.Text = Lr("Remove")
            '
            'BtnCmenuServerListRefresh
            '
            Me.BtnCmenuServerListRefresh.Image = CType(resources.GetObject("BtnCmenuServerListRefresh.Image"), System.Drawing.Image)
            Me.BtnCmenuServerListRefresh.Name = "BtnCmenuServerListRefresh"
            Me.BtnCmenuServerListRefresh.Size = New System.Drawing.Size(117, 22)
            Me.BtnCmenuServerListRefresh.Text = Lr("Refresh")
            '
            'ALVServerSettingsBannedPlayer
            '
            Me.ALVServerSettingsBannedPlayer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ALVServerSettingsBannedPlayer.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColServerSettingsBannedPlayers})
            Me.ALVServerSettingsBannedPlayer.ContextMenuStrip = Me.CmenuServerLists
            Me.ALVServerSettingsBannedPlayer.FullRowSelect = True
            Me.ALVServerSettingsBannedPlayer.Location = New System.Drawing.Point(434, 277)
            Me.ALVServerSettingsBannedPlayer.Margin = New System.Windows.Forms.Padding(4)
            Me.ALVServerSettingsBannedPlayer.Name = "ALVServerSettingsBannedPlayer"
            Me.ALVServerSettingsBannedPlayer.Size = New System.Drawing.Size(190, 160)
            Me.ALVServerSettingsBannedPlayer.TabIndex = 3
            Me.MainToolTip.SetToolTip(Me.ALVServerSettingsBannedPlayer, "Right-click to see al available actions.")
            Me.ALVServerSettingsBannedPlayer.UseCompatibleStateImageBehavior = False
            Me.ALVServerSettingsBannedPlayer.View = System.Windows.Forms.View.Details
            '
            'ColServerSettingsBannedPlayers
            '
            Me.ColServerSettingsBannedPlayers.Text = Lr("Banned Players")
            Me.ColServerSettingsBannedPlayers.Width = 185
            '
            'ALVServerSettingsOPs
            '
            Me.ALVServerSettingsOPs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ALVServerSettingsOPs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColServerSettingsOps})
            Me.ALVServerSettingsOPs.ContextMenuStrip = Me.CmenuServerLists
            Me.ALVServerSettingsOPs.FullRowSelect = True
            Me.ALVServerSettingsOPs.Location = New System.Drawing.Point(197, 277)
            Me.ALVServerSettingsOPs.Margin = New System.Windows.Forms.Padding(4)
            Me.ALVServerSettingsOPs.Name = "ALVServerSettingsOPs"
            Me.ALVServerSettingsOPs.Size = New System.Drawing.Size(190, 160)
            Me.ALVServerSettingsOPs.TabIndex = 2
            Me.MainToolTip.SetToolTip(Me.ALVServerSettingsOPs, "Right-click to see al available actions.")
            Me.ALVServerSettingsOPs.UseCompatibleStateImageBehavior = False
            Me.ALVServerSettingsOPs.View = System.Windows.Forms.View.Details
            '
            'ColServerSettingsOps
            '
            Me.ColServerSettingsOps.Text = Lr("OPs")
            Me.ColServerSettingsOps.Width = 185
            '
            'ALVServerSettingsWhiteList
            '
            Me.ALVServerSettingsWhiteList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ALVServerSettingsWhiteList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColServerSettingsWhitelist})
            Me.ALVServerSettingsWhiteList.ContextMenuStrip = Me.CmenuServerLists
            Me.ALVServerSettingsWhiteList.FullRowSelect = True
            Me.ALVServerSettingsWhiteList.Location = New System.Drawing.Point(0, 277)
            Me.ALVServerSettingsWhiteList.Margin = New System.Windows.Forms.Padding(4)
            Me.ALVServerSettingsWhiteList.Name = "ALVServerSettingsWhiteList"
            Me.ALVServerSettingsWhiteList.Size = New System.Drawing.Size(190, 160)
            Me.ALVServerSettingsWhiteList.TabIndex = 1
            Me.MainToolTip.SetToolTip(Me.ALVServerSettingsWhiteList, "Right-click to see al available actions.")
            Me.ALVServerSettingsWhiteList.UseCompatibleStateImageBehavior = False
            Me.ALVServerSettingsWhiteList.View = System.Windows.Forms.View.Details
            '
            'ColServerSettingsWhitelist
            '
            Me.ColServerSettingsWhitelist.Text = Lr("Whitelist")
            Me.ColServerSettingsWhitelist.Width = 185
            '
            'ALVServerSettings
            '
            Me.ALVServerSettings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVServerSettings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColServerSettingsName, Me.ColServerSettingsValue})
            Me.ALVServerSettings.ContextMenuStrip = Me.CmenuServerSettings
            Me.ALVServerSettings.FullRowSelect = True
            Me.ALVServerSettings.Location = New System.Drawing.Point(0, 0)
            Me.ALVServerSettings.Name = "ALVServerSettings"
            Me.ALVServerSettings.Size = New System.Drawing.Size(819, 270)
            Me.ALVServerSettings.TabIndex = 0
            Me.MainToolTip.SetToolTip(Me.ALVServerSettings, "Right-click to see al available actions.")
            Me.ALVServerSettings.UseCompatibleStateImageBehavior = False
            Me.ALVServerSettings.View = System.Windows.Forms.View.Details
            '
            'ColServerSettingsName
            '
            Me.ColServerSettingsName.Text = Lr("Name")
            Me.ColServerSettingsName.Width = 200
            '
            'ColServerSettingsValue
            '
            Me.ColServerSettingsValue.Text = Lr("Value")
            Me.ColServerSettingsValue.Width = 400
            '
            'CmenuServerSettings
            '
            Me.CmenuServerSettings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnCmenuServerSettingsAdd, Me.BtnCmenuServerSettingsEdit, Me.BtnCmenuServerSettingsRemove, Me.BtnCmenuServerSettingsRefresh})
            Me.CmenuServerSettings.Name = "CmenuServerSettings"
            Me.CmenuServerSettings.Size = New System.Drawing.Size(118, 92)
            '
            'BtnCmenuServerSettingsAdd
            '
            Me.BtnCmenuServerSettingsAdd.Image = CType(resources.GetObject("BtnCmenuServerSettingsAdd.Image"), System.Drawing.Image)
            Me.BtnCmenuServerSettingsAdd.Name = "BtnCmenuServerSettingsAdd"
            Me.BtnCmenuServerSettingsAdd.Size = New System.Drawing.Size(117, 22)
            Me.BtnCmenuServerSettingsAdd.Text = Lr("Add...")
            '
            'BtnCmenuServerSettingsEdit
            '
            Me.BtnCmenuServerSettingsEdit.Image = CType(resources.GetObject("BtnCmenuServerSettingsEdit.Image"), System.Drawing.Image)
            Me.BtnCmenuServerSettingsEdit.Name = "BtnCmenuServerSettingsEdit"
            Me.BtnCmenuServerSettingsEdit.Size = New System.Drawing.Size(117, 22)
            Me.BtnCmenuServerSettingsEdit.Text = Lr("Edit...")
            '
            'BtnCmenuServerSettingsRemove
            '
            Me.BtnCmenuServerSettingsRemove.Image = CType(resources.GetObject("BtnCmenuServerSettingsRemove.Image"), System.Drawing.Image)
            Me.BtnCmenuServerSettingsRemove.Name = "BtnCmenuServerSettingsRemove"
            Me.BtnCmenuServerSettingsRemove.Size = New System.Drawing.Size(117, 22)
            Me.BtnCmenuServerSettingsRemove.Text = Lr("Remove")
            '
            'BtnCmenuServerSettingsRefresh
            '
            Me.BtnCmenuServerSettingsRefresh.Image = CType(resources.GetObject("BtnCmenuServerSettingsRefresh.Image"), System.Drawing.Image)
            Me.BtnCmenuServerSettingsRefresh.Name = "BtnCmenuServerSettingsRefresh"
            Me.BtnCmenuServerSettingsRefresh.Size = New System.Drawing.Size(117, 22)
            Me.BtnCmenuServerSettingsRefresh.Text = Lr("Refresh")
            '
            'TabBackups
            '
            Me.TabBackups.Controls.Add(Me.BtnBackupExecute)
            Me.TabBackups.Controls.Add(Me.BtnBackupImport)
            Me.TabBackups.Controls.Add(Me.BtnBackupExport)
            Me.TabBackups.Controls.Add(Me.BtnBackupAdd)
            Me.TabBackups.Controls.Add(Me.BtnBackupEdit)
            Me.TabBackups.Controls.Add(Me.ALVBackups)
            Me.TabBackups.Controls.Add(Me.BtnBackupRemove)
            Me.TabBackups.ImageKey = "Drive-Backup-icon.png"
            Me.TabBackups.Location = New System.Drawing.Point(4, 23)
            Me.TabBackups.Name = "TabBackups"
            Me.TabBackups.Size = New System.Drawing.Size(831, 516)
            Me.TabBackups.TabIndex = 8
            Me.TabBackups.Text = Lr("Backup")
            Me.TabBackups.UseVisualStyleBackColor = True
            '
            'BtnBackupExecute
            '
            Me.BtnBackupExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnBackupExecute.Location = New System.Drawing.Point(505, 487)
            Me.BtnBackupExecute.Name = "BtnBackupExecute"
            Me.BtnBackupExecute.Size = New System.Drawing.Size(75, 23)
            Me.BtnBackupExecute.TabIndex = 12
            Me.BtnBackupExecute.Text = Lr("Execute")
            Me.BtnBackupExecute.UseVisualStyleBackColor = True
            '
            'BtnBackupImport
            '
            Me.BtnBackupImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnBackupImport.Location = New System.Drawing.Point(7, 487)
            Me.BtnBackupImport.Name = "BtnBackupImport"
            Me.BtnBackupImport.Size = New System.Drawing.Size(75, 22)
            Me.BtnBackupImport.TabIndex = 11
            Me.BtnBackupImport.Text = Lr("Import...")
            Me.BtnBackupImport.UseVisualStyleBackColor = True
            '
            'BtnBackupExport
            '
            Me.BtnBackupExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.BtnBackupExport.Location = New System.Drawing.Point(88, 487)
            Me.BtnBackupExport.Name = "BtnBackupExport"
            Me.BtnBackupExport.Size = New System.Drawing.Size(75, 23)
            Me.BtnBackupExport.TabIndex = 10
            Me.BtnBackupExport.Text = Lr("Export...")
            Me.BtnBackupExport.UseVisualStyleBackColor = True
            '
            'BtnBackupAdd
            '
            Me.BtnBackupAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnBackupAdd.Location = New System.Drawing.Point(586, 487)
            Me.BtnBackupAdd.Name = "BtnBackupAdd"
            Me.BtnBackupAdd.Size = New System.Drawing.Size(75, 23)
            Me.BtnBackupAdd.TabIndex = 7
            Me.BtnBackupAdd.Text = Lr("Add...")
            Me.BtnBackupAdd.UseVisualStyleBackColor = True
            '
            'BtnBackupEdit
            '
            Me.BtnBackupEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnBackupEdit.Location = New System.Drawing.Point(667, 487)
            Me.BtnBackupEdit.Name = "BtnBackupEdit"
            Me.BtnBackupEdit.Size = New System.Drawing.Size(75, 23)
            Me.BtnBackupEdit.TabIndex = 8
            Me.BtnBackupEdit.Text = Lr("Edit...")
            Me.BtnBackupEdit.UseVisualStyleBackColor = True
            '
            'ALVBackups
            '
            Me.ALVBackups.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ALVBackups.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColBackupName, Me.ColBackupFolders, Me.ColBackupDestination, Me.ColBackupCompression})
            Me.ALVBackups.FullRowSelect = True
            Me.ALVBackups.Location = New System.Drawing.Point(7, 6)
            Me.ALVBackups.Name = "ALVBackups"
            Me.ALVBackups.Size = New System.Drawing.Size(816, 475)
            Me.ALVBackups.TabIndex = 6
            Me.ALVBackups.UseCompatibleStateImageBehavior = False
            Me.ALVBackups.View = System.Windows.Forms.View.Details
            '
            'ColBackupName
            '
            Me.ColBackupName.Text = Lr("Name")
            Me.ColBackupName.Width = 150
            '
            'ColBackupFolders
            '
            Me.ColBackupFolders.Text = Lr("Folders")
            Me.ColBackupFolders.Width = 280
            '
            'ColBackupDestination
            '
            Me.ColBackupDestination.Text = Lr("Destination")
            Me.ColBackupDestination.Width = 280
            '
            'ColBackupCompression
            '
            Me.ColBackupCompression.Text = Lr("Compression")
            Me.ColBackupCompression.Width = 80
            '
            'BtnBackupRemove
            '
            Me.BtnBackupRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnBackupRemove.Location = New System.Drawing.Point(748, 487)
            Me.BtnBackupRemove.Name = "BtnBackupRemove"
            Me.BtnBackupRemove.Size = New System.Drawing.Size(75, 23)
            Me.BtnBackupRemove.TabIndex = 9
            Me.BtnBackupRemove.Text = Lr("Remove")
            Me.BtnBackupRemove.UseVisualStyleBackColor = True
            '
            'TabOptionsInfo
            '
            Me.TabOptionsInfo.Controls.Add(Me.LblDonateInfo)
            Me.TabOptionsInfo.Controls.Add(Me.BtnFeedback)
            Me.TabOptionsInfo.Controls.Add(Me.GBInfoSettings)
            Me.TabOptionsInfo.Controls.Add(Me.GBOptionsInfoSound)
            Me.TabOptionsInfo.Controls.Add(Me.GBOptionsInfoAboutTray)
            Me.TabOptionsInfo.Controls.Add(Me.GBOptionsInfoAboutText)
            Me.TabOptionsInfo.Controls.Add(Me.GBOptionsInfoAboutComputerInfo)
            Me.TabOptionsInfo.Controls.Add(Me.GBOptionsInfoAbout)
            Me.TabOptionsInfo.ImageKey = "settings.png"
            Me.TabOptionsInfo.Location = New System.Drawing.Point(4, 23)
            Me.TabOptionsInfo.Name = "TabOptionsInfo"
            Me.TabOptionsInfo.Padding = New System.Windows.Forms.Padding(3)
            Me.TabOptionsInfo.Size = New System.Drawing.Size(831, 516)
            Me.TabOptionsInfo.TabIndex = 4
            Me.TabOptionsInfo.Text = Lr("Options & Info")
            Me.TabOptionsInfo.UseVisualStyleBackColor = True
            '
            'LblDonateInfo
            '
            Me.LblDonateInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblDonateInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.LblDonateInfo.Location = New System.Drawing.Point(583, 335)
            Me.LblDonateInfo.Name = "LblDonateInfo"
            Me.LblDonateInfo.Size = New System.Drawing.Size(231, 169)
            Me.LblDonateInfo.TabIndex = 8
            Me.LblDonateInfo.Text = resources.GetString("LblDonateInfo.Text")
            '
            'BtnFeedback
            '
            Me.BtnFeedback.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.BtnFeedback.Location = New System.Drawing.Point(586, 282)
            Me.BtnFeedback.Name = "BtnFeedback"
            Me.BtnFeedback.Size = New System.Drawing.Size(233, 52)
            Me.BtnFeedback.TabIndex = 5
            Me.BtnFeedback.Text = Lr("Tell us what you think!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Give feedback by clicking here") & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            Me.BtnFeedback.UseVisualStyleBackColor = True
            '
            'GBInfoSettings
            '
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsLightMode)
            Me.GBInfoSettings.Controls.Add(Me.ChkAutoStartWindows)
            Me.GBInfoSettings.Controls.Add(Me.BtnOptionsResetAll)
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsCheckUpdates)
            Me.GBInfoSettings.Controls.Add(Me.LbloptionsRestartRequiredInfo)
            Me.GBInfoSettings.Controls.Add(Me.lblSettingsTabPages)
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsTabServeroptions)
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsTabPlugins)
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsTabTaskManager)
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsTaberrors)
            Me.GBInfoSettings.Controls.Add(Me.ChkOptionsTabplayers)
            Me.GBInfoSettings.Controls.Add(Me.lblOptionsConfigLocation)
            Me.GBInfoSettings.Controls.Add(Me.CBInfoSettingsFileLocation)
            Me.GBInfoSettings.Controls.Add(Me.ChkRunServerOnGUIStart)
            Me.GBInfoSettings.Controls.Add(Me.lblInfoSettingsLanguage)
            Me.GBInfoSettings.Controls.Add(Me.CBInfoSettingsLanguage)
            Me.GBInfoSettings.Location = New System.Drawing.Point(9, 162)
            Me.GBInfoSettings.Name = "GBInfoSettings"
            Me.GBInfoSettings.Size = New System.Drawing.Size(280, 348)
            Me.GBInfoSettings.TabIndex = 4
            Me.GBInfoSettings.TabStop = False
            Me.GBInfoSettings.Text = Lr("Application Settings")
            '
            'ChkOptionsLightMode
            '
            Me.ChkOptionsLightMode.AutoSize = True
            Me.ChkOptionsLightMode.Location = New System.Drawing.Point(6, 228)
            Me.ChkOptionsLightMode.Name = "ChkOptionsLightMode"
            Me.ChkOptionsLightMode.Size = New System.Drawing.Size(108, 17)
            Me.ChkOptionsLightMode.TabIndex = 19
            Me.ChkOptionsLightMode.Text = Lr("Run in light mode")
            Me.MainToolTip.SetToolTip(Me.ChkOptionsLightMode, "Run the GUI in light mode." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "This will disable all functionallity that isn't neede" & _
            "d." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Usefull to reduce system load or as a ""safe mode"" in case of problems." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.ChkOptionsLightMode.UseVisualStyleBackColor = True
            '
            'ChkAutoStartWindows
            '
            Me.ChkAutoStartWindows.AutoSize = True
            Me.ChkAutoStartWindows.Location = New System.Drawing.Point(6, 297)
            Me.ChkAutoStartWindows.Name = "ChkAutoStartWindows"
            Me.ChkAutoStartWindows.Size = New System.Drawing.Size(169, 17)
            Me.ChkAutoStartWindows.TabIndex = 18
            Me.ChkAutoStartWindows.Text = Lr("Run GUI when windows starts")
            Me.MainToolTip.SetToolTip(Me.ChkAutoStartWindows, "Start the server when the GUI starts, using the settings from the SuperStart tab." & _
            "")
            Me.ChkAutoStartWindows.UseVisualStyleBackColor = True
            '
            'BtnOptionsResetAll
            '
            Me.BtnOptionsResetAll.Location = New System.Drawing.Point(6, 319)
            Me.BtnOptionsResetAll.Name = "BtnOptionsResetAll"
            Me.BtnOptionsResetAll.Size = New System.Drawing.Size(268, 23)
            Me.BtnOptionsResetAll.TabIndex = 9
            Me.BtnOptionsResetAll.Text = Lr("Reset all settings")
            Me.BtnOptionsResetAll.UseVisualStyleBackColor = True
            '
            'ChkOptionsCheckUpdates
            '
            Me.ChkOptionsCheckUpdates.AutoSize = True
            Me.ChkOptionsCheckUpdates.Location = New System.Drawing.Point(6, 251)
            Me.ChkOptionsCheckUpdates.Name = "ChkOptionsCheckUpdates"
            Me.ChkOptionsCheckUpdates.Size = New System.Drawing.Size(163, 17)
            Me.ChkOptionsCheckUpdates.TabIndex = 7
            Me.ChkOptionsCheckUpdates.Text = Lr("Check for updates on startup")
            Me.MainToolTip.SetToolTip(Me.ChkOptionsCheckUpdates, "Check for GUI updates on startup. Highly recommended." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.ChkOptionsCheckUpdates.UseVisualStyleBackColor = True
            '
            'LbloptionsRestartRequiredInfo
            '
            Me.LbloptionsRestartRequiredInfo.Location = New System.Drawing.Point(6, 192)
            Me.LbloptionsRestartRequiredInfo.Name = "LbloptionsRestartRequiredInfo"
            Me.LbloptionsRestartRequiredInfo.Size = New System.Drawing.Size(268, 33)
            Me.LbloptionsRestartRequiredInfo.TabIndex = 17
            Me.LbloptionsRestartRequiredInfo.Text = Lr("You might need to restart the GUI, in order for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "these settings to take full eff") & _
        "ect."
            '
            'lblSettingsTabPages
            '
            Me.lblSettingsTabPages.AutoSize = True
            Me.lblSettingsTabPages.Location = New System.Drawing.Point(48, 82)
            Me.lblSettingsTabPages.Name = "lblSettingsTabPages"
            Me.lblSettingsTabPages.Size = New System.Drawing.Size(58, 13)
            Me.lblSettingsTabPages.TabIndex = 16
            Me.lblSettingsTabPages.Text = Lr("Tabpages:")
            '
            'ChkOptionsTabServeroptions
            '
            Me.ChkOptionsTabServeroptions.AutoSize = True
            Me.ChkOptionsTabServeroptions.Location = New System.Drawing.Point(112, 174)
            Me.ChkOptionsTabServeroptions.Name = "ChkOptionsTabServeroptions"
            Me.ChkOptionsTabServeroptions.Size = New System.Drawing.Size(140, 17)
            Me.ChkOptionsTabServeroptions.TabIndex = 15
            Me.ChkOptionsTabServeroptions.Text = Lr("Show server options tab")
            Me.ChkOptionsTabServeroptions.UseVisualStyleBackColor = True
            '
            'ChkOptionsTabPlugins
            '
            Me.ChkOptionsTabPlugins.AutoSize = True
            Me.ChkOptionsTabPlugins.Location = New System.Drawing.Point(112, 151)
            Me.ChkOptionsTabPlugins.Name = "ChkOptionsTabPlugins"
            Me.ChkOptionsTabPlugins.Size = New System.Drawing.Size(107, 17)
            Me.ChkOptionsTabPlugins.TabIndex = 14
            Me.ChkOptionsTabPlugins.Text = Lr("Show plugins tab")
            Me.ChkOptionsTabPlugins.UseVisualStyleBackColor = True
            '
            'ChkOptionsTabTaskManager
            '
            Me.ChkOptionsTabTaskManager.AutoSize = True
            Me.ChkOptionsTabTaskManager.Location = New System.Drawing.Point(112, 128)
            Me.ChkOptionsTabTaskManager.Name = "ChkOptionsTabTaskManager"
            Me.ChkOptionsTabTaskManager.Size = New System.Drawing.Size(138, 17)
            Me.ChkOptionsTabTaskManager.TabIndex = 13
            Me.ChkOptionsTabTaskManager.Text = Lr("Show task manager tab")
            Me.ChkOptionsTabTaskManager.UseVisualStyleBackColor = True
            '
            'ChkOptionsTaberrors
            '
            Me.ChkOptionsTaberrors.AutoSize = True
            Me.ChkOptionsTaberrors.Location = New System.Drawing.Point(112, 105)
            Me.ChkOptionsTaberrors.Name = "ChkOptionsTaberrors"
            Me.ChkOptionsTaberrors.Size = New System.Drawing.Size(132, 17)
            Me.ChkOptionsTaberrors.TabIndex = 12
            Me.ChkOptionsTaberrors.Text = Lr("Show error logging tab")
            Me.ChkOptionsTaberrors.UseVisualStyleBackColor = True
            '
            'ChkOptionsTabplayers
            '
            Me.ChkOptionsTabplayers.AutoSize = True
            Me.ChkOptionsTabplayers.Location = New System.Drawing.Point(112, 82)
            Me.ChkOptionsTabplayers.Name = "ChkOptionsTabplayers"
            Me.ChkOptionsTabplayers.Size = New System.Drawing.Size(107, 17)
            Me.ChkOptionsTabplayers.TabIndex = 11
            Me.ChkOptionsTabplayers.Text = Lr("Show players tab")
            Me.ChkOptionsTabplayers.UseVisualStyleBackColor = True
            '
            'lblOptionsConfigLocation
            '
            Me.lblOptionsConfigLocation.Location = New System.Drawing.Point(9, 53)
            Me.lblOptionsConfigLocation.Name = "lblOptionsConfigLocation"
            Me.lblOptionsConfigLocation.Size = New System.Drawing.Size(97, 13)
            Me.lblOptionsConfigLocation.TabIndex = 5
            Me.lblOptionsConfigLocation.Text = Lr("Config location:")
            Me.lblOptionsConfigLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CBInfoSettingsFileLocation
            '
            Me.CBInfoSettingsFileLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CBInfoSettingsFileLocation.FormattingEnabled = True
            Me.CBInfoSettingsFileLocation.Location = New System.Drawing.Point(112, 50)
            Me.CBInfoSettingsFileLocation.Name = "CBInfoSettingsFileLocation"
            Me.CBInfoSettingsFileLocation.Size = New System.Drawing.Size(162, 21)
            Me.CBInfoSettingsFileLocation.TabIndex = 4
            Me.MainToolTip.SetToolTip(Me.CBInfoSettingsFileLocation, resources.GetString("CBInfoSettingsFileLocation.ToolTip"))
            '
            'ChkRunServerOnGUIStart
            '
            Me.ChkRunServerOnGUIStart.AutoSize = True
            Me.ChkRunServerOnGUIStart.Location = New System.Drawing.Point(6, 274)
            Me.ChkRunServerOnGUIStart.Name = "ChkRunServerOnGUIStart"
            Me.ChkRunServerOnGUIStart.Size = New System.Drawing.Size(157, 17)
            Me.ChkRunServerOnGUIStart.TabIndex = 3
            Me.ChkRunServerOnGUIStart.Text = Lr("Run server when GUI starts")
            Me.MainToolTip.SetToolTip(Me.ChkRunServerOnGUIStart, "Start the server when the GUI starts, using the settings from the SuperStart tab." & _
            "")
            Me.ChkRunServerOnGUIStart.UseVisualStyleBackColor = True
            '
            'lblInfoSettingsLanguage
            '
            Me.lblInfoSettingsLanguage.Location = New System.Drawing.Point(9, 26)
            Me.lblInfoSettingsLanguage.Name = "lblInfoSettingsLanguage"
            Me.lblInfoSettingsLanguage.Size = New System.Drawing.Size(97, 13)
            Me.lblInfoSettingsLanguage.TabIndex = 1
            Me.lblInfoSettingsLanguage.Text = Lr("Language:")
            Me.lblInfoSettingsLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'CBInfoSettingsLanguage
            '
            Me.CBInfoSettingsLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CBInfoSettingsLanguage.FormattingEnabled = True
            Me.CBInfoSettingsLanguage.Location = New System.Drawing.Point(112, 23)
            Me.CBInfoSettingsLanguage.Name = "CBInfoSettingsLanguage"
            Me.CBInfoSettingsLanguage.Size = New System.Drawing.Size(162, 21)
            Me.CBInfoSettingsLanguage.TabIndex = 0
            Me.MainToolTip.SetToolTip(Me.CBInfoSettingsLanguage, "Set the language for the GUI.")
            '
            'GBOptionsInfoSound
            '
            Me.GBOptionsInfoSound.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBOptionsInfoSound.Controls.Add(Me.ChkInfoSoundOnSevere)
            Me.GBOptionsInfoSound.Controls.Add(Me.ChkInfoSoundOnWarning)
            Me.GBOptionsInfoSound.Controls.Add(Me.ChkInfoSoundOnLeave)
            Me.GBOptionsInfoSound.Controls.Add(Me.ChkInfoSoundOnJoin)
            Me.GBOptionsInfoSound.Location = New System.Drawing.Point(586, 162)
            Me.GBOptionsInfoSound.Name = "GBOptionsInfoSound"
            Me.GBOptionsInfoSound.Size = New System.Drawing.Size(233, 113)
            Me.GBOptionsInfoSound.TabIndex = 3
            Me.GBOptionsInfoSound.TabStop = False
            Me.GBOptionsInfoSound.Text = Lr("Sound settings")
            '
            'ChkInfoSoundOnSevere
            '
            Me.ChkInfoSoundOnSevere.AutoSize = True
            Me.ChkInfoSoundOnSevere.Location = New System.Drawing.Point(5, 82)
            Me.ChkInfoSoundOnSevere.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoSoundOnSevere.Name = "ChkInfoSoundOnSevere"
            Me.ChkInfoSoundOnSevere.Size = New System.Drawing.Size(173, 17)
            Me.ChkInfoSoundOnSevere.TabIndex = 13
            Me.ChkInfoSoundOnSevere.Text = Lr("Play sound on severe message")
            Me.MainToolTip.SetToolTip(Me.ChkInfoSoundOnSevere, "Play a sound when the server sends a severe message")
            Me.ChkInfoSoundOnSevere.UseVisualStyleBackColor = True
            '
            'ChkInfoSoundOnWarning
            '
            Me.ChkInfoSoundOnWarning.AutoSize = True
            Me.ChkInfoSoundOnWarning.Location = New System.Drawing.Point(5, 61)
            Me.ChkInfoSoundOnWarning.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoSoundOnWarning.Name = "ChkInfoSoundOnWarning"
            Me.ChkInfoSoundOnWarning.Size = New System.Drawing.Size(178, 17)
            Me.ChkInfoSoundOnWarning.TabIndex = 12
            Me.ChkInfoSoundOnWarning.Text = Lr("Play sound on warning message")
            Me.MainToolTip.SetToolTip(Me.ChkInfoSoundOnWarning, "Play a sound when the server sends a warning" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.ChkInfoSoundOnWarning.UseVisualStyleBackColor = True
            '
            'ChkInfoSoundOnLeave
            '
            Me.ChkInfoSoundOnLeave.AutoSize = True
            Me.ChkInfoSoundOnLeave.Location = New System.Drawing.Point(5, 39)
            Me.ChkInfoSoundOnLeave.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoSoundOnLeave.Name = "ChkInfoSoundOnLeave"
            Me.ChkInfoSoundOnLeave.Size = New System.Drawing.Size(153, 17)
            Me.ChkInfoSoundOnLeave.TabIndex = 11
            Me.ChkInfoSoundOnLeave.Text = Lr("Play sound on player leave")
            Me.MainToolTip.SetToolTip(Me.ChkInfoSoundOnLeave, "Play a sound when a player leaves the server")
            Me.ChkInfoSoundOnLeave.UseVisualStyleBackColor = True
            '
            'ChkInfoSoundOnJoin
            '
            Me.ChkInfoSoundOnJoin.AutoSize = True
            Me.ChkInfoSoundOnJoin.Location = New System.Drawing.Point(5, 18)
            Me.ChkInfoSoundOnJoin.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoSoundOnJoin.Name = "ChkInfoSoundOnJoin"
            Me.ChkInfoSoundOnJoin.Size = New System.Drawing.Size(143, 17)
            Me.ChkInfoSoundOnJoin.TabIndex = 10
            Me.ChkInfoSoundOnJoin.Text = Lr("Play sound on player join")
            Me.MainToolTip.SetToolTip(Me.ChkInfoSoundOnJoin, "Play a sound when a player connects to the server")
            Me.ChkInfoSoundOnJoin.UseVisualStyleBackColor = True
            '
            'GBOptionsInfoAboutTray
            '
            Me.GBOptionsInfoAboutTray.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBOptionsInfoAboutTray.Controls.Add(Me.ChkInfoTrayOnWarning)
            Me.GBOptionsInfoAboutTray.Controls.Add(Me.ChkInfoTrayOnLeave)
            Me.GBOptionsInfoAboutTray.Controls.Add(Me.ChkInfoTrayOnJoin)
            Me.GBOptionsInfoAboutTray.Controls.Add(Me.ChkInfoTrayOnSevere)
            Me.GBOptionsInfoAboutTray.Controls.Add(Me.ChkInfoTrayAlways)
            Me.GBOptionsInfoAboutTray.Controls.Add(Me.ChkInfoTrayMinimize)
            Me.GBOptionsInfoAboutTray.Location = New System.Drawing.Point(586, 6)
            Me.GBOptionsInfoAboutTray.Name = "GBOptionsInfoAboutTray"
            Me.GBOptionsInfoAboutTray.Size = New System.Drawing.Size(233, 150)
            Me.GBOptionsInfoAboutTray.TabIndex = 2
            Me.GBOptionsInfoAboutTray.TabStop = False
            Me.GBOptionsInfoAboutTray.Text = Lr("Tray and sound")
            '
            'ChkInfoTrayOnWarning
            '
            Me.ChkInfoTrayOnWarning.AutoSize = True
            Me.ChkInfoTrayOnWarning.Location = New System.Drawing.Point(5, 106)
            Me.ChkInfoTrayOnWarning.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoTrayOnWarning.Name = "ChkInfoTrayOnWarning"
            Me.ChkInfoTrayOnWarning.Size = New System.Drawing.Size(80, 17)
            Me.ChkInfoTrayOnWarning.TabIndex = 5
            Me.ChkInfoTrayOnWarning.Text = Lr("On warning")
            Me.MainToolTip.SetToolTip(Me.ChkInfoTrayOnWarning, "Show a tray balloon when the server throws a warning message" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
            Me.ChkInfoTrayOnWarning.UseVisualStyleBackColor = True
            '
            'ChkInfoTrayOnLeave
            '
            Me.ChkInfoTrayOnLeave.AutoSize = True
            Me.ChkInfoTrayOnLeave.Location = New System.Drawing.Point(5, 85)
            Me.ChkInfoTrayOnLeave.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoTrayOnLeave.Name = "ChkInfoTrayOnLeave"
            Me.ChkInfoTrayOnLeave.Size = New System.Drawing.Size(100, 17)
            Me.ChkInfoTrayOnLeave.TabIndex = 4
            Me.ChkInfoTrayOnLeave.Text = Lr("On player leave")
            Me.MainToolTip.SetToolTip(Me.ChkInfoTrayOnLeave, "Show a tray balloon when a player leaves.")
            Me.ChkInfoTrayOnLeave.UseVisualStyleBackColor = True
            '
            'ChkInfoTrayOnJoin
            '
            Me.ChkInfoTrayOnJoin.AutoSize = True
            Me.ChkInfoTrayOnJoin.Location = New System.Drawing.Point(5, 64)
            Me.ChkInfoTrayOnJoin.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoTrayOnJoin.Name = "ChkInfoTrayOnJoin"
            Me.ChkInfoTrayOnJoin.Size = New System.Drawing.Size(90, 17)
            Me.ChkInfoTrayOnJoin.TabIndex = 3
            Me.ChkInfoTrayOnJoin.Text = Lr("On player join")
            Me.MainToolTip.SetToolTip(Me.ChkInfoTrayOnJoin, "Show a tray balloon when a player joins.")
            Me.ChkInfoTrayOnJoin.UseVisualStyleBackColor = True
            '
            'ChkInfoTrayOnSevere
            '
            Me.ChkInfoTrayOnSevere.AutoSize = True
            Me.ChkInfoTrayOnSevere.Location = New System.Drawing.Point(5, 127)
            Me.ChkInfoTrayOnSevere.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoTrayOnSevere.Name = "ChkInfoTrayOnSevere"
            Me.ChkInfoTrayOnSevere.Size = New System.Drawing.Size(75, 17)
            Me.ChkInfoTrayOnSevere.TabIndex = 2
            Me.ChkInfoTrayOnSevere.Text = Lr("On severe")
            Me.MainToolTip.SetToolTip(Me.ChkInfoTrayOnSevere, "Show a tray balloon when the server throws a severe message")
            Me.ChkInfoTrayOnSevere.UseVisualStyleBackColor = True
            '
            'ChkInfoTrayAlways
            '
            Me.ChkInfoTrayAlways.AutoSize = True
            Me.ChkInfoTrayAlways.Location = New System.Drawing.Point(5, 39)
            Me.ChkInfoTrayAlways.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoTrayAlways.Name = "ChkInfoTrayAlways"
            Me.ChkInfoTrayAlways.Size = New System.Drawing.Size(129, 17)
            Me.ChkInfoTrayAlways.TabIndex = 1
            Me.ChkInfoTrayAlways.Text = Lr("Always show balloons")
            Me.MainToolTip.SetToolTip(Me.ChkInfoTrayAlways, "Always show tray balloons, even if the GUI isn't minimized to tray.")
            Me.ChkInfoTrayAlways.UseVisualStyleBackColor = True
            '
            'ChkInfoTrayMinimize
            '
            Me.ChkInfoTrayMinimize.AutoSize = True
            Me.ChkInfoTrayMinimize.Location = New System.Drawing.Point(5, 18)
            Me.ChkInfoTrayMinimize.Margin = New System.Windows.Forms.Padding(2)
            Me.ChkInfoTrayMinimize.Name = "ChkInfoTrayMinimize"
            Me.ChkInfoTrayMinimize.Size = New System.Drawing.Size(98, 17)
            Me.ChkInfoTrayMinimize.TabIndex = 0
            Me.ChkInfoTrayMinimize.Text = Lr("Minimize to tray")
            Me.MainToolTip.SetToolTip(Me.ChkInfoTrayMinimize, "Minimize the GUI to tray when the minimize button is clicked.")
            Me.ChkInfoTrayMinimize.UseVisualStyleBackColor = True
            '
            'GBOptionsInfoAboutText
            '
            Me.GBOptionsInfoAboutText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.ChkTextUTF8)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.CBTextOptionsFont)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.TxtSettingsTextFontPreview)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblTextSettingsFontSize)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblTextSettingsFont)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.NumTextOptionsFontSize)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblSettingsTextFontPreview)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.TxtSettingsTextColorUnknown)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblSettingsTextColorUnknown)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.TxtSettingsTextColorSevere)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblSettingsTextColorSevere)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.TxtSettingsTextColorWarning)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblSettingsTextColorWarning)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.TxtSettingsTextColorPlayer)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblSettingsTextColorPlayer)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.TxtSettingsTextColorInfo)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.LblSettingsTextColorInfo)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.ChkShowDate)
            Me.GBOptionsInfoAboutText.Controls.Add(Me.ChkShowTime)
            Me.GBOptionsInfoAboutText.Location = New System.Drawing.Point(295, 162)
            Me.GBOptionsInfoAboutText.Name = "GBOptionsInfoAboutText"
            Me.GBOptionsInfoAboutText.Size = New System.Drawing.Size(285, 348)
            Me.GBOptionsInfoAboutText.TabIndex = 3
            Me.GBOptionsInfoAboutText.TabStop = False
            Me.GBOptionsInfoAboutText.Text = Lr("Text output")
            '
            'ChkTextUTF8
            '
            Me.ChkTextUTF8.AutoSize = True
            Me.ChkTextUTF8.Location = New System.Drawing.Point(9, 251)
            Me.ChkTextUTF8.Name = "ChkTextUTF8"
            Me.ChkTextUTF8.Size = New System.Drawing.Size(145, 17)
            Me.ChkTextUTF8.TabIndex = 24
            Me.ChkTextUTF8.Text = Lr("UTF-8 compatibility mode")
            Me.MainToolTip.SetToolTip(Me.ChkTextUTF8, "Enable this if you need to read chinese (or other UTF-8) text from the server out" & _
            "put, or need to write chinese (or other UTF-8) text to the server input.")
            Me.ChkTextUTF8.UseVisualStyleBackColor = True
            '
            'CBTextOptionsFont
            '
            Me.CBTextOptionsFont.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
            Me.CBTextOptionsFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.CBTextOptionsFont.FormattingEnabled = True
            Me.CBTextOptionsFont.IntegralHeight = False
            Me.CBTextOptionsFont.Location = New System.Drawing.Point(109, 198)
            Me.CBTextOptionsFont.MaxDropDownItems = 20
            Me.CBTextOptionsFont.Name = "CBTextOptionsFont"
            Me.CBTextOptionsFont.Size = New System.Drawing.Size(135, 21)
            Me.CBTextOptionsFont.TabIndex = 23
            '
            'TxtSettingsTextFontPreview
            '
            Me.TxtSettingsTextFontPreview.Location = New System.Drawing.Point(9, 290)
            Me.TxtSettingsTextFontPreview.Multiline = True
            Me.TxtSettingsTextFontPreview.Name = "TxtSettingsTextFontPreview"
            Me.TxtSettingsTextFontPreview.ReadOnly = True
            Me.TxtSettingsTextFontPreview.Size = New System.Drawing.Size(235, 33)
            Me.TxtSettingsTextFontPreview.TabIndex = 22
            Me.TxtSettingsTextFontPreview.Text = Lr("font preview")
            Me.TxtSettingsTextFontPreview.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'LblTextSettingsFontSize
            '
            Me.LblTextSettingsFontSize.Location = New System.Drawing.Point(6, 227)
            Me.LblTextSettingsFontSize.Name = "LblTextSettingsFontSize"
            Me.LblTextSettingsFontSize.Size = New System.Drawing.Size(97, 13)
            Me.LblTextSettingsFontSize.TabIndex = 21
            Me.LblTextSettingsFontSize.Text = Lr("Font size:")
            Me.LblTextSettingsFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'LblTextSettingsFont
            '
            Me.LblTextSettingsFont.Location = New System.Drawing.Point(6, 201)
            Me.LblTextSettingsFont.Name = "LblTextSettingsFont"
            Me.LblTextSettingsFont.Size = New System.Drawing.Size(97, 13)
            Me.LblTextSettingsFont.TabIndex = 20
            Me.LblTextSettingsFont.Text = Lr("Font :")
            Me.LblTextSettingsFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'NumTextOptionsFontSize
            '
            Me.NumTextOptionsFontSize.Location = New System.Drawing.Point(109, 225)
            Me.NumTextOptionsFontSize.Maximum = New Decimal(New Integer() {36, 0, 0, 0})
            Me.NumTextOptionsFontSize.Minimum = New Decimal(New Integer() {8, 0, 0, 0})
            Me.NumTextOptionsFontSize.Name = "NumTextOptionsFontSize"
            Me.NumTextOptionsFontSize.Size = New System.Drawing.Size(46, 20)
            Me.NumTextOptionsFontSize.TabIndex = 19
            Me.MainToolTip.SetToolTip(Me.NumTextOptionsFontSize, "The font size that will be used for most lists and textboxes.")
            Me.NumTextOptionsFontSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
            '
            'LblSettingsTextFontPreview
            '
            Me.LblSettingsTextFontPreview.AutoSize = True
            Me.LblSettingsTextFontPreview.Location = New System.Drawing.Point(6, 274)
            Me.LblSettingsTextFontPreview.Name = "LblSettingsTextFontPreview"
            Me.LblSettingsTextFontPreview.Size = New System.Drawing.Size(71, 13)
            Me.LblSettingsTextFontPreview.TabIndex = 13
            Me.LblSettingsTextFontPreview.Text = Lr("Font preview:")
            '
            'TxtSettingsTextColorUnknown
            '
            Me.TxtSettingsTextColorUnknown.Location = New System.Drawing.Point(109, 172)
            Me.TxtSettingsTextColorUnknown.Name = "TxtSettingsTextColorUnknown"
            Me.TxtSettingsTextColorUnknown.ReadOnly = True
            Me.TxtSettingsTextColorUnknown.Size = New System.Drawing.Size(135, 20)
            Me.TxtSettingsTextColorUnknown.TabIndex = 11
            '
            'LblSettingsTextColorUnknown
            '
            Me.LblSettingsTextColorUnknown.Location = New System.Drawing.Point(6, 175)
            Me.LblSettingsTextColorUnknown.Name = "LblSettingsTextColorUnknown"
            Me.LblSettingsTextColorUnknown.Size = New System.Drawing.Size(97, 13)
            Me.LblSettingsTextColorUnknown.TabIndex = 10
            Me.LblSettingsTextColorUnknown.Text = Lr("Unknown :")
            Me.LblSettingsTextColorUnknown.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'TxtSettingsTextColorSevere
            '
            Me.TxtSettingsTextColorSevere.Location = New System.Drawing.Point(109, 146)
            Me.TxtSettingsTextColorSevere.Name = "TxtSettingsTextColorSevere"
            Me.TxtSettingsTextColorSevere.ReadOnly = True
            Me.TxtSettingsTextColorSevere.Size = New System.Drawing.Size(135, 20)
            Me.TxtSettingsTextColorSevere.TabIndex = 9
            '
            'LblSettingsTextColorSevere
            '
            Me.LblSettingsTextColorSevere.Location = New System.Drawing.Point(6, 149)
            Me.LblSettingsTextColorSevere.Name = "LblSettingsTextColorSevere"
            Me.LblSettingsTextColorSevere.Size = New System.Drawing.Size(97, 13)
            Me.LblSettingsTextColorSevere.TabIndex = 8
            Me.LblSettingsTextColorSevere.Text = Lr("[SEVERE] :")
            Me.LblSettingsTextColorSevere.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'TxtSettingsTextColorWarning
            '
            Me.TxtSettingsTextColorWarning.Location = New System.Drawing.Point(109, 120)
            Me.TxtSettingsTextColorWarning.Name = "TxtSettingsTextColorWarning"
            Me.TxtSettingsTextColorWarning.ReadOnly = True
            Me.TxtSettingsTextColorWarning.Size = New System.Drawing.Size(135, 20)
            Me.TxtSettingsTextColorWarning.TabIndex = 7
            '
            'LblSettingsTextColorWarning
            '
            Me.LblSettingsTextColorWarning.Location = New System.Drawing.Point(6, 123)
            Me.LblSettingsTextColorWarning.Name = "LblSettingsTextColorWarning"
            Me.LblSettingsTextColorWarning.Size = New System.Drawing.Size(97, 13)
            Me.LblSettingsTextColorWarning.TabIndex = 6
            Me.LblSettingsTextColorWarning.Text = Lr("[WARNING] :")
            Me.LblSettingsTextColorWarning.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'TxtSettingsTextColorPlayer
            '
            Me.TxtSettingsTextColorPlayer.Location = New System.Drawing.Point(109, 94)
            Me.TxtSettingsTextColorPlayer.Name = "TxtSettingsTextColorPlayer"
            Me.TxtSettingsTextColorPlayer.ReadOnly = True
            Me.TxtSettingsTextColorPlayer.Size = New System.Drawing.Size(135, 20)
            Me.TxtSettingsTextColorPlayer.TabIndex = 5
            '
            'LblSettingsTextColorPlayer
            '
            Me.LblSettingsTextColorPlayer.Location = New System.Drawing.Point(9, 97)
            Me.LblSettingsTextColorPlayer.Name = "LblSettingsTextColorPlayer"
            Me.LblSettingsTextColorPlayer.Size = New System.Drawing.Size(94, 13)
            Me.LblSettingsTextColorPlayer.TabIndex = 4
            Me.LblSettingsTextColorPlayer.Text = Lr("Player event :")
            Me.LblSettingsTextColorPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'TxtSettingsTextColorInfo
            '
            Me.TxtSettingsTextColorInfo.Location = New System.Drawing.Point(109, 68)
            Me.TxtSettingsTextColorInfo.Name = "TxtSettingsTextColorInfo"
            Me.TxtSettingsTextColorInfo.ReadOnly = True
            Me.TxtSettingsTextColorInfo.Size = New System.Drawing.Size(135, 20)
            Me.TxtSettingsTextColorInfo.TabIndex = 3
            '
            'LblSettingsTextColorInfo
            '
            Me.LblSettingsTextColorInfo.Location = New System.Drawing.Point(6, 71)
            Me.LblSettingsTextColorInfo.Name = "LblSettingsTextColorInfo"
            Me.LblSettingsTextColorInfo.Size = New System.Drawing.Size(97, 13)
            Me.LblSettingsTextColorInfo.TabIndex = 2
            Me.LblSettingsTextColorInfo.Text = Lr("[INFO] :")
            Me.LblSettingsTextColorInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'ChkShowDate
            '
            Me.ChkShowDate.AutoSize = True
            Me.ChkShowDate.Location = New System.Drawing.Point(6, 42)
            Me.ChkShowDate.Name = "ChkShowDate"
            Me.ChkShowDate.Size = New System.Drawing.Size(77, 17)
            Me.ChkShowDate.TabIndex = 1
            Me.ChkShowDate.Text = Lr("Show date")
            Me.MainToolTip.SetToolTip(Me.ChkShowDate, "Show the date in front of each entry in the log (general tab)")
            Me.ChkShowDate.UseVisualStyleBackColor = True
            '
            'ChkShowTime
            '
            Me.ChkShowTime.AutoSize = True
            Me.ChkShowTime.Location = New System.Drawing.Point(6, 19)
            Me.ChkShowTime.Name = "ChkShowTime"
            Me.ChkShowTime.Size = New System.Drawing.Size(75, 17)
            Me.ChkShowTime.TabIndex = 0
            Me.ChkShowTime.Text = Lr("Show time")
            Me.MainToolTip.SetToolTip(Me.ChkShowTime, "Show the time in front of each entry in the log (general tab)")
            Me.ChkShowTime.UseVisualStyleBackColor = True
            '
            'GBOptionsInfoAboutComputerInfo
            '
            Me.GBOptionsInfoAboutComputerInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GBOptionsInfoAboutComputerInfo.Controls.Add(Me.LblInfoComputerExtIP)
            Me.GBOptionsInfoAboutComputerInfo.Controls.Add(Me.LblInfoComputerLocIP)
            Me.GBOptionsInfoAboutComputerInfo.Controls.Add(Me.LblInfoComputerOS)
            Me.GBOptionsInfoAboutComputerInfo.Controls.Add(Me.LblInfoComputerCPU)
            Me.GBOptionsInfoAboutComputerInfo.Controls.Add(Me.LblInfoComputerRAM)
            Me.GBOptionsInfoAboutComputerInfo.Controls.Add(Me.LblInfoComputerComputerName)
            Me.GBOptionsInfoAboutComputerInfo.Location = New System.Drawing.Point(295, 6)
            Me.GBOptionsInfoAboutComputerInfo.Name = "GBOptionsInfoAboutComputerInfo"
            Me.GBOptionsInfoAboutComputerInfo.Size = New System.Drawing.Size(285, 150)
            Me.GBOptionsInfoAboutComputerInfo.TabIndex = 1
            Me.GBOptionsInfoAboutComputerInfo.TabStop = False
            Me.GBOptionsInfoAboutComputerInfo.Text = Lr("Computer Info")
            '
            'LblInfoComputerExtIP
            '
            Me.LblInfoComputerExtIP.AutoSize = True
            Me.LblInfoComputerExtIP.Location = New System.Drawing.Point(6, 32)
            Me.LblInfoComputerExtIP.Name = "LblInfoComputerExtIP"
            Me.LblInfoComputerExtIP.Size = New System.Drawing.Size(41, 13)
            Me.LblInfoComputerExtIP.TabIndex = 13
            Me.LblInfoComputerExtIP.TabStop = True
            Me.LblInfoComputerExtIP.Text = Lr("Ext. IP:")
            Me.MainToolTip.SetToolTip(Me.LblInfoComputerExtIP, "Your external IP. This is what people outside your house need to connect to your " & _
            "server. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click to copy to clipboard.")
            '
            'LblInfoComputerLocIP
            '
            Me.LblInfoComputerLocIP.AutoSize = True
            Me.LblInfoComputerLocIP.Location = New System.Drawing.Point(6, 16)
            Me.LblInfoComputerLocIP.Name = "LblInfoComputerLocIP"
            Me.LblInfoComputerLocIP.Size = New System.Drawing.Size(49, 13)
            Me.LblInfoComputerLocIP.TabIndex = 14
            Me.LblInfoComputerLocIP.TabStop = True
            Me.LblInfoComputerLocIP.Text = Lr("Local IP:")
            Me.MainToolTip.SetToolTip(Me.LblInfoComputerLocIP, "Your internal IP. This is what people inside your house need to connect to your s" & _
            "erver. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click to copy to clipboard.")
            '
            'LblInfoComputerOS
            '
            Me.LblInfoComputerOS.AutoSize = True
            Me.LblInfoComputerOS.Location = New System.Drawing.Point(6, 107)
            Me.LblInfoComputerOS.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.LblInfoComputerOS.Name = "LblInfoComputerOS"
            Me.LblInfoComputerOS.Size = New System.Drawing.Size(25, 13)
            Me.LblInfoComputerOS.TabIndex = 12
            Me.LblInfoComputerOS.Text = Lr("OS:")
            '
            'LblInfoComputerCPU
            '
            Me.LblInfoComputerCPU.AutoSize = True
            Me.LblInfoComputerCPU.Location = New System.Drawing.Point(6, 77)
            Me.LblInfoComputerCPU.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.LblInfoComputerCPU.Name = "LblInfoComputerCPU"
            Me.LblInfoComputerCPU.Size = New System.Drawing.Size(32, 13)
            Me.LblInfoComputerCPU.TabIndex = 11
            Me.LblInfoComputerCPU.Text = Lr("CPU:")
            '
            'LblInfoComputerRAM
            '
            Me.LblInfoComputerRAM.AutoSize = True
            Me.LblInfoComputerRAM.Location = New System.Drawing.Point(6, 92)
            Me.LblInfoComputerRAM.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.LblInfoComputerRAM.Name = "LblInfoComputerRAM"
            Me.LblInfoComputerRAM.Size = New System.Drawing.Size(34, 13)
            Me.LblInfoComputerRAM.TabIndex = 10
            Me.LblInfoComputerRAM.Text = Lr("RAM:")
            '
            'LblInfoComputerComputerName
            '
            Me.LblInfoComputerComputerName.AutoSize = True
            Me.LblInfoComputerComputerName.Location = New System.Drawing.Point(6, 62)
            Me.LblInfoComputerComputerName.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.LblInfoComputerComputerName.Name = "LblInfoComputerComputerName"
            Me.LblInfoComputerComputerName.Size = New System.Drawing.Size(86, 13)
            Me.LblInfoComputerComputerName.TabIndex = 9
            Me.LblInfoComputerComputerName.Text = Lr("Computer Name:")
            Me.MainToolTip.SetToolTip(Me.LblInfoComputerComputerName, "The name of your computer inside your network")
            '
            'GBOptionsInfoAbout
            '
            Me.GBOptionsInfoAbout.Controls.Add(Me.ALlblInfoAppWeb)
            Me.GBOptionsInfoAbout.Controls.Add(Me.BtnInfoAppUpdater)
            Me.GBOptionsInfoAbout.Controls.Add(Me.lblInfoAppLatest)
            Me.GBOptionsInfoAbout.Controls.Add(Me.lblInfoAppCopyright)
            Me.GBOptionsInfoAbout.Controls.Add(Me.lblInfoAppVersion)
            Me.GBOptionsInfoAbout.Controls.Add(Me.lblInfoAppAuthors)
            Me.GBOptionsInfoAbout.Controls.Add(Me.lblInfoAppName)
            Me.GBOptionsInfoAbout.Location = New System.Drawing.Point(9, 6)
            Me.GBOptionsInfoAbout.Name = "GBOptionsInfoAbout"
            Me.GBOptionsInfoAbout.Size = New System.Drawing.Size(280, 150)
            Me.GBOptionsInfoAbout.TabIndex = 0
            Me.GBOptionsInfoAbout.TabStop = False
            Me.GBOptionsInfoAbout.Text = Lr("About")
            '
            'ALlblInfoAppWeb
            '
            Me.ALlblInfoAppWeb.AutoSize = True
            Me.ALlblInfoAppWeb.Location = New System.Drawing.Point(6, 77)
            Me.ALlblInfoAppWeb.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.ALlblInfoAppWeb.Name = "ALlblInfoAppWeb"
            Me.ALlblInfoAppWeb.Size = New System.Drawing.Size(33, 13)
            Me.ALlblInfoAppWeb.TabIndex = 6
            Me.ALlblInfoAppWeb.TabStop = True
            Me.ALlblInfoAppWeb.Text = Lr("Web:")
            '
            'BtnInfoAppUpdater
            '
            Me.BtnInfoAppUpdater.Location = New System.Drawing.Point(6, 121)
            Me.BtnInfoAppUpdater.Name = "BtnInfoAppUpdater"
            Me.BtnInfoAppUpdater.Size = New System.Drawing.Size(268, 23)
            Me.BtnInfoAppUpdater.TabIndex = 5
            Me.BtnInfoAppUpdater.Text = Lr("Open Updater")
            Me.BtnInfoAppUpdater.UseVisualStyleBackColor = True
            '
            'lblInfoAppLatest
            '
            Me.lblInfoAppLatest.AutoSize = True
            Me.lblInfoAppLatest.Location = New System.Drawing.Point(6, 102)
            Me.lblInfoAppLatest.Name = "lblInfoAppLatest"
            Me.lblInfoAppLatest.Size = New System.Drawing.Size(76, 13)
            Me.lblInfoAppLatest.TabIndex = 4
            Me.lblInfoAppLatest.Text = Lr("Latest version:")
            '
            'lblInfoAppCopyright
            '
            Me.lblInfoAppCopyright.AutoSize = True
            Me.lblInfoAppCopyright.Location = New System.Drawing.Point(6, 62)
            Me.lblInfoAppCopyright.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.lblInfoAppCopyright.Name = "lblInfoAppCopyright"
            Me.lblInfoAppCopyright.Size = New System.Drawing.Size(54, 13)
            Me.lblInfoAppCopyright.TabIndex = 3
            Me.lblInfoAppCopyright.Text = Lr("Copyright:")
            '
            'lblInfoAppVersion
            '
            Me.lblInfoAppVersion.AutoSize = True
            Me.lblInfoAppVersion.Location = New System.Drawing.Point(6, 47)
            Me.lblInfoAppVersion.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.lblInfoAppVersion.Name = "lblInfoAppVersion"
            Me.lblInfoAppVersion.Size = New System.Drawing.Size(45, 13)
            Me.lblInfoAppVersion.TabIndex = 2
            Me.lblInfoAppVersion.Text = Lr("Version:")
            '
            'lblInfoAppAuthors
            '
            Me.lblInfoAppAuthors.AutoSize = True
            Me.lblInfoAppAuthors.Location = New System.Drawing.Point(6, 32)
            Me.lblInfoAppAuthors.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.lblInfoAppAuthors.Name = "lblInfoAppAuthors"
            Me.lblInfoAppAuthors.Size = New System.Drawing.Size(46, 13)
            Me.lblInfoAppAuthors.TabIndex = 1
            Me.lblInfoAppAuthors.Text = Lr("Authors:")
            '
            'lblInfoAppName
            '
            Me.lblInfoAppName.AutoSize = True
            Me.lblInfoAppName.Location = New System.Drawing.Point(6, 17)
            Me.lblInfoAppName.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
            Me.lblInfoAppName.Name = "lblInfoAppName"
            Me.lblInfoAppName.Size = New System.Drawing.Size(38, 13)
            Me.lblInfoAppName.TabIndex = 0
            Me.lblInfoAppName.Text = Lr("Name:")
            '
            'lblStatusBarServerState
            '
            Me.lblStatusBarServerState.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblStatusBarServerState.Location = New System.Drawing.Point(5, 544)
            Me.lblStatusBarServerState.MaximumSize = New System.Drawing.Size(120, 0)
            Me.lblStatusBarServerState.MinimumSize = New System.Drawing.Size(100, 0)
            Me.lblStatusBarServerState.Name = "lblStatusBarServerState"
            Me.lblStatusBarServerState.Size = New System.Drawing.Size(110, 13)
            Me.lblStatusBarServerState.TabIndex = 2
            '
            'LblStatusBarServerOutput
            '
            Me.LblStatusBarServerOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.LblStatusBarServerOutput.Location = New System.Drawing.Point(121, 544)
            Me.LblStatusBarServerOutput.MinimumSize = New System.Drawing.Size(200, 0)
            Me.LblStatusBarServerOutput.Name = "LblStatusBarServerOutput"
            Me.LblStatusBarServerOutput.Size = New System.Drawing.Size(467, 13)
            Me.LblStatusBarServerOutput.TabIndex = 3
            '
            'ErrProv
            '
            Me.ErrProv.ContainerControl = Me
            '
            'Tray
            '
            Me.Tray.ContextMenuStrip = Me.CmenuTray
            Me.Tray.Icon = CType(resources.GetObject("Tray.Icon"), System.Drawing.Icon)
            Me.Tray.Text = Lr("BukkitGUI")
            Me.Tray.Visible = True
            '
            'CmenuTray
            '
            Me.CmenuTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnCmenuTrayExit})
            Me.CmenuTray.Name = "CmenuTray"
            Me.CmenuTray.Size = New System.Drawing.Size(93, 26)
            '
            'BtnCmenuTrayExit
            '
            Me.BtnCmenuTrayExit.Image = CType(resources.GetObject("BtnCmenuTrayExit.Image"), System.Drawing.Image)
            Me.BtnCmenuTrayExit.Name = "BtnCmenuTrayExit"
            Me.BtnCmenuTrayExit.Size = New System.Drawing.Size(92, 22)
            Me.BtnCmenuTrayExit.Text = Lr("Exit")
            '
            'lblInfo2
            '
            Me.lblInfo2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblInfo2.AutoSize = True
            Me.lblInfo2.Location = New System.Drawing.Point(592, 544)
            Me.lblInfo2.Name = "lblInfo2"
            Me.lblInfo2.Size = New System.Drawing.Size(227, 13)
            Me.lblInfo2.TabIndex = 6
            Me.lblInfo2.Text = Lr("BukkitGui v2 is available! visit get.bertware.net")
            '
            'Mainform
            '
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(834, 562)
            Me.Controls.Add(Me.lblInfo2)
            Me.Controls.Add(Me.LblStatusBarServerOutput)
            Me.Controls.Add(Me.lblStatusBarServerState)
            Me.Controls.Add(Me.TabCtrlMain)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "Mainform"
            Me.Text = Lr("BukkitGUI")
            Me.GBGeneralGeneral.ResumeLayout(False)
            Me.SplitGeneral.Panel1.ResumeLayout(False)
            Me.SplitGeneral.Panel2.ResumeLayout(False)
            Me.SplitGeneral.Panel2.PerformLayout()
            Me.SplitGeneral.ResumeLayout(False)
            Me.CmenuPlayerList.ResumeLayout(False)
            Me.TabCtrlMain.ResumeLayout(False)
            Me.TabGeneral.ResumeLayout(False)
            Me.GbGeneralInfo.ResumeLayout(False)
            Me.GbGeneralInfo.PerformLayout()
            Me.PanelPerformanceInfo.ResumeLayout(False)
            Me.PanelPerformanceInfo.PerformLayout()
            Me.TabPlayers.ResumeLayout(False)
            Me.TabPlayers.PerformLayout()
            CType(Me.TBPlayersPlayersView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabSuperStart.ResumeLayout(False)
            Me.GBSuperStartMaintainance.ResumeLayout(False)
            Me.GBSuperStartMaintainance.PerformLayout()
            CType(Me.NumSuperstartCustomBuild, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PBSuperStartServerIcon, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GBSuperStartRemoteServer.ResumeLayout(False)
            Me.GBSuperStartRemoteServer.PerformLayout()
            CType(Me.NumSuperstartRemotePort, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GBSuperstartJavaServer.ResumeLayout(False)
            Me.GBSuperstartJavaServer.PerformLayout()
            CType(Me.TBSuperstartJavaMaxRam, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.TBSuperstartJavaMinRam, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabErrorLogging.ResumeLayout(False)
            Me.TabTaskManager.ResumeLayout(False)
            Me.TabPlugins.ResumeLayout(False)
            Me.TabCtrlPlugins.ResumeLayout(False)
            Me.TabPluginsInstall.ResumeLayout(False)
            Me.TabPluginsInstall.PerformLayout()
            Me.CMenuBukGetPlugins.ResumeLayout(False)
            Me.TabPluginsInstalled.ResumeLayout(False)
            Me.TabPluginsInstalled.PerformLayout()
            Me.CMenuInstalledPlugins.ResumeLayout(False)
            Me.TabServerOptions.ResumeLayout(False)
            Me.TabServerOptions.PerformLayout()
            Me.CmenuServerLists.ResumeLayout(False)
            Me.CmenuServerSettings.ResumeLayout(False)
            Me.TabBackups.ResumeLayout(False)
            Me.TabOptionsInfo.ResumeLayout(False)
            Me.GBInfoSettings.ResumeLayout(False)
            Me.GBInfoSettings.PerformLayout()
            Me.GBOptionsInfoSound.ResumeLayout(False)
            Me.GBOptionsInfoSound.PerformLayout()
            Me.GBOptionsInfoAboutTray.ResumeLayout(False)
            Me.GBOptionsInfoAboutTray.PerformLayout()
            Me.GBOptionsInfoAboutText.ResumeLayout(False)
            Me.GBOptionsInfoAboutText.PerformLayout()
            CType(Me.NumTextOptionsFontSize, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GBOptionsInfoAboutComputerInfo.ResumeLayout(False)
            Me.GBOptionsInfoAboutComputerInfo.PerformLayout()
            Me.GBOptionsInfoAbout.ResumeLayout(False)
            Me.GBOptionsInfoAbout.PerformLayout()
            CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).EndInit()
            Me.CmenuTray.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents GBGeneralGeneral As System.Windows.Forms.GroupBox
        Friend WithEvents ChkGeneralSay As System.Windows.Forms.CheckBox
        Friend WithEvents TabCtrlMain As System.Windows.Forms.TabControl
        Friend WithEvents TabGeneral As System.Windows.Forms.TabPage
        Friend WithEvents TabPlayers As System.Windows.Forms.TabPage
        Friend WithEvents BtnGeneralSendCmd As System.Windows.Forms.Button
        Friend WithEvents GbGeneralInfo As System.Windows.Forms.GroupBox
        Friend WithEvents ARTXTServerOutput As Net.Bertware.Controls.AdvancedRichTextBox
        Friend WithEvents BtnGeneralKill As System.Windows.Forms.Button
        Friend WithEvents BtnGeneralReload As System.Windows.Forms.Button
        Friend WithEvents BtnGeneralRestart As System.Windows.Forms.Button
        Friend WithEvents BtnGeneralStartStop As System.Windows.Forms.Button
        Friend WithEvents TabSuperStart As System.Windows.Forms.TabPage
        Friend WithEvents GBSuperStartRemoteServer As System.Windows.Forms.GroupBox
        Friend WithEvents GBSuperstartJavaServer As System.Windows.Forms.GroupBox
        Friend WithEvents CBSuperstartServerType As System.Windows.Forms.ComboBox
        Friend WithEvents lblSuperStartType As System.Windows.Forms.Label
        Friend WithEvents MainToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents GBSuperStartMaintainance As System.Windows.Forms.GroupBox
        Friend WithEvents BtnSuperStartLaunch As System.Windows.Forms.Button
        Friend WithEvents llblSuperStartsite As Net.Bertware.Controls.AdvancedLinkLabel
        Friend WithEvents lblSuperStartLatestDev As System.Windows.Forms.Label
        Friend WithEvents lblSuperStartLatestBeta As System.Windows.Forms.Label
        Friend WithEvents lblSuperStartLatestStable As System.Windows.Forms.Label
        Friend WithEvents BtnSuperStartDownloadCustomBuild As System.Windows.Forms.Button
        Friend WithEvents BtnSuperStartDownloadDev As System.Windows.Forms.Button
        Friend WithEvents BtnSuperStartDownloadBeta As System.Windows.Forms.Button
        Friend WithEvents BtnSuperStartDownloadRecommended As System.Windows.Forms.Button
        Friend WithEvents PBSuperStartServerIcon As System.Windows.Forms.PictureBox
        Friend WithEvents TxtSuperstartJavaCustomArgs As System.Windows.Forms.TextBox
        Friend WithEvents TxtSuperstartJavaJarFile As System.Windows.Forms.TextBox
        Friend WithEvents lblSuperStartCustomArg As System.Windows.Forms.Label
        Friend WithEvents lblSuperStartJarFile As System.Windows.Forms.Label
        Friend WithEvents lblSuperStartMaxRam As System.Windows.Forms.Label
        Friend WithEvents LblSuperStartMinRam As System.Windows.Forms.Label
        Friend WithEvents lblSuperStartJavaVersion As System.Windows.Forms.Label
        Friend WithEvents CBSuperstartJavaJRE As System.Windows.Forms.ComboBox
        Friend WithEvents TBSuperstartJavaMaxRam As System.Windows.Forms.TrackBar
        Friend WithEvents TBSuperstartJavaMinRam As System.Windows.Forms.TrackBar
        Friend WithEvents lblGeneralRAMTotal As System.Windows.Forms.Label
        Friend WithEvents lblGeneralRAMServer As System.Windows.Forms.Label
        Friend WithEvents lblGeneralRAMGUI As System.Windows.Forms.Label
        Friend WithEvents lblGeneralCPUTotal As System.Windows.Forms.Label
        Friend WithEvents lblGeneralCPUServer As System.Windows.Forms.Label
        Friend WithEvents lblGeneralCPUGUI As System.Windows.Forms.Label
        Friend WithEvents PBGeneralCPUTotal As Net.Bertware.Controls.VistaProgressBar
        Friend WithEvents PBGeneralCPUServer As Net.Bertware.Controls.VistaProgressBar
        Friend WithEvents PBGeneralRAMTotal As Net.Bertware.Controls.VistaProgressBar
        Friend WithEvents PBGeneralRAMServer As Net.Bertware.Controls.VistaProgressBar
        Friend WithEvents PBGeneralRAMGUI As Net.Bertware.Controls.VistaProgressBar
        Friend WithEvents ALVGeneralPlayers As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents lblGeneralTimeSinceStartText As System.Windows.Forms.Label
        Friend WithEvents ALVPlayersPlayers As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColPlayersPlayersName As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColPlayersPlayersIP As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColPlayersPlayersLocation As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColPlayersPlayersTJoined As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColPlayersPlayersWhitelist As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColPlayersPlayersOP As System.Windows.Forms.ColumnHeader
        Friend WithEvents lblStatusBarServerState As System.Windows.Forms.Label
        Friend WithEvents LblStatusBarServerOutput As System.Windows.Forms.Label
        Friend WithEvents LblPlayersViewMode As System.Windows.Forms.Label
        Friend WithEvents TBPlayersPlayersView As System.Windows.Forms.TrackBar
        Friend WithEvents ImgListPlayerAvatars As System.Windows.Forms.ImageList
        Friend WithEvents ErrProv As System.Windows.Forms.ErrorProvider
        Friend WithEvents GeneralColName As System.Windows.Forms.ColumnHeader
        Friend WithEvents lblGeneralRAMServerValue As System.Windows.Forms.Label
        Friend WithEvents lblGeneralRAMGUIValue As System.Windows.Forms.Label
        Friend WithEvents lblGeneralRAMTotalValue As System.Windows.Forms.Label
        Friend WithEvents lblGeneralCPUTotalValue As System.Windows.Forms.Label
        Friend WithEvents lblGeneralCPUServerValue As System.Windows.Forms.Label
        Friend WithEvents lblGeneralCPUGUIValue As System.Windows.Forms.Label
        Friend WithEvents TabPlugins As System.Windows.Forms.TabPage
        Friend WithEvents TabCtrlPlugins As System.Windows.Forms.TabControl
        Friend WithEvents TabPluginsInstall As System.Windows.Forms.TabPage
        Friend WithEvents TabPluginsInstalled As System.Windows.Forms.TabPage
        Friend WithEvents ALVInstalledPlugins As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColName As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColVersion As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColAuthor As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColDescription As System.Windows.Forms.ColumnHeader
        Friend WithEvents lblInstallPluginsCategory As System.Windows.Forms.Label
        Friend WithEvents LblInstallPluginsFilter As System.Windows.Forms.Label
        Friend WithEvents CBInstallPluginsCategory As System.Windows.Forms.ComboBox
        Friend WithEvents TxtInstallPluginsFilter As System.Windows.Forms.TextBox
        Friend WithEvents ALVBukGetPlugins As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColBukgetPluginName As System.Windows.Forms.ColumnHeader
        Friend WithEvents CMenuInstalledPlugins As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents CmenuInstalledPluginsMoreInfo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CmenuInstalledPluginsUpdate As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CmenuInstalledPluginsProjectPage As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CmenuInstalledPluginsRemove As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CMenuBukGetPlugins As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents CmenuInstalledPluginsRefresh As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TabOptionsInfo As System.Windows.Forms.TabPage
        Friend WithEvents GBOptionsInfoSound As System.Windows.Forms.GroupBox
        Friend WithEvents GBOptionsInfoAboutTray As System.Windows.Forms.GroupBox
        Friend WithEvents GBOptionsInfoAboutText As System.Windows.Forms.GroupBox
        Friend WithEvents GBOptionsInfoAboutComputerInfo As System.Windows.Forms.GroupBox
        Friend WithEvents GBOptionsInfoAbout As System.Windows.Forms.GroupBox
        Friend WithEvents BtnInfoAppUpdater As System.Windows.Forms.Button
        Friend WithEvents lblInfoAppLatest As System.Windows.Forms.Label
        Friend WithEvents lblInfoAppCopyright As System.Windows.Forms.Label
        Friend WithEvents lblInfoAppVersion As System.Windows.Forms.Label
        Friend WithEvents lblInfoAppAuthors As System.Windows.Forms.Label
        Friend WithEvents lblInfoAppName As System.Windows.Forms.Label
        Friend WithEvents ALlblInfoAppWeb As Net.Bertware.Controls.AdvancedLinkLabel
        Friend WithEvents LblInfoComputerOS As System.Windows.Forms.Label
        Friend WithEvents LblInfoComputerCPU As System.Windows.Forms.Label
        Friend WithEvents LblInfoComputerRAM As System.Windows.Forms.Label
        Friend WithEvents LblInfoComputerComputerName As System.Windows.Forms.Label
        Friend WithEvents ChkInfoTrayOnWarning As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoTrayOnLeave As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoTrayOnJoin As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoTrayOnSevere As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoTrayAlways As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoTrayMinimize As System.Windows.Forms.CheckBox
        Friend WithEvents LblInstallPluginsInfo As System.Windows.Forms.Label
        Friend WithEvents lblinstalledpluginsInfo As System.Windows.Forms.Label
        Friend WithEvents ImgListTabIcons As System.Windows.Forms.ImageList
        Friend WithEvents GBInfoSettings As System.Windows.Forms.GroupBox
        Friend WithEvents lblOptionsConfigLocation As System.Windows.Forms.Label
        Friend WithEvents CBInfoSettingsFileLocation As System.Windows.Forms.ComboBox
        Friend WithEvents ChkRunServerOnGUIStart As System.Windows.Forms.CheckBox
        Friend WithEvents lblInfoSettingsLanguage As System.Windows.Forms.Label
        Friend WithEvents CBInfoSettingsLanguage As System.Windows.Forms.ComboBox
        Friend WithEvents btnCMenuBukGetPluginsMoreInfo As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuBukGetPluginsInstallPlugin As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuBukGetPluginsProjectPage As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TabTaskManager As System.Windows.Forms.TabPage
        Friend WithEvents BtnTaskManagerAdd As System.Windows.Forms.Button
        Friend WithEvents BtnTaskManagerEdit As System.Windows.Forms.Button
        Friend WithEvents ALVTaskPlanner As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents BtnTaskManagerRemove As System.Windows.Forms.Button
        Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
        Friend WithEvents CmenuTray As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents BtnCmenuTrayExit As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ChkShowDate As System.Windows.Forms.CheckBox
        Friend WithEvents ChkShowTime As System.Windows.Forms.CheckBox
        Friend WithEvents ALVTaskPlannerColName As System.Windows.Forms.ColumnHeader
        Friend WithEvents ALVTaskPlannerColTriggerType As System.Windows.Forms.ColumnHeader
        Friend WithEvents ALVTaskPlannerColTriggerParam As System.Windows.Forms.ColumnHeader
        Friend WithEvents ALVTaskPlannerColActionType As System.Windows.Forms.ColumnHeader
        Friend WithEvents ALVTaskPlannerColActionParam As System.Windows.Forms.ColumnHeader
        Friend WithEvents CmenuInstalledPluginsViewVersions As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnTaskManagerImport As System.Windows.Forms.Button
        Friend WithEvents BtnTaskManagerExport As System.Windows.Forms.Button
        Friend WithEvents CmenuPlayerList As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents BtnCMenuPlayerListOp As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuPlayerListDeop As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuPlayerListKick As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuPlayerListBan As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuPlayerListGive As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TabErrorLogging As System.Windows.Forms.TabPage
        Friend WithEvents TabServerOptions As System.Windows.Forms.TabPage
        Friend WithEvents lblSettingsTabPages As System.Windows.Forms.Label
        Friend WithEvents ChkOptionsTabServeroptions As System.Windows.Forms.CheckBox
        Friend WithEvents ChkOptionsTabPlugins As System.Windows.Forms.CheckBox
        Friend WithEvents ChkOptionsTabTaskManager As System.Windows.Forms.CheckBox
        Friend WithEvents ChkOptionsTaberrors As System.Windows.Forms.CheckBox
        Friend WithEvents ChkOptionsTabplayers As System.Windows.Forms.CheckBox
        Friend WithEvents LbloptionsRestartRequiredInfo As System.Windows.Forms.Label
        Friend WithEvents SplitGeneral As System.Windows.Forms.SplitContainer
        Friend WithEvents ImgListErrorManager As System.Windows.Forms.ImageList
        Friend WithEvents ALVErrors As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColErrorID As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColErrorType As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColErrorMsg As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColErrorTime As System.Windows.Forms.ColumnHeader
        Friend WithEvents BtnErrorLoggingDetails As System.Windows.Forms.Button
        Friend WithEvents MTxtSuperstartRemotePassword As System.Windows.Forms.MaskedTextBox
        Friend WithEvents MTxtSuperstartRemoteSalt As System.Windows.Forms.MaskedTextBox
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents NumSuperstartRemotePort As System.Windows.Forms.NumericUpDown
        Friend WithEvents TxtSuperstartRemoteUsername As System.Windows.Forms.TextBox
        Friend WithEvents TxtSuperstartRemoteHost As System.Windows.Forms.TextBox
        Friend WithEvents PBGeneralCPUGUI As Net.Bertware.Controls.VistaProgressBar
        Friend WithEvents LblSettingsTextFontPreview As System.Windows.Forms.Label
        Friend WithEvents TxtSettingsTextColorUnknown As System.Windows.Forms.TextBox
        Friend WithEvents LblSettingsTextColorUnknown As System.Windows.Forms.Label
        Friend WithEvents TxtSettingsTextColorSevere As System.Windows.Forms.TextBox
        Friend WithEvents LblSettingsTextColorSevere As System.Windows.Forms.Label
        Friend WithEvents TxtSettingsTextColorWarning As System.Windows.Forms.TextBox
        Friend WithEvents LblSettingsTextColorWarning As System.Windows.Forms.Label
        Friend WithEvents TxtSettingsTextColorPlayer As System.Windows.Forms.TextBox
        Friend WithEvents LblSettingsTextColorPlayer As System.Windows.Forms.Label
        Friend WithEvents TxtSettingsTextColorInfo As System.Windows.Forms.TextBox
        Friend WithEvents LblSettingsTextColorInfo As System.Windows.Forms.Label
        Friend WithEvents LblTextSettingsFontSize As System.Windows.Forms.Label
        Friend WithEvents LblTextSettingsFont As System.Windows.Forms.Label
        Friend WithEvents NumTextOptionsFontSize As System.Windows.Forms.NumericUpDown
        Friend WithEvents TxtSettingsTextFontPreview As System.Windows.Forms.TextBox
        Friend WithEvents BtnSuperstartJavaJarFileBrowse As System.Windows.Forms.Button
        Friend WithEvents ALVServerSettings As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ALVServerSettingsWhiteList As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ALVServerSettingsOPs As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ALVServerSettingsBannedPlayer As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ALVServerSettingsBannedIP As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColServerSettingsName As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColServerSettingsValue As System.Windows.Forms.ColumnHeader
        Friend WithEvents CmenuServerLists As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents CmenuServerSettings As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents BtnCmenuServerListsRemove As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnServerSettingsAddIPBan As System.Windows.Forms.Button
        Friend WithEvents BtnServerSettingsAddPlayerBan As System.Windows.Forms.Button
        Friend WithEvents BtnServerSettingsAddOP As System.Windows.Forms.Button
        Friend WithEvents BtnServerSettingsAddWhitelist As System.Windows.Forms.Button
        Friend WithEvents ColServerSettingsBannedIp As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColServerSettingsBannedPlayers As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColServerSettingsOps As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColServerSettingsWhitelist As System.Windows.Forms.ColumnHeader
        Friend WithEvents BtnCmenuServerSettingsAdd As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCmenuServerSettingsEdit As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCmenuServerSettingsRemove As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents BtnCmenuServerListRefresh As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCmenuServerSettingsRefresh As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents TxtServerSettingsAddWhitelist As Net.Bertware.Controls.AdvancedTextBox
        Friend WithEvents TxtServerSettingsAddIPBan As Net.Bertware.Controls.AdvancedTextBox
        Friend WithEvents TxtServerSettingsAddPlayerBan As Net.Bertware.Controls.AdvancedTextBox
        Friend WithEvents TxtServerSettingsAddOP As Net.Bertware.Controls.AdvancedTextBox
        Friend WithEvents ChkInfoSoundOnSevere As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoSoundOnWarning As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoSoundOnLeave As System.Windows.Forms.CheckBox
        Friend WithEvents ChkInfoSoundOnJoin As System.Windows.Forms.CheckBox
        Friend WithEvents TabBackups As System.Windows.Forms.TabPage
        Friend WithEvents BtnBackupExecute As System.Windows.Forms.Button
        Friend WithEvents BtnBackupImport As System.Windows.Forms.Button
        Friend WithEvents BtnBackupExport As System.Windows.Forms.Button
        Friend WithEvents BtnBackupAdd As System.Windows.Forms.Button
        Friend WithEvents BtnBackupEdit As System.Windows.Forms.Button
        Friend WithEvents ALVBackups As Net.Bertware.Controls.AdvancedListView
        Friend WithEvents ColBackupName As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColBackupFolders As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColBackupDestination As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColBackupCompression As System.Windows.Forms.ColumnHeader
        Friend WithEvents BtnBackupRemove As System.Windows.Forms.Button
        Friend WithEvents LblDonateInfo As System.Windows.Forms.Label
        Friend WithEvents BtnFeedback As System.Windows.Forms.Button
        Friend WithEvents BtnCmenuPlayerListRefresh As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ChkOptionsCheckUpdates As System.Windows.Forms.CheckBox
        Friend WithEvents BtnOptionsResetAll As System.Windows.Forms.Button
        Friend WithEvents TxtSuperstartJavaCustomSwitch As System.Windows.Forms.TextBox
        Friend WithEvents LblSuperStartCustomSwitches As System.Windows.Forms.Label
        Friend WithEvents ChkErrorsHideStackTrace As System.Windows.Forms.CheckBox
        Friend WithEvents ChkErrorsHideError As System.Windows.Forms.CheckBox
        Friend WithEvents ChkErrorsHideWarning As System.Windows.Forms.CheckBox
        Friend WithEvents ChkSuperstartAutoUpdateNotify As System.Windows.Forms.CheckBox
        Friend WithEvents ChkSuperStartRetrieveCurrent As System.Windows.Forms.CheckBox
        Friend WithEvents BtnCMenuPlayerListGameMode As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuPlayerListGamemodeSurvival As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnCMenuPlayerListGamemodeCreative As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnSuperStartGetCurrent As System.Windows.Forms.Button
        Friend WithEvents CBTextOptionsFont As FontCombo.FontComboBox
        Friend WithEvents ColBukgetPluginDescription As System.Windows.Forms.ColumnHeader
        Friend WithEvents BtnCMenuBukGetPluginsRefresh As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ColUpdateDate As System.Windows.Forms.ColumnHeader
        Friend WithEvents BtnTaskManagerTest As System.Windows.Forms.Button
        Friend WithEvents BtnCMenuPlayerListGamemodeAdventure As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents CmenuInstalledPluginsOpenFolder As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents BtnGeneralClearOutput As System.Windows.Forms.Button
        Friend WithEvents ALVTaskPlannerColisEnabled As System.Windows.Forms.ColumnHeader
        Friend WithEvents ChkAutoStartWindows As System.Windows.Forms.CheckBox
        Friend WithEvents ChkOptionsLightMode As System.Windows.Forms.CheckBox
        Friend WithEvents PanelPerformanceInfo As System.Windows.Forms.Panel
        Friend WithEvents ChkSuperstartAutoUpdate As System.Windows.Forms.CheckBox
        Friend WithEvents ColBukgetPluginVersion As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColBukgetPluginBukkitVersion As System.Windows.Forms.ColumnHeader
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents NumSuperstartCustomBuild As System.Windows.Forms.NumericUpDown
        Friend WithEvents LblInstallPluginsLoading As System.Windows.Forms.Label
        Friend WithEvents BtnErrorLoggingCopy As System.Windows.Forms.Button
        Friend WithEvents LblGeneralTimeSinceStartValue As System.Windows.Forms.Label
        Friend WithEvents ChkTextUTF8 As System.Windows.Forms.CheckBox
        Friend WithEvents LblInfoComputerLocIP As System.Windows.Forms.LinkLabel
        Friend WithEvents LblInfoComputerExtIP As System.Windows.Forms.LinkLabel
        Friend WithEvents BtnInstallPluginsSearch As System.Windows.Forms.Button
        Friend WithEvents TxtGeneralServerIn As System.Windows.Forms.TextBox
        Friend WithEvents BtnBrowseOutput As System.Windows.Forms.Button
        Friend WithEvents BtnSuperStartPortForwarding As System.Windows.Forms.Button
        Friend WithEvents lblInfo2 As System.Windows.Forms.Label

    End Class

End Namespace
