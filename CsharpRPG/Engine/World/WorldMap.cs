using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RPG.Engine
{
    public class WorldMap // Class holding the play area and tiles inside
    {
        World world;

        public List<Tile> TilesOnMap { get; set; } // List of Tiles on map
        public List<Tile> DecosOnMap { get; set; }
        public PictureBox gameForm { get; set; } // Picture box to display on
        public Bitmap Image { get; set; }
        public string Name { get; set; }
        public Point MapLoc { get; set; }

        public WorldMap(string _name, World _world)
        {
            world = _world;
            Name = _name;
            Image = new Bitmap(world.WIDTH, world.HEIGHT);
        }
    }

    public class Tile// Class to hold tile information 
    {
        const int ICON_SIZE = 32; // 32x32 icons

        int dense;
        Point location;

        public int ID { get; set; }
        public string Name { get; set; }
        public Bitmap Image { get; set; }
        public int Dense { get { return dense; } set { dense = value; } } // Tile Density (Can you walk over it or not)
        public Point Location { get { return location; } set { location = value; } } // Tile Point
        public string Type { get; set; }

        public Tile(int _id, string _name, int _dense, Point _location, Bitmap _img, string type)
        {
            ID = _id;
            Name = _name;
            Image = _img;
            dense = _dense;
            location = _location;
            Type = type;
        }
        public Tile(Tile tile)
        {
            ID = tile.ID;
            Name = tile.Name;
            Image = tile.Image;
            dense = tile.Dense;
            location = tile.Location;
            Type = tile.Type;
        }
    } 
}

