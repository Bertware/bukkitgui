Imports System.Threading
Imports Net.Bertware.BukkitGUI.Core


Public Class LanguageInstaller
    Private languages As List(Of Translation)
    Private Event LanguagesLoaded()

    Private Sub LanguageInstaller_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim t As New Threading.Thread(AddressOf getLanguages)
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
            If languages Is Nothing Then Exit Sub
            For Each t As Translation In languages
                Dim lvi As New ListViewItem({t.language, t.version, t.translator, t.comment})
                ALVLanguages.Items.Add(lvi)
            Next
        End If
    End Sub

    Private Sub getLanguages()
        languages = New List(Of Translation)

        Dim result As String = AdvancedWebClient.downloadstring(serverinteraction.translations_list)
        If result Is Nothing OrElse result = "" Then
            result = AdvancedWebClient.downloadstring(serverinteraction.translations_list)
            If result Is Nothing OrElse result = "" Then Exit Sub
        End If

        Dim langxml As New fxml
        langxml.Owner = "LanguageInstaller"
        langxml.LoadXML(result)
        For Each xmle As Xml.XmlElement In langxml.GetElementsByName("language")
            Try
                Dim t As New Translation
                t.language = xmle.GetAttribute("name")
                t.translator = xmle.GetElementsByTagName("translator")(0).InnerText
                t.comment = xmle.GetElementsByTagName("comment")(0).InnerText
                t.version = xmle.GetElementsByTagName("version")(0).InnerText
                t.url = xmle.GetElementsByTagName("url")(0).InnerText
                languages.Add(t)
            Catch ex As Exception
                livebug.write(loggingLevel.Severe, "LanguageInstaller", "Could not parse translation info", ex.Message)
            End Try
        Next

        RaiseEvent LanguagesLoaded()
    End Sub

    Private Sub BtnInstall_Click(sender As System.Object, e As System.EventArgs) Handles BtnInstall.Click
        If ALVLanguages.SelectedItems.Count < 1 Then Exit Sub

        Dim language As String = ALVLanguages.SelectedItems(0).SubItems(0).Text
        livebug.write(loggingLevel.Fine, "LanguageInstaller", "Installing language: " & language)
        Dim t As Translation = Nothing
        For Each lang As Translation In languages
            If lang.language = language Then t = lang
        Next

        If t Is Nothing Then Exit Sub

        livebug.write(loggingLevel.Fine, "LanguageInstaller", "Downloading language: " & t.url)

        Dim _
            fd As _
                New FileDownloader(t.url, common.Localization_path & "/" & t.language & ".xml",
                                   lr("Installing language:") & " " & t.language)
        fd.ShowDialog()

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class

Class Translation
    Public language As String, translator As String, version As String, url As String, comment As String
End Class