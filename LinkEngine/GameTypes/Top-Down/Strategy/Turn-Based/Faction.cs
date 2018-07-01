using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.GameTypes.Top_Down.Strategy.Turn_Based
{
    public class Faction
    {
        short MAX_TERRITORIES = 15;

        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Leader Leader { get; set; }

        /// <summary>
        /// The territory capacity is the cap on how many territories a player can create. 
        /// This number does not cap how many can be owned by the player
        /// </summary>
        public short TerritoryCapacity { get { return MAX_TERRITORIES; } set { MAX_TERRITORIES = value; } }
        /// <summary>
        /// This array will hold the names of all territories that can be built by this faction
        /// </summary>
        public string[] TerritoryNames { get; set; }

        /// <summary>
        /// Creates a new faction based on the parameters given
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <param name="leader"></param>
        /// <param name="territory_cap">Used to set a certain number of territories a player can establish. Default is 15 but can be changed</param>
        public Faction (string name, string desc, Leader leader, short territory_cap = 15)
        {
            Name = name;
            Description = desc;
            Leader = leader;
            TerritoryCapacity = territory_cap;
        }
    }
}
