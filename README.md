![image](https://github.com/thesung26/CanvasZoomPanDrag/assets/70112212/4699a082-1f72-4cac-b799-41954a96104f)

**In Xaml:**
        <cvLib:BorderZoomPanDrag Grid.Row="1" BorderThickness="1" BorderBrush="Black" ClipToBounds="True">
            <Canvas Background="White"
                    Width="350"
                    Height="350"
                    x:Name="cv"
                    >
            </Canvas>
        </cvLib:BorderZoomPanDrag>

**In Code-behind:**
  - Init canvas manager:
    
    CanvasManager canvasManager = new CanvasManager(cv, new Point3D(), new Point3D(10000, 10000, 0));
  - Init drawing (Drawing/SelectableDrawing/DraggableDrawing):
    
    Selectable sel = new Selectable(new List<Point3D> { new Point3D(0, 0, 0) }, canvasManager);

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
  - Draw:
    
    canvasManager.Add(sel);

**Demo:** https://github.com/thesung26/CanvasZoomPanDrag/tree/main/DemoDotnetFramework
