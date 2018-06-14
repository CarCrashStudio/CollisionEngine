using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Tile> TilesOnMap { get; set; } // List of Tiles on map

        public List<Tile> DecosOnMap { get; set; }

        public Point MapLoc { get; set; }



        public SetGeneration(string Name, int Width, int Height, int centerX, int centerY, int playerX, int playerY)
        {
            BuildMap(Width, Height, Name);

            CalibrateMap(centerX, centerY, playerX, playerY);

            SetMap(Width, Height);

        }

        public void ShiftMap(int x, int y)

        {

            MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);

        }

        public Bitmap SetMap(int Width, int Height)
        {
            return ScreenObject.Draw(Width, Height, new Point(MapLoc.X * 32, (MapLoc.Y - 10) * 32), null);
        }



        /* BuildMap()

         * Create a new Map Location Point

         * Create a New Image of the map

         */

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

        void CalibrateMap(int centerX, int centerY, int playerX, int playerY)

        {

            MapLoc = new Point(

                    // X Coordinate

                    centerX - playerX,



                    // Y Coordinate

                    centerY - playerY

                );

        }

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



                tile = new Tile(Tile.ByID(id, TileList));

                tile.X = x;
                tile.Y = y;



                TileList.Add(tile);

                x++;

                if (x > (Width / 32) - 1) { y++; x = 0; }

            }

            reader.Close();

        }

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
