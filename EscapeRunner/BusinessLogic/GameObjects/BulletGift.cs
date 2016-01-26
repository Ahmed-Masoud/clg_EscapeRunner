using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    class BulletGift : Gift
    {
        private static readonly List<Bitmap> animation = Model.BulletGift;
        int imageIndex = 0;
        private static int imageCount = animation.Count;
        public BulletGift(IndexPair indexPair)
        {
            this.indexPair = indexPair;
            IsTaken = false;
            isoPoint = IsometricExtensionMethods.IndexesToCorrdinates(indexPair).TwoDimensionsToIso();
            isoPoint.X += 10;
            isoPoint.Y += 10;            
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
