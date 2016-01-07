using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.GameObjects
{
    public class ProjectilePool
    {
        public static int NumberOfProjectiles = 10;
        private static List<IWeapon> projectiles;
        private readonly int verticalOffset = 35, horizontalOffset = 35, chHeight = 10, chWidth = 10;

        static ProjectilePool()
        {
            projectiles = new List<IWeapon>(10);

            for (int i = 0; i < NumberOfProjectiles - 3; i++)
            {
                projectiles.Add(new ProjectileClassA(i));
                projectiles[i].Used = false;
            }
            for (int i = NumberOfProjectiles - 3; i < NumberOfProjectiles - 1; i++)
            {
                projectiles.Add(new ProjectileClassB(i));
                projectiles[i].Used = false;
            }
        }

        private static ProjectilePool instance = new ProjectilePool();

        public static ProjectilePool Instance
        {
            get { return instance; }
        }

        public IWeapon Acquire(Point position, bool isSuperWeapon)
        {
            if (isSuperWeapon)
            {
                for (int i = NumberOfProjectiles - 3; i < NumberOfProjectiles - 1; i++)
                {
                    if (!projectiles[i].Used)
                    {
                        position = SetExplosionPlace(Player.Direction, position);
                        projectiles[i].ExplosionPosition = position;
                        return projectiles[i];
                    }
                }
                throw new InvalidOperationException("All superweapon objects are currently in use");
            }
            else
            {
                for (int i = 0; i < NumberOfProjectiles - 3; i++)
                {
                    if (!projectiles[i].Used)
                    {
                        projectiles[i].ExplosionPosition = SetExplosionPlace(Player.Direction, position);
                        projectiles[i].Used = true;
                        return projectiles[i];
                    }
                }
                throw new InvalidOperationException("All normal weapons are currently in use");
            }
        }

        public void Dispose(IWeapon projectile)
        {
            // TODO check if the location and image counter are reset
            projectile.Used = false;

        }

        private Point SetExplosionPlace(Directions direction, Point position)
        {

            switch (direction)
            {
                case Directions.Up:
                    position.Y += verticalOffset + chHeight;
                    break;

                case Directions.Down:
                    position.Y -= verticalOffset;
                    break;

                case Directions.Left:

                    position.Y += verticalOffset / 3;
                    position.X += horizontalOffset + chHeight / 2;
                    break;

                case Directions.Right:
                    position.Y += verticalOffset / 3;
                    position.X -= horizontalOffset - chWidth;
                    break;
            }

            return position;
        }
    }
}