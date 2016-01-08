using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.GameObjects
{
    /// <summary>
    /// Object Pool
    /// </summary>
    public class ProjectilePool
    {
        #region Public Fields

        public static int NumberOfProjectiles = 10;

        #endregion

        #region Private Fields

        private static ProjectilePool instance;
        private static List<IWeapon> projectiles;
        private readonly int verticalOffset = 35, horizontalOffset = 35, chHeight = 10, chWidth = 10;

        #endregion

        #region Public Constructors

        static ProjectilePool()
        {
            projectiles = new List<IWeapon>(10);

            // Create normal weapon spawns
            for (int i = 0; i < NumberOfProjectiles - 3; i++)
            {
                projectiles.Add(new ProjectileClassA(i));
                projectiles[i].Used = false;
            }

            // Create super weapon spawns
            for (int i = NumberOfProjectiles - 3; i < NumberOfProjectiles - 1; i++)
            {
                projectiles.Add(new ProjectileClassB(i));
                projectiles[i].Used = false;
            }
        }

        #endregion

        #region Public Properties

        // The singleton
        public static ProjectilePool Instance { get { return instance == null ? instance = new ProjectilePool() : instance; } }

        #endregion

        #region Public Methods

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
                throw new InvalidOperationException("All superweapon objects are currently in use");
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

        #endregion

        #region Private Methods

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
                    position.X += (int)(chWidth * 0.75);
                    position.Y += verticalOffset + chHeight;
                    break;

                case Directions.Down:
                    position.X += (int)(chWidth * 0.75);
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

        #endregion
    }
}