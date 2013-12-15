Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.MCInterop

'This file contains classes to pass items to a sub that executes thread safe
'As these routines can only accept one item, multiple parameters are bundled here
'


Public Class thds_pass_servermessage
    Public shorttext As String
    Public fulltext As String
    Public color As Color

    Public Sub New()
        shorttext = ""
        fulltext = ""
        color = Drawing.Color.Black
    End Sub

    Public Sub New(text As String)
        shorttext = text.Split(Environment.NewLine)(0)
        fulltext = text
        color = Drawing.Color.Black
    End Sub

    Public Sub New(text As String, color As Color)
        shorttext = text.Split(Environment.NewLine)(0)
        fulltext = text
        Me.color = color
    End Sub
End Class

Public Class thds_pass_lookup
    Public text As String, type As MessageType

    Public Sub New()

    End Sub

    Public Sub New(text As String, type As MessageType)
        Me.text = text
        Me.type = type
    End Sub
End Class