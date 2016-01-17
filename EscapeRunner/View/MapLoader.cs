using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.View
{
    public static class MapLoader
    {
        private static int[,] level;
        private static List<Bitmap> flares;
        private static int flareCounter = 0;

        private static int[,] JingZhou = {
    //{ 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,  5 },
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
    //{ 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,  5 }
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
        private static List<Bitmap> tiles = Model.LevelMap;

        private static Point location = new Point(500, 100);
        private static Point startLocation = new Point(340, -320);

        static MapLoader()
        {
            flares = Model.FlareAnimation;
            foreach (int x in JingZhou)
                levelTiles.Add(tiles[x]);
        }

        public static void drawLevel(Graphics g)
        {
            int counter = 0;
            g.DrawImage(Properties.Resources.Background, 0, 0, MainWindow.RightBound, MainWindow.LowerBound);
            g.DrawImage(flares[flareCounter % flares.Count], 60, 370, 46, 134);
            g.DrawImage(flares[flareCounter++ % flares.Count], 605, 625, 46, 134);
            g.DrawImage(flares[flareCounter++ % flares.Count], 1222, 335, 46, 134);

            int rows = JingZhou.GetLength(0);
            int counter2 = 0;
            for (int i = 0; i < JingZhou.GetLength(0); i++)
            {
                for (int j = 0; j < JingZhou.GetLength(1); j++)
                {
                    //Point p = twoDToIso(location);
                    //g.DrawImage(levelTiles[i * rows + j], p.X, p.Y, 65, 58);

                    //location.X += 40;
                    //if (counter++ == JingZhou.GetLength(0))
                    //{
                    //    location.Y += 40;
                    //    location.X = 0;
                    //    counter = 0;
                    //}
                    //location.X = 0;
                    //location.Y = 0;
                    Point p = twoDToIso(location);
                    //location.X += 40;

                    if (JingZhou[i, j] == 5)
                        g.DrawImage(levelTiles[counter2++], p.X, p.Y - 64, 64, 128);
                    else
                        g.DrawImage(levelTiles[counter2++], p.X, p.Y, 64, 64);

                    //g.DrawImage(tile, location.X, location.Y, 40, 40);
                    location.X += 32;
                    if (counter++ == JingZhou.GetLength(1) - 1)
                    {
                        location.Y += 32;
                        location.X = startLocation.X;
                        counter = 0;
                    }
                }
            }
            //foreach (var tile in levelTiles)
            //{

            //    //isoTo2D(location)
            //    //twoDToIso
            //    //Point p = getTileCoordinates((location), 40);

            //    Point p = twoDToIso(location);
            //    //location.X += 40;

            //    if(tile)
            //    g.DrawImage(tile, p.X, p.Y,64, 64);

            //    //g.DrawImage(tile, location.X, location.Y, 40, 40);
            //    location.X += 32;
            //    if (counter++ == JingZhou.GetLength(1) - 1)
            //    {
            //        location.Y += 32;
            //        location.X = startLocation.X;
            //        counter = 0;
            //    }

            //}
            location = startLocation;
            
        }

        private static Point getTileCoordinates(Point pt, int tileHeight)
        {
            Point tempPt = new Point(0, 0);
            tempPt.X = (int)Math.Floor((double)(pt.X / tileHeight));
            tempPt.Y = (int)Math.Floor((double)(pt.Y / tileHeight));
            return (tempPt);
        }

        private static Point isoTo2D(Point pt)
        {
            Point tempPt = new Point();
            tempPt.X = (2 * pt.Y + pt.X) / 2;
            tempPt.Y = (2 * pt.Y - pt.X) / 2;
            return tempPt;
        }

        private static Point twoDToIso(Point pt)
        {
            Point tempPt = new Point(0, 0);
            tempPt.X = (pt.X - pt.Y);
            tempPt.Y = (pt.X + pt.Y) / 2;
            return (tempPt);
        }
    }
}