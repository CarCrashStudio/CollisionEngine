using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLink.Models;
using MonoLink.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink.Entities
{
    public class Entity : Sprite
    {
        bool has_attacked = false;
        /// <summary>
        /// These are the types of attributes to only change on level-up
        /// </summary>
        public Attributes BaseAttributes { get; set; }

        /// <summary>
        /// These are extra attributes that can be gained from different sources (equipment, power-ups, spells etc)
        /// </summary>
        public List<Attributes> AttributeModifiers { get; set; }

        public Attributes TotalAttributes
        {
            get
            {
                var atr = BaseAttributes + AttributeModifiers.Sum();
                if (atr.CurrentAttackSpeed < 0)
                    atr.CurrentAttackSpeed = 0;
                if (atr.CurrentHP < 0)
                    atr.CurrentHP = 0;
                return atr;
            }
        }
        public bool HasAttacked { get { return has_attacked; } set { has_attacked = value; } }
        public bool IsDead { get { return (TotalAttributes.CurrentHP <= 0);  } }

        public Entity(Texture2D texture)
          : base(texture)
        {
        }

        protected void check_world_collision(Tile tile)
        {
            if (!tile.Passable)
            {
                if ((Velocity.X > 0 && IsTouchingLeft(tile)) ||
                (Velocity.X < 0 && IsTouchingRight(tile)))
                    Velocity.X = 0;

                if ((Velocity.Y > 0 && IsTouchingTop(tile)) ||
                    (Velocity.Y < 0 && IsTouchingBottom(tile)))
                    Velocity.Y = 0;
            }

        }
        protected void check_entity_collision(Entity entity)
        {
            bool coll = false;
            if ((Velocity.X > 0 && IsTouchingLeft(entity)) ||
                (Velocity.X < 0 && IsTouchingRight(entity)))
            {
                Velocity.X = 0;
                coll = true;
            }

            if ((Velocity.Y > 0 && IsTouchingTop(entity)) ||
                (Velocity.Y < 0 && IsTouchingBottom(entity)))
            {
                Velocity.Y = 0;
                coll = true;
            }

            if (coll && (GetType() != entity.GetType()))
                Attack(ref entity);
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Hitbox.Right + this.Velocity.X > sprite.Hitbox.Left &&
              this.Hitbox.Left < sprite.Hitbox.Left &&
              this.Hitbox.Bottom > sprite.Hitbox.Top &&
              this.Hitbox.Top < sprite.Hitbox.Bottom;
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Hitbox.Left + this.Velocity.X < sprite.Hitbox.Right &&
              this.Hitbox.Right > sprite.Hitbox.Right &&
              this.Hitbox.Bottom > sprite.Hitbox.Top &&
              this.Hitbox.Top < sprite.Hitbox.Bottom;
        }
        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Hitbox.Bottom + this.Velocity.Y > sprite.Hitbox.Top &&
              this.Hitbox.Top < sprite.Hitbox.Top &&
              this.Hitbox.Right > sprite.Hitbox.Left &&
              this.Hitbox.Left < sprite.Hitbox.Right;
        }
        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Hitbox.Top + this.Velocity.Y < sprite.Hitbox.Bottom &&
              this.Hitbox.Bottom > sprite.Hitbox.Bottom &&
              this.Hitbox.Right > sprite.Hitbox.Left &&
              this.Hitbox.Left < sprite.Hitbox.Right;
        }

        public void Attack(ref Entity entity)
        {
            if (!has_attacked)
            {
                int min = ((int)TotalAttributes.Strength) - (int)((5 * Math.Round(TotalAttributes.Strength / 100, 2, MidpointRounding.AwayFromZero)) * 100);
                int max = ((int)TotalAttributes.Strength) + (int)((2 * Math.Round(TotalAttributes.Strength / 100, 2, MidpointRounding.AwayFromZero)) * 100);
                if (min < 0)
                    min = 0;
                int damage = Helpers.Random.Next(min, max);
                if (damage > 0)
                {
                    // add an attribute modifier for the damage taken
                    // first we want to see if there is already a modifier named 'dmg'
                    var dmg = (from a in entity.AttributeModifiers where a.Name == "dmg" select a).FirstOrDefault();
                    if (dmg != null)
                    {
                        // we need to remove this damage modifier from the list
                        entity.AttributeModifiers.Remove(dmg);
                        // increase the damage of the copy that we created by the value we just calculated
                        dmg.CurrentHP -= damage;
                        // add the copy with the new damage back into the attributes
                        entity.AttributeModifiers.Add(dmg);
                    }
                    else entity.AttributeModifiers.Add(new Attributes() { CurrentHP = -damage, Name = "dmg" });

                    // remove the atkspd countdown modifier
                    has_attacked = true;
                }
            }
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

            Position += Velocity;
            Velocity = Vector2.Zero;
        }
    }
}
