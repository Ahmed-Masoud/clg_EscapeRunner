using System;
using System.Collections;
using System.Drawing;
using System.IO;

//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

//using System.Drawing.Imaging;

namespace LevelBuilder
{
    public partial class LevelBuilder
    {
        public void AddTiles(string folderName)
        {   // add tiles to tile library
            ArrayList tilesArrayList = new ArrayList();

            Cursor.Current = Cursors.WaitCursor;
            if (!Directory.Exists(folderName))
                throw new FileNotFoundException();

            foreach (string f in Directory.GetFiles(folderName))
            {   // load tiles
                if (Path.GetExtension(f) == ".bmp" ||
                    Path.GetExtension(f) == ".png" ||
                    Path.GetExtension(f) == ".jpg" ||
                    Path.GetExtension(f) == ".jpeg")
                    tilesArrayList.Add(f.ToString());
            }

            // delete all controls
            pnlTileLibrary.Controls.Clear();
            pnlTileLibrary.Refresh();

            int t = 0;

            // resize _tile_library
            if (tile_library != null && tile_library.Length > 0)
            {   // add to the library
                t = tile_library.Length;
                Array.Resize(ref tile_library, tilesArrayList.Count + tile_library.Length);
            }
            else
            {   // load new library
                Array.Resize(ref tile_library, tilesArrayList.Count);
            }

            foreach (string i in tilesArrayList)
            {   // update tiles library
                Tile newTile = new Tile();
                PictureBox pB = new PictureBox();
                pB.Left = 20;
                pB.Top = (t * (tile_height + 5)) + 20;
                pB.Width = tile_width;
                pB.Height = tile_height;
                pB.Name = t.ToString();
                pB.Load(i);
                newTile.TileID = t;
                newTile.TileName = t.ToString();
                newTile.TilePictureBox = pB;
                tile_library[t] = newTile;
                pB.MouseClick += new MouseEventHandler(tilePicBox_MouseClick);

                t++;
            }

            RenderTiles();

            Cursor.Current = Cursors.Default;
        }

        public void ClearSelectedTile()
        {   // deselect tile
            gbTileProperties.Enabled = false;
            selected_tile = null;
            lblTileIDValue.Text = "";
            tbTileName.Text = "";
            pbSelectedTile.Image = null;
            pbSelectedTile.Name = "";
        }

        public void ClearTiles()
        {   // clear all tiles in the tile library
            if (tile_library != null)
            {
                Array.Clear(tile_library, 0, tile_library.Length);
                Array.Resize(ref tile_library, 0);
            }

            // initialized _map
            for (int x = 0; x < map_width; x++)
                for (int y = 0; y < map_height; y++)
                    map[x, y] = -1;

            ClearSelectedTile();
        }

        public void DeleteSelectedTile(int selectedTileID)
        {   // remove selected tile from tile library
            if (tile_library != null)
            {   // remove that tile
                int i = 0;
                for (i = selectedTileID; i < tile_library.Length - 1; i++)
                {
                    tile_library[i].TileWidth = tile_library[i + 1].TileWidth;
                    tile_library[i].TileHeight = tile_library[i + 1].TileHeight;
                    tile_library[i].TilePath = tile_library[i + 1].TilePath;
                    tile_library[i].TileWalkable = tile_library[i + 1].TileWalkable;
                    tile_library[i].TilePictureBox = tile_library[i + 1].TilePictureBox;

                    if (tile_library[i + 1].TileName == tile_library[i + 1].TileID.ToString())
                        tile_library[i].TileName = tile_library[i].TileID.ToString();
                    else
                        tile_library[i].TileName = tile_library[i + 1].TileName;

                    //_tile_library[i].TileID = i;
                }
                Array.Clear(tile_library, i, 1);
                Array.Resize(ref tile_library, tile_library.Length - 1);
            }

            // update _map
            for (int x = 0; x < map_width; x++)
            {
                for (int y = 0; y < map_height; y++)
                {
                    if (map[x, y] == selectedTileID)
                        map[x, y] = -1;
                    else if (map[x, y] > selectedTileID)
                        map[x, y] = map[x, y] - 1;
                }
            }

            RenderTiles();
            RenderMap();
            ClearSelectedTile();
        }

        public void LoadTiles(string fileName)
        {   // load tiles from the saved tile library
            // clear tile library
            if (tile_library != null)
                Array.Clear(tile_library, 0, tile_library.Length);

            // load tiles
            ///////////////
            string pbDirName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);

            ArrayList tilesArrayList = new ArrayList();

            // delete all controls
            pnlTileLibrary.Controls.Clear();
            pnlTileLibrary.Refresh();

