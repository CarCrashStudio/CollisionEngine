using LinkEngine.Components;
using System.Collections.Generic;

namespace LinkEngine.Entities
{
    public class Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Facing { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public BoxCollider2D collider { get; set; }

        public Entity (int id, string name, int health, int maxHealth)
        {
            ID = id;
            Name = name;
            health = Health;
            MaxHealth = maxHealth;
        }

        /// <summary>
        /// Change the entities X and Y coordinates
        /// </summary>
        /// <param name="x">The value to change the X coordinate</param>
        /// <param name="y">The Value to change the Y coordinate</param>
        public void Move(int x, int y)
        {
            // Move the entity according to what is put in the parameters, +1,-1,0
            collider.X_1 += x;
            collider.X_2 += x;

            collider.Y_1 += y;
            collider.Y_2 += y;
        }

        /// <summary>
        /// Check if the entity health is 0
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsDead()
        {
            if (Health <= 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
