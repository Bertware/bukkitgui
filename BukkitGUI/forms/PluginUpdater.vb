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

Imports System.Threading
Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.MCInterop

Public Class PluginUpdater
    ' <summary>
    '     The list of plugins to update
    ' </summary>
    ' <remarks></remarks>
    Public plugins As List(Of plugindescriptor)


    ''' <summary>
    '''     Dictionarry that links every plugin to a bukget object.
    ''' </summary>
    ''' <remarks></remarks>
    Private dc As IDictionary(Of plugindescriptor, BukgetPlugin)

    Private _updated As Boolean = False


    ''' <summary>
    '''     Immediatly update the plugins when the form is loaded.
    ''' </summary>
    ''' <remarks></remarks>
    Public UpdateOnLoad As Boolean = False

    Public ReadOnly Property Updated As Boolean
        Get
            Return _updated
        End Get
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        plugins = New List(Of plugindescriptor)
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(plugin As plugindescriptor)

        ' This call is required by the designer.
        InitializeComponent()
        Me.plugins = New List(Of plugindescriptor)
        Me.plugins.Add(plugin)
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Public Sub New(plugins As List(Of plugindescriptor))

        ' This call is required by the designer.
        InitializeComponent()
        Me.plugins = New List(Of plugindescriptor)
        For Each entry As plugindescriptor In plugins
            Me.plugins.Add(entry)
        Next
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(plugins() As plugindescriptor)

        ' This call is required by the designer.
        InitializeComponent()
        Me.plugins = New List(Of plugindescriptor)
        For Each entry As plugindescriptor In plugins
            Me.plugins.Add(entry)
        Next
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    ''' <summary>
    '''     Init the form + start  async load of data.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PluginUpdater_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If plugins Is Nothing OrElse plugins.Count < 1 Then Exit Sub
        If UpdateOnLoad = True Then
            BtnUpdate.Enabled = False
            ALVPlugins.Enabled = False
            BtnClose.Enabled = False
            ChkCheckAll.Enabled = False
            ChkForce.Enabled = False
            CBBukkitBuild.Enabled = False
        End If
        Dim t As New Thread(AddressOf LoadAsync)
        t.IsBackground = True
        t.Start()
    End Sub


    ''' <summary>
    '''     Load all plugin data async.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAsync()
        Try
            If plugins Is Nothing Then Exit Sub

            dc = New Dictionary(Of plugindescriptor, BukgetPlugin)

            For i As UInt16 = 0 To plugins.Count - 1
                Dim plugin As plugindescriptor = plugins(i)
                If plugin IsNot Nothing AndAlso plugin.name IsNot Nothing Then
                    Me.SetStatus(lr("Loading plugin data:") & plugin.name & "(" & i + 1 & "/" & plugins.Count & ")")
                    Log(loggingLevel.Fine, "PluginUpdater",
                        "Loading plugin data:" & plugin.name & "(" & i + 1 & "/" & plugins.Count & ")")
                    If dc.ContainsKey(plugin) = False Then
                        dc.Add(plugin, GetPluginInfoByNamespace(plugin.main, False))
                        Log(loggingLevel.Fine, "PluginUpdater",
                            "Added plugin data:" & plugin.name & "(" & i + 1 & "/" & plugins.Count & ")")
                    Else
                        Log(loggingLevel.Fine, "PluginUpdater",
                            "Discarded plugin data:" & plugin.name & "(" & i + 1 & "/" & plugins.Count & ")")
                    End If

                    Dim tmpp As Double = Math.Round(100 * ((i + 1) / plugins.Count))
                    If tmpp > 100 Then tmpp = 100
                    Me.SetProgress(CByte(tmpp))

                End If
            Next
            Log(loggingLevel.Fine, "PluginUpdater", "Loaded plugin data (" & plugins.Count & ")")
            LoadUI()
        Catch ex As Exception
            Log(loggingLevel.Severe, "PluginUpdater", "Severe exception in LoadAsync routine!", ex.Message)
        End Try
    End Sub


    ''' <summary>
    '''     Load the user interface with the listview items, once all data is loaded.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadUI()
        Try
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf LoadUI)
                Me.Invoke(d, New Object())
            Else
                If dc Is Nothing Then Exit Sub
                Me.SetStatus(lr("Loading plugin data to screen..."))
                Log(loggingLevel.Fine, "PluginUpdater", "Loading UI data (" & dc.Count & ")")
                For Each entry As KeyValuePair(Of plugindescriptor, BukgetPlugin) In dc
                    Try
                        Dim lvi As New ListViewItem
                        If entry.Value Is Nothing Then
                            lvi =
                                New ListViewItem(
                                    {entry.Key.name, entry.Key.version, lr("No data available"), lr("No data available")})
                            lvi.BackColor = Color.LightGray
                            lvi.Tag = "FALSE"
                        Else
                            If entry.Value.versions Is Nothing OrElse entry.Value.versions.Count < 1 Then
                                lvi =
                                    New ListViewItem(
                                        {entry.Key.name, entry.Key.version, lr("No downloads available"),
                                         lr("No downloads available")})
                                lvi.BackColor = Color.LightGray
                                lvi.Tag = "FALSE"
                            Else
                                lvi =
                                    New ListViewItem(
                                        {entry.Key.name, entry.Key.version, entry.Value.versions(0).version,
                                         Serialize(entry.Value.versions(0).builds, ",")})
                                lvi.Tag = "TRUE"
                                If CheckVersion(entry.Key.version, entry.Value.versions(0).version) = 1 Then
                                    lvi.Checked = True
                                Else
                                    lvi.Checked = False
                                End If
                            End If
                        End If
                        ALVPlugins.Items.Add(lvi)
                    Catch ex As Exception
                        If entry.Key IsNot Nothing AndAlso entry.Key.name IsNot Nothing Then
                            Log(loggingLevel.Warning, "PluginUpdater",
                                "Couldn't load plugin:" & entry.Key.name, ex.Message)
                        Else
                            Log(loggingLevel.Warning, "PluginUpdater", "Couldn't load plugin", ex.Message)
                        End If
                    End Try
                Next
                Me.SetStatus(lr("Idle"))
            End If
            If UpdateOnLoad Then Plugins_Update()
        Catch ex As Exception
            Log(loggingLevel.Severe, "PluginUpdater", "Severe exception in LoadUI routine!", ex.Message)
        End Try
    End Sub


    ''' <summary>
    '''     Close the form, prevent cross thread exceptions.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseThisForm() Handles BtnClose.Click
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf CloseThisForm)
            Me.Invoke(d, New Object() {})
        Else
            Me.Close()
        End If
    End Sub

    Private Sub SetStatus(text As String)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf SetStatus)
            Me.Invoke(d, New Object() {text})
        Else
            Me.lblStatus.Text = lr("Status:") & " " & text
        End If
    End Sub

    Private Sub SetProgress(progress As Byte)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf SetProgress)
            Me.Invoke(d, New Object() {progress})
        Else
            Me.Pbar.Value = progress
        End If
    End Sub


    ''' <summary>
    '''     Update each plugin, if it's checked
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Plugins_Update() Handles BtnUpdate.Click
        Try
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf Plugins_Update)
                If Not (Me.Disposing Or Me.IsDisposed) Then Me.Invoke(d, New Object() {})
            Else
                Log(loggingLevel.Info, "PluginUpdater", "Starting plugins update")
                If ALVPlugins.CheckedItems.Count < 1 Then Exit Sub
                Dim i As UInt16 = 1
                For Each item As ListViewItem In ALVPlugins.CheckedItems
                    If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "TRUE" Then
                        For Each pair As KeyValuePair(Of plugindescriptor, BukgetPlugin) In dc
                            Try
                                If pair.Key.name = item.SubItems(0).Text Then
                                    If _
                                        pair.Value Is Nothing OrElse pair.Value.versions Is Nothing OrElse
                                        pair.Value.versions(0) Is Nothing Then Exit For
                                    If _
                                        Me IsNot Nothing AndAlso Not (Me.Disposing Or Me.IsDisposed) AndAlso
                                        Not (lblStatus.IsDisposed Or lblStatus.Disposing) Then _
                                        Me.SetStatus(
                                            lr("Updating plugin") & " " & pair.Key.name & " " & lr("to version") & " " &
                                            pair.Value.versions(0).version & " (" & (i) & "/" &
                                            ALVPlugins.CheckedItems.Count & ")")
                                    Log(loggingLevel.Fine, "PluginUpdater", "Updating plugin:" & pair.Key.name)
                                    UpdatePluginToVersion(pair.Key, pair.Value.versions(0), False)
                                    Exit For
                                End If
                            Catch ex As Exception
                                Log(loggingLevel.Warning, "PluginUpdater",
                                    "Exception in Update routine for " & pair.Key.name, ex.Message)
                            End Try
                        Next
                    Else
                        If _
                            Me IsNot Nothing AndAlso Not (Me.Disposing Or Me.IsDisposed) AndAlso
                            Not (lblStatus.IsDisposed Or lblStatus.Disposing) Then _
                            Me.SetStatus(
                                lr("Skipping plugin") & " " & item.SubItems(0).Text & " - " & lr("cannot update") & "(" &
                                i + 1 & " / " & dc.Count & ")")
                        Log(loggingLevel.Fine, "PluginUpdater", "Skipping plugin:" & item.SubItems(0).Text)
                    End If
                    Dim tmpp As Double = Math.Round(100 * ((i + 1) / dc.Count))
                    If tmpp > 100 Then tmpp = 100
                    Me.SetProgress(CByte(tmpp))
                    i = i + 1
                Next
                If _
                    Me IsNot Nothing AndAlso Not (Me.Disposing Or Me.IsDisposed) AndAlso
                    Not (lblStatus.IsDisposed Or lblStatus.Disposing) Then Me.SetStatus(lr("Idle"))
                If _
                    Me IsNot Nothing AndAlso BtnClose IsNot Nothing AndAlso Not (Me.Disposing Or Me.IsDisposed) AndAlso
                    Not (BtnClose.Disposing Or BtnClose.IsDisposed) Then BtnClose.Enabled = True
                RefreshAllInstalledPluginsAsync()
                If Me IsNot Nothing AndAlso Not (Me.Disposing Or Me.IsDisposed) Then Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(lr("Something went wrong while updating. Please check if all plugins are updated."),
                            lr("Something went wrong"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log(loggingLevel.Warning, "PluginUpdater", "Severe exception in Update routine!", ex.Message)
        End Try
    End Sub

    Private Sub ChkCheckAll_CheckedChanged(sender As Object, e As EventArgs) _
        Handles ChkCheckAll.CheckedChanged
        If ChkCheckAll.Checked Then
            For Each item As ListViewItem In ALVPlugins.Items
                item.Checked = True
            Next
        Else
            For Each item As ListViewItem In ALVPlugins.Items
                item.Checked = False
            Next
        End If
    End Sub
End Class