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


Imports Net.Bertware.BukkitGUI.Core
Imports Net.Bertware.BukkitGUI.TaskManager
Imports Net.Bertware.BukkitGUI.Utilities

Public Class TaskDialog
    Public task As task = Nothing
    Private _existing As Boolean = False

    'server start;server stop;elapsed time;current time;player join;player leave;server empty, server crash
    Private _
        trigger_help() As tsi =
            {New tsi(lr("Run this task when the server starts."), lr("No parameters required."), False) _
             , New tsi(lr("Run this task before the server starts."), lr("No parameters required."), False) _
             , New tsi(lr("Run this task when the server stops."), lr("No parameters required."), False) _
             , New tsi(lr("Run this task before the server stops."), lr("No parameters required."), False) _
             ,
             New tsi(lr("Run this task every hh:mm:ss while the server is running."), lr("The time in hh:mm:ss"), True) _
             ,
             New tsi(lr("Run this task when the computer time is equal to hh:mm:ss."),
                     lr("The time in hh:mm:ss. You can add multiple times, each in hh:mm:ss format, split by ;"), True) _
             ,
             New tsi(lr("Run this task every time a player joins the server."),
                     lr("Optional: the player name(s) to trigger at, split by;"), True) _
             ,
             New tsi(lr("Run this task every time a player disconnects."),
                     lr("Optional: the player name(s) to trigger at, split by ;"), True) _
             ,
             New tsi(lr("Run this task when a player disconnects and there are no longer players online."),
                     lr("No parameters required."), False) _
             ,
             New tsi(
                 lr(
                     "Run this task when the server doesn't respond any longer. This task will send a heartbeat to the server, and check if the server responds"),
                 lr("The amount of time between 2 heartbeats, in hh:mm:ss"), True) _
             ,
             New tsi(lr("Run this task when another task finished. You can set an optional timeout."),
                     lr(
                         "the name of the task. You can add a timeout, by adding the time in /hh:mm:ss format after the task name."),
                     True) _
             ,
             New tsi(
                 lr("Run this task when certain text is received from the server. Warning! this might be CPU intensive!"),
                 lr(
                     "the text that will trigger this. Note: if this text is CONTAINED in the server text, the event will run."),
                 True)}


    'execute;shellexecute;command;stop server;start server;restart server; Brute restart; Synchronize list: backup;
    Private _
        action_help() As tsi =
            {New tsi(lr("Run a program, or a file that is associated with a default program."),
                     lr("the path to the program or file"), True) _
             , New tsi(lr("Execute a CMD command."), lr("the CMD command"), True) _
             ,
             New tsi(lr("Send a command to the server, if the server is running."),
                     lr("the command to send. You can specify multiple commands, separated by ;"), True) _
             , New tsi(lr("Stop the server, if it's running."), lr("No parameters required"), False) _
             ,
             New tsi(lr("Start the server, if it isn't already running. No parameters required."),
                     lr("The server will start with the settings set in the superstart tab."), False) _
             ,
             New tsi(lr("Restart the server, if it's running. No parameters required."),
                     lr("The server will start with the settings set in the superstart tab."), False) _
             ,
             New tsi(
                 lr(
                     "Restart a server that doesn't respond, by killing it and starting it again. Might cause data loss. USE WITH CAUTION."),
                 lr("No parameters required"), False) _
             ,
             New tsi(lr("Create a backup. Enter the name of the backup to execute."), lr("The name of the backup"), True) _
             ,
             New tsi(lr("Sycnhronize the GUI player list with the server player list"), lr("No parameters required"),
                     False) _
             ,
             New tsi(lr("Kick all the players that are online."),
                     lr("Optional: the reason for kicking them. (if you have a plugin that supports kick reasons)"),
                     True) _
             , New tsi(lr("Close the GUI."), lr("No parameters required"), False)
            }


    Private Sub TaskDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If task IsNot Nothing Then
            _existing = True
            ChkEnable.Checked = task.IsEnabled
            If task.name IsNot Nothing Then TxtTaskName.Text = task.name
            CBTriggerType.SelectedIndex = task.trigger_type
            If task.trigger_parameters IsNot Nothing Then TxtTriggerParam.Text = task.trigger_parameters
            CBActionType.SelectedIndex = task.action_type
            If task.action_parameters IsNot Nothing Then TxtActionParam.Text = task.action_parameters
            validate_name()
            validate_triggerparameter()
            validate_actionparameter()
        End If
    End Sub

    Private Function validate_triggerparameter() As Boolean
        BtnSave.Enabled = False
        If CBTriggerType.SelectedItem Is Nothing Then Return False : Exit Function
        Select Case CBTriggerType.SelectedIndex
            Case task.trigger.current_time
                If TxtTriggerParam.Text.Contains(";") Then
                    For Each time As String In TxtTriggerParam.Text.Split(";")
                        If time <> "" AndAlso time.Trim(";") <> "" Then
                            If time.Trim(";").Length <> 8 Then _
                                ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : _
                                    Return False : Exit Function
                            If time.Trim(";")(2) <> ":" Or time.Trim(";")(5) <> ":" Then _
                                ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : _
                                    Return False : Exit Function
                            If _
                                Char.IsNumber(time.Trim(";")(0)) = False Or Char.IsNumber(time.Trim(";")(1)) = False Or
                                Char.IsNumber(time.Trim(";")(3)) = False Or
                                Char.IsNumber(time.Trim(";")(4)) = False Or Char.IsNumber(time.Trim(";")(6)) = False Or
                                Char.IsNumber(time.Trim(";")(7)) = False Then _
                                ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : _
                                    Return False : Exit Function
                        End If
                    Next
                Else
                    If TxtTriggerParam.Text.Length <> 8 Then _
                        ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                            Exit Function
                    If TxtTriggerParam.Text(2) <> ":" Or TxtTriggerParam.Text(5) <> ":" Then _
                        ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                            Exit Function
                    If _
                        Char.IsNumber(TxtTriggerParam.Text(0)) = False Or Char.IsNumber(TxtTriggerParam.Text(1)) = False Or
                        Char.IsNumber(TxtTriggerParam.Text(3)) = False Or
                        Char.IsNumber(TxtTriggerParam.Text(4)) = False Or Char.IsNumber(TxtTriggerParam.Text(6)) = False Or
                        Char.IsNumber(TxtTriggerParam.Text(7)) = False Then _
                        ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                            Exit Function
                End If
            Case task.trigger.elapsed_time
                If TxtTriggerParam.Text.Length <> 8 Then _
                    ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                        Exit Function
                If TxtTriggerParam.Text(2) <> ":" Or TxtTriggerParam.Text(5) <> ":" Then _
                    ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                        Exit Function
                If _
                    Char.IsNumber(TxtTriggerParam.Text(0)) = False Or Char.IsNumber(TxtTriggerParam.Text(1)) = False Or
                    Char.IsNumber(TxtTriggerParam.Text(3)) = False Or
                    Char.IsNumber(TxtTriggerParam.Text(4)) = False Or Char.IsNumber(TxtTriggerParam.Text(6)) = False Or
                    Char.IsNumber(TxtTriggerParam.Text(7)) = False Then _
                    ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                        Exit Function

            Case task.trigger.heartbeat_fail
                If TxtTriggerParam.Text.Length <> 8 Then _
                    ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                        Exit Function
                If TxtTriggerParam.Text(2) <> ":" Or TxtTriggerParam.Text(5) <> ":" Then _
                    ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                        Exit Function
                If _
                    Char.IsNumber(TxtTriggerParam.Text(0)) = False Or Char.IsNumber(TxtTriggerParam.Text(1)) = False Or
                    Char.IsNumber(TxtTriggerParam.Text(3)) = False Or
                    Char.IsNumber(TxtTriggerParam.Text(4)) = False Or Char.IsNumber(TxtTriggerParam.Text(6)) = False Or
                    Char.IsNumber(TxtTriggerParam.Text(7)) = False Then _
                    ErrProv.SetError(TxtTriggerParam, lr("Time should be in format hh:mm:ss")) : Return False : _
                        Exit Function

        End Select
        ErrProv.SetError(TxtTriggerParam, "")
        BtnSave.Enabled = True
        Return True
    End Function

    Private Function validate_actionparameter() As Boolean
        BtnSave.Enabled = False
        If CBActionType.SelectedItem Is Nothing Then Return False : Exit Function
        Select Case CBActionType.SelectedIndex
            Case task.action.command
                If TxtActionParam.Text = "" Then _
                    ErrProv.SetError(TxtActionParam, lr("You need to enter a command")) : Return False : Exit Function
            Case task.action.execute
                If TxtActionParam.Text = "" Then _
                    ErrProv.SetError(TxtActionParam, lr("You need to enter a file or program")) : Return False : _
                        Exit Function
            Case task.action.shellexecute
                If TxtActionParam.Text = "" Then _
                    ErrProv.SetError(TxtActionParam, lr("You need to enter a command")) : Return False : Exit Function
            Case task.action.backup
                If TxtActionParam.Text = "" Then _
                    ErrProv.SetError(TxtActionParam, lr("You need to enter a backup name")) : Return False : _
                        Exit Function
                If GetBackupByName(TxtActionParam.Text) Is Nothing Then _
                    ErrProv.SetError(TxtActionParam, lr("You need to enter a valid backup name")) : Return False : _
                        Exit Function
        End Select
        ErrProv.SetError(TxtActionParam, "")
        BtnSave.Enabled = True
        Return True
    End Function

    Private Function validate_name() As Boolean
        BtnSave.Enabled = False
        If TxtTaskName.Text = "" Then _
            ErrProv.SetError(TxtTaskName, lr("The name must be at least 1 character long")) : Return False : _
                Exit Function
        For Each character In TxtTaskName.Text.ToCharArray
            If Not (Char.IsLetterOrDigit(character) Or character = Char.Parse("_") Or character = Char.Parse("-")) Then _
                ErrProv.SetError(TxtTaskName, lr("You can only use the following characters: a-z ; 1-9 ; _ ; -")) : _
                    Return False : Exit Function
        Next
        BtnSave.Enabled = True
        ErrProv.SetError(TxtTaskName, "")
        Return True
    End Function

    Private Sub CBTriggerType_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles CBTriggerType.SelectedIndexChanged
        lblTriggerHelp.Text = lr("Explanation") & ": " & trigger_help(CBTriggerType.SelectedIndex).explanation & vbCrLf &
                              lr("Parameters") & ": " & trigger_help(CBTriggerType.SelectedIndex).parameters
        If trigger_help(CBTriggerType.SelectedIndex).has_parameters = False Then _
            TxtTriggerParam.Enabled = False : TxtTriggerParam.Text = "" Else TxtTriggerParam.Enabled = True
        validate_triggerparameter()
    End Sub

    Private Sub CBActionType_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles CBActionType.SelectedIndexChanged
        lblActionHelp.Text = lr("Explanation") & ": " & action_help(CBActionType.SelectedIndex).explanation & vbCrLf &
                             lr("Parameters") & ": " & action_help(CBActionType.SelectedIndex).parameters
        If action_help(CBActionType.SelectedIndex).has_parameters = False Then _
            TxtActionParam.Enabled = False : TxtActionParam.Text = "" Else TxtActionParam.Enabled = True
        validate_actionparameter()
    End Sub

    Private Sub TxtTriggerParam_TextChanged(sender As Object, e As EventArgs) _
        Handles TxtTriggerParam.TextChanged, TxtTriggerParam.GotFocus, TxtTriggerParam.LostFocus
        validate_triggerparameter()
    End Sub

    Private Sub TxtActionParam_TextChanged(sender As Object, e As EventArgs) _
        Handles TxtActionParam.TextChanged, TxtActionParam.GotFocus, TxtActionParam.LostFocus
        validate_actionparameter()
    End Sub

    Private Sub TxtTaskName_TextChanged(sender As Object, e As EventArgs) _
        Handles TxtTaskName.TextChanged, TxtTaskName.GotFocus, TxtTaskName.LostFocus
        validate_name()
    End Sub


    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Log(loggingLevel.Fine, "TaskDialog", "Saving task...")

        If Not (validate_name() And validate_triggerparameter() And validate_actionparameter()) Then _
            Log(loggingLevel.Fine, "TaskDialog", "Saving task... Cancelling... Invalid settings") _
                : _
                MessageBox.Show(lr("You didn't enter valid values. Make sure all values are valid, and try again"),
                                lr("Save cancelled"), MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub

        Dim t As New task
        With t
            .name = TxtTaskName.Text
            .trigger_parameters = TxtTriggerParam.Text
            .action_parameters = TxtActionParam.Text
            .trigger_type = CBTriggerType.SelectedIndex
            .action_type = CBActionType.SelectedIndex

            .canEnable = ChkEnable.Checked
        End With
        Log(loggingLevel.Fine, "TaskDialog", "Saving task... Task created")

        If _existing Then
            Log(loggingLevel.Fine, "TaskDialog", "Updating task...")
            saveTask(task, t)
            Log(loggingLevel.Fine, "TaskDialog", "Updated task...")
        Else
            Log(loggingLevel.Fine, "TaskDialog", "Adding task...")
            addTask(t, t.canEnable)
            Log(loggingLevel.Fine, "TaskDialog", "Added task...")
        End If

        task = t
        Log(loggingLevel.Fine, "TaskDialog", "Saving task... Completed")

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class

Class tsi 'TaskSettingsInfo
    Public explanation As String = ""
    Public parameters As String = ""
    Public has_parameters As Boolean = True

    Public Sub New(explanation As String, parameters As String, has_parameters As String)
        Me.explanation = explanation
        Me.parameters = parameters
        Me.has_parameters = has_parameters
    End Sub
End Class