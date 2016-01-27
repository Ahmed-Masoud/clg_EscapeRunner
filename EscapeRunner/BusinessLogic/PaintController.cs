using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic
{
    /// <summary>
    /// Controller partial class to implement the drawing and sort algorithms
    /// </summary>
    public partial class Controller
    {
        private static PointF point = new PointF();
        private static bool increasing = true;

        public static void WindowRefresh(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawGraphics(e.Graphics);
        }

        /// <summary>
        /// This method draws the floor only which causes no problems
        /// </summary>
        /// <param name="g"></param>
        public static Bitmap DrawBackgroundImage()
        {
            Bitmap returnBitmap = new Bitmap(window.Width, window.Height);
            using (Graphics gfx = Graphics.FromImage(returnBitmap))
            {
                gfx.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;

                gfx.DrawImage(Model.Backgrounds[0], 0, 0, MainWindow.RightBound, MainWindow.LowerBound);
                MapLoader.DrawLevelFloor(gfx);
                gfx.Save();
            }
            return returnBitmap;
        }

        public static void UpdateTiles(Graphics g)
        {
            List<IDrawable> allReDrawable = new List<IDrawable>(Controller.ConstantObjects.Count);
            allReDrawable.AddRange(Controller.ConstantObjects);
            //sorting them
            allReDrawable.Sort((p1, p2) => (p1.myPoint.X + p1.myPoint.Y).CompareTo(p2.myPoint.X + p2.myPoint.Y));
            foreach (IDrawable x in allReDrawable)
            {
                x.UpdateGraphics(g);
            }
        }

        public static void DrawMovingBackground(Graphics g)
        {
            g.DrawImage(Model.Backgrounds[0], point.X, point.Y - 30, MainWindow.RightBound, MainWindow.LowerBound + 100);
        }

        public static void DrawShots(Graphics g)
        {
            if (movingObjects.Count > 0)
            {
                // Draw the bullets
                for (int i = 0; i < movingObjects.Count; i++)
                {
                    var temp = movingObjects[i];
                    if (temp is Monster)
                        temp.UpdateGraphics(g);
                    else
                    {
                        if (temp is IWeapon)
                            if (((IWeapon)temp).Used == true)
                                temp.UpdateGraphics(g);
                            // Delete the shot directly if it finished animation
                            else
                            {
                                // Release bullet resources
                                movingObjects.RemoveAt(i);
                                i--;
                                if (movingObjects.Count == 0)
                                    break;
                            }
                    }
                }
            }
        }

        public static void Next()
        {
            if (increasing)
            {
                point.Y = (float)(point.Y + 0.7);
                if (point.Y >= 5)
                {
                    increasing = false;
                }
            }
            else
            {
                point.Y = (float)(point.Y - 0.7);
                if (point.Y <= 0)
                {
                    increasing = true;
                }
            }
        }
    }
}