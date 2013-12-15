Imports System.Threading

Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.Utilities
Imports Net.Bertware.BukkitGUI.MCInterop

Imports System.IO

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

        Public ReadOnly Property Fetched As Boolean
            Get
                Return (_lrb IsNot Nothing AndAlso _lbeta IsNot Nothing AndAlso _ldev IsNot Nothing)
            End Get
        End Property

        Public Sub FetchLatestVersionsAsync()
            Dim t As New Thread(AddressOf FetchLatestVersions)
            t.IsBackground = True
            t.Name = "[BukkitTools]FetchLatestVersions"
            t.Start()
        End Sub

        Public Sub FetchLatestVersions()
            _lrb = dlb.getlatest(BukkitVersionType.rb)
            livebug.write(loggingLevel.Fine, "BukkitTools", "Fetched latest recommended bukkit version (" & _lrb.build & ")")
            _lbeta = dlb.getlatest(BukkitVersionType.beta)
            livebug.write(loggingLevel.Fine, "BukkitTools", "Fetched latest beta bukkit version (" & _lbeta.build & ")")
            _ldev = dlb.getlatest(BukkitVersionType.dev)
            livebug.write(loggingLevel.Fine, "BukkitTools", "Fetched latest dev bukkit version (" & _ldev.build & ")")
            RaiseEvent BukkitVersionFetchComplete()
        End Sub

        Public Function Download(v As dlb.BukkitVersionType, target As String) As DialogResult
            If server.running Then
                Dim d As New ServerStopDialog
                If d.ShowDialog <> DialogResult.OK Then
                    Return DialogResult.Cancel : Exit Function
                End If
            End If
            Dim info As dlb_download = dlb.getlatest(v) 'Always update to the latest
            Dim fd As New FileDownloader(info.file_url, target, lr("Downloading latest Bukkit version"))
            Return fd.ShowDialog()
        End Function

        Public Function DownloadCustom(build As UInt16, target As String) As DialogResult
            If server.running Then
                Dim d As New ServerStopDialog
                If d.ShowDialog <> DialogResult.OK Then
                    Return DialogResult.Cancel : Exit Function
                End If
            End If
            Dim info As dlb_download = dlb.GetCustomBuild(build) 'Always update to the latest
            Dim fd As New FileDownloader(info.file_url, target, lr("Downloading Bukkit build") & " " & build)
            Return fd.ShowDialog()
        End Function

        Public Function GetCurrentBukkitVersion(java As javaVersion, bukkitpath As String) As BukkitVersionDetails
            '"C:/Program Files/java/jre7/bin/java.exe" -Xincgc -Xmx32M -jar "Craftbukkit.jar" -v
            'git-Bukkit-1.2.5-R4.0-b2222jnks
            If bukkitpath.ToLower.Contains("bukkit") = False Then
                Return New BukkitVersionDetails()
                Exit Function
            End If

            Try
                Dim p As New Process
                livebug.write(loggingLevel.Fine, "BukkitTools", "Determining current bukkit version")
                With p.StartInfo
                    .FileName = javaAPI.GetExec(java)
                    .Arguments = "-Xincgc -Xmx32M -jar """ & bukkitpath & """ -v"
                    .RedirectStandardOutput = True
                    .RedirectStandardError = True
                    .UseShellExecute = False
                    .CreateNoWindow = True
                End With
                If FileIO.FileSystem.FileExists(p.StartInfo.FileName) = False Then livebug.write(loggingLevel.Warning, "BukkitTools", "Could not determine current bukkit version: Java not found") : Return New BukkitVersionDetails : Exit Function
                If FileIO.FileSystem.FileExists(bukkitpath) = False Then livebug.write(loggingLevel.Warning, "BukkitTools", "Could not determine current bukkit version: Bukkit not found") : Return New BukkitVersionDetails : Exit Function
                p.Start()
                Dim sr As New StreamReader(p.StandardOutput.BaseStream)
                Dim vstring As String = sr.ReadToEnd
                livebug.write(loggingLevel.Fine, "BukkitTools", "Current version string : " & vstring)
                Dim v As BukkitVersionDetails = New BukkitVersionDetails(vstring)
                livebug.write(loggingLevel.Fine, "BukkitTools", "Current version : " & v.Build)
                Return v
            Catch pex As System.Security.SecurityException
                MessageBox.Show(lr("The current bukkit version could not be determined. It seems like you don't have permissions to do this. Try running the GUI as administator"), lr("Insufficient rights"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                livebug.write(loggingLevel.Warning, "BukkitTools", "Security error in GetCurrentBukkitVersion! " & pex.Message)
                Return New BukkitVersionDetails
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "BukkitTools", "Could not determine current bukkit version, exception: " & ex.Message)
                Return New BukkitVersionDetails
            End Try

        End Function

        Function ParseVersionString(text As String) As Integer
            Dim pattern As String = "(#\d\d\d\d|#\d\d\d|b\d\d\d\djnks|b\d\d\djnks)"
            Dim match = System.Text.RegularExpressions.Regex.Match(text, pattern)
            Dim chars() As Char = {"#", "b", "j", "n", "k", "s"}
            If match Is Nothing OrElse match.Value Is Nothing OrElse match.Value = "" Then Return 0 Else Return CInt(match.Value.Trim(chars))
        End Function

        Function ParseVersionStringBukkitVer(text As String) As String
            Dim pattern As String = "(\d.\d.\d|\d.\d)(\-R\d|)"
            Dim match = System.Text.RegularExpressions.Regex.Match(text, pattern)
            If match Is Nothing OrElse match.Value Is Nothing OrElse match.Value = "" Then Return 0 Else Return match.Value
        End Function

        Function ParseVersionStringMCVer(text As String) As String
            Dim pattern As String = "MC: (\d.\d.\d|\d.\d)"
            Dim match = System.Text.RegularExpressions.Regex.Match(text, pattern)
            If match Is Nothing OrElse match.Value Is Nothing OrElse match.Value = "" Then Return 0 Else Return match.Value
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
            Build = BukkitTools.ParseVersionString(VersionString)
            BukkitVer = BukkitTools.ParseVersionStringBukkitVer(VersionString)
            MCVer = BukkitTools.ParseVersionStringMCVer(VersionString)
        End Sub

        Public Shadows Function ToString() As String
            Return BukkitVer & " (#" & Build & ")"
        End Function
    End Class
End Namespace
