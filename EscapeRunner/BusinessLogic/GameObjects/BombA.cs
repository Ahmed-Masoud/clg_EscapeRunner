using System.Collections.Generic;
using System.Drawing;

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
            Controller.GraphicsSynchronizationTimer.Elapsed += GraphicsSynchronizationTimer_Elapsed;
        }

        private void GraphicsSynchronizationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LoadNextAnimationImage();
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
                AudioController.PlayBombExplosion();
                this.IsTaken = true;
                // TODO Game Over
                Controller.GameOver(1);
            }
        }

        public override void UpdateGraphics(Graphics g)
        {
            DrawFrame(g, animation[imageIndex]);
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= imageCount;
        }
    }
}
