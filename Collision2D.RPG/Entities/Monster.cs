using Collision2D.RPG.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Collision2D.RPG.Entities
{
    public class Monster : Entity
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

        public List<LootItem> Loot { get; set; }

        public Monster(int id, string name, short spawn, Texture2D texture, Vector2 pos) :
            base(texture, pos)
        {
            Loot = new List<LootItem>();
            SpawnChance = spawn;
        }
    }
}
