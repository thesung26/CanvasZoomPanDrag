using CanvasZoomPanDragLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace CanvasZoomPanDragLib.Models
{
	public abstract class DraggableDrawingBase : SelectableDrawingBase, IDraggableDrawing
	{
		Point firstMousePosition = new Point();
		TranslateTransform translateTransform = new TranslateTransform();
		protected DraggableDrawingBase(Point3D pDecart, CanvasManager canvasManager) : base(pDecart, canvasManager)
		{
			ShapesOnCanvas.ForEach(p =>
			{
				p.RenderTransform = translateTransform;
				p.MouseLeftButtonDown += P_MouseLeftButtonDown;
				p.MouseMove += P_MouseMove;
			});
		}

		private void P_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var s = e.Source as Shape;
			if (s == null) return;
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (s.IsMouseCaptured)
				{
					ShapesOnCanvas.ForEach(shape =>
					{
						Point currentPosition = e.GetPosition(Parrent.Canvas);
						double deltaX = currentPosition.X - firstMousePosition.X;
						double deltaY = currentPosition.Y - firstMousePosition.Y;

						var trantf = (TranslateTransform)shape.RenderTransform;

						trantf.X += deltaX;
						trantf.Y += deltaY;

						firstMousePosition = currentPosition;
					});
				}
			}
			else
			{
				s.ReleaseMouseCapture();
				Dragged?.Invoke(this, EventArgs.Empty);
			}
		}
		private void P_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var s = e.Source as Shape;
			if (s == null) return;
			firstMousePosition = e.GetPosition(Parrent.Canvas);
			s.CaptureMouse();
		}

		public event EventHandler Dragged;
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
	}
}
