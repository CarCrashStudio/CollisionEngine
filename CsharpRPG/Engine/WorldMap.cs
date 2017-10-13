using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using _2D_Graphics_Engine.Engine;

namespace CsharpRPG.Engine
{
    public class WorldMap // Class holding the play area and tiles inside
    {
        World world;

        int x = 0;
        int y = 0; // Tile Coords vars

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

            BuildMap();
            CalibrateMap();
            SetMap();
        }
        
        public void ShiftMap(int x, int y)
        {
            MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);            
        }
        public void SetMap()
        {
            world.HudForm.Image = ScreenObject.Draw(world.HudForm.Width, world.HudForm.Height, new Point(MapLoc.X * 32, (MapLoc.Y - 10) * 32), Image);
        }

        /* BuildMap()
         * Create a new Map Location Point
         * Create a New Image of the map
         */
        void BuildMap() 
        {
            MapLoc = new Point(0, 0); // Initialize the Point for the Map Location
            Image = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(Name, Properties.Resources.Culture));
            Image = ScreenObject.Draw(world.HudForm.Width, world.HudForm.Height, new Point(0, 0), Image);

            TilesOnMap = new List<Tile>();
            ReadTextFile(TilesOnMap, Name + "Text");
            DrawBuidlings();

            DecosOnMap = new List<Tile>();
            ReadTextFile(DecosOnMap, Name + "Decos");
            DrawDecorations();
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
        void ReadTextFile(List<Tile> TileList, string TextFile)
        {
            x = 0;
            y = 0;
            int id = 0; // Variable for Tile Id;
            Tile tile; // Blank Tile;

            string str = Properties.Resources.ResourceManager.GetObject(TextFile, Properties.Resources.Culture).ToString();

            for (int i = 0; i < str.Length;)
            {
                string currentChar = str[i].ToString();
                i++;
                while (!currentChar.Contains(" "))
                {
                    if (!currentChar.Contains(";")) //HERES THE PROBLEM
                    {
                        if (currentChar.Contains(" ")) break;
                        else { currentChar += str[i]; i++; }
                    }
                    else { currentChar = currentChar.Remove(currentChar.Length - 1); break; }
                }

                id = int.Parse(currentChar);

                tile = new Tile(world.TileByID(id));
                tile.Location = new Point(x, y);

                TileList.Add(tile);
                x++;
                if (x > (world.WIDTH / 32) - 1) { y++; x = 0; }
            }
        }
        void DrawBuidlings()
        {
            foreach (Tile tile in TilesOnMap)
            {
                if(tile.ID > 999 && tile.ID < 10000)
                {
                    Image = ScreenObject.Draw(world.HudForm.Width, world.HudForm.Height, new Point(tile.Location.X *32, tile.Location.Y *32), tile.Image, Image);
                }
            }
        }
        void DrawDecorations()
        {
            foreach (Tile tile in DecosOnMap)
            {
                if (tile.ID > 199 && tile.ID < 300)
                {
                    Image = ScreenObject.Draw(world.HudForm.Width, world.HudForm.Height, new Point(tile.Location.X * 32, tile.Location.Y * 32), tile.Image, Image);
                }
            }
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

