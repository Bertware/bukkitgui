Imports System.ComponentModel

''' <summary>
''' Slightly modificated RichttextBox class. Adds for example colored output and autoscroll
''' </summary>
''' <remarks></remarks>
Public Class AdvancedRichTextBox
    Inherits RichTextBox

    Const def_autoscroll As Boolean = True

    Private _Autoscroll As Boolean = def_autoscroll

    <DefaultValue(def_autoscroll)> _
    Public Property AutoScrollDown As Boolean
        Get
            Return _Autoscroll
        End Get
        Set(value As Boolean)
            _Autoscroll = value
        End Set
    End Property

    Public Overloads Sub AppendText(text As String)
        If Not text.EndsWith(vbCrLf) Then text += vbCrLf

        Me.SelectionStart = Me.TextLength 'make sure cursor is at end of the text
        'Setting the correct color
        Me.SelectionColor = Color.Black
        Me.SelectedText = text

        ScrollDown()

    End Sub

    Public Overloads Sub AppendText(text As String, color As Color)
        text = text.Trim().Trim(vbCrLf).Trim(vbCr).Trim(Environment.NewLine).Trim("\r\n").Trim
        If text Is Nothing OrElse text = "" Then Exit Sub
        If Not text.EndsWith(vbCrLf) Then text += vbCrLf

        Me.SelectionStart = Me.TextLength 'make sure cursor is at end of the text
        'Setting the correct color
        Me.SelectionColor = color
        Me.SelectedText = text

        ScrollDown()
    End Sub

    Public Sub NewLine()
        Dim str As String = vbCrLf
        Me.SelectionStart = Me.TextLength 'make sure cursor is at end of the text
        'Setting the correct color
        Me.SelectionColor = Color.Black
        Me.SelectedText = str

        ScrollDown()
    End Sub

    Public Sub ScrollToBottom()
        With (Me) 'auto scroll down
            .Select(Me.TextLength, 0)
            .ScrollToCaret()
        End With
    End Sub

    Private Sub ScrollDown() Handles MyBase.TextChanged
        If _Autoscroll Then
            With (Me) 'auto scroll down
                .Select(Me.TextLength, 0)
                .ScrollToCaret()
            End With
        End If
    End Sub

End Class
