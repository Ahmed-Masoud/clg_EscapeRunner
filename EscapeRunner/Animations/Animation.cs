using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner
{
    /// <summary>
    /// Moving directions
    /// </summary>
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right,
    }

    public abstract class Animation
    {

        protected int imageIndex;
        public Point AnimationPosition { get; set; }

        protected int animationWidth, animationHeight;
        /// <summary>
        /// Draws the target animation
        /// </summary>
        public void Draw(Graphics g, Directions direction, Bitmap animationImage)
        {
            // The object isn't yet initialized
            g.DrawImage(animationImage, AnimationPosition.X, AnimationPosition.Y, animationWidth, animationHeight);
        }

        public abstract void LoadNextAnimationImage();
        
    }
}