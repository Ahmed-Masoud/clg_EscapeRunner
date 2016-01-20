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
        int j, i;
        public IndexPair( int i, int j)
        {
            this.j = j;
            this.i = i;
        }
        public int J { get { return j; } set { j = value; } }
        public int I { get { return i; } set { i = value; } }
    }
}
