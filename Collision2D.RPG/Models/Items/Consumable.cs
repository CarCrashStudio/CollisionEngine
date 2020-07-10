using Collision2D.RPG.Entities;
using System;
using System.Linq;

namespace Collision2D.RPG.Models
{
    /// <summary>
    /// Consumable items can be used to give temporary boosts to stats or regain lost stats like hp and mana
    /// </summary>
    public class Consumable
    {
        int amountToHeal;

        public Item Details { get; set; }
        /// <summary>
        /// Can be used only for a short period of time
        /// </summary>
        public Buff Buff { get; set; }

        /// <summary>
        /// Added to the entities AttributesModifiers on use
        /// </summary>
        public Attributes Mod { get; set; }

        public Consumable (Item details, int _amountToHeal, int _cost)
        {
            Details = details;
            amountToHeal = _amountToHeal;
        }

        public void Use (Entity Target)
        {
            // If the consumable has a buff, add this buff to the entity's buff manager
            if (Buff != null)
                Target.Buffs.InEffect.Add(Buff);
            // If the consumbale has a permanent modifier, add this to the entity's AttributeModifiers
            if (Mod != null)
                Target.AttributeModifiers.Append(Mod);
            
        }
    }
}
