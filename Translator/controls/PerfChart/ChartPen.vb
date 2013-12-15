Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<TypeConverter(GetType(ExpandableObjectConverter))> _
Public Class ChartPen
    Private _pen As Pen

    Public Property Color As Color
        Get
            Return Me.Pen.Color
        End Get
        Set(ByVal value As Color)
            Me.Pen.Color = value
        End Set
    End Property

    Public Property DashStyle As DashStyle
        Get
            Return Me._pen.DashStyle
        End Get
        Set(ByVal value As DashStyle)
            Me._pen.DashStyle = value
        End Set
    End Property

    <Browsable(False)> _
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public ReadOnly Property Pen As Pen
        Get
            Return Me._pen
        End Get
    End Property

    Public Property Width As Single
        Get
            Return Me._pen.Width
        End Get
        Set(ByVal value As Single)
            Me._pen.Width = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me._pen = New Pen(Color.Black)
    End Sub
End Class

