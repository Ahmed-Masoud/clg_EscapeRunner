using System.Drawing;

namespace EscapeRunner.Animations
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
        public Point AnimationPosition { get; set; } = Point.Empty;
        protected int animationWidth, animationHeight;

        protected IReciever reciever = new AnimationFactory();

        public Animation(AnimationType animationType)
        {
            reciever.Type = animationType;
        }

        /// <summary>
        /// Draws the target animation
        /// </summary>
        public virtual void DrawFrame(Graphics g, Bitmap animationImage)
        {
            g.DrawImage(animationImage, AnimationPosition.X, AnimationPosition.Y, animationWidth, animationHeight);
        }

        public abstract void LoadNextAnimationImage();
    }
}