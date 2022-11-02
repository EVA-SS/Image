using System.Drawing.Drawing2D;

namespace TSkin
{
    public static class GDIEx
    {
        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius > 0)
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
            }
            else
            {
                var points = new Point[] { new Point(rect.X, rect.Y), new Point(rect.Width + rect.X, rect.Y), new Point(rect.Width + rect.X, rect.Height + rect.Y), new Point(rect.X, rect.Height + rect.Y), new Point(rect.X, rect.Y), };
                path.Reset();
                path.AddPolygon(points);
            }
            return path;
        }
        public static GraphicsPath CreateRoundedRectanglePath(this RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius > 0)
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
            }
            else
            {
                var points = new PointF[] { new PointF(rect.X, rect.Y), new PointF(rect.Width + rect.X, rect.Y), new PointF(rect.Width + rect.X, rect.Height + rect.Y), new PointF(rect.X, rect.Height + rect.Y), new PointF(rect.X, rect.Y), };
                path.Reset();
                path.AddPolygon(points);
            }
            return path;
        }
    }
}