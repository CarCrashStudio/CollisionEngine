using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.GameTypes.Top_Down.Strategy.Turn_Based
{
    public class Player
    {
        /// <summary>
        /// The Faction is who the player will play as ingame
        /// </summary>
        public Faction Faction { get; set; }

        /// <summary>
        /// The camera controlling how the player see the world
        /// </summary>
        public Entities.Camera camera { get; set; }
    }
}
