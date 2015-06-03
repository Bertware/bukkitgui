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

Public Class InstalledPluginDialog
    Public plugin As plugindescriptor

    Public Sub New(mainspace As String)
        Me.InitializeComponent()
        If plugin IsNot Nothing Then Me.plugin = plugin.ToSafeObject
    End Sub

    Public Sub New(plugin As plugindescriptor)
        Me.InitializeComponent()
        If plugin IsNot Nothing Then Me.plugin = plugin.ToSafeObject
    End Sub

    Private Sub InstalledPluginDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If plugin Is Nothing Then Exit Sub

        lblName.Text = lr("Name:") & " " & plugin.name
        Me.Text = lr("Plugin details:") & " " & plugin.name
        LblAuthors.Text = lr("Author(s):") & " " & Serialize(plugin.authors)
        LblPath.Text = lr("Path:") & " " & plugin_dir & "/" & plugin.filename
        lblVersion.Text = lr("Version:") & " " & plugin.version
        lblSoftdepend.Text = lr("Softdepend:") & " " & Serialize(plugin.softdepend)
        lblNamespace.Text = lr("Main namespace:") & " " & plugin.main

        For Each Command As pluginCommand In plugin.commands
            Dim _
                lvi As _
                    New ListViewItem(
                        {Command.name, Command.description, Command.usage, Serialize(Command.aliases)})
            ALVCommands.Items.Add(lvi)
        Next

        For Each permission As pluginPermission In plugin.permissions
            Dim lvi As New ListViewItem({permission.name, permission.description})
            ALVPermissions.Items.Add(lvi)
            If permission.children IsNot Nothing AndAlso permission.children.Count > 0 Then
                For Each child As pluginPermissionChild In permission.children
                    Dim clvi As New ListViewItem({child.name, "child"})
                    clvi.IndentCount = 4
                    ALVPermissions.Items.Add(clvi)
                Next
            End If
        Next
    End Sub

    Private Sub btnLatestVersion_Click(sender As Object, e As EventArgs) Handles btnLatestVersion.Click
        If plugin IsNot Nothing Then ShowPluginDialogByNamespace(plugin.main)
    End Sub
End Class