            if (Directory.Exists(pbDirName))
            {
                foreach (string f in Directory.GetFiles(pbDirName))
                {   // load tiles
                    if (Path.GetExtension(f) == ".bmp" ||
                        Path.GetExtension(f) == ".png" ||
                        Path.GetExtension(f) == ".jpg" ||
                        Path.GetExtension(f) == ".jpeg")
                        tilesArrayList.Add(f.ToString());
                }

                int t = 0;

                // resize _tile_library
                Array.Resize(ref tile_library, tilesArrayList.Count);

                foreach (string i in tilesArrayList)
                {   // update tiles library
                    Tile newTile = new Tile();
                    PictureBox pB = new PictureBox();
                    pB.Left = 20;
                    pB.Top = (t * (tile_height + 5)) + 20;
                    pB.Width = tile_width;
                    pB.Height = tile_height;
                    pB.Name = t.ToString();
                    pB.Load(i);
                    newTile.TileID = t;
                    newTile.TileName = t.ToString();
                    newTile.TilePictureBox = pB;
                    tile_library[t] = newTile;
                    pB.MouseClick += new MouseEventHandler(tilePicBox_MouseClick);

                    t++;
                }
            }
            else
            {
                MessageBox.Show(pbDirName + " doesn't exist! This folder is needed for the Tiles Library.", "Cannot Find Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string tileLibraryFileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + "-tile.xml";
            FileStream tfs = new FileStream(tileLibraryFileName, FileMode.Open);
            XmlDocument textReader = new XmlDocument();
            textReader.Load(tfs);

            XmlNodeList tileList = textReader.GetElementsByTagName("Tile");

            foreach (XmlNode node in tileList)
            {
                XmlElement tileElement = (XmlElement)node;

                int tileID = Convert.ToInt32(tileElement.Attributes["ID"].InnerText);

                tile_library[tileID].TileName = tileElement.GetElementsByTagName("Name")[0].InnerText;
                tile_library[tileID].TileWidth = Convert.ToInt32(tileElement.GetElementsByTagName("Width")[0].InnerText);
                tile_library[tileID].TileHeight = Convert.ToInt32(tileElement.GetElementsByTagName("Height")[0].InnerText);
                tile_library[tileID].TileWalkable = Convert.ToBoolean(tileElement.GetElementsByTagName("Walkable")[0].InnerText);
            }

            tfs.Close();
        }

        public void RenderTiles()
        {   // render tiles in the tile library panel
            this.pnlTileLibrary.Controls.Clear();

            if (tile_library != null)
            {
                for (int i = 0; i < tile_library.Length; i++)
                {   // reload tiles panel
                    PictureBox pb = tile_library[i].TilePictureBox;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    if (pb != null)
                    {
                        pb.Left = 20;
                        pb.Top = (i * (tile_height + 5)) + 20;
                        pb.Width = tile_width;
                        pb.Height = tile_height;
                        pb.Name = i.ToString();
                        this.pnlTileLibrary.Controls.Add(pb);
                    }
                }
            }

            this.pnlTileLibrary.Refresh();
        }

        public void SaveTiles(string fileName)
        {   // save tiles to xml file
            string tileFileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + "-tile.xml";
            FileStream tfs = new FileStream(tileFileName, FileMode.Create);
            XmlTextWriter textWriter = new XmlTextWriter(tfs, null);
            bool saved = false;

            string pbDirName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);
            Directory.CreateDirectory(pbDirName);

            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("MapTilesLibrary");
            textWriter.WriteAttributeString("TotalTiles", tile_library.Length.ToString());
            textWriter.WriteComment("This Tiles Library is generated by Map Editor!");

            for (int i = 0; i < tile_library.Length; i++)
            {
                textWriter.WriteStartElement("Tile");
                textWriter.WriteAttributeString("ID", tile_library[i].TileID.ToString());

                textWriter.WriteStartElement("Name");
                textWriter.WriteString(tile_library[i].TileName);
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("Width");
                textWriter.WriteString(tile_library[i].TileWidth.ToString());
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("Height");
                textWriter.WriteString(tile_library[i].TileHeight.ToString());
                textWriter.WriteEndElement();
                textWriter.WriteStartElement("Walkable");
                textWriter.WriteString(tile_library[i].TileWalkable.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();

                PictureBox pb = tile_library[i].TilePictureBox;

                if (Convert.ToInt32(pb.Name) >= 0)
                {
                    Image img = pb.Image;
                    Bitmap bmp = new Bitmap(img);

                    string pbFileName = "";

                    if (Convert.ToInt32(pb.Name) < 10)
                        pbFileName = pbDirName + "\\00" + pb.Name.ToString() + ".png";
                    else if (Convert.ToInt32(pb.Name) < 100)
                        pbFileName = pbDirName + "\\0" + pb.Name.ToString() + ".png";
                    else
                        pbFileName = pbDirName + "\\" + pb.Name.ToString() + ".png";

                    try
                    {
                        bmp.Save(pbFileName, System.Drawing.Imaging.ImageFormat.Png);
                        saved = true;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString(), "Fail to Save Tiles - " + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        saved = false;
                    }
                }
            }

            if (saved)
            {
                MessageBox.Show("Map Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            // close the root element
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            textWriter.Close();
            tfs.Close();
        }
    }
}