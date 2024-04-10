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
		CanvasManager canvasManager;
		Selectable sel;
		Draggable drag;

		public MainWindow()
		{
			InitializeComponent();
			canvasManager = new CanvasManager(cv, new Point3D(), new Point3D(10000, 10000, 0));
			sel = new Selectable(new List<Point3D> { new Point3D(0, 0, 0) }, canvasManager);
			drag = new Draggable(new List<Point3D> { new Point3D(0, 0, 0), new Point3D(5000, 0, 0), new Point3D(5000, 5000, 0), new Point3D(0, 5000, 0) }, canvasManager);
			drag.Dragged += (s, e) =>
			{
				string mess = "";
				mess += "Dragged" + "\n";
				mess += "Current Points" + "\n";
				mess += drag.GetCurrentBasePointsDecart()[0].ToString();
				MessageBox.Show(mess);
			};
			canvasManager.Add(sel);
			canvasManager.Add(drag);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var ps = drag.GetCurrentBasePointsDecart();
			string mess = "";
			mess += "Base Points" + "\n";
			foreach (var p in drag.BasePointsDecart)
			{
				mess += p.ToString() + "\n";
			}
			mess += "--------------" + "\n";
			mess += "Current Points" + "\n";
			foreach (var p in ps)
			{
				mess += p.ToString() + "\n";
			}
			MessageBox.Show(mess);
		}
	}
	class Selectable : SelectableDrawingBase
	{
		public Selectable(IList<Point3D> pDecart, CanvasManager canvasManager) : base(pDecart, canvasManager)
		{
		}

		protected override List<Shape> CreateShapesOnCanvas()
		{
			var shapes = new List<Shape>();
			var el = new Ellipse();
			var pCanvas = BasePointsCanvas[0];
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
		public Draggable(IList<Point3D> pDecart, CanvasManager canvasManager) : base(pDecart, canvasManager)
		{
		}

		protected override List<Shape> CreateShapesOnCanvas()
		{
			var shapes = new List<Shape>();
			var p0 = BasePointsCanvas[0];
			var p1 = BasePointsCanvas[1];
			var p2 = BasePointsCanvas[2];
			var p3 = BasePointsCanvas[3];

			Line line = new Line();
			line.StrokeThickness = 4;
			line.Stroke = COLOR_DEFAULT;
			line.X1 = p0.X;
			line.X2 = p1.X;
			line.Y1 = p0.Y;
			line.Y2 = p1.Y;
			shapes.Add(line);
			Line line1 = new Line();
			line1.StrokeThickness = 4;
			line1.Stroke = COLOR_DEFAULT;
			line1.X1 = p1.X;
			line1.X2 = p2.X;
			line1.Y1 = p1.Y;
			line1.Y2 = p2.Y;
			shapes.Add(line1);
			Line line2 = new Line();
			line2.StrokeThickness = 4;
			line2.Stroke = COLOR_DEFAULT;
			line2.X1 = p2.X;
			line2.X2 = p3.X;
			line2.Y1 = p2.Y;
			line2.Y2 = p3.Y;
			shapes.Add(line2);
			Line line3 = new Line();
			line3.StrokeThickness = 4;
			line3.Stroke = COLOR_DEFAULT;
			line3.X1 = p3.X;
			line3.X2 = p0.X;
			line3.Y1 = p3.Y;
			line3.Y2 = p0.Y;
			shapes.Add(line3);

			return shapes;
		}
	}
}
