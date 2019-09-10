using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink2D.Models;
using MonoLink2D.World;

namespace MonoLink2D.Entities
{
    public class Enemy : Entity
    {
        protected IEnumerable<Tile> map;
        protected IEnumerable<Entity> entities;

        public IEnumerable<Tile> Map { get { return map; } set { map = value; } }
        public IEnumerable<Entity> Entities { get { return entities; } set { entities = value; } }

        public Entity Target { get; set; }
        public float FollowDistance { get; set; }
        public bool IsRemoved { get; set; }

        public Enemy(Texture2D texture, Vector2 pos)
        {
            Sprite = new Sprite(texture)
            {
                Position = pos,
            };
        }
        public void Follow(GameTime gameTime, IEnumerable<Tile> world, IEnumerable<Entity> entities)
        {
            if (Target == null)
                return;

            var distance = Target.Sprite.Position - Sprite.Position;
            Sprite.Rotation = (float)Math.Atan2(distance.Y, distance.X);

            Sprite.Direction = new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation));

            var currentDistance = Vector2.Distance(Sprite.Position, Target.Sprite.Position);
            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min(Math.Abs(currentDistance - FollowDistance), Sprite.LinearVelocity);
                Sprite.Velocity = (Sprite.Direction * t) * (float)gameTime.ElapsedGameTime.TotalSeconds;

                Move(world, entities);
            }
        }
        public void Follow(GameTime gameTime, IEnumerable<Tile> world, IEnumerable<Entity> entities, ref bool isColliding)
        {
            if (Target == null)
                return;

            var distance = Target.Sprite.Position - Sprite.Position;
            Sprite.Rotation = (float)Math.Atan2(distance.Y, distance.X);

            Sprite.Direction = new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation));

            var currentDistance = Vector2.Distance(Sprite.Position, Target.Sprite.Position);
            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min(Math.Abs(currentDistance - FollowDistance), Sprite.LinearVelocity);
                Sprite.Velocity = (Sprite.Direction * t) * (float)gameTime.ElapsedGameTime.TotalSeconds;

                Move(world, entities, ref isColliding);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Follow(gameTime, map, entities);
            base.Update(gameTime);
        }
    }
}
