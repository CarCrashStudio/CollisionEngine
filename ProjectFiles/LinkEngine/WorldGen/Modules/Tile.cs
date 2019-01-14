﻿namespace LinkEngine.WorldGen
{
    /// <summary>
    /// Tile represents a square world object
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// The ID of the Tile
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The Name of the Tile
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Density of the Tile, meaning is it passable or not
        /// </summary>
        public int Dense { get; set; }

        /// <summary>
        /// The location / room the tile belongs to
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// The X coordinate of the Tile
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The Y coordinate of the Tile
        /// </summary>
        public int Y { get; set; }

        public int Local_X { get; set; }
        public int Local_Y { get; set; }

        /// <summary>
        /// The Tile Type, categotized as Path, Ground, or void
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Creates a brand new tile based on the given parameters
        /// </summary>
        /// <param name="_id">The ID of the tile</param>
        /// <param name="_name">The name of the Tile</param>
        /// <param name="_dense">The Density of the tile</param>
        /// <param name="x">The X coord of the Tile</param>
        /// <param name="y">The Y coord of the Tile</param>
        /// <param name="type">The Type of the Tile</param>
        public Tile(int _id, string _name, int _dense, int x, int y, string type)
        {
            ID = _id;
            Name = _name;
            Dense = _dense;
            Type = type;
            X = x;
            Y = y;

            Local_X = x;
            Local_Y = y;
        }

        /// <summary>
        /// Creates a copy of an already made Tile
        /// </summary>
        /// <param name="tile">The Tile to copy</param>
        public Tile(Tile tile)
        {
            ID = tile.ID;
            Name = tile.Name;
            Dense = tile.Dense;
            Type = tile.Type;
            X = tile.X;
            Y = tile.Y;
        }

        public Tile(Tile tile, int x, int y)
        {
            ID = tile.ID;
            Name = tile.Name;
            Dense = tile.Dense;
            Type = tile.Type;
            X = x;
            Y = y;
        }

        public void UpdateLocalCoords()
        {
            Local_X = X;
            Local_Y = Y;
        }
    }
}
