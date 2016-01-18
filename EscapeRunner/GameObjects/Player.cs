using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner.GameObjects
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
        public static int Dx { get; } = 5;

        /// <summary>
        /// Differential movement element in the Y direction
        /// </summary>
        public static int Dy { get; } = 5;

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
            
            Point isoScreenEdge = new Point(MainWindow.RightBound, MainWindow.LowerBound).TwoDimensionsToIso() ;
            switch (direction)
            {
                case Directions.Up:
                    return isoPosition.Y - Dy >= MainWindow.UpperBound ? true : false;

                case Directions.Down:
                    return isoPosition.Y + Dy <= MainWindow.LowerBound - windowButtomMargin ? true : false;

                case Directions.Left:
                    return isoPosition.X - Dx >= MainWindow.LeftBound ? true : false;

                case Directions.Right:
                    return isoPosition.X + Dx <= MainWindow.RightBound - windowSideMargin ? true : false;
            }
            // Invalid case
            return false;
        }

        private void Move(Directions direction, int deltaHorizontal, int deltaVertical)
        {
            // Get a handle to the player position because we can't change the contents of the points
            Point newPosition = playerAnimation.AnimationPosition;
            switch (direction)
            {
                case Directions.Up:
                    newPosition.Y -= deltaVertical;
                    break;

                case Directions.Down:
                    newPosition.Y += deltaVertical;
                    break;

                case Directions.Left:
                    newPosition.X -= deltaHorizontal;
                    break;

                case Directions.Right:
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