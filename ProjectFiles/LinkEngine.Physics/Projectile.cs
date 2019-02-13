using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    /// <summary>
    /// As a delegate of the Collider2D class Projectile2D can have forces exerted upon it
    /// </summary>
    public class Projectile2D : Collider2D
    {
        public int Speed { get; set; }
        public int Size { get; set; }

        /// <summary>
        /// Initializes a new Projectile object
        /// </summary>
        public Projectile2D (int x, int y, int z, int h, int w) :
            base (x, y, z, h, w)
        {

        }

        public bool Fire (Vector ray, Collider2D[] allColliders)
        {
            return Raycast(ray, allColliders);
        }
        bool Raycast(Vector ray, Collider2D[] allColliders)
        {
            Vector pos = Transform.Position;
            while (!isColliding(allColliders))
            {
                pos += ray;
            }

            return isColliding(allColliders);
        }
    }
}
