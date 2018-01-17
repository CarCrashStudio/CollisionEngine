using System;
using System.Collections.Generic;

namespace RPG.Engine
{
    public class Dungeon // Class holding the play area and tiles inside
    {
        public string Name { get; set; }
        Random rand = new Random();

        public Dungeon(string _name, int numOfRooms, Biome biome)
        {
            Name = _name;
            BuildMap(_name, numOfRooms, biome);
        }

        void BuildMap(string name, int numOfRooms, Biome biome)
        {
            for (int i = 0; i < numOfRooms; i++)
            {
                GenerateRoom(biome, i, "", "", rand.Next(5, 10), rand.Next(5, 10), 0, 0, "South");
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
        // --- EastWest Facing Door Tile [7]

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
        public void GenerateRoom (Biome biome, int id, string name, string desc, int width, int length, int playerX, int playerY, string playerFacing)
        {
            bool containsLoot = false, hasMonster = false, hasDoor = false;

            // Generate a room based on the biome and tile size
            Location procLoc = new Location(id, name, desc, width, length);

            for(int y = 0; y < length; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if(y == 0)
                    {
                        // make a South Facing wall tile
                        procLoc.Tiles.Add(biome.AvailibleTiles[0]);
                    }
                    else if(y == length - 1)
                    {
                        // make a North Facing wall tile
                        procLoc.Tiles.Add(biome.AvailibleTiles[1]);
                    }
                    else if(x == 0 || x == width - 1)
                    {
                        // make a  WestEast Facing wall tile
                        procLoc.Tiles.Add(biome.AvailibleTiles[2]);
                    }
                    else
                    {
                        // make a floor tile
                        procLoc.Tiles.Add(biome.AvailibleTiles[3]);
                    }
                }
            }

            // Room needs atleast one door
            while (!hasDoor)
            {
                Random rand = new Random();

                // place door where player entered from
                int x = playerX, y = playerY;

                for (int i = 0; i < procLoc.Tiles.Count; i++)
                {
                    if (procLoc.Tiles[i].X == x && procLoc.Tiles[i].Y == y)
                    {
                        if(playerFacing == "South")
                        {
                            procLoc.Transitions.Add(new Transition(biome.AvailibleTiles[5], "South", null));
                        }
                        else if (playerFacing == "North")
                        {
                            procLoc.Transitions.Add(new Transition(biome.AvailibleTiles[6], "North", null));
                        }
                        else if (playerFacing == "East" || playerFacing == "West")
                        {
                            procLoc.Transitions.Add(new Transition(biome.AvailibleTiles[7], playerFacing, null));
                        }
                        hasDoor = true;
                    }
                }

                // Random number of doors to generate
                int numDoors = rand.Next(width);

                x = rand.Next(width);
                y = rand.Next(length);

                // EastWest facing door
                if ((x == 0 || x == width))
                {
                    for (int i = 0; i < procLoc.Tiles.Count; i++)
                    {
                        if (procLoc.Tiles[i].X == x && procLoc.Tiles[i].Y == y)
                        {
                            procLoc.Transitions.Add(new Transition(biome.AvailibleTiles[7], "East", null));
                        }
                    }
                }

                // South facing door
                else if (y == 0)
                {
                    for (int i = 0; i < procLoc.Tiles.Count; i++)
                    {
                        if (procLoc.Tiles[i].X == x && procLoc.Tiles[i].Y == y)
                        {
                            procLoc.Transitions.Add(new Transition(biome.AvailibleTiles[5], "South", null));
                        }
                    }
                }

                // North Facing Door
                else if (y == length)
                {
                    for (int i = 0; i < procLoc.Tiles.Count; i++)
                    {
                        if (procLoc.Tiles[i].X == x && procLoc.Tiles[i].Y == y)
                        {
                            procLoc.Transitions.Add(new Transition(biome.AvailibleTiles[6], "North", null));
                        }
                    }
                }
            }

            // Room should include some harvestable item (Treasure, Plant)
            while (!containsLoot)
            {
                Random rand = new Random();

                int x = rand.Next(width);
                int y = rand.Next(length);

                if(!(x == 0 || x == width))
                {
                    if(!(y == 0 || y == length))
                    {
                        for (int i = 0; i < procLoc.Tiles.Count; i++)
                        {
                            if(procLoc.Tiles[i].X == x && procLoc.Tiles[i].Y == y)
                            {
                                procLoc.Tiles[i] = new Tile(biome.AvailibleTiles[4]);
                                containsLoot = true;
                            }
                        }
                    }
                }
            }

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
                        int i = rand.Next(biome.AvailibleMonsters.Count);
                        procLoc.MonsterLivingHere = new Monster(biome.AvailibleMonsters[i]);
                        hasMonster = true;
                    }
                }
            }
        }
    }
    
} 

