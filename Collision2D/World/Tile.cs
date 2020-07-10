using Microsoft.Xna.Framework.Graphics;
using Collision2D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Collision2D.Utils
{
    public class Tile
    {
        public int ID { get; private set; }

        public Sprite Sprite { get; set; }

        public bool Passable { get; set; }
        public TileType Type { get; set; }
        public Directions Facing { get; set; }
        public bool Seen { get; set; }
        public Tile(int id, Texture2D _texture) 
        {
            ID = id;

            Sprite = new Sprite(_texture);
            Sprite.IconSize = _texture.Width;
        }

        public Tile (Tile tile)
        {
            if (tile != null)
            {
                ID = tile.ID;
                Sprite = tile.Sprite;

                Passable = tile.Passable;
                Type = tile.Type;
                Facing = tile.Facing;
            }
        }
    }
}
