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

Imports Net.Bertware.BukkitGUI.MCInterop

Namespace Utilities
    Public Class PlayerJoinEventArgs
        Public Enum playerjoinreason
            listupdate
            join
        End Enum

        Public Sub New(reason As playerjoinreason, message As String, Optional ByVal PlayerJoin As PlayerJoin = Nothing)
            Me.reason = reason
            Me.message = message
            Me.PlayerJoin = PlayerJoin
        End Sub

        
        ''' <summary>
        '''     The reason for this join event
        ''' </summary>
        ''' <remarks></remarks>
        Public reason As playerjoinreason

        
        ''' <summary>
        '''     The message that triggered this event
        ''' </summary>
        ''' <remarks></remarks>
        Public message As String

        
        ''' <summary>
        '''     The PlayerJoin object about this join
        ''' </summary>
        ''' <remarks></remarks>
        Public PlayerJoin As PlayerJoin
    End Class

    Public Class PlayerDisconnectEventArgs
        Public Enum playerleavereason
            listupdate
            leave
            kick
            ban
        End Enum

        Public Sub New(player As SimplePlayer, reason As playerleavereason, message As String,
                       Optional ByVal details As Object = Nothing)
            Me.player = player
            Me.reason = reason
            Me.message = message
            Me.details = details
        End Sub

        
        ''' <summary>
        '''     The simpleplayer object about the disconnected player
        ''' </summary>
        ''' <remarks></remarks>
        Public player As SimplePlayer

        
        ''' <summary>
        '''     The disconnect reason, leave, kick, ban, or list update
        ''' </summary>
        ''' <remarks></remarks>
        Public reason As playerleavereason

        
        ''' <summary>
        '''     The message that raised this event
        ''' </summary>
        ''' <remarks></remarks>
        Public message As String

        
        ''' <summary>
        '''     More details, can be playerleave, playerban, playerkick
        ''' </summary>
        ''' <remarks></remarks>
        Public details As Object
    End Class

    Public Class ListUpdateEventArgs
    ''' <summary>
    '''     List with added players
    ''' </summary>
    ''' <remarks></remarks>
                                    Public Added_players As List(Of String)

                                    
                                    ''' <summary>
                                    '''     List with removed players
                                    ''' </summary>
                                    ''' <remarks></remarks>
                                    Public Removed_players As List(Of String)

        Public Sub New()
            Added_players = New List(Of String)
            Removed_players = New List(Of String)
        End Sub

        Public Sub New(added_players As List(Of String), removed_players As List(Of String))
            Me.Added_players = added_players
            Me.Removed_players = removed_players
        End Sub
    End Class

    Public Class ErrorReceivedEventArgs 'for both severe and warning
        
        ''' <summary>
        '''     The message that raised this event
        ''' </summary>
        ''' <remarks></remarks>
        Public message As String

        
        ''' <summary>
        '''     the output type, warning or severe
        ''' </summary>
        ''' <remarks></remarks>
        Public type As MessageType

        Public Sub New(message As String, type As MessageType)
            Me.message = message
            Me.type = type
        End Sub
    End Class

    Public Class StackTraceReceivedEventArgs
    ''' <summary>
    '''     The message that raised this event
    ''' </summary>
    ''' <remarks></remarks>
                                            Public message As String

        Public Sub New(message As String)
            Me.message = message
        End Sub
    End Class
End Namespace