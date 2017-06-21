using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace RPG_Engine
{
    public class WorldMap // Class holding the play area and tiles inside
    {
        World world;

        int x = 0;
        int y = 0; // Tile Coords vars
        string name; // World/File name

        public string Name { get { return name; } set { name = value; } } // Map name
        public List<Tile> TilesOnMap { get; set; } // List of Tiles on map
        public PictureBox gameForm { get; set; } // Picture box to display on
        public Bitmap CurrentMap { get; set; } // Bitmap to save full map to, increase load time
        public Point MapLoc { get; set; }

        public WorldMap(string _name, PictureBox _gameForm, World _world)
        {
            name = _name;
            gameForm = _gameForm;
            world = _world;

            //CurrentMap = (Bitmap)world.charForm.Image;

            BuildMap();
            CalibrateMap();
            SetMap();
        }

        void BuildMap() // read through a text file of IDs and places them in the CurrentMap bitmap 
        {
            MapLoc = new Point(0, 0);
            CurrentMap = (Bitmap)world.charForm.Image;

            int id = 0;
            Tile tile;

            TilesOnMap = new List<Tile>();
            StreamReader reader;
            reader = File.OpenText("maps/" + name + ".txt");

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
                    else { reader.Read(); break; }                    
                }

                string read = "";
                read = currentChar;
                id = int.Parse(read);

                tile = new Tile(world.TileByID(id));
                tile.Location = new Point(x, y);

                CurrentMap = DrawTile(tile);
                CurrentMap.Save(name + ".png");

                TilesOnMap.Add(tile);
                x++;
                if (x > world.MAX_MAP_SIZE) { y++; x = 0; }
            }
            reader.Close();
        } 
        public Bitmap DrawTile(Tile tile) // Draw the tile to the CurrentMap Bitmap 
        {
            var bitmap = new Bitmap(CurrentMap, world.WIDTH, world.HEIGHT);  
            var graphics = Graphics.FromImage(bitmap);

            graphics.DrawImage(tile.Image, new Point((tile.Location.X * 32), (tile.Location.Y * 32)));

            return bitmap;
        }

        public void ShiftMap(int x, int y)
        {
            MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);
        }
        Bitmap DrawMap()
        {
            var bitmap = new Bitmap(gameForm.Width, gameForm.Height);
            var graphics = Graphics.FromImage(bitmap);

            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage(CurrentMap, new Point(MapLoc.X * 32, MapLoc.Y * 32));

            return bitmap;
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
        { gameForm.Image = DrawMap(); }
    } 
    public class Tile // Class to hold tile information 
    {
        const int ICON_SIZE = 32; // 32x32 icons

        int id;
        string name;
        int dense;
        Point location;
        Bitmap img;

        public int ID { get { return id; } set { id = value; } } // Tile ID
        public string Name { get { return name; } set { name = value; } } // Tile Name
        public int Dense { get { return dense; } set { dense = value; } } // Tile Density (Can you walk over it or not)
        public Point Location { get { return location; } set { location = value; } } // Tile Point
        public Bitmap Image { get { return img; } set { img = value; } } // Tile Image

        public Tile(int _id, string _name, int _dense, Point _location, Bitmap _img)
        {
            id = _id;
            name = _name;
            dense = _dense;
            location = _location;
            img = _img;
        }
        public Tile(Tile tile)
        {
            id = tile.ID;
            name = tile.Name;
            dense = tile.Dense;
            location = tile.Location;
            img = tile.Image;
        }
    } 
}

