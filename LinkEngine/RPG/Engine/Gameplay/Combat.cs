using System;
using System.Collections.Generic;

namespace LinkEngine.RPG
{
    class Combat
    {
        Random rand = new Random();

        bool canDodge = false;
        bool canBlock = false;

        int damageAmount = 0;

        /// <summary>
        /// The Attack command to be executed whenever attack input is triggered
        /// </summary>
        /// <param name="Attacker">The entity executing the command</param>
        /// <param name="Defender">The target entity</param>
        public void Attack (RPGEntity Attacker, RPGEntity Defender)
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
                damageAmount = (damage(Attacker.Strength, Attacker.StrengthModifiers) - block(Defender.Endurance, Defender.Agility, Defender.EnduranceModifiers, Defender.AgilityModifiers));

                // Make sure damage amount is not negative, that will give the player health
                if (damageAmount > 0)
                {
                    // deal final result to defender
                    Defender.Health -= damageAmount;
                }
            }
        }

        int damage (short str, List<Modifier> strMods)
        {
            foreach(Modifier mod in strMods)
            {
                str += mod.ModifierAmount;
            }

            return rand.Next(str * 10);
        }

        int block (short end, short agi, List<Modifier> endMods, List<Modifier> agiMods)
        {
            foreach (Modifier mod in endMods)
            {
                end += mod.ModifierAmount;
            }
            foreach (Modifier mod in agiMods)
            {
                agi += mod.ModifierAmount;
            }
            return rand.Next((end + agi) * 10);
        }
    }
}
