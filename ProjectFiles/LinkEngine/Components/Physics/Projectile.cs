using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine
{
    /// <summary>
    /// The Projectile class holds all information for any projectile object. The projectile class contains a Raycast object that will store the data of the object hit.
    /// </summary>
    public class Projectile
    {
        public int Speed { get; set; }
        public int Size { get; set; }

        public Raycast Raycast { get; set; }

        /// <summary>
        /// Initializes a new Projectile object
        /// </summary>
        public Projectile ()
        {

        }

        public object Fire (Vector ray, Collider2D[] allColliders)
        {
            Raycast = new Raycast(ray, allColliders);
            return Raycast.Hit;
        }
    }
}
