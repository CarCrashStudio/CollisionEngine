using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class Biome
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Tile> availableTiles { get; set; }
        public List<Biome> RelatedBiomes { get; set; }

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
