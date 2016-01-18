using System.Drawing;

namespace EscapeRunner.GameObjects
{
    /// <summary>
    /// Objects that exist in the object pool
    /// </summary>
    public interface IWeapon
    {
        Point BulletStartPosition { get; set; }
        Point ExplosionPosition { get; set; }
        int Index { get; set; }
        bool Used { get; set; }

        void Reset();
    }
}