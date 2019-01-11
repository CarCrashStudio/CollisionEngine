using LinkEngine.Components;
using System.Collections.Generic;

namespace LinkEngine.Entities
{
    public class Entity : GameObject
    {
        public int ID { get; set; }
        public string Facing { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public Entity (int id, string name, int health, int maxHealth)
        {
            ID = id;
            Name = name;
            health = Health;
            MaxHealth = maxHealth;
            Components = new List<Component>();
            Components.Add(new Collider2D(0, 0, 0, 0, 0));
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
