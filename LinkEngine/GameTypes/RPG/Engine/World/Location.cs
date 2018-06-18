using System.Collections.Generic;
using LinkEngine.WorldGen;

namespace LinkEngine.RPG
{
    public class Location : Room
    {
        public RPGItem ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }

        public Monster MonsterLivingHere { get; set; }
        public List<NPC> NPCsLivingHere { get; set; }

        public Location (int id, string name, string desc, int width, int length) : base (id, name, desc, width, length)
        {
            NPCsLivingHere = new List<NPC>();
        }
    }
}
