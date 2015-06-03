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

'The config for the GUI
'Most functions are already provided by fxml
'Used to read/write to config
Imports System.Xml
Imports Microsoft.VisualBasic.FileIO

Namespace Core
    ' <summary>
    '     Read/Write settings to config.xml
    ' </summary>
    ' <remarks></remarks>
    Module config
        Dim cfile As fxml

        Const CURR_VER As String = "1.2" 'Minimal version for the config xml file
        Private config_XML As String = ConfigPath & "/config.xml"

        Public ReadOnly Property FxmlHandle As fxml
            Get
                Return cfile
            End Get
        End Property


        ''' <summary>
        '''     Initialize the config file
        ''' </summary>
        ''' <returns>True on success</returns>
        ''' <remarks>Must be done before anything else</remarks>
        Public Function init() As Boolean
            config_XML = ConfigPath & "/config.xml"

            If Not FileSystem.FileExists(config_XML) Then _
                Create_file(config_XML, "<config version=""" & CURR_VER & """></config>")
            If File_Empty(config_XML) Then _
                Create_file(config_XML, "<config version=""" & CURR_VER & """></config>")

            cfile = New fxml(config_XML, "config") 'initialize file

            Select Case cfile.Verify_version("config", CURR_VER) 'check version
                Case False
                    Create_file(config_XML, "<config version=""" & CURR_VER & """></config>") _
                    'config content will be added automaticly during use.
            End Select
            Log(loggingLevel.Fine, "Config", "Config initialized")

            Return True
        End Function


        ''' <summary>
        '''     Read a string from the config.
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
            If read(element, defaultvalue.ToString.ToLower, parent).ToLower = "true" Then Return True Else _
                Return False
        End Function

        Public Function readAttribute(element As String, attribute As String,
                                      Optional ByVal defaultvalue As String = "",
                                      Optional ByVal parent As String = "") As String
            Return cfile.readAttribute(element, attribute, defaultvalue, parent)
        End Function


        ''' <summary>
        '''     Write a string to the config.
        ''' </summary>
        ''' <param name="element">The element to write to</param>
        ''' <param name="value">The value that will be written</param>
        ''' <param name="parent">The parent node for the setting</param>
        ''' <returns>The written XML element</returns>
        ''' <remarks></remarks>
        Public Function write(element As String, value As String, Optional ByVal parent As String = "",
                              Optional ByVal attributes As List(Of CXMLAttribute) = Nothing) As XmlElement
            Return cfile.write(element, value, parent, attributes)
        End Function

        Public Function writeAsBool(element As String, value As Boolean, Optional parent As String = "") _
            As XmlElement
            Return write(element, value.ToString.ToLower, parent)
        End Function

        Public Function writeAttribute(element As String, attribute As String, value As String,
                                       Optional ByVal parent As String = "") As Boolean
            Return cfile.writeAttribute(element, attribute, value, parent)
        End Function
    End Module
End Namespace