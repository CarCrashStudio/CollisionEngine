using Collision2D.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Collision2D.ProceduralGeneration
{
    /// <summary>
    /// Builds a tile list based on a file of tile IDs
    /// </summary>
    public static class Template
    {
        public static List<Tile> available_tiles;
        /// <summary>
        /// Builds a world from a file containing tile IDs
        /// </summary>
        /// <param name="file">the file to pull data from</param>
        /// <returns></returns>
        public static List<Tile> BuildWorld(string file)
        {
            List<Tile> tiles = new List<Tile>();
            StreamReader reader = File.OpenText(file);
            int x = 0, y = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var IDs = line.Split(',');
                foreach (var id in IDs)
                {
                    Tile tile = new Tile(available_tiles.Where(t => t.ID == int.Parse(id)).Single());
                    tile.Sprite.Position = new Microsoft.Xna.Framework.Vector2(x, y);
                    tiles.Add(tile);
                    x++;
                }
                y++;
            }
            return tiles;
        }
    }
}
