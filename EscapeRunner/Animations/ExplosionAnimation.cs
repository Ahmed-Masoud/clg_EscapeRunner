using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    /// <summary>
    /// Explosion animation is implemented using prototype design pattern
    /// </summary>
    public class ExplosionAnimation : Animation, IPrototype<Animation>
    {
        #region Private Fields

        // List is marked static to avoid loading the resources from the hard disk each time an
        // explosion occurs Load the contents of the explosion animation
        private static readonly List<Bitmap> explosionImages = DataSource.LoadExplosionAnimationFromDisk();

        #endregion

        #region Internal Constructors

        internal ExplosionAnimation() : base(AnimationType.ExplosionAnimation)
        {
            imageIndex = 0;
            if (explosionImages != null)
                ImageCount = explosionImages.Count;

            // Inherited variables
            animationHeight = 35;
            animationWidth = 35;
        }

        #endregion

        #region Public Properties

        public int ImageCount { get; private set; }

        #endregion

        #region Public Methods

        public void DrawFrame(Graphics g)
        {
            // Draw only one frame of the explosion
            DrawFrame(g, explosionImages[imageIndex]);
            LoadNextAnimationImage();
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= ImageCount;
        }

        #endregion

        #region Other member methods

        /// <summary>
        /// Returns a copy of the object
        /// </summary>
        Animation IPrototype<Animation>.Clone()
        {
            // Ensuring type-safety
            ExplosionAnimation clone = (ExplosionAnimation)this.MemberwiseClone();
            // The clone starts animating from the first frame
            clone.imageIndex = 0;
            return clone;
        }

        #endregion Other member methods
    }
}