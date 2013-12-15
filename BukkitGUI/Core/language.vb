Imports System.Xml


Imports System.IO

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
        Private def_english As String = common.Localization_path & "/" & "default.xml"
        Const CURR_VER As String = "1.0"

        Dim lxml As fxml

        Dim fails As Byte = 0
        Dim failed As Boolean = False
        Dim enabled As Boolean = False

        Public ReadOnly Property languages As List(Of String)
            Get
                Dim l As New List(Of String)
                For Each file As String In FileIO.FileSystem.GetFiles(common.Localization_path)
                    l.Add(GetLanguageName(file))
                Next
                Return l
            End Get
        End Property

        Public Property language_file As String
            Get
                Return config.read("language", def_english)
            End Get
            Set(value As String)
                config.write("language", value)
                init()
            End Set
        End Property

        Public ReadOnly Property current_language As String
            Get
                Return GetLanguageName(config.read("language", def_english))
            End Get
        End Property


        Public Sub init() 'Open XML file, MUST BE DONE BEFORE USEAGE!
            Try
                Dim localization_file As String = config.read("language", def_english)

                If Not FileIO.FileSystem.FileExists(localization_file) Then common.Create_file(localization_file, "<language version=""" & CURR_VER & """></language>")
                If common.File_Empty(localization_file) Then common.Create_file(localization_file, "<language version=""" & CURR_VER & """></language>")

                lxml = New fxml(localization_file, "language") 'initialize fxml, path is given so file is available instantly
                enabled = True
            Catch memex As System.OutOfMemoryException
                livebug.write(loggingLevel.Warning, "language", "Language initialization failed (OutOfMemory) - " & memex.Message)
            Catch pex As Security.SecurityException
                livebug.write(loggingLevel.Warning, "language", "Language initialization failed (SecurityException) - " & pex.Message)
            Catch ioex As IO.IOException
                livebug.write(loggingLevel.Warning, "language", "Language initialization failed (IOException) - " & ioex.Message)
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "language", "Language initialization failed", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Replace a text with it's translation. If a translation isn't available, the original value will be returned
        ''' </summary>
        ''' <param name="original">the original (english) text</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function lr(original As String) As String 'Language Replace - replace an english string with the translation
            Try
                If enabled = False Then Return original : Exit Function
                If failed Then Return original : Exit Function
                If original Is Nothing Then Return Nothing : Exit Function
                'if item doesn't exist, add to the XML file and return translation.
                'if item exists, return translation

                If lxml Is Nothing OrElse lxml.Document Is Nothing Then Return original : Exit Function

                Dim res As Xml.XmlElement = lxml.getElementByAttribute("text", "string", original)

                If res IsNot Nothing Then
                    ' Debug.WriteLine("Translated """ + original + """ into """ + System.Web.HttpUtility.UrlDecode(res.InnerText) + """", "[language]")
                    Return System.Web.HttpUtility.UrlDecode(res.InnerText)
                Else 'Item doesn't exist. Create it, so it can be translated
                    livebug.write(loggingLevel.Fine, "language", "Translation not found, adding. Original:" & original)
                    Dim l As New List(Of CXMLAttribute)
                    l.Add(New CXMLAttribute("string", original))
                    lxml.write("text", System.Web.HttpUtility.UrlEncodeUnicode(original), "", l, True)
                    livebug.write(loggingLevel.Fine, "language", "Added to language file. Returning original value")
                    ' Debug.WriteLine("""" + original + """ was not translated (not available)", "[language]")
                    Return original 'no translation available, so keep the original language
                End If
            Catch ex As Exception
                livebug.write(loggingLevel.Warning, "language", "Could not get translation for original text:" & original & vbCrLf & "Error: " & ex.Message)
                fails = fails + 1
                If fails = 8 Then
                    livebug.write(loggingLevel.Severe, "language", "Language Failed! Original values will be returned")
                    failed = True
                End If
                Return original
            End Try
        End Function

        Public Function GetLanguageFilePath(language As String) As String
            Return common.Localization_path & "/" & language & ".xml"
        End Function

        Public Function GetLanguageName(languagePath As String) As String
            Dim fi As FileInfo = FileIO.FileSystem.GetFileInfo(languagePath)
            Return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length)
        End Function

    End Module
End Namespace