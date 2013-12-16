Imports System.IO
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities
Imports Net.Bertware.BukkitGUI.MCInterop

Imports System.Threading

Namespace MCInterop
    Module InstalledPluginManager
        ''' <summary>
        ''' Dictionarry of plugins, filename as key, descriptor as value
        ''' </summary>
        ''' <remarks></remarks>
        Public plugins As IDictionary(Of String, plugindescriptor)

        Public Event InstalledPluginsLoaded_Base(list As Dictionary(Of String, plugindescriptor))
        Public Event InstalledPluginsLoaded_Full(list As Dictionary(Of String, plugindescriptor))

        Public plugin_dir As String = common.Server_root & "\plugins"

        Public ReadOnly Property loaded As Boolean
            Get
                Return (plugins IsNot Nothing AndAlso plugins.Count > 0)
            End Get
        End Property

        Public Sub InitAsync()
            Dim t As New Thread(AddressOf init)
            t.SetApartmentState(ApartmentState.MTA)
            t.IsBackground = True
            t.Name = "InstalledPlugins_init"
            t.Start()

        End Sub

      

        Public Sub init()
            livebug.write(loggingLevel.Fine, "Pluginmanager", "Initializing / Loading")

            plugin_dir = common.Server_root & "/plugins" 'plugins should be in this folder

            If Not FileIO.FileSystem.DirectoryExists(plugin_dir) Then 'if not exists, create or exit
                Try
                    FileIO.FileSystem.CreateDirectory(plugin_dir)
                Catch secex As UnauthorizedAccessException
                    MessageBox.Show(lr("Access to to the following folder was denied.") & plugin_dir & vbCrLf & lr("Please run this app as administrator to ensure proper working"), lr("Access to folder denied"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    livebug.write(loggingLevel.Severe, "pluginmanager", "Could not create folder due to insufficient rights: " & plugin_dir, secex.Message)
                Catch ex As Exception
                    livebug.write(loggingLevel.Severe, "pluginmanager", "Could not create folder: " & plugin_dir, ex.Message)
                End Try

            End If

            'create parent elements in the config
            config.write("pluginmanager", "", "")
            config.write("installedplugins", "", "pluginmanager")
            config.write("installplugins", "", "pluginmanager")


            'Create a list of simple plugin items to start
            CreateSimpleList()

            livebug.write(loggingLevel.Fine, "pluginmanager", "Loaded base list: " & plugins.Count & " plugins loaded", "pluginmanager")

            If plugins.Count > 0 Then 'if we found plugins, get the details (async!)
                Dim t As New Thread(AddressOf CreateDetailledList)
                t.IsBackground = True
                t.Name = "InstalledPlugins_CreateDetailledList"
                t.Start()
            Else
                livebug.write(loggingLevel.Fine, "pluginmanager", "No plugins in base list, detailled list creation cancelled")
            End If
        End Sub

#Region "listing"
        ''' <summary>
        ''' Create a simple list of the plugin in the plugin folder. Fast, but lacks details.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateSimpleList()
            Dim pluginfiles() As FileInfo = New DirectoryInfo(plugin_dir).GetFiles

            If pluginfiles Is Nothing OrElse pluginfiles.Length < 1 Then
                livebug.write(loggingLevel.Warning, "pluginmanager", "Cancelled simple list creation: no plugins")
                Exit Sub
            End If

            plugins = New Dictionary(Of String, plugindescriptor) 'create dictionary

            For i As UInt16 = 0 To pluginfiles.Length - 1 'load all the plugins in the dictionary
                Try
                    If pluginfiles(i).Extension = ".jar" Then
                        Dim pld As New plugindescriptor
                        pld.filename = pluginfiles(i).Name
                        pld.name = pluginfiles(i).Name
                        pld.FileCreationDate = IO.File.GetLastWriteTime(pluginfiles(i).Name)
                        plugins.Add(pluginfiles(i).Name, pld)
                    End If
                Catch ex As Exception
                    If pluginfiles(i) IsNot Nothing Then livebug.write(loggingLevel.Warning, "InstalledPlugins", "Couldn't add plugin to plugin list:" & pluginfiles(i).Name, ex.Message) Else livebug.write(loggingLevel.Warning, "InstalledPlugins", "Couldn't add plugin to plugin list", ex.Message)
                End Try
            Next

            RaiseEvent InstalledPluginsLoaded_Base(plugins) 'we got the first list now
        End Sub

        ''' <summary>
        ''' Create a plugin list based upon the plugin.yml files, might take a while. Recommended to run async.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub CreateDetailledList()
            livebug.write(loggingLevel.Fine, "InstalledPlugins", "Loading full list", "Pluginmanager.InstalledPluginmanager.CreateDetailledList()")
            If plugins Is Nothing OrElse plugins.Count = 0 Then Exit Sub
            Try
                For i As UInt16 = 0 To plugins.Values.Count - 1
                    Try

                        livebug.write(loggingLevel.Fine, "InstalledPlugins", "Loading plugin " & i + 1 & " of " & plugins.Count & " : " & plugins.Keys(i))
                        Dim pld As New plugindescriptor
                        pld = pld.loadplugin(plugins.Keys(i))
                        If pld IsNot Nothing Then plugins(pld.filename) = pld
                        livebug.write(loggingLevel.Fine, "InstalledPlugins", "Loaded plugin " & i + 1 & " of " & plugins.Count & " : " & plugins.Keys(i))

                    Catch ex As Exception
                        If plugins.Keys(i) IsNot Nothing Then livebug.write(loggingLevel.Warning, "InstalledPlugins", "Plugin not loaded:" & plugins.Keys(i), ex.Message) Else livebug.write(loggingLevel.Warning, "InstalledPlugins", "Plugin not loaded: " & i, ex.Message)
                    End Try
                Next
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "InstalledPlugins", "Couldn't create detailled list", ex.Message)
            End Try

            RaiseEvent InstalledPluginsLoaded_Full(plugins)
        End Sub

        ''' <summary>
        ''' Clear the cache of plugin.yml files
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ClearPluginCache() 'see plugindescriptor for more cache code
            If FileIO.FileSystem.DirectoryExists(common.Cache_path & "/plugins/") Then FileIO.FileSystem.DeleteDirectory(common.Cache_path & "/plugins/", FileIO.DeleteDirectoryOption.DeleteAllContents)
        End Sub

        ''' <summary>
        ''' Refreshes list of all installed plugins
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RefreshAllInstalledPluginsAsync()
            Dim t As New Thread(AddressOf init)
            t.SetApartmentState(ApartmentState.MTA)
            t.IsBackground = True
            t.Name = "InstalledPlugins_Update"
            t.Start()
        End Sub

        ''' <summary>
        ''' Reloads plugin data for a single plugin
        ''' </summary>
        ''' <param name="path"></param>
        ''' <remarks></remarks>
        Public Sub ReloadSingleInstalledPluginAsync(path As String)
            livebug.write(loggingLevel.Fine, "InstalledPlugins", "updating plugin information:" & path)
            Dim pld As plugindescriptor = New plugindescriptor().loadplugin(path, False)
            livebug.write(loggingLevel.Fine, "InstalledPlugins", "updated plugin information:" & path)
            RefreshAllInstalledPluginsAsync()
        End Sub
#End Region

#Region "actions"
        ''' <summary>
        ''' DEPRECATED
        ''' Searches for an installed plugin based on the name. ONLY to be used for checking console output. DO NOT use this in the plugin manager
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="softcheck"></param>
        ''' <returns></returns>
        ''' <remarks>DEPRECTATED, only for console parsing</remarks>
        Public Function GetInstalledPluginByName(name As String, Optional ByVal softcheck As Boolean = False) As plugindescriptor
            Try
                If name Is Nothing Or name = "" Then Return Nothing : Exit Function
                If plugins Is Nothing OrElse plugins.Count < 1 Then Return Nothing : Exit Function

                Dim result As plugindescriptor = Nothing
                For Each plugin As plugindescriptor In plugins.Values
                    If plugin.name = name Then result = plugin
                Next

                If softcheck And result Is Nothing Then
                    For Each plugin As plugindescriptor In plugins.Values
                        If plugin.name.Contains(name) Or name.Contains(plugin.name) Then result = plugin
                    Next
                End If

                Return result
            Catch ex As Exception
                If name Is Nothing Then
                    livebug.write(loggingLevel.Severe, "InstalledPlugins", "Severe exception in GetPluginByName:" & ex.Message)
                Else
                    livebug.write(loggingLevel.Severe, "InstalledPlugins", "Severe exception in GetPluginByName for " & name, ex.Message)
                End If
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Update a plugin to its latest version
        ''' </summary>
        ''' <param name="plugin">the plugin to update</param>
        ''' <param name="UpdateLists">refresh the installed plugins list afterwards</param>
        ''' <param name="ShowUI">Show the UI, or do this in the background</param>
        ''' <remarks></remarks>
        Public Sub UpdatePluginToLatest(plugin As plugindescriptor, Optional ByVal UpdateLists As Boolean = True, Optional ByVal ShowUI As Boolean = False)
            InstallPluginByNamespace(plugin.main, plugin_dir.Trim("/") & "/" & plugin.filename, UpdateLists, ShowUI)
        End Sub

        ''' <summary>
        ''' Update a plugin to its latest version
        ''' </summary>
        ''' <param name="plugin">the plugin to update</param>
        ''' <param name="version">The version that should be installed</param>
        ''' <param name="UpdateLists">refresh the installed plugins list afterwards</param>
        ''' <param name="ShowUI">Show the UI, or do this in the background</param>
        ''' <remarks></remarks>
        Public Sub UpdatePluginToVersion(plugin As plugindescriptor, version As PluginVersion, Optional ByVal UpdateLists As Boolean = True, Optional ByVal ShowUI As Boolean = False)
            PluginInstaller.Install(version, plugin_dir.Trim("/") & "/" & plugin.filename, UpdateLists, ShowUI)
        End Sub

        ''' <summary>
        ''' Get the mainspace for an installed plugin
        ''' </summary>
        ''' <param name="name">The filename of the plugin</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetPluginMainspaceByFileName(name As String) As String
            Return plugins(name).main
        End Function

        ''' <summary>
        ''' Get an installed plugin, based on it's namespace
        ''' </summary>
        ''' <param name="plugin_namespace"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetInstalledPluginByNamespace(plugin_namespace As String) As plugindescriptor
            Dim result As plugindescriptor = Nothing
            For Each plg As plugindescriptor In plugins.Values
                If plugin_namespace.ToLower = (plg.main.ToLower) Then Return plg
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' Get an installed plugin, based upon it's filename
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetPluginByFileName(name As String) As plugindescriptor
            If plugins.ContainsKey(name) Then Return plugins(name) Else Return Nothing
        End Function

        ''' <summary>
        ''' Open the project page for an installed plugin, by providing the filename
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <remarks></remarks>
        Public Sub OpenProjectPageByFileName(filename As String)
            OpenProjectPageByNamespace(GetPluginMainspaceByFileName(filename))
        End Sub

        ''' <summary>
        ''' Remove an installed plugin. Will show prompts.
        ''' </summary>
        ''' <param name="pld">The plugin to remove</param>
        ''' <remarks>Will also search for config folders</remarks>
        Public Sub RemoveInstalledplugin(pld As plugindescriptor)
            If pld Is Nothing Then Exit Sub
            Try
                If MessageBox.Show(lr("Delete this plugin?") & " " & pld.filename, lr("Delete plugin?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub

                If server.running Then
                    If MessageBox.Show(lr("You need to stop the server in order to remove this plugin. Stop server?"), lr("Server stop required"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        ServerStopDialog.ShowDialog()
                    End If
                End If

                FileIO.FileSystem.DeleteFile(plugin_dir & "/" & pld.filename)
                For Each Dir As String In FileIO.FileSystem.GetDirectories(plugin_dir)
                    If Dir.Contains(pld.name) Then
                        If MessageBox.Show(lr("It seems that this folder was created by the plugin:") & vbCrLf & Dir & vbCrLf & lr("Remove this folder?"), lr("Remove folder?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            FileIO.FileSystem.DeleteDirectory(Dir, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                    End If
                Next

                If FileIO.FileSystem.DirectoryExists(common.Cache_path & "/plugins/" & pld.filename) Then FileIO.FileSystem.DeleteDirectory(common.Cache_path & "/plugins/" & pld.filename, FileIO.DeleteDirectoryOption.DeleteAllContents) 'delete cached data

                plugins.Remove(pld.filename)

                RaiseEvent InstalledPluginsLoaded_Full(plugins) 'refresh the UI, this plugin is removed

                MessageBox.Show(lr("Plugin succesfully removed!"), lr("Succes!"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(lr("Plugin removal failed. There might be some files left undeleted"), lr("Failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Open the plugins folder in explorer.exe
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ShowPluginsFolder()
            Try
                Process.Start("explorer.exe", New DirectoryInfo(plugin_dir).FullName)
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "InstalledPlugins", "Couldn't open plugins folder!", ex.Message)
            End Try
        End Sub
#End Region

    End Module
End Namespace

