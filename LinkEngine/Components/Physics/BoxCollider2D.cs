namespace LinkEngine.Components
{
    public class BoxCollider2D
    {
        /// <summary>
        /// X_1 is the TopLeft X coordinate
        /// </summary>
        public int X_1 { get; set; }

        /// <summary>
        /// Y_1 is the TopLeft Y coordinate
        /// </summary>
        public int Y_1 { get; set; }

        /// <summary>
        /// X_2 is the BottomRight X coordinate
        /// </summary>
        public int X_2 { get; set; }

        /// <summary>
        /// Y_2 is the BottomRight Y coordinate
        /// </summary>
        public int Y_2 { get; set; }

        /// <summary>
        /// If the collision object has gravity, its Y coordinates should be reduced
        /// </summary>
        public bool HasGravity { get; set; }

        /// <summary>
        /// If the collision object has friction, push forces will have less effect
        /// </summary>
        public bool HasFriction { get; set; }

        /// <summary>
        /// If the collision object is touching the ground
        /// </summary>
        public bool IsGrounded { get; set; }

        /// <summary>
        /// Checks to see if this object is colliding with another collision object
        /// </summary>
        /// <param name="collider">the object to collide with</param>
        /// <returns>Returns true if the objects are colliding</returns>
        public bool isColliding(BoxCollider2D collider)
        {
            return ((collider.X_1 > X_1 && collider.X_2 < X_2) && (collider.Y_1 > Y_1 && collider.Y_2 > Y_2));
        }

        /// <summary>
        /// Fall is a recursive function that will subtract 1 from Y_1 and Y_2 until the object collides with another
        /// </summary>
        /// <param name="collider">The object to collide with</param>
        public void Fall (BoxCollider2D collider)
        {
            if (HasGravity && !IsGrounded)
            {
                Y_1--;
                Y_2--;

                if (!isColliding(collider))
                {
                    Fall(collider);
                }
                else
                {
                    IsGrounded = true;
                }
            }
        }
    }
}
