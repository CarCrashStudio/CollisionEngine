using LinkEngine.WorldGen;

namespace LinkEngine.Dungeon
{
    public class Dungeon
    {
        DungeonGeneration generator = new DungeonGeneration();
        public Dungeon(string _name, int numOfRooms, Biome biome, int playerX, int playerY)
        {
            generator.BuildMap(numOfRooms, biome, playerX, playerY);
        }
    }
} 

