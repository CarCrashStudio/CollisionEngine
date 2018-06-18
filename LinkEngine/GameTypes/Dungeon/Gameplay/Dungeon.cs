using System.Collections.Generic;

namespace LinkEngine.Dungeon
{
    public class Dungeon : WorldGen.World
    {
        public List<DungeonItem> ItemsInDungeon { get; set; }

        List<string> PotionImages;
        List<string> HelmetImages;
        List<string> TorsoImages;
        List<string> LegImages;
        List<string> SwordImages;
        List<string> ShieldImages;

        public Dungeon()
        {
            ItemsInDungeon = new List<DungeonItem>();
        }

        /// <summary>
        /// Randomizes the images used for the Icons
        /// </summary>
        public void RandomizeItems ()
        {
            ItemsInDungeon = new List<DungeonItem>();
        }
    }
} 

