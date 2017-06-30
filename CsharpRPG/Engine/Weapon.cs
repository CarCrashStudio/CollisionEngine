using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Weapon : Item
    {
        int minimumDamage;
        int maximumDamage;

        public Weapon(int _id, string _name, string _namePlural, int _minDamage, int _maxDamage, int _cost, bool _MainHand, bool _OffHand, Bitmap _img) :
            base(_id, _name, _namePlural, _cost, _img)
        {
            minimumDamage = _minDamage;
            maximumDamage = _maxDamage;

            MainHand = _MainHand;
            OffHand = _OffHand;
        }

        public int MinimumDamage { get { return minimumDamage; } set { minimumDamage = value; } }
        public int MaximumDamage { get { return maximumDamage; } set { maximumDamage = value; } }
        public bool Equipped { get; set; }
        public bool MainHand { get; set; }
        public bool OffHand { get; set; }

        
    }
}
