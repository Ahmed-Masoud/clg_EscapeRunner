//using System.Linq;
//using System.Threading.Tasks;

namespace LevelBuilder
{
    public class ClipboardNode
    {
        private int map_x;
        private int map_y;
        private int value;

        public ClipboardNode(int x, int y, int v)
        {
            map_x = x;
            map_y = y;
            value = v;
        }

        public int MapX
        {
            get { return map_x; }
            set { map_x = value; }
        }

        public int MapY
        {
            get { return map_y; }
            set { map_y = value; }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}