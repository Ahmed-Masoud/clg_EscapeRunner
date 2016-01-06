using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.Animations
{
    public class PlayerAnimation : Animation
    {
        private int imageMaxIndex;

        public PlayerAnimation()
        {
            imageIndex = 0;

            if (animationImages == null)
            {
                animationImages = new List<Bitmap>();
                LoadAnimationFromDisk();
            }
            animationHeight = 50;
            animationWidth = 50;

        }

        public override void LoadAnimationFromDisk()
        {
            // Animation images are included in the base class as a protected List<Bitmap>
            animationImages.Add(Properties.Resources.wingMan1);
            animationImages.Add(Properties.Resources.wingMan2);
            animationImages.Add(Properties.Resources.wingMan3);
            animationImages.Add(Properties.Resources.wingMan4);
            animationImages.Add(Properties.Resources.wingMan5);
            animationImages.Add(Properties.Resources.wingMan6);
            animationImages.Add(Properties.Resources.wingMan7);
            animationImages.Add(Properties.Resources.wingMan8);
            animationImages.Add(Properties.Resources.wingMan9);

            imageMaxIndex = animationImages.Count;
        }
    }
}