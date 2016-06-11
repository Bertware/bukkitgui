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
Imports System.Security
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.Win32


Namespace Core
    Module filelocation
        ' ! ! ! IMPORTANT ! ! !
        ' This module is loaded before config and translation.
        ' Due to this, translations and config aren't available to use here.
        '

        Public ReadOnly filelocation_xml As String = AppdataPath & "/filelocation.xml"

        Enum filelocation
            global_files
            local_files
        End Enum

        Private Const REG_VAL = "filestorage"
        Private Const REG_VAL_LOCAL = "local"
        Private Const REG_VAL_GLOBAL = "global"

        Public Sub init()
            Try
                If IsRunningOnMono Then BasePath = LocalPath : UpdateLocations() : Exit Sub

                If Not FileSystem.FileExists(filelocation_xml) Then
                    Debug.WriteLine("filelocation.xml missing")
                    Dim newlocation As String = REG_VAL_GLOBAL
                    If FileSystem.DirectoryExists(My.Application.Info.DirectoryPath & "\BukkitGUI\Config\") Then _
                        newlocation = REG_VAL_LOCAL : Debug.WriteLine("Local settings detected")
                    Debug.WriteLine("Set filelocation to " & newlocation)
                    Create_file(filelocation_xml,
                                "<filelocation><location>" & newlocation & "</location></filelocation>")
                End If


                Select Case location
                    Case filelocation.global_files
                        BasePath = AppdataPath
                        UpdateLocations()
                        Debug.WriteLine("file location set to " & BasePath)
                    Case filelocation.local_files
                        BasePath = LocalPath
                        UpdateLocations()
                        Debug.WriteLine("file location set to " & BasePath)
                End Select
            Catch ex As Exception
                BasePath = LocalPath
                UpdateLocations()
                Debug.WriteLine("AppData unavailable: file location set to " & BasePath)
            End Try
        End Sub

        Public Property location As filelocation

            Get
                Try
                    If Registry.CurrentUser.OpenSubKey(RegistryHkcuSoftware) Is Nothing Then _
                        Registry.CurrentUser.CreateSubKey(RegistryHkcuSoftware)

                    Dim regKey As RegistryKey
                    regKey = Registry.CurrentUser.OpenSubKey(RegistryHkcuSoftware, True)
                    If regKey.GetValue(REG_VAL) Is Nothing Then 'If still on the old system, use the old system

                        If IsRunningOnMono Then Return filelocation.local_files : Exit Property
                        Dim locxml As New fxml(filelocation_xml, "configlocation", True)

                        Select Case locxml.read("location", REG_VAL_GLOBAL)
                            Case REG_VAL_GLOBAL
                                regKey.SetValue(REG_VAL, REG_VAL_GLOBAL)
#If DEBUG Then
                                Return filelocation.local_files
#Else
                                Return filelocation.global_files
#End If
                            Case REG_VAL_LOCAL
                                regKey.SetValue(REG_VAL, REG_VAL_LOCAL)
                                Return filelocation.local_files
                            Case Else
                                Return Nothing
                        End Select

                    Else 'if the new system is available
                        Dim _location As String = regKey.GetValue(REG_VAL)
                        'Check for remainants of the old system
                        Try
                            '    If IO.File.Exists(filelocation_xml) Then IO.File.Delete(filelocation_xml) 'Don't do this yet, this would break different versions on the same system and prevent rollbacks
                        Catch
                            Debug.WriteLine("Couldn't remove old filelocation system")
                        End Try

#If DEBUG Then
                Return filelocation.local_files
#Else
                        Select Case _location
                            Case REG_VAL_GLOBAL
                                Return filelocation.global_files
                            Case REG_VAL_LOCAL
                                Return filelocation.local_files
                            Case Else
                                Return Nothing
                        End Select

#End If
                    End If
                Catch
                    Return Nothing
                End Try
            End Get

            Set(value As filelocation)
                If Registry.CurrentUser.OpenSubKey(RegistryHkcuSoftware) Is Nothing Then _
                    Registry.CurrentUser.CreateSubKey(RegistryHkcuSoftware)

                If value = filelocation.local_files Then
                    Try
                        Log(loggingLevel.Fine, "FileLocation", "Changing file location to local")
                        livebug.dispose()

                        If Not Directory.Exists(LocalPath) Then Directory.CreateDirectory(LocalPath)
                        FileSystem.CopyDirectory(AppdataPath, LocalPath, True)

                        Dim regKey As RegistryKey
                        regKey = Registry.CurrentUser.OpenSubKey(RegistryHkcuSoftware, True)
                        regKey.SetValue(REG_VAL, REG_VAL_LOCAL)

                        MessageBox.Show(
                            Lr(
                                "A restart is required in order for the changes to take effect. The GUI will now close itself"),
                            Lr("Restart required"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                        For Each Frm As Form In My.Application.OpenForms
                            Frm.Close()
                        Next
                    Catch ioex As InvalidOperationException
                        'Cyclic operation
                        MessageBox.Show(
                            "Something went wrong while copying files from appdata to the local folder:" & vbCrLf &
                            "Cyclic operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch dnfex As DirectoryNotFoundException
                        'source doesn't exist
                        MessageBox.Show(
                            "Something went wrong while copying files from appdata to the local folder:" & vbCrLf &
                            "Source folder doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch sex As SecurityException
                        'no permissions
                        MessageBox.Show(
                            "Something went wrong while copying files from appdata to the local folder:" & vbCrLf &
                            "You don't have the right permissions to read the source folder and/or write the destination folder",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch uaex As UnauthorizedAccessException
                        'no permissions
                        MessageBox.Show(
                            "Something went wrong while copying files from appdata to the local folder:" & vbCrLf &
                            "You don't have the right permissions to read the source folder and/or write the destination folder",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        'unknown error
                        MessageBox.Show(
                            "Something went wrong while copying files from appdata to the local folder:" & vbCrLf &
                            "unknown error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Else
                    Try
                        Log(loggingLevel.Fine, "FileLocation", "Changing file location to global")
                        livebug.dispose()

                        If FileSystem.DirectoryExists(LocalPath) Then _
                            FileSystem.CopyDirectory(LocalPath, AppdataPath, True)
                        If FileSystem.DirectoryExists(LocalPath) Then _
                            FileSystem.DeleteDirectory(LocalPath,
                                                       DeleteDirectoryOption.DeleteAllContents)

                        Dim regKey As RegistryKey
                        regKey = Registry.CurrentUser.OpenSubKey(RegistryHkcuSoftware, True)
                        regKey.SetValue(REG_VAL, REG_VAL_GLOBAL)

                        MessageBox.Show(
                            Lr(
                                "A restart is required in order for the changes to take effect. The GUI will now close itself"),
                            Lr("Restart required"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                        For Each Frm As Form In My.Application.OpenForms
                            thds_closeform(Frm)
                        Next
                    Catch ioex As InvalidOperationException
                        'Cyclic operation
                        MessageBox.Show(
                            "Something went wrong while copying files from the local folder to appdata:" & vbCrLf &
                            "Cyclic operation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch dnfex As DirectoryNotFoundException
                        'source doesn't exist
                        MessageBox.Show(
                            "Something went wrong while copying files from the local folder to appdata:" & vbCrLf &
                            "Source folder doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch sex As SecurityException
                        'no permissions
                        MessageBox.Show(
                            "Something went wrong while copying files from the local folder to appdata:" & vbCrLf &
                            "You don't have the right permissions to read the source folder and/or write the destination folder",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch uaex As UnauthorizedAccessException
                        'no permissions
                        MessageBox.Show(
                            "Something went wrong while copying files from the local folder to appdata:" & vbCrLf &
                            "You don't have the right permissions to read the source folder and/or write the destination folder",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        'unknown error
                        MessageBox.Show(
                            "Something went wrong while copying files from the local folder to appdata:" & vbCrLf &
                            "unknown error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End If
            End Set
        End Property

        Private Sub thds_closeform(frm As Form)
            If frm.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf thds_closeform)
                frm.Invoke(d, New Object() {frm})
            Else
                frm.Close()
            End If
        End Sub
    End Module
End Namespace