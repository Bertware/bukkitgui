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

Namespace Utilities
    Module ErrorManager
        Dim errors As List(Of ServerError)

        Public Event ErrorCaught(e As ServerError)
        Public Event ErrorLVICreated(lvi As ListViewItem)

        Public ReadOnly Property ErrorList As List(Of ServerError)
            Get
                Return errors
            End Get
        End Property

        Dim _hidewarn As Boolean, _hideerr As Boolean, _hidetrc As Boolean

        Public Property Hide_Warnings As Boolean
            Get
                Return _hidewarn
            End Get
            Set(value As Boolean)
                _hidewarn = value
                writeAsBool("hide_warning", value, "output")
            End Set
        End Property

        Public Property Hide_Errors As Boolean
            Get
                Return _hideerr
            End Get
            Set(value As Boolean)
                _hideerr = value
                writeAsBool("hide_error", value, "output")
            End Set
        End Property

        Public Property Hide_Stacktrace As Boolean
            Get
                Return _hidetrc
            End Get
            Set(value As Boolean)
                _hidetrc = value
                writeAsBool("hide_stacktrace", value, "output")
            End Set
        End Property

        Public Sub init()
            errors = New List(Of ServerError)

            AddHandler serverOutputHandler.WarningReceived, AddressOf WarningReceived
            AddHandler serverOutputHandler.SevereReceived, AddressOf SevereReceived
            AddHandler serverOutputHandler.StackTraceReceived, AddressOf StackTraceReceived
            _hidewarn = readAsBool("hide_warning", False, "output")
            _hideerr = readAsBool("hide_error", False, "output")
            _hidetrc = readAsBool("hide_stacktrace", False, "output")
        End Sub

        Private Sub WarningReceived(e As ErrorReceivedEventArgs)
            CreateError(MessageType.warning, e.message.Substring(e.message.IndexOf("]") + 1))
        End Sub

        Private Sub SevereReceived(e As ErrorReceivedEventArgs)
            CreateError(MessageType.severe, e.message.Substring(e.message.IndexOf("]") + 1))
        End Sub

        Private Sub StackTraceReceived(e As StackTraceReceivedEventArgs)
            If e.message.ToLower.Contains("[severe]") Then e.message = e.message.Split("]")(1).TrimStart("]").Trim
            CreateError(MessageType.javastacktrace, e.message)
        End Sub

        Private Sub CreateError(Type As serverOutputHandler.MessageType, text As String)
            Dim e As ServerError = New ServerError(errors.Count, Type, text)
            errors.Add(e)
            RaiseEvent ErrorLVICreated(GetLVI(e))
        End Sub

        Public Function GetLVI(e As ServerError) As ListViewItem
            Dim imageid As Integer = 0
            If e.type = MessageType.severe Or e.type = MessageType.javastacktrace Then imageid = 1
            Dim _
                lvi As _
                    New ListViewItem({e.id.ToString.PadLeft(3, "0"), e.type.ToString, Date.Now.ToLongTimeString, e.text})
            lvi.ImageIndex = imageid
            lvi.ForeColor = getMessageColor(e.type)
            Return lvi
        End Function
    End Module

    Module ErrorAnalyzer
        Public Function GetCause(text As String) As ErrorCause
            If text.Contains("**** SERVER IS RUNNING IN OFFLINE/INSECURE MODE!") Or
               text.Contains("The server will make no attempt to authenticate usernames. Beware.") Or
               text.Contains(
                   "While this makes the game possible to play without internet access, it also opens up the ability for hackers to connect with any username they choose.") Or
               text.Contains("To change this, set ""online-mode"" to ""true"" in the server.properties file.") Then
                Return New ErrorCause_setting("online-mode", "True", text)

            ElseIf text.Contains("**** FAILED TO BIND TO PORT!") Or
                   text.Contains("The exception was: java.net.BindException: Address already in use: JVM_Bind") Or
                   text.Contains("Perhaps a server is already running on that port?") Then
                Return _
                    New ErrorCause_Other(
                        "Another program is using this port. Maybe there is still another java process running?", text)

            ElseIf text.Contains("Error occurred while enabling") And text.Contains("(Is it up to date?)") Then
                Dim name As String = text.Substring(text.IndexOf("enabling") + 5).Trim.Split(" ")(1)
                If name Is Nothing Then name = "unkown"
                Return _
                    New ErrorCause_Plugin(GetInstalledPluginByName(name, True), "Outdated plugin" & ": " & name, text)

            ElseIf text.Contains("Could not load") And text.Contains("in folder") Then
                Dim name As String = text.Substring(text.IndexOf("plugins/") + 8).Split("'")(0)
                If name Is Nothing Then name = "unkown"
                Return New ErrorCause_Plugin(GetPluginByFileName(name), "Caused by plugin" & ": " & name, text)

            ElseIf _
                text.Contains("at") And text.Contains(".") And text.Contains("java:") And text.Contains("(") And
                text.Contains(")") _
                And Not text.Contains("org.bukkit") And Not text.Contains("net.minecraft") Then
                Dim plg As plugindescriptor = GetInstalledPluginByNamespace(text)
                If plg IsNot Nothing Then _
                    Return New ErrorCause_Plugin(plg, "Unspecified plugin problem in plugin " & plg.name, text) Else _
                    Return New ErrorCause_Plugin(plg, "Unspecified plugin problem, plugin unkown", text)

            ElseIf _
                text.Contains("at") And text.Contains(".") And (text.Contains("java:") Or text.Contains("SourceFile:")) And
                text.Contains("(") And text.Contains(")") _
                And (text.Contains("org.bukkit") Or text.Contains("net.minecraft")) Then
                Return _
                    New ErrorCause_Other("Unspecified error. Please check all other errors before worrying about this.",
                                         text)
            ElseIf _
                text.Split("[") IsNot Nothing AndAlso text.Split("[").Length > 1 AndAlso text.Split("]") IsNot Nothing AndAlso
                text.Split("]").Length > 1 Then
                Dim name As String = text
                name = name.Trim.Trim("[").Split("]")(0).Trim("]").Trim("[").Trim
                Dim plg As plugindescriptor = GetInstalledPluginByName(name, True)
                If plg IsNot Nothing Then _
                    Return New ErrorCause_Plugin(plg, "Error caused and handled by plugin " & plg.name, text) Else _
                    Return New ErrorCause_Plugin(plg, "Error caused and handled by unkown plugin", text)

            Else
                Return New ErrorCause_Other("Unkown cause", text)
            End If
        End Function
    End Module


    Public Class ServerError
        Public type As MessageType
        Public text As String
        Public id As UInt64

        Public Sub New(id As UInt64, type As MessageType, text As String)
            Me.text = text
            Me.type = type
            Me.id = id
        End Sub
    End Class

    Public MustInherit Class ErrorCause
        Public Enum ErrorCauseType
            plugin
            setting
            other
        End Enum

        Public type As ErrorCauseType 'type of error
        Public text As String 'text that caused the error
        Public description As String
    End Class

    Public Class ErrorCause_Plugin
        Inherits ErrorCause
        Public plugin As plugindescriptor

        Public Sub New()
            Me.type = ErrorCauseType.plugin
        End Sub

        Public Sub New(plugin As plugindescriptor, description As String)
            Me.plugin = plugin
            Me.description = description
        End Sub

        Public Sub New(plugin As plugindescriptor, description As String, txt As String)
            Me.plugin = plugin
            Me.description = description
            Me.text = txt
        End Sub
    End Class

    Public Class ErrorCause_setting
        Inherits ErrorCause
        Public setting As String = ""
        Public Fixvalue As String = ""

        Public Sub New()
            Me.type = ErrorCauseType.setting
        End Sub

        Public Sub New(setting As String, newvalue As String)
            Me.type = ErrorCauseType.setting
            Me.setting = setting
            Me.Fixvalue = newvalue
            Me.description = "Incorrect setting: " & setting
        End Sub

        Public Sub New(setting As String, newvalue As String, txt As String)
            Me.type = ErrorCauseType.setting
            Me.setting = setting
            Me.Fixvalue = newvalue
            Me.description = "Incorrect setting: " & setting
            Me.text = txt
        End Sub
    End Class

    Public Class ErrorCause_Other
        Inherits ErrorCause

        Public Sub New(descr As String)
            Me.type = ErrorCauseType.other
            Me.description = descr
        End Sub

        Public Sub New(descr As String, txt As String)
            Me.type = ErrorCauseType.other
            Me.description = descr
            Me.text = txt
        End Sub
    End Class
End Namespace