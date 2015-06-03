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
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports NATUPNPLib
Imports Net.Bertware.BukkitGUI.Core

Namespace Utilities
    Public Class UPnP
        Implements IDisposable

        Private upnpnat As UPnPNAT
        Private staticMapping As IStaticPortMappingCollection
        Private dynamicMapping As IDynamicPortMappingCollection

        Private staticEnabled As Boolean = True
        Private dynamicEnabled As Boolean = True

        
        ''' <summary>
        '''     The different supported protocols
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum Protocol

            
            ''' <summary>
            '''     Transmission Control Protocol
            ''' </summary>
            ''' <remarks></remarks>
            TCP

            
            ''' <summary>
            '''     User Datagram Protocol
            ''' </summary>
            ''' <remarks></remarks>
            UDP
        End Enum

        
        ''' <summary>
        '''     Returns if UPnP is enabled.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property UPnPEnabled As Boolean
            Get
                Return staticEnabled = True OrElse dynamicEnabled = True
            End Get
        End Property

        
        ''' <summary>
        '''     The UPnP Managed Class
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

            'Create the new NAT Class
            upnpnat = New UPnPNAT

            'generate the static mappings
            Me.GetStaticMappings()
            Me.GetDynamicMappings()
        End Sub

        
        ''' <summary>
        '''     Returns all static port mappings
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetStaticMappings()
            Try
                staticMapping = upnpnat.StaticPortMappingCollection()
            Catch ex As NotImplementedException
                staticEnabled = False
            End Try
        End Sub

        
        ''' <summary>
        '''     Returns all dynamic port mappings
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GetDynamicMappings()
            Try
                dynamicMapping = upnpnat.DynamicPortMappingCollection()
            Catch ex As NotImplementedException
                dynamicEnabled = False
            End Try
        End Sub

        
        ''' <summary>
        '''     Adds a port mapping to the UPnP enabled device.
        ''' </summary>
        ''' <param name="localIP">The local IP address to map to.</param>
        ''' <param name="Port">The port to forward.</param>
        ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
        ''' <param name="desc">A small description of the port.</param>
        ''' <exception cref="ApplicationException">This exception is thrown when UPnP is disabled.</exception>
        ''' <exception cref="ObjectDisposedException">This exception is thrown when this class has been disposed.</exception>
        ''' <exception cref="ArgumentException">This exception is thrown when any of the supplied arguments are invalid.</exception>
        ''' <remarks></remarks>
        Public Sub Add(ByVal localIP As String, ByVal Port As Integer, ByVal prot As Protocol, ByVal desc As String)

            ' Begin utilizing
            If Exists(Port, prot) Then Throw New ArgumentException("This mapping already exists!", "Port;Protocol")

            ' Check
            If Not IsPrivateIP(localIP) Then Throw New ArgumentException("This is not a local IP address!", "localIP")

            ' Final check!
            If Not staticEnabled Then _
                Throw New ApplicationException("UPnP is not enabled, or there was an error with UPnP Initialization.")

            ' Okay, continue on
            staticMapping.Add(Port, prot.ToString(), Port, localIP, True, desc)
        End Sub

        
        ''' <summary>
        '''     Removes a port mapping from the UPnP enabled device.
        ''' </summary>
        ''' <param name="Port">The port to remove.</param>
        ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
        ''' <exception cref="ApplicationException">This exception is thrown when UPnP is disabled.</exception>
        ''' <exception cref="ObjectDisposedException">This exception is thrown when this class has been disposed.</exception>
        ''' <exception cref="ArgumentException">This exception is thrown when the port [or protocol] is invalid.</exception>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal Port As Integer, ByVal Prot As Protocol)

            ' Begin utilizing
            If Not Exists(Port, Prot) Then Throw New ArgumentException("This mapping doesn't exist!", "Port;prot")

            ' Final check!
            If Not staticEnabled Then _
                Throw New ApplicationException("UPnP is not enabled, or there was an error with UPnP Initialization.")

            ' Okay, continue on
            staticMapping.Remove(Port, Prot.ToString)
        End Sub

        
        ''' <summary>
        '''     Checks to see if a port exists in the mapping.
        ''' </summary>
        ''' <param name="Port">The port to check.</param>
        ''' <param name="prot">The protocol of the port [TCP/UDP]</param>
        ''' <exception cref="ApplicationException">This exception is thrown when UPnP is disabled.</exception>
        ''' <exception cref="ObjectDisposedException">This exception is thrown when this class has been disposed.</exception>
        ''' <exception cref="ArgumentException">This exception is thrown when the port [or protocol] is invalid.</exception>
        ''' <remarks></remarks>
        Public Function Exists(ByVal Port As Long, ByVal Prot As Protocol) As Boolean
            Try
                ' Final check!
                If Not staticEnabled Then _
                    Throw _
                        New ApplicationException("UPnP is not enabled, or there was an error with UPnP Initialization.")

                Log(livebug.loggingLevel.Info, "uPnP", "Checking if port is in use...")
                ' Begin checking
                For Each mapping As IStaticPortMapping In staticMapping

                    ' Compare
                    If mapping.ExternalPort = Port AndAlso mapping.Protocol.ToLower.Equals(Prot.ToString.ToLower) Then _
                        Return True

                Next
                Log(loggingLevel.Info, "uPnP", "ok: Port is not in use", Port & ":" & Prot.ToString)
                'Nothing!
                Return False
            Catch ex As Exception
                Log(loggingLevel.Warning, "uPnP", "Couldn't check if port mapping exists", ex.Message)
                Throw New Exception("Couldn't check if mapping exists!", ex)
                Return False
            End Try
        End Function

        
        ''' <summary>
        '''     Attempts to locate the local IP address of this computer.
        ''' </summary>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Shared Function LocalIP() As String
            Dim IPList As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName)
            For Each IPaddress In IPList.AddressList
                If _
                    (IPaddress.AddressFamily = AddressFamily.InterNetwork) AndAlso
                    IsPrivateIP(IPaddress.ToString()) Then
                    Return IPaddress.ToString
                End If
            Next
            Return String.Empty
        End Function

        
        ''' <summary>
        '''     Checks to see if an IP address is a local IP address.
        ''' </summary>
        ''' <param name="CheckIP">The IP address to check.</param>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        Private Shared Function IsPrivateIP(ByVal CheckIP As String) As Boolean
            Dim Quad1, Quad2 As Integer

            Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
            Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
            Select Case Quad1
                Case 10
                    Return True
                Case 172
                    If Quad2 >= 16 And Quad2 <= 31 Then Return True
                Case 192
                    If Quad2 = 168 Then Return True
            End Select
            Return False
        End Function

        
        ''' <summary>
        '''     Disposes of the UPnP class
        ''' </summary>
        ''' <param name="disposing">True or False makes no difference.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub Dispose(disposing As Boolean)
            Marshal.ReleaseComObject(staticMapping)
            Marshal.ReleaseComObject(dynamicMapping)
            Marshal.ReleaseComObject(upnpnat)
        End Sub

        
        ''' <summary>
        '''     Dispose!
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        
        ''' <summary>
        '''     Prints out some debugging information to use.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetMapping() As List(Of PortMappingEntry)

            ' Return list
            Dim L As New List(Of PortMappingEntry)

            ' Loop through all the data after a check
            If staticEnabled Then
                For Each mapping As IStaticPortMapping In staticMapping
                    Try
                        L.Add(New PortMappingEntry(CUInt(mapping.InternalPort), mapping.InternalClient,
                                                   mapping.Description, mapping.Protocol))
                    Catch ex As Exception
                        Log(loggingLevel.Warning, "uPnP", "Couldn't load mapping: " & ex.Message)
                    End Try

                Next
            End If

            ' Give it back
            Return L
        End Function
    End Class

    Public Class PortMappingEntry
        Public Port As UInteger, Ip As String, Name As String, Protocol As UPnP.Protocol

        Public Sub New()
        End Sub

        Public Sub New(port As UInteger, ip As String, name As String, protocol As UPnP.Protocol)
            Try
                Me.Port = port
                Me.Ip = ip
                Me.Name = name
                Me.Protocol = protocol
            Catch ex As Exception
                Throw New InvalidCastException("Couldn't create portmapping entry")
            End Try
        End Sub

        Public Sub New(port As UInteger, ip As String, name As String, protocol As String)
            Try
                Me.Port = port
                Me.Ip = ip
                Me.Name = name
                If protocol.ToLower.Contains("udp") Then Me.Protocol = UPnP.Protocol.UDP Else _
                    Me.Protocol = UPnP.Protocol.TCP
            Catch ex As Exception
                Throw New InvalidCastException("Couldn't create portmapping entry")
            End Try
        End Sub
    End Class
End Namespace