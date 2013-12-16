Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Core

Namespace MCInterop
    ''' <summary>
    ''' Module for installing plugins from dev.bukkit.org
    ''' </summary>
    ''' <remarks></remarks>
    Public Module PluginInstaller
        ''' <summary>
        ''' Install a plugin, supports .jar and .zip files
        ''' </summary>
        ''' <param name="version">Version to install</param>
        ''' <param name="targetlocation">Target location, plugins/name by default</param>
        ''' <param name="updatelist">Update the list of installed plugins</param>
        ''' <param name="ShowUI">Allow pop-up dialogs</param>
        ''' <remarks></remarks>
        Public Sub Install(version As PluginVersion, Optional ByVal targetlocation As String = "",
                           Optional updatelist As Boolean = True, Optional ByVal ShowUI As Boolean = True)
            If targetlocation = "" AndAlso version.filename IsNot Nothing Then
                targetlocation = plugin_dir & "/" & version.filename
            End If

            If version.filename.EndsWith(".jar") Then
                InstallJar(version, targetlocation, updatelist, ShowUI)
            ElseIf version.filename.EndsWith(".zip") Then
                InstallZip(version, targetlocation, updatelist, ShowUI)
            Else
                MessageBox.Show(
                    lr("The file you chose to download is not supported yet.") & vbCrLf &
                    lr("At this moment only .jar and .zip files are supported."), lr("Not supported"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End Sub

        ''' <summary>
        ''' Install a jarfile
        ''' </summary>
        ''' <param name="version">Version to install</param>
        ''' <param name="targetlocation">Target location, plugins/name by default</param>
        ''' <param name="updatelist">Update the list of installed plugins</param>
        ''' <param name="ShowUI">Allow pop-up dialogs</param>
        ''' <remarks></remarks>
        Private Sub InstallJar(version As PluginVersion, Optional ByVal targetlocation As String = "",
                               Optional updatelist As Boolean = True, Optional ByVal ShowUI As Boolean = True)
            If ShowUI Then
                If _
                    MessageBox.Show(
                        lr("You are about to install") & " " & version.filename.Replace(".jar", "") & " (" &
                        version.version & ")" & vbCrLf & lr("Do you wish to continue?"), lr("Continue?"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            End If

            livebug.write(loggingLevel.Fine, "BukGetAPI",
                          "Installing plugin:" & version.filename & ", packed as jar file")

            If targetlocation = "" Then targetlocation = plugin_dir & "/" & version.filename

            Dim name As String = version.version
            If version.pluginname IsNot Nothing AndAlso version.pluginname <> "" Then _
                name = version.pluginname & " - " & version.version
            Dim fdd As New FileDownloader(version.DownloadLink, targetlocation, lr("installing plugin:") & name)
            fdd.ShowDialog()

            ReloadSingleInstalledPluginAsync(targetlocation)
            If ShowUI Then
                MessageBox.Show(
                    version.filename.Replace(".jar", "") & " (" & version.version & ") " &
                    lr("was installed succesfully"), lr("Plugin Installed"), MessageBoxButtons.OK,
                    MessageBoxIcon.Information)
            End If
            If updatelist Then InstalledPluginManager.RefreshAllInstalledPluginsAsync() 'refresh installed list
        End Sub

        ''' <summary>
        ''' Install plguins from a zip file
        ''' </summary>
        ''' <param name="version">Version to install</param>
        ''' <param name="targetlocation">Target location, plugins/name by default</param>
        ''' <param name="updatelist">Update the list of installed plugins</param>
        ''' <param name="ShowUI">Allow pop-up dialogs</param>
        ''' <remarks></remarks>
        Private Sub InstallZip(version As PluginVersion, Optional ByVal targetlocation As String = "",
                               Optional updatelist As Boolean = True, Optional ByVal ShowUI As Boolean = True)
            If ShowUI Then
                If _
                    MessageBox.Show(
                        lr("You are about to install") & " " & version.filename.Replace(".zip", "") & " (" &
                        version.version & ")" & vbCrLf & lr("Do you wish to continue?"), lr("Continue?"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            End If

            livebug.write(loggingLevel.Fine, "BukGetAPI",
                          "Installing plugin:" & version.filename & ", packed as zip file")

            If targetlocation = "" Then targetlocation = plugin_dir & "/" & version.filename

            Dim zipfile As String = common.Tmp_path & "install.zip"
            Dim extraction As String = common.Tmp_path & "/install/"

            Dim name As String = version.version
            If version.pluginname IsNot Nothing AndAlso version.pluginname <> "" Then _
                name = version.pluginname & " - " & version.version
            Dim fdd As New FileDownloader(version.DownloadLink, zipfile, lr("installing plugin:") & name)
            fdd.ShowDialog()

            Utilities.compression.decompress(extraction, zipfile)

            Dim installed As Boolean = False
            Dim folderinstalled = False

            'file is decompressed, now search the needed files
            Dim di As New DirectoryInfo(extraction)
            If di.Exists = False Then
                di.Create()
                Utilities.compression.decompress(extraction, zipfile)
            End If

            Dim fnames As New List(Of String)
            For Each File As FileInfo In di.GetFiles
                If File.Extension = ".jar" Then
                    FileSystem.CopyFile(File.FullName, plugin_dir & "/" & File.Name, True)
                    fnames.Add(File.Name)
                    installed = True
                    livebug.write(loggingLevel.Fine, "BukGetAPI", "Jar file found in .zip (L1), copied:" & File.Name)
                End If
            Next


            For Each Dir As DirectoryInfo In di.GetDirectories
                Dim copy As Boolean = False

                For Each f As String In fnames
                    If f.Contains(Dir.Name) Then _
                        copy = True : _
                            livebug.write(loggingLevel.Fine, "BukgetAPI",
                                          "Config/Info folder found in .zip, marked directory for copy:" & Dir.Name)
                Next
                If Not copy Then
                    For Each File As FileInfo In Dir.GetFiles()
                        If _
                            File.Extension = ".txt" Or File.Extension = ".yml" Or File.Extension = ".cfg" Or
                            File.Extension = ".csv" Or File.Extension = ".js" Then
                            copy = True
                            livebug.write(loggingLevel.Fine, "BukgetAPI",
                                          "Config/Info file found in .zip, marked directory for copy:" & File.Name)
                        End If
                    Next
                End If
                If copy Then _
                    FileSystem.CopyDirectory(Dir.FullName, plugin_dir & "/" & Dir.Name, True) : installed = False : _
                        folderinstalled = True

                'L2
                If Not installed Then
                    For Each File As FileInfo In Dir.GetFiles
                        If File.Extension = ".jar" Then
                            FileSystem.CopyFile(File.FullName, plugin_dir & "/" & File.Name, True)
                            installed = True
                            livebug.write(loggingLevel.Fine, "BukgetAPI",
                                          "Jar file found in .zip (L2), copied:" & File.Name)
                        End If
                    Next
                End If

                If Not folderinstalled Then
                    For Each Dir_2 As DirectoryInfo In Dir.GetDirectories
                        Dim copy_2 As Boolean = False
                        For Each f As String In fnames
                            If f.Contains(Dir_2.Name) Then _
                                copy_2 = True : _
                                    livebug.write(loggingLevel.Fine, "BukgetAPI",
                                                  "Config/Info folder found in .zip, marked directory for copy:" &
                                                  Dir_2.Name)
                        Next
                        For Each File As FileInfo In Dir_2.GetFiles()
                            If _
                                File.Extension = ".txt" Or File.Extension = ".yml" Or File.Extension = ".cfg" Or
                                File.Extension = ".csv" Or File.Extension = ".js" Then
                                copy_2 = True
                                livebug.write(loggingLevel.Fine, "BukgetAPI",
                                              "Config/Info file found in .zip, marked directory for copy:" & File.Name)
                            End If
                        Next
                        If copy_2 Then _
                            FileSystem.CopyDirectory(Dir.FullName, plugin_dir & "/" & Dir_2.Name, True) : _
                                installed = False : folderinstalled = True
                    Next
                End If

                ' end of second level searching

            Next

            livebug.write(loggingLevel.Fine, "BukgetAPI",
                          "Finished plugin installation: Succeed?" & (installed Or folderinstalled).ToString)
            'remove files

            If FileSystem.FileExists(zipfile) Then _
                FileSystem.DeleteFile(zipfile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            If FileSystem.DirectoryExists(extraction) Then _
                FileSystem.DeleteDirectory(extraction, FileIO.DeleteDirectoryOption.DeleteAllContents)


            If (installed Or folderinstalled) Then
                If ShowUI Then
                    MessageBox.Show(
                        version.filename.Replace(".zip", "") & " (" & version.version & ") " &
                        lr("was installed succesfully"), lr("Plugin Installed"), MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show(
                    version.filename.Replace(".zip", "") & " (" & version.version & ") " &
                    lr("couldn't be installed. You have to visit the project page in order to install it manually."),
                    lr("Plugin Installation failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If updatelist Then InstalledPluginManager.RefreshAllInstalledPluginsAsync() 'refresh installed list
        End Sub
    End Module
End Namespace