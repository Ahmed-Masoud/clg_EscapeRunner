using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace LevelBuilder
{
    public partial class LevelBuilder
    {
        public void OpenMap(string fileName)
        {   // open saved map
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            XmlTextReader reader = new XmlTextReader(fileStream);

            int row = -1;
            int col = -1;

            // parse file and read each node
            while (reader.Read())
            {
                if (reader.NodeType.ToString() == "Element")
                {   // element node
                    if (reader.Name == "LVMap")
                    {   // header node
                        map_name = reader.GetAttribute("MapName");
                        map_width = Convert.ToInt32(reader.GetAttribute("MapWidth"));
                        map_height = Convert.ToInt32(reader.GetAttribute("MapHeight"));
                        tile_width = Convert.ToInt32(reader.GetAttribute("TileWidth"));
                        tile_height = Convert.ToInt32(reader.GetAttribute("TileHeight"));

                        tbMapName.Text = map_name;
                        nudMapWidth.Value = map_width;
                        nudMapHeight.Value = map_height;
                        nudTileWidth.Value = tile_width;
                        nudTileHeight.Value = tile_height;

                        map = new int[map_width, map_height];

                        // initialized _map
                        for (int x = 0; x < map_width; x++)
                            for (int y = 0; y < map_height; y++)
                                map[x, y] = -1;
                    }
                    else if (reader.Name == "Row")
                    {
                        row = Convert.ToInt32(reader.GetAttribute("Position"));
                    }
                    else if (reader.Name == "Column")
                    {
                        col = Convert.ToInt32(reader.GetAttribute("Position"));
                    }
                }
                else if (reader.NodeType.ToString() == "Text")
                {
                    map[col, row] = int.Parse(reader.Value);
                }
            }

            reader.Close();

            ClearSelectedTile();
            backup_map.SetMap(map_width, map_height, tile_width, tile_height, map, false);
        }

        public void RenderMap()
        {   // render map

            if (isIsometric)
            {
                pbMap.Width = (tile_width * map_width) + 270;
                pbMap.Height = (tile_height * map_height);
            }
            else
            {
                pbMap.Width = tile_width * map_width;
                pbMap.Height = tile_height * map_height;
            }
            
            Bitmap bmp = new Bitmap(pbMap.Width, pbMap.Height);
            pbMap.Image = bmp;
            pbMapSmall.Image = bmp;

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

            if (isIsometric)
            {
                Point startLocation = new Point(270, -100);
                Point location = startLocation;

                for (int x = 0; x < map_width; x++)
                {
                    for (int y = 0; y < map_height; y++)
                    {
                        if (map[x, y] != -1)
                        {
                            Model.TileType tileType = (Model.TileType)map[x, y];
                            Model.LevelTile levelTile = new Model.LevelTile(location, tileType, (Bitmap)tile_library[map[x, y]].TilePictureBox.Image);
                            location.X += 20;

                            levelTile.Draw(gfx);
                        }
                    }
                    location.Y += 20;
                    location.X = startLocation.X;
                }
            }
            else
            {
                for (int x = 0; x < map_width; x++)
                {
                    for (int y = 0; y < map_height; y++)
                    {
                        if (map[x, y] != -1)
                        {
                            if (tile_library != null && tile_library.Length > map[x, y])
                            {
                                PictureBox tile = tile_library[map[x, y]].TilePictureBox;
                                if (tile != null)
                                {
                                    gfx.DrawImage(tile.Image, x * tile_width, y * tile_height, tile_width, tile_height);

                                    if (show_walkable_on && !tile_library[map[x, y]].TileWalkable)
                                    {   // show walkble tile
                                        gfx.FillRectangle(new SolidBrush(Color.FromArgb(128, 50, 50, 255)), x * tile_width + 2, y * tile_height + 2, tile_width - 4, tile_height - 4);
                                    }
                                }
                            }
                        }
                    }
                }
            }



            if (map_width > map_height)
            {
                int mh = map_height * 150 / map_width;
                pbMapSmall.Width = 150;
                pbMapSmall.Height = mh;
            }
            else if (map_width < map_height)
            {
                int mw = map_width * 150 / map_height;
                pbMapSmall.Width = mw;
                pbMapSmall.Height = 150;
            }
            else
            {
                pbMapSmall.Width = 150;
                pbMapSmall.Height = 150;
            }

            pbMapSmall.Refresh();

            if (grid_on)
            {   // draw grids
                for (int a = 1; a < map_width; a++)
                {   // draw vertical lines
                    gfx.DrawLine(Pens.Gray, a * tile_width, 0, a * tile_width, map_height * tile_height);
                }

                for (int b = 1; b < map_height; b++)
                {   // draw horizontal lines

                    gfx.DrawLine(Pens.Gray, 0, b * tile_height, map_width * tile_width, b * tile_height);
                }
            }

            if (selection.BottomRightX > map_width)
                selection.BottomRightX = map_width;
            if (selection.BottomRightY > map_height)
                selection.BottomRightY = map_height;

            Rectangle marquee = new Rectangle(selection.TopLeftX * tile_width,
                                    selection.TopLeftY * tile_height,
                                    (selection.BottomRightX - selection.TopLeftX) * tile_width,
                                    (selection.BottomRightY - selection.TopLeftY) * tile_height);

            Pen mypen = new Pen(new SolidBrush(Color.Blue));
            mypen.Color = Color.Blue;
            mypen.Width = 2;
            mypen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
            mypen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            gfx.DrawRectangle(mypen, marquee);
            gfx.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Aqua)), marquee);

            gfx.Dispose();
            pbMap.Refresh();

        }

        public void ResetMap()
        {   // reset map
            current_working_filename = "Untitled.lv";
            map_name = "Untitled";
            tbMapName.Text = map_name;

            tile_width = 30;
            tile_height = 25;
            map_width = 18;
            map_height = 20;

            nudMapWidth.Value = map_width;
            nudMapHeight.Value = map_height;
            nudTileWidth.Value = tile_width;
            nudTileHeight.Value = tile_height;

            map = new int[map_width, map_height];

            if (tile_library != null)
            {
                Array.Clear(tile_library, 0, tile_library.Length);
                Array.Resize(ref tile_library, 0);
            }

            // initialized _map
            for (int x = 0; x < map_width; x++)
                for (int y = 0; y < map_height; y++)
                    map[x, y] = -1;

            pbMap.Width = tile_width * map_width;
            pbMap.Height = tile_height * map_height;

            ClearSelectedTile();
            backup_map.SetMap(map_width, map_height, tile_width, tile_height, map, false);
        }

        public void ResizeMap(int newWidth, int newHeight)
        {   // resize map
            resize_map = new int[newWidth, newHeight];

            // initialized _resize_map
            for (int x = 0; x < newWidth; x++)
                for (int y = 0; y < newHeight; y++)
                    resize_map[x, y] = -1;

            int w = 0;
            int h = 0;

            if (newWidth < map_width)
                w = newWidth;
            else
                w = map_width;

            if (newHeight < map_height)
                h = newHeight;
            else
                h = map_height;

            // copy possible tiles to the _resize_map
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    resize_map[x, y] = map[x, y];

            // now resize the (old) _map
            map = new int[newWidth, newHeight];

            // initialized _map
            for (int x = 0; x < newWidth; x++)
                for (int y = 0; y < newHeight; y++)
                    map[x, y] = -1;

            // and finally generate (new) _map
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    map[x, y] = resize_map[x, y];
        }

        public void SaveMap(string fileName)
        {   // save map
            FileStream fs = new FileStream(fileName, FileMode.Create);
            XmlTextWriter w = new XmlTextWriter(fs, null);

            w.WriteStartDocument();
            w.WriteComment("This map is generated by LV Map Editor");
            w.WriteStartElement("LVMap");
            w.WriteAttributeString("MapName", map_name.ToString());
            w.WriteAttributeString("MapWidth", map_width.ToString());
            w.WriteAttributeString("MapHeight", map_height.ToString());
            w.WriteAttributeString("TileWidth", tile_width.ToString());
            w.WriteAttributeString("TileHeight", tile_height.ToString());

            for (int row = 0; row < map_height; row++)
            {
                // write the first row of the map
                w.WriteStartElement("Row");
                w.WriteAttributeString("Position", row.ToString());

                for (int col = 0; col < map_width; col++)
                {
                    w.WriteStartElement("Column");
                    w.WriteAttributeString("Position", col.ToString());
                    w.WriteString(map[col, row].ToString());
                    w.WriteEndElement();
                }

                w.WriteEndElement();
            }

            // close the root element
            w.WriteEndElement();
            w.WriteEndDocument();
            w.Close();

            backup_map.SetMap(map_width, map_height, tile_width, tile_height, map, false);
        }

        public void SelectTool(ToolType tool)
        {   // select tool
            // end dragging
            selection.IsDragging = false;

            if (tool == ToolType.selection)
            {
                selected_tool = ToolType.selection;
                btnToolSelection.FlatStyle = FlatStyle.Flat;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.brush)
            {
                selected_tool = ToolType.brush;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Flat;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.fill)
            {
                selected_tool = ToolType.fill;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Flat;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.selectTile)
            {
                selected_tool = ToolType.selectTile;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Flat;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.eraser)
            {
                selected_tool = ToolType.eraser;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Flat;
            }

            // change mouse cursor
            pbMap.Cursor = Cursors.Cross;

        }

        public void SetupMap()
        {   // setup map
            map_name = tbMapName.Text;

            // update tile size
            tile_width = Convert.ToInt32(nudTileWidth.Value);
            tile_height = Convert.ToInt32(nudTileHeight.Value);

            int w = Convert.ToInt32(nudMapWidth.Value);
            int h = Convert.ToInt32(nudMapHeight.Value);

            ResizeMap(w, h);

            // update map size
            map_width = w;
            map_height = h;

            pbMap.Width = tile_width * map_width;
            pbMap.Height = tile_height * map_height;

            RenderTiles();
            RenderMap();
            ClearSelectedTile();
        }

        public static Point isoTo2D(Point pt)
        {
            Point tempPt = new Point();
            tempPt.X = (2 * pt.Y + pt.X) / 2;
            tempPt.Y = (2 * pt.Y - pt.X) / 2;
            return tempPt;
        }

        public static Point twoDToIso(Point pt)
        {
            Point tempPt = new Point(0, 0);
            tempPt.X = (pt.X - pt.Y);
            tempPt.Y = (pt.X + pt.Y) / 2;
            return (tempPt);
        }

    }
}