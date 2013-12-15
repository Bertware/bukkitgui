Imports Net.Bertware.BukkitGUI.Core


Namespace Utilities
    ''' <summary>
    ''' This module adds options that are only editable in the config.xml
    ''' </summary>
    ''' <remarks></remarks>
    Module AdvancedOptions

        Public SpamFilter As List(Of String)
        Public MinotarSize As Byte = 32
        Public MinotarSource As String = "http://minotar.net"

        Public Sub AdvancedOptions_Load()
            Try
                SpamFilter = New List(Of String)
                config.read("spamfilter", "", "advanced") 'make sure one element is created
                Dim nlist As Xml.XmlNodeList = config.FxmlHandle.GetElementsByName("spamfilter")
                For Each element As Xml.XmlElement In nlist
                    'if innertext is nothing or "", don't add!
                    If element.InnerText IsNot Nothing AndAlso element.InnerText IsNot Nothing AndAlso element.InnerText <> "" AndAlso element.InnerText.Trim <> "" Then SpamFilter.Add(element.InnerText)
                Next

                Dim tmp As Integer = 0
                If Integer.TryParse(config.read("minotar_size", "32", "advanced"), tmp) Then
                    If tmp > 128 Then MinotarSize = 128 Else MinotarSize = tmp
                Else
                    MinotarSize = 32
                End If

                MinotarSource = config.read("minotar_source", "http://minotar.net", "advanced")

            Catch ex As Exception
            End Try
        End Sub

    End Module

End Namespace