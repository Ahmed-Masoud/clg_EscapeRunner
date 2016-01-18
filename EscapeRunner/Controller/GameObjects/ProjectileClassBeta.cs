using System;
using System.Drawing;

namespace EscapeRunner.GameObjects
{
    internal class ProjectileClassBeta : IWeapon, IDrawable
    {
        internal ProjectileClassBeta(int index)
        {
            //ExplosionPosition = Point.Empty;
            Index = index;
        }

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

        public int Index
        {
            get { return Index; }
            set { }
        }

        public bool Used { get; set; }

        public void Reset()
        {
            //ExplosionPosition = Point.Empty;
            Used = false;
        }

        public void UpdateGraphics(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}