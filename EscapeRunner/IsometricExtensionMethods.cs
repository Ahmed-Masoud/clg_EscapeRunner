using System;
using System.Drawing;

namespace EscapeRunner
{
    public static class IsometricExtensionMethods
    {
        public static Point getTileCoordinates(this Point pt, int tileHeight)
        {
            Point tempPt = new Point(0, 0);
            tempPt.X = (int)Math.Floor((double)(pt.X / tileHeight));
            tempPt.Y = (int)Math.Floor((double)(pt.Y / tileHeight));
            return tempPt;
        }

        public static Point IsoToTwoDimensions(this Point pt)
        {
            Point tempPt = new Point();
            tempPt.X = (2 * pt.Y + pt.X) / 2;
            tempPt.Y = (2 * pt.Y - pt.X) / 2;
            return tempPt;
        }

        public static Point TwoDimensionsToIso(this Point pt)
        {
            Point tempPt = new Point(0, 0);
            tempPt.X = (pt.X - pt.Y);
            tempPt.Y = (pt.X + pt.Y) / 2;
            return tempPt;
        }
    }
}