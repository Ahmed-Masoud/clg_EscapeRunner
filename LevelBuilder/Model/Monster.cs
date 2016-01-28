using System.Drawing;

namespace LevelBuilder.Model
{
    public class MapMonster : MapObject
    {
        private Point endPoint;

        private bool start;
        private bool end;

        public MapMonster()
        {
            start = false;
            end = false;
            endPoint = new Point(-1, -1);
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public bool Start
        {
            get { return start; }
            set { start = value; }
        }

        public bool End
        {
            get { return end; }
            set { end = value; }
        }
    }
}