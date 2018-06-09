using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class Biome
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Tile> AvailibleTiles { get; set; }

        public Biome(int id, string name, Tile[] tileAry)
        {
            ID = id;
            Name = name;
            AvailibleTiles = new List<Tile>();

            for(int i = 0; i < tileAry.Length; i++)
            {
                AvailibleTiles.Add(tileAry[i]);
            }
        }
    }
}
