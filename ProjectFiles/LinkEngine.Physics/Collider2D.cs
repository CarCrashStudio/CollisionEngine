using System.Threading;

namespace LinkEngine.Components
{
    public class Collider2D
    {
        public Vector Velocity { get; set; }

        /// <summary>
        /// Parent is the object that owns this collider component
        /// </summary>
        public object Parent { get; set; }

        /// <summary>
        /// The transform of an object is the part of the object that moves around screen
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// If the collision object has gravity, its Y coordinates should be reduced
        /// </summary>
        public bool HasGravity { get; set; }

        /// <summary>
        /// If the collision object is slippery, push forces will cause colliders to slide more
        /// </summary>
        public bool IsSlippery { get; set; }

        /// <summary>
        /// If the collision object is touching the ground
        /// </summary>
        public bool IsGrounded { get; set; }

        public Collider2D(int x, int y, int z, int h, int w)
        {
            Transform = new Transform(x, y, z, h, w);
        }

        /// <summary>
        /// This function will take the current collider's position and size and check to see if it is colliding with another collider object.
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public bool isColliding(Collider2D collider)
        {
            return ((collider.Transform.Position.X > Transform.Position.X - (Transform.Size.Width / 2) && collider.Transform.Position.X + Transform.Size.Width < Transform.Position.X + (Transform.Size.Width / 2)) && (collider.Transform.Position.Y > Transform.Position.Y - (Transform.Size.Height / 2) && collider.Transform.Position.Y + Transform.Size.Height > Transform.Position.Y + (Transform.Size.Height / 2)));
        }
    }
}
