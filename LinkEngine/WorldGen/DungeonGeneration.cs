using System;
using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class DungeonGeneration
    {
        public string Name { get; set; }
        public List<Room> Rooms { get; set; }

        Random rand = new Random();
        public void BuildMap(int numOfRooms, Biome biome, int playerX, int playerY)
        {
            Rooms = new List<Room>();
            for (int i = 0; i < numOfRooms; i++)
            {
                GenerateRoom(biome, i, "", "", rand.Next(5, 10), rand.Next(5, 10), ref playerX, ref playerY, "South");
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
        /// <returns>Void</returns>
        public void GenerateRoom(Biome biome, int id, string name, string desc, int width, int length, ref int playerX, ref int playerY, string playerFacing)
        {
            bool hasDoor = false;

            // Generate a room based on the biome and tile size
            Rooms.Add(new Room(id, name, desc, width, length));

            if (biome.AvailibleTiles.Count != 0)
            {
                GenerateTiles(width, length, biome, Rooms.Count - 1);

                // Room needs atleast one door
                GenerateDoors(ref hasDoor, ref playerX, ref playerY, playerFacing, width, length, biome, Rooms.Count - 1);

                // Room should include some harvestable item (Treasure, Plant)
                //GenerateLoot(ref containsLoot, width, length, biome, Rooms.Count - 1);
            }

            //if (biome.AvailibleMonsters.Count != 0)
            //{
            //    SpawnMonster(ref hasMonster, length, width, biome, Rooms.Count - 1);
            //}
        }

        public void GenerateHallway(Biome biome, int width, int length, ref int playerX, ref int playerY, string Facing, int roomNumber)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (Facing == "North" || Facing == "South")
                    {
                        if (j == 0 || j == width)
                        {
                            Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[2].ID, biome.AvailibleTiles[2].Name, biome.AvailibleTiles[2].Dense, i, j, biome.AvailibleTiles[2].Type));
                        }
                    }
                    if (Facing == "East" || Facing == "West")
                    {
                        if (i == 0)
                        {
                            // make a South Facing wall tile
                            Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[0].ID, biome.AvailibleTiles[0].Name, biome.AvailibleTiles[0].Dense, i, j, biome.AvailibleTiles[0].Type));
                        }
                        else if (i == length - 1)
                        {
                            // make a North Facing wall tile
                            Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[1].ID, biome.AvailibleTiles[1].Name, biome.AvailibleTiles[1].Dense, i, j, biome.AvailibleTiles[1].Type));
                        }
                    }
                }
            }
        }
        void GenerateDoors(ref bool hasDoor, ref int playerX, ref int playerY, string playerFacing, int width, int length, Biome biome, int roomNumber)
        {
            while (!hasDoor)
            {
                Random rand = new Random();

                // place door where player entered from
                int x = playerX, y = playerY;

                for (int i = 0; i < Rooms[roomNumber].Tiles.Count; i++)
                {
                    if (Rooms[roomNumber].Tiles[i].X == x && Rooms[roomNumber].Tiles[i].Y == y)
                    {
                        if (playerFacing == "South")
                        {
                            Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[5].ID, biome.AvailibleTiles[5].Name, biome.AvailibleTiles[5].Dense, x, 0, biome.AvailibleTiles[5].Type);
                            Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[5].ID, biome.AvailibleTiles[5].Name, biome.AvailibleTiles[5].Dense, x, 0, biome.AvailibleTiles[5].Type), "South", null));
                        }
                        else if (playerFacing == "North")
                        {
                            Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[6].ID, biome.AvailibleTiles[6].Name, biome.AvailibleTiles[6].Dense, x, length - 1, biome.AvailibleTiles[6].Type);
                            Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[6].ID, biome.AvailibleTiles[6].Name, biome.AvailibleTiles[6].Dense, x, length, biome.AvailibleTiles[6].Type), "North", null));
                        }
                        else if (playerFacing == "East")
                        {
                            Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[7].ID, biome.AvailibleTiles[7].Name, biome.AvailibleTiles[7].Dense, 0, y, biome.AvailibleTiles[7].Type);
                            Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[7].ID, biome.AvailibleTiles[7].Name, biome.AvailibleTiles[7].Dense, 0, y, biome.AvailibleTiles[7].Type), "East", null));
                        }
                        else if (playerFacing == "West")
                        {
                            Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[8].ID, biome.AvailibleTiles[8].Name, biome.AvailibleTiles[8].Dense, width - 1, y, biome.AvailibleTiles[8].Type);
                            Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[8].ID, biome.AvailibleTiles[8].Name, biome.AvailibleTiles[8].Dense, width, y, biome.AvailibleTiles[8].Type), "West", null));
                        }
                        hasDoor = true;
                    }
                }
                playerX = Rooms[roomNumber].Transitions[0].X;
                playerY = Rooms[roomNumber].Transitions[0].Y;

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
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[7].ID, biome.AvailibleTiles[7].Name, biome.AvailibleTiles[7].Dense, x, y, biome.AvailibleTiles[7].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[7].ID, biome.AvailibleTiles[7].Name, biome.AvailibleTiles[7].Dense, x, y, biome.AvailibleTiles[7].Type), "East", null));
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
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[8].ID, biome.AvailibleTiles[8].Name, biome.AvailibleTiles[8].Dense, x, y, biome.AvailibleTiles[8].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[8].ID, biome.AvailibleTiles[8].Name, biome.AvailibleTiles[8].Dense, x, y, biome.AvailibleTiles[8].Type), "West", null));
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
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[5].ID, biome.AvailibleTiles[5].Name, biome.AvailibleTiles[5].Dense, x, y, biome.AvailibleTiles[5].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[5].ID, biome.AvailibleTiles[5].Name, biome.AvailibleTiles[5].Dense, x, y, biome.AvailibleTiles[5].Type), "South", null));
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
                                        Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[6].ID, biome.AvailibleTiles[6].Name, biome.AvailibleTiles[6].Dense, x, y, biome.AvailibleTiles[6].Type);
                                        Rooms[roomNumber].Transitions.Add(new Transition(new Tile(biome.AvailibleTiles[6].ID, biome.AvailibleTiles[6].Name, biome.AvailibleTiles[6].Dense, x, y, biome.AvailibleTiles[6].Type), "North", null));
                                    }
                                }
                            }
                        }
                    } while (!(x == 0 || x == width || y == 0 || y == length));
                }
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
                                Rooms[roomNumber].Tiles[i] = new Tile(biome.AvailibleTiles[4]);
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
                    if (y == 0)
                    {
                        // make a South Facing wall tile
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[0].ID, biome.AvailibleTiles[0].Name, biome.AvailibleTiles[0].Dense, x, y, biome.AvailibleTiles[0].Type));
                    }
                    else if (y == length - 1)
                    {
                        // make a North Facing wall tile
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[1].ID, biome.AvailibleTiles[1].Name, biome.AvailibleTiles[1].Dense, x, y, biome.AvailibleTiles[1].Type));
                    }
                    else if (x == 0 || x == width - 1)
                    {
                        // make a  WestEast Facing wall tile
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[2].ID, biome.AvailibleTiles[2].Name, biome.AvailibleTiles[2].Dense, x, y, biome.AvailibleTiles[2].Type));
                    }
                    else
                    {
                        // make a floor tile
                        Rooms[roomNumber].Tiles.Add(new Tile(biome.AvailibleTiles[3].ID, biome.AvailibleTiles[3].Name, biome.AvailibleTiles[3].Dense, x, y, biome.AvailibleTiles[3].Type));
                    }
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
                        //int i = rand.Next(biome.AvailibleMonsters.Count - 1);
                        //Rooms[roomNumber].MonsterLivingHere = new Monster(biome.AvailibleMonsters[i]);
                        //hasMonster = true;
                    }
                }
            }
        }
    }
}