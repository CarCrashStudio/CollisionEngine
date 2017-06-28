using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpRPG.Engine
{
    public class Potion : Item
    {
        int amountToHeal;

        public Potion(int _id, string _name, string _namePlural, int _amountToHeal, int _cost) :
            base(_id, _name, _namePlural, _cost)
        {
            amountToHeal = _amountToHeal;
        }
     
        public int AmountToBuff { get { return amountToHeal; } set { amountToHeal = value; } }
    }
}
