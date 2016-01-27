﻿using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class CoinGift : Gift
    {
        private static readonly List<Bitmap> animation = Model.CoinGift;
        private int imageIndex = 0;
        private static int imageCount = animation.Count;

        public CoinGift(IndexPair indexPair)
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