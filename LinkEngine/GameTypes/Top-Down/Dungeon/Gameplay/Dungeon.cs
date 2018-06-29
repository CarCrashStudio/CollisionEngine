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

        public void LoadItemImages()
        {
            PotionImages = new List<string>();
            HelmetImages = new List<string>();
            TorsoImages = new List<string>();
            LegImages = new List<string>();
            SwordImages = new List<string>();
            ShieldImages = new List<string>();

            // load them here
        }

        // Indicies for Biome Lists
        // --- South Facing Wall Tile [0]
        // --- North Facing Wall Tile [1]
        // --- EastWest Facing Wall Tile [2]
        // --- Floor Tile [3]
        // --- Chest Tile [4]
        // --- South Facing Door Tile [5]
        // --- North Facing Door Tile [6]
        // --- East Facing Door Tile [7]
        // --- West Facing Door Tile [8]
    }
} 

