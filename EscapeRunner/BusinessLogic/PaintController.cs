using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
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
#if !DEBUG
            DrawMovingBackground(g);
#endif
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
            foreach (LevelTile x in MapLoader.ObstacleTiles)
            {
                foreach (IDrawable y in DrawableObjects)
                {
                    if (y is Player)
                    {
                        IndexPair tempCoordinates = Player.PlayerCoordiantes;
                        if ((x.TileIndecies.I == tempCoordinates.I && x.TileIndecies.J == tempCoordinates.J + 1)
                            || (x.TileIndecies.J == tempCoordinates.J && x.TileIndecies.I == tempCoordinates.I + 1)
                            || (x.TileIndecies.J == tempCoordinates.J + 1 && x.TileIndecies.I == tempCoordinates.I + 1))
                        {
                            ((Player)y).UpdateGraphics(g);
                            x.Draw(g);
                            break;
                        }
                        else if ((x.TileIndecies.I == tempCoordinates.I && x.TileIndecies.J == tempCoordinates.J - 1)
                            || (x.TileIndecies.J == tempCoordinates.J && x.TileIndecies.I == tempCoordinates.I - 1)
                            || (x.TileIndecies.I == tempCoordinates.I - 1 && x.TileIndecies.J == tempCoordinates.J - 1))
                        {
                            x.Draw(g);
                            ((Player)y).UpdateGraphics(g);
                            break;
                        }
                        else
                        {
                            x.Draw(g);
                        }
                    }
                    /*if (y is Monster)
                    {
                        temp = IsometricExtensionMethods.IndexesToCorrdinates(x.TileIndecies);
                        if ((temp.X == ((Monster)y).Position.X && temp.Y == ((Monster)y).Position.Y + 32) || (temp.Y == ((Monster)y).Position.Y && temp.X == ((Monster)y).Position.X + 32) || (temp.X == ((Monster)y).Position.X + 32 && temp.Y == ((Monster)y).Position.Y + 32))
                        {
                            x.Draw(g);
                            ((Monster)y).UpdateGraphics(g);

                            break;
                        }
                        else if ((temp.X == ((Monster)y).Position.X && temp.Y == ((Monster)y).Position.Y - 32) || (temp.Y == ((Monster)y).Position.Y && temp.X == ((Monster)y).Position.X - 32) || (temp.X == ((Monster)y).Position.X - 32 && temp.Y == ((Monster)y).Position.Y - 32))
                        {
                            x.Draw(g);
                            ((Monster)y).UpdateGraphics(g);
                            break;
                        }
                        else x.Draw(g);
                    }*/
                }
            }
        }

        public static void DrawMovingBackground(Graphics g)
        {
            g.DrawImage(Model.Backgrounds[0], point.X, point.Y - 30, MainWindow.RightBound, MainWindow.LowerBound + 100);
        }

        public static void DrawShots(Graphics g)
        {
            if (drawableObjects.Count > 0)
            {
                // Draw the bullets
                for (int i = 0; i < drawableObjects.Count; i++)
                {
                    var temp = drawableObjects[i];
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
                                drawableObjects.RemoveAt(i);
                                i--;
                                if (drawableObjects.Count == 0)
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