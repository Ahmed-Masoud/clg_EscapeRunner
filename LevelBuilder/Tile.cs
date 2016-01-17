//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{
    public class Tile
    {
        private int height;
        private int id;
        private string name;
        private string path;
        private PictureBox pictureBox;
        private bool walkable;
        private int width;

        public Tile()
        {
            walkable = true;
            width = 0;
            height = 0;
            path = "";
        }

        public int TileHeight
        {
            get { return height; }
            set { height = value; }
        }

        public int TileID
        {
            get { return id; }
            set { id = value; }
        }

        public string TileName
        {
            get { return name; }
            set { name = value; }
        }

        public string TilePath
        {
            get { return path; }
            set { path = value; }
        }

        public PictureBox TilePictureBox
        {
            get { return pictureBox; }
            set { pictureBox = value; }
        }

        public bool TileWalkable
        {
            get { return walkable; }
            set { walkable = value; }
        }

        public int TileWidth
        {
            get { return width; }
            set { width = value; }
        }
    }
}