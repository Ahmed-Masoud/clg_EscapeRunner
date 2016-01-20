using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic
{
    public struct IndexPair
    {
        int i, j;
        public IndexPair(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
        public int I { get { return i; } set { i = value; } }
        public int J { get { return j; } set { j = value; } }
    }
}
