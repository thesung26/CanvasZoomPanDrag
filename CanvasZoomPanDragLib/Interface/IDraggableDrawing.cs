using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasZoomPanDragLib.Interface
{
    public interface IDraggableDrawing
    {
        event EventHandler Dragged;
        event EventHandler Dragging;
    }
}
