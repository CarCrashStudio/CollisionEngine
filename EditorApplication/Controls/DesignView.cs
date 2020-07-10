using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

namespace EditorApplication
{
    class DesignView : MonoGame.Forms.Controls.MonoGameControl
    {
        public event EventHandler onUpdate;

        MouseState mouse;
        public uint CellWidth { get; set; }
        public uint CellHeight { get; set; }
        public Vector2 CurrentCell { get; set; }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            onUpdate?.Invoke(this, new EventArgs());

            base.Update(gameTime);
        }
        protected override void Draw()
        {
            // set the background color of the editor to be the chosen back colour of the control
            GraphicsDevice.Clear(new Color(BackColor.R, BackColor.G, BackColor.B, BackColor.A));
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            CurrentCell = new Vector2((int)(mouse.X / CellWidth), (int)(mouse.Y / CellHeight));
            base.OnMouseEnter(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            CurrentCell = new Vector2((int)(mouse.X / CellWidth), (int)(mouse.Y / CellHeight));
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            CurrentCell = Vector2.Zero;
            base.OnMouseLeave(e);
        }
    }
}
