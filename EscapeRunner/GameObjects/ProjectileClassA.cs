using System;
using System.Drawing;
using EscapeRunner.Animations;

namespace EscapeRunner.GameObjects
{
    /// <summary>
    /// Projectiles are implemented using command design pattern
    /// </summary>
    public class ProjectileClassA : IWeapon, IDrawable
    {
        private ExplosionAnimation explosionAnimation;

        private ProjectileClassA(Point explosionLocation)
        {
            explosionAnimation = new ExplosionAnimation(explosionLocation);
        }

        public void UpdateGraphics(Graphics g)
        {
            explosionAnimation.Draw(g, Player.Direction);
        }
    }
}