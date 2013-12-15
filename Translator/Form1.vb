Imports System.Xml

Public Class Form1
    Dim lang_xml As New fxml
    Const CURR_VER As String = "1.0"

    Private Sub BtnBrowse_Click(sender As System.Object, e As System.EventArgs) Handles BtnBrowse.Click
        Dim ofd As New OpenFileDialog
        ofd.Title = "Open log file..."
        ofd.Multiselect = False
        ofd.ShowDialog()
        TxtPath.Text = ofd.FileName

        lang_xml = New fxml(TxtPath.Text)
        ALVItems.Items.Clear()
        For Each xmle As XmlElement In lang_xml.GetElementsByName("text")
            ALVItems.Items.Add(New ListViewItem({ALVItems.Items.Count, xmle.GetAttribute("string"), System.Web.HttpUtility.UrlDecode(xmle.InnerText)}))
        Next
    End Sub

    Private Sub ALVItems_DoubleClick(sender As System.Object, e As System.EventArgs) Handles ALVItems.DoubleClick
        If ALVItems.SelectedItems Is Nothing OrElse ALVItems.SelectedItems.Count < 1 Then Exit Sub
        Dim sd As New TranslateDialog
        sd.Original = ALVItems.SelectedItems(0).SubItems(1).Text
        sd.Translation = ALVItems.SelectedItems(0).SubItems(2).Text
        If sd.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        ALVItems.SelectedItems(0).SubItems(2).Text = sd.Translation
        Dim el As XmlElement = CType(lang_xml.getElementByAttribute("text", "string", sd.Original), XmlElement)
        el.InnerText = sd.Translation
        lang_xml.save()
    End Sub

End Class
