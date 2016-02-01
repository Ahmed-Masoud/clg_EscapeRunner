using EscapeRunner.BusinessLogic;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    internal sealed class BulletAnimation : AnimationObject, ICollide, IPrototype<AnimationObject>
    {
        // List is marked static to avoid loading the resources from the hard disk each time an
        private static readonly List<Bitmap> bulletImages = Model.BulletAnimation;

        private static Point levelEdge = MapLoader.LevelDimensions.IndexesToCoordinates();
        private static Point levelOrigin = new IndexPair(0, 0).IndexesToCoordinates();
        private Directions bulletDirection;
        private new int imageIndex = 0;

        public event EventHandler BulletOutOfBounds;
        // Horizontal displacement is bigger because the screen
        // is always horizontally bigger
        private int verticalDisplacement = 30, horizontalDisplacement = 37;

        private bool active = false;
        public int ImageCount { get; private set; }

        /// <summary>
        /// States whether the bullet will change directions
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Turns the bullet and collider on or off
        /// </summary>
        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
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

            // Shift bullet bounds
            tempEdge.X -= 30;
            tempEdge.Y -= 30;

            levelEdge = tempEdge;
        }

        /// <summary>
        /// Draws the bullet and updates to the next animation
        /// </summary>
        /// <param name="g"></param>
        public void DrawBullet(Graphics g) 
        {
            if (!Active)
                return;

            // Move the bullet image to the end of the screen / until it collides The bullet object
            // disposes when it hits a wall / end of screen

            // Change animation position because the bullet moves
            MoveBullet();

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
            CheckBulletOutsideBounds();
        }

        AnimationObject IPrototype<AnimationObject>.Clone()
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
        }
        /// <summary>
        /// Checks the location of the bullet and notifies if the bullet is outside playground bounds
        /// </summary>
        private void CheckBulletOutsideBounds()
        {
            Point anPosition = AnimationPosition;
            // If the bullet is outside the bounds
            if (anPosition.X >= levelEdge.X
                || anPosition.X <= levelOrigin.X
                || anPosition.Y >= levelEdge.Y
                || anPosition.Y <= levelOrigin.Y)
            {
                BulletOutOfBounds?.Invoke(this, null);
            }

        }
        public override string ToString()
        {
            return "bullet";
        }

        private void MoveBullet()
        {
            Point position = AnimationPosition;

            // Get the direction of the shot only once per shot
            if (!Locked)
            {
                bulletDirection = Player.Direction;
                Locked = true;
                active = true;
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
    }
}