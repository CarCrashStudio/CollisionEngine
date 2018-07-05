using LinkEngine.Components;

namespace LinkEngine.Platformer
{
    public class Platform
    {
        /// <summary>
        /// The collider incharge of physics calculations
        /// </summary>
        public Collider2D Collider;

        /// <summary>
        /// Initializes a new Platform at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Platform(int x, int y, int width, int height, bool hasGrav)
        {
            Collider = new Collider2D();
            Collider.Width = width;
            Collider.Height = height;
            Collider.Transform.Position = new Vector(x, y, 0);
            Collider.HasGravity = hasGrav;
        }
    }
}
