Imports Net.Bertware.BukkitGUI.Core


Namespace Utilities
    Module LicenseManager
        Const EULA_URL As String = "http://legal.bertware.net/bukkitgui/eula.html"
        Const PRIVACY_URL As String = "http://legal.bertware.net/bukkitgui/privacy.html"

        ''' <summary>
        ''' Initialize the config file
        ''' </summary>
        ''' <returns>True on success</returns>
        ''' <remarks>Must be done before anything else</remarks>
        Public Function init() As Boolean
            If config.readAsBool("license_eula_accept", False, "") = False Then _
                ShowEULA(True) : config.writeAsBool("license_eula_accept", True, "")
            If config.readAsBool("license_privacy_accept", False, "") = False Then _
                ShowPrivacy(True) : config.writeAsBool("license_privacy_accept", True, "")
            Return True
        End Function

        Private Function ShowEULA(MustAccept As Boolean) As Boolean
            Dim lad As New LicenseAcceptDialog("End User License Agreement", EULA_URL)
            While lad.DialogResult <> DialogResult.OK
                lad = New LicenseAcceptDialog("End User License Agreement", EULA_URL)
                lad.ShowDialog()
                If lad.DialogResult = DialogResult.OK Then Exit While
                If _
                    MessageBox.Show(lr("You have to accept in order to use the application. Do you want to exit?"),
                                    lr("Exit?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes _
                    Then
                    livebug.write(loggingLevel.Fine, "LicenseManager", "User doesn't accept EULA. Exiting program")
                    Process.GetCurrentProcess.Kill()
                End If
            End While
            Return True
        End Function

        Private Function ShowPrivacy(MustAccept As Boolean) As Boolean
            Dim lad As New LicenseAcceptDialog("Privacy Policy", PRIVACY_URL)
            While lad.DialogResult <> DialogResult.OK
                lad = New LicenseAcceptDialog("Privacy Policy", PRIVACY_URL)
                lad.ShowDialog()
                If lad.DialogResult = DialogResult.OK Then Exit While
                If _
                    MessageBox.Show(lr("You have to accept in order to use the application. Do you want to exit?"),
                                    lr("Exit?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes _
                    Then
                    livebug.write(loggingLevel.Fine, "LicenseManager",
                                  "User doesn't accept privacy policy. Exiting program")
                    Process.GetCurrentProcess.Kill()
                End If
            End While
            Return True
        End Function
    End Module
End Namespace