using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LevelBuilder
{
    public class Clipboard
    {
        private int width;
        private int height;
        private ArrayList data;

        public Clipboard()
        {
            width = 0;
            height = 0;
            data = new ArrayList();
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public ArrayList Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
