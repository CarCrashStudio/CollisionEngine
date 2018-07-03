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
        /// SetGeneration creates a new map from a text resource with id's pre laid out
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="playerX"></param>
        /// <param name="playerY"></param>
        public SetGeneration(string Name, int Width, int Height)
        {
            BuildMap(Width, Height, Name);
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
            Image = new Bitmap("maps/worldmap.png");
            // Image = ScreenObject.Draw(Width, Height, new Point(0, 0), null);

            TilesOnMap = new List<Tile>();
            ReadTextFile(TilesOnMap, Name, Width);
            DrawBuidlings(Width, Height);

            DecosOnMap = new List<Tile>();
            ReadTextFile(DecosOnMap, Name + "Decos", Width);
            DrawDecorations(Width, Height);

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

                tile = new Tile(World.TileBy(id));

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
                    // Image = ScreenObject.Draw(Width,Height, new Point(tile.X * 32, tile.Y * 32), Image);
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
                    // Image = ScreenObject.Draw(Width, Height, new Point(tile.X * 32, tile.Y * 32), Image);
                }
            }
        }
    }
}
