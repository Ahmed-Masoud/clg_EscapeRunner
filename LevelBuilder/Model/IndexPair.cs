using System;
using System.Runtime.Serialization;

namespace LevelBuilder.Model
{
#pragma warning disable
    public struct IndexPair
    {
        [DataMember]
        private int j, i;

        public int J { get { return j; } set { j = value; } }
        public int I { get { return i; } set { i = value; } }

        public IndexPair(int i, int j)
        {
            this.j = j;
            this.i = i;
        }

        public override bool Equals(object obj)
        {
            if (obj is IndexPair)
            {
                IndexPair p = (IndexPair)obj;

                if (this.i == p.i && this.j == p.j)
                    return true;
                else
                    return false;
            }
            throw new InvalidCastException();
        }

        // Operator overloads

        public static bool operator ==(IndexPair p1, IndexPair p2)
        {
            if (p1.i == p2.i && p1.j == p2.j)
                return true;
            return false;
        }

        public static bool operator !=(IndexPair p1, IndexPair p2)
        {
            if (p1.i != p2.i || p1.j != p2.j)
                return true;
            return false;
        }
    }
}