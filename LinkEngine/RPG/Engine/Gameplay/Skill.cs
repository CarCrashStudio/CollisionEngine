using System;
using System.Reflection;

namespace RPG
{
    public class Skill
    {
        Random rand = new Random();
        public int buffamnt = 0;

        public int ID { get; set; }
        public string Name { get; set; }

        public int BuffAmount { get { return buffamnt * SkillLevel; } } // Amount to buff TargetVariable
        public string TargetVariable { get; set; } // The Target Entity's Health, Strength, Defense, etc. to be buffed or debuffed

        public int SkillLevel { get; set; } // The level of the skill, incresing buff and debuff amounts
        public int SkillExp { get; set; } // exp gained by using skill in combat
        public int SkillMaxExp { get; set; } // amount of exp till level up
        public int SkillType { get; set; }

        public enum Types
        {
            Attack=0,
            Buff=1,
            Debuff=2,
        }

        public Skill(int id, string name, string targetVar, int buff, int type)
        {
            ID = id;
            Name = name;
            buffamnt = buff;
            TargetVariable = targetVar;
            SkillMaxExp = 1000;

            SkillType = type;
        }
        public Skill(Skill skill)
        {
            ID = skill.ID;
            Name = skill.Name;
            buffamnt = skill.buffamnt;
            TargetVariable = skill.TargetVariable;
            SkillMaxExp = skill.SkillMaxExp;
            SkillType = skill.SkillType;
        }
        public Skill()
        {

        }

        /// <summary>
        /// Target a variable on he Entity class and buff it
        /// </summary>
        /// <param name="Caster">Entity using the skill</param>
        /// <param name="Target">The Target of the skill</param>
        public void Use(Entity Caster, Entity Target)
        {
            if(SkillType == (int)Types.Attack)
            {
                Target.Health -= Damage(Caster, Target);
            }
            else
            {
                var property = typeof(Entity).GetProperty(TargetVariable);
                var value = (int)property.GetValue(property, null);
                property.SetValue(Target, value + BuffAmount, null);
            }

            SkillExp += rand.Next(100);
            if(SkillExp >= SkillMaxExp)
            {
                LevelUp();
            }           
        }

        /// <summary>
        /// Level up the skill
        /// </summary>
        void LevelUp()
        {
            SkillLevel++;
            SkillExp = 0;
        }

        /// <summary>
        /// Calculate the amount of damage to deal in an attack
        /// </summary>
        /// <param name="attacker">The Entity dealing the attack</param>
        /// <param name="defender">The Entity defenging itself</param>
        /// <returns>Damgae value</returns>
        int Damage(Entity attacker, Entity defender)
        {
            int temp = rand.Next(attacker.Strength);
            temp -= rand.Next(defender.Defense);
            if (temp < 0)
            {
                temp = 1;
            }
            return temp;
        }
    }
}
