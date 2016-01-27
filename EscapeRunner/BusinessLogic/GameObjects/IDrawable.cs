using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    /// <summary>
    /// Interface to declare the objects that can be drawn, used to update the GUI easily
    /// </summary>
    public interface IDrawable
    {
        void UpdateGraphics(Graphics g);

        Point myPoint { get; }
    }
}