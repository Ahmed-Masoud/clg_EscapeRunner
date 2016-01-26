using EscapeRunner.BusinessLogic;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    public sealed class PlayerAnimation : Animation
    {
        private static List<Bitmap> animationImages;

        public PlayerAnimation()
        {
            imageIndex = 0;

            if (animationImages == null)
            {
                animationImages = Model.CharacterAnimation;
            }
            animationHeight = 64;
            animationWidth = 64;

            this.collider = new Collider(new Rectangle(AnimationPosition, new Size(animationWidth, animationHeight)));

            PlayerSize = new Size(animationWidth, animationHeight);
        }

        public static Size PlayerSize { get; private set; }

        public void Draw(Graphics g, Directions direction)
        {
            Bitmap animationImage = animationImages[imageIndex];
            // Call the method in a suitable way
            if (animationImages != null)
            {
                switch (direction)
                {
                    case Directions.Up:
                        //animationImage = RotateAnimation(animationImage, RotateFlipType.RotateNoneFlipX, RotateFlipType.RotateNoneFlipX);
                        break;

                    case Directions.Down:
                        //animationImage = RotateAnimation(animationImage, RotateFlipType.RotateNoneFlipX, RotateFlipType.RotateNoneFlipNone);
                        break;

                    case Directions.Left:
                        //animationImage = RotateAnimation(animationImage, RotateFlipType., RotateFlipType.RotateNoneFlipX);
                        break;
                    case Directions.Right:
                       
                        break;
                }

                // Call the base class method to draw the image
                base.DrawFrame(g, animationImage);
            }
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= animationImages.Count;
        }

        public override string ToString()
        {
            return "player";
        }
    }
}