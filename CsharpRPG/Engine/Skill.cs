using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _2D_Graphics_Engine.Engine;

namespace CsharpRPG.Engine
{
    public class Skill
    {
        Random rand = new Random();
        public int buffamnt = 0;

        public int ID { get; set; }
        public string Name { get; set; }
        public Bitmap Image { get; set; }
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

        public Skill(int id, string name, Bitmap img, string targetVar, int buff, int type)
        {
            ID = id;
            Name = name;
            Image = img;
            buffamnt = buff;
            TargetVariable = targetVar;
            SkillMaxExp = 1000;

            SkillType = type;
        }
        public Skill(Skill skill)
        {
            ID = skill.ID;
            Name = skill.Name;
            Image = skill.Image;
            buffamnt = skill.buffamnt;
            TargetVariable = skill.TargetVariable;
            SkillMaxExp = skill.SkillMaxExp;
            SkillType = skill.SkillType;
        }
        public Skill()
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

                PropertyInfo var = Target.FindVariable("Strength");
                //int str = (int)var.GetValue();
                //Target.FindVariable("Strength").SetValue(var, str + Buffamnt);
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
