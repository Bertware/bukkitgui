Imports System.IO
Imports Yaml
Imports Yaml.Grammar

Imports Net.Bertware.BukkitGUI.Core


Namespace MCInterop
    Public Class plugindescriptor
        'Should be always available
        Public name As String
        Public version As String
        Public authors() As String
        Public description As String

        'Additional:
        Public main As String 'main namespace
        Public commands As List(Of pluginCommand)
        Public permissions As List(Of pluginPermission)
        Public softdepend() As String

        '------------------------------
        'can be added if loaded from file
        Public filename As String
        Public FileCreationDate As Date
        '------------------------------


        ''' <summary>
        ''' Loads the plugin.yml file of a .jar plugin
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
                Dim nfi As New FileInfo(common.Cache_path & "/plugins/" & fi.Name & "/plugin.yml")

                livebug.write(loggingLevel.Fine, "plugindescriptor", "loading plugin (step 1/2): " & fi.Name & " - cache allowed:" & read_cache)

                If nfi.Exists And read_cache = True Then 'check if the cache exists, if not, create cache (we need this cache file, it will be read later on)
                    livebug.write(loggingLevel.Fine, "plugindescriptor", "Reading plugin data from cache...")
                Else 'create cache
                    If path Is Nothing OrElse path = "" OrElse fi.Exists = False Then Return Nothing : Exit Function
                    livebug.write(loggingLevel.Fine, "plugindescriptor", "Plugin data not available in cache or cache not allowed. Building cache for plugin...")
                    Utilities.compression.decompress(common.Tmp_path & "/plugin", path)
                    If Not FileIO.FileSystem.FileExists(common.Tmp_path & "/plugin/plugin.yml") Then Return Nothing : Exit Function
                    common.SafeFileCopy(common.Tmp_path & "/plugin/plugin.yml", nfi.FullName, True)
                    If FileIO.FileSystem.DirectoryExists(common.Tmp_path & "/plugin") Then FileIO.FileSystem.DeleteDirectory(common.Tmp_path & "/plugin", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If

                livebug.write(loggingLevel.Fine, "plugindescriptor", "loading plugin (step 2/2): " & fi.Name & " - cache allowed:" & read_cache)

                If nfi Is Nothing OrElse FileIO.FileSystem.FileExists(nfi.FullName) = False Then
                    Me.filename = New FileInfo(path).Name
                    If Me.filename.Contains(".") Then Me.name = Me.filename.Split(".")(0)
                End If


                If FileIO.FileSystem.FileExists(nfi.FullName) Then loadymlfile(nfi.FullName) 'Load the cache

                Me.FileCreationDate = IO.File.GetLastWriteTime(path)
                Me.filename = New FileInfo(path).Name

                If Me.name Is Nothing OrElse Me.name = "" AndAlso Me.filename.Contains(".") Then Me.name = Me.filename.Split(".")(0) 'if name couldn't be read from yml, parse filename

                livebug.write(loggingLevel.Fine, "plugindescriptor", "loaded plugin: " & fi.Name & " - cache allowed:" & read_cache)

                Return Me 'return this item
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "Plugindescriptor", "An exception occured when trying to load plugin", ex.Message)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Loads the contents of a plugin.yml file
        ''' </summary>
        ''' <param name="path">the path of the plugin.yml file</param>
        ''' <returns>The plugindescriptor (me)</returns>
        ''' <remarks></remarks>
        Public Function loadymlfile(path As String) As plugindescriptor
            Try
                If path Is Nothing OrElse path = "" OrElse FileIO.FileSystem.FileExists(path) = False Then Return Nothing : Exit Function
                Dim content As String
                Dim sr As New StreamReader(path)
                content = sr.ReadToEnd
                sr.Close()
                sr.Dispose()
                loadyml(content)
                Return Me
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "PluginDescriptor", "An exception occured when trying to load yml file", ex.Message)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Loads the contents of a plugin.yml file
        ''' </summary>
        ''' <param name="ymltext">the yml formatted text</param>
        ''' <returns>The plugindescriptor (me)</returns>
        ''' <remarks></remarks>
        Public Function loadyml(ymltext As String) As plugindescriptor
            Try
                Dim sc As New Scalar
                Dim seq As New Sequence
                Dim map As New Mapping
                Dim t_scalar As Type = sc.GetType
                Dim t_sequence As Type = seq.GetType
                Dim t_mapping As Type = map.GetType


                If ymltext Is Nothing Or ymltext = "" Then Return Nothing : Exit Function

                Dim yml As YamlStream = Grammar.YamlParser.Load(ymltext)

                If yml Is Nothing Then Return Nothing : Exit Function
                If yml.Documents(0).Root.GetType.Equals(t_mapping) Then
                    For Each item As MappingEntry In CType(yml.Documents(0).Root, Grammar.Mapping).Enties
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
                                    If item.Value.GetType.Equals(t_mapping) Then Me.commands = parseCommands(CType(item.Value, Mapping)) Else commands = New List(Of pluginCommand)
                                Case "permissions"
                                    If item.Value.GetType.Equals(t_mapping) Then Me.permissions = parsePermissions(CType(item.Value, Mapping)) Else permissions = New List(Of pluginPermission)
                            End Select
                        End If
                    Next
                End If
                Return Me
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "PluginDescriptor", "An exception occured when trying to parse yml text", ex.Message)
                Return Nothing
            End Try
        End Function

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
                livebug.write(loggingLevel.Warning, "PluginDescriptor", "An exception occured when trying to load plugin commands", ex.Message)
                Return New List(Of pluginCommand)
            End Try
        End Function

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
                                            For Each thirdlevel As MappingEntry In CType(secondlevel.Value, Mapping).Enties
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
                livebug.write(loggingLevel.Warning, "PluginDescriptor", "An exception occured when trying to load plugin permissions", ex.Message)
                Return New List(Of pluginPermission)
            End Try
        End Function

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
                livebug.write(loggingLevel.Severe, "PluginDescriptor", "An exception occured when trying to convert array to sequence", ex.Message)
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