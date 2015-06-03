'============================================='
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
'============================================='

'fxml - FastXML
'wrapper around XmlDcoument, for easy and safe read/write of elements and attributes.
'Support for reading from parent node.
'Logging can be disabled, to prevent errors when livebug uses this class to write it's log.

Imports System.Xml
Imports System.IO
Imports Microsoft.VisualBasic.FileIO.FileSystem

Imports System.Threading
Imports System.Windows.Forms

Public Class fxml

    Public Document As XmlDocument = Nothing
    Public path As String

    ''' <summary>
    ''' Create a new instance, but loading of xml content is still needed
    ''' </summary>
    ''' <remarks>Load or LoadXML should be called before further usage of the class</remarks>
    Public Sub New()
        Document = New XmlDocument
        path = ""
    End Sub

    ''' <summary>
    ''' Create a new instance, and immediatly load the file
    ''' </summary>
    ''' <param name="filepath">the file that will be loaded</param>
    ''' <remarks>After this routine all functions are ready to use</remarks>
    Public Sub New(filepath As String) 'also loads file. No_log feature needed for livebug
        Document = New XmlDocument
        path = filepath
        Load()
    End Sub

    ''' <summary>
    ''' Load the file defined in the "path" variable
    ''' </summary>
    ''' <returns>the created document</returns>
    ''' <remarks></remarks>
    Public Function Load() As XmlDocument
        Debug.WriteLine("Loading XML file: " & path)
        Document = New XmlDocument
        Try
            Document.Load(path)
        Catch ex As Exception
            MessageBox.Show("The following xml file could not be loaded. Please check for errors: " & path, "Invalid XML file", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Debug.WriteLine("XML file loaded")
        Return Document
    End Function

    ''' <summary>
    ''' Load an xml text
    ''' </summary>
    ''' <param name="xml">the xml text to load</param>
    ''' <returns>the created document</returns>
    ''' <remarks></remarks>
    Public Function LoadXML(xml As String) As XmlDocument
        If xml = "" Then Return Nothing : Exit Function
        Debug.WriteLine("Loading XML file from contents: " & xml)
        Document = New XmlDocument
        Document.LoadXml(xml)
        Debug.WriteLine("XML file loaded from contents")
        Return Document
    End Function


    ''' <summary>
    ''' Read the innertext of an element.
    ''' </summary>
    ''' <param name="element">the element to read</param>
    ''' <param name="defaultvalue">default value, will be saved if the element doesn't exist</param>
    ''' <param name="parent">The parent node. Will be created if not existing</param>
    ''' <returns>-Returns the inner text of the element. returns an empty string if an error occured</returns>
    Public Function read(element As String, Optional ByVal defaultvalue As String = "", Optional ByVal parent As String = "") As String
        parent = parent.ToLower
        element = element.ToLower
        If parent <> "" Then CheckParent(parent, True)

        Try
            Dim node As XmlNode = Nothing
            Try
                If parent = "" Then
                    If Document.GetElementsByTagName(element).Count > 0 Then node = Document.GetElementsByTagName(element)(0)
                Else
                    CheckParent(parent)
                    If Document.GetElementsByTagName(parent)(0).Item(element) IsNot Nothing Then node = Document.GetElementsByTagName(parent)(0).Item(element)
                End If
            Catch ex As Exception
                Debug.WriteLine("Error while reading setting, element or parent doesn't exist?" & ex.Message)
            End Try

            If node Is Nothing Then 'If element doesn't exist, create
                If parent = "" Then
                    Dim newNode As System.Xml.XmlElement
                    node = Document.FirstChild
                    newNode = Document.CreateElement(element)
                    newNode.InnerText = defaultvalue
                    node.AppendChild(newNode)
                    save() 'save
                    node = Document.GetElementsByTagName(element)(0)
                    Debug.WriteLine("Added not existing setting (Parent not provided)")
                Else
                    Dim newNode As System.Xml.XmlElement
                    node = Document.GetElementsByTagName(parent)(0)
                    newNode = Document.CreateElement(element)
                    newNode.InnerText = defaultvalue
                    node.AppendChild(newNode)
                    save() 'save
                    node = Document.GetElementsByTagName(parent)(0).Item(element)
                    Debug.WriteLine("Added not existing setting (Parent provided)")
                End If
            End If

            If node IsNot Nothing Then
                Debug.WriteLine("Requested XML element: " & element & " - Value: " & node.InnerText)
                Return node.InnerText
            Else
                Debug.WriteLine("Requested XML element could not be read: " & element)
                Return ""
            End If
        Catch genex As Exception
            Debug.WriteLine("Requested XML element could not be read due to error: " & element)
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Read the boolean value, stored in the inner text of an element.
    ''' </summary>
    ''' <param name="element">the element to read</param>
    ''' <param name="defaultvalue">default value, will be saved if the element doesn't exist</param>
    ''' <param name="parent">The parent node. Will be created if not existing</param>
    ''' <returns>-Returns the boolean value rrepresenting the inner text of the element. returns a false if an error occured</returns>
    Public Function readAsBool(element As String, Optional ByVal defaultvalue As Boolean = False, Optional ByVal parent As String = "") As Boolean
        If read(element, defaultvalue, parent).ToLower = "true" Then Return True Else Return False
    End Function

    ''' <summary>
    ''' Read the innertext of an element.
    ''' </summary>
    ''' <param name="element">the element to read</param>
    ''' <param name="attribute">The name of the attribute to read</param>
    ''' <param name="defaultvalue">default value, will be saved if the element doesn't exist</param>
    ''' <param name="parent">The parent node. Will be created if not existing</param>
    ''' <returns>Returns the value from the attribute</returns>
    Public Function readAttribute(element As String, attribute As String, Optional ByVal defaultvalue As String = "", Optional ByVal parent As String = "") As String
        attribute = attribute.ToLower
        parent = parent.ToLower
        element = element.ToLower

        If parent <> "" Then CheckParent(parent, True)

        Try
            Dim node As XmlElement = Nothing
            Try
                If parent = "" Then
                    If Document.GetElementsByTagName(element).Count > 0 Then node = Document.GetElementsByTagName(element)(0)
                Else
                    CheckParent(parent)
                    If Document.GetElementsByTagName(parent)(0).Item(element) IsNot Nothing Then node = Document.GetElementsByTagName(parent)(0).Item(element)
                End If
            Catch ex As Exception
                Debug.WriteLine("Error while reading attribute, element or parent doesn't exist?" & ex.Message)
            End Try

            If node Is Nothing Then 'If element doesn't exist, create
                If parent = "" Then
                    Dim newNode As System.Xml.XmlElement
                    node = Document.FirstChild
                    newNode = Document.CreateElement(element)
                    newNode.InnerText = ""
                    node.AppendChild(newNode)
                    save() 'save
                    node = Document.GetElementsByTagName(element)(0)
                    Debug.WriteLine("Added not existing setting (Parent not provided)")
                Else
                    Dim newNode As System.Xml.XmlElement
                    node = Document.GetElementsByTagName(parent)(0)
                    newNode = Document.CreateElement(element)
                    newNode.InnerText = ""
                    node.AppendChild(newNode)
                    save() 'save
                    node = Document.GetElementsByTagName(parent)(0).Item(element)
                    Debug.WriteLine("Added not existing setting (Parent provided)")
                End If
            End If

            Dim res As String = Nothing

            res = node.GetAttribute(attribute)

            If res Is Nothing Then
                node.SetAttribute(attribute, defaultvalue)
                res = node.GetAttribute(attribute)
            End If

            If res IsNot Nothing Then
                Debug.WriteLine("Requested XML attribute: " & element & " - Attribute: " & attribute & " - Value: " & res)
                Return res
            Else
                Debug.WriteLine("Requested XML attribute could not be read: " & element & " - Attribute: " & attribute)
                Return ""
            End If
        Catch genex As Exception
            Debug.WriteLine("Requested XML attribute could not be read due to error: " & element & " - Attribute: " & attribute)
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Get an element out of a number of elements with the same name, where a certain attribute has a certain value.
    ''' </summary>
    ''' <param name="elementname">The name of the element</param>
    ''' <param name="attribute">the attribute to check</param>
    ''' <param name="value">the value to check</param>
    ''' <returns>the found element, nothing if nothing found</returns>
    ''' <remarks></remarks>
    Public Function getElementByAttribute(elementname As String, attribute As String, value As String) As XmlElement  'Get the element containing that containts an attribute with the provided value
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
    ''' Write a value as an xml element to the loaded object
    ''' </summary>
    ''' <param name="element">the name of the element. will be forced to lowercase</param>
    ''' <param name="value">the value to write</param>
    ''' <param name="parent">the parent node for the element. will be forced to lowercase</param>
    ''' <param name="attributes">A list of attributes that should be added to the element</param>
    ''' <param name="duplicate_names">If duplicate named elements are allowed</param>
    ''' <returns>Returns the xml element</returns>
    ''' <remarks>creates items if they don't exist.</remarks>
    Public Function write(element As String, value As String, Optional ByVal parent As String = "", Optional ByVal attributes As List(Of CXMLAttribute) = Nothing, Optional duplicate_names As Boolean = False) As XmlElement
        parent = parent.ToLower
        element = element.ToLower
        If parent <> "" Then CheckParent(parent, True)
        Dim node As XmlElement = Nothing

        Try
            If parent = "" Then
                If (Document.GetElementsByTagName(element) IsNot Nothing AndAlso Document.GetElementsByTagName(element).Count > 0 AndAlso Document.GetElementsByTagName(element)(0) IsNot Nothing AndAlso duplicate_names = False) Then 'If exists
                    node = Document.GetElementsByTagName(element)(0)
                    node.InnerText = value
                    If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                        For i As Byte = 0 To attributes.Count - 1
                            node.SetAttribute(attributes(i).name, attributes(i).value)
                        Next
                    End If
                    save() 'save

                Else 'if not existing
                    node = Document.CreateElement(element)
                    node.InnerText = value
                    If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                        For i As Byte = 0 To attributes.Count - 1
                            node.SetAttribute(attributes(i).name, attributes(i).value)
                        Next
                    End If
                    Document.DocumentElement.AppendChild(node)
                    save() 'save

                End If
            Else
                CheckParent(parent)

                If (Document.GetElementsByTagName(parent) IsNot Nothing AndAlso Document.GetElementsByTagName(parent).Count > 0 AndAlso Document.GetElementsByTagName(parent)(0) IsNot Nothing _
                    AndAlso CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element) IsNot Nothing _
                    AndAlso CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element).Count > 0 _
                    AndAlso CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element)(0) IsNot Nothing AndAlso duplicate_names = False) Then 'If exists (both parent and node)

                    node = CType(Document.GetElementsByTagName(parent)(0), XmlElement).GetElementsByTagName(element)(0)
                    node.InnerText = value
                    If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                        For i As Byte = 0 To attributes.Count - 1
                            node.SetAttribute(attributes(i).name, attributes(i).value)
                        Next
                    End If
                    save() 'save

                Else 'if not existing
                    node = Document.CreateElement(element)
                    node.InnerText = value
                    If attributes IsNot Nothing AndAlso attributes.Count > 0 Then
                        For i As Byte = 0 To attributes.Count - 1
                            node.SetAttribute(attributes(i).name, attributes(i).value)
                        Next
                    End If
                    CType(Document.GetElementsByTagName(parent)(0), XmlElement).AppendChild(node)
                    save() 'save

                End If
            End If

            If node IsNot Nothing Then
                Debug.WriteLine("Saved XML element: " & element & " - Value: " & node.InnerText)
                Return node
            Else
                Debug.WriteLine("Requested XML element could not be written: " & element)
                Return Nothing
            End If
        Catch genex As Exception
            Debug.WriteLine("Requested XML element could not be written due to error: " & element)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Write an boolean value as an xml element to the loaded object
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
    ''' Set an attribute for an xml element in the loaded object
    ''' </summary>
    ''' <param name="element">The element that should be edited</param>
    ''' <param name="attribute">the attribute that should be added/edited</param>
    ''' <param name="value">the value for the attribute</param>
    ''' <param name="parent">the parent of the element</param>
    ''' <returns>True if succeed</returns>
    ''' <remarks>Will create the parent/element/attribute if it doesn't exist.</remarks>
    Public Function writeAttribute(element As String, attribute As String, value As String, Optional ByVal parent As String = "") As Boolean
        attribute = attribute.ToLower
        parent = parent.ToLower
        element = element.ToLower
        Try
            Dim node As XmlElement = Nothing
            Try
                If parent = "" Then
                    If Document.GetElementsByTagName(element).Count > 0 Then node = Document.GetElementsByTagName(element)(0)
                Else
                    CheckParent(parent)
                    If Document.GetElementsByTagName(parent)(0).Item(element) IsNot Nothing Then node = Document.GetElementsByTagName(parent)(0).Item(element)
                End If
            Catch ex As Exception
                Debug.WriteLine("Error while reading attribute, element or parent doesn't exist?" & ex.Message)
            End Try

            If node Is Nothing Then 'If element doesn't exist, create
                If parent = "" Then
                    Dim newNode As System.Xml.XmlElement
                    node = Document.FirstChild
                    newNode = Document.CreateElement(element)
                    newNode.InnerText = ""
                    node.AppendChild(newNode)
                    save() 'save
                    node = Document.GetElementsByTagName(element)(0)
                    Debug.WriteLine("Added not existing setting (Parent not provided)")
                Else
                    Dim newNode As System.Xml.XmlElement
                    node = Document.GetElementsByTagName(parent)(0)
                    newNode = Document.CreateElement(element)
                    newNode.InnerText = ""
                    node.AppendChild(newNode)
                    save() 'save
                    node = Document.GetElementsByTagName(parent)(0).Item(element)
                    Debug.WriteLine("Added not existing setting (Parent provided)")
                End If
            End If

            node.SetAttribute(attribute, value)

            Debug.WriteLine("Saved XML attribute: " & element & " - Attribute: " & attribute & " - Value: " & node.InnerText)
            Return True

        Catch genex As Exception
            Debug.WriteLine("Requested XML attribute could not be read due to error: " & element & " - Attribute: " & attribute)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Check if a certain node exists
    ''' </summary>
    ''' <param name="parent">the node to check</param>
    ''' <param name="create">if the node should be created if it doesn't exist</param>
    ''' <returns>True if the node exists or is created</returns>
    ''' <remarks></remarks>
    Function CheckParent(parent As String, Optional create As Boolean = True) As Boolean 'Check if a (parent) element exists in the current document
        parent = parent.ToLower
        If Document.GetElementsByTagName(parent.ToLower) Is Nothing OrElse Document.GetElementsByTagName(parent.ToLower)(0) Is Nothing OrElse Document.GetElementsByTagName(parent.ToLower).Count < 1 Then 'if nothing create
            If create Then CreateParent(parent) Else Return False 'if create = true, create the parent node.
        End If
        Return True
    End Function

    ''' <summary>
    ''' Create a new empty node, for parent nodes
    ''' </summary>
    ''' <param name="parent">the name of the new element</param>
    ''' <returns>the newly created element</returns>
    ''' <remarks></remarks>
    Function CreateParent(parent As String) As XmlElement 'Create a node in the current document
        parent = parent.ToLower
        Dim n As XmlNode
        Dim root = Document.DocumentElement 'get the root
        n = Document.CreateElement(parent) 'create element
        root.PrependChild(n) 'insert element
        save() 'save 
        Return n
    End Function

    ''' <summary>
    ''' Get the first element with the given name.
    ''' </summary>
    ''' <param name="element"></param>
    ''' <returns>the found xml element, or nothing when an error occured</returns>
    ''' <remarks>Catches errors + returns as xmlelement instead of node</remarks>
    Function GetElementByName(element As String) As XmlElement
        Try
            Return Document.GetElementsByTagName(element)(0)
        Catch ex As Exception
            Debug.WriteLine("An exception occured while getting an xml element based on the name: element:" & element & " - ex:" & ex.Message)
            Return Nothing
        End Try
    End Function

    Function GetElementsByName(element As String) As XmlNodeList
        Try
            Return Document.GetElementsByTagName(element)
        Catch ex As Exception
            Debug.WriteLine("An exception occured while getting xmlnodelist based on element name: element:" & element & " - ex:" & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RemoveElement(name As String) As Boolean
        Dim element As XmlElement = Nothing

        If Document.GetElementsByTagName(name) IsNot Nothing AndAlso Document.GetElementsByTagName(name).Count > 0 AndAlso Document.GetElementsByTagName(name)(0) IsNot Nothing Then
            element = Document.GetElementsByTagName(name)(0)
        Else
            Debug.WriteLine("Cannot remove item, doesn't exist:" & name)
            Return False
            Exit Function
        End If

        Document.RemoveChild(element)
        save() 'save 
        Return True
    End Function

    Public Function RemoveElement(element As XmlElement) As Boolean
        Document.DocumentElement.RemoveChild(element)
        save() 'save 
        Return True
    End Function

    Public Function RemoveElement(element As XmlNode) As Boolean
        Document.RemoveChild(element)
        save() 'save 
        Return True
    End Function

    Private saving As Boolean = False

    Public Function save() As Boolean
        If (path IsNot Nothing AndAlso path <> "") Then
            While saving = True 'only one save action can be performed at once, to prevent fileio errors
                '2 options: wait to save, or cancel save
                Thread.Sleep(0.01) 'wait to save
                'Exit sub  'cancel save
            End While
            saving = True
            If FileIO.FileSystem.FileExists(path) Then Document.Save(path)
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
