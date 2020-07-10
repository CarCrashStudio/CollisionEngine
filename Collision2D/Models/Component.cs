using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils
{
    public abstract class Component
    {
        #region Events
        public abstract event EventHandler Click;

        public abstract event EventHandler MouseDown;
        public abstract event EventHandler MouseUp;
        public abstract event EventHandler MouseHover;
        public abstract event EventHandler MouseEnter;
        public abstract event EventHandler MouseLeave;
        #endregion

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
