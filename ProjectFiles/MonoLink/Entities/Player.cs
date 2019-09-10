using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink2D.Models;
using MonoLink2D.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink2D.Entities
{
    public class Player : Entity
    {
        /// <summary>
        /// All inputs the player can use.
        /// </summary>
        public virtual Input Input { get; set; }

        public Player(Texture2D texture, Vector2 pos)
        {
            Sprite = new Sprite(texture)
            {
                Position = pos,
            };
        }
    }
}
