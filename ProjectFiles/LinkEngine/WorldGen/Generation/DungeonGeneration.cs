using System;
using System.Collections.Generic;
using System.Drawing;

namespace LinkEngine.WorldGen
{
    public class DungeonGeneration
    {
        public string Name { get; set; }
        public List<Location> Rooms { get; set; }
        public List<Location> Halls { get; set; }
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
        public void BuildMap(int numOfRooms, ref int playerX, ref int playerY, int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            Rooms = new List<Location>();
            Halls = new List<Location>();

            object[] X = new object[4];
            object[] Y = new object[4];
            string[] sides = new string[4];
            for (int i = 0; i < numOfRooms; i++)
            {
                if (i > 0)
                {
                    for (int j = 0; j < X.Length; j++)
                    {
                        if (!(X[j] == null || Y[j] == null))
                        {
                            GenerateRoom(i, rand.Next(minWidth, maxWidth), rand.Next(minHeight, maxHeight), (int)X[j], (int)Y[j]);
                            //MakeOpening((int)X[j], (int)Y[j], i, hallways[j]);
                            int built = BuildHallways(ref X, ref Y, ref sides);
                            for (int k = built - 1; k > -1; k--)
                            {
                                switch (sides[built - k])
                                {
                                    case "top":
                                        Rooms[Rooms.Count - 1].LocationToNorth = Halls[Halls.Count - (k + 1)];
                                        break;
                                    case "bot":
                                        Rooms[Rooms.Count - 1].LocationToSouth = Halls[Halls.Count - (k + 1)];
                                        break;
                                    case "left":
                                        Rooms[Rooms.Count - 1].LocationToWest = Halls[Halls.Count - (k + 1)];
                                        break;
                                    case "right":
                                        Rooms[Rooms.Count - 1].LocationToEast = Halls[Halls.Count - (k + 1)];
                                        break;
                                }
                            }

                            i++;
                        }
                    }
                }
                else
                {
                    GenerateRoom(i, rand.Next(minWidth, maxWidth), rand.Next(minHeight, maxHeight), 0, 0);
                    int built = BuildHallways(ref X, ref Y, ref sides);
                    for (int k = built - 1; k > -1; k--)
                    {
                        switch (sides[built - k])
                        {
                            case "top":
                                Rooms[Rooms.Count - 1].LocationToNorth = Halls[Halls.Count - (k + 1)];
                                break;
                            case "bot":
                                Rooms[Rooms.Count - 1].LocationToSouth = Halls[Halls.Count - (k + 1)];
                                break;
                            case "left":
                                Rooms[Rooms.Count - 1].LocationToWest = Halls[Halls.Count - (k + 1)];
                                break;
                            case "right":
                                Rooms[Rooms.Count - 1].LocationToEast = Halls[Halls.Count - (k + 1)];
                                break;
                        }
                    }
                    break;
                }
            }
            SpawnMonster();
        }

