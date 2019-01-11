using System;
using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class DungeonGeneration
    {
        public string Name { get; set; }
        public List<Location> Rooms { get; set; }

        Random rand = new Random();

        /// <summary>
        /// <para>BuildMap takes an integer number of rooms to create, a <seealso cref="Biome"/> (or tileset) to pull tiles from, and the player's x and y coordinates.</para>
        /// <para>The functions loops for the number of rooms specified and each time runs the <seealso cref="GenerateRoom(Biome, int, string, string, int, int, ref int, ref int, string)"/> function and creates a new room.</para>
        /// <para>When looping, this function will attempt to place hallways between rooms using GenerateHallway.</para>
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="numOfRooms"></param>
        /// <param name="biome"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        public void BuildMap(int numOfRooms, Biome biome, ref int playerX, ref int playerY, int minWidth, int maxWidth, int minLength, int maxLength)
        {
            Rooms = new List<Location>();
            for (int i = 0; i < numOfRooms; i++)
            {
                GenerateRoom(biome, i, "", "", rand.Next(minWidth, maxWidth), rand.Next(minLength, maxLength), ref playerX, ref playerY, "South");
            }
        }

        // Indicies for Biome Lists
        // --- South Facing Wall Tile [0]
        // --- North Facing Wall Tile [1]
        // --- EastWest Facing Wall Tile [2]
        // --- Floor Tile [3]
        // --- Chest Tile [4]
        // --- South Facing Door Tile [5]
        // --- North Facing Door Tile [6]
        // --- East Facing Door Tile [7]
        // --- West Facing Door Tile [8]

        /// <summary>
        /// Create a Procedurally generated location
        /// </summary>
        /// <param name="biome"> The biome type to use, this is what defines the tiles to use</param>
        /// <param name="id">ID of the location to create</param>
        /// <param name="name">Name of the location to create</param>
        /// <param name="desc">Description of the location to create</param>
        /// <param name="width">The width of the location</param>
        /// <param name="length">the length of the location</param>
        /// <param name="playerX">the last x coordinate of the player</param>
        /// <param name="playerY">the last y coordinate of the player</param>
        /// <param name="playerFacing">the last direction the player was facing</param>
        public void GenerateRoom(Biome biome, int id, string name, string desc, int width, int length, ref int playerX, ref int playerY, string playerFacing)
        {
            bool hasDoor = false;

            // Generate a room based on the biome and tile size
            Rooms.Add(new Location(id, name, desc, width, length));

            // check that the biome has tiles to pull from
            if (biome.availableTiles.Count != 0)
            {
                GenerateTiles(width, length, biome, Rooms.Count - 1);

                // Room needs atleast one door
                GenerateDoors(ref hasDoor, ref playerX, ref playerY, playerFacing, width, length, biome, Rooms.Count - 1);
            }

            //if (biome.availableMonsters.Count != 0)
            //{
            //    SpawnMonster(ref hasMonster, length, width, biome, Rooms.Count - 1);
            //}
        }

        /// <summary>
        /// GenerateHallway is used to build structures that can connect the rooms together.
        /// </summary>
        /// <param name="biome"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        /// <param name="Facing"></param>
        /// <param name="roomNumber"></param>
        public void GenerateHallway(Biome biome, int width, int length, ref int playerX, ref int playerY, string Facing, int roomNumber)
        {
            if (Facing == "North" || Facing == "South")
            {
                VerticalHallway(biome, width, length, roomNumber);
            }
            if (Facing == "East" || Facing == "West")
            {
                HorizontalHallway(biome, width, length, roomNumber);
            }
        }
        void VerticalHallway(Biome biome, int width, int length, int roomNumber)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j == 0 || j == width)
                    {
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.availableTiles[2].ID, biome.availableTiles[2].Name, biome.availableTiles[2].Dense, i, j, biome.availableTiles[2].Type));
                    }
                }
            }
        }
        void HorizontalHallway(Biome biome, int width, int length, int roomNumber)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0)
                    {
                        // make a South Facing wall tile
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.availableTiles[0].ID, biome.availableTiles[0].Name, biome.availableTiles[0].Dense, i, j, biome.availableTiles[0].Type));
                    }
                    else if (i == length - 1)
                    {
                        // make a North Facing wall tile
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.availableTiles[1].ID, biome.availableTiles[1].Name, biome.availableTiles[1].Dense, i, j, biome.availableTiles[1].Type));
                    }
                }
            }
        }

        // Private Functions
        void GenerateDoors(ref bool hasDoor, ref int playerX, ref int playerY, string playerFacing, int width, int length, Biome biome, int roomNumber)
        {
            while (!hasDoor)
            {
                Random rand = new Random();
                int x = playerX, y = playerY;

                place_door_where_player_entered(ref hasDoor, ref playerX, ref playerY, playerFacing, width, length, biome, roomNumber);
                // Random number of doors to generate
                int numDoors = rand.Next(1, 3);

                for (int j = 0; j < numDoors; j++)
                {
                    do
                    {
                        x = rand.Next(width);
                        y = rand.Next(length);

                        if (x == 0 || x == width || y == 0 || y == length)
                        {
                            // East facing door
                            if (x == 0)
                            {
                                for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
                                {
                                    if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                                    {
                                        if (y == 0 || y == length)
                                        {
                                            y = rand.Next(length);
                                        }
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[7].ID, biome.availableTiles[7].Name, biome.availableTiles[7].Dense, x, y, biome.availableTiles[7].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[7].ID, biome.availableTiles[7].Name, biome.availableTiles[7].Dense, x, y, biome.availableTiles[7].Type), "East", null));
                                    }
                                }
                            }

                            // West facing door
                            else if (x == width)
                            {
                                for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
                                {
                                    if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                                    {
                                        if (y == 0 || y == length)
                                        {
                                            y = rand.Next(length);
                                        }
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[8].ID, biome.availableTiles[8].Name, biome.availableTiles[8].Dense, x, y, biome.availableTiles[8].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[8].ID, biome.availableTiles[8].Name, biome.availableTiles[8].Dense, x, y, biome.availableTiles[8].Type), "West", null));
                                    }
                                }
                            }

                            // South facing door
                            else if (y == 0)
                            {
                                for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
                                {
                                    if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                                    {
                                        if (x == 0 || x == width)
                                        {
                                            x = rand.Next(width);
                                        }
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[5].ID, biome.availableTiles[5].Name, biome.availableTiles[5].Dense, x, y, biome.availableTiles[5].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[5].ID, biome.availableTiles[5].Name, biome.availableTiles[5].Dense, x, y, biome.availableTiles[5].Type), "South", null));
                                    }
                                }
                            }

                            // North Facing Door
                            else if (y == length)
                            {
                                for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
                                {
                                    if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                                    {
                                        if (x == 0 || x == width)
                                        {
                                            x = rand.Next(width);
                                        }
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[6].ID, biome.availableTiles[6].Name, biome.availableTiles[6].Dense, x, y, biome.availableTiles[6].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[6].ID, biome.availableTiles[6].Name, biome.availableTiles[6].Dense, x, y, biome.availableTiles[6].Type), "North", null));
                                    }
                                }
                            }
                        }
                    } while (!(x == 0 || x == width || y == 0 || y == length));
                }
            }
        }
        void place_door_where_player_entered(ref bool hasDoor, ref int playerX, ref int playerY, string playerFacing, int width, int length, Biome biome, int roomNumber)
        {
            // place door where player entered from
            int x = playerX, y = playerY;

            for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
            {
                if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                {
                    if (playerFacing == "South")
                    {
                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[5].ID, biome.availableTiles[5].Name, biome.availableTiles[5].Dense, x, 0, biome.availableTiles[5].Type);
                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[5].ID, biome.availableTiles[5].Name, biome.availableTiles[5].Dense, x, 0, biome.availableTiles[5].Type), "South", null));
                    }
                    else if (playerFacing == "North")
                    {
                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[6].ID, biome.availableTiles[6].Name, biome.availableTiles[6].Dense, x, length - 1, biome.availableTiles[6].Type);
                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[6].ID, biome.availableTiles[6].Name, biome.availableTiles[6].Dense, x, length, biome.availableTiles[6].Type), "North", null));
                    }
                    else if (playerFacing == "East")
                    {
                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[7].ID, biome.availableTiles[7].Name, biome.availableTiles[7].Dense, 0, y, biome.availableTiles[7].Type);
                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[7].ID, biome.availableTiles[7].Name, biome.availableTiles[7].Dense, 0, y, biome.availableTiles[7].Type), "East", null));
                    }
                    else if (playerFacing == "West")
                    {
                        Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[8].ID, biome.availableTiles[8].Name, biome.availableTiles[8].Dense, width - 1, y, biome.availableTiles[8].Type);
                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.availableTiles[8].ID, biome.availableTiles[8].Name, biome.availableTiles[8].Dense, width, y, biome.availableTiles[8].Type), "West", null));
                    }
                    hasDoor = true;
                }
            }
            if (Rooms[roomNumber].Transitions.Count > 0)
            {
                playerX = Rooms[roomNumber].Transitions[0].X;
                playerY = Rooms[roomNumber].Transitions[0].Y;
            }
        }
        
        void GenerateLoot(ref bool containsLoot, int width, int length, Biome biome, int roomNumber)
        {
            while (!containsLoot)
            {
                Random rand = new Random();

                int x = rand.Next(width);
                int y = rand.Next(length);

                if (!(x == 0 || x == width))
                {
                    if (!(y == 0 || y == length))
                    {
                        for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
                        {
                            if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                            {
                                Rooms[roomNumber].Tiles[i] = new Tile(biome.availableTiles[4]);
                                containsLoot = true;
                            }
                        }
                    }
                }
            }
        }
        void GenerateTiles(int width, int length, Biome biome, int roomNumber)
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile tile = new Tile(0, null, 0, 0, 0, null);
                    if (y == 0)
                    {
                        // make a South Facing wall tile
                        tile = biome.availableTiles[0];
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (y == length - 1)
                    {
                        // make a North Facing wall tile
                        tile = biome.availableTiles[1];
                        tile.X = x;
                        tile.Y = y;
                    }
                    else if (x == 0 || x == width - 1)
                    {
                        // make a  WestEast Facing wall tile
                        tile = biome.availableTiles[2];
                        tile.X = x;
                        tile.Y = y;
                    }
                    else
                    {
                        // make a floor tile
                        tile = biome.availableTiles[3];
                        tile.X = x;
                        tile.Y = y;
                    }
                    Rooms[roomNumber].Tiles.Add(tile);
                }
            }
        }
        void SpawnMonster(ref bool hasMonster, int length, int width, Biome biome, int roomNumber)
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
                        //int i = rand.Next(biome.availableMonsters.Count - 1);
                        //Rooms[roomNumber].MonsterLivingHere = new Monster(biome.availableMonsters[i]);
                        //hasMonster = true;
                    }
                }
            }
        }
    }
}