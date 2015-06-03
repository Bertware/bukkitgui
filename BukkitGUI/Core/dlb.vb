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

'dlb- dl.bukkit.org
'provides functionality to download bukkit, get latest versions and so on


'example output from http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/view/build-2150/
'<root>
'<broken_reason/>
'<build_number>2150</build_number>
'<created>2012-04-05 11:14:24</created>
'<url>http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/view/01028_1.2.5-R1.1/</url>
'<is_broken>False</is_broken>
'<html_url>http://dl.bukkit.org/downloads/craftbukkit/view/01028_1.2.5-R1.1/</html_url>
'<project>
'   <name>CraftBukkit</name>
'   <github_project_url>https://github.com/Bukkit/CraftBukkit</github_project_url>
'   <url>http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/</url>
'   <html_url>http://dl.bukkit.org/downloads/craftbukkit/</html_url>
'   <download_count>3215933</download_count><slug>craftbukkit</slug>
'</project>
'<version>1.2.5-R1.1</version>
'<file>
'   <url>http://dl.bukkit.org/downloads/craftbukkit/get/01028_1.2.5-R1.1/craftbukkit-dev.jar</url>
'   <checksum_md5>5ce4ab8b0bc31e6547eb47cef2507eeb</checksum_md5>
'   <size>11102004</size>
'</file>
'<commit>
'    <ref>caee2402f59d12df6338c3d95ce2ec411a8c55db</ref>
'   <html_url>https://github.com/Bukkit/CraftBukkit/commit/caee2402f59d12df6338c3d95ce2ec411a8c55db</html_url>
'</commit>
'<target_filename>craftbukkit-dev.jar</target_filename>
'<download_count>16800</download_count>
'<channel>
'<filename_format>%(project_slug)s-dev.jar</filename_format>
'<url>http://dl.bukkit.org/api/1.0/downloads/channels/dev/</url>
'<priority>1000</priority><name>Development Build</name>
'<slug>dev</slug>
'</channel>
'</root>
Imports System.Net
Imports System.Text.RegularExpressions
Imports Net.Bertware.BukkitGUI.Core

Namespace MCInterop
    Module dlb
        Const dlb_api = "http://dl.bukkit.org/api/1.0/downloads/"

        Enum BukkitVersionType
            dev 'development build
            rb 'recommended build
            beta 'beta build
        End Enum

        
        ''' <summary>
        '''     Check if the needed servers are available
        ''' </summary>
        ''' <param name="warn">Should a dialog box be showed when the server is unavailable?</param>
        ''' <returns>True if ok, false if a necessary server is unavailable</returns>
        ''' <remarks>Warning not implemented yet</remarks>
        Public Function CheckServers(Optional ByVal warn As Boolean = False) As Boolean
            Return CheckServer(dlb_api)
        End Function

        
        ''' <summary>
        '''     Get the file info about the latest version.
        ''' </summary>
        ''' <param name="version">The version to get info about (recommended/beta/dev)</param>
        ''' <returns>Returns a dlb_download item, based upon the received XML</returns>
        Public Function getlatest(version As BukkitVersionType) As dlb_download
            Dim xml As String = getcontents(build_url(version)) 'get xml
            Dim dlbd As New dlb_download(xml) 'create dlb_download from xml
            Return dlbd 'return result
        End Function

        
        ''' <summary>
        '''     Get the file info about a specified bukkit build
        ''' </summary>
        ''' <param name="build">The build number. Between 1325 and the current build</param>
        ''' <returns>a dlb_download item, containing all the info</returns>
        ''' <remarks></remarks>
        Public Function GetCustomBuild(build As UInt16) As dlb_download
            If build < 1325 Then build = 1325
            Return New dlb_download(getcontents(build_url(build)))
        End Function

        Private Function build_url(version As BukkitVersionType) As String
            Return "http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/view/latest-" & version.ToString & "/" _
            'build URL for dlb api - http://dl.bukkit.org/about/
        End Function

        Private Function build_url(build As UInt16) As String
            Return "http://dl.bukkit.org/api/1.0/downloads/projects/craftbukkit/view/build-" & build.ToString & "/" _
            'build URL for dlb api - http://dl.bukkit.org/about/
        End Function

        Private Function getcontents(url As String) As String
            Try
                Dim webc As New WebClient 'new webclient
                webc.Headers = header 'get header collection from serverinteraction module
                webc.Headers.Add(HttpRequestHeader.Accept, "application/xml") 'make sure received data is in XML format
                Return webc.DownloadString(url) 'return result
            Catch ex As Exception
                Return Nothing
                Log(loggingLevel.Severe, "dlb", "Could not download data from " & url, ex.Message)
            End Try
        End Function
    End Module

    Public Class dlb_download
        Public name As String,
               file_size As UInt64,
               build As UInt16,
               created As DateTime,
               html_url As String,
               target_filename As String,
               file_url As String,
               version As String


        Public Sub New(Xml As String)
            Try
                If Xml Is Nothing OrElse Xml = "" OrElse Xml.Contains("<") = False OrElse Xml.Contains(">") = False Then
                    Log(loggingLevel.Warning, "dlb",
                        "Could not create dlb_download object, xml invalid. Xml:" & Xml)
                    Exit Sub
                End If
                Dim bxml As New fxml 'use fxml to parse the xml quickly
                bxml.Owner = "dlb_download" 'for logging purposes
                bxml.LoadXML(Xml.ToLower)
                name = bxml.read("Name", "Craftbukkit")
                build = CInt(bxml.read("build_number", "0"))
                file_size = CInt(bxml.read("size", "0", "file"))
                html_url = bxml.read("html_url", "", "project")
                target_filename = bxml.read("target_filename", "craftbukkit.jar", "")
                file_url = bxml.read("url", "", "file")
                version = bxml.read("version", "", "").ToUpper

                If file_url.StartsWith("http") = False Then file_url = "http://dl.bukkit.org/" & file_url.Trim("/")

                Dim created_string As String = bxml.read("created", "", "") 'e.g. 2012-04-05 11:14:24
                Dim dtstring As String =
                        Regex.Match(created_string,
                                    "\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}").ToString
                dtstring = dtstring.Replace(":", "-").Replace(" ", "-")
                Dim dtarr() As String = dtstring.Split("-")
                created = New DateTime(CInt(dtarr(0)), CInt(dtarr(1)), CInt(dtarr(2)), CInt(dtarr(3)), CInt(dtarr(4)),
                                       CInt(dtarr(5)))
            Catch ex As Exception
                Log(loggingLevel.Severe, "dlb", "Severe error while trying to create dlb object!", ex.Message)
            End Try
        End Sub

        Public Sub New()
            name = "Craftbukkit"
            build = 0
            file_size = 0
            html_url = ""
            target_filename = "craftbukkit.jar"
            file_url = ""
            version = ""
            created = New DateTime(0)
        End Sub

        Public Sub New(name As String, build As String)
            Me.name = name
            Me.build = build
            Me.version = ""
        End Sub
    End Class
End Namespace