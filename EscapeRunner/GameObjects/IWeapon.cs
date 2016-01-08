using System.Drawing;

namespace EscapeRunner.GameObjects
{
    /// <summary>
    /// Objects that exist in the object pool
    /// </summary>
    public interface IWeapon
    {
        int Index { get; set; }
        bool Used { get; set; }
        Point ExplosionPosition { get; set; }
        Point BulletStartPosition { get; set; }

        void Reset();
    }
}