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
        public static int numberOfProjectiles = 10;

        private static ProjectilePool instance;
        private static List<IWeapon> projectiles;

        private readonly int verticalOffset = 35;
        private int chHeight = PlayerAnimation.PlayerSize.Height, chWidth = PlayerAnimation.PlayerSize.Width;
        public static ProjectilePool Instance { get { return instance == null ? instance = new ProjectilePool() : instance; } }

        public static IndexPair superWeaponIndexes;
        public static IndexPair weaponIndexes;

        static ProjectilePool()
        {
            projectiles = new List<IWeapon>(numberOfProjectiles);

            // Create normal weapon spawns
            for (int i = 0; i < numberOfProjectiles - 3; i++)
            {
                projectiles.Add(new ProjectileClassAlpha());
                projectiles[i].Used = false;
            }
            weaponIndexes = new IndexPair(0, numberOfProjectiles - 3);

            // Create super weapon spawns
            for (int i = numberOfProjectiles - 3; i < numberOfProjectiles - 1; i++)
            {
                projectiles.Add(new ProjectileClassBeta());
                projectiles[i].Used = false;
            }
            superWeaponIndexes = new IndexPair(numberOfProjectiles - 3, numberOfProjectiles - 1);

        }

        // The singleton


        public IWeapon Acquire(Point position, bool isSuperWeapon)
        {
            var returnedIWeapon = CheckBulletAvailablility(position, isSuperWeapon);

            if (returnedIWeapon != null)
                return returnedIWeapon;

            throw new InvalidOperationException($"All weapon objects are currently in use");
        }

        /// <summary>
        /// Configure the explosion position based on the character's position
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="position"></param>
        /// <returns>The actual explosion position</returns>
        private Point SetExplosionPlace(Directions direction, Point position)
        {

            // TODO fix for isometric view
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

        private IWeapon CheckBulletAvailablility(Point position, bool isSuperWeapon)
        {
            IndexPair loopCounterRange;
            // Use the index pair as a start and end point to search inside the object pool
            if (isSuperWeapon)
                loopCounterRange = superWeaponIndexes;
            else
                loopCounterRange = weaponIndexes;

            for (int i = loopCounterRange.I; i < loopCounterRange.J; i++)
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
            return null;
        }
    }
}