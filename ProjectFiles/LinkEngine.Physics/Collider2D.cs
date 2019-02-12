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

        /// <summary>
        /// This function will take the current collider's position and size and check to see if it is colliding with another collider object.
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public bool isColliding(Collider2D collider)
        {
            return ((collider.Transform.Position.X > Transform.Position.X - (Transform.Size.Width / 2) && collider.Transform.Position.X + Transform.Size.Width < Transform.Position.X + (Transform.Size.Width / 2)) && (collider.Transform.Position.Y > Transform.Position.Y - (Transform.Size.Height / 2) && collider.Transform.Position.Y + Transform.Size.Height > Transform.Position.Y + (Transform.Size.Height / 2)));
        }

        /// <summary>
        /// Fall is a recursive function that will subtract 1 from Y_1 and Y_2 until the object collides with another
        /// </summary>
        /// <param name="collider">The object to collide with</param>
        public bool Fall(Collider2D[] colliders)
        {
            if (HasGravity && !IsGrounded)
            {
                Transform.Move(Velocity.Mulitply(new Vector(1,-1,0)));

                foreach (Collider2D collider in colliders)
                {
                    if (!isColliding(collider))
                    {
                        Fall(colliders);
                    }
                    else
                    {
                        IsGrounded = true;
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Push is a threaded function that adds acceleration to the x and y coordinates
        /// Push will check if the object is grounded and if it has friction to take into account the deceleration of the collider
        /// </summary>
        /// <param name="acc">the amount to </param>
        public void Push(Vector acc, Collider2D[] colliders, int xDirection, int yDirection)
        {
            // create a new pus thread start
            ThreadStart ts = new ThreadStart(()=>push(acc, colliders, xDirection,yDirection));

            // Begin running the physics thread
            Physics.Initialize(ts);

            // end the physics thread
            Physics.StopPhysics();
        }

        // this function runs as the threadstart parameter
        void push(Vector acc, Collider2D[] colliders, int xDirection, int yDirection)
        {
            Velocity = acc;
            bool stopped = false;

            while (!stopped)
            {
                // execute physics code

                // if the collider is grounded don't reduce x velocity
                // if y is going up, acc.y should be decreasing until it reaches the maximum point
                // once the maximum point is reached the y velocity should increase, and y should start falling
                if (!IsGrounded || (Velocity.Y != 0 && yDirection == 1))
                {
                    // collider is still rising
                    Transform.Move(Velocity);

                    Velocity.Subtract(new Vector(0, -1, 0));
                }
                else
                {
                   
                    if (IsGrounded)
                    {
                        // object is just sliding on the ground
                        Transform.Move(Velocity);
                        Velocity.Subtract(new Vector(-1, 0, 0));
                    }
                    if (Velocity.Y == 0)
                    {
                        // y velocity has reached 0, begin falling
                        // Execute Fall function, will return false once collider is grounded
                        stopped = Fall(colliders);
                    }
                    
                }
                Thread.Sleep(500);
            }

        }

        public Collider2D (int x, int y, int z, int h, int w)
        {
            Transform = new Components.Transform(x, y, z, h, w);
        }
    }
}
