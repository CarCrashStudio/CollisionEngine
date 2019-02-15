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

        /// <summary>
        /// Fire() will cast a ray until the projectile hits a collider2D object. 
        /// </summary>
        /// <param name="ray">a vector telling the raycast which direction to go. Ex (1, -1, 0) will move the ray right and up</param>
        /// <param name="allColliders"></param>
        /// <returns></returns>
        public Collider2D Fire (Vector ray, Collider2D[] allColliders)
        {
            return Raycast(ray, allColliders);
        }
        Collider2D Raycast(Vector ray, Collider2D[] allColliders)
        {
            Vector pos = Transform.Position;
            while (!isColliding(allColliders))
            {
                pos += ray;
                // pull the projectile down towards the ground
                Pull(2, 0, -1);
            }

            return GetColliding(allColliders);
        }
    }
}
