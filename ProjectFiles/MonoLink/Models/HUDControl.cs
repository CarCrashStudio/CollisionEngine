using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink2D.Models
{
    public class HUDControl : Sprite
    {
        public string identifier = "";
        public HUDControl(string id, Texture2D texture, Vector2 position)
            : base(texture)
        {
            identifier = id;
            Position = position;
        }
    }
}
