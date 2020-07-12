using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils.Physics
{
    public enum ForceMode { FORCE, IMPULSE }
    public enum RigidBodyType { KINEMATIC, DYNAMIC }
    public class RigidBody2D
    {
        private RigidBodyType type = RigidBodyType.DYNAMIC;
        public Vector2 position = Vector2.Zero;
        public Vector2 rotation = Vector2.Zero;
        private Vector2 velocity = Vector2.Zero;
        public float mass = 1;

        public bool simulateGravity = true;
        public float gravity = 9.8f;

        public RigidBody2D(RigidBodyType type, Vector2 position, Vector2 rotation)
        {
            this.type = type;
            this.position = position;
            this.rotation = rotation;
        }
        public void RemoveVelocity()
        {
            velocity = Vector2.Zero;
        }
        public void AddForce (Vector2 force, ForceMode forceMode)
        {
            if (forceMode == ForceMode.FORCE)
                velocity += force / mass;
            else 
                velocity += force;
        }
        public void Update (GameTime gameTime)
        {
            if (simulateGravity && type == RigidBodyType.DYNAMIC)
            {
                AddForce(new Vector2(0, -gravity), ForceMode.FORCE);
                position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}
