using EscapeRunner.GameObjects;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    internal sealed class BulletAnimation : Animation, IPrototype<Animation>
    {
        #region Private Fields

        // List is marked static to avoid loading the resources from the hard disk each time an
        // explosion occurs
        private static readonly List<Bitmap> bulletImages = DataSource.LoadBulletClassA();

        private Directions bulletDirection;
        private new int imageIndex;
        private bool needDirection = true;

        public bool Locked { get; set; }

        // Horizontal displacement is bigger because the screen is always horizontally bigger
        private int verticalDisplacement = 9, horizontalDisplacement = 18;

        #endregion

        #region Public Constructors

        public BulletAnimation() : base(AnimationType.BulletAnimation)
        {
            imageIndex = 0;

            if (bulletImages != null)
                ImageCount = bulletImages.Count;

            animationHeight = 30;
            animationWidth = 30;
        }

        #endregion

        #region Public Properties

        public int ImageCount { get; private set; }

        #endregion

        #region Public Methods

        public bool BulletReachedEnd()
        {
            // The bullet is reads to be redrawn when it reaches the end of the screen or collides
            // with an object
            return needDirection;
        }

        public void DrawFrame(Graphics g)
        {
            // Move the bullet image to the end of the screen / until it collides The bullet object
            // disposes when it hits a wall / end of screen

            // Change animation position because the bullet moves
            SetBulletPlace();

            if (bulletDirection == Directions.Up || bulletDirection == Directions.Down)
            {
                Bitmap temp = bulletImages[imageIndex];

                DrawFrame(g, RotateAnimation(temp, RotateFlipType.Rotate90FlipNone, RotateFlipType.Rotate270FlipNone));
            }
            else
            {
                DrawFrame(g, bulletImages[imageIndex]);
            }

            LoadNextAnimationImage();
        }

        Animation IPrototype<Animation>.Clone()
        {
            BulletAnimation clone = (BulletAnimation)this.MemberwiseClone();
            // The clone starts animating from the first frame
            clone.imageIndex = 0;
            return clone;
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= ImageCount;

            // TODO check collision
            if (AnimationPosition.X >= MainWindow.RightBound || AnimationPosition.X <= MainWindow.LeftBound
                || AnimationPosition.Y >= MainWindow.LowerBound || AnimationPosition.Y <= MainWindow.UpperBound)
            {
                needDirection = true;
            }
        }

        #endregion

        #region Private Methods

        private void SetBulletPlace()
        {
            Point position = AnimationPosition;

            // Get the direction of the shot only once per shot

            if (needDirection)
            {
                if (!Locked)
                    bulletDirection = Player.Direction;
                Locked = true;
                needDirection = false;
            }


            switch (bulletDirection)
            {
                case Directions.Up:
                    position.Y += verticalDisplacement;
                    break;

                case Directions.Down:
                    position.Y -= verticalDisplacement;
                    break;

                case Directions.Left:
                    position.X += horizontalDisplacement;
                    break;

                case Directions.Right:
                    position.X -= horizontalDisplacement;
                    break;
            }
            // Set the bullet's new position
            AnimationPosition = position;
        }

        #endregion
    }
}