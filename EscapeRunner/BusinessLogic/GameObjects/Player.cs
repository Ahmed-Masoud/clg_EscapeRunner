using EscapeRunner.Animations;
using EscapeRunner.View;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    public sealed class Player : IDrawable
    {
        private static PlayerAnimation playerAnimation;
        private static Player playerInstance;
        private static IndexPair playerCoordinates;

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

        public Point DrawLocation { get { return playerAnimation.AnimationPosition; } }

        public void Initialize()
        {
            playerCoordinates = MapLoader.WalkableTiles[0].TileIndecies;
            playerAnimation = (PlayerAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.PlayerAnimation);

            // Initialize the player location to the top of the screen
            playerAnimation.AnimationPosition = playerCoordinates.IndexesToCoordinates();
            playerAnimation.Collider.Collided += Collider_Collided;
            Direction = Directions.Right;
        }

        private void Collider_Collided(CollisionEventArgs e)
        {
        }

        /// <summary>
        /// Position of the player
        /// </summary>
        public void StartMoving(Directions direction)
        {
            Direction = direction;
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
                playerAnimation.AnimationPosition = temp.IndexesToCoordinates();
                Direction = direction;

                if (MapLoader.Level[temp.I, temp.J] == 6)
                {
                    Controller.GameOver(2);
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