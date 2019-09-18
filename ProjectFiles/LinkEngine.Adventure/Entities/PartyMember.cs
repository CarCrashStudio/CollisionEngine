using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLink2D.World;
using System.Collections.Generic;

namespace LinkEngine.RPG2D.Entities
{
    public class PartyMember : Entity
    {
        public PartyMember(Texture2D texture, Vector2 pos, Entity target, float follow_distance) :
            base(texture,pos)
        {
            FollowTarget = target;
            FollowDistance = follow_distance;
        }
        public override void Update(GameTime gameTime)
        {
            Follow();
            base.Update(gameTime);
        }
    }
}
