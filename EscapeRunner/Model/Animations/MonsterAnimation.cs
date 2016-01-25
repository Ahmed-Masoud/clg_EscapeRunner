using EscapeRunner.BusinessLogic;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace EscapeRunner.Animations
{
    public sealed class MonsterAnimation : Animation, ICollide
    {
        private static List<Bitmap> animationImages;

        public MonsterAnimation()
        {
            imageIndex = 0;

            if (animationImages == null)
            {
                animationImages = Model.MonsterAnimation;
            }
            animationHeight = 64;
            animationWidth = 64;

            // Initialize Collider
            this.Collider = new Collider(new Rectangle(AnimationPosition.X, AnimationPosition.Y, animationWidth, animationHeight));
        }

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
        public override string ToString()
        {
            return "monster";
        }
    }
}