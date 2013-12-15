Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace Owf.Controls
    Friend Class A1PanelGraphics
        Public Shared Function GetRoundPath(r As Rectangle, depth As Integer) As GraphicsPath
            Dim graphPath As New GraphicsPath()

            graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90)
            graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90)
            graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90)
            graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90)
            graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, CSng(r.Y + depth / 2))

            Return graphPath
        End Function
    End Class
End Namespace
