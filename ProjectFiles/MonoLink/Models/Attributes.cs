using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink.Models
{
    public class Attributes
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int CurrentHP { get; set; }
        public int Exp { get; set; }
        public int CurrentExp { get; set; }
        public float AttackSpeed { get; set; }
        public float CurrentAttackSpeed { get; set; }

        public float Speed { get; set; }

        // RPG Modifiers
        public float Strength { get; set; }
        public float Dexterity { get; set; }
        public float Intelligence { get; set; }
        public float Wisdom { get; set; }
        public float Constitution { get; set; }
        public float Charisma { get; set; }

        public static Attributes operator +(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                HP = a.HP + b.HP,
                CurrentHP = a.CurrentHP + b.CurrentHP,
                Exp = a.Exp + b.Exp,
                CurrentExp = a.CurrentExp + b.CurrentExp,

                Speed = a.Speed + b.Speed,

                Strength = a.Strength + b.Strength,
                Dexterity = a.Dexterity + b.Dexterity,
                Intelligence = a.Intelligence + b.Intelligence,
                Wisdom = a.Wisdom + b.Wisdom,
                Constitution = a.Constitution + b.Constitution,
                Charisma = a.Charisma + b.Charisma,
            };
        }
        public static Attributes operator -(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                HP = a.HP - b.HP,
                CurrentHP = a.CurrentHP - b.CurrentHP,
                Exp = a.Exp - b.Exp,
                CurrentExp = a.CurrentExp - b.CurrentExp,

                Speed = a.Speed - b.Speed,

                Strength = a.Strength - b.Strength,
                Dexterity = a.Dexterity - b.Dexterity,
                Intelligence = a.Intelligence - b.Intelligence,
                Wisdom = a.Wisdom - b.Wisdom,
                Constitution = a.Constitution - b.Constitution,
                Charisma = a.Charisma - b.Charisma,
            };
        }
    }
}
