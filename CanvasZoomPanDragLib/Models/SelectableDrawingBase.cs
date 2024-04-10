using CanvasZoomPanDragLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CanvasZoomPanDragLib.Models
{
	public abstract class SelectableDrawingBase : DrawingBase, ISelectableDrawing
	{
		protected SelectableDrawingBase(IList<Point3D> psDecart, CanvasManager canvasManager) : base(psDecart, canvasManager)
		{
			ShapesOnCanvas.ForEach(p =>
			{
				p.MouseLeftButtonDown += OnLMouseDownClick;
			});
		}

		protected virtual Brush COLOR_DEFAULT { get; set; } = Brushes.PaleGoldenrod;
		protected virtual Brush COLOR_HIGHLIGHT { get; set; } = Brushes.Black;
		protected virtual Brush COLOR_SELECTED { get; set; } = Brushes.Blue;
		protected virtual Brush COLOR_CREATED { get; set; } = Brushes.Green;

		private bool _isSelected = false;
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				OnSelected(_isSelected);
			}
		}

		private bool _isCreated = false;
		public bool IsCreated
		{
			get => _isCreated;
			set
			{
				_isCreated = value;
				OnCreated(_isCreated);
			}
		}

		public event EventHandler SelectedChanged;

		public virtual void OnMouseEnter()
		{
			ShapesOnCanvas.ForEach(ins =>
			{
				ins.Stroke = COLOR_HIGHLIGHT;
				ins.Fill = COLOR_HIGHLIGHT;
			}
			);
		}
		public virtual void OnMouseLeave()
		{
			ShapesOnCanvas.ForEach(ins =>
			{
				if (IsSelected)
				{
					ins.Stroke = COLOR_SELECTED;
					ins.Fill = COLOR_SELECTED;
				}
				else
				{
					ins.Stroke = COLOR_DEFAULT;
					ins.Fill = COLOR_DEFAULT;
				}
			}
			);
		}
		protected virtual void OnSelected(bool val)
		{
			ShapesOnCanvas.ForEach(ins =>
			{
				if (val == true)
				{
					ins.Stroke = COLOR_SELECTED;
					ins.Fill = COLOR_SELECTED;
				}
				else
				{
					ins.Stroke = COLOR_DEFAULT;
					ins.Fill = COLOR_DEFAULT;
				}
			}
			);
			SelectedChanged?.Invoke(this, EventArgs.Empty);
		}
		protected virtual void OnCreated(bool val)
		{
			ShapesOnCanvas.ForEach(ins =>
			{
				if (val == true)
				{
					ins.Fill = COLOR_CREATED;
				}
				else
				{
					ins.Fill = COLOR_DEFAULT;
				}
			}
			);
		}
		protected virtual void OnLMouseDownClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			IsSelected = !IsSelected;
			SelectedChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
