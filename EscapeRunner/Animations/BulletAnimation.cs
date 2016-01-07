using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace EscapeRunner.Animations
{
    internal sealed class BulletAnimation : Animation
    {
        // This will be used to destroy the bullet when it hits anything
        private CancellationTokenSource cts;

        // List is marked static to avoid loading the resources from the hard disk each time an explosion occurs
        static List<Bitmap> animationImages;

        public BulletAnimation()
        {
            imageIndex = 0;

            if (animationImages == null)
            {
                animationImages = DataSource.LoadExplosionAnimationFromDisk();
            }
            animationHeight = 30;
            animationWidth = 30;

            cts = new CancellationTokenSource();
        }

        public override void LoadNextAnimationImage()
        {
            imageIndex++;
            imageIndex %= animationImages.Count;
        }

        public void ShootBullet()
        {
            // Move the bullet image to the end of the screen / until it collides
            // The bullet object disposes when it hits a wall / end of screen


        }


    }
}