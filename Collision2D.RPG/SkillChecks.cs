using System;
using Collision2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.RPG.Models
{
    // This is a class containing Extension Functions for the Entity class.
    // These functions are used to perform various skill checks
    public static class SkillChecks
    {
        #region BaseChecks
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int StrengthCheck (this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);
            float check = roll + me.TotalAttributes.Strength;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int DexterityCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);
            float check = roll + me.TotalAttributes.Dexterity;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int ConstitutionCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);
            float check = roll + me.TotalAttributes.Constitution;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int IntelligenceCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);
            float check = roll + me.TotalAttributes.Intelligence;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int WisdomCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);
            float check = roll + me.TotalAttributes.Wisdom;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int CharismaCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);
            float check = roll + me.TotalAttributes.Charisma;
            return (int)check;
        }
        /// <summary>
        /// Rolls a d(int)Dice.d20 then adds weapon proficiency modifiers
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int AttackCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);


            // next we check if the current entity is attacking unarmed or not
            float check = 0;
            if (me.IsUnarmed)
                // unarmed attack is calculate by roll + proficiency + strength
                check = roll + me.TotalAttributes.Proficiency + me.TotalAttributes.Strength;
            else
            {
                // we will create a temporary check variable to store the largest check value
                float temp_check = 0;

                // we need to get the currently equipped weapon
                Weapon weapon = me.MainHand as Weapon;

                // if the entity has an off hand weapon, add off hand bonus + proficiency(only if proficient)
                if(me.OffHand != null)
                {
                    Weapon off = me.OffHand as Weapon;

                    if (me.WeaponProficiencies.Contains(off.Type))
                        temp_check += me.TotalAttributes.Proficiency + off.OffHandBonus;
                    else
                    // the entity is not proficient and therefore the proficiency modifier is not added on
                        temp_check += off.OffHandBonus;
                }


                // check if the entity is proficient with this kind of weapon
                if (me.WeaponProficiencies.Contains(weapon.Type))
                {
                    // get the type of weapon to determine attribute modifier
                    if (weapon.Type == WeaponType.MartialRange || weapon.Type == WeaponType.SimpleRange)
                        // range weapons use dexterity
                        temp_check = roll + me.TotalAttributes.Proficiency + me.TotalAttributes.Dexterity;
                    else
                        // melee weapons use strength
                        temp_check = roll + me.TotalAttributes.Proficiency + me.TotalAttributes.Strength;
                }
                else
                // the entity is not proficient and therefore the proficiency modifier is not added on
                {
                    // get the type of weapon to determine attribute modifier
                    if (weapon.Type == WeaponType.MartialRange || weapon.Type == WeaponType.SimpleRange)
                        // range weapons use dexterity
                        temp_check = roll + me.TotalAttributes.Dexterity;
                    else
                        // melee weapons use strength
                        temp_check = roll + me.TotalAttributes.Strength;
                }

                // set the check to the temp check if it is higher than the current check
                if (temp_check > check)
                    check = temp_check;
            }
            return (int)check;
        }
        #endregion
        #region SkillChecks
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int PerceptionCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);

            float check;
            if (me.SkillMasteries.Contains(Skills.Perception))
                check = roll + me.TotalAttributes.Wisdom + (me.TotalAttributes.Proficiency * 2);
            else if (me.SkillProficiencies.Contains(Skills.Perception))
                check = roll + me.TotalAttributes.Wisdom + me.TotalAttributes.Proficiency;
            else
                check = roll + me.TotalAttributes.Wisdom;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int AthleticsCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);

            float check;
            if (me.SkillMasteries.Contains(Skills.Athletics))
                check = roll + me.TotalAttributes.Wisdom + (me.TotalAttributes.Proficiency * 2);
            else if (me.SkillProficiencies.Contains(Skills.Athletics))
                check = roll + me.TotalAttributes.Dexterity + me.TotalAttributes.Proficiency;
            else
                check = roll + me.TotalAttributes.Dexterity;
            return (int)check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="me"></param>
        /// <param name="Target"></param>
        public static int PerformanceCheck(this Entities.Entity me)
        {
            // get a randomroll between 1 and 20 like rolling a d20
            float roll = Utils.Helpers.Random.Next(1, 20);

            float check;
            if (me.SkillMasteries.Contains(Skills.Performance))
                check = roll + me.TotalAttributes.Charisma + (me.TotalAttributes.Proficiency * 2);
            else if (me.SkillProficiencies.Contains(Skills.Performance))
                check = roll + me.TotalAttributes.Charisma + me.TotalAttributes.Proficiency;
            else
                check = roll + me.TotalAttributes.Charisma;
            return (int)check;
        }
        #endregion
    }
}
