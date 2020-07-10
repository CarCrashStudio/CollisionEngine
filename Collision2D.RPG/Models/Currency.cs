using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG.Models
{
    public class Currency
    {
        const ushort SILVER_GOLD_CONVERSION = 10;
        const ushort SILVER_COPPER_CONVERSION = 10;
        const ushort COPPER_GOLD_CONVERSION = 10;

        private uint _gold;
        private uint _silver;
        private uint _copper;

        /// <summary>
        /// The total number of gold pieces.
        /// </summary>
        public uint Gold 
        { 
            get => _gold; 
            set => _gold = value; 
        }
        /// <summary>
        /// The total number of silver pieces.
        /// </summary>
        public uint Silver 
        { 
            get => _silver; 
            set => _silver = value; 
        }
        /// <summary>
        /// The total number of copper pieces.
        /// </summary>
        public uint Copper 
        { 
            get => _copper; 
            set => _copper = value; 
        }

        /// <summary>
        /// Instantiates a new currency object containing the given amount of gold, silver and copper
        /// </summary>
        /// <param name="gold">The non-negative amount of gold in this object</param>
        /// <param name="silver">The non-negative amount of silver in this object</param>
        /// <param name="copper">The non-negative amount of copper in this object</param>
        public Currency(uint gold = 0, uint silver = 0, uint copper = 0)
        {
            Gold = gold;
            Silver = silver;
            Copper = copper;
        }

        public uint Total()
        {
            return _gold + _silver + _copper;
        }

        private uint convert_gold_to_silver(uint gold)
        {
            return gold / SILVER_GOLD_CONVERSION;
        }
        private uint convert_gold_to_copper(uint gold)
        {
            return gold / COPPER_GOLD_CONVERSION;
        }
        private uint convert_silver_to_gold(uint silver)
        {
            return silver * SILVER_GOLD_CONVERSION;
        }
        private uint convert_silver_to_copper(uint silver)
        {
            return silver / SILVER_COPPER_CONVERSION;
        }
        private uint convert_copper_to_silver(uint copper)
        {
            return copper * SILVER_COPPER_CONVERSION;
        }
        private uint convert_copper_to_gold(uint copper)
        {
            return copper * COPPER_GOLD_CONVERSION;
        }

        /// <summary>
        /// Adds the Gold, Silver, and Copper, respectively, of two Currency Objects
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Currency operator +(Currency left, Currency right)
        {
            return new Currency(left.Gold + right.Gold,
                left.Silver + right.Silver,
                left.Copper + right.Copper);
        }
        public static Currency operator -(Currency left, Currency right)
        {
            var newGold = (left.Gold - right.Gold) < 0 ? 0 : left.Gold - right.Gold;
            var newSilver = (left.Silver - right.Silver) < 0 ? 0 : left.Silver - right.Silver;
            var newCopper = (left.Copper - right.Copper) < 0 ? 0 : left.Copper - right.Copper;

            return new Currency(newGold, newSilver, newCopper);
        }
        public static bool operator >(Currency left, Currency right)
        {
            return left.Total() > right.Total();
        }
        public static bool operator >(Currency left, uint right)
        {
            return left.Total() > right;
        }
        public static bool operator <(Currency left, Currency right)
        {
            return left.Total() < right.Total();
        }
        public static bool operator <(Currency left, uint right)
        {
            return left.Total() < right;
        }
    }
}
