﻿Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
Imports System.Xml
Imports System.Reflection

Public Class LangGen
    Dim proj As New fxml()
    Dim lang As New fxml

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles BtnGo.Click
        TxtLog.AutoScrollDown = True

        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = "D:\Users\Bert\Documents\Visual Studio 2010\Projects\BukkitGUI\BukkitGUI\BukkitGUI"
        ofd.Filter = "Project file (*.vbproj)|*.vbproj"
        ofd.Multiselect = False
        ofd.Title = "Select solution..."
        If ofd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        Dim source As String = ofd.FileName

        Dim sfd As New SaveFileDialog
        sfd.Title = "Create translation file"
        sfd.Filter = "Translation file (*.xml)|*.xml"
        sfd.OverwritePrompt = True
        sfd.DefaultExt = ".xml"
        sfd.AddExtension = True
        sfd.SupportMultiDottedExtensions = True
        If sfd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        Dim destination As String = sfd.FileName

        If Not FileIO.FileSystem.FileExists(destination) Then
            Dim fs = File.Create(destination)
            Dim sw As New StreamWriter(fs)
            sw.Write("<language version=""1.0""></language>")
            sw.Close()
            fs.Close()
        End If

        proj = New fxml(source)
        lang = New fxml(destination)

        Dim t As New Thread(AddressOf run)
        t.IsBackground = True
        t.Start()
    End Sub

    Private Sub run()
        Dim filelist As New List(Of String)
        Dim base As String = New FileInfo(proj.path).Directory.FullName
        For Each item As XmlElement In proj.GetElementsByName("Compile")
            If item.GetAttribute("Include").EndsWith(".vb") Then filelist.Add(base & "/" & item.GetAttribute("Include"))
        Next
        writelog(filelist.Count & " files found to parse. Starting...")

        For f = 0 To filelist.Count - 1
            Dim file As String = filelist(f)
            writelog("Parsing " & f + 1 & "/" & filelist.Count & ":" & file)
            Dim content As String = New StreamReader(New FileStream(File, FileMode.Open)).ReadToEnd
            Dim i As UInt64 = 0
            Dim l As UInt64 = content.ToCharArray.Length
            While i < l - 1
                If i > 3 Then
                    If content(i) = Char.Parse("""") And content(i - 1) = Char.Parse("(") And content(i - 2) = Char.Parse("r") And content(i - 3) = Char.Parse("l") Then
                        'Start detected
                        Dim e As String = content.IndexOf("""", CInt(i + 1))
                        Dim text As String = content.Substring(i + 1, e - i - 1)
                        i = e + 1
                        Dim res As Xml.XmlElement = lang.getElementByAttribute("text", "string", text)
                        If res Is Nothing Then
                            Dim cl As New List(Of CXMLAttribute)
                            cl.Add(New CXMLAttribute("string", text))
                            lang.write("text", text, "", cl, True)
                        End If
                    End If
                End If
                i = i + 1
            End While
            Addprogress(100 / filelist.Count)
        Next
    End Sub

    Private Sub Addprogress(p As Byte)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf Addprogress)
            Me.Invoke(d, New Object() {p})
        Else
            If VPBar.Value + p <= 100 Then VPBar.Value += p
        End If
    End Sub

    Private Sub writelog(text As String)
        If Me.InvokeRequired Then
            Dim d As New ContextCallback(AddressOf writelog)
            Me.Invoke(d, New Object() {text})
        Else
            TxtLog.Text += text.Trim(vbCrLf) & vbCrLf
        End If
    End Sub
End Class