using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    public sealed class PlayerAnimation : Animation
    {
        private static List<Bitmap> animationImages;

        public PlayerAnimation() : base(AnimationType.PlayerAnimation)
        {
            imageIndex = 0;

            if (animationImages == null)
            {
                animationImages = DataSource.LoadCharacterAnimationFromDisk();
            }
            animationHeight = 50;
            animationWidth = 50;
        }

        public void Draw(Graphics g, Directions direction)
        {
            Bitmap animationImage = animationImages[imageIndex];
            // Cll the method in a suitable way
            if (animationImages != null)
            {
                switch (direction)
                {
                    case Directions.Up:
                        break;

                    case Directions.Down:
                        break;

                    case Directions.Left:
                        animationImage = RotateAnimation(animationImage, RotateFlipType.RotateNoneFlipX, RotateFlipType.RotateNoneFlipX);
                        break;

                    case Directions.Right:
                        break;

                    default:
                        break;
                }

                // Call the base class method to draw the image
                base.DrawFrame(g, animationImage);

                // Remove this to keep the bird constant
                //LoadNextAnimationImage();
            }
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= animationImages.Count;
        }
    }
}