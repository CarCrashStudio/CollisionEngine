using System.Collections.Generic;

namespace RPG.Engine
{
    public class Biome
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Tile> AvailibleTiles { get; set; }
        public List<Monster> AvailibleMonsters { get; set; }

        public Biome(int id, string name, Tile[] tileAry, Monster[] monsterAry)
        {
            ID = id;
            Name = name;
            AvailibleMonsters = new List<Monster>();
            AvailibleTiles = new List<Tile>();

            for(int i = 0; i < tileAry.Length; i++)
            {
                AvailibleTiles.Add(tileAry[i]);
            }
            for (int i = 0; i < monsterAry.Length; i++)
            {
                AvailibleMonsters.Add(monsterAry[i]);
            }
        }
    }
}
