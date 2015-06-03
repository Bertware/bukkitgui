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

Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.MCInterop

Public Class BukgetPluginDialog
    Public Plugin As BukgetPlugin
    Public file As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(plugin As BukgetPlugin)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Plugin = plugin
    End Sub

    Public Sub New(plugin As SimpleBukgetPlugin)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Plugin = GetPluginInfoByNamespace(plugin.main)
    End Sub

    Public Sub New(main As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Plugin = GetPluginInfoByNamespace(main)
    End Sub

    Private Sub PluginDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Plugin Is Nothing Then
            Log(livebug.loggingLevel.Warning, "BukGetPluginDialog",
                "Load of plugin data failed! No plugin defined.")
            MessageBox.Show(lr("No data for this plugin available"), lr("no data available"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Me.Close()
        Else
            Try
                loadplugin()
            Catch ex As Exception
                Log(loggingLevel.Warning, "BukGetPluginDialog", "Visualisation of plugin data failed!",
                    ex.Message)
                MessageBox.Show(lr("Could not load plugin data for this plugin"), lr("Could not load plugin"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End Try
        End If
    End Sub

    Private Sub loadplugin()
        If Plugin Is Nothing Then Exit Sub

        If Plugin.name IsNot Nothing AndAlso Plugin.name <> "" Then
            lblName.Text = lr("Name:") & " " & Plugin.name
        Else
            If Plugin.slug IsNot Nothing AndAlso Plugin.slug <> "" Then lblName.Text = lr("Name:") & " " & Plugin.slug
        End If

        If Plugin.Description IsNot Nothing AndAlso Plugin.name <> "" Then _
            lblDescription.Text = lr("Description:") & " " & Plugin.Description
        lblStatus.Text = lr("Status:") & " " & Plugin.status.ToString
        If Plugin.BukkitDevLink IsNot Nothing Then ALlblWebsite.Text = lr("Website:") & " " & Plugin.BukkitDevLink

        If Plugin.Author IsNot Nothing Then lblAuthors.Text = lr("Authors:") & " " & Serialize(Plugin.Author)

        If Plugin.Category IsNot Nothing Then _
            lblcategories.Text = lr("Categories:") & " " & Serialize(Plugin.Category).ToString.Replace("_", " ")

        For Each Version As PluginVersion In Plugin.versions
            Dim str(5) As String
            str(0) = Version.ReleaseDate.Year.ToString & "/" & Version.ReleaseDate.Month.ToString.PadLeft(2, "0") & "/" &
                     Version.ReleaseDate.Day.ToString.PadLeft(2, "0") & " - " &
                     Version.ReleaseDate.Hour.ToString.PadLeft(2, "0") & ":" &
                     Version.ReleaseDate.Minute.ToString.PadLeft(2, "0")
            str(1) = Version.version.ToString
            str(2) = Version.filename
            str(3) = ""
            For Each build As String In Version.builds
                str(3) = build & ","
            Next
            str(3) = str(3).Trim(",")
            str(4) = Version.type.ToString
            Dim lvi As New ListViewItem(str)
            LVVersions.Items.Add(lvi)
        Next
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnInstall_Click() Handles BtnInstall.Click, LVVersions.DoubleClick
        If LVVersions.SelectedItems.Count > 0 Then
            If file = "" Then Install(Plugin.versions(LVVersions.SelectedIndices(0)))
            If file <> "" Then Install(Plugin.versions(LVVersions.SelectedIndices(0)), file)
        Else
            If file = "" Then Install(Plugin.versions(0))
            If file <> "" Then Install(Plugin.versions(0), file)
        End If
        Me.Close()
    End Sub
End Class