Imports Net.Bertware.BukkitGUI.Core

Namespace Utilities
    Public Module javaAPI

        Const default_x64_jre6x32 As String = "C:/Program Files (x86)/Java/jre6/bin/java.exe"
        Const default_x64_jre6x64 As String = "C:/Program Files/Java/jre6/bin/java.exe"
        Const default_x64_jre7x32 As String = "C:/Program Files (x86)/Java/jre7/bin/java.exe"
        Const default_x64_jre7x64 As String = "C:/Program Files/Java/jre7/bin/java.exe"

        Const default_x32_jre6x32 As String = "C:/Program Files/Java/jre6/bin/java.exe"
        Const default_x32_jre7x32 As String = "C:/Program Files/Java/jre7/bin/java.exe"

        Enum javaVersion
            jre6x32 = 0
            jre6x64 = 1
            jre7x32 = 2
            jre7x64 = 3
        End Enum

        Public Property jre6x32 As String
            Get
                Dim path As String = config.read("jre6x32", "undefined", "java")

                If path = "undefined" Then
                    Select Case common.Is64BitOS
                        Case True
                            If FileIO.FileSystem.FileExists(default_x64_jre6x32) Then path = default_x64_jre6x32
                        Case False
                            If FileIO.FileSystem.FileExists(default_x32_jre6x32) Then path = default_x32_jre6x32
                    End Select
                    config.write("jre6x32", path, "java")
                End If

                Return path
            End Get
            Set(value As String)
                config.write("jre6x32", value, "java")
            End Set
        End Property

        Public Property jre6x64 As String
            Get
                Dim path As String = config.read("jre6x64", "undefined", "java")
                If path = "undefined" Then
                    If FileIO.FileSystem.FileExists(default_x64_jre6x64) Then path = default_x64_jre6x64
                    config.write("jre6x64", path, "java")
                End If

                Return path
            End Get
            Set(value As String)
                config.write("jre6x64", value, "java")
            End Set
        End Property

        Public Property jre7x32 As String
            Get
                Dim path As String = config.read("jre7x32", "undefined", "java")
                If path = "undefined" Then
                    Select Case common.Is64BitOS
                        Case True
                            If FileIO.FileSystem.FileExists(default_x64_jre7x32) Then path = default_x64_jre7x32
                        Case False
                            If FileIO.FileSystem.FileExists(default_x32_jre7x32) Then path = default_x32_jre7x32
                    End Select
                    config.write("jre7x32", path, "java")
                End If
                Return path
            End Get
            Set(value As String)
                config.write("jre7x32", value, "java")
            End Set
        End Property

        Public Property jre7x64 As String
            Get
                Dim path As String = config.read("jre7x64", "undefined", "java")
                If path = "undefined" Then
                    If FileIO.FileSystem.FileExists(default_x64_jre7x64) Then path = default_x64_jre7x64
                    config.write("jre7x64", path, "java")
                End If
                Return path
            End Get
            Set(value As String)
                config.write("jre7x64", value, "java")
            End Set
        End Property

        ''' <summary>
        ''' Get the path of java.exe for the given java version
        ''' </summary>
        ''' <param name="jre">The java version, as JavaAPI.Javaversion</param>
        ''' <returns>Returns the path as a string</returns>
        ''' <remarks></remarks>
        Public Function GetExec(jre As javaVersion) As String
            Select Case jre
                Case javaVersion.jre6x32
                    Return jre6x32
                Case javaVersion.jre6x64
                    Return jre6x64
                Case javaVersion.jre7x32
                    Return jre7x32
                Case javaVersion.jre7x64
                    Return jre7x64
                Case Else
                    Dim jp As String = config.read("alternative_java", "", "java")
                    If jp Is Nothing OrElse jp = "" OrElse IO.File.Exists(jp) = False Then
                        jp = SelectAlternativeJava()
                        If jp Is Nothing Then jp = ""
                    End If
                    Return jp
            End Select
        End Function

        Public Function SelectAlternativeJava() As String
            Dim ofd As New OpenFileDialog
            ofd.Filter = "*.exe|*.exe"
            ofd.Title = "Select Java exectutable"
            If ofd.ShowDialog() = DialogResult.OK Then config.write("alternative_java", ofd.FileName, "java") : Return ofd.FileName Else Return Nothing
        End Function

        ''' <summary>
        ''' Checks if the given java version is 32 bit
        ''' </summary>
        ''' <param name="jre">The java version to check, as JavaAPI.Javaversion (or integer)</param>
        ''' <returns>True if 32 bit</returns>
        ''' <remarks></remarks>
        Public Function Is32bit(jre As javaVersion) As Boolean
            If jre.ToString.Contains("x32") Then Return True Else Return False
        End Function

        ''' <summary>
        ''' Checks if the given java version is 64 bit
        ''' </summary>
        ''' <param name="jre">The java version to check, as JavaAPI.Javaversion (or integer)</param>
        ''' <returns>True if 64 bit</returns>
        ''' <remarks></remarks>
        Public Function Is64bit(jre As javaVersion) As Boolean
            If jre.ToString.Contains("x64") Then Return True Else Return False
        End Function

        ''' <summary>
        ''' Kills all running java processes
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub KillAll()
            Try
                livebug.write(loggingLevel.Fine, "JavaAPI", "Killing all java processes")
                For Each P As Process In Process.GetProcessesByName("java")
                    If P.HasExited = False Then P.Kill()
                Next
                livebug.write(loggingLevel.Fine, "JavaAPI", "Killed all java processes")
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "JavaAPI", "Error while killing all java processes", ex.Message)
            End Try
        End Sub
    End Module
End Namespace