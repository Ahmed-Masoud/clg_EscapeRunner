using System.Drawing;

namespace EscapeRunner.GameObjects
{
    internal class ProjectileClassB : IWeapon
    {
        internal ProjectileClassB(int index)
        {
            //ExplosionPosition = Point.Empty;
            Index = index;
        }

        public Point ExplosionPosition
        {
            get { return ExplosionPosition; }
            set
            {
                // TODO add the bullet animation position to value
                ExplosionPosition = value;
            }
        }

        public int Index
        {
            get { return Index; }
            set
            { }
        }

        public bool Used { get; set; }

        // TODO remove fake int d

        public void Reset()
        {
            //ExplosionPosition = Point.Empty;
            Used = false;
        }
    }
}