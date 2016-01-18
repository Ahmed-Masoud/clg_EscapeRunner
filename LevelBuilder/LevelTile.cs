using System.Drawing;

namespace LevelBuilder
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
        private Point twoDimPoint;
        private TileType type;

        public LevelTile(Point twoDimPoint, TileType type, Bitmap texture)
        {
            dimensions = new Size(40, 38);
            this.type = type;
            this.twoDimPoint = twoDimPoint;
            this.texture = texture;
        }

        public void Draw(Graphics g)
        {
            Point tempPoint = LevelBuilder.twoDToIso(twoDimPoint);
            g.DrawImage(texture, tempPoint.X, tempPoint.Y, dimensions.Width, dimensions.Height);
            
        }
    }
}