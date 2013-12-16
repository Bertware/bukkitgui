Imports Net.Bertware.BukkitGUI.MCInterop
Imports Net.Bertware.BukkitGUI.Core
Imports System.Xml

Namespace Utilities
    Public Module BackupManager
        Public backups As List(Of BackupSetting)

        Public backup_xml_path As String = common.Config_path & "/backups.xml"
        Public backup_xml As fxml

        Public Event BackupsLoaded()
        Const version As String = "1.0"

        Public ReadOnly Property Loaded As Boolean
            Get
                Return backups IsNot Nothing
            End Get
        End Property

        Public Sub init()
            If Not FileIO.FileSystem.FileExists(backup_xml_path) Then _
                common.Create_file(backup_xml_path, "<backups version=""" & version & """></backups>")
            backup_xml = New fxml(backup_xml_path, "BackupManager", True)
            LoadAllBackups()
        End Sub

        Public Sub LoadAllBackups()
            backups = New List(Of BackupSetting)
            livebug.write(loggingLevel.Fine, "Backupmanager", "Loading backups...")
            Dim elements As XmlNodeList = backup_xml.GetElementsByName("backup")
            If elements IsNot Nothing AndAlso elements.Count > 0 Then
                For i = 0 To elements.Count - 1
                    livebug.write(loggingLevel.Fine, "Backupmanager",
                                  "Parsing backup " & i + 1 & " out of " & elements.Count)
                    Dim xmle As XmlElement = elements(i)
                    If xmle IsNot Nothing AndAlso xmle.GetAttribute("name") IsNot Nothing Then
                        Try
                            Dim bs As New BackupSetting
                            bs.name = xmle.GetAttribute("name")
                            Dim folders_e As XmlElement = xmle.GetElementsByTagName("folders")(0)
                            For Each folder As String In folders_e.InnerText.Split(";")
                                bs.folders.Add(folder.Trim(";").Trim)
                            Next

                            Dim destination_e As XmlElement = xmle.GetElementsByTagName("destination")(0)
                            bs.destination = destination_e.InnerText

                            Dim compression_e As XmlElement = xmle.GetElementsByTagName("compression")(0)
                            bs.compression = (compression_e.InnerText.ToLower = "true")

                            backups.Add(bs)

                            livebug.write(loggingLevel.Fine, "Backupmanager",
                                          "Loaded backup:" & bs.name & " :Backup enabled")
                        Catch ex As Exception
                            livebug.write(loggingLevel.Warning, "Backupmanager", "Could not load backup:" & ex.Message)
                        End Try
                    Else
                        livebug.write(loggingLevel.Warning, "Backupmanager", "Skipped backup! Wrong XML")
                        backup_xml.RemoveElement(CType(backup_xml.GetElementByName("backup")(i), Xml.XmlElement))
                        MessageBox.Show(
                            lr(
                                "One of your backup profiles wasn't loaded: the file is probably corrupt! The backup profile was removed."),
                            lr("Can't load backup profile"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Next
            End If
            livebug.write(loggingLevel.Fine, "Backupmanager", "Loaded backups: " & backups.Count & " backups loaded")
            RaiseEvent BackupsLoaded()
        End Sub

        Public Sub ReloadAllBackups()
            livebug.write(loggingLevel.Fine, "Backupmanager", "Reloading backups...", "BackupManager")
            LoadAllBackups()
        End Sub

        Public Sub addBackup(bs As BackupSetting)
            Try
                Dim toplevelelement As XmlElement = backup_xml.write("Backup", "", "", Nothing, True)
                toplevelelement.SetAttribute("name", bs.name)

                Dim folders_element As XmlElement = backup_xml.Document.CreateElement("folders")
                folders_element.InnerText = common.serialize(bs.folders, ";")
                toplevelelement.AppendChild(folders_element)

                Dim destination_element As XmlElement = backup_xml.Document.CreateElement("destination")
                destination_element.InnerText = bs.destination
                toplevelelement.AppendChild(destination_element)
                backup_xml.save()

                Dim compression_element As XmlElement = backup_xml.Document.CreateElement("compression")
                compression_element.InnerText = bs.compression.ToString
                toplevelelement.AppendChild(compression_element)
                backup_xml.save()

                backups.Add(bs)
                RaiseEvent BackupsLoaded()
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "Backupmanager", "Severe error in addBackup! " & ex.Message)
            End Try
        End Sub

        Public Sub saveBackup(ByRef OldBackup As BackupSetting, ByRef NewBackup As BackupSetting)
            livebug.write(loggingLevel.Fine, "Backupmanager",
                          "Updating backup: " & OldBackup.name & " - Will be replaced by its updated version")
            deleteBackup(OldBackup)
            addBackup(NewBackup)
            RaiseEvent BackupsLoaded()
        End Sub

        Public Sub deleteBackup(ByRef bs As BackupSetting)
            Try
                backups.Remove(bs)
                backup_xml.RemoveElement(backup_xml.getElementByAttribute("backup", "name", bs.name))
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "Backupmanager", "Severe error in deleteBackup(bs)!", ex.Message)
            End Try
            RaiseEvent BackupsLoaded()
        End Sub

        Public Sub deleteBackup(name As String)
            Try
                backups.Remove(GetBackupByName(name))
                backup_xml.RemoveElement(backup_xml.getElementByAttribute("backup", "name", name))
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "Backupmanager", "Severe error in deleteBackup(name)!", ex.Message)
            End Try
            RaiseEvent BackupsLoaded()
        End Sub

        Public Function GetBackupByName(name As String) As BackupSetting
            Try
                Dim result As BackupSetting = Nothing
                For Each bs As BackupSetting In backups
                    If bs.name = name Then result = bs
                Next
                Return result
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "Backupmanager", "Severe error in GetBackupByName! " & ex.Message)
                Return Nothing
            End Try
        End Function

        Public Sub import()
            livebug.write(loggingLevel.Fine, "BackupManager", "Starting Import routine")
            Dim ofd As New OpenFileDialog
            ofd.Filter = "Backup manager file (*.bs)|*.bs"
            ofd.Multiselect = False
            ofd.Title = "Import Backups"
            If ofd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then _
                livebug.write(loggingLevel.Fine, "BackupManager", "Import cancelled") : Exit Sub
            Try
                Dim impxml As New fxml(ofd.FileName, "Backupmanager", True)
                For Each element As XmlElement In impxml.GetElementsByName("Backup")
                    Dim tmpnode As XmlElement = backup_xml.Document.ImportNode(element, True)
                    backup_xml.Document.DocumentElement.AppendChild(tmpnode)
                Next
                backup_xml.save()
                livebug.write(loggingLevel.Fine, "BackupManager", "Import finished!", "Backupmanager")
                ReloadAllBackups()
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "BackupManager", "Error while importing Backups", ex.Message)
                MessageBox.Show(lr("Error while importing the Backup! Is this a valid file?"), lr("Import failed!"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Public Sub export(name As String)
            livebug.write(loggingLevel.Fine, "BackupManager", "Starting Export routine (single), backup:" & name)
            Dim sfd As New SaveFileDialog
            sfd.Title = "Export backup settings"
            sfd.Filter = "Backup manager file (*.bs)|*.bs"
            sfd.OverwritePrompt = True
            sfd.SupportMultiDottedExtensions = True
            sfd.DefaultExt = ".Backup"
            sfd.AddExtension = True
            If sfd.ShowDialog = DialogResult.Cancel Then _
                livebug.write(loggingLevel.Fine, "BackupManager", "Export cancelled") : Exit Sub
            Try
                common.Create_file(sfd.FileName, "<backups version=""" & version & """></backups>")
                Dim expxml As New fxml(sfd.FileName, "Backupmanager", True)
                Dim tmpnode As XmlElement = expxml.Document.ImportNode(
                    backup_xml.getElementByAttribute("backup", "name", name), True)
                expxml.Document.DocumentElement.AppendChild(tmpnode)
                expxml.save()
                livebug.write(loggingLevel.Fine, "BackupManager", "Export finished!")
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "BackupManager", "Error while exporting Backup", ex.Message)
                MessageBox.Show(lr("Error while exporting the Backup!"), lr("Export failed!"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End Try
        End Sub

        Public Sub export(names As List(Of String))
            livebug.write(loggingLevel.Fine, "BackupManager", "Starting Export routine (multiple)")
            Dim sfd As New SaveFileDialog
            sfd.Title = "Export backup settings"
            sfd.Filter = "Backup manager file (*.bs)|*.bs"
            sfd.OverwritePrompt = True
            sfd.DefaultExt = ".bs"
            sfd.AddExtension = True
            sfd.SupportMultiDottedExtensions = True
            If sfd.ShowDialog = DialogResult.Cancel Then _
                livebug.write(loggingLevel.Fine, "BackupManager", "Export cancelled") : Exit Sub
            Try
                common.Create_file(sfd.FileName, "<backups version=""" & version & """></backups>")
                Dim expxml As New fxml(sfd.FileName, "Backupmanager", True)
                For Each name As String In names
                    Dim tmpnode As XmlElement = expxml.Document.ImportNode(
                        backup_xml.getElementByAttribute("Backup", "name", name), True)
                    expxml.Document.DocumentElement.AppendChild(tmpnode)
                Next
                expxml.save()
                livebug.write(loggingLevel.Fine, "BackupManager", "Export finished!")
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "BackupManager", "Error while exporting backups", ex.Message)
                MessageBox.Show(lr("Error while exporting the backups!"), lr("Export failed!"), MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End Try
        End Sub
    End Module

    Public Class BackupSetting
        Public name As String
        Public folders As List(Of String)
        Public destination As String
        Public compression As Boolean

        Public Sub New()
            Me.name = ""
            Me.folders = New List(Of String)
            Me.destination = ""
            Me.compression = False
        End Sub

        Public Sub New(name As String, folders As List(Of String), destination As String,
                       Optional ByVal compression As Boolean = False)
            Me.name = name
            Me.folders = folders
            Me.destination = destination
            Me.compression = compression
        End Sub

        Public Sub execute(showUI As Boolean)
            Try

                Dim foldername As String = CreateName()
                Dim tmp_bu As String = My.Computer.FileSystem.CombinePath(common.Tmp_path, foldername)

                Dim dest_hnd As String = ParseParameters(destination)


                livebug.write(loggingLevel.Fine, "BackupManager", "Calculating disk space needed...")
                Dim tsize As UInt64 = 0
                For Each folder As String In folders
                    Try
                        tsize += common.GetFolderSize(folder, True)
                    Catch ex As Exception
                    End Try
                Next

                Dim drivematch As System.Text.RegularExpressions.MatchCollection =
                        System.Text.RegularExpressions.Regex.Matches(dest_hnd, "^\w:\\")
                If drivematch.Count > 0 Then
                    If Not IO.Directory.Exists(drivematch(0).Value) Then
                        MessageBox.Show(
                            lr(
                                "The backup can't be performed: the following drive could not be found. Is this a removeable device (e.g. USB stick)") &
                            vbCrLf & drivematch(0).Value, lr("Drive not found"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                        livebug.write(loggingLevel.Warning, "BackupManager", "BAckup failed: drive not present")
                        Exit Sub
                    End If
                End If

                If Not FileIO.FileSystem.DirectoryExists(dest_hnd) Then FileIO.FileSystem.CreateDirectory(dest_hnd)

                If compression = False Then
                    tmp_bu = My.Computer.FileSystem.CombinePath(dest_hnd, foldername)

                    livebug.write(loggingLevel.Fine, "BackupManager",
                                  "Needed:" & common.ByteToMb(tsize) & "Mb - Available:" &
                                  common.ByteToMb(GetSpaceInDrive(Me.destination.Substring(0, 1))) & "Mb")
                    If _
                        common.ByteToMb(GetSpaceInDrive(Me.destination.Substring(0, 1))) - 512 - common.ByteToMb(tsize) <
                        0 Then
                        MessageBox.Show(
                            "You don't have enough free space at one fo your disks to execute this backup! cancelling...",
                            "Backup failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                Else
                    livebug.write(loggingLevel.Fine, "BackupManager",
                                  "Needed:" & common.ByteToMb(tsize) & "Mb - Available:" &
                                  common.ByteToMb(GetSpaceInDrive("c")) & "Mb")
                    If common.ByteToMb(GetSpaceInDrive("c")) - 512 - common.ByteToMb(tsize) < 0 Then
                        MessageBox.Show(
                            "You don't have enough free space at one fo your disks to execute this backup! cancelling...",
                            "Backup failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    livebug.write(loggingLevel.Fine, "BackupManager",
                                  "Needed:" & common.ByteToMb(tsize) & "Mb - Available:" &
                                  common.ByteToMb(GetSpaceInDrive(Me.destination.Substring(0, 1))) & "Mb")
                    If _
                        common.ByteToMb(GetSpaceInDrive(Me.destination.Substring(0, 1))) - 512 - common.ByteToMb(tsize) <
                        0 Then
                        MessageBox.Show(
                            "You don't have enough free space at one fo your disks to execute this backup! cancelling...",
                            "Backup failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If

                If FileIO.FileSystem.DirectoryExists(tmp_bu) Then Exit Sub 'this means another backup is running

                If folders Is Nothing OrElse folders.Count = 0 Then
                    If showUI Then _
                        MessageBox.Show(
                            lr("Backup cancelled") & " : " & name & vbCrLf &
                            lr("Reason: Nothing selected to backup. Select folders first."),
                            lr("Backup cancelled"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    livebug.write(loggingLevel.Fine, "BackupManager",
                                  "Backup cancelled:" & name & " - Nothing selected to backup.")
                    Exit Sub
                End If

                For Each folder As String In folders
                    Try
                        Dim tg As String

                        drivematch = System.Text.RegularExpressions.Regex.Matches(folder, "^\w:\\")
                        If drivematch.Count > 0 Then
                            If Not IO.Directory.Exists(drivematch(0).Value) Then
                                MessageBox.Show(
                                    lr(
                                        "The backup can't be performed: the following drive could not be found. Is this a removeable device (e.g. USB stick)") &
                                    vbCrLf & drivematch(0).Value, lr("Drive not found"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                                livebug.write(loggingLevel.Warning, "BackupManager", "BAckup failed: drive not present")
                                Exit Sub
                            End If
                        End If

                        If folder.Contains("\") Then
                            tg = My.Computer.FileSystem.CombinePath(tmp_bu, folder.Split("\").Last)
                        Else
                            tg = folder
                        End If
                        If Not FileIO.FileSystem.DirectoryExists(tg) Then FileIO.FileSystem.CreateDirectory(tg)
                        If FileIO.FileSystem.DirectoryExists(folder) Then _
                            FileIO.FileSystem.CopyDirectory(folder, tg, True)
                    Catch ex As Exception
                        livebug.write(loggingLevel.Warning, "BackupManager", "Couldn't backup this folder: " & folder,
                                      ex.Message)
                        MessageBox.Show(
                            lr("Couldn't backup this folder:") & " " & folder & vbCrLf & lr("Reason:") & " " &
                            ex.Message & lr("Data:") & " " & ex.Data.ToString, "Couldn't backup folder",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Next

                If compression Then
                    If FileIO.FileSystem.DirectoryExists(dest_hnd) = False Then _
                        FileIO.FileSystem.CreateDirectory(dest_hnd)
                    If FileIO.FileSystem.DirectoryExists(tmp_bu) Then _
                        Utilities.compression.compress(tmp_bu, dest_hnd.TrimEnd("/") & "/" & foldername & ".zip")
                    If FileIO.FileSystem.DirectoryExists(tmp_bu) Then _
                        FileIO.FileSystem.DeleteDirectory(tmp_bu, FileIO.DeleteDirectoryOption.DeleteAllContents)
                Else
                    'If FileIO.FileSystem.DirectoryExists(tmp_bu) Then FileIO.FileSystem.CopyDirectory(tmp_bu, dest_hnd.TrimEnd("/").TrimEnd & "/" & foldername)
                    'ALREADY MOVED TO CORRECT LOCATION
                End If

                If showUI Then _
                    MessageBox.Show(lr("Backup completed!") & " : " & name, lr("Backup completed"), MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)
            Catch ioex As IO.IOException
                MessageBox.Show(
                    lr("An error occured while creating a backup for the following backup scheme:") & Me.name & vbCrLf &
                    lr(
                        "The backup might be incomplete or corrupt. Make sure all task settings are valid. The error has been logged"),
                    lr("Backup failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                livebug.write(loggingLevel.Warning, "BackupManager",
                              "An exception occured while executing this backup:" & name, ioex.Message)
            Catch uaex As UnauthorizedAccessException
                MessageBox.Show(
                    lr("An error occured while creating a backup for the following backup scheme:") & Me.name & vbCrLf &
                    lr(
                        "The backup might be incomplete or corrupt. Make sure all task settings are valid. The error has been logged"),
                    lr("Backup failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                livebug.write(loggingLevel.Warning, "BackupManager",
                              "An exception occured while executing this backup:" & name, uaex.Message)
            Catch sex As Security.SecurityException
                MessageBox.Show(
                    lr("An error occured while creating a backup for the following backup scheme:") & Me.name & vbCrLf &
                    lr(
                        "The backup might be incomplete or corrupt. Make sure all task settings are valid. The error has been logged"),
                    lr("Backup failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                livebug.write(loggingLevel.Warning, "BackupManager",
                              "An exception occured while executing this backup:" & name, sex.Message)
            Catch ex As Exception
                MessageBox.Show(
                    lr("An error occured while creating a backup for the following backup scheme:") & Me.name & vbCrLf &
                    lr(
                        "The backup might be incomplete or corrupt. Make sure all task settings are valid. The error has been logged"),
                    lr("Backup failed"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                livebug.write(loggingLevel.Warning, "BackupManager",
                              "An exception occured while executing this backup:" & name, ex.Message)
            End Try
        End Sub

        Private Function ParseParameters(text As String) As String
            Try
                livebug.write(loggingLevel.Fine, "BackupManager", "Parsing action parameters for " & text)

                text = text.Replace("%server-cpu%", performance.server_cpu)
                text = text.Replace("%gui-cpu%", performance.gui_cpu)
                text = text.Replace("%total-cpu%", performance.total_cpu)

                text = text.Replace("%server-ram%", performance.server_mem)
                text = text.Replace("%gui-ram%", performance.gui_mem)
                text = text.Replace("%total-ram%", performance.total_mem)

                text = text.Replace("%gui-dir%", My.Application.Info.DirectoryPath)
                text = text.Replace("%gui-ver%", My.Application.Info.Version.ToString)

                text = text.Replace("%time-long%", Date.Now.ToLongTimeString)
                text = text.Replace("%time-short%", Date.Now.ToShortTimeString)
                text = text.Replace("%date-long%", Date.Now.ToLongDateString)
                text = text.Replace("%date-short%", Date.Now.ToShortDateString)

                text = text.Replace("%server-running%", server.running.ToString.ToLower)

                If playerList IsNot Nothing Then _
                    text = text.Replace("%players%", common.serialize(server.playerNameList, ",")) Else _
                    text = text.Replace("%players%", "INVALID")
                If playerList IsNot Nothing Then _
                    text = text.Replace("%playercount%", common.serialize(server.playerNameList, ",")) Else _
                    text = text.Replace("%players%", "INVALID")
                If playerList IsNot Nothing AndAlso playerList.Count > 0 Then _
                    text = text.Replace("%lastplayer%", server.playerList.Last.name) Else _
                    text = text.Replace("%lastplayer%", "INVALID")
                livebug.write(loggingLevel.Fine, "BackupManager", "Parsed parameters: " & text)
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "BackupManager", "Severe error in ParseParameters!", ex.Message)
            End Try
            Return text
        End Function

        Private Function CreateName() As String
            Try
                Return _
                    "backup_" & name & "_" & Date.Now.Year.ToString.PadLeft(4, "0") & "-" &
                    Date.Now.Month.ToString.PadLeft(2, "0") & "-" & Date.Now.Day.ToString.PadLeft(2, "0") & "_" _
                    & Date.Now.Hour.ToString.PadLeft(2, "0") & "-" & Date.Now.Minute.ToString.PadLeft(2, "0") & "-" &
                    Date.Now.Second.ToString.PadLeft(2, "0")
            Catch ex As Exception
                Return _
                    "backup_UNKOWN_NAME_" & Date.Now.Year.ToString.PadLeft(4, "0") & "-" &
                    Date.Now.Month.ToString.PadLeft(2, "0") & "-" & Date.Now.Day.ToString.PadLeft(2, "0") & "_" _
                    & Date.Now.Hour.ToString.PadLeft(2, "0") & "-" & Date.Now.Minute.ToString.PadLeft(2, "0") & "-" &
                    Date.Now.Second.ToString.PadLeft(2, "0")
            End Try
        End Function
    End Class
End Namespace