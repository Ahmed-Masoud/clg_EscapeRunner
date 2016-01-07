using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner.GameObjects
{
    /// <summary>
    /// Projectiles are implemented using object pool design pattern
    /// </summary>
    public class ProjectileClassA : IWeapon, IDrawable
    {
        private static ExplosionAnimation prototypeExplosion = new ExplosionAnimation();
        public ExplosionAnimation explosionAnimation;   // Change ExplosionAnimation class to public to make this work

        int paintedFrames = 0;

        //private BulletAnimation bulletAnimation;
        private Point explosionPosition = Point.Empty;

        public Point ExplosionPosition
        {
            set
            {
                explosionAnimation.AnimationPosition = value;
                explosionPosition = value;
            }
            get { return explosionPosition; }
        }

        private bool used;

        public bool Used
        {
            get { return used; }
            set
            {
                if (value == true)
                {
                    // Initiate explosion
                    paintedFrames = 0;
                    used = value;
                }
                used = value;
            }
        }

        internal ProjectileClassA(int index)
        {
            // Used for lazy initialization in the bullet pool
            explosionAnimation = ((IPrototype<ExplosionAnimation>)prototypeExplosion).Clone();
            explosionAnimation.AnimationPosition = Point.Empty;

            Index = index;
        }

        public void UpdateGraphics(Graphics g)
        {
            // The position is set in the object pool
            explosionAnimation.DrawFrame(g, Player.Direction);
            Reset();
        }

        /// <summary>
        /// Used in object bool when the object is declared as not used
        /// </summary>
        public void Reset()
        {
            paintedFrames++;
            paintedFrames %= explosionAnimation.ImageCount;
            if (paintedFrames == 0)
            {
                paintedFrames = 0;
                // TODO check if removing the place reset affects the painting
                //ExplosionPosition = Point.Empty;
                Used = false;
            }
        }

        public int Index { get; set; }
    }
}