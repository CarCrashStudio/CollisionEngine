using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class CircleCollider2D : Collider2D
    {
        public int Radius { get; set; }

        /// <summary>
        /// This function checks if the collider is colliding with another collider relative to the center of the circle
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public bool IsColliding (Collider2D collider)
        {
            if (collider.Transform.Position.X > this.Transform.Position.X - Radius)
            {
                if (collider.Transform.Position.X < this.Transform.Position.X + Radius)
                {
                    if (collider.Transform.Position.Y > this.Transform.Position.Y - Radius)
                    {
                        if (collider.Transform.Position.Y < this.Transform.Position.Y + Radius)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
