using System.Collections.Generic;

namespace LinkEngine.Entities
{
    public class Enemy : Entity
    {
        public short Strength { get; set; }
        public short Defense { get; set; }

        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }

        public Enemy(int id, string name, int health, int maxHealth, short str, short def) : base (id, name, health, maxHealth)
        {
            Strength = str;
            Defense = def;
        }
    }
}
