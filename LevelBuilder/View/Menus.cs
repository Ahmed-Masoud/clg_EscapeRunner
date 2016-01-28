using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LevelBuilder
{
    public partial class LevelBuilder
    {
        #region File Menu

        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // setup new map
            if (backup_map.IsDirty(map_width, map_height, tile_width, tile_height, map))//(_tile_library != null && _tile_library.Length > 0))
            {
                DialogResult result = MessageBox.Show("Do you want to save current map?" + Path.GetFileName(current_working_filename) + "?", "Create New Map", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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
                    return;
                }
            }

            MapSetup MapSetupWindow = new MapSetup();
            MapSetupWindow.ParentForm = this;
            MapSetupWindow.LoadFromParentForm();
            MapSetupWindow.Show();
            MapSetupWindow.TopMost = true;

            backup_map.SetMap(map_width, map_height, tile_width, tile_height, map, false);
            saveMapToolStripMenuItem.Enabled = false;
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // save map
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SaveMap(current_working_filename);
                SaveTiles(current_working_filename);
                codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                    tile_library, tile_width, tile_height,
                    tbCode,
                    player, monsters, coins, bullets, bombs);
                codesGenerator.saveCSharp(current_working_filename);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to save: " + current_working_filename + "\n" + ex.Message);
            }

            MessageBox.Show("Map Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void saveMapAsToolStripMenuItem_Click(object sender, EventArgs e)
        {   // save map as
            string initialPath = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Directory.GetCurrentDirectory())) + "\\" + "Levels";

            saveMapDialog.InitialDirectory = initialPath;
            DialogResult saveMap = this.saveMapDialog.ShowDialog();
            if (saveMap == DialogResult.OK)
            {   // save Map
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    SaveMap(this.saveMapDialog.FileName);
                    SaveTiles(this.saveMapDialog.FileName);
                    codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                        tile_library, tile_width, tile_height,
                        tbCode,
                        player, monsters, coins, bullets, bombs);
                    codesGenerator.saveCSharp(current_working_filename);

                    current_working_filename = this.saveMapDialog.FileName;

                    Cursor.Current = Cursors.Default;

                    saveMapToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail to save: " + current_working_filename + "\n" + ex.Message);
                }
            }
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // open saved map
            string path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Directory.GetCurrentDirectory()))) + "\\";
            path = Path.Combine(path, Path.Combine("EscapeRunner", Path.Combine("Res", "Levels")));

            openMapDialog.InitialDirectory = path;
            DialogResult openMap = this.openMapDialog.ShowDialog();
            if (openMap == DialogResult.OK)
            {   // open Map
                string lvFileName = this.openMapDialog.FileName;

                if (Path.GetExtension(lvFileName) != ".lv")
                {
                    MessageBox.Show(lvFileName + " is not a lv file", "Cannot Load File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        OpenMap(this.openMapDialog.FileName);
                        LoadTiles(this.openMapDialog.FileName);

                        RenderTiles();
                        RenderMap();

                        current_working_filename = this.openMapDialog.FileName;

                        // switch to design view
                        tctrlDesign.SelectedTab = tpgDesign;

                        Cursor.Current = Cursors.Default;

                        saveMapToolStripMenuItem.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to open: " + this.openMapDialog.FileName + "\n" + ex.Message);
                    }
                }
            }
        }

        private void exitDLMapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {   // exit map editor
            Application.Exit();
        }

        #endregion File Menu

        #region Edit Menu

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selection.StartDrag.X != -1 && selection.StartDrag.Y != -1 && selection.StopDrag.X != -1 && selection.StopDrag.Y != -1)
            {
                if (clipboard.Data.Count < 1)
                    return;

                redo.Clear();

                if (selection.BottomRightX > map_width)
                    selection.BottomRightX = map_width;
                if (selection.BottomRightY > map_height)
                    selection.BottomRightY = map_height;

                int id = 0;
                if (undo.Count > 0)
                    id = undo.Peek().Id + 1;

                int pasteEndX = selection.TopLeftX + clipboard.Width;
                int pasteEndY = selection.TopLeftY + clipboard.Height;

                if (pasteEndX > map_width)
                    return;
                if (pasteEndY > map_height)
                    return;

                int pos = 0;
                for (int i = selection.TopLeftX; i < pasteEndX; i++)
                {   // copy selected tiles
                    for (int j = selection.TopLeftY; j < pasteEndY; j++)
                    {
                        if (map[i, j] != ((Model.ClipboardNode)clipboard.Data[pos]).Value)
                        {
                            undo.Push(new Model.HistoryNode(id, i, j, map[i, j]));
                            map[i, j] = ((Model.ClipboardNode)clipboard.Data[pos]).Value;
                        }
                        pos++;
                        pos = pos % clipboard.Data.Count;
                    }
                }

                if (undo.Count > 0)
                    undoToolStripMenuItem.Enabled = true;
                else
                    undoToolStripMenuItem.Enabled = false;

                RenderMap();
            }
            else
            {
                MessageBox.Show("No selection", "Paste", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redo.Count < 1)
                return;

            int nodeId = redo.Peek().Id;
            while (redo.Count > 0 && nodeId == redo.Peek().Id)
            {   // redo
                Model.HistoryNode redoo = redo.Pop();
                // save current map for undo
                undo.Push(new Model.HistoryNode(redoo.Id, redoo.MapX, redoo.MapY, map[redoo.MapX, redoo.MapY]));
                // render redo
                map[redoo.MapX, redoo.MapY] = redoo.Value;
            }

            RenderMap();

            undoToolStripMenuItem.Enabled = true;
            if (redo.Count > 0)
                redoToolStripMenuItem.Enabled = true;
            else
                redoToolStripMenuItem.Enabled = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selection.StartDrag.X != -1 && selection.StartDrag.Y != -1 && selection.StopDrag.X != -1 && selection.StopDrag.Y != -1)
            {
                clipboard = new Model.Clipboard();

                if (selection.BottomRightX > map_width)
                    selection.BottomRightX = map_width;
                if (selection.BottomRightY > map_height)
                    selection.BottomRightY = map_height;

                int newX = 0;
                int newY = 0;
                for (int i = selection.TopLeftX; i < selection.BottomRightX; i++)
                {   // copy selected tiles
                    for (int j = selection.TopLeftY; j < selection.BottomRightY; j++)
                    {
                        clipboard.Data.Add(new Model.ClipboardNode(newX, newY, map[i, j]));
                        newY++;
                    }
                    newX++;
                }

                clipboard.Width = selection.BottomRightX - selection.TopLeftX;
                clipboard.Height = selection.BottomRightY - selection.TopLeftY;

                pasteToolStripMenuItem.Enabled = true;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undo.Count < 1)
                return;

            int nodeId = undo.Peek().Id;
            while (undo.Count > 0 && nodeId == undo.Peek().Id)
            {   // undo
                Model.HistoryNode undoo = undo.Pop();
                // save for redo
                redo.Push(new Model.HistoryNode(undoo.Id, undoo.MapX, undoo.MapY, map[undoo.MapX, undoo.MapY]));
                // render undo
                map[undoo.MapX, undoo.MapY] = undoo.Value;
            }

            RenderMap();

            redoToolStripMenuItem.Enabled = true;
            if (undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        #endregion Edit Menu

        #region View Menu

        private void codeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {   // switch to code view
            tctrlDesign.SelectedTab = tpgCode;
            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;

            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode,
                player, monsters, coins, bullets, bombs);

            if (rbCPP.Checked)
                codesGenerator.GenerateCPP();
            else if (rbCS.Checked)
                codesGenerator.GenerateCSharp();
            else if (rbXML.Checked)
                codesGenerator.GenerateXML();
            else
                MessageBox.Show("Please select the language you want to generate", "No Language Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void designViewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {   // switch to design view
            tctrlDesign.SelectedTab = tpgDesign;
            codeViewToolStripMenuItem.Checked = false;
            designViewToolStripMenuItem.Checked = true;
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {   // toggle view grid
            bool isChecked = gridToolStripMenuItem.Checked;
            if (isChecked)
            {
                gridToolStripMenuItem.Checked = false;
                grid_on = false;
            }
            else
            {
                gridToolStripMenuItem.Checked = true;
                grid_on = true;
            }

            RenderMap();
        }

        private void showWalkableToolStripMenuItem_Click(object sender, EventArgs e)
        {   // toggle view walkable
            bool isChecked = showWalkableToolStripMenuItem.Checked;
            if (isChecked)
            {
                showWalkableToolStripMenuItem.Checked = false;
                show_walkable_on = false;
            }
            else
            {
                showWalkableToolStripMenuItem.Checked = true;
                show_walkable_on = true;
            }

            RenderMap();
        }

        private void isometricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            twoDToolStripMenuItem.Checked = false;
            isometricToolStripMenuItem.Checked = true;

            isIsometric = true;
            grid_on = false;
            gridToolStripMenuItem.Checked = false;
            RenderMap();
        }

        private void twoDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isometricToolStripMenuItem.Checked = false;
            twoDToolStripMenuItem.Checked = true;

            isIsometric = false;
            grid_on = true;
            gridToolStripMenuItem.Checked = true;
            RenderMap();
        }

        #endregion View Menu

        #region Select Menu

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = new Controller.SelectionTool();
            selection.StartDrag = new Point(0, 0);
            selection.StopDrag = new Point(map_width, map_height);
            RenderMap();
        }

        private void deselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = new Controller.SelectionTool();
            RenderMap();
        }

        #endregion Select Menu

        #region Tiles Menu

        private void addTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {   // add tiles to tile library
            string path = Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(
                                    Directory.GetCurrentDirectory()))) + "\\";

            path = Path.Combine(path, Path.Combine("EscapeRunner", Path.Combine("Res", "Tiles"))) + "\\";

            AddTiles(path);

            backup_map.IsTileLibraryChanged = true;
        }

        private void clearTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {   // clear tile library
            Cursor.Current = Cursors.WaitCursor;

            if (MessageBox.Show("Clear the Tile Library?", "Clear Tile Library", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ClearTiles();

                RenderTiles();
                RenderMap();
            }

            Cursor.Current = Cursors.Default;
            backup_map.IsTileLibraryChanged = true;
        }

        private void deleteSelectedTileToolStripMenuItem_Click(object sender, EventArgs e)
        {   // delete selected tile
            if (selected_tile == null)
            {
                MessageBox.Show("Select a tile!");
            }
            else
            {
                if (MessageBox.Show("Delete this tile?", "Delete Tile", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteSelectedTile(Convert.ToInt32(selected_tile.Name));
                }
            }
            backup_map.IsTileLibraryChanged = true;
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {   // delete selected tile
            if (MessageBox.Show("Delete this tile?", "Delete Tile", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DeleteSelectedTile(Convert.ToInt32(selected_tile.Name));
                backup_map.IsTileLibraryChanged = true;
            }
        }

        private void toolStripMenuItemWalkable_Click(object sender, EventArgs e)
        {   // toggle walkable tile
            bool isWalkable = !tile_library[Convert.ToInt32(selected_tile.Name)].TileWalkable;
            tile_library[Convert.ToInt32(selected_tile.Name)].TileWalkable = isWalkable;
            cbTileWalkable.Checked = isWalkable;

            RenderMap();
            backup_map.IsTileLibraryChanged = true;
        }

        #endregion Tiles Menu

        #region Tools

        private void btnToolBrush_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.brush);
        }

        private void btnToolEraser_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.eraser);
        }

        private void btnToolFill_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.fill);
        }

        private void btnToolSelectColor_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.selectTile);
        }

        private void btnToolSelection_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.selection);
        }

        #endregion Tools

        #region Generate Code

        private void cArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate C++ codes
            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode,
                player, monsters, coins, bullets, bombs);
            codesGenerator.GenerateCPP();
            tctrlDesign.SelectedTab = tpgCode;
            rbCPP.Checked = true;
        }

        private void cArrayToolStripMenuItem1_Click(object sender, EventArgs e)
        {   // generate C# codes
            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode,
                player, monsters, coins, bullets, bombs);
            codesGenerator.GenerateCSharp();
            tctrlDesign.SelectedTab = tpgCode;
            rbCS.Checked = true;
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate xml
            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode,
                player, monsters, coins, bullets, bombs);
            codesGenerator.GenerateXML();
            tctrlDesign.SelectedTab = tpgCode;
            rbXML.Checked = true;
        }

        private void selectedLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate codes with selected language
            codesGenerator.setCodesGenerator(map, map_name, map_width, map_height,
                tile_library, tile_width, tile_height,
                tbCode,
                player, monsters, coins, bullets, bombs);

            if (rbCPP.Checked)
                codesGenerator.GenerateCPP();
            else if (rbCS.Checked)
                codesGenerator.GenerateCSharp();
            else if (rbXML.Checked)
                codesGenerator.GenerateXML();

            tctrlDesign.SelectedTab = tpgCode;
        }

        #endregion Generate Code

        #region Add Menu

        private void addPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playerCount == 0)
            {
                playerCount++;
                MessageBox.Show("Please Select Player Start Position:", "Player Position", MessageBoxButtons.OK);
                choosingPlayer = true;
                return;
            }
            else
            {
                MessageBox.Show("There is already a player placed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void deletePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playerCount != 0)
            {
                player = new Model.Player();
                playerCount = 0;
                MessageBox.Show("Player Deleted !", "Player Deletion", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("There is no player to delete !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addMonsterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.MapMonster monster = new Model.MapMonster();
            monsters.Add(monster);
            MessageBox.Show("Please Select Monster " + monsters.Count + " Start & End Positions:", "Monster " + monsters.Count + " Position", MessageBoxButtons.OK);
            choosingMonster = true;
            monsters[monsters.Count - 1].Start = true;
        }

        private void deleteMonsterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (monsters.Count > 0)
            {
                makeMonsterDeleteForm();
                //monstersCount--;
            }
            else
            {
                MessageBox.Show("There are no monsters to delete !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void makeMonsterDeleteForm()
        {
            Form f = new Form();
            ListBox lb = new ListBox();

            lb.MouseDoubleClick += (s, e) =>
            {
                int index = lb.IndexFromPoint(e.Location);
                monsters.RemoveAt(index);
                MessageBox.Show("Monster" + (index + 1) + "deleted !", "Monster Deletion", MessageBoxButtons.OK);
                f.Close();
            };

            for (int i = 0; i < monsters.Count; i++)
            {
                lb.Items.Add("Monster " + (i + 1));
            }

            f.Controls.Add(lb);
            f.Size = new Size(300, 200);
            lb.Width = f.Width;
            f.Show();
        }

        private void addBombToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.MapBomb bomb = new Model.MapBomb();
            bombs.Add(bomb);
            MessageBox.Show("Please Select Bomb " + bombs.Count + " Position:", "Bomb " + bombs.Count + " Position", MessageBoxButtons.OK);
            choosingBomb = true;
        }

        private void deleteBombToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bombs.Count > 0)
            {
                makeBombDeleteForm();
            }
            else
            {
                MessageBox.Show("There are no bombs to delete !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void makeBombDeleteForm()
        {
            Form f = new Form();
            ListBox lb = new ListBox();

            lb.MouseDoubleClick += (s, e) =>
            {
                int index = lb.IndexFromPoint(e.Location);
                bombs.RemoveAt(index);
                MessageBox.Show("Bomb" + (index + 1) + "deleted !", "Bomb Deletion", MessageBoxButtons.OK);
                f.Close();
            };

            for (int i = 0; i < bombs.Count; i++)
            {
                lb.Items.Add("Bomb " + (i + 1));
            }

            f.Controls.Add(lb);
            f.Size = new Size(300, 200);
            lb.Width = f.Width;
            f.Show();
        }

        private void addCoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.MapCoinGift coin = new Model.MapCoinGift();
            coins.Add(coin);
            MessageBox.Show("Please Select Coin " + coins.Count + " Position:", "Coin " + coins.Count + " Position", MessageBoxButtons.OK);
            choosingCoin = true;
        }

        private void deleteCoinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (coins.Count > 0)
            {
                makeCoinDeleteForm();
            }
            else
            {
                MessageBox.Show("There are no coins to delete !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void makeCoinDeleteForm()
        {
            Form f = new Form();
            ListBox lb = new ListBox();

            lb.MouseDoubleClick += (s, e) =>
            {
                int index = lb.IndexFromPoint(e.Location);
                coins.RemoveAt(index);
                MessageBox.Show("Coin" + (index + 1) + "deleted !", "Coin Deletion", MessageBoxButtons.OK);
                f.Close();
            };

            for (int i = 0; i < coins.Count; i++)
            {
                lb.Items.Add("Coin " + (i + 1));
            }

            f.Controls.Add(lb);
            f.Size = new Size(300, 200);
            lb.Width = f.Width;
            f.Show();
        }

        private void addBulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.MapBulletGift bullet = new Model.MapBulletGift();
            bullets.Add(bullet);
            MessageBox.Show("Please Select Bullet " + bullets.Count + " Position:", "Bomb " + bullets.Count + " Position", MessageBoxButtons.OK);
            choosingBullet = true;
        }

        private void deleteBulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bullets.Count > 0)
            {
                makeBulletDeleteForm();
            }
            else
            {
                MessageBox.Show("There are no bullets to delete !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void makeBulletDeleteForm()
        {
            Form f = new Form();
            ListBox lb = new ListBox();

            lb.MouseDoubleClick += (s, e) =>
            {
                int index = lb.IndexFromPoint(e.Location);
                bullets.RemoveAt(index);
                MessageBox.Show("Bullet" + (index + 1) + "deleted !", "Bullet Deletion", MessageBoxButtons.OK);
                f.Close();
            };

            for (int i = 0; i < bullets.Count; i++)
            {
                lb.Items.Add("Bullet " + (i + 1));
            }

            f.Controls.Add(lb);
            f.Size = new Size(300, 200);
            lb.Width = f.Width;
            f.Show();
        }

        #endregion Add Menu
    }
}