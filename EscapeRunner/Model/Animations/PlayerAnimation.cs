using EscapeRunner.BusinessLogic;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    public sealed class PlayerAnimation : AnimationObject
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
            Bitmap animationImage = Model.CharacterAnimation[1];
            // Call the method in a suitable way

            switch (direction)
            {
                case Directions.Up:
                    animationImage = Model.CharacterAnimation[1];
                    break;

                case Directions.Down:
                    animationImage = Model.CharacterAnimation[0];
                    break;

                case Directions.Left:
                    animationImage = Model.CharacterAnimation[2];
                    break;

                case Directions.Right:
                    animationImage = Model.CharacterAnimation[3];
                    break;
            }

            // Call the base class method to draw the image
            DrawFrame(g, animationImage);
            LoadNextAnimationImage();
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