using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Collision2D.Utils;
using System.Collections.Generic;

namespace Collision2D.RPG.Entities
{
    /// <summary>
    /// PartyMember is a class derived from Entity. This class is used for members of the Player's party and can be set to follow the player in the world or only be available in combat.
    /// </summary>
    public class PartyMember : Entity
    {
        /// <summary>
        /// Dictates whether the party member should follow the player around the world.
        /// </summary>
        public bool CanFollow { get; set; }
        public PartyMember(Texture2D texture, Vector2 pos) :
            base(texture,pos)
        {
            
        }
    }
}
