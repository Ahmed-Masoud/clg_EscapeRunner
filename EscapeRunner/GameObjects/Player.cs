using EscapeRunner.Animations;
using System.Drawing;

namespace EscapeRunner
{
    public class Player
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

                case Directions.UpLeft:
                    newPosition.Y -= deltaVertical;
                    newPosition.X -= deltaHorizontal;
                    break;

                case Directions.UpRight:
                    newPosition.Y -= deltaVertical;
                    newPosition.X += deltaVertical;
                    break;
                case Directions.DownLeft:
                    newPosition.Y += deltaVertical;
                    newPosition.X += deltaVertical;
                    break;
                case Directions.DownRight:
                    newPosition.Y += deltaVertical;
                    newPosition.X += deltaVertical;
                    break;
                default:
                    break;
            }


            // Change the displayed image
            playerAnimation.LoadNextAnimationImage();

            Direction = direction;
            playerAnimation.AnimationPosition = newPosition;
        }

        public void UpdateDrawing(Graphics g)
        {
            playerAnimation.Draw(g);
        }

        //private void UpdatePosition(int value, bool negativeVal,bool isHorizontalAxis = true)
        //{
        //    Point newPosition = playerAnimation.AnimationPosition;
        //    if (isHorizontalAxis)
        //    {
        //        if (value >= 0)
        //            newPosition.X += value;
        //        else
        //            newPosition.X -= value;
        //    }
        //    else
        //    {
        //        if (value >= 0)
        //            newPosition.Y += value;
        //        else
        //            newPosition.Y -= value;
        //    }

        //    // Update the position
        //    playerAnimation.AnimationPosition = newPosition;
        //}

        //private void UpdatePosition(int horizontalValue, int verticalValue)
        //{
        //    Point newPosition = playerAnimation.AnimationPosition;

        //    // Update horizontal value
        //    if (horizontalValue >= 0)
        //        newPosition.X += horizontalValue;
        //    else
        //        newPosition.X -= horizontalValue;

        //    // Update vertical value
        //    if (verticalValue >= 0)
        //        newPosition.Y += verticalValue;
        //    else
        //        newPosition.Y -= verticalValue;

        //    // Update the position
        //    playerAnimation.AnimationPosition = newPosition;
        //}
    }
}