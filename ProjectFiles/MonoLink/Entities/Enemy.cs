using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink.Models;
using MonoLink.World;

namespace MonoLink.Entities
{
    public class Enemy : Entity
    {
        

        public Sprite Target { get; set; }
        public float FollowDistance { get; set; }
        public bool IsRemoved { get; set; }

        public Enemy(Texture2D texture)
          : base(texture)
        {
        }

        

        public void Follow(GameTime gameTime, IEnumerable<Tile> world, IEnumerable<Entity> entities)
        {
            if (Target == null)
                return;

            var distance = Target.Position - Position;
            _rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            var currentDistance = Vector2.Distance(Position, Target.Position);
            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min(Math.Abs(currentDistance - FollowDistance), LinearVelocity);
                Velocity = (Direction * t) * (float)gameTime.ElapsedGameTime.TotalSeconds;

                Move(world, entities);
            }
        }
    }
}
