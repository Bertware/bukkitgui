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

Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports Net.Bertware.BukkitGUI.Core

Namespace forms


    Public Class LanguageInstaller
        Private ReadOnly _languages() As String =
                             {"bulgarian", "danish", "dutch", "french", "german", "italian", "japanese", "korean", "polish", "romanian", "russian",
                              "simplifiedChinese", "spanish", "traditionalChinese", "turkish"}

        Private Event LanguagesLoaded()

        Private Sub LanguageInstaller_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Dim t As New Thread(AddressOf GetLanguages)
            t.IsBackground = True
            t.Name = "LanguageInstaller_getlanguages"
            t.Start()
        End Sub
    
        Private Sub update_ui() Handles Me.LanguagesLoaded
            If Me.InvokeRequired Then
                Dim d As New ContextCallback(AddressOf update_ui)
                Me.Invoke(d, New Object())
            Else
                ALVLanguages.Items.Clear()
                If _languages Is Nothing Then Exit Sub
                For Each t As String In _languages
                    Dim lvi As New ListViewItem({t})
                    ALVLanguages.Items.Add(lvi)
                Next
                'For Each t As Translation In _languages
                '    Dim lvi As New ListViewItem({t.language, t.version, t.translator, t.comment})
                '    ALVLanguages.Items.Add(lvi)
                'Next
            End If
        End Sub

        Private Sub GetLanguages()
            RaiseEvent LanguagesLoaded()
            Exit Sub

            'Dim result As String = downloadstring(translations_list)
            'If result Is Nothing OrElse result = "" Then
            '    result = downloadstring(translations_list)
            '    If result Is Nothing OrElse result = "" Then Exit Sub
            'End If

            'Dim langxml As New fxml
            'langxml.Owner = "LanguageInstaller"
            'langxml.LoadXML(result)
            'For Each xmle As XmlElement In langxml.GetElementsByName("language")
            '    Try
            '        Dim t As New Translation
            '        t.language = xmle.GetAttribute("name")
            '        t.translator = xmle.GetElementsByTagName("translator")(0).InnerText
            '        t.comment = xmle.GetElementsByTagName("comment")(0).InnerText
            '        t.version = xmle.GetElementsByTagName("version")(0).InnerText
            '        t.url = xmle.GetElementsByTagName("url")(0).InnerText
            '        languages.Add(t)
            '    Catch ex As Exception
            '        Log(loggingLevel.Severe, "LanguageInstaller", "Could not parse translation info", ex.Message)
            '    End Try
            'Next

            'RaiseEvent LanguagesLoaded()
        End Sub

        Private Sub BtnInstall_Click(sender As Object, e As EventArgs) Handles BtnInstall.Click
            If ALVLanguages.SelectedItems.Count < 1 Then Exit Sub

            Dim language As String = ALVLanguages.SelectedItems(0).SubItems(0).Text
            Log(livebug.loggingLevel.Fine, "LanguageInstaller", "Installing language: " & language)


            ' livebug.write(loggingLevel.Fine, "Common", "DLL requested:" & args.Name)
            Dim resourceName As [String] = "Net.Bertware.BukkitGUI." & New AssemblyName("lang_" & language).Name & ".xml"
            Using reader = New StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                File.WriteAllText(GetLanguageFilePath(language), reader.ReadToEnd())
            End Using


            'Dim t As Translation = Nothing
            'For Each lang As Translation In languages
            '    If lang.language = language Then t = lang
            'Next

            'If t Is Nothing Then Exit Sub

            'Log(loggingLevel.Fine, "LanguageInstaller", "Downloading language: " & t.url)

            'Dim _
            '    fd As _
            '        New FileDownloader(t.url, LocalizationPath & "/" & t.language & ".xml",
            '                           Lr("Installing language:") & " " & t.language)
            'fd.ShowDialog()

            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub
    End Class

    Class Translation
        Public Language As String
        Public Translator As String
        Public Version As String
        Public Url As String
        Public Comment As String
    End Class
End Namespace
