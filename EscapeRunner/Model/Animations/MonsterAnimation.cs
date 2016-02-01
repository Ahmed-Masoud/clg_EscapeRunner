using EscapeRunner.BusinessLogic;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    public sealed class MonsterAnimation : AnimationObject, ICollide
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
            Controller.GraphicsSynchronizationTimer.Elapsed += GraphicsSynchronizationTimer_Elapsed;
        }

        private void GraphicsSynchronizationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LoadNextAnimationImage();
        }

        public void Draw(Graphics g, Directions direction)
        {
            Bitmap animationImage = animationImages[imageIndex];

            switch (direction)
            {
                case Directions.Up:
                    break;

                case Directions.Down:
                    break;

                case Directions.Left:
                    //animationImage = RotateAnimation(animationImage, RotateFlipType.RotateNoneFlipX, RotateFlipType.RotateNoneFlipX);
                    break;
            }
            base.DrawFrame(g, animationImage);
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