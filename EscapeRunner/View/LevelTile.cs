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

    public class LevelTile
    {
        private Size dimensions;

        private Bitmap texture;
        private IndexPair tileIndecies;
        private Point twoDimPoint;

        private TileType type;

        public IndexPair TileIndecies { get { return tileIndecies; } }
        public Point Position { get { return twoDimPoint; } }

        public TileType Type { get { return type; } }
        #region Debug Stuff

#if DEBUG
        private static Pen markingPen = new Pen(Color.Turquoise, 2);
        private static Font font = new Font(
           new FontFamily("Arial"),
           8,
           FontStyle.Regular,
           GraphicsUnit.Pixel);
#endif

        #endregion Debug Stuff

        public LevelTile(Point twoDimPoint, int textureIndex, TileType type, IndexPair tileIndecies)
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
            this.tileIndecies = tileIndecies;
        }

        public bool Walkable { get { return type == TileType.Floor ? true : false; } }

        private Point TwoDimPoint
        { get { return twoDimPoint; } }

        /// <summary>
        /// Draw the tile on the screen
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            Point tempPoint = twoDimPoint.TwoDimensionsToIso();
            g.DrawImage(texture, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);

            #region Debug Stuff

#if DEBUG
            // Draws the walkable area of the map
            //g.FillRectangle(Brushes.MistyRose, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);

            g.DrawString($"{TileIndecies.J.ToString()},{TileIndecies.I.ToString()}", font, Brushes.White, twoDimPoint);
            //g.DrawRectangle(markingPen, twoDimPoint.X, twoDimPoint.Y, dimensions.Width, dimensions.Height); // Print 2D
            //g.DrawRectangle(markingPen, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height); // Print ISO
#endif

            #endregion Debug Stuff
        }
    }
}