        // Rooms
        /// <summary>
        /// GenerateRoom assigns the bounds of the room being created then calls GenerateRoomTiles
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        void GenerateRoom(int id, int width, int length, int startx, int starty)
        {
            // Generate a room based on the Biome and tile size
            Rooms.Add(new Location(id, "", "", width, length));
            
            Rooms[Rooms.Count - 1].TopLeft_Bound = new Point(startx, starty);
            Rooms[Rooms.Count - 1].TopRight_Bound = new Point(Rooms[0].Width - 1, starty);
            Rooms[Rooms.Count - 1].BottomRight_Bound = new Point(Rooms[0].Width - 1, Rooms[0].Height - 1);
            Rooms[Rooms.Count - 1].BottomLeft_Bound = new Point(startx, Rooms[0].Height - 1);

            GenerateRoomTiles(width, length, Rooms.Count - 1, startx, starty);
        }
        /// <summary>
        /// Steps through the mutlidimensional Tiles array of Room and places a tile based on the x and y coordinate of the given for loops
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="roomNumber"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
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
                        tile = new Tile(Biome.AvailableTiles[7]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == width - 1 && y - starty == 0)
                    {
                        tile = new Tile(Biome.AvailableTiles[8]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == width - 1 && y - starty == length - 1)
                    {
                        tile = new Tile(Biome.AvailableTiles[6]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == 0 && y - starty == length - 1)
                    {
                        tile = new Tile(Biome.AvailableTiles[5]);
                        tile.X = x;
                        tile.Y = y;
                    }

                    // Walls
                    else if (y - starty == 0)
                    {
                        // make a South Facing wall tile
                        tile = new Tile(Biome.AvailableTiles[0]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (y - starty == length - 1)
                    {
                        // make a North Facing wall tile
                        tile = new Tile(Biome.AvailableTiles[1]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == 0)
                    {
                        // make a  West Facing wall tile
                        tile = new Tile(Biome.AvailableTiles[3]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x - startx == width - 1)
                    {
                        // make a  East Facing wall tile
                        tile = new Tile(Biome.AvailableTiles[2]);
                        tile.X = x;
                        tile.Y = y;
                    }

                    // Floor
                    else
                    {
                        // make a floor tile
                        tile = new Tile(Biome.AvailableTiles[4]);
                        tile.X = x;
                        tile.Y = y;
                    }
                    tile.Location = Rooms[roomNumber];
                    Tiles[y, x] = tile;
                }
            }
        }

        // Hallways
        /// <summary>
        /// BuildHallways makes preperations to build hallways off of a room. It calls IsHallwayOffMap and CanBuildHallwayOnThisSide to check if a hallway can be built. 
        /// Then calls MakeOpening and GenerateHallway.
        /// </summary>
        /// <param name="xs"></param>
        /// <param name="ys"></param>
        int BuildHallways(ref object[] xs, ref object[] ys, ref string[] sidesused)
        {
            // randomize a number of hallways to generate
            int hallways = rand.Next(4);
            int hallwaysBuilt = 0;
            for (int i = 0; i < hallways; i++)
            {
                int x = 0, y = 0;
                string hallway = "", side = "";

                Halls.Add(new Location(Halls.Count + i, "Hallway", "A Hallway", 3, 3));
                HallwayPlacement(ref x, ref y, ref hallway, ref side);

                Halls[Halls.Count - 1].TopLeft_Bound = new Point(x, y);
                Halls[Halls.Count - 1].TopRight_Bound = new Point(Halls[Halls.Count - 1].Width - 1, y);
                Halls[Halls.Count - 1].BottomRight_Bound = new Point(Halls[Halls.Count - 1].Width - 1, Halls[Halls.Count - 1].Height - 1);
                Halls[Halls.Count - 1].BottomLeft_Bound = new Point(x, Halls[Halls.Count - 1].Height - 1);

                if (!IsHallwayOffMap(x, y, hallway) && CanBuildHallwayOnThisSide(side, sidesused))
                {
                    MakeOpening(x, y, Rooms.Count - 1, hallway);
                    GenerateHallway(3, 3, Rooms.Count - 1, x, y, hallway);
                    sidesused[i] = side;

                    xs[i] = x;
                    ys[i] = y;

                    hallwaysBuilt++;
                }
            }
            return hallwaysBuilt;
        }
        /// <summary>
        /// HallwayPlacement randomly chooses a wall and x and y coordinates along that chosen wall to generate a hallway.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hallway"></param>
        /// <param name="side"></param>
        void HallwayPlacement(ref int x, ref int y, ref string hallway, ref string side)
        {
            x = rand.Next(2) * (Rooms[Rooms.Count - 1].Width - 1);
            y = rand.Next(2) * (Rooms[Rooms.Count - 1].Height - 1);

            switch (rand.Next(2))
            {
                case 0:
                    // place a horizontal hallway
                    hallway = "horiz";
                    y = rand.Next(1, (Rooms[Rooms.Count - 1].Height - 1));

                    if (x == 0)
                    {
                        side = "left";
                        Halls[Halls.Count - 1].LocationToEast = Rooms[Rooms.Count - 1];
                    }
                    else
                    {
                        side = "right";
                        Halls[Halls.Count - 1].LocationToWest = Rooms[Rooms.Count - 1];
                    }
                    break;
                case 1:
                    // place a vertical hallway
                    hallway = "vert";
                    x = rand.Next(1, (Rooms[Rooms.Count - 1].Width - 1));

                    if (y == 0)
                    {
                        side = "top";
                        Halls[Halls.Count - 1].LocationToSouth = Rooms[Rooms.Count - 1];
                    }
                    else
                    {
                        side = "bot";
                        Halls[Halls.Count - 1].LocationToNorth = Rooms[Rooms.Count - 1];
                    }
                    break;
            }
            
        }
        /// <summary>
        /// GenerateHallway checks which direction the hallway is going and calls the appropraite direction function.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="roomNumber"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hallway"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="roomNumber"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        void VerticalHallwayDown(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length + starty; y++)
            {
                for (int x = startx; x < width + startx; x++)
                {
                    if (x - startx == 0)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[3], x, y);
                    }
                    else if (x - startx == width - 1)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[2], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.AvailableTiles[4], x, y);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="roomNumber"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        void VerticalHallwayUp(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length - starty; y--)
            {
                for (int x = startx; x < width - startx; x--)
                {
                    if (x - startx == 0)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[3], x, y);
                    }
                    else if (x - startx == width - 1)
                    {
                        if (!(y - starty == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[2], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.AvailableTiles[4], x, y);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="roomNumber"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        void HorizontalHallwayLeft(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length - starty; y--)
            {
                for (int x = startx; x < width - startx; x--)
                {
                    if (y - starty == 0)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[0], x, y);
                    }
                    else if (y - starty == length - 1)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[1], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.AvailableTiles[4], x, y);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="roomNumber"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        void HorizontalHallwayRight(int width, int length, int roomNumber, int startx, int starty)
        {
            for (int y = starty; y < length + starty; y++)
            {
                for (int x = startx; x < width + startx; x++)
                {
                    if (y - starty == 0)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[0], x, y);
                    }
                    else if (y - starty == length - 1)
                    {
                        if (!(x - startx == 0))
                            Tiles[y, x] = new Tile(Biome.AvailableTiles[1], x, y);
                    }
                    else
                    {
                        Tiles[y, x] = new Tile(Biome.AvailableTiles[4], x, y);
                    }
                }
            }
        }

        void MakeOpening (int x, int y, int roomNumber, string hallway)
        {
            // Change the a wal tile to be a floor tile
            Tiles[y, x] = new Tile(Biome.AvailableTiles[4], x, y);

            // now change the side wall tiles to be corner tiles
            switch (hallway)
            {
                case "vert":
                    if (y == Rooms[roomNumber].TopLeft_Bound.Y)
                    {
                        Tiles[y, x - 1] = new Tile(Biome.AvailableTiles[6], x - 1, y); // left corner
                        Tiles[y, x + 1] = new Tile(Biome.AvailableTiles[5], x + 1,y); // right corner
                    }
                    else if (y == Rooms[roomNumber].BottomLeft_Bound.Y)
                    {
                        Tiles[y, x - 1] = new Tile(Biome.AvailableTiles[8], x - 1, y); // left corner
                        Tiles[y, x + 1] = new Tile(Biome.AvailableTiles[7], x + 1, y); // right corner
                    }
                    break;
                case "horiz":
                    if (x == Rooms[roomNumber].TopLeft_Bound.X)
                    {
                        Tiles[y - 1, x] = new Tile(Biome.AvailableTiles[6], x, y - 1); // left corner
                        Tiles[y + 1, x] = new Tile(Biome.AvailableTiles[8], x, y + 1); // right corner
                    }
                    else if (x == Rooms[roomNumber].TopRight_Bound.X)
                    {
                        Tiles[y - 1, x] = new Tile(Biome.AvailableTiles[5], x, y - 1); // left corner
                        Tiles[y + 1, x] = new Tile(Biome.AvailableTiles[7], x, y + 1); // right corner
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

        /// <summary>
        /// SpawnMonster will set the MonsterLivingHere property of Location equal to a randomly chosen monster from the biome
        /// </summary>
        void SpawnMonster()
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                Rooms[i].MonsterLivingHere = new Entities.Enemy(Biome.Enemies[rand.Next(Biome.Enemies.Count)]);
            }
        }
    }
}