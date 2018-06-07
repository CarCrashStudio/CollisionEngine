using System;

namespace LinkEngine
{
    class Combat
    {
        Random rand = new Random();
        public void Attack (Entity Attacker, Entity Defender)
        {
            // Check if the defender has an agility or luck skill
            // if the luck or agility skill is high enough, dodge the attack
            // check for any special abilities
            // Attacker deals damage to defender
        }

        int damage (int str)
        {
            return rand.Next(str * 10);
        }
    }
}
