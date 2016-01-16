using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{
    public class Tile
    {
        #region private members

        private int id;
        private string name;
        private bool walkable;
        private int width;
        private int height;
        private string path;
        PictureBox pictureBox;

        #endregion

        #region member functions

        public Tile()
        {
            walkable = true;
            width = 0;
            height = 0;
            path = "";
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

        public int TileHeight
        {
            get { return height; }
            set { height = value; }
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

        #endregion
    }
}
