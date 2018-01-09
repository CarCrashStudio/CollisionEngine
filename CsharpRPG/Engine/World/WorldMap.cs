using System;
using System.Collections.Generic;

namespace RPG.Engine
{
    public class WorldMap // Class holding the play area and tiles inside
    {
        public string Name { get; set; }
        public List<Tile> TilesOnMap;

        public WorldMap(string _name)
        {
            Name = _name;
            TilesOnMap = new List<Tile>();
        }

        void BuildMap(string name)
        {

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
        void ProceduralGeneration (Biome biome, int id, string name, string desc, int width, int length, int playerX, int playerY, string playerFacing)
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
    public class Tile// Class to hold tile information 
    {
        const int ICON_SIZE = 32; // 32x32 icons

        int dense;

        public int ID { get; set; }
        public string Name { get; set; }
        public int Dense { get { return dense; } set { dense = value; } } // Tile Density (Can you walk over it or not)

        public int X { get; set; }
        public int Y { get; set; }

        public string Type { get; set; }

        public Tile(int _id, string _name, int _dense, int x, int y, string type)
        {
            ID = _id;
            Name = _name;
            dense = _dense;
            Type = type;
            X = x;
            Y = y;
        }
        public Tile(Tile tile)
        {
            ID = tile.ID;
            Name = tile.Name;
            dense = tile.Dense;
            Type = tile.Type;
            X = tile.X;
            Y = tile.Y;
        }
    } 
}

