using System;
using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class Combat
    {
        Random rand = new Random();

        bool canDodge = false;
        bool canBlock = false;

        int damageAmount = 0;

        int damage(short str, List<Modifier> strMods)
        {
            foreach (Modifier mod in strMods)
            {
                str += mod.ModifierAmount;
            }

            return rand.Next(str * 10);
        }

        int PlayerBlock(short end, short agi, List<Modifier> endMods, List<Modifier> agiMods)
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

        int MonsterBlock(short def)
        {
            return rand.Next(def);
        }

        /// <summary>
        /// The MonsterAttack command to be executed whenever aa monster initiates its attack
        /// </summary>
        /// <param name="Attacker">The entity executing the command</param>
        /// <param name="Defender">The target entity</param>
        public void MonsterAttack (Monster Attacker, Character Defender)
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
                canBlock = Defender.EnduranceCheck(Attacker.Strength, Defender.EnduranceModifiers.ToArray());
            }

            // if the defender can't dodge the attack
            if (!canDodge || !canBlock)
            {
                // Deal damage to defender
                // Defenders endurance and agility has a chance counteract the damage
                damageAmount = (damage(Attacker.Strength, null) - PlayerBlock(Defender.Endurance, Defender.Agility, Defender.EnduranceModifiers, Defender.AgilityModifiers));

                // Make sure damage amount is not negative, that will give the player health
                if (damageAmount > 0)
                {
                    // deal final result to defender
                    Defender.Health -= damageAmount;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Defender"></param>
        /// <param name="Attacker"></param>
        public void PlayerAttack (Monster Defender, Character Attacker)
        {
            // Check if the defender has an agility or luck Ability
            if (Attacker.Luck > 0 || Attacker.Agility > 0)
            {
                // check for any special abilities
                // if the luck or agility Ability is high enough, Defender can't dodge the attack
                canDodge = !Attacker.LuckCheck(Defender.Strength, Attacker.LuckModifiers.ToArray());
                canDodge = !Attacker.AgilityCheck(Defender.Strength, Attacker.AgilityModifiers.ToArray());

                // if the defender has a higher strength or endurance, he can block the attack
                canBlock = !Attacker.StrengthCheck(Defender.Strength, Attacker.StrengthModifiers.ToArray());
                canBlock = !Attacker.EnduranceCheck(Defender.Strength, Attacker.EnduranceModifiers.ToArray());
            }

            // if the defender can't dodge the attack
            if (!canDodge || !canBlock)
            {
                // Deal damage to defender
                // Defenders endurance and agility has a chance counteract the damage
                damageAmount = (damage(Attacker.Strength, null) - MonsterBlock(Defender.Defense));

                // Make sure damage amount is not negative, that will give the player health
                if (damageAmount > 0)
                {
                    // deal final result to defender
                    Defender.Health -= damageAmount;
                }
            }
        }
    }
}
