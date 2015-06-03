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
Imports Jayrock.Json
Imports Jayrock.Json.Conversion
Imports Net.Bertware.BukkitGUI.Core

Namespace MCInterop
    Public Module BukGetAPI
        ' base api: http://api.bukget.org/3/
        
        ''' <summary>
        '''     Those are the info fields we want for a simpleBukgetPlugin, minimal data
        ''' </summary>
        ''' <remarks></remarks>
        Const FIELDS As String = "slug,plugin_name,description,versions.version,versions.game_versions,main"

        
        ''' <summary>
        '''     Those are the info fields we want for a BukgetPlugin object, including all data
        ''' </summary>
        ''' <remarks></remarks>
        Const FIELDS_ALL As String =
            "slug,plugin_name,server,server,categories,authors,webpage,dbo_page,description,versions.version,versions.md5,versions.filename,versions.link,versions.type,versions.download,versions.status,versions.game_versions,versions.date,versions.slug,versions.soft_dependencies,versions.hard_dependencies,main"

        Const API_BUKGET_BASE As String = "http://api.bukget.org/3/" 'Base address

        
        ''' <summary>
        '''     Get most popular plugins (list)
        ''' </summary>
        ''' <remarks></remarks>
        Const API_PLUGINLIST As String = API_BUKGET_BASE & "plugins?fields=" & FIELDS & "&sort=-popularity.weekly" _
        'Get a whole plugin list

        
        ''' <summary>
        '''     Get plugins in category. Append category
        ''' </summary>
        ''' <remarks></remarks>
        Const API_CATEGORY As String = API_BUKGET_BASE & "categories/" 'Category should be added

        
        ''' <summary>
        '''     URL to get plugin info. Append slug
        ''' </summary>
        ''' <remarks></remarks>
        Const API_PLUGIN As String = API_BUKGET_BASE & "plugins/bukkit/" 'Plugin name should be added

        
        ''' <summary>
        '''     url to search by namespace. Append namespace
        ''' </summary>
        ''' <remarks></remarks>
        Const API_SEARCHBYNAMESPACE As String = API_BUKGET_BASE & "search/main/=/"

        
        ''' <summary>
        '''     url to search by name. Append name
        ''' </summary>
        ''' <remarks></remarks>
        Const API_SEARCHBYNAME As String = API_BUKGET_BASE & "search/plugin_name/=/"

        
        ''' <summary>
        '''     url to search by name, without exact match. Append name
        ''' </summary>
        ''' <remarks></remarks>
        Const API_SEARCHBYNAMELIKE As String = API_BUKGET_BASE & "search/plugin_name/like/"

        
        ''' <summary>
        '''     url to search by namespace. Append namespace
        ''' </summary>
        ''' <remarks></remarks>
        Const API_POPULAR_SETSIZE As String = API_BUKGET_BASE & "plugins?fields=" & FIELDS &
                                              "&sort=-popularity.weekly&start=0&size="

        
        ''' <summary>
        '''     URL for top 20 most popular plugins.
        ''' </summary>
        ''' <remarks></remarks>
        Const API_POPULAR As String = API_POPULAR_SETSIZE & "20"

        Public pluginlist As List(Of SimpleBukgetPlugin)

        
        ''' <summary>
        '''     A list of plugins has been loaded
        ''' </summary>
        ''' <param name="e">list of the SimpleBukgetPlguins items</param>
        ''' <remarks>can be for category, most popular, all, or search result</remarks>
        Public Event PluginListLoaded(e As List(Of SimpleBukgetPlugin))

        Public IsPluginListLoaded As Boolean = False

        
        ''' <summary>
        '''     The plugin status on bukkitdev
        ''' </summary>
        ''' <remarks></remarks>
        Enum PluginStatus
            Planning
            Alpha
            Beta
            Release
            Mature
            Semi_normal
            Normal
        End Enum

#Region "Loading"

        Public Function GetPluginCategories() As List(Of String)
            Log(livebug.loggingLevel.Fine, "BukGetAPI", "Getting categories")
            Dim l As New List(Of String)
            Try
                Dim WebResult As String = downloadstring(API_CATEGORY) 'get from API
                If WebResult Is Nothing OrElse WebResult = "" OrElse WebResult = "null" Then
                    Return Nothing
                    Exit Function
                End If
                Dim jarr As JsonArray = JsonConvert.Import(WebResult)
                For i As UInt32 = 0 To jarr.Length - 1
                    Dim json As JsonObject = JsonConvert.Import(jarr.GetString(i))
                    l.Add(json("name"))
                Next

            Catch ex As Exception
                Log(loggingLevel.Warning, "BukGetAPI", "Couldn't load plugin categories", ex.Message)
            End Try
            Return l
        End Function

        Public Sub LoadMostPopularPluginsAsync(Optional ByVal amount As UInt16 = 20)
            Dim t As New Thread(AddressOf GetMostPopular)
            t.SetApartmentState(ApartmentState.MTA)
            t.Name = "BukgetAPI_GetPopularPlugins"
            t.IsBackground = True
            t.Start(amount)
        End Sub

        Public Function GetMostPopular(ByVal amount As UInt16) As List(Of SimpleBukgetPlugin) 'get a list of all plugins
            Try
                Dim WebResult As String = downloadstring(API_POPULAR_SETSIZE & amount)
                pluginlist = ParsePluginList(WebResult)

                If pluginlist Is Nothing OrElse pluginlist.Count = 0 Then
                    Log(loggingLevel.Fine, "BukGetAPI", "Retrying to get available plugins")
                    WebResult = downloadstring(API_POPULAR_SETSIZE & amount)
                    pluginlist = ParsePluginList(WebResult)
                End If

            Catch ex As Exception
                pluginlist = New List(Of SimpleBukgetPlugin)
                pluginlist.Add(New SimpleBukgetPlugin("", "Error while loading plugins. Internet available?"))
            End Try

            IsPluginListLoaded = True
            RaiseEvent PluginListLoaded(pluginlist)
            Log(loggingLevel.Fine, "BukGetAPI", "All plugins loaded")
            Return pluginlist
        End Function

        Public Sub LoadAllPluginsAsync()
            Dim t As New Thread(AddressOf GetAllPlugins)
            t.SetApartmentState(ApartmentState.MTA)
            t.Name = "BukgetAPI_GetAllPlugins"
            t.IsBackground = True
            t.Start()
        End Sub

        Public Function GetAllPlugins() As List(Of SimpleBukgetPlugin) 'get a list of all plugins
            Try
                Dim WebResult As String = downloadstring(API_PLUGINLIST)
                pluginlist = ParsePluginList(WebResult)

                If pluginlist Is Nothing OrElse pluginlist.Count = 0 Then
                    Log(loggingLevel.Fine, "BukGetAPI", "Retrying to get available plugins")
                    WebResult = downloadstring(API_PLUGINLIST)
                    pluginlist = ParsePluginList(WebResult)
                End If

            Catch ex As Exception
                pluginlist = New List(Of SimpleBukgetPlugin)
                pluginlist.Add(New SimpleBukgetPlugin("", "Error while loading plugins. Internet available?"))
            End Try

            IsPluginListLoaded = True
            RaiseEvent PluginListLoaded(pluginlist)
            Log(loggingLevel.Fine, "BukGetAPI", "All plugins loaded")
            Return pluginlist
        End Function

        Public Sub GetPluginsByCategoryAsync(ByVal category As String)
            Dim t As New Thread(AddressOf GetPluginsByCategory)
            t.SetApartmentState(ApartmentState.MTA)
            t.Name = "BukgetAPI_GetPopularPlugins"
            t.IsBackground = True
            t.Start(category)
        End Sub

        Public Function GetPluginsByCategory(category As String) As List(Of SimpleBukgetPlugin)
            Try
                Dim WebResult As String = downloadstring(API_CATEGORY & category)
                pluginlist = ParsePluginList(WebResult)
            Catch ex As Exception
                pluginlist.Add(New SimpleBukgetPlugin("", lr("Error while loading plugins. Internet available?")))
            End Try
            RaiseEvent PluginListLoaded(pluginlist)
            Return pluginlist
        End Function


        Public Sub GetPluginSearchOnlineAsync(name As String)
            Dim t As New Thread(AddressOf GetPluginSearchOnline)
            t.SetApartmentState(ApartmentState.MTA)
            t.Name = "BukgetAPI_SearchPlugins"
            t.IsBackground = True
            t.Start(name)
        End Sub

        Public Function GetPluginSearchOnline(name As String) As List(Of SimpleBukgetPlugin)
            Try
                Dim url = API_SEARCHBYNAMELIKE & name & "?fields=" & FIELDS

                Dim WebResult As String = downloadstring(url)
                pluginlist = ParsePluginList(WebResult)

                If pluginlist Is Nothing OrElse pluginlist.Count = 0 Then
                    Log(loggingLevel.Fine, "BukGetAPI", "Retrying to get available plugins")
                    WebResult = downloadstring(url)
                    pluginlist = ParsePluginList(WebResult)
                End If

            Catch ex As Exception
                pluginlist = New List(Of SimpleBukgetPlugin)
                pluginlist.Add(New SimpleBukgetPlugin("", "Error while loading plugins. Internet available?"))
            End Try
            If pluginlist.Count < 1 Then
                pluginlist.Add(New SimpleBukgetPlugin("", "No results"))
                pluginlist.Add(New SimpleBukgetPlugin("", "Try another search"))
            End If
            IsPluginListLoaded = True
            RaiseEvent PluginListLoaded(pluginlist)
            Log(loggingLevel.Fine, "BukGetAPI", "All plugins loaded")
            Return pluginlist
        End Function

#End Region

#Region "Parser"

        Private Function LoadPluginResult(webresult As String) As BukgetPlugin
            Dim pl As New BukgetPlugin
            If webresult Is Nothing OrElse webresult = "" OrElse webresult = "null" Then
                Return Nothing
                Exit Function
            End If
            pl = ParsePlugin(webresult) 'Parse result
            If pl.name Is Nothing Then Return Nothing : Exit Function
            Log(loggingLevel.Fine, "BukGetAPI", "Got plugin info from plugin: " & pl.name) 'echo results
            If pl.versions.Count <> 0 Then _
                Log(loggingLevel.Fine, "BukGetAPI",
                    "Latest version:" & pl.versions(0).version & " at " & pl.versions(0).ReleaseDate.ToString)
            If pl.versions.Count = 0 Then _
                Log(loggingLevel.Fine, "BukGetAPI", "There are no versions available for this plugin")
            Return pl
        End Function


        Private Function ParsePluginList(list As String) As List(Of SimpleBukgetPlugin)
            'This function parses a simple plugin list, like http://org/api/plugins

            Log(loggingLevel.Fine, "BukGetAPI", "Starting parse of plugin list...")
            If list Is Nothing OrElse list.Length = 0 Then
                Log(loggingLevel.Warning, "BukGetAPI", "No elements in list, cannot parse plugin list")
                Return New List(Of SimpleBukgetPlugin)
                Exit Function
            End If
            Dim reslist As New List(Of SimpleBukgetPlugin)
            Try
                Dim jarr As JsonArray = JsonConvert.Import(list)
                If jarr.Count > 0 Then
                    For i As UInt64 = 0 To jarr.Count - 1 'For each line (=plugin / entry) trim and add to list
                        Try
                            Dim pl As SimpleBukgetPlugin = ParsePluginListItem(jarr.GetString(i))
                            If pl IsNot Nothing Then reslist.Add(pl)
                        Catch ex As Exception
                            Log(loggingLevel.Severe, "BukGetAPI", "Something went wrong Parsing the plugin...",
                                ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                Log(loggingLevel.Severe, "BukGetAPI", "Something went wrong while parsing the plugin list",
                    ex.Message)
            End Try
            Log(loggingLevel.Fine, "BukGetAPI", "Parsing done... Parsed " & reslist.Count & " items")
            Return reslist
        End Function

        Private Function ParsePlugin(text As String) As BukgetPlugin

            '      "website": "http://dev.bukkit.org/server-mods/abacus/",
            '"dbo_page": "http://dev.bukkit.org/server-mods/abacus",
            '"description": "Offers ability to perform calculations while in Minecraft",
            '"logo_full": "",
            '"versions": [    ],
            '"plugin_name": "Abacus",
            '"server": "bukkit",
            '"authors": [
            '"Feaelin {iain@ruhlendavis.org}"
            '],
            '"logo": "",
            '"slug": "abacus",
            '"categories": [
            '"Informational"
            '],
            '"stage": "Beta"

            Dim pl As New BukgetPlugin

            If text Is Nothing Or text = "null" Then Return Nothing : Exit Function

            Dim obj As JsonObject
            If text.StartsWith("[") Then
                Dim array As JsonArray = JsonConvert.Import(text)
                obj = array(0)
            Else
                obj = JsonConvert.Import(text)
            End If


            If obj("slug") IsNot Nothing Then pl.slug = obj("slug") Else pl.slug = "unkown name"
            If obj("plugin_name") IsNot Nothing Then pl.name = obj("plugin_name") Else pl.name = pl.slug
            Log(loggingLevel.Fine, "BukGetAPI", "parsing plugin:" & pl.slug)
            If obj("description") IsNot Nothing Then pl.Description = obj("description") Else pl.Description = ""
            If obj("dbo_page") IsNot Nothing Then pl.BukkitDevLink = obj("dbo_page") Else _
                pl.BukkitDevLink = "http://dev.bukkit.org"
            If obj("link") IsNot Nothing Then pl.Website = obj("link") Else pl.Website = ""

            If obj("stage") IsNot Nothing Then
                Select Case obj("stage").ToString
                    Case "Release"
                        pl.status = PluginStatus.Release
                    Case "Alpha"
                        pl.status = PluginStatus.Alpha
                    Case "Beta"
                        pl.status = PluginStatus.Beta
                    Case "Mature"
                        pl.status = PluginStatus.Mature
                    Case "Planning"
                        pl.status = PluginStatus.Planning
                End Select
            End If

            Dim jarr As JsonArray
            If obj("authors") IsNot Nothing Then
                jarr = obj("authors")
                If jarr.Length > 0 Then
                    For i As Byte = 0 To jarr.Length - 1
                        If Not pl.Author.Contains(ParsePluginAuthor(jarr.GetString(i).ToString)) Then _
                            pl.Author.Add(ParsePluginAuthor(jarr.GetString(i).ToString)) 'if not already added, add
                    Next
                End If
            End If

            If obj("main") IsNot Nothing Then pl.main = obj("main").ToString 'name of this version

            Dim catlist As New List(Of String)

            If obj("categories") IsNot Nothing Then
                jarr = obj("categories")
                If jarr.Length > 0 Then
                    For i As Byte = 0 To jarr.Length - 1
                        Try
                            catlist.Add(jarr.GetString(i))
                        Catch
                        End Try
                    Next
                    pl.Category = catlist
                End If
            End If

            If obj("versions") IsNot Nothing Then
                jarr = obj("versions")
                If jarr.Count > 0 Then
                    For i As Byte = 0 To jarr.Count - 1
                        Dim v As PluginVersion = ParseVersion(jarr.GetString(i))
                        v.pluginname = pl.slug
                        If pl.name IsNot Nothing AndAlso pl.name <> "" Then v.pluginname = pl.name
                        pl.versions.Add(v)
                    Next
                Else
                    Log(loggingLevel.Warning, "BukGetAPI", "No version found!")
                End If
            End If

            Log(loggingLevel.Fine, "BukGetAPI", "Plugin parsed: " & pl.slug)
            Return pl
        End Function

        Public Function ParsePluginListItem(json As String) As SimpleBukgetPlugin
            Dim pl As New SimpleBukgetPlugin
            Dim obj As JsonObject

            If json.StartsWith("[") Then
                Dim jarr As JsonArray = JsonConvert.Import(json)
                If jarr IsNot Nothing Then obj = jarr(0) Else Return Nothing : Exit Function
            Else
                obj = JsonConvert.Import(json) 'create JSON object
            End If

            Try
                pl.main = obj("main")
                pl.slug = obj("slug")
                pl.descr = obj("description")
                pl.name = obj("plugin_name")


                If obj("versions") IsNot Nothing Then
                    Dim jarr As JsonArray = obj("versions")
                    obj = jarr(0)
                    If obj("game_versions") IsNot Nothing Then
                        Dim vjarr As JsonArray = obj("game_versions")
                        If vjarr.Count > 0 Then 'prevent error
                            pl.LastBukkit = vjarr.GetString(0).ToString 'add all versions
                        End If
                    End If
                    If obj("version") IsNot Nothing Then pl.LastVersion = obj("version").ToString 'name of this version
                    '   If pl.LastBukkit Is Nothing OrElse pl.LastBukkit = "" Then pl.LastBukkit = "unknown"
                    '    If pl.LastVersion Is Nothing OrElse pl.LastVersion = "" Then pl.LastVersion = "unknown"
                    '     If pl.name Is Nothing OrElse pl.name = "" Then pl.name = pl.slug
                End If
            Catch ex As Exception
                Log(loggingLevel.Warning, "BukgetAPI", "Couldn't parse plugin from JSON:" & json, ex.Message)
                Return Nothing
                Exit Function
            End Try

            Return pl
        End Function


        Private Function ParseVersion(text As String) As PluginVersion
            '           "versions": [
            '        {
            '"status": "Semi-normal",
            '"commands": [
            '{
            '   "usage": "",
            '"aliases": [],
            '"command": "abacus",
            '"permission-message": "§cYou do not have access to that command.",
            '"permission": "abacus.abacus"
            '}
            '],
            '"changelog": "LONG STRING OF STUFF!!!!!",
            '"game_versions": [
            '"CB 1.4.5-R0.2"
            '],
            '"filename": "Abacus.jar",
            '"hard_dependencies": [],
            '"date": 1353964798,
            '"version": "0.9.3",
            '"link": "http://dev.bukkit.org/server-mods/abacus/files/4-abacus-v0-9-2-cb-1-4-5-r0-2-compatible-w-1-3-2/",
            '"download": "http://dev.bukkit.org/media/files/650/288/Abacus.jar",
            '"md5": "1e1b6b6e131c617248f98c53bf650874",
            '"type": "Beta",
            '"slug": "4-abacus-v0-9-2-cb-1-4-5-r0-2-compatible-w-1-3-2",
            '"soft_dependencies": [],
            '"permissions": [
            '   {
            '        "default": "op",
            '         "role": "abacus.*"
            '     },
            '     {
            '       "default": true,
            '       "role": "abacus.abacus"
            '     }
            '  ]
            ' },

            ' ],

            Dim obj As JsonObject = JsonConvert.Import(text) 'create JSON object

            Dim v As New PluginVersion 'temporary variable, this variable will be returned
            If obj("game_versions") IsNot Nothing Then
                Dim vjarr As JsonArray = obj("game_versions")
                If vjarr.Count > 0 Then 'prevent error
                    For i As Byte = 0 To vjarr.Count - 1
                        v.builds.Add(vjarr.GetString(i).ToString) 'add all versions
                    Next
                End If
            End If


            If obj("version") IsNot Nothing Then v.version = obj("version").ToString 'name of this version
            If obj("download") IsNot Nothing Then v.DownloadLink = obj("download").ToString 'download link
            If obj("link") IsNot Nothing Then v.PageLink = obj("link").ToString 'download link
            If obj("date") IsNot Nothing Then _
                v.ReleaseDate = New Date(1970, 1, 1).AddSeconds(CDbl(obj("date").ToString)) _
            'date, in UNIX formart (seconds elapsed since 1/1/1970)
            If obj("filename") IsNot Nothing Then v.filename = obj("filename").ToString 'filename
            If obj("status") IsNot Nothing Then _
                v.type = DirectCast([Enum].Parse(GetType(PluginStatus), obj("status").ToString.Replace("-", "_")),
                                    PluginStatus)
            Return v
        End Function

        Public Function ParsePluginAuthor(str As String) As String
            If str Is Nothing OrElse str = "" Then Return "" : Exit Function
            Return str
        End Function

#End Region

        
        ''' <summary>
        '''     Get a bukgetplugin opbject based upon a plugin namespace
        ''' </summary>
        ''' <param name="Main"></param>
        ''' <param name="ShowUI"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetPluginInfoByNamespace(Main As String, Optional ByVal ShowUI As Boolean = True) _
            As BukgetPlugin
            Log(loggingLevel.Fine, "BukGetAPI", "Getting plugin info from plugin: " & Main)
            If Main Is Nothing OrElse Main = "" OrElse Main.Trim = "" Then Return Nothing : Exit Function
            Dim pl As New BukgetPlugin
            Try
                Dim WebResult As String =
                        downloadstring(API_SEARCHBYNAMESPACE & Main & "?fields=" & FIELDS_ALL) _
                'get from API
                pl = LoadPluginResult(WebResult)
            Catch ex As Exception
                If ShowUI Then _
                    MessageBox.Show(lr("An error occured while loading this data"), lr("Could not get data"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Warning, "BukGetAPI", "Couldn't load plugin data for " & Main, ex.Message)
            End Try
            Return pl
        End Function

        
        ''' <summary>
        '''     Show a dialog containing version info and plugin details
        ''' </summary>
        ''' <param name="main">The namespace of the plugin you want to display</param>
        ''' <remarks></remarks>
        Public Sub ShowPluginDialogByNamespace(main As String)
            Dim pd As New BukgetPluginDialog
            Dim pi As BukgetPlugin = GetPluginInfoByNamespace(main)
            If pi Is Nothing Then
                MessageBox.Show(lr("Could not get data for this plugin"), lr("Could not get data"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Exit Sub
            End If
            pd.Plugin = pi
            pd.ShowDialog()
        End Sub

        
        ''' <summary>
        '''     Open the project page on dev.bukkit.org for a plugin with the provided namespace
        ''' </summary>
        ''' <param name="main">The namespace of the plugin you want to display</param>
        ''' <remarks></remarks>
        Public Sub OpenProjectPageByNamespace(main As String)
            Dim p As New Process
            Dim pi As BukgetPlugin = GetPluginInfoByNamespace(main)
            If pi Is Nothing OrElse pi.BukkitDevLink.Contains("http://") = False Then
                MessageBox.Show(lr("Could not get data for this plugin"), lr("Could not get data"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Exit Sub
            End If
            p.StartInfo.FileName = pi.BukkitDevLink
            p.Start()
        End Sub

        
        ''' <summary>
        '''     Install a plugin by providing the namespace
        ''' </summary>
        ''' <param name="main">The namespace of the plugin you want to install</param>
        ''' <param name="targetlocation">Target location, plugins/name by default</param>
        ''' <param name="updatelist">Update the list of installed plugins</param>
        ''' <param name="ShowUI">Allow pop-up dialogs</param>
        ''' <remarks></remarks>
        Public Sub InstallPluginByNamespace(main As String, Optional ByVal targetlocation As String = "",
                                            Optional ByVal updatelist As Boolean = True,
                                            Optional ByVal ShowUI As Boolean = True)
            Dim pi As BukgetPlugin = GetPluginInfoByNamespace(main)
            InstallPlugin(pi, targetlocation, updatelist, ShowUI)
        End Sub

        
        ''' <summary>
        '''     Install a plugin by providing the namespace
        ''' </summary>
        ''' <param name="pi">The bukgetplugin object of the plugin you want to install</param>
        ''' <param name="targetlocation">Target location, plugins/name by default</param>
        ''' <param name="updatelist">Update the list of installed plugins</param>
        ''' <param name="ShowUI">Allow pop-up dialogs</param>
        ''' <remarks></remarks>
        Public Sub InstallPlugin(pi As BukgetPlugin, Optional ByVal targetlocation As String = "",
                                 Optional ByVal updatelist As Boolean = True, Optional ByVal ShowUI As Boolean = True)
            If pi Is Nothing Then
                MessageBox.Show(lr("Could not get data for this plugin"), lr("Could not get data"), MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
                Exit Sub
            End If

            If pi.versions.Count = 0 Then
                MessageBox.Show(
                    lr("Error: No downloads are available for this project. It is probably still under construction."),
                    lr("no downloads available"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If targetlocation = "" Then targetlocation = plugin_dir & "/" & pi.versions(0).filename

            Install(pi.versions(0), targetlocation, updatelist, ShowUI)
        End Sub
    End Module

    
    ''' <summary>
    '''     Less detailled plugin class for installing plugins and showing info
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SimpleBukgetPlugin
        Public slug As String = "",
               name As String = "",
               descr As String = "",
               LastVersion As String = "",
               LastBukkit As String = "",
               main As String = ""

        Public Sub New()
            Me.slug = ""
            Me.name = ""
            Me.descr = ""
            Me.LastVersion = ""
            Me.LastBukkit = ""
            Me.main = ""
        End Sub

        Public Sub New(Main As String, PluginName As String)
            Me.slug = ""
            Me.descr = ""
            Me.LastVersion = ""
            Me.LastBukkit = ""
            Me.name = PluginName
            Me.main = Main
        End Sub

        Public Sub New(Main As String, PluginName As String, Description As String)
            Me.slug = ""
            Me.name = ""
            Me.descr = ""
            Me.LastVersion = ""
            Me.LastBukkit = ""
            Me.name = PluginName
            Me.main = Main
            Me.descr = Description
        End Sub

        Public Sub New(Main As String, Name As String, PluginName As String, description As String,
                       LastVersion As String, LastBukkit As String)
            Me.slug = ""
            Me.name = ""
            Me.descr = ""
            Me.LastVersion = ""
            Me.LastBukkit = ""
            Me.name = PluginName
            Me.slug = Name
            Me.descr = description
            Me.LastVersion = LastVersion
            Me.LastBukkit = LastBukkit
        End Sub
    End Class

    
    ''' <summary>
    '''     Most detailled plugin class for installing plugins and showing info
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BukgetPlugin
        Public slug As String,
               main As String,
               name As String,
               Author As List(Of String),
               Category As List(Of String),
               status As PluginStatus,
               versions As List(Of PluginVersion),
               BukkitDevLink As String,
               Website As String,
               Description As String

        Public Sub New()
            slug = ""
            Author = New List(Of String)
            Category = New List(Of String)
            status = PluginStatus.Release
            versions = New List(Of PluginVersion)
            main = ""
            BukkitDevLink = ""
        End Sub

        Public Sub New(Title As String)
            slug = Title
            Author = New List(Of String)
            Category = New List(Of String)
            status = PluginStatus.Release
            versions = New List(Of PluginVersion)
            main = ""
            BukkitDevLink = ""
        End Sub

        Public Sub New(Title As String, Author_Name As String)
            slug = Title
            Author = New List(Of String)
            Author.Add(Author_Name)
            Category = New List(Of String)
            status = PluginStatus.Release
            versions = New List(Of PluginVersion)
            main = ""
            BukkitDevLink = ""
        End Sub


        Public Sub FromSimplePlugin(pl As SimpleBukgetPlugin)
            slug = pl.slug
            Author = New List(Of String)
            Category = New List(Of String)
            status = PluginStatus.Release
            versions = New List(Of PluginVersion)
            main = ""
            BukkitDevLink = ""
        End Sub
    End Class

    
    ''' <summary>
    '''     Contains details of a plugin version, including all data needed to update or install a plugin
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginVersion
        '    "date": 1317404619, 
        '    "dl_link": "http://dev.bukkit.org/media/files/LINK_TO_JAR/ZIP", 
        '    "filename": "AcceptRules.jar", 
        '    "game_builds": ["1000", "9999"], 
        '    "hard_dependencies": [], 
        '    "md5": "8184a10ef2657024ca0ceb38f9b681eb", 
        '    "name": "v0.7", 
        '    "soft_dependencies": []
        Public ReleaseDate As Date,
               DownloadLink As String,
               PageLink As String,
               version As String,
               builds As List(Of String),
               filename As String,
               type As PluginStatus,
               pluginname As String

        Public Sub New()
            ReleaseDate = New Date
            DownloadLink = ""
            version = ""
            filename = ""
            pluginname = ""
            builds = New List(Of String)
        End Sub

        Public Sub New(version_Date As Date, download_link As String, version_name As String)
            ReleaseDate = version_Date
            DownloadLink = download_link
            version = version_name
            builds = New List(Of String)
            filename = ""
            pluginname = ""
        End Sub

        Public Sub New(version_Date As Date, download_link As String, version_name As String,
                       bukkitversions As List(Of String))
            ReleaseDate = version_Date
            DownloadLink = download_link
            version = version_name
            builds = bukkitversions
            filename = ""
            pluginname = ""
        End Sub
    End Class
End Namespace