using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG.Models
{
    public class Weapon : Equipment
    {
        public WeaponType Type { get; set; }
        public Dice DamageDice { get; set; }
        public int OffHandBonus { get { if (Slots.Contains(Models.Slots.Off)) return 2; else return 0; } }

        public int DamageModifier { get; set; }

        public Weapon (int id, string name, string namePlural, Currency cost, Texture2D texture, Slots[] slots, Attributes mod, int damage) :
            base (new Item(id, name, namePlural, cost, texture), slots, mod)
        {
            DamageModifier = damage;
        }
    }
}
