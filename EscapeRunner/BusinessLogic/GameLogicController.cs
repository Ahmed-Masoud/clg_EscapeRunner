﻿using EscapeRunner.Animations;
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

        private Controller()
        {
        }

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

            Monster mon = new Monster();
            BulletGift giftaya = new BulletGift(new IndexPair(15, 7));

            CoinGift giftayatanya = new CoinGift(new IndexPair(15, 10).IndexesToCorrdinates());
            //giftayatanya.AddCollider();

            BombA bombaya = new BombA(new IndexPair(15, 4).IndexesToCorrdinates());
            bombaya.AddCollider();
            constantObjects.Add(bombaya);
            ConstantObjects.Add(giftayatanya);
            constantObjects.Add(giftaya);
            movingObjects.Add(player);
            movingObjects.Add(mon);
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

                // TODO play sound
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
                    AudioController.PlayLaserSound();
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
    }
}