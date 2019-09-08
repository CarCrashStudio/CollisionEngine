using Microsoft.Xna.Framework.Graphics;
using MonoLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink.World
{
    public class Tile : Sprite
    {
        public bool Passable { get; set; }
        public Tile(Texture2D _texture) 
            : base(_texture)
        {

        }
    }
}
