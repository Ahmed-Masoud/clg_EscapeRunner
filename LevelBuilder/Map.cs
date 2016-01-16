using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace LevelBuilder
{
    public class Map
    {
        private int tile_width;
        private int tile_height;
        private int map_width;
        private int map_height;
        private Boolean isTileLibraryChanged;
        private int[,] mapp;

        public Map()
        {
            map_width = 0;
            map_height = 0;
            tile_width = 0;
            tile_height = 0;
            isTileLibraryChanged = false;
        }

        public int TileWidth
        {
            get { return tile_width; }
            set { tile_width = value; }
        }

        public int TileHeight
        {
            get { return tile_height; }
            set { tile_height = value; }
        }

        public int MapWidth
        {
            get { return map_width; }
            set { map_width = value; }
        }

        public int MapHeight
        {
            get { return map_height; }
            set { map_height = value; }
        }

        public int[,] Mapp
        {
            get { return mapp; }
            set { mapp = value; }
        }

        public Boolean IsTileLibraryChanged
        {
            get { return isTileLibraryChanged; }
            set { isTileLibraryChanged = value; }
        }

        public void SetMap(int mapWidth, int mapHeight, int tileWidth, int tileHeight, int[,] map, Boolean isTileLibraryChanged)
        {
            map_width = mapWidth;
            map_height = mapHeight;
            tile_width = tileWidth;
            tile_height = tileHeight;
            this.isTileLibraryChanged = isTileLibraryChanged;

            this.mapp = new int[map_width, map_height];
            for (int x = 0; x < map_width; x++)
                for (int y = 0; y < map_height; y++)
                    this.mapp[x, y] = map[x, y];
        }

        public Boolean IsDirty(int mapWidth, int mapHeight, int tileWidth, int tileHeight, int[,] map)
        {
            if (IsTileLibraryChanged || mapWidth != map_width || mapHeight != map_height || tileWidth != tile_width || tileHeight != tile_height)
                return true;

            // if everything is same, check if map is changed
            for (int x = 0; x < map_width; x++)
                for (int y = 0; y < map_height; y++)
                    if (this.mapp[x, y] != map[x, y])
                        return true;

            return false;
        }

        private bool IsMapEmpty(int mapWidth, int mapHeight, int[,] map)
        {   // is map dirty?
            for (int x = 0; x < mapWidth; x++)
                for (int y = 0; y < mapHeight; y++)
                    if (map[x, y] != -1)
                        return false;

            return true;
        }
    }
}
