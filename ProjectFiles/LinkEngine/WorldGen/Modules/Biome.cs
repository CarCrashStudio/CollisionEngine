using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class Biome
    {
        /// <summary>
        /// The ID of the Biome
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the Biome
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// All tiles that can be used in this Biome
        /// </summary>
        public List<Tile> availableTiles { get; set; }
        public List<Entities.Enemy> AvailableEnemies { get; set; }
        /// <summary>
        /// All Biomes this Biome can connect to
        /// </summary>
        public List<Biome> RelatedBiomes { get; set; }

        /// <summary>
        /// Creates a new world biome using the given parameters
        /// </summary>
        /// <param name="id">The ID of the Biome</param>
        /// <param name="name">The Name of the Biome</param>
        /// <param name="tileAry">All Tiles that can be used in this biome</param>
        /// <param name="biomeAry">Biomes that can connect to this biome</param>
        public Biome(int id, string name, Tile[] tileAry, Biome[] biomeAry)
        {
            ID = id;
            Name = name;
            availableTiles = new List<Tile>();

            for(int i = 0; i < tileAry.Length; i++)
            {
                availableTiles.Add(tileAry[i]);
            }
            if (biomeAry != null)
            {
                for (int i = 0; i < biomeAry.Length; i++)
                {
                    RelatedBiomes.Add(biomeAry[i]);
                }
            }
        }
    }
}
