using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{
    public partial class MapSetup : Form
    {

        public LevelBuilder ParentForm;

        public MapSetup()
        {
            InitializeComponent();
        }

        public void LoadFromParentForm()
        {   // load some variables from parent form
            try
            {
                nudNewMapWidth.Value = ((LevelBuilder)this.ParentForm).nudMapWidth.Value;
                nudNewMapHeight.Value = ((LevelBuilder)this.ParentForm).nudMapHeight.Value;
                nudNewTileWidth.Value = ((LevelBuilder)this.ParentForm).nudTileWidth.Value;
                nudNewTileHeight.Value = ((LevelBuilder)this.ParentForm).nudTileHeight.Value;
                tbNewMapName.Text = ((LevelBuilder)this.ParentForm).tbMapName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCreateNewMap_Click(object sender, EventArgs e)
        {   // create new map
            if (tbNewMapName.Text == "")
            {
                MessageBox.Show("Please provide the map name");
            }
            else if (nudNewMapWidth.Value < 1 || nudNewMapHeight.Value < 1 ||
                nudNewTileWidth.Value < 1 || nudNewTileHeight.Value < 1)
            {
                MessageBox.Show("Please set the appropriate map size");
            }
            else
            {
                ((LevelBuilder)this.ParentForm).ResetMap();

                ((LevelBuilder)this.ParentForm).nudMapWidth.Value = nudNewMapWidth.Value;
                ((LevelBuilder)this.ParentForm).nudMapHeight.Value = nudNewMapHeight.Value;
                ((LevelBuilder)this.ParentForm).nudTileWidth.Value = nudNewTileWidth.Value;
                ((LevelBuilder)this.ParentForm).nudTileHeight.Value = nudNewTileHeight.Value;
                ((LevelBuilder)this.ParentForm).tbMapName.Text = tbNewMapName.Text;

                ((LevelBuilder)this.ParentForm).ClearTiles();
                ((LevelBuilder)this.ParentForm).SetupMap();

                this.Close();
            }
        }

        private void btnCancelNewMap_Click(object sender, EventArgs e)
        {   // cancel create new map
            this.Close();
        }
    }
}
