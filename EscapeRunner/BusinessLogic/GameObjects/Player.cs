using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    public class Player : IDrawable
    {
        private static PlayerAnimation playerAnimation;
        private static Player playerInstance;
        private static int windowButtomMargin = 90;
        private static int windowSideMargin = 70;

        private Player()
        {
            AnimationFactory factory = new AnimationFactory();
            playerAnimation = (PlayerAnimation)factory.GetAnimationCommandResult();

            // Initialize the player location to the top of the screen
            playerAnimation.AnimationPosition = new Point(456, 175);
            Direction = Directions.Right;
        }

        public static Directions Direction { get; set; }

        /// <summary>
        /// Differential movement element in the X direction
        /// </summary>
        public static int Dx { get; } = 64;

        /// <summary>
        /// Differential movement element in the Y direction
        /// </summary>
        public static int Dy { get; } = 64;

        public static Player PlayerInstance
        {
            get { return playerInstance == null ? playerInstance = new Player() : playerInstance; }
            private set { playerInstance = value; }
        }

        public static Point Position { get { return playerAnimation.AnimationPosition; } }

        /// <summary>
        /// Position of the player
        /// </summary>
        public void Move(Directions direction)
        {
            // Move the animation of the player
            if (CanMove(direction))
            {
                Move(direction, Dx, Dy);
            }
        }

        public void UpdateGraphics(Graphics g)
        {
            playerAnimation.Draw(g, Direction);
        }

        private static bool CanMove(Directions direction)
        {
            // Check if the player reached the bounds of the screen
            Point isoPosition = playerAnimation.AnimationPosition.TwoDimensionsToIso();

            Point isoScreenEdge = new Point(MainWindow.RightBound, MainWindow.LowerBound).TwoDimensionsToIso();
            switch (direction)
            {
                case Directions.Right:
                    return isoPosition.Y - Dy >= MainWindow.UpperBound ? true : false;

                case Directions.Left:
                    return isoPosition.Y + Dy <= MainWindow.LowerBound - windowButtomMargin ? true : false;

                case Directions.Up:
                    return isoPosition.X - Dx >= MainWindow.LeftBound ? true : false;

                case Directions.Down:
                    return isoPosition.X + Dx <= MainWindow.RightBound - windowSideMargin ? true : false;
            }
            // Invalid case
            return false;
        }

        /// <summary>
        /// Moves on the 2D Cartesian coordinates, conversion is on checking and drawing only
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="deltaHorizontal"></param>
        /// <param name="deltaVertical"></param>
        private void Move(Directions direction, int deltaHorizontal, int deltaVertical)
        {
            // Get a handle to the player position because we can't change the contents of the points
            Point newPosition = playerAnimation.AnimationPosition;
            switch (direction)
            {
                case Directions.Right:
                    newPosition.Y -= deltaVertical;
                    break;

                case Directions.Left:
                    newPosition.Y += deltaVertical;
                    break;

                case Directions.Up:
                    newPosition.X -= deltaHorizontal;
                    break;

                case Directions.Down:
                    newPosition.X += deltaVertical;
                    break;
            }

            // Change the displayed image ( enable this when the image is constant when the player
            // is idle ) and disable the same method in PlayerAnimation.cs
            playerAnimation.LoadNextAnimationImage();

            Direction = direction;
            playerAnimation.AnimationPosition = newPosition;
        }
    }
}