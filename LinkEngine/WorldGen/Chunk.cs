using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class Chunk
    {
        public short Size { get { return 10; } }
        public Biome containsBiome { get; set; }

        // Store the chunks that are adajacent
        // to this current chunk
        public Chunk toNorth { get; set; }
        public Chunk toSouth { get; set; }
        public Chunk toEast { get; set; }
        public Chunk toWest { get; set; }

        // Store the tiles contained in this chunk
        public List<Tile> Tiles = new List<Tile>();
    }
}
