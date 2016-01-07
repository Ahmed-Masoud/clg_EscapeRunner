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
        private static BulletAnimation prototypeBullet = new BulletAnimation();
        private ExplosionAnimation explosionAni;   // Change ExplosionAnimation class to public to make this work
        private BulletAnimation bulletAni;
        private int paintedFrames = 0;

        //private BulletAnimation bulletAnimation;
        private Point explosionPosition = Point.Empty;
        private Point bulletStartPosition = Point.Empty;
        private bool used;
        private bool explosionAwake;

        public Point ExplosionPosition
        {
            set
            {
                explosionPosition = explosionAni.AnimationPosition = value;
            }
            get { return explosionPosition; }
        }

        public Point BulletStartPosition
        {
            set
            {
                bulletStartPosition = bulletAni.AnimationPosition = value;
            }
            get { return bulletStartPosition; }
        }

        public bool Used
        {
            get { return used; }
            set
            {
                if (value == true)
                {
                    // Initiate explosion
                    paintedFrames = 0;
                    explosionAwake = true;

                    used = value;
                }
                used = value;
            }
        }

        internal ProjectileClassA(int index)
        {
            // Used for lazy initialization in the bullet pool
            explosionAni = (ExplosionAnimation)((IPrototype<Animation>)prototypeExplosion).Clone();
            bulletAni = (BulletAnimation)((IPrototype<Animation>)prototypeBullet).Clone();
            explosionAni.AnimationPosition = Point.Empty;
            bulletAni.AnimationPosition = Point.Empty;
            Index = index;
        }

        public void UpdateGraphics(Graphics g)
        {
            // The position is set in the object pool
            if (explosionAwake)
                explosionAni.DrawFrame(g);
            bulletAni.DrawFrame(g);
            Reset();
        }

        /// <summary>
        /// Updates the object state when it has finished animating and mark it for re-use
        /// </summary>
        public void Reset()
        {
            paintedFrames++;
            paintedFrames %= explosionAni.ImageCount;
            if (paintedFrames == 0)
            {
                paintedFrames = 0;
                explosionAwake = false;
                // TODO check if removing the place reset affects the painting
                //ExplosionPosition = Point.Empty;
            }
            if (bulletAni.BulletReachedEnd() && !explosionAwake)
            {
                // Release the bullet when all animations are drawn
                Used = false;
            }
        }

        public int Index { get; set; }
    }
}