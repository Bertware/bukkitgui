Imports System.ComponentModel

<TypeConverter(GetType(ExpandableObjectConverter))> _
Public Class PerfChartStyle
    Private _antiAliasing As Boolean

    Private _avgLinePen As ChartPen

    Private _backgroundColorBottom As Color

    Private _backgroundColorTop As Color

    Private _chartLinePen As ChartPen

    Private _horizontalGridPen As ChartPen

    Private _showAverageLine As Boolean

    Private _showHorizontalGridLines As Boolean

    Private _showVerticalGridLines As Boolean

    Private _verticalGridPen As ChartPen

    Public Property AntiAliasing As Boolean
        Get
            Return Me._antiAliasing
        End Get
        Set(ByVal value As Boolean)
            Me._antiAliasing = value
        End Set
    End Property

    Public Property AvgLinePen As ChartPen
        Get
            Return Me._avgLinePen
        End Get
        Set(ByVal value As ChartPen)
            Me._avgLinePen = value
        End Set
    End Property

    Public Property BackgroundColorBottom As Color
        Get
            Return Me._backgroundColorBottom
        End Get
        Set(ByVal value As Color)
            Me._backgroundColorBottom = value
        End Set
    End Property

    Public Property BackgroundColorTop As Color
        Get
            Return Me._backgroundColorTop
        End Get
        Set(ByVal value As Color)
            Me._backgroundColorTop = value
        End Set
    End Property

    Public Property ChartLinePen As ChartPen
        Get
            Return Me._chartLinePen
        End Get
        Set(ByVal value As ChartPen)
            Me._chartLinePen = value
        End Set
    End Property

    Public Property HorizontalGridPen As ChartPen
        Get
            Return Me._horizontalGridPen
        End Get
        Set(ByVal value As ChartPen)
            Me._horizontalGridPen = value
        End Set
    End Property

    Public Property ShowAverageLine As Boolean
        Get
            Return Me._showAverageLine
        End Get
        Set(ByVal value As Boolean)
            Me._showAverageLine = value
        End Set
    End Property

    Public Property ShowHorizontalGridLines As Boolean
        Get
            Return Me._showHorizontalGridLines
        End Get
        Set(ByVal value As Boolean)
            Me._showHorizontalGridLines = value
        End Set
    End Property

    Public Property ShowVerticalGridLines As Boolean
        Get
            Return Me._showVerticalGridLines
        End Get
        Set(ByVal value As Boolean)
            Me._showVerticalGridLines = value
        End Set
    End Property

    Public Property VerticalGridPen As ChartPen
        Get
            Return Me._verticalGridPen
        End Get
        Set(ByVal value As ChartPen)
            Me._verticalGridPen = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me._backgroundColorTop = Color.DarkGreen
        Me._backgroundColorBottom = Color.DarkGreen
        Me._showVerticalGridLines = True
        Me._showHorizontalGridLines = True
        Me._showAverageLine = True
        Me._antiAliasing = True

        Me._verticalGridPen = New ChartPen()
        Me._horizontalGridPen = New ChartPen()
        Me._avgLinePen = New ChartPen()
        Me._chartLinePen = New ChartPen()
    End Sub
End Class

