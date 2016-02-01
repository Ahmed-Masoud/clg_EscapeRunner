using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System.Drawing;
using System.Collections.Generic;
using System.Timers;
using System.Linq;

namespace EscapeRunner.BusinessLogic
{
    /// <summary>
    /// Controller partial class to implement the drawing and sort algorithms
    /// </summary>
    public partial class Controller
    {
        private static PointF backgroundIllusionPoint = new PointF();
        private static bool increasing = true;
        private static List<IDrawable> graphicObjects;

        private static Timer graphicsSynchronizationTimer = new Timer();

        public static Timer GraphicsSynchronizationTimer
        { get { return graphicsSynchronizationTimer; } set { graphicsSynchronizationTimer = value; } }

        public static void WindowRefresh(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (drawGraphics != null)
            {
                drawGraphics(e.Graphics);
                SortGraphics();

                DrawGraphics(g);
                DrawScore(g);
            }
            else
            {
                // End of game
                g.DrawImage(Model.Backgrounds[0], -5, -10, window.Bounds.X, window.Bounds.Y);
                Font font = new Font(new FontFamily("Segoe UI"), 52, FontStyle.Regular, GraphicsUnit.Pixel);
                g.DrawString($"Your Final Score is:{score.ToString()}", font, Brushes.White, new Point(80, 550));
            }
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
        public static void InitializeSortedGraphics()
        {
            graphicObjects = new List<IDrawable>(32);
            // Obstacles are initially added
            graphicObjects.AddRange(MapLoader.ObstacleTiles);

            graphicObjects.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
        }
        public static void SortGraphics()
        {
            for (int i = 0; i < constantObjects.Count; i++)
            {
                var item = constantObjects[i];
                if (item is BombA)
                {
                    if (((Bomb)item).Exploded)
                    {
                        constantObjects.Remove(item);
                        graphicObjects.Remove(item);
                        i--;
                        continue;   // Skip adding the item if it was removed
                    }
                }
                if (!graphicObjects.Contains(item))
                    graphicObjects.Add(item);
            }
            for (int i = 0; i < movingObjects.Count; i++)
            {
                var item = movingObjects[i];
                if (item is Monster)
                    if (!((Monster)item).Alive)
                    {
                        movingObjects.Remove(item);
                        graphicObjects.Remove(item);
                        i--;
                        continue;
                    }

                if (item is IWeapon)
                    if (!((IWeapon)item).Used)
                    {
                        movingObjects.Remove(item);
                        graphicObjects.Remove(item);

                        i--;
                        continue;
                    }

                if (!graphicObjects.Contains(item))
                    graphicObjects.Add(item);
            }

            graphicObjects.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
        }
        public static void DrawGraphics(Graphics g)
        {
            // Ground tiles are drawn first
            foreach (var item in MapLoader.WalkableTiles)
            {
                item.UpdateGraphics(g);
            }
            foreach (var item in graphicObjects)
            {
                item.UpdateGraphics(g);
            }
        }
        //public static void UpdateTiles(Graphics g)
        //{

        //    foreach (IDrawable x in ConstantObjects)
        //    {
        //        if (x is Gift)
        //        {
        //            x.UpdateGraphics(g);
        //        }
        //        else if (x is BombA)
        //        {
        //            if (!((Bomb)x).Exploded)
        //                x.UpdateGraphics(g);
        //        }
        //    }
        //    foreach (LevelTile x in MapLoader.ObstacleTiles)
        //    {
        //        if (x.TileIndecies.I > Player.PlayerCoordiantes.I
        //            || x.TileIndecies.J > Player.PlayerCoordiantes.J
        //            || x.TileIndecies.I > x.Position.X
        //            || x.TileIndecies.J > Player.PlayerCoordiantes.J)
        //            x.Draw(g);
        //    }
        //}

        public static void DrawMovingBackground(Graphics g)
        {
            g.DrawImage(Model.Backgrounds[0], backgroundIllusionPoint.X, backgroundIllusionPoint.Y - 30,
                MainWindow.RightBound, MainWindow.LowerBound + 100);
        }

        //public static void DrawShots(Graphics g)
        //{
        //    if (movingObjects.Count > 0)
        //    {
        //        // Draw the bullets
        //        for (int i = 0; i < movingObjects.Count; i++)
        //        {
        //            var temp = movingObjects[i];
        //            if (temp is Monster)
        //            {
        //                if (((Monster)temp).Alive)
        //                    temp.UpdateGraphics(g);
        //                else
        //                    movingObjects.Remove(temp);
        //            }
        //            else
        //            {
        //                if (temp is IWeapon)
        //                {
        //                    if (((IWeapon)temp).Used == true)
        //                        temp.UpdateGraphics(g);
        //                    // Delete the shot directly if it finished animation
        //                    else
        //                    {
        //                        // Release bullet resources
        //                        movingObjects.RemoveAt(i);
        //                        i--;
        //                        if (movingObjects.Count == 0)
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public static void Next()
        {
            if (increasing)
            {
                backgroundIllusionPoint.Y = (float)(backgroundIllusionPoint.Y + 0.7);
                if (backgroundIllusionPoint.Y >= 5)
                {
                    increasing = false;
                }
            }
            else
            {
                backgroundIllusionPoint.Y = (float)(backgroundIllusionPoint.Y - 0.7);
                if (backgroundIllusionPoint.Y <= 0)
                {
                    increasing = true;
                }
            }
        }
    }
}