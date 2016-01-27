using EscapeRunner.Animations;
using EscapeRunner.View;
using System.Drawing;
using System;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    public sealed class Player : IDrawable
    {
        private static PlayerAnimation playerAnimation;
        private static Player playerInstance;
        private static IndexPair playerCoordinates;

        //private short stepHorizontalCounter = 0;
        //private short stepVerticalCounter = 0;
        //int dx = 2;
        //int dy = 2;
        //private Collider collider;
        public static Directions Direction { get; set; }

        public static Player PlayerInstance
        {
            get { return playerInstance == null ? playerInstance = new Player() : playerInstance; }
            private set { playerInstance = value; }
        }

        /// <summary>
        /// 2D point of the player position
        /// </summary>
        public static Point Position { get { return playerAnimation.AnimationPosition; } }

        public static IndexPair PlayerCoordiantes { set { playerCoordinates = value; } get { return playerCoordinates; } }

        public Point DrawLocation
        {
            get
            {
                return playerAnimation.AnimationPosition;
            }
        }

        public void Initialize()
        {
            playerCoordinates = MapLoader.WalkableTiles[0].TileIndecies;
            playerAnimation = (PlayerAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.PlayerAnimation);

            // Initialize the player location to the top of the screen
            playerAnimation.AnimationPosition = playerCoordinates.IndexesToCorrdinates();
            playerAnimation.Collider.Collided += Collider_Collided;
            Direction = Directions.Right;
        }

        private void Collider_Collided(CollisionEventArgs e)
        {
            //System.Threading.Tasks.Task.Run(() =>
            //{
            //    //System.Threading.Thread.Sleep(100);
            //    //Program.MainWindow.RefreshTimer.Enabled = false;

            //});
        }

        /// <summary>
        /// Position of the player
        /// </summary>
        public void StartMoving(Directions direction)
        {
            Move(direction);
        }

        /// <summary>
        /// Moves on the 2D Cartesian coordinates, conversion is on checking and drawing only
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="deltaHorizontal"></param>
        /// <param name="deltaVertical"></param>
        private void Move(Directions direction)
        {
            IndexPair temp = playerCoordinates;
            switch (direction)
            {
                case Directions.Up:
                    temp.I--;
                    break;

                case Directions.Down:
                    temp.I++;
                    break;

                case Directions.Left:
                    temp.J--;
                    break;

                case Directions.Right:
                    temp.J++;
                    break;

                default:
                    break;
            }

            // Wall detection
            if (MapLoader.IsWalkable(temp) || MapLoader.Level[temp.I, temp.J] == 6 || MapLoader.Level[temp.I, temp.J] == 7)
            {
                playerCoordinates = temp;
                playerAnimation.AnimationPosition = temp.IndexesToCorrdinates();
                Direction = direction;
                if (MapLoader.Level[temp.I, temp.J] == 7)
                {
                    ProjectilePool pool = ProjectilePool.Instance;
                    try
                    {
                        IWeapon y = pool.Acquire(Position, false);
                        System.Windows.Forms.MessageBox.Show("Gift");
                    }
                    catch (InvalidOperationException) { }
                }
                if (MapLoader.Level[temp.I, temp.J] == 6)
                {
                    playerCoordinates = temp;
                    playerAnimation.AnimationPosition = temp.IndexesToCorrdinates();
                    Direction = direction;

                    System.Threading.Tasks.Task.Run(() =>
                    {
                        System.Threading.Thread.Sleep(100);
                        Program.MainWindow.RefreshTimer.Enabled = false;
                        System.Windows.Forms.MessageBox.Show("Game Over");
                    });
                }
            }
        }

        public void UpdateGraphics(Graphics g)
        {
            playerAnimation.Draw(g, Direction);
        }

        private Player()
        {
        }
    }
}