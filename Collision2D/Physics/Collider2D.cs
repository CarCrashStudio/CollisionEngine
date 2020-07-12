using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Physics
{
    /// <summary>
    /// Base class for all collider type objects
    /// </summary>
    public abstract class Collider2D
    {

        public Collider2D()
        {

        }

        public abstract bool isColliding(Collider2D other);
    }


    public class BoxCollider2D
    {
        private Vector2 position;
        private Vector2 size;

        public Rectangle rectangle { get { return new Rectangle(position.ToPoint(), size.ToPoint()); } }
        public bool isColliding(BoxCollider2D other)
        {
            return other.rectangle.Intersects(rectangle);
        } 
    }
}
