using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils
{
    public class HUDControl : Component
    {
        #region Fields
        private Texture2D _BackTexture;
        private Texture2D _texture;
        private SpriteFont _font;
        private MouseState _currentState;
        private MouseState _previousState;
        private Rectangle _rectangle;

        public override event EventHandler Click;
        public override event EventHandler MouseDown;
        public override event EventHandler MouseUp;
        public override event EventHandler MouseHover;
        public override event EventHandler MouseEnter;
        public override event EventHandler MouseLeave;
        #endregion

        #region Properties
        public string Name { get; set; }
        public string Parent { get; set;  }
        public bool Clicked { get; set; }
        public bool Enabled { get; set; }
        public bool Hovering { get; set; }
        public bool Visible { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D BackgroundImage { get { return _BackTexture; } set { _BackTexture = value; } }
        public Texture2D ForegroundImage { get { return _texture; } set { _texture = value; } }
        public Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            set
            {
                _rectangle = value;
            }
        }
        public string Text { get; set; }
        #endregion

        public HUDControl()
        {
            _currentState = Mouse.GetState();
            ForeColor = Color.Black;
            BackColor = Color.White;

            Visible = true;
            Enabled = true;
        }
        public HUDControl (Texture2D _texture, SpriteFont _font, Vector2 _position)
        {
            this._BackTexture = _texture;
            this._font = _font;

            Position = _position;
            if (Rectangle == Rectangle.Empty)
            {
                if (_BackTexture != null)
                    Rectangle = new Rectangle((int)Position.X, (int)Position.Y, BackgroundImage.Width, BackgroundImage.Height);
                else if (!string.IsNullOrEmpty(Text))
                    Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)_font.MeasureString(Text).X, (int)_font.MeasureString(Text).Y);
            }

            _currentState = Mouse.GetState();
            ForeColor = Color.Black;
            BackColor = Color.White;

            Visible = true;
            Enabled = true;
        }
        public HUDControl(Texture2D _texture, SpriteFont _font, Vector2 _position, string text)
        {
            this._BackTexture = _texture;
            this._font = _font;
            Text = text;
            Position = _position;
            if (Rectangle == Rectangle.Empty)
            {
                if (_BackTexture != null)
                    Rectangle = new Rectangle((int)Position.X, (int)Position.Y, BackgroundImage.Width, BackgroundImage.Height);
                else if (!string.IsNullOrEmpty(Text))
                    Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)_font.MeasureString(Text).X, (int)_font.MeasureString(Text).Y);
            }

            _currentState = Mouse.GetState();
            ForeColor = Color.Black;
            BackColor = Color.White;

            Visible = true;
            Enabled = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (_BackTexture != null)
                    spriteBatch.Draw(_BackTexture, Rectangle, BackColor);
                if(_texture != null)
                    spriteBatch.Draw(_texture, new Vector2((Rectangle.Width / 2) - (_texture.Width / 2), (Rectangle.Height / 2) - (_texture.Height / 2)), BackColor);
                if (!string.IsNullOrEmpty(Text))
                {
                    var x = Rectangle.Center.X - (_font.MeasureString(Text).X / 2);
                    var y = Rectangle.Center.Y - (_font.MeasureString(Text).Y / 2);

                    spriteBatch.DrawString(_font, Text, new Vector2(x, y), ForeColor);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // reset the Clicked State of the control
            Clicked = false;

            // set the previous state to the mouse state of the previous frame
            _previousState = _currentState;

            // set the currentState to the mouse State of the current frame
            _currentState = Mouse.GetState();

            var currentMouseRectangle = new Rectangle(_currentState.X, _currentState.Y, 1, 1);
            var previousMouseRectangle = new Rectangle(_previousState.X, _previousState.Y, 1, 1);

            // only check for events if the control is visible
            if (Visible)
            {
                // check if the mouse is over the control
                if (currentMouseRectangle.Intersects(Rectangle))
                {
                    // check if the mouse was clicked
                    if (_currentState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed)
                    {
                        // the mouse was clicked
                        // lets invoke the click method
                        if (Enabled)
                        {
                            Click?.Invoke(this, new EventArgs());
                            Clicked = true;
                        }
                    }
                    else
                    {
                        // check if the mouse was over the control previously
                        if (previousMouseRectangle.Intersects(Rectangle))
                        {
                            // the mouse is hovering over the control
                            // so lets invoke the hover event
                            if (Enabled)
                                MouseHover?.Invoke(this, new EventArgs());
                            else Hovering = false;
                        }
                        else
                        {
                            // the mouse has entered the control
                            // so lets invoke the MouseEnter event
                            if (Enabled)
                                MouseEnter?.Invoke(this, new EventArgs());
                            else Hovering = false;
                        }
                    }
                }
                // check if the mouse left the control
                else if (!currentMouseRectangle.Intersects(Rectangle) && previousMouseRectangle.Intersects(Rectangle))
                {
                    // Mouse has left the control bounds
                    // Lets invoke the control leave event
                    if (Enabled)
                        MouseLeave?.Invoke(this, new EventArgs());
                    else Hovering = false;
                }
            }
        }
    }
}
