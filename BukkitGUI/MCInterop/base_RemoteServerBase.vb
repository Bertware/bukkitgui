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



Namespace MCInterop
    ' <summary>
    '     Base class for built-in remote server support. Each remote server class should inherit this, and use these
    '     properties to allow integration with server.vb
    ' </summary>
    ' <remarks></remarks>
    Public MustInherit Class RemoteServerBase
        Private _instream As RemoteServerCache 'input stream for the server
        Private _outstream As RemoteServerCache 'output stream for the server
        Private _cred As RemoteCredentials


        ''' <summary>
        '''     Input stream, commands etc. should be written to this stream
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property StandardIn As RemoteServerCache
            Get
                Return _instream
            End Get
        End Property


        ''' <summary>
        '''     Output stream, console output should be read from this stream
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property StandardOut As RemoteServerCache
            Get
                Return _outstream
            End Get
        End Property

        Public Property Credentials As RemoteCredentials
            Get
                Return _cred
            End Get
            Set(value As RemoteCredentials)
                _cred = value
            End Set
        End Property

        Public Sub New()
            _instream = New RemoteServerCache
            _outstream = New RemoteServerCache
        End Sub


        ''' <summary>
        '''     This routine must be overridden with the start routine of the server, and will be called in the server module in
        '''     order to start the server.
        ''' </summary>
        ''' <remarks>No variables are passed, everything must be set before launch of this routine</remarks>
        Public Overridable Sub Run()
            _instream = New RemoteServerCache
            _outstream = New RemoteServerCache
        End Sub

        Public Overridable Sub Close()
            _instream = Nothing
            _outstream = Nothing
            StopServer()
        End Sub
    End Class

    Public Class RemoteCredentials
        Public Host As String, port As UInt16, login As String, password As String, salt As String
    End Class

    Public Class RemoteServerCache
        Public received As New List(Of String)

        Public Sub write(text As String)
            received.Add(text)
        End Sub

        Public Function read() As String
            If received Is Nothing OrElse received.Count = 0 Then Return Nothing : Exit Function
            Dim res As String = received(0)
            received.RemoveAt(0)
            Return res
        End Function

        Public Sub discard()
            received.Clear()
        End Sub

        Public ReadOnly Property EOS As Boolean
            Get
                Return received.Count = 0
            End Get
        End Property
    End Class
End Namespace