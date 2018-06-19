using System.Collections.Generic;
using LinkEngine.WorldGen;

namespace LinkEngine.RPG
{
    public class Location : Room
    {
        /// <summary>
        /// THe Item the player must have in inventory to enter location
        /// </summary>
        public RPGItem ItemRequiredToEnter { get; set; }
        /// <summary>
        /// A quest that can be given to the player when they enter the location
        /// </summary>
        public Quest QuestAvailableHere { get; set; }

        /// <summary>
        /// The monster that can spawn in this location
        /// </summary>
        public Monster MonsterLivingHere { get; set; }

        /// <summary>
        /// All NPCs available to interact with in this room
        /// </summary>
        public List<NPC> NPCsLivingHere { get; set; }

        /// <summary>
        /// Creates a new Location based on the given parameters
        /// Intitializes NPCsLivingHere
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        public Location (int id, string name, string desc, int width, int length) : base (id, name, desc, width, length)
        {
            NPCsLivingHere = new List<NPC>();
        }
    }
}
