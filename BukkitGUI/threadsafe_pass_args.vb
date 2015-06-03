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



'This file contains classes to pass items to a sub that executes thread safe
'As these routines can only accept one item, multiple parameters are bundled here
'
Imports Net.Bertware.BukkitGUI.MCInterop


Public Class thds_pass_servermessage
    Public shorttext As String
    Public fulltext As String
    Public color As Color

    Public Sub New()
        shorttext = ""
        fulltext = ""
        color = Color.Black
    End Sub

    Public Sub New(text As String)
        shorttext = text.Split(Environment.NewLine)(0)
        fulltext = text
        color = Color.Black
    End Sub

    Public Sub New(text As String, color As Color)
        shorttext = text.Split(Environment.NewLine)(0)
        fulltext = text
        Me.color = color
    End Sub
End Class

Public Class thds_pass_lookup
    Public text As String, type As serverOutputHandler.MessageType

    Public Sub New()
    End Sub

    Public Sub New(text As String, type As MessageType)
        Me.text = text
        Me.type = type
    End Sub
End Class