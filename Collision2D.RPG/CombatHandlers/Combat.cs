using Collision2D.RPG.Entities;
using Collision2D.RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG.CombatHandlers
{
    /// <summary>
    /// Base class containing functions for attacking, damage, and defense calculations
    /// </summary>
    public class Combat
    {
        protected Adventurer player;
        protected IEnumerable<PartyMember> playerParty;
        protected IEnumerable<Monster> monsters;

        public Combat(Adventurer player, IEnumerable<PartyMember> playerParty, IEnumerable<Monster> monsters)
        {
            this.player = player;
            this.playerParty = playerParty;
            this.monsters = monsters;
        }

        public void Attack(ref Entity attacker, ref Entity defender)
        {
            if (attacker != null)
            {
                int damage = calculate_damage(attacker, defender);
                if (damage > 0)
                {
                    // add an attribute modifier for the damage taken
                    // first we want to see if there is already a modifier named 'dmg'
                    if (defender.AttributeModifiers != null && defender.AttributeModifiers.Count() > 0)
                    {
                        var dmg = (from d in attacker.AttributeModifiers where d.Name == "dmg" select d).First();
                        if (dmg != null)
                        {
                            // we need to remove this damage modifier from the list
                            defender.AttributeModifiers = defender.AttributeModifiers.Where(x => x != dmg);
                            // increase the damage of the copy that we created by the value we just calculated
                            dmg.CurrentHP -= damage;
                            // add the copy with the new damage back into the attributes
                            defender.AttributeModifiers.Append(dmg);
                        }
                        else defender.AttributeModifiers.Append(new Attributes() { CurrentHP = -damage, Name = "dmg" });
                    }

                }
            }

        }

        protected int calculate_damage(Entity attacker, Entity defender)
        {
            // calculate the attackers roll to hit the defender, factoring in armed or unarmed proficiencies
            int hit = attacker.AttackCheck();
            if (hit >= defender.TotalAttributes.Defence)
            {
                // the attack roll is higher than the defenders defence which means the attack is succesful
                // if the attacker has a main hand and offhand weapon attached, attack with both weapons
                int damage = 0;

                Weapon weapon = attacker.MainHand as Weapon;
                damage += (Collision2D.Utils.Helpers.Random.Next(1, (int)weapon.DamageDice) + weapon.DamageModifier);

                if (attacker.OffHand != null)
                {
                    Weapon off = attacker.MainHand as Weapon;
                    damage += (Collision2D.Utils.Helpers.Random.Next(1, (int)off.DamageDice) + off.DamageModifier);
                }
                return damage;
            }
            else return 0;
        }
    }
}
