using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class Collider2D
    {
        public int Width { get; set; }
        public int Height { get; set; }

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
            return ((collider.Transform.Position.X > Transform.Position.X - (Width / 2) && collider.Transform.Position.X + Width < Transform.Position.X + (Width / 2)) && (collider.Transform.Position.Y > Transform.Position.Y - (Height / 2) && collider.Transform.Position.Y + Height > Transform.Position.Y + (Height / 2)));
        }

        /// <summary>
        /// Fall is a recursive function that will subtract 1 from Y_1 and Y_2 until the object collides with another
        /// </summary>
        /// <param name="collider">The object to collide with</param>
        public void Fall(Collider2D collider)
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
        public void Push(Vector acc)
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
