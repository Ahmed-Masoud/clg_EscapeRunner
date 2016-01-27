using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class CoinGift : Gift
    {
        private static readonly List<Bitmap> animation = Model.CoinGift;
        private static int imageCount = animation.Count;

        public CoinGift(Point location)
        {
            location.X += 10;
            location.Y += 10;
            animationWidth = 40;
            animationHeight = 40;
            AnimationPosition = location;
        }
        public override void AddCollider()
        {
            base.AddCollider();
            this.collider.Collided += Collider_Collided;
        }

        private void Collider_Collided(CollisionEventArgs e)
        {
            // Do stuff
        }

        public override void UpdateGraphics(Graphics g)
        {
            DrawFrame(g, animation[imageIndex]);

            LoadNextAnimationImage();
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= imageCount;
        }
    }
}