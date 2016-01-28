using System;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class ProjectileClassBeta : IWeapon, IDrawable
    {
        public Point BulletStartPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Point ExplosionPosition
        {
            get
            {
                return ExplosionPosition;
            }
            set
            {
                // TODO add the bullet animation position to value
                ExplosionPosition = value;
            }
        }

        public Point DrawLocation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Used { get; set; }

        public void UpdateGraphics(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}