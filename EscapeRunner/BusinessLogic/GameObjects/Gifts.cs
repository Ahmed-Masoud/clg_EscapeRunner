using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal abstract class Gift : Animation, IDrawable
    {
        protected int giftValue;
        public State CurrentState { get; set; } = new GiftStateAlive();

        public void ChangeState()
        {
            CurrentState.NextState(this);
        }

        /// <summary>
        /// 2D location of the object
        /// </summary>
        public Point DrawLocation { get { return animationPosition; } }

        protected static Size dimension = new Size(40, 40);

        public abstract void UpdateGraphics(Graphics g);
    }
}