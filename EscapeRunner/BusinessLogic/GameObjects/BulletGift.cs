using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class BulletGift : Gift
    {
        private static readonly List<Bitmap> animation = Model.BulletGift;
        private static int imageCount = animation.Count;

        public BulletGift(Point location) : base(location)
        {
            animationHeight = 40;
            animationWidth = 40;
            AnimationPosition = location;
            giftValue = 8;
        }

        public override void AddCollider()
        {
            base.AddCollider();
            this.collider.Collided += Collider_Collided;
        }

        private void Collider_Collided(CollisionEventArgs e)
        {
            if (e.CollidingObject.ToString().Equals("player"))
            {
                AudioController.PlaySmallPowerUp();
                this.ChangeState();
                collider.Active = false;
                Controller.Score += giftValue;
            }
        }

        public override void UpdateGraphics(Graphics g)
        {
            if (CurrentState is GiftStateDead)
                return;

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