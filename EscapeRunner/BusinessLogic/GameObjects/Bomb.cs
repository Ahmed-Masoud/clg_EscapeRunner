using EscapeRunner.Animations;
using System.Drawing;
using System;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal abstract class Bomb : Animation, IDrawable
    {
        public bool Exploded { get; set; }
        public Bomb(Point location)
        {
            this.ZOrder = location.X + location.Y;
        }
        /// <summary>
        /// 2D drawing location
        /// </summary>
        public Point DrawLocation { get { return animationPosition; } }

        public int ZOrder { get; set; } = 3;

        public abstract void UpdateGraphics(Graphics g);
    }
}