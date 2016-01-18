using System;

//using System.Linq;
//using System.Threading.Tasks;
using System.Drawing;

namespace LevelBuilder.Model
{
    internal class SelectionTool
    {
        private int bottom_right_x;
        private int bottom_right_y;
        private Boolean isDragging;
        private Point startDrag;
        private Point stopDrag;
        private int top_left_x;
        private int top_left_y;

        public SelectionTool()
        {
            startDrag = new Point(-1, -1);
            stopDrag = new Point(-1, -1);
            isDragging = false;
        }

        public int BottomRightX
        {
            get { return bottom_right_x; }
            set { bottom_right_x = value; }
        }

        public int BottomRightY
        {
            get { return bottom_right_y; }
            set { bottom_right_y = value; }
        }

        public Boolean IsDragging
        {
            get { return isDragging; }
            set { isDragging = value; }
        }

        public Point StartDrag
        {
            get { return startDrag; }
            set { startDrag = value; }
        }

        public Point StopDrag
        {
            get { return stopDrag; }
            set { stopDrag = value; FinalizeSelection(); }
        }

        public int TopLeftX
        {
            get { return top_left_x; }
            set { top_left_x = value; }
        }

        public int TopLeftY
        {
            get { return top_left_y; }
            set { top_left_y = value; }
        }

        private void FinalizeSelection()
        {
            top_left_x = startDrag.X;
            top_left_y = startDrag.Y;
            bottom_right_x = stopDrag.X;
            bottom_right_y = stopDrag.Y;

            if (startDrag.X > stopDrag.X)
            {   // swap StartDragX and StopDragX
                top_left_x = stopDrag.X;
                bottom_right_x = startDrag.X;
            }

            if (startDrag.Y > stopDrag.Y)
            {   // swap StartDragY and StopDragY
                top_left_y = stopDrag.Y;
                bottom_right_y = startDrag.Y;
            }

            bottom_right_x = bottom_right_x + 1;
            bottom_right_y = bottom_right_y + 1;
        }
    }
}