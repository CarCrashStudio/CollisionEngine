using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace CsharpRPG.Engine
{
    public class WorldMap : ScreenObject // Class holding the play area and tiles inside
    {
        World world;

        int x = 0;
        int y = 0; // Tile Coords vars

        public List<Tile> TilesOnMap { get; set; } // List of Tiles on map
        public PictureBox gameForm { get; set; } // Picture box to display on
        public Point MapLoc { get; set; }

        public WorldMap(string _name, World _world) : 
            base(0, _name, new Bitmap(_world.WIDTH, _world.HEIGHT))
        {
            world = _world;

            BuildMap();
            CalibrateMap();
            SetMap();
        }

        void BuildMap() // read through a text file of IDs and places them in the Image bitmap 
        {
            MapLoc = new Point(0, 0); // Initialize the Point for the Map Location

            int id = 0; // Variable for Tile Id;
            Tile tile; // Blank Tile;

            TilesOnMap = new List<Tile>(); 
            StreamReader reader;
            reader = File.OpenText("maps/" + Name + ".txt");

            while (!reader.EndOfStream)
            {
                string currentChar = char.ConvertFromUtf32(reader.Read());
                while (!currentChar.Contains(" "))
                {
                    if(x != world.MAX_MAP_SIZE)
                    {
                        if (currentChar.Contains(" "))
                        {
                            break;
                        }
                        else { currentChar += char.ConvertFromUtf32(reader.Read()); }
                    }
                    else { string temp = char.ConvertFromUtf32(reader.Read()); break; }                    
                }

                string read = "";
                read = currentChar;
                id = int.Parse(read);

                tile = new Tile(world.TileByID(id));
                tile.Location = new Point(x, y);

                Image = tile.Draw(world.WIDTH, world.HEIGHT, new Point((tile.Location.X * 32), (tile.Location.Y * 32)), Image);
                Image.Save(Name + ".png");

                TilesOnMap.Add(tile);
                x++;
                if (x > world.MAX_MAP_SIZE) { y++; x = 0; }
            }
            reader.Close();
        }

        public void ShiftMap(int x, int y)
        {
            MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);            
        }

        void CalibrateMap()
        {
            MapLoc = new Point(
                    // X Coordinate
                    world.CENTER.X - world.player.Location.X,

                    // Y Coordinate
                    world.CENTER.Y - world.player.Location.Y
                );
        }

        public void SetMap()
        {
            world.HudForm.Image = Draw(world.HudForm.Width, world.HudForm.Height, new Point(MapLoc.X * 32, MapLoc.Y * 32));
        }
    } 
    public class Tile : ScreenObject// Class to hold tile information 
    {
        const int ICON_SIZE = 32; // 32x32 icons

        int dense;
        Point location;

        public int Dense { get { return dense; } set { dense = value; } } // Tile Density (Can you walk over it or not)
        public Point Location { get { return location; } set { location = value; } } // Tile Point

        public Tile(int _id, string _name, int _dense, Point _location, Bitmap _img) :
            base(_id, _name, _img)
        {
            dense = _dense;
            location = _location;
        }
        public Tile(Tile tile) : 
            base(tile.ID, tile.Name, tile.Image)
        {
            dense = tile.Dense;
            location = tile.Location;
        }
    } 
}

