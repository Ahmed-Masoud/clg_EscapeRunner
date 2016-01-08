﻿using System.Drawing;

namespace EscapeRunner.Animations
{
    /// <summary>
    /// Interface to declare the objects that can be drawn, used to update the GUI easily
    /// </summary>
    internal interface IDrawable
    {
        void UpdateGraphics(Graphics g);
    }
}