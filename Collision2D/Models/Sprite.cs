using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Collision2D.Utils.Entities;
using Collision2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils
{
    public class Sprite : Component
    {
        protected int icon_size = 32;

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Texture2D _texture;

        protected float _layer { get; set; }
        protected Vector2 _origin { get; set; }
        protected Vector2 _position { get; set; }
        protected float _rotation { get; set; }

        public Color ForeColor { get; set; }

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }
        public int IconSize { get { return icon_size; } set { icon_size = value; } }

        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
            }
        }
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
            }
        }
        public Vector2 Velocity;

        public override event EventHandler Click;
        public override event EventHandler MouseDown;
        public override event EventHandler MouseUp;
        public override event EventHandler MouseHover;
        public override event EventHandler MouseEnter;
        public override event EventHandler MouseLeave;

        public Rectangle Hitbox { get { return new Rectangle((int)Position.X, (int)Position.Y, IconSize, IconSize); } }
        public Texture2D Texture { get { return _texture; } set { _texture = value; } }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);

        }
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Hitbox, Color.White);
        }
    }
}
