using System.Drawing;
using System.Collections.Generic;
using System;

namespace EscapeRunner.Animations
{
    public sealed class MonsterAnimation : Animation
    {
        private static List<Bitmap> animationImages;
        public MonsterAnimation() : base(AnimationType.MonsterAnimation)
        {
            imageIndex = 0;
            if (animationImages == null)
            {
                animationImages = Model.MonsterAnimation;
            }
            animationHeight = 64;
            animationWidth = 64;
            objectBounds = new Rectangle(AnimationPosition, new Size(animationWidth, animationHeight));

        }

        public static Size MonsterSize { get; private set; }

        public void Draw(Graphics g, Directions direction)
        {
            Bitmap animationImage = animationImages[imageIndex];
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
                }
                base.DrawFrame(g, animationImage);
            }
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= animationImages.Count;
        }
    }
}