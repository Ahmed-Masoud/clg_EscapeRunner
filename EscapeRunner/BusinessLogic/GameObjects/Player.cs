using EscapeRunner.Animations;
using EscapeRunner.View;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    public sealed class Player : IDrawable
    {
        private static PlayerAnimation playerAnimation;
        private static IndexPair playerCoordinates;
        private static Player playerInstance;
        public static Directions Direction { get; set; }

        /// <summary>
        /// 2D point of the player position
        /// </summary>
        public Point DrawLocation { get { return playerAnimation.AnimationPosition; } }

        public int ZOrder { get; set; } = 4;

        private Player()
        {
        }

        public static Player GetInstance()
        {
            if (playerInstance == null)
            {
                playerInstance = new Player();
                playerInstance.Initialize();
            }
            return playerInstance;
        }

        /// <summary>
        /// Position of the player
        /// </summary>
        public void StartMoving(Directions direction)
        {
            Direction = direction;
            Move(direction);
        }

        public void UpdateGraphics(Graphics g)
        {
            playerAnimation.Draw(g, Direction);
        }

        private void Collider_Collided(CollisionEventArgs e)
        {
        }

        private void GraphicsSynchronizationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Move the player slowly to the next point
        }

        private void Initialize()
        {
            // Start location of the player
            playerCoordinates = MapLoader.PlayerStartLocation;

            playerAnimation = (PlayerAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.PlayerAnimation);

            // Set Z order
            Point tempConverted = playerCoordinates.IndexesToCoordinates();
            playerAnimation.AnimationPosition = tempConverted;
            ZOrder = tempConverted.X + tempConverted.Y;

            playerAnimation.Collider.Collided += Collider_Collided;
            Direction = Directions.Right;
            //Controller.GraphicsSynchronizationTimer.Elapsed += GraphicsSynchronizationTimer_Elapsed;
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
            }

            // Wall detection
            if (MapLoader.IsWalkable(temp))
            {
                playerCoordinates = temp;
                Point tempConverted = temp.IndexesToCoordinates();
                playerAnimation.AnimationPosition = tempConverted;
                playerInstance.ZOrder = tempConverted.X + tempConverted.Y;
                Direction = direction;
            }
        }
    }
}