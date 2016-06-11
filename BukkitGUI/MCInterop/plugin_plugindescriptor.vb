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

Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities
Imports Yaml.Grammar


Namespace MCInterop
    Public Class plugindescriptor
        'Should be always available
        
        ''' <summary>
        '''     Plugin name
        ''' </summary>
        ''' <remarks>Required</remarks>
        Public name As String

        
        ''' <summary>
        '''     Plugin version
        ''' </summary>
        ''' <remarks>Required</remarks>
        Public version As String

        
        ''' <summary>
        '''     Plugin authors
        ''' </summary>
        ''' <remarks>Recommended</remarks>
        Public authors() As String

        
        ''' <summary>
        '''     Plugin description
        ''' </summary>
        ''' <remarks>Recommended</remarks>
        Public description As String

        'Additional:

        
        ''' <summary>
        '''     Main namespace
        ''' </summary>
        ''' <remarks>optional</remarks>
        Public main As String 'main namespace

        
        ''' <summary>
        '''     Commands registered by this plugin
        ''' </summary>
        ''' <remarks>optional</remarks>
        Public commands As List(Of pluginCommand)

        
        ''' <summary>
        '''     Permissions registered by this pluing
        ''' </summary>
        ''' <remarks>optional</remarks>
        Public permissions As List(Of pluginPermission)

        
        ''' <summary>
        '''     Soft depends on the following plugins
        ''' </summary>
        ''' <remarks>optional</remarks>
        Public softdepend() As String

        '------------------------------
        'can be added if loaded from file
        Public filename As String
        Public FileCreationDate As Date
        '------------------------------

        
        ''' <summary>
        '''     Loads the plugin.yml file of a .jar plugin
        ''' </summary>
        ''' <param name="path">the path of the plugin.jar file</param>
        ''' <param name="read_cache">if this plugin should be read from cache if possible</param>
        ''' <returns>The plugindescriptor (me)</returns>
        ''' <remarks></remarks>
        Public Function loadplugin(path As String, Optional read_cache As Boolean = True) As plugindescriptor
            Try
                'to reduce load times and CPU usage, plugin.yml files are cached
                'location: cache/plugins/plugin_name/plugin.yml
                If path.Contains(":\") = False Then path = plugin_dir & "\" & path 'relative directory detection

                Dim fi As New FileInfo(path)
                Dim nfi As New FileInfo(CachePath & "/plugins/" & fi.Name & "/plugin.yml")

                Log(loggingLevel.Fine, "plugindescriptor",
                    "loading plugin (step 1/2): " & fi.Name & " - cache allowed:" & read_cache)

                If nfi.Exists And read_cache = True Then _
'check if the cache exists, if not, create cache (we need this cache file, it will be read later on)
                    Log(loggingLevel.Fine, "plugindescriptor", "Reading plugin data from cache...")
                Else 'create cache
                    If path Is Nothing OrElse path = "" OrElse fi.Exists = False Then Return Nothing : Exit Function
                    Log(loggingLevel.Fine, "plugindescriptor",
                        "Plugin data not available in cache or cache not allowed. Building cache for plugin...")
                    decompress(TmpPath & "/plugin", path)
                    If Not FileSystem.FileExists(TmpPath & "/plugin/plugin.yml") Then _
                        Return Nothing : Exit Function
                    SafeFileCopy(TmpPath & "/plugin/plugin.yml", nfi.FullName, True)
                    If FileSystem.DirectoryExists(TmpPath & "/plugin") Then _
                        FileSystem.DeleteDirectory(TmpPath & "/plugin",
                                                   DeleteDirectoryOption.DeleteAllContents)
                End If

                Log(loggingLevel.Fine, "plugindescriptor",
                    "loading plugin (step 2/2): " & fi.Name & " - cache allowed:" & read_cache)

                If nfi Is Nothing OrElse FileSystem.FileExists(nfi.FullName) = False Then
                    Me.filename = New FileInfo(path).Name
                    If Me.filename.Contains(".") Then Me.name = Me.filename.Split(".")(0)
                End If


                If FileSystem.FileExists(nfi.FullName) Then loadymlfile(nfi.FullName) 'Load the cache

                Me.FileCreationDate = File.GetLastWriteTime(path)
                Me.filename = New FileInfo(path).Name

                If Me.name Is Nothing OrElse Me.name = "" AndAlso Me.filename.Contains(".") Then _
                    Me.name = Me.filename.Split(".")(0) 'if name couldn't be read from yml, parse filename

                Log(loggingLevel.Fine, "plugindescriptor",
                    "loaded plugin: " & fi.Name & " - cache allowed:" & read_cache)

                Return Me 'return this item
            Catch ex As Exception
                Log(loggingLevel.Warning, "Plugindescriptor",
                    "An exception occured when trying to load plugin", ex.Message)
                Return Nothing
            End Try
        End Function

        
        ''' <summary>
        '''     Loads the contents of a plugin.yml file
        ''' </summary>
        ''' <param name="path">the path of the plugin.yml file</param>
        ''' <returns>The plugindescriptor (me)</returns>
        ''' <remarks></remarks>
        Public Function loadymlfile(path As String) As plugindescriptor
            Try
                If path Is Nothing OrElse path = "" OrElse FileSystem.FileExists(path) = False Then _
                    Return Nothing : Exit Function
                Dim content As String
                Dim sr As New StreamReader(path)
                content = sr.ReadToEnd
                sr.Close()
                sr.Dispose()
                loadyml(content)
                Return Me
            Catch ex As Exception
                Log(loggingLevel.Severe, "PluginDescriptor",
                    "An exception occured when trying to load yml file", ex.Message)
                Return Nothing
            End Try
        End Function

        
        ''' <summary>
        '''     Loads the contents of a plugin.yml file
        ''' </summary>
        ''' <param name="ymltext">the yml formatted text</param>
        ''' <returns>The plugindescriptor (me)</returns>
        ''' <remarks></remarks>
        Public Function loadyml(ymltext As String) As plugindescriptor
            Try
                Dim sc As New Scalar
                Dim seq As New Sequence
                Dim map As New Mapping

                'References to check file types later on
                Dim t_scalar As Type = sc.GetType
                Dim t_sequence As Type = seq.GetType
                Dim t_mapping As Type = map.GetType


                If ymltext Is Nothing Or ymltext = "" Then Return Nothing : Exit Function

                Dim yml As YamlStream = YamlParser.Load(ymltext)

                If yml Is Nothing Then Return Nothing : Exit Function

                If yml.Documents(0).Root.GetType.Equals(t_mapping) Then 'if mapping start parsing
                    For Each item As MappingEntry In CType(yml.Documents(0).Root, Mapping).Enties

                        'Check the type, check for possible keys and load the value
                        If item.Value.GetType.Equals(t_scalar) Then
                            Select Case CType(item.Key, Scalar).Text
                                Case "name"
                                    Me.name = CType(item.Value, Scalar).Text
                                Case "version"
                                    Me.version = CType(item.Value, Scalar).Text
                                Case "description"
                                    Me.description = CType(item.Value, Scalar).Text
                                Case "main"
                                    Me.main = CType(item.Value, Scalar).Text
                                Case "author"
                                    authors = Array.CreateInstance(GetType(String), 1)
                                    Me.authors(0) = CType(item.Value, Scalar).Text
                                Case "authors"
                                    authors = Array.CreateInstance(GetType(String), 1)
                                    Me.authors(0) = CType(item.Value, Scalar).Text
                            End Select
                        ElseIf item.Value.GetType.Equals(t_sequence) Then
                            Select Case CType(item.Key, Scalar).Text
                                Case "author"
                                    Me.authors = ArrayFromSequence(CType(item.Value, Sequence))
                                Case "authors"
                                    Me.authors = ArrayFromSequence(CType(item.Value, Sequence))
                                Case "softdepend"
                                    Me.softdepend = ArrayFromSequence(CType(item.Value, Sequence))
                            End Select
                        ElseIf item.Value.GetType.Equals(t_mapping) Then
                            Select Case CType(item.Key, Scalar).Text
                                Case "commands"
                                    If item.Value.GetType.Equals(t_mapping) Then _
                                        Me.commands = parseCommands(CType(item.Value, Mapping)) Else _
                                        commands = New List(Of pluginCommand)
                                Case "permissions"
                                    If item.Value.GetType.Equals(t_mapping) Then _
                                        Me.permissions = parsePermissions(CType(item.Value, Mapping)) Else _
                                        permissions = New List(Of pluginPermission)
                            End Select
                        End If
                    Next
                End If
                Return Me
            Catch ex As Exception
                Log(loggingLevel.Warning, "PluginDescriptor",
                    "An exception occured when trying to parse yml text", ex.Message)
                Return Nothing
            End Try
        End Function

        
        ''' <summary>
        '''     Change nothing values to either empty lists or empty strings
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ToSafeObject() As plugindescriptor
            If name Is Nothing Then name = ""
            If version Is Nothing Then version = ""
            If authors Is Nothing Then authors = Array.CreateInstance(GetType(String), 1) : authors(0) = ""
            If description Is Nothing Then description = ""
            If main Is Nothing Then main = ""
            If commands Is Nothing Then commands = New List(Of pluginCommand)
            If permissions Is Nothing Then permissions = New List(Of pluginPermission)
            If softdepend Is Nothing Then softdepend = Array.CreateInstance(GetType(String), 1) : softdepend(0) = ""
            If filename Is Nothing Then filename = ""
            Return Me
        End Function

        
        ''' <summary>
        '''     Parse commands from plugin.yml
        ''' </summary>
        ''' <param name="map"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function parseCommands(map As Mapping) As List(Of pluginCommand)
            Try
                Dim l As New List(Of pluginCommand)

                Dim sc As New Scalar
                Dim seq As New Sequence
                Dim t_scalar As Type = sc.GetType
                Dim t_sequence As Type = seq.GetType
                Dim t_mapping As Type = map.GetType
                If map.GetType.Equals(t_mapping) Then
                    For Each entry As MappingEntry In map.Enties
                        Dim pc As New pluginCommand
                        pc.name = CType(entry.Key, Scalar).Text

                        If entry.Value.GetType.Equals(t_mapping) Then
                            For Each secondlevel As MappingEntry In CType(entry.Value, Mapping).Enties
                                If secondlevel.Key.GetType.Equals(t_scalar) Then
                                    Select Case CType(secondlevel.Key, Scalar).Text
                                        Case "description"
                                            pc.description = CType(secondlevel.Value, Scalar).Text
                                        Case "usage"
                                            pc.usage = CType(secondlevel.Value, Scalar).Text
                                        Case "alias"
                                            If entry.Value.GetType.Equals(t_sequence) Then
                                                pc.aliases = ArrayFromSequence(CType(secondlevel.Value, Sequence))
                                            ElseIf entry.Value.GetType.Equals(t_scalar) Then
                                                pc.aliases = Array.CreateInstance(GetType(String), 1)
                                                pc.aliases(0) = CType(entry.Value, Scalar).Text
                                            End If
                                        Case "aliases"
                                            If entry.Value.GetType.Equals(t_sequence) Then
                                                pc.aliases = ArrayFromSequence(CType(secondlevel.Value, Sequence))
                                            ElseIf entry.Value.GetType.Equals(t_scalar) Then
                                                pc.aliases = Array.CreateInstance(GetType(String), 1)
                                                pc.aliases(0) = CType(entry.Value, Scalar).Text
                                            End If
                                    End Select
                                End If
                            Next
                        End If

                        l.Add(pc)
                    Next
                End If
                Return l
            Catch ex As Exception
                Log(loggingLevel.Warning, "PluginDescriptor",
                    "An exception occured when trying to load plugin commands", ex.Message)
                Return New List(Of pluginCommand)
            End Try
        End Function

        
        ''' <summary>
        '''     Parse permissions from plugin.yml
        ''' </summary>
        ''' <param name="map"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function parsePermissions(map As Mapping) As List(Of pluginPermission)
            Try
                Dim l As New List(Of pluginPermission)

                Dim sc As New Scalar
                Dim seq As New Sequence
                Dim t_scalar As Type = sc.GetType
                Dim t_sequence As Type = seq.GetType
                Dim t_mapping As Type = map.GetType
                If map.GetType.Equals(t_mapping) Then
                    For Each entry As MappingEntry In map.Enties
                        Dim pp As New pluginPermission
                        pp.name = CType(entry.Key, Scalar).Text
                        If entry.Value.GetType.Equals(t_mapping) Then
                            For Each secondlevel As MappingEntry In CType(entry.Value, Mapping).Enties
                                If secondlevel.Key.GetType.Equals(t_scalar) Then
                                    Select Case CType(secondlevel.Key, Scalar).Text
                                        Case "description"
                                            pp.description = CType(secondlevel.Value, Scalar).Text
                                        Case "default"

                                        Case "children"
                                            pp.children = New List(Of pluginPermissionChild)
                                            For Each thirdlevel As MappingEntry In _
                                                CType(secondlevel.Value, Mapping).Enties
                                                Dim ppc As New pluginPermissionChild
                                                ppc.name = CType(thirdlevel.Key, Scalar).Text
                                                pp.children.Add(ppc)
                                            Next
                                    End Select
                                End If
                            Next
                            l.Add(pp)
                        End If
                    Next
                End If
                Return l
            Catch ex As Exception
                Log(loggingLevel.Warning, "PluginDescriptor",
                    "An exception occured when trying to load plugin permissions", ex.Message)
                Return New List(Of pluginPermission)
            End Try
        End Function

        
        ''' <summary>
        '''     Convert a sequence to an array
        ''' </summary>
        ''' <param name="seq"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ArrayFromSequence(seq As Sequence) As String()
            Try
                Dim sc As New Scalar
                Dim t_scalar As Type = sc.GetType
                Dim arr(seq.Enties.Count - 1) As String
                For i = 0 To seq.Enties.Count - 1
                    If seq.Enties(i).GetType.Equals(t_scalar) Then
                        arr(i) = (CType(seq.Enties(i), Scalar).Text)
                    End If
                Next
                Return arr
            Catch ex As Exception
                Log(loggingLevel.Severe, "PluginDescriptor",
                    "An exception occured when trying to convert array to sequence", ex.Message)
                Return {""}
            End Try
        End Function
    End Class

    Public Class pluginCommand
        Public name As String
        Public description As String
        Public usage As String
        Public aliases() As String
    End Class

    Public Class pluginPermission
        Public Enum defaultmode
            yes
            op
            no
        End Enum

        Public name As String
        Public description As String
        Public default_perm As defaultmode
        Public children As List(Of pluginPermissionChild)
    End Class

    Public Class pluginPermissionChild
        Public name As String
        Public default_perm As pluginPermission.defaultmode
    End Class
End Namespace