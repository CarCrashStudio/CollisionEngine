using Microsoft.Xna.Framework.Graphics;
using Collision2D.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.World
{
    public class Tile
    {
        public Sprite Sprite { get; set; }
        public bool Passable { get; set; }
        public Tile(Texture2D _texture) 
        {

        }
    }
}
