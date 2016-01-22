using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LevelBuilder.Model
{
    public class Player
    {
        private Point startPoint;

        public Player()
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
