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
Imports System.Reflection
Imports System.Security
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Utilities


Namespace Core
    ' <summary>
    '     This module features commonly used routines, and also manages the folders used by the program
    ' </summary>
    ' <remarks></remarks>
    Module Common
        Public Const ServerEncoding = "utf-8" 'ISO-8859-1
        Public Const RegistryHkcuSoftware = "Software\Bertware\BukkitGUI\"
        'path for configuration etc. are stored here
        'note:
        'Before usage in a module, the local copy of the string value should be updated in the init routine, as the location might be changed to local
        'Localization files are always stored in the appdata directory


        'GENERAL NEEDED VARIABLES =========================================================================================================
        Public ReadOnly LocalPath As String = Path.Combine(My.Application.Info.DirectoryPath, "BukkitGUI") _
        'base path. Can be changed to [currentdirectory]/BukkitGUI for local settings
        Public ReadOnly _
            AppdataPath As String =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                             "Bertware/BukkitGUI") _
        'base path. Can be changed to [currentdirectory]/BukkitGUI for local settings

        Public BasePath As String = AppdataPath
        'base path. Can be changed to [currentdirectory]/BukkitGUI for local settings. Default is appdata.

        Public CachePath As String = Path.Combine(BasePath, "Cache") 'contains cached data
        Public LoggingPath As String = Path.Combine(BasePath, "Logging") 'contains logs
        Public TmpPath As String = Path.Combine(BasePath, "Tmp") 'Contains temporary data, like updates
        Public ConfigPath As String = Path.Combine(BasePath, "Config")
        'Contains config, server configs, backup configs, schedules

        Public ServerRoot As String = My.Application.Info.DirectoryPath
        Public IsRunningLight As Boolean = False

        Public MainWindowHandle As IntPtr

        Public LocalizationPath As String = Path.Combine(AppdataPath, "Localization") 'Contains language files

        'END OF GENERAL NEEDED VARIABLES =========================================================================================================


        ''' <summary>
        '''     Checks if the current computer is running a 64bit OS
        ''' </summary>
        ''' <value></value>
        ''' <returns>True if 64 bit</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Is64BitOs As Boolean
            Get
                Return FileSystem.DirectoryExists("C:/Program Files (x86)/")
            End Get
        End Property


        ''' <summary>
        '''     Initiate the "common" module. This will also check all folders for existance.
        ''' </summary>
        ''' <remarks>Must be done, For example FileNotFound errors could be caused if a folder wasn't created here.</remarks>
        Public Sub Init()

            If location = filelocation.filelocation.local_files Then
                If Not FileSystem.DirectoryExists(LocalPath) Then FileSystem.CreateDirectory(LocalPath)
            End If

            UpdateLocations()

            Log(loggingLevel.Fine, "Common", "Common module initialized")
        End Sub


        ''' <summary>
        '''     Update the location variables, needed after the filelocation is updated
        ''' </summary>
        ''' <remarks>the localization folder is always located in the appdata folder</remarks>
        Public Sub UpdateLocations()
            Log(loggingLevel.Fine, "Common", "Updating locations...")
            LoggingPath = Path.Combine(BasePath, "Logging") 'contains logs
            TmpPath = Path.Combine(BasePath, "Tmp") 'Contains temporary data, like updates
            ConfigPath = Path.Combine(BasePath, "Config") _
            'Contains config, server configs, backup configs, schedules
            CachePath = Path.Combine(BasePath, "Cache") 'contains cached data
            LocalizationPath = Path.Combine(BasePath, "Localization") 'Contains language files

            For Each folder In {LoggingPath, TmpPath, ConfigPath, LocalizationPath, CachePath} _
'Check if all paths are available. If a folder doesn't exist, create it.
                Try
                    If Not FileSystem.DirectoryExists(folder) Then
                        Log(loggingLevel.Fine, "Common", "Folder doesn't exist, creating: " & folder)
                        FileSystem.CreateDirectory(folder)
                    End If
                Catch pex As SecurityException
                    Log(loggingLevel.Warning, "Common",
                        "Couldn't check or create folder because of security exception: " & folder,
                        pex.Message)
                    MessageBox.Show(
                        "This folder can't be accessed! Try running as administrator. The GUI might behave wrong if this folder can't be loaded or used.",
                        "Can't access folder!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ioex As IOException
                    Log(loggingLevel.Warning, "Common",
                        "Couldn't check or create folder because of IO exception: " & folder, ioex.Message)
                    MessageBox.Show(
                        "This folder can't be accessed! Try running as administrator. The GUI might behave wrong if this folder can't be loaded or used.",
                        "Can't access folder!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    Log(loggingLevel.Severe, "Common", "Could not check or create folder: " & folder,
                        ex.Message)
                End Try
            Next
        End Sub


        ''' <summary>
        '''     Check which from 2 versions is newer. returns -1 if old version is newer, 0 if equal, 1 if new version is newer
        ''' </summary>
        ''' <param name="oldversion">the old version, x.x, x.x.x or x.x.x.x format</param>
        ''' <param name="newversion">the new version, x.x, x.x.x or x.x.x.x format</param>
        ''' <returns>-1 if old version is newer, 0 if equal, 1 if new version is newer</returns>
        ''' <remarks>This function can handle version numbers with parts up to 65535</remarks>
        Public Function CheckVersion(oldversion As String, newversion As String) As SByte
            Try
                Log(loggingLevel.Fine, "Common", "Comparing versions: " & oldversion & " - " & newversion)

                If _
                    oldversion Is Nothing OrElse oldversion = "" OrElse newversion Is Nothing OrElse
                    newversion = "" Then
                    Log(loggingLevel.Warning, "Common", "Invalid version supplied!")
                    Return 0
                    Exit Function
                End If
                oldversion = oldversion.Replace("-", ".")
                newversion = newversion.Replace("-", ".")
                oldversion = Regex.Replace(oldversion, "[a-zA-z]*\s*", "")
                newversion = Regex.Replace(newversion, "[a-zA-z]*\s*", "")

                For Each C As Char In oldversion.ToCharArray
                    If (Char.IsPunctuation(C) = False And Char.IsNumber(C) = False) Then _
                        Log(loggingLevel.Warning, "Common", "Invalid version supplied!",
                            "oldversion:" & oldversion) : Return 1 : Exit Function
                Next

                For Each C As Char In newversion.ToCharArray
                    If (Char.IsPunctuation(C) = False And Char.IsNumber(C) = False) Then _
                        Log(loggingLevel.Warning, "Common", "Invalid version supplied!",
                            "newversion:" & newversion) : Return 1 : Exit Function
                Next

                If Not oldversion.Contains(".") Then oldversion += ".0.0.0"
                Select Case oldversion.Split(".").Length
                    Case 2
                        oldversion += ".0.0"
                    Case 3
                        oldversion += ".0"
                End Select
                If oldversion.StartsWith(".") Then oldversion = "0" + oldversion

                If Not newversion.Contains(".") Then newversion += ".0.0.0"
                Select Case newversion.Split(".").Length
                    Case 2
                        newversion += ".0.0"
                    Case 3
                        newversion += ".0"
                End Select
                If newversion.StartsWith(".") Then newversion = "0" + newversion

                Log(loggingLevel.Fine, "Common", "Normalized versions: " & oldversion & " - " & newversion)

                Dim ov As New Version(oldversion)
                Dim nv As New Version(newversion)
                Dim res As SByte = nv.CompareTo(ov)
                Log(loggingLevel.Fine, "Common", "Result of version compare:" & res.ToString)
                Return res
            Catch ex As Exception
                Log(loggingLevel.Warning, "Common", "Couldn't compare versions!", ex.Message)
                Return 0
            End Try
        End Function


        ''' <summary>
        '''     Create a file with default contents
        ''' </summary>
        ''' <param name="path">The path to the file that should be created.</param>
        ''' <param name="content">The content for the created file.</param>
        ''' <returns>Returns the filepath if succesfull</returns>
        Public Function Create_file(path As String, content As String,
                                    Optional ByVal disable_logging As Boolean = False) _
            As Boolean
            Try
                If Not disable_logging Then _
                    Log(loggingLevel.Fine, "Common", "File doesn't exist, creating... " & path)
                Dim fi As New FileInfo(path)
                If Not FileSystem.DirectoryExists(fi.Directory.FullName) Then _
                    FileSystem.CreateDirectory(fi.Directory.FullName)

                Dim fs = File.Create(path)
                If Not disable_logging Then _
                    Log(loggingLevel.Fine, "Common", "Writing default content to file... " & path)
                Dim sw As New StreamWriter(fs)
                sw.Write(content)
                sw.Close()
                If Not disable_logging Then Log(loggingLevel.Fine, "Common", "File created: " & path)
                Return True
            Catch pex As SecurityException
                Log(loggingLevel.Warning, "Common",
                    "Couldn't create file because of security exception: " & path, pex.Message)
                Return False
            Catch ioex As IOException
                Log(loggingLevel.Warning, "Common", "Couldn't create file because of IO exception: " & path,
                    ioex.Message)
                Return False
            Catch uac As UnauthorizedAccessException
                If Not disable_logging Then _
                    Log(loggingLevel.Warning, "Common",
                        "Couldn't create file because of access exception: " & path, uac.Message)
                MessageBox.Show(
                    Lr(
                        "This file couldn't be created:" & path &
                        ". Probably you don't have the required permissions. Try running as administrator"),
                    Lr("Couldn't create file"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Catch ex As Exception
                If Not disable_logging Then _
                    Log(loggingLevel.Severe, "Common", "Couldn't create file, generic exception!" & path,
                        ex.Message)
                Return False
            End Try
        End Function


        ''' <summary>
        '''     Checks if a file is empty
        ''' </summary>
        ''' <param name="file_path">the old version, x.x, x.x.x or x.x.x.x format</param>
        ''' <returns>True if empty, false if not empty</returns>
        ''' <remarks>Doesn't check if file exists, might crash on not existing file</remarks>
        Public Function File_Empty(file_path As String) As Boolean
            Dim fs As FileStream = File.OpenRead(file_path)
            Dim sr As New StreamReader(fs)
            Dim r As Boolean = True
            If sr.ReadToEnd = "" Then r = True Else r = False
            sr.Close()
            fs.Close()
            Return r
        End Function


        ''' <summary>
        '''     Convert an array of string to a string formatted as val1, val2, val3,...
        ''' </summary>
        ''' <param name="array">The array to convert</param>
        ''' <returns>The array as string</returns>
        ''' <remarks></remarks>
        Public Function Serialize(array() As String) As String
            Try
                If array Is Nothing OrElse array.Length < 1 Then
                    Return ""
                    Exit Function
                End If

                Dim result As String = ""
                If array.Length > 0 Then
                    For i As UInt16 = 0 To array.Length - 1
                        If array(i) IsNot Nothing Then result += array(i).ToString & ", "
                    Next
                End If
                result = result.Trim().Trim(",")
                Return result
            Catch ex As Exception
                Log(loggingLevel.Severe, "Common", "Couldn't serialize(array)", ex.Message)
                Return ""
            End Try
        End Function


        ''' <summary>
        '''     Convert an array of string to a string formatted as val1, val2, val3,...
        ''' </summary>
        ''' <param name="array">The array to convert</param>
        ''' <param name="separator">The character used to separate items</param>
        ''' <returns>The array as string</returns>
        ''' <remarks></remarks>
        Public Function Serialize(array() As String, separator As Char) As String
            Try
                If array Is Nothing OrElse array.Length < 1 Then
                    Return ""
                    Exit Function
                End If

                Dim result As String = ""
                If array.Length > 0 Then
                    For i As UInt16 = 0 To array.Length - 1
                        If array(i) IsNot Nothing Then result += array(i).ToString & separator
                    Next
                End If
                result = result.Trim().Trim(separator)
                Return result
            Catch ex As Exception
                Log(loggingLevel.Severe, "Common", "Couldn't serialize(array,separator)", ex.Message)
                Return ""
            End Try
        End Function


        ''' <summary>
        '''     Convert an list of string to a string formatted as val1, val2, val3,...
        ''' </summary>
        ''' <param name="list">The list to convert</param>
        ''' <returns>The list as string</returns>
        ''' <remarks></remarks>
        Public Function Serialize(list As List(Of String)) As String
            Try
                If list Is Nothing OrElse list.Count < 1 Then
                    Return ""
                    Exit Function
                End If

                Dim result As String = ""
                If list.Count > 0 Then
                    For i As UInt16 = 0 To list.Count - 1
                        If list(i) IsNot Nothing Then result += list(i).ToString & ", "
                    Next
                End If
                result = result.Trim().Trim(",")
                Return result
            Catch ex As Exception
                Log(loggingLevel.Severe, "Common", "Couldn't serialize(list)", ex.Message)
                Return ""
            End Try
        End Function


        ''' <summary>
        '''     Convert an list of string to a string formatted as val1, val2, val3,...
        ''' </summary>
        ''' <param name="list">The list to convert</param>
        ''' <param name="separator">The character used to separate items</param>
        ''' <returns>The list as string</returns>
        ''' <remarks></remarks>
        Public Function Serialize(list As List(Of String), separator As Char) As String
            Try
                If list Is Nothing OrElse list.Count < 1 Then
                    Return ""
                    Exit Function
                End If

                Dim result As String = ""
                If list.Count > 0 Then
                    For i As UInt16 = 0 To list.Count - 1
                        If list(i) IsNot Nothing Then result += list(i).ToString & separator
                    Next
                End If
                result = result.Trim().Trim(separator)
                Return result
            Catch ex As Exception
                Log(loggingLevel.Severe, "Common", "Couldn't serialize (list,separator)", ex.Message)
                Return ""
            End Try
        End Function


        ''' <summary>
        '''     Copy a file from one location to another. If the folder doesn't exist, it will be created.
        ''' </summary>
        ''' <param name="location">the source location of the file</param>
        ''' <param name="destination">the destination for the file, file name included.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SafeFileCopy(location As String, destination As String,
                                     Optional ByVal overwrite As Boolean = False) As String
            Dim fi As New FileInfo(location)
            Dim nfi As New FileInfo(destination)
            If Not nfi.Directory.Exists Then nfi.Directory.Create()
            If nfi.Exists Then
                If overwrite Then nfi.Delete() : fi.CopyTo(destination) Else  'Else no copy
            Else
                fi.CopyTo(destination)
            End If
            Return destination
        End Function

        '=================================================================================================================================================
        '=================================================================================================================================================
        '       This code is responsible for loading embedded dlls, and logging uncatched errors. Working, must always be loaded.
        '=================================================================================================================================================
        WithEvents Adom As AppDomain = AppDomain.CurrentDomain

        Public Function LoadDll(sender As Object, args As ResolveEventArgs) As Assembly _
            Handles Adom.AssemblyResolve 'Load embedded DLLs

            ' livebug.write(loggingLevel.Fine, "Common", "DLL requested:" & args.Name)

            Dim resourceName As [String] = "Net.Bertware.BukkitGUI." & New AssemblyName(args.Name).Name &
                                           ".dll"
            Using stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
                If stream Is Nothing OrElse stream.Length < 1 Then Return Nothing : Exit Function
                Dim assemblyData As [Byte]() = New [Byte](stream.Length - 1) {}
                stream.Read(assemblyData, 0, assemblyData.Length)
                Return Assembly.Load(assemblyData)
            End Using
        End Function

        Public Sub HandleUnhandledException(sender As Object, e As UnhandledExceptionEventArgs) _
            Handles Adom.UnhandledException _
'Catch unhandled exceptions, and log them. Also ask user to report them.
            WriteUnhandledError(e)
        End Sub

        '=================================================================================================================================================
        '=================================================================================================================================================


        ''' <summary>
        '''     Check if a server is available
        ''' </summary>
        ''' <param name="url">The URL to check</param>
        ''' <returns>True if available</returns>
        ''' <remarks></remarks>
        Public Function CheckServer(url As String) As Boolean
            If IsRunningOnMono Then
                Return True
            Else
                Dim res As Boolean = My.Computer.Network.Ping(url, 1000)
                Log(loggingLevel.Fine, "Common", "Checked server " & url & " - available:" & res.ToString)
                Return res
            End If
        End Function

        Public Sub Reset()
            If FileSystem.DirectoryExists(BasePath) Then _
                FileSystem.DeleteDirectory(BasePath, DeleteDirectoryOption.DeleteAllContents)
        End Sub


        ''' <summary>
        '''     Stop/Close everything
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Dispose()
            Disable()

            livebug.dispose()

            Process.GetCurrentProcess.Kill() 'For some reason the GUI stays open in the backgorund. workaround
        End Sub

        Function GetFileSize(ByVal FilePath As String) As UInt64
            Try
                Dim fi As FileInfo = New FileInfo(FilePath)
                Return fi.Length
            Catch ex As Exception
                Log(loggingLevel.Warning, "common", "GetFileSize(): Couldn't get filesize: " & ex.Message,
                    FilePath)
                Return False
            End Try
        End Function

        Function GetFolderSize(ByVal DirPath As String, Optional IncludeSubFolders As Boolean = True) _
            As UInt64

            Dim lngDirSize As UInt64
            Dim objFileInfo As FileInfo
            Dim objDir As DirectoryInfo = New DirectoryInfo(DirPath)
            Dim objSubFolder As DirectoryInfo

            Try

                'add length of each file
                For Each objFileInfo In objDir.GetFiles()
                    lngDirSize += objFileInfo.Length
                Next

                'call recursively to get sub folders
                'if you don't want this set optional
                'parameter to false 
                If IncludeSubFolders Then
                    For Each objSubFolder In objDir.GetDirectories()
                        lngDirSize += GetFolderSize(objSubFolder.FullName)
                    Next
                End If

            Catch Ex As Exception
                Return 0
            End Try

            Return lngDirSize
        End Function

        Public Function GetSpaceInDrive(DriveLetter As String) As UInt64
            Try
                Dim driveInfo As DriveInfo = New DriveInfo(DriveLetter)
                If driveInfo.IsReady Then
                    Return driveInfo.AvailableFreeSpace
                    Exit Function
                End If
                Return 0
            Catch ex As Exception
                Log(loggingLevel.Warning, "common", "Couldn't get free space on drive", DriveLetter)
                Return 0
            End Try
        End Function

        Public Function ByteToMb(size As UInt64) As UInt64
            Dim bc As New ByteConverter
            Return bc.ConvertAsRound(size, ByteConverter.size.size_byte, ByteConverter.size.size_mbyte)
        End Function

        Public Function ConvertByte(size As UInt64, targettype As ByteConverter.size,
                                    Optional ByVal decimals As Byte = 2) As UInt64
            Dim bc As New ByteConverter
            Return bc.ConvertAsRound(size, ByteConverter.size.size_byte, targettype)
        End Function

        Public ReadOnly Property IsRunningOnMono As Boolean
            Get
                Return Type.GetType("Mono.Runtime") IsNot Nothing
            End Get
        End Property
    End Module

    Public Class ByteConverter
        Enum size
            size_bit = 0.125
            size_byte = 1
            size_kbyte = 1024
            size_mbyte = 1048576
            size_gbyte = 1073741824
        End Enum

        Public Function ConvertAsDecimal(fromSize As UInt64, fromType As size, ToType As size) As Decimal
            Return (fromSize * (CType(fromType, UInt64) / CType(ToType, UInt64)))
        End Function

        Public Function ConvertAsRound(fromSize As UInt64, fromType As size, ToType As size,
                                       Optional ByVal decimals As Byte = 2) As UInt64
            Return Math.Round((fromSize * (CType(fromType, UInt64) / CType(ToType, UInt64))), decimals)
        End Function
    End Class
End Namespace