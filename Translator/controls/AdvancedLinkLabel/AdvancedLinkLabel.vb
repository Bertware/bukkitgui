Public Class AdvancedLinkLabel
    Inherits LinkLabel

    Public ReadOnly Property URL As String
        Get
            Dim _url As String = ""
            If Me.Text.Contains("http://") Then
                Dim url_start As Byte = Me.Text.IndexOf("http://")
                Dim url_stop As Byte : If Me.Text.Substring(url_start).Contains(" ") Then url_stop = Me.Text.IndexOf(" ", url_start) Else url_stop = Text.Length
                _url = Me.Text.Substring(url_start, url_stop - url_start)
            ElseIf Me.Text.Contains("https://") Then
                Dim url_start As Byte = Me.Text.IndexOf("https://")
                Dim url_stop As Byte : If Me.Text.Substring(url_start).Contains(" ") Then url_stop = Me.Text.IndexOf(" ", url_start) Else url_stop = Text.Length
                _url = Me.Text.Substring(url_start, url_stop - url_start)
            ElseIf Me.Text.Contains("www.") Then
                Dim url_start As Byte = Me.Text.IndexOf("www.")
                Dim url_stop As Byte : If Me.Text.Substring(url_start).Contains(" ") Then url_stop = Me.Text.IndexOf(" ", url_start) Else url_stop = Text.Length
                _url = "http://" & Me.Text.Substring(url_start, url_stop - url_start)
            End If
            Return _url
        End Get
    End Property

    Private Sub clicked() Handles MyBase.Click, MyBase.DoubleClick
        Try
            Dim p As New Process
            p.StartInfo.FileName = Me.URL
            p.Start()
        Catch ex As Exception
            Debug.WriteLine("Cannot go to webpage:" & Me.URL & " - Exception:" & ex.Message)
        End Try

    End Sub

End Class
