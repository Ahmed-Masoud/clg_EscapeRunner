using EscapeRunner.Animations;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace EscapeRunner.BusinessLogic
{
    public partial class Controller
    {
        private delegate void DrawingOperations(Graphics g);

        private static DrawingOperations drawGraphics;

        private static Player player;
        private static List<IDrawable> movingObjects = new List<IDrawable>();
        private static List<IDrawable> constantObjects = new List<IDrawable>();

        private static int score = 0;
        private static int tempScore = 0;
        public static int Score { get { return score; } set { score = value; } }

        private static Font font = new Font(new FontFamily("Segoe UI"), 42, FontStyle.Regular, GraphicsUnit.Pixel);

        public static List<IDrawable> MovingObjects
        {
            get { return movingObjects; }
        }

        public static List<IDrawable> ConstantObjects
        {
            get { return constantObjects; }
        }

        private static ProjectilePool projectilePool;
        private static MainWindow window;

        private static Timer backgroundIllusionTimer = new Timer();

        private Controller() { }

        public static void InitializeController()
        {
            window = Program.MainWindow;

            // Subscribe to the notify event
            window.ViewNotification += Window_ViewNotification;
            player = Player.PlayerInstance;
            player.Initialize();
            // Lazy initialization of projectile pool
            projectilePool = ProjectilePool.Instance;
            projectilePool.Initialize();

            IndexPair start;
            for (int i = 0; i < MapLoader.MonstersCount; i++)
            {
                start = new IndexPair(MapLoader.Monsters[i].StartPoint.X, MapLoader.Monsters[i].StartPoint.Y);
                IndexPair endPoint = new IndexPair(MapLoader.Monsters[i].EndPoint.X, MapLoader.Monsters[i].EndPoint.Y);
                Monster mon = new Monster(start, endPoint);
                movingObjects.Add(mon);
            }

            for (int i = 0; i < MapLoader.BombsCount; i++)
            {
                start = new IndexPair(MapLoader.Bombs[i].StartPoint.X, MapLoader.Bombs[i].StartPoint.Y);
                BombA bomb = new BombA(start.IndexesToCorrdinates());
                bomb.AddCollider();
                constantObjects.Add(bomb);
            }

            for (int i = 0; i < MapLoader.CoinsCount; i++)
            {
                start = new IndexPair(MapLoader.Coins[i].StartPoint.X, MapLoader.Coins[i].StartPoint.Y);
                CoinGift coin = new CoinGift(start.IndexesToCorrdinates());
                coin.AddCollider();
                constantObjects.Add(coin);
            }

            for (int i = 0; i < MapLoader.BulletsCount; i++)
            {
                start = new IndexPair(MapLoader.Bullets[i].StartPoint.X, MapLoader.Bullets[i].StartPoint.Y);
                BulletGift bullet = new BulletGift(start.IndexesToCorrdinates());
                bullet.AddCollider();
                constantObjects.Add(bullet);
            }

            movingObjects.Add(player);
            backgroundIllusionTimer.Interval = 100;
            backgroundIllusionTimer.Elapsed += BackgroundIllusionTimer_Elapsed;
            backgroundIllusionTimer.Enabled = true;

#if !DEBUG

#endif
            drawGraphics += DrawMovingBackground;
            drawGraphics += MapLoader.DrawGameFlares;
            drawGraphics += MapLoader.DrawLevelFloor;
            drawGraphics += MapLoader.DrawLevelObstacles;
            drawGraphics += player.UpdateGraphics;
            drawGraphics += UpdateTiles;
            drawGraphics += DrawShots;
            drawGraphics += DrawScore;

            graphicsSynchronizationTimer.Interval = 100;
            graphicsSynchronizationTimer.Enabled = true;
            graphicsSynchronizationTimer.Elapsed += GraphicsSynchronizationTimer_Elapsed;
        }

        private static void BackgroundIllusionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Next();
        }

        private static void FireBullet()
        {
            try
            {
                // Create a new explosion and add it to the drawable list
                IWeapon projectile = projectilePool.Acquire(Player.Position, false);
                movingObjects.Add((ProjectileClassAlpha)projectile);
                AudioController.PlayLaserSound();
            }
            catch (InvalidOperationException)
            {
                // Out of shots, don't fire
            }
        }

        private static void Window_ViewNotification(ViewNotificationEventArgs e)
        {
            switch (e.Notification)
            {
                case Notifing.Space:
                    FireBullet();

                    break;

                case Notifing.Right:
                    player.StartMoving(Directions.Right);
                    break;

                case Notifing.Left:
                    player.StartMoving(Directions.Left);
                    break;

                case Notifing.Down:
                    player.StartMoving(Directions.Down);
                    break;

                case Notifing.Up:
                    player.StartMoving(Directions.Up);
                    break;

                default:
                    break;
            }
        }
        public static void DrawScore(Graphics g)
        {
            g.DrawString($"{tempScore.ToString("000"),3}", font, Brushes.White, new Point(20, 20));
        }
        private static void GraphicsSynchronizationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Update score animatedly
            if (score > tempScore)
                tempScore++;
        }
        public static void GameOver(int imageNumber)
        {
            AudioController.StopBackgroundSound();
            drawGraphics -= DrawMovingBackground;
            drawGraphics -= MapLoader.DrawGameFlares;
            drawGraphics -= MapLoader.DrawLevelFloor;
            drawGraphics -= MapLoader.DrawLevelObstacles;
            drawGraphics -= player.UpdateGraphics;
            drawGraphics -= UpdateTiles;
            drawGraphics -= DrawShots;
            drawGraphics -= DrawScore;

            window.BackgroundImage = Model.Backgrounds[imageNumber];
            Program.MainWindow.RefreshTimer.Enabled = false;
            graphicsSynchronizationTimer.Enabled = false;
            window.Refresh();
        }
    }
}