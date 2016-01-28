using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    abstract class Bomb : Animation, IDrawable
    {
        public bool IsTaken
        {
            get; set;
        }

        /// <summary>
        /// 2D drawing location
        /// </summary>
        public Point DrawLocation
        {
            get
            {
                return animationPosition;
            }
        }

        public abstract void UpdateGraphics(Graphics g);
    }
}
