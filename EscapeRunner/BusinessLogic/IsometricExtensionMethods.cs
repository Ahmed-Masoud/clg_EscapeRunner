using EscapeRunner.View;
using System;
using System.Drawing;

namespace EscapeRunner.BusinessLogic
{
    public static class IsometricExtensionMethods
    {
        public static IndexPair GetTileCoordinates(this Point pt, int tileHeight)
        {
            IndexPair tempPt = new IndexPair(0, 0);
            tempPt.I = (int)Math.Floor((double)(pt.X / tileHeight));
            tempPt.J = (int)Math.Floor((double)(pt.Y / tileHeight));
            return tempPt;
        }

        //public static Point IsoToTwoDimensions(this Point pt)
        //{
        //    Point tempPt = new Point();
        //    tempPt.X = (2 * pt.Y + pt.X) / 2;
        //    tempPt.Y = (2 * pt.Y - pt.X) / 2;
        //    return tempPt;
        //}

        /// <summary>
        /// Converts Cartesian coordinates to isometric coordinates
        /// </summary>
        /// <returns>Isometric coordinates</returns>
        public static Point TwoDimensionsToIso(this Point pt)
        {
            Point tempPt = new Point(0, 0);
            tempPt.X = (pt.X - pt.Y);
            tempPt.Y = (pt.X + pt.Y) / 2;
            return tempPt;
        }

        /// <summary>
        /// Returns the corresponding Coordinates for an i and j indexes
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        public static Point IndexesToCoordinates(this IndexPair pair)
        {
            if (pair.J > MapLoader.LevelDimensions.J || pair.I > MapLoader.LevelDimensions.I || pair.J < 0 || pair.I < 0)
                throw new InvalidOperationException("Out of level bounds");

            Point tempPt = new Point(MapLoader.LevelStartLocation.X + (pair.J * 32),
                MapLoader.LevelStartLocation.Y + (pair.I * 32));

            return tempPt;
        }

        public static IndexPair CoordinatesToIndexes(this Point point)
        {
            IndexPair tempPair = new IndexPair((point.Y - MapLoader.LevelStartLocation.Y) / 32, (point.X - MapLoader.LevelStartLocation.X) / 32);
            return tempPair;
        }
    }
}