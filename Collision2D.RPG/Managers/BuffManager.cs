using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Collision2D.RPG.Managers
{
    public class BuffManager : Utils.Manager
    {
        public List<Models.Buff> InEffect { get; set; }

        public BuffManager ()
        {
            InEffect = new List<Models.Buff>();
        }
        public override void Update(GameTime gameTime)
        {
            foreach(var buff in InEffect)
            {
                buff.TimeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (buff.TimeRemaining <= 0)
                    InEffect.Remove(buff);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
