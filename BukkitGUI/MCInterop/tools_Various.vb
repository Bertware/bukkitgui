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

Namespace MCInterop
    Module VanillaTools
        Const SERVER_FILE As String = "https://s3.amazonaws.com/MinecraftDownload/launcher/minecraft_server.jar"

        Public Function Download(target As String) As DialogResult
            Dim fd As New FileDownloader(SERVER_FILE, target, "Downloading latest Vanilla version")
            Return fd.ShowDialog()
        End Function
    End Module

    Module SpigotTools
        Const SERVER_FILE As String =
            "http://ci.md-5.net/job/Spigot/lastStableBuild/artifact/Spigot-Server/target/spigot.jar"

        Const SERVER_FILE_DEV As String =
            "http://ci.md-5.net/job/Spigot/lastSuccessfulBuild/artifact/Spigot-Server/target/spigot.jar"

        Public Function Download(target As String) As DialogResult
            Dim fd As New FileDownloader(SERVER_FILE, target, "Downloading latest stable Spigot version")
            Return fd.ShowDialog()
        End Function

        Public Function DownloadDev(target As String) As DialogResult
            Dim fd As New FileDownloader(SERVER_FILE_DEV, target, "Downloading latest Spigot version")
            Return fd.ShowDialog()
        End Function
    End Module
End Namespace