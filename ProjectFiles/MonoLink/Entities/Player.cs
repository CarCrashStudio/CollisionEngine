using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLink.Models;
using MonoLink.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink.Entities
{
    public class Player : Entity
    {
        /// <summary>
        /// All inputs the play can use.
        /// </summary>
        public Input Input { get; set; }

        public Player(Texture2D texture)
          : base(texture)
        {
        }
    }
}
