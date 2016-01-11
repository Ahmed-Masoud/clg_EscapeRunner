using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    public class CustomCursor
    {
        private Cursor cursor;

        public CustomCursor(String filename, int xHotSpot, int yHotSpot)
        {
            Bitmap bitmap = new Bitmap(filename);
            cursor = CreateCursor(bitmap, xHotSpot, yHotSpot);

            bitmap.Dispose();
        }

        public Cursor CursorGraphic
        {
            get { return cursor; }
            set { cursor = value; }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(
            ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon,
            ref IconInfo pIconInfo);

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }
    }
}
