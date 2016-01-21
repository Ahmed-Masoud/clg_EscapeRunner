using EscapeRunner.Animations;
using EscapeRunner.View;
using System;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    public sealed class Player : IDrawable, ICollide
    {
        private static PlayerAnimation playerAnimation;
        private static Player playerInstance;
        private static IndexPair playerCoordinates;
        private short stepHorizontalCounter = 0;
        private short stepVerticalCounter = 0;
        int dx = 2;
        int dy = 2;
        int modTwoCounter = 1;
        private Collider collider;
        public static Directions Direction { get; set; }

        public static Player PlayerInstance
        {
            get { return playerInstance == null ? playerInstance = new Player() : playerInstance; }
            private set { playerInstance = value; }
        }

        public static Point Position { get { return playerAnimation.AnimationPosition; } }
        public static IndexPair PlayerCoordiantes { set { playerCoordinates = value; } get { return playerCoordinates; } }

        public IndexPair LocationIndexes
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        Collider ICollide.collider
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        private Player()
        {
            playerCoordinates = MapLoader.WalkableTiles[0].TileIndecies;
            AnimationFactory factory = new AnimationFactory();
            playerAnimation = (PlayerAnimation)factory.GetAnimationCommandResult(AnimationType.PlayerAnimation);

            // Initialize the player location to the top of the screen
            playerAnimation.AnimationPosition = playerCoordinates.IndexesToCorrdinates();

            Direction = Directions.Right;
            //collider = new Collider(playerInstance);  // Null reference EXCEPTION

        }

        /// <summary>
        /// Position of the player
        /// </summary>
        public void StartMoving(Directions direction)
        {
            // %2 to eliminate the double key press
            if (modTwoCounter++ % 2 == 0)
            {
                Move(direction);
                modTwoCounter = 1;
            }
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
            if (MapLoader.IsWalkable(temp))
            {
                playerCoordinates = temp;
                playerAnimation.AnimationPosition = temp.IndexesToCorrdinates();
                Direction = direction;
            }
        }

        public void UpdateGraphics(Graphics g)
        {
            playerAnimation.Draw(g, Direction);
        }
    }
}