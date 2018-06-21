using System.Collections.Generic;
using LinkEngine.Rendering;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace LinkEngine.WorldGen
{
    class SetGeneration
    {
        int x = 0;

        int y = 0; // Tile Coords vars

        Bitmap Image;

        /// <summary>
        /// List of all tiles currently on the map
        /// </summary>
        public List<Tile> TilesOnMap { get; set; }
        /// <summary>
        /// List of all decorations currently on map (fences, buildings etc)
        /// </summary>
        public List<Tile> DecosOnMap { get; set; }
        /// <summary>
        /// The Map Location point is used when shifting the map to be center on the player / camera
        /// </summary>
        public Point MapLoc { get; set; }

        /// <summary>
        /// SetGeneration creates a new map from a text resource with id's pre laid out
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        public SetGeneration(string Name, int Width, int Height, int centerX, int centerY, int playerX, int playerY)
        {
            BuildMap(Width, Height, Name);
            CalibrateMap(centerX, centerY, playerX, playerY);
            SetMap(Width, Height);
        }

        /// <summary>
        /// ShiftMap moves the map in any direction
        /// Used when keeping the player center screen
        /// </summary>
        /// <param name="x">the amount to shift the map on the X axis</param>
        /// <param name="y">the amount to shift the map on the Y axis</param>
        public void ShiftMap(int x, int y)
        {
            MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);
        }

        /// <summary>
        /// SetMap returns the drawn map to use in pictureboxes
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public Bitmap SetMap(int Width, int Height)
        {
            return ScreenObject.Draw(Width, Height, new Point(MapLoc.X * 32, (MapLoc.Y - 10) * 32), null);
        }

        /// <summary>
        /// Creates a new Map Location Point
        /// and builds a new map image
        /// </summary>
        /// <param name="Width">Width of the canvas to draw on</param>
        /// <param name="Height">Height of the canvas to draw on</param>
        /// <param name="Name">Name of the world</param>
        void BuildMap(int Width, int Height, string Name)
        {
            MapLoc = new Point(0, 0); // Initialize the Point for the Map Location
            Image = new Bitmap("maps/worldmap.png");
            Image = ScreenObject.Draw(Width, Height, new Point(0, 0), null);

            TilesOnMap = new List<Tile>();
            ReadTextFile(TilesOnMap, Name, Width);
            DrawBuidlings(Width, Height);

            DecosOnMap = new List<Tile>();
            ReadTextFile(DecosOnMap, Name + "Decos", Width);
            DrawDecorations(Width, Height);

        }

        /// <summary>
        /// CalibrateMap will adjust the map location so that the player can be centered on the screen
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        void CalibrateMap(int centerX, int centerY, int playerX, int playerY)
        {
            MapLoc = new Point(
                    // X Coordinate
                    centerX - playerX,

                    // Y Coordinate
                    centerY - playerY
                );
        }

        /// <summary>
        /// ReadTextFile will take a text file from the resource file and read the data
        /// </summary>
        /// <param name="TileList"></param>
        /// <param name="TextFile"></param>
        /// <param name="Width"></param>
        void ReadTextFile(List<Tile> TileList, string TextFile, int Width)
        {
            int id = 0; // Variable for Tile Id;

            Tile tile; // Blank Tile;

            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(TextFile));

            while (!reader.EndOfStream)
            {
                string currentChar = char.ConvertFromUtf32(reader.Read());

                while (!currentChar.Contains(" "))
                {
                    if (!currentChar.Contains(";")) //HERES THE PROBLEM
                    {
                        if (currentChar.Contains(" ")) break;
                        else { currentChar += char.ConvertFromUtf32(reader.Read()); }
                    }
                    else { currentChar = currentChar.Remove(currentChar.Length - 1); break; }
                }

                id = int.Parse(currentChar);

                tile = new Tile(World.TileByID(id));

                tile.X = x;
                tile.Y = y;

                TileList.Add(tile);

                x++;
                if (x > (Width / 32) - 1) { y++; x = 0; }
            }
            reader.Close();
        }

        /// <summary>
        /// DrawBuildings will take text input and draw buildings at there top left most corner
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        void DrawBuidlings(int Width, int Height)
        {
            foreach (Tile tile in TilesOnMap)
            {
                if (tile.ID > 999 && tile.ID < 10000)
                {
                    Image = ScreenObject.Draw(Width,Height, new Point(tile.X * 32, tile.Y * 32), Image);
                }
            }
        }

        /// <summary>
        /// DrawDecorations will take text input and  the map image and draw decorations over top of the map image
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        void DrawDecorations(int Width, int Height)
        {
            foreach (Tile tile in DecosOnMap)
            {
                if (tile.ID > 199 && tile.ID < 300)
                {
                    Image = ScreenObject.Draw(Width, Height, new Point(tile.X * 32, tile.Y * 32), Image);
                }
            }
        }
    }
}
