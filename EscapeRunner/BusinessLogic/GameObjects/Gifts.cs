using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    abstract class Gift : IDrawable
    {
        //protected Image myGif;
        protected IndexPair indexPair;
        protected Point isoPoint;
        public bool IsTaken
        {
            get; set;
        }

        public Point myPoint
        {
            get
            {
                return isoPoint;
            }
        }

        protected static Size dimension = new Size(40, 40);

        public abstract  void UpdateGraphics(Graphics g);
    }
}
