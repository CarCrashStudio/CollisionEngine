using LinkEngine.RPG2D.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLink2D.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.RPG2D.Entities
{
    public class Entity : MonoLink2D.Entities.Entity
    {
        protected float _layer { get; set; }
        protected Vector2 _origin { get; set; }
        protected Vector2 _position { get; set; }
        protected float _rotation { get; set; }
        protected Texture2D _texture;
        public Color Colour { get; set; }

        /// <summary>
        /// The sprite that we want to follow
        /// </summary>
        public Entity FollowTarget { get; set; }

        /// <summary>
        /// How close we want to be to our target
        /// </summary>
        public float FollowDistance { get; set; }
        public bool IsRemoved { get; set; }
        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;
        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
            }
        }
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
            }
        }

        public new Attributes BaseAttributes { get; set; }
        public new IEnumerable<Attributes> AttributeModifiers { get; set; }
        public new Attributes TotalAttributes
        {
            get
            {
                return BaseAttributes + AttributeModifiers.Sum();
            }
        }

        public new bool IsDead { get { return TotalAttributes.CurrentHP <= 0; } }

        protected bool IsNearLeft(Entity sprite)
        {
            return Sprite.Hitbox.Right + TotalAttributes.Reach > sprite.Sprite.Hitbox.Left &&
              Sprite.Hitbox.Left < sprite.Sprite.Hitbox.Left &&
              Sprite.Hitbox.Bottom > sprite.Sprite.Hitbox.Top &&
              Sprite.Hitbox.Top < sprite.Sprite.Hitbox.Bottom;
        }
        protected bool IsNearRight(Entity sprite)
        {
            return Sprite.Hitbox.Left + TotalAttributes.Reach < sprite.Sprite.Hitbox.Right &&
              Sprite.Hitbox.Right > sprite.Sprite.Hitbox.Right &&
              Sprite.Hitbox.Bottom > sprite.Sprite.Hitbox.Top &&
              Sprite.Hitbox.Top < sprite.Sprite.Hitbox.Bottom;
        }
        protected bool IsNearTop(Entity sprite)
        {
            return Sprite.Hitbox.Bottom + Sprite.Velocity.Y > sprite.Sprite.Hitbox.Top &&
              Sprite.Hitbox.Top < sprite.Sprite.Hitbox.Top &&
              Sprite.Hitbox.Right > sprite.Sprite.Hitbox.Left &&
              Sprite.Hitbox.Left < sprite.Sprite.Hitbox.Right;
        }
        protected bool IsNearBottom(Entity sprite)
        {
            return Sprite.Hitbox.Top + Sprite.Velocity.Y < sprite.Sprite.Hitbox.Bottom &&
              Sprite.Hitbox.Bottom > sprite.Sprite.Hitbox.Bottom &&
              Sprite.Hitbox.Right > sprite.Sprite.Hitbox.Left &&
              Sprite.Hitbox.Left < sprite.Sprite.Hitbox.Right;
        }

        public bool IsNear(Entity entity)
        {
            if (Sprite.Direction.X > 0)
                return IsNearLeft(entity);
            else if (Sprite.Direction.X < 0)
                return IsNearRight(entity);
            else if (Sprite.Direction.Y > 0)
                return IsNearBottom(entity);
            else if (Sprite.Direction.Y < 0)
                return IsNearTop(entity);
            else return false;
        }

        protected void Follow()
        {
            if (FollowTarget == null)
                return;

            var distance = FollowTarget.Position - this.Position;
            _rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);
            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min(Math.Abs(currentDistance - FollowDistance), LinearVelocity);
                var velocity = Direction * t;

                Position += velocity;
            }
        }
        protected void Follow(IEnumerable<Tile> tiles, IEnumerable<Entity> entities)
        {
            if (FollowTarget == null)
                return;

            var distance = FollowTarget.Position - this.Position;
            _rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);
            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min(Math.Abs(currentDistance - FollowDistance), LinearVelocity);
                var velocity = Direction * t;

                Move(tiles, entities);
            }
        }

        protected Entity(Texture2D texture, Vector2 pos)
        {
            Sprite = new MonoLink2D.Models.Sprite(texture)
            {
                Position = pos
            };
        }
    }
}
