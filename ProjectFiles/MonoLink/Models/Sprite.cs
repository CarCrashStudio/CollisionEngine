using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLink.Entities;
using MonoLink.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoLink.Models
{
    public class Sprite : Component
    {
        protected int icon_size = 32;
        protected Texture2D _texture;

        protected float _layer { get; set; }
        protected Vector2 _origin { get; set; }
        protected Vector2 _position { get; set; }
        protected float _rotation { get; set; }
        
        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;

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

        public Rectangle Hitbox { get { return new Rectangle((int)Position.X, (int)Position.Y, this is HUDControl ? _texture.Width : icon_size, this is HUDControl ? _texture.Height : icon_size); } }
        public Texture2D Texture { get { return _texture; } set { if (this is HUDControl) _texture = value; } }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (this is Entity)
            {
                
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
