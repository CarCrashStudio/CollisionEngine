using LinkEngine.Entities;
using LinkEngine.Gameplay.Modifiers;
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
        public void MonsterAttack (Enemy Attacker, Player Defender)
        {
            // Check if the defender has an agility or luck Ability
            if (Defender.Luck > 0 || Defender.Agility > 0)
            {
                // check for any special abilities
                // if the luck or agility Ability is high enough, dodge the attack
                canDodge = Defender.RPG.LuckCheck(Attacker.Strength, Defender.RPG.LuckModifiers.ToArray());
                canDodge = Defender.RPG.AgilityCheck(Attacker.Strength, Defender.RPG.AgilityModifiers.ToArray());

                // if the defender has a higher strength or endurance, he can block the attack
                canBlock = Defender.RPG.StrengthCheck(Attacker.Strength, Defender.RPG.StrengthModifiers.ToArray());
                canBlock = Defender.RPG.EnduranceCheck(Attacker.Strength, Defender.RPG.EnduranceModifiers.ToArray());
            }

            // if the defender can't dodge the attack
            if (!canDodge || !canBlock)
            {
                // Deal damage to defender
                // Defenders endurance and agility has a chance counteract the damage
                damageAmount = (damage(Attacker.Strength, null) - PlayerBlock(Defender.RPG.Endurance, Defender.RPG.Agility, Defender.RPG.EnduranceModifiers, Defender.RPG.AgilityModifiers));

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
        public void PlayerAttack (Enemy Defender, Player Attacker)
        {
            // Check if the defender has an agility or luck Ability
            if (Attacker.RPG.Luck > 0 || Attacker.RPG.Agility > 0)
            {
                // check for any special abilities
                // if the luck or agility Ability is high enough, Defender can't dodge the attack
                canDodge = !Attacker.RPG.LuckCheck(Defender.Strength, Attacker.RPG.LuckModifiers.ToArray());
                canDodge = !Attacker.RPG.AgilityCheck(Defender.Strength, Attacker.RPG.AgilityModifiers.ToArray());

                // if the defender has a higher strength or endurance, he can block the attack
                canBlock = !Attacker.RPG.StrengthCheck(Defender.Strength, Attacker.RPG.StrengthModifiers.ToArray());
                canBlock = !Attacker.RPG.EnduranceCheck(Defender.Strength, Attacker.RPG.EnduranceModifiers.ToArray());
            }

            // if the defender can't dodge the attack
            if (!canDodge || !canBlock)
            {
                // Deal damage to defender
                // Defenders endurance and agility has a chance counteract the damage
                damageAmount = (damage(Attacker.RPG.Strength, null) - MonsterBlock(Defender.Defense));

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
