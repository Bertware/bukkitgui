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
Imports System.Security
Imports System.Web
Imports System.Xml
Imports Microsoft.VisualBasic.FileIO

Namespace Core
    Module language
        'Language module
        'This module provides functionallity to translate the program.
        'Language is provided in XML format 
        '<text string="english text">translated text</text>
        '
        'To allow translation of a certain text, enter lr(text).
        'The translation will be returned.
        '
        '<text string="original">translation</text>
        '
        Private ReadOnly DefaultEnglish As String = LocalizationPath & "/" & "default.xml"
        Private Const CurrentFileVersion As String = "1.0"

        Dim _langXml As fxml

        Dim _fails As Byte = 0
        Dim _failed As Boolean = False
        Dim _enabled As Boolean = False

        Public ReadOnly Property Languages As List(Of String)
            Get
                Return (From file In FileSystem.GetFiles(LocalizationPath) Select GetLanguageName(file)).ToList()
            End Get
        End Property

        Public Property LanguageFile As String
            Get
                Return read("language", DefaultEnglish)
            End Get
            Set(value As String)
                write("language", value)
                Init()
            End Set
        End Property

        Public ReadOnly Property CurrentLanguage As String
            Get
                Return GetLanguageName(read("language", DefaultEnglish))
            End Get
        End Property


        Public Sub Init() 'Open XML file, MUST BE DONE BEFORE USEAGE!
            Try
                Dim localization_file As String = read("language", DefaultEnglish)

                If Not FileSystem.FileExists(localization_file) Then _
                    Create_file(localization_file, "<language version=""" & CurrentFileVersion & """></language>")
                If File_Empty(localization_file) Then _
                    Create_file(localization_file, "<language version=""" & CurrentFileVersion & """></language>")

                _langXml = New fxml(localization_file, "language") _
                'initialize fxml, path is given so file is available instantly
                _enabled = True
            Catch memex As OutOfMemoryException
                Log(loggingLevel.Warning, "language",
                    "Language initialization failed (OutOfMemory) - " & memex.Message)
            Catch pex As SecurityException
                Log(loggingLevel.Warning, "language",
                    "Language initialization failed (SecurityException) - " & pex.Message)
            Catch ioex As IOException
                Log(loggingLevel.Warning, "language",
                    "Language initialization failed (IOException) - " & ioex.Message)
            Catch ex As Exception
                Log(loggingLevel.Severe, "language", "Language initialization failed", ex.Message)
            End Try
        End Sub

        
        ''' <summary>
        '''     Replace a text with it's translation. If a translation isn't available, the original value will be returned
        ''' </summary>
        ''' <param name="original">the original (english) text</param>
        ''' <param name="p1">value to replace %1</param>
        ''' <param name="p2">value to replace %2</param>
        ''' <param name="p3">value to replace %3</param>
        ''' <param name="p4">value to replace %4</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Lr(original As String, Optional p1 As String = "", Optional p2 As String = "",
                           Optional p3 As String = "", Optional p4 As String = "") As String _
'Language Replace - replace an english string with the translation
            Try
                If _enabled = False Then Return original : Exit Function
                If _failed Then Return original : Exit Function
                If original Is Nothing Then Return Nothing : Exit Function

                'if item doesn't exist, add to the XML file and return translation.
                'if item exists, return translation

                If _langXml Is Nothing OrElse _langXml.Document Is Nothing Then Return original : Exit Function

                Dim res As XmlElement = _langXml.getElementByAttribute("text", "string", original)

                If res IsNot Nothing Then
                    ' Debug.WriteLine("Translated """ + original + """ into """ + System.Web.HttpUtility.UrlDecode(res.InnerText) + """", "[language]")
                    Return _
                        HttpUtility.UrlDecode(res.InnerText).Replace("%1", p1).Replace("%2", p2).Replace("%3", p3).
                            Replace("%4", p4)

                Else 'Item doesn't exist. Create it, so it can be translated
                    Log(loggingLevel.Fine, "language", "Translation not found, adding. Original:" & original)

                    Dim l As New List(Of CXMLAttribute)
                    l.Add(New CXMLAttribute("string", original))
                    _langXml.write("text", HttpUtility.UrlEncodeUnicode(original), "", l, True)

                    Log(loggingLevel.Fine, "language", "Added to language file. Returning original value")
                    ' Debug.WriteLine("""" + original + """ was not translated (not available)", "[language]")
                    Return original 'no translation available, so keep the original language
                End If

            Catch ex As Exception
                Log(loggingLevel.Warning, "language",
                    "Could not get translation for original text:" & original & vbCrLf & "Error: " &
                    ex.Message)
                _fails = _fails + 1

                If _fails = 8 Then
                    Log(loggingLevel.Severe, "language", "Language Failed! Original values will be returned")
                    _failed = True
                End If

                Return original
            End Try
        End Function

        Public Function GetLanguageFilePath(language As String) As String
            Return LocalizationPath & "/" & language & ".xml"
        End Function

        Public Function GetLanguageName(languagePath As String) As String
            Dim fi As FileInfo = FileSystem.GetFileInfo(languagePath)
            Return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length)
        End Function
    End Module
End Namespace