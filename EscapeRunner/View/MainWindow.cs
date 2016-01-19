using EscapeRunner.BusinessLogic;
using System;
using System.Windows.Forms;

namespace EscapeRunner
{
    public partial class MainWindow : Form
    {
        private System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();

        public MainWindow()
        {
            InitializeComponent();

            //pictureBox1.Image = Model.BulletAnimation[0];
            //pictureBox1.Width = 100;
            //pictureBox1.Height = 100;
            //pictureBox1.Show();

            this.Shown += MainWindow_Shown;

            this.WindowState = FormWindowState.Maximized;
            //this.UpdateBounds();

            refreshTimer.Enabled = true;
            refreshTimer.Interval = 20;

            //Controller.LazyInitialize();
            refreshTimer.Tick += new EventHandler(this.refreshTimer_Tick);
        }

        // Event for the MVC Pattern
        public delegate void KeyDownDelegate(ViewEventArgs x);

        public event KeyDownDelegate ViewNotification;

        public static int LeftBound { get; } = 0;

        public static int LowerBound { get; private set; }

        public static int RightBound { get; private set; }

        public static int UpperBound { get; } = 0;

        /// <summary>
        /// This method fires the notify event
        /// </summary>
        /// <param name="key"></param>
        protected void NotifyController(ViewKey key)
        {
            if (ViewNotification != null)
            {
                ViewNotification(new ViewEventArgs(key));
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left)
                NotifyController(ViewKey.Left);
            else if (e.KeyData == Keys.Right)
                NotifyController(ViewKey.Right);
            else if (e.KeyData == Keys.Up)
                NotifyController(ViewKey.Up);
            else if (e.KeyData == Keys.Down)
                NotifyController(ViewKey.Down);
            if (e.KeyData == Keys.Space)
                NotifyController(ViewKey.Space);
        }

        // Called on Refresh()

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            Controller.WindowRefresh(sender, e);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LowerBound = this.Height;
            RightBound = this.Width;

            // Fire the event with unknown args, to enter the (default) case in a switch
            //Bitmap backG = new Bitmap(Model.Backgrounds[0], RightBound, LowerBound);
            //this.BackgroundImage = backG;

            this.BackgroundImage = Controller.DrawFloor();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void MainWindow_Click(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show($"Mouse click coordinates x:{MousePosition.X}, y:{MousePosition.Y}");
#endif
        }
    }
}