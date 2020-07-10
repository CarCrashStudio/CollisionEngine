using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG
{
    public enum Skills
    {
        Perception = 0,
        Athletics = 1,
        Performance = 1,
        Deception = 1,
        Intimidation = 1,
        Survival = 1,
    }
    public enum SpellSaves
    {
        Strength = 0,
        Dexterity = 1,
        Constitution = 2,
        Wisdom = 3,
        Intelligence = 4,
        Charisma = 5,
    }
    public enum StatusEffects
    {
        Poison = 0,
        Exhausted = 1,
        Frightened = 2,
    }
    public enum WeaponType
    {
        SimpleMelee = 0,
        SimpleRange = 0,
        MartialMelee = 0,
        MartialRange = 0,
    }
    public enum Dice
    {
        d4 = 4,
        d6 = 6,
        d8 = 8,
        d10 =10,
        d12 = 12,
        d20 = 20
    }
    public enum SpellcastingTrait
    {
        Strength = 0,
        Dexterity = 1,
        Constitution = 2,
        Wisdom = 3,
        Intelligence = 4,
        Charisma = 5,
    }
}
