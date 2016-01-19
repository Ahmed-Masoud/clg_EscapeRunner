using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    /// <summary>
    /// Object Pool
    /// </summary>
    public class ProjectilePool
    {
        public static int NumberOfProjectiles = 10;

        private static ProjectilePool instance;
        private static List<IWeapon> projectiles;

        private readonly int verticalOffset = 35;
        private int chHeight = PlayerAnimation.PlayerSize.Height, chWidth = PlayerAnimation.PlayerSize.Width;

        static ProjectilePool()
        {
            projectiles = new List<IWeapon>(10);

            // Create normal weapon spawns
            for (int i = 0; i < NumberOfProjectiles - 3; i++)
            {
                projectiles.Add(new ProjectileClassAlpha(i));
                projectiles[i].Used = false;
            }

            // Create super weapon spawns
            for (int i = NumberOfProjectiles - 3; i < NumberOfProjectiles - 1; i++)
            {
                projectiles.Add(new ProjectileClassBeta(i));
                projectiles[i].Used = false;
            }
        }

        // The singleton
        public static ProjectilePool Instance { get { return instance == null ? instance = new ProjectilePool() : instance; } }

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
                        projectiles[i].BulletStartPosition = position;

                        projectiles[i].Used = true;
                        return projectiles[i];
                    }
                }
                throw new InvalidOperationException("All super weapon objects are currently in use");
            }
            else
            {
                for (int i = 0; i < NumberOfProjectiles - 3; i++)
                {
                    if (!projectiles[i].Used)
                    {
                        position = SetExplosionPlace(Player.Direction, position);
                        projectiles[i].ExplosionPosition = position;
                        projectiles[i].BulletStartPosition = position;

                        projectiles[i].Used = true;
                        return projectiles[i];
                    }
                }
                throw new InvalidOperationException("All normal weapons are currently in use");
            }
        }

        /// <summary>
        /// Configure the explosion position based on the character's position
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="position"></param>
        /// <returns>The actual explosion position</returns>
        private Point SetExplosionPlace(Directions direction, Point position)
        {
            switch (direction)
            {
                case Directions.Up:
                    position.X += (int)(chWidth * 0.37);
                    position.Y += verticalOffset + (int)(chHeight * 0.7);
                    break;

                case Directions.Down:
                    position.X += (int)(chWidth * 0.37);
                    position.Y -= verticalOffset;
                    break;

                case Directions.Left:

                    position.Y += chHeight / 2;
                    position.X += (int)(chWidth / 1.7);
                    break;

                case Directions.Right:
                    position.Y += chHeight / 2;
                    position.X += chWidth / 9;
                    break;
            }

            return position;
        }
    }
}