using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    /// <summary>
    /// Projectiles are implemented using object pool design pattern
    /// </summary>
    public sealed class ProjectileClassAlpha : IWeapon, IDrawable
    {
        private static BulletAnimation prototypeBulletAnimation =
            (BulletAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.BulletAnimation);

        private BulletAnimation bulletAni;
        private Point bulletStartPosition = Point.Empty;
        private ExplosionAnimation explosionAni;
        private Point explosionPosition = Point.Empty;

        private bool used;

        public Point BulletStartPosition
        {
            set { bulletStartPosition = bulletAni.AnimationPosition = value; }
            get { return bulletStartPosition; }
        }

        public Point DrawLocation => bulletAni.AnimationPosition;

        public Point ExplosionPosition
        {
            set { explosionPosition = explosionAni.AnimationPosition = value; }
            get { return explosionPosition; }
        }

        /// <summary>
        /// Changes the state of visibility and collider of the bullet
        /// </summary>
        public bool Used
        {
            get
            { return used; }
            set
            {
                if (value == true)
                {
                    bulletAni.Active = true;
                }
                else
                {
                    bulletAni.Active = false;
                    bulletAni.Locked = false;
                }
                used = value;
            }
        }

        public int ZOrder { get; set; }

        internal ProjectileClassAlpha()
        {
            // Used for lazy initialization in the bullet pool
            bulletAni = (BulletAnimation)((IPrototype<AnimationObject>)prototypeBulletAnimation).Clone();
            explosionAni = (ExplosionAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.ExplosionAnimation);

            explosionAni.AnimationPosition = Point.Empty;
            bulletAni.AnimationPosition = Point.Empty;
            bulletAni.Collider.Collided += BulletCollider_Collided;
            bulletAni.BulletOutOfBounds += BulletAni_BulletOutOfBounds;
        }

        public void UpdateGraphics(Graphics g)
        {
            bulletAni.DrawBullet(g);
            ZOrder = bulletAni.AnimationPosition.X + bulletAni.AnimationPosition.Y;
        }

        private void BulletAni_BulletOutOfBounds(object sender, System.EventArgs e)
        {
            this.Used = false;
        }

        private void BulletCollider_Collided(CollisionEventArgs e)
        {
            if (e.CollidingObject.ToString() == "monster")
            {
                // Release the bullet
                this.Used = false;
            }
        }
    }
}