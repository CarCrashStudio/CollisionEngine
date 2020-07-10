using Collision2D.Utils;

namespace Collision2D.RPG.Models
{
    public enum Slots : int
    {
        Head=0,
        Torso=1,
        Legs=2,
        Boots=3,
        Main=4,
        Off=5
    }
    public class Equipment
    {
        public Item Details { get; set; }
        /// <summary>
        /// Mod will be used to boost a player's skill only while it is equipped
        /// </summary>
        public Attributes Mod { get; set; }

        /// <summary>
        /// The index of where this item should go in the player's Equipment list
        /// </summary>
        public Slots[] Slots { get; set; }

        /// <summary>
        /// Creates a new Equipment from the parameters given
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_namePlural"></param>
        /// <param name="_cost"></param>
        public Equipment(Item details, Slots[] slots, Attributes mod)
        {
            Details = details;
            Slots = slots;
            Mod = mod;
        }
        /// <summary>
        /// Creates a copy of a piece of Equipment
        /// </summary>
        /// <param name="equ">The Equipment to copy</param>
        public Equipment(Equipment equ)
        {
            Details = equ.Details;
            Slots = equ.Slots;
            Mod = equ.Mod;
        }
    }
}
