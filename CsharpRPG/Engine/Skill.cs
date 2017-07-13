using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpRPG.Engine
{
    public class Skill : ScreenObject
    {
        Random rand = new Random();
        public int buffamnt = 0;

        public int Buffamnt { get { return buffamnt * SkillLevel; } } // Amount to buff TargetVariable
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

        public Skill(int id, string name, Bitmap img, string targetVar, int buff, int type) : 
            base(id,name,img)
        {
            buffamnt = buff;
            TargetVariable = targetVar;
            SkillMaxExp = 1000;

            SkillType = type;
        }
        public Skill(Skill skill) :
            base(skill.ID, skill.Name, skill.Image)
        {
            buffamnt = skill.buffamnt;
            TargetVariable = skill.TargetVariable;
            SkillMaxExp = skill.SkillMaxExp;
        }
        public Skill() : base(100000, "", new Bitmap(1, 1))
        {

        }

        public void Use(Entity Caster, Entity Target)
        {
            if(SkillType == (int)Types.Attack)
            {
                Target.Health -= Damage(Caster, Target);
            }
            else
            {
                Target.FindVariable(TargetVariable).SetValue(Target.FindVariable(TargetVariable), (int)Target.FindVariable(TargetVariable).GetValue(Target.FindVariable(TargetVariable)) + Buffamnt);
            }
            SkillExp += rand.Next(100);
            if(SkillExp >= SkillMaxExp)
            {
                LevelUp();
            }           
        }
        //void CalculateMaxExp()
        //{
        //    SkillMaxExp *= SkillLevel;
        //}
        void LevelUp()
        {
            SkillLevel++;
            SkillExp = 0;
            //CalculateMaxExp();
        }
        int Damage(Entity attacker, Entity defender)
        {
            int temp = rand.Next(attacker.MaximumDamage);
            temp -= rand.Next(defender.MaximumDefense);
            if (temp < 0)
            {
                temp = 1;
            }
            return temp;
        }
    }
}
