using EscapeRunner.BusinessLogic;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    internal sealed class BulletAnimation : Animation, ICollide, IPrototype<Animation>
    {
        // List is marked static to avoid loading the resources from the hard disk each time an
        private static readonly List<Bitmap> bulletImages = Model.BulletAnimation;

        private Directions bulletDirection;
        private new int imageIndex = 0;
        private bool needDirection = true;  // Bullets needs a direction and plays
        private Point levelOrigin = new IndexPair(1, 1).IndexesToCoordinates();
        private Point levelEdge = MapLoader.LevelDimensions.IndexesToCoordinates();
        private bool visible = false;

        // Horizontal displacement is bigger because the screen is always horizontally bigger
        private int verticalDisplacement = 30, horizontalDisplacement = 37;

        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                if (value)
                    collider.Active = true;
                else
                    collider.Active = false;
            }
        }

        public BulletAnimation()
        {
            imageIndex = 0;
            if (bulletImages != null)
                ImageCount = bulletImages.Count;

            animationHeight = 20;
            animationWidth = 20;
            this.collider = new Collider(new Rectangle(0, 0, animationWidth, animationHeight));
            Point tempEdge =
                new IndexPair(MapLoader.LevelDimensions.I - 1, MapLoader.LevelDimensions.J - 1).IndexesToCoordinates();
            levelEdge = tempEdge;
        }

        public int ImageCount { get; private set; }

        public bool Locked { get; set; }

        /// <summary>
        /// Draws the bullet and updates to the next animation
        /// </summary>
        /// <param name="g"></param>
        public void DrawBullet(Graphics g)
        {
            // Move the bullet image to the end of the screen / until it collides The bullet object
            // disposes when it hits a wall / end of screen

            // Change animation position because the bullet moves
            SetBulletPlace();

            // Flip the bullet
            if (bulletDirection == Directions.Up || bulletDirection == Directions.Down)
            {
                Bitmap temp = bulletImages[imageIndex];
                DrawFrame(g, RotateAnimation(temp, RotateFlipType.Rotate180FlipX, RotateFlipType.Rotate180FlipX));
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

            // If the bullet is outside the bounds
            if (AnimationPosition.X >= levelEdge.X
                || AnimationPosition.X <= levelOrigin.X
                || AnimationPosition.Y >= levelEdge.Y
                || AnimationPosition.Y <= levelOrigin.Y)
            {
                Visible = false;
            }
        }

        private void SetBulletPlace()
        {
            Point position = AnimationPosition;

            // Get the direction of the shot only once per shot
            if (!Locked)
            {
                bulletDirection = Player.Direction;
                Locked = true;
                visible = true;
            }
            switch (bulletDirection)
            {
                case Directions.Right:
                    position.X += horizontalDisplacement;
                    break;

                case Directions.Left:
                    position.X -= horizontalDisplacement;
                    break;

                case Directions.Up:
                    position.Y -= verticalDisplacement;
                    break;

                case Directions.Down:
                    position.Y += verticalDisplacement;
                    break;
            }
            // Set the bullet's new position
            AnimationPosition = position;
        }

        public bool BulletReachedEnd()
        {
            // The bullet is reads to be redrawn when it reaches the end of the screen or collides
            // with an object
            return needDirection;
        }

        public override string ToString()
        {
            return "bullet";
        }
    }
}