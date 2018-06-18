using System.Collections.Generic;

namespace LinkEngine.Dungeon
{
    public class Dungeon
    {
        public List<DungeonItem> ItemsInDungeon { get; set; }
        
        public Dungeon()
        {
            ItemsInDungeon = new List<DungeonItem>();

        }

        // Dungeon Item Creator
        public void RandomizeItems ()
        {
            ItemsInDungeon = new List<DungeonItem>();
        }
    }
} 

