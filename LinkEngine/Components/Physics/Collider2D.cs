using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public class Collider2D
    {
        /// <summary>
        /// The transform of an object is the part of the object that moves around screen
        /// </summary>
        public Transform Transform { get; set; }

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
    }
}
