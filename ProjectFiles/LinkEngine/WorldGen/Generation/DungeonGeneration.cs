using System;
using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class DungeonGeneration
    {
        public string Name { get; set; }
        public List<Location> Rooms { get; set; }
        public Tile[,] Tiles { get; set; }

        /// <summary>
        /// The number of tiles wide the Dungeon layout is
        /// </summary>
        public int DungeonWidth { get; set; }
        /// <summary>
        /// The number of tiles high the Dungeon layout is
        /// </summary>
        public int DungeonHeight { get; set; }

        Biome Biome;

        Random rand = new Random();

        public DungeonGeneration (int w, int h, Biome biome)
        {
            DungeonHeight = h;
            DungeonWidth = w;
            Biome = biome;

            Tiles = new Tile[h, w];
        }

        /// <summary>
        ///  Indicies for Biome Lists
        ///  --- South Facing Wall Tile [0]
        /// --- North Facing Wall Tile [1]
        /// --- East Facing Wall Tile [2]
        /// --- West Facing Wall Tile [3]
        /// --- Floor Tile [4]
        /// --- Bottom Left Corner [5]
        /// --- Bottom Right Corner [6]
        /// --- Top Left Corner [7]
        /// --- Top Right Corner [8]
        /// </summary>
        /// <param name="numOfRooms"></param>
        /// <param name="Biome"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        public void BuildMap(int numOfRooms, ref int playerX, ref int playerY, int minWidth, int maxWidth, int minLength, int maxLength)
        {
            Rooms = new List<Location>();

            object[] X = new object[4];
            object[] Y = new object[4];
            for (int i = 0; i < numOfRooms; i++)
            {
                if (i > 0)
                {
                    for (int j = 0; j < X.Length; j++)
                    {
                        if (!(X[j] == null || Y[j] == null))
                        {
                            GenerateRoom(i, rand.Next(minWidth, maxWidth), rand.Next(minLength, maxLength), (int)X[j], (int)Y[j]);
                            BuildHallways(ref X, ref Y);
                            i++;
                        }
                    }
                }
                else
                {
                    GenerateRoom(i, rand.Next(minWidth, maxWidth), rand.Next(minLength, maxLength), 0, 0);
                    BuildHallways(ref X, ref Y);
                    break;
                }
                
            }
        }

        // Rooms
        void GenerateRoom(int id, int width, int length, int startx, int starty)
        {
            // Generate a room based on the Biome and tile size
            Rooms.Add(new Location(id, "", "", width, length));
            
            Rooms[0].TopLeft_Bound = new Components.Vector(startx, starty, 0);
            Rooms[0].TopRight_Bound = new Components.Vector(Rooms[0].Width - 1, starty, 0);
            Rooms[0].BottomRight_Bound = new Components.Vector(Rooms[0].Width - 1, Rooms[0].Length - 1, 0);
            Rooms[0].BottomLeft_Bound = new Components.Vector(startx, Rooms[0].Length - 1, 0);

            GenerateRoomTiles(width, length, Rooms.Count - 1, startx, starty);
        }
        void GenerateRoomTiles(int width, int length, int roomNumber, int startx, int starty)
        {
            Tile tile;
            for (int y = 0; y < length + starty; y++)
            {
                for (int x = 0; x < width + startx; x++)
                {
                    tile = new Tile(0, null, 0, 0, 0, null);

                    // Wall Corners
                    if (x - startx == 0 && y - starty == 0)
                    {
                        tile = new Tile(Biome.availableTiles[7]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == width - 1 && y - starty == 0)
                    {
                        tile = new Tile(Biome.availableTiles[8]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == width - 1 && y - starty == length - 1)
                    {
                        tile = new Tile(Biome.availableTiles[6]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == 0 && y - starty == length - 1)
                    {
                        tile = new Tile(Biome.availableTiles[5]);
                        tile.X = x;
                        tile.Y = y;
                    }

                    // Walls
                    else if (y - starty == 0)
                    {
                        // make a South Facing wall tile
                        tile = new Tile(Biome.availableTiles[0]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (y - starty == length - 1)
                    {
                        // make a North Facing wall tile
                        tile = new Tile(Biome.availableTiles[1]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == 0)
                    {
                        // make a  West Facing wall tile
                        tile = new Tile(Biome.availableTiles[3]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == width - 1)
                    {
                        // make a  East Facing wall tile
                        tile = new Tile(Biome.availableTiles[2]);
                        tile.X = x;
                        tile.Y = y;
                    }

                    // Floor
                    else
                    {
                        // make a floor tile
                        tile = new Tile(Biome.availableTiles[4]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    tile.Location = Rooms[roomNumber];
                    Tiles[y, x] = tile;
                }
            }
        }

        // Hallways
        void BuildHallways(ref object[] xs, ref object[] ys)
        {
            string[] sidesused = { "", "", "", "" };
            // randomize a number of hallways to generate
            int hallways = rand.Next(4);
            for (int i = 0; i < hallways; i++)
            {
                int x = 0, y = 0;
                string hallway = "", side = "";
                HallwayPlacement(ref x, ref y, ref hallway, ref side);

                if (!IsHallwayOffMap(x, y, hallway) && CanBuildHallwayOnThisSide(side, sidesused))
                {
                    MakeOpening(x, y, Rooms.Count - 1, hallway);
                    GenerateHallway(3, 3, Rooms.Count - 1, x, y, hallway);
                    sidesused[i] = side;

                    xs[i] = x;
                    ys[i] = y;
                }
            }
        }
        void HallwayPlacement(ref int x, ref int y, ref string hallway, ref string side)
        {
            x = rand.Next(2) * (Rooms[Rooms.Count - 1].Width - 1);
            y = rand.Next(2) * (Rooms[Rooms.Count - 1].Length - 1);

            switch (rand.Next(2))
            {
                case 0:
                    // place a horizontal hallway
                    hallway = "horiz";
                    y = rand.Next(1, (Rooms[Rooms.Count - 1].Length - 1));

                    if (x == 0) side = "left";
                    else side = "right";
                    break;
                case 1:
                    // place a vertical hallway
                    hallway = "vert";
                    x = rand.Next(1, (Rooms[Rooms.Count - 1].Width - 1));

                    if (y == 0) side = "top";
                    else side = "bot";
                    break;
            }
        }
        void GenerateHallway(int width, int length, int roomNumber, int x, int y, string hallway)
        {
            if (hallway == "vert")
            {
                if (y == Rooms[roomNumber].TopLeft_Bound.Y)
                    VerticalHallwayUp(width, length, roomNumber, x - 1, y);
                else if (y == Rooms[roomNumber].BottomLeft_Bound.Y)
                    VerticalHallwayDown(width, length, roomNumber, x - 1, y);
            }
            else if (hallway == "horiz")
            {
                if (x == Rooms[roomNumber].TopLeft_Bound.X)
                    HorizontalHallwayLeft(width, length, roomNumber, x, y - 1);
                else if (x == Rooms[roomNumber].TopRight_Bound.X)
                    HorizontalHallwayRight(width, length, roomNumber, x, y - 1);
            }
        }

        void VerticalHallwayDown(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length + starty; y++)
            {
                for (int x = startx; x < width + startx; x++)
                {
                    if (x - startx == 0)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[3], x, y);
                    }
                    else if (x - startx == width - 1)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[2], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.availableTiles[4], x, y);
                    }
                }
            }
        }
        void VerticalHallwayUp(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length - starty; y--)
            {
                for (int x = startx; x < width - startx; x--)
                {
                    if (x - startx == 0)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[3], x, y);
                    }
                    else if (x - startx == width - 1)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[2], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.availableTiles[4], x, y);
                    }
                }
            }
        }
        void HorizontalHallwayLeft(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length - starty; y--)
            {
                for (int x = startx; x < width - startx; x--)
                {
                    if (y - starty == 0)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[0], x, y);
                    }
                    else if (y - starty == length - 1)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[1], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.availableTiles[4], x, y);
                    }
                }
            }
        }
        void HorizontalHallwayRight(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length + starty; y++)
            {
                for (int x = startx; x < width + startx; x++)
                {
                    if (y - starty == 0)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[0], x, y);
                    }
                    else if (y - starty == length - 1)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.availableTiles[1], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.availableTiles[4], x, y);
                    }
                }
            }
        }

        void MakeOpening (int x, int y, int roomNumber, string hallway)
        {
            // Change the a wal tile to be a floor tile
            Tiles[y, x] = new Tile(Biome.availableTiles[4], x, y);

            // now change the side wall tiles to be corner tiles
            switch (hallway)
            {
                case "vert":
                    if (y == Rooms[roomNumber].TopLeft_Bound.Y)
                    {
                        Tiles[y, x - 1] = new Tile(Biome.availableTiles[6], x - 1, y); // left corner
                        Tiles[y, x + 1] = new Tile(Biome.availableTiles[5], x + 1,y); // right corner
                    }
                    else if (y == Rooms[roomNumber].BottomLeft_Bound.Y)
                    {
                        Tiles[y, x - 1] = new Tile(Biome.availableTiles[8], x - 1, y); // left corner
                        Tiles[y, x + 1] = new Tile(Biome.availableTiles[7], x + 1, y); // right corner
                    }
                    break;
                case "horiz":
                    if (x == Rooms[roomNumber].TopLeft_Bound.X)
                    {
                        Tiles[y - 1, x] = new Tile(Biome.availableTiles[6], x, y - 1); // left corner
                        Tiles[y + 1, x] = new Tile(Biome.availableTiles[8], x, y + 1); // right corner
                    }
                    else if (x == Rooms[roomNumber].TopRight_Bound.X)
                    {
                        Tiles[y - 1, x] = new Tile(Biome.availableTiles[5], x, y - 1); // left corner
                        Tiles[y + 1, x] = new Tile(Biome.availableTiles[7], x, y + 1); // right corner
                    }
                    
                    break;
            }
        }

        bool IsHallwayOffMap(int x, int y, string hallway)
        {
            if (hallway == "vert")
            {
                if (y == 0)
                {
                    return true;
                }
                else if (y == DungeonHeight - 1)
                {
                    return true;
                }
            }
            else if (hallway == "horiz")
            {
                if (x == 0)
                {
                    return true;
                }
                else if (x == DungeonWidth - 1)
                {
                    return true;
                }
            }

            return false;
        }
        bool CanBuildHallwayOnThisSide(string side, string[] sidesused)
        {
            foreach (string used in sidesused)
            {
                if (used == side && used != "")
                {
                    return false;
                }
            }

            return true;
        }


        // Legacy Code
        void SpawnMonster(ref bool hasMonster, int length, int width, Biome Biome, int roomNumber)
        {
            // Room can include a monster spawn
            while (!hasMonster)
            {
                Random rand = new Random();

                int x = rand.Next(width);
                int y = rand.Next(length);

                if (!(x == 0 || x == width))
                {
                    if (!(y == 0 || y == length))
                    {
                        //int i = rand.Next(Biome.availableMonsters.Count - 1);
                        //Rooms[roomNumber].MonsterLivingHere = new Monster(Biome.availableMonsters[i]);
                        //hasMonster = true;
                    }
                }
            }
        }
    }
}