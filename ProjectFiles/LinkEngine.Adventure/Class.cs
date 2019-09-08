using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkEngine.Adventure
{
    /// <summary>
    /// Class defines an rpg class type. Class contans base values for the 7 skill traits. 
    /// </summary>
    public class Class
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Strength is used to calculate combat damage
        /// </summary>
        public short Strength { get; set; }
        /// <summary>
        /// Perception is used to discover the map more efficiently
        /// </summary>
        public short Perception { get; set; }
        /// <summary>
        /// Endurance is used when calculating blocking damage
        /// </summary>
        public short Endurance { get; set; }
        /// <summary>
        /// Charisma is used to gain dialoge options
        /// </summary>
        public short Charisma { get; set; }
        /// <summary>
        /// Intelligence is used to think up crafting recipes
        /// </summary>
        public short Intelligence { get; set; }
        /// <summary>
        /// Agility is used to calculate blocking ability
        /// </summary>
        public short Agility { get; set; }
        /// <summary>
        /// Luck is used to calculate Critical Hit chance and Blocking ability
        /// </summary>
        public short Luck { get; set; }

        public List<Modifier> StrengthModifiers { get; set; }
        public List<Modifier> PerceptionModifiers { get; set; }
        public List<Modifier> EnduranceModifiers { get; set; }
        public List<Modifier> CharismaModifiers { get; set; }
        public List<Modifier> IntelligenceModifiers { get; set; }
        public List<Modifier> AgilityModifiers { get; set; }
        public List<Modifier> LuckModifiers { get; set; }

        public int HP { get; set; }
        public int Mana { get; set; }

        public Class(string name, string desc, short str, short per, short end, short cha, short inte, short agi, short luc, int hp, int mana)
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
