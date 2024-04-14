using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace CanvasZoomPanDragLib.Utils
{
    public static class PointExt
    {
        public static Point ToPoint2D(this Point3D point3D)
        {
            return new Point(point3D.X, point3D.Y);
        }
        public static Point3D ToPoint3D(this Point point, double z = 0)
        {
            return new Point3D(point.X, point.Y, z);
        }
    }
}
