using EscapeRunner.Animations;
using EscapeRunner.BusinessLogic.GameObjects;
using EscapeRunner.View;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscapeRunner.BusinessLogic
{
    /// <summary>
    /// Controller partial class to implement the drawing and sort algorithms
    /// </summary>
    internal partial class Controller
    {
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

        private static void DrawGraphics(Graphics g)
        {
            MapLoader.DrawGameFlares(g);
            MapLoader.DrawLevelObstacle(g);
            player.UpdateGraphics(g);
            //MapLoader.DrawLevelObstacle(g);
            UpdateTiles(g);
            //MapLoader.DrawLevelFloor(g);

            

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

        public static void UpdateTiles(Graphics g)
        {
            foreach(LevelTile x in MapLoader.ObstacleTiles)
            {
                if (x.TileIndecies.I>Player.PlayerCoordiantes.I || x.TileIndecies.J > Player.PlayerCoordiantes.J) 
                {
                    x.Draw(g);
                }
            }
            /*IndexPair p = Player.PlayerCoordiantes;
            p.I++;
            if (!MapLoader.IsWalkable(p))
            {
                MapLoader.ObstacleTiles.Find(e => e.TileIndecies == p).Draw(g);

            }
            p.I--;
            p.J++;
            if (!MapLoader.IsWalkable(p))
            {
                MapLoader.ObstacleTiles.Find(e => e.TileIndecies == p).Draw(g);
            }*/

        }
    }
}