

'The config for the GUI
'Most functions are already provided by fxml
'Used to read/write to config


Namespace Core
    ''' <summary>
    ''' Read/Write settings to config.xml
    ''' </summary>
    ''' <remarks></remarks>
    Module config
        Dim cfile As fxml

        Const CURR_VER As String = "1.2" 'Minimal version for the config xml file
        Private config_XML As String = common.ConfigPath & "/config.xml"

        Public ReadOnly Property FxmlHandle As fxml
            Get
                Return cfile
            End Get
        End Property


        ''' <summary>
        ''' Initialize the config file
        ''' </summary>
        ''' <returns>True on success</returns>
        ''' <remarks>Must be done before anything else</remarks>
        Public Function init() As Boolean
            config_XML = common.ConfigPath & "/config.xml"

            If Not FileIO.FileSystem.FileExists(config_XML) Then _
                common.Create_file(config_XML, "<config version=""" & CURR_VER & """></config>")
            If common.File_Empty(config_XML) Then _
                common.Create_file(config_XML, "<config version=""" & CURR_VER & """></config>")

            cfile = New fxml(config_XML, "config") 'initialize file

            Select Case cfile.Verify_version("config", CURR_VER) 'check version
                Case False
                    common.Create_file(config_XML, "<config version=""" & CURR_VER & """></config>") _
                    'config content will be added automaticly during use.
            End Select
            livebug.write(loggingLevel.Fine, "Config", "Config initialized")

            Return True
        End Function

        ''' <summary>
        ''' Read a string from the config.
        ''' </summary>
        ''' <param name="element">The element to read from</param>
        ''' <param name="defaultvalue">The value that will be written and returned if the element doesn't exist</param>
        ''' <param name="parent">The parent node for the setting</param>
        ''' <returns>The value as string</returns>
        ''' <remarks></remarks>
        Public Function read(element As String, Optional ByVal defaultvalue As String = "",
                             Optional ByVal parent As String = "") As String
            Return cfile.read(element, defaultvalue, parent)
        End Function

        Public Function readAsBool(element As String, Optional ByVal defaultvalue As Boolean = False,
                                   Optional ByVal parent As String = "") As Boolean
            If read(element, defaultvalue.ToString.ToLower, parent).ToLower = "true" Then Return True Else Return False
        End Function

        Public Function readAttribute(element As String, attribute As String, Optional ByVal defaultvalue As String = "",
                                      Optional ByVal parent As String = "") As String
            Return cfile.readAttribute(element, attribute, defaultvalue, parent)
        End Function

        ''' <summary>
        ''' Write a string to the config.
        ''' </summary>
        ''' <param name="element">The element to write to</param>
        ''' <param name="value">The value that will be written</param>
        ''' <param name="parent">The parent node for the setting</param>
        ''' <returns>The written XML element</returns>
        ''' <remarks></remarks>
        Public Function write(element As String, value As String, Optional ByVal parent As String = "",
                              Optional ByVal attributes As List(Of CXMLAttribute) = Nothing) As Xml.XmlElement
            Return cfile.write(element, value, parent, attributes)
        End Function

        Public Function writeAsBool(element As String, value As Boolean, Optional parent As String = "") _
            As Xml.XmlElement
            Return write(element, value.ToString.ToLower, parent)
        End Function

        Public Function writeAttribute(element As String, attribute As String, value As String,
                                       Optional ByVal parent As String = "") As Boolean
            Return cfile.writeAttribute(element, attribute, value, parent)
        End Function
    End Module
End Namespace