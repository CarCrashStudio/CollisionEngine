using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Collision2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils.Entities
{
    public partial class Entity
    {
        public Sprite Sprite { get; protected set; }
        /// <summary>
        /// These are the types of attributes to only change on level-up
        /// </summary>
        public Attributes BaseAttributes { get; set; }

        /// <summary>
        /// These are extra attributes that can be gained from different sources (equipment, power-ups, spells etc)
        /// </summary>
        public List<Attributes> AttributeModifiers { get; set; }

        public virtual Attributes TotalAttributes
        {
            get
            {
                var atr = BaseAttributes + AttributeModifiers.Sum();
                if (atr.CurrentHP < 0)
                    atr.CurrentHP = 0;
                return atr;
            }
        }
        public bool IsDead { get { return (TotalAttributes.CurrentHP <= 0);  } }

        public Entity()
        {
            
        }

        protected void check_world_collision(Tile tile)
        {
            if (!tile.Passable)
            {
                if ((Sprite.Velocity.X > 0 && IsTouchingLeft(tile.Sprite)) ||
                (Sprite.Velocity.X < 0 && IsTouchingRight(tile.Sprite)))
                    Sprite.Velocity.X = 0;

                if ((Sprite.Velocity.Y > 0 && IsTouchingTop(tile.Sprite)) ||
                    (Sprite.Velocity.Y < 0 && IsTouchingBottom(tile.Sprite)))
                    Sprite.Velocity.Y = 0;
            }

        }
        protected void check_entity_collision(Entity entity)
        {
            if ((Sprite.Velocity.X > 0 && IsTouchingLeft(entity.Sprite)) ||
                (Sprite.Velocity.X < 0 && IsTouchingRight(entity.Sprite)))
            {
                Sprite.Velocity.X = 0;
            }

            if ((Sprite.Velocity.Y > 0 && IsTouchingTop(entity.Sprite)) ||
                (Sprite.Velocity.Y < 0 && IsTouchingBottom(entity.Sprite)))
            {
                Sprite.Velocity.Y = 0;
            }
        }
        protected void check_entity_collision(Entity entity, ref bool isColliding)
        {
            if ((Sprite.Velocity.X > 0 && IsTouchingLeft(entity.Sprite)) ||
                (Sprite.Velocity.X < 0 && IsTouchingRight(entity.Sprite)))
            {
                Sprite.Velocity.X = 0;
                isColliding = true;
            }

            if ((Sprite.Velocity.Y > 0 && IsTouchingTop(entity.Sprite)) ||
                (Sprite.Velocity.Y < 0 && IsTouchingBottom(entity.Sprite)))
            {
                Sprite.Velocity.Y = 0;
                isColliding = true;
            }
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Sprite.Hitbox.Right + Sprite.Velocity.X > sprite.Hitbox.Left &&
              this.Sprite.Hitbox.Left < sprite.Hitbox.Left &&
              this.Sprite.Hitbox.Bottom > sprite.Hitbox.Top &&
              this.Sprite.Hitbox.Top < sprite.Hitbox.Bottom;
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Sprite.Hitbox.Left + this.Sprite.Velocity.X < sprite.Hitbox.Right &&
              this.Sprite.Hitbox.Right > sprite.Hitbox.Right &&
              this.Sprite.Hitbox.Bottom > sprite.Hitbox.Top &&
              this.Sprite.Hitbox.Top < sprite.Hitbox.Bottom;
        }
        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Sprite.Hitbox.Bottom + this.Sprite.Velocity.Y > sprite.Hitbox.Top &&
              this.Sprite.Hitbox.Top < sprite.Hitbox.Top &&
              this.Sprite.Hitbox.Right > sprite.Hitbox.Left &&
              this.Sprite.Hitbox.Left < sprite.Hitbox.Right;
        }
        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Sprite.Hitbox.Top + this.Sprite.Velocity.Y < sprite.Hitbox.Bottom &&
              this.Sprite.Hitbox.Bottom > sprite.Hitbox.Bottom &&
              this.Sprite.Hitbox.Right > sprite.Hitbox.Left &&
              this.Sprite.Hitbox.Left < sprite.Hitbox.Right;
        }
        
        /// <summary>
        /// Changes the position of the player
        /// </summary>
        /// <param name="gameTime">The gameTime object of the game</param>
        /// <param name="world">an enumerable list of tiles in the map</param>
        /// <param name="keys">all keys currently being pressed down</param>
        public void Move(IEnumerable<Tile> world, IEnumerable<Entity> entities)
        {
            foreach (var tile in world)
                check_world_collision(tile);

            foreach (var entity in entities)
                if (entity != this)
                    check_entity_collision(entity);

            Sprite.Position += Sprite.Velocity;
            Sprite.Velocity = Vector2.Zero;
        }
        public void Move(IEnumerable<Tile> world, IEnumerable<Entity> entities, ref bool isColliding)
        {
            foreach (var tile in world)
                check_world_collision(tile);

            foreach (var entity in entities)
                if (entity != this)
                    check_entity_collision(entity, ref isColliding);

            Sprite.Position += Sprite.Velocity;
            Sprite.Velocity = Vector2.Zero;
        }
        public void Move()
        {
            Sprite.Position += Sprite.Velocity;
            Sprite.Velocity = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
