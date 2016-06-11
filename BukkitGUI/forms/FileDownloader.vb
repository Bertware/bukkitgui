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

Imports System.Net
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO
Imports Net.Bertware.BukkitGUI.Core

Public Class FileDownloader
    Public URL As String, target As String, message As String

    Dim speed As Int64 = 0
    Dim ETA_s As TimeSpan = New TimeSpan(0)
    Dim ETA_str As String = ""
    Dim tmrspeed As New Timers.Timer
    Dim received As Int64 = 0
    Dim ToReceive As Int64 = 0
    Dim tmptarget As String = ""

    Dim webc As WebClient

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.URL = ""
        Me.target = ""
        Me.message = ""
    End Sub

    Public Sub New(URL As String, target As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.URL = URL
        Me.target = target
        Me.message = ""
    End Sub

    Public Sub New(URL As String, target As String, message As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.URL = URL
        Me.target = target
        Me.message = message
    End Sub

    Private Sub FileDownloader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Log(livebug.loggingLevel.Fine, "FileDownloader", "Starting download from " & URL)
            LblAction.Text = message
            LblStatus.Text = lr("Contacting server...")
            VPBProgress.Value = 0
            speed = 0
            old_size = 0
            old_size_2 = 0
            received = 0

            tmptarget = TmpPath & "/download.tmp"

            webc = New WebClient
            webc.Headers = header

            AddHandler webc.DownloadProgressChanged, AddressOf DownloadProgressChange
            AddHandler webc.DownloadFileCompleted, AddressOf DownloadCompleted

            tmrspeed = New Timers.Timer
            tmrspeed.Interval = 500
            tmrspeed.AutoReset = True
            AddHandler tmrspeed.Elapsed, AddressOf UpdateSpeed
            webc.DownloadFileAsync(New Uri(URL), tmptarget)
            tmrspeed.Start()
        Catch ex As Exception
            MessageBox.Show(lr("File download failed!") & vbCrLf & ex.Message, lr("Download failed!"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log(loggingLevel.Warning, "FileDownloader", "File download failed!", ex.Message)
        End Try
    End Sub

    Private Sub DownloadProgressChange(s As Object, e As DownloadProgressChangedEventArgs)
        received = e.BytesReceived
        ToReceive = e.TotalBytesToReceive - e.BytesReceived
        setprogress(e)
    End Sub

    Private Sub DownloadCompleted()
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf DownloadCompleted)
            Me.Invoke(d, New Object() {})
        Else
            Try
                tmrspeed.Enabled = False
                If FileSystem.FileExists(tmptarget) Then FileSystem.MoveFile(tmptarget, target, True)
                Me.Close()
            Catch ex As Exception
                Log(loggingLevel.Warning, "PluginUpdater", "The downloaded file could not be saved.",
                    ex.Message)
                MessageBox.Show(
                    lr(
                        "The downloaded file could not be saved. Are you allowed to write to this location? Try running as administrator"),
                    lr("Couldn't save file"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Private Sub setprogress(e As DownloadProgressChangedEventArgs)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf setprogress)
            Me.Invoke(d, New Object() {e})
        Else
            LblStatus.Text = lr("Downloading:") & " " & e.ProgressPercentage & "% [" & Math.Round(e.BytesReceived/1024) &
                             "Kb/" & Math.Round(e.TotalBytesToReceive/1024) & "Kb] @ " & speed & " Kb/s  -  ETA:" &
                             ETA_str  'only translate static part, users will understand numeric values"
            VPBProgress.Value = e.ProgressPercentage
        End If
    End Sub

    Dim old_size As Int64 = 0, old_size_2 As Int64 = 0, old_size_3 As Int64 = 0, old_speed As Int64

    Private Sub UpdateSpeed()
        old_speed = speed
        speed =
            CLng(
                Math.Round(
                    (((received - old_size)/1024*2) + ((old_size - old_size_2)/1024*2) +
                     ((old_size_2 - old_size_3)/1024*2))/2))
        old_size_3 = old_size_2
        old_size_2 = old_size
        old_size = received
        If speed > 0 Then
            ETA_s = New TimeSpan(0, 0, CInt(Math.Round((ToReceive/1024)/((speed + old_speed)/2))))
            ETA_str = ETA_s.Minutes.ToString.PadLeft(2, "0") & ":" & ETA_s.Seconds.ToString.PadLeft(2, "0")
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Try
            webc.CancelAsync()
            webc.Dispose()
            If FileSystem.FileExists(tmptarget) Then _
                FileSystem.DeleteFile(tmptarget, UIOption.OnlyErrorDialogs,
                                      RecycleOption.DeletePermanently, UICancelOption.DoNothing)
        Catch ex As Exception
            Log(loggingLevel.Severe, "FileDownloader", "Something went wrong while cancelling a download",
                ex.Message)
        Finally
            Me.Close()
        End Try
    End Sub

    'Private Sub Adbox_Click(sender As System.Object, e As System.EventArgs)
    '   MessageBox.Show(lr("You will now be redirected to a 3rd party website that is not owned by Bertware."), lr("Redirect"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    '   Process.Start("http://www.serverminer.com/clients/aff.php?aff=088")
    'End Sub
End Class