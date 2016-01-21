using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System.Drawing;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic
{

    /// <summary>
    /// Controller partial class to implement the drawing and sort algorithms
    /// </summary>
    public partial class Controller
    {
        static PointF point = new PointF();
        static bool increasing = true;

        public static void WindowRefresh(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
#if !DEBUG
            DrawMovingBackground(g);
            drawGraphics(e.Graphics);
#else
            MapLoader.DrawLevelObstacle(g);
            player.UpdateGraphics(g);
#endif

        }

        /// <summary>
        /// This method draws the floor only which causes no problems
        /// </summary>
        /// <param name="g"></param>
        public static async Task<Bitmap> DrawBackgroundImage()
        {
            Bitmap returnBitmap = new Bitmap(window.Width, window.Height);
            await Task.Run(() =>
            { // Used window ( Forms.ActiveForm ) didn't work as the main window wasn't yet set as the ActiveForm

                using (Graphics gfx = Graphics.FromImage(returnBitmap))
                {
                    gfx.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;

                    gfx.DrawImage(Model.Backgrounds[0], 0, 0, MainWindow.RightBound, MainWindow.LowerBound);
                    MapLoader.DrawLevelFloor(gfx);
                    gfx.Save();
                }
            });
            return returnBitmap;
        }

        public static void UpdateTiles(Graphics g)
        {

            foreach (LevelTile x in MapLoader.ObstacleTiles)
                if (x.TileIndecies.I > Player.PlayerCoordiantes.I || x.TileIndecies.J > Player.PlayerCoordiantes.J)
                    x.Draw(g);
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
                point.Y = (float)(point.Y + 0.2);
                if (point.Y >= 5)
                {
                    increasing = false;
                }
            }
            else
            {
                point.Y = (float)(point.Y - 0.2);
                if (point.Y <= 0)
                {
                    increasing = true;
                }
            }
        }

    }
}