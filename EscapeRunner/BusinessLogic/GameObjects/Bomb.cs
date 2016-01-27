using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal abstract class Bomb : IDrawable
    {
        protected IndexPair indexPair;
        protected Point isoPoint;

        public bool IsTaken
        {
            get; set;
        }

        public Point DrawLocation
        {
            get
            {
                return isoPoint;
            }
        }

        protected static Size dimension = new Size(32, 32);

        public abstract void UpdateGraphics(Graphics g);
    }
}