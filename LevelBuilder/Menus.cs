﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LevelBuilder
{
    public partial class LevelBuilder
    {
        #region Context Menu Item functions

        #region Menu Tile

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
        #endregion

        #endregion

        #region Menu Item functions

        #region Menu File
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

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // open saved map
            openMapDialog.InitialDirectory = Application.ExecutablePath;
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

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // save map
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SaveMap(current_working_filename);
                SaveTiles(current_working_filename);
                //_my_map.SaveMap(_current_working_filename);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to save: " + current_working_filename + "\n" + ex.Message);
            }
        }

        private void saveMapAsToolStripMenuItem_Click(object sender, EventArgs e)
        {   // save map as
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

                    current_working_filename = this.saveMapDialog.FileName;
                    //tpgNewMap.Text = Path.GetFileNameWithoutExtension(_current_working_filename);

                    Cursor.Current = Cursors.Default;

                    saveMapToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail to save: " + current_working_filename + "\n" + ex.Message);
                }
            }
        }

        private void exitLevelBuilderToolStripMenuItem_Click(object sender, EventArgs e)
        {   // exit map editor
            Application.Exit();
        }
        #endregion

        #region Menu Edit
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undo.Count < 1)
                return;

            int nodeId = undo.Peek().Id;
            while (undo.Count > 0 && nodeId == undo.Peek().Id)
            {   // undo
                HistoryNode undo = this.undo.Pop();
                // save for redo
                redo.Push(new HistoryNode(undo.Id, undo.MapX, undo.MapY, map[undo.MapX, undo.MapY]));
                // render undo
                map[undo.MapX, undo.MapY] = undo.Value;
            }

            RenderMap();

            redoToolStripMenuItem.Enabled = true;
            if (undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redo.Count < 1)
                return;

            int nodeId = redo.Peek().Id;
            while (redo.Count > 0 && nodeId == redo.Peek().Id)
            {   // redo
                HistoryNode redo = this.redo.Pop();
                // save current map for undo
                undo.Push(new HistoryNode(redo.Id, redo.MapX, redo.MapY, map[redo.MapX, redo.MapY]));
                // render redo
                map[redo.MapX, redo.MapY] = redo.Value;

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
                clipboard = new Clipboard();

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
                        clipboard.Data.Add(new ClipboardNode(newX, newY, map[i, j]));
                        newY++;
                    }
                    newX++;
                }

                clipboard.Width = selection.BottomRightX - selection.TopLeftX;
                clipboard.Height = selection.BottomRightY - selection.TopLeftY;

                pasteToolStripMenuItem.Enabled = true;
            }
        }

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
                //if (_selection.BottomRightX > pasteEndX)
                //    pasteEndX = _selection.BottomRightX - ((_selection.BottomRightX - _selection.TopLeftX) % _clipboard.Width);
                //if (_selection.BottomRightY > pasteEndY)
                //    pasteEndY = _selection.BottomRightY - ((_selection.BottomRightY - _selection.TopLeftY) % _clipboard.Height);

                if (pasteEndX > map_width)
                    return;
                if (pasteEndY > map_height)
                    return;

                int pos = 0;
                for (int i = selection.TopLeftX; i < pasteEndX; i++)
                {   // copy selected tiles
                    for (int j = selection.TopLeftY; j < pasteEndY; j++)
                    {
                        if (map[i, j] != ((ClipboardNode)clipboard.Data[pos]).Value)
                        {
                            undo.Push(new HistoryNode(id, i, j, map[i, j]));
                            map[i, j] = ((ClipboardNode)clipboard.Data[pos]).Value;
                        }
                        pos++;
                        pos = pos % clipboard.Data.Count;
                        //pos = pos % ((pasteEndX - i) * _clipboard.Height);
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
        #endregion

        #region Menu View
        private void designViewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {   // switch to design view
            tctrlDesign.SelectedTab = tpgDesign;
            codeViewToolStripMenuItem.Checked = false;
            designViewToolStripMenuItem.Checked = true;
        }

        private void codeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {   // switch to code view
            tctrlDesign.SelectedTab = tpgCode;
            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;

            if (rbCPP.Checked)
                GenerateCArray();
            else if (rbCS.Checked)
                GenerateCSharpArray();
            else if (rbXML.Checked)
                GenerateXML();
            else
                MessageBox.Show("Please select the language you want to generate", "No Language Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        #endregion

        #region Menu Select
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = new SelectionTool();
            selection.StartDrag = new Point(0, 0);
            selection.StopDrag = new Point(map_width, map_height);
            RenderMap();
        }

        private void deselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selection = new SelectionTool();
            RenderMap();
        }
        #endregion

        #region Menu Tiles
        private void addTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {   // add tiles to tile library
            DialogResult loadTiles = this.folderBrowserDialogLoadTiles.ShowDialog();
            if (loadTiles == DialogResult.OK)
            {   // load tiles
                string folderName = this.folderBrowserDialogLoadTiles.SelectedPath;

                AddTiles(folderName);
            }
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

        #endregion

        #region Menu Code
        private void selectedLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate codes with selected language 
            if (rbCPP.Checked)
                GenerateCArray();
            else if (rbCS.Checked)
                GenerateCSharpArray();
            else if (rbXML.Checked)
                GenerateXML();

            tctrlDesign.SelectedTab = tpgCode;
        }

        private void cArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate C++ codes
            GenerateCArray();
            tctrlDesign.SelectedTab = tpgCode;
            rbCPP.Checked = true;
        }

        private void cArrayToolStripMenuItem1_Click(object sender, EventArgs e)
        {   // generate C# codes
            GenerateCSharpArray();
            tctrlDesign.SelectedTab = tpgCode;
            rbCS.Checked = true;
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate xml
            GenerateXML();
            tctrlDesign.SelectedTab = tpgCode;
            rbXML.Checked = true;
        }
        #endregion

        #endregion

        #region Tools

        private void btnToolSelection_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.selection);
        }

        #endregion

    }
}
