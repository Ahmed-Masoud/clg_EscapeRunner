using System.Drawing;

namespace LevelBuilder.Model
{
    public abstract class MapObject
    {
        private Point startPoint;

        public MapObject()
        {
            startPoint = new Point(-1, -1);
        }

        public Point StartPoint
        {
            get { return startPoint; }

            set { startPoint = value; }
        }
    }
}