'This class inherits textbox
'It adds more functionality, such as an "Enter" event, when the enter key is pressed, and history with up/down keys
'For example used as input textbox

Public Class AdvancedTextBox
    Inherits TextBox

    Public Event HistoryLevelDown(level As UInt16, text As String)
    Public Event HistoryLevelUp(level As UInt16, text As String)
    Public Event KeyPressEnter(text As String)
    Public Event KeyPressEscape(text As String)

    Private history As New TextBoxHistory
    Private auto_history As Boolean = False

    Public Property AutoHistory As Boolean
        Get
            Return auto_history
        End Get
        Set(value As Boolean)
            auto_history = value
        End Set
    End Property

    Private Sub KeyHandle(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        e.Handled = True
        If e.KeyCode = Keys.Down Then
            Dim ntext = history.LevelDown
            RaiseEvent HistoryLevelDown(history.level, ntext)
            Me.Text = ntext
            Me.Select(Me.TextLength, 0)
        ElseIf e.KeyCode = Keys.Up Then
            Dim ntext = history.LevelUp
            RaiseEvent HistoryLevelUp(history.level, ntext)
            Me.Text = ntext
            Me.Select(Me.TextLength, 0)
        ElseIf e.KeyCode = Keys.Enter And Me.Text <> "" Then
            Me.Text = Me.Text
            RaiseEvent KeyPressEnter(Me.Text)
            If auto_history Then AddToHistory(Me.Text)
            Me.Clear()
        ElseIf e.KeyCode = Keys.Escape Then
            RaiseEvent KeyPressEscape(Me.Text)
            Me.Clear()
        End If
        e.Handled = True
    End Sub

    ''' <summary>
    ''' Add an entry to the history of this textbox
    ''' </summary>
    ''' <param name="text">The text to be added</param>
    ''' <returns>The total history</returns>
    ''' <remarks></remarks>
    Public Function AddToHistory(text As String) As TextBoxHistory
        history.Add(text)
        Return history
    End Function
End Class

Public Class TextBoxHistory
    Dim Hlist As New List(Of String)
    Dim current_level As UInt16 = 0

    Private DidDown As Boolean = False
    Private DidUp As Boolean = False

    Public Property level As UInt16
        Get
            Return current_level
        End Get
        Set(value As UInt16)
            current_level = value
        End Set
    End Property

    Public Sub New()
        Hlist = New List(Of String)
        current_level = 0
    End Sub

    ''' <summary>
    ''' Add an entry
    ''' </summary>
    ''' <param name="text">The text content for this entry</param>
    ''' <remarks></remarks>
    Public Sub Add(text As String)
        Hlist.Add(text)
    End Sub

    ''' <summary>
    ''' Get the current history
    ''' </summary>
    ''' <returns>A list (of string) with the current history entries</returns>
    ''' <remarks></remarks>
    Public Function Read() As List(Of String)
        Return Hlist
    End Function

    ''' <summary>
    ''' Go up a level in the history
    ''' </summary>
    ''' <returns>The entry at this level</returns>
    ''' <remarks></remarks>
    Public Function LevelUp() As String
        Try
            If current_level > 0 And DidUp Then current_level = current_level - 1
            DidUp = True
            DidDown = False
            Return Hlist(current_level)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Go down a level in the history
    ''' </summary>
    ''' <returns>The entry at this level</returns>
    ''' <remarks></remarks>
    Public Function LevelDown() As String
        Try
            If (current_level + 1) < Hlist.Count And DidDown Then current_level = current_level + 1
            DidDown = True
            DidUp = False
            Return Hlist(current_level)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Go to the given level in the history
    ''' </summary>
    ''' <param name="i">The level to read</param>
    ''' <returns>The entry at this level</returns>
    ''' <remarks></remarks>
    Public Function GoToLevel(i As UInt16) As String
        Try
            If Hlist(i) IsNot Nothing Then current_level = i
            Return Hlist(current_level)
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
