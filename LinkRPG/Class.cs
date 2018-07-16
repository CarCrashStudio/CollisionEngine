using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.RPG
{
    /// <summary>
    /// Class defines an rpg class type. Class contans base values for the 7 skill traits. 
    /// </summary>
    public class Class
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public short Strength { get; set; }
        public short Perception { get; set; }
        public short Endurance { get; set; }
        public short Charisma { get; set; }
        public short Intelligence { get; set; }
        public short Agility { get; set; }
        public short Luck { get; set; }

        public int HP { get; set; }
        public int Mana { get; set; }

        public Class (string name, string desc, short str, short per, short end, short cha, short inte, short agi, short luc, int hp, int mana)
        {
            Name = name;
            Description = desc;
            Strength = str;
            Perception = per;
            Endurance = end;
            Charisma = cha;
            Intelligence = inte;
            Agility = agi;
            Luck = luc;
            HP = hp;
            Mana = mana;
        }
    }
}
