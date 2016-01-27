using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    abstract class Gift : Animation, IDrawable
    {
        //protected Image myGif;
        protected IndexPair indexPair;
        protected Point isoPoint;
        public bool IsTaken
        {
            get; set;
        }

        /// <summary>
        /// 2D location of the object
        /// </summary>
        public Point DrawLocation
        {
            get
            {
                return animationPosition;
            }
        }

        protected static Size dimension = new Size(40, 40);

        public abstract void UpdateGraphics(Graphics g);
    }
}
