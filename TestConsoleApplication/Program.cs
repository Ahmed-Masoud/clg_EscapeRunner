using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestConsoleApplication
{
#pragma warning disable

    [Serializable()]
    public struct IndexPair
    {
        private int j, i;

        public int J { get { return j; } set { j = value; } }
        public int I { get { return i; } set { i = value; } }

        public IndexPair(int i, int j)
        {
            this.j = j;
            this.i = i;
        }

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
    }

    internal class Program
    {
        private static int[,] arr = {
            { 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 6, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 7, 7, 7, 7, 7, 5, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 5, 7, 7, 7, 7, 7, 7, 7, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 6},
            { 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 5}
        };

        private static void Main(string[] args)
        {
            /*FileStream wS = new FileStream(@"E:\d.bin", FileMode.Create);
            Wrapper wrapper = new Wrapper(arr, new IndexPair(0, 2));
            try
            {
                wrapper.AddMonster(new IndexPair(5, 7));
            }
            catch (InvalidOperationException exc)
            {
                Console.WriteLine(exc.Message);
            }

            //BinaryWriter d = new BinaryWriter(s);
            BinaryFormatter ds = new BinaryFormatter();
            ds.Serialize(wS, wrapper);

            wS.Close();*/

            //Wrapper readWrappedInfo = new Wrapper();
            BinaryFormatter ds = new BinaryFormatter();
            FileStream rS = new FileStream(@"C:\Users\Amr\Documents\Visual Studio 2015\Projects\EscapeRunner\EscapeRunner\Res\Levels\level1.game", FileMode.Open);
            Object readWrappedInfo = ds.Deserialize(rS);
            rS.Close();
            Console.ReadKey();
        }

        [Serializable()]
        public class Wrapper
        {
            private const int walkableArrayTileNumber = 0;
            private int[,] level;
            private Dictionary<IndexPair, IndexPair> MonsterLocations;
            private IndexPair playerLocation;

            public Wrapper()
            {
            }

            public Wrapper(int[,] level, IndexPair playerLocation)
            {
                this.level = level;
                MonsterLocations = new Dictionary<IndexPair, IndexPair>();
                this.playerLocation = playerLocation;
            }

            public void AddMonster(KeyValuePair<IndexPair, IndexPair> monsterLocation)
            {
                MonsterLocations.Add(monsterLocation.Key, monsterLocation.Value);
            }
        }
    }
}