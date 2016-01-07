using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{
    class Tile
    {
        #region private members

        private int _id;
        private string _name;
        private bool _walkable;
        private int _width;
        private int _height;
        private string _path;
        PictureBox _pictureBox;

        #endregion

        #region member functions

        public Tile()
        {
            _walkable = true;
            _width = 0;
            _height = 0;
            _path = "";
        }

        public int TileID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string TileName
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool TileWalkable
        {
            get { return _walkable; }
            set { _walkable = value; }
        }

        public int TileWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        public int TileHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        public string TilePath
        {
            get { return _path; }
            set { _path = value; }
        }

        public PictureBox TilePictureBox
        {
            get { return _pictureBox; }
            set { _pictureBox = value; }
        }

        #endregion
    }
}
