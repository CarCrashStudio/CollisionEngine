namespace LinkEngine.Components
{
    public class BoxCollider2D : Collider2D
    {
        public int Height { get; set; }

        public int Width { get; set; }

        /// <summary>
        /// Checks to see if this object is colliding with another collision object
        /// </summary>
        /// <param name="collider">the object to collide with</param>
        /// <returns>Returns true if the objects are colliding</returns>
        public bool isColliding(Collider2D collider)
        {
            return ((collider.Transform.Position.X > Transform.Position.X - (Width / 2) && collider.Transform.Position.X + Width < Transform.Position.X + (Width / 2)) && (collider.Transform.Position.Y > Transform.Position.Y - (Height / 2) && collider.Transform.Position.Y + Height > Transform.Position.Y + (Height / 2)));
        }

        /// <summary>
        /// Fall is a recursive function that will subtract 1 from Y_1 and Y_2 until the object collides with another
        /// </summary>
        /// <param name="collider">The object to collide with</param>
        public void Fall (Collider2D collider)
        {
            if (HasGravity && !IsGrounded)
            {
                Transform.Position.Y--;

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

        /// <summary>
        /// Push is a recursive function that adds acceleration to the x and y coordinates
        /// Push will check if the object is grounded and if it has friction to take into account the deceleration of the collider
        /// </summary>
        /// <param name="acc">the amount to </param>
        public void Push (Vector acc)
        {
            //Acceleration = acc;
            //Transform.position.Add()

            //if (IsGrounded)
            //{
            //    if (HasFriction)
            //    {
            //        // if the object is on the ground and has friction. it will decelerate faster
            //        Acceleration.Subtract(new Vector (-3, -3, 0));
            //    }
            //}
        }
    }
}
