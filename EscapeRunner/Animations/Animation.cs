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
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    public abstract class Animation
    {
        protected List<Bitmap> animationImages;
        protected int imageIndex;
        public Point AnimationPosition { get; set; }

        protected int animationWidth, animationHeight;
        /// <summary>
        /// Draws the target animation
        /// </summary>
        public virtual void Draw(Graphics g)
        {
            // The object isn't yet initialized
            if (animationImages != null)
                g.DrawImage(animationImages[imageIndex], AnimationPosition.X, AnimationPosition.Y, animationWidth, animationHeight);
        }

        /// <summary>
        /// Loads the set of image that represent the animation
        /// </summary>
        public abstract void LoadAnimationFromDisk();

        public virtual void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= animationImages.Count;
        }
    }
}