using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace CanvasZoomPanDragLib.Models
{
	public abstract class DrawingBase : IDrawing
	{
		public List<Shape> ShapesOnCanvas { get; set; } = new List<Shape>();

		public CanvasManager Parrent { get; set; }
		public Point3D PointDecart { get; set; }

		public DrawingBase(Point3D pDecart, CanvasManager canvasManager)
		{
			OnBeforeInit();
			PointDecart = pDecart;
			Parrent = canvasManager;
			ShapesOnCanvas = CreateShapesOnCanvas();
		}
		protected abstract List<Shape> CreateShapesOnCanvas();
		protected virtual void OnBeforeInit()
		{
		}
	}
}
