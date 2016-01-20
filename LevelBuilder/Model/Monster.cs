using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LevelBuilder.Model
{
    public class Monster
    {
        private Point startPoint;
        private Point endPoint;

        private bool start;
        private bool end;

        public Monster()
        {
            start = false;
            end = false;
        }

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
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
