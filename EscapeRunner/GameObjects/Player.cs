using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner
{
    public class Player : IDrawable
    {
        private static int windowSideMargin = 70;
        private static int windowButtomMargin = 90;

        private static PlayerAnimation playerAnimation;
        private static Player playerInstance;

        public static Player PlayerInstance
        {
            get { return playerInstance == null ? new Player() : playerInstance; }
            private set { playerInstance = value; }
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

        public Point Position { get { return playerAnimation.AnimationPosition; } }

        /// <summary>
        /// Position of the player
        /// </summary>

        private Player()
        {
            AnimationFactory factory = new AnimationFactory();
            playerAnimation = (PlayerAnimation)factory.CreateAnimation(AnimationType.PlayerAnimation);

            // Initialize the player location to the top of the screen
            playerAnimation.AnimationPosition = new Point(5, 5);
            Direction = Directions.Right;
        }

        public void Move(Directions direction)
        {
            // Move the animation of the player
            if (CanMove(direction))
            {
                Move(direction, Dx, Dy);
            }
        }

        private static bool CanMove(Directions direction)
        {
            // Check if the player reached the bounds of the screen
            switch (direction)
            {
                case Directions.Up:
                    return playerAnimation.AnimationPosition.Y - Dy >= MainWindow.UpperBound ? true : false;

                case Directions.Down:
                    return playerAnimation.AnimationPosition.Y + Dy <= MainWindow.LowerBound - windowButtomMargin ? true : false;

                case Directions.Left:
                    return playerAnimation.AnimationPosition.X - Dx >= MainWindow.LeftBound ? true : false;

                case Directions.Right:
                    return playerAnimation.AnimationPosition.X + Dx <= MainWindow.RightBound - windowSideMargin ? true : false;
            }
            // Invalid case
            return false;
        }

        private void Move(Directions direction, int deltaHorizontal, int deltaVertical)
        {
            // TODO check if the player position is actually changing
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

            // Change the displayed image
            playerAnimation.LoadNextAnimationImage();

            Direction = direction;
            playerAnimation.AnimationPosition = newPosition;
        }

        public void UpdateGraphics(Graphics g)
        {
            playerAnimation.Draw(g, Direction);
        }
    }
}