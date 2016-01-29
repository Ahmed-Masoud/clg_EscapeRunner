using EscapeRunner.BusinessLogic;
using EscapeRunner.BusinessLogic.GameObjects;
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

    public class LevelTile : IDrawable
    {
        static Bitmap floorTexture;
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
            if (floorTexture == null)
                floorTexture = Model.TileTextures[0];

            if (type == TileType.Corner)
            {
                dimensions = new Size(64, 128);
                twoDimPoint.Y -= 64;
                twoDimPoint.X -= 64;
            }
            else
                dimensions = new Size(64, 64);

            if (type == TileType.Floor)
                this.texture = null;
            else
                this.texture = Model.TileTextures[textureIndex];

            this.type = type;
            this.twoDimPoint = twoDimPoint;
            this.tileIndecies = tileIndecies;
        }

        public bool Walkable { get { return type == TileType.Floor ? true : false; } }

        private Point TwoDimPoint
        { get { return twoDimPoint; } }

        public Point DrawLocation
        {
            get
            {
                if (type == TileType.Corner)
                {
                    return new Point(TwoDimPoint.X + 64, TwoDimPoint.Y + 64);
                }
                return TwoDimPoint;
            }
        }

        /// <summary>
        /// Draw the tile on the screen
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            Point tempPoint = twoDimPoint.TwoDimensionsToIso();

            // Draw cached ground tile
            if (this.type == TileType.Floor)
                g.DrawImage(floorTexture, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);
            else
                g.DrawImage(texture, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);

            #region Debug Stuff

#if DEBUG
            // Draws the walkable area of the map
            //g.FillRectangle(Brushes.MistyRose, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);

            //g.DrawString($"{TileIndecies.J.ToString()},{TileIndecies.I.ToString()}", font, Brushes.White, twoDimPoint);
            //g.DrawRectangle(markingPen, twoDimPoint.X, twoDimPoint.Y, dimensions.Width, dimensions.Height); // Print 2D
            //g.DrawRectangle(markingPen, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height); // Print ISO
#endif

            #endregion Debug Stuff
        }

        public void UpdateGraphics(Graphics g)
        {
            Point tempPoint = twoDimPoint.TwoDimensionsToIso();
            g.DrawImage(texture, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);
        }
    }
}