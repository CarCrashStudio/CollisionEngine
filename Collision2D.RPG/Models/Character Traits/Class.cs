using System;
using System.Collections.Generic;
using System.Linq;

namespace Collision2D.RPG.Models
{
    /// <summary>
    /// Class defines an rpg class type. Class contans base values for the 7 skill traits. 
    /// </summary>
    public class Class
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Speed { get; set; }

        public SpellcastingTrait SpellcastingTrait { get; set; }

        public Class(string name, string desc, int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma, SpellcastingTrait trait)
        {
            Name = name;
            Description = desc;

            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;

            SpellcastingTrait = trait;
        }
    }
}
