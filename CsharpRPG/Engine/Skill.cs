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

        public int Buffamnt { get; set; }
        public int Debuffamnt { get; set; }
        public string TargetVariable { get; set; }
        public int SkillLevel { get; set; }
        public int SkillExp { get; set; }
        public int SkillMaxExp { get; set; }

        public Skill(int id, string name, Bitmap img, string targetVar, int buff, int debuff) : 
            base(id,name,img)
        {
            Buffamnt = buff;
            Debuffamnt = debuff;
            TargetVariable = targetVar;
            SkillMaxExp = 1000;
        }
        public void Use(Entity Target)
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
                        Target.Health -= Debuffamnt;
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
