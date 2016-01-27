using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class BombA : Bomb
    {
        private static readonly List<Bitmap> animation = Model.BombA;
        private int imageIndex = 0;
        private static int imageCount = animation.Count;

        public BombA(IndexPair indexPair)
        {
            IsTaken = false;
            this.indexPair = indexPair;
            isoPoint = IsometricExtensionMethods.IndexesToCorrdinates(indexPair).TwoDimensionsToIso();
            isoPoint.X += 15;
            isoPoint.Y += 15;
        }

        public override void UpdateGraphics(Graphics g)
        {
            g.DrawImage(animation[imageIndex], isoPoint.X, isoPoint.Y, dimension.Width, dimension.Height);
            loadNextImage();
        }

        private void loadNextImage()
        {
            imageIndex++;
            imageIndex %= imageCount;
        }
    }
}