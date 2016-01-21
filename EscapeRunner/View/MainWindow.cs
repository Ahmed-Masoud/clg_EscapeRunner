﻿using EscapeRunner.BusinessLogic;
using System;
using System.Windows.Forms;

namespace EscapeRunner
{
    public partial class MainWindow : Form
    {
        private Timer refreshTimer = new Timer();
        private bool loaded = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Shown += MainWindow_Shown;
            this.FormClosing += MainWindow_FormClosing;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            // Don't start the timer until the objects are initialized
            refreshTimer.Enabled = false;
            refreshTimer.Interval = 20;
            refreshTimer.Tick += new EventHandler(this.refreshTimer_Tick);
        }

        // Event for the MVC Pattern
        public delegate void KeyDownDelegate(ViewNotificationEventArgs x);

        public event KeyDownDelegate ViewNotification;

        public static int LeftBound { get; } = 0;

        public static int LowerBound { get; private set; }

        public static int RightBound { get; private set; }

        public static int UpperBound { get; } = 0;

        /// <summary>
        /// This method fires the notify event
        /// </summary>
        /// <param name="notification"></param>
        protected void NotifyController(Notifing notification)
        {
            if (ViewNotification != null)
            {
                ViewNotification(new ViewNotificationEventArgs(notification));
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                NotifyController(Notifing.Left);
            else if (e.KeyCode == Keys.Right)
                NotifyController(Notifing.Right);
            else if (e.KeyCode == Keys.Up)
                NotifyController(Notifing.Up);
            else if (e.KeyCode == Keys.Down)
                NotifyController(Notifing.Down);
            else if (e.KeyCode == Keys.Space)
                NotifyController(Notifing.Space);
            else if (e.KeyCode == Keys.Escape)
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                }
        }

        // Called on Refresh()

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                Controller.WindowRefresh(sender, e);
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LowerBound = this.Height;
            RightBound = this.Width;

            // Resources are initialized, start ticking
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            await Model.InitializeModelAsync();
            //this.BackgroundImage = await Controller.DrawBackgroundImage();
            Controller.InitializeController();
            if (!this.Focused)
                this.Focus();
            loaded = true;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}