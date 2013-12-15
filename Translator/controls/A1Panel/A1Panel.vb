Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace Owf.Controls
    Partial Public Class A1Panel
        Inherits Panel
        Private _borderWidth As Integer = 1
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue(1)> _
        Public Property BorderWidth() As Integer
            Get
                Return _borderWidth
            End Get
            Set(value As Integer)
                _borderWidth = value
                Invalidate()
            End Set
        End Property

        Private _shadowOffSet As Integer = 5
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue(5)> _
        Public Property ShadowOffSet() As Integer
            Get
                Return _shadowOffSet
            End Get
            Set(value As Integer)
                _shadowOffSet = Math.Abs(value)
                Invalidate()
            End Set
        End Property

        Private _roundCornerRadius As Integer = 4
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue(4)> _
        Public Property RoundCornerRadius() As Integer
            Get
                Return _roundCornerRadius
            End Get
            Set(value As Integer)
                _roundCornerRadius = Math.Abs(value)
                Invalidate()
            End Set
        End Property

        Private _image As Image
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        Public Property Image() As Image
            Get
                Return _image
            End Get
            Set(value As Image)
                _image = value
                Invalidate()
            End Set
        End Property

        Private _imageLocation As New Point(4, 4)
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue("4,4")> _
        Public Property ImageLocation() As Point
            Get
                Return _imageLocation
            End Get
            Set(value As Point)
                _imageLocation = value
                Invalidate()
            End Set
        End Property

        Private _borderColor As Color = Color.Gray
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue("Color.Gray")> _
        Public Property BorderColor() As Color
            Get
                Return _borderColor
            End Get
            Set(value As Color)
                _borderColor = value
                Invalidate()
            End Set
        End Property

        Private _gradientStartColor As Color = Color.White
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue("Color.White")> _
        Public Property GradientStartColor() As Color
            Get
                Return _gradientStartColor
            End Get
            Set(value As Color)
                _gradientStartColor = value
                Invalidate()
            End Set
        End Property

        Private _gradientEndColor As Color = Color.Gray
        <Browsable(True), Category(A1PanelGlobals.A1Category)> _
        <DefaultValue("Color.Gray")> _
        Public Property GradientEndColor() As Color
            Get
                Return _gradientEndColor
            End Get
            Set(value As Color)
                _gradientEndColor = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            Me.SetStyle(ControlStyles.DoubleBuffer, True)
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(ControlStyles.ResizeRedraw, True)
            Me.SetStyle(ControlStyles.UserPaint, True)
            Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            InitializeComponent()
        End Sub

        Protected Overloads Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)

            Dim tmpShadowOffSet As Integer = Math.Min(Math.Min(_shadowOffSet, Me.Width - 2), Me.Height - 2)
            Dim tmpSoundCornerRadius As Integer = Math.Min(Math.Min(_roundCornerRadius, Me.Width - 2), Me.Height - 2)
            If Me.Width > 1 AndAlso Me.Height > 1 Then
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias

                Dim rect As New Rectangle(0, 0, Me.Width - tmpShadowOffSet - 1, Me.Height - tmpShadowOffSet - 1)
                Dim rectShadow As New Rectangle(tmpShadowOffSet, tmpShadowOffSet, Me.Width - tmpShadowOffSet - 1, Me.Height - tmpShadowOffSet - 1)

                Dim graphPathShadow As GraphicsPath = A1PanelGraphics.GetRoundPath(rectShadow, tmpSoundCornerRadius)
                Dim graphPath As GraphicsPath = A1PanelGraphics.GetRoundPath(rect, tmpSoundCornerRadius)

                If tmpSoundCornerRadius > 0 Then
                    Using gBrush As New PathGradientBrush(graphPathShadow)
                        gBrush.WrapMode = WrapMode.Clamp
                        Dim colorBlend As New ColorBlend(3)
                        colorBlend.Colors = New Color() {Color.Transparent, Color.FromArgb(180, Color.DimGray), Color.FromArgb(180, Color.DimGray)}

                        colorBlend.Positions = New Single() {0.0F, 0.1F, 1.0F}

                        gBrush.InterpolationColors = colorBlend
                        e.Graphics.FillPath(gBrush, graphPathShadow)
                    End Using
                End If

                ' Draw backgroup
                Dim brush As New LinearGradientBrush(rect, Me._gradientStartColor, Me._gradientEndColor, LinearGradientMode.BackwardDiagonal)
                e.Graphics.FillPath(brush, graphPath)
                e.Graphics.DrawPath(New Pen(Color.FromArgb(180, Me._borderColor), _borderWidth), graphPath)

                ' Draw Image
                If _image IsNot Nothing Then
                    e.Graphics.DrawImageUnscaled(_image, _imageLocation)
                End If
            End If
        End Sub
    End Class
End Namespace

