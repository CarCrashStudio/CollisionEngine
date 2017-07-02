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
        public int debuffamnt = 0;

        public int Buffamnt { get { return buffamnt * SkillLevel; } } // Amount to buff TargetVariable
        public int Debuffamnt { get { return debuffamnt * SkillLevel; } } // Amount to Debuff Targe Variable
        public string TargetVariable { get; set; } // The Target Entity's Health, Strength, Defense, etc. to be buffed or debuffed
        public int SkillLevel { get; set; } // The level of the skill, incresing buff and debuff amounts
        public int SkillExp { get; set; } // exp gained by using skill in combat
        public int SkillMaxExp { get; set; } // amount of exp till level up

        public Skill(int id, string name, Bitmap img, string targetVar, int buff, int debuff) : 
            base(id,name,img)
        {
            buffamnt = buff;
            debuffamnt = debuff;
            TargetVariable = targetVar;
            SkillMaxExp = 1000;
        }
        public Skill(Skill skill) :
            base(skill.ID, skill.Name, skill.Image)
        {
            buffamnt = skill.buffamnt;
            debuffamnt = skill.debuffamnt;
            TargetVariable = skill.TargetVariable;
            SkillMaxExp = skill.SkillMaxExp;
        }
        public void Use(Entity Caster, Entity Target)
        {
            switch (TargetVariable)
            {
                case "Health":
                    if(Buffamnt != 0)
                    {
                        Target.Health += Buffamnt;
                    }
                    if(Debuffamnt != 0)
                    {
                        Target.Health -= (Debuffamnt * Caster.MaximumDamage) - Target.MaximumDefense;
                    }
                    break;
                case "Strength":
                    if (Buffamnt != 0)
                    {
                        Target.MaximumDamage += Buffamnt;
                    }
                    if (Debuffamnt != 0)
                    {
                        Target.MaximumDamage -= Debuffamnt;
                    }
                    break;
                case "Defense":
                    if (Buffamnt != 0)
                    {
                        Target.MaximumDefense += Buffamnt;
                    }
                    if (Debuffamnt != 0)
                    {
                        Target.MaximumDefense -= Debuffamnt;
                    }
                    break;
            }
            SkillExp += rand.Next(100);
            if(SkillExp >= SkillMaxExp)
            {
                LevelUp();
            }           
        }
        void CalculateMaxExp()
        {
            SkillMaxExp *= SkillLevel;
        }
        void LevelUp()
        {
            SkillLevel++;
            SkillExp = 0;
            CalculateMaxExp();
        }
    }
    public class Buff : Skill
    {
        public Buff(int id, string name, Bitmap img, string targetvar, int buff, int debuff = 0) : 
            base(id, name, img, targetvar, buff, debuff)
        { }
    }
    public class Debuff : Skill
    {
        public Debuff(int id, string name, Bitmap img, string targetvar, int debuff, int buff = 0) :
            base(id, name, img, targetvar, buff, debuff)
        { }
    }
}
