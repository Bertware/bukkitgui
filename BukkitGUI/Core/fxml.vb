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

'fxml - FastXML
'wrapper around XmlDcoument, for easy and safe read/write of elements and attributes.
'Support for reading from parent node.
'Logging can be disabled, to prevent errors when livebug uses this class to write it's log.
Imports System.Threading
Imports System.Xml
Imports Microsoft.VisualBasic.FileIO

Namespace Core
    Public Class fxml
        Public Document As XmlDocument = Nothing
        Public Path As String
        Public Owner As String = "undefined" 'For logging puposes
        Public Log As Boolean = True
        Public DirectSave As Boolean = True

        Dim wfails As Integer = 0
        Const MAX_FAIL_WRITE As Byte = 8

        
        ''' <summary>
        '''     Create a new instance, but loading of xml content is still needed
        ''' </summary>
        ''' <remarks>Load or LoadXML should be called before further usage of the class</remarks>
        Public Sub New()
            Document = New XmlDocument
            path = ""
        End Sub

        
        ''' <summary>
        '''     Create a new instance, and immediatly load the file
        ''' </summary>
        ''' <param name="filepath">the file that will be loaded</param>
        ''' <param name="owner">The owner, for logging purposes</param>
        ''' <param name="log">If this class should log status or errors. Should only be "false" for livebug</param>
        ''' <remarks>After this routine all functions are ready to use</remarks>
        Public Sub New(filepath As String, owner As String, Optional ByVal log As Boolean = True) _
'also loads file. No_log feature needed for livebug
            Document = New XmlDocument
            Me.Owner = owner
            Me.log = log
            path = filepath
            Load()
        End Sub

        
        ''' <summary>
        '''     Load the file defined in the "path" variable
        ''' </summary>
        ''' <returns>the created document</returns>
        ''' <remarks></remarks>
        Public Function Load() As XmlDocument
            If Log Then livebug.Log(loggingLevel.Fine, "fxml - " & Owner, "Loading XML file: " & Path)
            Document = New XmlDocument
            Try
                Document.Load(path)
            Catch ex As Exception
                If Owner <> "language" Then
                    MessageBox.Show(
                        lr("The following xml file could not be loaded. Please check for errors") & ": " & path,
                        lr("Invalid XML file"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("The following xml file could not be loaded. Please check for errors: " & path,
                                    "Invalid XML file", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try


            If Log Then livebug.Log(loggingLevel.Fine, "fxml - " & Owner, "XML file loaded")
            Return Document
        End Function

        
        ''' <summary>
        '''     Load an xml text
        ''' </summary>
        ''' <param name="xml">the xml text to load</param>
        ''' <returns>the created document</returns>
        ''' <remarks></remarks>
        Public Function LoadXML(xml As String) As XmlDocument
            If xml = "" Then Return Nothing : Exit Function
            Document = Nothing
            Try
                If Log Then _
                    livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                "Loading XML file from contents: " & xml.Trim(vbCrLf).Trim(vbCr).Trim(vbLf))
                Document = New XmlDocument
                Document.LoadXml(xml)
                If Log Then livebug.Log(loggingLevel.Fine, "fxml - " & Owner, "XML file loaded from contents")
            Catch ex As Exception
                If Log Then livebug.Log(loggingLevel.Severe, "fxml - " & Owner, "XML file load failed: " & ex.Message)
            End Try
            Return Document
        End Function

        
        ''' <summary>
        '''     Check if the file is up to date.
        ''' </summary>
        ''' <param name="element_name">the element that contains the version attribute</param>
        ''' <param name="required_version">the required version. file must be equal or newer.</param>
        ''' <returns>Returns false if outdated</returns>
        ''' <remarks>The attributed should be named "version"</remarks>
        Public Function Verify_version(element_name As String, required_version As String) As Boolean _
'returns false if XML file is outdated. True if XML file is OK
            Dim e As XmlElement = Document.GetElementsByTagName(element_name)(0)
            If e Is Nothing Then Return True : Exit Function
            Dim ver As String = e.GetAttribute("version") 'Checks the version attribute
            If CheckVersion(ver, required_version) = 1 Then
                If Log Then livebug.Log(loggingLevel.Fine, "fxml - " & Owner, "Verifying versions, result: false")
                Return False
            Else
                If Log Then livebug.Log(loggingLevel.Fine, "fxml - " & Owner, "Verifying versions, result: true")
                Return True
            End If
        End Function

        
        ''' <summary>
        '''     Read the innertext of an element.
        ''' </summary>
        ''' <param name="element">the element to read</param>
        ''' <param name="defaultvalue">default value, will be saved if the element doesn't exist</param>
        ''' <param name="parent">The parent node. Will be created if not existing</param>
        ''' <returns>-Returns the inner text of the element. returns an empty string if an error occured</returns>
        Public Function read(element As String, Optional ByVal defaultvalue As String = "",
                             Optional ByVal parent As String = "") As String
            parent = parent.ToLower
            element = element.ToLower
            If parent <> "" Then CheckParent(parent, True)

            Try
                Dim node As XmlNode = Nothing
                Try
                    If parent = "" Then
                        If Document.GetElementsByTagName(element).Count > 0 Then _
                            node = Document.GetElementsByTagName(element)(0)
                    Else
                        CheckParent(parent)
                        If Document.GetElementsByTagName(parent)(0).Item(element) IsNot Nothing Then _
                            node = Document.GetElementsByTagName(parent)(0).Item(element)
                    End If
                Catch ex As Exception
                    If Log Then _
                        livebug.Log(loggingLevel.Severe, "fxml - " & Owner,
                                    "Error while reading setting, element or parent doesn't exist?" & ex.Message)
                End Try

                If node Is Nothing Then 'If element doesn't exist, create
                    If parent = "" Then
                        Dim newNode As XmlElement
                        node = Document.FirstChild
                        newNode = Document.CreateElement(element)
                        newNode.InnerText = defaultvalue
                        node.AppendChild(newNode)
                        If DirectSave Then save() 'save
                        node = Document.GetElementsByTagName(element)(0)
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Added not existing setting (Parent not provided)")
                    Else
                        Dim newNode As XmlElement
                        node = Document.GetElementsByTagName(parent)(0)
                        newNode = Document.CreateElement(element)
                        newNode.InnerText = defaultvalue
                        node.AppendChild(newNode)
                        If DirectSave Then save() 'save
                        node = Document.GetElementsByTagName(parent)(0).Item(element)
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Added not existing setting (Parent provided)")
                    End If
                End If

                If node IsNot Nothing Then
                    If element.Contains("pass") Or element.Contains("salt") Then
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Requested XML element: " & element & " - Value: ********")
                    Else
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Requested XML element: " & element & " - Value: " & node.InnerText)
                    End If
                    Return node.InnerText
                Else
                    If Log Then _
                        livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                    "Requested XML element could not be read: " & element)
                    Return ""
                End If
            Catch genex As Exception
                If Log Then _
                    livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                "Requested XML element could not be read due to error: " & element, genex.Message)
                Return ""
            End Try
        End Function

        
        ''' <summary>
        '''     Read the boolean value, stored in the inner text of an element.
        ''' </summary>
        ''' <param name="element">the element to read</param>
        ''' <param name="defaultvalue">default value, will be saved if the element doesn't exist</param>
        ''' <param name="parent">The parent node. Will be created if not existing</param>
        ''' <returns>-Returns the boolean value rrepresenting the inner text of the element. returns a false if an error occured</returns>
        Public Function readAsBool(element As String, Optional ByVal defaultvalue As Boolean = False,
                                   Optional ByVal parent As String = "") As Boolean
            If read(element, defaultvalue, parent).ToLower = "true" Then Return True Else Return False
        End Function

        
        ''' <summary>
        '''     Read the innertext of an element.
        ''' </summary>
        ''' <param name="element">the element to read</param>
        ''' <param name="attribute">The name of the attribute to read</param>
        ''' <param name="defaultvalue">default value, will be saved if the element doesn't exist</param>
        ''' <param name="parent">The parent node. Will be created if not existing</param>
        ''' <returns>Returns the value from the attribute</returns>
        Public Function readAttribute(element As String, attribute As String, Optional ByVal defaultvalue As String = "",
                                      Optional ByVal parent As String = "") As String
            attribute = attribute.ToLower
            parent = parent.ToLower
            element = element.ToLower

            If parent <> "" Then CheckParent(parent, True)

            Try
                Dim node As XmlElement = Nothing
                Try
                    If parent = "" Then
                        If Document.GetElementsByTagName(element).Count > 0 Then _
                            node = Document.GetElementsByTagName(element)(0)
                    Else
                        CheckParent(parent)
                        If Document.GetElementsByTagName(parent)(0).Item(element) IsNot Nothing Then _
                            node = Document.GetElementsByTagName(parent)(0).Item(element)
                    End If
                Catch ex As Exception
                    If Log Then _
                        livebug.Log(loggingLevel.Severe, "fxml - " & Owner,
                                    "Error while reading attribute, element or parent doesn't exist?", ex.Message)
                End Try

                If node Is Nothing Then 'If element doesn't exist, create
                    If parent = "" Then
                        Dim newNode As XmlElement
                        node = Document.FirstChild
                        newNode = Document.CreateElement(element)
                        newNode.InnerText = ""
                        node.AppendChild(newNode)
                        If DirectSave Then save() 'save
                        node = Document.GetElementsByTagName(element)(0)
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Added not existing setting (Parent not provided)")
                    Else
                        Dim newNode As XmlElement
                        node = Document.GetElementsByTagName(parent)(0)
                        newNode = Document.CreateElement(element)
                        newNode.InnerText = ""
                        node.AppendChild(newNode)
                        If DirectSave Then save() 'save
                        node = Document.GetElementsByTagName(parent)(0).Item(element)
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Added not existing setting (Parent provided)")
                    End If
                End If

                Dim res As String = Nothing

                res = node.GetAttribute(attribute)

                If res Is Nothing Then
                    node.SetAttribute(attribute, defaultvalue)
                    res = node.GetAttribute(attribute)
                End If

                If res IsNot Nothing Then
                    If Log Then _
                        livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                    "Requested XML attribute: " & element & " - Attribute: " & attribute &
                                    " - Value: " & res)
                    Return res
                Else
                    If Log Then _
                        livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                    "Requested XML attribute could not be read (res is null): " & element &
                                    " - Attribute: " & attribute)
                    Return ""
                End If
            Catch genex As Exception
                If Log Then _
                    livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                "Requested XML attribute could not be read due to error: " & element &
                                " - Attribute: " & attribute, genex.Message)
                Return ""
            End Try
        End Function

        
        ''' <summary>
        '''     Get an element out of a number of elements with the same name, where a certain attribute has a certain value.
        ''' </summary>
        ''' <param name="elementname">The name of the element</param>
        ''' <param name="attribute">the attribute to check</param>
        ''' <param name="value">the value to check</param>
        ''' <returns>the found element, nothing if nothing found</returns>
        ''' <remarks></remarks>
        Public Function getElementByAttribute(elementname As String, attribute As String, value As String) As XmlElement _
'Get the element containing that containts an attribute with the provided value
            elementname = elementname.ToLower
            attribute = attribute.ToLower 'value can contain uppercase, as uppercase might be needed for multi-language.
            Dim res As XmlElement = Nothing
            For Each xmle As XmlElement In Document.GetElementsByTagName(elementname)
                If xmle.GetAttribute(attribute) = value Then
                    res = xmle
                End If
            Next
            Return res
        End Function

        
        ''' <summary>
        '''     Write a value as an xml element to the loaded object
        ''' </summary>
        ''' <param name="element">the name of the element. will be forced to lowercase</param>
        ''' <param name="value">the value to write</param>
        ''' <param name="parent">the parent node for the element. will be forced to lowercase</param>
        ''' <param name="attributes">A list of attributes that should be added to the element</param>
        ''' <param name="duplicate_names">If duplicate named elements are allowed</param>
        ''' <returns>Returns the xml element</returns>
        ''' <remarks>creates items if they don't exist.</remarks>
        Public Function write(element As String, value As String, Optional ByVal parent As String = "",
                              Optional ByVal attributes As List(Of CXMLAttribute) = Nothing,
                              Optional duplicate_names As Boolean = False) As XmlElement
            If wfails >= MAX_FAIL_WRITE Then Return Nothing : Exit Function
            parent = parent.ToLower
            element = element.ToLower
            If parent <> "" Then CheckParent(parent, True)
            Dim node As XmlElement = Nothing

            Try
                If parent = "" Then
                    If _
                        (Document.GetElementsByTagName(element) IsNot Nothing AndAlso
                         Document.GetElementsByTagName(element).Count > 0 AndAlso
                         Document.GetElementsByTagName(element)(0) IsNot Nothing AndAlso duplicate_names = False) Then _
'If exists
                        node = Document.GetElementsByTagName(element)(0)
                        node.InnerText = value
                        If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                            For i As Byte = 0 To attributes.Count - 1
                                node.SetAttribute(attributes(i).name, attributes(i).value)
                            Next
                        End If
                        If DirectSave Then save() 'save

                    Else 'if not existing
                        node = Document.CreateElement(element)
                        node.InnerText = value
                        If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                            For i As Integer = 0 To attributes.Count - 1
                                node.SetAttribute(attributes(i).name, attributes(i).value)
                            Next
                        End If
                        Document.DocumentElement.AppendChild(node)
                        If DirectSave Then save() 'save

                    End If
                Else
                    CheckParent(parent)

                    If _
                        (Document.GetElementsByTagName(parent) IsNot Nothing AndAlso
                         Document.GetElementsByTagName(parent).Count > 0 AndAlso
                         Document.GetElementsByTagName(parent)(0) IsNot Nothing _
                         AndAlso
                         CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element) IsNot _
                         Nothing _
                         AndAlso
                         CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element).Count >
                         0 _
                         AndAlso
                         CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element)(0) IsNot _
                         Nothing AndAlso duplicate_names = False) Then 'If exists (both parent and node)

                        node =
                            CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element)(0)
                        node.InnerText = value
                        If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                            For i As Integer = 0 To attributes.Count - 1
                                node.SetAttribute(attributes(i).name, attributes(i).value)
                            Next
                        End If
                        If DirectSave Then save() 'save

                    Else 'if not existing
                        node = Document.CreateElement(element)
                        node.InnerText = value
                        If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                            For i As Integer = 0 To attributes.Count - 1
                                node.SetAttribute(attributes(i).name, attributes(i).value)
                            Next
                        End If
                        CType(Document.GetElementsByTagName(parent)(0), XmlElement).AppendChild(node)
                        If DirectSave Then save() 'save

                    End If
                End If

                If node IsNot Nothing Then
                    If log Then
                        If element.Contains("pass") Or element.Contains("salt") Then
                            If Log Then _
                                livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                            "Saved XML element: " & element & " - Value: ********")
                        Else
                            If Log Then _
                                livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                            "Saved XML element: " & element & " - Value: " & node.InnerText)
                        End If
                    End If

                    Return node
                Else
                    If Log Then _
                        livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                    "Requested XML element could not be written (node is null): " & element)
                    Return Nothing
                End If
            Catch genex As Exception
                If Log Then _
                    livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                "Requested XML element could not be written due to error: " & element, genex.Message)
                wfails += 1
                Return Nothing
            End Try
        End Function

        
        ''' <summary>
        '''     Write an boolean value as an xml element to the loaded object
        ''' </summary>
        ''' <param name="element">the name of the element. will be forced to lowercase</param>
        ''' <param name="value">the value to write, true or false</param>
        ''' <param name="parent">the parent node for the element. will be forced to lowercase</param>
        ''' <returns>Returns the xml element</returns>
        ''' <remarks>creates items if they don't exist. This function is created to make sure ReadAsBool works fine too.</remarks>
        Public Function writeAsBool(element As String, value As Boolean, Optional parent As String = "") As XmlElement
            Return write(element, value.ToString.ToLower, parent)
        End Function

        
        ''' <summary>
        '''     Set an attribute for an xml element in the loaded object
        ''' </summary>
        ''' <param name="element">The element that should be edited</param>
        ''' <param name="attribute">the attribute that should be added/edited</param>
        ''' <param name="value">the value for the attribute</param>
        ''' <param name="parent">the parent of the element</param>
        ''' <returns>True if succeed</returns>
        ''' <remarks>Will create the parent/element/attribute if it doesn't exist.</remarks>
        Public Function writeAttribute(element As String, attribute As String, value As String,
                                       Optional ByVal parent As String = "") As Boolean
            If wfails >= MAX_FAIL_WRITE Then Return False : Exit Function
            attribute = attribute.ToLower
            parent = parent.ToLower
            element = element.ToLower
            Try
                Dim node As XmlElement = Nothing
                Try
                    If parent = "" Then
                        If Document.GetElementsByTagName(element).Count > 0 Then _
                            node = Document.GetElementsByTagName(element)(0)
                    Else
                        CheckParent(parent)
                        If Document.GetElementsByTagName(parent)(0).Item(element) IsNot Nothing Then _
                            node = Document.GetElementsByTagName(parent)(0).Item(element)
                    End If
                Catch ex As Exception
                    If Log Then _
                        livebug.Log(loggingLevel.Severe, "fxml - " & Owner,
                                    "Error while reading attribute, element or parent doesn't exist?", ex.Message)
                End Try

                If node Is Nothing Then 'If element doesn't exist, create
                    If parent = "" Then
                        Dim newNode As XmlElement
                        node = Document.FirstChild
                        newNode = Document.CreateElement(element)
                        newNode.InnerText = ""
                        node.AppendChild(newNode)
                        If DirectSave Then save() 'save
                        node = Document.GetElementsByTagName(element)(0)
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Added not existing setting (Parent not provided)")
                    Else
                        Dim newNode As XmlElement
                        node = Document.GetElementsByTagName(parent)(0)
                        newNode = Document.CreateElement(element)
                        newNode.InnerText = ""
                        node.AppendChild(newNode)
                        If DirectSave Then save() 'save
                        node = Document.GetElementsByTagName(parent)(0).Item(element)
                        If Log Then _
                            livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                        "Added not existing setting (Parent provided)")
                    End If
                End If

                node.SetAttribute(attribute, value)

                If Log Then _
                    livebug.Log(loggingLevel.Fine, "fxml - " & Owner,
                                "Saved XML attribute: " & element & " - Attribute: " & attribute & " - Value: " &
                                node.InnerText)
                Return True

            Catch genex As Exception
                If Log Then _
                    livebug.Log(loggingLevel.Warning, "fxml - " & Owner,
                                "Requested XML attribute could not be written due to error: " & element &
                                " - Attribute: " & attribute, genex.Message)
                wfails += 1
                Return False
            End Try
        End Function

        
        ''' <summary>
        '''     Check if a certain node exists
        ''' </summary>
        ''' <param name="parent">the node to check</param>
        ''' <param name="create">if the node should be created if it doesn't exist</param>
        ''' <returns>True if the node exists or is created</returns>
        ''' <remarks></remarks>
        Function CheckParent(parent As String, Optional create As Boolean = True) As Boolean _
'Check if a (parent) element exists in the current document
            parent = parent.ToLower
            If _
                Document.GetElementsByTagName(parent.ToLower) Is Nothing OrElse
                Document.GetElementsByTagName(parent.ToLower)(0) Is Nothing OrElse
                Document.GetElementsByTagName(parent.ToLower).Count < 1 Then 'if nothing create
                If create Then CreateParent(parent) Else Return False 'if create = true, create the parent node.
            End If
            Return True
        End Function

        
        ''' <summary>
        '''     Create a new empty node, for parent nodes
        ''' </summary>
        ''' <param name="parent">the name of the new element</param>
        ''' <returns>the newly created element</returns>
        ''' <remarks></remarks>
        Function CreateParent(parent As String) As XmlElement 'Create a node in the current document
            If wfails >= MAX_FAIL_WRITE Then Return Nothing : Exit Function
            parent = parent.ToLower
            Dim n As XmlNode
            Dim root = Document.DocumentElement 'get the root
            n = Document.CreateElement(parent) 'create element
            root.PrependChild(n) 'insert element
            If DirectSave Then save() 'save 
            Return n
        End Function

        
        ''' <summary>
        '''     Get the first element with the given name.
        ''' </summary>
        ''' <param name="element"></param>
        ''' <returns>the found xml element, or nothing when an error occured</returns>
        ''' <remarks>Catches errors + returns as xmlelement instead of node</remarks>
        Function GetElementByName(element As String) As XmlElement
            Try
                Return Document.GetElementsByTagName(element)(0)
            Catch ex As Exception
                If Log Then _
                    livebug.Log(loggingLevel.Severe, "fxml - " & Owner,
                                "An exception occured while getting an xml element based on the name: element:" &
                                element, ex.Message)
                Return Nothing
            End Try
        End Function

        Function GetElementsByName(element As String) As XmlNodeList
            Try
                Return Document.GetElementsByTagName(element.ToLower)
            Catch ex As Exception
                If Log Then _
                    livebug.Log(loggingLevel.Severe, "fxml - " & Owner,
                                "An exception occured while getting xmlnodelist based on element name: element:" &
                                element, ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function RemoveElement(name As String) As Boolean
            Dim element As XmlElement = Nothing

            If _
                Document.GetElementsByTagName(name) IsNot Nothing AndAlso Document.GetElementsByTagName(name).Count > 0 AndAlso
                Document.GetElementsByTagName(name)(0) IsNot Nothing Then
                element = Document.GetElementsByTagName(name)(0)
            Else
                If Log Then _
                    livebug.Log(loggingLevel.Fine, "fxml - " & Owner, "Cannot remove item, doesn't exist:" & name)
                Return False
                Exit Function
            End If

            Document.RemoveChild(element)
            If DirectSave Then save() 'save 
            Return True
        End Function

        Public Function RemoveElement(element As XmlElement) As Boolean
            Document.DocumentElement.RemoveChild(element)
            If DirectSave Then save() 'save 
            Return True
        End Function

        Public Function RemoveElement(element As XmlNode) As Boolean
            Document.RemoveChild(element)
            If DirectSave Then save() 'save 
            Return True
        End Function

        Private saving As Boolean = False

        Public Function save() As Boolean
            If (path IsNot Nothing AndAlso path <> "") Then
                While saving = True 'only one save action can be performed at once, to prevent fileio errors
                    '2 options: wait to save, or cancel save
                    Thread.Sleep(1) 'wait to save
                    'Exit sub  'cancel save
                End While
                saving = True
                If FileSystem.FileExists(path) Then Document.Save(path)
                saving = False
            End If
            Return True
        End Function
    End Class

    Public Class CXMLAttribute
        Public name As String, value As String

        Public Sub New(name As String, value As String)
            Me.name = name
            Me.value = value
        End Sub
    End Class
End Namespace