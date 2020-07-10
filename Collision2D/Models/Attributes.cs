using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.Utils
{
    public class Attributes
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int CurrentHP { get; set; }
        public int Exp { get; set; }
        public int CurrentExp { get; set; }

        public float Speed { get; set; }
        public static Attributes operator +(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                HP = a.HP + b.HP,
                CurrentHP = a.CurrentHP + b.CurrentHP,
                Exp = a.Exp + b.Exp,
                CurrentExp = a.CurrentExp + b.CurrentExp,

                Speed = a.Speed + b.Speed,
            };
        }
        public static Attributes operator -(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                HP = a.HP - b.HP,
                CurrentHP = a.CurrentHP - b.CurrentHP,
                Exp = a.Exp - b.Exp,
                CurrentExp = a.CurrentExp - b.CurrentExp,

                Speed = a.Speed - b.Speed,
            };
        }
    }
}
