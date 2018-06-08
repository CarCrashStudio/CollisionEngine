using System;

namespace LinkEngine.RPG
{
    class Combat
    {
        Random rand = new Random();

        bool canDodge = false;
        bool canBlock = false;

        int damageAmount = 0;

        public void Attack (Entity Attacker, Entity Defender)
        {
            // Check if the defender has an agility or luck Ability
            if (Defender.Luck > 0 || Defender.Agility > 0)
            {
                // check for any special abilities
                // if the luck or agility Ability is high enough, dodge the attack
                canDodge = Defender.LuckCheck(Attacker.Strength, Defender.LuckModifiers.ToArray());
                canDodge = Defender.AgilityCheck(Attacker.Strength, Defender.AgilityModifiers.ToArray());

                // if the defender has a higher strength or endurance, he can block the attack
                canBlock = Defender.StrengthCheck(Attacker.Strength, Defender.StrengthModifiers.ToArray());
                canBlock = Defender.EnduranceCheck(Attacker.Endurance, Defender.EnduranceModifiers.ToArray());
            }

            // if the defender can't dodge the attack
            if (!canDodge || !canBlock)
            {
                // Deal damage to defender
                // Defenders endurance and agility has a chance counteract the damage
                damageAmount = (damage(Attacker.Strength) - block(Defender.Endurance, Defender.Agility));

                // Make sure damage amount is not negative, that will give the player health
                if (damageAmount > 0)
                {
                    // deal final result to defender
                    Defender.Health -= damageAmount;
                }
            }
        }

        int damage (short str)
        {
            return rand.Next(str * 10);
        }

        int block (short end, short agi)
        {
            return rand.Next((end + agi) * 10);
        }
    }
}
