using Collision2D.RPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG.CombatHandlers
{
    /// <summary>
    /// A final fantasy style turn based combat
    /// </summary>
    public class TurnBased : Combat
    {
        public Adventurer Player
        {
            get
            {
                return player;
            }
            set
            {
                if (value != null)
                    player = value;
            }
        }
        public IEnumerable<PartyMember> Party
        {
            get
            {
                return playerParty;
            }
        }
        public IEnumerable<Monster> Monsters
        {
            get
            {
                return monsters;
            }
        }
        public TurnBased(Adventurer player, IEnumerable<PartyMember> playerParty, IEnumerable<Monster> monsters) :
            base(player, playerParty, monsters)
        {

        }
    }
}
