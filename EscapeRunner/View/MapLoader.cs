using EscapeRunner.BusinessLogic;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.View
{
    public static class MapLoader
    {
        private static int flareCounter = 0;
        private static List<Bitmap> flares;

        private static int[,] level = {
    { 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 7, 7, 7, 7, 7, 5, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 5, 7, 7, 7, 7, 7, 7, 7, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    { 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 5}
};

        private static List<Bitmap> levelTiles = new List<Bitmap>();
        private static Point location = new Point(500, 100);
        private static List<Bitmap> tiles = Model.LevelMap;

        static MapLoader()
        {
            flares = Model.FlareAnimation;
            foreach (int x in level)
                levelTiles.Add(tiles[x]);
        }

        public static Point startLocation { get; } = new Point(390, -280);

        public static void drawLevel(Graphics g)
        {
            int counter = 0;
            g.DrawImage(flares[flareCounter % flares.Count], 67, 376, 46, 134);
            g.DrawImage(flares[flareCounter++ % flares.Count], 615, 645, 46, 134);
            g.DrawImage(flares[flareCounter++ % flares.Count], 1222, 335, 46, 134);

            int rows = level.GetLength(0);
            int counter2 = 0;
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    Point p = location.TwoDimensionsToIso();
                    if (level[i, j] == 5)
                        g.DrawImage(levelTiles[counter2++], p.X, p.Y - 64, 64, 128);
                    else
                        g.DrawImage(levelTiles[counter2++], p.X, p.Y, 64, 64);

                    //g.DrawImage(tile, location.X, location.Y, 40, 40);
                    location.X += 32;
                    if (counter++ == level.GetLength(1) - 1)
                    {
                        location.Y += 32;
                        location.X = startLocation.X;
                        counter = 0;
                    }
                }
            }
            location = startLocation;
        }
    }
}