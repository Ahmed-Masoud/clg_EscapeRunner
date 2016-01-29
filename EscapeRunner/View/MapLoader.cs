using EscapeRunner.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace EscapeRunner.View
{
    public static class MapLoader
    {
        private static int flareCounter = 0;
        private static List<Bitmap> flares;

        //private static int[,] level;

        private static int[,] level;

        private static Point startLocation = new Point(390, -280);
        private static IndexPair levelDimensions;
        private static Point location = startLocation;
        private static List<LevelTile> walkableTiles;
        private static List<LevelTile> obstacleTiles;
        private static Random randomNumberGenerator = new Random();

        private static int monstersCount = -1;
        private static Monster[] monsters;

        private static int bombsCount = -1;
        private static StaticObject[] bombs;

        private static int coinsCount = -1;
        private static StaticObject[] coins;

        private static int bulletsCount = -1;
        private static StaticObject[] bullets;

        private static IndexPair playerStartLocation = new IndexPair(18, 16);

        /// <summary>
        /// Tile types that make the level
        /// </summary>
        private static List<Bitmap> tileBlocks = Model.TileTextures;

        public static int[,] Level { get { return level; } }

        public static List<LevelTile> WalkableTiles { get { return walkableTiles; } }
        public static List<LevelTile> ObstacleTiles { get { return obstacleTiles; } }
        public static Point LevelStartLocation { get { return startLocation; } }
        public static IndexPair PlayerStartLocation { get { return playerStartLocation; } private set { playerStartLocation = value; } }
        public static IndexPair LevelDimensions { get { return levelDimensions; } }

        public static int MonstersCount { get { return monstersCount; } private set { monstersCount = value; } }

        public static Monster[] Monsters { get { return monsters; } private set { monsters = value; } }

        public static int BombsCount { get { return bombsCount; } private set { bombsCount = value; } }

        public static StaticObject[] Bombs { get { return bombs; } private set { bombs = value; } }

        public static int CoinsCount { get { return coinsCount; } private set { coinsCount = value; } }

        public static StaticObject[] Coins { get { return coins; } private set { coins = value; } }

        public static int BulletsCount { get { return bulletsCount; } private set { bulletsCount = value; } }

        public static StaticObject[] Bullets { get { return bullets; } private set { bullets = value; } }

        public static IndexPair MonsterStartLocation
        {
            get
            {
                // Return monster position that's at least 5 tile away from the player
                if (walkableTiles != null)
                    return walkableTiles[randomNumberGenerator.Next(5, (walkableTiles.Count - 1))].TileIndecies;
                throw new InvalidOperationException("Walkable tiles isn't initialized");
            }
        }

        static MapLoader()
        {
            try
            {
                LoadFromFile();
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
            }

            flares = Model.FlareAnimation;

            walkableTiles = new List<LevelTile>(256);
            obstacleTiles = new List<LevelTile>(32);

            int levelRows = level.GetLength(0);
            int levelColomns = level.GetLength(1);
            levelDimensions = new IndexPair(levelRows, levelColomns);
            LoadLevel();
        }

        public static bool IsWalkable(IndexPair pair)
        {
            return level[pair.I, pair.J] == 0 ? true : false;
        }

        /// <summary>
        /// Draw the fire flares underneath the level
        /// </summary>
        /// <param name="g"></param>
        public static void DrawGameFlares(Graphics g)
        {
            g.DrawImage(flares[flareCounter % flares.Count], 67, 376, 46, 104);
            g.DrawImage(flares[flareCounter % flares.Count], 615, 645, 46, 104);
            g.DrawImage(flares[flareCounter++ % flares.Count], 1222, 335, 46, 104);
        }

        public static void DrawLevelFloor(Graphics g)
        {
            // Draw the walkable ground
            foreach (var tile in walkableTiles)
                tile.Draw(g);
        }

        public static void DrawLevelObstacles(Graphics g)
        {
            // Find the player current position, don't draw the walls around it in the loop
            foreach (var tile in obstacleTiles)
                tile.Draw(g);
        }

        public static void LoadLevel()
        {
            for (int i = 0; i < levelDimensions.I; i++)
            {
                for (int j = 0; j < levelDimensions.J; j++)
                {
                    TileType tempType = (TileType)level[i, j];

                    if (tempType == TileType.Floor)
                        walkableTiles.Add(new LevelTile(location, level[i, j], tempType, new IndexPair(i, j)));
                    else
                    {
                        // All obstacle tiles need to be drawn on their own to implement depth sorting
                        obstacleTiles.Add(new LevelTile(location, level[i, j], tempType, new IndexPair(i, j)));
                    }

                    location.X += 32;
                }
                location.Y += 32;
                location.X = startLocation.X;
            }
            location = startLocation;
        }

        public static void LoadFromFile()
        {
            string path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Directory.GetCurrentDirectory())) + "\\";
            path = Path.Combine(path, Path.Combine("Res", "Levels"));
            string[] levelFiles = Directory.GetFiles(path, "*.game");

            path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Directory.GetCurrentDirectory())) + "\\Res\\Levels";
            ReadLevelFile(path + "\\level2.game");
        }

        public static void ReadLevelFile(string folderPath)
        {
            int rows = -1;
            int cols = -1;
            int[,] level;

            IndexPair playerPosition;

            int monstersCount = -1;
            EscapeRunner.View.MapLoader.Monster[] monsters;

            int bombsCount = -1;
            EscapeRunner.View.MapLoader.StaticObject[] bombs;

            int coinsCount = -1;
            EscapeRunner.View.MapLoader.StaticObject[] coins;

            int bulletsCount = -1;
            EscapeRunner.View.MapLoader.StaticObject[] bullets;

            StreamReader reader = new StreamReader(folderPath);

            // read player position
            string line = reader.ReadLine();

            if (line != null)
            {
                int[] points = Array.ConvertAll(line.Substring(24, (line.Length - 26)).Split(','), s => int.Parse(s));

                playerPosition = new IndexPair(points[1], points[0]);

                MapLoader.PlayerStartLocation = playerPosition;
            }
            line = reader.ReadLine();

            // read monsters count
            line = reader.ReadLine();

            if (line != null)
            {
                monstersCount = Convert.ToInt32(line.Substring(20, (line.Length - 21)));
            }
            line = reader.ReadLine();

            // read monsters locations
            if (monstersCount > 0)
            {
                line = reader.ReadLine();

                monsters = new Monster[monstersCount];
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < monstersCount)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (monstersCount - 1))
                            line = line.Substring(0, (line.Length - 2));

                        int[] points = Array.ConvertAll(line.Split(','), s => int.Parse(s));

                        Point start = new Point(points[1], points[0]);
                        Point end = new Point(points[3], points[2]);
                        monsters[counter] = new Monster(start, end);
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                MapLoader.MonstersCount = monstersCount;
                MapLoader.Monsters = monsters;
            }
            line = reader.ReadLine();

            // read bombs locations
            line = reader.ReadLine();

            if (line != null)
            {
                bombsCount = Convert.ToInt32(line.Substring(17, (line.Length - 18)));
            }
            line = reader.ReadLine();

            if (bombsCount > 0)
            {
                line = reader.ReadLine();
                bombs = new StaticObject[bombsCount];

                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < bombsCount)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (bombsCount - 1))
                            line = line.Substring(0, (line.Length - 2));

                        int[] points = Array.ConvertAll(line.Split(','), s => int.Parse(s));

                        Point start = new Point(points[1], points[0]);
                        bombs[counter] = new StaticObject(start);
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                MapLoader.BombsCount = bombsCount;
                MapLoader.Bombs = bombs;
            }
            line = reader.ReadLine();

            // read coins locations
            line = reader.ReadLine();

            if (line != null)
            {
                coinsCount = Convert.ToInt32(line.Substring(17, (line.Length - 18)));
            }
            line = reader.ReadLine();

            if (coinsCount > 0)
            {
                line = reader.ReadLine();
                coins = new StaticObject[coinsCount];

                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < coinsCount)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (coinsCount - 1))
                            line = line.Substring(0, (line.Length - 2));

                        int[] points = Array.ConvertAll(line.Split(','), s => int.Parse(s));

                        Point start = new Point(points[1], points[0]);
                        coins[counter] = new StaticObject(start);
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                MapLoader.CoinsCount = coinsCount;
                MapLoader.Coins = coins;
            }
            line = reader.ReadLine();

            // read bullets locations
            line = reader.ReadLine();

            if (line != null)
            {
                bulletsCount = Convert.ToInt32(line.Substring(19, (line.Length - 20)));
            }
            line = reader.ReadLine();

            if (coinsCount > 0)
            {
                line = reader.ReadLine();
                bullets = new StaticObject[bulletsCount];

                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < bulletsCount)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (bulletsCount - 1))
                            line = line.Substring(0, (line.Length - 2));

                        int[] points = Array.ConvertAll(line.Split(','), s => int.Parse(s));

                        Point start = new Point(points[1], points[0]);
                        bullets[counter] = new StaticObject(start);
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                MapLoader.BulletsCount = bulletsCount;
                MapLoader.Bullets = bullets;
            }
            line = reader.ReadLine();

            // read map dimensions
            line = reader.ReadLine();

            if (line != null)
            {
                cols = Convert.ToInt32(line.Substring(10, (line.Length - 11)));
            }

            line = reader.ReadLine();

            if (line != null)
            {
                rows = Convert.ToInt32(line.Substring(10, (line.Length - 11)));
            }

            // read level
            if (rows > 0 && cols > 0)
            {
                line = reader.ReadLine();
                line = reader.ReadLine();

                level = new int[rows, cols];
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (counter < rows)
                    {
                        line = line.Substring(2, (line.Length - 3));

                        if (counter != (rows - 1))
                            line = line.Substring(0, (line.Length - 2));

                        int counter2 = 0;

                        string[] lines = line.Split(',');

                        foreach (var item in lines)
                        {
                            if (counter2 < cols)
                                level[counter, counter2++] = int.Parse(item);
                        }
                        counter++;
                        //level[counter++] = Array.ConvertAll(line.Split(','), s => int.Parse(s));
                    }
                    else
                    {
                        break;
                    }
                }
                MapLoader.level = level;
            }

            reader.Close();
        }

        public struct Monster
        {
            private Point startPoint;
            private Point endPoint;

            public Point StartPoint
            {
                get { return startPoint; }
                set { startPoint = value; }
            }

            public Point EndPoint
            {
                get { return endPoint; }
                set { endPoint = value; }
            }

            public Monster(Point p1, Point p2)
            {
                this.startPoint = p1;
                this.endPoint = p2;
            }
        }

        public struct StaticObject
        {
            private Point startPoint;

            public Point StartPoint
            {
                get { return startPoint; }
                set { startPoint = value; }
            }

            public StaticObject(Point point)
            {
                this.startPoint = point;
            }
        }
    }
}