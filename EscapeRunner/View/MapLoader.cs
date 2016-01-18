using EscapeRunner.BusinessLogic;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.View
{
    public static class MapLoader
    {
        private static int colomns;
        private static int flareCounter = 0;
        private static List<Bitmap> flares;

        private static int[,] level = {
           { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4},
            { 3, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3}
        };

        private static LevelTile[,] levelTiles;

        private static int rows;
        private static List<Bitmap> tiles = Model.TileTextures;

        public static Point startLocation { get; } = new Point(390, -280);
        private static Point location = startLocation;

        static MapLoader()
        {
            //  = level.GetLength(1);
            //TODO read binary array From File
            rows = level.GetLength(0);
            colomns = level.GetLength(1);
            levelTiles = new LevelTile[level.GetLength(0), level.GetLength(1)];
            //TODO LoadLevel
            flares = Model.FlareAnimation;
            //foreach (int x in level)
            //    levelTiles.Add(tiles[x]);
            LoadLevel();

        }

        public static void drawLevel(Graphics g)
        {
            g.DrawImage(flares[flareCounter % flares.Count], 67, 376, 46, 134);
            g.DrawImage(flares[flareCounter++ % flares.Count], 615, 645, 46, 134);
            g.DrawImage(flares[flareCounter++ % flares.Count], 1222, 335, 46, 134);
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colomns; j++)
                {
                    levelTiles[i, j].Draw(g);
                }
            }
            location = startLocation;
        }

        public static void LoadLevel()
        {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colomns; j++)
                {
                    TileType tempType = (TileType)level[i, j];
                    levelTiles[i, j] = new LevelTile(location, level[i, j], tempType);

                    location.X += 32;
                }
                location.Y += 32;
                location.X = startLocation.X;
            }
            location = startLocation;
        }
    }
}