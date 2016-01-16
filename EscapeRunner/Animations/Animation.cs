﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace EscapeRunner.Animations
{
    /// <summary>
    /// Moving directions
    /// </summary>
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right,
    }

    public abstract class Animation
    {

        protected int animationWidth, animationHeight;
        protected int imageIndex;
        protected IReciever reciever = new AnimationFactory();
        protected Rectangle objectBounds;


        public Animation(AnimationType animationType)
        {
            reciever.Type = animationType;
        }

        // Trigger an event to allert the bullet that it has been relocated, and set its lock state
        public Point AnimationPosition { get; set; } = Point.Empty;

        /// <summary>
        /// Draws the target animation
        /// </summary>
        public virtual void DrawFrame(Graphics g, Bitmap animationImage)
        {
            g.DrawImage(animationImage, AnimationPosition.X, AnimationPosition.Y, animationWidth, animationHeight);
            objectBounds.Location = AnimationPosition;
        }

        public abstract void LoadNextAnimationImage();

        /// <summary>
        /// Rotates a given animation
        /// </summary>
        /// <param name="image">Target image</param>
        /// <param name="rotation">Desired rotation</param>
        /// <param name="counterRotation">The complement of the desired rotation</param>
        /// <returns>Transformed image</returns>
        public virtual Bitmap RotateAnimation(Bitmap image, RotateFlipType rotation, RotateFlipType counterRotation)
        {
            // The idea lies in rotating the returnBitmap and "drawing" the "image" on the rotated
            // returnBitmap Then returnBitmap is rotated to the complementing ( 360 - rotation )
            // direction so the image seems normal

            Bitmap returnBitmap = new Bitmap(animationWidth, animationHeight);
            //make a graphics object from the empty bitmap
            using (Graphics gx = Graphics.FromImage(returnBitmap))
            {
                ////move rotation point to center of image
                //gx.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
                ////rotate
                //gx.RotateTransform(angle);
                ////move image back
                //gx.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);

                returnBitmap.RotateFlip(rotation);      // ex:Rotate 90

                //draw passed in image onto graphics object
                gx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gx.DrawImage(image, 0, 0, animationWidth, animationHeight);
                returnBitmap.RotateFlip(counterRotation);    // ex:Rotate 270 back
            }

            return returnBitmap;
        }

    }
}