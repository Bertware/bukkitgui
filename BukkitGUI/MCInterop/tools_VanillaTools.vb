Namespace MCInterop
    Module VanillaTools
        Const SERVER_FILE As String = "https://s3.amazonaws.com/MinecraftDownload/launcher/minecraft_server.jar"

        Public Function Download(target As String) As DialogResult
            Dim fd As New FileDownloader(SERVER_FILE, target, "Downloading latest Vanilla version")
            Return fd.ShowDialog()
        End Function
    End Module
End Namespace