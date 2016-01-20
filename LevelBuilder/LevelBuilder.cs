using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.IO;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelBuilder
{

    public enum ToolType
    {
        brush = 1,
        selectTile = 2,
        eraser = 3,
        selection = 4,
        fill = 5
    }

    public enum CursorType
    {
        arrow = 1,
        wait = 2,
        brush = 3,
        selectColor = 4,
        eraser = 5,
        selection = 6,
        fill = 7
    }

    public partial class LevelBuilder : Form
    {
        #region private members

        private string current_working_filename;
        private string map_name;

        private int tile_width;
        private int tile_height;

        private int map_width;
        private int map_height;
        private int[,] map;
        private int[,] resize_map;

        private Model.Map backup_map;

        private Stack<Model.HistoryNode> undo;
        private Stack<Model.HistoryNode> redo;

        private Model.Clipboard clipboard;

        private Model.Tile[] tile_library;

        private PictureBox selected_tile;
        private ToolType selected_tool;

        private bool grid_on;
        private bool show_walkable_on;
        private bool isIsometric;
        private bool choosingPlayer;
        private bool choosingMonster;

        private Model.SelectionTool selection;

        private Model.CodesGenerator codesGenerator;

        private int playerCount;
        private int monstersCount;

        private Model.Player player;
        private List<Model.Monster> monsters;

        #endregion

        #region member functions

        public LevelBuilder()
        {
            InitializeComponent();
            Init();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {   // open file
                String lvFileName = Convert.ToString(args[1]);

                if (Path.GetExtension(lvFileName) != ".lv")
                {
                    MessageBox.Show(lvFileName + " is not a lv file", "Cannot Load File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        OpenMap(lvFileName);
                        LoadTiles(lvFileName);

                        RenderTiles();
                        RenderMap();

                        current_working_filename = lvFileName;

                        // switch to design view
                        tctrlDesign.SelectedTab = tpgDesign;

                        Cursor.Current = Cursors.Default;

                        saveMapToolStripMenuItem.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to open: " + lvFileName + "\n" + ex.Message);
                    }
                }
            }
            else
            {
                // reset and render map
                ResetMap();
                RenderMap();
            }
        }

        private void btnUpdateMap_Click(object sender, EventArgs e)
        {   // update map
            if (tbMapName.Text == "")
            {
                MessageBox.Show("Enter the map name", "Update Map Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (tbMapName.Text != map_name || nudMapWidth.Value != map_width || nudMapHeight.Value != map_height || nudTileWidth.Value != tile_width || nudTileHeight.Value != tile_height)
                {
                    DialogResult resizeDialog = MessageBox.Show("Do you want to do these changes? This action cannot be undone", "Update Map?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (resizeDialog != DialogResult.OK)
                    {
                        tbMapName.Text = map_name;
                        nudMapWidth.Value = map_width;
                        nudMapHeight.Value = map_height;
                        nudTileWidth.Value = tile_width;
                        nudTileHeight.Value = tile_height;
                    }
                    else
                    {
                        SetupMap();
                    }
                }
            }
        }

        private void btnUpdateTile_Click(object sender, EventArgs e)
        {   // update tile
            tile_library[Convert.ToInt32(pbSelectedTile.Name)].TileName = tbTileName.Text;
            tile_library[Convert.ToInt32(pbSelectedTile.Name)].TileWalkable = cbTileWalkable.Checked;

            RenderMap();
            backup_map.IsTileLibraryChanged = true;
        }

        private void tilePicBox_MouseClick(object sender, MouseEventArgs e)
        {   // select tile
            selected_tile = (PictureBox)sender;
            lblTileIDValue.Text = selected_tile.Name;
            tbTileName.Text = tile_library[Convert.ToInt32(selected_tile.Name)].TileName;
            cbTileWalkable.Checked = tile_library[Convert.ToInt32(selected_tile.Name)].TileWalkable;
            pbSelectedTile.Image = selected_tile.Image;
            pbSelectedTile.Name = selected_tile.Name;
            gbTileProperties.Enabled = true;

            if (e.Button == MouseButtons.Right)
            {   // show pop up menu
                toolStripMenuItemWalkable.Checked = tile_library[Convert.ToInt32(selected_tile.Name)].TileWalkable;
                contextMenuStripTile.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void mapPicBox_MouseDown(object sender, MouseEventArgs e)
        {   // mouse click over map
            Point myMouse = PointToClient(MousePosition);
            int clickedX = myMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 8;
            int clickedY = myMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 26;

            int mapX = clickedX / tile_width;
            int mapY = clickedY / tile_height;

            if (mapX < 0 || mapY < 0 || mapX >= map_width || mapY >= map_height)
                return;

            this.lblMapCoordinate.Text = "Map Coordinate: " + mapX + ", " + mapY;
            this.lblMouse.Text = "Mouse Position: " + clickedX + ", " + clickedY;

            if (e.Button == MouseButtons.Left)
            {
                if (selected_tool == ToolType.selection)
                {   // selection tool
                    selection = new Model.SelectionTool();
                    selection.IsDragging = true;
                    selection.StartDrag = new Point(mapX, mapY);
                    selection.StopDrag = new Point(mapX, mapY);
                }
                else if (selected_tool == ToolType.brush)
                {   // brush tool
                    if (pnlTileLibrary.Controls.ContainsKey(pbSelectedTile.Name) == true)
                    {
                        if (map[mapX, mapY] != Convert.ToInt32(pbSelectedTile.Name))
                        {
                            redo.Clear();

                            int id = 0;
                            if (undo.Count > 0)
                                id = undo.Peek().Id + 1;

                            undo.Push(new Model.HistoryNode(id, mapX, mapY, map[mapX, mapY]));
                            map[mapX, mapY] = Convert.ToInt32(pbSelectedTile.Name);
                        }
                    }
                }
                else if (selected_tool == ToolType.fill)
                {   // fill tool
                    if (pnlTileLibrary.Controls.ContainsKey(pbSelectedTile.Name) == true)
                    {
                        if (mapX >= selection.TopLeftX && mapX < selection.BottomRightX && mapY >= selection.TopLeftY && mapY < selection.BottomRightY)
                        {
                            redo.Clear();

                            int id = 0;
                            if (undo.Count > 0)
                                id = undo.Peek().Id + 1;

                            if (selection.BottomRightX > map_width)
                                selection.BottomRightX = map_width;
                            if (selection.BottomRightY > map_height)
                                selection.BottomRightY = map_height;

                            for (int i = selection.TopLeftX; i < selection.BottomRightX; i++)
                            {   // fill selected tiles
                                for (int j = selection.TopLeftY; j < selection.BottomRightY; j++)
                                {
                                    undo.Push(new Model.HistoryNode(id, i, j, map[i, j]));
                                    map[i, j] = Convert.ToInt32(pbSelectedTile.Name);
                                }
                            }
                        }
                    }
                }
                else if (selected_tool == ToolType.selectTile)
                {   // select color tool
                    if (map[mapX, mapY] > -1)
                    {
                        Model.Tile selectedTile = tile_library[map[mapX, mapY]];

                        selected_tile = selectedTile.TilePictureBox;
                        lblTileIDValue.Text = selected_tile.Name;
                        tbTileName.Text = selectedTile.TileName;
                        cbTileWalkable.Checked = selectedTile.TileWalkable;
                        pbSelectedTile.Image = selected_tile.Image;
                        pbSelectedTile.Name = selected_tile.Name;
                        gbTileProperties.Enabled = true;
                    }
                    else
                    {
                        ClearSelectedTile();
                    }
                }
                else if (selected_tool == ToolType.eraser)
                {   // eraser tool
                    if (selection.StartDrag.X != -1 && selection.StartDrag.Y != -1 && selection.StopDrag.X != -1 && selection.StopDrag.Y != -1)
                    {   // selection was made
                        if (mapX >= selection.TopLeftX && mapX < selection.BottomRightX && mapY >= selection.TopLeftY && mapY < selection.BottomRightY)
                        {
                            // is in selection
                        }
                        else
                        {
                            return;
                        }
                    }

                    if (map[mapX, mapY] != -1)
                    {
                        redo.Clear();

                        int id = 0;
                        if (undo.Count > 0)
                            id = undo.Peek().Id + 1;

                        undo.Push(new Model.HistoryNode(id, mapX, mapY, map[mapX, mapY]));
                        map[mapX, mapY] = -1;
                    }
                }

                RenderMap();

                if (choosingPlayer)
                {
                    if (player.Start)
                    {
                        player.StartPoint = new Point(mapX, mapY);
                        player.Start = false;
                        player.End = true;
                        return;
                    }
                    if (player.End)
                    {
                        player.EndPoint = new Point(mapX, mapY);
                        player.End = false;
                        choosingPlayer = false;
                        MessageBox.Show("Player Start Point x: " + player.StartPoint.X + ", y: " + player.StartPoint.Y + "\n" +
                                        "Player End Point x: " + player.EndPoint.X + ", y: " + player.EndPoint.Y);
                        return;
                    }
                }

                if (choosingMonster)
                {
                    int index = monstersCount - 1;
                    if (monsters[index].Start)
                    {
                        monsters[index].StartPoint = new Point(mapX, mapY);
                        monsters[index].Start = false;
                        monsters[index].End = true;
                        return;
                    }
                    if (monsters[index].End)
                    {
                        monsters[index].EndPoint = new Point(mapX, mapY);
                        monsters[index].End = false;
                        choosingMonster = false;
                        MessageBox.Show("Monster " + (index + 1) + " Start Point x: " + monsters[index].StartPoint.X + ", y: " + monsters[index].StartPoint.Y + "\n" +
                                        "Monster " + (index + 1) + " End Point x: " + monsters[index].EndPoint.X + ", y: " + monsters[index].EndPoint.Y);
                        return;
                    }
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                selection = new Model.SelectionTool();
                RenderMap();
            }

            if (undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        private void mapPicBox_MouseMove(object sender, MouseEventArgs e)
        {   // mouse move over map
            Point myMouse = PointToClient(MousePosition);
            int clickedX = myMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 8;
            int clickedY = myMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 26;

            int mapX = clickedX / tile_width;
            int mapY = clickedY / tile_height;

            if (selected_tool == ToolType.selection)
            {
                if (mapX < 0)
                    mapX = 0;
                else if (mapY < 0)
                    mapY = 0;

                if (mapX >= map_width)
                    mapX = map_width;
                else if (mapY >= map_height)
                    mapY = map_height;
            }
            else
            {
                if (mapX < 0 || mapY < 0 || mapX >= map_width || mapY >= map_height)
                    return;
            }

            this.lblMapCoordinate.Text = "Map Coordinate: " + mapX + ", " + mapY;
            this.lblMouse.Text = "Mouse Position: " + clickedX + ", " + clickedY;

            if (e.Button == MouseButtons.Left)
            {
                if (selected_tool == ToolType.selection)
                {   // selection tool
                    if (selection.IsDragging)
                    {
                        selection.StopDrag = new Point(mapX, mapY);
                        RenderMap();
                    }
                }
                else if (selected_tool == ToolType.brush)
                {   // brush tool
                    mapPicBox_MouseDown(sender, e);
                }
                else if (selected_tool == ToolType.selectTile)
                {   // select color tool
                    mapPicBox_MouseDown(sender, e);
                }
                else if (selected_tool == ToolType.eraser)
                {   // eraser tool
                    mapPicBox_MouseDown(sender, e);
                }
            }
        }

        private void mapPicBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void mapPicBox_MouseUp(object sender, MouseEventArgs e)
        {   // mouse release over map
            Point myMouse = PointToClient(MousePosition);
            int clickedX = myMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 8;
            int clickedY = myMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 26;

            int mapX = clickedX / tile_width;
            int mapY = clickedY / tile_height;

            if (mapX < 0)
                mapX = 0;
            else if (mapY < 0)
                mapY = 0;

            if (mapX >= map_width)
                mapX = map_width;
            else if (mapY >= map_height)
                mapY = map_height;

            if (selected_tool == ToolType.selection && selection.IsDragging)
            {   // selection tool
                selection.IsDragging = false;
                selection.StopDrag = new Point(mapX, mapY);

                RenderMap();
            }
        }

        private void MapEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                SelectTool(ToolType.selection);
            }
            else if (e.KeyCode == Keys.B)
            {
                SelectTool(ToolType.brush);
            }
            else if (e.KeyCode == Keys.E)
            {
                SelectTool(ToolType.eraser);
            }
            else if (e.KeyCode == Keys.F)
            {
                SelectTool(ToolType.fill);
            }
            else if (e.KeyCode == Keys.T)
            {
                SelectTool(ToolType.selectTile);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (selection.StartDrag.X != -1 && selection.StartDrag.Y != -1 && selection.StopDrag.X != -1 && selection.StopDrag.Y != -1)
                {   // selection was made
                    redo.Clear();

                    int id = 0;
                    if (undo.Count > 0)
                        id = undo.Peek().Id + 1;

                    if (selection.BottomRightX > map_width)
                        selection.BottomRightX = map_width;
                    if (selection.BottomRightY > map_height)
                        selection.BottomRightY = map_height;

                    for (int i = selection.TopLeftX; i < selection.BottomRightX; i++)
                    {   // delete selected tiles
                        for (int j = selection.TopLeftY; j < selection.BottomRightY; j++)
                        {
                            undo.Push(new Model.HistoryNode(id, i, j, map[i, j]));
                            map[i, j] = -1;
                        }
                    }

                    RenderMap();
                }
            }
        }

        private void rbCPP_CheckedChanged(object sender, EventArgs e)
        {   // radio button for XML is selected
            selectedLanguageToolStripMenuItem.Text = "Generate C++";

            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode);

            codesGenerator.GenerateCPP();

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void rbCS_CheckedChanged(object sender, EventArgs e)
        {   // radio button for XML is selected
            selectedLanguageToolStripMenuItem.Text = "Generate C#";

            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode);

            codesGenerator.GenerateCSharp();

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void rbXML_CheckedChanged(object sender, EventArgs e)
        {   // radio button for XML is selected
            selectedLanguageToolStripMenuItem.Text = "Generate XML";

            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode);

            codesGenerator.GenerateXML();

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void tctrlDesign_SelectedIndexChanged(object sender, EventArgs e)
        {   // panel's tab switched
            if (tctrlDesign.SelectedTab.Text == "Designer")
            {
                designViewToolStripMenuItem.Checked = true;
                codeViewToolStripMenuItem.Checked = false;
            }
            else if (tctrlDesign.SelectedTab.Text == "Code")
            {
                codeViewToolStripMenuItem.Checked = true;
                designViewToolStripMenuItem.Checked = false;

                codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode);

                if (rbCPP.Checked)
                    codesGenerator.GenerateCPP();
                else if (rbCS.Checked)
                    codesGenerator.GenerateCSharp();
                else if (rbXML.Checked)
                    codesGenerator.GenerateXML();
            }
        }

        private void MapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {   // exit map editor
            if (backup_map.IsDirty(map_width, map_height, tile_width, tile_height, map))//(_tile_library != null && _tile_library.Length > 0))
            {
                DialogResult result = MessageBox.Show("Do you want to save " + Path.GetFileName(current_working_filename) + "?", "Quit Level Builder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {   // save file before close
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (current_working_filename == "Untitled.lv")
                        {
                            string initialPath = Application.ExecutablePath + "\\" + "Projects";
                            saveMapDialog.InitialDirectory = initialPath;
                            DialogResult saveMap = this.saveMapDialog.ShowDialog();
                            if (saveMap == DialogResult.OK)
                            {   // save Map
                                try
                                {
                                    Cursor.Current = Cursors.WaitCursor;

                                    SaveMap(this.saveMapDialog.FileName);
                                    SaveTiles(this.saveMapDialog.FileName);
                                    //_my_map.SaveMap(this.saveMapDialog.FileName);

                                    Cursor.Current = Cursors.Default;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Fail to save: " + current_working_filename + "\n" + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            SaveMap(current_working_filename);
                            SaveTiles(current_working_filename);
                            //_my_map.SaveMap(_current_working_filename);
                        }

                        Cursor.Current = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fail to save: " + current_working_filename + "\n" + ex.Message);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MapEditor_Resize(object sender, EventArgs e)
        {   // when window is resize, update the design panel's size
            pnlDesign.Width = this.Width - pnlTiles.Width - 10;
            pnlDesign.Height = this.Height - 175;
        }

        private void Init()
        {
            // add event handlers
            pbMap.MouseDown += new MouseEventHandler(mapPicBox_MouseDown);
            pbMap.MouseMove += new MouseEventHandler(mapPicBox_MouseMove);
            pbMap.MouseHover += new EventHandler(mapPicBox_MouseHover);
            pbMap.MouseUp += new MouseEventHandler(mapPicBox_MouseUp);
            this.KeyDown += new KeyEventHandler(MapEditor_KeyDown);
            this.KeyPreview = true;
            FormClosing += new FormClosingEventHandler(MapEditor_FormClosing);

            // create tooltips for tools
            ToolTip toolTips = new ToolTip();
            toolTips.AutoPopDelay = 5000;
            toolTips.InitialDelay = 1000;
            toolTips.ReshowDelay = 500;
            toolTips.ShowAlways = true;
            toolTips.SetToolTip(btnToolSelection, "Selection(S)");
            toolTips.SetToolTip(btnToolBrush, "Brush(B)");
            toolTips.SetToolTip(btnToolEraser, "Eraser(E)");
            toolTips.SetToolTip(btnToolFill, "Fill(F)");
            toolTips.SetToolTip(btnToolSelectTile, "Select Tile(T)");

            // initialized some variables
            grid_on = true;
            show_walkable_on = false;
            isIsometric = false;
            choosingPlayer = false;
            choosingPlayer = false;

            selected_tile = null;
            selection = new Model.SelectionTool();

            // select brush as default tool
            SelectTool(ToolType.selection);

            backup_map = new Model.Map();

            undo = new Stack<Model.HistoryNode>();
            undoToolStripMenuItem.Enabled = false;

            redo = new Stack<Model.HistoryNode>();
            redoToolStripMenuItem.Enabled = false;

            clipboard = new Model.Clipboard();
            pasteToolStripMenuItem.Enabled = false;

            saveMapToolStripMenuItem.Enabled = false;

            tile_library = new Model.Tile[0];

            codesGenerator = new Model.CodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode);

            playerCount = 0;
            monstersCount = 0;

            player = new Model.Player();
            monsters = new List<Model.Monster>();
        }

        #endregion

    }
}
