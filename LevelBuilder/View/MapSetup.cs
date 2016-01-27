using System;

//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{
    public partial class MapSetup : Form
    {
#pragma warning disable
        public LevelBuilder ParentForm;
#pragma warning restore

        public MapSetup()
        {
            InitializeComponent();
        }

        public void LoadFromParentForm()
        {   // load some variables from parent form
            try
            {
                nudNewMapWidth.Value = this.ParentForm.nudMapWidth.Value;
                nudNewMapHeight.Value = this.ParentForm.nudMapHeight.Value;
                nudNewTileWidth.Value = this.ParentForm.nudTileWidth.Value;
                nudNewTileHeight.Value = this.ParentForm.nudTileHeight.Value;
                tbNewMapName.Text = this.ParentForm.tbMapName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelNewMap_Click(object sender, EventArgs e)
        {   // cancel create new map
            this.Close();
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
                this.ParentForm.ResetMap();

                this.ParentForm.nudMapWidth.Value = nudNewMapWidth.Value;
                this.ParentForm.nudMapHeight.Value = nudNewMapHeight.Value;
                this.ParentForm.nudTileWidth.Value = nudNewTileWidth.Value;
                this.ParentForm.nudTileHeight.Value = nudNewTileHeight.Value;
                this.ParentForm.tbMapName.Text = tbNewMapName.Text;

                this.ParentForm.ClearTiles();
                this.ParentForm.SetupMap();

                this.Close();
            }
        }
    }
}