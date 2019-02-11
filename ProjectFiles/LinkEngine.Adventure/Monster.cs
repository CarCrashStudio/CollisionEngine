using System;
using System.Linq;

namespace LinkEngine.Adventure
{
    public class Monster : LinkEngine.Entities.Enemy
    {
        public short SpawnChance { get; set; }

        public Monster(int id, string name, int health, int maxHealth, short str, short def, short spawn) :
            base(id, name, health, maxHealth, str, def)
        {
            SpawnChance = spawn;
        }
    }
}
