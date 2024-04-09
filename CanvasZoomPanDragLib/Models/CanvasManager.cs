using CanvasZoomPanDragLib.Interface;
using CanvasZoomPanDragLib.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace CanvasZoomPanDragLib.Models
{
	public class CanvasManager
	{
		public Canvas Canvas { get; protected set; }
		//public ObservableCollection<IElementRevit> AllObjects { get; set; } = new();
		public double Ratio { get; set; } = 0.9;
		public double Scale { get; protected set; }
		public double WidthCanvas { get; protected set; }
		public double HeightCanvas { get; protected set; }
		public double WidthDecart { get; protected set; }
		public double HeightDecart { get; protected set; }
		public Point3D CenterPointCanvas { get; protected set; }
		public Point3D CenterPointDecart { get; protected set; }

		public event EventHandler SelectedChanged = null;

		public CanvasManager(Canvas canvas, Point3D minPointDecart, Point3D maxPointDecart)
		{
			Canvas = canvas;
			WidthCanvas = canvas.ActualWidth;
			HeightCanvas = canvas.ActualHeight;
			WidthDecart = maxPointDecart.X - minPointDecart.X;
			HeightDecart = maxPointDecart.Y - minPointDecart.Y;
			Scale = Math.Max(WidthCanvas, HeightCanvas) * Ratio / Math.Max(WidthDecart, HeightDecart);
			CenterPointDecart = minPointDecart.MidPointWith(maxPointDecart);
			var pMinCanvas = new Point3D();
			var pMaxCanvas = new Point3D(WidthCanvas, HeightCanvas, 0);
			CenterPointCanvas = pMinCanvas.MidPointWith(pMaxCanvas);
		}
		public Point3D TransformDecartToCanvas(Point3D pDecart)
		{
			var x = (pDecart - CenterPointDecart).Dot(ThreeDExt.AxisX) * Scale;
			var y = (pDecart - CenterPointDecart).Dot(ThreeDExt.AxisY) * Scale;
			return new Point3D(x + WidthCanvas / 2, HeightCanvas / 2 - y, 0);
		}
		public Point3D TransformCanvasToDecart(Point3D pCanvas, double z = 0)
		{
			var x = (pCanvas - CenterPointCanvas).Dot(ThreeDExt.CanvasAxisX) / Scale;
			var y = (pCanvas - CenterPointCanvas).Dot(ThreeDExt.CanvasAxisY) / Scale;
			return new Point3D(x + WidthDecart / 2, HeightDecart / 2 - y, 0);
		}

		public void Draw<T>(T drawing) where T : IDrawing
		{
			drawing.ShapesOnCanvas.ForEach(shape =>
			{
				Canvas.Children.Add(shape);
				if (drawing is ISelectableDrawing selectableDrawing)
				{
					shape.MouseEnter += (s, e) => { selectableDrawing.OnMouseEnter(); };
					shape.MouseLeave += (s, e) => { selectableDrawing.OnMouseLeave(); };
					if (drawing is IDraggableDrawing)
					{

					}
				}
			});
		}
	}
}
