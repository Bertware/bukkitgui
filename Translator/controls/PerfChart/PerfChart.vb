Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class PerfChart
    Inherits UserControl
    Private _averageValue As Decimal

    Private _b3dstyle As Border3DStyle

    Private _components As IContainer

    Private _currentMaxValue As Decimal

    Private _drawValues As List(Of Decimal)

    Private Const GRID_SPACING As Integer = 16

    Private _gridScrollOffset As Integer

    Private Const MAX_VALUE_COUNT As Integer = 512

    Private _perfChartStyle As PerfChartStyle

    Private _scaleMode As CScaleMode

    Private _timerMode As CTimerMode

    Private WithEvents _tmrRefresh As Timer

    Private _valueSpacing As Integer

    Private _visibleValues As Integer

    Private _waitingValues As Queue(Of Decimal)

    Private _maximum As Integer = 100

    Public Enum CScaleMode
        Absolute
        Relative
    End Enum

    Public Enum CTimerMode
        Disabled
        Simple
        SynchronizedAverage
        SynchronizedSum
    End Enum


    <Category("Appearance")> _
    <DefaultValue(GetType(Border3DStyle), "Sunken")> _
    <Description("BorderStyle")> _
    Public Overloads Property BorderStyle As Border3DStyle
        Get
            Return Me._b3dstyle
        End Get
        Set(ByVal value As Border3DStyle)
            Me._b3dstyle = value
            MyBase.Invalidate()
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    <Category("Appearance")> _
    <Description("Appearance and Style")> _
    Public Property PerfChartStyle As PerfChartStyle
        Get
            Return Me._perfChartStyle
        End Get
        Set(ByVal value As PerfChartStyle)
            Me._perfChartStyle = value
        End Set
    End Property

    Public Property maximum As Integer
        Get
            Return _maximum
        End Get
        Set(value As Integer)
            _maximum = value
        End Set
    End Property
    Public Property ScaleMode As CScaleMode
        Get
            Return Me._scaleMode
        End Get
        Set(ByVal value As CScaleMode)
            Me._scaleMode = value
        End Set
    End Property

    Public Property TimerInterval As Integer
        Get
            Return Me._tmrRefresh.Interval()
        End Get
        Set(ByVal value As Integer)
            If (value < 15) Then
                Throw New ArgumentOutOfRangeException("TimerInterval", value, "The Timer interval must be greater then 15")
            End If
            Me._tmrRefresh.Interval = value
        End Set
    End Property

    Public Property TimerMode As CTimerMode
        Get
            Return Me._timerMode
        End Get
        Set(ByVal value As CTimerMode)
            If (value <> CTimerMode.Disabled OrElse Me._timerMode <> CTimerMode.Disabled) Then
                Me._timerMode = value
                Me._tmrRefresh.Stop()
                Me.ChartAppendFromQueue()
                Return
            Else
                Me._timerMode = value
                Me._tmrRefresh.Start()
            End If
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me._valueSpacing = 5
        Me._b3dstyle = Border3DStyle.Sunken
        Me._drawValues = New List(Of Decimal)(512)
        Me._waitingValues = New Queue(Of Decimal)()
        Me.InitializeComponent()
        Me.PerfChartStyle = New PerfChartStyle()
        MyBase.SetStyle(ControlStyles.UserPaint, True)
        MyBase.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        MyBase.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        MyBase.SetStyle(ControlStyles.ResizeRedraw, True)
        MyBase.Font = SystemInformation.MenuFont
    End Sub

    Public Sub AddValue(ByVal value As Decimal)
        If (Me._scaleMode = CScaleMode.Absolute AndAlso value > New Decimal(100)) Then
            Throw New Exception(String.Format("Values greater then 100 not allowed in ScaleMode: Absolute ({0})", value))
        End If
        Select Case Me._timerMode
            Case CTimerMode.Disabled
                Me.ChartAppend(value)
                Me.Invalidate()
                Return
            Case CTimerMode.SynchronizedSum
                Me.AddValueToQueue(value)
                Return
        End Select
        Throw New Exception(String.Format("Unsupported TimerMode: {0}", Me._timerMode))
    End Sub

    Private Sub AddValueToQueue(ByVal value As Decimal)
        Me._waitingValues.Enqueue(value)
    End Sub

    Private Function CalcVerticalPosition(ByVal value As Decimal) As Integer
        Dim num As Decimal = New Decimal(0)
        If (Me._scaleMode = CScaleMode.Absolute) Then
            num = value * Decimal.op_Implicit(MyBase.Height) / New Decimal(100)
        Else
            If (Me._scaleMode = CScaleMode.Relative) Then
                num = If(Me._currentMaxValue > New Decimal(0), value * Decimal.op_Implicit(MyBase.Height) / Me._currentMaxValue, New Decimal(0))
            End If
        End If
        num = Decimal.op_Implicit(MyBase.Height) - num
        Return Convert.ToInt32(Math.Round(num))
    End Function

    Private Sub ChartAppend(ByVal value As Decimal)
        Me._drawValues.Insert(0, Math.Max(value, New Decimal(0)))
        If (Me._drawValues.Count > 512) Then
            Me._drawValues.RemoveAt(512)
        End If
        Me._gridScrollOffset = Me._gridScrollOffset + Me._valueSpacing
        If (Me._gridScrollOffset > 16) Then
            Me._gridScrollOffset = Me._gridScrollOffset Mod 16
        End If
    End Sub

    Private Sub ChartAppendFromQueue()
        If (Me._waitingValues.Count > 0) Then
            If (Me._timerMode = CTimerMode.Simple) Then
                While Me._waitingValues.Count > 0
                    Me.ChartAppend(Me._waitingValues.Dequeue())
                End While
            Else
                If (Me._timerMode = CTimerMode.SynchronizedAverage OrElse Me._timerMode = CTimerMode.SynchronizedSum) Then
                    Dim num As Decimal = New Decimal(0)
                    Dim count As Integer = Me._waitingValues.Count
                    While Me._waitingValues.Count > 0
                        num = num + Me._waitingValues.Dequeue()
                    End While
                    If (Me._timerMode = CTimerMode.SynchronizedAverage) Then
                        num = num / Decimal.op_Implicit(count)
                    End If
                    Me.ChartAppend(num)
                    Me.ChartAppend(New Decimal(0))
                End If
            End If
        End If
        MyBase.Invalidate()
    End Sub

    Public Sub Clear()
        Me._drawValues.Clear()
        MyBase.Invalidate()
    End Sub

    Private Sub colorSet_ColorSetChanged(ByVal sender As Object, ByVal e As EventArgs)
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso Me._components IsNot Nothing) Then
            Me._components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub DrawAverageLine(ByVal g As Graphics)
        Dim i As Integer = 0
        Do
            Me._averageValue = Me._averageValue + Me._drawValues(i)
            i = i + 1
        Loop While i < Me._visibleValues
        Me._averageValue = Me._averageValue / Decimal.op_Implicit(Me._visibleValues)
        Dim num As Integer = Me.CalcVerticalPosition(Me._averageValue)
        g.DrawLine(Me.PerfChartStyle.AvgLinePen.Pen, 0, num, MyBase.Width, num)
    End Sub

    Private Sub DrawBackgroundAndGrid(ByVal g As Graphics)
        Dim rectangle As Rectangle = New Rectangle(0, 0, MyBase.Width, MyBase.Height)
        Using linearGradientBrush As Brush = New LinearGradientBrush(rectangle, Me._perfChartStyle.BackgroundColorTop, Me._perfChartStyle.BackgroundColorBottom, LinearGradientMode.Vertical)
            g.FillRectangle(linearGradientBrush, rectangle)
        End Using
        If (Me.PerfChartStyle.ShowVerticalGridLines) Then
            For i As Integer = MyBase.Width - Me._gridScrollOffset To 0 Step -16
                g.DrawLine(Me.PerfChartStyle.VerticalGridPen.Pen, i, 0, i, MyBase.Height)
            Next
        End If
        If (Me.PerfChartStyle.ShowHorizontalGridLines) Then
            Dim j As Integer = 0
            Do
                g.DrawLine(Me.PerfChartStyle.HorizontalGridPen.Pen, 0, j, MyBase.Width, j)
                j = j + 16
            Loop While j < MyBase.Height
        End If
    End Sub

    Private Sub DrawChart(ByVal g As Graphics)
        Me._visibleValues = Math.Min(MyBase.Width / Me._valueSpacing, Me._drawValues.Count)
        If (Me._scaleMode = CScaleMode.Relative) Then
            Me._currentMaxValue = Me.GetHighestValueForRelativeMode()
        End If
        Dim point As Point = New Point(MyBase.Width + Me._valueSpacing, MyBase.Height)
        Dim x As Point = New Point()
        If (Me._visibleValues > 0 AndAlso Me.PerfChartStyle.ShowAverageLine) Then
            Me._averageValue = New Decimal(0)
            Me.DrawAverageLine(g)
        End If
        Dim i As Integer = 0
        Do
            x.X = point.X - Me._valueSpacing
            Try
                x.Y = Me.CalcVerticalPosition(Me._drawValues(i))
            Catch ex As Exception
            End Try
            g.DrawLine(Me.PerfChartStyle.ChartLinePen.Pen, point, x)
            point = x
            i = i + 1
        Loop While i < Me._visibleValues
        If (Me._scaleMode = CScaleMode.Relative) Then
            Dim solidBrush As SolidBrush = New SolidBrush(Me.PerfChartStyle.ChartLinePen.Color)
            g.DrawString(Me._currentMaxValue.ToString(), MyBase.Font, solidBrush, 4, 2)
        End If
        ControlPaint.DrawBorder3D(g, 0, 0, MyBase.Width, MyBase.Height, Me._b3dstyle)
    End Sub

    Private Function GetHighestValueForRelativeMode() As Decimal
        Dim num As Decimal = New Decimal(0)
        Dim i As Integer = 0
        Do
            If (Me._drawValues(i) > num) Then
                num = Me._drawValues(i)
            End If
            i = i + 1
        Loop While i < Me._visibleValues
        Return num
    End Function

    Private Sub InitializeComponent()
        Me._components = New Container()
        MyBase.SuspendLayout()
        MyBase.AutoScaleDimensions = New SizeF(6, 13)
        MyBase.AutoScaleMode = AutoScaleMode.Font
        MyBase.Name = "PerfChart"
        MyBase.Size = New Size(235, 87)
        MyBase.ResumeLayout(False)
        Me._tmrRefresh = New Timer(Me._components)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Me.PerfChartStyle.AntiAliasing = True Then
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        End If
        Me.DrawBackgroundAndGrid(e.Graphics)
        Me.DrawChart(e.Graphics)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        MyBase.Invalidate()
    End Sub

    Private Sub tmrRefresh_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles _tmrRefresh.Tick
        If (MyBase.DesignMode) Then
            Return
        End If
        Me.ChartAppendFromQueue()
    End Sub
End Class

