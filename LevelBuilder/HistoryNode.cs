//using System.Linq;
//using System.Threading.Tasks;

namespace LevelBuilder
{
    public class HistoryNode
    {
        private int id;
        private int map_x;
        private int map_y;
        private int value;

        public HistoryNode(int id, int x, int y, int v)
        {
            this.id = id;
            map_x = x;
            map_y = y;
            value = v;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
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