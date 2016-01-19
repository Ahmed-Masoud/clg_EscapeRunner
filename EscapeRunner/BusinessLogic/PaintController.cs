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
        public static Bitmap DrawFloor()
        {
            Bitmap returnBitmap = new Bitmap(Form.ActiveForm.Width, Form.ActiveForm.Height);
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

        public static void DrawGraphics(Graphics g)
        {
            MapLoader.DrawGameFlares(g);
            MapLoader.DrawLevelObstacle(g);
            //MapLoader.DrawLevelFloor(e.Graphics);

            player.UpdateGraphics(g);

            if (drawableObjects.Count > 0)
            {
                // Draw the bullets
                for (int i = 0; i < drawableObjects.Count; i++)
                {
                    var temp = drawableObjects[i];
                    if (((IWeapon)temp).Used == true)
                        temp.UpdateGraphics(g);

                    // Delete the shot directly if it finished animation
                    if (((IWeapon)temp).Used == false)
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

        public static async Task MoveSlowlyAsync(ref Point newPosition, Directions direction, int deltaHorizontal, int deltaVertical)
        {
            //await Task.Run(() => { });
            int counter = 0;
            while (counter < 8)
            {
                switch (direction)
                {
                    case Directions.Up:
                        newPosition.Y -= deltaVertical;
                        break;

                    case Directions.Down:
                        newPosition.Y += deltaVertical;
                        break;

                    case Directions.Left:
                        newPosition.X -= deltaHorizontal;
                        break;

                    case Directions.Right:
                        newPosition.X += deltaVertical;
                        break;
                }
                Thread.Sleep(100);
                counter++;
            }

        }
    }
}