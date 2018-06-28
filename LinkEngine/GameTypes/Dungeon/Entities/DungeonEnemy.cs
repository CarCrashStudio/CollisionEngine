namespace LinkEngine.Dungeon
{
    public class DungeonEnemy : Entities.Enemy
    {
        System.Random rand = new System.Random();
        public DungeonEnemy (int id, string name, int health, int maxHealth, short str, short def) : 
            base(id, name, health, maxHealth, str, def)
        {
        }

        /// <summary>
        /// Will attack the player
        /// </summary>
        /// <param name="target">The Player</param>
        public void Attack(DungeonPlayer target)
        {
            // Randomize how much damage the player will do
            int damage = rand.Next(Strength);

            // randomize how much defense the target will do
            int defense = rand.Next(target.Defense);

            // if defense is greater than damage, attack misses
            if (damage > defense)
            {
                // subtract the defense from the damage
                damage -= defense;
            }

            // reduce target's health by damage value
            target.Health -= damage;
        }
    }
}
