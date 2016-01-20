using EscapeRunner.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.View
{
    public static class MapLoader
    {
        private static int flareCounter = 0;
        private static List<Bitmap> flares;

        private static int[,] level = {
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 2, 2, 2, 2, 3, 0, 0, 0, 3, 2, 2, 2, 2, 2, 2, 2, 4},
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 5, 5, 5, 5, 3, 0, 0, 0, 3, 5, 5, 5, 5, 5, 5, 5, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
    { 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3}
        };

        private static int levelColomns;
        private static int levelRows;
        private static Point startLocation = new Point(390, -280);
        private static Point playerStartLocation;
        private static IndexPair levelDimensions;
        private static Point location = startLocation;
        private static List<LevelTile> obstacleTiles;
        private static Random randomNumberGenerator = new Random();

        /// <summary>
        /// Tile types that make the level
        /// </summary>
        private static List<Bitmap> tileBlocks = Model.TileTextures;

        /// <summary>
        /// Floor tiles that will be drawn only once
        /// </summary>
        private static List<LevelTile> walkableTiles;
        public static List<LevelTile> WalkableTiles { get { return walkableTiles; } }
        public static List<LevelTile> ObstacleTiles { get { return obstacleTiles; } }
        public static Point LevelStartLocation { get { return startLocation; } }
        public static Point PlayerStartLocation { get { return playerStartLocation; } }
        public static IndexPair LevelDimensions { get { return levelDimensions; } }

        public static Point MonsterStartLocation
        {
            get
            {
                // Return monster position that's at least 5 tile away from the player
                if (walkableTiles != null)
                    return walkableTiles[randomNumberGenerator.Next(5, (walkableTiles.Count - 1))].Position;
                throw new InvalidOperationException("Walkable tiles isn't initialized");
            }
        }
        static MapLoader()
        {
            flares = Model.FlareAnimation;

            walkableTiles = new List<LevelTile>(256);
            obstacleTiles = new List<LevelTile>(32);

            //TODO LoadLevel
            //  = level.GetLength(1);
            //TODO read binary array From File
            levelRows = level.GetLength(0);
            levelColomns = level.GetLength(1);
            levelDimensions = new IndexPair(levelRows, levelColomns);
            LoadLevel();

            // Determine the player start location
            playerStartLocation = walkableTiles[0].Position;
        }

        public static bool IsWalkable(IndexPair pair)
        {
            return level[pair.I, pair.J] == 0 ? true : false;
        }

        /// <summary>
        /// Draw the fire flares underneath the level
        /// </summary>
        /// <param name="g"></param>
        public static void DrawGameFlares(Graphics g)
        {
            g.DrawImage(flares[flareCounter % flares.Count], 67, 376, 46, 104);
            g.DrawImage(flares[flareCounter % flares.Count], 615, 645, 46, 104);
            g.DrawImage(flares[flareCounter++ % flares.Count], 1222, 335, 46, 104);
        }

        public static void DrawLevelFloor(Graphics g)
        {
            // Draw the walkable ground
            foreach (var tile in walkableTiles)
                tile.Draw(g);
        }

        public static void DrawLevelObstacle(Graphics g)
        {
            // Find the player current position, don't draw the walls around it in the loop
            foreach (var tile in obstacleTiles)
                tile.Draw(g);
        }

        public static void LoadLevel()
        {
            for (int i = 0; i < levelRows; i++)
            {
                for (int j = 0; j < levelColomns; j++)
                {
                    TileType tempType = (TileType)level[i, j];

                    if (tempType == TileType.Floor)
                        walkableTiles.Add(new LevelTile(location, level[i, j], tempType, new IndexPair(i, j)));
                    else
                    {
                        // All obstacle tiles need to be drawn on their own to implement depth sorting
                        obstacleTiles.Add(new LevelTile(location, level[i, j], tempType, new IndexPair(i, j)));
                    }

                    location.X += 32;
                }
                location.Y += 32;
                location.X = startLocation.X;
            }
            location = startLocation;
        }
    }
}