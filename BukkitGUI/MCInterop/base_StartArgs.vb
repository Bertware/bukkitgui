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
Imports Net.Bertware.BukkitGUI.Utilities

Namespace MCInterop
    Public Class execStartArgs
        Public executable As String
        Public arguments As String

        Public Sub New(executable As String, arguments As String)
            Me.executable = executable
            Me.arguments = arguments
        End Sub
    End Class

    Public Class javaStartArgs
        Public executable As String = ""
        Public jar As String = ""
        Public switches As String = ""
        Public args As String = ""
        Public custom_arg As String = ""
        Public custom_switch As String = ""

        Public Sub New()
            Me.executable = ""
            Me.args = ""
            Me.jar = ""
            Me.switches = ""
            Me.custom_arg = ""
            Me.custom_switch = ""
        End Sub

        Public Sub New(executable As String, flags As String, jar As String, switches As String)
            Me.executable = executable
            Me.args = flags
            Me.jar = jar
            Me.switches = switches
            Me.custom_arg = ""
            Me.custom_switch = ""
        End Sub

        
        ''' <summary>
        '''     Creates a new JavaStartArgs instance
        ''' </summary>
        ''' <param name="jre">the java version to use, as javaAPI.Javaversion</param>
        ''' <param name="minram">the minimum amount of RAM for the VM (-xms)</param>
        ''' <param name="maxram">the maximum amount of RAM for the VM (-xmx)</param>
        ''' <param name="jar">the location of the jar file to be ran</param>
        ''' <param name="CustomArgs">Custom arguments that should be added (optional)</param>
        ''' <param name="server">The server type. Baded upon this type required switches will be added. (Optional)</param>
        ''' <remarks></remarks>
        Public Sub New(jre As javaAPI.javaVersion, minram As UInt32, maxram As UInt32, jar As String,
                       Optional ByVal CustomArgs As String = "", Optional ByVal CustomSwitches As String = "",
                       Optional ByVal server As McInteropType = McInteropType.bukkit)
            If minram < 16 Then minram = 16
            If maxram < 16 Then maxram = 16

            Me.executable = GetExec(jre)
            Me.args = "-Xms" & minram & "M -Xmx" & maxram & "M "
            Me.jar = jar
            Me.custom_arg = CustomArgs
            Me.custom_switch = CustomSwitches

            Select Case server
                Case McInteropType.bukkit
                    Me.switches = "-nojline"
                    Me.args += " -Duser.language=en "
                    If readAsBool("utf_8_compatibility", False, "output") Then _
                        Me.args += " -Dfile.encoding=utf-8 "

                Case McInteropType.vanilla
                    Me.switches = "nogui"
                Case McInteropType.spigot
                    Me.switches = "-nojline"
                    Me.args += " -Duser.language=en -XX:MaxPermSize=128M"
                    If readAsBool("utf_8_compatibility", False, "output") Then _
                        Me.args += " -Dfile.encoding=utf-8 "


            End Select
        End Sub

        
        ''' <summary>
        '''     Compile all the variables into one argument, needed to start the process
        ''' </summary>
        ''' <value>The argument needed to start the process, as string</value>
        ''' <returns>The argument needed to start the process, as string</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property buildArgs() As String
            Get
                Return _
                    (args & " " & custom_arg & " " & "-jar """ & jar & """ " & Me.switches & " " & custom_switch).
                        Replace("   ", " ").Replace("  ", " ").Trim
            End Get
        End Property
    End Class
End Namespace