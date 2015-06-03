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
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities

Namespace MCInterop
    Module BukkitTools
        Private _lrb As dlb_download, _lbeta As dlb_download, _ldev As dlb_download

        Public Event BukkitVersionFetchComplete()

        Public ReadOnly Property Latest_Recommended As UInt16
            Get
                Return _lrb.build
            End Get
        End Property

        Public ReadOnly Property Latest_Beta As UInt16
            Get
                Return _lbeta.build
            End Get
        End Property

        Public ReadOnly Property Latest_Dev As UInt16
            Get
                Return _ldev.build
            End Get
        End Property

        Public ReadOnly Property Recommended_info As dlb_download
            Get
                Return _lrb
            End Get
        End Property

        Public ReadOnly Property Beta_info As dlb_download
            Get
                Return _lbeta
            End Get
        End Property

        Public ReadOnly Property Dev_info As dlb_download
            Get
                Return _ldev
            End Get
        End Property

        
        ''' <summary>
        '''     Do we have the data already?
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Fetched As Boolean
            Get
                Return (_lrb IsNot Nothing AndAlso _lbeta IsNot Nothing AndAlso _ldev IsNot Nothing)
            End Get
        End Property

        
        ''' <summary>
        '''     Fetch versions on another thread
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub FetchLatestVersionsAsync()
            Dim t As New Thread(AddressOf FetchLatestVersions)
            t.IsBackground = True
            t.Name = "[BukkitTools]FetchLatestVersions"
            t.Start()
        End Sub

        
        ''' <summary>
        '''     Fetch and store the bukkit versions
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub FetchLatestVersions()
            _lrb = getlatest(BukkitVersionType.rb)
            Log(livebug.loggingLevel.Fine, "BukkitTools",
                "Fetched latest recommended bukkit version (" & _lrb.build & ")")
            _lbeta = getlatest(BukkitVersionType.beta)
            Log(loggingLevel.Fine, "BukkitTools", "Fetched latest beta bukkit version (" & _lbeta.build & ")")
            _ldev = getlatest(BukkitVersionType.dev)
            Log(loggingLevel.Fine, "BukkitTools", "Fetched latest dev bukkit version (" & _ldev.build & ")")
            RaiseEvent BukkitVersionFetchComplete()
        End Sub

        
        ''' <summary>
        '''     download bukkit
        ''' </summary>
        ''' <param name="v">file to download, as dlb item</param>
        ''' <param name="target">target location, where the file will be stored. This can overwrite an existing file</param>
        ''' <returns></returns>
        ''' <remarks>See also: dlb class</remarks>
        Public Function Download(v As BukkitVersionType, target As String) As DialogResult
            If running Then
                Dim d As New ServerStopDialog
                If d.ShowDialog <> DialogResult.OK Then
                    Return DialogResult.Cancel
                    Exit Function
                End If
            End If
            Dim info As dlb_download = getlatest(v) 'Always update to the latest
            Dim fd As New FileDownloader(info.file_url, target, lr("Downloading latest Bukkit version"))
            Return fd.ShowDialog()
        End Function

        
        ''' <summary>
        '''     Download a custom build number
        ''' </summary>
        ''' <param name="build"></param>
        ''' <param name="target"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DownloadCustom(build As UInt16, target As String) As DialogResult
            If running Then
                Dim d As New ServerStopDialog
                If d.ShowDialog <> DialogResult.OK Then
                    Return DialogResult.Cancel
                    Exit Function
                End If
            End If
            Dim info As dlb_download = GetCustomBuild(build) 'Always update to the latest
            Dim fd As New FileDownloader(info.file_url, target, lr("Downloading Bukkit build") & " " & build)
            Return fd.ShowDialog()
        End Function

        
        ''' <summary>
        '''     Run java to get the current bukkit versions
        ''' </summary>
        ''' <param name="java">java path</param>
        ''' <param name="bukkitpath">bukkit path</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCurrentBukkitVersion(java As javaAPI.javaVersion, bukkitpath As String) _
            As BukkitVersionDetails
            '"C:/Program Files/java/jre7/bin/java.exe" -Xincgc -Xmx32M -jar "Craftbukkit.jar" -v
            'git-Bukkit-1.2.5-R4.0-b2222jnks
            If bukkitpath.ToLower.Contains("bukkit") = False Then
                Return New BukkitVersionDetails()
                Exit Function
            End If

            Try
                Dim p As New Process
                Log(loggingLevel.Fine, "BukkitTools", "Determining current bukkit version")
                With p.StartInfo
                    .FileName = GetExec(java)
                    .Arguments = "-Xincgc -Xmx32M -jar """ & bukkitpath & """ -v"
                    .RedirectStandardOutput = True
                    .RedirectStandardError = True
                    .UseShellExecute = False
                    .CreateNoWindow = True
                End With
                If FileSystem.FileExists(p.StartInfo.FileName) = False Then _
                    Log(loggingLevel.Warning, "BukkitTools",
                        "Could not determine current bukkit version: Java not found") : _
                        Return New BukkitVersionDetails : Exit Function
                If FileSystem.FileExists(bukkitpath) = False Then _
                    Log(loggingLevel.Warning, "BukkitTools",
                        "Could not determine current bukkit version: Bukkit not found") : _
                        Return New BukkitVersionDetails : Exit Function
                p.Start()
                Dim sr As New StreamReader(p.StandardOutput.BaseStream)
                Dim vstring As String = sr.ReadToEnd
                Log(loggingLevel.Fine, "BukkitTools", "Current version string : " & vstring)
                Dim v As BukkitVersionDetails = New BukkitVersionDetails(vstring)
                Log(loggingLevel.Fine, "BukkitTools", "Current version : " & v.Build)
                Return v
            Catch pex As SecurityException
                MessageBox.Show(
                    lr(
                        "The current bukkit version could not be determined. It seems like you don't have permissions to do this. Try running the GUI as administator"),
                    lr("Insufficient rights"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Log(loggingLevel.Warning, "BukkitTools",
                    "Security error in GetCurrentBukkitVersion! " & pex.Message)
                Return New BukkitVersionDetails
            Catch ex As Exception
                Log(loggingLevel.Warning, "BukkitTools",
                    "Could not determine current bukkit version, exception: " & ex.Message)
                Return New BukkitVersionDetails
            End Try
        End Function

        
        ''' <summary>
        '''     parse a version string (jenkins etc)
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function ParseVersionString(text As String) As Integer
            Dim pattern As String = "(#\d\d\d\d|#\d\d\d|b\d\d\d\djnks|b\d\d\djnks)"
            Dim match = Regex.Match(text, pattern)
            Dim chars() As Char = {"#", "b", "j", "n", "k", "s"}
            If match Is Nothing OrElse match.Value Is Nothing OrElse match.Value = "" Then Return 0 Else _
                Return CInt(match.Value.Trim(chars))
        End Function

        
        ''' <summary>
        '''     parse a bukkit version (console output)
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function ParseVersionStringBukkitVer(text As String) As String
            Dim pattern As String = "(\d.\d.\d|\d.\d)(\-R\d|)"
            Dim match = Regex.Match(text, pattern)
            If match Is Nothing OrElse match.Value Is Nothing OrElse match.Value = "" Then Return 0 Else _
                Return match.Value
        End Function

        
        ''' <summary>
        '''     parse an MC version. Can be in the same version string as the bukkit version
        ''' </summary>
        ''' <param name="text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function ParseVersionStringMCVer(text As String) As String
            Dim pattern As String = "MC: (\d.\d.\d|\d.\d)"
            Dim match = Regex.Match(text, pattern)
            If match Is Nothing OrElse match.Value Is Nothing OrElse match.Value = "" Then Return 0 Else _
                Return match.Value
        End Function
    End Module

    Public Class BukkitVersionDetails
        Public Build As UInt32 = 0, BukkitVer As String = "", MCVer As String = ""

        Public Sub New()
            Build = 0
            BukkitVer = ""
            MCVer = ""
        End Sub

        Public Sub New(VersionString As String)
            Build = ParseVersionString(VersionString)
            BukkitVer = ParseVersionStringBukkitVer(VersionString)
            MCVer = ParseVersionStringMCVer(VersionString)
        End Sub

        Public Shadows Function ToString() As String
            Return BukkitVer & " (#" & Build & ")"
        End Function
    End Class
End Namespace
