using System.Collections.Generic;

namespace LinkEngine
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
        }
        public Entity(int id, string name, int health, int maxHealth, int w, int h) : 
            base (name, new Transform(0, 0, 0, w, h))
        {
            ID = id;
            health = Health;
            MaxHealth = maxHealth;
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
