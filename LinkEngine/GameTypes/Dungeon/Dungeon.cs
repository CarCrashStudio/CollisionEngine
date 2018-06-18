using LinkEngine.WorldGen;
using System.Collections.Generic;

namespace LinkEngine.Dungeon
{
    public class Dungeon
    {
        List<DungeonItem> ItemsInDungeon;

        DungeonGeneration generator = new DungeonGeneration();
        public Dungeon(string _name, int numOfRooms, Biome biome, int playerX, int playerY)
        {
            generator.BuildMap(numOfRooms, biome, playerX, playerY);
            RandomizeItems();
        }

        // Dungeon Item Creator
        void RandomizeItems ()
        {

        }
    }
} 

