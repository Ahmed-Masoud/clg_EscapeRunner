//using System.Linq;
//using System.Threading.Tasks;
using System.Collections;

namespace LevelBuilder
{
    public class Clipboard
    {
        private ArrayList data;
        private int height;
        private int width;

        public Clipboard()
        {
            width = 0;
            height = 0;
            data = new ArrayList();
        }

        public ArrayList Data
        {
            get { return data; }
            set { data = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}