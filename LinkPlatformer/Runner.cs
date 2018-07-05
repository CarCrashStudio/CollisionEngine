using LinkEngine.Components;

namespace LinkEngine.Platformer
{
    /// <summary>
    /// Runner is the player class of the Platforming Library. The Platforming Library makes great use of the physics components in LinkEngine.Compnents.
    /// </summary>
    public class Runner : Entities.Player
    {
        public int Speed { get; set; }
        public Vector Acceleration { get; set; }

        /// <summary>
        /// Intializes a new Runner
        /// </summary>
        public Runner () : base (0, "Runner", 100, 100)
        {
            collider = new Collider2D();
            collider.Transform.Position = new Vector(0,0,0);
        }

        /// <summary>
        /// Jump will call collider.Push to push the player into the air, then call Fall to bring him back to the ground
        /// </summary>
        /// <param name="ground"></param>
        public void Jump(Collider2D ground)
        {
            if (collider.IsGrounded)
            {
                // Calls Push function which will cause the player to be pushed into the air
                collider.Push(new Vector(0, 3, 0));

                // Once Push has finished running, call Fall and have the player come back down
                collider.Fall(ground);
            }
        }
    }
}
