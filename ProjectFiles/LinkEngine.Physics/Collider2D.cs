using System.Threading;

namespace LinkEngine.Components
{
    /* Notes on friction
     * Two Slippery objects will have a lower coefficient of friction while two non-slippery objects
     * will have a much higher coefficient of friction.
     * 
     * the function of force is f*m*n or Friction * Mass * force
     */

    /// <summary>
    /// Collider2D handles all two-dimensional collisions between other Collider2D objects.
    /// </summary>
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

        /// <summary>
        /// Mass is used to calculate the amount of friction to apply on an object
        /// </summary>
        public int Mass { get; set; }

        public Collider2D(int x, int y, int z, int h, int w)
        {
            Transform = new Transform(x, y, z, h, w);
        }

        /// <summary>
        /// This function will take the current collider's position and size and check to see if it is colliding with another collider object.
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public bool isColliding(Collider2D[] allColliders)
        {
            foreach (Collider2D collider in allColliders)
            {
                if ((collider.Transform.Position.X > Transform.Position.X - (Transform.Size.Width / 2) && collider.Transform.Position.X + Transform.Size.Width < Transform.Position.X + (Transform.Size.Width / 2)) && (collider.Transform.Position.Y > Transform.Position.Y - (Transform.Size.Height / 2) && collider.Transform.Position.Y + Transform.Size.Height > Transform.Position.Y + (Transform.Size.Height / 2)))
                {
                    return true;
                }
            }
            return false;
        }
        public Collider2D GetColliding(Collider2D[] allColliders)
        {
            foreach (Collider2D collider in allColliders)
            {
                if ((collider.Transform.Position.X > Transform.Position.X - (Transform.Size.Width / 2) && collider.Transform.Position.X + Transform.Size.Width < Transform.Position.X + (Transform.Size.Width / 2)) && (collider.Transform.Position.Y > Transform.Position.Y - (Transform.Size.Height / 2) && collider.Transform.Position.Y + Transform.Size.Height > Transform.Position.Y + (Transform.Size.Height / 2)))
                {
                    return collider;
                }
            }
            return null;
        }

        /// <summary>
        /// Push will exert a force on a collider2D object. This force will cause a change in the X or Y coordinates depending on which direction the force comes from.
        /// Without a Collider2D to  touch, Push will assume this collider is in the air and check for gravity.
        /// <param name="force">
        /// force is the amount of force to use on the object
        /// </paramref>
        /// <param name="x">
        /// x is the postive(1), negative(-1), or null(0) direction to push the object
        /// </param>
        /// <param name="y">
        /// y is the postive(1), negative(-1), or null(0) direction to pull the object
        /// </param>
        /// </summary>
        public void Push(int force, int x, int y)
        {
            if(force != 0)
            {
                // this object is not moving against another object therefore
                // we won't worry about whether it has surface friction or not.

                // we will set a new position for x and y based on the force * mass * x or y
                Transform.Position += new Vector(x * force, y * force, 0);
                Thread.Sleep(100);
                Push(force - 1, x, y);
            }
        }
        /// <summary>
        /// Push will exert a force on a collider2D object. This force will cause a change in the X or Y coordinates depending on which direction the force comes from.
        /// </summary>
        /// <param name="force"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="touching"></param>
        public void Push(int force, int x, int y, Collider2D touching)
        {
            int forceCoefficient = 0;
            if (force != 0)
            {
                if (touching.IsSlippery)
                {
                    forceCoefficient += 1;
                }
                if (this.IsSlippery)
                {
                    forceCoefficient += 1;
                }

                // we will set a new position for x and y based on the force * mass * x or y
                Transform.Position += new Vector(x * force, y * force, 0);
                Thread.Sleep(100);
                Push(force - 1, x, y);
            }
        }
        /// <summary>
        /// Pull will exert a force on a collider2D object. This force will cause a change in the X or Y coordinates depending on which direction the force comes from.
        /// <param name="force">
        /// force is the amount of force to use on the object
        /// </param>
        /// <param name="x">
        /// x is the postive(1), negative(-1), or null(0) direction to pull the object
        /// </param>
        /// <param name="y">
        /// y is the postive(1), negative(-1), or null(0) direction to pull the object
        /// </param>
        /// </summary>
        public void Pull(int force, int x, int y)
        {

        }
    }
}
