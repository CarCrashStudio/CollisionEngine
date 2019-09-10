using LinkEngine.RPG2D.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLink2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkEngine.RPG2D.Entities
{
    public class Monster : Enemy
    {
        public short SpawnChance { get; set; }
        public new Attributes BaseAttributes { get; set; }
        public new IEnumerable<Attributes> AttributeModifiers { get; set; }
        public new Attributes TotalAttributes
        {
            get
            {
                return BaseAttributes + AttributeModifiers.Sum();
            }
        }

        public Monster(int id, string name, short spawn, Texture2D texture, Vector2 pos) :
            base(texture, pos)
        {
            SpawnChance = spawn;
        }

        public override void Update (GameTime gameTime)
        {
            bool coll = false;

            Follow(gameTime, map, entities, ref coll);
            if (coll && (GetType() != Target.GetType()))
                this.Attack(Target);

            base.Update(gameTime);
        }
    }
}
