using EscapeRunner.BusinessLogic.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    /// <summary>
    /// Explosion animation is implemented using prototype design pattern
    /// </summary>
    public class ExplosionAnimation : AnimationObject, IPrototype<AnimationObject>, IDrawable
    {
        // List is marked static to avoid loading the resources from the hard disk each time an
        // explosion occurs Load the contents of the explosion animation
        private static readonly List<Bitmap> explosionImages = Model.ExplosionAnimation;
        public bool Active { get; set; }
        public event EventHandler AnimationEnded;
        internal ExplosionAnimation()
        {
            imageIndex = 0;
            if (explosionImages != null)
                ImageCount = explosionImages.Count;

            // Inherited variables
            animationHeight = 35;
            animationWidth = 35;
        }

        public int ImageCount { get; private set; }

        public Point DrawLocation => animationPosition;
        public int ZOrder { get; set; }

        public void ActivateExplosion(Point explosionLocation)
        {
            ZOrder = explosionLocation.X + explosionLocation.Y;
            animationPosition = explosionLocation;
        }

        public void DrawFrame(Graphics g)
        {
            if (!Active)
                return;
            // Draw only one frame of the explosion
            DrawFrame(g, explosionImages[imageIndex]);
            LoadNextAnimationImage();
        }

        /// <summary>
        /// Returns a copy of the object
        /// </summary>
        AnimationObject IPrototype<AnimationObject>.Clone()
        {
            // Ensuring type-safety
            ExplosionAnimation clone = (ExplosionAnimation)this.MemberwiseClone();
            // The clone starts animating from the first frame
            clone.imageIndex = 0;
            return clone;
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= ImageCount;

            if (imageIndex == 0)
                AnimationEnded?.Invoke(this, null);
        }

        public void UpdateGraphics(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}