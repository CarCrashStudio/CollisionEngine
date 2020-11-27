using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.HUDControls
{
    public class Button : Utils.HUDControl
    {
        public Button(Texture2D texture, SpriteFont spriteFont, Vector2 pos, string text)
            : base(texture, spriteFont, pos, text)
        {
            this.MouseEnter += delegate
            {
                this.BackColor = Color.Gray;
            };
            this.MouseLeave += delegate
            {
                this.BackColor = Color.White;
            };
            this.MouseHover += delegate
            {
                this.BackColor = Color.Gray;
            };
        }
    }
}
