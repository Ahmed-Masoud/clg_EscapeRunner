using EscapeRunner.BusinessLogic;
using System.Drawing;

namespace EscapeRunner.View
{
    public enum TileType
    {
        Floor,
        WestWall,
        NorthWall,
        Corner,
        EastWall,
        SouthWall,
    }

    internal class LevelTile
    {
        private Size dimensions;

        private Bitmap texture;
        static int tileIndex = 0;
        private int index;
        private Point twoDimPoint;

        private TileType type;

        public LevelTile(Point twoDimPoint, int textureIndex, TileType type)
        {
            if (type == TileType.Corner)
            {
                dimensions = new Size(64, 128);
                twoDimPoint.Y -= 64;
                twoDimPoint.X -= 64;
            }
            else
                dimensions = new Size(64, 64);

            this.texture = Model.TileTextures[textureIndex];
            this.type = type;
            this.twoDimPoint = twoDimPoint;
            index = tileIndex++;
        }

        public bool Walkable { get { return type == TileType.Floor ? true : false; } }

        private Point TwoDimPoint
        { get { return twoDimPoint; } }

        public void Draw(Graphics g)
        {
            Point tempPoint = twoDimPoint.TwoDimensionsToIso();
            g.DrawImage(texture, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);
#if DEBUG
            g.FillRectangle(Brushes.MistyRose, twoDimPoint.X, twoDimPoint.Y, dimensions.Width, dimensions.Height);
            FontConverter.FontNameConverter d = new FontConverter.FontNameConverter();
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               16,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            g.DrawString(index.ToString(), font, Brushes.Black, twoDimPoint);
            g.DrawRectangle(Pens.Peru, twoDimPoint.X, twoDimPoint.Y, dimensions.Width, dimensions.Height);
#endif
        }
    }
}