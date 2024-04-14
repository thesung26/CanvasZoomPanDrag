using CanvasZoomPanDragLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace CanvasZoomPanDragLib.Models
{
	public abstract class DraggableDrawingBase : SelectableDrawingBase, IDraggableDrawing
	{
		private Point firstMousePosition = new Point();
		private bool isTranTFChanged = false;
		public TranslateTransform TranslateTransform { get; private set; } = new TranslateTransform();
		protected DraggableDrawingBase(IList<Point3D> psDecart, CanvasManager canvasManager) : base(psDecart, canvasManager)
		{
			ShapesOnCanvas.ForEach(p =>
			{
				p.RenderTransform = TranslateTransform;
				p.MouseLeftButtonDown += P_MouseLeftButtonDown;
				p.MouseLeftButtonUp += P_MouseLeftButtonUp;
				p.MouseMove += P_MouseMove;
			});
		}

		private void P_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var s = e.Source as Shape;
			if (s == null) return;
			s.ReleaseMouseCapture();
			if (isTranTFChanged)
			{
				Dragged?.Invoke(this, EventArgs.Empty);
				isTranTFChanged = false;
			}
		}

		private void P_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var s = e.Source as Shape;
			if (s == null) return;
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (s.IsMouseCaptured)
				{
					IsSelected = true;
					ShapesOnCanvas.ForEach(shape =>
					{
						Point currentPosition = e.GetPosition(CanvasManager.Canvas);
						double deltaX = currentPosition.X - firstMousePosition.X;
						double deltaY = currentPosition.Y - firstMousePosition.Y;

						var trantf = (TranslateTransform)shape.RenderTransform;

						trantf.X += deltaX;
						trantf.Y += deltaY;

						firstMousePosition = currentPosition;
						isTranTFChanged = true;
					});
                    Dragging?.Invoke(this, EventArgs.Empty);
                }
			}
		}
		private void P_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var s = e.Source as Shape;
			if (s == null) return;
			firstMousePosition = e.GetPosition(CanvasManager.Canvas);
			s.CaptureMouse();
		}

		public event EventHandler Dragged;
		public event EventHandler Dragging;
		public override void OnMouseEnter()
		{
			base.OnMouseEnter();
			ShapesOnCanvas.ForEach(shape =>
			{
				shape.Cursor = Cursors.SizeAll;
			});
		}
		public override void OnMouseLeave()
		{
			base.OnMouseLeave();
			ShapesOnCanvas.ForEach(shape =>
			{
				shape.Cursor = Cursors.Arrow;
			});
		}

		public IList<Point3D> GetCurrentBasePointsDecart()
		{
			var x = TranslateTransform.X;
			var y = TranslateTransform.Y;
			var n = BasePointsDecart.Count;

			var r = new List<Point3D>();
			for (var i = 0; i < n; i++)
			{
				var pCanvas = BasePointsCanvas[i];
				var pCanvasCurrent = new Point3D(pCanvas.X + x, pCanvas.Y + y, 0);
				var pDercartCurrent = CanvasManager.TransformCanvasToDecart(pCanvasCurrent);
				r.Add(pDercartCurrent);
			}
			return r;
		}
	}
}
