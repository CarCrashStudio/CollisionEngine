using System.Collections.Generic;
using LinkEngine.Entities;
using LinkEngine.Gameplay.Items;
namespace LinkEngine.WorldGen
{
    public class Location
    {
        /// <summary>
        /// THe Item the player must have in inventory to enter location
        /// </summary>
        public Item ItemRequiredToEnter { get; set; }

        /// <summary>
        /// The monster that can spawn in this location
        /// </summary>
        public Enemy MonsterLivingHere { get; set; }

        /// <summary>
        /// All NPCs available to interact with in this room
        /// </summary>
        public List<NPC> NPCsLivingHere { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        public List<Tile> Tiles { get; set; }

        public List<Transition> Transitions { get; set; }

        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

        public Location(int _id, string _name, string _desc, int width, int length)
        {
            ID = _id;
            Name = _name;
            Description = _desc;
            Width = width;
            Length = length;
            Tiles = new List<Tile>();
            Transitions = new List<Transition>();
        }
    }
}
