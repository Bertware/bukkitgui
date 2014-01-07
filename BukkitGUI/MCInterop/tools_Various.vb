Namespace MCInterop
    Module VanillaTools
        Const SERVER_FILE As String = "https://s3.amazonaws.com/MinecraftDownload/launcher/minecraft_server.jar"

        Public Function Download(target As String) As DialogResult
            Dim fd As New FileDownloader(SERVER_FILE, target, "Downloading latest Vanilla version")
            Return fd.ShowDialog()
        End Function
    End Module

    Module SpigotTools
        Const SERVER_FILE As String = "http://ci.md-5.net/job/Spigot/lastStableBuild/artifact/Spigot-Server/target/spigot.jar"
        Const SERVER_FILE_DEV As String = "http://ci.md-5.net/job/Spigot/lastSuccessfulBuild/artifact/Spigot-Server/target/spigot.jar"

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