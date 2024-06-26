﻿using CanvasZoomPanDragLib.Models;
using CanvasZoomPanDragLib.Utils;
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
using static System.Net.Mime.MediaTypeNames;

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
            canvasManager = new CanvasManager(cv, new Point3D(0, 0, 0), new Point3D(10000, 10000, 0));
            sel = new Selectable(new List<Point3D> { new Point3D(0, 0, 0) }, canvasManager);
            drag = new Draggable(new List<Point3D> { new Point3D(0, 0, 0), new Point3D(5000, 0, 0), new Point3D(5000, 5000, 0) }, canvasManager);
            var txt = new Txt(new List<Point3D> { new Point3D(0, 4000, 0) }, canvasManager);
            drag.Dragging += Drag_Dragging;
            canvasManager.Add(sel);
            canvasManager.Add(drag);
            canvasManager.Add(txt);

            lbBase.Items.Add(drag.BasePointsDecart[0]);
            lbBase.Items.Add(drag.BasePointsDecart[1]);
            lbBase.Items.Add(drag.BasePointsDecart[2]);
            var ps = drag.GetCurrentBasePointsDecart();
            lbDragging.Items.Add(ps[0]);
            lbDragging.Items.Add(ps[1]);
            lbDragging.Items.Add(ps[2]);
        }

        private void Drag_Dragging(object sender, EventArgs e)
        {
            lbDragging.Items.Clear();
            var ps = drag.GetCurrentBasePointsDecart();
            lbDragging.Items.Add(ps[0]);
            lbDragging.Items.Add(ps[1]);
            lbDragging.Items.Add(ps[2]);
        }
    }

    class Txt : DrawingBase
    {
        public Txt(IList<Point3D> psDecart, CanvasManager canvasManager) : base(psDecart, canvasManager)
        {
        }

        protected override List<Shape> CreateShapesOnCanvas()
        {
            var p = BasePointsCanvas[0];
            Label textBox = new Label();
            textBox.Content = "セットバック11";
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.BorderBrush = Brushes.Brown;
            textBox.BorderThickness = new Thickness(2);
            textBox.Width = 100;
            textBox.Height = 50;
            var x = p.X - textBox.Width / 2;
            var y = p.Y - textBox.Height / 2;
            Canvas.SetLeft(textBox, x);
            Canvas.SetTop(textBox, y);
            textBox.RenderTransformOrigin = new Point(0.5, 0.5);
            textBox.RenderTransform = new RotateTransform(45, 0.5, 0.5);
            CanvasManager.Canvas.Children.Add(textBox);

            var shapes = new List<Shape>();
            var el = new Ellipse();
            Canvas.SetLeft(el, p.X - 5);
            Canvas.SetTop(el, p.Y - 5);
            el.Height = 10;
            el.Width = 10;
            el.Fill = Brushes.Black;
            shapes.Add(el);
            return shapes;
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

            var pl = new Polygon();
            pl.Fill = COLOR_DEFAULT;
            pl.Stroke = COLOR_DEFAULT;
            pl.Points.Add(p0.ToPoint2D());
            pl.Points.Add(p1.ToPoint2D());
            pl.Points.Add(p2.ToPoint2D());
            shapes.Add(pl);

            return shapes;
        }
    }
}
