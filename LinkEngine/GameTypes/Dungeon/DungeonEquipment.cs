using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Dungeon
{
    public class DungeonEquipment : DungeonItem
    {
        public short Slot { get; set; }

        public short Strength { get; set; }
        public short Defense { get; set; }

        public bool Equipped { get; set; }

        public DungeonEquipment (int _id, string _name, string _namePlural, short str, short def, bool equipable, bool consumable) : base(_id, _name, _namePlural, equipable, consumable)
        {
            Strength = str;
            Defense = def;
        }
    }
}
