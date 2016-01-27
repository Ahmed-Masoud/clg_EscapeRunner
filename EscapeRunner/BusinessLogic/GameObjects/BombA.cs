using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    class BombA : Bomb
    {
        private static readonly List<Bitmap> animation = Model.BombA;

        private static int imageCount = animation.Count;
        public BombA(Point location)
        {
            IsTaken = false;
            this.AnimationPosition = location;
            animationHeight = 32;
            animationWidth = 32;
        }
        public override void AddCollider()
        {
            base.AddCollider();
            this.collider.Collided += Collider_Collided;
        }

        private void Collider_Collided(CollisionEventArgs e)
        {
            if (e.CollidingObject.ToString().Equals("player"))
                this.IsTaken = true;
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
