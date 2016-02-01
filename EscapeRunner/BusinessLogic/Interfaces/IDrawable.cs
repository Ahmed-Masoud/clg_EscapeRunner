using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    /// <summary>
    /// Declares the ability to be drawn for GameLogic objects
    /// </summary>
    public interface IDrawable
    {
        void UpdateGraphics(Graphics g);

        Point DrawLocation { get; }
        /// <summary>
        /// Z order
        /// </summary>
        int ZOrder { get; set; }
    }
}