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
Imports System.Xml
Imports System.IO

Public Class BuildTranslatorMainForm

    Public FileList As List(Of String)

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If My.Application.CommandLineArgs IsNot Nothing AndAlso My.Application.CommandLineArgs.Count > 0 Then
            Dim tra() As Char = {"""", "-", "/"}
            BuildFileList(My.Application.CommandLineArgs(0).Trim(tra))
            Dim t As New Thread(AddressOf Run)
            t.IsBackground = True
            t.Start()
        Else
            WriteLogText("Invalid argument!")
        End If
    End Sub

    Public Function BuildFileList(path As String) As List(Of String)
        WriteLogText("Building file list...")
        path = path.Trim()
        Dim project As fxml = New fxml(path)
        Dim base As String = New FileInfo(project.path).Directory.FullName
        FileList = New List(Of String)
        Try
            For Each item As XmlElement In project.GetElementsByName("Compile")
                If (item.GetAttribute("Include").EndsWith(".vb") Or item.GetAttribute("Include").EndsWith(".cs")) Then FileList.Add(base & "/" & item.GetAttribute("Include"))
            Next
        Catch ex As Exception
            Debug.WriteLine("File list building failed! " & ex.Message)
        End Try
        WriteLogText(filelist.Count & " files found to parse.")
        Me.FileList = filelist
        Return filelist
    End Function

    Public Sub Run()
        For fileNumber As UInt16 = 0 To FileList.Count - 1
            Dim File As String = FileList(fileNumber)
            Dim t As New Thread(AddressOf RunOnFile)
            t.IsBackground = True
            t.Start(New FileDetails(File, fileNumber, FileList.Count))
        Next
    End Sub

    Public Sub WriteLogText(text As String)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Me.WriteLogText)
            Me.Invoke(d, New Object() {text})
        Else
            Me.TxtLog.Text += text & vbCrLf
            With (TxtLog) 'auto scroll down
                .Select(TxtLog.TextLength, 0)
                .ScrollToCaret()
            End With
        End If
    End Sub

    Dim completed As UInt16 = 0
    Public Sub NotifyFileCompleted(file As String)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Me.NotifyFileCompleted)
            Me.Invoke(d, New Object() {file})
        Else
            completed += 1
            Setprogress(Math.Round(100 * (completed / (FileList.Count))))
            WriteLogText(("Finished (" & completed.ToString & "/" & (FileList.Count).ToString.ToString + ")" & file).ToString)
        End If
        If completed = FileList.Count Then CloseForm()
    End Sub

    Public Sub Addprogress(p As Decimal)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Addprogress)
            Me.Invoke(d, New Object() {p})
        Else
            If Math.Round(p + PBarProgress.Value) <= PBarProgress.Maximum Then PBarProgress.Value = Math.Round(PBarProgress.Value + p)
        End If
    End Sub


    Public Sub Setprogress(p As Decimal)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Setprogress)
            Me.Invoke(d, New Object() {p})
        Else
            If p <= PBarProgress.Value Then PBarProgress.Value = p
        End If
    End Sub

    Public Sub RunOnFile(fi As FileDetails)
        Dim file As String = fi.path
        Dim total = fi.FileCount
        Dim id = fi.FileID
        Try
            Dim isDesigner As Boolean = file.ToLower.Contains("designer")

            If isDesigner And (file.ToLower.Contains("splashscreen") = False) And (file.ToLower.Contains("application.designer") = False And file.ToLower.Contains("resources.") = False And file.ToLower.Contains("settings.") = False) Then
                WriteLogText("Reading (" & id + 1 & "/" & total & ") :" & file)
                Dim text As String = IO.File.ReadAllText(file)
                WriteLogText("Parsing (" & id + 1 & "/" & total & ") :" & file)
                Dim justSkipped As Boolean = False

                Const P1 As String = "\.(Text|Tooltip)\s\=\s\""(.*)\""(\s|\n|\r|;)"
                Const P2 As String = "\""(.*)"
                Dim trimm() As Char = {vbCrLf, vbCr, vbLf, vbNewLine}

                While System.Text.RegularExpressions.Regex.Match(text, P1).Success = True
                    Dim m = System.Text.RegularExpressions.Regex.Match(text, P1)
                    Dim oldstr = System.Text.RegularExpressions.Regex.Match(text, P1).Value
                    Dim newstr = System.Text.RegularExpressions.Regex.Match(m.Value.Trim(trimm), "(.*) = ").Value & "lr(" & System.Text.RegularExpressions.Regex.Match(m.Value.Trim(trimm), P2).Value.Trim(trimm) & ")"
                    Debug.WriteLine("placing new translation: " & newstr)
                    text = text.Replace(oldstr, newstr).TrimEnd(trimm) & vbCrLf
                End While
                Debug.WriteLine("File Done")

                WriteLogText("Writing (" & id + 1 & "/" & total & ") :" & file)
                IO.File.WriteAllText(file, text)
                WriteLogText("Finished (" & id + 1 & "/" & total & ") :" & file)
            Else
                WriteLogText("Skipping (" & id + 1 & "/" & total & ") :" & file)
            End If
            NotifyFileCompleted(fi.path)
        Catch ex As Exception
            WriteLogText("!FAILED! (" & id + 1 & "/" & total & ") :" & file & " :: " & ex.Message)
        End Try
    End Sub


    Public Sub RunOnFileDeprecated(fi As FileDetails)
        Dim file As String = fi.path
        Dim total = fi.FileCount
        Dim id = fi.FileID
        Try
            Dim isDesigner As Boolean = file.ToLower.Contains("designer")

            If isDesigner And (file.ToLower.Contains("splashscreen") = False) And (file.ToLower.Contains("application.designer") = False And file.ToLower.Contains("resources.") = False And file.ToLower.Contains("settings.") = False) Then
                WriteLogText("Reading (" & id + 1 & "/" & total & ") :" & file)
                Dim text As String = IO.File.ReadAllText(file)
                WriteLogText("Parsing (" & id + 1 & "/" & total & ") :" & file)
                Dim JustSkipped As Boolean = False

                Dim i As UInt64 = 0
                While i < text.ToCharArray.Length - 1

                    Dim character As Char = text.ToCharArray()(i)

                    If character = Char.Parse("""") Then 'if character = "
                        Dim arr() As Char = text.ToCharArray 'get character array
                        Dim already_done As Boolean = (arr(i - 1) = Char.Parse("(") And arr(i - 2) = Char.Parse("r") And arr(i - 3) = Char.Parse("l")) Or arr(i + 1) = Char.Parse(")") 'if already translated
                        Dim last_part As Boolean = arr(i + 1) = Char.Parse(vbCr) Or arr(i + 1) = Char.Parse(" ")
                        Dim designer_skip As Boolean = isDesigner And (arr(i - 4) = Char.Parse("e") Or arr(i - 5) = Char.Parse("m") Or arr(i - 6) = Char.Parse("a") Or arr(i - 1) = Char.Parse("(") Or arr(i - 4) = Char.Parse("y")) 'if is designer, and this is a name


                        If Not designer_skip And Not already_done And Not last_part Then
                            Dim e As UInt64 = text.IndexOf(Char.Parse(""""), CInt(i + 1))
                            If text(e + 1) = Char.Parse("""") Then e = text.IndexOf(Char.Parse(""""), CInt(e + 1))
                            If text(e + 1) = Char.Parse("""") Then e = text.IndexOf(Char.Parse(""""), CInt(e + 1))

                            Dim p1 As String = text.Substring(0, i)
                            Dim p2 As String = text.Substring(i, e - i + 1)
                            Dim p3 As String = text.Substring(e + 1)
                            text = p1 & "lr(" & p2 & ")" & p3
                            i = e
                        End If

                    End If
                    i = i + 1
                End While
                WriteLogText("Writing (" & id + 1 & "/" & total & ") :" & file)
                IO.File.WriteAllText(file, text)
                WriteLogText("Finished (" & id + 1 & "/" & total & ") :" & file)
            Else
                WriteLogText("Skipping (" & id + 1 & "/" & total & ") :" & file)
            End If
            NotifyFileCompleted(fi.path)
        Catch ex As Exception
            WriteLogText("!FAILED! (" & id + 1 & "/" & total & ") :" & file & " :: " & ex.Message)
        End Try
    End Sub

    Private Sub CloseForm() 'Close the form in a thread safe way
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf CloseForm)
            Me.Invoke(d, New Object())
        Else
            Me.Close()
        End If
    End Sub
End Class

Public Class FileDetails
    Public path As String, FileID As UInt16, FileCount As UInt16

    Public Sub New(path As String, FileID As UInt16, FileCount As UInt16)
        Me.path = path
        Me.FileCount = FileCount
        Me.FileID = FileID
    End Sub
End Class