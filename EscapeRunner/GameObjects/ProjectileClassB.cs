using System;
using System.Drawing;

namespace EscapeRunner.GameObjects
{
    internal class ProjectileClassB : IWeapon
    {
        #region Internal Constructors

        internal ProjectileClassB(int index)
        {
            //ExplosionPosition = Point.Empty;
            Index = index;
        }

        #endregion

        #region Public Properties

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

        #endregion

        // TODO remove fake int d

        #region Public Methods

        public void Reset()
        {
            //ExplosionPosition = Point.Empty;
            Used = false;
        }

        #endregion
    }
}