using CanvasZoomPanDragLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoDotnetFramework
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			CanvasManager canvasManager = new CanvasManager(cv, new Point3D(), new Point3D(10000, 10000, 0));
			var sel = new Selectable(new Point3D(4000, 3000, 0), canvasManager);
			var drag = new Draggable(new Point3D(7000, 0, 0), canvasManager);
			canvasManager.Draw(sel);
			canvasManager.Draw(drag);
		}
	}
	class Selectable : SelectableDrawingBase
	{
		public Selectable(Point3D pDecart, CanvasManager canvasManager) : base(pDecart, canvasManager)
		{
		}

		protected override List<Shape> CreateShapesOnCanvas()
		{
			var shapes = new List<Shape>();
			var el = new Ellipse();
			var pCanvas = Parrent.TransformDecartToCanvas(PointDecart);
			Canvas.SetLeft(el, pCanvas.X - 10 / 2);
			Canvas.SetTop(el, pCanvas.Y - 10 / 2);
			el.Height = 10;
			el.Width = 10;
			el.Fill = COLOR_DEFAULT;
			shapes.Add(el);
			return shapes;
		}
	}

	class Draggable : DraggableDrawingBase
	{
		public Draggable(Point3D pDecart, CanvasManager canvasManager) : base(pDecart, canvasManager)
		{
		}

		protected override List<Shape> CreateShapesOnCanvas()
		{
			var shapes = new List<Shape>();

			var el = new Ellipse();
			var pCanvas = Parrent.TransformDecartToCanvas(PointDecart);
			Canvas.SetLeft(el, pCanvas.X - 10 / 2);
			Canvas.SetTop(el, pCanvas.Y - 10 / 2);
			el.Height = 10;
			el.Width = 10;
			el.Fill = COLOR_DEFAULT;
			shapes.Add(el);

			Line line = new Line();
			line.StrokeThickness = 4;
			line.Stroke = COLOR_DEFAULT;
			line.X1 = 10;
			line.X2 = 40;
			line.Y1 = 70;
			line.Y2 = 70;
			shapes.Add(line);

			return shapes;
		}
	}
}
